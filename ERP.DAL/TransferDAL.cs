using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Controllers;
using ERP.MODEL;
using Oracle.ManagedDataAccess.Client;

namespace ERP.DAL
{
    public class TransferDAL
    {
        OracleTransaction trans = null;

        #region Oracle Connection Check

        private OracleConnection GetConnection()
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConString"];
            string strConnString = conString.ConnectionString;
            return new OracleConnection(strConnString);
        }

        #endregion

        public TransferModel SearchEmployeeInfoById(TransferModel objTransferModel)
        {

            string sql = "";
            sql = "SELECT " +
                      "TO_CHAR (NVL (EMPLOYEE_NAME,'N/A'))EMPLOYEE_NAME, " +
                      "NVL (TO_CHAR (joining_date, 'dd/mm/yyyy'), ' ') joining_date, " +
                      "TO_CHAR (NVL (designation_name,'N/A'))designation_name, " +
                      "TO_CHAR (NVL (employee_id,'N/A'))employee_id, " +
                      "EMPLOYEE_PIC, " +
                      "NVL (TO_CHAR (transfer_date, 'dd/mm/yyyy'), ' ')transfer_date, " +
                    // "EMPLOYEE_ID, " +
                    //"EMPLOYEE_NAME, " +
                    //"TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                    //"DESIGNATION_NAME, " +
                    "HEAD_OFFICE_ID, " +
                    "BRANCH_OFFICE_ID " +
                    //"EMPLOYEE_PIC, " +
                    // "NVL(TO_CHAR(TRANSFER_DATE,'dd/mm/yyyy'), '')TRANSFER_DATE " +

                   "from vew_personal_info where employee_id = '" + objTransferModel.EmployeeId + "' and head_office_id = '" + objTransferModel.HeadOfficeId + "' AND branch_office_id = '" + objTransferModel.BranchOfficeId + "' ";

            //if (!string.IsNullOrWhiteSpace(objLeaveEntryForTransferModel.Year))
            //{
            //    sql = sql + " AND employee_id = '" + strEmployeeId + "' ";

            //}

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataReader objDataReader;

            using (OracleConnection strConn = GetConnection())
            {

                objCommand.Connection = strConn;
                strConn.Open();
                objDataReader = objCommand.ExecuteReader();
                try
                {
                    while (objDataReader.Read())
                    {

                        //objLeaveEntryForTransferModel.EmployeeName = objDataReader.GetString(0);
                        //objLeaveEntryForTransferModel.JoiningDate = objDataReader.GetString(1);
                        //objLeaveEntryForTransferModel.DesignationName = objDataReader.GetString(2);
                        //objLeaveEntryForTransferModel.EmployeeId = objDataReader.GetString(3);
                        //objLeaveEntryForTransferModel.TransferDate = objDataReader.GetString(4);
                        objTransferModel.EmployeeModel = new EmployeeModel();
                        objTransferModel.EmployeeId = objDataReader["employee_id"].ToString();
                        objTransferModel.EmployeeModel.EmployeeId = objDataReader["employee_id"].ToString();
                        objTransferModel.EmployeeModel.EmployeeName = objDataReader["EMPLOYEE_NAME"].ToString();
                        objTransferModel.EmployeeModel.JoiningDate = objDataReader["joining_date"].ToString();
                        objTransferModel.EmployeeModel.PresentDesignationName = objDataReader["designation_name"].ToString();
                        objTransferModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                        objTransferModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();
                        objTransferModel.EmployeeModel.EmployeeImage = objDataReader["EMPLOYEE_PIC"] == DBNull.Value ? new byte[0] : (byte[])objDataReader["EMPLOYEE_PIC"];
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);

                }

                finally
                {

                    strConn.Close();
                }

            }

            return objTransferModel;

        }


