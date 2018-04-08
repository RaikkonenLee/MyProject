using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApiSample.BL.Interfaces;
using ApiSample.Models;
using ApiSample.DA.Interfaces;

namespace ApiSample.BL.Services
{
    public class ProductService : IProductService
    {
        public IProductRepository ProductRepository { get; set; }

        public ProductService(IProductRepository productRepository)
        {
            ProductRepository = productRepository;
        }

        public IEnumerable<ProductForCategoryModel> GetProductByCategoryId(int categoryId)
        {
            return ProductRepository.GetProductByCategoryId(categoryId);
        }
    }
}
