using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace eApprovalDB
{
    public class CommonDB
    {
        public string GetConnectionString()
        {
            string constr = ConfigurationManager.ConnectionStrings["mycon"].ConnectionString;

            return constr;

            
        }

        public DataTable GetDepartments()
        {
            DataTable dtDepartments = new DataTable();
            string constr = GetConnectionString();
          
            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("Get_Departments", con);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtDepartments);
            }

            return dtDepartments;

        }


        public DataTable GetDepartments_RoleBased(eApprovalBO.eApprovalBO objGetDepartments_RoleBasedBO)
        {
            DataTable dtGetDepartments_RoleBased = new DataTable();
            string constr = GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("Get_Departments_RoleBased", con);

                cmd.Parameters.Add("@ROLEID", SqlDbType.Int).Value = Convert.ToInt32(objGetDepartments_RoleBasedBO.RoleID);
                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = Convert.ToInt32(objGetDepartments_RoleBasedBO.DeptID);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtGetDepartments_RoleBased);
            }

            return dtGetDepartments_RoleBased;

        }


        public DataTable GetRoles()
        {
            DataTable dtRoles = new DataTable();
            string constr = GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("Get_Roles", con);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtRoles);
            }

            return dtRoles;

        }


        public DataTable Getlocations()
        {
            DataTable dtLocation = new DataTable();
            string constr = GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_LOCATIONS", con);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtLocation);
            }

            return dtLocation;

        }

        public DataTable GetAccountHeads()
        {
            DataTable dtAccountHead = new DataTable();
            string constr = GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_ACCOUNT_HEADS", con);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtAccountHead);
            }

            return dtAccountHead;

        }


        public byte[] GetSignContent(eApprovalBO.eApprovalBO objGetSignContentBO)
        {
            byte[] SignContent = null;

            string constr = GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GETSIGNATURE_CONTENT", con);
                cmd.Parameters.Add("@USERID", SqlDbType.Int).Value = Convert.ToInt32(objGetSignContentBO.UserID);




                SqlParameter P = new SqlParameter("@SIGNCONTENT", SqlDbType.VarBinary,-1);

                P.Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(P);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                //SqlDataAdapter sda = new SqlDataAdapter(cmd);
                //sda.Fill(dtSignContent);

                SignContent = (byte[])P.Value;
            }

            return SignContent;

        }


        public DataTable GetOldSRno(eApprovalBO.eApprovalBO objGetOldSRnoBO)
        {
            DataTable dtGetOldSRno = new DataTable();
            string constr = GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_OLD_SRNOS", con);

                cmd.Parameters.Add("@DEPTID", SqlDbType.Int).Value = Convert.ToInt32(objGetOldSRnoBO.DeptID);

                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(dtGetOldSRno);
            }

            return dtGetOldSRno;

        }


        public int IsOldPasswordMatch(eApprovalBO.eApprovalBO objIsOldPasswordMatchBO)
        {
            int IsOldPasswordMatch = 0;

            string constr = GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("PROC_CHECK_OLD_PASSWORD", con);
                cmd.Parameters.Add("@USERID", SqlDbType.Int).Value = Convert.ToInt32(objIsOldPasswordMatchBO.UserID);
                cmd.Parameters.Add("@OLD_PASSWORD", SqlDbType.NVarChar).Value = objIsOldPasswordMatchBO.Password;

                SqlParameter P = new SqlParameter("@ISPASSWORD_MATCH", SqlDbType.Int);

                P.Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(P);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                IsOldPasswordMatch = Convert.ToInt32(P.Value);
            }

            return IsOldPasswordMatch;

        }


        public int ChangePassword(eApprovalBO.eApprovalBO objChangePasswordBO)
        {
            int Success = 0;

            string constr = GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("PROC_UPDATE_PASSWORD_CHANGE", con);
                cmd.Parameters.Add("@USERID", SqlDbType.Int).Value = Convert.ToInt32(objChangePasswordBO.UserID);
                cmd.Parameters.Add("@OLD_PASSWORD", SqlDbType.NVarChar).Value = objChangePasswordBO.Password;
                cmd.Parameters.Add("@NEW_PASSWORD", SqlDbType.NVarChar).Value = objChangePasswordBO.NewPassword;

                SqlParameter P = new SqlParameter("@SUCCESS", SqlDbType.Int);

                P.Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(P);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Success = Convert.ToInt32(P.Value);
            }

            return Success;

        }


        public string  GetFormParameterValue(eApprovalBO.eApprovalBO objGetFormParameterValueBO)
        {
            string Value = string.Empty;

            string constr = GetConnectionString();

            using (SqlConnection con = new SqlConnection(constr))
            {
                SqlCommand cmd = new SqlCommand("GET_FORM_PARAMETER_DATA", con);
                cmd.Parameters.Add("@PARAMID", SqlDbType.Int).Value = objGetFormParameterValueBO.ParamID;

                SqlParameter P = new SqlParameter("@VALUE", SqlDbType.NVarChar,2);

                P.Direction = ParameterDirection.Output;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(P);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();

                Value = P.Value.ToString();
            }

            return Value;

        }




        
        
    }
}
