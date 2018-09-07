using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aoite.Web;
using Aoite.Web.Plugins;
using Microsoft.Extensions.Logging;

namespace Webex.Platform.Filters
{
    public class CustomPluginBackgroundService : ScheduleBackgroundServiceBase
    {
        public CustomPluginBackgroundService(ILogger<CustomPluginBackgroundService> logger) : base(logger)
        {
#if DEBUG
            this.Timeout = TimeSpan.FromMinutes(15);
#endif
        }

        public override string Name => "自定义后台服务";

        protected override Task<TimeSpan> OnExecuteAsync(ScheduleServiceState state)
        {
            if(state.Valid())
            {
                this.Logger.LogInformation("后台服务 {0} - 当前时间为：{1}", this.Name, DateTime.Now);
                //todo: 实际业务逻辑
            }
            return Task.FromResult(TimeSpan.FromSeconds(5));
        }
    }
}
