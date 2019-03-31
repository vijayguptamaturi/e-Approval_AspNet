using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;
using eApprovalBO;

namespace eApproval.UserInterface.Master
{
    public partial class UserProfile : System.Web.UI.Page
    {
        eApprovalBLL.eApprovalBLL objBLL = new eApprovalBLL.eApprovalBLL();
        eApprovalBO.eApprovalBO objBO = new eApprovalBO.eApprovalBO();
        public static UserInfo objUserInfo = new UserInfo();
        eApprovalBLL.ErrorLogBLL objErrorLogBLL = new eApprovalBLL.ErrorLogBLL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["UserInfo"] != null)
                {
                    objUserInfo = (eApprovalBO.UserInfo)Session["UserInfo"];

                    BindUserGrid();

                }

                else
                {
                    Response.Redirect("~/Portal/Index.aspx");
                }

            }
        }

        protected void grdUserProfile_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
          

                grdUserProfile.PageIndex = e.NewPageIndex;
                BindUserGrid();
          
        }

        private void BindUserGrid()
        {
            try
            {

                objBO.UserID = "0";
                grdUserProfile.DataSource = objBLL.GetUserProfile(objBO);
                grdUserProfile.DataBind();

            }

            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }

        }



        protected void grdUserProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = grdUserProfile.SelectedRow;
            string UserID = row.Cells[0].Text;
            Response.Redirect("~/UserInterface/Master/UserProfile_New.aspx" + "?UID=" + UserID);

        }

        protected void grdUserProfile_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            GridViewRow pagerRow = (GridViewRow)gv.BottomPagerRow;

            if (pagerRow != null && pagerRow.Visible == false)
                pagerRow.Visible = true;
        }
    }
}