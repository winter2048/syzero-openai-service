using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyZero.Application.Routing;
using SyZero.OpenAI.IApplication.Completion.Dto;
using SyZero.OpenAI.IApplication.Image.Dto;

namespace SyZero.OpenAI.IApplication.Completion
{
    public interface ICompletionAppService : IApplicationServiceBase
    {
        [ApiMethod(HttpMethod.POST)]
        Task<string> Completion(CompletionDto completionDto);
    }
}
