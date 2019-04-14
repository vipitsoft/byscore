using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BYSCORE.Entity;

namespace BYSCORE.UI
{
    public class ConfigInfoService
    {
        private readonly WebApiHelper _webapiHelper;
        public ConfigInfoService(WebApiHelper webApiHelper)
        {
            _webapiHelper = webApiHelper;
        }

        public async Task<bool> Add(ConfigInfo model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.ConfigInfo.Add, model);
        }

        public async Task<PublicView> GetListByPage(PublicQuery query)
        {
            return await _webapiHelper.PostAsync<PublicView>(ApiUrls.ConfigInfo.GetListByPage, query);
        }

        public async Task<List<ConfigInfo>> GetList()
        {
            return await _webapiHelper.GetAsync<List<ConfigInfo>>(ApiUrls.ConfigInfo.Getlist);
        }

        public async Task<List<ConfigInfo>> GetListByType(int type)
        {
            return await _webapiHelper.GetAsync<List<ConfigInfo>>(ApiUrls.ConfigInfo.GetListByType.Link(type));
        }

        public async Task<ConfigInfo> Get(string code)
        {
            return await _webapiHelper.GetAsync<ConfigInfo>(ApiUrls.ConfigInfo.Get.Link(code));
        }

        public async Task<bool> Delete(string code, int type)
        {
            return await _webapiHelper.GetAsync<bool>(ApiUrls.ConfigInfo.Delete.Link(code).Link(type));
        }

        public async Task<bool> Update(ConfigInfo model)
        {
            return await _webapiHelper.PostAsync<bool>(ApiUrls.ConfigInfo.Update, model);
        }

        public async Task<ConfigInfo> GetByname(string name, int type)
        {
            return await _webapiHelper.GetAsync<ConfigInfo>(ApiUrls.ConfigInfo.GetByname.Link(name).Link(type));
        }

        public async Task<bool> PullSalary()
        {
            return await _webapiHelper.GetAsync<bool>(ApiUrls.ConfigInfo.PullSalary);
        }
    }
}
