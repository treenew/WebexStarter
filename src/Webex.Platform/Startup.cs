using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aoite.Web.Plugins;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Webex.Platform
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            this.Configuration = configuration;
            this.HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var redisSection = Configuration.GetSection("Redis");
            if(redisSection.Value.IsNull())
            {
                services
                    .AddSingleton<Aoite.ILockProvider, Aoite.SimpleLockProvider>()
                    .AddDistributedMemoryCache(); //- 添加分布式缓存，用于 Session 提供程序
            }
            else
            {
                services.AddRedisCache(redisSection);
            }

            services
                .AddWebDefaults(this.Configuration)
                .AddDbEngine(this.Configuration.GetConnectionString("DatabaseConnection"))
                .AddPlatform()
                .AddSession(options =>
                {
                    //- Use session
                    options.IdleTimeout = TimeSpan.FromHours(48);
                    options.Cookie.HttpOnly = true;
                })
                .AddMvc()

                .AddPlugins(this.HostingEnvironment, opts =>
                {
                    //- Use by plugin project
                    //opts.Root = "../";

                    opts.PluginHomePath = "/bms/index.html"; //- 默认主页
#if DEBUG
                    opts.Root = "../plugins"; //- 插件目录
                    opts.FiltersFolder = "../" + typeof(Startup).Namespace + ".Filters"; //- 筛选器目录
#endif
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //- Use by platform project
            loggerFactory.AddFile(env, this.Configuration.GetSection("Logging"));

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
