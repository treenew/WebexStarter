using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
namespace Bms.Sys
{
    public class Startup : PluginStartupBase
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment) : base(configuration, hostingEnvironment)
        {
        }
    }
}
