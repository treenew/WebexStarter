using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Webex.Platform;

namespace System
{
    public static class PlatformStartup
    {

        public static IServiceCollection AddPlatform(this IServiceCollection services)
        {
            //- 在此处添加公共服务依赖注入。
            return services;
        }
    }
}