        public string EmployeeLeaveSaveForTransfer(TransferModel objTransferModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_emp_leave_for_tf");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTransferModel.EmployeeId;

            if (objTransferModel.Year != "")
            {
                objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTransferModel.Year;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objTransferModel.LeaveTypeId != "")
            {
                objOracleCommand.Parameters.Add("p_leave_type_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTransferModel.LeaveTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_leave_type_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objTransferModel.LeaveBalance != "")
            {
                objOracleCommand.Parameters.Add("p_max_leave", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTransferModel.LeaveBalance;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_max_leave", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objTransferModel.Remarks != "")
            {
                objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTransferModel.Remarks;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTransferModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTransferModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTransferModel.BranchOfficeId;

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



        public IEnumerable<TransferModel> LoadEmpLBRecordForTF(TransferModel objTransferModel)
        {
            List<TransferModel> leaveList = new List<TransferModel>();
            string sql = "";
            sql = "SELECT " +
               "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "UNIT_ID, " +
                  "UNIT_NAME, " +
                  "DEPARTMENT_ID, " +
                  "DEPARTMENT_NAME, " +
                  "SECTION_ID, " +
                  "SECTION_NAME, " +
                  "SUB_SECTION_ID, " +
                  "SUB_SECTION_NAME, " +
                  "LEAVE_YEAR, " +
                  "LEAVE_TYPE_ID, " +
                  "leave_type_name, " +
                  "HEAD_OFFICE_ID, " +
                  "BRANCH_OFFICE_ID, " +
                  "MAX_LEAVE " +
                 " FROM vew_search_emp_leave_for_tf where head_office_id = '" + objTransferModel.HeadOfficeId + "' and branch_office_id = '" + objTransferModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objTransferModel.EmployeeId))
            {
                sql = sql + "and employee_id = '" + objTransferModel.EmployeeId + "' ";
            }

            if (!string.IsNullOrWhiteSpace(objTransferModel.Year))
            {
                sql = sql + "and LEAVE_YEAR = '" + objTransferModel.Year + "' ";
            }


            //if (objLeaveEntryForTransferModel.Year.Length > 0)
            //{
            //    sql = sql + "and leave_year = '" + objLeaveEntryForTransferModel.Year + "' ";
            //}

            //if (strLeaveTypeId.Length > 0)
            //{

            //    sql = sql + "and LEAVE_TYPE_ID = '" + strLeaveTypeId + "' ";
            //}


            sql = sql + " order by SL ";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objTransferModel = new TransferModel();
                        // objLeaveEntryForTransferModel.Serial = objDataReader["sl"].ToString();
                        objTransferModel.EmployeeId = objDataReader["EMPLOYEE_ID"].ToString();
                        objTransferModel.Year = objDataReader["LEAVE_YEAR"].ToString();
                        objTransferModel.LeaveTypeId = objDataReader["LEAVE_TYPE_ID"].ToString();
                        objTransferModel.LeaveTypeName = objDataReader["leave_type_name"].ToString();

                        objTransferModel.LeaveBalance = objDataReader["MAX_LEAVE"].ToString();

                        leaveList.Add(objTransferModel);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objDataReader.Close();
                    objConnection.Close();
                }
            }

           // return loanAdvanceList;
            return leaveList;
        }



        public DataTable GetEmpLeaveTransferEdit(TransferModel objTransferModel)
        {

            DataTable dt1 = new DataTable();

            string sql = "";

            sql = "SELECT " +
                   "rownum sl, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "DESIGNATION_NAME, " +
                   "department_name, " +
                   "LEAVE_TYPE_ID, " +
                   "leave_type_name, " +
                   "REMARKS, " +
                   "LEAVE_YEAR, " +
                   "MAX_LEAVE, " +
                   "HEAD_OFFICE_ID, " +
                   "BRANCH_OFFICE_ID " +
                   " FROM VEW_SEARCH_EMP_LEAVE_FOR_TF where head_office_id = '" + objTransferModel.HeadOfficeId + "' and branch_office_id = '" + objTransferModel.BranchOfficeId +
                   "' and employee_id = '" + objTransferModel.EmployeeId + "' " +
                   "and LEAVE_TYPE_ID='" + objTransferModel.LeaveTypeId + "' " +
                   "and LEAVE_YEAR= '" + objTransferModel.Year + "' ";

   
            //if (!string.IsNullOrEmpty(objAccessoriesOrderModel.GridOrderDate))               
            //{

            //    sql = sql + "and ORDER_DATE =To_Date( '" + objAccessoriesOrderModel.GridOrderDate + "', 'dd/mm/yy')";
            //}

            //if (!string.IsNullOrEmpty(objAccessoriesOrderModel.GridItemId))
            //{

            //    sql = sql + "and ITEM_ID= '" + objAccessoriesOrderModel.GridItemId + "'";
            //}

            //if (!string.IsNullOrEmpty(objAccessoriesOrderModel.GridItemCode))
            //{

            //    sql = sql + "and ITEM_CODE = '" + objAccessoriesOrderModel.GridItemCode + "'";
            //}


            //if (!string.IsNullOrEmpty(objAccessoriesOrderModel.GridItemId))
            //{

            //    sql = sql + "and TRAN_ID= '" + objAccessoriesOrderModel.GridTranId + "'";
            //}


            //sql = sql + " ORDER BY ORDER_DATE DESC";

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(dt1);
                    dt1.AcceptChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }


            return dt1;

        }







        //TRANSFER EMPLOYEE BY ID

        public DataTable LoadEmpTransferRecord(EmployeeTransferByIdModel objLEmployeeTransferById)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                "rownum sl, " +

   "EMPLOYEE_ID, " +
   "UNIT_ID, " +
   "UNIT_NAME, " +
   "DEPARTMENT_ID, " +
   "DEPARTMENT_NAME, " +
   "SECTION_ID, " +
   "SECTION_NAME, " +
   "SUB_SECTION_ID, " +
   "SUB_SECTION_NAME, " +
   "TRANSFER_YEAR, " +
   "TRANSFER_MONTH, " +
   "HEAD_OFFICE_ID, " +
   "BRANCH_OFFICE_ID, " +
   "EMPLOYEE_ID_NEW, " +
   "UNIT_ID_NEW, " +
   "UNIT_NAME_NEW, " +
   "DEPARTMENT_ID_NEW, " +
   "DEPARTMENT_NAME_NEW, " +
   "SECTION_ID_NEW, " +
   "SECTION_NAME_NEW, " +
   "SUB_SECTION_ID_NEW, " +
   "SUB_SECTION_NAME_NEW " +

                  " FROM VEW_EMPLOYEE_TRANSFER_BY_ID where head_office_id = '" + objLEmployeeTransferById.HeadOfficeId + "' and branch_office_id = '" + objLEmployeeTransferById.BranchOfficeId + "' AND TRANSFER_YEAR = '" + objLEmployeeTransferById.TransferYear + "' AND month_id = '" + objLEmployeeTransferById.MonthId + "'  ";

            if (objLEmployeeTransferById.EmployeeId.Length > 0)
            {

                sql = sql + "and employee_id = '" + objLEmployeeTransferById.EmployeeId + "' ";
            }




            sql = sql + " order by SL ";

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(dt);
                    dt.AcceptChanges();
                }
                catch (Exception ex)
                {

                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }


