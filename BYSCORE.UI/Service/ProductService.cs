using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BYSCORE.Entity;

namespace BYSCORE.UI
{
    public class ProductService : BaseService
    {
        private readonly WebApiHelper _webApiHelper;
        public ProductService(WebApiHelper webApiHelper)
        {
            _webApiHelper = webApiHelper;
        }

        internal async Task<PublicView> GetProducts(PublicQuery query)
        {
            return await _webApiHelper.PostAsync<PublicView>(ApiUrls.Product.GetList, query);
        }

        internal async Task<List<Product>> GetProducts()
        {
            return await _webApiHelper.GetAsync<List<Product>>(ApiUrls.Product.GetList);
        }

        internal async Task<Product> GetProduct(int id)
        {
            return await _webApiHelper.GetAsync<Product>(ApiUrls.Product.GetProduct.Link(id));
        }


        internal async Task<bool> AddProduct(Product product)
        {
            return await _webApiHelper.PostAsync<bool>(ApiUrls.Product.AddProduct, product);
        }

        internal async Task<bool> UpdateProduct(Product product)
        {
            return await _webApiHelper.PostAsync<bool>(ApiUrls.Product.UpdateProduct, product);
        }

        internal async Task<bool> DelProduct(int id)
        {
            return await _webApiHelper.GetAsync<bool>(ApiUrls.Product.DelProduct.Link(id));
        }
    }
}
