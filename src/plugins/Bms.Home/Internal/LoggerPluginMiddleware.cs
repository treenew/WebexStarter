using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aoite.Web.Plugins;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Bms.Home.Internal
{
    public class LoggerPluginMiddleware : IPluginMiddleware
    {
        private readonly ILogger<LoggerPluginMiddleware> _logger;

        public int Order => MiddlewareOrders.Default;

        public LoggerPluginMiddleware(ILogger<LoggerPluginMiddleware> logger)
        {
            this._logger = logger;
        }

        public Task OnErrorAsync(HttpContext httpContext, Exception exception)
        {
            this._logger.LogError(exception, "请求 {0} 发生异常。", httpContext.Request.Path.Value);
            return Task.CompletedTask;
        }

        public Task OnExecutedAsync(PluginExecutionContext context)
        {
            this._logger.LogInformation("请求 {0} 成功执行。", context.HttpContext.Request.Path.Value);
            return Task.CompletedTask;
        }

        public Task OnExecutingAsync(PluginExecutionContext context)
        {
            this._logger.LogInformation("请求 {0} 执行。", context.HttpContext.Request.Path.Value);
            return Task.CompletedTask;
        }
    }
}
