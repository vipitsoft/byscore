using System;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace BYSCORE.API.Filter
{
    public class ExceptionFilter : Attribute, IExceptionFilter
    {
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;

        public void OnException(ExceptionContext context)
        {
            nlog.Error(context.Exception, context.Exception.Message);
        }
    }
}
