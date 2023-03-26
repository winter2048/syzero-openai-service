using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyZero.OpenAI.Core.OpenAI.Dto;
using SyZero.Web.Common;
using Newtonsoft.Json;
using RestSharp;
using SyZero.Util;

namespace SyZero.OpenAI.Core.OpenAI
{
    public class OpenAIService
    {
        public async Task<ChatResponse> ChatCompletion(ChatRequest chatRequest)
        {
            var res = RestHelper.PostJson<ChatResponse>("https://api.openai.com/v1/chat/completions", JsonConvert.SerializeObject(chatRequest), $"Bearer {AppConfig.GetSection("OpenAIToken")}");
            return res.Entity;
        }

        public async Task<ImageResponse> ImageGeneration(ImageRequest chatRequest)
        {
            var res = RestHelper.PostJson<ImageResponse>("https://api.openai.com/v1/images/generations", JsonConvert.SerializeObject(chatRequest), $"Bearer {AppConfig.GetSection("OpenAIToken")}");
            return res.Entity;
        }

        public async Task<ImageResponse> ImageEdit(ImageEditRequest chatRequest)
        {
            var request = new RestRequest("https://api.openai.com/v1/images/edits", Method.Post);
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
            var request = new RestRequest("https://api.openai.com/v1/images/variations", Method.Post);
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
            var res = RestHelper.PostJson<CompletionResponse>("https://api.openai.com/v1/completions", JsonConvert.SerializeObject(completionRequest), $"Bearer {AppConfig.GetSection("OpenAIToken")}");
            return res.Entity;
        }
    }
}
