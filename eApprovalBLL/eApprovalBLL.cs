using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using eApprovalDB;
using System.Data;


namespace eApprovalBLL
{
    
    public class eApprovalBLL
    {
        eApprovalDB.eApprovalDB objDB = new eApprovalDB.eApprovalDB();
        eApprovalBO.eApprovalBO objBO = new eApprovalBO.eApprovalBO();


        #region loginpage
        public DataTable LoginValidation(eApprovalBO.eApprovalBO objLoginValidationBO)
        {
            DataTable dtLoginValidation = new DataTable();
            return dtLoginValidation = objDB.LoginValidation(objLoginValidationBO);
        }

        public DataTable GetUserInfo(eApprovalBO.eApprovalBO objGetUserInfoBO)
        {
            DataTable dtGetUserInfo = new DataTable();
            return dtGetUserInfo = objDB.GetUserInfo(objGetUserInfoBO);
        }

        #endregion



        #region UserProfile

        public DataTable GetUserProfile(eApprovalBO.eApprovalBO objGetUserProfileBO)
        {
            DataTable dtGetUserProfile = new DataTable();
            return dtGetUserProfile = objDB.GetUserProfile(objGetUserProfileBO);
        }


        public int GetNext_User_ID()
        {
            int NextuserID = 0;
            return NextuserID = objDB.GetNext_User_ID();
        }

        public string InsertUserProfile(eApprovalBO.eApprovalBO objInsertUserProfileBO)
        {
            string Result = string.Empty;
            return Result = objDB.InsertUserProfile(objInsertUserProfileBO);
        }



        public string UpdateUserProfile(eApprovalBO.eApprovalBO objUpdateUserProfileBO)
        {
            string Result = string.Empty;
            return Result = objDB.UpdateUserProfile(objUpdateUserProfileBO);
        }

        #endregion

        #region InitiateRequest

        public string GetSrNo(eApprovalBO.eApprovalBO objGetSrNoBO)
        {
            string SRno = string.Empty;
            return SRno = objDB.GetSrNo(objGetSrNoBO);
        }

        public string InsertInitiateSR(eApprovalBO.eApprovalBO objInsertInitiateSRBO)
        {
            string Result = string.Empty;
            return Result = objDB.InsertInitiateSR(objInsertInitiateSRBO);
        }


        //public string InsertFileContent(eApprovalBO.eApprovalBO objInsertFileContentBO)
        //{
        //    string Result = string.Empty;
        //    return Result = objDB.InsertFileContent(objInsertFileContentBO);
        //}


        #endregion

        #region ApprovalList

        public DataTable GetApprovalsList(eApprovalBO.eApprovalBO objGetApprovalsListBO)
        {
            DataTable dtGetApprovalsList = new DataTable();
            return dtGetApprovalsList = objDB.GetApprovalsList(objGetApprovalsListBO);
        }

        public DataSet GetGetSRData_Acc(eApprovalBO.eApprovalBO objGetGetSRData_AccBO)
        {
            DataSet dsGetGetSRData_Acc = new DataSet();
            return dsGetGetSRData_Acc = objDB.GetGetSRData_Acc(objGetGetSRData_AccBO);
        }

        public DataTable GetSRFileContent(eApprovalBO.eApprovalBO objGetSRFileContentBO)
        {
            DataTable dtGetSRFileContent = new DataTable();
            return dtGetSRFileContent = objDB.GetGetSRFileContent(objGetSRFileContentBO);
        }

        public string InsertFinaceApproval(eApprovalBO.eApprovalBO objInsertFinaceApprovalBO)
        {
            string Result = string.Empty;
            return Result = objDB.InsertFinaceApproval(objInsertFinaceApprovalBO);
        }




        public DataTable GetAccontsApprovalData_CEO(eApprovalBO.eApprovalBO objGetAccontsApprovalData_CEOBO)
        {
            DataTable dtGetAccontsApprovalData_CEO = new DataTable();
            return dtGetAccontsApprovalData_CEO = objDB.GetAccontsApprovalData_CEO(objGetAccontsApprovalData_CEOBO);
        }

        public string InsertCEOApproval(eApprovalBO.eApprovalBO objInsertCEOApprovalBO)
        {
            string Result = string.Empty;
            return Result = objDB.InsertCEOApproval(objInsertCEOApprovalBO);
        }


        #endregion

        #region View Approvals

        public DataTable GetViewApprovalsList(eApprovalBO.eApprovalBO objGetViewApprovalsListBO)
        {
            DataTable dtGetViewApprovalsList = new DataTable();
            return dtGetViewApprovalsList = objDB.GetViewApprovalsList(objGetViewApprovalsListBO);
        }



        public DataSet GetSRDataViewApprovals(eApprovalBO.eApprovalBO objGetSRDataViewApprovalsBO)
        {
            DataSet dtGetViewApprovalsList = new DataSet();
            return dtGetViewApprovalsList = objDB.GetSRDataViewApprovals(objGetSRDataViewApprovalsBO);
        }

        #endregion

        #region Homepage

        public DataTable GetViewApprovalsList_HomePage(eApprovalBO.eApprovalBO objGetViewApprovalsList_HomePageBO)
        {
            DataTable dtGetViewApprovalsList_HomePage = new DataTable();
            return dtGetViewApprovalsList_HomePage = objDB.GetViewApprovalsList_HomePage(objGetViewApprovalsList_HomePageBO);
        }

        #endregion

    }
}
