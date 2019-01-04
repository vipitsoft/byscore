using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Mvc;
using NLog;

namespace BYSCORE.API.Controllers
{
    [Route("api/log")]
    public class LogController
    {
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;

        [HttpPost, Route("debug")]
        public void Debug([FromBody]LogQuery logQuery)
        {
            nlog.Debug(logQuery.Message);
        }

        [HttpPost, Route("info")]
        public void Info([FromBody]LogQuery logQuery)
        {
            nlog.Info(logQuery.Message);
        }

        [HttpPost, Route("error")]
        public void Error([FromBody]LogQuery logQuery)
        {
            nlog.Error(logQuery.Exception, logQuery.Message);
        }
    }
}
