using System;
using System.Collections.Generic;
using System.Linq;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.EntityFrameworkCore;

namespace BYSCORE.Dao.Dao
{
    public class UserDao : DaoBase
    {
        private readonly CoreDbContext _coreDbContext;

        public UserDao(CoreDbContext coreDbContext)
        {
            _coreDbContext = coreDbContext;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user">user.</param>
        public bool Add(User user)
        {
            _coreDbContext.User.Add(user);

            bool isadd = _coreDbContext.SaveChanges() > 0;
            if (isadd)
            {
                if (user.MenuIds != null && user.MenuIds.Count > 0)
                {
                    List<UserMenu> usermenus = new List<UserMenu>();
                    foreach (int item in user.MenuIds)
                    {
                        UserMenu userMenu = new UserMenu
                        {
                            UserId = user.Id,
                            MenuId = item
                        };
                        usermenus.Add(userMenu);
                    }
                    _coreDbContext.UserMenu.AddRange(usermenus);
                }

                return _coreDbContext.SaveChanges() > 0;
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
            var model = _coreDbContext.User.Where(t => t.IsDelete == false && t.Code == code).FirstOrDefault();
            if (model == null)
            {
                return false;
            }

            model.IsDelete = true;
            _coreDbContext.User.Update(model);
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据code 获取
        /// </summary>
        /// <returns>The product by identifier.</returns>
        /// <param name="code">code.</param>
        public User Get(string code)
        {
            return _coreDbContext.User.Include(t => t.UserMenus).Include(t => t.Role).Include(t => t.Department).Include(t => t.Area).Where(t => t.IsDelete == false && t.Code == code).FirstOrDefault();
        }

        /// <summary>
        /// 获取所有用户(分页)
        /// </summary>
        /// <returns>The products.</returns>
        public PublicView GetListByPage(PublicQuery query)
        {
            var list = _coreDbContext.User.Include(t => t.Role).Include(t => t.Area).Include(t => t.Department)
            .Where(w => w.IsDelete == false);

            if (query.KeyWorld.IsNotNullOrEmpty())
            {
                list = list.Where(w => w.UserName.Contains(query.KeyWorld)
                    || w.Email.Contains(query.KeyWorld)
                    || w.Phone.Contains(query.KeyWorld));
            }
            list = list.OrderByDescending(t => t.CreatedTime).Paging<User>(out int totalCount, query.Page, query.PageSize);

            return new PublicView { TotalCount = totalCount, UserList = list.AsNoTracking().ToList() };
        }

        /// <summary>
        /// 获取所有用户(不分页)
        /// </summary>
        /// <returns>The users.</returns>
        public IEnumerable<User> GetList()
        {
            return _coreDbContext.User.Where(w => w.IsDelete == false).AsNoTracking().ToList();
        }

        public List<UserSelectView> GetUserNameList()
        {
            return _coreDbContext.User.Where(t => t.IsDelete == false)
            .Select(s => new UserSelectView { Id = s.Id, UserName = s.Name }).ToList();

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns><c>true</c>, if product was updated, <c>false</c> otherwise.</returns>
        /// <param name="model">model.</param>
        public bool Update(User model)
        {
            _coreDbContext.User.Attach(model);
            _coreDbContext.Entry(model).State = EntityState.Modified;

            if (model.MenuIds != null && model.MenuIds.Count > 0)
            {

                List<UserMenu> userMenus = _coreDbContext.UserMenu.Where(t => t.IsDelete == false && t.UserId == model.Id).ToList();
                foreach (var item in userMenus)
                {
                    _coreDbContext.UserMenu.Remove(item);
                }

                List<UserMenu> userMenulist = new List<UserMenu>();

                foreach (var item in model.MenuIds)
                {
                    UserMenu userMenu = new UserMenu
                    {
                        UserId = model.Id,
                        MenuId = item
                    };
                    userMenulist.Add(userMenu);
                }

                _coreDbContext.UserMenu.AddRange(userMenulist);
            }

            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="user">User.</param>
        public User UserLogin(User user)
        {

            User model = _coreDbContext.User.Include(t => t.Role).Include(t => t.Department).Include(t => t.Area)
            .Where(w =>
                              w.IsDelete == false
                              && (
                                  w.UserName == user.UserName
                                  || w.Email == user.UserName
                                  || w.Phone == user.UserName
                              )
                              && w.PassWord == user.PassWord
                ).FirstOrDefault();
            return model;

        }

        /// <summary>
        /// 获取用户拥有的菜单
        /// </summary>
        /// <returns>The user menus.</returns>
        public IEnumerable<Menu> GetUserMenus(string userCode)
        {
            User user = _coreDbContext.User
                .Include(t => t.UserMenus).Where(t => t.IsDelete == false && t.Code == userCode).FirstOrDefault();
            if (user == null || user.UserMenus.Count == 0)
            {
                return new List<Menu>();
            }
            List<Menu> menus = new List<Menu>();
            foreach (var item in user.UserMenus)
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

        /// <summary>
        /// 冻结账号
        /// </summary>
        /// <returns><c>true</c>, if user was frozen, <c>false</c> otherwise.</returns>
        /// <param name="user">User.</param>
        public bool FreezeUser(User user)
        {
            var model = _coreDbContext.User.Where(t => t.IsDelete == false && t.Code == user.Code).FirstOrDefault();
            if (model == null)
            {
                return false;
            }

            model.IsFreeze = user.IsFreeze;
            _coreDbContext.User.Update(model);
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <returns><c>true</c>, if user pwd was uped, <c>false</c> otherwise.</returns>
        /// <param name="user">User.</param>
        public bool UpUserPwd(User user)
        {
            var model = _coreDbContext.User.Where(t => t.IsDelete == false && t.Code == user.Code).FirstOrDefault();
            if (model == null)
            {
                return false;
            }

            model.PassWord = user.PassWord;
            _coreDbContext.User.Update(model);
            return _coreDbContext.SaveChanges() > 0;
        }

        public bool UpUser(User user)
        {
            var model = _coreDbContext.User.Where(t => t.IsDelete == false && t.Code == user.Code).FirstOrDefault();
            if (model == null)
            {
                return false;
            }
            if (user.UpType == "base")
            {
                model.PassWord = user.PassWord;
            }
            else if (user.UpType == "imgHead")
            {
                model.HeadImg = user.HeadImg;
            }
            _coreDbContext.User.Update(model);
            return _coreDbContext.SaveChanges() > 0;
        }
    }
}
