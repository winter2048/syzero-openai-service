using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyZero.Application.Routing;
using SyZero.OpenAI.IApplication.Image.Dto;

namespace SyZero.OpenAI.IApplication.Image
{
    public interface IImageAppService : IApplicationServiceBase
    {
        /// <summary>
        /// 生成图片
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        [ApiMethod(HttpMethod.POST)]
        Task<string[]> Generation(string msg);

        /// <summary>
        /// 编辑图片
        /// </summary>
        /// <param name="editImageDto"></param>
        /// <returns></returns>
        [ApiMethod(HttpMethod.POST)]
        Task<string[]> Edit(EditImageDto editImageDto);

        /// <summary>
        /// 重新生成
        /// </summary>
        /// <param name="editImageDto"></param>
        /// <returns></returns>
        [ApiMethod(HttpMethod.POST)]
        Task<string[]> Variation(VariationImageDto editImageDto);
    }
}
