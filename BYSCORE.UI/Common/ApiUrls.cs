using System;

namespace BYSCORE.UI
{
    public class ApiUrls
    {
        internal static class Product
        {
            private const string prefix = "/api/product/";

            public const string GetList = prefix + "getlist";

            public const string GetProduct = prefix + "getProduct";

            public const string AddProduct = prefix + "addProduct";

            public const string UpdateProduct = prefix + "updateProduct";

            public const string DelProduct = prefix + "delProduct";
        }

        internal static class Log
        {
            private const string prefix = "/api/log/";

            public const string Debug = prefix + "debug";

            public const string Info = prefix + "info";

            public const string Error = prefix + "error";
        }

        internal static class AppLog
        {
            private const string prefix = "/api/applog/";

            public const string GetAppLogs = prefix + "getapplogs";

            public const string GetAppLog = prefix + "getapplog";
        }
    }
}
