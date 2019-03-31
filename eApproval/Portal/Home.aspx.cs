using eApprovalBLL;
using eApprovalBO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eApproval.Portal
{
    public partial class Home : System.Web.UI.Page
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


                    if (Request.QueryString["UName"] != null)
                    {

                        string UName = Request.QueryString["UName"];

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", "Javascript:WelcomeNote('" + UName + "');", true);

                    }

                    objUserInfo = (eApprovalBO.UserInfo)Session["UserInfo"];

                    if (objUserInfo.RoleID == "1")
                    {
                        RequsetsDiv.Visible = true;
                        ApprovalDiv.Visible = false;
                    }

                    if (objUserInfo.RoleID == "2" || objUserInfo.RoleID == "3")
                    {
                        RequsetsDiv.Visible = false;
                        ApprovalDiv.Visible = true;
                    }

                    if (objUserInfo.RoleID == "5") // AccountsView
                    {
                        RequsetsDiv.Visible = true;
                        ApprovalDiv.Visible = false;

                        ImgbtnInitiateRequest.Enabled = false;
                        ImgbtnMyProfile.Enabled = false;
                        ImgbtnSettings.Enabled = false;
                        ImgbtnReports.Enabled = false;
                    }

                    if (objUserInfo.RoleID == "6") //Admin
                    {
                        RequsetsDiv.Visible = true;
                        ApprovalDiv.Visible = false;

                        ImgbtnInitiateRequest.Enabled = false;
                    }



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



                DataTable dtApprovalsListView_Home = new DataTable();

                objBO.RoleID = Convert.ToInt32(objUserInfo.RoleID);
                objBO.DeptID = Convert.ToInt32(objUserInfo.DeptID);

                dtApprovalsListView_Home = objBLL.GetViewApprovalsList_HomePage(objBO);

                grdApprovalViewList.DataSource = dtApprovalsListView_Home;
                grdApprovalViewList.DataBind();


            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }


        }

        protected void grdApprovalViewList_SelectedIndexChanged(object sender, EventArgs e)
        {

            GridViewRow row = grdApprovalViewList.SelectedRow;
            string SRNO = row.Cells[1].Text;

            string EncodedSRNO = Server.UrlEncode(SRNO);



            if (objUserInfo.RoleID == "1")
            {
                Response.Redirect("~/UserInterface/ViewApprovals.aspx" + "?SRNO=" + EncodedSRNO);
            }
            else if (objUserInfo.RoleID == "2")
            {
                Response.Redirect("~/UserInterface/FinanceApproval.aspx" + "?SRNO=" + EncodedSRNO);
            }
            else if (objUserInfo.RoleID == "3")
            {
                Response.Redirect("~/UserInterface/CEOApproval.aspx" + "?SRNO=" + EncodedSRNO);
            }



        }

        protected void grdApprovalViewList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdApprovalViewList.PageIndex = e.NewPageIndex;
            BindGrid();
        }

        protected void grdApprovalViewList_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            GridViewRow pagerRow = (GridViewRow)gv.BottomPagerRow;

            if (pagerRow != null && pagerRow.Visible == false)
                pagerRow.Visible = true;
        }


    }
}