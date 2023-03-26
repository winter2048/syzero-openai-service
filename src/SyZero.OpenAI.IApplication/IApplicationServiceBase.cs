using SyZero.Application.Attributes;
using SyZero.Application.Service;

namespace SyZero.OpenAI.IApplication
{
    [DynamicWebApi]
    public interface IApplicationServiceBase : IApplicationService, IDynamicWebApi
    {
    }
}



