using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// 表示一个分页参数。
    /// </summary>
    public class PageParameters
    {
        /// <summary>
        /// 获取或设置一个值，表示分页索引。
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// 获取或设置一个值，表示分页大小。
        /// </summary>
        public int PageSize { get; set; }
    }
}
