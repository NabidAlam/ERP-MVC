using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP;
using ERP.MODEL;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace ERP.DAL
{
    public class LoginDAL
    {

        OracleTransaction trans = null;

        #region "Oracle Connection Check"
        private OracleConnection GetConnection()
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConString"];
            string strConnString = conString.ConnectionString;
            return new OracleConnection(strConnString);
        }
        #endregion

        public LoginModel CheckValidUser(string strUserId, string strUserPassword, string strIPAddress)
        {
            string strMsg = "";

            LoginModel objLoginModel = new LoginModel();

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_check_valid_user");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (strUserId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = strUserId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (strUserPassword != "")
            {
                objOracleCommand.Parameters.Add("p_employee_password", OracleDbType.Varchar2, ParameterDirection.Input).Value = strUserPassword;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_password", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (strIPAddress != "")
            {
                objOracleCommand.Parameters.Add("p_ip_address", OracleDbType.Varchar2, ParameterDirection.Input).Value = strIPAddress;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_ip_address", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objLoginModel.MacAddress != null)
            {
                objOracleCommand.Parameters.Add("p_mac_address", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLoginModel.MacAddress;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_mac_address", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_designation_id", OracleDbType.Int32, 100, ParameterDirection.Output).Value = objLoginModel.DesignationId;
            objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Int32, 100, ParameterDirection.Output).Value = objLoginModel.UnitId;
            objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Int32, 100, ParameterDirection.Output).Value = objLoginModel.DepartmentId;
            objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Int32, 100, ParameterDirection.Output).Value = objLoginModel.SubSectionId;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Int32, 100, ParameterDirection.Output).Value = objLoginModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Int32, 100, ParameterDirection.Output).Value = objLoginModel.BranchOfficeId;
            objOracleCommand.Parameters.Add("p_soft_id", OracleDbType.Int32, 100, ParameterDirection.Output).Value = objLoginModel.SoftwareId;
            objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objOracleCommand.Connection = strConn;
                    strConn.Open();
                    trans = strConn.BeginTransaction();
                    objOracleCommand.ExecuteNonQuery();
                    trans.Commit();
                    strConn.Close();

                    objLoginModel.EmployeeId = objOracleCommand.Parameters["p_employee_id"].Value.ToString();
                    objLoginModel.DesignationId = objOracleCommand.Parameters["p_designation_id"].Value.ToString();
                    objLoginModel.UnitId = objOracleCommand.Parameters["p_unit_id"].Value.ToString();
                    objLoginModel.DepartmentId = objOracleCommand.Parameters["p_department_id"].Value.ToString();
                    objLoginModel.SubSectionId = objOracleCommand.Parameters["p_sub_section_id"].Value.ToString();
                    objLoginModel.HeadOfficeId = objOracleCommand.Parameters["p_head_office_id"].Value.ToString();
                    objLoginModel.BranchOfficeId = objOracleCommand.Parameters["p_branch_office_id"].Value.ToString();
                    objLoginModel.SoftwareId = objOracleCommand.Parameters["p_soft_id"].Value.ToString();

                    objLoginModel.Message = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                }

                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                    objOracleTransaction.Rollback();
                }

                finally
                {

                    strConn.Close();
                }

            }
            return objLoginModel;


        }
        public string ChangePassword(string strUserId, string strNewPassword, string strConfirmPassword, string strHeadOfficeId, string strBranchOfficeId)
        {
            string strMsg = "";

            LoginModel objLoginModel = new LoginModel();

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_change_password");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (strUserId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = strUserId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (strNewPassword != "")
            {
                objOracleCommand.Parameters.Add("p_employee_password", OracleDbType.Varchar2, ParameterDirection.Input).Value = strNewPassword;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_password", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = strUserId;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = strHeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = strBranchOfficeId;

            objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objOracleCommand.Connection = strConn;
                    strConn.Open();
                    trans = strConn.BeginTransaction();
                    objOracleCommand.ExecuteNonQuery();
                    trans.Commit();
                    strConn.Close();


                    strMsg = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                }

                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                    objOracleTransaction.Rollback();
                }

                finally
                {

                    strConn.Close();
                }

            }
            return strMsg;


        }

        public string CreateNewAccount(SignupModel objSignupModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_create_new_account");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objSignupModel.EmployeeId) ? objSignupModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("p_employee_password", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objSignupModel.Password) ? objSignupModel.Password : null;
            objOracleCommand.Parameters.Add("p_email_address", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objSignupModel.EmailAddress) ? objSignupModel.EmailAddress : null;
            objOracleCommand.Parameters.Add("p_mobile_no", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objSignupModel.MobileNo) ? objSignupModel.MobileNo : null;

            objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objOracleCommand.Connection = strConn;
                    strConn.Open();
                    trans = strConn.BeginTransaction();
                    objOracleCommand.ExecuteNonQuery();
                    trans.Commit();
                    strConn.Close();
                    strMsg = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                }

                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                    objOracleTransaction.Rollback();
                }

                finally
                {

                    strConn.Close();
                }

            }
            return strMsg;

        }
        public string ForgetPasswordRequest(ForgotPasswordModel objForgotPasswordModel)
        {
            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_forget_password_request");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objForgotPasswordModel.EmployeeId) ? objForgotPasswordModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("p_email_address", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objForgotPasswordModel.EmailAddress) ? objForgotPasswordModel.EmailAddress : null;
            objOracleCommand.Parameters.Add("p_mobile_no", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objForgotPasswordModel.MobileNo) ? objForgotPasswordModel.MobileNo : null;
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objOracleCommand.Connection = strConn;
                    strConn.Open();
                    trans = strConn.BeginTransaction();
                    objOracleCommand.ExecuteNonQuery();
                    trans.Commit();
                    strConn.Close();
                    strMsg = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                }

                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                    objOracleTransaction.Rollback();
                }

                finally
                {

                    strConn.Close();
                }

            }
            return strMsg;


        }
    }
}
