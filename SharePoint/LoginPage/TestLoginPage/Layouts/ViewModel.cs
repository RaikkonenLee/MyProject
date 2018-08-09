using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layouts
{
    public class ReturnViewModel<T>
    {
        public ReturnViewModel() { }

        public T Data { get; set; }
        public ErrCode ErrCode { get; set; }
        public string Message { get; set; }
    }

    public class LoginUrlViewModel
    {
        public LoginUrlViewModel() { }

        public string APCT { get; set; }
        public string COMP { get; set; }
        public Guid Token { get; set; }
        public string URLADDRESS { get; set; }
    }

    public class LoginEmpnoViewModel
    {
        public LoginEmpnoViewModel() { }

        public string Email { get; set; }
        public string Empname { get; set; }
        public string Empno { get; set; }
        public string IdNumber { get; set; }
    }

    public enum ErrCode
    {
        Case0 = 0,
        Case1 = 1,
        Case2 = 2,
        Case3 = 3,
        Case4 = 4
    }
}
