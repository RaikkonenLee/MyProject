using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FormLogin.Layouts
{
    public class ErpAPIService
    {
        public HttpService HttpService { get; private set; }

        public Environment Environment { get; private set; }

        public ErpAPIService(HttpService httpService, string environment)
        {
            HttpService = httpService;
            Environment = GetEnvironment(environment);
        }

        private Environment GetEnvironment(string environment)
        {
            Environment returnValue = Environment.None;
            //
            foreach (var item in Enum.GetNames(typeof(Environment)))
            {
                if (item.ToLower() == environment.ToLower())
                {
                    Enum.TryParse(item, out returnValue);
                    break;
                }
            }
            //
            return returnValue;
        }

        /// <summary>
        /// 取得Erp的Url
        /// </summary>
        /// <param name="siteName"></param>
        /// <returns></returns>
        public string GetErpUrl(string siteName)
        {
            string returnValue = string.Empty;
            //
            switch (Environment)
            {
                case Environment.Sys:
                    returnValue = ConfigurationManager.AppSettings[string.Concat("ErpNext", siteName, "Api")];
                    break;
                case Environment.Edu:
                    returnValue = ConfigurationManager.AppSettings[string.Concat("ErpNextEdu", siteName, "Api")];
                    break;
                case Environment.It:
                    returnValue = ConfigurationManager.AppSettings[string.Concat("ErpNextIt", siteName, "Api")];
                    break;
                default:
                    break;
            }
            //
            return returnValue;
        }
        
        /// <summary>
        /// 取得登入資訊
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public ReturnViewModel<LoginUrlViewModel> GetERPLoginInfo(String userId, String password, out string errorMessage)
        {
            errorMessage = string.Empty;
            string loginUrl = GetErpUrl(siteName: "Login");
            HttpWebResponse response;
            StringBuilder json = new StringBuilder();
            ReturnViewModel<LoginUrlViewModel> returnModel = new ReturnViewModel<LoginUrlViewModel>();
            Dictionary<string, string> requestContent;
            //
            requestContent = new Dictionary<string, string>
            {
                //
                { "Url", string.Concat(loginUrl, "Login") },
                { "ContentType", "application/json" },
                { "Method", "POST" }
            };
            //
            json.Append("{");
            json.Append("\"Data\": {");
            json.AppendFormat("\"EMPNO\": \"{0}\", ", userId);
            json.AppendFormat("\"ERP_PASSWORD\": \"{0}\" ", password);
            json.Append("}}");
            requestContent.Add("Json", json.ToString());
            //
            try
            {
                response = HttpService.GetResponse(requestContent);
                //
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    returnModel = JsonConvert.DeserializeAnonymousType(streamReader.ReadToEnd(), returnModel);
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.ToString();
            }
            //
            return returnModel;
        }

        /// <summary>
        /// 取得Email
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public ReturnViewModel<LoginEmpnoViewModel> GetUserEmail(string token)
        {
            string email = string.Empty,
                   hrApiUrl = GetErpUrl(siteName: "Hr");
            HttpWebResponse response;
            StringBuilder json = new StringBuilder();
            ReturnViewModel<LoginEmpnoViewModel> returnModel = null;
            Dictionary<string, string> requestContent = new Dictionary<string, string>
            {
                { "Url", string.Concat(hrApiUrl, "GetEmail") },
                { "ContentType", "application/json" },
                { "Method", "POST" }
            };
            //
            json.Append("{");
            json.AppendFormat("\"HS\": \"{0}\", ", "false");
            json.AppendFormat("\"TOKEN\": \"{0}\" ", token);
            json.Append("}");
            requestContent.Add("Json", json.ToString());
            //
            response = HttpService.GetResponse(requestContent);
            //
            if (response != null)
            {
                returnModel = new ReturnViewModel<LoginEmpnoViewModel>();
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    returnModel = JsonConvert.DeserializeAnonymousType(streamReader.ReadToEnd(), returnModel);
                }
            }
            //
            return returnModel;
        }

    }
}
