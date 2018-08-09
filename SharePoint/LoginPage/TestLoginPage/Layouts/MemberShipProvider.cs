using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;

namespace Layouts
{
    public class MyMemberShipProvider : SqlMembershipProvider
    {
        /// <summary>
        /// 初始化Membership
        /// </summary>
        public MyMemberShipProvider()
        {
            string connectionStringName = ConfigurationManager.AppSettings["ConnectionStringName"];
            string applicationName = ConfigurationManager.AppSettings["ApplicationName"];
            string membershipProviderName = ConfigurationManager.AppSettings["MembershipProviderName"];
            NameValueCollection nameValueCollection;
            //
            nameValueCollection = new NameValueCollection()
            {
                {"connectionStringName", connectionStringName},
                {"applicationName", applicationName},
                {"MinRequiredPasswordLength", "6"},
                {"MinRequiredNonAlphanumericCharacters", "0"}
            };
            this.Initialize(membershipProviderName, nameValueCollection);
        }

        /// <summary>
        /// 判斷使用者是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool ExistsUser(string userName)
        {
            return this.GetUser(userName, false) != null ? true : false;
        }

        /// <summary>
        /// 新增使用者
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool AddUser(string userName, string password, string email)
        {

            CreateUser(userName, password, email, "No Question", password, true, null, out MembershipCreateStatus status);
            return status == MembershipCreateStatus.Success ? true : false;
        }
    }
}
