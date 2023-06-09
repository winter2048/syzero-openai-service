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
using SyZero.OpenAI.IApplication.Completion;
using SyZero.OpenAI.IApplication.Completion.Dto;

namespace SyZero.OpenAI.Application.Completion
{
    public class CompletionAppService : ApplicationService, ICompletionAppService
    {
        private readonly ICache _cache;
        private readonly ISyEncode _syEncode;
        private readonly IToken _token;
        private readonly IJsonSerialize _jsonSerialize;
        private readonly ILogger _logger;
        private readonly OpenAIService _openAIService;

        public CompletionAppService(
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

        public async Task<string> Send(CompletionDto completionDto)
        {
            CheckPermission("");
            var res = await _openAIService.Completion(new Core.OpenAI.Dto.CompletionRequest()
            {
                Model = "text-davinci-003",
                Prompt = completionDto.Message
            });
            return res.Choices[0].Text;
        }
    }
}