using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiSample.Models;
using ApiSample.DA.Interfaces;
using ApiSample.DA.Tables;

namespace ApiSample.DA.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public ShopContext ShopContext { get; set; }

        public ProductRepository(ShopContext context)
        {
            ShopContext = context;
        }

        public IEnumerable<ProductForCategoryModel> GetProductByCategoryId(int categoryId)
        {
            var result = ShopContext.Products.Where(m => 
                m.SellingStartTime < DateTime.Now &&
                m.SellingEndTime >= DateTime.Now &&
                m.Category.Id == categoryId).
            Select(m => new ProductForCategoryModel()
            {
                Id = m.Id,
                Name = m.Name,
                Price = m.Price
            });
            //
            return result;
        }
    }
}
