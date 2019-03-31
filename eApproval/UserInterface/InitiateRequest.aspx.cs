using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using eApproval;
using eApprovalBO;
using System.Xml.Serialization;
using System.Text;
using System.Reflection;

namespace eApproval.UserInterface
{

    public partial class InitiateRequest : System.Web.UI.Page
    {

        eApprovalBLL.eApprovalBLL objBLL = new eApprovalBLL.eApprovalBLL();
        eApprovalBLL.CommonBLL objCBLL = new eApprovalBLL.CommonBLL();
        eApprovalBO.eApprovalBO objBO = new eApprovalBO.eApprovalBO();
        public static UserInfo objUserInfo = new UserInfo();

        eApprovalBLL.ErrorLogBLL objErrorLogBLL = new eApprovalBLL.ErrorLogBLL();
        

        DataTable dtFiledata = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                if (Session["UserInfo"] != null)
                {
                    objUserInfo = (eApprovalBO.UserInfo)Session["UserInfo"];

                    grdUploadData.DataSource = dtFiledata;
                    grdUploadData.DataBind();
                    ViewState["FileData"] = null;
                   
                    GetSRNo();
                    GetLocations();
                    GetAccountHeads();
                    txtDate.Text = DateTime.Now.ToShortDateString();
                    txtOriginatingDept.Text = objUserInfo.DepartName;

                    FetchSignature();

                    

                    txtSRNo.ReadOnly = true;
                    txtDate.ReadOnly = true;
                    txtAmountInWords.ReadOnly = true;
                    txtOriginatingDept.ReadOnly = true;

                    txtSRNo.Focus();


                    //Disabling Reamining Pannels

                    pnlFinanceRoutingDept.Enabled = false;
                    pnlCEOApproval.Enabled = false;
                    pnlMDApproval.Enabled = false;


                    lblOldSRno.Visible = false;
                    ddloldSrNo.Visible = false;


                   
                }
                else
                {
                    Response.Redirect("~/Portal/Index.aspx");
                }



            }

            //ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
            //scriptManager.RegisterPostBackControl(this.grdUploadData);
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

    


        private void CreateDatatable()
        {

            dtFiledata.Columns.Add("FileName", typeof(string));
            dtFiledata.Columns.Add("Exn", typeof(string));
            dtFiledata.Columns.Add("FileSize", typeof(Int64));
            dtFiledata.Columns.Add("FileContent", typeof(byte[]));

        }

        protected void btnOk_Click(object sender, EventArgs e)
        {

            if (FileUpload.HasFiles)
            {

                if (ViewState["FileData"] == null)
                {
                    CreateDatatable();
                    GetFilesFromUploadControl();
                }
                else
                {
                    dtFiledata = (DataTable)ViewState["FileData"];

                    GetFilesFromUploadControl();
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "Warning", "alert('Please Select Files & click OK!');", true);
                ModalPopupExtender.Show();
            }

        }

