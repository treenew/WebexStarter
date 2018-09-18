using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Aoite.Data.Linq;

namespace System
{
    /// <summary>
    /// 定义全局扩展方法。
    /// </summary>
    public static class GlobalExtensions
    {
        /// <summary>
        /// 提供页码和分页大小，从 <see cref="ITable{TSource}"/>  创建一个 <see cref="PageData{TSource}"/>。结果的 <see cref="PageData.Total"/> 属性返回的分页序列结果前将会自动计算序列总元素数。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/> 中的元素的类型。</typeparam>
        /// <param name="source">要从其创建 <see cref="PageData{TSource}"/> 的 <see cref="ITable{TSource}"/>。</param>
        /// <param name="args">分页参数。</param>
        /// <returns>一个包含输入序列中元素的  <see cref="PageData{TSource}"/>。</returns>
        public static PageData ToPage<TSource>(this ITable<TSource> source, PageParameters args)
        {
            return source.ToPage(args.PageNumber, args.PageSize);
        }

        /// <summary>
        /// 提供页码和分页大小，从 <see cref="ITable{TSource}"/>  创建一个 <see cref="PageData{TSource}"/>。结果的 <see cref="PageData.Total"/> 属性返回的分页序列结果前将会自动计算序列总元素数。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/> 中的元素的类型。</typeparam>
        /// <param name="source">要从其创建 <see cref="PageData{TSource}"/> 的 <see cref="ITable{TSource}"/>。</param>
        /// <param name="args">分页参数。</param>
        /// <returns>一个包含输入序列中元素的  <see cref="PageData{TSource}"/>。</returns>
        public static Task<PageData> ToPageAsync<TSource>(this ITable<TSource> source, PageParameters args)
        {
            return source.ToPageRealAsync(args).ContinueWith<PageData>(r => r.Result);
        }

        /// <summary>
        /// 提供页码和分页大小，从 <see cref="ITable{TSource}"/>  创建一个 <see cref="PageData{TSource}"/>。结果的 <see cref="PageData.Total"/> 属性返回的分页序列结果前将会自动计算序列总元素数。
        /// </summary>
        /// <typeparam name="TSource"><paramref name="source"/> 中的元素的类型。</typeparam>
        /// <param name="source">要从其创建 <see cref="PageData{TSource}"/> 的 <see cref="ITable{TSource}"/>。</param>
        /// <param name="args">分页参数。</param>
        /// <returns>一个包含输入序列中元素的  <see cref="PageData{TSource}"/>。</returns>
        public static Task<PageData<TSource>> ToPageRealAsync<TSource>(this ITable<TSource> source, PageParameters args)
        {
            return source.ToPageAsync(args.PageNumber, args.PageSize);
        }
    }
}
