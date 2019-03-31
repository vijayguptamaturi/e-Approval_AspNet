using eApprovalBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Reflection;

namespace eApproval.UserInterface
{
    public partial class ViewApprovals : System.Web.UI.Page
    {
        eApprovalBLL.eApprovalBLL objBLL = new eApprovalBLL.eApprovalBLL();
        eApprovalBLL.CommonBLL objCBLL = new eApprovalBLL.CommonBLL();
        eApprovalBO.eApprovalBO objBO = new eApprovalBO.eApprovalBO();
        public static UserInfo objUserInfo = new UserInfo();


        eApprovalBLL.ErrorLogBLL objErrorLogBLL = new eApprovalBLL.ErrorLogBLL();

        DataTable dtFiledata = new DataTable();

        public static string base64String = string.Empty;
        public static string base64String_Acc = string.Empty;
        public static string base64String_CEO = string.Empty;

        public static string RdlcChkBit = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                if (Session["UserInfo"] != null)
                {

                    if (Request.QueryString["SRNO"] != null)
                    {

                        objUserInfo = (eApprovalBO.UserInfo)Session["UserInfo"];

                        grdFileData.DataSource = dtFiledata;
                        grdFileData.DataBind();


                        GetSRNo();
                        GetLocations();
                        GetAccountHeads();

                        Disablingcontrols();

                        lnkbtnPrint.Visible = false;


                        txtSRType.ReadOnly = true;
                        txtOldSRNO.ReadOnly = true;

                        txtOldSRNO.Visible = false;
                        lblOldSRno.Visible = false;


                        BindSRData(Request.QueryString["SRNO"]);

                       

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


        protected void grdUploadData_RowCreated(object sender, GridViewRowEventArgs e)
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


        private void BindSRData(string SRNO)
        {
            try
            {

            DataSet dsSRData = new DataSet();

            objBO.SR_No = SRNO;

            dsSRData = objBLL.GetSRDataViewApprovals(objBO);


            if (dsSRData.Tables.Count > 0)
            {

                if (dsSRData.Tables[0].Rows.Count > 0) //Initiation Fileds bind
                {
                    base64String = string.Empty;


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
                    base64String = Convert.ToBase64String(bytes, 0, bytes.Length);

                    ImgInitiaterSignature.ImageUrl = "data:image/png;base64," + base64String;

                    ImgInitiaterSignature.CssClass = "Border";
                }


                if (dsSRData.Tables[1].Rows.Count > 0) //finace fileds binding
                {

                    base64String_Acc = string.Empty;

                    txtFinanceComments.Text = dsSRData.Tables[1].Rows[0]["ACCCOMMENTS"].ToString();

                    chkFinanceApprove.Checked = true;

                    byte[] bytes_Acc = (byte[])dsSRData.Tables[1].Rows[0]["ACCSIGNCONTENT"];
                     base64String_Acc = Convert.ToBase64String(bytes_Acc, 0, bytes_Acc.Length);

                    imgFinanceSignature.ImageUrl = "data:image/png;base64," + base64String_Acc;

                    imgFinanceSignature.CssClass = "Border";
                }


                if (dsSRData.Tables[2].Rows.Count > 0) //finace fileds binding
                {

                    base64String_CEO = string.Empty;

                    txtCEOComments.Text = dsSRData.Tables[2].Rows[0]["CEOCOMMENTS"].ToString();

                    string ChkBit = dsSRData.Tables[2].Rows[0]["CHKCEOAPP"].ToString().Trim();

                    RdlcChkBit = ChkBit;

                    if ( ChkBit == "A")
                    {
                        chkCEOApprove.SelectedIndex = 0;
                    }
                    else
                    {
                         chkCEOApprove.SelectedIndex = 1;
                    
                    }

                    byte[] bytes_CEO = (byte[])dsSRData.Tables[2].Rows[0]["CEOSIGNCONTENT"];
                    base64String_CEO = Convert.ToBase64String(bytes_CEO, 0, bytes_CEO.Length);

                    imgCEO.ImageUrl = "data:image/png;base64," + base64String_CEO;

                    imgCEO.CssClass = "Border";

                    lnkbtnPrint.Visible = true;

                    ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                    scriptManager.RegisterPostBackControl(this.lnkbtnPrint);
                }
                
                if (dsSRData.Tables[3].Rows.Count > 0) //File grid binding
                {
                    grdFileData.DataSource = dsSRData.Tables[3];
                    grdFileData.DataBind();

                    
                }

            }
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }

        }

        private void Disablingcontrols()
        {
            try
            {

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
           

            txtFinanceComments.ReadOnly = true;
            chkFinanceApprove.Enabled = false;

            txtCEOComments.ReadOnly = true;
            chkCEOApprove.Enabled = false;
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

     

        protected void lnkbtnPrint_Click(object sender, EventArgs e)
        {
            try
            {

            string contentType = string.Empty;
            contentType = "application/pdf";
            DataTable dsData = new DataTable();
           // dsData = LoopGrid();

            dsData.Columns.Add("SRNO", typeof(string));
            dsData.Columns.Add("LOCATION", typeof(string));
            dsData.Columns.Add("DATE", typeof(string));

            dsData.Rows.Add(txtSRNo.Text, ddlLocation.SelectedItem.Text,txtDate.Text);


            string ConsolAmount = string.Empty;

            if (txtAmountInAED.Text != string.Empty && txtAmountInAED.Text != "")
            {
                ConsolAmount = txtAmountInAED.Text + " (AED " + txtAmountInWords.Text + ")";
            }
            else
            {
                ConsolAmount = " ----";
            }

            


            string FileName = "SanctionRequest" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
            string extension;
            string encoding;
            string mimeType;
            string[] streams;
            Warning[] warnings;

            LocalReport report = new LocalReport();
            report.ReportPath = Server.MapPath("~/UserInterface/Rdlc/SRPrint.rdlc");
            ReportDataSource rds = new ReportDataSource();
            rds.Name = "SRDataset";//This refers to the dataset name in the RDLC file 
            rds.Value = dsData;
            report.DataSources.Add(rds);


            string strSRType = string.Empty;

            strSRType = txtSRType.Text;

            if (txtOldSRNO.Visible)
            {
                strSRType = strSRType + " (" + txtOldSRNO.Text + " )";
            }

            string ApprovalTick = string.Empty;
            string ApprovalEBox = string.Empty;
            string DisApprovalTick = string.Empty;
            string DisapprovalEBox = string.Empty;

            if (RdlcChkBit == "A")
            {
                ApprovalTick = "T";
                ApprovalEBox = "F";

                DisApprovalTick = "F";
                DisapprovalEBox = "T";
            }
            else if (RdlcChkBit == "R")
            {
                ApprovalTick = "F";
                ApprovalEBox = "T";

                DisApprovalTick = "T";
                DisapprovalEBox = "F";
            }


           // string date = DateTime.Now.ToString();
            ReportParameter[] rptparams = new ReportParameter[]{
           // new ReportParameter("SRNO",txtSRNo.Text),
           //new ReportParameter("LOCATION",ddlLocation.SelectedItem.Text),
           // new ReportParameter("DATE",txtDate.Text),
          
            new ReportParameter("ORIGINATINGDEPT",txtOriginatingDept.Text),
             new ReportParameter("SRTYPE",strSRType),
            new ReportParameter("SANCTIONREQUESTFOR",txtSectionRequestFor.Text),
            new ReportParameter("ACCOUNTHEAD",ddlAccountHead.SelectedItem.Text),
            new ReportParameter("AMOUNTAED",ConsolAmount),
            new ReportParameter("AMOUNTWORDS",txtOriginatingDept.Text),
            new ReportParameter("VENDORRECOMENDED",txtVendorRecommended.Text),
             new ReportParameter("JUSTIFICATIONDETAILS",txtJustification.Text),
              new ReportParameter("FINANCECOMMENTS",txtFinanceComments.Text),
             new ReportParameter("CEOCOMMENTS",txtCEOComments.Text),
             new ReportParameter("INITIATORSIGNATURE",base64String),
             new ReportParameter("FINANCESIGNATURE",base64String_Acc),
             new ReportParameter("CEOSIGNATURE",base64String_CEO),
             
             //Rdlc CEO Check Box VISIBILITY

             new ReportParameter("APPROVALTICK",ApprovalTick),
             new ReportParameter("APPROVALEBOX",ApprovalEBox),

             new ReportParameter("DISAPPROVALTICK",DisApprovalTick),
             new ReportParameter("DISAPPROVALEBOX",DisapprovalEBox)

            };
            report.SetParameters(rptparams);

            Byte[] mybytes = report.Render("PDF", null,
                            out extension, out encoding,
                            out mimeType, out streams, out warnings); //for exporting to PDF  
            using (FileStream fs = File.Create(Server.MapPath("~/download/") + FileName))
            {
                fs.Write(mybytes, 0, mybytes.Length);
            }

            Response.ClearHeaders();
            Response.ClearContent();
            Response.Buffer = true;
            Response.Clear();
            Response.ContentType = contentType;
            Response.AddHeader("Content-Disposition", "attachment; filename=" + FileName);
            Response.WriteFile(Server.MapPath("~/download/" + FileName));
            Response.Flush();
            Response.Close();
          //  Response.End();

            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }

            }
        

    }
}