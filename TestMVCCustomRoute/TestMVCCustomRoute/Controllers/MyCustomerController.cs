using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TestMVCCustomRoute.Controllers
{
    public class MyCustomerController : Controller
    {
        // GET: MyCustomer
        public ActionResult Index()
        {
            return View();
        }
    }
}