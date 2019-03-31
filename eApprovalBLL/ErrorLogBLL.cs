using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Xml.Linq;
using System.Web.SessionState;

namespace eApprovalBLL
{
    public class ErrorLogBLL
    {
        public void ErrorLog(string MethodName, string PageName, string ErrorMessage, string UserName)
        {
            string ErrorCode = string.Empty;
            eApprovalDB.ErrorLogDB objErrorLogDB = new eApprovalDB.ErrorLogDB();
            eApprovalBO.ErrorLogBO objErrorLogBO = new eApprovalBO.ErrorLogBO();

            objErrorLogBO.MethodName = MethodName;
            objErrorLogBO.PageName = PageName;
            objErrorLogBO.ErrorMessage = ErrorMessage;
            objErrorLogBO.UserName = UserName;
            ErrorCode = objErrorLogDB.ErrorLog(objErrorLogBO);
            HttpContext.Current.Session["ErrorMessage"] = ErrorCode;

            if (HttpContext.Current.Session["ErrorMessage"] != null)
            {
                System.Web.HttpContext.Current.Response.Redirect("/ErrorPage.aspx", false);
            }
        }
    }
}
