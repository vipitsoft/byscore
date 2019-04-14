using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BYSCORE.Entity;

namespace BYSCORE.UI
{
    public class RoleService
    {
        private readonly WebApiHelper _webapiHelper;
        public RoleService(WebApiHelper webApiHelper)
        {
            _webapiHelper = webApiHelper;
        }

        public async Task<bool> Add(Role model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.Role.Add, model);
        }

        public async Task<PublicView> GetListByPage(PublicQuery query)
        {
            return await _webapiHelper.PostAsync<PublicView>(ApiUrls.Role.GetListByPage, query);
        }

        public async Task<IEnumerable<Role>> GetList()
        {
            return await _webapiHelper.GetAsync<IEnumerable<Role>>(ApiUrls.Role.Getlist);
        }

        public async Task<Role> Get(string code)
        {
            return await _webapiHelper.GetAsync<Role>(ApiUrls.Role.Get.Link(code));
        }

        public async Task<bool> Delete(string code)
        {
            return await _webapiHelper.GetAsync<bool>(ApiUrls.Role.Delete.Link(code));
        }

        public async Task<bool> Update(Role model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.Role.Update, model);
        }

        public async Task<List<Menu>> GetRoleMenus(string roleCode)
        {
            return await _webapiHelper.GetAsync<List<Menu>>(ApiUrls.Role.GetRoleMenus.Link(roleCode));
        }
    }
}
