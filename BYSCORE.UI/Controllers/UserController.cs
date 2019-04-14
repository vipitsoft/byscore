using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYSCORE.UI.Controllers
{
    public class UserController : Controller
    {
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;
        private readonly UserService _userService;
        private readonly ICacheService _cacheService;
        public UserController(UserService userService, ICacheService cacheService)
        {
            _userService = userService;
            _cacheService = cacheService;
        }

        #region 登录
        // GET: /<controller>/
        [AllowAnonymous, HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            nlog.Debug("进入登录");
            TempData["returnUrl"] = returnUrl;
            return View();
        }

        [AllowAnonymous, HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            ViewBag.UserName = user.UserName;
            var userver = UserVerify.LoginVerify(user);
            if (!userver.Item2)
            {
                ViewBag.Errormessage = userver.Item1;
                return View();
            }

            user.PassWord = Encryption.MD5Str(user.PassWord);
            User model = await _userService.UserLogin(user);

            if (model == null)
            {
                ViewBag.Errormessage = "登录失败";
                return View();
            }

            if (model.IsFreeze == true)
            {
                ViewBag.Errormessage = "该账号已冻结，禁止登录！";
                return View();
            }
            //用户标识
            var identity = new ClaimsPrincipal(
                new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Sid, model.Code),
                    new Claim(ClaimTypes.Name, model.UserName),
                    new Claim(ClaimTypes.Thumbprint, model.HeadImg),
                    new Claim(ClaimTypes.Role, model.Role.CName),
                    new Claim(ClaimTypes.Email, model.Email),
                    new Claim(ClaimTypes.HomePhone, model.Phone)

            }, CookieAuthenticationDefaults.AuthenticationScheme)
            );
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.Now.AddMinutes(20)
                }
            );

            // 把user信息存储缓存
            _cacheService.Add(model.Code + "-" + SysConsts.USERINFO, model);

            model.LastLoginTime = DateTime.Now;
            await _userService.Update(model);

            string returnUrl = TempData["returnUrl"]?.ToString();
            if (string.IsNullOrEmpty(returnUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return Redirect(returnUrl);
        }

        [AllowAnonymous, HttpGet]
        public async Task<IActionResult> LoginOut()
        {
            // 清除缓存
            RefreshCache();

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "user");
        }


        #endregion


        /// <summary>
        /// 刷新缓存
        /// </summary>
        private void RefreshCache()
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            string userCode = claimIdentity.Claims.FirstOrDefault(w => w.Type == ClaimTypes.Sid).Value;

            IEnumerable<string> keys = new List<string>
            {
                userCode+"-"+SysConsts.USERMENULIST,
                userCode+"-"+SysConsts.USERMENUALL,
                userCode+"-"+SysConsts.USERDATALIST,
                userCode+"-"+SysConsts.USERBUTTONLIST,
                userCode+"-"+SysConsts.USERINFO
            };

            _cacheService.RemoveAll(keys);
        }
    }
}
