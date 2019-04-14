using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Dao.Dao;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYSCORE.API.Controllers
{
    [Route("api/user")]
    public class UserController : BaseController
    {
        private readonly UserDao _userDao;
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;

        public UserController(UserDao userDao)
        {
            _userDao = userDao;
        }

        [HttpPost, Route("add")]
        public bool Add([FromBody]User user)
        {
            try
            {
                return _userDao.Add(user);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "添加用户失败！", user.ToJSON());
                return false;
            }
        }

        /// <summary>
        /// 获取所有用户(分页)
        /// </summary>
        /// <returns>The products.</returns>
        /// 
        [HttpPost, Route("getListByPage")]
        public PublicView GetListByPage([FromBody]PublicQuery query)
        {
            try
            {
                return _userDao.GetListByPage(query);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "获取所有用户失败！", query.ToJSON());
                return null;
            }
        }

        // GET: api/values
        [HttpGet, Route("getlist")]
        public IEnumerable<User> GetList()
        {
            return _userDao.GetList();
        }

        [HttpGet, Route("getUserNameList")]
        public List<UserSelectView> GetUserNameList()
        {
            return _userDao.GetUserNameList();
        }

        // GET api/values/5
        [HttpGet, Route("get/{code}")]
        public User Get(string code)
        {
            return _userDao.Get(code);
        }

        // DELETE api/values/5
        [HttpGet, Route("delete/{code}")]
        public bool Delete(string code)
        {
            try
            {
                return _userDao.Delete(code);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "删除用户失败！", code);
                return false;
            }

        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        /// <param name="model">model.</param>
        /// 
        [HttpPost, Route("update")]
        public bool Update([FromBody]User model)
        {
            try
            {
                return _userDao.Update(model);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "更新用户失败！", model.ToJSON());
                return false;
            }
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns>The login.</returns>
        /// <param name="user">User.</param>
        [HttpPost, Route("userlogin")]
        public User UserLogin([FromBody]User user)
        {
            try
            {
                return _userDao.UserLogin(user);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "用户登录失败！", user.ToJSON());
                return null;
            }
        }

        /// <summary>
        /// 获取用户拥有的菜单
        /// </summary>
        /// <returns>The user menus.</returns>
        [HttpGet, Route("getUserMenus/{userCode}")]
        public IEnumerable<Menu> GetUserMenus(string userCode)
        {
            try
            {
                return _userDao.GetUserMenus(userCode);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "获取用户菜单失败！", userCode);
                return null;
            }
        }

        /// <summary>
        /// 冻结账号
        /// </summary>
        /// <returns><c>true</c>, if user was frozen, <c>false</c> otherwise.</returns>
        /// <param name="user">User.</param>
        /// 
        [HttpPost, Route("freezeUser")]
        public bool FreezeUser([FromBody]User user)
        {
            try
            {
                return _userDao.FreezeUser(user);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "冻结用户失败！", user.ToJSON());
                return false;
            }
        }

        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <returns><c>true</c>, if user pwd was uped, <c>false</c> otherwise.</returns>
        /// <param name="user">User.</param>
        /// 
        [HttpPost, Route("upUserPwd")]
        public bool UpUserPwd([FromBody]User user)
        {
            try
            {
                return _userDao.UpUserPwd(user);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "修改用户密码失败！", user.ToJSON());
                return false;
            }
        }


        [HttpPost, Route("upUser")]
        public bool UpUser([FromBody]User model)
        {
            try
            {
                return _userDao.UpUser(model);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "更新用户失败！", model.ToJSON());
                return false;
            }
        }
    }
}
