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
    [Route("api/menu")]
    public class MenuController : Controller
    {
        private readonly MenuDao _menuDao;
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;

        public MenuController(MenuDao menuDao)
        {
            _menuDao = menuDao;
        }

        [HttpPost, Route("add")]
        public bool Add([FromBody]Menu menu)
        {
            try
            {
                return _menuDao.Add(menu);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "添加菜单失败！", menu.ToJSON());
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
            return _menuDao.GetListByPage(query);
        }

        // GET: api/values
        [HttpGet, Route("getlist")]
        public IEnumerable<Menu> GetList()
        {
            try
            {
                return _menuDao.GetList();
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "获取菜单失败！");
                return null;
            }
        }

        // GET api/values/5
        [HttpGet, Route("get/{code}")]
        public Menu Get(string code)
        {
            return _menuDao.Get(code);
        }

        // DELETE api/values/5
        [HttpGet, Route("delete/{code}")]
        public bool Delete(string code)
        {
            try
            {
                return _menuDao.Delete(code);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "删除菜单失败！", code);
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
        public bool Update([FromBody]Menu model)
        {
            try
            {
                return _menuDao.Update(model);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "更新菜单失败！", model.ToJSON());
                return false;
            }
        }
    }
}
