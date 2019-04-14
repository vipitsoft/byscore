using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BYSCORE.Entity;

namespace BYSCORE.UI
{
    public class UserMenuService
    {
        private readonly WebApiHelper _webapiHelper;
        public UserMenuService(WebApiHelper webApiHelper)
        {
            _webapiHelper = webApiHelper;
        }

        public async Task<bool> Add(UserMenu model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.UserMenu.Add, model);
        }

        public async Task<PublicView> GetListByPage(PublicQuery query)
        {
            return await _webapiHelper.PostAsync<PublicView>(ApiUrls.UserMenu.GetListByPage, query);
        }

        public async Task<IEnumerable<UserMenu>> GetList()
        {
            return await _webapiHelper.GetAsync<IEnumerable<UserMenu>>(ApiUrls.UserMenu.Getlist);
        }

        public async Task<UserMenu> Get(string code)
        {
            return await _webapiHelper.GetAsync<UserMenu>(ApiUrls.UserMenu.Get.Link(code));
        }

        public async Task<bool> Delete(string code)
        {
            return await _webapiHelper.GetAsync<bool>(ApiUrls.UserMenu.Delete.Link(code));
        }

        public async Task<bool> Update(UserMenu model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.UserMenu.Update, model);
        }

        public async Task<List<UserMenu>> GetListByUserId(int id)
        {
            return await _webapiHelper.GetAsync<List<UserMenu>>(ApiUrls.UserMenu.GetListByUserId.Link(id));
        }
    }
}
