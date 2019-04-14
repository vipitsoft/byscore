using System;
using System.Collections.Generic;
using System.Linq;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.EntityFrameworkCore;

namespace BYSCORE.Dao
{
    public class UserMenuDao : DaoBase
    {
        private readonly CoreDbContext _coreDbContext;

        public UserMenuDao(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">menu.</param>
        public bool Add(UserMenu model)
        {
            _coreDbContext.UserMenu.Add(model);
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据code删除
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="code">code.</param>
        public bool Delete(string code)
        {
            var model = _coreDbContext.UserMenu.Where(t => t.IsDelete == false && t.Code == code).FirstOrDefault();
            if (model == null)
            {
                return false;
            }

            model.IsDelete = true;
            _coreDbContext.UserMenu.Update(model);
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据code 获取
        /// </summary>
        /// <returns></returns>
        /// <param name="code">code.</param>
        public UserMenu Get(string code)
        {
            return _coreDbContext.UserMenu.Where(t => t.IsDelete == false && t.Code == code).FirstOrDefault();
        }

        /// <summary>
        /// 获取所有(分页)
        /// </summary>
        /// <returns>The products.</returns>
        public PublicView GetListByPage(PublicQuery query)
        {
            var list = _coreDbContext.UserMenu.Where(w => w.IsDelete == false);

            list = list.OrderByDescending(t => t.CreatedTime).Paging<UserMenu>(out int totalCount, query.Page, query.PageSize);

            return new PublicView { TotalCount = totalCount, UserMenuList = list.AsNoTracking().ToList() };
        }

        /// <summary>
        /// 获取所有(不分页)
        /// </summary>
        /// <returns>The users.</returns>
        public IEnumerable<UserMenu> GetList()
        {
            return _coreDbContext.UserMenu.Where(w => w.IsDelete == false).AsNoTracking().ToList();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <returns><c>true</c>, if product was updated, <c>false</c> otherwise.</returns>
        /// <param name="model">model.</param>
        public bool Update(UserMenu model)
        {
            _coreDbContext.UserMenu.Update(model);
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据userid获取所有(不分页)
        /// </summary>
        /// <returns>.</returns>
        public IEnumerable<UserMenu> GetListByUserId(int id)
        {
            return _coreDbContext.UserMenu.Where(w => w.IsDelete == false && w.UserId == id).AsNoTracking().ToList();
        }
    }
}
