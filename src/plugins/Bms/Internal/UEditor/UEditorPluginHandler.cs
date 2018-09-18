using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aoite.Web.Plugins;
using Bms.Internal.UEditor.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Bms.Internal.UEditor
{
    public class UEditorPluginHandler : IPluginHandler
    {
        private readonly IAppStorage _appStorage;
        private readonly PluginProjectInfo _projectInfo;
        private readonly UEditorOptions _options;
        private readonly string _serverUrl;
        private readonly string _rawConfigJson;
        private readonly DateTime _createdTime;

        public int Order => 0;

        public UEditorPluginHandler(IAppStorage appStorage, PluginProjectInfo projectInfo)
        {
            this._appStorage = appStorage;
            this._projectInfo = projectInfo;
            var pathFormatter = new AuthPathFormatter(appStorage);
            var configPath = this._projectInfo.Metadate.GetValue<string>("ueditor");

            this._rawConfigJson = projectInfo.FolderProvider.GetFileInfo(configPath).CreateReadStream().ReadToEnd();
            this._options = new UEditorOptions(JObject.Parse(this._rawConfigJson), pathFormatter);
            this._serverUrl = this._options.GetString("serverUrl");
            this._createdTime = DateTime.Now;
        }

        public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
        {
            Handler action = null;
            switch(httpContext.Request.Query["action"])
            {
                case "config":
                    action = new ConfigHandler(httpContext, this._rawConfigJson, this._createdTime);
                    break;
                case "uploadimage":
                    action = new UploadHandler(httpContext, this._options, new UploadConfig()
                    {
                        AllowExtensions = this._options.GetStringList("imageAllowFiles"),
                        PathFormat = this._options.GetString("imagePathFormat"),
                        SizeLimit = this._options.GetInt("imageMaxSize"),
                        UploadFieldName = this._options.GetString("imageFieldName")
                    });
                    break;
                case "uploadvideo":
                    action = new UploadHandler(httpContext, this._options, new UploadConfig()
                    {
                        AllowExtensions = this._options.GetStringList("videoAllowFiles"),
                        PathFormat = this._options.GetString("videoPathFormat"),
                        SizeLimit = this._options.GetInt("videoMaxSize"),
                        UploadFieldName = this._options.GetString("videoFieldName")
                    });
                    break;
                case "uploadfile":
                    action = new UploadHandler(httpContext, this._options, new UploadConfig()
                    {
                        AllowExtensions = this._options.GetStringList("fileAllowFiles"),
                        PathFormat = this._options.GetString("filePathFormat"),
                        SizeLimit = this._options.GetInt("fileMaxSize"),
                        UploadFieldName = this._options.GetString("fileFieldName")
                    });
                    break;
                case "listimage":
                    action = new ListFileManager(httpContext, this._options, this._options.Formatter.Format(this._options.GetString("imageManagerListPath")), this._options.GetStringList("imageManagerAllowFiles"));
                    break;
                case "listfile":
                    action = new ListFileManager(httpContext, this._options, this._options.Formatter.Format(this._options.GetString("fileManagerListPath")), this._options.GetStringList("fileManagerAllowFiles"));
                    break;
                case "catchimage":
                    action = new CrawlerHandler(httpContext, this._options);
                    break;
                default:
                    action = new NotSupportedHandler(httpContext);
                    break;
            }
            await action.ProcessAsync();
        }

        public bool IsMatch(HttpContext httpContext)
        {
            return httpContext.Request.Path.Value.iEquals(this._serverUrl);
        }
    }
}
