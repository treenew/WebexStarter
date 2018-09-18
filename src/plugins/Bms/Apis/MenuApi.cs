using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aoite.Web.Plugins;
using Microsoft.Extensions.Configuration;
using Webex.Platform.Internal;

namespace Bms.Apis
{
    public class MenuApi
    {
        private readonly PluginProjectInfo _pluginProject;
        private readonly IMenuFactory _menuFactory;

        public MenuApi(PluginProjectInfo pluginProject, IMenuFactory menuFactory)
        {
            this._pluginProject = pluginProject;
            this._menuFactory = menuFactory;
        }

        public async Task<IResult<IReadOnlyCollection<MenuItem>>> GetMenus()
        {
            var provider = this._menuFactory.Get<DefaultMenuProvider>(this._pluginProject, this._pluginProject.Metadate.GetValue<string>("menus"));
            return Result.Success(await provider.GetCurrentMenusAsync());
        }
    }
}
