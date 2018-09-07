using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aoite.Web.Plugins;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Webex.Platform.Filters
{
    public class AuthPluginMiddleware : IPluginMiddleware
    {
        private readonly IAppStorage _appStorage;

        public int Order => MiddlewareOrders.Auth;

        public AuthPluginMiddleware(IAppStorage appStorage)
        {
            this._appStorage = appStorage;
        }

        public Task OnErrorAsync(HttpContext httpContext, Exception exception) => Task.CompletedTask;

        public Task OnExecutedAsync(PluginExecutionContext context) => Task.CompletedTask;

        public Task OnExecutingAsync(PluginExecutionContext context)
        {
            if(context.MethodInfo.Type.Project.IsModule(Definiens.Bms) && !this.Authorize(context))
            {
                context.Unauthorized();
            }
            return Task.CompletedTask;
        }

        private bool Authorize(PluginExecutionContext context)
        {
            if(context.MethodInfo.NoAuth) return true;

            return this._appStorage.Identity != null;
        }
    }
}
