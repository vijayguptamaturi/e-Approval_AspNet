using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eApprovalBO;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace eApprovalDB
{

    public class eApprovalDB
    {


        #region Objects Declaration

        eApprovalBO.eApprovalBO objBO = new eApprovalBO.eApprovalBO();
        CommonDB objCDB = new CommonDB();

        #endregion



        #region Login  Validation

        public DataTable LoginValidation(eApprovalBO.eApprovalBO objLoginValidationBO)
        {
            DataTable dtLoginValidation = new DataTable();


            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {

                SqlCommand cmd = new SqlCommand("Login_validation", con);
                cmd.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = objLoginValidationBO.UserName;
                cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = objLoginValidationBO.Password;

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open(); int rows = cmd.ExecuteNonQuery();
                con.Close();


                SqlDataAdapter sda = new SqlDataAdapter(cmd);


                sda.Fill(dtLoginValidation);

                con.Close();
            }

            return dtLoginValidation;

        }

        #endregion



        public DataTable GetUserInfo(eApprovalBO.eApprovalBO GetUserInfoBO)
        {
            DataTable dtGetUserInfo = new DataTable();


            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {

                SqlCommand cmd = new SqlCommand("GET_USER_INFO", con);
                cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = GetUserInfoBO.UserID;
                cmd.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = GetUserInfoBO.UserName;

                cmd.CommandType = CommandType.StoredProcedure;

                con.Open(); int rows = cmd.ExecuteNonQuery();
                con.Close();


                SqlDataAdapter sda = new SqlDataAdapter(cmd);


                sda.Fill(dtGetUserInfo);

                con.Close();
            }

            return dtGetUserInfo;
        }


        #region UserProfile

        public DataTable GetUserProfile(eApprovalBO.eApprovalBO objGetUserProfileBO)
        {
            DataTable dtUserProfile = new DataTable();

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_USER_PROFILE_DATA", con);
                cmd.Parameters.Add("@USERID", SqlDbType.VarChar).Value = objGetUserProfileBO.UserID;

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtUserProfile);
            }

            return dtUserProfile;

        }


        public int GetNext_User_ID()
        {
            int NextUserID = 0;

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_NEXT_USERID_UPROFILE", con);
                SqlParameter P = new SqlParameter("@NEXT_USER_ID", SqlDbType.BigInt);

                P.Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(P);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                NextUserID = Convert.ToInt32(P.Value.ToString());

            }


            return NextUserID;


        }


        public string InsertUserProfile(eApprovalBO.eApprovalBO objInsertUserProfileBO)
        {
            string Result = string.Empty;

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("PROC_INSERT_USER_PROFILE", con);
                cmd.Parameters.Add("@USERID", SqlDbType.Int).Value = Convert.ToInt32(objInsertUserProfileBO.UserID);
                cmd.Parameters.Add("@USERNAME", SqlDbType.VarChar).Value = objInsertUserProfileBO.UserName;
                cmd.Parameters.Add("@PASSWORD", SqlDbType.VarChar).Value = objInsertUserProfileBO.Password;
                cmd.Parameters.Add("@FULLNAME", SqlDbType.VarChar).Value = objInsertUserProfileBO.FullName;
                cmd.Parameters.Add("@EMAILID", SqlDbType.VarChar).Value = objInsertUserProfileBO.EmailID;
                cmd.Parameters.Add("@MOBILENO", SqlDbType.VarChar).Value = objInsertUserProfileBO.MobileNo;
                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = objInsertUserProfileBO.DeptID;
                cmd.Parameters.Add("@ROLEID", SqlDbType.Int).Value = objInsertUserProfileBO.RoleID;
                cmd.Parameters.Add("@CREATEDBY", SqlDbType.Int).Value = objInsertUserProfileBO.LoginUserID;
                cmd.Parameters.Add("@SINGDOCUMENTCONTENT", SqlDbType.Binary).Value = objInsertUserProfileBO.SignatureContent;



                SqlParameter P = new SqlParameter("@RESULT", SqlDbType.VarChar, 2);
                P.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(P);


                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Result = P.Value.ToString();
            }

            return Result;

        }



        public string UpdateUserProfile(eApprovalBO.eApprovalBO objUpdateUserProfileBO)
        {
            string Result = string.Empty;

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("PROC_UPDATE_USER_PROFILE", con);
                cmd.Parameters.Add("@USERID", SqlDbType.Int).Value = Convert.ToInt32(objUpdateUserProfileBO.UserID);
                cmd.Parameters.Add("@FULLNAME", SqlDbType.VarChar).Value = objUpdateUserProfileBO.FullName;
                cmd.Parameters.Add("@EMAILID", SqlDbType.VarChar).Value = objUpdateUserProfileBO.EmailID;
                cmd.Parameters.Add("@MOBILENO", SqlDbType.VarChar).Value = objUpdateUserProfileBO.MobileNo;
                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = objUpdateUserProfileBO.DeptID;
                cmd.Parameters.Add("@ROLEID", SqlDbType.Int).Value = objUpdateUserProfileBO.RoleID;
                cmd.Parameters.Add("@MODIFIEDBY", SqlDbType.Int).Value = objUpdateUserProfileBO.LoginUserID;
                cmd.Parameters.Add("@SINGDOCUMENTCONTENT", SqlDbType.Binary).Value = objUpdateUserProfileBO.SignatureContent;



                SqlParameter P = new SqlParameter("@RESULT", SqlDbType.VarChar, 2);
                P.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(P);


                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Result = P.Value.ToString();
            }

            return Result;

        }





        #endregion


        #region InititateRequest

        public string GetSrNo(eApprovalBO.eApprovalBO objGetSrNoBO)
        {
            string SRno = string.Empty;

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_NEW_SR_NO", con);
                cmd.Parameters.Add("@USERID", SqlDbType.Int).Value = Convert.ToInt32(objGetSrNoBO.UserID);
                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = Convert.ToInt32(objGetSrNoBO.DeptID);

                SqlParameter P = new SqlParameter("@SR_NO", SqlDbType.NVarChar, 400);
                P.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(P);


                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                SRno = P.Value.ToString();
            }

            return SRno;

        }


        public string InsertInitiateSR(eApprovalBO.eApprovalBO objInsertInitiateSRBO)
        {
            string Result = string.Empty;

            string constr = objCDB.GetConnectionString();

            //using (SqlConnection con = new SqlConnection(constr))
            //{            
              
                SqlConnection con = new SqlConnection(constr);
                con.Open();
                SqlTransaction Tran = con.BeginTransaction();

                try
                {
                    SqlCommand cmd = new SqlCommand("PROC_INSERT_INITIATE_SR", con, Tran);
                    cmd.Parameters.Add("@SR_NO", SqlDbType.NVarChar).Value = objInsertInitiateSRBO.SR_No;
                    cmd.Parameters.Add("@LOCATION_ID", SqlDbType.BigInt).Value = objInsertInitiateSRBO.LocationID;
                    cmd.Parameters.Add("@DEPT_ID", SqlDbType.BigInt).Value = objInsertInitiateSRBO.DeptID;
                    cmd.Parameters.Add("@DATE", SqlDbType.DateTime).Value = objInsertInitiateSRBO.Date;
                    cmd.Parameters.Add("@SANCTION_REQUESTFOR", SqlDbType.NVarChar).Value = objInsertInitiateSRBO.SanctionRequestFor;
                    cmd.Parameters.Add("@ACCOUNT_HEAD_APPLICABLEID", SqlDbType.BigInt).Value = objInsertInitiateSRBO.AccountHeadID;
                    cmd.Parameters.Add("@AMOUNT_AED", SqlDbType.Decimal).Value = objInsertInitiateSRBO.Amount_AED;
                    cmd.Parameters.Add("@AMOUNT_INWORDS", SqlDbType.NVarChar).Value = objInsertInitiateSRBO.AmountInWords;
                    cmd.Parameters.Add("@VENDOR_RECOMENDED", SqlDbType.NVarChar).Value = objInsertInitiateSRBO.VendorRecomended;
                    cmd.Parameters.Add("@JUSTIFICATION_DETAILS", SqlDbType.NVarChar).Value = objInsertInitiateSRBO.JustificationDetails;
                    cmd.Parameters.Add("@INITIATOR_USERID", SqlDbType.BigInt).Value = objInsertInitiateSRBO.LoginUserID;

                    cmd.Parameters.Add("@SR_TYPEID", SqlDbType.BigInt).Value = objInsertInitiateSRBO.SRTypeID;
                    cmd.Parameters.Add("@SR_TYPE", SqlDbType.NVarChar).Value = objInsertInitiateSRBO.SRType;
                    cmd.Parameters.Add("@OLD_SR_NO", SqlDbType.NVarChar).Value = objInsertInitiateSRBO.OldSRno;




                    SqlParameter P = new SqlParameter("@RESULT", SqlDbType.VarChar, 2);
                    P.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(P);


                    cmd.CommandType = CommandType.StoredProcedure;
                    //con.Open();
                    cmd.ExecuteNonQuery();
                    //con.Close();

                    Result = P.Value.ToString();

                    //}

                    if (Result == "S")
                    {
                        if (objInsertInitiateSRBO.FileData !=null)
                        {
                            for (int i = 0; i < objInsertInitiateSRBO.FileData.Rows.Count; i++)
                            {
                                //using (SqlConnection con = new SqlConnection(constr))
                                //{
                                SqlCommand Command = new SqlCommand("PROC_INSERT_FILE_CONTENT", con, Tran);


                                Command.Parameters.Add("@SR_NO", SqlDbType.NVarChar).Value = objInsertInitiateSRBO.SR_No;

                                Command.Parameters.Add("@FILE_NAME", SqlDbType.NVarChar).Value = objInsertInitiateSRBO.FileData.Rows[i]["FileName"].ToString();
                                Command.Parameters.Add("@FILE_CONTENT", SqlDbType.VarBinary).Value = (byte[])objInsertInitiateSRBO.FileData.Rows[i]["FileContent"];
                                Command.Parameters.Add("@FILE_EXN", SqlDbType.NVarChar).Value = objInsertInitiateSRBO.FileData.Rows[i]["Exn"].ToString();
                                Command.Parameters.Add("@FILE_SIZE", SqlDbType.BigInt).Value = Convert.ToInt64(objInsertInitiateSRBO.FileData.Rows[i]["FileSize"]);

                                SqlParameter ReturnP = new SqlParameter("@RESULT", SqlDbType.VarChar, 2);
                                ReturnP.Direction = ParameterDirection.Output;
                                Command.Parameters.Add(ReturnP);


                                Command.CommandType = CommandType.StoredProcedure;
                                //con.Open();
                                Command.ExecuteNonQuery();
                                //con.Close();

                                Result = ReturnP.Value.ToString();
                                //}
                            }
                        }

                        
                    }

                    if (Result == "S")
                    {

                            SqlCommand Command = new SqlCommand("UPDATE_NEXT_SR_NO", con, Tran);
                          
                            Command.Parameters.Add("@DEPT_ID", SqlDbType.Int).Value = objInsertInitiateSRBO.DeptID;
                          
                            SqlParameter ReturnP = new SqlParameter("@RESULT", SqlDbType.VarChar, 2);
                            ReturnP.Direction = ParameterDirection.Output;
                            Command.Parameters.Add(ReturnP);
                            Command.CommandType = CommandType.StoredProcedure;                         
                            Command.ExecuteNonQuery();                         
                            Result = ReturnP.Value.ToString();
                           
                    }

                    Tran.Commit();
                }
                catch
                {
                    Tran.Rollback();

                    Result = "F";
                }

                


            return Result;

        }


        //public string InsertFileContent(eApprovalBO.eApprovalBO objInsertFileContentBO)
        //{
        //    string Result = string.Empty;

        //    string constr = objCDB.GetConnectionString();



        //    for (int i = 0; i < objInsertFileContentBO.FileData.Rows.Count; i++)
        //    {
        //        using (SqlConnection con = new SqlConnection(constr))
        //        {
        //            SqlCommand Command = new SqlCommand("PROC_INSERT_FILE_CONTENT", con);


        //            Command.Parameters.Add("@SR_NO", SqlDbType.NVarChar).Value = objInsertFileContentBO.SR_No;
        //            Command.Parameters.Add("@FILE_NAME", SqlDbType.NVarChar).Value = objInsertFileContentBO.FileData.Rows[i]["FileName"].ToString();
        //            Command.Parameters.Add("@FILE_CONTENT", SqlDbType.VarBinary).Value = (byte[])objInsertFileContentBO.FileData.Rows[i]["FileContent"];
        //            Command.Parameters.Add("@FILE_EXN", SqlDbType.NVarChar).Value = objInsertFileContentBO.FileData.Rows[i]["Exn"].ToString();
        //            Command.Parameters.Add("@FILE_SIZE", SqlDbType.BigInt).Value = Convert.ToInt64(objInsertFileContentBO.FileData.Rows[i]["FileSize"]);

        //            SqlParameter ReturnP = new SqlParameter("@RESULT", SqlDbType.VarChar, 2);
        //            ReturnP.Direction = ParameterDirection.Output;
        //            Command.Parameters.Add(ReturnP);


        //            Command.CommandType = CommandType.StoredProcedure;
        //            con.Open();
        //            Command.ExecuteNonQuery();
        //            con.Close();

        //            Result = ReturnP.Value.ToString();
        //        }
        //    }

               

        //    return Result;

        //}



        #endregion


        #region ApprovalsList

        public DataTable GetApprovalsList(eApprovalBO.eApprovalBO objGetApprovalsListBO)
        {
            DataTable dtGetApprovalsList = new DataTable();

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_APPROVALS_LIST", con);
                cmd.Parameters.Add("@ROLEID", SqlDbType.Int).Value = objGetApprovalsListBO.RoleID;

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtGetApprovalsList);
            }

            return dtGetApprovalsList;
        }


        public DataSet GetGetSRData_Acc(eApprovalBO.eApprovalBO objGetGetSRData_AccBO)
        {
            DataSet dsGetSRData = new DataSet();

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_SR_DATA_ACC_APPROVAL", con);
                cmd.Parameters.Add("@SRNO", SqlDbType.NVarChar).Value = objGetGetSRData_AccBO.SR_No;

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dsGetSRData);
            }

            return dsGetSRData;
        }


        public DataTable GetGetSRFileContent(eApprovalBO.eApprovalBO objGetGetSRFileContentBO)
        {
            DataTable dtGetGetSRFileContent = new DataTable();

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_SR_FILE_DATA", con);
                cmd.Parameters.Add("@SRNO", SqlDbType.NVarChar).Value = objGetGetSRFileContentBO.SR_No;
                cmd.Parameters.Add("@FILE_NAME", SqlDbType.NVarChar).Value = objGetGetSRFileContentBO.FileName;
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtGetGetSRFileContent);
            }

            return dtGetGetSRFileContent;
        }

        public string InsertFinaceApproval(eApprovalBO.eApprovalBO objInsertFinaceApprovalBO)
        {
            string Result = string.Empty;

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("PROC_INSERT_FINANCE_APPROVAL", con);
                cmd.Parameters.Add("@SR_NO", SqlDbType.NVarChar).Value = objInsertFinaceApprovalBO.SR_No;
                cmd.Parameters.Add("@FINANCECOMMENTS", SqlDbType.NVarChar).Value = objInsertFinaceApprovalBO.FinaceApprovalComments;
                cmd.Parameters.Add("@FIN_USERID", SqlDbType.NVarChar).Value = objInsertFinaceApprovalBO.UserID;



                SqlParameter P = new SqlParameter("@RESULT", SqlDbType.NVarChar, 10);
                P.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(P);


                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Result = P.Value.ToString();
            }

            return Result;

        }



        public DataTable GetAccontsApprovalData_CEO(eApprovalBO.eApprovalBO objGetAccontsApprovalData_CEOBO)
        {
            DataTable dtGetAccontsApprovalData_CEO = new DataTable();

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_APPROV_FINACE_SIGNATURE", con);
                cmd.Parameters.Add("@SR_NO", SqlDbType.NVarChar).Value = objGetAccontsApprovalData_CEOBO.SR_No;
              
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtGetAccontsApprovalData_CEO);
            }

            return dtGetAccontsApprovalData_CEO;
        }


        public string InsertCEOApproval(eApprovalBO.eApprovalBO objInsertCEOApprovalBO)
        {
            string Result = string.Empty;

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("PROC_INSERT_CEO_APPROVAL", con);
                cmd.Parameters.Add("@SR_NO", SqlDbType.NVarChar).Value = objInsertCEOApprovalBO.SR_No;
                cmd.Parameters.Add("@CEO_COMMENTS", SqlDbType.NVarChar).Value = objInsertCEOApprovalBO.CEOApprovalComments;
                cmd.Parameters.Add("@CEO_USERID", SqlDbType.NVarChar).Value = objInsertCEOApprovalBO.UserID;
                cmd.Parameters.Add("@CEO_CHK_FLAG", SqlDbType.NVarChar).Value = objInsertCEOApprovalBO.CEOApprovalChkFlag;


                SqlParameter P = new SqlParameter("@RESULT", SqlDbType.NVarChar, 10);
                P.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(P);


                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Result = P.Value.ToString();
            }

            return Result;

        }

        #endregion


        #region ViewApprovalsList

        public DataTable GetViewApprovalsList(eApprovalBO.eApprovalBO objGetViewApprovalsListBO)
        {
            DataTable dtGetViewApprovalsList = new DataTable();

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_VIEW_APPROVALS_LIST", con);
                cmd.Parameters.Add("@ROLEID", SqlDbType.Int).Value = objGetViewApprovalsListBO.RoleID;
                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = objGetViewApprovalsListBO.DeptID;

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtGetViewApprovalsList);
            }

            return dtGetViewApprovalsList;
        }



        public DataSet GetSRDataViewApprovals(eApprovalBO.eApprovalBO objGetSRDataViewApprovalsBO)
        {
            DataSet dtGetSRDataViewApprovals = new DataSet();

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_SR_DATA_VIEW_APPROVALS", con);
                cmd.Parameters.Add("@SRNO", SqlDbType.NVarChar).Value = objGetSRDataViewApprovalsBO.SR_No;
              

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtGetSRDataViewApprovals);
            }

            return dtGetSRDataViewApprovals;
        }


        #endregion

        #region HomePage


        public DataTable GetViewApprovalsList_HomePage(eApprovalBO.eApprovalBO objGetViewApprovalsList_HomePageBO)
        {
            DataTable dtGetViewApprovalsList_HomePage = new DataTable();

            string constr = objCDB.GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_LIST_HOME_PAGE", con);
                cmd.Parameters.Add("@ROLEID", SqlDbType.Int).Value = objGetViewApprovalsList_HomePageBO.RoleID;
                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = objGetViewApprovalsList_HomePageBO.DeptID;

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtGetViewApprovalsList_HomePage);
            }

            return dtGetViewApprovalsList_HomePage;
        }


        #endregion



    }
}
