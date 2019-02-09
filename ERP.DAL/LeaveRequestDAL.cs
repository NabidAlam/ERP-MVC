using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.MODEL;
using Oracle.ManagedDataAccess.Client;

namespace ERP.DAL
{
    public class LeaveRequestDAL
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


        public string SaveLeaveEntry(LeaveRequestModel objLeaveRequestModel)
        {



            string strMsg = "";


            int x = objLeaveRequestModel.GridLeaveTypeId.Count();
            for (int i = 0; i < x; i++)
            {
                //var GridEmployeeId = objLeaveRequestModel.GridEmployeeId[i];
                var GridLeaveTypeId = objLeaveRequestModel.GridLeaveTypeId[i];
                var GridLeaveStartDate = objLeaveRequestModel.GridLeaveStartDate[i];
                var GridLeaveEndDate = objLeaveRequestModel.GridLeaveEndDate[i];
                //var GridApprovedEmployeeId = objLeaveRequestModel.GridApprovedEmployeeId[i];
                var GridRemarks = objLeaveRequestModel.GridRemarks[i];
             


                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_LEAVE_SAVE");
                objOracleCommand.CommandType = CommandType.StoredProcedure;


                if (!string.IsNullOrWhiteSpace(objLeaveRequestModel.EmployeeId))
                {
                    objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.EmployeeId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (objLeaveRequestModel.GridLeaveTypeId.Length > 0)
                {
                    objOracleCommand.Parameters.Add("p_leave_type_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = GridLeaveTypeId;
                }
             
                else
                {
                    objOracleCommand.Parameters.Add("p_leave_type_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (objLeaveRequestModel.GridLeaveStartDate.Length > 0)
                {
                    objOracleCommand.Parameters.Add("p_leave_start_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = GridLeaveStartDate;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_leave_start_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (objLeaveRequestModel.GridLeaveEndDate.Length > 0)
                {
                    objOracleCommand.Parameters.Add("p_leave_end_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = GridLeaveEndDate;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_leave_end_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (objLeaveRequestModel.GridRemarks.Length > 0)
                {
                    objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = GridRemarks;
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.CurrentYear;

                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.BranchOfficeId;
              
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
                        objOracleTransaction.Rollback();
                        throw new Exception("Error : " + ex.Message);
                    }

                    finally
                    {
                        strConn.Close();
                    }
                }
            }
            return strMsg;
        }
        public IEnumerable<LeaveRequestModel> LoadEmployeeLeaveSummeryRecord(LeaveRequestModel objLeaveRequestModel)
        {
            List<LeaveRequestModel> leaveSummaryList = new List<LeaveRequestModel>();


            string sql = "";
            //sql = "SELECT " +
            //      "rownum sl, " +
            //      "leave_type_name, " +
            //      "total_leave, " +
            //      "max_leave, " +
            //      "remain_leave " +
            //      " FROM vew_leave_summery_search where head_office_id = '" + objLeaveRequestModel.HeadOfficeId + "' " +
            //      "and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' " +
            //      "AND  leave_year = to_char(sysdate, 'YYYY') " +
            //      "and employee_id = '" + objLeaveRequestModel.EmployeeId + "'   ";

            sql = "SELECT " +
                "rownum sl, " +
                "leave_type_name, " +
                "total_leave, " +
                "max_leave, " +
                "remain_leave, " +
                "leave_year "+
                " FROM vew_leave_summery_search where head_office_id = '" + objLeaveRequestModel.HeadOfficeId + "' " +
                "and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' " +
                "AND leave_year =  '"+objLeaveRequestModel.CurrentYear+"'" +
                "and employee_id = '" + objLeaveRequestModel.EmployeeId + "'   ";
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
                        objLeaveRequestModel = new LeaveRequestModel();
                        //objLeaveRequestModel.EmployeeModel = new EmployeeModel();

                        objLeaveRequestModel.LeaveTypeName = objDataReader["leave_type_name"].ToString();
                        objLeaveRequestModel.LeaveTaken = objDataReader["total_leave"].ToString();
                        objLeaveRequestModel.LeaveEntitled = objDataReader["max_leave"].ToString();
                        objLeaveRequestModel.LeaveRemain = objDataReader["remain_leave"].ToString();
                        objLeaveRequestModel.CurrentYear = objDataReader["leave_year"].ToString();
                        leaveSummaryList.Add(objLeaveRequestModel);
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

            return leaveSummaryList;

        }
        public IEnumerable<LeaveRequestModel> GetAllLeaveRequestRecord(LeaveRequestModel objLeaveRequestModel)
        {
            List<LeaveRequestModel> leaveRequestList = new List<LeaveRequestModel>();

            //string vQuery = "";
            //vQuery = "SELECT " +
            //       "rownum sl, " +
            //       "EMPLOYEE_ID, " +
            //       "EMPLOYEE_NAME, " +
            //       "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
            //       "TO_CHAR(leave_from_date,'dd/mm/yyyy')leave_from_date, " +
            //       "TO_CHAR(leave_to_date,'dd/mm/yyyy')leave_to_date, " +
            //       "DESIGNATION_NAME, " +
            //       "department_name, " +
            //       "ACTIVE_YN, " +
            //       "LEAVE_TYPE_ID, " +
            //       "leave_type_name, " +
            //       "REMARKS, " +
            //       "APPROVED_EMPLOYEE_ID, " +
            //       "APPROVED_EMPLOYEE_NAME, " +
            //       "HR_APPROVED_STATUS, " +
            //       "TL_APPROVED_STATUS " +
            //       " FROM VEW_EMP_LEAVE_RECORD where head_office_id = '" + objLeaveRequestModel.HeadOfficeId + "' and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' and employee_id = '" + objLeaveRequestModel.EmployeeId + "'   ";
            ////here current year will be added with the and condition 

            //string vQuery = "";
            //vQuery = "SELECT " +
            //       "rownum sl, " +
            //       "EMPLOYEE_ID, " +
            //       "EMPLOYEE_NAME, " +
            //       "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
            //       "TO_CHAR(leave_from_date,'dd/mm/yyyy')leave_from_date, " +
            //       "TO_CHAR(leave_to_date,'dd/mm/yyyy')leave_to_date, " +
            //       "DESIGNATION_NAME, " +
            //       "department_name, " +
            //       "ACTIVE_YN, " +
            //       "LEAVE_TYPE_ID, " +
            //       "leave_type_name, " +
            //       "REMARKS, " +
            //       "APPROVED_EMPLOYEE_ID, " +
            //       "APPROVED_EMPLOYEE_NAME, " +
            //       "HR_APPROVED_STATUS, " +
            //       "LEAVE_YEAR, " +
            //       "TL_APPROVED_STATUS, " +
            //       "TOTAL_NO_OF_LEAVE " +
            //       " FROM VEW_EMP_LEAVE_RECORD where head_office_id = '" + objLeaveRequestModel.HeadOfficeId + "' " +
            //       "and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' " +
            //       "AND  LEAVE_YEAR = to_char(sysdate, 'YYYY') " +
            //       "and employee_id = '" + objLeaveRequestModel.EmployeeId + "'   ";

            string vQuery = "";
            vQuery = "SELECT " +
                   "rownum sl, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "TO_CHAR(leave_from_date,'dd/mm/yyyy')leave_from_date, " +
                   "TO_CHAR(leave_to_date,'dd/mm/yyyy')leave_to_date, " +
                   "DESIGNATION_NAME, " +
                   "department_name, " +
                   "ACTIVE_YN, " +
                   "LEAVE_TYPE_ID, " +
                   "leave_type_name, " +
                   "REMARKS, " +
                   "APPROVED_EMPLOYEE_ID, " +
                   "APPROVED_EMPLOYEE_NAME, " +
                   "HR_APPROVED_STATUS, " +
                   "LEAVE_YEAR, " +
                   "TL_APPROVED_STATUS, " +
                   "TOTAL_NO_OF_LEAVE, " +
                   "TL_APPROVE_YN, " +
                   "HR_APPROVE_YN " +
                   " FROM VEW_EMP_LEAVE_RECORD where head_office_id = '" + objLeaveRequestModel.HeadOfficeId + "' " +
                   "and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' " +
                   "AND  LEAVE_YEAR = '"+objLeaveRequestModel.CurrentYear+"' " +
                   "and employee_id = '" + objLeaveRequestModel.EmployeeId + "'   ";


            
          
            vQuery += " ORDER BY sl";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objLeaveRequestModel = new LeaveRequestModel();
                        objLeaveRequestModel.EmployeeModel = new EmployeeModel();

                        objLeaveRequestModel.EmployeeId = objDataReader["EMPLOYEE_ID"].ToString();
                        objLeaveRequestModel.LeaveTypeId = objDataReader["LEAVE_TYPE_ID"].ToString();
                        objLeaveRequestModel.LeaveTypeName = objDataReader["leave_type_name"].ToString();
                        objLeaveRequestModel.LeaveFromDate = objDataReader["leave_from_date"].ToString();
                        objLeaveRequestModel.LeaveToDate = objDataReader["leave_to_date"].ToString();
                        objLeaveRequestModel.Remarks = objDataReader["REMARKS"].ToString();
                        objLeaveRequestModel.TotalNoOfLeave = objDataReader["TOTAL_NO_OF_LEAVE"].ToString();
                        objLeaveRequestModel.HR_Status = objDataReader["HR_APPROVED_STATUS"].ToString();
                        objLeaveRequestModel.TL_Status = objDataReader["TL_APPROVED_STATUS"].ToString();
                        objLeaveRequestModel.HR_Flag = objDataReader["HR_APPROVE_YN"].ToString();
                        objLeaveRequestModel.TL_Flag = objDataReader["TL_APPROVE_YN"].ToString();
                        objLeaveRequestModel.CurrentYear = objDataReader["LEAVE_YEAR"].ToString();


                        leaveRequestList.Add(objLeaveRequestModel);
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

            return leaveRequestList;
        }
        public LeaveRequestModel GetAllLeaveRequestEmployeeRecord(LeaveRequestModel objLeaveRequestModel)
        {



         



            //string vQuery = "";
            //vQuery = "SELECT " +
            //  "EMPLOYEE_NAME, " +
            //  "TO_CHAR(joining_date,'dd/mm/yyyy')joining_date, " +          
            //  "designation_name, " +
            //  "employee_id, " +
            //  "HEAD_OFFICE_ID, " +
            //  "BRANCH_OFFICE_ID " +
            //  "from vew_personal_info where employee_id = '" + objLeaveRequestModel.EmployeeId + "' and head_office_id = '" + objLeaveRequestModel.HeadOfficeId + "' AND branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' ";

            string vQuery = "";
            vQuery = "SELECT " +
                  
                    "EMPLOYEE_ID, " +
                    "EMPLOYEE_NAME, " +
                    "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                    "DESIGNATION_NAME, " +
                    "HEAD_OFFICE_ID, " +
                    "BRANCH_OFFICE_ID, " +
                    "EMPLOYEE_PIC, " +
                     "NVL(TO_CHAR(TRANSFER_DATE,'dd/mm/yyyy'), '')TRANSFER_DATE " +

                    "FROM VEW_PERSONAL_INFO where head_office_id = '" + objLeaveRequestModel.HeadOfficeId + "' and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' AND employee_id = '" + objLeaveRequestModel.EmployeeId + "'   ";






            //sql = sql + " order by SL ";

            //if (!string.IsNullOrWhiteSpace(objFabricModel.SearchBy))
            //{
            //    vQuery += "and ( (lower(FABRIC_CODE) like lower( '%" + objFabricModel.SearchBy + "%')  or upper(FABRIC_CODE)like upper('%" + objFabricModel.SearchBy + "%') )" +
            //              "or (TO_CHAR (PURCHASE_DATE, 'dd/mm/yyyy') like '" + objFabricModel.SearchBy + "' or TO_CHAR (PURCHASE_DATE, 'dd/mm/yyyy') like '" + objFabricModel.SearchBy + "')" +
            //              "or (lower(FABRIC_TYPE_NAME) like lower( '%" + objFabricModel.SearchBy + "%')  or upper(FABRIC_TYPE_NAME)like upper('%" + objFabricModel.SearchBy + "%') )" +
            //              "or (lower(FABRIC_UNIT_NAME) like lower( '%" + objFabricModel.SearchBy + "%')  or upper(FABRIC_UNIT_NAME)like upper('%" + objFabricModel.SearchBy + "%') )" +
            //              "or (lower(CATEGORY_NAME) like lower( '%" + objFabricModel.SearchBy + "%')  or upper(CATEGORY_NAME)like upper('%" + objFabricModel.SearchBy + "%') )" +
            //              "or (lower(SUPPLIER_NAME) like lower( '%" + objFabricModel.SearchBy + "%')  or upper(SUPPLIER_NAME)like upper('%" + objFabricModel.SearchBy + "%') )" +
            //              "or (lower(LOCATION_NAME) like lower( '%" + objFabricModel.SearchBy + "%')  or upper(LOCATION_NAME)like upper('%" + objFabricModel.SearchBy + "%') )" +
            //              "or (lower(DESIGNER_NAME) like lower( '%" + objFabricModel.SearchBy + "%')  or upper(DESIGNER_NAME)like upper('%" + objFabricModel.SearchBy + "%') )" +
            //              "or (lower(LAB_TEST_NAME) like lower( '%" + objFabricModel.SearchBy + "%')  or upper(LAB_TEST_NAME)like upper('%" + objFabricModel.SearchBy + "%') ) )";
            //}
            //else
            //{
            //    vQuery += "AND PURCHASE_DATE = TO_DATE(SYSDATE)";
            //}

            //vQuery = vQuery + " ORDER BY sl";
            vQuery += " ORDER BY joining_date";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objLeaveRequestModel = new LeaveRequestModel();
                        objLeaveRequestModel.EmployeeModel = new EmployeeModel();

                        objLeaveRequestModel.EmployeeId = objDataReader["employee_id"].ToString();
                        objLeaveRequestModel.EmployeeModel.EmployeeId = objDataReader["employee_id"].ToString();
                        objLeaveRequestModel.EmployeeModel.EmployeeName = objDataReader["EMPLOYEE_NAME"].ToString();
                        objLeaveRequestModel.EmployeeModel.JoiningDate = objDataReader["joining_date"].ToString();
                        objLeaveRequestModel.EmployeeModel.PresentDesignationName = objDataReader["designation_name"].ToString();
                        objLeaveRequestModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                        objLeaveRequestModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();
                        objLeaveRequestModel.EmployeeModel.EmployeeImage = objDataReader["EMPLOYEE_PIC"] == DBNull.Value ? new byte[0] : (byte[])objDataReader["EMPLOYEE_PIC"];
                        objLeaveRequestModel.TransferDate = objDataReader["TRANSFER_DATE"].ToString();


                        
                        //objLeaveRequestModel.CurrentYear = objDataReader["LEAVE_YEAR"].ToString();

                        //leaveRequestList.Add(objLeaveRequestModel);
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

            return objLeaveRequestModel;
        }
        public DataTable GetLeaveRequestForEdit(LeaveRequestModel objLeaveRequestModel)
        {

            DataTable dt2 = new DataTable();

            string sql = "";

            sql = "SELECT " +
                   "rownum sl, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "TO_CHAR(leave_from_date,'dd/mm/yyyy')leave_from_date, " +
                   "TO_CHAR(leave_to_date,'dd/mm/yyyy')leave_to_date, " +
                   "DESIGNATION_NAME, " +
                   "department_name, " +
                   "ACTIVE_YN, " +
                   "LEAVE_TYPE_ID, " +
                   "leave_type_name, " +
                   "REMARKS, " +
                   "APPROVED_EMPLOYEE_ID, " +
                   "APPROVED_EMPLOYEE_NAME, " +
                   "HR_APPROVED_STATUS, " +
                   "LEAVE_YEAR, " +
                   "TL_APPROVED_STATUS, " +
                   "TOTAL_NO_OF_LEAVE " +

                   " FROM VEW_EMP_LEAVE_RECORD where head_office_id = '" + objLeaveRequestModel.HeadOfficeId + "' and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + 
                   "' AND  LEAVE_YEAR = '"+objLeaveRequestModel.CurrentYear+"' and employee_id = '" + objLeaveRequestModel.EmployeeId + "' " +
                   "and LEAVE_TYPE_ID='"+objLeaveRequestModel.LeaveTypeId+ "' " +
                   "and leave_from_date= TO_DATE('" + objLeaveRequestModel.LeaveFromDate + "', 'dd/mm/yyyy') " +
                   "and leave_to_date= TO_DATE('" + objLeaveRequestModel.LeaveToDate + "', 'dd/mm/yyyy')";



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
                    objDataAdapter.Fill(dt2);
                    dt2.AcceptChanges();
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


            return dt2;

        }
        public string DeleteEmpLeaveRecord(LeaveRequestModel objLeaveRequestModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_DELETE_EMPLOYEE_LEAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objLeaveRequestModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objLeaveRequestModel.LeaveFromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_Date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.LeaveFromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_Date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;

            }
           

            if (objLeaveRequestModel.LeaveToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_Date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.LeaveToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_Date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.BranchOfficeId;

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
        #region
        // Pending List for team leader
        public List<LeaveRequestModel> GetLeaveRequestPendingListForTL(LeaveRequestModel objLeaveRequestModel)
        {

            List<LeaveRequestModel> listIndividualLeaveReq = new List<LeaveRequestModel>();


       
            string sql = "";
            sql = "SELECT " +
                  "LEAVE_TYPE_NAME, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "DESIGNATION_NAME, " +
                   "JOINING_DATE, " +
                   "TO_CHAR(LEAVE_FROM_DATE,'dd/mm/yyyy')LEAVE_FROM_DATE, " +
                   "TO_CHAR(LEAVE_TO_DATE,'dd/mm/yyyy')LEAVE_TO_DATE, " +
                   "TOTAL_NO_OF_LEAVE, " +
                   "HEAD_OFFICE_ID, " +
                   "BRANCH_OFFICE_ID, " +
                   "REMARKS " +
                   " FROM VEW_EMPLOYEE_LEAVE_PL_FOR_TL where head_office_id = '" + objLeaveRequestModel.HeadOfficeId +
                   "' and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' " +
                   
                   "and TEAM_LEADER_ID = '" + objLeaveRequestModel.TeamLeaderId +
                   "' and create_date between to_date ('" + objLeaveRequestModel.LeaveStartDate + "', 'dd/mm/yyyy')  and to_date ('" + objLeaveRequestModel.LeaveEndDate + "', 'dd/mm/yyyy')   ";

            if (objLeaveRequestModel.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objLeaveRequestModel.EmployeeId + "' ";
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
                        objLeaveRequestModel = new LeaveRequestModel();
                        objLeaveRequestModel.EmployeeModel = new EmployeeModel();

                        objLeaveRequestModel.EmployeeId = objReader["employee_id"].ToString();
                        objLeaveRequestModel.EmployeeModel.EmployeeId = objReader["employee_id"].ToString();
                        objLeaveRequestModel.EmployeeModel.EmployeeName = objReader["EMPLOYEE_NAME"].ToString();
                        objLeaveRequestModel.EmployeeModel.JoiningDate = objReader["joining_date"].ToString();
                        objLeaveRequestModel.EmployeeModel.PresentDesignationName = objReader["designation_name"].ToString();
                        objLeaveRequestModel.LeaveTypeName = objReader["LEAVE_TYPE_NAME"].ToString();
                        objLeaveRequestModel.LeaveFromDate = objReader["LEAVE_FROM_DATE"].ToString();
                        objLeaveRequestModel.LeaveToDate = objReader["LEAVE_TO_DATE"].ToString();
                        objLeaveRequestModel.TotalNoOfLeave = objReader["TOTAL_NO_OF_LEAVE"].ToString();
                        objLeaveRequestModel.Remarks = objReader["REMARKS"].ToString();

                        objLeaveRequestModel.HeadOfficeId = objReader["HEAD_OFFICE_ID"].ToString();
                        objLeaveRequestModel.BranchOfficeId = objReader["BRANCH_OFFICE_ID"].ToString();
                        //objLeaveRequestModel.EmployeeModel.EmployeeImage = objReader["EMPLOYEE_IMAGE"] == DBNull.Value ? new byte[0] : (byte[])objReader["EMPLOYEE_IMAGE"];

                        listIndividualLeaveReq.Add(objLeaveRequestModel);
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

            return listIndividualLeaveReq;

        }
        public string ApprovedEmpLeaveRequestByTL(LeaveRequestModel objLeaveRequestModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_employee_leave_req_by_tl");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objLeaveRequestModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objLeaveRequestModel.LeaveFromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_Leave_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.LeaveFromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_Leave_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objLeaveRequestModel.LeaveToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_Leave_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.LeaveToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_Leave_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.BranchOfficeId;

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
        //approval by hr
        // Pending List for team leader
        public List<LeaveRequestModel> GetLeaveRequestPendingListForHR(LeaveRequestModel objLeaveRequestModel)
        {

            List<LeaveRequestModel> listIndividualLeaveReq = new List<LeaveRequestModel>();



            string sql = "";
            sql = "SELECT " +
                  "LEAVE_TYPE_NAME, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "DESIGNATION_NAME, " +
                   "JOINING_DATE, " +
                   "TO_CHAR(LEAVE_FROM_DATE,'dd/mm/yyyy')LEAVE_FROM_DATE, " +
                   "TO_CHAR(LEAVE_TO_DATE,'dd/mm/yyyy')LEAVE_TO_DATE, " +
                   "TOTAL_NO_OF_LEAVE, " +
                   "HEAD_OFFICE_ID, " +
                   "BRANCH_OFFICE_ID, " +
                   "REMARKS " +
                   " FROM VEW_EMPLOYEE_LEAVE_PL_FOR_HR where head_office_id = '" + objLeaveRequestModel.HeadOfficeId +
                   "' and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' " +
                  
                   " and create_date between to_date ('" + objLeaveRequestModel.LeaveStartDate + "', 'dd/mm/yyyy')  and to_date ('" + objLeaveRequestModel.LeaveEndDate + "', 'dd/mm/yyyy')   ";

            if (objLeaveRequestModel.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objLeaveRequestModel.EmployeeId + "' ";
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
                        objLeaveRequestModel = new LeaveRequestModel();
                        objLeaveRequestModel.EmployeeModel = new EmployeeModel();

                        objLeaveRequestModel.EmployeeId = objReader["employee_id"].ToString();
                        objLeaveRequestModel.EmployeeModel.EmployeeId = objReader["employee_id"].ToString();
                        objLeaveRequestModel.EmployeeModel.EmployeeName = objReader["EMPLOYEE_NAME"].ToString();
                        objLeaveRequestModel.EmployeeModel.JoiningDate = objReader["joining_date"].ToString();
                        objLeaveRequestModel.EmployeeModel.PresentDesignationName = objReader["designation_name"].ToString();
                        objLeaveRequestModel.LeaveTypeName = objReader["LEAVE_TYPE_NAME"].ToString();
                        objLeaveRequestModel.LeaveFromDate = objReader["LEAVE_FROM_DATE"].ToString();
                        objLeaveRequestModel.LeaveToDate = objReader["LEAVE_TO_DATE"].ToString();
                        objLeaveRequestModel.TotalNoOfLeave = objReader["TOTAL_NO_OF_LEAVE"].ToString();
                        objLeaveRequestModel.Remarks = objReader["REMARKS"].ToString();

                        objLeaveRequestModel.HeadOfficeId = objReader["HEAD_OFFICE_ID"].ToString();
                        objLeaveRequestModel.BranchOfficeId = objReader["BRANCH_OFFICE_ID"].ToString();
                        //objLeaveRequestModel.EmployeeModel.EmployeeImage = objReader["EMPLOYEE_IMAGE"] == DBNull.Value ? new byte[0] : (byte[])objReader["EMPLOYEE_IMAGE"];

                        listIndividualLeaveReq.Add(objLeaveRequestModel);
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

            return listIndividualLeaveReq;

        }
        public string ApprovedEmpLeaveRequestByHR(LeaveRequestModel objLeaveRequestModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_employee_leave_req_by_hr");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objLeaveRequestModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objLeaveRequestModel.LeaveFromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_Leave_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.LeaveFromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_Leave_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objLeaveRequestModel.LeaveToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_Leave_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.LeaveToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_Leave_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.BranchOfficeId;

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



        //INDIVIDUAL LEAVE PROCESS
        public string IndividualLeaveProess(LeaveRequestModel objLeaveRequestModel)
        {

            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_leave_individual_process");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objLeaveRequestModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objLeaveRequestModel.CurrentYear != "")
            {
                objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.CurrentYear;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }






            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.BranchOfficeId;

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



        public string IndividualLeaveProessTemp(LeaveRequestModel objLeaveRequestModel)
        {

            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_leave_process_temp");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objLeaveRequestModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objLeaveRequestModel.CurrentYear != "")
            {
                objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.CurrentYear;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }






            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.BranchOfficeId;

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



   //     public DataTable LoadEmployeeLeaveSummeryTempRecord(LeaveRequestModel objLeaveRequestModel)
   //     {

   //         DataTable dt = new DataTable();

   //         string sql = "";
   //         sql = "SELECT " +
   //             "rownum sl, " +
   //               "leave_type_name, " +
   //"total_leave, " +
   //"max_leave, " +
   //"remain_leave " +


   //               " FROM vew_leave_temp where head_office_id = '" + objLeaveRequestModel.HeadOfficeId + "' and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' AND  leave_year = '" + objLeaveRequestModel.CurrentYear + "' and employee_id = '" + objLeaveRequestModel.EmployeeId + "'   ";


   //         sql = sql + " order by SL ";

   //         OracleCommand objCommand = new OracleCommand(sql);
   //         OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
   //         using (OracleConnection strConn = GetConnection())
   //         {
   //             try
   //             {
   //                 objCommand.Connection = strConn;
   //                 strConn.Open();
   //                 objDataAdapter.Fill(dt);
   //                 dt.AcceptChanges();
   //             }
   //             catch (Exception ex)
   //             {

   //                 throw new Exception("Error : " + ex.Message);
   //             }

   //             finally
   //             {

   //                 strConn.Close();
   //             }

   //         }


   //         return dt;

   //     }



        public IEnumerable<LeaveRequestModel> LoadEmployeeLeaveSummeryTempRecord(LeaveRequestModel objLeaveRequestModel)
        {
            List<LeaveRequestModel> leaveSummaryList = new List<LeaveRequestModel>();


            string sql = "";
            sql = "SELECT " +
                "rownum sl, " +
                  "leave_type_name, " +
                   "total_leave, " +
                   "max_leave, " +
                   "leave_year, "+
                   "remain_leave " +
                  " FROM vew_leave_temp where head_office_id = '" + objLeaveRequestModel.HeadOfficeId + "' and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' AND  leave_year = '" + objLeaveRequestModel.CurrentYear + "' and employee_id = '" + objLeaveRequestModel.EmployeeId + "'   ";


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
                        objLeaveRequestModel = new LeaveRequestModel();
                        //objLeaveRequestModel.EmployeeModel = new EmployeeModel();

                        objLeaveRequestModel.LeaveTypeName = objDataReader["leave_type_name"].ToString();
                        objLeaveRequestModel.LeaveTaken = objDataReader["total_leave"].ToString();
                        objLeaveRequestModel.LeaveEntitled = objDataReader["max_leave"].ToString();
                        objLeaveRequestModel.LeaveRemain = objDataReader["remain_leave"].ToString();
                        objLeaveRequestModel.CurrentYear = objDataReader["leave_year"].ToString();

                        leaveSummaryList.Add(objLeaveRequestModel);
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

            return leaveSummaryList;

        }


        public List<LeaveRequestModel> GetLeaveRequestApprovedListForTL(LeaveRequestModel objLeaveRequestModel)
        {

            List<LeaveRequestModel> listIndividualLeaveReq = new List<LeaveRequestModel>();



            string sql = "";
            sql = "SELECT " +
                  "LEAVE_TYPE_NAME, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "DESIGNATION_NAME, " +
                   "JOINING_DATE, " +
                   "TO_CHAR(LEAVE_FROM_DATE,'dd/mm/yyyy')LEAVE_FROM_DATE, " +
                   "TO_CHAR(LEAVE_TO_DATE,'dd/mm/yyyy')LEAVE_TO_DATE, " +
                   "TOTAL_NO_OF_LEAVE, " +
                   "HEAD_OFFICE_ID, " +
                   "BRANCH_OFFICE_ID, " +
                   "REMARKS " +
                   " FROM VEW_EMPLOYEE_LEAVE_AL_FOR_TL where head_office_id = '" + objLeaveRequestModel.HeadOfficeId +
                   "' and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' " +
                
                   "and TEAM_LEADER_ID = '" + objLeaveRequestModel.TeamLeaderId +
                   "' and create_date between to_date ('" + objLeaveRequestModel.LeaveFromDate + "', 'dd/mm/yyyy')  and to_date ('" + objLeaveRequestModel.LeaveToDate + "', 'dd/mm/yyyy')   ";

            if (objLeaveRequestModel.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objLeaveRequestModel.EmployeeId + "' ";
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
                        objLeaveRequestModel = new LeaveRequestModel();
                        objLeaveRequestModel.EmployeeModel = new EmployeeModel();

                        objLeaveRequestModel.EmployeeId = objReader["employee_id"].ToString();
                        objLeaveRequestModel.EmployeeModel.EmployeeId = objReader["employee_id"].ToString();
                        objLeaveRequestModel.EmployeeModel.EmployeeName = objReader["EMPLOYEE_NAME"].ToString();
                        objLeaveRequestModel.EmployeeModel.JoiningDate = objReader["joining_date"].ToString();
                        objLeaveRequestModel.EmployeeModel.PresentDesignationName = objReader["designation_name"].ToString();
                        objLeaveRequestModel.LeaveTypeName = objReader["LEAVE_TYPE_NAME"].ToString();
                        objLeaveRequestModel.LeaveFromDate = objReader["LEAVE_FROM_DATE"].ToString();
                        objLeaveRequestModel.LeaveToDate = objReader["LEAVE_TO_DATE"].ToString();
                        objLeaveRequestModel.TotalNoOfLeave = objReader["TOTAL_NO_OF_LEAVE"].ToString();
                        objLeaveRequestModel.Remarks = objReader["REMARKS"].ToString();

                        objLeaveRequestModel.HeadOfficeId = objReader["HEAD_OFFICE_ID"].ToString();
                        objLeaveRequestModel.BranchOfficeId = objReader["BRANCH_OFFICE_ID"].ToString();
                        //objLeaveRequestModel.EmployeeModel.EmployeeImage = objReader["EMPLOYEE_IMAGE"] == DBNull.Value ? new byte[0] : (byte[])objReader["EMPLOYEE_IMAGE"];

                        listIndividualLeaveReq.Add(objLeaveRequestModel);
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

            return listIndividualLeaveReq;

        }



        public List<LeaveRequestModel> GetLeaveRequestApprovedListForHR(LeaveRequestModel objLeaveRequestModel)
        {

            List<LeaveRequestModel> listIndividualLeaveReq = new List<LeaveRequestModel>();



            string sql = "";
            sql = "SELECT " +
                  "LEAVE_TYPE_NAME, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "DESIGNATION_NAME, " +
                   "JOINING_DATE, " +
                   "TO_CHAR(LEAVE_FROM_DATE,'dd/mm/yyyy')LEAVE_FROM_DATE, " +
                   "TO_CHAR(LEAVE_TO_DATE,'dd/mm/yyyy')LEAVE_TO_DATE, " +
                   "TOTAL_NO_OF_LEAVE, " +
                   "HEAD_OFFICE_ID, " +
                   "BRANCH_OFFICE_ID, " +
                   "REMARKS " +
                   " FROM VEW_EMPLOYEE_LEAVE_AL_FOR_HR where head_office_id = '" + objLeaveRequestModel.HeadOfficeId +
                   "' and branch_office_id = '" + objLeaveRequestModel.BranchOfficeId + "' " +
                 
                   " and create_date between to_date ('" + objLeaveRequestModel.LeaveFromDate + "', 'dd/mm/yyyy')  and to_date ('" + objLeaveRequestModel.LeaveToDate + "', 'dd/mm/yyyy')   ";

            if (objLeaveRequestModel.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objLeaveRequestModel.EmployeeId + "' ";
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
                        objLeaveRequestModel = new LeaveRequestModel();
                        objLeaveRequestModel.EmployeeModel = new EmployeeModel();

                        objLeaveRequestModel.EmployeeId = objReader["employee_id"].ToString();
                        objLeaveRequestModel.EmployeeModel.EmployeeId = objReader["employee_id"].ToString();
                        objLeaveRequestModel.EmployeeModel.EmployeeName = objReader["EMPLOYEE_NAME"].ToString();
                        objLeaveRequestModel.EmployeeModel.JoiningDate = objReader["joining_date"].ToString();
                        objLeaveRequestModel.EmployeeModel.PresentDesignationName = objReader["designation_name"].ToString();
                        objLeaveRequestModel.LeaveTypeName = objReader["LEAVE_TYPE_NAME"].ToString();
                        objLeaveRequestModel.LeaveFromDate = objReader["LEAVE_FROM_DATE"].ToString();
                        objLeaveRequestModel.LeaveToDate = objReader["LEAVE_TO_DATE"].ToString();
                        objLeaveRequestModel.TotalNoOfLeave = objReader["TOTAL_NO_OF_LEAVE"].ToString();
                        objLeaveRequestModel.Remarks = objReader["REMARKS"].ToString();

                        objLeaveRequestModel.HeadOfficeId = objReader["HEAD_OFFICE_ID"].ToString();
                        objLeaveRequestModel.BranchOfficeId = objReader["BRANCH_OFFICE_ID"].ToString();
                        //objLeaveRequestModel.EmployeeModel.EmployeeImage = objReader["EMPLOYEE_IMAGE"] == DBNull.Value ? new byte[0] : (byte[])objReader["EMPLOYEE_IMAGE"];

                        listIndividualLeaveReq.Add(objLeaveRequestModel);
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

            return listIndividualLeaveReq;

        }



        public string SaveLeaveEntrybyHR(LeaveRequestModel objLeaveRequestModel)
        {



            string strMsg = "";


            int x = objLeaveRequestModel.GridLeaveTypeId.Count();
            for (int i = 0; i < x; i++)
            {
                //var GridEmployeeId = objLeaveRequestModel.GridEmployeeId[i];
                var GridLeaveTypeId = objLeaveRequestModel.GridLeaveTypeId[i];
                var GridLeaveStartDate = objLeaveRequestModel.GridLeaveStartDate[i];
                var GridLeaveEndDate = objLeaveRequestModel.GridLeaveEndDate[i];
                //var GridApprovedEmployeeId = objLeaveRequestModel.GridApprovedEmployeeId[i];
                var GridRemarks = objLeaveRequestModel.GridRemarks[i];



                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("PRO_EMPLOYEE_LEAVE_SAVE_HR");
                objOracleCommand.CommandType = CommandType.StoredProcedure;


                if (!string.IsNullOrWhiteSpace(objLeaveRequestModel.EmployeeId))
                {
                    objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveRequestModel.EmployeeId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (objLeaveRequestModel.GridLeaveTypeId.Length > 0)
                {
                    objOracleCommand.Parameters.Add("p_leave_type_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = GridLeaveTypeId;
                }

                else
                {
                    objOracleCommand.Parameters.Add("p_leave_type_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (objLeaveRequestModel.GridLeaveStartDate.Length > 0)
                {
                    objOracleCommand.Parameters.Add("p_leave_start_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = GridLeaveStartDate;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_leave_start_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (objLeaveRequestModel.GridLeaveEndDate.Length > 0)
                {
                    objOracleCommand.Parameters.Add("p_leave_end_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = GridLeaveEndDate;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_leave_end_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (objLeaveRequestModel.GridRemarks.Length > 0)
                {
                    objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = GridRemarks;
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.CurrentYear;

                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLeaveRequestModel.BranchOfficeId;

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
                        objOracleTransaction.Rollback();
                        throw new Exception("Error : " + ex.Message);
                    }

                    finally
                    {
                        strConn.Close();
                    }
                }
            }
            return strMsg;
        }



        //  "AND  LEAVE_YEAR = '"+objLeaveRequestModel.CurrentYear+"' " +


    }
}
