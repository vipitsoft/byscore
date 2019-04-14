using System;
using System.Collections.Generic;
using System.Linq;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.EntityFrameworkCore;

namespace BYSCORE.Dao.Dao
{
    public class RoleMenuDao : DaoBase
    {
        private readonly CoreDbContext _coreDbContext;

        public RoleMenuDao(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="menu">menu.</param>
        public bool Add(RoleMenu menu)
        {
            _coreDbContext.RoleMenu.Add(menu);
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据code删除
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="code">code.</param>
        public bool Delete(string code)
        {
            var model = _coreDbContext.RoleMenu.Where(t => t.IsDelete == false && t.Code == code).FirstOrDefault();
            if (model == null)
            {
                return false;
            }

            model.IsDelete = true;
            _coreDbContext.RoleMenu.Update(model);
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据code 获取
        /// </summary>
        /// <returns></returns>
        /// <param name="code">code.</param>
        public RoleMenu Get(string code)
        {
            return _coreDbContext.RoleMenu.Where(t => t.IsDelete == false && t.Code == code).FirstOrDefault();
        }

        /// <summary>
        /// 获取所有(分页)
        /// </summary>
        /// <returns>The products.</returns>
        public PublicView GetListByPage(PublicQuery query)
        {
            var list = _coreDbContext.RoleMenu.Where(w => w.IsDelete == false);

            list = list.OrderByDescending(t => t.CreatedTime).Paging<RoleMenu>(out int totalCount, query.Page, query.PageSize);

            return new PublicView { TotalCount = totalCount, RoleMenuList = list.AsNoTracking().ToList() };
        }

        /// <summary>
        /// 获取所有(不分页)
        /// </summary>
        /// <returns>.</returns>
        public IEnumerable<RoleMenu> GetList()
        {
            return _coreDbContext.RoleMenu.Where(w => w.IsDelete == false).AsNoTracking().ToList();
        }

        /// <summary>
        /// 根据roleid获取所有(不分页)
        /// </summary>
        /// <returns>.</returns>
        public IEnumerable<RoleMenu> GetListByRoleId(int id)
        {
            return _coreDbContext.RoleMenu.Where(w => w.IsDelete == false && w.RoleId == id).AsNoTracking().ToList();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <returns><c>true</c>, if product was updated, <c>false</c> otherwise.</returns>
        /// <param name="model">model.</param>
        public bool Update(RoleMenu model)
        {
            _coreDbContext.RoleMenu.Update(model);
            return _coreDbContext.SaveChanges() > 0;
        }
    }
}
