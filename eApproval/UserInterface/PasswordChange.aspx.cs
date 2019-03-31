using eApprovalBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eApproval.UserInterface
{
    public partial class PasswordChange : System.Web.UI.Page
    {

        eApprovalBLL.eApprovalBLL objBLL = new eApprovalBLL.eApprovalBLL();
        eApprovalBLL.CommonBLL objCBLL = new eApprovalBLL.CommonBLL();
        eApprovalBO.eApprovalBO objBO = new eApprovalBO.eApprovalBO();
        public static UserInfo objUserInfo = new UserInfo();


        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {


                if (Session["UserInfo"] != null)
                {
                    objUserInfo = (eApprovalBO.UserInfo)Session["UserInfo"];

                    txtUserID.Text = objUserInfo.UserID;
                    txtUserName.Text = objUserInfo.UserName;

                }

                else
                {
                    Response.Redirect("~/Portal/Index.aspx");
                }


            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           
                if (txtOldPassword.Text != string.Empty && txtNewPassword.Text != string.Empty &&  txtConfirmPassword.Text != string.Empty)
                {

                    ChangePassword();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('All text fields are Mandatory')", true);
                }
            }
          


        private void ChangePassword()
        {
            objBO.UserID = txtUserID.Text;
            objBO.Password = txtOldPassword.Text;

            if (objCBLL.IsOldPasswordMatch(objBO) == 1)
            {
                objBO.UserID = txtUserID.Text;
                objBO.Password = txtOldPassword.Text;
                objBO.NewPassword = txtNewPassword.Text;

                if (objCBLL.ChangePassword(objBO) == 1)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Password is Changed Sucessfully..')", true);
                    txtOldPassword.Text = "";
                    txtNewPassword.Text = "";
                    txtConfirmPassword.Text = "";
                    txtOldPassword.Focus();

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Updation is failed Due to some Error..')", true);
                }
               

            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Entered Old Password is Wrong')", true);
                txtOldPassword.Focus();
            }
        }




        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtOldPassword.Text = "";
            txtNewPassword.Text = "";
            txtConfirmPassword.Text = "";
        }
    }
}