using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BYSCORE.Entity;

namespace BYSCORE.UI
{
    public class RoleMenuService
    {
        private readonly WebApiHelper _webapiHelper;
        public RoleMenuService(WebApiHelper webApiHelper)
        {
            _webapiHelper = webApiHelper;
        }

        public async Task<bool> Add(RoleMenu model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.RoleMenu.Add, model);
        }

        public async Task<PublicView> GetListByPage(PublicQuery query)
        {
            return await _webapiHelper.PostAsync<PublicView>(ApiUrls.RoleMenu.GetListByPage, query);
        }

        public async Task<IEnumerable<RoleMenu>> GetList()
        {
            return await _webapiHelper.GetAsync<IEnumerable<RoleMenu>>(ApiUrls.RoleMenu.Getlist);
        }

        public async Task<RoleMenu> Get(string code)
        {
            return await _webapiHelper.GetAsync<RoleMenu>(ApiUrls.RoleMenu.Get);
        }

        public async Task<bool> Delete(string code)
        {
            return await _webapiHelper.GetAsync<bool>(ApiUrls.RoleMenu.Delete);
        }

        public async Task<bool> Update(RoleMenu model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.RoleMenu.Update, model);
        }

        public async Task<List<RoleMenu>> GetListByRoleId(int id)
        {
            return await _webapiHelper.GetAsync<List<RoleMenu>>(ApiUrls.RoleMenu.GetListByRoleId.Link(id));
        }
    }
}
