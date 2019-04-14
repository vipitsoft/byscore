using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Dao.Dao;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Mvc;
using NLog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYSCORE.API.Controllers
{
    [Route("api/rolemenu")]
    public class RoleMenuController : BaseController
    {
        private readonly RoleMenuDao _roleMenuDao;
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;

        public RoleMenuController(RoleMenuDao roleMenuDao)
        {
            _roleMenuDao = roleMenuDao;
        }

        [HttpPost, Route("add")]
        public bool Add([FromBody]RoleMenu user)
        {
            try
            {
                return _roleMenuDao.Add(user);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "添加角色权限失败！", user.ToJSON());
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
            return _roleMenuDao.GetListByPage(query);
        }

        /// <summary>
        /// 根据roleid获取所有(不分页)
        /// </summary>
        /// <returns>.</returns>
        [HttpGet, Route("getListByRoleId/{id}")]
        public IEnumerable<RoleMenu> GetListByRoleId(int id)
        {
            return _roleMenuDao.GetListByRoleId(id);
        }

        // GET: api/values
        [HttpGet, Route("getlist")]
        public IEnumerable<RoleMenu> GetList()
        {
            return _roleMenuDao.GetList();
        }

        // GET api/values/5
        [HttpGet, Route("get/{code}")]
        public RoleMenu Get(string code)
        {
            return _roleMenuDao.Get(code);
        }

        // DELETE api/values/5
        [HttpGet, Route("delete/{code}")]
        public bool Delete(string code)
        {
            try
            {
                return _roleMenuDao.Delete(code);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "删除角色权限失败！", code);
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
        public bool Update(RoleMenu model)
        {
            try
            {
                return _roleMenuDao.Update(model);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "更新角色权限失败！", model.ToJSON());
                return false;
            }
        }
    }
}
