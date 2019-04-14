using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Dao;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Mvc;
using NLog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Beyondsoft.Resume.API.Controllers
{
    [Route("api/usermenu")]
    public class UserMenuController : Controller
    {
        private readonly UserMenuDao _userMenuDao;
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;

        public UserMenuController(UserMenuDao userMenuDao)
        {
            _userMenuDao = userMenuDao;
        }

        [HttpPost, Route("add")]
        public bool Add([FromBody]UserMenu user)
        {
            try
            {
                return _userMenuDao.Add(user);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "添加用户权限失败！", user.ToJSON());
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
            return _userMenuDao.GetListByPage(query);
        }

        // GET: api/values
        [HttpGet, Route("getlist")]
        public IEnumerable<UserMenu> GetList()
        {
            return _userMenuDao.GetList();
        }

        // GET api/values/5
        [HttpGet, Route("get/{code}")]
        public UserMenu Get(string code)
        {
            return _userMenuDao.Get(code);
        }

        // DELETE api/values/5
        [HttpGet, Route("delete/{code}")]
        public bool Delete(string code)
        {
            try
            {
                return _userMenuDao.Delete(code);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "删除用户权限失败！", code);
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
        public bool Update(UserMenu model)
        {
            try
            {
                return _userMenuDao.Update(model);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "更新用户权限失败！", model.ToJSON());
                return false;
            }
        }

        /// <summary>
        /// 根据userid获取所有(不分页)
        /// </summary>
        /// <returns>.</returns>
        /// 
        [HttpGet, Route("getListByUserId/{id}")]
        public IEnumerable<UserMenu> GetListByUserId(int id)
        {
            return _userMenuDao.GetListByUserId(id);
        }
    }
}
