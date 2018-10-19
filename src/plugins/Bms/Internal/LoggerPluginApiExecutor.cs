using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aoite.Web.Plugins;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Bms.Internal
{
    public class LoggerPluginApiExecutor : IPluginExecutor<PluginApiFilterContext>
    {
        private readonly ILogger<LoggerPluginApiExecutor> _logger;

        public int Order => PluginOrders.Default;

        public LoggerPluginApiExecutor(ILogger<LoggerPluginApiExecutor> logger)
        {
            this._logger = logger;
        }

        public Task ExecutedAsync(PluginApiFilterContext context)
        {
            this._logger.LogInformation("请求 {0} 成功执行。", context.HttpContext.Request.Path.Value);
            return Task.CompletedTask;
        }

        public Task ExecutingAsync(PluginApiFilterContext context)
        {
            this._logger.LogInformation("请求 {0} 执行。", context.HttpContext.Request.Path.Value);
            return Task.CompletedTask;
        }
    }
}