        private void GetFilesFromUploadControl()
        {
            try
            {

            HttpFileCollection multipleFiles = Request.Files;
            for (int fileCount = 0; fileCount < multipleFiles.Count; fileCount++)
            {
                HttpPostedFile fi = multipleFiles[fileCount];
                string Name = fi.FileName;

                Stream InputStream = fi.InputStream;

                int FileSize = fi.ContentLength;

                BinaryReader br = new BinaryReader(InputStream);

                byte[] documentcontent = br.ReadBytes((int)InputStream.Length);

                FileInfo fif = new FileInfo(fi.FileName);
                string extn = fif.Extension;

                dtFiledata.Rows.Add(Name, extn, FileSize, documentcontent);

            }

            grdUploadData.DataSource = dtFiledata;
            grdUploadData.DataBind();

            ViewState["FileData"] = dtFiledata;

            HiddenFieldCount.Value = dtFiledata.Rows.Count.ToString();

            VisibleUploadDoc(dtFiledata.Rows.Count);
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

                dtFiledata = (DataTable)ViewState["FileData"];

                string FileName = dtFiledata.Rows[grdIndex]["FileName"].ToString();
                byte[] documentBytes = (byte[])dtFiledata.Rows[grdIndex]["FileContent"];

                Response.ClearContent();
                Response.ContentType = "application/octectstream";
                Response.AddHeader("Content-Disposition", string.Format("attachment; filename={0}", FileName));
                Response.AddHeader("Content-Length", documentBytes.Length.ToString());
                Response.BinaryWrite(documentBytes);
                Response.Flush();
                Response.Close();
            }
            else if (e.CommandName == "Delete")
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;

                int grdIndex = gvRow.RowIndex;

                int RemoveAt = grdIndex;

                dtFiledata = (DataTable)ViewState["FileData"];

                dtFiledata.Rows.RemoveAt(RemoveAt);
                dtFiledata.AcceptChanges();


                ViewState["FileData"] = dtFiledata;
                grdUploadData.DataSource = dtFiledata;
                grdUploadData.DataBind();

                HiddenFieldCount.Value = dtFiledata.Rows.Count.ToString();

                VisibleUploadDoc(dtFiledata.Rows.Count);

            }
        }

        protected void grdUploadData_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                ImageButton viewimgbtn = e.Row.FindControl("btnView") as ImageButton;
                ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
                scriptManager.RegisterPostBackControl(viewimgbtn);
            }
        }

        private void VisibleUploadDoc(int FilesCount)
        {
            if (FilesCount >= 5)
            {
                lnkbtnUploadFiles.Visible = false;
            }
            else
            {
                lnkbtnUploadFiles.Visible = true;
            }
        }

        protected void txtAmountInAED_TextChanged(object sender, EventArgs e)
        {
            try
            {


                if (txtAmountInAED.Text != string.Empty && txtAmountInAED.Text != "")
                {
                    txtAmountInWords.Text = objCBLL.changeCurrencyToWords(txtAmountInAED.Text);
                }
                else
                {
                    txtAmountInWords.Text = "";
                }
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }  
           
            
        }




        protected void chkInitiate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkInitiate.Checked)
            {
                BindSignature();
               
            }
            else
            {
                ImgInitiaterSignature.ImageUrl = "";
               
            }
        }

        public void FetchSignature()
        {
            try
            {
                objBO.UserID = objUserInfo.UserID;

                //DataTable dtSignContent = new DataTable();


                //dtSignContent = objCBLL.GetSigContent(objBO);

                //byte[] bytes = (byte[])dtSignContent.Rows[0]["SIGNCONTENT"];

                byte[] bytes = objCBLL.GetSigContent(objBO);
                string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                //ImgInitiaterSignature.ImageUrl = "data:image/png;base64," + base64String;

                ViewState["Sign"] = base64String;
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }
           
        }


        private void BindSignature()
        {
            ImgInitiaterSignature.ImageUrl = "data:image/png;base64," + (string)ViewState["Sign"];
            

        }

        protected void btninitiateSubmit_Click(object sender, EventArgs e)
        {

            try
            {

                if (ddlLocation.SelectedIndex.Equals(-1) || ddlAccountHead.SelectedIndex.Equals(-1))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Pls Select Valid Location or Account Heads')", true);
                    return;
                }


                if (txtSectionRequestFor.Text == "" || txtVendorRecommended.Text == "" || txtJustification.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('All Text Fileds are Mandotory')", true);
                    return;
                }
                if (!chkInitiate.Checked)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Please check Initiate Check Box and then Click Submit')", true);
                    return;
                }

                if (SavePhysicalFiles() == false)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Some Error in saving Physical Files')", true);
                    return;
                }


                objBO.SR_No = txtSRNo.Text;
                objBO.LocationID = Convert.ToInt32(ddlLocation.SelectedValue);
                objBO.Date = Convert.ToDateTime(txtDate.Text);
                objBO.SanctionRequestFor = txtSectionRequestFor.Text;
                objBO.AccountHeadID = Convert.ToInt32(ddlAccountHead.SelectedValue);
                objBO.Amount_AED = txtAmountInAED.Text == string.Empty || txtAmountInAED.Text == "" ? 0 : Convert.ToDecimal(txtAmountInAED.Text);
                objBO.AmountInWords = txtAmountInWords.Text;
                objBO.VendorRecomended = txtVendorRecommended.Text;
                objBO.JustificationDetails = txtJustification.Text;
                objBO.LoginUserID = objUserInfo.UserID;
                objBO.DeptID = Convert.ToInt32(objUserInfo.DeptID);

                objBO.SRTypeID = ddlSrType.SelectedValue;
                objBO.SRType = ddlSrType.SelectedItem.Text;

                if (ddlSrType.SelectedValue == "2")
                {
                    objBO.OldSRno = ddloldSrNo.SelectedIndex != 0 ? ddloldSrNo.SelectedItem.Text : "-1";
                }



                objBO.FileData = (DataTable)ViewState["FileData"];


                if (objBLL.InsertInitiateSR(objBO) == "S")
                {
                    //eApprovalBO.eApprovalBO[] Filecontent = new eApprovalBO.eApprovalBO[8];

                    //objBO.FileData = (DataTable)ViewState["FileData"];

                    //objBO.SR_No = txtSRNo.Text;

                    //for (int i = 0; i < dtFiledata.Rows.Count; i++)
                    //{
                    //    Filecontent[i].SR_No = txtSRNo.Text;
                    //    Filecontent[i].FileName = dtFiledata.Rows[i]["Filename"].ToString();
                    //    Filecontent[i].FileSize = Convert.ToInt64(dtFiledata.Rows[i]["FileSize"]);
                    //    Filecontent[i].FileContent = (byte[])dtFiledata.Rows[i]["Filecontent"];
                    //    Filecontent[i].Extn = dtFiledata.Rows[i]["Exn"].ToString();

                    //}

                    //if (objBLL.InsertFileContent(objBO) == "S")
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Initiated Sucessfully.')", true);
                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Initiate Request is Failed.')", true);
                    //}

                    // ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Successfully Initiated with SR No : '+'" + txtSRNo.Text + "')", true);



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

                 ScriptManager.RegisterStartupScript(this, this.GetType(), "alertMessage", "alert('Successfully Initiated with SR No : '+'" + txtSRNo.Text + "'); window.location='" + Request.ApplicationPath + "UserInterface/InitiateRequest.aspx';", true);





                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Initiate Request is Failed.')", true);
                }
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }


        }


        public bool SavePhysicalFiles()
        {
            try
            {
                int i = 1;

                DataTable dtSavingdata = new DataTable();

                dtSavingdata = (DataTable)ViewState["FileData"];

                if (dtSavingdata!=null)
                {
                    foreach (DataRow dr in dtSavingdata.Rows)
                    {

                        string SRNO = txtSRNo.Text;
                        string FileName = dr["FileName"].ToString();
                        byte[] SavingDocumentBytes = (byte[])dr["FileContent"];

                       

                        string SavingFileName = txtSRNo.Text + "-" + Convert.ToString(i) + "-" + FileName;

                        SavingFileName = SavingFileName.Replace("/", "-");

                        System.IO.File.WriteAllBytes(Server.MapPath("~/DocumentFolder/" + SavingFileName), SavingDocumentBytes);




                        //   const int myBufferSize = 1024;
                        //   Stream myInputStream = new MemoryStream(SavingDocumentBytes);

                        //   string Filepath = Server.MapPath("~/DocumentFolder/");

                        //   string MainFilePath = Filepath + "Install.pdf";

                        ////   StreamWriter s = new StreamWriter(MainFilePath);

                        //   Stream myOutputStream = System.IO.File.OpenWrite(MainFilePath);
                        //   byte[] buffer = new Byte[myBufferSize];
                        //   int numbytes;
                        //   while ((numbytes = myInputStream.Read(buffer, 0, myBufferSize)) > 0)
                        //   {
                        //       myOutputStream.Write(buffer, 0, numbytes);
                        //   }
                        //   myInputStream.Close();
                        //   myOutputStream.Close();


                        i++;

                    }
                }

               
                return true;
            }
            catch
            {

                return false;
            }

        }

        protected void ddlSrType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSrType.SelectedValue !="0")
            {
                if (ddlSrType.SelectedValue =="1")   //Regular SR
                {
                    lblOldSRno.Visible = false;
                    ddloldSrNo.Visible = false;
                }
                else if (ddlSrType.SelectedValue == "2") //Rate SR
                {
                    lblOldSRno.Visible = true;
                    ddloldSrNo.Visible = true;
                    BindOldSRno();
                }
            }
            else
            {
                lblOldSRno.Visible = false;
                ddloldSrNo.Visible = false;
            }
            
        }

        private void BindOldSRno()
        {
            try
            {

            objBO.DeptID = Convert.ToInt32(objUserInfo.DeptID);

            ddloldSrNo.DataSource = objCBLL.GetOldSRno(objBO);
            ddloldSrNo.DataTextField = "SRNO";
           
            ddloldSrNo.DataBind();
            ddloldSrNo.Items.Insert(0, "--Select--");
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }

        }

        


      


        //private string ToXml(DataTable ds)
        //{
        //    using (var memoryStream = new MemoryStream())
        //    {
        //        using (TextWriter streamWriter = new StreamWriter(memoryStream))
        //        {
        //            var xmlSerializer = new XmlSerializer(typeof(DataTable));
        //            xmlSerializer.Serialize(streamWriter, ds);
        //            return Encoding.UTF8.GetString(memoryStream.ToArray());
        //        }
        //    }
        //}

        //private string ToXml(DataTable dt)
        //{
        //    StringBuilder sb = new StringBuilder();

            

        //    for (int i = 0; i < dt.Rows.Count;i++ )
        //    {
        //        byte[] documentBytes = (byte[])dtFiledata.Rows[i]["FileContent"];

        //        sb.Append(String.Format("<FileContent FileName='{0}' Extn='{1}' FileSize='{2}' FileContent='{3}'/>", dt.Rows[i]["FileName"].ToString(), dt.Rows[i]["Exn"].ToString(), dt.Rows[i]["FileSize"].ToString(),documentBytes));

        //    }

        //    return String.Format("<ROOT>{0}</ROOT>", sb.ToString());
        //}

      
       


    }
}