using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApiSample.BL.Interfaces;

namespace ApiSample.UI.WebSite.Controllers
{
    public class ProductController : Controller
    {
        public IProductService ProductService { get; set; }

        public ProductController(IProductService productService)
        {
            ProductService = productService;
        }

        // GET: Product
        public ActionResult GetProductByCategory(int id)
        {
            var result = ProductService.GetProductByCategoryId(id);
            //
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}