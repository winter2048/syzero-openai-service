using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace SyZero.OpenAI.Core.OpenAI.Dto
{
    public class ImageRequest
    {
        [JsonProperty("prompt")]
        public string Prompt { get; set; }

        [JsonProperty("n")]
        public int N { get; set; } = 1;

        [JsonProperty("size")]
        public string Size { get; set; } = "256x256";

        [JsonProperty("response_format")]
        public string ResponseFormat { get; set; } = "b64_json";

        public ImageRequest() { }

        public ImageRequest(string prompt)
        {
            Prompt = prompt;
        }
    }

    public class ImageEditRequest : ImageRequest
    {
        [JsonProperty("image")]
        public byte[] Image { get; set; }

        [JsonProperty("mask")]
        public byte[] Mask { get; set; }
    }

    public class ImageResponse
    {
        [JsonProperty("created")]
        public string Created { get; set; }

        [JsonProperty("data")]
        public List<ImageData> Data { get; set; }
    }

    public class ImageData
    {
        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("b64_json")]
        public string Base64 { get; set; }
    }
}