            return dt;

        }



        public EmployeeTransferByIdModel SearchEmployeeTransferById(EmployeeTransferByIdModel objEmployeeTransferById)
        {

            string sql = "";
            sql = "SELECT " +
                      "TO_CHAR (NVL (EMPLOYEE_NAME,'N/A'))EMPLOYEE_NAME, " +
                      "NVL (TO_CHAR (joining_date, 'dd/mm/yyyy'), ' ') joining_date, " +
                      "TO_CHAR (NVL (designation_name,'N/A'))designation_name, " +
                      "TO_CHAR (NVL (employee_id,'N/A'))employee_id, " +
                      "EMPLOYEE_PIC, " +
                      "NVL (TO_CHAR (transfer_date, 'dd/mm/yyyy'), ' ')transfer_date, " +
                    // "EMPLOYEE_ID, " +
                    //"EMPLOYEE_NAME, " +
                    //"TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                    //"DESIGNATION_NAME, " +
                    "HEAD_OFFICE_ID, " +
                    "BRANCH_OFFICE_ID " +
                   //"EMPLOYEE_PIC, " +
                   // "NVL(TO_CHAR(TRANSFER_DATE,'dd/mm/yyyy'), '')TRANSFER_DATE " +

                   "from vew_personal_info where employee_id = '" + objEmployeeTransferById.EmployeeId + "' and head_office_id = '" + objEmployeeTransferById.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeTransferById.BranchOfficeId + "' ";

            //if (!string.IsNullOrWhiteSpace(objLeaveEntryForTransferModel.Year))
            //{
            //    sql = sql + " AND employee_id = '" + strEmployeeId + "' ";

            //}

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataReader objDataReader;

            using (OracleConnection strConn = GetConnection())
            {

                objCommand.Connection = strConn;
                strConn.Open();
                objDataReader = objCommand.ExecuteReader();
                try
                {
                    while (objDataReader.Read())
                    {

                        //objLeaveEntryForTransferModel.EmployeeName = objDataReader.GetString(0);
                        //objLeaveEntryForTransferModel.JoiningDate = objDataReader.GetString(1);
                        //objLeaveEntryForTransferModel.DesignationName = objDataReader.GetString(2);
                        //objLeaveEntryForTransferModel.EmployeeId = objDataReader.GetString(3);
                        //objLeaveEntryForTransferModel.TransferDate = objDataReader.GetString(4);
                        objEmployeeTransferById.EmployeeModel = new EmployeeModel();
                        objEmployeeTransferById.EmployeeId = objDataReader["employee_id"].ToString();
                        objEmployeeTransferById.EmployeeModel.EmployeeId = objDataReader["employee_id"].ToString();
                        objEmployeeTransferById.EmployeeModel.EmployeeName = objDataReader["EMPLOYEE_NAME"].ToString();
                        objEmployeeTransferById.EmployeeModel.JoiningDate = objDataReader["joining_date"].ToString();
                        objEmployeeTransferById.EmployeeModel.PresentDesignationName = objDataReader["designation_name"].ToString();
                        objEmployeeTransferById.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                        objEmployeeTransferById.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();
                        objEmployeeTransferById.EmployeeModel.EmployeeImage = objDataReader["EMPLOYEE_PIC"] == DBNull.Value ? new byte[0] : (byte[])objDataReader["EMPLOYEE_PIC"];
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);

                }

                finally
                {

                    strConn.Close();
                }

            }

            return objEmployeeTransferById;

        }



        public string EmployeeTransferProcess(EmployeeTransferByIdModel objEmployeeTransferById)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_emp_transfer_sco");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeTransferById.EmployeeId;


            if (objEmployeeTransferById.UnitId != "")
            {
                objOracleCommand.Parameters.Add("P_unit_id_new", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeTransferById.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_unit_id_new", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objEmployeeTransferById.DepartmentId != "")
            {
                objOracleCommand.Parameters.Add("p_department_id_new", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeTransferById.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id_new", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;

            }


            if (objEmployeeTransferById.SectionId != "")
            {
                objOracleCommand.Parameters.Add("p_section_id_new", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeTransferById.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id_new", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            if (objEmployeeTransferById.SubSectionId != "")
            {
                objOracleCommand.Parameters.Add("p_sub_section_id_new", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeTransferById.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id_new", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_transfer_year", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeTransferById.TransferYear;
            objOracleCommand.Parameters.Add("p_month_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeTransferById.MonthId;



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeTransferById.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeTransferById.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeTransferById.BranchOfficeId;

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
