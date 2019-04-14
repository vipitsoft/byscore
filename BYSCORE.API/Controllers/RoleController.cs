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
    [Route("api/role")]
    public class RoleController : Controller
    {
        private readonly RoleDao _roleDao;
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;

        public RoleController(RoleDao roleDao)
        {
            _roleDao = roleDao;
        }

        [HttpPost, Route("add")]
        public bool Add([FromBody]Role role)
        {
            try
            {
                return _roleDao.Add(role);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "添加角色失败！", role.ToJSON());
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
            return _roleDao.GetListByPage(query);
        }

        // GET: api/values
        [HttpGet, Route("getlist")]
        public IEnumerable<Role> GetList()
        {
            return _roleDao.GetList();
        }

        // GET api/values/5
        [HttpGet, Route("get/{code}")]
        public Role Get(string code)
        {
            try
            {
                return _roleDao.Get(code);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "查询角色失败！", code);
                return new Role();
            }
        }

        // DELETE api/values/5
        [HttpGet, Route("delete/{code}")]
        public bool Delete(string code)
        {
            try
            {
                return _roleDao.Delete(code);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "删除角色失败！", code);
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
        public bool Update([FromBody]Role model)
        {
            try
            {
                return _roleDao.Update(model);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "更新角色失败！", model.ToJSON());
                return false;
            }
        }

        /// <summary>
        /// 获取角色拥有的菜单
        /// </summary>
        /// <returns>The role menus.</returns>
        [HttpGet, Route("getRoleMenus/{roleCode}")]
        public IEnumerable<Menu> GetRoleMenus(string roleCode)
        {
            try
            {
                return _roleDao.GetRoleMenus(roleCode);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "查询角色拥有的菜单失败！", roleCode);
                return new List<Menu>();
            }
        }
    }
}
