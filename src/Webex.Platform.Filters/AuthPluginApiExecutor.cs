using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aoite.Web.Plugins;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Webex.Platform.Filters
{
    public class AuthPluginApiExecutor : IPluginExecutor<PluginApiFilterContext>
    {
        private readonly IAppStorage _appStorage;

        public int Order => PluginOrders.Auth;

        public AuthPluginApiExecutor(IAppStorage appStorage)
        {
            this._appStorage = appStorage;
        }

        public Task ExecutedAsync(PluginApiFilterContext context) => Task.CompletedTask;

        public Task ExecutingAsync(PluginApiFilterContext context)
        {
            if(context.PluginMethod.Type.Project.IsModule(Definiens.Bms) && !this.Authorize(context))
            {
                context.Unauthorized();
            }
            return Task.CompletedTask;
        }

        private bool Authorize(PluginApiFilterContext context)
        {
            if(context.PluginMethod.NoAuth) return true;

            return this._appStorage.Identity != null;
        }
    }
}
