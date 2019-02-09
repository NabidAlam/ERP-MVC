using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.MODEL;
using Oracle.ManagedDataAccess.Client;

namespace ERP.DAL
{
    public class AttendanceDAL
    {
        private OracleTransaction trans = null;

        #region Oracle Connection Check

        private OracleConnection GetConnection()
        {
            System.Configuration.ConnectionStringSettings conString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConString"];
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
            OracleCommand objOracleCommand = new OracleCommand("pro_attendance_data_upload")
            {
                CommandType = CommandType.StoredProcedure
            };


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
            OracleCommand objOracleCommand = new OracleCommand("pro_attendence_file_upload")
            {
                CommandType = CommandType.StoredProcedure
            };


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


            using (OracleConnection strConn = GetConnection())
            {
                strConn.Open();
                try
                {
                    foreach (AttendanceApprovalModel model in objApprovalModel.ListAttendanceApprovalModels)
                    {
                        OracleCommand objOracleCommand = new OracleCommand("PRO_ATTENDENCE_APPROVED")
                        {
                            Connection = strConn,
                            CommandType = CommandType.StoredProcedure
                        };


                        if (model.EmployeeId != "")
                        {
                            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objApprovalModel.EmployeeId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (model.LogDate.Length > 6)
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



                        trans = strConn.BeginTransaction();
                        objOracleCommand.ExecuteNonQuery();
                        trans.Commit();

                        strMsg = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                    }

                    strConn.Close();
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
            OracleCommand objOracleCommand = new OracleCommand("PRO_ATTENDENCE_APPROVED_DELETE")
            {
                CommandType = CommandType.StoredProcedure
            };


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
        public List<AttendanceProcessManualModel> GetMissingEmployeeAttendance(AttendanceProcessManualModel objProcessManualModel)
        {

            //DataTable dt = new DataTable();

            List<AttendanceProcessManualModel> listAttendanceProcessManualModels = new List<AttendanceProcessManualModel>();

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


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        AttendanceProcessManualModel objApprovalModel = new AttendanceProcessManualModel
                        {
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),

                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),

                            JoiningDate = objReader["JOINING_DATE"].ToString(),

                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),

                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),

                            LogDate = objReader["log_date"].ToString(),

                            DayTypeId = objReader["day_type_id"].ToString(),

                            InTime = objReader["FIRST_IN"].ToString(),

                            OutTime = objReader["LAST_OUT"].ToString(),

                            LeaveStatus = objReader["puntch_type_tatus"].ToString(),

                            AttendanceStatus = objReader["ACTIVE_STATUS"].ToString(),

                            CorrectedAttendance = objReader["PUNCH_STATUS"].ToString(),

                            SerialNumber = objReader["sl"].ToString()
                        };

                        listAttendanceProcessManualModels.Add(objApprovalModel);
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
            return listAttendanceProcessManualModels;

        }

        // Load absent attendance of employees 
        public List<AttendanceProcessManualModel> GetAbsentEmployeeAttendance(AttendanceProcessManualModel objProcessManualModel)
        {

            //DataTable dt = new DataTable();

            List<AttendanceProcessManualModel> listAbsentEmployee = new List<AttendanceProcessManualModel>();

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


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        AttendanceProcessManualModel objApprovalModel = new AttendanceProcessManualModel
                        {
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),

                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),

