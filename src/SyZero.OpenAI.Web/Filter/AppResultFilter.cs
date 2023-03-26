using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SyZero.OpenAI.Web.Models;

namespace SyZero.OpenAI.Web.Filter
{
    /// <summary>
    /// 异常过滤器
    /// </summary>
    public class AppResultFilter : IResultFilter, IOrderedFilter
    {
        public int Order { get; set; } = int.MaxValue - 10;


        public void OnResultExecuted(ResultExecutedContext context)
        {
            // context.Result 
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            context.Result = new JsonResult(new ResultModel((context.Result as ObjectResult)?.Value));
        }
    }



}



