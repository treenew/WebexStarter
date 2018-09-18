using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aoite.Web.Plugins;
using Microsoft.Extensions.Configuration;

namespace Webex.Platform.Internal
{

    public interface IMenuProvider
    {
        IReadOnlyCollection<MenuItem> Menus { get; }
        Task<IReadOnlyCollection<MenuItem>> GetCurrentMenusAsync();
    }

    public class DefaultMenuProvider : IMenuProvider
    {
        public IReadOnlyCollection<MenuItem> Menus { get; private set; }

        public virtual string SectionName { get; } = nameof(Menus);

        public DefaultMenuProvider(PluginProjectInfo pluginProject, string subpath)
        {
            pluginProject.LoadConfiguration(subpath, this.CreateMenuItems);
        }

        protected virtual void CreateMenuItems(IConfiguration configuration)
        {
            var menus = new List<MenuItem>();
            if(this.SectionName.IsNotNull()) configuration = configuration.GetSection(this.SectionName);
            configuration.Bind(menus);
            this.Menus = menus;
        }

        public virtual Task<IReadOnlyCollection<MenuItem>> GetCurrentMenusAsync() => Task.FromResult(this.Menus);
    }
}
