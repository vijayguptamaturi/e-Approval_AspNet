using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using eApprovalBO;
using eApprovalBLL;
using System.Reflection;

namespace eApproval.UserInterface.Master
{
    public partial class UserProfile_New : System.Web.UI.Page
    {
        eApprovalBLL.eApprovalBLL objBLL = new eApprovalBLL.eApprovalBLL();
        eApprovalBLL.CommonBLL objCBLL = new eApprovalBLL.CommonBLL();
        eApprovalBO.eApprovalBO objBO = new eApprovalBO.eApprovalBO();
        public static UserInfo objUserInfo = new UserInfo();

        eApprovalBLL.ErrorLogBLL objErrorLogBLL = new ErrorLogBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
          
                if (Session["UserInfo"] != null)
                {
                    objUserInfo = (eApprovalBO.UserInfo)Session["UserInfo"];
                    GetDepartments();
                    GetRoles();
                    GetNextUserID();

                    if (Request.QueryString["UID"] != null)
                    {
                        BindUserDetails(Request.QueryString["UID"]);
                        txtFullName.Focus();
                        btnSave.Text = "Update";
                        lblUProfile.Text = "Update Profile";
                    }
                    else
                    {
                        txtUserName.Focus();
                        btnSave.Text = "Save";
                        lblUProfile.Text = "New User";
                    }

                }

                else
                {
                    Response.Redirect("~/Portal/Index.aspx");
                }

               
            }
        }


        public void GetDepartments()
        {

            try
            {

                ddlDept.DataSource = objCBLL.GetDepartments();

                ddlDept.DataTextField = "DEPT NAME";
                ddlDept.DataValueField = "DEPT ID";
                ddlDept.DataBind();
                ddlDept.Items.Insert(0, "--Select--");
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }

        }

        public void GetRoles()
        {
            try
            {

            ddlRole.DataSource = objCBLL.GetRoles();

            ddlRole.DataTextField = "ROLE NAME";
            ddlRole.DataValueField = "ROLE ID";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, "--Select--");
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }
        }

        public void GetNextUserID()
        {
            try
            {
                txtUserID.Text = objBLL.GetNext_User_ID().ToString();
            }
         
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }

            

        }

        public void BindUserDetails(string Userid)
        {

            try
            {

    

            objBO.UserID = Userid;
            DataTable dtBindDetails = new DataTable();
            dtBindDetails = objBLL.GetUserProfile(objBO);

            if (dtBindDetails.Rows.Count > 0)
            {
                txtUserID.Text = dtBindDetails.Rows[0]["USERID"].ToString();
                txtUserName.Text = dtBindDetails.Rows[0]["USERNAME"].ToString();
                //txtPassword.Text = dtBindDetails.Rows[0]["USERID"].ToString();
                //txtConfirmPassword.Text = dtBindDetails.Rows[0]["USERID"].ToString();
                txtFullName.Text = dtBindDetails.Rows[0]["UFULLNAME"].ToString();
                txtEmailID.Text = dtBindDetails.Rows[0]["EMAILID"].ToString();
                txtMobileNo.Text = dtBindDetails.Rows[0]["MOBILENO"].ToString();
                ddlDept.SelectedIndex = ddlDept.Items.IndexOf(ddlDept.Items.FindByText(dtBindDetails.Rows[0]["DEPTNAME"].ToString()));
                ddlRole.SelectedIndex = ddlRole.Items.IndexOf(ddlRole.Items.FindByText(dtBindDetails.Rows[0]["ROLENAME"].ToString()));

                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                txtConfirmPassword.Enabled = false;
            }
            }

            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (btnSave.Text == "Update")
            {

                UpdateProfile();
            }
            else
            {
                InsertProfile();
            }
        }

        private void InsertProfile()
        {
            try
            {

         

            string Result = string.Empty;

            byte[] documentcontent = null;

            if (FileUploadToServer.HasFile)
            {

                documentcontent = FileUploadToServer.FileBytes;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Please Upload the Signature File')", true);
                return;
            }
            objBO.UserID = txtUserID.Text;
            objBO.UserName = txtUserName.Text;
            objBO.Password = txtPassword.Text;
            objBO.FullName = txtFullName.Text;
            objBO.EmailID = txtEmailID.Text;
            objBO.MobileNo = txtMobileNo.Text;
            objBO.DeptID = Convert.ToInt32(ddlDept.SelectedValue);
            objBO.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
            objBO.SignatureContent = documentcontent;
            objBO.LoginUserID = objUserInfo.UserID;

            Result = objBLL.InsertUserProfile(objBO);

            if (Result == "S")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('User Created Sucessfully')", true);
                this.ClearTextFileds();
                GetNextUserID();
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('User Creation is Failed')", true);
            }

            }

            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }


        }

        private void ClearTextFileds()
        {
            try
            {

            
            txtUserID.Text = "";
            txtUserName.Text = "";
            txtFullName.Text = "";
            txtMobileNo.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtMobileNo.Text = "";
            txtEmailID.Text = "";
            ddlDept.SelectedIndex = -1;
            ddlRole.SelectedIndex = -1;
            }

            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }
        }


        private void UpdateProfile()
        {
            try
            {

        

            string Result = string.Empty;

            byte[] documentcontent = null;

            if (FileUploadToServer.HasFile)
            {
                documentcontent = FileUploadToServer.FileBytes;
            }
           
            objBO.UserID = txtUserID.Text;
            objBO.FullName = txtFullName.Text;
            objBO.EmailID = txtEmailID.Text;
            objBO.MobileNo = txtMobileNo.Text;
            objBO.DeptID = Convert.ToInt32(ddlDept.SelectedValue);
            objBO.RoleID = Convert.ToInt32(ddlRole.SelectedValue);
            objBO.SignatureContent = documentcontent;
            objBO.LoginUserID = objUserInfo.UserID;

            Result = objBLL.UpdateUserProfile(objBO);

            if (Result == "S")
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('User Updated Sucessfully')", true);
                //Response.Redirect("~/UserInterface/Master/UserProfile.aspx");

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('User Updated Sucessfully'); window.location='" + Request.ApplicationPath + "~/UserInterface/Master/UserProfile.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('User Updation is Failed')", true);
            }
            }

            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/UserInterface/Master/UserProfile_New.aspx");
        }

       

    }
}