using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SyZero.Client;
using SyZero.OpenAI.IApplication.Completion.Dto;
using SyZero.OpenAI.IApplication.Image.Dto;

namespace SyZero.OpenAI.IApplication.Completion
{
    public class CompletionAppServiceFallback : ICompletionAppService, IFallback
    {
        public Task<string> Send(CompletionDto completionDto)
        {
            throw new NotImplementedException();
        }
    }
}