                            JoiningDate = objReader["JOINING_DATE"].ToString(),

                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),

                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),

                            LogDate = objReader["log_date"].ToString(),

                            DayTypeId = objReader["day_type_id"].ToString(),

                            InTime = objReader["FIRST_IN"].ToString(),

                            OutTime = objReader["LAST_OUT"].ToString(),

                            LeaveStatus = objReader["puntch_type_tatus"].ToString(),

                            AttendanceStatus = objReader["ACTIVE_STATUS"].ToString(),

                            CorrectedAttendance = objReader["PUNCH_STATUS"].ToString(),

                            SerialNumber = objReader["sl"].ToString()
                        };

                        listAbsentEmployee.Add(objApprovalModel);
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
            return listAbsentEmployee;
        }

        // Load attendance of employees 
        public List<AttendanceProcessManualModel> GetEmployeeRecordForAttendanceManual(AttendanceProcessManualModel objProcessManualModel)
        {

            //DataTable dt = new DataTable();

            List<AttendanceProcessManualModel> listEmployeeRecord = new List<AttendanceProcessManualModel>();


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


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        AttendanceProcessManualModel objApprovalModel = new AttendanceProcessManualModel
                        {
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),

                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),

                            JoiningDate = objReader["JOINING_DATE"].ToString(),

                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),

                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),

                            LogDate = objReader["log_date"].ToString(),

                            DayTypeId = objReader["day_type_id"].ToString(),

                            InTime = objReader["FIRST_IN"].ToString(),

                            OutTime = objReader["LAST_OUT"].ToString(),

                            LeaveStatus = objReader["puntch_type_tatus"].ToString(),

                            AttendanceStatus = objReader["ACTIVE_STATUS"].ToString(),

                            CorrectedAttendance = objReader["PUNCH_STATUS"].ToString(),

                            SerialNumber = objReader["sl"].ToString()
                        };

                        listEmployeeRecord.Add(objApprovalModel);
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


            return listEmployeeRecord;

        }


        // add missing attendance of employee to db
        public string AddAttendanceRecordMissing(AttendanceProcessManualModel objProcessManualModel)
        {

            string strMsg = "";

           
            OracleCommand objOracleCommand = new OracleCommand("PRO_ADD_ATTENDANCE_MISSING")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objProcessManualModel.EmployeeId != "")
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

        // add absent attendance of employee to db
        public string AddAttendanceRecordAbsent(AttendanceProcessManualModel objProcessManualModel)
        {

            string strMsg = "";
            
            OracleCommand objOracleCommand = new OracleCommand("PRO_ADD_ATTENDANCE_ABSENT")
            {
                CommandType = CommandType.StoredProcedure
            };



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

        // add manual attendance of employee to db
        public string AddRecordAttendanceManual(AttendanceProcessManualModel objProcessManualModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("PRO_ADD_ATTENDANCE_REOCRD")
            {
                CommandType = CommandType.StoredProcedure
            };


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
            OracleCommand objOracleCommand = new OracleCommand("pro_attendance_process_manual")
            {
                CommandType = CommandType.StoredProcedure
            };

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

            using (OracleConnection strConn = GetConnection())
            {
                strConn.Open();
                try
                {
                    foreach (AttendanceProcessManualModel model in objProcessManualModel.AttendanceProcessManualList)
                    {
                        OracleCommand objOracleCommand = new OracleCommand("PRO_ATTENDENCE_MANUAL_SAVE")
                        {
                            Connection = strConn,
                            CommandType = CommandType.StoredProcedure
                        };

                        if (model.EmployeeId != null)
                        {
                            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = model.EmployeeId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }


                        if (model.LogDate.Length > 6)
                        {
                            objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = model.LogDate;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("P_LOG_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (model.InTime != null)
                        {
                            objOracleCommand.Parameters.Add("P_FIRST_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = model.InTime;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("P_FIRST_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }


                        if (model.OutTime != null)
                        {
                            objOracleCommand.Parameters.Add("P_LAST_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = model.OutTime;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("P_LAST_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }


                        if (model.LunchOutTime != "")
                        {
                            objOracleCommand.Parameters.Add("P_LUNCH_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = model.LunchOutTime;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("P_LUNCH_OUT", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (model.LunchInTime != "")
                        {
                            objOracleCommand.Parameters.Add("LUNCH_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = model.LunchInTime;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("LUNCH_IN", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (objProcessManualModel.UnitId != null)
                        {
                            objOracleCommand.Parameters.Add("P_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.UnitId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("P_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (objProcessManualModel.DepartmentId != null)
                        {
                            objOracleCommand.Parameters.Add("P_DEPARTMENT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.DepartmentId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("P_DEPARTMENT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (objProcessManualModel.SectionId != null)
                        {
                            objOracleCommand.Parameters.Add("P_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SectionId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("P_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (model.SubSectionId != null)
                        {
                            objOracleCommand.Parameters.Add("P_SUB_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = model.SubSectionId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("P_SUB_SECTION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }


                        if (model.DayTypeId != null)
                        {
                            objOracleCommand.Parameters.Add("P_DAY_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = model.DayTypeId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("P_DAY_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }


                        objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.UpdateBy;
                        objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.HeadOfficeId;
                        objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objProcessManualModel.BranchOfficeId;

                        objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                        trans = strConn.BeginTransaction();
                        objOracleCommand.ExecuteNonQuery();
                        trans.Commit();

                        strMsg = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                    }
                    strConn.Close();
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
            OracleCommand objOracleCommand = new OracleCommand("pro_attendance_data_upload")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objProcessManualModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
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
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProcessManualModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objProcessManualModel.SubSectionId != null)
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

        #region Assign Shfit to Employee 

        public List<AssignEmployeeShiftModel> GetEmployeeRecordForAssignShift(AssignEmployeeShiftModel objAssignShiftModel)
        {

            List<AssignEmployeeShiftModel> listEmployee = new List<AssignEmployeeShiftModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "DESIGNATION_NAME, " +
                  "DEPARTMENT_NAME, " +
                  "UNIT_NAME, " +
                  "SECTION_NAME, " +
                  "SUB_SECTION_NAME, " +
                  "GRADE_NO, " +
                  "active_status, " +
                  "EMPLOYEE_IMAGE " +

                  " FROM VEW_EMP_RECORD_SEARCH where head_office_id = '" + objAssignShiftModel.HeadOfficeId + "' and branch_office_id = '" + objAssignShiftModel.BranchOfficeId + "' AND active_yn = 'Y'   ";

            if (objAssignShiftModel.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objAssignShiftModel.EmployeeId + "' ";
            }

            if (objAssignShiftModel.DepartmentId != null)
            {

                sql = sql + "and department_id = '" + objAssignShiftModel.DepartmentId + "' ";
            }

            if (objAssignShiftModel.UnitId != null)
            {

                sql = sql + "and unit_id = '" + objAssignShiftModel.UnitId + "' ";
            }

            if (objAssignShiftModel.SubSectionId != null)
            {

                sql = sql + "and sub_section_id = '" + objAssignShiftModel.SubSectionId + "' ";
            }

            if (objAssignShiftModel.SectionId != null)
            {

                sql = sql + "and section_id = '" + objAssignShiftModel.SectionId + "' ";
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
                        AssignEmployeeShiftModel objEmployeeJd = new AssignEmployeeShiftModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["SL"]).ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            UnitName = objReader["UNIT_NAME"].ToString(),
                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),
                            SectionName = objReader["SECTION_NAME"].ToString(),
                            SubSectionName = objReader["SUB_SECTION_NAME"].ToString(),
                            EmployeeGrade = objReader["GRADE_NO"].ToString(),
                            ActiveStatus = objReader["active_status"].ToString(),
                            EmpoyeeImage = objReader["EMPLOYEE_IMAGE"] == DBNull.Value ? new byte[0] : (byte[])objReader["EMPLOYEE_IMAGE"]
                        };


                        listEmployee.Add(objEmployeeJd);
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
            return listEmployee;

        }

        public string SaveAssignEmployeeShift(AssignEmployeeShiftModel objAssignShiftModel)
        {

            string strMsg = "";

            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    strConn.Open();
                    foreach (AssignEmployeeShiftModel assign in objAssignShiftModel.AssignEmployeeShiftList)
                    {
                        OracleCommand objOracleCommand = new OracleCommand("pro_assign_shift_save")
                        {
                            Connection = strConn,
                            CommandType = CommandType.StoredProcedure
                        };


                        if (assign.EmployeeId != null)
                        {
                            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = assign.EmployeeId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (objAssignShiftModel.FirstDate != null)
                        {
                            objOracleCommand.Parameters.Add("p_log_date_from", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAssignShiftModel.FirstDate;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_log_date_from", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (objAssignShiftModel.LastDate != null)
                        {
                            objOracleCommand.Parameters.Add("p_log_date_to", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAssignShiftModel.LastDate;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_log_date_to", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (objAssignShiftModel.ShiftTypeId != null)
                        {
                            objOracleCommand.Parameters.Add("p_shift_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAssignShiftModel.ShiftTypeId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_shift_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAssignShiftModel.UpdateBy;
                        objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAssignShiftModel.HeadOfficeId;
                        objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAssignShiftModel.BranchOfficeId;

                        objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;


                        //objOracleCommand.Connection = strConn;
                        //strConn.Open();
                        trans = strConn.BeginTransaction();
                        objOracleCommand.ExecuteNonQuery();
                        trans.Commit();
                        strMsg = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                    }

                    strConn.Close();
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


        public List<AssignEmployeeShiftModel> GetAssignedEmployeeList(AssignEmployeeShiftModel objAssignShiftModel)
        {
            List<AssignEmployeeShiftModel> listEmployee = new List<AssignEmployeeShiftModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "DESIGNATION_NAME, " +
                  "DEPARTMENT_NAME, " +
                  "SHIFT_NAME," +
                  "TO_CHAR(LOG_DATE,'dd/mm/yyyy')LOG_DATE," +
                  //"UNIT_NAME, " +
                  //"SECTION_NAME, " +
                  //"SUB_SECTION_NAME, " +
                  //"GRADE_NO, " +
                  "active_yn, " +
                  "file_size " +

                  " FROM VEW_EMP_ASSIGNED_SHIFT where head_office_id = '" + objAssignShiftModel.HeadOfficeId + "' and branch_office_id = '" + objAssignShiftModel.BranchOfficeId + "' AND active_yn = 'Y'   ";
            /*and log_date between to_date('"+ objAssignShiftModel.FirstDate+"','dd/mm/yyyy') and to_date('"+objAssignShiftModel.LastDate+"','dd/mm/yyyy')*/

            if (objAssignShiftModel.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objAssignShiftModel.EmployeeId + "' ";
            }
            if (objAssignShiftModel.ShiftTypeId != null)
            {

                sql = sql + "and shift_id = '" + objAssignShiftModel.ShiftTypeId + "' ";
            }


            if (objAssignShiftModel.DepartmentId != null)
            {

                sql = sql + "and department_id = '" + objAssignShiftModel.DepartmentId + "' ";
            }

            if (objAssignShiftModel.UnitId != null)
            {

                sql = sql + "and unit_id = '" + objAssignShiftModel.UnitId + "' ";
            }

            if (objAssignShiftModel.SubSectionId != null)
            {

                sql = sql + "and sub_section_id = '" + objAssignShiftModel.SubSectionId + "' ";
            }

            if (objAssignShiftModel.SectionId != null)
            {

                sql = sql + "and section_id = '" + objAssignShiftModel.SectionId + "' ";
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
                        AssignEmployeeShiftModel objEmployeeJd = new AssignEmployeeShiftModel();

                        objEmployeeJd.SerialNumber = Convert.ToInt32(objReader["SL"]).ToString();
                        objEmployeeJd.EmployeeId = objReader["EMPLOYEE_ID"].ToString();
                        objEmployeeJd.EmployeeName = objReader["EMPLOYEE_NAME"].ToString();
                        objEmployeeJd.JoiningDate = objReader["JOINING_DATE"].ToString();
                        objEmployeeJd.DesignationName = objReader["DESIGNATION_NAME"].ToString();
                        //objEmployeeJd.UnitName = objReader["UNIT_NAME"].ToString();
                        objEmployeeJd.DepartmentName = objReader["DEPARTMENT_NAME"].ToString();
                        objEmployeeJd.ShiftTypeName = objReader["SHIFT_NAME"].ToString();
                        objEmployeeJd.LogDate = objReader["LOG_DATE"].ToString();
                        //UnitName = objReader["UNIT_NAME"].ToString(),
                        //SectionName = objReader["SECTION_NAME"].ToString(),
                        //SubSectionName = objReader["SUB_SECTION_NAME"].ToString(),
                        //EmployeeGrade = objReader["GRADE_NO"].ToString(),
                        objEmployeeJd.ActiveStatus = objReader["active_yn"].ToString();
                        objEmployeeJd.EmpoyeeImage = objReader["FILE_SIZE"] == DBNull.Value ? new byte[0] : (byte[])objReader["FILE_SIZE"];


                        listEmployee.Add(objEmployeeJd);
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
            return listEmployee;

        }

        #endregion

    }
}
