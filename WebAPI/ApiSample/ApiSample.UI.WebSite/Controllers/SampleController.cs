using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApiSample.BL.Interfaces;
using ApiSample.BL.Services;
using System.Web.Mvc;

namespace ApiSample.UI.WebSite.Controllers
{
    public class SampleController : Controller
    {
        public ISampleService SampleSerivice { get; set; }

        //public SampleController() : this(new SampleService())
        //{
            
        //}

        public SampleController(ISampleService sampleService)
        {
            SampleSerivice = sampleService;
        }

        public ActionResult Index()
        {
            var data = SampleSerivice.GetSamples();
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
