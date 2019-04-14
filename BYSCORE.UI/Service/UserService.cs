using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using BYSCORE.Entity;

namespace BYSCORE.UI
{
    public class UserService
    {
        private readonly WebApiHelper _webapiHelper;
        public UserService(WebApiHelper webApiHelper)
        {
            _webapiHelper = webApiHelper;
        }

        public async Task<bool> Add(User model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.User.Add, model);
        }

        public async Task<PublicView> GetListByPage(PublicQuery query)
        {
            return await _webapiHelper.PostAsync<PublicView>(ApiUrls.User.GetListByPage, query);
        }

        public async Task<IEnumerable<User>> GetList()
        {
            return await _webapiHelper.GetAsync<IEnumerable<User>>(ApiUrls.User.Getlist);
        }

        public async Task<List<UserSelectView>> GetUserNameList()
        {
            return await _webapiHelper.GetAsync<List<UserSelectView>>(ApiUrls.User.GetUserNameList);
        }

        public async Task<User> Get(string code)
        {
            return await _webapiHelper.GetAsync<User>(ApiUrls.User.Get.Link(code));
        }

        public async Task<bool> Delete(string code)
        {
            return await _webapiHelper.GetAsync<bool>(ApiUrls.User.Delete.Link(code));
        }

        public async Task<bool> Update(User model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.User.Update, model);
        }

        public async Task<User> UserLogin(User model)
        {
            return await _webapiHelper.PostAsync<User>(ApiUrls.User.UserLogin, model);
        }

        public async Task<List<Menu>> GetUserMenus(string userCode)
        {
            return await _webapiHelper.GetAsync<List<Menu>>(ApiUrls.User.GetUserMenus.Link(userCode));
        }

        public async Task<bool> FreezeUser(User model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.User.FreezeUser, model);
        }

        public async Task<bool> UpUserPwd(User model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.User.UpUserPwd, model);
        }

        public async Task<bool> UpUser(User model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.User.UpUser, model);
        }
    }
}
