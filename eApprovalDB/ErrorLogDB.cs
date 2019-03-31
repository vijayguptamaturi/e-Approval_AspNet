using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace eApprovalDB
{
    public class ErrorLogDB
    {
        string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;
        eApprovalBO.ErrorLogBO objErrorLogBO = new eApprovalBO.ErrorLogBO();
        public string ErrorLog(eApprovalBO.ErrorLogBO objErrorLogBO)
        {
            string ErrorCode = string.Empty;
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand command = new SqlCommand("PROC_INSERT_ERROR_LOG", con);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.Add("@MethodName", SqlDbType.NVarChar).Value = objErrorLogBO.MethodName;
                command.Parameters.Add("@PageName", SqlDbType.NVarChar).Value = objErrorLogBO.PageName;
                command.Parameters.Add("@ErrorMessage", SqlDbType.NVarChar).Value = objErrorLogBO.ErrorMessage;
                command.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = objErrorLogBO.UserName;


                SqlParameter ECode = new SqlParameter("@ERRORCODE", SqlDbType.NVarChar, 100);
                ECode.Direction = ParameterDirection.Output;
                command.Parameters.Add(ECode);

                con.Open();
                command.ExecuteNonQuery();
                ErrorCode = ECode.Value.ToString();
                con.Close();
            }
            return ErrorCode;

        }
    }
}
