using Org.BouncyCastle.Crypto.Tls;
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
using SyZero.OpenAI.IApplication.Image;
using SyZero.OpenAI.IApplication.Image.Dto;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;
using OpenAI_API;
using OpenAI;
using System.Net.NetworkInformation;
using OpenAI.Files;

namespace SyZero.OpenAI.Application.Image
{
    public class ImageAppService : ApplicationService, IImageAppService
    {
        private readonly ICache _cache;
        private readonly ISyEncode _syEncode;
        private readonly IToken _token;
        private readonly IJsonSerialize _jsonSerialize;
        private readonly ILogger _logger;
        private readonly OpenAIService _openAIService;

        public ImageAppService(
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

        public async Task<string[]> Edit(EditImageDto editImageDto)
        {
            CheckPermission("");
            var imageRqt = new Core.OpenAI.Dto.ImageEditRequest()
            {
                Prompt = editImageDto.Message
            };

            if (!string.IsNullOrEmpty(editImageDto.ImageBase64))
            {
                imageRqt.Image = Convert.FromBase64String(editImageDto.ImageBase64.Split(',').Last());
            }

            if (!string.IsNullOrEmpty(editImageDto.MaskBase64))
            {
                imageRqt.Mask = Convert.FromBase64String(editImageDto.MaskBase64.Split(',').Last());
            }

            var res = await _openAIService.ImageEdit(imageRqt);

            return res.Data.Select(p => "data:image/png;base64," + p.Base64).ToArray();
        }

        public async Task<string[]> Generation(string msg)
        {
            CheckPermission("");
            var res = await _openAIService.ImageGeneration(new Core.OpenAI.Dto.ImageRequest()
            {
                Prompt = msg
            });

            return res.Data.Select(p => "data:image/png;base64," + p.Base64).ToArray();
        }

        public async Task<string[]> Variation(VariationImageDto editImageDto)
        {
            CheckPermission("");
            var imageRqt = new Core.OpenAI.Dto.ImageEditRequest();

            if (!string.IsNullOrEmpty(editImageDto.ImageBase64))
            {
                imageRqt.Image = Convert.FromBase64String(editImageDto.ImageBase64.Split(',').Last());
            }

            var res = await _openAIService.ImageVariation(imageRqt);

            return res.Data.Select(p => "data:image/png;base64," + p.Base64).ToArray();
        }
    }
}