using eApprovalBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Reflection;

namespace eApproval.UserInterface
{
    public partial class ApprovalsList : System.Web.UI.Page
    {
        eApprovalBLL.eApprovalBLL objBLL = new eApprovalBLL.eApprovalBLL();
        eApprovalBLL.CommonBLL objCBLL = new eApprovalBLL.CommonBLL();
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

                    BindGrid();
                    
                }
                else
                {
                    Response.Redirect("~/Portal/Index.aspx");
                }



            }
        }

        private void BindGrid()
        {
            try
            {

                objBO.RoleID = Convert.ToInt32(objUserInfo.RoleID);

                grdApprovalList.DataSource = objBLL.GetApprovalsList(objBO);
                grdApprovalList.DataBind();
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }

        }

        protected void grdApprovalList_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = grdApprovalList.SelectedRow;
            string SRNO = row.Cells[1].Text;

            string EncodedSRNO = Server.UrlEncode(SRNO);

            if (objUserInfo.RoleID == "2")
            {
                Response.Redirect("~/UserInterface/FinanceApproval.aspx" + "?SRNO=" + EncodedSRNO);
            }
            if (objUserInfo.RoleID == "3")
            {
                Response.Redirect("~/UserInterface/CEOApproval.aspx" + "?SRNO=" + EncodedSRNO);
            }
        }

        protected void grdApprovalList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdApprovalList.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void grdApprovalList_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            GridViewRow pagerRow = (GridViewRow)gv.BottomPagerRow;

            if (pagerRow != null && pagerRow.Visible == false)
                pagerRow.Visible = true;
        }
    }
}