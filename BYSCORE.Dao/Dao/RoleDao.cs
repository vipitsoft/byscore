using System;
using System.Collections.Generic;
using System.Linq;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.EntityFrameworkCore;

namespace BYSCORE.Dao.Dao
{
    public class RoleDao : DaoBase
    {
        private readonly CoreDbContext _coreDbContext;

        public RoleDao(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="role">role.</param>
        public bool Add(Role role)
        {
            _coreDbContext.Role.Add(role);

            bool isadd = _coreDbContext.SaveChanges() > 0;
            if (isadd)
            {
                if (role.MenuIds != null && role.MenuIds.Count > 0)
                {
                    List<RoleMenu> roleMenus = new List<RoleMenu>();
                    foreach (int item in role.MenuIds)
                    {
                        RoleMenu roleMenu = new RoleMenu
                        {
                            RoleId = role.Id,
                            MenuId = item
                        };
                        roleMenus.Add(roleMenu);
                    }
                    _coreDbContext.RoleMenu.AddRange(roleMenus);
                    return _coreDbContext.SaveChanges() > 0;
                }
                return isadd;
            }

            return false;
        }

        /// <summary>
        /// 根据code删除
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="code">code.</param>
        public bool Delete(string code)
        {
            var model = _coreDbContext.Role.Where(t => t.IsDelete == false && t.Code == code).FirstOrDefault();
            if (model == null)
            {
                return false;
            }

            model.IsDelete = true;
            _coreDbContext.Role.Update(model);
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据code 获取
        /// </summary>
        /// <returns></returns>
        /// <param name="code">code.</param>
        public Role Get(string code)
        {
            return _coreDbContext.Role.Where(t => t.IsDelete == false && t.Code == code).FirstOrDefault();
        }

        /// <summary>
        /// 获取所有(分页)
        /// </summary>
        /// <returns>The products.</returns>
        public PublicView GetListByPage(PublicQuery query)
        {
            var list = _coreDbContext.Role.Where(w => w.IsDelete == false);

            if (query.KeyWorld.IsNotNullOrEmpty())
            {
                list = list.Where(w => w.CName.Contains(query.KeyWorld));
            }
            list = list.OrderByDescending(t => t.CreatedTime).Paging<Role>(out int totalCount, query.Page, query.PageSize);

            return new PublicView { TotalCount = totalCount, RoleList = list.AsNoTracking().ToList() };
        }

        /// <summary>
        /// 获取所有(不分页)
        /// </summary>
        /// <returns>The users.</returns>
        public IEnumerable<Role> GetList()
        {
            return _coreDbContext.Role.Where(w => w.IsDelete == false).AsNoTracking().ToList();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <returns><c>true</c>, if product was updated, <c>false</c> otherwise.</returns>
        /// <param name="model">model.</param>
        public bool Update(Role model)
        {
            _coreDbContext.Role.Attach(model);
            _coreDbContext.Entry(model).State = EntityState.Modified;

            if (model.MenuIds != null && model.MenuIds.Count > 0)
            {
                List<RoleMenu> rolemenus = _coreDbContext.RoleMenu.Where(t => t.IsDelete == false && t.RoleId == model.Id).ToList();
                foreach (var item in rolemenus)
                {
                    _coreDbContext.RoleMenu.Remove(item);
                }

                List<RoleMenu> roleMenus = new List<RoleMenu>();

                foreach (var item in model.MenuIds)
                {
                    RoleMenu roleMenu = new RoleMenu
                    {
                        RoleId = model.Id,
                        MenuId = item
                    };
                    roleMenus.Add(roleMenu);
                }

                _coreDbContext.RoleMenu.AddRange(roleMenus);
            }
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 获取角色拥有的菜单
        /// </summary>
        /// <returns>The role menus.</returns>
        public IEnumerable<Menu> GetRoleMenus(string roleCode)
        {
            Role role = _coreDbContext.Role
                .Include(t => t.RoleMenus).Where(t => t.IsDelete == false && t.Code == roleCode).FirstOrDefault();
            if (role == null || role.RoleMenus.Count == 0)
            {
                return new List<Menu>();
            }
            List<Menu> menus = new List<Menu>();
            foreach (var item in role.RoleMenus)
            {
                Menu menu = _coreDbContext.Menu.Where(t => t.IsDelete == false && t.Id == item.MenuId)
                .OrderByDescending(t => t.CreatedTime).OrderBy(t => t.Sort).FirstOrDefault();
                if (menu != null)
                {
                    menus.Add(menu);
                }

            }

            return menus;
        }
    }
}
