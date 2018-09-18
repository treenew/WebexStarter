using System;
using System.Collections.Generic;
using System.Text;

namespace Webex.Platform.Internal
{
    /// <summary>
    /// 表示一个菜单项。
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        /// 获取或设置一个值，表示菜单标题。
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 获取或设置一个值，表示菜单路径。
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 获取或设置一个值，表示子菜单。
        /// </summary>
        public List<MenuItem> Items { get; set; }
        /// <summary>
        /// 获取或设置一个值，表示扩展菜单角色。
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        public string Roles { get; set; }
    }
}
