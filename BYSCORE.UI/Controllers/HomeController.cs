using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BYSCORE.UI.Models;
using BYSCORE.Entity;
using BYSCORE.Common;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace BYSCORE.UI.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly LogService _logService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserService _userService;
        public HomeController(LogService logService, IHttpContextAccessor httpContextAccessor, UserService userService)
        {
            _logService = logService;
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> UserInfo()
        {
            var identity = (ClaimsIdentity)_httpContextAccessor.HttpContext.User.Identity;
            string userCode = identity.Claims.FirstOrDefault(w => w.Type == ClaimTypes.Sid).Value;
            User model = await _userService.Get(userCode);
            return View(model);
        }
    }
}
