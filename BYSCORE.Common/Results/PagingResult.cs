using System;
using System.Collections.Generic;
using System.Linq;

namespace BYSCORE.Common.Results
{
    /// <summary>
    /// 分页接口
    /// </summary>
    public interface IPagingResult
    {
        /// <summary>
        /// 总数
        /// </summary>
        int TotalCount { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        int PageSize { get; set; }
    }

    /// <summary>
    /// 分页接口
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public interface IPagingResult<T> : IPagingResult
    {
        /// <summary>
        /// 分页结果集
        /// </summary>
        List<T> Entities { get; set; }
    }

    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public class PagingResult<T> : IPagingResult<T>
    {
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 分页结果集
        /// </summary>
        public List<T> Entities { get; set; }

        /// <summary>
        /// 实例构造函数
        /// </summary>
        public PagingResult() { }

        /// <summary>
        /// 实例构造函数
        /// </summary>
        /// <param name="totalCount">总数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="entities">分页结果</param>
        public PagingResult(int totalCount, int pageIndex, int pageSize, IQueryable<T> entities)
        {
            TotalCount = totalCount;
            Entities = entities.ToList();
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        /// <summary>
        /// 实例构造函数
        /// </summary>
        /// <param name="totalCount">总数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页数量</param>
        /// <param name="entities">分页结果</param>
        public PagingResult(int totalCount, int pageIndex, int pageSize, IEnumerable<T> entities)
        {
            TotalCount = totalCount;
            Entities = entities.ToList();
            PageIndex = pageIndex;
            PageSize = pageSize;
        }
    }
}
