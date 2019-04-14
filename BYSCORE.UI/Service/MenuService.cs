using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BYSCORE.Entity;

namespace BYSCORE.UI
{
    public class MenuService
    {
        private readonly WebApiHelper _webapiHelper;
        public MenuService(WebApiHelper webApiHelper)
        {
            _webapiHelper = webApiHelper;
        }

        public async Task<bool> Add(Menu model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.Menu.Add, model);
        }

        public async Task<PublicView> GetListByPage(PublicQuery query)
        {
            return await _webapiHelper.PostAsync<PublicView>(ApiUrls.Menu.GetListByPage, query);
        }

        public async Task<List<Menu>> GetList()
        {
            return await _webapiHelper.GetAsync<List<Menu>>(ApiUrls.Menu.Getlist);
        }

        public async Task<Menu> Get(string code)
        {
            return await _webapiHelper.GetAsync<Menu>(ApiUrls.Menu.Get.Link(code));
        }

        public async Task<bool> Delete(string code)
        {
            return await _webapiHelper.GetAsync<bool>(ApiUrls.Menu.Delete.Link(code));
        }

        public async Task<bool> Update(Menu model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.Menu.Update, model);
        }
    }
}
