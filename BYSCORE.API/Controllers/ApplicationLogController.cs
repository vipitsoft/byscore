using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSCORE.Dao;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYSCORE.API.Controllers
{
    [Route("api/applog")]
    public class ApplicationLogController : Controller
    {
        private readonly ApplicationLogDao _applicationLogDao;
        public ApplicationLogController(ApplicationLogDao applicationLogDao)
        {
            _applicationLogDao = applicationLogDao;
        }

        [HttpPost, Route("getapplogs")]
        public PublicView GetAppLogs([FromBody]PublicQuery publicQuery)
        {
            return _applicationLogDao.GetApplicationLogs(publicQuery);
        }

        [HttpGet, Route("getapplog/{id}")]
        public ApplicationLog GetAppLog(int id)
        {
            return _applicationLogDao.GetApplicationLog(id);
        }
    }
}
