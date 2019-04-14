using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Dao;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYSCORE.API.Controllers
{
    [Route("api/configinfo")]
    public class ConfigInfoController : BaseController
    {
        private readonly ConfigInfoDao _configInfoDao;

        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;

        public ConfigInfoController(ConfigInfoDao configInfoDao)
        {
            _configInfoDao = configInfoDao;
        }

        /// <summary>
        /// 添加配置
        /// </summary>
        /// <param name="configInfo">configInfo.</param>
        [HttpPost, Route("add")]
        public bool Add([FromBody]ConfigInfo configInfo)
        {
            try
            {
                return _configInfoDao.Add(configInfo);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "添加配置信息失败！", configInfo.ToJSON());
                return false;
            }
        }

        /// <summary>
        /// 根据code删除
        /// </summary>
        /// <returns>The delete.</returns>
        /// <param name="code">code.</param>
        [HttpGet, Route("delete/{code}/{type}")]
        public bool Delete(string code, int type)
        {
            try
            {
                return _configInfoDao.Delete(code, type);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "删除配置信息失败！code=" + code);
                return false;
            }
        }

        /// <summary>
        /// 根据code获取数据
        /// </summary>
        /// <returns>The get.</returns>
        /// <param name="code">Code.</param>
        [HttpGet, Route("get/{code}")]
        public ConfigInfo Get(string code)
        {
            try
            {
                return _configInfoDao.Get(code);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, string.Format("获取配置信息失败！code=【{0}】", code));
                return null;
            }
        }

        /// <summary>
        /// 获取所有(分页)
        /// </summary>
        /// <returns>The products.</returns>
        [HttpPost, Route("getListByPage")]
        public PublicView GetListByPage([FromBody]PublicQuery query)
        {
            try
            {
                return _configInfoDao.GetListByPage(query);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "获取配置信息列表失败！", query.ToJSON());
                return null;
            }
        }

        /// <summary>
        /// 获取所有(不分页)
        /// </summary>
        /// <returns>The users.</returns>
        [HttpGet, Route("getlist")]
        public IEnumerable<ConfigInfo> GetList()
        {
            try
            {
                return _configInfoDao.GetList();
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "获取配置信息列表失败！");
                return null;
            }
        }

        /// <summary>
        /// 获取所有(不分页)
        /// </summary>
        /// <returns>The users.</returns>
        /// 
        [HttpGet, Route("getlistbytype/{type}")]
        public IEnumerable<ConfigInfo> GetListByType(int type)
        {
            try
            {
                return _configInfoDao.GetList(type);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "获取配置信息列表失败！type=" + type);
                return null;
            }
        }


        /// <summary>
        /// 更新配置信息
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="model">Model.</param>
        /// 
        [HttpPost, Route("update")]
        public bool Update([FromBody]ConfigInfo model)
        {
            try
            {
                return _configInfoDao.Update(model);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "更新配置信息失败！", model.ToJSON());
                return false;
            }
        }

        /// <summary>
        /// 根据名称获取配置信息
        /// </summary>
        /// <returns>The config info by name.</returns>
        /// <param name="name">Name.</param>
        /// 
        [HttpGet, Route("getbyname/{name}/{type}")]
        public ConfigInfo GetConfigInfoByName(string name, int type)
        {
            try
            {
                return _configInfoDao.GetConfigInfoByName(name, type);
            }
            catch (Exception ex)
            {
                nlog.Error(ex, "根据名称获取配置失败！name = " + name);
                return null;
            }
        }

    }
}
