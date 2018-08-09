using System;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.SharePoint.IdentityModel.Pages;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.Generic;
using Layouts;

namespace LoginPage.Layouts.Login
{
    public partial class Login : FormsSignInPage
    {
        
        protected override void OnInit(EventArgs eventArgs)
        {
            //從ERP的連結將Url的Token傳入，取得Token後將Token跟ERP做認證取得使用者資訊
            //取得後再跟Sharepoint做認證取得Cookie
            string token = string.Empty,
                   errorMessage = string.Empty;
            if (Request.QueryString.Count > 0)
            {
                token = Request.QueryString[0];
                ReturnViewModel<LoginEmpnoViewModel> userInfo = null;
                if (!string.IsNullOrEmpty(token))
                {
                    userInfo = GetUserEmail(token.ToString());
                    //
                    LoginProcess(userInfo, out errorMessage);
                }
            }
            //
            base.OnInit(eventArgs);
        }

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                this.signInControl.LoggingIn += SignInControl_LoggingIn;
            }
            catch (Exception)
            {
                ;
            }
        }

        /// <summary>
        /// 登入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void SignInControl_LoggingIn(object sender, System.Web.UI.WebControls.LoginCancelEventArgs e)
        {
            string userName = signInControl.UserName.Trim(),
                   password = signInControl.Password.Trim(),
                   errorMessage = string.Empty;
            ReturnViewModel<LoginUrlViewModel> loginData;
            ReturnViewModel<LoginEmpnoViewModel> userInfo = null;
            ClaimsFormsPageMessage.Text = string.Empty;
            //
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                loginData = GetERPLoginInfo(userName, password, out errorMessage);
                //
                switch (loginData.ErrCode)
                {
                    case ErrCode.Case0:
                        userInfo = GetUserEmail(loginData.Data.Token.ToString());
                        //
                        LoginProcess(userInfo, out errorMessage);
                        break;
                    default:
                        if (userName.ToLower() == "fbaadmin")
                        {
                            userInfo = new ReturnViewModel<LoginEmpnoViewModel>() { Data = new LoginEmpnoViewModel() { Empname = "fbaadmin" } };
                            //
                            LoginProcess(userInfo, out errorMessage);
                        }
                        else
                        {
                            errorMessage = errorMessage != string.Empty ? string.Concat(errorMessage, ";", loginData.Message) : loginData.Message;
                            signInControl.PasswordRequiredErrorMessage = errorMessage;
                        }
                        break;
                }
            }
            else
            {
                errorMessage = "帳號密碼不得為空值";
            }
            //
            if (errorMessage != string.Empty)
            {
                ClaimsFormsPageMessage.Text = errorMessage;
                e.Cancel = true;
            }
            
        }

        /// <summary>
        /// 登入取得Cookie並導向首頁
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="errorMessage"></param>
        public void LoginProcess(ReturnViewModel<LoginEmpnoViewModel> userInfo, out string errorMessage)
        {
            MyMemberShipProvider membershipProvider = new MyMemberShipProvider();
            bool hasUser = false;
            Cookie cookie;
            string webSiteUrl = ConfigurationManager.AppSettings["WebSiteUrl"],
                   defaultPage = ConfigurationManager.AppSettings["DefaultPage"];
            errorMessage = string.Empty;
            //
            if (membershipProvider.ExistsUser(userInfo.Data.Empname))
            {
                hasUser = true;
            }
            else
            {
                //SharePoint不要存密碼
                hasUser = membershipProvider.AddUser(userInfo.Data.Empname, "p@ssw0rd", userInfo.Data.Email);
            }
            if (hasUser)
            {
                cookie = GetAuthCookie(webSiteUrl, userInfo.Data.Empname, "p@ssw0rd");
                if (cookie != null)
                {
                    Response.Cookies.Add(new System.Web.HttpCookie(cookie.Name, cookie.Value)
                    {
                        Domain = cookie.Domain,
                        Expires = cookie.Expires,
                    });
                    Response.Redirect(string.Concat(webSiteUrl, defaultPage));
                }
                else
                {
                    errorMessage = "帳號密碼輸入錯誤";
                }
            }
            else
            {
                errorMessage = "帳號密碼新增錯誤";
            }
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
            string loginUrl = ConfigurationManager.AppSettings["ErpNextLoginApi"];
            HttpWebResponse response;
            StringBuilder json = new StringBuilder();
            ReturnViewModel<LoginUrlViewModel> returnModel = new ReturnViewModel<LoginUrlViewModel>();
            Dictionary<string, string> requestContent = new Dictionary<string, string>
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
                response = GetResponse(requestContent);
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
                   hrApiUrl = ConfigurationManager.AppSettings["ErpNextHrApi"];
            HttpWebResponse response;
            StringBuilder json = new StringBuilder();
            ReturnViewModel<LoginEmpnoViewModel> returnModel = new ReturnViewModel<LoginEmpnoViewModel>();
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
            response = GetResponse(requestContent);
            //
            using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
            {
                returnModel = JsonConvert.DeserializeAnonymousType(streamReader.ReadToEnd(), returnModel);
            }
            //if (returnModel != null && returnModel.Data != null)
            //{
            //    email = returnModel.Data.Email;
            //}
            //
            return returnModel;
        }

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
            response = request.GetResponse() as HttpWebResponse;
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
        public static Cookie GetAuthCookie(String url, String userName, String password)
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
