using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyZero.Client;
using SyZero.OpenAI.IApplication.Image.Dto;

namespace SyZero.OpenAI.IApplication.Image
{
    public class ImageAppServiceFallback : IImageAppService, IFallback
    {
        public Task<string[]> Edit(EditImageDto editImageDto)
        {
            throw new NotImplementedException();
        }

        public Task<string[]> Generation(string msg)
        {
            throw new NotImplementedException();
        }

        public Task<string[]> Variation(VariationImageDto editImageDto)
        {
            throw new NotImplementedException();
        }
    }
}
