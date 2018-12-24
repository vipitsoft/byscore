using System;
using System.Threading.Tasks;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.Extensions.Options;

namespace BYSCORE.UI
{
    public class LogService
    {
        private readonly IOptions<AppSettings> _settings;
        private string baseUrl;
        public LogService(IOptions<AppSettings> settings)
        {
            _settings = settings;
            AppSettings model = _settings.Value;
            baseUrl = model.ApiBaseUrl;
        }

        internal void Info(LogQuery logQuery)
        {
            Task.Factory.StartNew(() =>
            {
                HttpHelper.Post(baseUrl + ApiUrls.Log.Info, logQuery.ToJson());
            });
        }

        internal void Error(LogQuery logQuery)
        {
            Task.Factory.StartNew(() =>
            {
                HttpHelper.Post(baseUrl + ApiUrls.Log.Error, logQuery.ToJson());
            });
        }

        internal void Debug(LogQuery logQuery)
        {
            Task.Factory.StartNew(() =>
            {
                HttpHelper.Post(baseUrl + ApiUrls.Log.Debug, logQuery.ToJson());
            });
        }
    }
}
