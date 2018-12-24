using System;
using System.Threading.Tasks;
using BYSCORE.Entity;

namespace BYSCORE.UI
{
    public class AppLogService : BaseService
    {
        private readonly WebApiHelper _webapiHelper;
        public AppLogService(WebApiHelper webApiHelper)
        {
            _webapiHelper = webApiHelper;
        }

        public async Task<PublicView> GetAppLogs(PublicQuery publicQuery)
        {
            return await _webapiHelper.PostAsync<PublicView>(ApiUrls.AppLog.GetAppLogs, publicQuery);
        }

        public async Task<ApplicationLog> GetAppLog(int id)
        {
            return await _webapiHelper.GetAsync<ApplicationLog>(ApiUrls.AppLog.GetAppLog.Link(id));
        }
    }
}
