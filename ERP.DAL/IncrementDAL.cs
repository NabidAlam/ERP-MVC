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
    public class IncrementDAL
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


        #region Increment Entry

        public List<IncrementEntryModel> GetIncrementEntryList(IncrementEntryModel objIncrementModel)
        {
            List<IncrementEntryModel> listIncrements = new List<IncrementEntryModel>();

            string sql = "";



            sql = "SELECT " +
                  "rownum sl, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                  "DESIGNATION_NAME, " +
                  "DEPARTMENT_NAME, " +
                  "GRADE_NO, " +
                  "TOTAL_MONTH, " +
                  "JOINING_SALARY," +
                  "GROSS_SALARY," +
                  "Increment_Amount," +
                  " TOTAL_AMOUNT" +

                  " FROM VEW_INCREMENT_ADD where head_office_id = '" + objIncrementModel.HeadOfficeId + "' and branch_office_id = '" + objIncrementModel.BranchOfficeId + "'  ";

            if (objIncrementModel.EmployeeId != null)
            {

                sql = sql + "and EMPLOYEE_id = '" + objIncrementModel.EmployeeId + "' ";
            }

            if (objIncrementModel.EmployeeName != null)
            {

                sql = sql + "and employee_name = '" + objIncrementModel.EmployeeName + "' ";
            }

            if (objIncrementModel.EmployeeCardNo != null)
            {

                sql = sql + "and CARD_NO = '" + objIncrementModel.EmployeeCardNo + "' ";
            }

            if (objIncrementModel.Year != null)
            {

                sql = sql + "and increment_year = '" + objIncrementModel.Year + "' ";
            }

            if (objIncrementModel.UnitId != null)
            {

                sql = sql + "and unit_id = '" + objIncrementModel.UnitId + "' ";
            }

            if (objIncrementModel.DepartmentId != null)
            {

                sql = sql + "and department_id = '" + objIncrementModel.DepartmentId + "' ";
            }

            if (objIncrementModel.SectionId != null)
            {

                sql = sql + "and section_id = '" + objIncrementModel.SectionId + "' ";
            }

            if (objIncrementModel.SubSectionId != null)
            {

                sql = sql + "and sub_section_id = '" + objIncrementModel.SubSectionId + "' ";
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
                        IncrementEntryModel objIncr = new IncrementEntryModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["SL"]).ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),

                            EmployeeGrade = objReader["GRADE_NO"].ToString(),
                            TotalMonth = objReader["TOTAL_MONTH"].ToString(),
                            JoiningSalary = objReader["JOINING_SALARY"].ToString(),
                            GrossSalary = objReader["GROSS_SALARY"].ToString(),
                            IncrementAmount = objReader["Increment_Amount"].ToString(),
                            TotalAmount = objReader["TOTAL_AMOUNT"].ToString()
                        };

                        listIncrements.Add(objIncr);
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



            return listIncrements;
        }

        public string EmployeeIncrement(IncrementEntryModel objIncrementModel)
        {

            string strMsg = "";

            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    strConn.Open();
                    //foreach (IncrementEntryModel assign in objIncrementModel.IncrementEntryList)
                    {
                        OracleCommand objOracleCommand = new OracleCommand("pro_increment_add")
                        {
                            Connection = strConn,
                            CommandType = CommandType.StoredProcedure
                        };


                        if (objIncrementModel.Year != null)
                        {
                            objOracleCommand.Parameters.Add("p_increment_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementModel.Year;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_increment_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (objIncrementModel.UnitId != null)
                        {
                            objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementModel.UnitId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (objIncrementModel.DepartmentId != null)
                        {
                            objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementModel.DepartmentId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (objIncrementModel.SectionId != null)
                        {
                            objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementModel.SectionId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }


                        if (objIncrementModel.SubSectionId != null)
                        {
                            objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementModel.SubSectionId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIncrementModel.UpdateBy;
                        objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIncrementModel.HeadOfficeId;
                        objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIncrementModel.BranchOfficeId;

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


        public string SaveEmployeeIncrement(IncrementEntryModel objIncrementModel)
        {

            string strMsg = "";

            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    strConn.Open();
                    foreach (IncrementEntryModel assign in objIncrementModel.IncrementEntryList)
                    {
                        OracleCommand objOracleCommand = new OracleCommand("pro_increment_save")
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

                        if (objIncrementModel.Year != null)
                        {
                            objOracleCommand.Parameters.Add("p_INCREMENT_YEAR", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementModel.Year;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_INCREMENT_YEAR", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (assign.IncrementAmount != null)
                        {
                            objOracleCommand.Parameters.Add("p_INCREMENT_AMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = assign.IncrementAmount;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_INCREMENT_AMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }


                        objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIncrementModel.UpdateBy;

                        objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIncrementModel.HeadOfficeId;

                        objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIncrementModel.BranchOfficeId;

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

        public string ProcessEmployeeIncrement(IncrementEntryModel objIncrementModel)
        {

            string strMsg = "";

            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    strConn.Open();
                    //foreach (IncrementEntryModel assign in objIncrementModel.IncrementEntryList)
                    {
                        OracleCommand objOracleCommand = new OracleCommand("pro_increment_process")
                        {
                            Connection = strConn,
                            CommandType = CommandType.StoredProcedure
                        };


                        if (objIncrementModel.Year != null)
                        {
                            objOracleCommand.Parameters.Add("p_increment_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementModel.Year;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_increment_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (objIncrementModel.UnitId != null)
                        {
                            objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementModel.UnitId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (objIncrementModel.DepartmentId != null)
                        {
                            objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementModel.DepartmentId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        if (objIncrementModel.SectionId != null)
                        {
                            objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementModel.SectionId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }


                        if (objIncrementModel.SubSectionId != null)
                        {
                            objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementModel.SubSectionId;
                        }
                        else
                        {
                            objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                        }

                        objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIncrementModel.UpdateBy;
                        objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIncrementModel.HeadOfficeId;
                        objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objIncrementModel.BranchOfficeId;

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
        #endregion
    }
}
