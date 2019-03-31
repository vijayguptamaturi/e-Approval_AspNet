using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace eApprovalDB
{
   public class MailDB
    {
       CommonDB objCDB = new CommonDB();
        public DataTable GetSRDataForMail(string SRNO)
        {

            string constr = objCDB.GetConnectionString();

            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(constr))
            {

                SqlCommand cmd = new SqlCommand("GETSR_SENDMAIL", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@SRNO", SqlDbType.NVarChar).Value = SRNO;

                con.Open();
                cmd.ExecuteNonQuery();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dt);
            }

            return dt;

        }


        public DataSet GetMailDetails(int UserID, int RoleID, string SRNO)
        {

            string constr = objCDB.GetConnectionString();

            DataSet  ds = new DataSet();

            using (SqlConnection con = new SqlConnection(constr))
            {

                SqlCommand cmd = new SqlCommand("GET_MAIL_CREDENTIAL_DETAILS", con);

                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ROLEID", SqlDbType.Int).Value = RoleID;

                cmd.Parameters.Add("@USERID", SqlDbType.Int).Value = UserID;

                cmd.Parameters.Add("@SRNO", SqlDbType.NVarChar).Value = SRNO;

                con.Open();
                cmd.ExecuteNonQuery();

                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(ds);
            }

            return ds;

        }



    }
}
