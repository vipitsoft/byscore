using System;
using System.Linq;
using BYSCORE.Common.Results;

namespace BYSCORE.Common
{
    public static class EFExtension
    {
        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="totalCount">总数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns></returns>
        public static IQueryable<T> Paging<T>(this IQueryable<T> source, out int totalCount, int pageIndex = 1, int pageSize = 10)
        {
            totalCount = source.Count();
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 20;
            }
            return source.Skip(((pageIndex - 1) * pageSize) < totalCount ? ((pageIndex - 1) * pageSize) : totalCount - (totalCount % pageSize)).Take(pageSize);
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <returns></returns>
        public static PagingResult<T> Paging<T>(this IQueryable<T> source, int pageIndex = 1, int pageSize = 20)
        {
            var totalCount = source.Count();
            if (pageIndex <= 0)
            {
                pageIndex = 1;
            }
            if (pageSize <= 0)
            {
                pageSize = 20;
            }
            return new PagingResult<T>(totalCount, pageIndex, pageSize,
                source.Skip(((pageIndex - 1) * pageSize) < totalCount ? ((pageIndex - 1) * pageSize) : totalCount - (totalCount % pageSize)).Take(pageSize));
        }
    }
}
