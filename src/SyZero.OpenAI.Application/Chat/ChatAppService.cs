﻿using Org.BouncyCastle.Crypto.Tls;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using SyZero;
using SyZero.Application.Service;
using SyZero.Cache;
using SyZero.Logger;
using SyZero.Runtime.Security;
using SyZero.Runtime.Session;
using SyZero.Serialization;
using SyZero.Web.Common;
using SyZero.OpenAI.IApplication.Chat;
using SyZero.OpenAI.Repository;
using System.Linq;
using Org.BouncyCastle.Bcpg;
using System.Net;
using System.Collections.Generic;
using SyZero.OpenAI.IApplication.Chat.Dto;
using SyZero.Client;
using System.Data;
using SqlSugar.Extensions;
using SyZero.OpenAI.Core.OpenAI;
using SqlSugar;

namespace SyZero.OpenAI.Application.Chat
{
    public class ChatAppService : ApplicationService, IChatAppService
    {
        private readonly ICache _cache;
        private readonly ISyEncode _syEncode;
        private readonly IToken _token;
        private readonly IJsonSerialize _jsonSerialize;
        private readonly ILogger _logger;
        private readonly OpenAIService _openAIService;

        public ChatAppService(
           ICache cache,
           ISyEncode syEncode,
           IToken token,
           IJsonSerialize jsonSerialize,
           ILogger logger,
           OpenAIService openAIService)
        {
            _cache = cache;
            _syEncode = syEncode;
            _token = token;
            _jsonSerialize = jsonSerialize;
            _logger = logger;
            _openAIService = openAIService;
        }

        public async Task<string> CreateSession()
        {
            CheckPermission("");
            var sessionId = Guid.NewGuid().ToString();
            var messages = new List<ChatMessageDto>();
            await _cache.SetAsync($"ChatSession:{SySession.UserId}:{sessionId}", messages);
            return sessionId;
        }

        public async Task DelSession(string sessionId)
        {
            CheckPermission("");
            if (!_cache.Exist($"ChatSession:{SySession.UserId}:{sessionId}"))
            {
                throw new SyMessageException("会话不存在！");
            }

            _cache.Remove($"ChatSession:{SySession.UserId}:{sessionId}");
        }

        public async Task<ChatSessionDto> GetSession(string sessionId)
        {
            CheckPermission("");
            if (!_cache.Exist($"ChatSession:{SySession.UserId}:{sessionId}"))
            {
                throw new SyMessageException("会话不存在！");
            }

            var messages = _cache.Get<List<ChatMessageDto>>($"ChatSession:{SySession.UserId}:{sessionId}");
            return new ChatSessionDto()
            {
                Id = sessionId,
                Messages = messages
            };
        }

        public async Task<string[]> MySession()
        {
            CheckPermission("");
            var keys = _cache.GetKeys($"ChatSession:{SySession.UserId}:*");
            return keys.Select(p => p.Split(":").Last()).ToArray();
        }

        public async Task<string> SendMessage(SendMessageDto messageDto)
        {
            CheckPermission("");
            var chatSession = await GetSession(messageDto.SessionId);
            chatSession.Messages.Add(new ChatMessageDto(MessageRoleEnum.User, messageDto.Message));

            var res =  _openAIService.ChatCompletionAsync(new Core.OpenAI.Dto.ChatRequest()
            {
                Model = "gpt-3.5-turbo",
                Messages = chatSession.Messages.Select(p => new Core.OpenAI.Dto.Message { Role = p.Role.ToString().ToLower(), Content = p.Content }).ToList()
            });

            string content = "";
            await  foreach (var item in res)
            {
                content += item.Choices[0]?.Delta?.Content;
                Console.Write(item.Choices[0]?.Delta?.Content);
            }

            chatSession.Messages.Add(new ChatMessageDto(MessageRoleEnum.Assistant, content));
            await _cache.SetAsync($"ChatSession:{SySession.UserId}:{messageDto.SessionId}", chatSession.Messages);
            // return res.Choices[0].Message.Content;
            return content;
        }
    }
}



