using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using WZH.Common.Response;

namespace WZH.Api.Filter
{
    /// <summary>
    ///  Action过滤器
    /// </summary>
    public class ValidateModelActionFilter : IActionFilter
    {
        /// <summary>
        /// Action执行完后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        /// <summary>
        /// Action之前过滤验证Cmd
        /// </summary>
        /// <param name="context"></param>
        /// 
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.Values.SelectMany(x => x.Errors.Select(p => p.ErrorMessage)).ToList();
                if (errors.Any())
                {
                    context.Result = new BadRequestObjectResult(ApiResponse<object>.Fail(errors[0]));
                }
            }
        }
    }
}
