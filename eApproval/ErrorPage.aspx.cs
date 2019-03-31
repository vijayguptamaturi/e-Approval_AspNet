using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eApproval
{
    public partial class ErrorPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["ErrorMessage"] != null)
            {
                lblMsg.Text = "There is something went wrong please contact support team... Your Ref ID.." + HttpContext.Current.Session["ErrorMessage"];
                Session.Abandon();

                Session.Remove("UserInfo");

                FormsAuthentication.SignOut();

            }

            if (lblMsg.Text == string.Empty)
            {
                lblMsg.Text = "Server is not responding please contact support team";

            }
        }
    }
}