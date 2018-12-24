using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYSCORE.UI.Controllers
{
    public class AppLogController : Controller
    {
        private AppLogService _applogService;
        private readonly LogService _logService;
        public AppLogController(AppLogService appLogService, LogService logService)
        {
            _applogService = appLogService;
            _logService = logService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetAppLogList(PublicQuery query)
        {
            try
            {
                PublicView publicView = await _applogService.GetAppLogs(query);
                return Json(new { total = publicView.TotalCount, rows = publicView.ApplicationLogList });
            }
            catch (Exception ex)
            {
                _logService.Error(new LogQuery { Message = "获取异常日志列表失败！", Exception = ex, Obj = query });
                return Json(new { total = 0, data = "" });
            }
        }


        public async Task<IActionResult> AppLogDetail(int id)
        {
            ApplicationLog model = await _applogService.GetAppLog(id);
            return View(model);
        }

    }
}
