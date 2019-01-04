using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BYSCORE.UI.Models;
using BYSCORE.Entity;
using BYSCORE.Common;

namespace BYSCORE.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProductService _productService;
        private readonly LogService _logService;
        public HomeController(ProductService productService, LogService logService)
        {
            _productService = productService;
            _logService = logService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ProductList()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> GetProductList(PublicQuery query)
        {
            try
            {
                PublicView publicView = await _productService.GetProducts(query);
                return Json(new { total = publicView.TotalCount, rows = publicView.ProductList });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("获取产品列表失败！query = 【{0}】", query.ToJSON()), ex);
                return Json(new { total = 0, data = "" });
            }
        }

        public IActionResult ProductAdd()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> ProductAdd(Product product)
        {
            try
            {
                _logService.Debug(string.Format("添加产品参数！product = 【{0}】", product.ToJSON()));
                var ret = await _productService.AddProduct(product);

                return Json(new { isadd = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("添加产品失败！product = 【{0}】", product.ToJSON()), ex);
                return Json(new { isadd = false });
            }
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(int id)
        {
            Product model = await _productService.GetProduct(id);
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> ProductEdit(Product product)
        {
            try
            {
                var ret = await _productService.UpdateProduct(product);

                return Json(new { isedit = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("编辑产品失败！product = 【{0}】", product.ToJSON()), ex);
                return Json(new { isedit = false });
            }
        }

        public async Task<JsonResult> DelProduct(int id)
        {
            try
            {
                var ret = await _productService.DelProduct(id);

                return Json(new { isdel = ret });
            }
            catch (Exception ex)
            {
                _logService.Error(string.Format("编辑产品失败！id=【{0}】", id), ex);
                return Json(new { isdel = false });
            }
        }

        public async Task<IActionResult> ExportExcel()
        {
            try
            {
                List<Product> list = await _productService.GetProducts();
                string[] title = { "产品名", "产品介绍", "分类", "价格", "折扣" };
                byte[] files = ExcelHelper.OutputExcel(list, title);

                return new FileContentResult(files, "application/vnd.ms-excel"); //.WriteAllBytes(@"c:\test\test.xls", files));
            }
            catch (Exception ex)
            {
                _logService.Error("导出excel失败！", ex);
                return null;
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
