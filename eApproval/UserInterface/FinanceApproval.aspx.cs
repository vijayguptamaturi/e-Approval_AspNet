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
    public partial class FinanceApproval : System.Web.UI.Page
    {
        eApprovalBLL.eApprovalBLL objBLL = new eApprovalBLL.eApprovalBLL();
        eApprovalBLL.CommonBLL objCBLL = new eApprovalBLL.CommonBLL();
        eApprovalBO.eApprovalBO objBO = new eApprovalBO.eApprovalBO();
        public static UserInfo objUserInfo = new UserInfo();

        eApprovalBLL.ErrorLogBLL objErrorLogBLL = new eApprovalBLL.ErrorLogBLL();

        DataTable dtFileData = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["UserInfo"] != null)
                {

                    if (Request.QueryString["SRNO"] != null)
                    {

                        objUserInfo = (eApprovalBO.UserInfo)Session["UserInfo"];

                        grdFileData.DataSource = dtFileData;
                        grdFileData.DataBind();


                        GetSRNo();
                        GetLocations();
                        GetAccountHeads();
                  
                        FetchSignature();


                        txtSRNo.ReadOnly = true;
                        txtDate.ReadOnly = true;
                        txtAmountInWords.ReadOnly = true;
                        txtOriginatingDept.ReadOnly = true;
                        ddlLocation.Enabled = false;
                        ddlLocation.CssClass = "ColouredDropdowndisabled";
                        ddlAccountHead.Enabled = false;
                        ddlAccountHead.CssClass = "ColouredDropdowndisabled";
                        txtSectionRequestFor.ReadOnly = true;
                        txtAmountInAED.ReadOnly = true;
                        txtVendorRecommended.ReadOnly = true;
                        txtJustification.ReadOnly = true;
                        chkInitiate.Enabled = false;
                        btninitiateSubmit.Enabled = false;

                        txtSRType.ReadOnly = true;
                        txtOldSRNO.ReadOnly = true;

                        txtOldSRNO.Visible = false;
                        lblOldSRno.Visible = false;

                        //disabling other pannels

                        pnlCEOApproval.Enabled = false;
                        pnlMDApproval.Enabled = false;


                        BindSR_Data(Request.QueryString["SRNO"]);

                    }
                    else
                    {
                        Response.Redirect("~/UserInterface/ApprovalsList.aspx");
                    }

                }
                else
                {
                    Response.Redirect("~/Portal/Index.aspx");
                }



            }
        }

        private void GetSRNo()
        {
            try
            {

            objBO.UserID = objUserInfo.UserID;
            objBO.DeptID = Convert.ToInt32(objUserInfo.DeptID);

            string SrNo = objBLL.GetSrNo(objBO);

            if (SrNo != "")
            {
                txtSRNo.Text = SrNo;
            }
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }

        }


        private void GetLocations()
        {
            try
            {

            ddlLocation.DataSource = objCBLL.GetLocations();
            ddlLocation.DataTextField = "LOCATIONNAME";
            ddlLocation.DataValueField = "LOCATIONID";

            ddlLocation.DataBind();
            ddlLocation.Items.Insert(0, "--Select--");
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }

        }

        private void GetAccountHeads()
        {
            try
            {

            ddlAccountHead.DataSource = objCBLL.GetAccountHead();
            ddlAccountHead.DataTextField = "ACCOUNTNAME";
            ddlAccountHead.DataValueField = "ACCOUNTID";

            ddlAccountHead.DataBind();
            ddlAccountHead.Items.Insert(0, "--Select--");
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }

        }


        private void BindSR_Data(string SRNO)
        {
            try
            {


            DataSet dsSRData = new DataSet();

             objBO.SR_No=SRNO;

             dsSRData = objBLL.GetGetSRData_Acc(objBO);

            if(dsSRData.Tables.Count>0)
            {
                txtSRNo.Text = dsSRData.Tables[0].Rows[0]["SRNO"].ToString();
                txtDate.Text = dsSRData.Tables[0].Rows[0]["DATE"].ToString();
                txtOriginatingDept.Text = dsSRData.Tables[0].Rows[0]["DEPTNAME"].ToString();
                txtSectionRequestFor.Text = dsSRData.Tables[0].Rows[0]["SANCTIONREQUESTFOR"].ToString();

                txtAmountInAED.Text = Convert.ToDecimal(dsSRData.Tables[0].Rows[0]["AMOUNTAED"].ToString()) == 0 ? string.Empty : dsSRData.Tables[0].Rows[0]["AMOUNTAED"].ToString(); 


                txtAmountInWords.Text = dsSRData.Tables[0].Rows[0]["AMOUNTINWORDS"].ToString();
                txtVendorRecommended.Text = dsSRData.Tables[0].Rows[0]["VENDORNAME"].ToString();
                txtJustification.Text = dsSRData.Tables[0].Rows[0]["JUSTIFICATIONDETAILS"].ToString();

                ddlLocation.SelectedIndex = ddlLocation.Items.IndexOf(ddlLocation.Items.FindByValue(dsSRData.Tables[0].Rows[0]["LOCATIONID"].ToString()));
                ddlAccountHead.SelectedIndex = ddlAccountHead.Items.IndexOf(ddlAccountHead.Items.FindByValue(dsSRData.Tables[0].Rows[0]["ACCOUNTHEADID"].ToString()));


                txtSRType.Text = dsSRData.Tables[0].Rows[0]["SRTYPE"].ToString();

                if (dsSRData.Tables[0].Rows[0]["OLDSRNO"].ToString() != string.Empty && dsSRData.Tables[0].Rows[0]["OLDSRNO"].ToString() != null && dsSRData.Tables[0].Rows[0]["OLDSRNO"].ToString() != "")
                {
                    lblOldSRno.Visible = true;
                    txtOldSRNO.Visible = true;
                    txtOldSRNO.Text = dsSRData.Tables[0].Rows[0]["OLDSRNO"].ToString();
                }



                chkInitiate.Checked = true;

                byte[] bytes = (byte[])dsSRData.Tables[0].Rows[0]["INITIATORSIGN"];
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                ImgInitiaterSignature.ImageUrl = "data:image/png;base64," + base64String;


                if (dsSRData.Tables[1].Rows.Count > 0)
                {
                    grdFileData.DataSource = dsSRData.Tables[1];
                    grdFileData.DataBind();
                }
                


            }

            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }


        }

        protected void btn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "View")
            {

                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int grdIndex = gvRow.RowIndex;

                DataTable dtFileContent = new DataTable();

                objBO.SR_No = txtSRNo.Text;
                objBO.FileName = grdFileData.Rows[grdIndex].Cells[1].Text;

                dtFileContent = objBLL.GetSRFileContent(objBO);

                string FileName = dtFileContent.Rows[0]["FILENAME"].ToString();
                byte[] documentBytes = (byte[])dtFileContent.Rows[0]["FILECONTENT"];

                Response.ClearContent();
                Response.ContentType = "application/octectstream";
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", FileName));
                Response.AddHeader("Content-Length", documentBytes.Length.ToString());
                Response.BinaryWrite(documentBytes);
                Response.Flush();
                Response.Close();
            }
        }

        protected void grdFileData_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton viewimgbtn = e.Row.FindControl("btnView") as ImageButton;
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(viewimgbtn);

                ImageButton Deletebtn = e.Row.FindControl("btnDelete") as ImageButton;

                Deletebtn.Enabled = false;
                Deletebtn.CssClass = "DisbledImagebtn";
            }
        }

        protected void chkFinanceApprove_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFinanceApprove.Checked)
            {
                BindSignature();

            }
            else
            {
                imgFinanceSignature.ImageUrl = "";

            }
        }

        public void FetchSignature()
        {
            try
            {


            objBO.UserID = objUserInfo.UserID;

            byte[] bytes = objCBLL.GetSigContent(objBO);
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            ViewState["Sign"] = base64String;
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }
        }


        private void BindSignature()
        {
            imgFinanceSignature.ImageUrl = "data:image/png;base64," + (string)ViewState["Sign"];

        }

        protected void btnFinanceSubmit_Click(object sender, EventArgs e)
        {
            try
            {

            if (txtFinanceComments.Text == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Please Enter Some Commnets')", true);
                return;
            }

            if (!chkFinanceApprove.Checked)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Please check Approve Check Box and then Click Submit')", true);
                return;
            }

            string Result = string.Empty;

            objBO.SR_No = txtSRNo.Text;
            objBO.FinaceApprovalComments = txtFinanceComments.Text;
            objBO.UserID = objUserInfo.UserID;

            Result = objBLL.InsertFinaceApproval(objBO);

            if (Result == "S")
            {

                //Mail Sending Check From Database
                objBO.ParamID = 1;

                if (objCBLL.GetFormParameterValue(objBO) == "Y")
                {
                    eApprovalBLL.MailBLL MailBLL = new eApprovalBLL.MailBLL();

                    if (MailBLL.SendSRMail(Convert.ToInt32(objUserInfo.UserID), Convert.ToInt32(objUserInfo.RoleID), txtSRNo.Text) == "F")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Mail Sending is Failed.')", true);
                    }
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Approval Submitted Sucessfully with SR No : '+'" + txtSRNo.Text + "'); window.location='" + Request.ApplicationPath + "UserInterface/ApprovalsList.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Approval Submission is Failed.')", true);
            }


            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }

        }

    }
}