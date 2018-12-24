using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BYSCORE.Dao;
using BYSCORE.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NLog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BYSCORE.API.Controllers
{
    [Route("api/product")]
    public class ProductController : Controller
    {
        private readonly ProductDao _productDao;
        private readonly Logger nlog = LogManager.GetCurrentClassLogger(); //获得日志实;
        public ProductController(ProductDao productDao)
        {
            _productDao = productDao;
        }

        // GET: api/values
        [HttpPost]
        [Route("getlist")]
        public PublicView Get([FromBody]PublicQuery query)
        {
            return _productDao.GetProducts(query);
        }

        [HttpGet]
        [Route("getlist")]
        public IEnumerable<Product> GetProducts()
        {
            return _productDao.GetProducts();
        }


        // GET api/values/5
        [HttpGet]
        [Route("getProduct/{id}")]
        public Product Get(int id)
        {
            return _productDao.GetProductByID(id);
        }

        [HttpPost]
        [Route("addProduct")]
        public bool AddProduct([FromBody]Product product)
        {
            try
            {
                return _productDao.AddProduct(product);
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(() =>
                {
                    nlog.Error(ex, "添加产品错误！", product);
                });
                return false;
            }
        }

        [HttpPost]
        [Route("updateProduct")]
        public bool UpdateProduct([FromBody]Product product)
        {
            try
            {
                return _productDao.UpdateProduct(product);
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(() =>
                {
                    nlog.Error(ex, "更新产品错误！", product);
                });
                return false;
            }
        }

        [HttpGet]
        [Route("delProduct/{id}")]
        public bool DeleteProduct(int id)
        {
            try
            {
                return _productDao.DeleteProductByID(id);
            }
            catch (Exception ex)
            {
                Task.Factory.StartNew(() =>
                {
                    nlog.Error(ex, "删除产品错误！", id);
                });
                return false;
            }
        }
    }
}
