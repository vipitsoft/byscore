using System;
using System.Collections.Generic;
using System.Linq;
using BYSCORE.Common;
using BYSCORE.Entity;
using Microsoft.EntityFrameworkCore;

namespace BYSCORE.Dao
{
    public class ProductDao
    {
        private readonly CoreDbContext _coreDbContext;

        public ProductDao(CoreDbContext cOREDbContext)
        {
            _coreDbContext = cOREDbContext;
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="product">Product.</param>
        public bool AddProduct(Product product)
        {
            _coreDbContext.Add(product);
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据id 删除
        /// </summary>
        /// <returns><c>true</c>, if product by identifier was deleted, <c>false</c> otherwise.</returns>
        /// <param name="id">Identifier.</param>
        public bool DeleteProductByID(int id)
        {
            var model = _coreDbContext.Product.Where(t => t.IsDelete == false && t.Id == id).FirstOrDefault();
            if (model == null)
            {
                return false;
            }

            model.IsDelete = true;
            return _coreDbContext.SaveChanges() > 0;
        }

        /// <summary>
        /// 根据id 获取
        /// </summary>
        /// <returns>The product by identifier.</returns>
        /// <param name="id">Identifier.</param>
        public Product GetProductByID(int id)
        {
            return _coreDbContext.Product.Where(t => t.IsDelete == false && t.Id == id).FirstOrDefault();
        }

        /// <summary>
        /// 获取所有产品(分页)
        /// </summary>
        /// <returns>The products.</returns>
        public PublicView GetProducts(PublicQuery query)
        {
            var list = _coreDbContext.Product.Where(w => w.IsDelete == false);

            if (query.Name.IsNotNullOrEmpty())
            {
                list = list.Where(w => w.Name.Contains(query.Name));
            }

            if (query.AddTime.IsNotNullOrEmpty())
            {
                DateTime time = Convert.ToDateTime(query.AddTime);
                list = list.Where(w => w.AddTime >= time && w.AddTime <= time);
            }
            list = list.OrderByDescending(t => t.AddTime).Paging<Product>(out int totalCount, query.Page, query.PageSize);

            return new PublicView { TotalCount = totalCount, ProductList = list.AsNoTracking().ToList() };
        }

        /// <summary>
        /// 获取所有产品(不分页)
        /// </summary>
        /// <returns>The products.</returns>
        public IEnumerable<Product> GetProducts()
        {
            return _coreDbContext.Product.Where(w => w.IsDelete == false).AsNoTracking().ToList();
        }
        /// <summary>
        /// 更新产品
        /// </summary>
        /// <returns><c>true</c>, if product was updated, <c>false</c> otherwise.</returns>
        /// <param name="product">Product.</param>
        public bool UpdateProduct(Product product)
        {
            _coreDbContext.Product.Update(product);
            return _coreDbContext.SaveChanges() > 0;
        }
    }
}
