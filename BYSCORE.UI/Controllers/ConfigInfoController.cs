using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Entity;
using BYSCORE.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYSCORE.UI.Controllers
{
    [Authorize]
    public class ConfigInfoController : BaseController
    {

        private readonly ConfigInfoService _configInfoService;

        private readonly LogService _logService;

        public ConfigInfoController(ConfigInfoService configInfoService, LogService logService)
        {
            _configInfoService = configInfoService;
            _logService = logService;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {

            #region 部门

            List<ConfigInfo> depts = await _configInfoService.GetListByType((int)ConfigInfoType.Department);
            ViewBag.Depts = depts;
            #endregion
            return View();
        }

        #region 部门管理
        public async Task<JsonResult> GetDepartments()
        {
            try
            {
                List<ConfigInfo> configInfos = await SetConfigInfos((int)ConfigInfoType.Department);
                ViewBag.Depts = configInfos;
                List<TreeViewModel> treeViewModels = new List<TreeViewModel>();
                List<ConfigInfo> configs = configInfos.Where(w => w.ParentId == 0).ToList();
                foreach (var item in configs)
                {
                    TreeViewModel treeViewModel = new TreeViewModel
                    {
                        Id = item.Id,
                        Code = item.Code,
                        NodeId = item.Id,
                        Text = item.Name,
                        Nodes = GetNodes(item.Id, configInfos),
                    };

                    treeViewModels.Add(treeViewModel);
                }
                return Json(new { isGet = true, Msg = "", data = treeViewModels });

            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("获取部门失败！"), ex);
                return Json(new { isGet = false, Msg = "获取部门失败!" });
            }
        }

        private List<TreeViewModel> GetNodes(int id, List<ConfigInfo> configs)
        {
            List<TreeViewModel> treeViewModels = new List<TreeViewModel>();
            List<ConfigInfo> nodeconfigs = configs.Where(w => w.ParentId == id).ToList();
            foreach (var item in nodeconfigs)
            {
                TreeViewModel treeViewModel = new TreeViewModel
                {
                    Id = item.Id,
                    Code = item.Code,
                    NodeId = item.Id,
                    Text = item.Name,
                    Nodes = GetNodes(item.Id, configs)
                };

                treeViewModels.Add(treeViewModel);
            }
            return treeViewModels.Count == 0 ? null : treeViewModels;
        }


        #endregion

        #region 区域管理
        public IActionResult AreaManage()
        {
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> GetAreas(PublicQuery query)
        {
            try
            {
                query.ConfigType = (int)ConfigInfoType.Area;
                var configInfos = await _configInfoService.GetListByPage(query);
                if (configInfos == null)
                {
                    return Json(new { isGet = false, msg = "未获取到区域！" });
                }
                return Json(new { total = configInfos.TotalCount, rows = configInfos.ConfigInfoList });

            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("获取区域失败！"), ex);
                return Json(new { isGet = false, Msg = "获取区域失败!" });
            }
        }
        #endregion


        #region 根据类型获取配置信息
        public async Task<List<ConfigInfo>> SetConfigInfos(int type)
        {
            return await _configInfoService.GetListByType(type);

        }
        #endregion


        #region 配置信息公共的增删改查接口

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The dept by code.</returns>
        /// <param name="code">Code.</param>
        public async Task<JsonResult> GetByCode(string code)
        {
            try
            {
                ConfigInfo configInfo = await _configInfoService.Get(code);

                if (configInfo == null)
                {
                    return Json(new { isGet = false, msg = "未获取到配置信息！" });
                }
                return Json(new { isGet = true, msg = "", data = configInfo });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("获取配置信息失败！"), ex);
                return Json(new { isGet = false, Msg = "获取配置信息失败!" });
            }



        }

        public async Task<JsonResult> ConfigInfoAdd(ConfigInfo configInfo)
        {
            try
            {
                configInfo.Code = Guid.NewGuid().ToString("N");
                configInfo.CreatedTime = DateTime.Now;
                bool isAdd = await _configInfoService.Add(configInfo);
                if (isAdd)
                {
                    return Json(new { isAdd = true });
                }

                return Json(new { isAdd = false, msg = "添加配置失败！" });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("添加配置失败！configinfo={0}", configInfo.ToJSON()), ex);
                return Json(new { isAdd = false, msg = "添加配置失败！" });
            }
        }

        [HttpPost]
        public async Task<JsonResult> ConfigInfoEdit(ConfigInfo configInfo)
        {
            try
            {
                bool isEdit = await _configInfoService.Update(configInfo);
                if (isEdit)
                {
                    return Json(new { isEdit = true });
                }

                return Json(new { isEdit = false, msg = "编辑配置失败！" });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("编辑配置失败！configinfo={0}", configInfo.ToJSON()), ex);
                return Json(new { isEdit = false, msg = "编辑配置失败！" });
            }
        }

        public async Task<JsonResult> ConfigInfoDel(string code, int type)
        {
            try
            {
                bool isDel = await _configInfoService.Delete(code, type);
                if (isDel)
                {
                    return Json(new { isDel = true });
                }

                return Json(new { isDel = false, msg = "删除配置失败！" });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("删除配置失败！code={0}", code), ex);
                return Json(new { isDel = false, msg = "删除配置失败！" });
            }
        }
        #endregion

    }
}
