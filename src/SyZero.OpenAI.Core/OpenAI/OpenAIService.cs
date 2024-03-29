﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyZero.OpenAI.Core.OpenAI.Dto;
using SyZero.Web.Common;
using Newtonsoft.Json;
using RestSharp;
using SyZero.Util;
using System.IO;

namespace SyZero.OpenAI.Core.OpenAI
{
    public class OpenAIService
    {
        private string baseUrl => AppConfig.GetSection("OpenAIUrl") ?? "https://api.openai.com";

        public async Task<ChatResponse> ChatCompletion(ChatRequest chatRequest)
        {
            var res = RestHelper.PostJson<ChatResponse>($"{baseUrl}/v1/chat/completions", JsonConvert.SerializeObject(chatRequest), $"Bearer {AppConfig.GetSection("OpenAIToken")}");
            return res.Entity;
        }

        public async IAsyncEnumerable<ChatResponse> ChatCompletionAsync(ChatRequest chatRequest)
        {
            var request = new RestRequest($"{baseUrl}/v1/chat/completions", Method.Post);
            var client = AutofacUtil.GetService<RestClient>();
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {AppConfig.GetSection("OpenAIToken")}");
            chatRequest.Stream = true;
            request.AddBody(JsonConvert.SerializeObject(chatRequest));
        
            var response = await client.DownloadStreamAsync(request);
            var stream = new StreamReader(response, Encoding.UTF8);
            string line;
            while ((line = stream.ReadLine()) != null)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    line = line.Substring("data: ".Length);
                    if (line != "[DONE]")
                    {
                        yield return JsonConvert.DeserializeObject<ChatResponse>(line);
                    }
                }
            }
        }

        public async Task<ImageResponse> ImageGeneration(ImageRequest chatRequest)
        {
            var res = RestHelper.PostJson<ImageResponse>($"{baseUrl}/v1/images/generations", JsonConvert.SerializeObject(chatRequest), $"Bearer {AppConfig.GetSection("OpenAIToken")}");
            return res.Entity;
        }

        public async Task<ImageResponse> ImageEdit(ImageEditRequest chatRequest)
        {
            var request = new RestRequest($"{baseUrl}/v1/images/edits", Method.Post);
            request.AlwaysMultipartFormData = true;
            request.AddHeader("Authorization", $"Bearer {AppConfig.GetSection("OpenAIToken")}");
            request.AddFile("image", chatRequest.Image, "image.png");
            if (chatRequest.Mask != null)
            {
                request.AddFile("mask", chatRequest.Mask, "mask.png");
            }
            request.AddParameter("prompt", chatRequest.Prompt);
            request.AddParameter("n", chatRequest.N);
            request.AddParameter("size", chatRequest.Size);
            request.AddParameter("response_format", chatRequest.ResponseFormat);
            var res = await RestHelper.ExecuteAsync<ImageResponse>(request);
            return res;
        }

        public async Task<ImageResponse> ImageVariation(ImageEditRequest chatRequest)
        {
            var request = new RestRequest($"{baseUrl}/v1/images/variations", Method.Post);
            request.AlwaysMultipartFormData = true;
            request.AddHeader("Authorization", $"Bearer {AppConfig.GetSection("OpenAIToken")}");
            request.AddFile("image", chatRequest.Image, "image.png");
            request.AddParameter("n", chatRequest.N);
            request.AddParameter("size", chatRequest.Size);
            request.AddParameter("response_format", chatRequest.ResponseFormat);
            var res = await RestHelper.ExecuteAsync<ImageResponse>(request);
            return res;
        }

        public async Task<CompletionResponse> Completion(CompletionRequest completionRequest)
        {
            var res = RestHelper.PostJson<CompletionResponse>($"{baseUrl}/v1/completions", JsonConvert.SerializeObject(completionRequest), $"Bearer {AppConfig.GetSection("OpenAIToken")}");
            return res.Entity;
        }
    }
}
