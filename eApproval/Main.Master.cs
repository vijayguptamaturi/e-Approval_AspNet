using eApprovalBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eApproval
{
    public partial class Main : System.Web.UI.MasterPage
    {
        //public static eApprovalBO.UserInfo sessionUInfo = Session["UserInfo"] as eApprovalBO.UserInfo;

        public static UserInfo objUserInfo = new UserInfo();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                if(Session["UserInfo"]!=null)
                {
                    objUserInfo = (eApprovalBO.UserInfo)Session["UserInfo"];

                    lblUserName.Text = objUserInfo.UserFullName;

                    //lblUserName.Text = sessionUInfo.UserFullName;

                    LinksVisibility();

                    
                }
                else
                {
                    Response.Redirect("~/Portal/Index.aspx");
                }
            }
        }

        //protected void lnkInititateRequest_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/UserInterface/InitiateRequest.aspx");
        //}

        //protected void lnkApprovals_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/UserInterface/ApprovalsList.aspx");
        //}

        //protected void lnkViews_Click(object sender, EventArgs e)
        //{
        //    Response.Redirect("~/UserInterface/ViewsList.aspx");
        //}


        private void LinksVisibility()
        {
            //Initiator
            if (objUserInfo.RoleID == "1")
            {
                aInitiateRequest.Visible = true;
                aApprovalList.Visible = false;
                aMaster.Visible = false;

            }

            if (objUserInfo.RoleID == "2" || objUserInfo.RoleID == "3") //Acc and CEO approval
            {
                aInitiateRequest.Visible = false;
                aApprovalList.Visible = true;
                aMaster.Visible = false;
            }

            if (objUserInfo.RoleID == "5") // AccountsView
            {
                aInitiateRequest.Visible = false;
                aApprovalList.Visible = false;
                aViewsList.Visible = true;
                aMaster.Visible = false;
            }

            if (objUserInfo.RoleID == "6") //Admin
            {
                aInitiateRequest.Visible = false;
                aApprovalList.Visible = false;
                aViewsList.Visible = false;
                aMaster.Visible = true;
            }
        }


    }
}