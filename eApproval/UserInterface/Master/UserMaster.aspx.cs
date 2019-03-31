using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eApproval.UserInterface.Master
{
    public partial class UserMaster : System.Web.UI.Page
    {
        eApprovalBLL.eApprovalBLL objBLL = new eApprovalBLL.eApprovalBLL();
        eApprovalBLL.CommonBLL objCBLL = new eApprovalBLL.CommonBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblNewTab.Text = "Add New";
                btnSave.Text = "Save";
                GetDepartments();
                GetRoles();
                txtUserName.Focus();
            }
        }

        public void GetDepartments()
        {
            ddlDept.DataSource = objCBLL.GetDepartments();

            ddlDept.DataTextField = "DEPT NAME";
            ddlDept.DataValueField = "DEPT ID";
            ddlDept.DataBind();
            ddlDept.Items.Insert(0, "--Select--");
        }

        public void GetRoles()
        {
            ddlRole.DataSource = objCBLL.GetRoles();

            ddlRole.DataTextField = "ROLE NAME";
            ddlRole.DataValueField = "ROLE ID";
            ddlRole.DataBind();
            ddlRole.Items.Insert(0, "--Select--");
        }

    }
}