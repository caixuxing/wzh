using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using WZH.Common.Response;

namespace WZH.Api.Filter
{
    /// <summary>
    /// 全局异常错误日志
    /// </summary>
    public class GlobalExceptionsFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        ///
        /// </summary>
        private readonly ILogger<GlobalExceptionsFilterAttribute> _loggerHelper;

        /// <summary>
        ///
        /// </summary>
        /// <param name="loggerHelper"></param>
        public GlobalExceptionsFilterAttribute(ILogger<GlobalExceptionsFilterAttribute> loggerHelper)
        {
            _loggerHelper = loggerHelper;
        }

        public override void OnException(ExceptionContext context)
        {
            var json = ApiResponse<string>.Fail("");
            json.msg = context.Exception.Message;//错误信息
            json.status = 500;//500异常
            var res = new ContentResult();

            res.Content = System.Text.Json.JsonSerializer.Serialize(json, new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            });
            context.Result = res;
            //采用log4net 进行错误日志记录
            _loggerHelper.LogError(json.msg + WriteLog(json.msg, context.Exception));
            base.OnException(context);
        }

        /// <summary>
        /// 自定义返回格式
        /// </summary>
        /// <param name="throwMsg"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string WriteLog(string throwMsg, Exception ex)
        {
            return string.Format("\r\n【自定义错误】：{0} \r\n【异常类型】：{1} \r\n【异常信息】：{2} \r\n【堆栈调用】：{3}", new object[] { throwMsg,
                ex.GetType().Name, ex.Message, ex.StackTrace });
        }
    }
}