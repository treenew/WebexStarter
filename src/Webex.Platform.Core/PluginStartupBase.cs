using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aoite.Web.Plugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace System
{
    public abstract class PluginStartupBase
    {
        public PluginStartupBase(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.Configuration = configuration;
            this.HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddWebDefaults(this.Configuration)
                .AddDbEngine(this.Configuration.GetConnectionString("DatabaseConnection"))
                .AddSingleton<Aoite.ILockProvider, Aoite.SimpleLockProvider>()
                .AddDistributedMemoryCache() //- 添加分布式缓存，用于 Session 提供程序
                .AddSession()
                .AddMvc()
                .AddPlugins(this.HostingEnvironment, opts =>
                {
                    //- Use by plugin project
                    opts.Root = "../";
                    opts.IsApplicationPlugin = true;
                    opts.FiltersFolder = "../../Webex.Platform.Filters"; //-根据实际情况修改目录

                    //- Use by platform project
                    //opts.Root = "../plugins"; //- 插件目录
                    //opts.PluginHomePath = ""; //- 默认主页
                    //opts.FiltersFolder = "../" + typeof(Startup).Namespace + ".Filters"; //- 筛选器目录
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //- Use by platform project
            //loggerFactory.AddFile(env, Configuration.GetSection("Logging"));

            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseWebDefaults()
                .UseSession()
                .UsePlugins()
                .UseMvc()
                ;
        }
    }
}
