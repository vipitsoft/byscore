using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using BYSCORE.Common;
using BYSCORE.UI.Common;
using Microsoft.Extensions.Options;
using BYSCORE.Entity;

namespace BYSCORE.UI
{
    public class WebApiHelper
    {
        private readonly HttpClient client;
        private readonly IHttpContextAccessor _accessor;
        private readonly IOptions<AppSettings> _settings;
        public WebApiHelper(HttpClient httpClient, IHttpContextAccessor accessor, IOptions<AppSettings> settings)
        {
            _settings = settings;
            AppSettings model = _settings.Value;
            httpClient.BaseAddress = new Uri(model.ApiBaseUrl);
            client = httpClient;
            _accessor = accessor;
        }

        public async Task<T> DeleteAsync<T>(string requestUri)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            BuildLogHeaders(client);
            var response = await client.DeleteAsync(requestUri);
            if(response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();

            }

            VerifyStatus(response);
            return default(T);            
        }

        public async Task<string> GetString(string url)
        {
            
            client.DefaultRequestHeaders.AcceptCharset.Add(new StringWithQualityHeaderValue("utf-8"));
            return await client.GetStringAsync(url);

        }

        public async Task<T> GetAsync<T>( string requestUri)
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = new TimeSpan(10, 0, 0);
            BuildLogHeaders(client);
            var response = await client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            VerifyStatus(response);
            return default(T);

        }

        public async Task<string> GetAsync(string requestUri)
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            BuildLogHeaders(client);
            var response = await client.GetAsync(requestUri);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
          
            VerifyStatus(response);
            return null;

        }

        public async Task<T> PostAsync<T>(string requestUri, object data)
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            BuildLogHeaders(client);
            var response = await client.PostAsJsonAsync(requestUri, data);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            VerifyStatus(response);
            return default(T);
        }
       
        public  async Task<string> PostAsync(string requestUri, object data)
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await client.PostAsJsonAsync(requestUri, data);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsStringAsync();
            }
         
            VerifyStatus(response);
            return null;
        }

        public  async Task<T> PostFormAsync<T>(string requestUri, object data)
        {
            
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var content = new FormUrlEncodedContent(data);
            var response = await client.PostAsJsonAsync(requestUri, data);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            VerifyStatus(response);
            return default(T);
        }
       

        public async Task<T> PutAsync<T>(string requestUri, object data)
        {

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            BuildLogHeaders(client);
            var response = await client.PutAsJsonAsync(requestUri, data);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadAsAsync<T>();
            }

            VerifyStatus(response);
            return default(T);
        }

        private  void VerifyStatus(HttpResponseMessage response)
        {
            string msg = response.Content.ReadAsStringAsync().Result;
            //ServiceException exception = new ServiceException(msg);

            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    //WebLog.Error(new UnauthorizedAccessException(response.ReasonPhrase, new Exception(response.RequestMessage.ToString())));
                }
                else
                {
                    //WebLog.Error(new HttpException((int)response.StatusCode, response.ReasonPhrase, new Exception(exception.SimplifyMessage + response.RequestMessage.ToString())));
                }
            }
        }

        private  void BuildLogHeaders(HttpClient httpClient)
        {
            //try
            //{
            //    TencentIdentity userinfo = (TencentIdentity)_accessor.HttpContext.User.Identity;
            //    //登录信息
            //    httpClient.DefaultRequestHeaders.Authorization = CreateBasicCredentials(userinfo.Name, userinfo.StaffId, userinfo.ChineseName);
            //    //填充日志所需头
            //    if (_accessor.HttpContext != null)
            //    {
            //        var rid = _accessor.HttpContext.Items["_RequestID"] as string;
            //        if (rid == null)
            //        {
            //            rid = Guid.NewGuid().ToString("N");
            //            _accessor.HttpContext.Items["_RequestID"] = rid;
            //        }
            //        httpClient.DefaultRequestHeaders.Add("X-RequestID", rid.UrlEncode());
            //        httpClient.DefaultRequestHeaders.Add("X-UserIP", _accessor.HttpContext.Connection.RemoteIpAddress.ToString());
            //        httpClient.DefaultRequestHeaders.Add("X-LoginUserName", _accessor.HttpContext.User.Identity.Name.UrlEncode());
            //        httpClient.DefaultRequestHeaders.Add("X-UserName", _accessor.HttpContext.User.Identity.Name.UrlEncode());
            //        httpClient.DefaultRequestHeaders.Add("X-Url", _accessor.HttpContext.Request.GetAbsoluteUri().UrlEncode());
            //        httpClient.DefaultRequestHeaders.Add("X-UserAgent", _accessor.HttpContext.Request.Headers["user-agent"].ToString().UrlEncode());
            //        var referrer = _accessor.HttpContext.Request.Headers["referer"].ToString() == "" ? "" : _accessor.HttpContext.Request.Headers["referer"].ToString();
            //        httpClient.DefaultRequestHeaders.Add("X-Referer", referrer.UrlEncode());
            //    }
            //}
            //catch (Exception ex)
            //{
            //    //WebLog.Error(ex);
            //}
        }

        private AuthenticationHeaderValue CreateBasicCredentials(string userName, int staffId, string chineseName)
        {
            string toEncode = userName + ":" + staffId + ":" + chineseName;
            var toBase64 = Encoding.UTF8.GetBytes(toEncode);
            var base64 = Convert.ToBase64String(toBase64);
            return new AuthenticationHeaderValue("Basic", base64);
        }

       
    }
}