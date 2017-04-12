using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Security.Application;

namespace TestAntiXss.Controllers
{
    public class TestController : ApiController
    {
        // GET: api/Test
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Test/5
        public string Get(string id)
        {
            //濾掉javascript的區塊
            string value = Sanitizer.GetSafeHtmlFragment(id);
            //濾掉javascript的區塊及html的部分換行
            string value2 = Sanitizer.GetSafeHtml(id);
            return id;
        }

        // POST: api/Test
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Test/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Test/5
        public void Delete(int id)
        {
        }
    }
}
