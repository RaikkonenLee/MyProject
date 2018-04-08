using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using ApiSample.DA.Tables;
using ApiSample.DA.Repositories;
using ApiSample.Models;
using TechTalk.SpecFlow.Assist;

namespace ApiSample.DA.Repositories.Test
{
    [Binding]
    public sealed class 分類商品查詢功能步驟
    {
        public ShopContext ShopContext { get; private set; }

        public int QueryCategoryId { get; private set; }

        public ProductRepository ProductRepository { get; private set; }

        public IEnumerable<ProductForCategoryModel> ProductResult { get; private set; }

        [BeforeScenario]
        public void ScenarioSetup()
        {
            ShopContext = new ShopContext();
            ShopContext.Database.Delete();
        }

        [AfterScenario]
        public void ScenarioTeardown()
        {
            ShopContext.Dispose();
        }

        [Given(@"資料庫中有分類資料")]
        public void 假設資料庫中有分類資料(Table table)
        {
            var categories = table.CreateSet<Category>();
            //
            foreach (var category in categories)
            {
                ShopContext.Categories.Add(category);
            }
            //
            ShopContext.SaveChanges();
        }

        [Given(@"資料庫中有產品資料")]
        public void 假設資料庫中有產品資料(Table table)
        {
            var products = table.CreateSet<Product>();
            //
            foreach (var product in products)
            {
                ShopContext.Products.Add(product);
            }
            //
            ShopContext.SaveChanges();
        }

        [Given(@"當查詢分類(.*)的商品時")]
        public void 假設當查詢分類的商品時(int categoryId)
        {
            QueryCategoryId = categoryId;
        }

        [When(@"執行分類商品查詢")]
        public void 當執行分類商品查詢()
        {
            if (ProductRepository == null)
            {
                ProductRepository = new ProductRepository(ShopContext);
            }
            ProductResult = ProductRepository.GetProductByCategoryId(QueryCategoryId);
        }

        [Then(@"得到商品")]
        public void 那麼得到商品(Table table)
        {
            table.CompareToSet(ProductResult);
        }

    }
}
