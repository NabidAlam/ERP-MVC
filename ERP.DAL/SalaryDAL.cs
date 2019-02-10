using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.MODEL;
using Oracle.ManagedDataAccess.Client;

namespace ERP.DAL
{
    public class SalaryDAL
    {
        OracleTransaction objTransaction;
        private OracleTransaction trans = null;

        #region Oracle Connection

        private OracleConnection GetConnection()
        {
            var conString = ConfigurationManager.ConnectionStrings["DBConString"];
            string strConnString = conString.ConnectionString;
            return new OracleConnection(strConnString);
        }

        #endregion


        #region Working Day

        public List<WorkingDayModel> GetAllCOEmployeesForMonthlySalary(WorkingDayModel objWorkingDayModel)
        {
            List<WorkingDayModel> workingDayList = new List<WorkingDayModel>();

            string vMessage = "";


            string vQuery = "SELECT 'Y' from VEW_SALARY_PERMISSION where employee_id = '" + objWorkingDayModel.UpdateBy +
                            "' and permission_yn='Y' AND HEAD_OFFICE_ID = '" + objWorkingDayModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID ='" + objWorkingDayModel.BranchOfficeId + "' ";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        vMessage = objDataReader.GetString(0);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objConnection.Close();
                }
            }



            if (vMessage == "Y")
            {
                string vQuery1 = "SELECT " +
                                 "rownum SL, " +
                                 "EMPLOYEE_ID, " +
                                 "EMPLOYEE_NAME, " +
                                 "DESIGNATION_NAME, " +
                                 "to_char(JOINING_DATE, 'dd/mm/yyyy')JOINING_DATE, " +
                                 "DEPARTMENT_NAME, " +
                                 "CARD_NO, " +
                                 "SALARY_YEAR, " +
                                 "MONTH_ID, " +
                                 "WORKING_DAY, " +
                                 "ADVANCE_AMOUNT, " +
                                 "LEAVE_ALLOWANCE, " +
                                 "TAX_DEDUCATION_AMOUNT, " +
                                 "PF_ADVANCE_AMOUNT," +
                                 "FOOD_DEDUCTION_AMOUNT, " +
                                 "GROSS_SALARY, " +
                                 "MONTH_DAY, " +
                                 "ARREAR_AMOUNT " +

                                 //"FROM VEW_SALARY_RECORD_FOR_SO where salary_year = (TO_CHAR(TO_DATE ('" + objWorkingDayModel.ToDate + "', 'dd/mm/yyyy'), 'YYYY')) and month_id = (TO_CHAR(TO_DATE ('" + objWorkingDayModel.ToDate + "', 'dd/mm/yyyy'), 'MM')) and   head_office_id = '" + objWorkingDayModel.HeadOfficeId + "' and branch_office_id = '" + objWorkingDayModel.BranchOfficeId + "'   ";
                                 "FROM VEW_SALARY_RECORD_FOR_SO where salary_year = '" + objWorkingDayModel.SalaryYear + "' and month_id = '" + objWorkingDayModel.MonthId + "' and   head_office_id = '" + objWorkingDayModel.HeadOfficeId + "' and branch_office_id = '" + objWorkingDayModel.BranchOfficeId + "'   ";

                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.EmployeeName))
                {
                    vQuery1 += "and (lower(employee_name) like lower( '%" + objWorkingDayModel.EmployeeName + "%')  or upper(employee_name)like upper('%" + objWorkingDayModel.EmployeeName + "%') )";
                }
                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.EmployeeId))
                {
                    vQuery1 += "and employee_id = '" + objWorkingDayModel.EmployeeId + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.CardNumber))
                {
                    vQuery1 += "and CARD_NO = '" + objWorkingDayModel.CardNumber + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.UnitId))
                {
                    vQuery1 += "and unit_id = '" + objWorkingDayModel.UnitId + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.DepartmentId))
                {
                    vQuery1 += "and department_id = '" + objWorkingDayModel.DepartmentId + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.SectionId))
                {
                    vQuery1 += "and section_id = '" + objWorkingDayModel.SectionId + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.SubSectionId))
                {
                    vQuery1 += "and sub_section_id = '" + objWorkingDayModel.SubSectionId + "' ";
                }

                vQuery1 += " ORDER BY SL";


                using (OracleConnection objConnection = GetConnection())
                {
                    OracleCommand objCommand = new OracleCommand(vQuery1, objConnection);
                    objConnection.Open();
                    OracleDataReader objReader = objCommand.ExecuteReader();

                    try
                    {
                        while (objReader.Read())
                        {
                            objWorkingDayModel = new WorkingDayModel();

                            //objWorkingDayModel.SerialNumber = Convert.ToInt32(objReader["SL"]).ToString();
                            objWorkingDayModel.EmployeeId = objReader["EMPLOYEE_ID"].ToString();
                            objWorkingDayModel.EmployeeName = objReader["EMPLOYEE_NAME"].ToString();
                            objWorkingDayModel.DesignationName = objReader["DESIGNATION_NAME"].ToString();
                            objWorkingDayModel.JoiningDate = objReader["JOINING_DATE"].ToString();
                            objWorkingDayModel.DepartmentName = objReader["DEPARTMENT_NAME"].ToString();
                            objWorkingDayModel.CardNumber = objReader["CARD_NO"].ToString();
                            objWorkingDayModel.SalaryYear = objReader["SALARY_YEAR"].ToString();
                            objWorkingDayModel.MonthId = objReader["MONTH_ID"].ToString();
                            objWorkingDayModel.WorkingDay = objReader["WORKING_DAY"].ToString();
                            objWorkingDayModel.Advance = objReader["ADVANCE_AMOUNT"].ToString();
                            //objWorkingDayModel.LeaveAllowance = objReader["LEAVE_ALLOWANCE"].ToString();
                            objWorkingDayModel.Tax = objReader["TAX_DEDUCATION_AMOUNT"].ToString();
                            //objWorkingDayModel.PFAdvanceAmount = objReader["PF_ADVANCE_AMOUNT"].ToString();
                            //objWorkingDayModel.FoodDeductionAmount = objReader["FOOD_DEDUCTION_AMOUNT"].ToString();
                            //objWorkingDayModel.GrossSalary = objReader["GROSS_SALARY"].ToString();
                            objWorkingDayModel.MonthDay = objReader["MONTH_DAY"].ToString();
                            objWorkingDayModel.Arrear = objReader["ARREAR_AMOUNT"].ToString();

                            workingDayList.Add(objWorkingDayModel);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        objConnection.Close();
                    }
                }
            }
            else
            {
                string vQuery2 = "SELECT " +
                                 "rownum SL, " +
                                 "EMPLOYEE_ID, " +
                                 "EMPLOYEE_NAME, " +
                                 "DESIGNATION_NAME, " +
                                 "to_char(JOINING_DATE, 'dd/mm/yyyy')JOINING_DATE, " +
                                 "DEPARTMENT_NAME, " +
                                 "CARD_NO, " +
                                 "SALARY_YEAR, " +
                                 "MONTH_ID, " +
                                 "WORKING_DAY, " +
                                 "ADVANCE_AMOUNT, " +
                                 "LEAVE_ALLOWANCE, " +
                                 "TAX_DEDUCATION_AMOUNT, " +
                                 "PF_ADVANCE_AMOUNT," +
                                 "FOOD_DEDUCTION_AMOUNT, " +
                                 "0 GROSS_SALARY, " +
                                 "MONTH_DAY, " +
                                 "ARREAR_AMOUNT " +

                                 //"FROM VEW_SALARY_RECORD_FOR_SO where salary_year = (TO_CHAR(TO_DATE ('" + objWorkingDayModel.ToDate + "', 'dd/mm/yyyy'), 'YYYY')) and month_id = (TO_CHAR(TO_DATE ('" + objWorkingDayModel.ToDate + "', 'dd/mm/yyyy'), 'MM')) and head_office_id = '" + objWorkingDayModel.HeadOfficeId + "' and branch_office_id = '" + objWorkingDayModel.BranchOfficeId + "' ";
                                 "FROM VEW_SALARY_RECORD_FOR_SO where salary_year = '" + objWorkingDayModel.SalaryYear + "' and month_id = '" + objWorkingDayModel.MonthId + "' and head_office_id = '" + objWorkingDayModel.HeadOfficeId + "' and branch_office_id = '" + objWorkingDayModel.BranchOfficeId + "' ";

                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.EmployeeName))
                {
                    vQuery2 += "and (lower(employee_name) like lower( '%" + objWorkingDayModel.EmployeeName + "%')  or upper(employee_name)like upper('%" + objWorkingDayModel.EmployeeName + "%') )";
                }
                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.EmployeeId))
                {
                    vQuery2 += "and employee_id = '" + objWorkingDayModel.EmployeeId + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.CardNumber))
                {
                    vQuery2 += "and CARD_NO = '" + objWorkingDayModel.CardNumber + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.UnitId))
                {
                    vQuery2 += "and unit_id = '" + objWorkingDayModel.UnitId + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.DepartmentId))
                {
                    vQuery2 += "and department_id = '" + objWorkingDayModel.DepartmentId + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.SectionId))
                {
                    vQuery2 += "and section_id = '" + objWorkingDayModel.SectionId + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objWorkingDayModel.SubSectionId))
                {
                    vQuery2 += "and sub_section_id = '" + objWorkingDayModel.SubSectionId + "' ";
                }

                vQuery2 += " ORDER BY SL";


                using (OracleConnection objConnection = GetConnection())
                {
                    OracleCommand objCommand = new OracleCommand(vQuery2, objConnection);
                    objConnection.Open();
                    OracleDataReader objReader = objCommand.ExecuteReader();

                    try
                    {
                        while (objReader.Read())
                        {
                            objWorkingDayModel = new WorkingDayModel();

                            //objWorkingDayModel.SerialNumber = Convert.ToInt32(objReader["SL"]).ToString();
                            objWorkingDayModel.EmployeeId = objReader["EMPLOYEE_ID"].ToString();
                            objWorkingDayModel.EmployeeName = objReader["EMPLOYEE_NAME"].ToString();
                            objWorkingDayModel.DesignationName = objReader["DESIGNATION_NAME"].ToString();
                            objWorkingDayModel.JoiningDate = objReader["JOINING_DATE"].ToString();
                            objWorkingDayModel.DepartmentName = objReader["DEPARTMENT_NAME"].ToString();
                            objWorkingDayModel.CardNumber = objReader["CARD_NO"].ToString();
                            objWorkingDayModel.SalaryYear = objReader["SALARY_YEAR"].ToString();
                            objWorkingDayModel.MonthId = objReader["MONTH_ID"].ToString();
                            objWorkingDayModel.WorkingDay = objReader["WORKING_DAY"].ToString();
                            objWorkingDayModel.Advance = objReader["ADVANCE_AMOUNT"].ToString();
                            //objWorkingDayModel.LeaveAllowance = objReader["LEAVE_ALLOWANCE"].ToString();
                            objWorkingDayModel.Tax = objReader["TAX_DEDUCATION_AMOUNT"].ToString();
                            //objWorkingDayModel.PFAdvanceAmount = objReader["PF_ADVANCE_AMOUNT"].ToString();
                            //objWorkingDayModel.FoodDeductionAmount = objReader["FOOD_DEDUCTION_AMOUNT"].ToString();
                            //objWorkingDayModel.GrossSalary = objReader["GROSS_SALARY"].ToString();
                            objWorkingDayModel.MonthDay = objReader["MONTH_DAY"].ToString();
                            objWorkingDayModel.Arrear = objReader["ARREAR_AMOUNT"].ToString();

                            workingDayList.Add(objWorkingDayModel);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        objConnection.Close();
                    }
                }
            }

            return workingDayList;
        }

        public string AddCOStaffRecordForMonthlySalary(WorkingDayModel objWorkingDayModel)
        {
            string vMessage;

            OracleCommand objOracleCommand = new OracleCommand { CommandText = "pro_salary_add_co", CommandType = CommandType.StoredProcedure };

            objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SalaryYear) ? objWorkingDayModel.SalaryYear : null;
            objOracleCommand.Parameters.Add("P_MONTH_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.MonthId) ? objWorkingDayModel.MonthId : null;

            //objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.FromDate) ? objWorkingDayModel.FromDate : null;
            //objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.ToDate) ? objWorkingDayModel.ToDate : null;

            objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.UnitId) ? objWorkingDayModel.UnitId : null;
            objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.DepartmentId) ? objWorkingDayModel.DepartmentId : null;
            objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SectionId) ? objWorkingDayModel.SectionId : null;
            objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SubSectionId) ? objWorkingDayModel.SubSectionId : null;

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.BranchOfficeId;

            objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            using (OracleConnection objConnection = GetConnection())
            {
                try
                {
                    objOracleCommand.Connection = objConnection;
                    objConnection.Open();
                    objTransaction = objConnection.BeginTransaction();
                    objOracleCommand.ExecuteNonQuery();
                    objTransaction.Commit();
                    objConnection.Close();

                    vMessage = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                }
                catch (Exception ex)
                {
                    objTransaction.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objTransaction.Dispose();
                    objConnection.Close();
                }
            }

            return vMessage;
        }

        public string CalculateWorkingDayCOForMonthlySalary(WorkingDayModel objWorkingDayModel)
        {
            string vMessage;

            OracleCommand objOracleCommand = new OracleCommand { CommandText = "PRO_CALCULATE_WD_FOR_CO", CommandType = CommandType.StoredProcedure };

            objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SalaryYear) ? objWorkingDayModel.SalaryYear : null;
            objOracleCommand.Parameters.Add("P_MONTH_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.MonthId) ? objWorkingDayModel.MonthId : null;

            //objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.FromDate) ? objWorkingDayModel.FromDate : null;
            //objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.ToDate) ? objWorkingDayModel.ToDate : null;

            objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.UnitId) ? objWorkingDayModel.UnitId : null;
            objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.DepartmentId) ? objWorkingDayModel.DepartmentId : null;
            objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SectionId) ? objWorkingDayModel.SectionId : null;
            objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SubSectionId) ? objWorkingDayModel.SubSectionId : null;

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.BranchOfficeId;

            objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objOracleCommand.Connection = strConn;
                    strConn.Open();
                    objTransaction = strConn.BeginTransaction();
                    objOracleCommand.ExecuteNonQuery();
                    objTransaction.Commit();
                    strConn.Close();

                    vMessage = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                }
                catch (Exception ex)
                {
                    objTransaction.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objTransaction.Dispose();
                    strConn.Close();
                }
            }

            return vMessage;
        }

        public string UpdateWorkingDayCOForMonthlySalary(WorkingDayModel objWorkingDayModel)
        {
            string vMessage = null;

            foreach (WorkingDayModel model in objWorkingDayModel.WorkingDayList)
            {
                OracleCommand objOracleCommand = new OracleCommand { CommandText = "PRO_SALARY_INFO_CO", CommandType = CommandType.StoredProcedure };

                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.EmployeeId) ? model.EmployeeId : null;

                objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SalaryYear) ? objWorkingDayModel.SalaryYear : null;
                objOracleCommand.Parameters.Add("P_MONTH_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.MonthId) ? objWorkingDayModel.MonthId : null;

                //objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.FromDate) ? objWorkingDayModel.FromDate : null;
                //objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.ToDate) ? objWorkingDayModel.ToDate : null;

                objOracleCommand.Parameters.Add("p_working_day", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.WorkingDay) ? model.WorkingDay : null;

                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.BranchOfficeId;

                objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                using (OracleConnection objConnection = GetConnection())
                {
                    try
                    {
                        objOracleCommand.Connection = objConnection;
                        objConnection.Open();
                        objTransaction = objConnection.BeginTransaction();
                        objOracleCommand.ExecuteNonQuery();
                        objTransaction.Commit();
                        objConnection.Close();

                        vMessage = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        objTransaction.Rollback();
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        objTransaction.Dispose();
                        objConnection.Close();
                    }
                }
            }

            return vMessage;
        }

        #endregion

        #region Salary AAT

        public List<WorkingDayModel> GetAllCOEmployeesForAAT(WorkingDayModel objWorkingDayModel)
        {
            List<WorkingDayModel> workingDayList = new List<WorkingDayModel>();

            string vQuery = "SELECT " +
                            "rownum SL, " +
                            "EMPLOYEE_ID, " +
                            "EMPLOYEE_NAME, " +
                            "DESIGNATION_NAME, " +
                            "to_char(JOINING_DATE, 'dd/mm/yyyy')JOINING_DATE, " +
                            "DEPARTMENT_NAME, " +
                            "CARD_NO, " +
                            "SALARY_YEAR, " +
                            "MONTH_ID, " +
                            "ADVANCE_AMOUNT, " +
                            "TAX_DEDUCATION_AMOUNT, " +
                            "ARREAR_AMOUNT " +

                            "FROM VEW_SALARY_RECORD_FOR_SCO_AAT where salary_year ='" + objWorkingDayModel.SalaryYear + "' and month_id = '" + objWorkingDayModel.MonthId + "' and   head_office_id = '" + objWorkingDayModel.HeadOfficeId + "' and branch_office_id = '" + objWorkingDayModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objWorkingDayModel.EmployeeName))
            {
                vQuery += "and (lower(employee_name) like lower( '%" + objWorkingDayModel.EmployeeName + "%')  or upper(employee_name)like upper('%" + objWorkingDayModel.EmployeeName + "%') )";
            }
            if (!string.IsNullOrWhiteSpace(objWorkingDayModel.EmployeeId))
            {
                vQuery += "and employee_id = '" + objWorkingDayModel.EmployeeId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objWorkingDayModel.UnitId))
            {
                vQuery += "and unit_id = '" + objWorkingDayModel.UnitId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objWorkingDayModel.DepartmentId))
            {
                vQuery += "and department_id = '" + objWorkingDayModel.DepartmentId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objWorkingDayModel.SectionId))
            {
                vQuery += "and section_id = '" + objWorkingDayModel.SectionId + "' ";
            }
            if (!string.IsNullOrWhiteSpace(objWorkingDayModel.SubSectionId))
            {
                vQuery += "and sub_section_id = '" + objWorkingDayModel.SubSectionId + "' ";
            }

            vQuery += "ORDER BY SL";


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        objWorkingDayModel = new WorkingDayModel();

                        //objWorkingDayModel.SerialNumber = Convert.ToInt32(objReader["SL"]).ToString();
                        objWorkingDayModel.EmployeeId = objReader["EMPLOYEE_ID"].ToString();
                        objWorkingDayModel.EmployeeName = objReader["EMPLOYEE_NAME"].ToString();
                        objWorkingDayModel.DesignationName = objReader["DESIGNATION_NAME"].ToString();
                        objWorkingDayModel.JoiningDate = objReader["JOINING_DATE"].ToString();
                        objWorkingDayModel.DepartmentName = objReader["DEPARTMENT_NAME"].ToString();
                        objWorkingDayModel.CardNumber = objReader["CARD_NO"].ToString();
                        objWorkingDayModel.SalaryYear = objReader["SALARY_YEAR"].ToString();
                        objWorkingDayModel.MonthId = objReader["MONTH_ID"].ToString();
                        //objWorkingDayModel.WorkingDay = objReader["WORKING_DAY"].ToString();
                        objWorkingDayModel.Advance = objReader["ADVANCE_AMOUNT"].ToString();
                        //objWorkingDayModel.LeaveAllowance = objReader["LEAVE_ALLOWANCE"].ToString();
                        objWorkingDayModel.Tax = objReader["TAX_DEDUCATION_AMOUNT"].ToString();
                        //objWorkingDayModel.PFAdvanceAmount = objReader["PF_ADVANCE_AMOUNT"].ToString();
                        //objWorkingDayModel.FoodDeductionAmount = objReader["FOOD_DEDUCTION_AMOUNT"].ToString();
                        //objWorkingDayModel.GrossSalary = objReader["GROSS_SALARY"].ToString();
                        //objWorkingDayModel.MonthDay = objReader["MONTH_DAY"].ToString();
                        objWorkingDayModel.Arrear = objReader["ARREAR_AMOUNT"].ToString();

                        workingDayList.Add(objWorkingDayModel);
                    }

                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objConnection.Close();
                }
            }

            return workingDayList;
        }

        public string AddCOStaffRecordForAAT(WorkingDayModel objWorkingDayModel)
        {
            string vMessage;

            OracleCommand objOracleCommand = new OracleCommand { CommandText = "PRO_SALARY_ADD_CO_AAT", CommandType = CommandType.StoredProcedure };

            objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SalaryYear) ? objWorkingDayModel.SalaryYear : null;
            objOracleCommand.Parameters.Add("P_MONTH_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.MonthId) ? objWorkingDayModel.MonthId : null;
            objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.UnitId) ? objWorkingDayModel.UnitId : null;
            objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.DepartmentId) ? objWorkingDayModel.DepartmentId : null;
            objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SectionId) ? objWorkingDayModel.SectionId : null;
            objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SubSectionId) ? objWorkingDayModel.SubSectionId : null;

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.BranchOfficeId;

            objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            using (OracleConnection objConnection = GetConnection())
            {
                try
                {
                    objOracleCommand.Connection = objConnection;
                    objConnection.Open();
                    objTransaction = objConnection.BeginTransaction();
                    objOracleCommand.ExecuteNonQuery();
                    objTransaction.Commit();
                    objConnection.Close();

                    vMessage = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                }
                catch (Exception ex)
                {
                    objTransaction.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objTransaction.Dispose();
                    objConnection.Close();
                }
            }

            return vMessage;
        }

        public string UpdateSalaryAATInfoCO(WorkingDayModel objWorkingDayModel)
        {
            string vMessage = null;

            foreach (WorkingDayModel model in objWorkingDayModel.WorkingDayList)
            {
                if (!string.IsNullOrWhiteSpace(model.Advance) || !string.IsNullOrWhiteSpace(model.Arrear) || !string.IsNullOrWhiteSpace(model.Tax))
                {
                    OracleCommand objOracleCommand = new OracleCommand { CommandText = "PRO_CO_AAT_ENTRY", CommandType = CommandType.StoredProcedure };

                    objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.EmployeeId) ? model.EmployeeId : null;
                    objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SalaryYear) ? objWorkingDayModel.SalaryYear : null;
                    objOracleCommand.Parameters.Add("P_MONTH_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.MonthId) ? objWorkingDayModel.MonthId : null;
                    objOracleCommand.Parameters.Add("P_ADVANCE_AMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.Advance) ? model.Advance : null;
                    objOracleCommand.Parameters.Add("P_ARREAR_AMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.Arrear) ? model.Arrear : null;
                    objOracleCommand.Parameters.Add("TAX_DEDUCATION_AMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(model.Tax) ? model.Tax : null;

                    objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.UpdateBy;
                    objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.BranchOfficeId;

                    objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                    using (OracleConnection objConnection = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = objConnection;
                            objConnection.Open();
                            objTransaction = objConnection.BeginTransaction();
                            objOracleCommand.ExecuteNonQuery();
                            objTransaction.Commit();
                            objConnection.Close();

                            vMessage = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                        }
                        catch (Exception ex)
                        {
                            objTransaction.Rollback();
                            throw new Exception("Error : " + ex.Message);
                        }
                        finally
                        {
                            objTransaction.Dispose();
                            objConnection.Close();
                        }
                    }
                }
            }

            return vMessage;
        }

        #endregion

        #region Salary Process

        public string ProcessMonthlySalaryCO(WorkingDayModel objWorkingDayModel)
        {
            string vMessage;

            OracleCommand objOracleCommand = new OracleCommand { CommandText = "pro_salary_process_sco", CommandType = CommandType.StoredProcedure };

            objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SalaryYear) ? objWorkingDayModel.SalaryYear : null;
            objOracleCommand.Parameters.Add("P_MONTH_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.MonthId) ? objWorkingDayModel.MonthId : null;
            objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.UnitId) ? objWorkingDayModel.UnitId : null;
            objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.DepartmentId) ? objWorkingDayModel.DepartmentId : null;
            objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SectionId) ? objWorkingDayModel.SectionId : null;
            objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objWorkingDayModel.SubSectionId) ? objWorkingDayModel.SubSectionId : null;

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objWorkingDayModel.BranchOfficeId;

            objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            using (OracleConnection objConnection = GetConnection())
            {
                try
                {
                    objOracleCommand.Connection = objConnection;
                    objConnection.Open();
                    objTransaction = objConnection.BeginTransaction();
                    objOracleCommand.ExecuteNonQuery();
                    objTransaction.Commit();
                    objConnection.Close();

                    vMessage = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                }
                catch (Exception ex)
                {
                    objTransaction.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objTransaction.Dispose();
                    objConnection.Close();
                }
            }

            return vMessage;
        }

        #endregion

        #region Employee Earn Leave

        public List<EmployeeEarnLeaveModel> GetEarnLeaveRecord(EmployeeEarnLeaveModel objEarnLeaveModel)
        {
            List<EmployeeEarnLeaveModel> listEarnLeave = new List<EmployeeEarnLeaveModel>();
            string sql1 = "";

            sql1 = "SELECT " +
                   "rownum SL, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "DESIGNATION_NAME, " +
                   "to_char(JOINING_DATE, 'dd/mm/yyyy')JOINING_DATE, " +
                   "DEPARTMENT_NAME, " +
                   "EL_DAY, " +
                   "NET_PAYMENT_AMOUNT, " +
                   "total_month " +

                   "FROM vew_el_record where EARN_LEAVE_YEAR ='" + objEarnLeaveModel.SalaryYear + "'  and   head_office_id = '" + objEarnLeaveModel.HeadOfficeId + "' and branch_office_id = '" + objEarnLeaveModel.BranchOfficeId + "'   ";

            if (objEarnLeaveModel.EmployeeName != null)
            {

                sql1 = sql1 + "and (lower(employee_name) like lower( '%" + objEarnLeaveModel.EmployeeId + "%')  or upper(employee_name)like upper('%" + objEarnLeaveModel.EmployeeName + "%') )";
            }

            if (objEarnLeaveModel.EmployeeId != null)
            {

                sql1 = sql1 + "and employee_id = '" + objEarnLeaveModel.EmployeeId + "' ";
            }

            if (objEarnLeaveModel.UnitId != null)
            {

                sql1 = sql1 + "and unit_id = '" + objEarnLeaveModel.UnitId + "' ";
            }

            if (objEarnLeaveModel.SectionId != null)
            {

                sql1 = sql1 + "and section_id = '" + objEarnLeaveModel.SectionId + "' ";
            }

            if (objEarnLeaveModel.SubSectionId != null)
            {

                sql1 = sql1 + "and sub_section_id = '" + objEarnLeaveModel.SubSectionId + "' ";
            }
            if (objEarnLeaveModel.DepartmentId != null)
            {

                sql1 = sql1 + "and department_id = '" + objEarnLeaveModel.DepartmentId + "' ";
            }
            sql1 = sql1 + " ORDER BY SL ";


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql1, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        EmployeeEarnLeaveModel objEarnModel = new EmployeeEarnLeaveModel() { };

                        objEarnModel.SerialNumber = Convert.ToInt32(objReader["SL"]).ToString();
                        objEarnModel.EmployeeId = objReader["EMPLOYEE_ID"].ToString();
                        objEarnModel.EmployeeName = objReader["EMPLOYEE_NAME"].ToString();
                        objEarnModel.DesignationName = objReader["DESIGNATION_NAME"].ToString();
                        objEarnModel.JoiningDate = objReader["JOINING_DATE"].ToString();
                        objEarnModel.DepartmentName = objReader["DEPARTMENT_NAME"].ToString();
                        objEarnModel.EarnLeaveDay = objReader["EL_DAY"].ToString();
                        objEarnModel.Amount = objReader["NET_PAYMENT_AMOUNT"].ToString();
                        objEarnModel.TotalMonth = objReader["total_month"].ToString();

                        listEarnLeave.Add(objEarnModel);
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

            return listEarnLeave;

        }

        public string AddEarnLeaveEmployee(EmployeeEarnLeaveModel objEarnLeaveModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_earn_leave_add")
            {
                CommandType = CommandType.StoredProcedure
            };


            if (objEarnLeaveModel.SalaryYear != null)
            {
                objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEarnLeaveModel.SalaryYear;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objEarnLeaveModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEarnLeaveModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objEarnLeaveModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEarnLeaveModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objEarnLeaveModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEarnLeaveModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objEarnLeaveModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEarnLeaveModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEarnLeaveModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEarnLeaveModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEarnLeaveModel.BranchOfficeId;

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
                }

                finally
                {

                    strConn.Close();
                }

            }


            return strMsg;

        }

        public string EarnLeaveProcess(EmployeeEarnLeaveModel objEarnLeaveProcessModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_earn_leave_process");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objEarnLeaveProcessModel.SalaryYear != null)
            {
                objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEarnLeaveProcessModel.SalaryYear;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objEarnLeaveProcessModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEarnLeaveProcessModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objEarnLeaveProcessModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEarnLeaveProcessModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objEarnLeaveProcessModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEarnLeaveProcessModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objEarnLeaveProcessModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEarnLeaveProcessModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEarnLeaveProcessModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEarnLeaveProcessModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEarnLeaveProcessModel.BranchOfficeId;

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

        public string SaveEarnLearnInfo(EmployeeEarnLeaveModel objEarnLeaveSaveModel)
        {
            string strMsg = "";

            foreach (EmployeeEarnLeaveModel el in objEarnLeaveSaveModel.EmployeeEarnLeaveList)
            {
                OracleCommand objOracleCommand = new OracleCommand("PRO_EL_BASIC_INFO");
                objOracleCommand.CommandType = CommandType.StoredProcedure;

                if (el.EmployeeId != "")
                {
                    objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = el.EmployeeId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }
                if (objEarnLeaveSaveModel.SalaryYear != "")
                {
                    objOracleCommand.Parameters.Add("P_EL_YEAR", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEarnLeaveSaveModel.SalaryYear;
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_EL_YEAR", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }
                if (el.EarnLeaveDay != "")
                {
                    objOracleCommand.Parameters.Add("P_EL_DAY", OracleDbType.Varchar2, ParameterDirection.Input).Value = el.EarnLeaveDay;
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_EL_DAY", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEarnLeaveSaveModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEarnLeaveSaveModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEarnLeaveSaveModel.BranchOfficeId;

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
            return strMsg;
        }

        #endregion
		
		
		     #region LOAN ADVANCE







        public IEnumerable<LoanAdvanceModel> LoadAdvanceRecord(LoanAdvanceModel objLoanAdvanceModel)
        {

            List<LoanAdvanceModel> loanAdvanceList = new List<LoanAdvanceModel>();

            string sql = "";
            sql = "SELECT " +
                "rownum sl, " +
                "loan_year, " +
                "month_name, " +
                "loan_amount, " +
                "deduction_amount, " +
                "remaining_amount, " +
                "STATUS " +
                  " FROM VEW_SEARCH_ADVANCE_LOAN where head_office_id = '" + objLoanAdvanceModel.HeadOfficeId + "' and branch_office_id = '" + objLoanAdvanceModel.BranchOfficeId + "' AND employee_id = '" + objLoanAdvanceModel.EmployeeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objLoanAdvanceModel.EmployeeId))
            {

                sql = sql + "and employee_id = '" + objLoanAdvanceModel.EmployeeId + "' ";
            }


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
                        objLoanAdvanceModel = new LoanAdvanceModel();
                        objLoanAdvanceModel.Serial = objDataReader["sl"].ToString();
                        objLoanAdvanceModel.Year = objDataReader["loan_year"].ToString();
                        objLoanAdvanceModel.MonthName = objDataReader["month_name"].ToString();
                        objLoanAdvanceModel.LoanAmount = objDataReader["loan_amount"].ToString();
                        objLoanAdvanceModel.DeductionAmount = objDataReader["deduction_amount"].ToString();
                        objLoanAdvanceModel.RemainingAmount = objDataReader["remaining_amount"].ToString();
                        objLoanAdvanceModel.Status = objDataReader["STATUS"].ToString();

                        loanAdvanceList.Add(objLoanAdvanceModel);
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

            return loanAdvanceList;
        }



        public LoanAdvanceModel GetEmployeeRecordById(LoanAdvanceModel objLoanAdvanceModel)
        {


            string vQuery = "";
            vQuery = "SELECT " +

                    "EMPLOYEE_ID, " +
                    "EMPLOYEE_NAME, " +
                    "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                    "DESIGNATION_NAME, " +
                    "HEAD_OFFICE_ID, " +
                    "BRANCH_OFFICE_ID, " +
                    "EMPLOYEE_PIC " +

                    "FROM VEW_PERSONAL_INFO where head_office_id = '" + objLoanAdvanceModel.HeadOfficeId + "' and branch_office_id = '" + objLoanAdvanceModel.BranchOfficeId + "' AND employee_id = '" + objLoanAdvanceModel.EmployeeId + "'   ";

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
                        objLoanAdvanceModel = new LoanAdvanceModel();
                        objLoanAdvanceModel.EmployeeModel = new EmployeeModel();

                        objLoanAdvanceModel.EmployeeId = objDataReader["employee_id"].ToString();
                        objLoanAdvanceModel.EmployeeModel.EmployeeId = objDataReader["employee_id"].ToString();
                        objLoanAdvanceModel.EmployeeModel.EmployeeName = objDataReader["EMPLOYEE_NAME"].ToString();
                        objLoanAdvanceModel.EmployeeModel.JoiningDate = objDataReader["joining_date"].ToString();
                        objLoanAdvanceModel.EmployeeModel.PresentDesignationName = objDataReader["designation_name"].ToString();
                        objLoanAdvanceModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                        objLoanAdvanceModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();
                        objLoanAdvanceModel.EmployeeModel.EmployeeImage = objDataReader["EMPLOYEE_PIC"] == DBNull.Value ? new byte[0] : (byte[])objDataReader["EMPLOYEE_PIC"];
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

            return objLoanAdvanceModel;
        }




        public string AdvanceLoanRecordSave(LoanAdvanceModel objLoanAdvanceModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_advance_loan_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objLoanAdvanceModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLoanAdvanceModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objLoanAdvanceModel.Year != "")
            {
                objOracleCommand.Parameters.Add("p_loan_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLoanAdvanceModel.Year;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_loan_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objLoanAdvanceModel.MonthId != "")
            {
                objOracleCommand.Parameters.Add("p_month_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLoanAdvanceModel.MonthId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_month_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objLoanAdvanceModel.LoanAmount != "")
            {
                objOracleCommand.Parameters.Add("p_loan_amount", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLoanAdvanceModel.LoanAmount;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_loan_amount", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objLoanAdvanceModel.DeductionAmount != "")
            {
                objOracleCommand.Parameters.Add("P_DEDUCTION_AMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLoanAdvanceModel.DeductionAmount;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_DEDUCTION_AMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLoanAdvanceModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLoanAdvanceModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objLoanAdvanceModel.BranchOfficeId;

            objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objOracleCommand.Connection = strConn;
                    strConn.Open();
                    objTransaction = strConn.BeginTransaction();
                    objOracleCommand.ExecuteNonQuery();
                    objTransaction.Commit();
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




        #endregion


        #region PF ADVANCE
        public IEnumerable<PFAdvanceModel> LoadPFAdvanceRecord(PFAdvanceModel objPfAdvanceModel)
        {

            List<PFAdvanceModel> loanAdvanceList = new List<PFAdvanceModel>();

            string sql = "";
            sql = "SELECT " +
                "rownum sl, " +
                "loan_year, " +
                "month_name, " +
                "loan_amount, " +
                "deduction_amount, " +
                "remaining_amount, " +
                "STATUS " +
                  " FROM VEW_SEARCH_PF_LOAN where head_office_id = '" + objPfAdvanceModel.HeadOfficeId + "' and branch_office_id = '" + objPfAdvanceModel.BranchOfficeId + "' AND employee_id = '" + objPfAdvanceModel.EmployeeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objPfAdvanceModel.EmployeeId))
            {

                sql = sql + "and employee_id = '" + objPfAdvanceModel.EmployeeId + "' ";
            }


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
                        objPfAdvanceModel = new PFAdvanceModel();
                        objPfAdvanceModel.Serial = objDataReader["sl"].ToString();
                        objPfAdvanceModel.Year = objDataReader["loan_year"].ToString();
                        objPfAdvanceModel.MonthName = objDataReader["month_name"].ToString();
                        objPfAdvanceModel.LoanAmount = objDataReader["loan_amount"].ToString();
                        objPfAdvanceModel.DeductionAmount = objDataReader["deduction_amount"].ToString();
                        objPfAdvanceModel.RemainingAmount = objDataReader["remaining_amount"].ToString();
                        objPfAdvanceModel.Status = objDataReader["STATUS"].ToString();

                        loanAdvanceList.Add(objPfAdvanceModel);
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

            return loanAdvanceList;
        }
        public PFAdvanceModel PFGetEmployeeRecordById(PFAdvanceModel objPfAdvanceModel)
        {


            string vQuery = "";
            vQuery = "SELECT " +

                    "EMPLOYEE_ID, " +
                    "EMPLOYEE_NAME, " +
                    "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                    "DESIGNATION_NAME, " +
                    "HEAD_OFFICE_ID, " +
                    "BRANCH_OFFICE_ID, " +
                    "EMPLOYEE_PIC " +

                    "FROM VEW_PERSONAL_INFO where head_office_id = '" + objPfAdvanceModel.HeadOfficeId + "' and branch_office_id = '" + objPfAdvanceModel.BranchOfficeId + "' AND employee_id = '" + objPfAdvanceModel.EmployeeId + "'   ";

            //vQuery += " ORDER BY joining_date";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objPfAdvanceModel = new PFAdvanceModel();
                        objPfAdvanceModel.EmployeeModel = new EmployeeModel();

                        objPfAdvanceModel.EmployeeId = objDataReader["employee_id"].ToString();
                        objPfAdvanceModel.EmployeeModel.EmployeeId = objDataReader["employee_id"].ToString();
                        objPfAdvanceModel.EmployeeModel.EmployeeName = objDataReader["EMPLOYEE_NAME"].ToString();
                        objPfAdvanceModel.EmployeeModel.JoiningDate = objDataReader["joining_date"].ToString();
                        objPfAdvanceModel.EmployeeModel.PresentDesignationName = objDataReader["designation_name"].ToString();
                        objPfAdvanceModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                        objPfAdvanceModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();
                        objPfAdvanceModel.EmployeeModel.EmployeeImage = objDataReader["EMPLOYEE_PIC"] == DBNull.Value ? new byte[0] : (byte[])objDataReader["EMPLOYEE_PIC"];
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

            return objPfAdvanceModel;
        }
        public string AdvancePFRecordSave(PFAdvanceModel objPfAdvanceModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_pf_loan_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objPfAdvanceModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPfAdvanceModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objPfAdvanceModel.Year != "")
            {
                objOracleCommand.Parameters.Add("p_loan_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPfAdvanceModel.Year;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_loan_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objPfAdvanceModel.MonthId != "")
            {
                objOracleCommand.Parameters.Add("p_month_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPfAdvanceModel.MonthId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_month_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objPfAdvanceModel.LoanAmount != "")
            {
                objOracleCommand.Parameters.Add("p_loan_amount", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPfAdvanceModel.LoanAmount;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_loan_amount", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objPfAdvanceModel.DeductionAmount != "")
            {
                objOracleCommand.Parameters.Add("P_DEDUCTION_AMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPfAdvanceModel.DeductionAmount;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_DEDUCTION_AMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objPfAdvanceModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objPfAdvanceModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objPfAdvanceModel.BranchOfficeId;

            objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objOracleCommand.Connection = strConn;
                    strConn.Open();
                    objTransaction = strConn.BeginTransaction();
                    objOracleCommand.ExecuteNonQuery();
                    objTransaction.Commit();
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
        #endregion




      

        public string SaveEmpGrossSalary(EmployeeAddSalary objEmployeeAddSalary)
        {

            string strMsg = null;

          
            foreach (EmployeeAddSalary model in objEmployeeAddSalary.EmployeeAddSalaryList)
            {
                OracleCommand objOracleCommand = new OracleCommand { CommandText = "PRO_EMPLOYEE_SALARY_SAVE", CommandType = CommandType.StoredProcedure };

                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input)
                    .Value = !string.IsNullOrWhiteSpace(model.EmployeeId) ? model.EmployeeId : null;

                objOracleCommand.Parameters.Add("p_joining_salary", OracleDbType.Varchar2, ParameterDirection.Input)
                    .Value = !string.IsNullOrWhiteSpace(model.JoiningSalary) ? model.JoiningSalary : null;

                objOracleCommand.Parameters.Add("p_Gross_salary", OracleDbType.Varchar2, ParameterDirection.Input)
                    .Value = !string.IsNullOrWhiteSpace(model.GrossSalary) ? model.GrossSalary : null;

                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input)
                    .Value = objEmployeeAddSalary.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input)
                    .Value = objEmployeeAddSalary.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100,
                    ParameterDirection.Input).Value = objEmployeeAddSalary.BranchOfficeId;

                objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction =
                    ParameterDirection.Output;

                using (OracleConnection objConnection = GetConnection())
                {
                    try
                    {
                        objOracleCommand.Connection = objConnection;
                        objConnection.Open();
                        objTransaction = objConnection.BeginTransaction();
                        objOracleCommand.ExecuteNonQuery();
                        objTransaction.Commit();
                        objConnection.Close();

                        strMsg = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
                    }
                    catch (Exception ex)
                    {
                        objTransaction.Rollback();
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        objTransaction.Dispose();
                        objConnection.Close();
                    }
                }



            }
            return strMsg;
        }

        public List<EmployeeAddSalary> LoadSalaryRecordIsNull(EmployeeAddSalary objEmployeeAddSalary)
        {

            List<EmployeeAddSalary> salaryList = new List<EmployeeAddSalary>();

            objEmployeeAddSalary.Active_YN = "Y";
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
                   "JOINING_SALARY, " +
                   "GROSS_SALARY " +
                  " FROM vew_gross_salary_is_null where head_office_id = '" + objEmployeeAddSalary.HeadOfficeId + "' and branch_office_id = '" + objEmployeeAddSalary.BranchOfficeId + "' AND active_yn = '" + objEmployeeAddSalary.Active_YN + "'   ";


            if (!string.IsNullOrWhiteSpace(objEmployeeAddSalary.EmployeeId))
            {

                sql = sql + "and employee_id = '" + objEmployeeAddSalary.EmployeeId + "' ";
            }

            if (!string.IsNullOrWhiteSpace(objEmployeeAddSalary.EmployeeName))
            {
                sql = sql + "and (lower(employee_name) like lower( '%" + objEmployeeAddSalary.EmployeeName + "%')  or upper(employee_name)like upper('%" + objEmployeeAddSalary.EmployeeName + "%') )";

            }

            if (!string.IsNullOrWhiteSpace(objEmployeeAddSalary.DepartmentId))
            {

                sql = sql + "and department_id = '" + objEmployeeAddSalary.DepartmentId + "' ";
            }

            if (!string.IsNullOrWhiteSpace(objEmployeeAddSalary.UnitId))
            {

                sql = sql + "and unit_id = '" + objEmployeeAddSalary.UnitId + "' ";
            }

            if (!string.IsNullOrWhiteSpace(objEmployeeAddSalary.SubSectionId))
            {

                sql = sql + "and sub_section_id = '" + objEmployeeAddSalary.SubSectionId + "' ";
            }

            if (!string.IsNullOrWhiteSpace(objEmployeeAddSalary.SectionId))
            {

                sql = sql + "and section_id = '" + objEmployeeAddSalary.SectionId + "' ";
            }




            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objEmployeeAddSalary = new EmployeeAddSalary();
                        objEmployeeAddSalary.EmployeeId = objDataReader["EMPLOYEE_ID"].ToString();
                        objEmployeeAddSalary.EmployeeName = objDataReader["EMPLOYEE_NAME"].ToString();
                        objEmployeeAddSalary.JoiningDate = objDataReader["JOINING_DATE"].ToString();
                        objEmployeeAddSalary.Designation = objDataReader["DESIGNATION_NAME"].ToString();
                        objEmployeeAddSalary.UnitName = objDataReader["UNIT_NAME"].ToString();
                        objEmployeeAddSalary.Departmentname = objDataReader["DEPARTMENT_NAME"].ToString();
                        objEmployeeAddSalary.SectionName = objDataReader["SECTION_NAME"].ToString();
                        objEmployeeAddSalary.JoiningSalary = objDataReader["JOINING_SALARY"].ToString();
                        objEmployeeAddSalary.GrossSalary = objDataReader["GROSS_SALARY"].ToString();
                        objEmployeeAddSalary.Status = objDataReader["active_status"].ToString();

                        salaryList.Add(objEmployeeAddSalary);
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

            return salaryList;

        }


  #region SALARY PROCESS CO
        public string ProcessCOMonthlySalary(SalaryProcessCOModel objSalaryProcessCoModel)
        {

            string strMsg = "";

            using (OracleConnection strConn = GetConnection())
            {
                OracleCommand objOracleCommand = new OracleCommand { CommandText = "pro_salary_process_sco", CommandType = CommandType.StoredProcedure, Connection = strConn };

                try
                {
                    objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objSalaryProcessCoModel.SalaryYear) ? objSalaryProcessCoModel.SalaryYear : null;
                    objOracleCommand.Parameters.Add("p_month_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objSalaryProcessCoModel.MonthId) ? objSalaryProcessCoModel.MonthId : null;

                    objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objSalaryProcessCoModel.UnitId) ? objSalaryProcessCoModel.UnitId : null;
                    objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objSalaryProcessCoModel.DepartmentId) ? objSalaryProcessCoModel.DepartmentId : null;
                    objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objSalaryProcessCoModel.SectionId) ? objSalaryProcessCoModel.SectionId : null;
                    objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objSalaryProcessCoModel.SubSectionId) ? objSalaryProcessCoModel.SubSectionId : null;

                    objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSalaryProcessCoModel.UpdateBy;
                    objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSalaryProcessCoModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSalaryProcessCoModel.BranchOfficeId;

                    objOracleCommand.Parameters.Add("p_message", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                    strConn.Open();
                    objTransaction = strConn.BeginTransaction();
                    objOracleCommand.ExecuteNonQuery();
                    objTransaction.Commit();


                    strMsg = objOracleCommand.Parameters["p_message"].Value.ToString();
                }

                catch (Exception ex)
                {
                    objTransaction.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    objTransaction.Dispose();
                    objOracleCommand.Dispose();
                    strConn.Dispose();
                }

            }


            return strMsg;

        }

        public List<SalaryProcessCOModel> LoadCORecordForSalary(SalaryProcessCOModel objSalaryProcessCoModel)
        {

            List<SalaryProcessCOModel> salaryList = new List<SalaryProcessCOModel>();

            string sql = "", strMsg = "", sql1 = "", sql2 = "";


            sql = "SELECT 'Y' " +


                  "from VEW_SALARY_PERMISSION where employee_id = '" + objSalaryProcessCoModel.UpdateBy +
                  "' and permission_yn='Y' AND HEAD_OFFICE_ID = '" + objSalaryProcessCoModel.HeadOfficeId +
                  "' AND BRANCH_OFFICE_ID ='" + objSalaryProcessCoModel.BranchOfficeId + "' ";


            //SearchBy

            OracleCommand objCommand1 = new OracleCommand(sql);
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


                        strMsg = objDataReader1.GetString(0);



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



            if (strMsg == "Y")
            {

                sql1 = "SELECT " +
                       "rownum SL, " +
                       "EMPLOYEE_ID, " +
                       "EMPLOYEE_NAME, " +
                       "DESIGNATION_NAME, " +
                       "SALARY_YEAR, " +
                       "MONTH_ID, " +
                       "to_char(JOINING_DATE, 'dd/mm/yyyy')JOINING_DATE, " +
                       "DEPARTMENT_NAME, " +

                       "to_char(nvl(WORKING_DAY,''))WORKING_DAY, " +

                       "to_char(nvl(MONTH_DAY, ''))MONTH_DAY " +

                       "FROM VEW_SCO_RECORD_FOR_SALARY where salary_year ='" + objSalaryProcessCoModel.SalaryYear +
                       "' and month_id = '" + objSalaryProcessCoModel.MonthId + "' and   head_office_id = '" +
                       objSalaryProcessCoModel.HeadOfficeId + "' and branch_office_id = '" +
                       objSalaryProcessCoModel.BranchOfficeId + "'   ";

                if (!string.IsNullOrWhiteSpace(objSalaryProcessCoModel.EmployeeName))
                {

                    sql1 = sql1 + "and (lower(employee_name) like lower( '%" + objSalaryProcessCoModel.EmployeeName +
                           "%')  or upper(employee_name)like upper('%" + objSalaryProcessCoModel.EmployeeName + "%') )";
                }

                //if (strEmployeeId.Length > 0)
                if (!string.IsNullOrWhiteSpace(objSalaryProcessCoModel.EmployeeId))
                {

                    sql1 = sql1 + "and employee_id = '" + objSalaryProcessCoModel.EmployeeId + "' ";
                }

                if (!string.IsNullOrWhiteSpace(objSalaryProcessCoModel.UnitId))
                {

                    sql1 = sql1 + "and unit_id = '" + objSalaryProcessCoModel.UnitId + "' ";
                }

                if (!string.IsNullOrWhiteSpace(objSalaryProcessCoModel.SectionId))
                {

                    sql1 = sql1 + "and section_id = '" + objSalaryProcessCoModel.SectionId + "' ";
                }

                if (!string.IsNullOrWhiteSpace(objSalaryProcessCoModel.SubSectionId))
                {

                    sql1 = sql1 + "and sub_section_id = '" + objSalaryProcessCoModel.SubSectionId + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objSalaryProcessCoModel.DepartmentId))
                {

                    sql1 = sql1 + "and department_id = '" + objSalaryProcessCoModel.DepartmentId + "' ";
                }



                sql1 = sql1 + " ORDER BY SL ";




                using (OracleConnection objConnection = GetConnection())
                {
                    OracleCommand objCommand2 = new OracleCommand(sql1, objConnection);
                    objConnection.Open();
                    OracleDataReader objDataReader2 = objCommand2.ExecuteReader();

                    try
                    {
                        while (objDataReader2.Read())
                        {
                            objSalaryProcessCoModel = new SalaryProcessCOModel();
                            objSalaryProcessCoModel.EmployeeId = objDataReader2["EMPLOYEE_ID"].ToString();
                            objSalaryProcessCoModel.EmployeeName = objDataReader2["EMPLOYEE_NAME"].ToString();
                            objSalaryProcessCoModel.JoiningDate = objDataReader2["JOINING_DATE"].ToString();
                            objSalaryProcessCoModel.Designation = objDataReader2["DESIGNATION_NAME"].ToString();
                            objSalaryProcessCoModel.Departmentname = objDataReader2["DEPARTMENT_NAME"].ToString();
                            objSalaryProcessCoModel.SalaryYear = objDataReader2["SALARY_YEAR"].ToString();
                            objSalaryProcessCoModel.MonthId = objDataReader2["MONTH_ID"].ToString();
                            objSalaryProcessCoModel.MonthDay = objDataReader2["MONTH_DAY"].ToString();
                            objSalaryProcessCoModel.WorkingDay = objDataReader2["WORKING_DAY"].ToString();

                            salaryList.Add(objSalaryProcessCoModel);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        objDataReader1.Close();
                        objConnection.Close();
                    }
                }
                return salaryList;
            }

            else
            {

                sql2 = "SELECT " +
                       "rownum SL, " +
                       "EMPLOYEE_ID, " +
                       "EMPLOYEE_NAME, " +
                       "DESIGNATION_NAME, " +
                       "SALARY_YEAR, " +
                       "MONTH_ID, " +
                       "to_char(JOINING_DATE, 'dd/mm/yyyy')JOINING_DATE, " +
                       "DEPARTMENT_NAME, " +

                       "to_char(nvl(WORKING_DAY,'0'))WORKING_DAY, " +

                       "to_char(nvl(MONTH_DAY, '0'))MONTH_DAY " +


                       "FROM VEW_SCO_RECORD_FOR_SALARY where salary_year ='" + objSalaryProcessCoModel.SalaryYear +
                       "' and month_id = '" + objSalaryProcessCoModel.MonthId + "' and   head_office_id = '" +
                       objSalaryProcessCoModel.HeadOfficeId + "' and branch_office_id = '" +
                       objSalaryProcessCoModel.BranchOfficeId + "'   ";

                if (!string.IsNullOrWhiteSpace(objSalaryProcessCoModel.EmployeeName))
                {

                    sql2 = sql2 + "and (lower(employee_name) like lower( '%" + objSalaryProcessCoModel.EmployeeName +
                           "%')  or upper(employee_name)like upper('%" + objSalaryProcessCoModel.EmployeeName + "%') )";
                }

                if (!string.IsNullOrWhiteSpace(objSalaryProcessCoModel.EmployeeId))
                {

                    sql2 = sql2 + "and employee_id = '" + objSalaryProcessCoModel.EmployeeId + "' ";
                }

                if (!string.IsNullOrWhiteSpace(objSalaryProcessCoModel.UnitId))
                {

                    sql2 = sql2 + "and unit_id = '" + objSalaryProcessCoModel.UnitId + "' ";
                }

                if (!string.IsNullOrWhiteSpace(objSalaryProcessCoModel.SectionId))
                {

                    sql2 = sql2 + "and section_id = '" + objSalaryProcessCoModel.SectionId + "' ";
                }

                if (!string.IsNullOrWhiteSpace(objSalaryProcessCoModel.SubSectionId))
                {

                    sql2 = sql2 + "and sub_section_id = '" + objSalaryProcessCoModel.SubSectionId + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objSalaryProcessCoModel.DepartmentId))
                {

                    sql2 = sql2 + "and department_id = '" + objSalaryProcessCoModel.DepartmentId + "' ";
                }



                sql2 = sql2 + " ORDER BY SL ";




                using (OracleConnection objConnection = GetConnection())
                {
                    OracleCommand objCommand3 = new OracleCommand(sql2, objConnection);
                    objConnection.Open();
                    OracleDataReader objDataReader3 = objCommand3.ExecuteReader();

                    try
                    {
                        while (objDataReader3.Read())
                        {
                            objSalaryProcessCoModel = new SalaryProcessCOModel();
                            objSalaryProcessCoModel.EmployeeId = objDataReader3["EMPLOYEE_ID"].ToString();
                            objSalaryProcessCoModel.EmployeeName = objDataReader3["EMPLOYEE_NAME"].ToString();
                            objSalaryProcessCoModel.JoiningDate = objDataReader3["JOINING_DATE"].ToString();
                            objSalaryProcessCoModel.Designation = objDataReader3["DESIGNATION_NAME"].ToString();
                            objSalaryProcessCoModel.Departmentname = objDataReader3["DEPARTMENT_NAME"].ToString();
                            objSalaryProcessCoModel.SalaryYear = objDataReader3["SALARY_YEAR"].ToString();
                            objSalaryProcessCoModel.MonthId = objDataReader3["MONTH_ID"].ToString();
                            objSalaryProcessCoModel.MonthDay = objDataReader3["MONTH_DAY"].ToString();
                            objSalaryProcessCoModel.WorkingDay = objDataReader3["WORKING_DAY"].ToString();

                            salaryList.Add(objSalaryProcessCoModel);
                        }

                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        objDataReader3.Close();
                        objConnection.Close();
                    }
                }

                // }

                return salaryList;

            }



        }


        #endregion




    }
}