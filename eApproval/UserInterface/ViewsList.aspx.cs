using eApprovalBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using System.Reflection;

namespace eApproval.UserInterface
{
    public partial class ViewsList : System.Web.UI.Page
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

                    ViewState["sortOrder"] = "";      //It is saving desc/asc 
                    // ViewState["FilteredData"] = null;   //Filtered datatable saving in filertedData viewstate
                    // ViewState["ViewList"] = null;       //Sorted datatable saving in ViewList viewstate

                    BindGrid();

                    GetDeaprtments();

                    ddlDepartments.Visible = false;



                }
                else
                {
                    Response.Redirect("~/Portal/Index.aspx");
                }

            }
        }


        private void GetDeaprtments()
        {
            try
            {
            objBO.RoleID = Convert.ToInt32(objUserInfo.RoleID);
            objBO.DeptID = Convert.ToInt32(objUserInfo.DeptID);

            ddlDepartments.DataSource = objCBLL.GetDepartments_RoleBased(objBO);
            ddlDepartments.DataTextField = "DEPTNAME";
            ddlDepartments.DataValueField = "DEPTID";

            ddlDepartments.DataBind();
            ddlDepartments.Items.Insert(0, "--Select--");
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }


        }



        private void BindGrid()
        {
            try
            {


            DataTable dtApprovalsListView = new DataTable();

            objBO.RoleID = Convert.ToInt32(objUserInfo.RoleID);
            objBO.DeptID = Convert.ToInt32(objUserInfo.DeptID);

            dtApprovalsListView = objBLL.GetViewApprovalsList(objBO);

            grdApprovalViewList.DataSource = dtApprovalsListView;
            grdApprovalViewList.DataBind();

            ViewState["ViewList"] = dtApprovalsListView;

            ViewState["SearchClick"] = dtApprovalsListView;
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

            //string EncodedSRNO = FormsAuthentication.HashPasswordForStoringInConfigFile(SRNO, "SHA1");

            Response.Redirect("~/UserInterface/ViewApprovals.aspx" + "?SRNO=" + EncodedSRNO);

        }


        private void BindGrid_PageIndex()
        {
            if (ViewState["ViewList"] != null)
            {
                DataTable dtView = (DataTable)ViewState["ViewList"];

                grdApprovalViewList.DataSource = dtView;
                grdApprovalViewList.DataBind();

            }
        }


        protected void grdApprovalViewList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdApprovalViewList.PageIndex = e.NewPageIndex;
            // BindGrid();

            BindGrid_PageIndex();
        }

        protected void grdApprovalViewList_PreRender(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            GridViewRow pagerRow = (GridViewRow)gv.BottomPagerRow;


            if (ViewState["ViewList"] != null)
            {
                DataTable dtView = (DataTable)ViewState["ViewList"];
            }


            if (pagerRow != null && pagerRow.Visible == false)
                pagerRow.Visible = true;
        }

        protected void ddlSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!ddlSearch.SelectedIndex.Equals(0))
            {
                if (ddlSearch.SelectedIndex.Equals(1))
                {
                    txtSearchData.Visible = true;
                    ddlDepartments.Visible = false;

                    BindGrid();

                    txtSearchData.Focus();
                }
                else if (ddlSearch.SelectedIndex.Equals(2))
                {
                    ddlDepartments.SelectedIndex = -1;
                    txtSearchData.Visible = false;
                    ddlDepartments.Visible = true;
                    ddlDepartments.Focus();

                    BindGrid();
                }
            }
            else
            {
                //DataTable dtViewList = (DataTable)ViewState["ViewList"];
                //grdApprovalViewList.DataSource = dtViewList;
                //grdApprovalViewList.DataBind();

                BindGrid();

                txtSearchData.Text = "";

                txtSearchData.Visible = true;
                ddlDepartments.Visible = false;

                // ViewState["FilteredData"] = null;
            }

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {

            //Validations



            if (ddlSearch.SelectedIndex.Equals(0))
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Pl select Search Option & click Search..')", true);

                ddlSearch.Focus();

                return;
            }

            else if (ddlSearch.SelectedIndex.Equals(1))
            {
                if (txtSearchData.Text == "")
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Pl Enter Search Data & click Search..')", true);

                    txtSearchData.Focus();
                    return;
                }


            }
            else if (ddlSearch.SelectedIndex.Equals(2))
            {


                if (ddlDepartments.SelectedIndex.Equals(0))
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Alert Message", "alert('Pl select Department & click Search..')", true);

                    ddlDepartments.Focus();

                    return;
                }
            }


            if (ViewState["SearchClick"] != null)
            {

                string searchType = string.Empty;
                string SearchData = string.Empty;

                if (ddlSearch.SelectedIndex.Equals(1))
                {
                    searchType = "SRNO";
                    SearchData = txtSearchData.Text;
                }
                else if (ddlSearch.SelectedIndex.Equals(2))
                {
                    searchType = "INITIATINGDEPT";
                    SearchData = ddlDepartments.SelectedItem.Text;
                }




                DataTable dtViewList = (DataTable)ViewState["SearchClick"];

                DataTable RowsFound = SearchRecords(searchType, dtViewList, SearchData);

                grdApprovalViewList.DataSource = RowsFound;

                grdApprovalViewList.DataBind();

                // ViewState["FilteredData"] = RowsFound;

                ViewState["ViewList"] = RowsFound;

            }
            }
            catch (Exception ex)
            {
                objErrorLogBLL.ErrorLog(MethodInfo.GetCurrentMethod().Name, Request.Url.PathAndQuery, ex.Message, objUserInfo.UserName);
            }






        }

        public DataTable SearchRecords(string Col1, DataTable RecordDT_, string KeyWORD)
        {
            DataTable TempTable = RecordDT_;

            DataView DV = new DataView(TempTable);
            DV.RowFilter = string.Format(string.Format("Convert({0},'System.String')", Col1) + " LIKE '*{0}*'", KeyWORD);
            return DV.ToTable();
        }



        #region Grid Sorting Code

        protected void grdApprovalViewList_Sorting(object sender, GridViewSortEventArgs e)
        {
           // ViewState["SortExpression"] = e.SortExpression;

            GridBind_SortedData(e.SortExpression, sortOrder);


        }

        public string sortOrder
        {
            get
            {
                if (ViewState["sortOrder"].ToString() == "desc")
                {
                    ViewState["sortOrder"] = "asc";


                }
                else
                {
                    ViewState["sortOrder"] = "desc";

                }

                return ViewState["sortOrder"].ToString();
            }
            set
            {
                ViewState["sortOrder"] = value;
            }
        }


        public void GridBind_SortedData(string sortExp, string sortDir)
        {

            //DataView myDataView = new DataView();

            // DataTable dtViewList= new DataTable();

            //if (ViewState["FilteredData"] != null)
            //{
            //    dtViewList = (DataTable)ViewState["FilteredData"];
            //}
            //else
            //{
            //    dtViewList = (DataTable)ViewState["ViewList"];
            //}


            //myDataView = dtViewList.DefaultView;

            //if (sortExp != string.Empty)
            //{
            //    myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            //}

            //grdApprovalViewList.DataSource = myDataView;
            //grdApprovalViewList.DataBind();

            DataView myDataView = new DataView();

            DataTable dtViewList = (DataTable)ViewState["ViewList"];

            myDataView = dtViewList.DefaultView;

            if (sortExp != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", sortExp, sortDir);
            }



            grdApprovalViewList.DataSource = myDataView;
            grdApprovalViewList.DataBind();

            dtViewList = myDataView.ToTable();

            ViewState["ViewList"] = dtViewList;

        }




        #endregion

        //protected void grdApprovalViewList_RowCreated(object sender, GridViewRowEventArgs e)
        //{
        //    string imgAsc = @" <img src='~/Images/asc.gif' border='0' title='Ascending'  />";
        //    string imgDes = @" <img src='~/Images/desc.gif' border='0' title='Descendng' />";
        //    if (e.Row.RowType == DataControlRowType.Header)
        //    {
        //        foreach (TableCell cell in e.Row.Cells)
        //        {
        //            if (cell.HasControls())
        //            {
        //                LinkButton lnkbtn = null;


        //                if (cell.Controls[0] is LinkButton)
        //                {
        //                    lnkbtn = (LinkButton)cell.Controls[0];
        //                }


        //                if (ViewState["SortExpression"] == null)
        //                {
        //                    return;
        //                }

        //                Image img = new Image();
        //                string Exp = ViewState["SortExpression"].ToString();

        //                if (lnkbtn.Text == Exp)
        //                {
        //                    if (sortOrder == "asc")
        //                    {
        //                        //  lnkbtn.Text += imgAsc;
        //                        img.ImageUrl = "~/Images/asc.gif";
        //                    }
        //                    else
        //                        //  lnkbtn.Text += imgDes;
        //                        img.ImageUrl = "~/Images/desc.gif";
        //                }

        //                cell.Controls.Add(new LiteralControl("&nbsp;"));
        //                cell.Controls.Add(img);
        //            }

                   
        //        }

        //    }
        //}


    }


}
