using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FormLogin.Layouts
{
    public class HttpService
    {
        /// <summary>
        /// 取得Response
        /// </summary>
        /// <param name="requestContent"></param>
        /// <returns></returns>
        public HttpWebResponse GetResponse(Dictionary<string, string> requestContent)
        {
            HttpWebRequest request = WebRequest.Create(requestContent["Url"]) as HttpWebRequest;
            request.ContentType = requestContent["ContentType"];
            request.Method = requestContent["Method"];
            HttpWebResponse response;
            //
            using (StreamWriter streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(requestContent["Json"]);
                streamWriter.Flush();
                streamWriter.Close();
            }
            //
            try
            {
                response = request.GetResponse() as HttpWebResponse;
            }
            catch (Exception)
            {
                response = null;
            }
            //
            return response;
        }

        /// <summary>
        /// 取得Cookie
        /// </summary>
        /// <param name="url"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Cookie GetAuthCookie(String url, String userName, String password)
        {
            Cookie returnValue = null;
            CookieContainer CookieJar = new CookieContainer();
            Uri authServiceUri = new Uri(url + "/_vti_bin/authentication.asmx");
            HttpWebRequest spAuthReq = HttpWebRequest.Create(authServiceUri) as HttpWebRequest;
            spAuthReq.CookieContainer = CookieJar;
            spAuthReq.Headers["SOAPAction"] = "http://schemas.microsoft.com/sharepoint/soap/Login";
            spAuthReq.ContentType = "text/xml; charset=utf-8";
            spAuthReq.Method = "POST";
            string envelope =
                "<?xml version=\"1.0\" encoding=\"utf-8\"?>"
                + "<soap:Envelope xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\""
                + " xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">"
                + "<soap:Body>"
                + "<Login xmlns=\"http://schemas.microsoft.com/sharepoint/soap/\">"
                + "<username>{0}</username>"
                + "<password>{1}</password>"
                + "</Login>" + "</soap:Body>"
                + "</soap:Envelope>";
            envelope = string.Format(envelope, userName, password);
            StreamWriter streamWriter = new StreamWriter(spAuthReq.GetRequestStream());
            streamWriter.Write(envelope);
            streamWriter.Close();
            HttpWebResponse response = spAuthReq.GetResponse() as HttpWebResponse;
            if (response.StatusCode == HttpStatusCode.OK && response.Cookies.Count > 0)
            {
                returnValue = response.Cookies[0];
            }
            response.Close();
            //
            return returnValue;
        }

    }
}
