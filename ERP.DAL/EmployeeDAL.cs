using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.MODEL;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ERP.DAL
{
    public class EmployeeDAL
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

        #region Attendance Process

        public AttendanceProcessModel GetDirectoryName(string strHeadOfficeId, string strBranchOfficeId)
        {

            AttendanceProcessModel objGetDir = new AttendanceProcessModel();

            string sql = "";
            sql = "SELECT " +
                  "NVL(UPLOAD_DIR_NAME,'N/A') " +
                  "FROM S_DATA_UPLOAD_DIR  ";

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
                        objGetDir.DataUploadDir = objDataReader.GetString(0);
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
            return objGetDir;
        }
        public string SaveAttendenceProcessDaily(AttendanceProcessModel objProcessModel)
        {

            string strMsg = "";

            
            OracleCommand objOracleCommand = new OracleCommand("pro_attendance_data_upload");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objProcessModel.UnitId != "")
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessModel.DepartmentId != "")
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessModel.SectionId != "")
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessModel.SubSectionId != "")
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessModel.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }
            return strMsg;
        }

        public string AttendenceFileProcess(AttendanceProcessModel objAttendanceModel)
        {

            string strMsg = "";
            
            OracleCommand objOracleCommand = new OracleCommand("pro_attendence_file_upload");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objAttendanceModel.FileName.Length > 0)
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendanceModel.FileName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendanceModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendanceModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendanceModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendanceModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendanceModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendanceModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendanceModel.BranchOfficeId;

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

                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    strConn.Close();
                }

            }
            return strMsg;
        }


        #endregion

        #region Duty Roaster

        public DataTable GetDutyRoasterRecord(DutyRoasterModel objRoasterModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "DESIGNATION_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "FIRST_IN_TIME, " +
                  "LAST_OUT_TIME, " +
                  "TO_CHAR(log_date,'dd/mm/yyyy')log_date, " +
                  "day_name, " +
                  "department_name, " +
                  "weekly_holiday_id " +

                  " FROM vew_duty_roasting_record where head_office_id = '" + objRoasterModel.HeadOfficeId + "' " +
                  "and branch_office_id = '" + objRoasterModel.BranchOfficeId + "' " +
                  "AND  employee_id = '" + objRoasterModel.EmployeeId + "'" +
                  " and log_date between to_Date('" + objRoasterModel.FromDate + "', 'dd/mm/yyyy')  " +
                  "and to_Date('" + objRoasterModel.ToDate + "', 'dd/mm/yyyy') ";

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

        public string SearchDutyRoaster(DutyRoasterModel objRoasterModel)
        {

            string strMsg = "";
            
            OracleCommand objOracleCommand = new OracleCommand("pro_individual_duty_roaster");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objRoasterModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objRoasterModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objRoasterModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objRoasterModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objRoasterModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objRoasterModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objRoasterModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objRoasterModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objRoasterModel.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }

            return strMsg;
        }

        public string SearchDutyRoasterPremonth(DutyRoasterModel objRoasterModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_duty_roaster_pre_month");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objRoasterModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objRoasterModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objRoasterModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objRoasterModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objRoasterModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objRoasterModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objRoasterModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objRoasterModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objRoasterModel.BranchOfficeId;

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

        public string SaveDutyRoaster(DutyRoasterModel objRoasterModel)
        {

            string strMsg = "";

           
            OracleCommand objOracleCommand = new OracleCommand("PRO_DUTY_ROASTING_TIME_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objRoasterModel.EmployeeId != null)
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objRoasterModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objRoasterModel.InTime != null)
            {
                objOracleCommand.Parameters.Add("P_FIRST_IN_TIME", OracleDbType.Varchar2, ParameterDirection.Input).Value = objRoasterModel.InTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FIRST_IN_TIME", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objRoasterModel.OutTime != null)
            {
                objOracleCommand.Parameters.Add("P_LAST_OUT_TIME", OracleDbType.Varchar2, ParameterDirection.Input).Value = objRoasterModel.OutTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LAST_OUT_TIME", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objRoasterModel.LogDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objRoasterModel.LogDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objRoasterModel.WeeklyHolidayId != null)
            {
                objOracleCommand.Parameters.Add("P_WEEKLY_HOLIDAY_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objRoasterModel.WeeklyHolidayId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_WEEKLY_HOLIDAY_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objRoasterModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objRoasterModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objRoasterModel.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    strConn.Close();
                }

            }
            return strMsg;
        }

        #endregion

        #region Attendance Approval

        public DataTable GetEmployeeAttendanceRecordForApproved(AttendanceApprovalModel objApprovalModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +

                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy') JOINING_DATE, " +

                  "DESIGNATION_NAME, " +

                  "DEPARTMENT_NAME, " +
                  "TO_CHAR(LOG_DATE,'dd/mm/yyyy') LOG_DATE, " +

                  "FIRST_IN, " +
                  "LAST_OUT, " +
                  "DAY_TYPE, " +
                  "TOTAL_DUTY_TIME, " +
                  "APPROVED_STATUS " +


                  " FROM VEW_ATTENDANCE_PENDING_LIST where head_office_id = '" + objApprovalModel.HeadOfficeId + "' and " +
                  "branch_office_id = '" + objApprovalModel.BranchOfficeId + "' AND active_yn = '" + objApprovalModel.ActiveYN + "' AND " +
                  "log_date between to_Date('" + objApprovalModel.FromDate + "', 'dd/mm/yyyy') and  to_Date('" + objApprovalModel.ToDate + "', 'dd/mm/yyyy')  ";

            if (objApprovalModel.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objApprovalModel.EmployeeId + "' ";
            }

            if (objApprovalModel.EmployeeName != null)
            {
                sql = sql + "and (lower(employee_name) like lower( '%" + objApprovalModel.EmployeeName + "%')  or upper(employee_name)like upper('%" + objApprovalModel.EmployeeName + "%') )";

            }

            if (objApprovalModel.DepartmentId != null)
            {

                sql = sql + "and department_id = '" + objApprovalModel.DepartmentId + "' ";
            }

            if (objApprovalModel.UnitId != null)
            {

                sql = sql + "and unit_id = '" + objApprovalModel.UnitId + "' ";
            }

            if (objApprovalModel.SubSectionId != null)
            {

                sql = sql + "and sub_section_id = '" + objApprovalModel.SubSectionId + "' ";
            }

            if (objApprovalModel.SectionId != null)
            {

                sql = sql + "and section_id = '" + objApprovalModel.SectionId + "' ";
            }

            sql = sql + " order by SL";


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

        public string ProcessAttendanceApproved(AttendanceApprovalModel objApprovalModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("PRO_ATTENDENCE_APPROVED");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objApprovalModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objApprovalModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objApprovalModel.LogDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objApprovalModel.LogDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objApprovalModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("P_DEPARTMENT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objApprovalModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_DEPARTMENT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objApprovalModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("P_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objApprovalModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objApprovalModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("P_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objApprovalModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objApprovalModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("P_SUB_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objApprovalModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SUB_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objApprovalModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objApprovalModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objApprovalModel.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    strConn.Close();
                }

            }
            return strMsg;
        }

        public string DeleteAttendanceApproved(AttendanceApprovalModel objApprovalModel)
        {
            string strMsg = "";

        
            OracleCommand objOracleCommand = new OracleCommand("PRO_ATTENDENCE_APPROVED_DELETE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objApprovalModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objApprovalModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objApprovalModel.LogDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objApprovalModel.LogDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objApprovalModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objApprovalModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objApprovalModel.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    strConn.Close();
                }
            }
            return strMsg;
        }
        #endregion

        #region Attendance Process Manual

        // Load Missing attendance of employees 
        public DataTable GetMissingEmployeeAttendance(AttendanceProcessManualModel objProcessManualModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +

                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +

                  "DESIGNATION_NAME, " +

                  "DEPARTMENT_NAME, " +
                  "FIRST_IN, " +
                  "LAST_OUT, " +
                  "TO_CHAR(LOG_DATE,'dd/mm/yyyy')LOG_DATE, " +
                  "day_type_id, " +
                  "ACTIVE_STATUS, " +
                  "PUNCH_STATUS, " +
                  "puntch_type_tatus " + //leave status


                  " FROM vew_attendance_record_manul where head_office_id = '" + objProcessManualModel.HeadOfficeId + "' " +
                  "and branch_office_id = '" + objProcessManualModel.BranchOfficeId + "' AND active_yn = '" + objProcessManualModel.InActive + "' " +
                  "AND log_date between to_Date('" + objProcessManualModel.FromDate + "', 'dd/mm/yyyy') and  to_Date('" + objProcessManualModel.ToDate + "'," +
                  " 'dd/mm/yyyy') and missing_yn = 'Y' and manual_yn = 'N' ";

            if (objProcessManualModel.EmployeeId != null)
            {
                sql = sql + "and employee_id = '" + objProcessManualModel.EmployeeId + "' ";
            }

            if (objProcessManualModel.EmployeeName != null)
            {
                sql = sql + "and (lower(employee_name) like lower( '%" + objProcessManualModel.EmployeeName + "%') " +
                      " or upper(employee_name)like upper('%" + objProcessManualModel.EmployeeName + "%') )";

            }

            if (objProcessManualModel.DepartmentId != null)
            {
                sql = sql + "and department_id = '" + objProcessManualModel.DepartmentId + "' ";
            }

            if (objProcessManualModel.UnitId != null)
            {
                sql = sql + "and unit_id = '" + objProcessManualModel.UnitId + "' ";
            }

            if (objProcessManualModel.SubSectionId != null)
            {
                sql = sql + "and sub_section_id = '" + objProcessManualModel.SubSectionId + "' ";
            }

            if (objProcessManualModel.SectionId != null)
            {
                sql = sql + "and section_id = '" + objProcessManualModel.SectionId + "' ";
            }

            sql = sql + " order by SL";


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

        // Load absent attendance of employees 
        public DataTable GetAbsentEmployeeAttendance(AttendanceProcessManualModel objProcessManualModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +

                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +

                  "DESIGNATION_NAME, " +

                  "DEPARTMENT_NAME, " +
                  "FIRST_IN, " +
                  "LAST_OUT, " +
                  "TO_CHAR(LOG_DATE,'dd/mm/yyyy')LOG_DATE, " +

                  "day_type_id, " +
                  "ACTIVE_STATUS, " +
                  "PUNCH_STATUS, " +
                  "puntch_type_tatus " + //leave status


                  " FROM vew_attendance_record_manul where head_office_id = '" + objProcessManualModel.HeadOfficeId + "' " +
                  "and branch_office_id = '" + objProcessManualModel.BranchOfficeId + "' " +
                  "AND active_yn = '" + objProcessManualModel.InActive + "' " +
                  "AND log_date between to_Date('" + objProcessManualModel.FromDate + "', 'dd/mm/yyyy') and  to_Date('" + objProcessManualModel.ToDate + "', 'dd/mm/yyyy') " +
                  "and ABSENT_YN = 'Y' and manual_yn = 'N' ";

            if (objProcessManualModel.EmployeeId != null)
            {
                sql = sql + "and employee_id = '" + objProcessManualModel.EmployeeId + "' ";
            }

            if (objProcessManualModel.EmployeeName != null)
            {
                sql = sql + "and (lower(employee_name) like lower( '%" + objProcessManualModel.EmployeeName + "%')  or upper(employee_name)like upper('%" + objProcessManualModel.EmployeeName + "%') )";
            }

            if (objProcessManualModel.DepartmentId != null)
            {
                sql = sql + "and department_id = '" + objProcessManualModel.DepartmentId + "' ";
            }

            if (objProcessManualModel.UnitId != null)
            {
                sql = sql + "and unit_id = '" + objProcessManualModel.UnitId + "' ";
            }

            if (objProcessManualModel.SubSectionId != null)
            {
                sql = sql + "and sub_section_id = '" + objProcessManualModel.SubSectionId + "' ";
            }

            if (objProcessManualModel.SectionId != null)
            {

                sql = sql + "and section_id = '" + objProcessManualModel.SectionId + "' ";
            }

            sql = sql + " order by SL";


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

        // Load attendance of employees 
        public DataTable GetEmployeeRecordForAttendanceManual(AttendanceProcessManualModel objProcessManualModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum Sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +

                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +

                  "DESIGNATION_NAME, " +

                  "DEPARTMENT_NAME, " +
                  "FIRST_IN, " +
                  "LAST_OUT, " +
                  "TO_CHAR(LOG_DATE,'dd/mm/yyyy')LOG_DATE, " +

                  "day_type_id, " +
                  "ACTIVE_STATUS, " +
                  "PUNCH_STATUS, " +
                  "puntch_type_tatus " +  //leave status

                  " FROM vew_attendance_record_manul where head_office_id = '" + objProcessManualModel.HeadOfficeId + "'" +

                  " and branch_office_id = '" + objProcessManualModel.BranchOfficeId + "'" +

                  " AND active_yn = '" + objProcessManualModel.InActive + "' " +

                  "AND log_date between to_Date('" + objProcessManualModel.FromDate + "', 'dd/mm/yyyy') and  to_Date('" + objProcessManualModel.ToDate + "', 'dd/mm/yyyy')  ";

            if (objProcessManualModel.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objProcessManualModel.EmployeeId + "' ";
            }

            if (objProcessManualModel.EmployeeName != null)
            {
                sql = sql + "and (lower(employee_name) like lower( '%" + objProcessManualModel.EmployeeName + "%')  " +
                      "or upper(employee_name)like upper('%" + objProcessManualModel.EmployeeName + "%') )";

            }

            if (objProcessManualModel.DepartmentId != null)
            {

                sql = sql + "and department_id = '" + objProcessManualModel.DepartmentId + "' ";
            }

            if (objProcessManualModel.UnitId != null)
            {

                sql = sql + "and unit_id = '" + objProcessManualModel.UnitId + "' ";
            }

            if (objProcessManualModel.SubSectionId != null)
            {

                sql = sql + "and sub_section_id = '" + objProcessManualModel.SubSectionId + "' ";
            }

            if (objProcessManualModel.SectionId != null)
            {

                sql = sql + "and section_id = '" + objProcessManualModel.SectionId + "' ";
            }


            sql = sql + " order by SL";


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


        // add missing attendance of employee to db
        public string AddAttendanceRecordMissing(AttendanceProcessManualModel objProcessManualModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_ADD_ATTENDANCE_MISSING");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objProcessManualModel.EmployeeId != null)
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("p_unit_d", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_d", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("p_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("p_sub_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }





            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.BranchOfficeId;

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

        // add absent attendance of employee to db
        public string AddAttendanceRecordAbsent(AttendanceProcessManualModel objProcessManualModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_ADD_ATTENDANCE_ABSENT");
            objOracleCommand.CommandType = CommandType.StoredProcedure;



            if (objProcessManualModel.EmployeeId != null)
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("p_unit_d", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_d", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("p_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("p_sub_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }





            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.BranchOfficeId;

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

        // add manual attendance of employee to db
        public string AddRecordAttendanceManual(AttendanceProcessManualModel objProcessManualModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("PRO_ADD_ATTENDANCE_REOCRD");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objProcessManualModel.EmployeeId != null)
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("p_unit_d", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_d", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("p_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("p_sub_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }
            }
            return strMsg;
        }

        // process attendance----
        public string ProcessAttendanceManual(AttendanceProcessManualModel objProcessManualModel)
        {

            string strMsg = "";
            
            OracleCommand objOracleCommand = new OracleCommand("pro_attendance_process_manual");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objProcessManualModel.UnitId != "")
            {
                objOracleCommand.Parameters.Add("p_unit_d", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_d", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.DepartmentId != "")
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.SectionId != "")
            {
                objOracleCommand.Parameters.Add("p_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.SubSectionId != "")
            {
                objOracleCommand.Parameters.Add("p_sub_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_Section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.BranchOfficeId;

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

                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    strConn.Close();
                }

            }
            return strMsg;
        }

        // save edited data of attendance of the employee----
        public string SaveAttendenceManualEntry(AttendanceProcessManualModel objProcessManualModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("PRO_ATTENDENCE_MANUAL_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objProcessManualModel.EmployeeId != null)
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.LogDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.LogDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.InTime != null)
            {
                objOracleCommand.Parameters.Add("P_FIRST_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.InTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FIRST_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.OutTime != null)
            {
                objOracleCommand.Parameters.Add("P_LAST_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.OutTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LAST_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.LunchOutTime != "")
            {
                objOracleCommand.Parameters.Add("P_LUNCH_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.LunchOutTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LUNCH_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.LunchInTime != "")
            {
                objOracleCommand.Parameters.Add("LUNCH_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.LunchInTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("LUNCH_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("P_DEPARTMENT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_DEPARTMENT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("P_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("P_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("P_SUB_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SUB_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.DayTypeId != null)
            {
                objOracleCommand.Parameters.Add("P_DAY_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.DayTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_DAY_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }

            return strMsg;
        }

        // process attendance 
        public string AttendenceProcessDaily(AttendanceProcessManualModel objProcessManualModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_attendance_data_upload");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objProcessManualModel.UnitId != "")
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.DepartmentId != "")
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.SectionId != "")
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.SubSectionId != "")
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objProcessManualModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    strConn.Close();
                }

            }
            return strMsg;
        }
        #endregion

        #region Employee Entry       

        public string PermissionYn(string strEmployeeId, string strHeadOfficeId, string strBranchOfficeId)
        {

            string sql = "", strMsg = "";
            sql = "SELECT  'Y' " +

                " from vew_salary_permission where employee_id = '" + strEmployeeId + "' AND head_office_id = '" + strHeadOfficeId + "' AND branch_office_id = '" + strBranchOfficeId + "'  ";



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


                        strMsg = objDataReader.GetString(0);

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

            return strMsg;

        }


        //name: mezba & date: 09.01.2019
        public string SaveEmployeeInformation(EmployeeModel objEmployeeModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_INFORMATION_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId) ? objEmployeeModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("p_card_no", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.CardNo) ? objEmployeeModel.CardNo : null;
            objOracleCommand.Parameters.Add("p_employee_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeName) ? objEmployeeModel.EmployeeName : null;
            objOracleCommand.Parameters.Add("P_EMPLOYEE_NAME_BANGLA", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeNameBangla) ? objEmployeeModel.EmployeeNameBangla : null;
            objOracleCommand.Parameters.Add("P_FATHER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.FatherName) ? objEmployeeModel.FatherName : null;
            objOracleCommand.Parameters.Add("P_MOTHER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.MotherName) ? objEmployeeModel.MotherName : null;
            objOracleCommand.Parameters.Add("P_DATE_OF_BIRTH", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.DateOfBirth) ? objEmployeeModel.DateOfBirth : null;
            objOracleCommand.Parameters.Add("P_PUNCH_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.PunchCode) ? objEmployeeModel.PunchCode : null;
            objOracleCommand.Parameters.Add("P_BLOOD_GROUP_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.BloodGroupId) ? objEmployeeModel.BloodGroupId : null;
            objOracleCommand.Parameters.Add("P_GENDER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.GenderId) ? objEmployeeModel.GenderId : null;
            objOracleCommand.Parameters.Add("P_MARITAL_STATUS_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.MaritalStatusId) ? objEmployeeModel.MaritalStatusId : null;
            objOracleCommand.Parameters.Add("P_DISTRICT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.DistrictId) ? objEmployeeModel.DistrictId : null;
            objOracleCommand.Parameters.Add("P_DIVISION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.DivisionId) ? objEmployeeModel.DivisionId : null;
            objOracleCommand.Parameters.Add("P_RELIGION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ReligionId) ? objEmployeeModel.ReligionId : null;
            objOracleCommand.Parameters.Add("P_COUNTRY_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.CountryId) ? objEmployeeModel.CountryId : null;
            objOracleCommand.Parameters.Add("P_NID_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.NIDNo) ? objEmployeeModel.NIDNo : null;
            objOracleCommand.Parameters.Add("P_TIN_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.TINNo) ? objEmployeeModel.TINNo : null;
            objOracleCommand.Parameters.Add("P_SPOUSE_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.SpouseName) ? objEmployeeModel.SpouseName : null;
            objOracleCommand.Parameters.Add("P_PRESENT_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.PresentAddress) ? objEmployeeModel.PresentAddress : null;
            objOracleCommand.Parameters.Add("P_PERMANENT_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.PermanentAddress) ? objEmployeeModel.PermanentAddress : null;
            objOracleCommand.Parameters.Add("P_MAIL_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmailAddress) ? objEmployeeModel.EmailAddress : null;
            objOracleCommand.Parameters.Add("P_CONTACT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ContactNo) ? objEmployeeModel.ContactNo : null;
            objOracleCommand.Parameters.Add("P_EMERGENCY_CONTACT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmergencyContactNo) ? objEmployeeModel.EmergencyContactNo : null;
            objOracleCommand.Parameters.Add("P_PASSPORT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.PassportNo) ? objEmployeeModel.PassportNo : null;
            objOracleCommand.Parameters.Add("P_DRIVING_LICENSE_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.DrivingLicenseNo) ? objEmployeeModel.DrivingLicenseNo : null;
            objOracleCommand.Parameters.Add("P_OCCURENCE_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeementType) ? objEmployeeModel.EmployeementType : null;
            objOracleCommand.Parameters.Add("P_JOINING_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.JoiningDate) ? objEmployeeModel.JoiningDate : null;
            objOracleCommand.Parameters.Add("P_JOB_CONFIRMATION_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.JobConfirmationDate) ? objEmployeeModel.JobConfirmationDate : null;
            objOracleCommand.Parameters.Add("P_JOINING_DESIGNATION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.JoiningDesignationId) ? objEmployeeModel.JoiningDesignationId : null;
            objOracleCommand.Parameters.Add("P_PRESENT_DESIGNATION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.PresentDesignationId) ? objEmployeeModel.PresentDesignationId : null;
            objOracleCommand.Parameters.Add("P_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.UnitId) ? objEmployeeModel.UnitId : null;
            objOracleCommand.Parameters.Add("P_DEPARTMENT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.DepartmentId) ? objEmployeeModel.DepartmentId : null;
            objOracleCommand.Parameters.Add("P_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.SectionId) ? objEmployeeModel.SectionId : null;
            objOracleCommand.Parameters.Add("P_SUB_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.SubSectionId) ? objEmployeeModel.SubSectionId : null;
            objOracleCommand.Parameters.Add("P_GRADE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.GradeId) ? objEmployeeModel.GradeId : null;
            objOracleCommand.Parameters.Add("P_JOINING_SALARY", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.JoiningSalary) ? objEmployeeModel.JoiningSalary : null;
            objOracleCommand.Parameters.Add("P_FIRST_SALARY", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.FirstSalary) ? objEmployeeModel.FirstSalary : null;
            objOracleCommand.Parameters.Add("P_GROSS_SALARY", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.GrossSalary) ? objEmployeeModel.GrossSalary : null;
            objOracleCommand.Parameters.Add("P_ACCOUNT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.AccountNo) ? objEmployeeModel.AccountNo : null;
            objOracleCommand.Parameters.Add("P_APPROVED_BY_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ApprovedById) ? objEmployeeModel.ApprovedById : null;
            objOracleCommand.Parameters.Add("P_SUPERVISOR_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.SuperVisorId) ? objEmployeeModel.SuperVisorId : null;
            objOracleCommand.Parameters.Add("P_REFERENCE_EMPLOYEE_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ReferenceBy) ? objEmployeeModel.ReferenceBy : null;
            objOracleCommand.Parameters.Add("P_RESIGN_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ResignDate) ? objEmployeeModel.ResignDate : null;
            objOracleCommand.Parameters.Add("P_EMPLOYEE_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeTypeId) ? objEmployeeModel.EmployeeTypeId : null;
            objOracleCommand.Parameters.Add("P_JOB_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.JobTypeId) ? objEmployeeModel.JobTypeId : null;
            objOracleCommand.Parameters.Add("P_PAYMENT_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.PaymentTypeId) ? objEmployeeModel.PaymentTypeId : null;
            objOracleCommand.Parameters.Add("P_ACTIVE_YN", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.ActiveYN ?? null;
            objOracleCommand.Parameters.Add("P_PROBATION_PERIOD_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ProbationPeriodId) ? objEmployeeModel.ProbationPeriodId : null;
            objOracleCommand.Parameters.Add("P_SHIFT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ShiftId) ? objEmployeeModel.ShiftId : null;
            objOracleCommand.Parameters.Add("P_JOB_LOCATION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.JobLocationId) ? objEmployeeModel.JobLocationId : null;
            objOracleCommand.Parameters.Add("P_LOCAL_GUARDIAN_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.LocalGuardianName) ? objEmployeeModel.LocalGuardianName : null;
            objOracleCommand.Parameters.Add("P_LOCAL_GUARDIAN_NID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.LocalGuardianNID) ? objEmployeeModel.LocalGuardianNID : null;
            objOracleCommand.Parameters.Add("P_LOCAL_GUARDIAN_CONTACT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.LocalGuardianPhone) ? objEmployeeModel.LocalGuardianPhone : null;
            objOracleCommand.Parameters.Add("P_TAX_FILE_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.TaxFileNo) ? objEmployeeModel.TaxFileNo : null;
            objOracleCommand.Parameters.Add("P_PARENTS_CONTACT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ParentContactNo) ? objEmployeeModel.ParentContactNo : null;
            objOracleCommand.Parameters.Add("P_RETIREMENT_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.RetirementDate) ? objEmployeeModel.RetirementDate : null;
            objOracleCommand.Parameters.Add("P_WEEKLY_HOLIDAY_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.HolidayId) ? objEmployeeModel.HolidayId : null;
            objOracleCommand.Parameters.Add("P_BKAS_ACCOUNT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.BkashAccountNo) ? objEmployeeModel.BkashAccountNo : null;
            objOracleCommand.Parameters.Add("P_TRANSFER_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.TransferDate) ? objEmployeeModel.TransferDate : null;
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;

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



        public string SaveEmployeePreviousJobInformation(EmployeeModel objEmployeeModel)
        {


            string strMsg = "";
            int j = 0;


            var x = objEmployeeModel.OrganizationName.First();

            if (!string.IsNullOrWhiteSpace(x))
            {
                j = objEmployeeModel.OrganizationName.Count();
            }


            for (int i = 0; i < j; i++)
            {


                //var arrayTranId = objEmployeeModel.TranId[i];
                var arrayOrganizationName = objEmployeeModel.OrganizationName[i];
                var arrayJoiningDate = objEmployeeModel.PreviousJobJoiningDate[i];
                var arrayDesignation = objEmployeeModel.PreviousJobDesignationId[i];
                var arraySalary = objEmployeeModel.PreviousJobSalary[i];
                var arrayResignDate = objEmployeeModel.PreviousJobResignDate[i];


                if (!string.IsNullOrWhiteSpace(arrayOrganizationName))
                {
                    OracleTransaction objOracleTransaction = null;
                    OracleCommand objOracleCommand = new OracleCommand("PRO_EMP_PRE_JOB_HISTORY_SAVE");
                    objOracleCommand.CommandType = CommandType.StoredProcedure;

                    //objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(arrayTranId) ? arrayTranId : null;
                    objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId) ? objEmployeeModel.EmployeeId : null;
                    objOracleCommand.Parameters.Add("P_ORGANIZATION_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(arrayOrganizationName) ? arrayOrganizationName : null;
                    objOracleCommand.Parameters.Add("P_GROSS_SALARY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(arraySalary) ? arraySalary : null;
                    objOracleCommand.Parameters.Add("P_JOINING_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(arrayJoiningDate) ? arrayJoiningDate : null;
                    objOracleCommand.Parameters.Add("P_RESIGN_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(arrayResignDate) ? arrayResignDate : null;
                    objOracleCommand.Parameters.Add("P_DESIGNATION_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(arrayDesignation) ? arrayDesignation : null;
                    objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
                    objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;



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
                }

            }

            return strMsg;

        }
        public string SaveEmployeeEducationInformation(EmployeeModel objEmployeeModel)
        {


            string strMsg = "";


            int j = 0;
            var x = objEmployeeModel.DegreeId.First();

            if (!string.IsNullOrWhiteSpace(x))
            {
                j = objEmployeeModel.DegreeId.Count();
            }


            for (int i = 0; i < j; i++)
            {
                var arrayInstituteName = objEmployeeModel.InstituteName[i];
                var arrayDegreeId = objEmployeeModel.DegreeId[i];
                var arrayMajorSubjectId = objEmployeeModel.MajorSubjectId[i];
                var arrayYear = objEmployeeModel.Year[i];
                var arrayCGPA = objEmployeeModel.CGPA[i];

                if (!string.IsNullOrWhiteSpace(arrayDegreeId))
                {
                    OracleTransaction objOracleTransaction = null;
                    OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_EDUCATION_SAVE");
                    objOracleCommand.CommandType = CommandType.StoredProcedure;


                    objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId) ? objEmployeeModel.EmployeeId : null;
                    objOracleCommand.Parameters.Add("P_INSTITUTE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(arrayInstituteName) ? arrayInstituteName : null;
                    objOracleCommand.Parameters.Add("P_DEGREE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(arrayDegreeId) ? arrayDegreeId : null;
                    objOracleCommand.Parameters.Add("P_MAJOR_SUBJECT_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(arrayMajorSubjectId) ? arrayMajorSubjectId : null;
                    objOracleCommand.Parameters.Add("P_CGPA", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(arrayCGPA) ? arrayCGPA : null;
                    objOracleCommand.Parameters.Add("P_PASSING_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(arrayYear) ? arrayYear : null;
                    objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
                    objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;



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
                }


            }


            return strMsg;

        }
        public string SaveEmployeeImage(EmployeeModel objEmployeeModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_IMAGE_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId) ? objEmployeeModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ImageFileName) ? objEmployeeModel.ImageFileName : null;


            if (!string.IsNullOrWhiteSpace(objEmployeeModel.ImageFileSize))
            {
                byte[] array = System.Convert.FromBase64String(objEmployeeModel.ImageFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }


            objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ImageFileExtension) ? objEmployeeModel.ImageFileExtension : null;
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;

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
        public string SaveEmployeeSignature(EmployeeModel objEmployeeModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_SIGNATURE_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId) ? objEmployeeModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.SignatureFileName) ? objEmployeeModel.SignatureFileName : null;

            if (objEmployeeModel.SignatureFileSize != "")
            {
                byte[] array = System.Convert.FromBase64String(objEmployeeModel.SignatureFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }


            objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.SignatureFileExtension) ? objEmployeeModel.SignatureFileExtension : null;
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;

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
        public string SaveEmployeeCv(EmployeeModel objEmployeeModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_CV_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId) ? objEmployeeModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.CVFileName) ? objEmployeeModel.CVFileName : null;



            if (objEmployeeModel.CVFileSize != "")
            {
                byte[] array = System.Convert.FromBase64String(objEmployeeModel.CVFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }


            objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.CVFileExtension) ? objEmployeeModel.CVFileExtension : null;
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;

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
        public string SaveEmployeeExperienceCertificate(EmployeeModel objEmployeeModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_EXPERI_CERTIFICATE_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId) ? objEmployeeModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ExpCertifiateFileName) ? objEmployeeModel.ExpCertifiateFileName : null;




            if (objEmployeeModel.ExpCertificateFileSize != "")
            {
                byte[] array = System.Convert.FromBase64String(objEmployeeModel.ExpCertificateFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }


            objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ExpCertificateFileExtension) ? objEmployeeModel.ExpCertificateFileExtension : null;
            objOracleCommand.Parameters.Add("P_RESIGN_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.PrevJobResignDate) ? objEmployeeModel.PrevJobResignDate : null;
            objOracleCommand.Parameters.Add("P_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
            objOracleCommand.Parameters.Add("P_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("P_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;
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
        public string SaveEmployeeClearenceCertificate(EmployeeModel objEmployeeModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_CLEARENCE_CERTIFICATE_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId) ? objEmployeeModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ClrCertifiateFileName) ? objEmployeeModel.ClrCertifiateFileName : null;




            if (objEmployeeModel.ClrCertificateFileSize != "")
            {
                byte[] array = System.Convert.FromBase64String(objEmployeeModel.ClrCertificateFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }


            objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.ClrCertificateFileExtension) ? objEmployeeModel.ClrCertificateFileExtension : null;
            objOracleCommand.Parameters.Add("P_RESIGN_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.PrevJobResignDate) ? objEmployeeModel.PrevJobResignDate : null;
            objOracleCommand.Parameters.Add("P_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
            objOracleCommand.Parameters.Add("P_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("P_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;



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
        public string SaveEmployeeEducationCertificate(EmployeeModel objEmployeeModel)
        {


            string strMsg = "";

            
            OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_CERTIFICATE_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId) ? objEmployeeModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.CertifiateFileName) ? objEmployeeModel.CertifiateFileName : null;




            if (objEmployeeModel.CertificateFileSize != "")
            {
                byte[] array = System.Convert.FromBase64String(objEmployeeModel.CertificateFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }


            objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.CertificateFileExtension) ? objEmployeeModel.CertificateFileExtension : null;
            objOracleCommand.Parameters.Add("P_PASSING_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objEmployeeModel.PassingYear) ? objEmployeeModel.PassingYear : null;
            objOracleCommand.Parameters.Add("P_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
            objOracleCommand.Parameters.Add("P_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("P_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;
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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }



            return strMsg;

        }
        public EmployeeModel GetJobConfirmationDate(EmployeeModel objEmployeeModel)
        {

            string sql = "", sql1 = "";
            sql = "SELECT 'Y' from vew_search_djr where employee_id = '" + objEmployeeModel.EmployeeId + "'  AND joining_date = to_Date('" + objEmployeeModel.JoiningDate + "', 'dd/mm/yyyy') AND head_office_id = '" + objEmployeeModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeModel.BranchOfficeId + "' and JOB_CONFIRMATION_DATE IS NOT NULL ";


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
                        objEmployeeModel.Msg = objDataReader.GetString(0);

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


            if (objEmployeeModel.Msg != "Y")
            {

                sql1 = "SELECT   NVL (TO_CHAR (to_Date(ADD_MONTHS (TO_DATE ('" + objEmployeeModel.JoiningDate + "', 'dd/mm/yyyy'), 6)) ,'dd/mm/yyyy'), ' ') JOB_CONFIRMATION_DATE from dual ";


                OracleCommand objCommand1 = new OracleCommand(sql1);
                OracleDataReader objDataReader1;

                using (OracleConnection strConn = GetConnection())
                {

                    objCommand1.Connection = strConn;
                    strConn.Open();
                    objDataReader1 = objCommand1.ExecuteReader();
                    try
                    {
                        while (objDataReader1.Read())
                        {


                            objEmployeeModel.JobConfirmationDate = objDataReader1.GetString(0);


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

            }

            return objEmployeeModel;

        }
        public EmployeeModel GetRetirementDate(EmployeeModel objEmployeeModel)
        {

            string sql = "", sql1 = "";
            sql = "SELECT 'Y' from vew_search_djr where employee_id = '" + objEmployeeModel.EmployeeId + "'  AND date_of_birth = to_Date('" + objEmployeeModel.DateOfBirth + "', 'dd/mm/yyyy') AND head_office_id = '" + objEmployeeModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeModel.BranchOfficeId + "' and RETIREMENT_DATE IS NOT NULL ";


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
                        objEmployeeModel.Msg = objDataReader.GetString(0);
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


            if (objEmployeeModel.Msg != "Y")
            {

                sql1 = "SELECT   NVL (TO_CHAR (to_Date(ADD_MONTHS (TO_DATE ('" + objEmployeeModel.DateOfBirth + "', 'dd/mm/yyyy'), 60*12)) ,'dd/mm/yyyy'), ' ') RETIREMENT_DATE from dual ";


                OracleCommand objCommand1 = new OracleCommand(sql1);
                OracleDataReader objDataReader1;

                using (OracleConnection strConn = GetConnection())
                {

                    objCommand1.Connection = strConn;
                    strConn.Open();
                    objDataReader1 = objCommand1.ExecuteReader();
                    try
                    {
                        while (objDataReader1.Read())
                        {
                            objEmployeeModel.RetirementDate = objDataReader1.GetString(0);
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

            }

            return objEmployeeModel;

        }
        public DataTable GetEmployeeId(EmployeeDataById objEmployeeDataById)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "EMPLOYEE_ID " +
                   " FROM VEW_EMP_RECORD_SEARCH WHERE HEAD_OFFICE_ID = '" + objEmployeeDataById.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objEmployeeDataById.BranchOfficeId + "' AND ACTIVE_YN = 'Y' ";

            /* if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchEmployeeId))
             {
                 sql = sql + "AND (LOWER(EMPLOYEE_ID) LIKE LOWER( '%" + objEmployeeModel.SearchEmployeeId + "%')  OR UPPER (EMPLOYEE_ID)LIKE UPPER('%" + objEmployeeModel.SearchEmployeeId + "%') )";
             }
             */

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


        //mezba 1
        public DataTable LoadEmployeeData(EmployeeModel objEmployeeModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "rownum SL, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                   "DESIGNATION_NAME, " +
                   "DEPARTMENT_NAME, " +
                   "UNIT_NAME, " +
                   "SECTION_NAME, " +
                   "SUB_SECTION_NAME, " +
                   "GRADE_NO, " +
                   "ACTIVE_YN, " +
                   "ACTIVE_STATUS, " +
                   "EMPLOYEE_IMAGE_NAME, " +
                   "EMPLOYEE_IMAGE_SIZE " +
                   " FROM VEW_EMP_RECORD_SEARCH WHERE HEAD_OFFICE_ID = '" + objEmployeeModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objEmployeeModel.BranchOfficeId + "' AND ACTIVE_YN = '" + objEmployeeModel.SearchInactiveYN + "' ";



            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchEmployeeId))
            {

                sql = sql + "AND EMPLOYEE_ID = '" + objEmployeeModel.SearchEmployeeId.Trim() + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchEmployeeName))
            {

                sql = sql + "AND (LOWER(EMPLOYEE_NAME) LIKE LOWER( '%" + objEmployeeModel.SearchEmployeeName.Trim() + "%')  OR UPPER (EMPLOYEE_NAME)LIKE UPPER('%" + objEmployeeModel.SearchEmployeeName.Trim() + "%') )";
            }


            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchCardNo))
            {

                sql = sql + "AND CARD_NO = '" + objEmployeeModel.SearchCardNo.Trim() + "' ";
            }


            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchUnitId))
            {

                sql = sql + "AND UNIT_ID = '" + objEmployeeModel.SearchUnitId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchDepartmentId))
            {

                sql = sql + "AND DEPARTMENT_ID = '" + objEmployeeModel.SearchDepartmentId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchSectionId))
            {

                sql = sql + "AND SECTION_ID = '" + objEmployeeModel.SearchSectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchSubSectionId))
            {

                sql = sql + "AND SUB_SECTION_ID = '" + objEmployeeModel.SearchSubSectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchPunchCode))
            {

                sql = sql + "AND PUNCH_CODE = '" + objEmployeeModel.SearchPunchCode.Trim() + "' ";
            }


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

        //mezba 2
        public DataTable LoadEmployeeDataForIdCard(EmployeeIdCardModel objEmployeeIdCardModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "rownum SL, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                   "DESIGNATION_NAME, " +
                   "DEPARTMENT_NAME, " +
                   "UNIT_NAME, " +
                   "SECTION_NAME, " +
                   "SUB_SECTION_NAME, " +
                   "GRADE_NO, " +
                   "ACTIVE_YN, " +
                   "ACTIVE_STATUS, " +
                   "EMPLOYEE_IMAGE_NAME, " +
                   "EMPLOYEE_IMAGE_SIZE " +
                   " FROM VEW_EMP_RECORD_SEARCH WHERE HEAD_OFFICE_ID = '" + objEmployeeIdCardModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objEmployeeIdCardModel.BranchOfficeId + "' AND ACTIVE_YN = 'Y' ";



            if (!string.IsNullOrWhiteSpace(objEmployeeIdCardModel.EmployeeId))
            {

                sql = sql + "AND EMPLOYEE_ID = '" + objEmployeeIdCardModel.EmployeeId.Trim() + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeIdCardModel.EmployeeName))
            {

                sql = sql + "AND (LOWER(EMPLOYEE_NAME) LIKE LOWER( '%" + objEmployeeIdCardModel.EmployeeName.Trim() + "%')  OR UPPER (EMPLOYEE_NAME)LIKE UPPER('%" + objEmployeeIdCardModel.EmployeeName.Trim() + "%') )";
            }


            if (!string.IsNullOrWhiteSpace(objEmployeeIdCardModel.CardNo))
            {

                sql = sql + "AND CARD_NO = '" + objEmployeeIdCardModel.CardNo.Trim() + "' ";
            }


            if (!string.IsNullOrWhiteSpace(objEmployeeIdCardModel.UnitId))
            {

                sql = sql + "AND UNIT_ID = '" + objEmployeeIdCardModel.UnitId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeIdCardModel.DepartmentId))
            {

                sql = sql + "AND DEPARTMENT_ID = '" + objEmployeeIdCardModel.DepartmentId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeIdCardModel.SectionId))
            {

                sql = sql + "AND SECTION_ID = '" + objEmployeeIdCardModel.SectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeIdCardModel.SubSectionId))
            {

                sql = sql + "AND SUB_SECTION_ID = '" + objEmployeeIdCardModel.SubSectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeIdCardModel.PunchCode))
            {

                sql = sql + "AND PUNCH_CODE = '" + objEmployeeIdCardModel.PunchCode.Trim() + "' ";
            }


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

        //mezba 3
        public DataTable LoadEmployeeImageData(EmployeeImageModel objEmployeeImageModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "rownum SL, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                   "DESIGNATION_NAME, " +
                   "DEPARTMENT_NAME, " +
                   "UNIT_NAME, " +
                   "SECTION_NAME, " +
                   "SUB_SECTION_NAME, " +
                   "GRADE_NO, " +
                   "ACTIVE_YN, " +
                   "ACTIVE_STATUS, " +
                   "EMPLOYEE_IMAGE_NAME, " +
                   "EMPLOYEE_IMAGE_SIZE " +
                   " FROM VEW_EMP_RECORD_SEARCH WHERE HEAD_OFFICE_ID = '" + objEmployeeImageModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objEmployeeImageModel.BranchOfficeId + "' AND ACTIVE_YN = '" + objEmployeeImageModel.Status + "' ";



            sql = sql + "AND EMPLOYEE_IMAGE_NAME IS NOT NULL ";



            if (!string.IsNullOrWhiteSpace(objEmployeeImageModel.EmployeeId))
            {

                sql = sql + "AND EMPLOYEE_ID = '" + objEmployeeImageModel.EmployeeId.Trim() + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeImageModel.EmployeeName))
            {

                sql = sql + "AND (LOWER(EMPLOYEE_NAME) LIKE LOWER( '%" + objEmployeeImageModel.EmployeeName.Trim() + "%')  OR UPPER (EMPLOYEE_NAME)LIKE UPPER('%" + objEmployeeImageModel.EmployeeName.Trim() + "%') )";
            }


            if (!string.IsNullOrWhiteSpace(objEmployeeImageModel.CardNo))
            {

                sql = sql + "AND CARD_NO = '" + objEmployeeImageModel.CardNo + "' ";
            }


            if (!string.IsNullOrWhiteSpace(objEmployeeImageModel.UnitId))
            {

                sql = sql + "AND UNIT_ID = '" + objEmployeeImageModel.UnitId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeImageModel.DepartmentId))
            {

                sql = sql + "AND DEPARTMENT_ID = '" + objEmployeeImageModel.DepartmentId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeImageModel.SectionId))
            {

                sql = sql + "AND SECTION_ID = '" + objEmployeeImageModel.SectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeImageModel.SubSectionId))
            {

                sql = sql + "AND SUB_SECTION_ID = '" + objEmployeeImageModel.SubSectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeImageModel.PunchCode))
            {

                sql = sql + "AND PUNCH_CODE = '" + objEmployeeImageModel.PunchCode + "' ";
            }


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

        public DataTable LoadEmployePreviousJobData(EmployeeModel objEmployeeModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "EMPLOYEE_ID, " +
                  "ORGANIZATION_NAME, " +
                  "TO_CHAR(JOINING_DATE_PRE,'dd/mm/yyyy')JOINING_DATE, " +
                  "DESIGNATION_ID, " +
                  "GROSS_SALARY, " +
                  "TO_CHAR(RESIGN_DATE,'dd/mm/yyyy')RESIGN_DATE, " +
                  //"TRAN_ID, " +
                  "CERTIFICATE_FILE_NAME, " +
                  "CLEARANCE_FILE_NAME " +
                  " FROM VEW_EMPLOYEE_PRE_JOB_HISTORY where head_office_id = '" + objEmployeeModel.HeadOfficeId + "' and branch_office_id = '" + objEmployeeModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId))
            {

                sql = sql + "and employee_id = '" + objEmployeeModel.EmployeeId + "' ";
            }


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
        public DataTable LoadEmployeEducationData(EmployeeModel objEmployeeModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "EMPLOYEE_ID, " +
                  "INSTITUTE_NAME, " +
                  "DEGREE_ID, " +
                  "MAJOR_SUBJECT_ID, " +
                  "PASSING_YEAR, " +
                  "CGPA, " +
                  "FILE_NAME " +
                  " FROM VEW_EMPLOYEE_EDUCATION_HISTORY where head_office_id = '" + objEmployeeModel.HeadOfficeId + "' and branch_office_id = '" + objEmployeeModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId))
            {

                sql = sql + "and employee_id = '" + objEmployeeModel.EmployeeId + "' ";
            }


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
        public DataTable LoadEmployeeDataForEdit(EmployeeModel objEmployeeModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "rownum SL, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                   "DESIGNATION_NAME, " +
                   "DEPARTMENT_NAME, " +
                   "UNIT_NAME, " +
                   "SECTION_NAME, " +
                   "SUB_SECTION_NAME, " +
                   "GRADE_NO, " +
                   "ACTIVE_YN, " +
                   "ACTIVE_STATUS, " +
                   "EMPLOYEE_IMAGE_NAME, " +
                   "EMPLOYEE_IMAGE_SIZE " +
                   " FROM VEW_EMP_RECORD_SEARCH where head_office_id = '" + objEmployeeModel.HeadOfficeId + "' and branch_office_id = '" + objEmployeeModel.BranchOfficeId + "' and active_yn= 'Y' ";


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
        public EmployeeModel SearchEmployeeInformation(EmployeeModel objEmployeeModel)
        {

            string sql = "";
            sql = "SELECT " +
               "TO_CHAR (NVL (CARD_NO,'0')), " +
               "TO_CHAR (NVL (EMPLOYEE_NAME,'N/A')), " +
               "TO_CHAR (NVL (EMPLOYEE_NAME_BANGLA,'N/A')), " +
               "TO_CHAR (NVL (FATHER_NAME,'N/A')), " +
               "TO_CHAR (NVL (MOTHER_NAME,'N/A')), " +
               "NVL (TO_CHAR (DATE_OF_BIRTH, 'dd/mm/yyyy'), ' '), " +
               "TO_CHAR (NVL (PUNCH_CODE, '0')), " +
               "TO_CHAR (NVL (BLOOD_GROUP_ID, '0')), " +
               "TO_CHAR (NVL (GENDER_ID, '0')), " +
               "TO_CHAR (NVL (MARITAL_STATUS_ID, '0')), " +
               "TO_CHAR (NVL (DISTRICT_ID, '0')), " +
               "TO_CHAR (NVL (DIVISION_ID, '0')), " +
               "TO_CHAR (NVL (RELIGION_ID, '0')), " +
               "TO_CHAR (NVL (COUNTRY_ID, '0')), " +
               "TO_CHAR (NVL (NID_NO,'0')), " +
               "TO_CHAR (NVL (TIN_NO,'0')), " +
               "TO_CHAR (NVL (SPOUSE_NAME,'N/A')),  " +
               "TO_CHAR (NVL (PRESENT_ADDRESS,'N/A')), " +
               "TO_CHAR (NVL (PERMANENT_ADDRESS,'N/A')), " +
               "TO_CHAR (NVL (MAIL_ADDRESS,'no@email.com')), " +
               "TO_CHAR (NVL (CONTACT_NO,'0')), " +
               "TO_CHAR (NVL (EMERGENCY_CONTACT_NO,'0')), " +
               "TO_CHAR (NVL (PASSPORT_NO,'0')), " +
               "TO_CHAR (NVL (DRIVING_LICENSE_NO,'0')), " +
               "TO_CHAR (NVL (OCCURENCE_TYPE_ID, '0')), " +
               "NVL (TO_CHAR (JOINING_DATE, 'dd/mm/yyyy'), ' '), " +
               "NVL (TO_CHAR (JOB_CONFIRMATION_DATE, 'dd/mm/yyyy'), ' '), " +
               "TO_CHAR (NVL (JOINING_DESIGNATION_ID, '0')), " +
               "TO_CHAR (NVL (PRESENT_DESIGNATION_ID, '0')), " +
               "TO_CHAR (NVL (UNIT_ID, '0')), " +
               "TO_CHAR (NVL (DEPARTMENT_ID, '0')), " +
               "TO_CHAR (NVL (SECTION_ID, '0')), " +
               "TO_CHAR (NVL (GRADE_ID, '0')), " +
               "TO_CHAR (NVL (JOINING_SALARY,'0')), " +
               "TO_CHAR (NVL (FIRST_SALARY,'0')), " +
               "TO_CHAR (NVL (GROSS_SALARY,'0')), " +
               "TO_CHAR (NVL (ACCOUNT_NO,'0')), " +
               "TO_CHAR (NVL (APPROVED_EMPLOYEE_ID,'0')), " +
               "TO_CHAR (NVL (SUPERVISOR_EMPLOYEE_ID,'0')), " +
               "TO_CHAR (NVL (REFERENCE_EMPLOYEE_NAME,'N/A')), " +
               "NVL (TO_CHAR (RESIGN_DATE, 'dd/mm/yyyy'), ' '), " +
               "TO_CHAR (NVL (ACTIVE_YN, '0')), " +
               "TO_CHAR (NVL (EMPLOYEE_TYPE_ID, '0')), " +
               "TO_CHAR (NVL (JOB_TYPE_ID, '0')), " +
               "TO_CHAR (NVL (PAYMENT_TYPE_ID, '0')), " +
               "TO_CHAR (NVL (PROBATION_PERIOD_ID, '0')), " +
               "TO_CHAR (NVL (SHIFT_ID, '0')), " +
               "TO_CHAR (NVL (JOB_LOCATION_ID, '0')), " +
               "TO_CHAR (NVL (LOCAL_GUARDIAN_NAME,'N/A')), " +
               "TO_CHAR (NVL (LOCAL_GUARDIAN_NID,'0')), " +
               "TO_CHAR (NVL (LOCAL_GUARDIAN_CONTACT_NO,'0')), " +
               "TO_CHAR (NVL (SUB_SECTION_ID, '0')), " +
               "NVL (TO_CHAR (RETIREMENT_DATE, 'dd/mm/yyyy'), ' '), " +
               "TO_CHAR (NVL (WEEKLY_HOLIDAY_ID, '0')), " +
               "TO_CHAR (NVL (BKAS_ACCOUNT_NO, '0')), " +
               "TO_CHAR (NVL (PARENTS_CONTACT_NO,'N/A')), " +
               "TO_CHAR (NVL (TAX_FILE_NO,'N/A')), " +
               "NVL (TO_CHAR (TRANSFER_DATE, 'dd/mm/yyyy'), ' ') " +
               "FROM VEW_EMPLOYEE_INFORMATION where employee_id = '" + objEmployeeModel.EmployeeId + "' and head_office_id = '" + objEmployeeModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeModel.BranchOfficeId + "' ";


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

                        objEmployeeModel.CardNo = objDataReader.GetString(0);
                        objEmployeeModel.EmployeeName = objDataReader.GetString(1);
                        objEmployeeModel.EmployeeNameBangla = objDataReader.GetString(2);
                        objEmployeeModel.FatherName = objDataReader.GetString(3);
                        objEmployeeModel.MotherName = objDataReader.GetString(4);
                        objEmployeeModel.DateOfBirth = objDataReader.GetString(5);
                        objEmployeeModel.PunchCode = objDataReader.GetString(6);
                        objEmployeeModel.BloodGroupId = objDataReader.GetString(7);
                        objEmployeeModel.GenderId = objDataReader.GetString(8);
                        objEmployeeModel.MaritalStatusId = objDataReader.GetString(9);
                        objEmployeeModel.DistrictId = objDataReader.GetString(10);
                        objEmployeeModel.DivisionId = objDataReader.GetString(11);
                        objEmployeeModel.ReligionId = objDataReader.GetString(12);
                        objEmployeeModel.CountryId = objDataReader.GetString(13);
                        objEmployeeModel.NIDNo = objDataReader.GetString(14);
                        objEmployeeModel.TINNo = objDataReader.GetString(15);
                        objEmployeeModel.SpouseName = objDataReader.GetString(16);
                        objEmployeeModel.PresentAddress = objDataReader.GetString(17);
                        objEmployeeModel.PermanentAddress = objDataReader.GetString(18);
                        objEmployeeModel.EmailAddress = objDataReader.GetString(19);
                        objEmployeeModel.ContactNo = objDataReader.GetString(20);
                        objEmployeeModel.EmergencyContactNo = objDataReader.GetString(21);
                        objEmployeeModel.PassportNo = objDataReader.GetString(22);
                        objEmployeeModel.DrivingLicenseNo = objDataReader.GetString(23);
                        objEmployeeModel.EmployeementType = objDataReader.GetString(24);
                        objEmployeeModel.JoiningDate = objDataReader.GetString(25);
                        objEmployeeModel.JobConfirmationDate = objDataReader.GetString(26);
                        objEmployeeModel.JoiningDesignationId = objDataReader.GetString(27);
                        objEmployeeModel.PresentDesignationId = objDataReader.GetString(28);
                        objEmployeeModel.UnitId = objDataReader.GetString(29);
                        objEmployeeModel.DepartmentId = objDataReader.GetString(30);
                        objEmployeeModel.SectionId = objDataReader.GetString(31);
                        objEmployeeModel.GradeId = objDataReader.GetString(32);
                        objEmployeeModel.JoiningSalary = objDataReader.GetString(33);
                        objEmployeeModel.FirstSalary = objDataReader.GetString(34);
                        objEmployeeModel.GrossSalary = objDataReader.GetString(35);
                        objEmployeeModel.AccountNo = objDataReader.GetString(36);
                        objEmployeeModel.ApprovedById = objDataReader.GetString(37);
                        objEmployeeModel.SuperVisorId = objDataReader.GetString(38);
                        objEmployeeModel.ReferenceBy = objDataReader.GetString(39);
                        objEmployeeModel.ResignDate = objDataReader.GetString(40);


                        var activeStatus = objDataReader.GetString(41);

                        if (activeStatus == "Y")
                        {
                            objEmployeeModel.ActiveYN = "true";
                        }
                        else
                        {
                            objEmployeeModel.ActiveYN = "false";
                        }


                        objEmployeeModel.EmployeeTypeId = objDataReader.GetString(42);
                        objEmployeeModel.JobTypeId = objDataReader.GetString(43);
                        objEmployeeModel.PaymentTypeId = objDataReader.GetString(44);
                        objEmployeeModel.ProbationPeriodId = objDataReader.GetString(45);
                        objEmployeeModel.ShiftId = objDataReader.GetString(46);
                        objEmployeeModel.JobLocationId = objDataReader.GetString(47);
                        objEmployeeModel.LocalGuardianName = objDataReader.GetString(48);
                        objEmployeeModel.LocalGuardianNID = objDataReader.GetString(49);
                        objEmployeeModel.LocalGuardianPhone = objDataReader.GetString(50);
                        objEmployeeModel.SubSectionId = objDataReader.GetString(51);
                        objEmployeeModel.RetirementDate = objDataReader.GetString(52);
                        objEmployeeModel.HolidayId = objDataReader.GetString(53);
                        objEmployeeModel.BkashAccountNo = objDataReader.GetString(54);
                        objEmployeeModel.ParentContactNo = objDataReader.GetString(55);
                        objEmployeeModel.TaxFileNo = objDataReader.GetString(56);
                        objEmployeeModel.TransferDate = objDataReader.GetString(57);
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

            return objEmployeeModel;

        }



        public EmployeeModel SearchEmployeePhoto(EmployeeModel objEmployeeModel)
        {

            string sql = "";
            sql = "SELECT " +
               "FILE_NAME, " +
               "FILE_SIZE " +
               "FROM EMPLOYEE_IMAGE where employee_id = '" + objEmployeeModel.EmployeeId + "' and head_office_id = '" + objEmployeeModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeModel.BranchOfficeId + "' ";


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

                        if (objDataReader.GetString(0) != null)
                        {
                            objEmployeeModel.EditImageFileByte = (byte[])objDataReader.GetValue(1);
                            objEmployeeModel.EditImageFileNameBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(objEmployeeModel.EditImageFileByte);
                        }

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

            return objEmployeeModel;

        }
        public EmployeeModel SearchEmployeeSignature(EmployeeModel objEmployeeModel)
        {

            string sql = "";
            sql = "SELECT " +
               "FILE_NAME, " +
               "FILE_SIZE " +
               "FROM EMPLOYEE_SIGNATURE where employee_id = '" + objEmployeeModel.EmployeeId + "' and head_office_id = '" + objEmployeeModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeModel.BranchOfficeId + "' ";


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

                        if (objDataReader.GetString(0) != null)
                        {
                            objEmployeeModel.EditSignatureFileByte = (byte[])objDataReader.GetValue(1);
                            objEmployeeModel.EditSignatureFileNameBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(objEmployeeModel.EditSignatureFileByte);
                        }

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

            return objEmployeeModel;

        }
        public EmployeeModel SearchEmployeeCv(EmployeeModel objEmployeeModel)
        {

            string sql = "";
            sql = "SELECT " +
               "FILE_NAME, " +
               "FILE_SIZE " +
               "FROM EMPLOYEE_CV where employee_id = '" + objEmployeeModel.EmployeeId + "' and head_office_id = '" + objEmployeeModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeModel.BranchOfficeId + "' ";


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
                        objEmployeeModel.CVFileName = objDataReader.GetString(0);
                        if (objDataReader.GetString(0) != null)
                        {
                            objEmployeeModel.EditCVFileByte = (byte[])objDataReader.GetValue(1);
                        }


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

            return objEmployeeModel;

        }
        public EmployeeModel SearchEducationCertificate(EmployeeModel objEmployeeModel)
        {

            string sql = "";
            sql = "SELECT " +
               "FILE_NAME, " +
               "FILE_SIZE " +
               "FROM EMPLOYEE_CERTIFICATE where employee_id = '" + objEmployeeModel.EmployeeId + "' and PASSING_YEAR = '" + objEmployeeModel.EditGridYear + "' and head_office_id = '" + objEmployeeModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeModel.BranchOfficeId + "' ";


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
                        objEmployeeModel.CertifiateFileName = objDataReader.GetString(0);
                        if (objDataReader.GetString(0) != null)
                        {
                            objEmployeeModel.EditEducationFileByte = (byte[])objDataReader.GetValue(1);
                        }


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

            return objEmployeeModel;

        }
        public EmployeeModel SearchJobCertificate(EmployeeModel objEmployeeModel)
        {

            string sql = "";
            sql = "SELECT " +
               "FILE_NAME, " +
               "FILE_SIZE " +
               "FROM EMP_PRE_JOB_EXPERI_CERTIFICATE where employee_id = '" + objEmployeeModel.EmployeeId + "' and resign_date = TO_DATE('" + objEmployeeModel.EditGridResignDate + "', 'dd/MM/yyyy') and head_office_id = '" + objEmployeeModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeModel.BranchOfficeId + "' ";


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
                        objEmployeeModel.ExpCertifiateFileName = objDataReader.GetString(0);
                        if (objDataReader.GetString(0) != null)
                        {
                            objEmployeeModel.EditJobCertificateFileByte = (byte[])objDataReader.GetValue(1);
                        }


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

            return objEmployeeModel;

        }
        public EmployeeModel SearchJobClearance(EmployeeModel objEmployeeModel)
        {

            string sql = "";
            sql = "SELECT " +
               "FILE_NAME, " +
               "FILE_SIZE " +
               "FROM EMP_PRE_JOB_CLEAR_CERTIFICATE where employee_id = '" + objEmployeeModel.EmployeeId + "' and resign_date = TO_DATE('" + objEmployeeModel.EditGridResignDate + "', 'dd/MM/yyyy') and head_office_id = '" + objEmployeeModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeModel.BranchOfficeId + "' ";


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
                        objEmployeeModel.ClrCertifiateFileName = objDataReader.GetString(0);
                        if (objDataReader.GetString(0) != null)
                        {
                            objEmployeeModel.EditJobClearanceFileByte = (byte[])objDataReader.GetValue(1);
                        }


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

            return objEmployeeModel;

        }

        public string DeleteEmployeeEducation(EmployeeModel objEmployeeModel)
        {

            string strMsg = "";

            if (!string.IsNullOrWhiteSpace(objEmployeeModel.DeleteEmployeeId) && (!string.IsNullOrWhiteSpace(objEmployeeModel.DeleteEducationYear)))
            {

                string[] DeleteEducationYearArray = objEmployeeModel.DeleteEducationYear.Split(',');

                int x = DeleteEducationYearArray.Count();
                for (int i = 0; i < x; i++)
                {
                    var EducationYear = DeleteEducationYearArray[i];

                   
                    OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_EDUCATION_DELETE");
                    objOracleCommand.CommandType = CommandType.StoredProcedure;

                    objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.DeleteEmployeeId ?? null;
                    objOracleCommand.Parameters.Add("P_PASSING_YEAR", OracleDbType.Varchar2, ParameterDirection.Input).Value = EducationYear ?? null;
                    objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
                    objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;

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
                            trans.Rollback();
                            throw new Exception("Error : " + ex.Message);
                            
                        }

                        finally
                        {

                            strConn.Close();
                        }

                    }

                }



            }

            return strMsg;
        }
        public string DeleteEmployeeEducationCertificate(EmployeeModel objEmployeeModel)
        {

            string strMsg = "";

            if (!string.IsNullOrWhiteSpace(objEmployeeModel.DeleteEmployeeId) && (!string.IsNullOrWhiteSpace(objEmployeeModel.DeleteEducationYear)))
            {

                string[] DeleteEducationYearArray = objEmployeeModel.DeleteEducationYear.Split(',');

                int x = DeleteEducationYearArray.Count();
                for (int i = 0; i < x; i++)
                {
                    var EducationYear = DeleteEducationYearArray[i];
                    
                    OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_EDU_CER_DELETE");
                    objOracleCommand.CommandType = CommandType.StoredProcedure;


                    objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.DeleteEmployeeId ?? null;
                    objOracleCommand.Parameters.Add("P_PASSING_YEAR", OracleDbType.Varchar2, ParameterDirection.Input).Value = EducationYear ?? null;
                    objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
                    objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;

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
                            trans.Rollback();
                            throw new Exception("Error : " + ex.Message);
                            
                        }

                        finally
                        {

                            strConn.Close();
                        }

                    }

                }



            }

            return strMsg;
        }
        public string DeleteEmployeePreviousJob(EmployeeModel objEmployeeModel)
        {

            string strMsg = "";

            if (!string.IsNullOrWhiteSpace(objEmployeeModel.DeleteEmployeeId) && (!string.IsNullOrWhiteSpace(objEmployeeModel.DeleteResignDate)))
            {

                string[] DeleteResignDateArray = objEmployeeModel.DeleteResignDate.Split(',');


                int x = DeleteResignDateArray.Count();
                for (int i = 0; i < x; i++)
                {
                    var ResignDate = DeleteResignDateArray[i];

                    
                    OracleCommand objOracleCommand = new OracleCommand("PRO_DELETE_EMP_PRE_JOB_HISTORY");
                    objOracleCommand.CommandType = CommandType.StoredProcedure;

                    objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.DeleteEmployeeId ?? null;
                    objOracleCommand.Parameters.Add("P_RESIGN_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = ResignDate ?? null;
                    objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
                    objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;

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
                            trans.Rollback();
                            throw new Exception("Error : " + ex.Message);
                            
                        }

                        finally
                        {

                            strConn.Close();
                        }

                    }

                }



            }

            return strMsg;
        }
        public string DeleteEmpPrvJobExpCertificate(EmployeeModel objEmployeeModel)
        {

            string strMsg = "";

            if (!string.IsNullOrWhiteSpace(objEmployeeModel.DeleteEmployeeId) && (!string.IsNullOrWhiteSpace(objEmployeeModel.DeleteResignDate)))
            {

                string[] DeleteResignDateArray = objEmployeeModel.DeleteResignDate.Split(',');


                int x = DeleteResignDateArray.Count();
                for (int i = 0; i < x; i++)
                {
                    var ResignDate = DeleteResignDateArray[i];

                    OracleTransaction objOracleTransaction = null;
                    OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_JOB_EXPE_DELETE");
                    objOracleCommand.CommandType = CommandType.StoredProcedure;

                    objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.DeleteEmployeeId ?? null;
                    objOracleCommand.Parameters.Add("P_RESIGN_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = ResignDate ?? null;
                    objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
                    objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;

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

                }



            }

            return strMsg;
        }
        public string DeleteEmpPrvJobClrCertificate(EmployeeModel objEmployeeModel)
        {

            string strMsg = "";

            if (!string.IsNullOrWhiteSpace(objEmployeeModel.DeleteEmployeeId) && (!string.IsNullOrWhiteSpace(objEmployeeModel.DeleteResignDate)))
            {

                string[] DeleteResignDateArray = objEmployeeModel.DeleteResignDate.Split(',');


                int x = DeleteResignDateArray.Count();
                for (int i = 0; i < x; i++)
                {
                    var ResignDate = DeleteResignDateArray[i];

                   OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_JOB_CLEAR_DELETE");
                    objOracleCommand.CommandType = CommandType.StoredProcedure;

                    objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.DeleteEmployeeId ?? null;
                    objOracleCommand.Parameters.Add("P_RESIGN_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = ResignDate ?? null;
                    objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
                    objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;

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
                            trans.Rollback();
                            throw new Exception("Error : " + ex.Message);
                            
                        }

                        finally
                        {

                            strConn.Close();
                        }

                    }

                }



            }

            return strMsg;
        }

        #endregion

        #region Employee Data Correction

        public List<EmployeeDataCorrectionModel> GetAllEmployeesForDataCorrection(EmployeeDataCorrectionModel objEmployeeDataCorrectionModel)
        {
            List<EmployeeDataCorrectionModel> employeeDataCorrectionList = new List<EmployeeDataCorrectionModel>();

            var sql = "SELECT " +
                        "rownum sl, " +
                        "EMPLOYEE_ID, " +
                        "EMPLOYEE_NAME, " +
                        "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                        "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                        "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                        "DESIGNATION_ID, " +
                        "DESIGNATION_NAME, " +
                        "DEPARTMENT_ID, " +
                        "DEPARTMENT_NAME, " +
                        "UNIT_ID, " +
                        "UNIT_NAME, " +
                        "SECTION_ID, " +
                        "SECTION_NAME, " +
                        "GRADE_NO, " +
                        "ACTIVE_YN, " +
                        "active_status, " +
                        "SUB_SECTION_ID, " +
                        "SUB_SECTION_NAME, " +
                        "CARD_NO, " +
                        "WEEKLY_HOLIDAY_ID, " +
                        "EMPLOYEE_TYPE_ID, " +
                        "old_punch_code, " +
                        "PUNCH_CODE " +

                        " FROM vew_emp_record_search where head_office_id = '" + objEmployeeDataCorrectionModel.HeadOfficeId
                        + "' and branch_office_id = '" + objEmployeeDataCorrectionModel.BranchOfficeId + "' AND active_yn = '" + objEmployeeDataCorrectionModel.Status + "' ";


            if (!string.IsNullOrWhiteSpace(objEmployeeDataCorrectionModel.EmployeeId))
            {
                sql = sql + "and employee_id = '" + objEmployeeDataCorrectionModel.EmployeeId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objEmployeeDataCorrectionModel.EmployeeName))
            {
                sql = sql + "and (lower(employee_name) like lower( '%" + objEmployeeDataCorrectionModel.EmployeeName + "%')  or upper(employee_name)like upper('%" + objEmployeeDataCorrectionModel.EmployeeName + "%') )";
            }
            if (!string.IsNullOrWhiteSpace(objEmployeeDataCorrectionModel.DepartmentId))
            {
                sql = sql + "and department_id = '" + objEmployeeDataCorrectionModel.DepartmentId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objEmployeeDataCorrectionModel.UnitId))
            {
                sql = sql + "and unit_id = '" + objEmployeeDataCorrectionModel.UnitId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objEmployeeDataCorrectionModel.SectionId))
            {
                sql = sql + "and section_id = '" + objEmployeeDataCorrectionModel.SectionId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objEmployeeDataCorrectionModel.SubSectionId))
            {
                sql = sql + "and sub_section_id = '" + objEmployeeDataCorrectionModel.SubSectionId + "' ";
            }

            //if (objEmployeeDataCorrectionModel.IsNull == "Y")
            if (objEmployeeDataCorrectionModel.IsNull == "true")
            {
                sql = sql + "and (unit_id is null or department_id is null or section_id is null or sub_section_id is null) ";
            }


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        objEmployeeDataCorrectionModel = new EmployeeDataCorrectionModel();

                        objEmployeeDataCorrectionModel.SerialNumber = Convert.ToInt32(objReader["SL"]).ToString();
                        objEmployeeDataCorrectionModel.EmployeeId = objReader["EMPLOYEE_ID"].ToString();
                        objEmployeeDataCorrectionModel.EmployeeName = objReader["EMPLOYEE_NAME"].ToString();
                        objEmployeeDataCorrectionModel.PunchCode = objReader["PUNCH_CODE"].ToString();
                        objEmployeeDataCorrectionModel.OldId = objReader["old_punch_code"].ToString();
                        objEmployeeDataCorrectionModel.CardNumber = objReader["CARD_NO"].ToString();
                        objEmployeeDataCorrectionModel.EmployeeTypeId = objReader["EMPLOYEE_TYPE_ID"].ToString();
                        objEmployeeDataCorrectionModel.WeeklyHolidayId = objReader["WEEKLY_HOLIDAY_ID"].ToString();
                        objEmployeeDataCorrectionModel.DesignationId = objReader["DESIGNATION_ID"].ToString();
                        objEmployeeDataCorrectionModel.UnitId = objReader["UNIT_ID"].ToString();
                        objEmployeeDataCorrectionModel.DepartmentId = objReader["DEPARTMENT_ID"].ToString();
                        objEmployeeDataCorrectionModel.SectionId = objReader["SECTION_ID"].ToString();
                        objEmployeeDataCorrectionModel.SubSectionId = objReader["SUB_SECTION_ID"].ToString();

                        employeeDataCorrectionList.Add(objEmployeeDataCorrectionModel);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }

            return employeeDataCorrectionList;
        }

        public string UpdateEmployeeData(EmployeeDataCorrectionModel objEmployeeDataCorrectionModel)
        {
            string vMessage = null;

            foreach (EmployeeDataCorrectionModel model in objEmployeeDataCorrectionModel.EmployeeDataCorrectionList)
            {
                OracleCommand objOracleCommand = new OracleCommand { CommandText = "pro_data_correction", CommandType = CommandType.StoredProcedure };


                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.EmployeeId) ? model.EmployeeId : null;
                objOracleCommand.Parameters.Add("p_designation_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.DesignationId) ? model.DesignationId : null;
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.DepartmentId) ? model.DepartmentId : null;
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.UnitId) ? model.UnitId : null;
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.SectionId) ? model.SectionId : null;
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.SubSectionId) ? model.SubSectionId : null;
                objOracleCommand.Parameters.Add("p_card_no", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.CardNumber) ? model.CardNumber : null;
                objOracleCommand.Parameters.Add("P_WEEKLY_HOLIDAY_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.WeeklyHolidayId) ? model.WeeklyHolidayId : null;
                objOracleCommand.Parameters.Add("P_EMPLOYEE_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.EmployeeTypeId) ? model.EmployeeTypeId : null;
                objOracleCommand.Parameters.Add("P_OLD_PUNCH_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.OldId) ? model.OldId : null;
                objOracleCommand.Parameters.Add("P_PUNCH_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.PunchCode) ? model.PunchCode : null;

                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeDataCorrectionModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeDataCorrectionModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeDataCorrectionModel.BranchOfficeId;

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

                        vMessage = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        trans.Dispose();
                        strConn.Close();
                    }
                }
            }

            return vMessage;
        }

        #endregion

        #region Employee ID Card Process

        public string DeleteIDCard(EmployeeModel objEmployeeModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_ID_CARD_DELETE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;

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

        public string ProcessIDCard(EmployeeModel objEmployeeModel)
        {
            string strMsg = null;

            foreach (EmployeeModel model in objEmployeeModel.EmployeeList)
            {
                OracleCommand objOracleCommand = new OracleCommand { CommandText = "PRO_EMPLOYEE_ID_CARD", CommandType = CommandType.StoredProcedure };


                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.EmployeeId) ? model.EmployeeId : null;
                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeModel.BranchOfficeId;

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
                        trans.Rollback();
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        trans.Dispose();
                        strConn.Close();
                    }
                }
            }

            return strMsg;
        }

        #endregion

        #region Employee Image Download

        public DataTable LoadEmployeeImageData(EmployeeModel objEmployeeModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "rownum SL, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                   "DESIGNATION_NAME, " +
                   "DEPARTMENT_NAME, " +
                   "UNIT_NAME, " +
                   "SECTION_NAME, " +
                   "SUB_SECTION_NAME, " +
                   "GRADE_NO, " +
                   "ACTIVE_YN, " +
                   "ACTIVE_STATUS, " +
                   "EMPLOYEE_IMAGE_NAME, " +
                   "EMPLOYEE_IMAGE_SIZE " +
                   " FROM VEW_EMP_RECORD_SEARCH WHERE HEAD_OFFICE_ID = '" + objEmployeeModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objEmployeeModel.BranchOfficeId + "' AND ACTIVE_YN = '" + objEmployeeModel.SearchInactiveYN + "' ";



            sql = sql + "AND EMPLOYEE_IMAGE_NAME IS NOT NULL ";



            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchEmployeeId))
            {

                sql = sql + "AND EMPLOYEE_ID = '" + objEmployeeModel.SearchEmployeeId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchEmployeeName))
            {

                sql = sql + "AND (LOWER(EMPLOYEE_NAME) LIKE LOWER( '%" + objEmployeeModel.SearchEmployeeName + "%')  OR UPPER (EMPLOYEE_NAME)LIKE UPPER('%" + objEmployeeModel.SearchEmployeeName + "%') )";
            }


            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchCardNo))
            {

                sql = sql + "AND CARD_NO = '" + objEmployeeModel.SearchCardNo + "' ";
            }


            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchUnitId))
            {

                sql = sql + "AND UNIT_ID = '" + objEmployeeModel.SearchUnitId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchDepartmentId))
            {

                sql = sql + "AND DEPARTMENT_ID = '" + objEmployeeModel.SearchDepartmentId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchSectionId))
            {

                sql = sql + "AND SECTION_ID = '" + objEmployeeModel.SearchSectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchSubSectionId))
            {

                sql = sql + "AND SUB_SECTION_ID = '" + objEmployeeModel.SearchSubSectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objEmployeeModel.SearchPunchCode))
            {

                sql = sql + "AND PUNCH_CODE = '" + objEmployeeModel.SearchPunchCode + "' ";
            }


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


        #endregion

        #region Employee Job Confirmation List

        //mezba 4
        public DataTable LoadEmployeeJobConfirmationRecord(EmployeeJobConfirmationModel objEmployeeJobConfirmationModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                "rownum sl, " +
                        "EMPLOYEE_ID, " +
                        "EMPLOYEE_NAME, " +
                        "CARD_NO, " +
                        "EMPLOYEE_NAME, " +
                        "EMPLOYEE_NAME_BANGLA, " +
                        "FATHER_NAME, " +
                        "to_char(DATE_OF_BIRTH, 'dd/mm/yyyy')DATE_OF_BIRTH, " +
                        "PUNCH_CODE, " +
                        "BLOOD_GROUP_ID, " +
                        "BLOOD_GROUP_NAME, " +
                        "GENDER_ID, " +
                        "GENDER_NAME, " +
                        "MARITAL_STATUS_ID, " +
                        "MARITAL_STATUS_NAME, " +
                        "DISTRICT_ID, " +
                        "DISTRICT_NAME, " +
                        "DIVISION_ID, " +
                        "DIVISION_NAME, " +
                        "RELIGION_ID, " +
                        "RELIGION_NAME, " +
                        "COUNTRY_ID, " +
                        "COUNTRY_NAME, " +
                        "NID_NO, " +
                        "TIN_NO, " +
                        "SPOUSE_NAME, " +
                        "PRESENT_ADDRESS, " +
                        "PERMANENT_ADDRESS, " +
                        "MAIL_ADDRESS, " +
                        "CONTACT_NO, " +
                        "EMERGENCY_CONTACT_NO, " +
                        "MOTHER_NAME, " +
                        "PASSPORT_NO, " +
                        "DRIVING_LICENSE_NO, " +
                        "UPDATE_DATE, " +
                        "HEAD_OFFICE_ID, " +
                        "HEAD_OFFICE_NAME, " +
                        "BRANCH_OFFICE_ID, " +
                        "BRANCH_OFFICE_NAME, " +
                        "BRANCH_OFFICE_ADDRESS, " +
                        "OCCURENCE_TYPE_ID, " +
                        "OCCURENCE_TYPE_NAME, " +
                        "to_char(JOINING_DATE, 'dd/mm/yyyy')JOINING_DATE, " +
                        "PROVIDENT_FUND_DATE, " +
                        "JOINING_DESIGNATION_ID, " +
                        "JOINING_DESIGNATION_NAME, " +
                        "PRESENT_DESIGNATION_ID, " +
                        "PRESENT_DESIGNATION_NAME, " +
                        "UNIT_NAME, " +
                        "UNIT_ID, " +
                        "DEPARTMENT_ID, " +
                        "SECTION_NAME, " +
                        "SECTION_ID, " +
                        "SUB_SECTION_ID, " +
                        "SUB_SECTION_NAME, " +
                        "GRADE_ID, " +
                        "GRADE_NO, " +
                        "JOINING_SALARY, " +
                        "FIRST_SALARY, " +
                        "GROSS_SALARY, " +
                        "ACCOUNT_NO, " +
                        "APPROVED_BY_NAME, " +
                        "SUPERVISOR_NAME, " +
                        "REFERENCE_EMPLOYEE_NAME, " +
                        "RESIGN_DATE, " +
                        "ACTIVE_YN, " +
                        "EMPLOYEE_TYPE_ID, " +
                        "EMPLOYEE_TYPE_NAME, " +
                        "JOB_TYPE_ID, " +
                        "JOB_TYPE_NAME, " +
                        "PAYMENT_TYPE_ID, " +
                        "PAYMENT_TYPE_NAME, " +
                        "PROBATION_PERIOD_ID, " +
                        "PROBATION_PERIOD, " +
                        "SHIFT_ID, " +
                        "SHIFT_NAME, " +
                        "JOB_LOCATION_ID, " +
                        "JOB_LOCATION, " +
                        "LOCAL_GUARDIAN_NAME, " +
                        "LOCAL_GUARDIAN_NID, " +
                        "LOCAL_GUARDIAN_CONTACT_NO, " +
                        "EMPLOYEEE_PIC, " +
                        "EMPLOYEEE_SIGNATURE, " +
                        "to_char(JOB_CONFIRMATION_DATE, 'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                        "DEPARTMENT_NAME " +
                        "FROM VEW_JOB_CONFIRMATION_LIST  where head_office_id = '" + objEmployeeJobConfirmationModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeJobConfirmationModel.BranchOfficeId + "' and JOB_CONFIRMATION_DATE between to_date( '" + objEmployeeJobConfirmationModel.FromDate.Trim() + "', 'dd/mm/yyyy') and to_date( '" + objEmployeeJobConfirmationModel.ToDate.Trim() + "', 'dd/mm/yyyy') and active_yn = 'Y' ";


            if (!string.IsNullOrWhiteSpace(objEmployeeJobConfirmationModel.EmployeeId))
            {
                sql = sql + "and employee_id = '" + objEmployeeJobConfirmationModel.EmployeeId.Trim() + "' ";

            }

            if (!string.IsNullOrWhiteSpace(objEmployeeJobConfirmationModel.UnitId))
            {
                sql = sql + "and unit_id = '" + objEmployeeJobConfirmationModel.UnitId + "' ";

            }

            if (!string.IsNullOrWhiteSpace(objEmployeeJobConfirmationModel.DepartmentId))
            {
                sql = sql + "and department_id = '" + objEmployeeJobConfirmationModel.DepartmentId + "' ";

            }


            if (!string.IsNullOrWhiteSpace(objEmployeeJobConfirmationModel.SectionId))
            {
                sql = sql + "and section_id = '" + objEmployeeJobConfirmationModel.SectionId + "' ";

            }

            if (!string.IsNullOrWhiteSpace(objEmployeeJobConfirmationModel.SubSectionId))
            {
                sql = sql + "and sub_section_id = '" + objEmployeeJobConfirmationModel.SubSectionId + "' ";

            }


            sql = sql + " order by sl ";

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

        //mezba 5
        public DataSet EmployeeJobConfirmationDetail(EmployeeJobConfirmationModel objEmployeeJobConfirmationModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
                           "'Employee Job Confirmation List '|| 'From  '|| to_date( '" + objEmployeeJobConfirmationModel.FromDate + "', 'dd/mm/yyyy') || ' to ' || to_date('" + objEmployeeJobConfirmationModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                           "EMPLOYEE_ID, " +
                           "EMPLOYEE_NAME, " +
                           "EMPLOYEE_ID, " +
                           "CARD_NO, " +
                           "EMPLOYEE_NAME, " +
                           "EMPLOYEE_NAME_BANGLA, " +
                           "FATHER_NAME, " +
                           "DATE_OF_BIRTH, " +
                           "PUNCH_CODE, " +
                           "BLOOD_GROUP_ID, " +
                           "BLOOD_GROUP_NAME, " +
                           "GENDER_ID, " +
                           "GENDER_NAME, " +
                           "MARITAL_STATUS_ID, " +
                           "MARITAL_STATUS_NAME, " +
                           "DISTRICT_ID, " +
                           "DISTRICT_NAME, " +
                           "DIVISION_ID, " +
                           "DIVISION_NAME, " +
                           "RELIGION_ID, " +
                           "RELIGION_NAME, " +
                           "COUNTRY_ID, " +
                           "COUNTRY_NAME, " +
                           "NID_NO, " +
                           "TIN_NO, " +
                           "SPOUSE_NAME, " +
                           "PRESENT_ADDRESS, " +
                           "PERMANENT_ADDRESS, " +
                           "MAIL_ADDRESS, " +
                           "CONTACT_NO, " +
                           "EMERGENCY_CONTACT_NO, " +
                           "MOTHER_NAME, " +
                           "PASSPORT_NO, " +
                           "DRIVING_LICENSE_NO, " +
                           "UPDATE_DATE, " +
                           "HEAD_OFFICE_ID, " +
                           "HEAD_OFFICE_NAME, " +
                           "BRANCH_OFFICE_ID, " +
                           "BRANCH_OFFICE_NAME, " +
                           "BRANCH_OFFICE_ADDRESS, " +
                           "OCCURENCE_TYPE_ID, " +
                           "OCCURENCE_TYPE_NAME, " +
                           "JOINING_DATE, " +
                           "PROVIDENT_FUND_DATE, " +
                           "JOINING_DESIGNATION_ID, " +
                           "JOINING_DESIGNATION_NAME, " +
                           "PRESENT_DESIGNATION_ID, " +
                           "PRESENT_DESIGNATION_NAME, " +
                           "UNIT_NAME, " +
                           "UNIT_ID, " +
                           "DEPARTMENT_ID, " +
                           "SECTION_NAME, " +
                           "SECTION_ID, " +
                           "SUB_SECTION_ID, " +
                           "SUB_SECTION_NAME, " +
                           "GRADE_ID, " +
                           "GRADE_NO, " +
                           "JOINING_SALARY, " +
                           "FIRST_SALARY, " +
                           "GROSS_SALARY, " +
                           "ACCOUNT_NO, " +
                           "APPROVED_BY_NAME, " +
                           "SUPERVISOR_NAME, " +
                           "REFERENCE_EMPLOYEE_NAME, " +
                           "RESIGN_DATE, " +
                           "ACTIVE_YN, " +
                           "EMPLOYEE_TYPE_ID, " +
                           "EMPLOYEE_TYPE_NAME, " +
                           "JOB_TYPE_ID, " +
                           "JOB_TYPE_NAME, " +
                           "PAYMENT_TYPE_ID, " +
                           "PAYMENT_TYPE_NAME, " +
                           "PROBATION_PERIOD_ID, " +
                           "PROBATION_PERIOD, " +
                           "SHIFT_ID, " +
                           "SHIFT_NAME, " +
                           "JOB_LOCATION_ID, " +
                           "JOB_LOCATION, " +
                           "LOCAL_GUARDIAN_NAME, " +
                           "LOCAL_GUARDIAN_NID, " +
                           "LOCAL_GUARDIAN_CONTACT_NO, " +
                           "EMPLOYEEE_PIC, " +
                           "EMPLOYEEE_SIGNATURE, " +
                           "JOB_CONFIRMATION_DATE, " +
                           "DEPARTMENT_NAME, " +
                           "(select employee_name from EMPLOYEE_BASIC where EMPLOYEE_ID = '" + objEmployeeJobConfirmationModel.UpdateBy + "') UPDATE_BY " +
                            "from VEW_EMPLOYEE_INFORMATION  where head_office_id = '" + objEmployeeJobConfirmationModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeJobConfirmationModel.BranchOfficeId + "' and active_yn = 'Y' ";


                    if (!string.IsNullOrEmpty(objEmployeeJobConfirmationModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objEmployeeJobConfirmationModel.EmployeeId.Trim() + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeJobConfirmationModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeJobConfirmationModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeJobConfirmationModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeJobConfirmationModel.DepartmentId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objEmployeeJobConfirmationModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeJobConfirmationModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeJobConfirmationModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeJobConfirmationModel.SubSectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeJobConfirmationModel.FromDate) && (!string.IsNullOrEmpty(objEmployeeJobConfirmationModel.ToDate)))
                    {
                        sql = sql + "and JOB_CONFIRMATION_DATE between to_date( '" + objEmployeeJobConfirmationModel.FromDate.Trim() + "', 'dd/mm/yyyy') and to_date( '" + objEmployeeJobConfirmationModel.ToDate.Trim() + "', 'dd/mm/yyyy')  ";

                    }

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_EMPLOYEE_JCD");
                            objDataAdapter.Dispose();
                            objOracleCommand.Dispose();
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

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        #endregion
        public List<ApproverModel> GetAllEmployeesForApprover(ApproverModel objApproverModel)
        {
            List<ApproverModel> objApproverList = new List<ApproverModel>();

            string sql = "";
            sql = "SELECT " +
                    "ROWNUM SL, " +
                    "EMPLOYEE_ID, " +
                    "EMPLOYEE_NAME, " +
                    "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                    "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                    "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                    "PRESENT_DESIGNATION_ID, " +
                    "DESIGNATION_NAME, " +
                    "DEPARTMENT_ID, " +
                    "DEPARTMENT_NAME, " +
                    "UNIT_ID, " +
                    "UNIT_NAME, " +
                    "SECTION_ID, " +
                    "SECTION_NAME, " +
                    "GRADE_NO, " +
                    "ACTIVE_YN, " +
                    "active_status, " +
                    "EMPLOYEE_IMAGE, " +
                    "SUB_SECTION_NAME " +
                    "FROM vew_emp_record_search where head_office_id = '" + objApproverModel.HeadOfficeId + "' and branch_office_id = '" + objApproverModel.BranchOfficeId + "' AND active_yn = '" + objApproverModel.Status + "'   ";

            if (!string.IsNullOrWhiteSpace(objApproverModel.EmployeeId))
            {
                sql = sql + "and employee_id = '" + objApproverModel.EmployeeId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objApproverModel.EmployeeName))
            {
                sql = sql + "and (lower(employee_name) like lower( '%" + objApproverModel.EmployeeName + "%')  or upper(employee_name)like upper('%" + objApproverModel.EmployeeName + "%') )";
            }
            if (!string.IsNullOrWhiteSpace(objApproverModel.DepartmentId))
            {
                sql = sql + "and department_id = '" + objApproverModel.DepartmentId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objApproverModel.UnitId))
            {
                sql = sql + "and unit_id = '" + objApproverModel.UnitId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objApproverModel.SubSectionId))
            {
                sql = sql + "and sub_section_id = '" + objApproverModel.SubSectionId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objApproverModel.SectionId))
            {
                sql = sql + "and section_id = '" + objApproverModel.SectionId + "' ";
            }

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        ApproverModel objApprover = new ApproverModel();

                        objApprover.SerialNumber = Convert.ToInt32(objReader["SL"]).ToString();
                        objApprover.EmployeeId = objReader["EMPLOYEE_ID"].ToString();
                        objApprover.EmployeeName = objReader["EMPLOYEE_NAME"].ToString();
                        objApprover.JoiningDate = objReader["JOINING_DATE"].ToString();
                        objApprover.DesignationName = objReader["DESIGNATION_NAME"].ToString();
                        objApprover.UnitName = objReader["UNIT_NAME"].ToString();
                        objApprover.DepartmentName = objReader["DEPARTMENT_NAME"].ToString();
                        objApprover.SectionName = objReader["SECTION_NAME"].ToString();
                        objApprover.SubSectionName = objReader["SUB_SECTION_NAME"].ToString();
                        objApprover.Status = objReader["active_status"].ToString();

                        objApprover.EmployeeImage = objReader["EMPLOYEE_IMAGE"] == DBNull.Value ? new byte[0] : (byte[])objReader["EMPLOYEE_IMAGE"];

                        objApproverList.Add(objApprover);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }

            return objApproverList;
        }
        public List<SupervisorModel> GetAllEmployeesForSupervisor(SupervisorModel objSupervisorModel)
        {
            List<SupervisorModel> supervisorList = new List<SupervisorModel>();

            string sql = "";
            sql = "SELECT " +
                    "ROWNUM SL, " +
                    "EMPLOYEE_ID, " +
                    "EMPLOYEE_NAME, " +
                    "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                    "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                    "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                    "PRESENT_DESIGNATION_ID, " +
                    "DESIGNATION_NAME, " +
                    "DEPARTMENT_ID, " +
                    "DEPARTMENT_NAME, " +
                    "UNIT_ID, " +
                    "UNIT_NAME, " +
                    "SECTION_ID, " +
                    "SECTION_NAME, " +
                    "GRADE_NO, " +
                    "ACTIVE_YN, " +
                    "active_status, " +
                    "EMPLOYEE_IMAGE, " +
                    "SUB_SECTION_NAME " +
                    "FROM vew_emp_record_search where head_office_id = '" + objSupervisorModel.HeadOfficeId + "' and branch_office_id = '" + objSupervisorModel.BranchOfficeId + "' AND active_yn = '" + objSupervisorModel.Status + "'   ";

            if (!string.IsNullOrWhiteSpace(objSupervisorModel.EmployeeId))
            {
                sql = sql + "and employee_id = '" + objSupervisorModel.EmployeeId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objSupervisorModel.EmployeeName))
            {
                sql = sql + "and (lower(employee_name) like lower( '%" + objSupervisorModel.EmployeeName + "%')  or upper(employee_name)like upper('%" + objSupervisorModel.EmployeeName + "%') )";
            }
            if (!string.IsNullOrWhiteSpace(objSupervisorModel.DepartmentId))
            {
                sql = sql + "and department_id = '" + objSupervisorModel.DepartmentId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objSupervisorModel.UnitId))
            {
                sql = sql + "and unit_id = '" + objSupervisorModel.UnitId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objSupervisorModel.SubSectionId))
            {
                sql = sql + "and sub_section_id = '" + objSupervisorModel.SubSectionId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objSupervisorModel.SectionId))
            {
                sql = sql + "and section_id = '" + objSupervisorModel.SectionId + "' ";
            }

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        SupervisorModel objSupervisor = new SupervisorModel();

                        objSupervisor.SerialNumber = Convert.ToInt32(objReader["SL"]).ToString();
                        objSupervisor.EmployeeId = objReader["EMPLOYEE_ID"].ToString();
                        objSupervisor.EmployeeName = objReader["EMPLOYEE_NAME"].ToString();
                        objSupervisor.JoiningDate = objReader["JOINING_DATE"].ToString();
                        objSupervisor.DesignationName = objReader["DESIGNATION_NAME"].ToString();
                        objSupervisor.UnitName = objReader["UNIT_NAME"].ToString();
                        objSupervisor.DepartmentName = objReader["DEPARTMENT_NAME"].ToString();
                        objSupervisor.SectionName = objReader["SECTION_NAME"].ToString();
                        objSupervisor.SubSectionName = objReader["SUB_SECTION_NAME"].ToString();
                        objSupervisor.Status = objReader["active_status"].ToString();

                        objSupervisor.EmployeeImage = objReader["EMPLOYEE_IMAGE"] == DBNull.Value ? new byte[0] : (byte[])objReader["EMPLOYEE_IMAGE"];

                        supervisorList.Add(objSupervisor);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }

            return supervisorList;
        }
        public EmployeeModel GetEmployeeProfileById(string pEmployeeId, string pHeadOfficeId, string pBranchOfficeId)
        {
            EmployeeModel objEmployeeModel = null;

            string sql = "";
            sql = "SELECT " +

                    //Employee Basic Information
                    "TO_CHAR (NVL (employee_id, 'N/A')), " +
                    "TO_CHAR (NVL (EMPLOYEE_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (FATHER_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (MOTHER_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (BLOOD_GROUP_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (GENDER_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (MARITAL_STATUS_NAME, 'N/A')), " +

                    "TO_CHAR (NVL (RELIGION_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (NID_NO, 'N/A')), " +
                    "TO_CHAR (NVL (TIN_NO, 'N/A')), " +

                    "TO_CHAR (NVL (PUNCH_CODE, 'N/A')), " +
                    "TO_CHAR (NVL (LOCAL_GUARDIAN_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (LOCAL_GUARDIAN_NID, 'N/A')), " +
                    "TO_CHAR (NVL (PASSPORT_NO, 'N/A')), " +


                    //Employee Job Detail Information
                    "NVL (TO_CHAR (JOINING_DATE, 'dd/mm/yyyy'), ' '), " +
                    "NVL (TO_CHAR (JOB_CONFIRMATION_DATE, 'dd/mm/yyyy'), ' '), " +
                    "TO_CHAR (NVL (JOINING_DESIGNATION_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (PRESENT_DESIGNATION_NAME, 'N/A')), " +


                    "TO_CHAR (NVL (UNIT_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (DEPARTMENT_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (SECTION_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (SUB_SECTION_NAME, 'N/A')), " +


                    "TO_CHAR (NVL (ACCOUNT_NO, 'N/A')), " +
                    "TO_CHAR (NVL (JOINING_SALARY, '0')), " +
                    "TO_CHAR (NVL (FIRST_SALARY, '0')), " +
                    "TO_CHAR (NVL (GROSS_SALARY, '0')), " +
                    "TO_CHAR (NVL (SUPERVISOR_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (APPROVED_BY_NAME, 'N/A')), " +
                    "NVL (TO_CHAR (RESIGN_DATE, 'dd/mm/yyyy'), ' '), " +

                    //Address Information
                    "TO_CHAR (NVL (DISTRICT_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (DIVISION_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (PRESENT_ADDRESS, 'N/A')), " +
                    "TO_CHAR (NVL (PERMANENT_ADDRESS, 'N/A')), " +

                    //Contact Information
                    "TO_CHAR (NVL (CONTACT_NO, 'N/A')), " +
                    "TO_CHAR (NVL (EMERGENCY_CONTACT_NO, 'N/A')), " +
                    "TO_CHAR (NVL (LOCAL_GUARDIAN_CONTACT_NO, 'N/A')), " +
                    "TO_CHAR (NVL (MAIL_ADDRESS, 'N/A')), " +

                    "TO_CHAR (NVL (emp_title, 'N/A')), " +
                    "NVL (TO_CHAR (DATE_OF_BIRTH, 'dd/mm/yyyy'), ' '), " +
                    "EMPLOYEEE_PIC, " +


                    /*Most Recent Education Information*/

                    "TO_CHAR (NVL (PRE_INSTITUTE_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (PRE_DEGREE_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (PRE_MAJOR_SUBJECT_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (PRE_CGPA, 'N/A')), " +
                    "TO_CHAR (NVL (PRE_PASSING_YEAR, '0')), " +


                    /*Most Recent Previous Job Information*/
                    "TO_CHAR (NVL (PRE_ORGANIZATION_NAME, 'N/A'))," +
                    "NVL (TO_CHAR (PRE_JOINING_DATE, 'dd/mm/yyyy'), ' ')," +
                    "TO_CHAR (NVL (PRE_DESIGNATION_NAME, 'N/A'))," +
                    "NVL (TO_CHAR (PRE_RESIGN_DATE, 'dd/mm/yyyy'), ' ')," +
                    "TO_CHAR (NVL (PRE_GROSS_SALARY, '0'))," +
                    "TO_CHAR (NVL (EMPLOYMENT_STATUS, 'N/A'))" +

                    " from VEW_EMPLOYEE_PROFILE where employee_id = '" + pEmployeeId + "' AND head_office_id = '" + pHeadOfficeId + "' AND branch_office_id = '" + pBranchOfficeId + "' ";



            using (OracleConnection strConn = GetConnection())
            {
                strConn.Open();
                OracleCommand objCommand = new OracleCommand(sql, strConn);
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    if (objDataReader != null && objDataReader.HasRows)
                    {
                        objDataReader.Read();
                        objEmployeeModel = new EmployeeModel();

                        objEmployeeModel.EmployeeId = objDataReader.GetString(0);
                        objEmployeeModel.EmployeeName = objDataReader.GetString(1);
                        objEmployeeModel.FatherName = objDataReader.GetString(2);
                        objEmployeeModel.MotherName = objDataReader.GetString(3);
                        objEmployeeModel.BloodGroupId = objDataReader.GetString(4);
                        objEmployeeModel.GenderId = objDataReader.GetString(5);
                        objEmployeeModel.MaritalStatusId = objDataReader.GetString(6);
                        objEmployeeModel.ReligionId = objDataReader.GetString(7);
                        objEmployeeModel.NIDNo = objDataReader.GetString(8);
                        objEmployeeModel.TINNo = objDataReader.GetString(9);
                        objEmployeeModel.PunchCode = objDataReader.GetString(10);
                        objEmployeeModel.LocalGuardianName = objDataReader.GetString(11);
                        objEmployeeModel.LocalGuardianNIDNo = objDataReader.GetString(12);
                        objEmployeeModel.PassportNo = objDataReader.GetString(13);

                        objEmployeeModel.JoiningDate = objDataReader.GetString(14);
                        objEmployeeModel.JobConfirmationDate = objDataReader.GetString(15);
                        objEmployeeModel.JoiningDesignationId = objDataReader.GetString(16);
                        objEmployeeModel.PresentDesignationId = objDataReader.GetString(17);
                        objEmployeeModel.UnitId = objDataReader.GetString(18);
                        objEmployeeModel.DepartmentId = objDataReader.GetString(19);
                        objEmployeeModel.SectionId = objDataReader.GetString(20);
                        objEmployeeModel.SubSectionId = objDataReader.GetString(21);
                        objEmployeeModel.AccountNo = objDataReader.GetString(22);
                        objEmployeeModel.JoiningSalary = objDataReader.GetString(23);
                        objEmployeeModel.FirstSalary = objDataReader.GetString(24);
                        objEmployeeModel.GrossSalary = objDataReader.GetString(25);
                        objEmployeeModel.SupervisorName = objDataReader.GetString(26);
                        //objEmployeeModel.ApprovedEmployeeName = objDataReader.GetString(27);
                        objEmployeeModel.ApproverEmployeeName = objDataReader.GetString(27);
                        objEmployeeModel.ResignDate = objDataReader.GetString(28);

                        objEmployeeModel.DistrictId = objDataReader.GetString(29);
                        objEmployeeModel.DivisionId = objDataReader.GetString(30);
                        objEmployeeModel.PresentAddress = objDataReader.GetString(31);
                        objEmployeeModel.PermanentAddress = objDataReader.GetString(32);

                        objEmployeeModel.ContactNo = objDataReader.GetString(33);
                        objEmployeeModel.EmergencyContactNo = objDataReader.GetString(34);
                        objEmployeeModel.LocalGuardianContactNo = objDataReader.GetString(35);
                        objEmployeeModel.EmailAddress = objDataReader.GetString(36);
                        //objEmployeeModel.EmpTitle = objDataReader.GetString(37);
                        objEmployeeModel.EmployeeTitle = objDataReader.GetString(37);
                        objEmployeeModel.DateOfBirth = objDataReader.GetString(38);

                        //objEmployeeModel.EmployeeImage = objDataReader["EMPLOYEEE_PIC"] == DBNull.Value ? new byte[0] : (byte[])objDataReader["EMPLOYEEE_PIC"];
                        objEmployeeModel.EmployeeImage = objDataReader[39] == DBNull.Value ? new byte[0] : (byte[])objDataReader[39];


                        objEmployeeModel.PreviousInstituteName = objDataReader.GetString(40);
                        objEmployeeModel.PreviousDegreeName = objDataReader.GetString(41);
                        objEmployeeModel.PreviousMajorSubjectName = objDataReader.GetString(42);
                        objEmployeeModel.PreviousCGPA = objDataReader.GetString(43);
                        objEmployeeModel.PreviousPassingYear = objDataReader.GetString(44);

                        objEmployeeModel.PreviousOrganizationName = objDataReader.GetString(45);
                        objEmployeeModel.PreviousJoiningDate = objDataReader.GetString(46);
                        objEmployeeModel.PreviousDesignationName = objDataReader.GetString(47);
                        objEmployeeModel.PreviousResignDate = objDataReader.GetString(48);
                        objEmployeeModel.PreviousGrossSalary = objDataReader.GetString(49);

                        objEmployeeModel.EmploymentStatus = objDataReader.GetString(50);



                        objDataReader.Close();
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

            return objEmployeeModel;
        }

        public EmployeeModel CheckValidPassword(EmployeeModel objEmployeeModel)
        {
            //LoginModel objLoginModel = new LoginModel();
            OracleCommand objOracleCommand = new OracleCommand("pro_check_password")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objEmployeeModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objEmployeeModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objEmployeeModel.Password != "")
            {
                objOracleCommand.Parameters.Add("p_employee_password", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeModel.Password;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_password", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



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

                    objEmployeeModel.Message = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                }

                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }
            return objEmployeeModel;


        }

        #region Employee Resign Entry


        //mezba 6
        public DataTable LoadAllResignRecord(EmployeeResignModel objEmployeeResignModel)
        {
            DataTable dt = new DataTable();
            string sql = "";

            sql = "SELECT " +
                  "rownum SL , " +
                  "EMPLOYEE_ID , " +
                  "EMPLOYEE_NAME , " +
                  "JOINING_DESIGNATION_NAME , " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE ," +
                  "TO_CHAR(resign_date,'dd/mm/yyyy')resign_date , " +
                  "resign_cause , " +
                  "remarks " +
                   " from VEW_EMPLOYEE_RESIGN where  head_office_id = '" + objEmployeeResignModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeResignModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objEmployeeResignModel.SearchBy))
            {

                sql = sql + "and (lower(EMPLOYEE_ID) like lower( '%" + objEmployeeResignModel.SearchBy.Trim() + "%')  or upper(EMPLOYEE_ID)like upper('%" + objEmployeeResignModel.SearchBy.Trim() + "%')) or (lower(EMPLOYEE_NAME) like lower( '%" + objEmployeeResignModel.SearchBy.Trim() + "%')  or upper(EMPLOYEE_NAME)like upper('%" + objEmployeeResignModel.SearchBy.Trim() + "%'))";
            }


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

        public EmployeeResignModel LoadDataToMain(EmployeeResignModel objEmployeeResignModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (EMPLOYEE_NAME ,'0')), " +
                  "TO_CHAR (NVL (JOINING_DESIGNATION_NAME ,'0')), " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE ," +
                  "TO_CHAR(resign_date,'dd/mm/yyyy')resign_date , " +
                  "TO_CHAR (NVL (resign_cause ,'0')), " +
                  "TO_CHAR (NVL (remarks ,'0')) " +
                  " from VEW_EMPLOYEE_RESIGN where EMPLOYEE_ID = '" + objEmployeeResignModel.EmployeeId + "' AND  head_office_id = '" + objEmployeeResignModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeResignModel.BranchOfficeId + "' ";


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

                        objEmployeeResignModel.EmployeeName = objDataReader.GetString(0);
                        objEmployeeResignModel.ResignDesignation = objDataReader.GetString(1);
                        objEmployeeResignModel.JoiningDate = objDataReader.GetString(2);
                        objEmployeeResignModel.ResignDate = objDataReader.GetString(3);
                        objEmployeeResignModel.ResignCause = objDataReader.GetString(4);
                        objEmployeeResignModel.ResignRemarks = objDataReader.GetString(5);

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


            return objEmployeeResignModel;

        }
        public EmployeeResignModel ResignEmployeePhoto(EmployeeResignModel objEmployeeResignModel)
        {

            string sql = "";
            sql = "SELECT " +
               "FILE_NAME, " +
               "FILE_SIZE " +
               "FROM EMPLOYEE_IMAGE where employee_id = '" + objEmployeeResignModel.EmployeeId + "' and head_office_id = '" + objEmployeeResignModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeResignModel.BranchOfficeId + "' ";


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

                        if (objDataReader.GetString(0) != null)
                        {
                            objEmployeeResignModel.EditImageFileByte = (byte[])objDataReader.GetValue(1);
                            objEmployeeResignModel.EditImageFileNameBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(objEmployeeResignModel.EditImageFileByte);
                        }

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

            return objEmployeeResignModel;

        }
        public EmployeeResignModel ResignEmployeeInformation(EmployeeResignModel objEmployeeResignModel)
        {

            string sql = "";
            sql = "SELECT " +
               "TO_CHAR (NVL (EMPLOYEE_ID,'N/A')), " +
               "TO_CHAR (NVL (EMPLOYEE_NAME,'N/A')), " +
               "NVL (TO_CHAR (JOINING_DATE, 'dd/mm/yyyy'), ' '), " +
               "TO_CHAR (NVL (PRESENT_DESIGNATION_NAME,'N/A'))" +
               "FROM VEW_EMP_INFO_RESIGN WHERE EMPLOYEE_ID = '" + objEmployeeResignModel.EmployeeId + "' AND HEAD_OFFICE_ID = '" + objEmployeeResignModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objEmployeeResignModel.BranchOfficeId + "' AND ACTIVE_YN = 'Y' ";

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

                        objEmployeeResignModel.EmployeeId = objDataReader.GetString(0);
                        objEmployeeResignModel.EmployeeName = objDataReader.GetString(1);
                        objEmployeeResignModel.JoiningDate = objDataReader.GetString(2);
                        objEmployeeResignModel.ResignDesignation = objDataReader.GetString(3);

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

            return objEmployeeResignModel;

        }
        public string SaveEmployeeEntryResign(EmployeeResignModel objEmployeeResignModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_employee_resign_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;



            objOracleCommand.Parameters.Add("P_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeResignModel.EmployeeId) ? objEmployeeResignModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("P_resign_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeResignModel.ResignDate) ? objEmployeeResignModel.ResignDate : null;
            objOracleCommand.Parameters.Add("P_resign_cause", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeResignModel.ResignCause) ? objEmployeeResignModel.ResignCause : null;
            objOracleCommand.Parameters.Add("P_remarks", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeResignModel.ResignRemarks) ? objEmployeeResignModel.ResignRemarks : null;
            objOracleCommand.Parameters.Add("P_UPDATE_BY", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeResignModel.UpdateBy;
            objOracleCommand.Parameters.Add("P_CREATE_BY", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeResignModel.UpdateBy;
            objOracleCommand.Parameters.Add("P_HEAD_OFFICE_ID", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeResignModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("P_BRANCH_OFFICE_ID", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeResignModel.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                    
                }

                finally
                {

                    strConn.Close();
                }

            }

            return strMsg;

        }

        #endregion

        #region Team Leader Entry

        //mezba 7
        public DataTable LoadTeamLeaderRecord(TeamLeaderModel objTeamLeaderModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                "rownum sl, " +
                "EMPLOYEE_ID, " +
                "EMPLOYEE_NAME, " +
                "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                "DESIGNATION_NAME, " +
                "DEPARTMENT_NAME, " +
                "UNIT_NAME, " +
                "SECTION_NAME, " +
                "active_status, " +
                "EMPLOYEE_IMAGE, " +
                "SUB_SECTION_NAME " +
                " FROM vew_team_leader_info where head_office_id = '" + objTeamLeaderModel.HeadOfficeId + "' and branch_office_id = '" + objTeamLeaderModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrWhiteSpace(objTeamLeaderModel.SearchBy))
            {
                sql = sql + "and (lower(employee_name) like lower( '%" + objTeamLeaderModel.SearchBy.Trim() + "%')  or upper(employee_name)like upper('%" + objTeamLeaderModel.SearchBy.Trim() + "%')  or lower(employee_id) like lower( '%" + objTeamLeaderModel.SearchBy.Trim() + "%')  or upper(employee_id)like upper('%" + objTeamLeaderModel.SearchBy.Trim() + "%') )";

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


        //mezba 8
        public string TeamLeaderEmployeeSave(TeamLeaderModel objTeamLeaderModel)
        {
            string strMsg = null;

            foreach (TeamLeaderModel model in objTeamLeaderModel.EmployeeList)
            {
                OracleCommand objOracleCommand = new OracleCommand { CommandText = "PRO_TEAM_LEADER_SAVE", CommandType = CommandType.StoredProcedure };


                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.EmployeeId) ? model.EmployeeId : null;
                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTeamLeaderModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTeamLeaderModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTeamLeaderModel.BranchOfficeId;

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
                        trans.Rollback();
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        trans.Dispose();
                        strConn.Close();
                    }
                }
            }

            return strMsg;
        }

        //mezba 9
        public DataTable LoadEmployeeDataForTeamLeader(TeamLeaderModel objTeamLeaderModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "rownum SL, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                   "DESIGNATION_NAME, " +
                   "DEPARTMENT_NAME, " +
                   "UNIT_NAME, " +
                   "SECTION_NAME, " +
                   "SUB_SECTION_NAME, " +
                   "GRADE_NO, " +
                   "ACTIVE_YN, " +
                   "ACTIVE_STATUS, " +
                   "EMPLOYEE_IMAGE_NAME, " +
                   "EMPLOYEE_IMAGE_SIZE " +
                   " FROM VEW_EMP_RECORD_SEARCH WHERE HEAD_OFFICE_ID = '" + objTeamLeaderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objTeamLeaderModel.BranchOfficeId + "' AND ACTIVE_YN = 'Y' ";



            if (!string.IsNullOrWhiteSpace(objTeamLeaderModel.EmployeeId))
            {

                sql = sql + "AND EMPLOYEE_ID = '" + objTeamLeaderModel.EmployeeId.Trim() + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objTeamLeaderModel.EmployeeName))
            {

                sql = sql + "AND (LOWER(EMPLOYEE_NAME) LIKE LOWER( '%" + objTeamLeaderModel.EmployeeName.Trim() + "%')  OR UPPER (EMPLOYEE_NAME)LIKE UPPER('%" + objTeamLeaderModel.EmployeeName.Trim() + "%') )";
            }


            if (!string.IsNullOrWhiteSpace(objTeamLeaderModel.CardNo))
            {

                sql = sql + "AND CARD_NO = '" + objTeamLeaderModel.CardNo.Trim() + "' ";
            }


            if (!string.IsNullOrWhiteSpace(objTeamLeaderModel.UnitId))
            {

                sql = sql + "AND UNIT_ID = '" + objTeamLeaderModel.UnitId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objTeamLeaderModel.UnitId))
            {

                sql = sql + "AND DEPARTMENT_ID = '" + objTeamLeaderModel.DepartmentId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objTeamLeaderModel.SectionId))
            {

                sql = sql + "AND SECTION_ID = '" + objTeamLeaderModel.SectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objTeamLeaderModel.SubSectionId))
            {

                sql = sql + "AND SUB_SECTION_ID = '" + objTeamLeaderModel.SubSectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objTeamLeaderModel.PunchCode))
            {

                sql = sql + "AND PUNCH_CODE = '" + objTeamLeaderModel.PunchCode.Trim() + "' ";
            }


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


        #endregion

        #region Team Leader Hierarchy Entry

        //mezba 10
        public DataTable GetTeamLeaderName(string pHeadOfficeId, string pBranchOfficeId)
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME " +
                  "FROM vew_get_team_leader  where head_office_id= '" + pHeadOfficeId + "' and branch_office_id = '" + pBranchOfficeId + "' order by EMPLOYEE_NAME ";

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(dt);

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

        //mezba 11
        public DataTable LoadEmployeeDataForTeamLeaderHierarchy(TeamLeaderHierarchyModel objTeamLeaderHierarchyModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "rownum SL, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                   "DESIGNATION_NAME, " +
                   "DEPARTMENT_NAME, " +
                   "UNIT_NAME, " +
                   "SECTION_NAME, " +
                   "SUB_SECTION_NAME, " +
                   "GRADE_NO, " +
                   "ACTIVE_YN, " +
                   "ACTIVE_STATUS, " +
                   "EMPLOYEE_IMAGE_NAME, " +
                   "EMPLOYEE_IMAGE_SIZE " +
                   " FROM VEW_EMP_RECORD_SEARCH WHERE HEAD_OFFICE_ID = '" + objTeamLeaderHierarchyModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objTeamLeaderHierarchyModel.BranchOfficeId + "' AND ACTIVE_YN = 'Y' ";



            if (!string.IsNullOrWhiteSpace(objTeamLeaderHierarchyModel.EmployeeId))
            {

                sql = sql + "AND EMPLOYEE_ID = '" + objTeamLeaderHierarchyModel.EmployeeId.Trim() + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objTeamLeaderHierarchyModel.EmployeeName))
            {

                sql = sql + "AND (LOWER(EMPLOYEE_NAME) LIKE LOWER( '%" + objTeamLeaderHierarchyModel.EmployeeName.Trim() + "%')  OR UPPER (EMPLOYEE_NAME)LIKE UPPER('%" + objTeamLeaderHierarchyModel.EmployeeName.Trim() + "%') )";
            }


            if (!string.IsNullOrWhiteSpace(objTeamLeaderHierarchyModel.CardNo))
            {

                sql = sql + "AND CARD_NO = '" + objTeamLeaderHierarchyModel.CardNo.Trim() + "' ";
            }


            if (!string.IsNullOrWhiteSpace(objTeamLeaderHierarchyModel.UnitId))
            {

                sql = sql + "AND UNIT_ID = '" + objTeamLeaderHierarchyModel.UnitId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objTeamLeaderHierarchyModel.DepartmentId))
            {

                sql = sql + "AND DEPARTMENT_ID = '" + objTeamLeaderHierarchyModel.DepartmentId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objTeamLeaderHierarchyModel.SectionId))
            {

                sql = sql + "AND SECTION_ID = '" + objTeamLeaderHierarchyModel.SectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objTeamLeaderHierarchyModel.SubSectionId))
            {

                sql = sql + "AND SUB_SECTION_ID = '" + objTeamLeaderHierarchyModel.SubSectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objTeamLeaderHierarchyModel.PunchCode))
            {

                sql = sql + "AND PUNCH_CODE = '" + objTeamLeaderHierarchyModel.PunchCode.Trim() + "' ";
            }


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


        //mezba 12
        public DataTable ShowSubordinateEmpRecordForTeamHierarchy(TeamLeaderHierarchyModel objTeamLeaderHierarchyModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "rownum SL, " +
                   "EMPLOYEE_ID, " +
                   "TEAM_LEADER_NAME, " +
                   "SUBORDINATE_EMPLOYEE_NAME, " +
                   "SUBORDINATE_EMPLOYEE_ID, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "DESIGNATION_NAME, " +
                   "DEPARTMENT_NAME, " +
                   "SUB_SECTION_NAME, " +
                   "SECTION_NAME, " +
                   "UNIT_NAME, " +
                   "ACTIVE_STATUS, " +
                   "EMPLOYEE_IMAGE " +
                   " FROM VEW_TEAM_LEADER_HIERARCHY WHERE HEAD_OFFICE_ID = '" + objTeamLeaderHierarchyModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objTeamLeaderHierarchyModel.BranchOfficeId + "'    ";



            if (!string.IsNullOrWhiteSpace(objTeamLeaderHierarchyModel.SearchBy))
            {
                sql = sql + "AND (LOWER(TEAM_LEADER_NAME) LIKE LOWER( '%" + objTeamLeaderHierarchyModel.SearchBy.Trim() + "%')  OR UPPER(TEAM_LEADER_NAME)LIKE UPPER('%" + objTeamLeaderHierarchyModel.SearchBy.Trim() + "%') OR LOWER(EMPLOYEE_ID) LIKE LOWER( '%" + objTeamLeaderHierarchyModel.SearchBy.Trim() + "%')  OR UPPER(EMPLOYEE_ID)LIKE UPPER('%" + objTeamLeaderHierarchyModel.SearchBy.Trim() + "%'))";

            }

            sql = sql + " ORDER BY SL ";

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

        //mezba 13
        public string TeamLeaderHierarchyEmployeeSave(TeamLeaderHierarchyModel objTeamLeaderHierarchyModel)
        {
            string strMsg = null;


            foreach (TeamLeaderHierarchyModel model in objTeamLeaderHierarchyModel.EmployeeList)
            {


                OracleCommand objOracleCommand = new OracleCommand { CommandText = "PRO_TEAM_LEADER_Hierarchy_SAVE", CommandType = CommandType.StoredProcedure };



                objOracleCommand.Parameters.Add("P_TEAM_LEADER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTeamLeaderHierarchyModel.TeamLeaderId;
                objOracleCommand.Parameters.Add("p_sub_ordinate_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.EmployeeId) ? model.EmployeeId : null;
                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTeamLeaderHierarchyModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTeamLeaderHierarchyModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTeamLeaderHierarchyModel.BranchOfficeId;

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
                        trans.Rollback();
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        trans.Dispose();
                        strConn.Close();
                    }
                }

            }

            return strMsg;
        }
        #endregion

        #region Employee Movement Register Request

        public MovementRegisterModel GetEmployeeDetailsMovementRegister(MovementRegisterModel objMovementRequest)
        {
            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy') JOINING_DATE, " +
                  "DESIGNATION_NAME, " +
                  "employee_pic " +

                  " FROM VEW_PERSONAL_INFO where head_office_id = '" + objMovementRequest.HeadOfficeId +
                  "' and branch_office_id = '" + objMovementRequest.BranchOfficeId + "' ";

            if (objMovementRequest.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objMovementRequest.EmployeeId + "' ";
            }


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                MovementRegisterModel objMovement = null;
                try
                {
                    while (objReader.Read())
                    {
                        objMovement = new MovementRegisterModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["sl"]).ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            EmployeeImage = objReader["employee_pic"] == DBNull.Value ? new byte[0] : (byte[])objReader["employee_pic"]
                        };

                    }
                    objReader.Close();
                    objConnection.Close();
                    return objMovement;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
            }
        }


        public MovementRegisterModel GetEmployeeDetailsMovementRegisterbyId(MovementRegisterModel objMovementRequest)  //for edit movement register
        {
            string sql = "";

            sql = "SELECT " +
                  "tran_id, " +
                  "FIRST_IN, " +
                  "LAST_OUT, " +
                  "TO_CHAR(log_date,'dd/mm/yyyy')log_date, " +
                  "REMARKS, " +
                  "MOVEMENT_TYPE_ID " +

                  " FROM INDIVIDUAL_MOVEMENT_RESISTER where  head_office_id = '" + objMovementRequest.HeadOfficeId + "' and branch_office_id = '" + objMovementRequest.BranchOfficeId + "' AND  employee_id = '" + objMovementRequest.EmployeeId + "' ";


            if (objMovementRequest.TranId != null)
            {

                sql = sql + "and tran_id = '" + objMovementRequest.TranId + "' ";
            }

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                MovementRegisterModel objMovementRegModel = null;
                try
                {
                    while (objReader.Read())
                    {
                        objMovementRegModel = new MovementRegisterModel
                        {
                            TranId = objReader["tran_id"].ToString(),
                            FirstIn = objReader["FIRST_IN"].ToString(),
                            LastOut = objReader["LAST_OUT"].ToString(),
                            LogDate = objReader["log_date"].ToString(),
                            Remarks = objReader["REMARKS"].ToString(),
                            MovementTypeId = objReader["MOVEMENT_TYPE_ID"].ToString()
                        };
                    }
                    objReader.Close();
                    objConnection.Close();
                    return objMovementRegModel;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
            }
        }

        public List<MovementRegisterModel> GetMovementRegisterRecord(MovementRegisterModel objMovementRegModel)   // M
        {
            List<MovementRegisterModel> listMovementRegister = new List<MovementRegisterModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  //"EMPLOYEE_ID, " +
                  //"EMPLOYEE_NAME, " +
                  //"DESIGNATION_NAME, " +
                  //"TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "FIRST_IN, " +
                  "LAST_OUT, " +
                  "TO_CHAR(log_date,'dd/mm/yyyy')log_date, " +
                  "day_name, " +
                  "department_name, " +
                  "REMARKS, " +
                  "MOVEMENT_TYPE_ID, " +
                  "MOVEMENT_TYPE_NAME, " +
                  "HR_APPROVE_STATUS, " +
                  "TEAM_LEADER_APPROVE_STATUS, " +
                  "TRAN_ID, " +
                  "TC_TEAM_LEADER_STATUS, " +
                  "TC_HR_STATUS " +

                  " FROM VEW_EMP_MOVEMENT_REGISTER where head_office_id = '" + objMovementRegModel.HeadOfficeId + "' and branch_office_id = '" + objMovementRegModel.BranchOfficeId + "' AND  employee_id = '" + objMovementRegModel.EmployeeId + "' and log_date between to_Date('" + objMovementRegModel.ToDate + "', 'dd/mm/yyyy') and to_Date('" + objMovementRegModel.FromDate + "', 'dd/mm/yyyy') ";


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        objMovementRegModel = new MovementRegisterModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["SL"]).ToString(),
                            FirstIn = objReader["FIRST_IN"].ToString(),
                            LastOut = objReader["LAST_OUT"].ToString(),
                            //LogDate = objReader["log_date"].ToString(),
                            DayName = objReader["day_name"].ToString(),
                            LogDate = objReader["log_date"].ToString(),
                            Remarks = objReader["REMARKS"].ToString(),
                            MovementTypeId = objReader["MOVEMENT_TYPE_ID"].ToString(),
                            MovementTypeName = objReader["MOVEMENT_TYPE_NAME"].ToString(),
                            HrApproveStatus = objReader["HR_APPROVE_STATUS"].ToString(),
                            TeamLeaderApproveStatus = objReader["TEAM_LEADER_APPROVE_STATUS"].ToString(),
                            TranId = objReader["Tran_Id"].ToString(),
                            TimeChangeByTl = objReader["TC_TEAM_LEADER_STATUS"].ToString(),
                            TimeChangeByHr = objReader["TC_HR_STATUS"].ToString()
                        };

                        listMovementRegister.Add(objMovementRegModel);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }

            return listMovementRegister;
        }


        public string SaveIndividualMovementRegister(MovementRegisterModel objMovementRegModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("PRO_SAVE_EMP_MOVEMENT_REGISTER")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objMovementRegModel.TranId != "")
            {
                objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegModel.TranId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objMovementRegModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objMovementRegModel.LogDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegModel.LogDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objMovementRegModel.MovementTypeId != "")
            {
                objOracleCommand.Parameters.Add("P_MOVEMENT_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegModel.MovementTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_MOVEMENT_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objMovementRegModel.FirstIn != "")
            {
                objOracleCommand.Parameters.Add("P_FIRST_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegModel.FirstIn;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FIRST_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objMovementRegModel.LastOut != "")
            {
                objOracleCommand.Parameters.Add("P_LAST_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegModel.LastOut;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LAST_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objMovementRegModel.Remarks != "")
            {
                objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegModel.Remarks;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objMovementRegModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objMovementRegModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objMovementRegModel.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);

                }

                finally
                {
                    strConn.Close();
                }

            }
            return strMsg;
        }


        public string DeleteIndividualMovementRegister(MovementRegisterModel objMovementRegModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("PRO_DELETE_EMP_MR")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objMovementRegModel.TranId != "")
            {
                objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegModel.TranId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objMovementRegModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objMovementRegModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objMovementRegModel.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);

                }

                finally
                {
                    strConn.Close();
                }

            }
            return strMsg;
        }


        #endregion

        #region Movement Register Approve/ Pending List for Team Leader

        public List<MovementRegisterModel> GetMovementRegisterPendingListForTeamLeader(MovementRegisterModel objMovementRegisterModel)
        {

            List<MovementRegisterModel> pendingListForTeamLeader = new List<MovementRegisterModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum SL, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(log_date,'dd/mm/yyyy')log_date, " +
                  "DAY_NAME, " +
                  "DESIGNATION_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "DEPARTMENT_NAME, " +
                  "MOVEMENT_TYPE_NAME, " +
                  "FIRST_IN, " +
                  "FIRST_IN_TL, " +
                  "LAST_OUT, " +
                  "LAST_OUT_TL, " +
                  "REMARKS, " +
                  "TEAM_LEADER_APPROVE_YN, " +
                  "TEAM_LEADER_REMARKS, " +
                  "TC_TEAM_LEADER_STATUS, " +
                  "TEAM_LEADER_APPROVE_STATUS, " +
                  "TRAN_ID " +
                  " FROM VEW_EMP_MOVEMENT_REGISTER where head_office_id = '" + objMovementRegisterModel.HeadOfficeId + "' and branch_office_id = '" + objMovementRegisterModel.BranchOfficeId + "' and log_date between to_Date('" + objMovementRegisterModel.ToDate + "', 'dd/mm/yyyy') and to_Date('" + objMovementRegisterModel.FromDate + "', 'dd/mm/yyyy') and HR_APPROVE_YN = 'N' AND TEAM_LEADER_APPROVE_YN = 'N'  ";

            if (!string.IsNullOrWhiteSpace(objMovementRegisterModel.EmployeeId))
            {
                sql = sql + "and employee_id = '" + objMovementRegisterModel.EmployeeId + "' ";
            }

            sql = sql + " order by SL";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();


                try
                {
                    while (objReader.Read())
                    {
                        MovementRegisterModel objPendingListforTeamLead = new MovementRegisterModel
                        {
                            SerialNumber = objReader["SL"].ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),
                            LogDate = objReader["LOG_DATE"].ToString(),
                            DayName = objReader["DAY_NAME"].ToString(),
                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),

                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),
                            MovementTypeName = objReader["MOVEMENT_TYPE_NAME"].ToString(),

                            FirstIn = objReader["FIRST_IN"].ToString(),
                            FirstInTl = objReader["FIRST_IN_TL"].ToString(),

                            LastOut = objReader["LAST_OUT"].ToString(),
                            LastOutTl = objReader["LAST_OUT_TL"].ToString(),

                            Remarks = objReader["REMARKS"].ToString(),
                            TeamLeaderApproveYesNo = objReader["TEAM_LEADER_APPROVE_YN"].ToString(),
                            TeamLeaderRemarks = objReader["TEAM_LEADER_REMARKS"].ToString(),

                            TimeChangeByTl = objReader["TC_TEAM_LEADER_STATUS"].ToString(),
                            TeamLeaderApproveStatus = objReader["TEAM_LEADER_Approve_STATUS"].ToString(),
                            TranId = objReader["TRAN_ID"].ToString()
                        };
                        pendingListForTeamLeader.Add(objPendingListforTeamLead);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }
            return pendingListForTeamLeader;
        }

        public string ApproveMovementRegisterByTeamLeader(MovementRegisterModel objMovementModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_movement_approval_by_tl")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objMovementModel.TranId != "")
            {
                objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementModel.TranId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objMovementModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objMovementModel.LogDate != "")
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementModel.LogDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objMovementModel.TeamLeaderRemarks != "")
            {
                objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementModel.TeamLeaderRemarks;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objMovementModel.FirstInTl != "")
            {
                objOracleCommand.Parameters.Add("P_FIRST_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementModel.FirstInTl;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FIRST_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objMovementModel.LastOutTl != "")
            {
                objOracleCommand.Parameters.Add("P_LAST_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementModel.LastOutTl;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LAST_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objMovementModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objMovementModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objMovementModel.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    strConn.Close();
                }

            }
            return strMsg;
        }


        public List<MovementRegisterModel> GetMovementRegisterApprovedListForTeamLealder(MovementRegisterModel objMovementModel)
        {

            List<MovementRegisterModel> listApprovedMovement = new List<MovementRegisterModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "DESIGNATION_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "department_name, " +

                  "FIRST_IN, " +
                  "LAST_OUT, " +
                  "TO_CHAR(log_date,'dd/mm/yyyy')log_date, " +
                  "day_name, " +
                  "MOVEMENT_TYPE_NAME, " +
                  "REMARKS, " +

                  "FIRST_IN_TL, " +
                  "LAST_OUT_TL, " +

                  "TEAM_LEADER_REMARKS," +
                  //"TC_Team_Leader_STATUS, " +
                  "TEAM_LEADER_Approve_STATUS" +

                  " FROM VEW_EMP_MOVEMENT_REGISTER where head_office_id = '" + objMovementModel.HeadOfficeId + "' and branch_office_id = '" + objMovementModel.BranchOfficeId + "' and log_date between to_Date('" + objMovementModel.ToDate + "', 'dd/mm/yyyy') and to_Date('" + objMovementModel.FromDate + "', 'dd/mm/yyyy') and Team_Leader_APPROVE_YN = 'Y' and team_leader_id = '" + objMovementModel.UpdateBy + "' ";


            if (objMovementModel.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objMovementModel.EmployeeId + "' ";
            }
            sql = sql + " order by SL";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();


                try
                {
                    while (objReader.Read())
                    {
                        MovementRegisterModel objPendingListforTeamLead = new MovementRegisterModel
                        {
                            SerialNumber = objReader["SL"].ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),

                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),

                            FirstIn = objReader["FIRST_IN"].ToString(),
                            LastOut = objReader["LAST_OUT"].ToString(),
                            LogDate = objReader["LOG_DATE"].ToString(),
                            DayName = objReader["DAY_NAME"].ToString(),
                            MovementTypeName = objReader["MOVEMENT_TYPE_NAME"].ToString(),
                            Remarks = objReader["REMARKS"].ToString(),

                            LastOutTl = objReader["LAST_OUT_TL"].ToString(),
                            FirstInTl = objReader["FIRST_IN_TL"].ToString(),

                            TeamLeaderRemarks = objReader["TEAM_LEADER_REMARKS"].ToString(),
                            //TimeChangeByTl = objReader["TC_Team_Leader_STATUS"].ToString(),
                            TeamLeaderApproveStatus = objReader["TEAM_LEADER_Approve_STATUS"].ToString()
                        };
                        listApprovedMovement.Add(objPendingListforTeamLead);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }
            return listApprovedMovement;

        }


        #endregion

        #region Employee Movement Register Approve/ Pending List for Hr

        public List<MovementRegisterModel> GetEmployeeMovementRegisterPendingListforHr(MovementRegisterModel objMovementRegisterModel)
        {
            List<MovementRegisterModel> listMovementRegforHr = new List<MovementRegisterModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum SL, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(log_date,'dd/mm/yyyy')log_date, " +
                  "DAY_NAME, " +
                  "DESIGNATION_ID, " +
                  "DESIGNATION_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "UNIT_ID, " +
                  "UNIT_NAME, " +
                  "DEPARTMENT_ID, " +
                  "DEPARTMENT_NAME, " +
                  "SECTION_ID, " +
                  "SECTION_NAME, " +
                  "SUB_SECTION_ID, " +
                  "SUB_SECTION_NAME, " +
                  "MOVEMENT_TYPE_ID, " +
                  "MOVEMENT_TYPE_NAME, " +
                  "FIRST_IN, " +
                  "LAST_OUT, " +
                  "REMARKS, " +
                  "TEAM_LEADER_APPROVE_YN, " +
                  "HR_APPROVE_YN, " +
                  "HR_APPROVE_STATUS, " +
                  "TEAM_LEADER_REMARKS, " +
                  "HR_REMARKS, " +
                  "SUBMIT_YN, " +
                  "TO_CHAR(approved_date_hr,'dd/mm/yyyy')approved_date_hr, " +
                  "FIRST_IN_TL, " +
                  "LAST_OUT_TL, " +
                  "FIRST_IN_HR, " +
                  "LAST_OUT_HR, " +
                  "TC_TEAM_LEADER_STATUS, " +
                  "TC_HR_STATUS, " +
                  "TEAM_LEADER_APPROVE_STATUS, " +
                  "TRAN_ID " +
                  " FROM VEW_EMP_MOVEMENT_REGISTER where head_office_id = '" + objMovementRegisterModel.HeadOfficeId + "' and branch_office_id = '" + objMovementRegisterModel.BranchOfficeId + "' and log_date between to_Date('" + objMovementRegisterModel.ToDate + "', 'dd/mm/yyyy') and to_Date('" + objMovementRegisterModel.FromDate + "', 'dd/mm/yyyy') and HR_APPROVE_YN = 'N' AND TEAM_LEADER_APPROVE_YN = 'Y'  ";

            if (!string.IsNullOrWhiteSpace(objMovementRegisterModel.EmployeeId))
            {

                sql = sql + "and employee_id = '" + objMovementRegisterModel.EmployeeId + "' ";
            }


            sql = sql + " order by SL";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();


                try
                {
                    while (objReader.Read())
                    {
                        MovementRegisterModel objPendingListforTeamLead = new MovementRegisterModel
                        {
                            SerialNumber = objReader["SL"].ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),

                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),

                            FirstIn = objReader["FIRST_IN"].ToString(),
                            LastOut = objReader["LAST_OUT"].ToString(),
                            LogDate = objReader["LOG_DATE"].ToString(),
                            DayName = objReader["DAY_NAME"].ToString(),
                            MovementTypeName = objReader["MOVEMENT_TYPE_NAME"].ToString(),
                            Remarks = objReader["REMARKS"].ToString(),

                            LastOutTl = objReader["LAST_OUT_TL"].ToString(),
                            FirstInTl = objReader["FIRST_IN_TL"].ToString(),

                            TeamLeaderRemarks = objReader["TEAM_LEADER_REMARKS"].ToString(),
                            TimeChangeByTl = objReader["TC_Team_Leader_STATUS"].ToString(),
                            TeamLeaderApproveStatus = objReader["TEAM_LEADER_Approve_STATUS"].ToString(),
                            TranId = objReader["TRAN_ID"].ToString()
                        };
                        listMovementRegforHr.Add(objPendingListforTeamLead);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }
            return listMovementRegforHr;


        }

        public string ApproveMovementRegisterHr(MovementRegisterModel objMovementRegisterModelHr)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_movement_approval_by_hr");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objMovementRegisterModelHr.TranId != "")
            {
                objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegisterModelHr.TranId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objMovementRegisterModelHr.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegisterModelHr.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objMovementRegisterModelHr.LogDate != "")
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegisterModelHr.LogDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objMovementRegisterModelHr.HrRemarks != "")
            {
                objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegisterModelHr.HrRemarks;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objMovementRegisterModelHr.FirstInHr != "")
            {
                objOracleCommand.Parameters.Add("P_FIRST_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegisterModelHr.FirstInHr;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FIRST_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objMovementRegisterModelHr.LastOutHr != "")
            {
                objOracleCommand.Parameters.Add("P_LAST_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMovementRegisterModelHr.LastOutHr;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_LAST_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objMovementRegisterModelHr.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objMovementRegisterModelHr.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objMovementRegisterModelHr.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }




            return strMsg;
        }

        public List<MovementRegisterModel> GetEmployeeMovementRegisterApprovedListforHr(MovementRegisterModel objMovementRegisterModel)
        {
            List<MovementRegisterModel> listMovementRegforHr = new List<MovementRegisterModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "DESIGNATION_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "department_name, " +

                  "FIRST_IN, " +
                  "LAST_OUT, " +
                  "TO_CHAR(log_date,'dd/mm/yyyy')log_date, " +
                  "day_name, " +
                  "REMARKS, " +
                  "MOVEMENT_TYPE_NAME, " +

                  "FIRST_IN_TL, " +
                  "LAST_OUT_TL, " +
                  "Team_Leader_Remarks, " +

                  "TEAM_LEADER_Approve_STATUS," +

                  "LAST_OUT_HR, " +
                  "FIRST_IN_HR, " +
                  "HR_APPROVE_STATUS, " +
                  "HR_REMARKS, " +
                  "TC_HR_STATUS, " +
                  "TO_CHAR(approved_date_hr,'dd/mm/yyyy')approved_date_hr " +


                  " FROM VEW_EMP_MOVEMENT_REGISTER where head_office_id = '" + objMovementRegisterModel.HeadOfficeId + "' and branch_office_id = '" + objMovementRegisterModel.BranchOfficeId + "' and log_date between to_Date('" + objMovementRegisterModel.ToDate + "', 'dd/mm/yyyy') and to_Date('" + objMovementRegisterModel.FromDate + "', 'dd/mm/yyyy') and HR_APPROVE_YN = 'Y' and submit_yn = 'Y' ";

            if (!string.IsNullOrWhiteSpace(objMovementRegisterModel.EmployeeId))
            {

                sql = sql + "and employee_id = '" + objMovementRegisterModel.EmployeeId + "' ";
            }


            sql = sql + " order by SL";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();


                try
                {
                    while (objReader.Read())
                    {
                        MovementRegisterModel objPendingListforTeamLead = new MovementRegisterModel
                        {
                            SerialNumber = objReader["SL"].ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),

                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),

                            FirstIn = objReader["FIRST_IN"].ToString(),
                            LastOut = objReader["LAST_OUT"].ToString(),
                            LogDate = objReader["LOG_DATE"].ToString(),
                            DayName = objReader["DAY_NAME"].ToString(),
                            MovementTypeName = objReader["MOVEMENT_TYPE_NAME"].ToString(),
                            Remarks = objReader["REMARKS"].ToString(),

                            LastOutTl = objReader["LAST_OUT_TL"].ToString(),
                            FirstInTl = objReader["FIRST_IN_TL"].ToString(),
                            TeamLeaderRemarks = objReader["TEAM_LEADER_REMARKS"].ToString(),
                            TeamLeaderApproveStatus = objReader["TEAM_LEADER_Approve_STATUS"].ToString(),

                            LastOutHr = objReader["LAST_OUT_HR"].ToString(),
                            FirstInHr = objReader["FIRST_IN_HR"].ToString(),
                            HrRemarks = objReader["HR_REMARKS"].ToString(),
                            HrApproveStatus = objReader["HR_APPROVE_STATUS"].ToString()

                        };
                        listMovementRegforHr.Add(objPendingListforTeamLead);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }
            return listMovementRegforHr;


        }

        #endregion

        #region Employee Provident Fund


        //mezba 13
        public DataTable LoadEmployeeDataProvidentFund(ProvidentFundModel objProvidentFundModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "rownum SL, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                   "DESIGNATION_NAME, " +
                   "DEPARTMENT_NAME, " +
                   "UNIT_NAME, " +
                   "SECTION_NAME, " +
                   "SUB_SECTION_NAME, " +
                   "GRADE_NO, " +
                   "ACTIVE_YN, " +
                   "ACTIVE_STATUS, " +
                   "EMPLOYEE_IMAGE_NAME, " +
                   "EMPLOYEE_IMAGE_SIZE " +
                   " FROM VEW_EMP_RECORD_SEARCH WHERE HEAD_OFFICE_ID = '" + objProvidentFundModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objProvidentFundModel.BranchOfficeId + "' AND ACTIVE_YN = '" + objProvidentFundModel.SearchInactiveYesNo + "' ";



            if (!string.IsNullOrWhiteSpace(objProvidentFundModel.SearchEmployeeId))
            {

                sql = sql + "AND EMPLOYEE_ID = '" + objProvidentFundModel.SearchEmployeeId.Trim() + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objProvidentFundModel.SearchEmployeeName))
            {

                sql = sql + "AND (LOWER(EMPLOYEE_NAME) LIKE LOWER( '%" + objProvidentFundModel.SearchEmployeeName.Trim() + "%')  OR UPPER (EMPLOYEE_NAME)LIKE UPPER('%" + objProvidentFundModel.SearchEmployeeName.Trim() + "%') )";
            }


            if (!string.IsNullOrWhiteSpace(objProvidentFundModel.SearchCardNo))
            {

                sql = sql + "AND CARD_NO = '" + objProvidentFundModel.SearchCardNo.Trim() + "' ";
            }


            if (!string.IsNullOrWhiteSpace(objProvidentFundModel.SearchUnitId))
            {

                sql = sql + "AND UNIT_ID = '" + objProvidentFundModel.SearchUnitId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objProvidentFundModel.SearchDepartmentId))
            {

                sql = sql + "AND DEPARTMENT_ID = '" + objProvidentFundModel.SearchDepartmentId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objProvidentFundModel.SearchSectionId))
            {

                sql = sql + "AND SECTION_ID = '" + objProvidentFundModel.SearchSectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objProvidentFundModel.SearchSubSectionId))
            {

                sql = sql + "AND SUB_SECTION_ID = '" + objProvidentFundModel.SearchSubSectionId + "' ";
            }



            if (!string.IsNullOrWhiteSpace(objProvidentFundModel.SearchPunchCode))
            {

                sql = sql + "AND PUNCH_CODE = '" + objProvidentFundModel.SearchPunchCode.Trim() + "' ";
            }


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

        public ProvidentFundModel SearchEmployeeInformationProvidentFund(ProvidentFundModel objProvidentFundModel)
        {

            string sql = "";
            sql = "SELECT " +
                    "TO_CHAR (NVL (EMPLOYEE_ID, 'N/A')), " +
                    "TO_CHAR (NVL (EMPLOYEE_NAME, 'N/A')), " +
                    "NVL (TO_CHAR (JOINING_DATE, 'dd/mm/yyyy'), ' '), " +
                    "TO_CHAR (NVL (PRESENT_DESIGNATION_NAME, 'N/A')), " +
                    "EMPLOYEEE_PIC " +
                    " FROM VEW_EMPLOYEE_PROFILE where employee_id = '" + objProvidentFundModel.EmployeeId + "' AND head_office_id = '" + objProvidentFundModel.HeadOfficeId + "' AND branch_office_id = '" + objProvidentFundModel.BranchOfficeId + "' ";



            using (OracleConnection strConn = GetConnection())
            {
                strConn.Open();
                OracleCommand objCommand = new OracleCommand(sql, strConn);
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    if (objDataReader != null && objDataReader.HasRows)
                    {
                        objDataReader.Read();


                        objProvidentFundModel.EmployeeId = objDataReader.GetString(0);
                        objProvidentFundModel.EmployeeName = objDataReader.GetString(1);
                        objProvidentFundModel.JoiningDate = objDataReader.GetString(2);
                        objProvidentFundModel.Designation = objDataReader.GetString(3);
                        objProvidentFundModel.EmployeeImage = objDataReader[4] == DBNull.Value ? new byte[0] : (byte[])objDataReader[4];
                        objProvidentFundModel.EmployeeImageBase64 = Convert.ToBase64String(objProvidentFundModel.EmployeeImage);

                        objDataReader.Close();
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

            return objProvidentFundModel;
        }

        public string SaveEmployeeProfidentFundInformation(ProvidentFundModel objProvidentFundModel)
        {

            string strMsg = "";

            if (!string.IsNullOrWhiteSpace(objProvidentFundModel.NomineeName) && (!string.IsNullOrWhiteSpace(objProvidentFundModel.Percentage)))
            {

                string[] arrayNomineeName = objProvidentFundModel.NomineeName.Split('~');
                string[] arrayNomineeAddress = objProvidentFundModel.NomineeAddress.Split('~');
                string[] arrayNomineeRelation = objProvidentFundModel.NomineeRelation.Split('~');
                string[] arrayMon = objProvidentFundModel.Mon.Split('~');



                string[] arrayUnderAge = new string[100];
                if (!string.IsNullOrWhiteSpace(objProvidentFundModel.UnderAge))
                {
                    arrayUnderAge = objProvidentFundModel.UnderAge.Contains("~") ? objProvidentFundModel.UnderAge.Split('~') : objProvidentFundModel.UnderAge.Split();
                }


                string[] arrayHandiCap = new string[100];
                if (!string.IsNullOrWhiteSpace(objProvidentFundModel.HandiCap))
                {
                    arrayHandiCap = objProvidentFundModel.HandiCap.Contains("~") ? objProvidentFundModel.HandiCap.Split('~') : objProvidentFundModel.HandiCap.Split();
                }

                string[] arrayTranId = new string[100];
                if (!string.IsNullOrWhiteSpace(objProvidentFundModel.TranId))
                {
                    arrayTranId = objProvidentFundModel.TranId.Contains("~") ? objProvidentFundModel.TranId.Split('~') : objProvidentFundModel.TranId.Split();
                }


                string[] arrayPercentage = objProvidentFundModel.Percentage.Split('~');
                string[] arrayGuardianName = objProvidentFundModel.GuardianName.Split('~');
                string[] arrayGuardianAddress = objProvidentFundModel.GuardianAddress.Split('~');



                int x = arrayPercentage.Count();
                for (int i = 0; i < x; i++)
                {
                    var nomineeName = arrayNomineeName[i];
                    var nomineeAddress = arrayNomineeAddress[i];
                    var nomineeRelation = arrayNomineeRelation[i];
                    var non = arrayMon[i];
                    var underAge = arrayUnderAge[i];
                    var handiCap = arrayHandiCap[i];
                    var tranId = arrayTranId[i];

                    var age = "";
                    var handi = "";

                    age = underAge == "Y" ? "Y" : "N";
                    handi = handiCap == "Y" ? "Y" : "N";

                    var percentage = arrayPercentage[i];
                    var guardianName = arrayGuardianName[i];
                    var guardianAddress = arrayGuardianAddress[i];

                    OracleTransaction objOracleTransaction = null;
                    OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_PROVIDENT_FUND");
                    objOracleCommand.CommandType = CommandType.StoredProcedure;

                    objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProvidentFundModel.EmployeeId ?? null;
                    objOracleCommand.Parameters.Add("P_NOMINEE_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = nomineeName ?? null;
                    objOracleCommand.Parameters.Add("P_NOMINEE_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = nomineeAddress ?? null;
                    objOracleCommand.Parameters.Add("P_RELATIONSHIP_WITH_NOMINEE", OracleDbType.Varchar2, ParameterDirection.Input).Value = nomineeRelation ?? null;
                    objOracleCommand.Parameters.Add("P_ADULT_YN", OracleDbType.Varchar2, ParameterDirection.Input).Value = age ?? null;
                    objOracleCommand.Parameters.Add("P_HANDICAP_YN", OracleDbType.Varchar2, ParameterDirection.Input).Value = handi ?? null;
                    objOracleCommand.Parameters.Add("P_NO_OF_NOMINEE", OracleDbType.Varchar2, ParameterDirection.Input).Value = non ?? null;
                    objOracleCommand.Parameters.Add("P_NOMINEE_PERCENTAGE", OracleDbType.Varchar2, ParameterDirection.Input).Value = percentage ?? null;
                    objOracleCommand.Parameters.Add("P_NOMINEE_GUARDIAN_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = guardianName ?? null;
                    objOracleCommand.Parameters.Add("P_NOMINEE_GUARDIAN_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = guardianAddress ?? null;
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = tranId ?? null;
                    objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProvidentFundModel.UpdateBy;
                    objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProvidentFundModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProvidentFundModel.BranchOfficeId;

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

                }



            }

            return strMsg;
        }

        public DataTable ShowEmployeProvidentRecord(ProvidentFundModel objProvidentFundModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "NOMINEE_NAME, " +
                  "NOMINEE_ADDRESS, " +
                  "RELATIONSHIP_WITH_NOMINEE, " +
                  "ADULT_YN, " +
                  "HANDICAP_YN, " +
                  "NO_OF_NOMINEE, " +
                  "NOMINEE_PERCENTAGE, " +
                  "NOMINEE_GUARDIAN_NAME, " +
                  "NOMINEE_GUARDIAN_ADDRESS, " +
                  "TRAN_ID " +
                  " FROM VEW_EMPLOYEE_PROVIDENT_FUND where head_office_id = '" + objProvidentFundModel.HeadOfficeId + "' and branch_office_id = '" + objProvidentFundModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objProvidentFundModel.EmployeeId))
            {

                sql = sql + "and employee_id = '" + objProvidentFundModel.EmployeeId + "' ";
            }


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

        public string DeleteEmployeeProfidentFundInformation(ProvidentFundModel objProvidentFundModel)
        {

            string strMsg = "";

            if (!string.IsNullOrWhiteSpace(objProvidentFundModel.DeleteEmployeeId) && (!string.IsNullOrWhiteSpace(objProvidentFundModel.DeleteTranId)))
            {

                string[] deleteTranIdArray = objProvidentFundModel.DeleteTranId.Split(',');

                int x = deleteTranIdArray.Count();
                for (int i = 0; i < x; i++)
                {
                    var tranId = deleteTranIdArray[i];

                    OracleTransaction objOracleTransaction = null;
                    OracleCommand objOracleCommand = new OracleCommand("PRO_EMP_PROVIDENT_FUND_DELETE");
                    objOracleCommand.CommandType = CommandType.StoredProcedure;

                    objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProvidentFundModel.DeleteEmployeeId ?? null;
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = tranId ?? null;
                    objOracleCommand.Parameters.Add("P_UPDATE_BY", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProvidentFundModel.UpdateBy;
                    objOracleCommand.Parameters.Add("P_HEAD_OFFICE_ID", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProvidentFundModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("P_BRANCH_OFFICE_ID", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProvidentFundModel.BranchOfficeId;
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

                }



            }

            return strMsg;
        }


        #endregion


        #region Employee File Upload

        public List<EmployeeFileUploadModel> LoadEmployeeRecordForJd(EmployeeFileUploadModel objEmployeeFileUpload)
        {

            List<EmployeeFileUploadModel> listEmployeeJd = new List<EmployeeFileUploadModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                  "PRESENT_DESIGNATION_ID, " +
                  "DESIGNATION_NAME, " +
                  "DEPARTMENT_ID, " +
                  "DEPARTMENT_NAME, " +
                  "UNIT_ID, " +
                  "UNIT_NAME, " +
                  "SECTION_ID, " +
                  "SECTION_NAME, " +
                  "GRADE_NO, " +
                  "ACTIVE_YN, " +
                  "active_status, " +
                  "EMPLOYEE_IMAGE, " +
                  "SUB_SECTION_NAME " +


                  " FROM VEW_SEARCH_EMP_UPLOAD_DOC where head_office_id = '" + objEmployeeFileUpload.HeadOfficeId + "' and branch_office_id = '" + objEmployeeFileUpload.BranchOfficeId + "' AND active_yn = '" + objEmployeeFileUpload.ActiveYn + "'   ";

            if (objEmployeeFileUpload.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objEmployeeFileUpload.EmployeeId + "' ";
            }

            if (objEmployeeFileUpload.DepartmentId != null)
            {

                sql = sql + "and department_id = '" + objEmployeeFileUpload.DepartmentId + "' ";
            }

            if (objEmployeeFileUpload.UnitId != null)
            {

                sql = sql + "and unit_id = '" + objEmployeeFileUpload.UnitId + "' ";
            }

            if (objEmployeeFileUpload.SubSectionId != null)
            {

                sql = sql + "and sub_section_id = '" + objEmployeeFileUpload.SubSectionId + "' ";
            }

            if (objEmployeeFileUpload.SectionId != null)
            {

                sql = sql + "and section_id = '" + objEmployeeFileUpload.SectionId + "' ";
            }


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();


                try
                {
                    while (objReader.Read())
                    {
                        EmployeeFileUploadModel objEmployeeJd = new EmployeeFileUploadModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["SL"]).ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString()
                        };
                        objEmployeeJd.DepartmentName = objReader["DEPARTMENT_NAME"].ToString();
                        //objEmployeeJd.JdFileName = objReader["FILE_NAME"].ToString();
                        //objEmployeeJd.JdFileSize = objReader["FILE_SIZE"].ToString();
                        //objEmployeeJd.JdFileExtension = objReader["FILE_EXTENSION"].ToString();


                        listEmployeeJd.Add(objEmployeeJd);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }
            return listEmployeeJd;

        }


        public List<EmployeeFileUploadModel> LoadEmployeeRecordForAl(EmployeeFileUploadModel objEmployeeFileUpload)
        {

            List<EmployeeFileUploadModel> listEmployeeJd = new List<EmployeeFileUploadModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "DESIGNATION_NAME, " +
                  "DEPARTMENT_NAME " +


                  //"EMPLOYEE_IMAGE, " +
                  //"SUB_SECTION_NAME, " +
                  //"EMP_AL, " +
                  //"FILE_NAME, " +
                  //"FILE_SIZE, " +
                  //"FILE_EXTENSION " +

                  " FROM VEW_SEARCH_EMP_UPLOAD_DOC where head_office_id = '" + objEmployeeFileUpload.HeadOfficeId + "' and branch_office_id = '" + objEmployeeFileUpload.BranchOfficeId + "' /*AND active_yn = '" + objEmployeeFileUpload.ActiveYn + "'*/   ";

            if (objEmployeeFileUpload.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objEmployeeFileUpload.EmployeeId + "' ";
            }

            if (objEmployeeFileUpload.DepartmentId != null)
            {

                sql = sql + "and department_id = '" + objEmployeeFileUpload.DepartmentId + "' ";
            }

            if (objEmployeeFileUpload.UnitId != null)
            {

                sql = sql + "and unit_id = '" + objEmployeeFileUpload.UnitId + "' ";
            }

            if (objEmployeeFileUpload.SubSectionId != null)
            {

                sql = sql + "and sub_section_id = '" + objEmployeeFileUpload.SubSectionId + "' ";
            }

            if (objEmployeeFileUpload.SectionId != null)
            {

                sql = sql + "and section_id = '" + objEmployeeFileUpload.SectionId + "' ";
            }


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        EmployeeFileUploadModel objEmployeeJd = new EmployeeFileUploadModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["SL"]).ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),

                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString()
                        };
                        //objEmployeeJd.AlFileName = objReader["FILE_NAME"].ToString();
                        //objEmployeeJd.AlFileSize = objReader["FILE_SIZE"].ToString();
                        //objEmployeeJd.AlFileExtension = objReader["FILE_EXTENSION"].ToString();


                        listEmployeeJd.Add(objEmployeeJd);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }
            return listEmployeeJd;

        }

        public string SaveEmpJd(EmployeeFileUploadModel objFileUpload)
        {

            string strMsg = "";
            OracleCommand objOracleCommand = new OracleCommand("pro_employee_jd_save")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objFileUpload.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objFileUpload.JdFileName != "")
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.JdFileName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objFileUpload.JdFileSize != null)
            {
                byte[] array = System.Convert.FromBase64String(objFileUpload.JdFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }

            if (objFileUpload.JdFileExtension != "")
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.JdFileExtension;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }




            return strMsg;
        }

        public string SaveEmpJl(EmployeeFileUploadModel objFileUpload)
        {

            string strMsg = "";

            //OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_employee_jl_save")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objFileUpload.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objFileUpload.JlFileName != "")
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.JlFileName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objFileUpload.JlFileSize != null)
            {
                byte[] array = System.Convert.FromBase64String(objFileUpload.JlFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }

            if (objFileUpload.JlFileExtension != "")
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.JlFileExtension;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }




            return strMsg;
        }

        public string SaveEmpAl(EmployeeFileUploadModel objFileUpload)
        {

            string strMsg = "";
            OracleCommand objOracleCommand = new OracleCommand("pro_employee_al_save")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objFileUpload.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objFileUpload.AlFileName != "")
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.AlFileName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objFileUpload.AlFileSize != null)
            {
                byte[] array = System.Convert.FromBase64String(objFileUpload.AlFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }

            if (objFileUpload.AlFileExtension != "")
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.AlFileExtension;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }




            return strMsg;
        }

        public string SaveEmpNid(EmployeeFileUploadModel objFileUpload)
        {

            string strMsg = "";
            OracleCommand objOracleCommand = new OracleCommand("pro_employee_nid_save")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objFileUpload.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objFileUpload.NidFileName != "")
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.NidFileName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objFileUpload.NidFileSize != null)
            {
                byte[] array = System.Convert.FromBase64String(objFileUpload.NidFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }

            if (objFileUpload.NidFileExtension != "")
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.NidFileExtension;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }




            return strMsg;
        }

        public string SaveEmpNp(EmployeeFileUploadModel objFileUpload)
        {

            string strMsg = "";
            OracleCommand objOracleCommand = new OracleCommand("pro_employee_np_save")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objFileUpload.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objFileUpload.NpFileName != "")
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.NpFileName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objFileUpload.NpFileSize != null)
            {
                byte[] array = System.Convert.FromBase64String(objFileUpload.NpFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }

            if (objFileUpload.NpFileExtension != "")
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.NpFileExtension;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }




            return strMsg;
        }

        public string SaveEmpPf(EmployeeFileUploadModel objFileUpload)
        {

            string strMsg = "";

            // OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_employee_pf_save")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objFileUpload.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objFileUpload.PfFileName != "")
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.PfFileName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objFileUpload.PfFileSize != null)
            {
                byte[] array = System.Convert.FromBase64String(objFileUpload.PfFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }

            if (objFileUpload.PfFileExtension != "")
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFileUpload.PfFileExtension;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objFileUpload.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {

                    strConn.Close();
                }

            }




            return strMsg;
        }


        //employee job description
        public DataTable DownloadEmpJd(EmployeeFileUploadModel objFileUpload)
        {
            DataTable dt = new DataTable();
            string sql = "";

            sql = "SELECT " +

                  "FILE_NAME, " +
                  "FILE_SIZE, " +
                  "FILE_EXTENSION " +

                  "FROM VEW_DISPLAY_EMP_JD WHERE EMPLOYEE_ID='" + objFileUpload.EmployeeId + "'   ";

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(dt);

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

        //employee joinning letter
        public DataTable DownloadEmpJl(EmployeeFileUploadModel objFileUpload)
        {
            DataTable dt = new DataTable();
            string sql = "";

            sql = "SELECT " +

                  "FILE_NAME, " +
                  "FILE_SIZE, " +
                  "FILE_EXTENSION " +

                  "FROM VEW_DISPLAY_EMP_JL WHERE EMPLOYEE_ID='" + objFileUpload.EmployeeId + "'   ";

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(dt);

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

        //employee appointment letter
        public DataTable DownloadEmpAl(EmployeeFileUploadModel objFileUpload)
        {
            DataTable dt = new DataTable();
            string sql = "";

            sql = "SELECT " +

                  "FILE_NAME, " +
                  "FILE_SIZE, " +
                  "FILE_EXTENSION " +

                  "FROM VEW_DISPLAY_EMP_AL WHERE EMPLOYEE_ID='" + objFileUpload.EmployeeId + "'   ";

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(dt);

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

        //employee national identity card
        public DataTable DownloadEmpNid(EmployeeFileUploadModel objFileUpload)
        {
            DataTable dt = new DataTable();
            string sql = "";

            sql = "SELECT " +

                  "FILE_NAME, " +
                  "FILE_SIZE, " +
                  "FILE_EXTENSION " +

                  "FROM VEW_DISPLAY_EMP_NID WHERE EMPLOYEE_ID='" + objFileUpload.EmployeeId + "'   ";

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(dt);

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

        //employee nominee photo
        public DataTable DownloadEmpNp(EmployeeFileUploadModel objFileUpload)
        {
            DataTable dt = new DataTable();
            string sql = "";

            sql = "SELECT " +

                  "FILE_NAME, " +
                  "FILE_SIZE, " +
                  "FILE_EXTENSION " +

                  "FROM VEW_DISPLAY_EMP_NP WHERE EMPLOYEE_ID='" + objFileUpload.EmployeeId + "'   ";

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(dt);

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

        //employee provident fund related document
        public DataTable DownloadEmpPf(EmployeeFileUploadModel objFileUpload)
        {
            DataTable dt = new DataTable();
            string sql = "";

            sql = "SELECT " +

                  "FILE_NAME, " +
                  "FILE_SIZE, " +
                  "FILE_EXTENSION " +

                  "FROM VEW_DISPLAY_EMP_PF WHERE EMPLOYEE_ID='" + objFileUpload.EmployeeId + "'   ";

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(dt);

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


        // Individual Job description---

        public EmployeeIndividualJdModel GetEmployeeRecordForJD(EmployeeIndividualJdModel objEmpIndividualJd)
        {
            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(DATE_OF_BIRTH,'dd/mm/yyyy')DATE_OF_BIRTH, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "TO_CHAR(JOB_CONFIRMATION_DATE,'dd/mm/yyyy')JOB_CONFIRMATION_DATE, " +
                  "PRESENT_DESIGNATION_ID, " +
                  "DESIGNATION_NAME, " +
                  "DEPARTMENT_ID, " +
                  "DEPARTMENT_NAME, " +
                  "UNIT_ID, " +
                  "UNIT_NAME, " +
                  "SECTION_ID, " +
                  "SECTION_NAME, " +
                  "GRADE_NO, " +
                  "ACTIVE_YN, " +
                  "active_status, " +
                  "EMPLOYEE_IMAGE, " +
                  "SUB_SECTION_NAME, " +
                  "EMP_JD " +
                  //"file_name " +


                  " FROM VEW_SEARCH_JD_RECORD where head_office_id = '" + objEmpIndividualJd.HeadOfficeId + "' and branch_office_id = '" + objEmpIndividualJd.BranchOfficeId +
                  "' AND active_yn = '" + objEmpIndividualJd.CheckedYN + "'   ";

            if (objEmpIndividualJd.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objEmpIndividualJd.EmployeeId + "' ";
            }

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                EmployeeIndividualJdModel objJdIndividual = null;
                try
                {
                    while (objReader.Read())
                    {
                        objJdIndividual = new EmployeeIndividualJdModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["sl"]).ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),
                            EmployeeImage = objReader["EMPLOYEE_IMAGE"] == DBNull.Value ? new byte[0] : (byte[])objReader["EMPLOYEE_IMAGE"]
                        };
                    }
                    objReader.Close();
                    objConnection.Close();
                    return objJdIndividual;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {

                }
            }
        }

        public EmployeeIndividualJdModel GetApproveStatusJD(EmployeeIndividualJdModel objEmpIndividualJd)
        {
            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "APPROVED_YN_TL," +
                  "APPROVED_YN_HR," +
                  "f_name " +


                  " FROM VEW_DISPLAY_EMP_INDIVIDUAL_JD where head_office_id = '" + objEmpIndividualJd.HeadOfficeId + "' and branch_office_id = '" + objEmpIndividualJd.BranchOfficeId + "'   ";

            if (objEmpIndividualJd.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objEmpIndividualJd.EmployeeId + "' ";
            }

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                EmployeeIndividualJdModel objJdIndividual = null;
                try
                {
                    while (objReader.Read())
                    {
                        objJdIndividual = new EmployeeIndividualJdModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["sl"]).ToString(),
                            TeamLeaderStatus = objReader["APPROVED_YN_TL"].ToString(),
                            HrStatus = objReader["APPROVED_YN_HR"].ToString(),
                            IndividualJdFileName = objReader["f_name"].ToString()
                        };
                    }
                    objReader.Close();
                    objConnection.Close();
                    return objJdIndividual;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {

                }
            }
        }

        public string SaveEmpIndividualJD(EmployeeIndividualJdModel objIndividualJd)
        {

            string strMsg = "";


            OracleCommand objOracleCommand = new OracleCommand("pro_emp_individual_jd_save")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objIndividualJd.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIndividualJd.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objIndividualJd.IndividualJdFileName != "")
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIndividualJd.IndividualJdFileName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objIndividualJd.IndividualJdFileSize != null)
            {
                byte[] array = System.Convert.FromBase64String(objIndividualJd.IndividualJdFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }

            if (objIndividualJd.IndividualJdFileExtension != "")
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIndividualJd.IndividualJdFileExtension;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIndividualJd.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIndividualJd.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIndividualJd.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);

                }

                finally
                {
                    strConn.Close();
                }

            }
            return strMsg;
        }

        //employee can download his/her respective job description file and view.
        public DataTable DownloadEmpIndividualJd(EmployeeIndividualJdModel objIndividualJd)
        {
            DataTable dt = new DataTable();

            string sql1 = "";
            int vCount = 0;

            sql1 = "SELECT count (*) Y from " +
                   "  VEW_DISPLAY_EMP_INDIVIDUAL_JD where employee_id = '" + objIndividualJd.EmployeeId + "' and head_office_id = '" + objIndividualJd.HeadOfficeId + "' and branch_office_id = '" + objIndividualJd.BranchOfficeId + "' " +
                   " and APPROVED_YN_HR = 'Y' and APPROVED_YN_TL = 'Y' ";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand1 = new OracleCommand(sql1, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand1.ExecuteReader();


                try
                {
                    while (objReader.Read())
                    {
                        {
                            vCount = Convert.ToInt32(objReader["Y"]);
                        };
                    }
                    objReader.Close();
                    objConnection.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {

                }
            }


            if (vCount > 0)
            {
                string sql = "";

                sql = "SELECT " +

                      "FILE_NAME, " +
                      "FILE_SIZE, " +
                      "FILE_EXTENSION " +

                      "FROM VEW_DISPLAY_EMP_INDIVIDUAL_JD e WHERE EMPLOYEE_ID='" + objIndividualJd.EmployeeId + "'   " +

                      "     AND tran_id = " +
                      " (SELECT MAX(tran_id) " +
                      "   FROM employee_jd " +
                      " WHERE employee_id = e.employee_id " +
                      " AND head_office_id = e.head_office_id " +
                      "   AND branch_office_id = e.branch_office_id " +
                      "    AND APPROVED_YN_HR = 'Y' " +
                      "   AND APPROVED_YN_TL = 'Y') ";

                OracleCommand objCommand = new OracleCommand(sql);
                OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
                using (OracleConnection strConn = GetConnection())
                {
                    try
                    {
                        objCommand.Connection = strConn;
                        strConn.Open();
                        objDataAdapter.Fill(dt);

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
            }
            else
            {
                string sql = "";

                sql = "SELECT " +

                      "FILE_NAME, " +
                      "FILE_SIZE, " +
                      "FILE_EXTENSION " +

                      "FROM VEW_DISPLAY_EMP_INDIVIDUAL_JD2 e WHERE EMPLOYEE_ID='" + objIndividualJd.EmployeeId + "'   " +
                      "     AND tran_id = " +
                      " (SELECT MAX(tran_id) " +
                      "   FROM employee_jd " +
                      " WHERE employee_id = e.employee_id " +
                      " AND head_office_id = e.head_office_id " +
                      "   AND branch_office_id = e.branch_office_id " +
                      "    AND APPROVED_YN_HR = 'Y' " +
                      "   AND APPROVED_YN_TL = 'Y') ";

                OracleCommand objCommand = new OracleCommand(sql);
                OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
                using (OracleConnection strConn = GetConnection())
                {
                    try
                    {
                        objCommand.Connection = strConn;
                        strConn.Open();
                        objDataAdapter.Fill(dt);

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
            }
            return dt;

        }


        public DataTable DownloadEmpIndividualJdBeforeApprovalForTl(EmployeeIndividualJdModel objIndividualJd)
        {
            DataTable dt = new DataTable();
            string sql = "";

            sql = "SELECT " +

                  "FILE_NAME, " +
                  "FILE_SIZE, " +
                  "FILE_EXTENSION " +

                  "FROM VEW_JD_PENDING_LIST_FOR_TL e  WHERE TEAM_LEADER_ID='" + objIndividualJd.UpdateBy + "'   " +
                  " AND tran_id = " +
                  " (SELECT MAX(tran_id) " +
                  "   FROM employee_jd " +
                  " WHERE employee_id = e.employee_id " +
                  " AND head_office_id = e.head_office_id " +
                  "AND branch_office_id = e.branch_office_id " +
                  "  AND APPROVED_YN_HR = 'Y' " +
                  "  AND APPROVED_YN_TL = 'Y') ";

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(dt);

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

        public DataTable DownloadEmpIndividualJdBeforeApprovalForHr(EmployeeIndividualJdModel objIndividualJd)
        {
            DataTable dt = new DataTable();
            string sql = "";

            sql = "SELECT " +

                  "FILE_NAME, " +
                  "FILE_SIZE, " +
                  "FILE_EXTENSION " +

                  "FROM VEW_JD_PENDING_LIST_FOR_HR e WHERE TEAM_LEADER_ID='" + objIndividualJd.UpdateBy + "'   " +
                  " AND tran_id = " +
                  " (SELECT MAX(tran_id) " +
                  "   FROM employee_jd " +
                  " WHERE employee_id = e.employee_id " +
                  " AND head_office_id = e.head_office_id " +
                  "AND branch_office_id = e.branch_office_id " +
                  "  AND APPROVED_YN_HR = 'Y' " +
                  "  AND APPROVED_YN_TL = 'Y') ";

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(dt);

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


        public List<EmployeeIndividualJdModel> GetJDPendingListForTL(EmployeeIndividualJdModel objIndividualJd)
        {

            List<EmployeeIndividualJdModel> listIndividualJd = new List<EmployeeIndividualJdModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "tran_id, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "DESIGNATION_NAME, " +
                  "DEPARTMENT_NAME, " +
                  "UNIT_NAME, " +
                  "SECTION_NAME, " +
                  "FILE_NAME " +

                  " FROM VEW_JD_PENDING_LIST_FOR_TL where head_office_id = '" + objIndividualJd.HeadOfficeId +
                  "' and branch_office_id = '" + objIndividualJd.BranchOfficeId + "' " +
                  "and TEAM_LEADER_ID = '" + objIndividualJd.TeamLeaderId +
                  "' and create_date between to_date ('" + objIndividualJd.ToDate + "', 'dd/mm/yyyy')  and to_date ('" + objIndividualJd.FromDate + "', 'dd/mm/yyyy')   ";

            if (objIndividualJd.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objIndividualJd.EmployeeId + "' ";
            }

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        objIndividualJd = new EmployeeIndividualJdModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["SL"]).ToString(),
                            TranId = objReader["tran_id"].ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),
                            IndividualJdFileName = objReader["FILE_NAME"].ToString()
                        };

                        listIndividualJd.Add(objIndividualJd);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }

            return listIndividualJd;

        }

        public string ApprovedEmpJDByTL(EmployeeIndividualJdModel objIndividualJd)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_employee_jd_save_by_tl")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objIndividualJd.TranId != "")
            {
                objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIndividualJd.TranId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objIndividualJd.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIndividualJd.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }




            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIndividualJd.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIndividualJd.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIndividualJd.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);

                }

                finally
                {

                    strConn.Close();
                }

            }

            return strMsg;
        }

        public List<EmployeeIndividualJdModel> GetJDPendingListForHR(EmployeeIndividualJdModel objIndividualJd)
        {

            List<EmployeeIndividualJdModel> jdPendingListForHr = new List<EmployeeIndividualJdModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "tran_id, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "DESIGNATION_NAME, " +
                  "DEPARTMENT_NAME, " +
                  "UNIT_NAME, " +
                  "SECTION_NAME, " +
                  "APPROVED_YN_TL_STATUS, " +
                  "FILE_NAME " +


                  " FROM VEW_JD_PENDING_LIST_FOR_HR where head_office_id = '" + objIndividualJd.HeadOfficeId +
                  "' and branch_office_id = '" + objIndividualJd.BranchOfficeId +
                  "'  and create_date between to_date ('" + objIndividualJd.ToDate + "', 'dd/mm/yyyy')  and to_date ('" + objIndividualJd.FromDate + "', 'dd/mm/yyyy')   ";

            if (objIndividualJd.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objIndividualJd.EmployeeId + "' ";
            }


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        objIndividualJd = new EmployeeIndividualJdModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["SL"]).ToString(),
                            TranId = objReader["tran_id"].ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),
                            IndividualJdFileName = objReader["FILE_NAME"].ToString()
                        };

                        jdPendingListForHr.Add(objIndividualJd);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }


            return jdPendingListForHr;

        }

        public string ApprovedEmpJDByHR(EmployeeIndividualJdModel objIndividualJd)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_employee_jd_save_by_hr")
            {
                CommandType = CommandType.StoredProcedure
            };

            if (objIndividualJd.TranId != "")
            {
                objOracleCommand.Parameters.Add("P_Tran_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIndividualJd.TranId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_Tran_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objIndividualJd.EmployeeId != null)
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIndividualJd.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIndividualJd.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIndividualJd.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIndividualJd.BranchOfficeId;

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
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);

                }

                finally
                {

                    strConn.Close();
                }

            }




            return strMsg;
        }



        public List<EmployeeIndividualJdModel> GetJDApprovedListForTL(EmployeeIndividualJdModel objIndividualJd)
        {

            List<EmployeeIndividualJdModel> jdApprovedListForTl = new List<EmployeeIndividualJdModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "DESIGNATION_NAME, " +
                  "DEPARTMENT_NAME, " +
                  //"UNIT_NAME, " +
                  //"SECTION_NAME " +
                  "FILE_NAME " +


                  " FROM VEW_JD_APPROVED_LIST_FOR_TL where head_office_id = '" + objIndividualJd.HeadOfficeId +
                  "' and branch_office_id = '" + objIndividualJd.BranchOfficeId +
                  "' and TEAM_LEADER_ID = '" + objIndividualJd.UpdateBy +
                  "' and create_date between to_date ('" + objIndividualJd.ToDate + "', 'dd/mm/yyyy')  and to_date ('" + objIndividualJd.FromDate + "', 'dd/mm/yyyy')   ";

            if (objIndividualJd.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objIndividualJd.EmployeeId + "' ";
            }

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        objIndividualJd = new EmployeeIndividualJdModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["SL"]).ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),
                            IndividualJdFileName = objReader["FILE_NAME"].ToString()
                        };

                        jdApprovedListForTl.Add(objIndividualJd);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }
            return jdApprovedListForTl;

        }

        public List<EmployeeIndividualJdModel> GetJDApprovedListForHr(EmployeeIndividualJdModel objIndividualJd)
        {

            List<EmployeeIndividualJdModel> jdApprovedListForHr = new List<EmployeeIndividualJdModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "DESIGNATION_NAME, " +
                  "DEPARTMENT_NAME, " +
                  //"UNIT_NAME, " +
                  //"SECTION_NAME " +
                  "FILE_NAME " +

                  " FROM VEW_JD_APPROVED_LIST_FOR_hr where head_office_id = '" + objIndividualJd.HeadOfficeId +
                  "' and branch_office_id = '" + objIndividualJd.BranchOfficeId +

                  "' and trunc(create_date) between to_date ('" + objIndividualJd.ToDate + "', 'dd/mm/yyyy')  and to_date ('" + objIndividualJd.FromDate + "', 'dd/mm/yyyy')   ";

            if (objIndividualJd.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objIndividualJd.EmployeeId + "' ";
            }

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        objIndividualJd = new EmployeeIndividualJdModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["SL"]).ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),
                            IndividualJdFileName = objReader["FILE_NAME"].ToString()
                        };

                        jdApprovedListForHr.Add(objIndividualJd);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objReader.Close();
                    objConnection.Close();
                }
            }
            return jdApprovedListForHr;

        }



        #endregion
    }
}
