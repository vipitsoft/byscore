using System;
using System.Collections.Generic;
using System.Linq;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.EntityFrameworkCore;

namespace BYSCORE.Dao.Dao
{
    public class MenuDao : DaoBase
    {
        private readonly CoreDbContext _coreDbContext;

        public MenuDao(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="menu">menu.</param>
        public bool Add(Menu menu)
        {
            _coreDbContext.Menu.Add(menu);
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据code删除
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="code">code.</param>
        public bool Delete(string code)
        {
            var model = _coreDbContext.Menu.Where(t => t.IsDelete == false && t.Code == code).FirstOrDefault();
            if (model == null)
            {
                return false;
            }

            model.IsDelete = true;
            _coreDbContext.Menu.Update(model);

            DelChild(model.Id);

            return _coreDbContext.SaveChanges() > 0;
        }

        private void DelChild(int parid)
        {
            var menus = _coreDbContext.Menu.Where(t => t.ParentId == parid).ToList();
            foreach (var item in menus)
            {
                item.IsDelete = true;
                _coreDbContext.Menu.Update(item);
                DelChild(item.Id);
            }
        }

        /// <summary>
        /// 根据code 获取
        /// </summary>
        /// <returns></returns>
        /// <param name="code">code.</param>
        public Menu Get(string code)
        {
            return _coreDbContext.Menu.Where(t => t.IsDelete == false && t.Code == code).FirstOrDefault();
        }

        /// <summary>
        /// 获取所有(分页)
        /// </summary>
        /// <returns>The products.</returns>
        public PublicView GetListByPage(PublicQuery query)
        {
            var list = _coreDbContext.Menu.Where(w => w.IsDelete == false);

            if (query.KeyWorld.IsNotNullOrEmpty())
            {
                list = list.Where(w => w.CName.Contains(query.KeyWorld));
            }
            list = list.OrderByDescending(t => t.CreatedTime).Paging<Menu>(out int totalCount, query.Page, query.PageSize);

            return new PublicView { TotalCount = totalCount, MenuList = list.AsNoTracking().ToList() };
        }

        /// <summary>
        /// 获取所有(不分页)
        /// </summary>
        /// <returns>The users.</returns>
        public IEnumerable<Menu> GetList()
        {
            return _coreDbContext.Menu.Where(w => w.IsDelete == false).AsNoTracking().ToList();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <returns><c>true</c>, if product was updated, <c>false</c> otherwise.</returns>
        /// <param name="model">model.</param>
        public bool Update(Menu model)
        {
            Menu menu = _coreDbContext.Menu.Where(w => w.Code == model.Code).FirstOrDefault();

            if (menu == null)
            {
                return false;
            }
            menu.CName = model.CName;
            menu.EName = model.EName;
            menu.Icon = model.Icon;
            menu.Level = model.Level;
            menu.ParentId = model.ParentId;
            menu.Remarks = model.Remarks;
            menu.Type = model.Type;
            menu.Url = model.Url;
            menu.Sort = model.Sort;

            _coreDbContext.Menu.Update(menu);
            return _coreDbContext.SaveChanges() > 0;
        }
    }
}
