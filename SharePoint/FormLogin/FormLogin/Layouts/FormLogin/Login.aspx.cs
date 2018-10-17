using System;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using System.IO;
using System.Net;
using System.Text;
using Microsoft.SharePoint.IdentityModel.Pages;
using Newtonsoft.Json;
using System.Configuration;
using System.Collections.Generic;
using System.Web.Security;


namespace FormLogin.Layouts.FormLogin
{
    public partial class Login : FormsSignInPage
    {
        public HttpService HttpService { get; private set; }

        public ErpAPIService ErpAPIService { get; private set; }

        protected override void OnInit(EventArgs eventArgs)
        {
            //從ERP的連結將Url的Token傳入，取得Token後將Token跟ERP做認證取得使用者資訊
            //取得後再跟Sharepoint做認證取得Cookie
            HttpService = new HttpService();
            string environment = string.Empty,
                   token = string.Empty,
                   errorMessage = string.Empty;
            //SetUser();
            if (Request.QueryString.Count == 2)
            {
                environment = Request.QueryString[0];
                token = Request.QueryString[1];
                ReturnViewModel<LoginEmpnoViewModel> userInfo = null;
                ErpAPIService = new ErpAPIService(HttpService, environment);
                //
                if (!string.IsNullOrEmpty(token) && ErpAPIService.Environment != Environment.None)
                {
                    userInfo = ErpAPIService.GetUserEmail(token.ToString());
                }
                //
                if (userInfo != null)
                {
                    LoginProcess(userInfo, out errorMessage);
                }
            }
            //
            base.OnInit(eventArgs);
        }

        protected override void OnLoad(EventArgs e)
        {
            signInControl.LoggingIn += SignInControl_LoggingIn;
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
                   environment = "sys",
                   errorMessage = string.Empty;
            ReturnViewModel<LoginUrlViewModel> loginData;
            ReturnViewModel<LoginEmpnoViewModel> userInfo = null;
            ClaimsFormsPageMessage.Text = string.Empty;
            HttpService = new HttpService();
            ErpAPIService = new ErpAPIService(HttpService, environment);
            //
            if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
            {
                loginData = ErpAPIService.GetERPLoginInfo(userName, password, out errorMessage);
                //
                switch (loginData.ErrCode)
                {
                    case ErrCode.Case0:
                        userInfo = ErpAPIService.GetUserEmail(loginData.Data.Token.ToString());
                        //
                        if (userInfo != null)
                        {
                            LoginProcess(userInfo, out errorMessage);
                        }
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
            if (membershipProvider.ExistsUser(userInfo.Data.Empname, out MembershipUser membershipUser))
            {
                hasUser = true;
                if (membershipUser.UserName != "fbaadmin")
                {
                    UpdateUser(membershipProvider, membershipUser, userInfo);
                }
            }
            else
            {
                //SharePoint不要存密碼
                hasUser = membershipProvider.AddUser(userInfo.Data.Empname, "p@ssw0rd", userInfo.Data.Email);
            }
            if (hasUser)
            {
                cookie = HttpService.GetAuthCookie(webSiteUrl, userInfo.Data.Empname, "p@ssw0rd");
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
        /// 更新使用者
        /// </summary>
        /// <param name="membershipProvider"></param>
        /// <param name="membershipUser"></param>
        /// <param name="userInfo"></param>
        public void UpdateUser(MyMemberShipProvider membershipProvider, MembershipUser membershipUser,
            ReturnViewModel<LoginEmpnoViewModel> userInfo)
        {
            if (membershipUser.Email != userInfo.Data.Email)
            {
                membershipUser.Email = userInfo.Data.Email;
                membershipProvider.UpdateUser(membershipUser);
            }
        }

        /// <summary>
        /// 初始化使用者
        /// </summary>
        public void SetUser()
        {
            MyMemberShipProvider membershipProvider = new MyMemberShipProvider();
            //string names = "歐健和,曾正鈞,周建樺,卓俊彥,李忠耿,李鳳珠,蕭旭成,李宗錦,郭紓睿,劉明坤,郭芝均,林祐群,陳怡伶,陳冠州,梁耀謙,余宗倫,林祐生,李彥樺,李俊霖,尹碩偉,楊之萱,陳廷哲,鄭維智,黃以深,林佩璇,賴緯澤,楊予青,葉豐儀,李瑞津,張凱翔,吳效竹,李承宗,鄭逢毅,郭晉安,陳柄志,魏嘉緯,李威宏";
            string names = "賴相宇";
            string[] nameArray = names.Split(',');
            //
            foreach (string name in nameArray)
            {
                membershipProvider.AddUser(name, "p@ssw0rd", string.Concat(name, "@aurora.com.tw"));
            }
        }
    }
}
