using System;
using System.Linq;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.EntityFrameworkCore;

namespace BYSCORE.Dao
{
    public class ApplicationLogDao
    {
        private readonly CoreDbContext _coreDbContext;
        public ApplicationLogDao(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 获取所有异常(分页)
        /// </summary>
        /// <returns>The products.</returns>
        public PublicView GetApplicationLogs(PublicQuery query)
        {
            var list = _coreDbContext.ApplicationLog.Where(w => w.Id > 0);

            if (query.Level.IsNotNullOrEmpty())
            {
                list = list.Where(t => t.Level == query.Level);
            }

            if (query.AddTime.IsNotNullOrEmpty())
            {
                DateTime tempTime = query.AddTime.ToDateTime();
                DateTime stime = new DateTime(tempTime.Year, tempTime.Month, tempTime.Day, 0, 0, 0);
                DateTime etime = new DateTime(tempTime.Year, tempTime.Month, tempTime.Day, 23, 59, 59);
                list = list.Where(w => w.Logged >= stime && w.Logged <= etime);
            }
            list = list.OrderByDescending(t => t.Logged).Paging(out int totalCount, query.Page, query.PageSize);

            return new PublicView { TotalCount = totalCount, ApplicationLogList = list.AsNoTracking().ToList() };
        }

        /// <summary>
        /// 获取异常详情
        /// </summary>
        /// <returns>The application log.</returns>
        /// <param name="id">Identifier.</param>
        public ApplicationLog GetApplicationLog(int id)
        {
            return _coreDbContext.ApplicationLog.Where(w => w.Id == id).FirstOrDefault();
        }
    }
}
