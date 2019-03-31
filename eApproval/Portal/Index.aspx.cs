using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using eApprovalBLL;

namespace eApproval.Portal
{
    public partial class Index : System.Web.UI.Page
    {

        eApprovalBLL.eApprovalBLL objBLL = new eApprovalBLL.eApprovalBLL();
        eApprovalBO.eApprovalBO objBO = new eApprovalBO.eApprovalBO();
        eApprovalBO.UserInfo objUserInfo = new eApprovalBO.UserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (this.Page.User.Identity.IsAuthenticated)
                {

                    Session.Remove("UserInfo");

                    FormsAuthentication.SignOut();

                    
                }
            }

            if (Request.QueryString["ReturnUrl"] != null)
            {
                Response.Redirect("~/Portal/Index.aspx");
            }




        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {

         
  
            DataTable dtLoginValidation = new DataTable();
            objBO.UserName = txtUserName.Text;
            objBO.Password = txtPassword.Text;
            dtLoginValidation = objBLL.LoginValidation(objBO);



            if (dtLoginValidation.Rows.Count > 0)
            {
                DataTable dtUserInfo = new DataTable();
                objBO.UserID = dtLoginValidation.Rows[0]["USERID"].ToString();
                objBO.UserName = dtLoginValidation.Rows[0]["USERNAME"].ToString();
                dtUserInfo = objBLL.GetUserInfo(objBO);

                objUserInfo.UserID = dtUserInfo.Rows[0]["USERID"].ToString();
                objUserInfo.UserName = dtUserInfo.Rows[0]["USERNAME"].ToString();
                objUserInfo.UserFullName = dtUserInfo.Rows[0]["USERFULLNAME"].ToString();
                objUserInfo.DeptID = dtUserInfo.Rows[0]["DEPTID"].ToString();
                objUserInfo.DepartName = dtUserInfo.Rows[0]["DEPTNAME"].ToString();
                objUserInfo.RoleID = dtUserInfo.Rows[0]["ROLEID"].ToString();
                objUserInfo.RoleName = dtUserInfo.Rows[0]["ROLENAME"].ToString();


                Session["UserInfo"] = objUserInfo;

                //Session["UserInfo"] = new eApprovalBO.UserInfo();

                //objUserInfo = (eApprovalBO.UserInfo)Session["UserInfo"];

                //string Redirecturl = ConfigurationSettings.AppSettings["PortalUrl"].ToString() + "Home.aspx?UName=" + txtUserName.Text;
                //Response.Redirect(Redirecturl);


                //New Concept Forms Authentication

                string roles = dtUserInfo.Rows[0]["ROLEID"].ToString();

                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, txtUserName.Text, DateTime.Now, DateTime.Now.AddMinutes(9880), false, roles, FormsAuthentication.FormsCookiePath);

                string hash = FormsAuthentication.Encrypt(ticket);

                
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, hash);

                if (ticket.IsPersistent)
                {
                    cookie.Expires = ticket.Expiration;
                }
                Response.Cookies.Add(cookie);
                Response.Redirect(FormsAuthentication.GetRedirectUrl(txtUserName.Text, false));


            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('Please Enter Valid Credentials!');", true);
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
            }


        }




    }
}