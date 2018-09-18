using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aoite.Web.Plugins;
using Microsoft.Extensions.Configuration;

namespace Webex.Platform.Internal
{
    public interface IMenuFactory
    {
        T Get<T>(PluginProjectInfo pluginProject, string subpath) where T : class, IMenuProvider;
    }

    public class MenuFactory : IMenuFactory
    {
        private readonly ConcurrentDictionary<string, IMenuProvider> _providers;
        private readonly IServiceProvider _services;

        public MenuFactory(IServiceProvider services)
        {
            this._services = services;
            this._providers = new ConcurrentDictionary<string, IMenuProvider>(StringComparer.OrdinalIgnoreCase);
        }

        public T Get<T>(PluginProjectInfo pluginProject, string subpath) where T : class, IMenuProvider
        {
            var key = pluginProject.FolderProvider.GetFileInfo(subpath).PhysicalPath;
            return (T)this._providers.GetOrAdd(key, k => this._services.CreateInstance<T>(pluginProject, subpath));
        }
    }
}
