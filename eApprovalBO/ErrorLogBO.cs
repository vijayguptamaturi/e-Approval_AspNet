using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eApprovalBO
{
     public class ErrorLogBO
    {
        public string MethodName { get; set; }

        public string PageName { get; set; }

        public string UserName { get; set; }

        public string ErrorMessage { get; set; }
    }
}
