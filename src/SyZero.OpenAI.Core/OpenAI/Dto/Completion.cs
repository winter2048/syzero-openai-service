using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyZero.OpenAI.Core.OpenAI.Dto
{
    public class CompletionRequest
    {
        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; } = null;

        [JsonProperty("max_tokens")]
        public int MaxTokens { get; set; } = 2048;

        [JsonProperty("temperature")]
        public int Temperature { get; set; } = 1;

        [JsonProperty("top_p")]
        public int TopP { get; set; } = 1;

        [JsonProperty("n")]
        public int N { get; set; } = 1;

        [JsonProperty("stream")]
        public bool Stream { get; set; } = false;

        [JsonProperty("logprobs")]
        public int? Logprobs { get; set; } = null;

        [JsonProperty("echo")]
        public bool Echo { get; set; } = false;

        [JsonProperty("stop")]
        public string Stop { get; set; } = null;

        [JsonProperty("presence_penalty")]
        public int PresencePenalty { get; set; } = 0;

        [JsonProperty("frequency_penalty")]
        public int FrequencyPenalty { get; set; } = 0;

        [JsonProperty("best_of")]
        public int BestOf { get; set; } = 1;
    }

    public class CompletionResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("object")]
        public string Object { get; set; }

        [JsonProperty("created")]
        public int Created { get; set; }

        [JsonProperty("model")]
        public string Model { get; set; }

        [JsonProperty("choices")]
        public List<Choice> Choices { get; set; }

        public class Choice
        {
            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("index")]
            public int Index { get; set; }

            [JsonProperty("logprobs")]
            public object Logprobs { get; set; }

            [JsonProperty("finish_reason")]
            public string FinishReason { get; set; }
        }
    }
}
