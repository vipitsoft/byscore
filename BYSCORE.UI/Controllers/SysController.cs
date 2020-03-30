using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYSCORE.UI.Controllers
{
    [Authorize]
    public class SysController : BaseController
    {
        private readonly RoleService _roleService;

        private readonly LogService _logService;

        private readonly MenuService _menuService;

        private readonly RoleMenuService _roleMenuService;

        private readonly UserMenuService _userMenuService;

        private readonly UserService _userService;

        private readonly IOptions<AppSettings> _settings;

        private AppSettings _appSettings;

        private ConfigInfoService _configInfoService;

        private readonly ICacheService _cacheService;

        public SysController(RoleService roleService
                            , LogService logService
                            , MenuService menuService
                            , RoleMenuService roleMenuService
                            , UserMenuService userMenuService
                            , UserService userService
                            , IOptions<AppSettings> settings
                            , ConfigInfoService configInfoService
                            , ICacheService cacheService)
        {
            _roleService = roleService;
            _logService = logService;
            _menuService = menuService;
            _roleMenuService = roleMenuService;
            _userMenuService = userMenuService;
            _userService = userService;
            _settings = settings;
            _appSettings = _settings.Value;
            _configInfoService = configInfoService;
            _cacheService = cacheService;
        }

        // GET: /<controller>/
        public IActionResult UserList()
        {
            return View();
        }


        #region 角色管理
        public IActionResult RoleManage()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetRoleList(PublicQuery query)
        {
            try
            {
                PublicView publicView = await _roleService.GetListByPage(query);
                return Json(new { total = publicView.TotalCount, rows = publicView.RoleList });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("获取角色列表失败！query = 【{0}】", query.ToJSON()), ex);
                return Json(new { total = 0, data = "" });
            }
        }

        public IActionResult RoleAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> RoleAdd(Role role, string MenuIds)
        {
            try
            {
                role.MenuIds = Tools.GetIntIds(MenuIds).ToList();
                _logService.Debug(string.Format("添加角色参数！role = 【{0}】", role.ToJSON()));
                var ret = await _roleService.Add(role);
                if (ret)
                {
                    RefreshCache();
                }
                return Json(new { isadd = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("添加角色失败！role = 【{0}】", role.ToJSON()), ex);
                return Json(new { isadd = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> RoleEdit(string code)
        {
            Role model = await _roleService.Get(code);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> RoleEdit(Role role, string MenuIds)
        {
            try
            {
                role.MenuIds = Tools.GetIntIds(MenuIds).ToList();
                var ret = await _roleService.Update(role);
                if (ret)
                {
                    RefreshCache();
                }
                return Json(new { isedit = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("编辑角色失败！role = 【{0}】", role.ToJSON()), ex);
                return Json(new { isedit = false });
            }
        }

        public async Task<JsonResult> Delrole(string code)
        {
            try
            {
                var ret = await _roleService.Delete(code);
                if (ret)
                {
                    RefreshCache();
                }
                return Json(new { isdel = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("删除角色失败！code=【{0}】", code), ex);
                return Json(new { isdel = false });
            }
        }


        private bool GetRoleMenuChecked(List<RoleMenu> roleMenus, int menuId, int roleId)
        {

            if (roleMenus == null && roleMenus.Count == 0)
            {
                return false;
            }

            return roleMenus.Any(t => t.MenuId == menuId);
        }
        #endregion

        #region 菜单管理
        public IActionResult MenuSet()
        {
            return View();
        }
        [HttpPost]
        public JsonResult GetMenuList(int roleId = 0, int userId = 0)
        {
            try
            {
                List<Menu> menus = _cacheService.Get<List<Menu>>(SysConsts.MENUALLLIST);
                if (menus == null || menus.Count == 0)
                {
                    return Json(new { isGet = false, Msg = "获取菜单树失败!" });
                }
                List<RoleMenu> roleMenus = new List<RoleMenu>();
                List<UserMenu> userMenus = new List<UserMenu>();
                if (roleId > 0)
                {
                    roleMenus = _roleMenuService.GetListByRoleId(roleId).Result;
                }

                if (userId > 0)
                {
                    userMenus = _userMenuService.GetListByUserId(userId).Result;
                }
                List<TreeViewModel> treeViewModels = new List<TreeViewModel>();
                List<Menu> parmenus = menus.Where(w => w.ParentId == 0).ToList();
                foreach (var item in parmenus)
                {
                    TreeViewModel treeViewModel = new TreeViewModel
                    {
                        Id = item.Id,
                        Code = item.Code,
                        NodeId = item.Id,
                        Text = item.CName,
                        Nodes = GetNodes(item.Id, menus, roleId, userId, roleMenus, userMenus),
                        Tags = new List<string> { GetTypeName(item.Type) }
                    };

                    if (roleId > 0)
                    {
                        treeViewModel.State = new State { Checked = GetRoleMenuChecked(roleMenus, item.Id, roleId) };
                    }

                    if (userId > 0)
                    {
                        treeViewModel.State = new State { Checked = GetUserMenuChecked(userMenus, item.Id, roleId) };
                    }

                    treeViewModels.Add(treeViewModel);
                }
                return Json(new { isGet = true, Msg = "", data = treeViewModels });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("获取菜单列表失败！"), ex);
                return Json(new { isGet = false, Msg = "获取菜单树失败!" });
            }
        }

        private List<TreeViewModel> GetNodes(int id, List<Menu> menus, int roleId = 0, int userId = 0, List<RoleMenu> roleMenus = null, List<UserMenu> userMenus = null)
        {
            List<TreeViewModel> treeViewModels = new List<TreeViewModel>();
            List<Menu> nodemenus = menus.Where(w => w.ParentId == id).ToList();
            foreach (var item in nodemenus)
            {
                TreeViewModel treeViewModel = new TreeViewModel
                {
                    Id = item.Id,
                    Code = item.Code,
                    NodeId = item.Id,
                    Text = item.CName,
                    Nodes = GetNodes(item.Id, menus, roleId, userId, roleMenus, userMenus),
                    Tags = new List<string> { GetTypeName(item.Type) }
                };

                if (roleId > 0)
                {
                    treeViewModel.State = new State { Checked = GetRoleMenuChecked(roleMenus, item.Id, roleId) };
                }

                if (userId > 0)
                {
                    treeViewModel.State = new State { Checked = GetUserMenuChecked(userMenus, item.Id, roleId) };
                }

                treeViewModels.Add(treeViewModel);
            }
            return treeViewModels.Count == 0 ? null : treeViewModels;
        }

        private string GetTypeName(int type)
        {
            switch (type)
            {
                case 0:
                    return "导航菜单";
                case 1:
                    return "按钮";
                case 2:
                    return "数据接口";
                default:
                    return "";

            }
        }

        [HttpPost]
        public async Task<JsonResult> MenuAdd(Menu menu)
        {
            try
            {
                menu.Code = Guid.NewGuid().ToString("N");
                menu.CreatedTime = DateTime.Now;
                menu.Url = menu.Url.ToLower();
                menu.EName = menu.EName.ToLower();
                _logService.Debug(string.Format("添加菜单参数！menu = 【{0}】", menu.ToJSON()));
                var ret = await _menuService.Add(menu);
                if (ret)
                {
                    RefreshCache();
                }
                return Json(new { isadd = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("添加菜单失败！menu = 【{0}】", menu.ToJSON()), ex);
                return Json(new { isadd = false });
            }
        }


        [HttpPost]
        public async Task<JsonResult> MenuEdit(Menu menu)
        {
            try
            {
                menu.Url = menu.Url.ToLower();
                menu.EName = menu.EName.ToLower();
                var ret = await _menuService.Update(menu);
                if (ret)
                {
                    RefreshCache();
                }
                return Json(new { isedit = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("编辑菜单失败！menu = 【{0}】", menu.ToJSON()), ex);
                return Json(new { isedit = false });
            }
        }
        /// <summary>
        /// 根据code获取菜单详细信息
        /// </summary>
        /// <returns>The menu by code.</returns>
        /// <param name="code">Code.</param>
        public JsonResult GetMenuByCode(string code)
        {
            try
            {
                List<Menu> menus = _cacheService.Get<List<Menu>>(SysConsts.MENUALLLIST);
                if (menus == null && menus.Count == 0)
                {
                    return Json(new { isGet = false, Msg = "未获取到菜单详细信息，刷新页面试试！" });
                }

                Menu menu = menus.FirstOrDefault(t => t.Code == code);
                if (menu == null)
                {
                    return Json(new { isGet = false, Msg = "未获取到菜单详细信息，刷新页面试试！" });
                }

                return Json(new { Menu = menu, isGet = true, Msg = "" });
            }
            catch (Exception ex)
            {
                return Json(new { isGet = false, Msg = ex });
            }
        }

        public async Task<JsonResult> DelMenu(string code)
        {
            try
            {
                var ret = await _menuService.Delete(code);
                if (ret)
                {
                    RefreshCache();
                }
                return Json(new { isdel = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("删除菜单失败！code=【{0}】", code), ex);
                return Json(new { isdel = false });
            }
        }

        #endregion

        #region 用户管理
        private bool GetUserMenuChecked(List<UserMenu> userMenus, int menuId, int userId)
        {

            if (userMenus == null && userMenus.Count == 0)
            {
                return false;
            }

            return userMenus.Any(t => t.MenuId == menuId);
        }

        public IActionResult UserManage()
        {
            return View();
        }


        [HttpPost]
        public async Task<JsonResult> GetUserList(PublicQuery query)
        {
            try
            {
                PublicView publicView = await _userService.GetListByPage(query);
                foreach (var item in publicView.UserList)
                {
                    if (item.Role != null)
                    {
                        item.RoleName = item.Role.CName;
                    }
                    if (item.Department != null)
                    {
                        item.DepartmentName = item.Department.Name;
                    }
                    if (item.Area != null)
                    {
                        item.AreaName = item.Area.Name;
                    }


                }
                return Json(new { total = publicView.TotalCount, rows = publicView.UserList });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("获取用户列表失败！query = 【{0}】", query.ToJSON()), ex);
                return Json(new { total = 0, data = "" });
            }
        }

        public async Task<IActionResult> UserAdd()
        {
            List<Role> roles = await _roleService.GetList() as List<Role>;
            ViewBag.Roles = roles;
            List<ConfigInfo> configInfos = await _configInfoService.GetList();
            ViewBag.Departments = configInfos.Where(w => w.Type == (int)ConfigInfoType.Department).ToList();
            ViewBag.Areas = configInfos.Where(w => w.Type == (int)ConfigInfoType.Area).ToList();

            return View();
        }

        [HttpPost]
        public async Task<JsonResult> UserAdd(User user, string MenuIds)
        {
            try
            {
                user.MenuIds = Tools.GetIntIds(MenuIds).ToList();
                user.PassWord = Encryption.MD5Str(user.PassWord);
                _logService.Debug(string.Format("添加用户参数！user = 【{0}】", user.ToJSON()));
                var ret = await _userService.Add(user);
                if (ret)
                {
                    RefreshCache();
                }
                return Json(new { isadd = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("添加用户失败！user = 【{0}】", user.ToJSON()), ex);
                return Json(new { isadd = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> UserEdit(string code)
        {
            List<Role> roles = await _roleService.GetList() as List<Role>;
            ViewBag.Roles = roles;
            List<ConfigInfo> configInfos = await _configInfoService.GetList();
            ViewBag.Departments = configInfos.Where(w => w.Type == (int)ConfigInfoType.Department).ToList();
            ViewBag.Areas = configInfos.Where(w => w.Type == (int)ConfigInfoType.Area).ToList();

            User model = await _userService.Get(code);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> UserEdit(User user, string MenuIds)
        {
            try
            {
                user.MenuIds = Tools.GetIntIds(MenuIds).ToList();

                var ret = await _userService.Update(user);
                if (ret)
                {
                    User model = await _userService.Get(user.Code);
                    _cacheService.Replace(model.Code + "-" + SysConsts.USERINFO, model);
                    RefreshCache();
                }
                return Json(new { isedit = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("编辑用户失败！role = 【{0}】", user.ToJSON()), ex);
                return Json(new { isedit = false });
            }
        }

        public async Task<JsonResult> DelUser(string code)
        {
            try
            {
                var ret = await _userService.Delete(code);
                if (ret)
                {
                    RefreshCache();
                }
                return Json(new { isdel = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("删除用户失败！code=【{0}】", code), ex);
                return Json(new { isdel = false });
            }
        }

        public async Task<JsonResult> FreezeUser(string code, bool isFreeze)
        {
            try
            {
                User user = new User { Code = code, IsFreeze = isFreeze };
                var ret = await _userService.FreezeUser(user);

                return Json(new { isdel = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("冻结用户失败！code=【{0}】", code), ex);
                return Json(new { isdel = false });
            }
        }

        public async Task<JsonResult> UpUserPwd(string code, string pwd)
        {
            try
            {
                User user = new User { Code = code, PassWord = Encryption.MD5Str(pwd) };
                var ret = await _userService.UpUserPwd(user);

                return Json(new { isdel = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("修改用户密码失败！code=【{0}】", code), ex);
                return Json(new { isdel = false });
            }
        }
        #endregion

        #region 头像上传
        [HttpPost]
        public async Task<JsonResult> Upload([FromServices]IHostingEnvironment environment, UploadModel uploadModel, string userCode)
        {
            var data = new PicData();
            try
            {
                string path = string.Empty;
                var file = uploadModel.file;
                if (file == null) { data.Msg = "请选择上传的文件。"; data.IsUp = false; return Json(data); }
                //格式限制
                var allowType = new string[] { "image/jpg", "image/jpeg", "image/png" };
                if (!allowType.Contains(file.ContentType))
                {
                    data.IsUp = false;
                    data.Msg = "图片格式错误";
                    return Json(data);
                }


                if (file.Length > 1024 * 1024)
                {
                    data.IsUp = false;
                    data.Msg = "图片过大";
                    return Json(data);
                }

                string webRootPath = environment.WebRootPath;
                string dirPath = "Upload/HeadImg/" + DateTime.Now.ToString("yyyy-MM-dd");
                string webDirPath = Path.Combine(webRootPath, dirPath);
                if (!Directory.Exists(webDirPath))
                {
                    Directory.CreateDirectory(webDirPath);
                }
                //string filePath = Encryption.MD5Str(file.FileName);
                FileInfo fileInfo = new FileInfo(file.FileName);
                string filePath = Encryption.MD5Str(file.FileName) + fileInfo.Extension;
                string strpath = Path.Combine(webDirPath, filePath);

                using (var stream = new FileStream(strpath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    await file.CopyToAsync(stream);
                }
                data.IsUp = true;
                data.Msg = "上传成功";
                data.Path = _appSettings.WebSite + "/" + dirPath + "/" + filePath;

                User user;
                if (!string.IsNullOrEmpty(userCode))
                {
                    user = new User { Code = userCode, HeadImg = data.Path, UpType = "imgHead" };
                    await _userService.UpUser(user);
                    user = await _userService.Get(userCode);
                    // 刷新缓存
                    _cacheService.Replace(userCode + "-" + SysConsts.USERINFO, user);
                }

                return Json(data);
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("上传头像失败！"), ex);
                data.IsUp = false;
                data.Msg = "上传失败！";
                return Json(data);
            }
        }
        public class PicData
        {
            public bool IsUp { get; set; }
            public string Msg { get; set; }
            public string Path { get; set; }
        }
        #endregion

        /// <summary>
        /// 刷新缓存
        /// </summary>
        private void RefreshCache()
        {
            IEnumerable<string> keys = _cacheService.GetCacheKeys();

            _cacheService.RemoveAll(keys);
        }


        [HttpPost]
        public async Task<JsonResult> UpUser(User user)
        {
            try
            {
                user.UpType = "base";
                if (user.PassWord != null)
                {
                    user.PassWord = Encryption.MD5Str(user.PassWord);
                }
                var ret = await _userService.UpUser(user);
                if (ret)
                {
                    User model = await _userService.Get(user.Code);
                    _cacheService.Replace(model.Code + "-" + SysConsts.USERINFO, model);
                }
                return Json(new { isedit = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("修改用户密码失败！code=【{0}】", user.Code), ex);
                return Json(new { isdel = false });
            }
        }
    }
}
