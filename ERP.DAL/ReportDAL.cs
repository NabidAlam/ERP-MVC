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
    public class ReportDAL
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

        #region "Invetory Report"
        public DataSet rdoProductDetailSheet(ProductReport objProductModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +
                       "RPT_TITLE, " +
                       "STYLE_NO, " +
                       "DESIGNER_ID, " +
                       "DESIGNER_NAME, " +
                       "MERCHANDISER_ID, " +
                       "MERCHANDISER_NAME, " +
                       "COLOR_ID, " +
                       "COLOR_NAME, " +
                       "FIT_ID, " +
                       "FIT_NAME, " +
                       "CATEGORY_ID, " +
                       "CATEGORY_NAME, " +
                       "SUB_CATEGORY_ID, " +
                       "SUB_CATEGORY_NAME, " +
                       "CREATE_BY, " +
                       "CREATE_DATE, " +
                       "UPDATE_BY, " +
                       "UPDATE_DATE, " +
                       "HEAD_OFFICE_ID, " +
                       "HEAD_OFFICE_NAME, " +
                       "BRANCH_OFFICE_ID, " +
                       "BRANCH_OFFICE_NAME, " +
                       "BRANCH_OFFICE_ADDRESS, " +
                       "TRAN_ID, " +
                       "SEASON_ID, " +
                       "SEASON_NAME, " +
                       "SEASON_YEAR, " +
                       "SEASON_SATUS, " +
                       "REMARKS, " +
                       "FABRIC_CODE, " +
                       "MONTH_ID, " +
                       "MONTH_NAME, " +
                       "COLOR_WAY_NO_ID, " +
                       "COLOR_WAY_NO_NAME, " +
                       "COLOR_WAY_TYPE_ID, " +
                       "COLOR_WAY_TYPE_NAME, " +
                       "FABRIC_CONSUMPTION, " +
                       "FABRIC_TYPE_ID, " +
                       "FABRIC_TYPE_NAME, " +
                       "SIZE_ID, " +
                       "SAMPLE_SIZE, " +
                       "PRODUCTION_QUANTITY, " +
                       "SHOP_IN_HOUSE_DATE, " +
                       "WASH_TYPE_ID, " +
                       "WASH_TYPE_NAME, " +
                       "STYLE_NAME, " +
                       "file_size, " +
                       "color_way_status, " +
                       "SWATCH_PIC, " +
                       "occasion_id, "+
                       "occasion_name, "+
                    "dm "+

                    " from VEW_RPT_PRODUCT_DETAIL_FD  where head_office_id = '" + objProductModel.HeadOfficeId + "' AND branch_office_id = '" + objProductModel.BranchOfficeId + "' and style_no ='" + objProductModel.StyleNumber + "' and SEASON_ID ='" + objProductModel.SeasonId + "' and SEASON_YEAR ='" + objProductModel.SeasonYear + "'  ";


                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_PRODUCT_DETAIL_FD");
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

        // Po Reports
        public DataSet rdoPoDetailsSheet(PoReport objPoReport)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +
   "RPT_TITLE, " +
   "DELIVERY_ADDRESS, " +
   "BRANCH_OFFICE_NAME, " +
   "FULL_OFFICE_NAME, " +
   "BRANCH_OFFICE_ADDRESS, " +
   "PO_NO, " +
   "STYLE_NO, " +
   "SEASON_ID, " +
   "SEASON_YEAR, " +
   "SEASON_STATUS, " +
   "COST_PRICE, " +
   "RETAIL_PRICE, " +
   "FABRIC_QUANTITY, " +
   "STORE_DELIVERY_DATE, " +
   "EMBROIDARY_YN, " +
   "EMBROIDARY_STATUS, " +
   "KARCHUPI_YN, " +
   "KARCHUPI_STATUS, " +
   "PRINT_YN, " +
   "PRINT_STATUS, " +
   "WASH_YN, " +
   "WASH_STATUS, " +
   "INSERT_DATE, " +
   "COLOR_ID, " +
   "FABRIC_TYPE_ID, " +
   "FABRIC_CODE, " +
   "COLOR_WAY_NO_ID, " +
   "COLOR_WAY_NAME, " +
   "SIZE_ID, " +
   "SIZE_NAME, " +
   "SIZE_VALUE, " +
   "CREATE_BY, " +
   "CREATE_DATE, " +
   "UPDATE_BY, " +
   "MERCHANDISER_NAME, " +
   "DEPARTMENT_NAME, " +
   "DESIGNER_NAME, " +
   "MONTH_ID, " +
   "UPDATE_DATE, " +
   "HEAD_OFFICE_ID, " +
   "HEAD_OFFICE_NAME, " +
   "BRANCH_OFFICE_ID, " +
   "TRAN_ID, " +
   "SUPPLIER_OFFICE_NAME, " +
   "SUPPLIER_OFFICE_ADDRESS, " +
   "TOTAL_PRICE " +

" from VEW_RPT_PO_DETAILS  where head_office_id = '" + objPoReport.HeadOfficeId + "' AND branch_office_id = '" + objPoReport.BranchOfficeId + "' and style_no ='" + objPoReport.StyleNo + "' and SEASON_ID ='" + objPoReport.SeasoneId + "' and SEASON_YEAR ='" + objPoReport.SeasoneYear + "' and PO_NO ='" + objPoReport.PoNumber + "'  ";





                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_PO_DETAILS");
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

        public string ProcessIndividualAttendance(AttendenceReportModel objAttendenceReportModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_rpt_individual_attendence");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objAttendenceReportModel.EmployeeId) ? objAttendenceReportModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.FromDate.Length > 6 ? objAttendenceReportModel.FromDate : null;
            objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.ToDate.Length > 6 ? objAttendenceReportModel.ToDate : null;
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.BranchOfficeId;

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

            return strMsg;
        }
        public string AttendanceSummaryProcess(AttendenceReportModel objAttendenceReportModel)
        {

            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_attendance_summery");
            objOracleCommand.CommandType = CommandType.StoredProcedure;




            if (objAttendenceReportModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }





            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.BranchOfficeId;

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
        public string AttendanceProcessHigestDutyTime(AttendenceReportModel objAttendenceReportModel)
        {

            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_rpt_maximum_duty");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objAttendenceReportModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            if (objAttendenceReportModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.BranchOfficeId;

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
        public string AttendanceProcessEarlyOut(AttendenceReportModel objAttendenceReportModel)
        {

            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_rpt_early_out");
            objOracleCommand.CommandType = CommandType.StoredProcedure;



            if (objAttendenceReportModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            if (objAttendenceReportModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.BranchOfficeId;

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
        public string AttendanceProcessTopLateEmployee(AttendenceReportModel objAttendenceReportModel)
        {

            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_rpt_in_late");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objAttendenceReportModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            if (objAttendenceReportModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.BranchOfficeId;

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
        public string AttendanceProcessPIN(AttendenceReportModel objAttendenceReportModel)
        {

            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_rpt_punctual_in");
            objOracleCommand.CommandType = CommandType.StoredProcedure;





            if (objAttendenceReportModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.BranchOfficeId;

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
        public string AttendanceProcessPOut(AttendenceReportModel objAttendenceReportModel)
        {

            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_rpt_punctual_out");
            objOracleCommand.CommandType = CommandType.StoredProcedure;





            if (objAttendenceReportModel.UnitId != null)
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.DepartmentId != null)
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.SectionId != null)
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objAttendenceReportModel.SubSectionId != null)
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            if (objAttendenceReportModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAttendenceReportModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAttendenceReportModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAttendenceReportModel.BranchOfficeId;

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

        public DataSet TrimsDetailSheet(TrimsReport objTrimsReport)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +
                   "STYLE_NO, " +
                   "STYLE_NAME, " +
                   "SEASON_ID, " +
                   "SEASON_YEAR, " +
                   "SEASON_SATUS, " +
                   "INTERLING, " +
                   "MAIN_LABEL, " +
                   "CARE_LABEL, " +
                   "SIZE_LABEL, " +
                   "SEWING_THREAD, " +
                   "HANG_TAG, " +
                   "ACCESSORIES_ID, " +
                   "ACCESSORIES_NAME, " +
                   "TRAN_ID, " +
                   "TRIMS_CODE, " +
                   "PER_GARMENTS_QUANTITY, " +
                   "TOTAL_QUANTITY, " +
                   "UPDATE_BY, " +
                   "UPDATE_DATE, " +
                   "CREATE_BY, " +
                   "CREATE_DATE, " +
                   "HEAD_OFFICE_ID, " +
                   "HEAD_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ID, " +
                   "BRANCH_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ADDRESS, " +
                   "RPT_TITLE " +

                        " from VEW_RPT_TRIMS_DETAIL where head_office_id = '" + objTrimsReport.HeadOfficeId + "' AND branch_office_id = '" + objTrimsReport.BranchOfficeId + "' and style_no ='" + objTrimsReport.StyleNo + "' and SEASON_ID ='" + objTrimsReport.SeasoneId + "' and SEASON_YEAR ='" + objTrimsReport.SeasoneYear + "'  ";





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
                            objDataAdapter.Fill(ds, "VEW_RPT_TRIMS_DETAIL");
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
        public string ProcessIndividualAttendance(ReportModel objReportModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_rpt_individual_attendence");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objReportModel.EmployeeId) ? objReportModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objReportModel.FromDate.Length > 6 ? objReportModel.FromDate : null;
            objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objReportModel.ToDate.Length > 6 ? objReportModel.ToDate : null;
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objReportModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objReportModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objReportModel.BranchOfficeId;

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

            return strMsg;
        }
        public DataSet GetIndividualAttendanceData(ReportModel objReportModel)
        {
            DataSet dataSet;

            try
            {
                //DataSet dataSet;
                ////DataTable dataTable = new DataTable();

                //try
                //{
                string sql = "";
                sql = "SELECT " +

               "'Individual Attendence History '|| 'From  '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy') || ' to ' || to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                "emp_title, " +
                "EMPLOYEE_ID, " +
                "EMPLOYEE_NAME, " +
                "JOINING_DATE, " +
                "DESIGNATION_NAME, " +
                "UNIT_ID, " +
                "UNIT_NAME, " +
                "DEPARTMENT_ID, " +
                "DEPARTMENT_NAME, " +
                "SECTION_ID, " +
                "SECTION_NAME, " +
                "SUB_SECTION_ID, " +
                "SUB_SECTION_NAME, " +
                "PUNCH_CODE, " +
                "LOG_DATE, " +
                "FIRST_IN, " +
                "LAST_OUT, " +
                "TOTAL_DUTY_TIME, " +
                "TOTAL_EARLY_OUT_TIME, " +
                "TOTAL_LATE_TIME, " +
                "LATE_YN, " +
                "PUNCTUAL_YN, " +
                "ABSENT_YN, " +
                "LEAVE_YN, " +
                "HOLIDAY_YN, " +
                "DAY_TYPE, " +
                "REMARKS, " +
                "UPDATE_DATE, " +
                "HEAD_OFFICE_ID, " +
                "BRANCH_OFFICE_ID, " +
                "LEAVE_TYPE_ID, " +
                "DAY_NAME, " +
                "TOTAL_LATE_DAY, " +
                "MONTH_DAY, " +
                "WORKING_DAY, " +
                "HEAD_OFFICE_NAME, " +
                "BRANCH_OFFICE_NAME, " +
                "BRANCH_OFFICE_ADDRESS, " +
                "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +
                "DAILY_DUTY_TIME, " +
                "DAILY_EARLY_OUT_TIME, " +
                "DAILY_LATE_TIME, " +
                "emp_id, " +
                "emp_name, " +
                "emp_joining_date, " +
                "emp_designation, " +
                "emp_department, " +
                "emp_date_of_birth, " +
                "SHIFT_IN_TIME, " +
                "SHIFT_OUT_TIME, " +
                "SHIFT_STATUS, " +
                "WORKING_HOUR, " +
                "WORKING_MINUTE, " +
                "WORKING_SECOND,  " +
                "TOTAL_PRESENT_DAY,  " +
                "TOTAL_ABSENT_DAY, " +
                "TOTAL_HOLIDAY, " +
                "TOTAL_WORKING_HOUR, " +
                "TOTAL_WORKING_DAY, " +
                "TOTAL_LATE_HOUR, " +
                "TOTAL_LATE_MINUTE, " +
                "TOTAL_LATE_SECOND, " +
                "TOTAL_LATE_HMS, " +
                "PUNCH_STATUS, " +
                "TOTAL_WORKING_HOUR, " +
                "TOTAL_WORKING_MINUTE, " +
                "TOTAL_WORKING_SECOND, " +
                "TOTAL_WORKING_STATUS, " +
                "AVG_DUTY_TIME," +
                "TOTAL_EARLY_OUT_HOUR, " +
                "TOTAL_EARLY_OUT_MINUTE, " +
                "TOTAL_EARLY_OUT_SECOND, " +
                "TOTAL_EARLY_OUT_STATUS, " +
                "color_status, " +

                "(SELECT COUNT(*) " +
                " FROM employee_leave " +
                " WHERE leave_type_id = '3' " +
                " AND leave_start_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                " AND employee_id = '" + objReportModel.EmployeeId + "') total_cl,  " +

                "(SELECT COUNT(*) " +
                " FROM employee_leave " +
                " WHERE leave_type_id = '4' " +
                " AND leave_start_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                " AND employee_id = '" + objReportModel.EmployeeId + "') total_sl,  " +

                "(SELECT COUNT(*) " +
                " FROM employee_leave " +
                " WHERE leave_type_id = '5' " +
                " AND leave_start_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                " AND employee_id = '" + objReportModel.EmployeeId + "') total_lwp,  " +

                "(SELECT COUNT(*) " +
                " FROM employee_leave " +
                " WHERE leave_type_id = '6' " +
                " AND leave_start_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                " AND employee_id = '" + objReportModel.EmployeeId + "') total_ml,  " +

                "(SELECT COUNT(*) " +
                " FROM employee_leave " +
                " WHERE leave_type_id = '10' " +
                " AND leave_start_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                " AND employee_id = '" + objReportModel.EmployeeId + "') total_el,  " +

                "(SELECT COUNT(*) " +
                " FROM employee_leave " +
                " WHERE leave_type_id = '7' " +
                " AND leave_start_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                " AND employee_id = '" + objReportModel.EmployeeId + "') total_pl,  " +

                " (SELECT (SELECT COUNT(*) " +
                " FROM employee_attendance " +
                " WHERE leave_type_id = '9' " +
                " AND log_date BETWEEN TO_DATE ('" + objReportModel.FromDate + "', 'dd/mm/yyyy') " +
                " AND TO_DATE ('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                " AND employee_id = '" + objReportModel.EmployeeId + "' ) " +
                " + (SELECT COUNT(*) " +
                " FROM employee_attendance " +
                " WHERE holiday_type_id = '2' " +
                " AND log_date BETWEEN TO_DATE ('" + objReportModel.FromDate + "', 'dd/mm/yyyy') " +
                " AND TO_DATE ('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                " AND employee_id = '" + objReportModel.EmployeeId + "')  " +

                " FROM DUAL)total_govt_holiday, " +


                "(SELECT COUNT(*) " +
                " FROM vew_rpt_individual_attendance where " +
                " log_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') and WEEKLY_HOLIDAY_YN ='Y' " +
                " AND employee_id = '" + objReportModel.EmployeeId + "' and leave_type_id is null and (shift_in_time is null and shift_out_time is null) ) total_weekly_holiday,  " +

                "TOTAL_WORKING_HOUR, " +
                "TOTAL_WORKING_MINUTE, " +
                "TOTAL_WORKING_SECOND," +
                "total_working_status, " +
                "AVG_DUTY_TIME, " +
                "TOTAL_EARLY_OUT_HOUR, " +
                "TOTAL_EARLY_OUT_MINUTE, " +
                "TOTAL_EARLY_OUT_SECOND, " +
                "total_early_out_status, " +
                   "employee_pic, " +
   "EL_DUE                  , " +
  "CL_DUE                  , " +
  "SL_DUE                  ," +
  "TOTAL_DEDUCTED_DAY      ," +
  "TOTAL_EL_DUE, " +
  "TOTAL_ALTERNATIVE_HOLIDAY, " +
  "transfer_date " +


                "from vew_rpt_individual_attendance  where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') and employee_id = '" + objReportModel.EmployeeId + "' ";

                ////sql = sql + " order by to_number(card_no)";

                using (OracleConnection strConn = GetConnection())
                {
                    OracleCommand objOracleCommand = new OracleCommand(sql, strConn);
                    strConn.Open();
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter(objOracleCommand);

                    try
                    {
                        ////strConn.Open();
                        ////OracleDataAdapter objDataAdapter = new OracleDataAdapter(objOracleCommand);
                        ////dataTable.Clear();

                        dataSet = new DataSet();
                        objDataAdapter.Fill(dataSet, "vew_rpt_individual_attendance");
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
                //}
                //catch (Exception ex)
                //{
                //    throw ex;
                //}

                //return dataSet;
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }

            return dataSet;
        }
        public string IndividualLeaveProess(ReportModel objReportModel)
        {

            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_LEAVE_INDIVIDUAL_PROCESS");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objReportModel.EmployeeId) ? objReportModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objReportModel.LeaveYear) ? objReportModel.LeaveYear : null;
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objReportModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objReportModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objReportModel.BranchOfficeId;

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
        public string IndividualLeaveProess(EmployeeReportModel objEmployeeReportModel)
        {

            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_LEAVE_INDIVIDUAL_PROCESS");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeReportModel.EmployeeId) ? objEmployeeReportModel.EmployeeId : null;
            objOracleCommand.Parameters.Add("p_leave_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objEmployeeReportModel.Year) ? objEmployeeReportModel.Year : null;
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeReportModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeReportModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objEmployeeReportModel.BranchOfficeId;

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

        public DataSet GetLeaveIndividual(ReportModel objReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +
                       "RPT_TITLE, " +
                       "EMPLOYEE_ID, " +
                       "EMPLOYEE_NAME, " +
                       "DESIGNATION_ID, " +
                       "DESIGNATION_NAME, " +
                       "LEAVE_TYPE_ID, " +
                       "LEAVE_TYPE_NAME, " +
                       "LEAVE_TYPE, " +
                       "LEAVE_YEAR, " +
                       "TOTAL_LEAVE, " +
                       "MAX_LEAVE, " +
                       "LEAVE_REMAIN, " +
                       "DEPARTMENT_NAME, " +
                       "UNIT_NAME, " +
                       "SECTION_NAME, " +
                       "SUB_SECTION_NAME, " +
                       "HEAD_OFFICE_ID, " +
                       "HEAD_OFFICE_NAME, " +
                       "BRANCH_OFFICE_ID, " +
                       "BRANCH_OFFICE_NAME, " +
                       "BRANCH_OFFICE_ADDRESS, " +
                       "CARD_NO, " +
                       "UNIT_ID, " +
                       "department_id, " +
                       "section_id, " +
                       "sub_section_id, " +
                       "new_emp_id, " +
                       "new_emp_name, " +
                       "new_emp_designation, " +
                       "new_emp_joining_date, " +
                       "new_emp_birth_date, " +
                       "new_emp_department, " +
                       "leave_start_date, " +
                       "leave_end_date, " +
                       "TOTAL_CL_TAKEN, " +
                       "CL_REMAINING, " +
                       "cl_max, " +
                       "TOTAL_SL_TAKEN, " +
                       "SL_REMAINING, " +
                       "sl_max, " +
                       "TOTAL_LWP_TAKEN, " +
                       "LWP_REMAINING," +
                       "lwp_max, " +
                       "TOTAL_ML_TAKEN, " +
                       "ML_REMAINING, " +
                       "ml_max, " +
                       "TOTAL_PL_TAKEN, " +
                       "PL_REMAINING, " +
                       "pl_max, " +
                       "TOTAL_FL_TAKEN, " +
                       "FL_REMAINING, " +
                       "fl_max, " +
                       "TOTAL_GH_TAKEN, " +
                       "GH_REMAINING, " +
                       "gh_max, " +
                       "TOTAL_EL_TAKEN, " +
                       "EL_REMAINING, " +
                       "EL_MAX, " +
                        "employee_pic, " +
   "LEAVE_TYPE_NAME_SUM, " +
   "LEAVE_TYPE_SUM_CL, " +
   "LEAVE_TYPE_SUM_SL, " +
   "LEAVE_TYPE_SUM_LWP, " +
   "LEAVE_TYPE_SUM_ML, " +
   "LEAVE_TYPE_SUM_PL, " +
   "LEAVE_TYPE_SUM_FL, " +
   "LEAVE_TYPE_SUM_GH, " +
   "LEAVE_TYPE_SUM_EL, " +
   "REMAIN_LEAVE_CL, " +
   "REMAIN_LEAVE_SL, " +
   "REMAIN_LEAVE_LWP, " +
   "REMAIN_LEAVE_ML, " +
   "REMAIN_LEAVE_PL, " +
   "REMAIN_LEAVE_FL, " +
   "REMAIN_LEAVE_GH, " +
   "REMAIN_LEAVE_EL, " +

   "cl_leave_name, " +
   "sl_leave_name, " +
   "el_leave_name, " +
   "ml_leave_name, " +
   "pl_leave_name, " +
   "lwp_leave_name " +

                       " from VEW_RPT_LEAVE_INDIVIDUAL l where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and leave_year = '"+objReportModel.LeaveYear+"'  and employee_id = '" + objReportModel.EmployeeId + "' ";


                    if (!string.IsNullOrWhiteSpace(objReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrWhiteSpace(objReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrWhiteSpace(objReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrWhiteSpace(objReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_LEAVE_INDIVIDUAL");
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
        public DataSet GetFabricData(ReportFabricModel objReportFabricModel)
        {
            DataSet dataSet;

            try
            {
                string vQuery = "SELECT " +
                                "'Fabric Information Between ' || TO_DATE( '" + objReportFabricModel.FromDate + "', 'dd/mm/yyyy') || ' AND '|| TO_DATE( '" + objReportFabricModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                                "FABRIC_ID, " +
                                "FABRIC_CODE," +
                                "PURCHASE_DATE," +
                                "FABRIC_TYPE_ID," +
                                "FABRIC_TYPE_NAME," +
                                "FABRIC_UNIT_ID," +
                                "FABRIC_UNIT_NAME," +
                                "SUPPLIER_ID," +
                                "SUPPLIER_NAME," +
                                "WIDTH," +
                                "LOCATION_ID," +
                                "LOCATION_NAME," +
                                "CATEGORY_ID," +
                                "CATEGORY_NAME," +
                                "ORDER_QUANTITY," +
                                "RECEIVED_QUANTITY," +
                                "PRICE," +
                                "TOTAL_AMOUNT," +
                                "DESIGNER_ID," +
                                "DESIGNER_NAME," +
                                "SAMPLE_QUANTITY," +
                                "OTHER_QUANTITY," +
                                "BULK_QUANTITY," +
                                "TOTAL_QUANTITY," +
                                "BALANCE_QUANTITY," +
                                "LAB_TEST_ID," +
                                "LAB_TEST_NAME," +
                                "CREATE_BY," +
                                "CREATE_DATE," +
                                "UPDATE_BY," +
                                "UPDATE_DATE," +
                                "HEAD_OFFICE_ID," +
                                "HEAD_OFFICE_NAME," +
                                "BRANCH_OFFICE_ID," +
                                "BRANCH_OFFICE_NAME," +
                                "BRANCH_OFFICE_ADDRESS" +
                                " FROM VEW_RPT_FABRIC_INFORMATION WHERE HEAD_OFFICE_ID = '" + objReportFabricModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objReportFabricModel.BranchOfficeId + "'  AND PURCHASE_DATE BETWEEN TO_DATE('" + objReportFabricModel.FromDate + "', 'DD/MM/YYYY') AND TO_DATE('" + objReportFabricModel.ToDate + "', 'DD/MM/YYYY') ";

                if (!string.IsNullOrWhiteSpace(objReportFabricModel.SupplierId))
                {
                    vQuery += " AND SUPPLIER_ID = '" + objReportFabricModel.SupplierId + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objReportFabricModel.FabricCode))
                {
                    vQuery += " AND FABRIC_CODE = '" + objReportFabricModel.FabricCode + "' ";
                }

                using (OracleConnection objConnection = GetConnection())
                {
                    OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                    objConnection.Open();
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);

                    try
                    {
                        dataSet = new DataSet();
                        objDataAdapter.Fill(dataSet, "VEW_RPT_FABRIC_INFORMATION");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        objDataAdapter.Dispose();
                        objCommand.Dispose();
                        objConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }

            return dataSet;
        }
        public DataSet DisplayIdCard(ReportModel objReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
                           "GROUP_NAME, " +
                           "BRANCH_OFFICE_NAME, " +
                           "BRANCH_OFFICE_ADDRESS, " +
                           "PHONE_NO, " +
                           "MOBILE_NO, " +
                           "EMPLOYEE_ID, " +
                           "WEBSITE_ADDRESS, " +
                           "EMP_IMAGE, " +
                           "EMP_SIGNATURE, " +
                           "EMPLOYEE_NAME, " +
                           "DESIGNATION_NAME, " +
                           "NID_NO, " +
                           "DATE_OF_BIRTH, " +
                           "JOINING_DATE, " +
                           "BLOOD_GROUP_NAME, " +
                           "EMERGENCY_CONTACT_NO, " +
                           "DEPARTMENT_ID, " +
                           "UNIT_ID, " +
                           "SECTION_ID, " +
                           "SUB_SECTION_ID, " +
                           "new_emp_id, " +
                            "(select employee_name from REGISTRATION where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY " +
                            "from VEW_ID_CARD  where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "'";


                    if (!string.IsNullOrWhiteSpace(objReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objReportModel.EmployeeId + "' ";

                    }


                    if (!string.IsNullOrWhiteSpace(objReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objReportModel.DepartmentId + "' ";

                    }


                    if (!string.IsNullOrWhiteSpace(objReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objReportModel.UnitId + "' ";

                    }



                    if (!string.IsNullOrWhiteSpace(objReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objReportModel.SectionId + "' ";

                    }


                    if (!string.IsNullOrWhiteSpace(objReportModel.SubSectionId))
                    {
                        sql = sql + "and SUB_section_id = '" + objReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_ID_CARD");
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
        #region Attendance Report  

        public DataSet DailyAttendanceSheet(ReportModel objReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    if (objReportModel.UnitId != null && objReportModel.DepartmentId != null && objReportModel.SectionId != null && objReportModel.SubSectionId != null)
                    {
                        sql = "SELECT " +

                              "'Daily Attendence Report on '|| ' - '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                              "EMPLOYEE_ID, " +
                              "EMPLOYEE_NAME, " +
                              "JOINING_DATE, " +
                              "DESIGNATION_NAME, " +
                              "UNIT_ID, " +
                              "UNIT_NAME, " +
                              "DEPARTMENT_ID, " +
                              "DEPARTMENT_NAME, " +
                              "SECTION_ID, " +
                              "SECTION_NAME, " +
                              "SUB_SECTION_ID, " +
                              "SUB_SECTION_NAME, " +
                              "FIRST_IN, " +
                              "LAST_OUT, " +
                              "FIRST_LATE_HOUR, " +
                              "FIRST_LATE_MINUTE, " +
                              "LUNCH_OUT, " +
                              "LUNCH_IN, " +
                              "LUNCH_LATE_HOUR, " +
                              "LUNCH_LATE_MINUTE, " +
                              "OT_HOUR, " +
                              "OT_MINUTE, " +
                              "LOG_LATE, " +
                              "LUNCH_LATE, " +
                              "LOG_DATE, " +
                              "PUNCH_CODE, " +
                              "DAY_TYPE, " +
                              "REMARKS, " +

                              "UPDATE_DATE, " +
                              "HEAD_OFFICE_ID, " +
                              "HEAD_OFFICE_NAME, " +
                              "BRANCH_OFFICE_ID, " +
                              "BRANCH_OFFICE_NAME, " +
                              "BRANCH_OFFICE_ADDRESS, " +
                              "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +

                              "LATE_YN, " +
                              "late_status, " +
                              "PUNCTUAL_YN, " +
                              "PUNCTUAL_STATUS, " +
                              "LATE_HMS, " +
                              "total_duty_time, " +
                              "EARLY_OUT_TIME, " +
                              "finger_yn,  " +
                              "card_yn,  " +
                              "punch_type,  " +
                              "first_in_new, " +
                              "last_out_new, " +

                              "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and unit_id = '" + objReportModel.UnitId + "'  and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_employee, " +


                              " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_present_employee, " +



                              " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_absent_employee, " +

                              " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_holiday_employee, " +



                              " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_leave_employee, " +


                              " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)toal_late_employee, " +


                              " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_punctual_employee, " +

                              " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_gh, " +


                              "NB, " +
                              "present_status, " +
                              "absent_status, " +
                              "leave_status, " +
                              "WORKING_HOLIDAY_STATUS, " +
                              "late_status_new, " +

                              "PLELA_ALL, " +
                              "late_hm, " +


                              " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_early_out, " +


                              " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_early_late, " +

                              " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_cl, " +


                              " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_sl, " +

                              " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_ml, " +

                              " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and unit_id = '" + objReportModel.UnitId + "' and deparment_id = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_el, " +

                              " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_pl, " +

                              " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_lwp, " +


                              " (SELECT 'Alternative Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and ALTERNATIVE_HOLIDAY_YN = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  and  sub_section_id = '" + objReportModel.SubSectionId + "') " +
                              " FROM DUAL)total_ah_employee " +

                              "from VEW_RPT_DAILY_ATTENDANCE_SHEET a  where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') ";

                        if (objReportModel.UnitId != null)
                        {
                            sql = sql + "and unit_id = '" + objReportModel.UnitId + "' ";

                        }

                        if (objReportModel.DepartmentId != null)
                        {
                            sql = sql + "and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' ";

                        }

                        if (objReportModel.SectionId != null)
                        {
                            sql = sql + "and section_id = '" + objReportModel.SectionId + "' ";

                        }

                        if (objReportModel.SubSectionId != null)
                        {
                            sql = sql + "and sub_section_id = '" + objReportModel.SubSectionId + "' ";

                        }

                    }


                    else if (objReportModel.UnitId != null && objReportModel.DepartmentId != null && objReportModel.SectionId != null )
                    {
                        sql = "SELECT " +

                              "'Daily Attendence Report on '|| ' - '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                              "EMPLOYEE_ID, " +
                              "EMPLOYEE_NAME, " +
                              "JOINING_DATE, " +
                              "DESIGNATION_NAME, " +
                              "UNIT_ID, " +
                              "UNIT_NAME, " +
                              "DEPARTMENT_ID, " +
                              "DEPARTMENT_NAME, " +
                              "SECTION_ID, " +
                              "SECTION_NAME, " +
                              "SUB_SECTION_ID, " +
                              "SUB_SECTION_NAME, " +
                              "FIRST_IN, " +
                              "LAST_OUT, " +
                              "FIRST_LATE_HOUR, " +
                              "FIRST_LATE_MINUTE, " +
                              "LUNCH_OUT, " +
                              "LUNCH_IN, " +
                              "LUNCH_LATE_HOUR, " +
                              "LUNCH_LATE_MINUTE, " +
                              "OT_HOUR, " +
                              "OT_MINUTE, " +
                              "LOG_LATE, " +
                              "LUNCH_LATE, " +
                              "LOG_DATE, " +
                              "PUNCH_CODE, " +
                              "DAY_TYPE, " +
                              "REMARKS, " +

                              "UPDATE_DATE, " +
                              "HEAD_OFFICE_ID, " +
                              "HEAD_OFFICE_NAME, " +
                              "BRANCH_OFFICE_ID, " +
                              "BRANCH_OFFICE_NAME, " +
                              "BRANCH_OFFICE_ADDRESS, " +
                              "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +

                              "LATE_YN, " +
                              "late_status, " +
                              "PUNCTUAL_YN, " +
                              "PUNCTUAL_STATUS, " +
                              "LATE_HMS, " +
                              "total_duty_time, " +
                              "EARLY_OUT_TIME, " +
                              "finger_yn,  " +
                              "card_yn,  " +
                              "punch_type,  " +
                              "first_in_new, " +
                              "last_out_new, " +

                              "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and unit_id = '" + objReportModel.UnitId + "'  and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_employee, " +


                              " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_present_employee, " +



                              " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_absent_employee, " +

                              " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_holiday_employee, " +



                              " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  ) " +
                              " FROM DUAL)total_leave_employee, " +


                              " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)toal_late_employee, " +


                              " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_punctual_employee, " +

                              " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  ) " +
                              " FROM DUAL)total_gh, " +


                              "NB, " +
                              "present_status, " +
                              "absent_status, " +
                              "leave_status, " +
                              "WORKING_HOLIDAY_STATUS, " +
                              "late_status_new, " +

                              "PLELA_ALL, " +
                              "late_hm, " +


                              " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_early_out, " +


                              " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_early_late, " +

                              " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_cl, " +


                              " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_sl, " +

                              " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'  ) " +
                              " FROM DUAL)total_ml, " +

                              " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_el, " +

                              " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_pl, " +

                              " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_lwp, " +


                              " (SELECT 'Alternative Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and ALTERNATIVE_HOLIDAY_YN = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' and  section_id = '" + objReportModel.SectionId + "'   ) " +
                              " FROM DUAL)total_ah_employee " +

                              "from VEW_RPT_DAILY_ATTENDANCE_SHEET a  where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') ";

                        if (objReportModel.UnitId != null)
                        {
                            sql = sql + "and unit_id = '" + objReportModel.UnitId + "' ";

                        }

                        if (objReportModel.DepartmentId != null)
                        {
                            sql = sql + "and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' ";

                        }

                        if (objReportModel.SectionId != null)
                        {
                            sql = sql + "and section_id = '" + objReportModel.SectionId + "' ";

                        }

                     

                    }

                    else if (objReportModel.UnitId != null && objReportModel.DepartmentId != null )
                    {
                        sql = "SELECT " +

                              "'Daily Attendence Report on '|| ' - '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                              "EMPLOYEE_ID, " +
                              "EMPLOYEE_NAME, " +
                              "JOINING_DATE, " +
                              "DESIGNATION_NAME, " +
                              "UNIT_ID, " +
                              "UNIT_NAME, " +
                              "DEPARTMENT_ID, " +
                              "DEPARTMENT_NAME, " +
                              "SECTION_ID, " +
                              "SECTION_NAME, " +
                              "SUB_SECTION_ID, " +
                              "SUB_SECTION_NAME, " +
                              "FIRST_IN, " +
                              "LAST_OUT, " +
                              "FIRST_LATE_HOUR, " +
                              "FIRST_LATE_MINUTE, " +
                              "LUNCH_OUT, " +
                              "LUNCH_IN, " +
                              "LUNCH_LATE_HOUR, " +
                              "LUNCH_LATE_MINUTE, " +
                              "OT_HOUR, " +
                              "OT_MINUTE, " +
                              "LOG_LATE, " +
                              "LUNCH_LATE, " +
                              "LOG_DATE, " +
                              "PUNCH_CODE, " +
                              "DAY_TYPE, " +
                              "REMARKS, " +

                              "UPDATE_DATE, " +
                              "HEAD_OFFICE_ID, " +
                              "HEAD_OFFICE_NAME, " +
                              "BRANCH_OFFICE_ID, " +
                              "BRANCH_OFFICE_NAME, " +
                              "BRANCH_OFFICE_ADDRESS, " +
                              "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +

                              "LATE_YN, " +
                              "late_status, " +
                              "PUNCTUAL_YN, " +
                              "PUNCTUAL_STATUS, " +
                              "LATE_HMS, " +
                              "total_duty_time, " +
                              "EARLY_OUT_TIME, " +
                              "finger_yn,  " +
                              "card_yn,  " +
                              "punch_type,  " +
                              "first_in_new, " +
                              "last_out_new, " +

                              "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and unit_id = '" + objReportModel.UnitId + "'  and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'    ) " +
                              " FROM DUAL)total_employee, " +


                              " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'     ) " +
                              " FROM DUAL)total_present_employee, " +



                              " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'    ) " +
                              " FROM DUAL)total_absent_employee, " +

                              " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'   ) " +
                              " FROM DUAL)total_holiday_employee, " +



                              " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'   ) " +
                              " FROM DUAL)total_leave_employee, " +


                              " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'    ) " +
                              " FROM DUAL)toal_late_employee, " +


                              " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'   ) " +
                              " FROM DUAL)total_punctual_employee, " +

                              " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'  ) " +
                              " FROM DUAL)total_gh, " +


                              "NB, " +
                              "present_status, " +
                              "absent_status, " +
                              "leave_status, " +
                              "WORKING_HOLIDAY_STATUS, " +
                              "late_status_new, " +

                              "PLELA_ALL, " +
                              "late_hm, " +


                              " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'    ) " +
                              " FROM DUAL)total_early_out, " +


                              " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'   ) " +
                              " FROM DUAL)total_early_late, " +

                              " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'   ) " +
                              " FROM DUAL)total_cl, " +


                              " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'     ) " +
                              " FROM DUAL)total_sl, " +

                              " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'    ) " +
                              " FROM DUAL)total_ml, " +

                              " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'     ) " +
                              " FROM DUAL)total_el, " +

                              " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'     ) " +
                              " FROM DUAL)total_pl, " +

                              " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'    ) " +
                              " FROM DUAL)total_lwp, " +


                              " (SELECT 'Alternative Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and ALTERNATIVE_HOLIDAY_YN = 'Y' and unit_id = '" + objReportModel.UnitId + "' and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "'   ) " +
                              " FROM DUAL)total_ah_employee " +

                              "from VEW_RPT_DAILY_ATTENDANCE_SHEET a  where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') ";

                        if (objReportModel.UnitId != null)
                        {
                            sql = sql + "and unit_id = '" + objReportModel.UnitId + "' ";

                        }

                        if (objReportModel.DepartmentId != null)
                        {
                            sql = sql + "and DEPARTMENT_ID = '" + objReportModel.DepartmentId + "' ";

                        }

                       


                    }


                    else if (objReportModel.UnitId != null)            
                    {
                        sql = "SELECT " +

                              "'Daily Attendence Report on '|| ' - '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                              "EMPLOYEE_ID, " +
                              "EMPLOYEE_NAME, " +
                              "JOINING_DATE, " +
                              "DESIGNATION_NAME, " +
                              "UNIT_ID, " +
                              "UNIT_NAME, " +
                              "DEPARTMENT_ID, " +
                              "DEPARTMENT_NAME, " +
                              "SECTION_ID, " +
                              "SECTION_NAME, " +
                              "SUB_SECTION_ID, " +
                              "SUB_SECTION_NAME, " +
                              "FIRST_IN, " +
                              "LAST_OUT, " +
                              "FIRST_LATE_HOUR, " +
                              "FIRST_LATE_MINUTE, " +
                              "LUNCH_OUT, " +
                              "LUNCH_IN, " +
                              "LUNCH_LATE_HOUR, " +
                              "LUNCH_LATE_MINUTE, " +
                              "OT_HOUR, " +
                              "OT_MINUTE, " +
                              "LOG_LATE, " +
                              "LUNCH_LATE, " +
                              "LOG_DATE, " +
                              "PUNCH_CODE, " +
                              "DAY_TYPE, " +
                              "REMARKS, " +

                              "UPDATE_DATE, " +
                              "HEAD_OFFICE_ID, " +
                              "HEAD_OFFICE_NAME, " +
                              "BRANCH_OFFICE_ID, " +
                              "BRANCH_OFFICE_NAME, " +
                              "BRANCH_OFFICE_ADDRESS, " +
                              "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +

                              "LATE_YN, " +
                              "late_status, " +
                              "PUNCTUAL_YN, " +
                              "PUNCTUAL_STATUS, " +
                              "LATE_HMS, " +
                              "total_duty_time, " +
                              "EARLY_OUT_TIME, " +
                              "finger_yn,  " +
                              "card_yn,  " +
                              "punch_type,  " +
                              "first_in_new, " +
                              "last_out_new, " +

                              "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_employee, " +


                              " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_present_employee, " +



                              " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_absent_employee, " +

                              " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' ) " +
                              " FROM DUAL)total_holiday_employee, " +



                              " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "' ) " +
                              " FROM DUAL)total_leave_employee, " +


                              " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)toal_late_employee, " +


                              " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_punctual_employee, " +

                              " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and unit_id = '" + objReportModel.UnitId + "' ) " +
                              " FROM DUAL)total_gh, " +


                              "NB, " +
                              "present_status, " +
                              "absent_status, " +
                              "leave_status, " +
                              "WORKING_HOLIDAY_STATUS, " +
                              "late_status_new, " +

                              "PLELA_ALL, " +
                              "late_hm, " +


                              " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_early_out, " +


                              " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y' and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_early_late, " +

                              " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_cl, " +


                              " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_sl, " +

                              " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_ml, " +

                              " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_el, " +

                              " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_pl, " +

                              " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_lwp, " +


                              " (SELECT 'Alternative Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and ALTERNATIVE_HOLIDAY_YN = 'Y' and unit_id = '" + objReportModel.UnitId + "') " +
                              " FROM DUAL)total_ah_employee " +

                              "from VEW_RPT_DAILY_ATTENDANCE_SHEET a  where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') ";

                        if (objReportModel.UnitId != null)
                        {
                            sql = sql + "and unit_id = '" + objReportModel.UnitId + "' ";

                        }

                    }

                    //else if (objReportModel.SubSectionId != null)
                    //{
                    //    sql = "SELECT " +

                    //          "'Daily Attendence '|| ' - '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                    //          "EMPLOYEE_ID, " +
                    //          "EMPLOYEE_NAME, " +
                    //          "JOINING_DATE, " +
                    //          "DESIGNATION_NAME, " +
                    //          "UNIT_ID, " +
                    //          "UNIT_NAME, " +
                    //          "DEPARTMENT_ID, " +
                    //          "DEPARTMENT_NAME, " +
                    //          "SECTION_ID, " +
                    //          "SECTION_NAME, " +
                    //          "SUB_SECTION_ID, " +
                    //          "SUB_SECTION_NAME, " +
                    //          "FIRST_IN, " +
                    //          "LAST_OUT, " +
                    //          "FIRST_LATE_HOUR, " +
                    //          "FIRST_LATE_MINUTE, " +
                    //          "LUNCH_OUT, " +
                    //          "LUNCH_IN, " +
                    //          "LUNCH_LATE_HOUR, " +
                    //          "LUNCH_LATE_MINUTE, " +
                    //          "OT_HOUR, " +
                    //          "OT_MINUTE, " +
                    //          "LOG_LATE, " +
                    //          "LUNCH_LATE, " +
                    //          "LOG_DATE, " +
                    //          "PUNCH_CODE, " +
                    //          "DAY_TYPE, " +
                    //          "REMARKS, " +

                    //          "UPDATE_DATE, " +
                    //          "HEAD_OFFICE_ID, " +
                    //          "HEAD_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ID, " +
                    //          "BRANCH_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ADDRESS, " +
                    //          "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +

                    //          "LATE_YN, " +
                    //          "late_status, " +
                    //          "PUNCTUAL_YN, " +
                    //          "PUNCTUAL_STATUS, " +
                    //          "LATE_HMS, " +
                    //          "total_duty_time, " +
                    //          "EARLY_OUT_TIME, " +
                    //          "finger_yn,  " +
                    //          "card_yn,  " +
                    //          "punch_type,  " +
                    //          "first_in_new, " +
                    //          "last_out_new, " +

                    //          "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_employee, " +


                    //          " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_present_employee, " +



                    //          " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_absent_employee, " +

                    //          " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and sub_Section_id = '" + objReportModel.SubSectionId + "' ) " +
                    //          " FROM DUAL)total_holiday_employee, " +


                    //          " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and sub_Section_id = '" + objReportModel.SubSectionId + "' ) " +
                    //          " FROM DUAL)total_leave_employee, " +


                    //          " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)toal_late_employee, " +


                    //          " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_punctual_employee, " +

                    //          " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and sub_Section_id = '" + objReportModel.SubSectionId + "' ) " +
                    //          " FROM DUAL)total_gh, " +


                    //          "NB, " +
                    //          "present_status, " +
                    //          "absent_status, " +
                    //          "leave_status, " +
                    //          "WORKING_HOLIDAY_STATUS, " +
                    //          "late_status_new, " +
                    //          "PLELA_ALL, " +
                    //          "late_hm, " +



                    //          " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_early_out, " +


                    //          " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and late_yn = 'Y' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_early_late, " +

                    //          " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_cl, " +


                    //          " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_sl, " +

                    //          " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_ml, " +

                    //          " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_el, " +

                    //          " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_pl, " +


                    //          " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_lwp, " +

                    //         " (SELECT 'Alternative Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and ALTERNATIVE_HOLIDAY_YN = 'Y' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_ah_employee " +



                    //          "from VEW_RPT_DAILY_ATTENDANCE_SHEET a where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') ";

                    //    if (objReportModel.SubSectionId != null)
                    //    {
                    //        sql = sql + "and sub_section_id = '" + objReportModel.SubSectionId + "' ";

                    //    }

                    //}

                    //else if (objReportModel.DepartmentId != null)
                    //{
                    //    sql = "SELECT " +

                    //          "'Daily Attendence '|| ' - '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                    //          "EMPLOYEE_ID, " +
                    //          "EMPLOYEE_NAME, " +
                    //          "JOINING_DATE, " +
                    //          "DESIGNATION_NAME, " +
                    //          "UNIT_ID, " +
                    //          "UNIT_NAME, " +
                    //          "DEPARTMENT_ID, " +
                    //          "DEPARTMENT_NAME, " +
                    //          "SECTION_ID, " +
                    //          "SECTION_NAME, " +
                    //          "SUB_SECTION_ID, " +
                    //          "SUB_SECTION_NAME, " +
                    //          "FIRST_IN, " +
                    //          "LAST_OUT, " +
                    //          "FIRST_LATE_HOUR, " +
                    //          "FIRST_LATE_MINUTE, " +
                    //          "LUNCH_OUT, " +
                    //          "LUNCH_IN, " +
                    //          "LUNCH_LATE_HOUR, " +
                    //          "LUNCH_LATE_MINUTE, " +
                    //          "OT_HOUR, " +
                    //          "OT_MINUTE, " +
                    //          "LOG_LATE, " +
                    //          "LUNCH_LATE, " +
                    //          "LOG_DATE, " +
                    //          "PUNCH_CODE, " +
                    //          "DAY_TYPE, " +
                    //          "REMARKS, " +

                    //          "UPDATE_DATE, " +
                    //          "HEAD_OFFICE_ID, " +
                    //          "HEAD_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ID, " +
                    //          "BRANCH_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ADDRESS, " +
                    //          "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +

                    //          "LATE_YN, " +
                    //          "late_status, " +
                    //          "PUNCTUAL_YN, " +
                    //          "PUNCTUAL_STATUS, " +
                    //          "LATE_HMS, " +
                    //          "total_duty_time, " +
                    //          "EARLY_OUT_TIME, " +
                    //          "finger_yn,  " +
                    //          "card_yn,  " +
                    //          "punch_type,  " +
                    //          "first_in_new, " +
                    //          "last_out_new, " +

                    //          "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_employee, " +

                    //          " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_present_employee, " +

                    //          " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_absent_employee, " +

                    //          " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and department_id = '" + objReportModel.DepartmentId + "' ) " +
                    //          " FROM DUAL)total_holiday_employee, " +

                    //          " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and department_id = '" + objReportModel.DepartmentId + "' ) " +
                    //          " FROM DUAL)total_leave_employee, " +

                    //          " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)toal_late_employee, " +


                    //          " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_punctual_employee, " +

                    //          " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and department_id = '" + objReportModel.DepartmentId + "' ) " +
                    //          " FROM DUAL)total_gh, " +

                    //          "NB, " +

                    //          "present_status, " +
                    //          "absent_status, " +
                    //          "leave_status, " +
                    //          "WORKING_HOLIDAY_STATUS, " +
                    //          "late_status_new, " +
                    //          "PLELA_ALL, " +
                    //          "late_hm, " +


                    //          " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_early_out, " +

                    //          " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and late_yn = 'Y' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_early_late, " +

                    //          " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_cl, " +

                    //          " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_sl, " +

                    //          " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_ml, " +

                    //          " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_el, " +

                    //          " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_pl, " +

                    //          " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_lwp, " +

                    //          " (SELECT 'Alternative Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and ALTERNATIVE_HOLIDAY_YN = 'Y' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_ah_employee " +

                    //          "from VEW_RPT_DAILY_ATTENDANCE_SHEET a where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') ";


                    //    if (objReportModel.DepartmentId != null)
                    //    {
                    //        sql = sql + "and department_id = '" + objReportModel.DepartmentId + "' ";

                    //    }

                    //}

                    else
                    {
                        sql = "SELECT " +

                              "'Daily Attendence '|| ' - '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                              "EMPLOYEE_ID, " +
                              "EMPLOYEE_NAME, " +
                              "JOINING_DATE, " +
                              "DESIGNATION_NAME, " +
                              "UNIT_ID, " +
                              "UNIT_NAME, " +
                              "DEPARTMENT_ID, " +
                              "DEPARTMENT_NAME, " +
                              "SECTION_ID, " +
                              "SECTION_NAME, " +
                              "SUB_SECTION_ID, " +
                              "SUB_SECTION_NAME, " +
                              "FIRST_IN, " +
                              "LAST_OUT, " +
                              "FIRST_LATE_HOUR, " +
                              "FIRST_LATE_MINUTE, " +
                              "LUNCH_OUT, " +
                              "LUNCH_IN, " +
                              "LUNCH_LATE_HOUR, " +
                              "LUNCH_LATE_MINUTE, " +
                              "OT_HOUR, " +
                              "OT_MINUTE, " +
                              "LOG_LATE, " +
                              "LUNCH_LATE, " +
                              "LOG_DATE, " +
                              "PUNCH_CODE, " +
                              "DAY_TYPE, " +
                              "REMARKS, " +

                              "UPDATE_DATE, " +
                              "HEAD_OFFICE_ID, " +
                              "HEAD_OFFICE_NAME, " +
                              "BRANCH_OFFICE_ID, " +
                              "BRANCH_OFFICE_NAME, " +
                              "BRANCH_OFFICE_ADDRESS, " +
                              "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +

                              "LATE_YN, " +
                              "late_status, " +
                              "PUNCTUAL_YN, " +
                              "PUNCTUAL_STATUS, " +
                              "LATE_HMS, " +
                              "total_duty_time, " +
                              "EARLY_OUT_TIME, " +
                              "finger_yn,  " +
                              "card_yn,  " +
                              "punch_type,  " +
                              "first_in_new, " +
                              "last_out_new, " +

                              "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  ) " +
                              " FROM DUAL)total_employee, " +


                              " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00')  " +
                              " FROM DUAL)total_present_employee, " +



                              " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' ) " +
                              " FROM DUAL)total_absent_employee, " +

                              " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' ) " +
                              " FROM DUAL)total_holiday_employee, " +



                              " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' ) " +
                              " FROM DUAL)total_leave_employee, " +


                              " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y' ) " +
                              " FROM DUAL)toal_late_employee, " +


                              " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' ) " +
                              " FROM DUAL)total_punctual_employee, " +

                              " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' ) " +
                              " FROM DUAL)total_gh, " +


                              "NB, " +
                              "present_status, " +
                              "absent_status, " +
                              "leave_status, " +
                              "WORKING_HOLIDAY_STATUS, " +
                              "late_status_new, " +
                              "PLELA_ALL, " +
                              "late_hm, " +


                              " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' ) " +
                              " FROM DUAL)total_early_out, " +


                              " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                              "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and late_yn = 'Y' ) " +
                              " FROM DUAL)total_early_late, " +

                              " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' ) " +
                              " FROM DUAL)total_cl, " +


                              " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' ) " +
                              " FROM DUAL)total_sl, " +

                              " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' ) " +
                              " FROM DUAL)total_ml, " +

                              " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' ) " +
                              " FROM DUAL)total_el, " +

                              " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' ) " +
                              " FROM DUAL)total_pl, " +


                              " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' ) " +
                              " FROM DUAL)total_lwp, " +

                              " (SELECT 'Alternative Holiday : '|| (SELECT COUNT(*) " +
                              "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                              "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') AND ALTERNATIVE_HOLIDAY_YN = 'Y' ) " +
                              " FROM DUAL)total_ah_employee " +


                              "from VEW_RPT_DAILY_ATTENDANCE_SHEET a where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') ";


                        if (objReportModel.EmployeeId != null)
                        {
                            sql = sql + "and employee_id = '" + objReportModel.EmployeeId + "' ";

                        }
                        if (objReportModel.DepartmentId != null)
                        {
                            sql = sql + "and department_id = '" + objReportModel.DepartmentId + "' ";

                        }

                        if (objReportModel.UnitId != null)
                        {
                            sql = sql + "and unit_id = '" + objReportModel.UnitId + "' ";

                        }

                        if (objReportModel.SectionId != null)
                        {
                            sql = sql + "and section_id = '" + objReportModel.SectionId + "' ";

                        }

                        if (objReportModel.SubSectionId != null)
                        {
                            sql = sql + "and sub_section_id = '" + objReportModel.SubSectionId + "' ";

                        }

                    }

                    //sql = sql + " order by to_number(card_no)";

                    //sql = sql + " order by to_number(card_no)";

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_DAILY_ATTENDANCE_SHEET");
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

        public DataSet DailyLateSheet(ReportModel objReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +

                          "'Daily Late Attendence '|| '-  '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy') RPT_TITLE, " +
                          "EMPLOYEE_ID, " +
                          "EMPLOYEE_NAME, " +
                          "JOINING_DATE, " +
                          "DESIGNATION_NAME, " +
                          "UNIT_ID, " +
                          "UNIT_NAME, " +
                          "DEPARTMENT_ID, " +
                          "DEPARTMENT_NAME, " +
                          "SECTION_ID, " +
                          "SECTION_NAME, " +
                          "SUB_SECTION_ID, " +
                          "SUB_SECTION_NAME, " +
                          "FIRST_IN, " +
                          "LAST_OUT, " +
                          "FIRST_LATE_HOUR, " +
                          "FIRST_LATE_MINUTE, " +
                          "LUNCH_OUT, " +
                          "LUNCH_IN, " +
                          "LUNCH_LATE_HOUR, " +
                          "LUNCH_LATE_MINUTE, " +
                          "OT_HOUR, " +
                          "OT_MINUTE, " +
                          "LOG_LATE, " +
                          "LUNCH_LATE, " +
                          "LOG_DATE, " +
                          "PUNCH_CODE, " +
                          "DAY_TYPE, " +
                          "REMARKS, " +

                          "UPDATE_DATE, " +
                          "HEAD_OFFICE_ID, " +
                          "HEAD_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ID, " +
                          "BRANCH_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ADDRESS, " +
                          "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +

                          "LATE_YN, " +
                          "late_status, " +
                          "PUNCTUAL_YN, " +
                          "PUNCTUAL_STATUS, " +
                          "LATE_HMS, " +
                          "total_duty_time, " +
                          "EARLY_OUT_TIME, " +

                          "shift_in_time, " +
                          "shift_out_time, " +
                          "late_hm " +

                          "from VEW_RPT_DAILY_LATE_SHEET  where head_office_id = '" + objReportModel.HeadOfficeId +
                          "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "'" +
                          " and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') " +
                          "and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') ";


                    if (objReportModel.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objReportModel.EmployeeId + "' ";

                    }
                    if (objReportModel.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objReportModel.DepartmentId + "' ";

                    }

                    if (objReportModel.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objReportModel.UnitId + "' ";

                    }

                    if (objReportModel.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objReportModel.SectionId + "' ";

                    }

                    if (objReportModel.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objReportModel.SubSectionId + "' ";

                    }

                    //sql = sql + " order by to_number(card_no)";

                    //sql = sql + " order by to_number(card_no)";

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_DAILY_LATE_SHEET");
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

        public DataSet DailyAbsentSheet(ReportModel objReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +

                          "'Absent Information '|| 'From  '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy') || ' to ' || to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                          "EMPLOYEE_ID, " +
                          "EMPLOYEE_NAME, " +
                          "JOINING_DATE, " +
                          "DESIGNATION_NAME, " +
                          "UNIT_ID, " +
                          "UNIT_NAME, " +
                          "DEPARTMENT_ID, " +
                          "DEPARTMENT_NAME, " +
                          "SECTION_ID, " +
                          "SECTION_NAME, " +
                          "SUB_SECTION_ID, " +
                          "SUB_SECTION_NAME, " +
                          "FIRST_IN, " +
                          "LAST_OUT, " +
                          "FIRST_LATE_HOUR, " +
                          "FIRST_LATE_MINUTE, " +
                          "LUNCH_OUT, " +
                          "LUNCH_IN, " +
                          "LUNCH_LATE_HOUR, " +
                          "LUNCH_LATE_MINUTE, " +
                          "OT_HOUR, " +
                          "OT_MINUTE, " +
                          "LOG_LATE, " +
                          "LUNCH_LATE, " +
                          "LOG_DATE, " +
                          "PUNCH_CODE, " +
                          "DAY_TYPE, " +
                          "REMARKS, " +

                          "UPDATE_DATE, " +
                          "HEAD_OFFICE_ID, " +
                          "HEAD_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ID, " +
                          "BRANCH_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ADDRESS, " +
                          "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +

                          "LATE_YN, " +
                          "late_status, " +
                          "PUNCTUAL_YN, " +
                          "PUNCTUAL_STATUS, " +
                          "LATE_HMS, " +
                          "total_duty_time, " +
                          "EARLY_OUT_TIME " +

                          "from VEW_RPT_DAILY_ABSENT_SHEET  where head_office_id = '" + objReportModel.HeadOfficeId + "'" +
                          " AND branch_office_id = '" + objReportModel.BranchOfficeId + "'" +
                          " and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') ";


                    if (objReportModel.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objReportModel.EmployeeId + "' ";

                    }
                    if (objReportModel.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objReportModel.DepartmentId + "' ";

                    }

                    if (objReportModel.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objReportModel.UnitId + "' ";

                    }

                    if (objReportModel.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objReportModel.SectionId + "' ";

                    }

                    if (objReportModel.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objReportModel.SubSectionId + "' ";

                    }

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_DAILY_ABSENT_SHEET");
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

        public DataSet DailyAttendanceMissingSheet(ReportModel objReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                          "'Detail In Time or Out Time Missing Histroy from  ' || to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +


                          "CARD_NO, " +
                          "EMPLOYEE_ID, " +
                          "EMPLOYEE_NAME, " +
                          "JOINING_DATE, " +
                          "DESIGNATION_NAME, " +
                          "UNIT_ID, " +
                          "UNIT_NAME, " +
                          "DEPARTMENT_ID, " +
                          "DEPARTMENT_NAME, " +
                          "SECTION_ID, " +
                          "SECTION_NAME, " +
                          "SUB_SECTION_ID, " +
                          "SUB_SECTION_NAME, " +
                          "PUNCH_CODE, " +
                          "DAY_TYPE, " +
                          "FIRST_IN, " +
                          "LAST_OUT, " +
                          "FIRST_LATE_HOUR, " +
                          "FIRST_LATE_MINUTE, " +
                          "LUNCH_OUT, " +
                          "LUNCH_IN, " +
                          "LUNCH_LATE_HOUR, " +
                          "LUNCH_LATE_MINUTE, " +
                          "OT_HOUR, " +
                          "OT_MINUTE, " +
                          "LOG_DATE, " +
                          "REMARKS, " +
                          "FIRST_LATE_SECOND, " +
                          "TOTAL_DUTY_TIME, " +
                          "EARLY_OUT_TIME, " +
                          "FINGER_YN, " +
                          "CARD_YN, " +
                          "WORKING_HOUR, " +
                          "WORKING_MINUTE, " +
                          "WORKING_SECOND, " +
                          "UPDATE_BY, " +
                          "UPDATE_DATE, " +
                          "HEAD_OFFICE_ID, " +
                          "HEAD_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ID, " +
                          "BRANCH_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ADDRESS " +

                          "from VEW_RPT_DAILY_ATTEN_MISSING  where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') ";


                    if (objReportModel.EmployeeId != "")
                    {
                        sql = sql + "and employee_id = '" + objReportModel.EmployeeId + "' ";

                    }
                    if (objReportModel.DepartmentId != "")
                    {
                        sql = sql + "and department_id = '" + objReportModel.DepartmentId + "' ";
                    }

                    if (objReportModel.UnitId != "")
                    {
                        sql = sql + "and unit_id = '" + objReportModel.UnitId + "' ";
                    }

                    if (objReportModel.SectionId != "")
                    {
                        sql = sql + "and section_id = '" + objReportModel.SectionId + "' ";

                    }

                    if (objReportModel.SubSectionId != "")
                    {
                        sql = sql + "and sub_section_id = '" + objReportModel.SubSectionId + "' ";

                    }

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_DAILY_ATTEN_MISSING");
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

        public DataSet IndividualAttendanceSheetForManualAttendance(ReportModel objReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +

                          "'Individual Attendence History '|| 'From  '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy') || ' to ' || to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                          "emp_title, " +
                          "EMPLOYEE_ID, " +
                          "EMPLOYEE_NAME, " +
                          "JOINING_DATE, " +
                          "DESIGNATION_NAME, " +
                          "UNIT_ID, " +
                          "UNIT_NAME, " +
                          "DEPARTMENT_ID, " +
                          "DEPARTMENT_NAME, " +
                          "SECTION_ID, " +
                          "SECTION_NAME, " +
                          "SUB_SECTION_ID, " +
                          "SUB_SECTION_NAME, " +
                          "PUNCH_CODE, " +
                          "LOG_DATE, " +
                          "FIRST_IN, " +
                          "LAST_OUT, " +
                          "TOTAL_DUTY_TIME, " +
                          "TOTAL_EARLY_OUT_TIME, " +
                          "TOTAL_LATE_TIME, " +
                          "LATE_YN, " +
                          "PUNCTUAL_YN, " +
                          "ABSENT_YN, " +
                          "LEAVE_YN, " +
                          "HOLIDAY_YN, " +
                          "DAY_TYPE, " +
                          "REMARKS, " +
                          "UPDATE_DATE, " +
                          "HEAD_OFFICE_ID, " +
                          "BRANCH_OFFICE_ID, " +
                          "LEAVE_TYPE_ID, " +
                          "DAY_NAME, " +
                          "TOTAL_LATE_DAY, " +
                          "MONTH_DAY, " +
                          "WORKING_DAY, " +
                          "HEAD_OFFICE_NAME, " +
                          "BRANCH_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ADDRESS, " +
                          "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +
                          "DAILY_DUTY_TIME, " +
                          "DAILY_EARLY_OUT_TIME, " +
                          "DAILY_LATE_TIME, " +
                          "emp_id, " +
                          "emp_name, " +
                          "emp_joining_date, " +
                          "emp_designation, " +
                          "emp_department, " +
                          "emp_date_of_birth, " +
                          "SHIFT_IN_TIME, " +
                          "SHIFT_OUT_TIME, " +
                          "SHIFT_STATUS, " +
                          "WORKING_HOUR, " +
                          "WORKING_MINUTE, " +
                          "WORKING_SECOND,  " +
                          "TOTAL_PRESENT_DAY,  " +
                          "TOTAL_ABSENT_DAY, " +
                          "TOTAL_HOLIDAY, " +
                          "TOTAL_WORKING_HOUR, " +
                          "TOTAL_WORKING_DAY, " +

                          "TOTAL_LATE_HOUR, " +
                          "TOTAL_LATE_MINUTE, " +
                          "TOTAL_LATE_SECOND, " +
                          "TOTAL_LATE_HMS, " +
                          "PUNCH_STATUS, " +
                          "TOTAL_WORKING_HOUR, " +
                          "TOTAL_WORKING_MINUTE, " +
                          "TOTAL_WORKING_SECOND, " +
                          "TOTAL_WORKING_STATUS, " +
                          "AVG_DUTY_TIME," +
                          "TOTAL_EARLY_OUT_HOUR, " +
                          "TOTAL_EARLY_OUT_MINUTE, " +
                          "TOTAL_EARLY_OUT_SECOND, " +
                          "TOTAL_EARLY_OUT_STATUS, " +
                          "color_status, " +

                          "(SELECT COUNT(*) " +
                          "                       FROM employee_leave " +
                          "                     WHERE leave_type_id = '3' " +
                          "                          AND leave_start_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                          "                         AND employee_id = '" + objReportModel.EmployeeId + "') total_cl,  " +

                          "(SELECT COUNT(*) " +
                          "                       FROM employee_leave " +
                          "                     WHERE leave_type_id = '4' " +
                          "                          AND leave_start_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                          "                         AND employee_id = '" + objReportModel.EmployeeId + "') total_sl,  " +

                          "(SELECT COUNT(*) " +
                          "                       FROM employee_leave " +
                          "                     WHERE leave_type_id = '5' " +
                          "                          AND leave_start_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                          "                         AND employee_id = '" + objReportModel.EmployeeId + "') total_lwp,  " +


                          "(SELECT COUNT(*) " +
                          "                       FROM employee_leave " +
                          "                     WHERE leave_type_id = '6' " +
                          "                          AND leave_start_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                          "                         AND employee_id = '" + objReportModel.EmployeeId + "') total_ml,  " +



                          "(SELECT COUNT(*) " +
                          "                       FROM employee_leave " +
                          "                     WHERE leave_type_id = '10' " +
                          "                          AND leave_start_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                          "                         AND employee_id = '" + objReportModel.EmployeeId + "') total_el,  " +



                          "(SELECT COUNT(*) " +
                          "                       FROM employee_leave " +
                          "                     WHERE leave_type_id = '7' " +
                          "                          AND leave_start_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                          "                         AND employee_id = '" + objReportModel.EmployeeId + "') total_pl,  " +



                          " (SELECT (SELECT COUNT(*) " +
                          "    FROM employee_attendance " +
                          "  WHERE leave_type_id = '9' " +
                          "     AND log_date BETWEEN TO_DATE ('" + objReportModel.FromDate + "', 'dd/mm/yyyy') " +
                          "              AND TO_DATE ('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                          "   AND employee_id = '" + objReportModel.EmployeeId + "' ) " +
                          "  + (SELECT COUNT(*) " +
                          "   FROM employee_attendance " +
                          "  WHERE holiday_type_id = '2' " +
                          "     AND log_date BETWEEN TO_DATE ('" + objReportModel.FromDate + "', 'dd/mm/yyyy') " +
                          "              AND TO_DATE ('" + objReportModel.ToDate + "', 'dd/mm/yyyy') " +
                          " AND employee_id = '" + objReportModel.EmployeeId + "')  " +




                          " FROM DUAL)total_govt_holiday, " +






                          "(SELECT COUNT(*) " +
                          "                       FROM vew_rpt_individual_attendance where " +
                          "                           log_date BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') and WEEKLY_HOLIDAY_YN ='Y' " +
                          "                         AND employee_id = '" + objReportModel.EmployeeId + "' and leave_type_id is null and (shift_in_time is null and shift_out_time is null) ) total_weekly_holiday,  " +

                          " TOTAL_WORKING_HOUR, " +
                          "TOTAL_WORKING_MINUTE, " +
                          "TOTAL_WORKING_SECOND," +
                          "total_working_status, " +
                          "AVG_DUTY_TIME, " +
                          "TOTAL_EARLY_OUT_HOUR, " +
                          "TOTAL_EARLY_OUT_MINUTE, " +
                          "TOTAL_EARLY_OUT_SECOND, " +
                          "total_early_out_status, " +
                          "employee_pic, " +

                            
   "EL_DUE                  , " +
  "CL_DUE                  , " +
  "SL_DUE                  ," +
  "TOTAL_DEDUCTED_DAY      ," +
  "TOTAL_EL_DUE, " +
  "TOTAL_ALTERNATIVE_HOLIDAY, " +
  "transfer_date " +



                          "from vew_rpt_individual_attendance  where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') and employee_id = '" + objReportModel.EmployeeId + "' ";

                    //sql = sql + " order by to_number(card_no)";

                    //sql = sql + " order by to_number(card_no)";

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "vew_rpt_individual_attendance");
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
        #region Working Day

        public DataSet GetCOMonthlyWorkingDayData(WorkingDayModel objWorkingDayModel)
        {
            DataSet objDataSet;

            try
            {
                string vQuery = "SELECT " +
                                "RPT_TITLE, " +
                                "EMPLOYEE_ID, " +
                                "EMPLOYEE_NAME, " +
                                "DESIGNATION_NAME, " +
                                "JOINING_DATE, " +
                                "CARD_NO, " +
                                "SALARY_YEAR, " +
                                "MONTH_ID, " +
                                "MONTH_NAME, " +
                                "DEPARTMENT_ID, " +
                                "DEPARTMENT_NAME, " +
                                "UNIT_ID, " +
                                "UNIT_NAME, " +
                                "SECTION_ID, " +
                                "SECTION_NAME, " +
                                "SUB_SECTION_ID, " +
                                "SUB_SECTION_NAME, " +
                                "WORKING_DAY, " +
                                "ADVANCE_AMOUNT, " +
                                "LEAVE_ALLOWANCE, " +
                                "TAX_DEDUCATION_AMOUNT, " +
                                "PF_ADVANCE_AMOUNT, " +
                                "FOOD_DEDUCTION_AMOUNT, " +
                                "FOOD_ALLOWANCE_AMOUNT, " +
                                "CREATE_BY, " +
                                "CREATE_DATE, " +
                                "UPDATE_BY, " +
                                "UPDATE_DATE, " +
                                "HEAD_OFFICE_ID, " +
                                "HEAD_OFFICE_NAME, " +
                                "BRANCH_OFFICE_ID, " +
                                "BRANCH_OFFICE_NAME, " +
                                "BRANCH_OFFICE_ADDRESS, " +
                                "GROSS_SALARY, " +
                                "MONTH_DAY, " +
                                "ARREAR_AMOUNT " +

                                "from vew_rpt_working_day_list_co  where head_office_id = '" + objWorkingDayModel.HeadOfficeId + "' AND branch_office_id = '" + objWorkingDayModel.BranchOfficeId + "' and salary_year ='" + objWorkingDayModel.SalaryYear + "' and month_id = '" + objWorkingDayModel.MonthId + "' ";


                if (!string.IsNullOrEmpty(objWorkingDayModel.UnitId))
                {
                    vQuery += "and unit_id = '" + objWorkingDayModel.UnitId + "' ";
                }
                if (!string.IsNullOrEmpty(objWorkingDayModel.DepartmentId))
                {
                    vQuery += "and department_id = '" + objWorkingDayModel.DepartmentId + "' ";
                }
                if (!string.IsNullOrEmpty(objWorkingDayModel.SectionId))
                {
                    vQuery += "and section_id = '" + objWorkingDayModel.SectionId + "' ";
                }
                if (!string.IsNullOrEmpty(objWorkingDayModel.SubSectionId))
                {
                    vQuery += "and sub_section_id = '" + objWorkingDayModel.SubSectionId + "' ";
                }
                if (!string.IsNullOrEmpty(objWorkingDayModel.EmployeeId))
                {
                    vQuery += "and employee_id = '" + objWorkingDayModel.EmployeeId + "' ";
                }
                if (!string.IsNullOrEmpty(objWorkingDayModel.EmployeeName))
                {
                    vQuery += "and EMPLOYEE_NAME = '" + objWorkingDayModel.EmployeeName + "' ";
                }
                if (!string.IsNullOrEmpty(objWorkingDayModel.CardNumber))
                {
                    vQuery += "and CARD_NO = '" + objWorkingDayModel.CardNumber + "' ";
                }


                using (OracleConnection objConnection = GetConnection())
                {
                    OracleCommand objOracleCommand = new OracleCommand(vQuery, objConnection);
                    objConnection.Open();
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter(objOracleCommand);

                    try
                    {
                        objDataSet = new DataSet();
                        objDataAdapter.Fill(objDataSet, "vew_rpt_working_day_list_co");
                    }
                    catch (Exception exception)
                    {
                        throw new Exception("Error: " + exception.Message);
                    }
                    finally
                    {
                        objDataAdapter.Dispose();
                        objOracleCommand.Dispose();
                        objConnection.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }

            return objDataSet;
        }

        #endregion
        #region Nabid Report Appointment Letter, Release Letter and Confirmation Letter
        //APPOINTMENT LETTER - NABID - 15 DEC 18
        public DataSet AppointmentLetter(ReportAppointmentLetterModel objAppointmentLetterModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
                           "REF, " +
                           "EMPLOYEE_ID, " +
                           "EMPLOYEE_NAME, " +
                           "FM_NAME, " +
                           "PRESENT_ADDRESS, " +
                           "SUBJECT, " +
                           "DEAR, " +
                           "WITH_REFERENCE, " +
                           "TERMS_CONDITIONS, " +
                           "NO_ONE, " +
                           "NO_TWO, " +
                           "BASIC_SALARY, " +
                           "HOUSE_RENT, " +
                           "MEDICAL_ALLOWANCE, " +
                           "TRANSPORT_ALLOWANCE, " +
                           "FOOD_ALLOWANCE, " +
                           "GROSS_SALARY, " +
                           "NO_THREE, " +
                           "NO_FOUR, " +
                           "NO_FIVE, " +
                           "NO_SIX, " +
                           "NO_SEVEN, " +
                           "NO_EIGHT, " +
                           "ON_COMPLETION, " +
                           "TERMINATION_OF_SERVICE, " +
                           "SINCERELY_YOURS, " +
                           "HR_NAME, " +
                           "HR_DESIGNATION, " +
                           "BRANCH_OFFICE_NAME, " +
                           "LAST_PART, " +
                           "to_date('" + objAppointmentLetterModel.Date + "', 'dd/mm/yyyy')PRINTING_DATE, " +
                           "TS_ONE, " +
                           "TS_TWO, " +
                           "TS_THREE, " +
                           "TS_FOUR, " +
                           "IF_THE_ABOVE_TERMS, " +
                           "WE_CONGRATULATE, " +
                           "DEPARTMENT_NAME " +
                           "from vew_rpt_appointment_letter  where head_office_id = '" + objAppointmentLetterModel.HeadOfficeId + "' AND branch_office_id = '" + objAppointmentLetterModel.BranchOfficeId + "' and employee_id = '" + objAppointmentLetterModel.EmployeeId + "' ";

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "vew_rpt_appointment_letter");
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

        public string SaveJobDescription(ReportAppointmentLetterModel objAppointmentLetterModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_employee_job_description");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objAppointmentLetterModel.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAppointmentLetterModel.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objAppointmentLetterModel.JobDescription != "")
            {
                objOracleCommand.Parameters.Add("p_job_description", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAppointmentLetterModel.JobDescription;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_job_description", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }




            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAppointmentLetterModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAppointmentLetterModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAppointmentLetterModel.BranchOfficeId;

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



        //CONFIRMATION LETTER - NABID - 15 DEC 18

        public DataSet ConfirmationLetter(ReportConfirmationLetterModel objConfirmationLetterModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +

                       "REF, " +
                       "EMPLOYEE_ID, " +
                       "EMPLOYEE_NAME, " +
                       "DESIGNATION_NAME, " +
                       "BRANCH_OFFICE_NAME, " +
                       "SUBJECT, " +
                       "DEAR, " +
                       "CONGRATULATION, " +
                       "'In view of satisfactory performance, you have been confirmed the position of ' ||  (SELECT designation_name from l_designation WHERE designation_id = j.present_designation_id) ||  'at Snowtex with effect from, ' || to_date('" + objConfirmationLetterModel.Date + "', 'dd/mm/yyyy')|| '.' in_view_of_satisfiction, " +
                       "CONGRATULATIONS," +
                       "HR_NAME," +
                       "HR_DESIGNATION, " +
                       "DEPARTMENT_NAME, " +
                       "HEAD_OFFICE_ID, " +
                       "BRANCH_OFFICE_ID, " +
                       "to_date('" + objConfirmationLetterModel.Date + "', 'dd/mm/yyyy') printing_date, " +
                       "new_emp_id, " +
                       "hr_department_name " +
                       "from vew_rpt_confirmation_letter j  where head_office_id = '" + objConfirmationLetterModel.HeadOfficeId + "' AND branch_office_id = '" + objConfirmationLetterModel.BranchOfficeId + "' and employee_id = '" + objConfirmationLetterModel.EmployeeId + "' ";

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();
                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "vew_rpt_confirmation_letter");
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


        //RELEASE LETTER - NABID - 15 DEC 18
        public DataSet DisplayResignLetter(ReportReleaseLetterModel objReleaseLetterModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
                           "REF, " +
                           "EMPLOYEE_ID, " +
                           "EMPLOYEE_NAME, " +
                           "PRESENT_DESIGNATION_ID, " +
                           "DESIGNATION_NAME," +
                           "BRANCH_OFFICE_NAME, " +
                           "TO_WHOM, " +
                           "BODY_OF_RESIGN, " +
                           "WE_WISH, " +
                           "HR_NAME, " +
                           "HR_DESIGNATION, " +
                           "DEPARTMENT_NAME," +
                           "HEAD_OFFICE_ID, " +
                           "BRANCH_OFFICE_ID, " +
                           "to_date('" + objReleaseLetterModel.Date + "', 'dd/mm/yyyy')PRINTING_DATE " +
                           "from vew_rpt_resign_leter  where head_office_id = '" + objReleaseLetterModel.HeadOfficeId + "' AND branch_office_id = '" + objReleaseLetterModel.BranchOfficeId + "' and employee_id = '" + objReleaseLetterModel.EmployeeId + "' ";




                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "vew_rpt_resign_leter");
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
        #region Fabric Module

        public DataSet GetFabricRequisitionById(FabricRequisitionModel objFabricRequisitionModel)
        {
            string vQuery = "SELECT * FROM VEW_RPT_FABRIC_REQUISITION WHERE HEAD_OFFICE_ID = '" + objFabricRequisitionModel.HeadOfficeId + "' and BRANCH_OFFICE_ID = '" + objFabricRequisitionModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objFabricRequisitionModel.FabricRequisitionId))
            {
                vQuery += " and FABRIC_REQUISITION_ID = '" + objFabricRequisitionModel.FabricRequisitionId + "'";
            }

            using (OracleConnection objConnection = GetConnection())
            {
                objConnection.Open();
                OracleDataAdapter objDataAdapter = new OracleDataAdapter(vQuery, objConnection);

                try
                {
                    DataSet objDataSet = new DataSet();
                    objDataAdapter.Fill(objDataSet, "VEW_RPT_FABRIC_REQUISITION");
                    return objDataSet;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objDataAdapter.Dispose();
                    objConnection.Close();
                }
            }
        }

        public DataSet GetFabricRequisitionPurchaseById(FabricRequisitionModel objFabricRequisitionModel)
        {
            string vQuery = "SELECT * FROM VEW_RPT_FABRIC_REQUIS_PURCHASE WHERE FP_HEAD_OFFICE_ID = '" + objFabricRequisitionModel.HeadOfficeId + "' and FP_BRANCH_OFFICE_ID = '" + objFabricRequisitionModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objFabricRequisitionModel.FabricRequisitionId))
            {
                vQuery += " and FR_FABRIC_REQUISITION_ID = '" + objFabricRequisitionModel.FabricRequisitionId + "'";
            }

            using (OracleConnection objConnection = GetConnection())
            {
                objConnection.Open();
                OracleDataAdapter objDataAdapter = new OracleDataAdapter(vQuery, objConnection);

                try
                {
                    DataSet objDataSet = new DataSet();
                    objDataAdapter.Fill(objDataSet, "VEW_RPT_FABRIC_REQUIS_PURCHASE");
                    return objDataSet;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objDataAdapter.Dispose();
                    objConnection.Close();
                }
            }
        }

        #endregion
        
        #region"Merchandising Report"

        public DataSet GetPOData(PurchaseOrderModel objPurchaseOrderModel)
        {
            DataSet dataSet;

            try
            {
               

                    string sql = "SELECT " +
                                   "RPT_TITLE, " +
                                   "TRAN_ID, " +
                                   "INVOICE_NO, " +
                                   "ORDER_CREATION_DATE, " +
                                   "ORDER_NO, " +
                                   "HAND_OVER_DATE, " +
                                   "ORDER_TYPE_ID, " +
                                   "ORDER_TYPE_NAME, " +
                                   "MODEL_NO, " +
                                   "ITEM_DESCRIPTION, " +
                                   "ITEM_CODE, " +
                                   "SIZE_ID, " +
                                   "SIZE_NAME, " +
                                   "PCB_VALUE, " +
                                   "UE_VALUE, " +
                                   "PACKAGING_VALUE, " +
                                   "STYLE_NO, " +
                                   "ORDER_QUANTITY, " +
                                   "SHIP_QUANTITY, " +
                                   "REMAIN_QUANTITY, " +
                                   "UNIT_PRICE, " +
                                   "TOTAL_PRICE, " +
                                   "PORT_OF_DISTINATION, " +
                                   "DELIVERY_DATE, " +
                                   "MODE_OF_SHIPMENT_ID, " +
                                   "MODE_OF_SHIPMENT_NAME, " +
                                   "PORT_OF_LOADING_ID, " +
                                   "PORT_OF_LOADING_NAME, " +
                                   "CURRENCY_ID, " +
                                   "CURRENCY_NAME, " +
                                   "UPDATE_BY, " +
                                   "UPDATE_DATE, " +
                                   "HEAD_OFFICE_ID, " +
                                   "HEAD_OFFICE_NAME, " +
                                   "BRANCH_OFFICE_ID, " +
                                   "BRANCH_OFFICE_NAME, " +
                                   "BRANCH_OFFICE_ADDRESS, " +
                                   "PRICE_IN_WORD_IS, " +
                                   "PRICE_IN_WORD_INV " +


                                " FROM VEW_RPT_PURCHASE_ORDER_SUB WHERE HEAD_OFFICE_ID = '" + objPurchaseOrderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objPurchaseOrderModel.BranchOfficeId + "' ";

                    if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptInvoiceNumber))
                    {
                        sql += " AND INVOICE_NO = '" + objPurchaseOrderModel.rptInvoiceNumber + "' ";
                    }
                    if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptStyleNo))
                    {
                        sql += " AND STYLE_NO = '" + objPurchaseOrderModel.rptStyleNo + "' ";
                    }
                    if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptOrderNumber))
                    {
                        sql += " AND ORDER_NO = '" + objPurchaseOrderModel.rptOrderNumber + "' ";
                    }
                    if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptModelNo))
                    {
                        sql += " AND MODEL_NO = '" + objPurchaseOrderModel.rptModelNo + "' ";
                    }
                    if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.FromDate) && !string.IsNullOrWhiteSpace(objPurchaseOrderModel.Todate))
                    {
                        if (objPurchaseOrderModel.FromDate.Length > 6 && objPurchaseOrderModel.Todate.Length > 6)
                        {
                            sql = sql + "and HAND_OVER_DATE between to_date( '" + objPurchaseOrderModel.FromDate + "', 'dd/mm/yyyy') and to_date( '" + objPurchaseOrderModel.Todate + "', 'dd/mm/yyyy')  ";

                        }

                    }

                





                using (OracleConnection objConnection = GetConnection())
                {
                    OracleCommand objCommand = new OracleCommand(sql, objConnection);
                    objConnection.Open();
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);

                    try
                    {
                        dataSet = new DataSet();
                        objDataAdapter.Fill(dataSet, "VEW_RPT_PURCHASE_ORDER_SUB");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        objDataAdapter.Dispose();
                        objCommand.Dispose();
                        objConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }

            return dataSet;
        }

        public DataSet GetPOSumData(PurchaseOrderModel objPurchaseOrderModel)
        {
            DataSet dataSet;

            try
            {
                string sql = "SELECT " +
                             "INVOICE_NO, " +
                             "ORDER_CREATION_DATE, " +
                             "ORDER_NO, " +
                             "HAND_OVER_DATE, " +
                             "ORDER_TYPE_ID, " +
                             "MODEL_NO, " +
                             "STYLE_NO, " +
                             "ORDER_QUANTITY, " +
                             "SHIP_QUANTITY, " +
                             "REMAIN_QUANTITY, " +
                             "UNIT_PRICE, " +
                             "TOTAL_PRICE, " +
                             "PORT_OF_DISTINATION, " +
                             "DELIVERY_DATE, " +
                             "MODE_OF_SHIPMENT_ID, " +
                             "PORT_OF_LOADING_ID, " +
                             "CURRENCY_ID, " +
                             "CREATE_BY, " +
                             "CREATE_DATE, " +
                             "UPDATE_BY, " +
                             "UPDATE_DATE, " +
                             "HEAD_OFFICE_ID, " +
                             "HEAD_OFFICE_NAME, " +
                             "BRANCH_OFFICE_ID, " +
                             "BRANCH_OFFICE_NAME, " +
                             "BRANCH_OFFICE_ADDRESS, " +
                             "RPT_TITLE, " +
                             "PRICE_IN_WORD " +
                              " FROM VEW_RPT_PO_MERCHAN_SUM WHERE HEAD_OFFICE_ID = '" + objPurchaseOrderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objPurchaseOrderModel.BranchOfficeId + "' ";

                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptStyleNo))
                {
                    sql += " AND STYLE_NO = '" + objPurchaseOrderModel.rptStyleNo + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptOrderNumber))
                {
                    sql += " AND ORDER_NO = '" + objPurchaseOrderModel.rptOrderNumber + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptModelNo))
                {
                    sql += " AND MODEL_NO = '" + objPurchaseOrderModel.rptModelNo + "' ";
                }

                using (OracleConnection objConnection = GetConnection())
                {
                    OracleCommand objCommand = new OracleCommand(sql, objConnection);
                    objConnection.Open();
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);

                    try
                    {
                        dataSet = new DataSet();
                        objDataAdapter.Fill(dataSet, "VEW_RPT_PURCHASE_ORDER_SUB");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        objDataAdapter.Dispose();
                        objCommand.Dispose();
                        objConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }

            return dataSet;
        }

        public DataSet GetPODataDeleteHistory(PurchaseOrderModel objPurchaseOrderModel)
        {
            DataSet dataSet;

            try
            {
                string sql = "SELECT " +
                                   "RPT_TITLE, " +
                                   "TRAN_ID, " +
                                   "INVOICE_NO, " +
                                   "ORDER_CREATION_DATE, " +
                                   "ORDER_NO, " +
                                   "HAND_OVER_DATE, " +
                                   "ORDER_TYPE_ID, " +
                                   "ORDER_TYPE_NAME, " +
                                   "MODEL_NO, " +
                                   "ITEM_DESCRIPTION, " +
                                   "ITEM_CODE, " +
                                   "SIZE_ID, " +
                                   "SIZE_NAME, " +
                                   "PCB_VALUE, " +
                                   "UE_VALUE, " +
                                   "PACKAGING_VALUE, " +
                                   "STYLE_NO, " +
                                   "ORDER_QUANTITY, " +
                                   "SHIP_QUANTITY, " +
                                   "REMAIN_QUANTITY, " +
                                   "UNIT_PRICE, " +
                                   "TOTAL_PRICE, " +
                                   "PORT_OF_DISTINATION, " +
                                   "DELIVERY_DATE, " +
                                   "MODE_OF_SHIPMENT_ID, " +
                                   "MODE_OF_SHIPMENT_NAME, " +
                                   "PORT_OF_LOADING_ID, " +
                                   "PORT_OF_LOADING_NAME, " +
                                   "CURRENCY_ID, " +
                                   "CURRENCY_NAME, " +
                                   "UPDATE_BY, " +
                                   "UPDATE_DATE, " +
                                   "HEAD_OFFICE_ID, " +
                                   "HEAD_OFFICE_NAME, " +
                                   "BRANCH_OFFICE_ID, " +
                                   "BRANCH_OFFICE_NAME, " +
                                   "BRANCH_OFFICE_ADDRESS, " +
                                   "PRICE_IN_WORD_IS, " +
                                   "PRICE_IN_WORD_INV " +


                                " FROM VEW_RPT_PURCHASE_ORDER_DELETE WHERE HEAD_OFFICE_ID = '" + objPurchaseOrderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objPurchaseOrderModel.BranchOfficeId + "' ";

                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptInvoiceNumber))
                {
                    sql += " AND INVOICE_NO = '" + objPurchaseOrderModel.rptInvoiceNumber + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptStyleNo))
                {
                    sql += " AND STYLE_NO = '" + objPurchaseOrderModel.rptStyleNo + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptOrderNumber))
                {
                    sql += " AND ORDER_NO = '" + objPurchaseOrderModel.rptOrderNumber + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptModelNo))
                {
                    sql += " AND MODEL_NO = '" + objPurchaseOrderModel.rptModelNo + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.FromDate) && !string.IsNullOrWhiteSpace(objPurchaseOrderModel.Todate))
                {
                    if (objPurchaseOrderModel.FromDate.Length > 6 && objPurchaseOrderModel.Todate.Length > 6)
                    {
                        sql = sql + "and HAND_OVER_DATE between to_date( '" + objPurchaseOrderModel.FromDate + "', 'dd/mm/yyyy') and to_date( '" + objPurchaseOrderModel.Todate + "', 'dd/mm/yyyy')  ";

                    }

                }

                using (OracleConnection objConnection = GetConnection())
                {
                    OracleCommand objCommand = new OracleCommand(sql, objConnection);
                    objConnection.Open();
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);

                    try
                    {
                        dataSet = new DataSet();
                        objDataAdapter.Fill(dataSet, "VEW_RPT_PURCHASE_ORDER_SUB");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        objDataAdapter.Dispose();
                        objCommand.Dispose();
                        objConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }

            return dataSet;
        }


        public DataSet GetPODataRejectHistory(PurchaseOrderModel objPurchaseOrderModel)
        {
            DataSet dataSet;

            try
            {
                string sql = "SELECT " +
                                   "RPT_TITLE, " +
                                   "TRAN_ID, " +
                                   "INVOICE_NO, " +
                                   "ORDER_CREATION_DATE, " +
                                   "ORDER_NO, " +
                                   "HAND_OVER_DATE, " +
                                   "ORDER_TYPE_ID, " +
                                   "ORDER_TYPE_NAME, " +
                                   "MODEL_NO, " +
                                   "ITEM_DESCRIPTION, " +
                                   "ITEM_CODE, " +
                                   "SIZE_ID, " +
                                   "SIZE_NAME, " +
                                   "PCB_VALUE, " +
                                   "UE_VALUE, " +
                                   "PACKAGING_VALUE, " +
                                   "STYLE_NO, " +
                                   "ORDER_QUANTITY, " +
                                   "SHIP_QUANTITY, " +
                                   "REMAIN_QUANTITY, " +
                                   "UNIT_PRICE, " +
                                   "TOTAL_PRICE, " +
                                   "PORT_OF_DISTINATION, " +
                                   "DELIVERY_DATE, " +
                                   "MODE_OF_SHIPMENT_ID, " +
                                   "MODE_OF_SHIPMENT_NAME, " +
                                   "PORT_OF_LOADING_ID, " +
                                   "PORT_OF_LOADING_NAME, " +
                                   "CURRENCY_ID, " +
                                   "CURRENCY_NAME, " +
                                   "UPDATE_BY, " +
                                   "UPDATE_DATE, " +
                                   "HEAD_OFFICE_ID, " +
                                   "HEAD_OFFICE_NAME, " +
                                   "BRANCH_OFFICE_ID, " +
                                   "BRANCH_OFFICE_NAME, " +
                                   "BRANCH_OFFICE_ADDRESS, " +
                                   "PRICE_IN_WORD_IS, " +
                                   "PRICE_IN_WORD_INV " +


                                " FROM VEW_RPT_PURCHASE_ORDER_REJECT WHERE HEAD_OFFICE_ID = '" + objPurchaseOrderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objPurchaseOrderModel.BranchOfficeId + "' ";

                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptInvoiceNumber))
                {
                    sql += " AND INVOICE_NO = '" + objPurchaseOrderModel.rptInvoiceNumber + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptStyleNo))
                {
                    sql += " AND STYLE_NO = '" + objPurchaseOrderModel.rptStyleNo + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptOrderNumber))
                {
                    sql += " AND ORDER_NO = '" + objPurchaseOrderModel.rptOrderNumber + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.rptModelNo))
                {
                    sql += " AND MODEL_NO = '" + objPurchaseOrderModel.rptModelNo + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.FromDate) && !string.IsNullOrWhiteSpace(objPurchaseOrderModel.Todate))
                {
                    if (objPurchaseOrderModel.FromDate.Length > 6 && objPurchaseOrderModel.Todate.Length > 6)
                    {
                        sql = sql + "and HAND_OVER_DATE between to_date( '" + objPurchaseOrderModel.FromDate + "', 'dd/mm/yyyy') and to_date( '" + objPurchaseOrderModel.Todate + "', 'dd/mm/yyyy')  ";

                    }

                }

                using (OracleConnection objConnection = GetConnection())
                {
                    OracleCommand objCommand = new OracleCommand(sql, objConnection);
                    objConnection.Open();
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);

                    try
                    {
                        dataSet = new DataSet();
                        objDataAdapter.Fill(dataSet, "VEW_RPT_PURCHASE_ORDER_SUB");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        objDataAdapter.Dispose();
                        objCommand.Dispose();
                        objConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }

            return dataSet;
        }


        public DataSet GetSelectionData(SelectionEntryModel objSelectionEntryModel)
        {
            DataSet dataSet;

            try
            {
                string sql = "SELECT " +
                                   "RPT_TITLE, " +
                                   "TRAN_ID, " +
                                   "STYLE_NO, " +
                                   "MODEL_NO, " +
                                   "TOTAL_RECEIVED, " +
                                   "COUNTRY_ID, " +
                                   "COUNTRY_NAME, " +
                                   "EUROPE_SS_QUANTITY, " +
                                   "EUROPE_AW_QUANTITY, " +
                                   "COUNTRY_ORDER_QUANTITY, " +
                                   "ELASTICITY, " +
                                   "SUPPLY, " +
                                   "COUNTRY_RECEIVED_QUANTITY, " +
                                   "HEAD_OFFICE_ID, " +
                                   "HEAD_OFFICE_NAME, " +
                                   "BRANCH_OFFICE_ID, " +
                                   "BRANCH_OFFICE_NAME, " +
                                   "BRANCH_OFFICE_ADDRESS " +

                                " FROM VEW_RPT_SELECTION_SUB WHERE HEAD_OFFICE_ID = '" + objSelectionEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objSelectionEntryModel.BranchOfficeId + "' ";

                if (!string.IsNullOrWhiteSpace(objSelectionEntryModel.rptStyleNo))
                {
                    sql += " AND STYLE_NO = '" + objSelectionEntryModel.rptStyleNo + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objSelectionEntryModel.rptModelNo))
                {
                    sql += " AND MODEL_NO = '" + objSelectionEntryModel.rptModelNo + "' ";
                }
                if (!string.IsNullOrWhiteSpace(objSelectionEntryModel.rptCountryId))
                {
                    sql += " AND COUNTRY_ID = '" + objSelectionEntryModel.rptCountryId + "' ";
                }

                using (OracleConnection objConnection = GetConnection())
                {
                    OracleCommand objCommand = new OracleCommand(sql, objConnection);
                    objConnection.Open();
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);

                    try
                    {
                        dataSet = new DataSet();
                        objDataAdapter.Fill(dataSet, "VEW_RPT_SELECTION_SUB");
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        objDataAdapter.Dispose();
                        objCommand.Dispose();
                        objConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error : " + ex.Message);
            }

            return dataSet;
        }




        #endregion


       
        #region Salary Report

        public DataSet CoMonthlyWorkingDayList(SalaryReportModel objReportSalary)
        {
            try
            {
                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                          "RPT_TITLE, " +
                          "EMPLOYEE_ID, " +
                          "EMPLOYEE_NAME, " +
                          "DESIGNATION_NAME, " +
                          "JOINING_DATE, " +
                          "CARD_NO, " +
                          "SALARY_YEAR, " +
                          "MONTH_ID, " +
                          "MONTH_NAME, " +
                          "DEPARTMENT_ID, " +
                          "DEPARTMENT_NAME, " +
                          "UNIT_ID, " +
                          "UNIT_NAME, " +
                          "SECTION_ID, " +
                          "SECTION_NAME, " +
                          "SUB_SECTION_ID, " +
                          "SUB_SECTION_NAME, " +
                          "WORKING_DAY, " +
                          "ADVANCE_AMOUNT, " +
                          "LEAVE_ALLOWANCE, " +
                          "TAX_DEDUCATION_AMOUNT, " +
                          "PF_ADVANCE_AMOUNT, " +
                          "FOOD_DEDUCTION_AMOUNT, " +
                          "FOOD_ALLOWANCE_AMOUNT, " +
                          "CREATE_BY, " +
                          "CREATE_DATE, " +
                          "UPDATE_BY, " +
                          "UPDATE_DATE, " +
                          "HEAD_OFFICE_ID, " +
                          "HEAD_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ID, " +
                          "BRANCH_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ADDRESS, " +
                          "GROSS_SALARY, " +
                          "MONTH_DAY, " +
                          "ARREAR_AMOUNT " +

                          " from vew_rpt_working_day_list_co  where head_office_id = '" + objReportSalary.HeadOfficeId + "' AND branch_office_id = '" +
                          objReportSalary.BranchOfficeId + "' and salary_year ='" + objReportSalary.SalaryYear + "' and month_id = '" + objReportSalary.MonthId + "'  ";


                    if (objReportSalary.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objReportSalary.EmployeeId + "' ";

                    }
                    if (objReportSalary.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objReportSalary.DepartmentId + "' ";

                    }

                    if (objReportSalary.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objReportSalary.UnitId + "' ";

                    }

                    if (objReportSalary.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objReportSalary.SectionId + "' ";

                    }

                    if (objReportSalary.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objReportSalary.SubSectionId + "' ";

                    }

                    //sql = sql + " order by to_number(card_no)";

                    //sql = sql + " order by to_number(card_no)";


                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "vew_rpt_working_day_list_co");
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

        public DataSet CoMonthlySalarySheet(SalaryReportModel objReportSalary)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                          "SL,  " +
                          "RPT_TITLE,  " +
                          "EMPLOYEE_ID,  " +
                          "EMPLOYEE_NAME,  " +
                          "DESIGNATION_NAME,  " +
                          "JOINING_DATE,  " +
                          "SALARY_YEAR,  " +
                          "MONTH_ID,  " +
                          "MONTH_NAME,  " +
                          "GRADE_ID,  " +
                          "UNIT_ID,  " +
                          "UNIT_NAME,  " +
                          "DEPARTMENT_ID,  " +
                          "DEPARTMENT_NAME,  " +
                          "SECTION_ID,  " +
                          "SECTION_NAME,  " +
                          "SUB_SECTION_ID,  " +
                          "SUB_SECTION_NAME,  " +
                          "WORKING_DAY,  " +
                          "ABSENT_DAY,  " +
                          "ABSENT_HOUR,  " +
                          "TOTAL_LEAVE,  " +
                          "GROSS_SALARY,  " +
                          "BASIC_SALARY,  " +
                          "HOUSE_RENT,  " +
                          "CONVEYANCE_ALLOWANCE,  " +
                          "FOOD_ALLOWANCE,  " +
                          "MEDICAL_ALLOWANCE,  " +
                          "LEAVE_ALLOWANCE,  " +
                          "ATTENDANCE_BONUS_AMOUNT,  " +
                          "OT_HOUR,  " +
                          "OT_RATE,  " +
                          "OT_AMOUNT,  " +
                          "ABSENT_DEDUCT_AMOUNT,  " +
                          "TAX_DEDUCATION_AMOUNT,  " +
                          "ADVANCE_AMOUNT,  " +

                          "PF_ADVANCE_AMOUNT,  " +
                          "PF_DEDUCTION_AMOUNT,  " +
                          "FOOD_DEDUCTION_AMOUNT,  " +
                          "STAMP_FEE,  " +
                          "TOTAL_PAY_AMOUNT,  " +
                          "TOTAL_AMOUNT,  " +
                          "NET_PAYMENT_AMOUNT,  " +
                          "CREATE_BY,  " +
                          "CREATE_DATE,  " +
                          "UPDATE_BY,  " +
                          "UPDATE_DATE,  " +
                          "HEAD_OFFICE_ID,  " +
                          "HEAD_OFFICE_NAME,  " +
                          "BRANCH_OFFICE_ID,  " +
                          "BRANCH_OFFICE_NAME,  " +
                          "BRANCH_OFFICE_ADDRESS,  " +
                          "NET_PAYMENT_AMOUNT_IN_WORD,  " +
                          "PAYMENT_AMOUNT_IN_WORD_TOTAL " +

                          " from VEW_RPT_MONTHLY_SALARY  where head_office_id = '" + objReportSalary.HeadOfficeId + "' AND branch_office_id = '" + objReportSalary.BranchOfficeId + "' and salary_year ='" + objReportSalary.SalaryYear + "' and month_id = '" + objReportSalary.MonthId + "'  ";


                    if (objReportSalary.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objReportSalary.EmployeeId + "' ";

                    }
                    if (objReportSalary.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objReportSalary.DepartmentId + "' ";

                    }

                    if (objReportSalary.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objReportSalary.UnitId + "' ";

                    }

                    if (objReportSalary.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objReportSalary.SectionId + "' ";

                    }

                    if (objReportSalary.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objReportSalary.SubSectionId + "' ";

                    }
                    //sql = sql + " order by to_number(card_no)";

                    //sql = sql + " order by to_number(card_no)";

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_MONTHLY_SALARY");
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

        public DataSet CoMonthlySalarySlip(SalaryReportModel objReportSalary)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                          "SL,  " +
                          "RPT_TITLE,  " +
                          "EMPLOYEE_ID,  " +
                          "EMPLOYEE_NAME,  " +
                          "DESIGNATION_NAME,  " +
                          "JOINING_DATE,  " +
                          "SALARY_YEAR,  " +
                          "MONTH_ID,  " +
                          "MONTH_NAME,  " +
                          "GRADE_ID,  " +
                          "UNIT_ID,  " +
                          "UNIT_NAME,  " +
                          "DEPARTMENT_ID,  " +
                          "DEPARTMENT_NAME,  " +
                          "SECTION_ID,  " +
                          "SECTION_NAME,  " +
                          "SUB_SECTION_ID,  " +
                          "SUB_SECTION_NAME,  " +
                          "WORKING_DAY,  " +
                          "ABSENT_DAY,  " +
                          "ABSENT_HOUR,  " +
                          "TOTAL_LEAVE,  " +
                          "GROSS_SALARY,  " +
                          "BASIC_SALARY,  " +
                          "HOUSE_RENT,  " +
                          "CONVEYANCE_ALLOWANCE,  " +
                          "FOOD_ALLOWANCE,  " +
                          "MEDICAL_ALLOWANCE,  " +
                          "LEAVE_ALLOWANCE,  " +
                          "ATTENDANCE_BONUS_AMOUNT,  " +
                          "OT_HOUR,  " +
                          "OT_RATE,  " +
                          "OT_AMOUNT,  " +
                          "ABSENT_DEDUCT_AMOUNT,  " +
                          "TAX_DEDUCATION_AMOUNT,  " +
                          "ADVANCE_AMOUNT,  " +

                          "PF_ADVANCE_AMOUNT,  " +
                          "PF_DEDUCTION_AMOUNT,  " +
                          "FOOD_DEDUCTION_AMOUNT,  " +
                          "STAMP_FEE,  " +
                          "TOTAL_PAY_AMOUNT,  " +
                          "TOTAL_AMOUNT,  " +
                          "NET_PAYMENT_AMOUNT,  " +
                          "CREATE_BY,  " +
                          "CREATE_DATE,  " +
                          "UPDATE_BY,  " +
                          "UPDATE_DATE,  " +
                          "HEAD_OFFICE_ID,  " +
                          "HEAD_OFFICE_NAME,  " +
                          "BRANCH_OFFICE_ID,  " +
                          "BRANCH_OFFICE_NAME,  " +
                          "BRANCH_OFFICE_ADDRESS,  " +
                          "NET_PAYMENT_AMOUNT_IN_WORD,  " +
                          "PAYMENT_AMOUNT_IN_WORD_TOTAL, " +
                          "bank_yn, " +
                          "bank_status, " +
                          "account_no, " +
                          "month_day " +



                          " from VEW_RPT_MONTHLY_PAY_SLIP  where head_office_id = '" + objReportSalary.HeadOfficeId + "' AND branch_office_id = '" + objReportSalary.BranchOfficeId + "' and salary_year ='" + objReportSalary.SalaryYear + "' and month_id = '" + objReportSalary.MonthId + "'  ";


                    if (objReportSalary.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objReportSalary.EmployeeId + "' ";

                    }
                    if (objReportSalary.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objReportSalary.DepartmentId + "' ";

                    }

                    if (objReportSalary.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objReportSalary.UnitId + "' ";

                    }

                    if (objReportSalary.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objReportSalary.SectionId + "' ";

                    }

                    if (objReportSalary.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objReportSalary.SubSectionId + "' ";

                    }
                    //sql = sql + " order by to_number(card_no)";
                    //sql = sql + " order by to_number(card_no)";


                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_MONTHLY_PAY_SLIP");
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

        public DataSet CoMonthlyBankSalarySheet(SalaryReportModel objReportSalary)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                          "SL,  " +
                          "RPT_TITLE,  " +
                          "EMPLOYEE_ID,  " +
                          "EMPLOYEE_NAME,  " +
                          "DESIGNATION_NAME,  " +
                          "JOINING_DATE,  " +
                          "SALARY_YEAR,  " +
                          "MONTH_ID,  " +
                          "MONTH_NAME,  " +
                          "GRADE_ID,  " +
                          "UNIT_ID,  " +
                          "UNIT_NAME,  " +
                          "DEPARTMENT_ID,  " +
                          "DEPARTMENT_NAME,  " +
                          "SECTION_ID,  " +
                          "SECTION_NAME,  " +
                          "SUB_SECTION_ID,  " +
                          "SUB_SECTION_NAME,  " +
                          "WORKING_DAY,  " +
                          "ABSENT_DAY,  " +
                          "ABSENT_HOUR,  " +
                          "TOTAL_LEAVE,  " +
                          "GROSS_SALARY,  " +
                          "BASIC_SALARY,  " +
                          "HOUSE_RENT,  " +
                          "CONVEYANCE_ALLOWANCE,  " +
                          "FOOD_ALLOWANCE,  " +
                          "MEDICAL_ALLOWANCE,  " +
                          "LEAVE_ALLOWANCE,  " +
                          "ATTENDANCE_BONUS_AMOUNT,  " +
                          "OT_HOUR,  " +
                          "OT_RATE,  " +
                          "OT_AMOUNT,  " +
                          "ABSENT_DEDUCT_AMOUNT,  " +
                          "TAX_DEDUCATION_AMOUNT,  " +
                          "ADVANCE_AMOUNT,  " +

                          "PF_ADVANCE_AMOUNT,  " +
                          "PF_DEDUCTION_AMOUNT,  " +
                          "FOOD_DEDUCTION_AMOUNT,  " +
                          "STAMP_FEE,  " +
                          "TOTAL_PAY_AMOUNT,  " +
                          "TOTAL_AMOUNT,  " +
                          "NET_PAYMENT_AMOUNT,  " +
                          "CREATE_BY,  " +
                          "CREATE_DATE,  " +
                          "UPDATE_BY,  " +
                          "UPDATE_DATE,  " +
                          "HEAD_OFFICE_ID,  " +
                          "HEAD_OFFICE_NAME,  " +
                          "BRANCH_OFFICE_ID,  " +
                          "BRANCH_OFFICE_NAME,  " +
                          "BRANCH_OFFICE_ADDRESS,  " +
                          "NET_PAYMENT_AMOUNT_IN_WORD,  " +
                          "PAYMENT_AMOUNT_IN_WORD_TOTAL, " +
                          "bank_yn, " +
                          "bank_status, " +
                          "account_no " +

                          " from VEW_RPT_MONTHLY_BANK_SALARY  where head_office_id = '" + objReportSalary.HeadOfficeId + "' AND branch_office_id = '" + objReportSalary.BranchOfficeId + "' and salary_year ='" + objReportSalary.SalaryYear + "' and month_id = '" + objReportSalary.MonthId + "'  ";


                    if (objReportSalary.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objReportSalary.EmployeeId + "' ";

                    }
                    if (objReportSalary.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objReportSalary.DepartmentId + "' ";

                    }

                    if (objReportSalary.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objReportSalary.UnitId + "' ";

                    }

                    if (objReportSalary.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objReportSalary.SectionId + "' ";

                    }

                    if (objReportSalary.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objReportSalary.SubSectionId + "' ";

                    }

                    //sql = sql + " order by to_number(card_no)";


                    //sql = sql + " order by to_number(card_no)";


                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_MONTHLY_BANK_SALARY");
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

        public DataSet CoMonthlyCashSalarySheet(SalaryReportModel objReportSalary)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                          "SL,  " +
                          "RPT_TITLE,  " +
                          "EMPLOYEE_ID,  " +
                          "EMPLOYEE_NAME,  " +
                          "DESIGNATION_NAME,  " +
                          "JOINING_DATE,  " +
                          "SALARY_YEAR,  " +
                          "MONTH_ID,  " +
                          "MONTH_NAME,  " +
                          "GRADE_ID,  " +
                          "UNIT_ID,  " +
                          "UNIT_NAME,  " +
                          "DEPARTMENT_ID,  " +
                          "DEPARTMENT_NAME,  " +
                          "SECTION_ID,  " +
                          "SECTION_NAME,  " +
                          "SUB_SECTION_ID,  " +
                          "SUB_SECTION_NAME,  " +
                          "WORKING_DAY,  " +
                          "ABSENT_DAY,  " +
                          "ABSENT_HOUR,  " +
                          "TOTAL_LEAVE,  " +
                          "GROSS_SALARY,  " +
                          "BASIC_SALARY,  " +
                          "HOUSE_RENT,  " +
                          "CONVEYANCE_ALLOWANCE,  " +
                          "FOOD_ALLOWANCE,  " +
                          "MEDICAL_ALLOWANCE,  " +
                          "LEAVE_ALLOWANCE,  " +
                          "ATTENDANCE_BONUS_AMOUNT,  " +
                          "OT_HOUR,  " +
                          "OT_RATE,  " +
                          "OT_AMOUNT,  " +
                          "ABSENT_DEDUCT_AMOUNT,  " +
                          "TAX_DEDUCATION_AMOUNT,  " +
                          "ADVANCE_AMOUNT,  " +

                          "PF_ADVANCE_AMOUNT,  " +
                          "PF_DEDUCTION_AMOUNT,  " +
                          "FOOD_DEDUCTION_AMOUNT,  " +
                          "STAMP_FEE,  " +
                          "TOTAL_PAY_AMOUNT,  " +
                          "TOTAL_AMOUNT,  " +
                          "NET_PAYMENT_AMOUNT,  " +
                          "CREATE_BY,  " +
                          "CREATE_DATE,  " +
                          "UPDATE_BY,  " +
                          "UPDATE_DATE,  " +
                          "HEAD_OFFICE_ID,  " +
                          "HEAD_OFFICE_NAME,  " +
                          "BRANCH_OFFICE_ID,  " +
                          "BRANCH_OFFICE_NAME,  " +
                          "BRANCH_OFFICE_ADDRESS,  " +
                          "NET_PAYMENT_AMOUNT_IN_WORD,  " +
                          "PAYMENT_AMOUNT_IN_WORD_TOTAL, " +
                          "bank_yn, " +
                          "bank_status, " +
                          "account_no " +




                          " from VEW_RPT_MONTHLY_CASH_SALARY  where head_office_id = '" + objReportSalary.HeadOfficeId + "' AND branch_office_id = '" + objReportSalary.BranchOfficeId + "' and salary_year ='" + objReportSalary.SalaryYear + "' and month_id = '" + objReportSalary.MonthId + "'  ";


                    if (objReportSalary.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objReportSalary.EmployeeId + "' ";

                    }
                    if (objReportSalary.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objReportSalary.DepartmentId + "' ";

                    }

                    if (objReportSalary.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objReportSalary.UnitId + "' ";

                    }

                    if (objReportSalary.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objReportSalary.SectionId + "' ";

                    }

                    if (objReportSalary.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objReportSalary.SubSectionId + "' ";

                    }
                    //sql = sql + " order by to_number(card_no)";

                    //sql = sql + " order by to_number(card_no)";

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_MONTHLY_CASH_SALARY");
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

        public DataSet HourlyMonthlySalarySheet(SalaryReportModel objReportSalary)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                          "SL,  " +
                          "RPT_TITLE,  " +
                          "EMPLOYEE_ID,  " +
                          "EMPLOYEE_NAME,  " +
                          "DESIGNATION_NAME,  " +
                          "JOINING_DATE,  " +
                          "SALARY_YEAR,  " +
                          "MONTH_ID,  " +
                          "MONTH_NAME,  " +
                          "GRADE_ID,  " +
                          "UNIT_ID,  " +
                          "UNIT_NAME,  " +
                          "DEPARTMENT_ID,  " +
                          "DEPARTMENT_NAME,  " +
                          "SECTION_ID,  " +
                          "SECTION_NAME,  " +
                          "SUB_SECTION_ID,  " +
                          "SUB_SECTION_NAME,  " +
                          "WORKING_DAY,  " +
                          "ABSENT_DAY,  " +
                          "ABSENT_HOUR,  " +
                          "TOTAL_LEAVE,  " +
                          "GROSS_SALARY,  " +
                          "BASIC_SALARY,  " +
                          "HOUSE_RENT,  " +
                          "CONVEYANCE_ALLOWANCE,  " +
                          "FOOD_ALLOWANCE,  " +
                          "MEDICAL_ALLOWANCE,  " +
                          "LEAVE_ALLOWANCE,  " +
                          "ATTENDANCE_BONUS_AMOUNT,  " +
                          "OT_HOUR,  " +
                          "OT_RATE,  " +
                          "OT_AMOUNT,  " +
                          "ABSENT_DEDUCT_AMOUNT,  " +
                          "TAX_DEDUCATION_AMOUNT,  " +
                          "ADVANCE_AMOUNT,  " +

                          "PF_ADVANCE_AMOUNT,  " +
                          "PF_DEDUCTION_AMOUNT,  " +
                          "FOOD_DEDUCTION_AMOUNT,  " +
                          "STAMP_FEE,  " +
                          "TOTAL_PAY_AMOUNT,  " +
                          "TOTAL_AMOUNT,  " +
                          "NET_PAYMENT_AMOUNT,  " +
                          "CREATE_BY,  " +
                          "CREATE_DATE,  " +
                          "UPDATE_BY,  " +
                          "UPDATE_DATE,  " +
                          "HEAD_OFFICE_ID,  " +
                          "HEAD_OFFICE_NAME,  " +
                          "BRANCH_OFFICE_ID,  " +
                          "BRANCH_OFFICE_NAME,  " +
                          "BRANCH_OFFICE_ADDRESS,  " +
                          "NET_PAYMENT_AMOUNT_IN_WORD,  " +
                          "WORKING_HOUR, " +
                          "WORKING_MINUTE, " +
                          "WORKING_SECOND, " +
                          "new_dept_name " +




                          " from vew_rpt_monthly_salary_hourly  where head_office_id = '" + objReportSalary.HeadOfficeId + "' AND branch_office_id = '" + objReportSalary.BranchOfficeId + "' and salary_year ='" + objReportSalary.SalaryYear + "' and month_id = '" + objReportSalary.MonthId + "'  ";


                    if (objReportSalary.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objReportSalary.EmployeeId + "' ";

                    }
                    if (objReportSalary.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objReportSalary.DepartmentId + "' ";

                    }

                    if (objReportSalary.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objReportSalary.UnitId + "' ";

                    }

                    if (objReportSalary.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objReportSalary.SectionId + "' ";

                    }

                    if (objReportSalary.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objReportSalary.SubSectionId + "' ";

                    }
                    //sql = sql + " order by to_number(card_no)";

                    //sql = sql + " order by to_number(card_no)";

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "vew_rpt_monthly_salary_hourly");
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

        public DataSet WorkerSalarySheet(SalaryReportModel objReportSalary)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                          "SL, " +
                          " '' RPT_TITLE, " +
                          "EMPLOYEE_ID, " +
                          "EMPLOYEE_NAME, " +
                          "EMPLOYEE_NAME_BANGLA, " +
                          "CARD_NO, " +
                          "DESIGNATION_NAME, " +
                          "DESIGNATION_NAME_BANGLA, " +
                          "JOINING_DATE, " +
                          "SALARY_YEAR, " +
                          "MONTH_ID, " +
                          "MONTH_NAME, " +
                          "GRADE_ID, " +
                          "GRADE_NO, " +
                          "ABSENT_HOUR, " +
                          "TOTAL_LEAVE, " +
                          "GROSS_SALARY, " +
                          "BASIC_SALARY, " +
                          "HOUSE_RENT, " +
                          "CONVEYANCE_ALLOWANCE, " +
                          "FOOD_ALLOWANCE, " +
                          "MEDICAL_ALLOWANCE, " +
                          "LEAVE_ALLOWANCE, " +
                          "ATTENDANCE_BONUS_AMOUNT, " +
                          "OT_HOUR, " +
                          "OT_RATE, " +
                          "OT_AMOUNT, " +
                          "ABSENT_DEDUCT_AMOUNT, " +
                          "TAX_DEDUCATION_AMOUNT, " +
                          "ADVANCE_AMOUNT, " +
                          "PF_ADVANCE_AMOUNT, " +
                          "PF_DEDUCTION_AMOUNT, " +
                          "PF_ADVANCE_AMOUNT, " +
                          "PF_DEDUCTION_AMOUNT, " +
                          "FOOD_DEDUCTION_AMOUNT," +
                          "STAMP_FEE, " +
                          "TOTAL_PAY_AMOUNT, " +
                          "TOTAL_AMOUNT, " +
                          "NET_PAYMENT_AMOUNT, " +
                          "CREATE_BY, " +
                          "CREATE_DATE, " +
                          "UPDATE_BY, " +
                          "UPDATE_DATE, " +
                          "HEAD_OFFICE_ID, " +
                          "HEAD_OFFICE_NAME, " +
                          "HEAD_OFFICE_NAME_BANGLA, " +
                          "BRANCH_OFFICE_ID, " +
                          "BRANCH_OFFICE_NAME, " +
                          "BRANCH_OFFICE_NAME_BANGLA, " +
                          "BRANCH_OFFICE_ADDRESS, " +
                          "NET_PAYMENT_AMOUNT_IN_WORD, " +
                          "PAYMENT_AMOUNT_IN_WORD_TOTAL, " +
                          "BANK_YN, " +
                          "BANK_STATUS, " +
                          "PAYMENT_DATE, " +
                          "total_working_day, " +
                          "DEPARTMENT_NAME_BANGLA, " +
                          "SECTION_NAME_BANGLA, " +
                          "SUB_SECTION_NAME_BANGLA, " +
                          "UNIT_NAME_BANGLA, " +
                          "absent_Day " +

                          " from VEW_RPT_SALARY_SHEET_WORKER  where head_office_id = '" + objReportSalary.HeadOfficeId + "' AND branch_office_id = '" + objReportSalary.BranchOfficeId + "' and salary_year ='" + objReportSalary.SalaryYear + "' and month_id = '" + objReportSalary.MonthId + "'  ";


                    if (objReportSalary.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objReportSalary.EmployeeId + "' ";

                    }
                    if (objReportSalary.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objReportSalary.DepartmentId + "' ";

                    }

                    if (objReportSalary.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objReportSalary.UnitId + "' ";

                    }

                    if (objReportSalary.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objReportSalary.SectionId + "' ";

                    }

                    if (objReportSalary.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objReportSalary.SubSectionId + "' ";

                    }
                    //sql = sql + " order by to_number(card_no)";

                    //sql = sql + " order by to_number(card_no)";

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_SALARY_SHEET_WORKER");
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

        public DataSet WorkerPaySlip(SalaryReportModel objReportSalary)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                          "SL, " +
                          " '' RPT_TITLE, " +
                          "EMPLOYEE_ID, " +
                          "EMPLOYEE_NAME, " +
                          "EMPLOYEE_NAME_BANGLA, " +
                          "CARD_NO, " +
                          "DESIGNATION_NAME, " +
                          "DESIGNATION_NAME_BANGLA, " +
                          "JOINING_DATE, " +
                          "SALARY_YEAR, " +
                          "MONTH_ID, " +
                          "MONTH_NAME, " +
                          "GRADE_ID, " +
                          "GRADE_NO, " +
                          "UNIT_ID, " +
                          "UNIT_NAME, " +
                          "UNIT_NAME_BANGLA, " +
                          "DEPARTMENT_ID, " +
                          "DEPARTMENT_NAME, " +
                          "DEPARTMENT_NAME_BANGLA, " +
                          "SECTION_ID, " +
                          "SECTION_NAME, " +
                          "SECTION_NAME_BANGLA, " +
                          "SUB_SECTION_ID, " +
                          "SUB_SECTION_NAME, " +
                          "SUB_SECTION_NAME_BANGLA, " +
                          "WORKING_DAY, " +
                          "ABSENT_DAY, " +
                          "ABSENT_HOUR, " +
                          "TOTAL_LEAVE, " +
                          "GROSS_SALARY, " +
                          "BASIC_SALARY, " +
                          "HOUSE_RENT, " +
                          "CONVEYANCE_ALLOWANCE, " +
                          "FOOD_ALLOWANCE, " +
                          "MEDICAL_ALLOWANCE, " +
                          "LEAVE_ALLOWANCE, " +
                          "ATTENDANCE_BONUS_AMOUNT, " +
                          "OT_HOUR, " +
                          "OT_RATE, " +
                          "OT_AMOUNT, " +
                          "ABSENT_DEDUCT_AMOUNT, " +
                          "TAX_DEDUCATION_AMOUNT, " +
                          "ADVANCE_AMOUNT, " +
                          "PF_ADVANCE_AMOUNT, " +
                          "PF_DEDUCTION_AMOUNT, " +
                          "FOOD_DEDUCTION_AMOUNT," +
                          "STAMP_FEE, " +
                          "TOTAL_PAY_AMOUNT, " +
                          "TOTAL_AMOUNT, " +
                          "NET_PAYMENT_AMOUNT, " +
                          "CREATE_BY, " +
                          "CREATE_DATE, " +
                          "UPDATE_BY, " +
                          "UPDATE_DATE, " +
                          "HEAD_OFFICE_ID, " +
                          "HEAD_OFFICE_NAME, " +
                          "HEAD_OFFICE_NAME_BANGLA, " +
                          "BRANCH_OFFICE_ID, " +
                          "BRANCH_OFFICE_NAME, " +
                          "BRANCH_OFFICE_NAME_BANGLA, " +
                          "BRANCH_OFFICE_ADDRESS, " +
                          "NET_PAYMENT_AMOUNT_IN_WORD, " +
                          "PAYMENT_AMOUNT_IN_WORD_TOTAL, " +
                          "BANK_YN, " +
                          "BANK_STATUS, " +
                          "PAYMENT_DATE, " +
                          "total_working_day " +



                          " from VEW_RPT_SALARY_SLIP_WORKER  where head_office_id = '" + objReportSalary.HeadOfficeId + "' AND branch_office_id = '" + objReportSalary.BranchOfficeId + "' and salary_year ='" + objReportSalary.SalaryYear + "' and month_id = '" + objReportSalary.MonthId + "'  ";


                    if (objReportSalary.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objReportSalary.EmployeeId + "' ";

                    }
                    if (objReportSalary.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objReportSalary.DepartmentId + "' ";

                    }

                    if (objReportSalary.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objReportSalary.UnitId + "' ";

                    }

                    if (objReportSalary.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objReportSalary.SectionId + "' ";

                    }

                    if (objReportSalary.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objReportSalary.SubSectionId + "' ";

                    }
                    //sql = sql + " order by to_number(card_no)";

                    //sql = sql + " order by to_number(card_no)";

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_SALARY_SLIP_WORKER");
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

        public DataSet IncrementYearlyCo(SalaryReportModel objReportSalary)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                          "RPT_TITLE, " +
                          "EMPLOYEE_ID, " +
                          "EMPLOYEE_NAME, " +
                          "JOINING_DATE, " +
                          "DESIGNATION_NAME, " +
                          "UNIT_ID, " +
                          "UNIT_NAME, " +
                          "DEPARTMENT_ID, " +
                          "DEPARTMENT_NAME, " +
                          "SECTION_ID, " +
                          "SECTION_NAME, " +
                          "SUB_SECTION_ID, " +
                          "SUB_SECTION_NAME, " +
                          "INCREMENT_YEAR, " +
                          "GROSS_SALARY, " +
                          "INCREMENT_AMOUNT, " +
                          "DESIGNATION_ID, " +
                          "CREATE_BY," +
                          "CREATE_DATE, " +
                          "UPDATE_BY," +
                          "UPDATE_DATE, " +
                          "HEAD_OFFICE_ID, " +
                          "HEAD_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ID, " +
                          "BRANCH_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ADDRESS, " +
                          "TOTAL_AMOUNT " +



                          " from VEW_INCREMENT_CO  where head_office_id = '" + objReportSalary.HeadOfficeId + "' AND branch_office_id = '" + objReportSalary.BranchOfficeId + "' and increment_year ='" + objReportSalary.SalaryYear + "'  ";


                    if (objReportSalary.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objReportSalary.EmployeeId + "' ";

                    }
                    if (objReportSalary.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objReportSalary.DepartmentId + "' ";

                    }

                    if (objReportSalary.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objReportSalary.UnitId + "' ";

                    }

                    if (objReportSalary.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objReportSalary.SectionId + "' ";

                    }

                    if (objReportSalary.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objReportSalary.SubSectionId + "' ";

                    }

                    //sql = sql + " order by to_number(card_no)";

                    //sql = sql + " order by to_number(card_no)";


                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_INCREMENT_CO");
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
        #region Salary Certificate

        public DataSet GetSalaryCertificate(SalaryCertificateModel objSalaryCertificate)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                          "RPT_HEADER, " +
                          "EMPLOYEE_ID, " +
                          "EMPLOYEE_NAME, " +
                          "RPT_BODY, " +
                          "RPT_FOOTER, " +
                          "SINCERELY, " +
                          "APPROVED_EMP_NAME, " +
                          "APPROVED_EMP_DESIGNATION, " +
                          "BRANCH_OFFICE_NAME, " +
                          "REF_NO, " +
                          "SALARY_YEAR, " +
                          "MONTH_ID, " +
                          "MONTH_NAME, " +
                          "UNIT_ID, " +
                          "UNIT_NAME, " +
                          "DEPARTMENT_ID, " +
                          "DEPARTMENT_NAME, " +
                          "SECTION_ID, " +
                          "SECTION_NAME, " +
                          "SUB_SECTION_ID, " +
                          "SUB_SECTION_NAME, " +
                          "GROSS_SALARY, " +
                          "BASIC_SALARY, " +
                          "HOUSE_RENT, " +
                          "CONVEYANCE_ALLOWANCE, " +
                          "FOOD_ALLOWANCE, " +
                          "MEDICAL_ALLOWANCE, " +
                          "TAX_DEDUCATION_AMOUNT, " +
                          "PF_DEDUCTION_AMOUNT, " +
                          "NET_PAYMENT_AMOUNT,  " +
                          "CREATE_BY, " +
                          "CREATE_DATE, " +
                          "UPDATE_BY, " +
                          "UPDATE_DATE, " +
                          "HEAD_OFFICE_ID, " +
                          "BRANCH_OFFICE_ID, " +
                          "PAYMENT_AMOUNT_IN_WORD_TOTAL " +


                          " from VEW_RPT_SALARY_CERTIFICATE  where head_office_id = '" + objSalaryCertificate.HeadOfficeId + "' AND branch_office_id = '" + objSalaryCertificate.BranchOfficeId + "' and salary_year ='" + objSalaryCertificate.SalaryYear + "' and month_id = '" + objSalaryCertificate.MonthId + "'  ";


                    if (objSalaryCertificate.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objSalaryCertificate.EmployeeId + "' ";

                    }
                    if (objSalaryCertificate.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objSalaryCertificate.DepartmentId + "' ";

                    }

                    if (objSalaryCertificate.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objSalaryCertificate.UnitId + "' ";

                    }

                    if (objSalaryCertificate.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objSalaryCertificate.SectionId + "' ";

                    }

                    if (objSalaryCertificate.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objSalaryCertificate.SubSectionId + "' ";

                    }

                    //sql = sql + " order by to_number(card_no)";

                    //sql = sql + " order by to_number(card_no)";


                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "VEW_RPT_SALARY_CERTIFICATE");
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

        public List<SalaryCertificateModel> GetEmpRecordForSC(SalaryCertificateModel objSalaryCertificate)
        {

            // DataTable dt = new DataTable();
            List<SalaryCertificateModel> salaryCertificateList = new List<SalaryCertificateModel>();

            string sql = "";
            sql = "SELECT " +
                  "rownum SL, " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME, " +
                  "DESIGNATION_NAME, " +
                  "to_char(JOINING_DATE, 'dd/mm/yyyy')JOINING_DATE, " +
                  "DEPARTMENT_NAME, " +
                  "EMPLOYEE_PIC," +
                  "GROSS_SALARY " +

                  "FROM vew_emp_record_for_sc where   head_office_id = '" + objSalaryCertificate.HeadOfficeId + "' and branch_office_id = '" + objSalaryCertificate.BranchOfficeId + "'   ";

            if (objSalaryCertificate.EmployeeName != null)
            {

                sql = sql + "and (lower(employee_name) like lower( '%" + objSalaryCertificate.EmployeeId + "%')  or upper(employee_name)like upper('%" + objSalaryCertificate.EmployeeName + "%') )";
            }

            if (objSalaryCertificate.EmployeeId != null)
            {

                sql = sql + "and employee_id = '" + objSalaryCertificate.EmployeeId + "' ";
            }

            if (objSalaryCertificate.UnitId != null)
            {

                sql = sql + "and unit_id = '" + objSalaryCertificate.UnitId + "' ";
            }

            if (objSalaryCertificate.SectionId != null)
            {

                sql = sql + "and section_id = '" + objSalaryCertificate.SectionId + "' ";
            }

            if (objSalaryCertificate.SubSectionId != null)
            {

                sql = sql + "and sub_section_id = '" + objSalaryCertificate.SubSectionId + "' ";
            }
            if (objSalaryCertificate.DepartmentId != null)
            {

                sql = sql + "and department_id = '" + objSalaryCertificate.DepartmentId + "' ";
            }

            sql = sql + " ORDER BY SL ";


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();


                try
                {
                    while (objReader.Read())
                    {
                        SalaryCertificateModel objSalary = new SalaryCertificateModel
                        {
                            SerialNumber = Convert.ToInt32(objReader["SL"]).ToString(),
                            EmployeeId = objReader["EMPLOYEE_ID"].ToString(),
                            EmployeeName = objReader["EMPLOYEE_NAME"].ToString(),
                            JoiningDate = objReader["JOINING_DATE"].ToString(),
                            DesignationName = objReader["DESIGNATION_NAME"].ToString(),
                            DepartmentName = objReader["DEPARTMENT_NAME"].ToString(),
                            Salary = objReader["GROSS_SALARY"].ToString(),
                            EmployeeImage = objReader["EMPLOYEE_PIC"] == DBNull.Value ? new byte[0] : (byte[])objReader["EMPLOYEE_PIC"]
                        };
                        salaryCertificateList.Add(objSalary);
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
                return salaryCertificateList;
            }
        }

        public string AddEmpRecordForSC(SalaryCertificateModel objSalaryCertificate)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_emp_add_for_sc");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objSalaryCertificate.EmployeeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.EmployeeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objSalaryCertificate.SalaryYear != "")
            {
                objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.SalaryYear;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objSalaryCertificate.MonthId != "")
            {
                objOracleCommand.Parameters.Add("P_MONTH_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.MonthId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_MONTH_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objSalaryCertificate.UnitId != "")
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objSalaryCertificate.DepartmentId != "")
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objSalaryCertificate.SectionId != "")
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objSalaryCertificate.SubSectionId != "")
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSalaryCertificate.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSalaryCertificate.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSalaryCertificate.BranchOfficeId;

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

        public string ProcessEmpRecordForSC(SalaryCertificateModel objSalaryCertificate)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_salary_certificate_process");
            objOracleCommand.CommandType = CommandType.StoredProcedure;



            if (objSalaryCertificate.SalaryYear != "")
            {
                objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.SalaryYear;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objSalaryCertificate.MonthId != "")
            {
                objOracleCommand.Parameters.Add("P_MONTH_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.MonthId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_MONTH_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objSalaryCertificate.UnitId != "")
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objSalaryCertificate.DepartmentId != "")
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objSalaryCertificate.SectionId != "")
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objSalaryCertificate.SubSectionId != "")
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryCertificate.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSalaryCertificate.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSalaryCertificate.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSalaryCertificate.BranchOfficeId;

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
        #region Employee Earn Leave Report

        public DataSet GetYearlyEarnLeaveReport(EmployeeEarnLeaveModel objEarnLeaveReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +

                          "'Earn Leave Of '|| '-  '||  '" + objEarnLeaveReportModel.SalaryYear + "' RPT_TITLE, " +
                          "EMPLOYEE_ID, " +
                          "EMPLOYEE_NAME, " +
                          "JOINING_DATE, " +
                          "EARN_LEAVE_YEAR, " +
                          "LIMIT_DATE, " +
                          "DESIGNATION_ID, " +
                          "DESIGNATION_NAME, " +
                          "UNIT_ID, " +
                          "UNIT_NAME, " +
                          "DEPARTMENT_ID, " +
                          "DEPARTMENT_NAME, " +
                          "SECTION_ID, " +
                          "SECTION_NAME, " +
                          "SUB_SECTION_ID, " +
                          "SUB_SECTION_NAME, " +
                          "TOTAL_WORKING_DAY, " +
                          "TOTAL_HOLIDAY, " +
                          "TOTAL_LATE_HOUR, " +
                          "EL_DAY, " +
                          "BASIC_SALARY, " +
                          "GROSS_SALARY," +
                          "NET_PAYMENT_AMOUNT, " +
                          "CREATE_BY," +
                          "CREATE_DATE, " +
                          "UPDATE_BY, " +
                          "UPDATE_DATE, " +
                          "HEAD_OFFICE_ID, " +
                          "HEAD_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ID, " +
                          "BRANCH_OFFICE_NAME, " +
                          "BRANCH_OFFICE_ADDRESS, " +
                          "PAYMENT_AMOUNT_IN_WORD_TOTAL " +

                          "from vew_rpt_el  where head_office_id = '" + objEarnLeaveReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEarnLeaveReportModel.BranchOfficeId + "' and EARN_LEAVE_YEAR =  '" + objEarnLeaveReportModel.SalaryYear + "' ";


                    if (objEarnLeaveReportModel.EmployeeId != null)
                    {
                        sql = sql + "and employee_id = '" + objEarnLeaveReportModel.EmployeeId + "' ";

                    }

                    if (objEarnLeaveReportModel.UnitId != null)
                    {
                        sql = sql + "and unit_id = '" + objEarnLeaveReportModel.UnitId + "' ";
                    }


                    if (objEarnLeaveReportModel.DepartmentId != null)
                    {
                        sql = sql + "and department_id = '" + objEarnLeaveReportModel.DepartmentId + "' ";
                    }


                    if (objEarnLeaveReportModel.SectionId != null)
                    {
                        sql = sql + "and section_id = '" + objEarnLeaveReportModel.SectionId + "' ";
                    }

                    if (objEarnLeaveReportModel.SubSectionId != null)
                    {
                        sql = sql + "and sub_section_id = '" + objEarnLeaveReportModel.SubSectionId + "' ";
                    }

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "vew_rpt_el");
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
        #region Attendance Report


        public DataSet DailyAttendanceSheet(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = "SELECT " +

                        "'Daily Attendence Report on '|| ' - '|| to_date( '" + objAttendenceReportModel.FromDate.Trim() + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                        "EMPLOYEE_ID, " +
                        "EMPLOYEE_NAME, " +
                        "JOINING_DATE, " +
                        "DESIGNATION_NAME, " +
                        "UNIT_ID, " +
                        "UNIT_NAME, " +
                        "DEPARTMENT_ID, " +
                        "DEPARTMENT_NAME, " +
                        "SECTION_ID, " +
                        "SECTION_NAME, " +
                        "SUB_SECTION_ID, " +
                        "SUB_SECTION_NAME, " +
                        "FIRST_IN, " +
                        "LAST_OUT, " +
                        "FIRST_LATE_HOUR, " +
                        "FIRST_LATE_MINUTE, " +
                        "LUNCH_OUT, " +
                        "LUNCH_IN, " +
                        "LUNCH_LATE_HOUR, " +
                        "LUNCH_LATE_MINUTE, " +
                        "OT_HOUR, " +
                        "OT_MINUTE, " +
                        "LOG_LATE, " +
                        "LUNCH_LATE, " +
                        "LOG_DATE, " +
                        "PUNCH_CODE, " +
                        "DAY_TYPE, " +
                        "REMARKS, " +
                        "UPDATE_DATE, " +
                        "HEAD_OFFICE_ID, " +
                        "HEAD_OFFICE_NAME, " +
                        "BRANCH_OFFICE_ID, " +
                        "BRANCH_OFFICE_NAME, " +
                        "BRANCH_OFFICE_ADDRESS, " +
                        "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objAttendenceReportModel.UpdateBy + "') UPDATE_BY, " +
                        "LATE_YN, " +
                        "late_status, " +
                        "PUNCTUAL_YN, " +
                        "PUNCTUAL_STATUS, " +
                        "LATE_HMS, " +
                        "total_duty_time, " +
                        "EARLY_OUT_TIME, " +
                        "finger_yn,  " +
                        "card_yn,  " +
                        "punch_type,  " +
                        "first_in_new, " +
                        "last_out_new, " +

                        "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate.Trim() + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate.Trim() + "', 'dd/mm/yyyy')   and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)total_employee, " +


                          " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate.Trim() + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate.Trim() + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)total_present_employee, " +



                          " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)total_absent_employee, " +

                          " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and unit_id = '" + objAttendenceReportModel.UnitId + "' ) " +
                        " FROM DUAL)total_holiday_employee, " +



                            " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and unit_id = '" + objAttendenceReportModel.UnitId + "' ) " +
                        " FROM DUAL)total_leave_employee, " +


                              " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)toal_late_employee, " +


                                " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)total_punctual_employee, " +

                          " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and unit_id = '" + objAttendenceReportModel.UnitId + "' ) " +
                        " FROM DUAL)total_gh, " +


                        "NB, " +
                           "present_status, " +
                           "absent_status, " +
                           "leave_status, " +
                           "WORKING_HOLIDAY_STATUS, " +
                           "late_status_new, " +

                           "PLELA_ALL, " +
                           "late_hm, " +


                                      " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)total_early_out, " +


                                   " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)total_early_late, " +

                              " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)total_cl, " +


                              " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)total_sl, " +

                          " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)total_ml, " +

                          " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)total_el, " +

                          " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)total_pl, " +


                          " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
                        " FROM DUAL)total_lwp " +


                        "from VEW_RPT_DAILY_ATTENDANCE_SHEET a  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') ";




                        if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                        {
                            sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                        }



                    }



                    else if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = "SELECT " +

                        "'Daily Attendence '|| ' - '|| to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                        "EMPLOYEE_ID, " +
                        "EMPLOYEE_NAME, " +
                        "JOINING_DATE, " +
                        "DESIGNATION_NAME, " +
                        "UNIT_ID, " +
                        "UNIT_NAME, " +
                        "DEPARTMENT_ID, " +
                        "DEPARTMENT_NAME, " +
                        "SECTION_ID, " +
                        "SECTION_NAME, " +
                        "SUB_SECTION_ID, " +
                        "SUB_SECTION_NAME, " +
                        "FIRST_IN, " +
                        "LAST_OUT, " +
                        "FIRST_LATE_HOUR, " +
                        "FIRST_LATE_MINUTE, " +
                        "LUNCH_OUT, " +
                        "LUNCH_IN, " +
                        "LUNCH_LATE_HOUR, " +
                        "LUNCH_LATE_MINUTE, " +
                        "OT_HOUR, " +
                        "OT_MINUTE, " +
                        "LOG_LATE, " +
                        "LUNCH_LATE, " +
                        "LOG_DATE, " +
                        "PUNCH_CODE, " +
                        "DAY_TYPE, " +
                        "REMARKS, " +

                        "UPDATE_DATE, " +
                        "HEAD_OFFICE_ID, " +
                        "HEAD_OFFICE_NAME, " +
                        "BRANCH_OFFICE_ID, " +
                        "BRANCH_OFFICE_NAME, " +
                        "BRANCH_OFFICE_ADDRESS, " +
                        "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objAttendenceReportModel.UpdateBy + "') UPDATE_BY, " +

                        "LATE_YN, " +
                        "late_status, " +
                        "PUNCTUAL_YN, " +
                        "PUNCTUAL_STATUS, " +
                        "LATE_HMS, " +
                        "total_duty_time, " +
                        "EARLY_OUT_TIME, " +
                        "finger_yn,  " +
                        "card_yn,  " +
                        "punch_type,  " +
                        "first_in_new, " +
                        "last_out_new, " +

                        "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')   and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)total_employee, " +


                          " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)total_present_employee, " +



                          " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)total_absent_employee, " +

                          " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "' ) " +
                        " FROM DUAL)total_holiday_employee, " +



                            " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "' ) " +
                        " FROM DUAL)total_leave_employee, " +


                              " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)toal_late_employee, " +


                                " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)total_punctual_employee, " +

                          " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "' ) " +
                        " FROM DUAL)total_gh, " +


                        "NB, " +
                           "present_status, " +
                           "absent_status, " +
                           "leave_status, " +
                           "WORKING_HOLIDAY_STATUS, " +
                             "late_status_new, " +
                             "PLELA_ALL, " +
                           "late_hm, " +



                                      " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)total_early_out, " +


                                   " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')   and late_yn = 'Y' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)total_early_late, " +

                              " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)total_cl, " +


                              " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)total_sl, " +

                          " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)total_ml, " +

                          " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)total_el, " +

                          " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)total_pl, " +


                          " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                               "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                               "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
                        " FROM DUAL)total_lwp " +



                        "from VEW_RPT_DAILY_ATTENDANCE_SHEET a where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') ";




                        if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                        {
                            sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

                        }

                    }
                    else if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = "SELECT " +

"'Daily Attendence '|| ' - '|| to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
"EMPLOYEE_ID, " +
"EMPLOYEE_NAME, " +
"JOINING_DATE, " +
"DESIGNATION_NAME, " +
"UNIT_ID, " +
"UNIT_NAME, " +
"DEPARTMENT_ID, " +
"DEPARTMENT_NAME, " +
"SECTION_ID, " +
"SECTION_NAME, " +
"SUB_SECTION_ID, " +
"SUB_SECTION_NAME, " +
"FIRST_IN, " +
"LAST_OUT, " +
"FIRST_LATE_HOUR, " +
"FIRST_LATE_MINUTE, " +
"LUNCH_OUT, " +
"LUNCH_IN, " +
"LUNCH_LATE_HOUR, " +
"LUNCH_LATE_MINUTE, " +
"OT_HOUR, " +
"OT_MINUTE, " +
"LOG_LATE, " +
"LUNCH_LATE, " +
"LOG_DATE, " +
"PUNCH_CODE, " +
"DAY_TYPE, " +
"REMARKS, " +

"UPDATE_DATE, " +
"HEAD_OFFICE_ID, " +
"HEAD_OFFICE_NAME, " +
"BRANCH_OFFICE_ID, " +
"BRANCH_OFFICE_NAME, " +
"BRANCH_OFFICE_ADDRESS, " +
"(select employee_name from employee_basic where EMPLOYEE_ID = '" + objAttendenceReportModel.UpdateBy + "') UPDATE_BY, " +

"LATE_YN, " +
"late_status, " +
"PUNCTUAL_YN, " +
"PUNCTUAL_STATUS, " +
"LATE_HMS, " +
"total_duty_time, " +
"EARLY_OUT_TIME, " +
"finger_yn,  " +
"card_yn,  " +
"punch_type,  " +
"first_in_new, " +
"last_out_new, " +

"( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')   and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_employee, " +


  " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_present_employee, " +



  " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_absent_employee, " +

  " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and department_id = '" + objAttendenceReportModel.DepartmentId + "' ) " +
" FROM DUAL)total_holiday_employee, " +



    " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and department_id = '" + objAttendenceReportModel.DepartmentId + "' ) " +
" FROM DUAL)total_leave_employee, " +


      " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)toal_late_employee, " +


        " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_punctual_employee, " +

  " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and department_id = '" + objAttendenceReportModel.DepartmentId + "' ) " +
" FROM DUAL)total_gh, " +



"NB, " +

   "present_status, " +
   "absent_status, " +
   "leave_status, " +
   "WORKING_HOLIDAY_STATUS, " +
     "late_status_new, " +
     "PLELA_ALL, " +
   "late_hm, " +



              " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_early_out, " +


           " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')   and late_yn = 'Y' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_early_late, " +

      " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_cl, " +


      " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_sl, " +

  " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_ml, " +

  " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_el, " +

  " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_pl, " +


  " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_lwp " +





"from VEW_RPT_DAILY_ATTENDANCE_SHEET a where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') ";


                        if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                        {
                            sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                        }

                    }


                    else
                    {
                        sql = "SELECT " +

"'Daily Attendence '|| ' - '|| to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
"EMPLOYEE_ID, " +
"EMPLOYEE_NAME, " +
"JOINING_DATE, " +
"DESIGNATION_NAME, " +
"UNIT_ID, " +
"UNIT_NAME, " +
"DEPARTMENT_ID, " +
"DEPARTMENT_NAME, " +
"SECTION_ID, " +
"SECTION_NAME, " +
"SUB_SECTION_ID, " +
"SUB_SECTION_NAME, " +
"FIRST_IN, " +
"LAST_OUT, " +
"FIRST_LATE_HOUR, " +
"FIRST_LATE_MINUTE, " +
"LUNCH_OUT, " +
"LUNCH_IN, " +
"LUNCH_LATE_HOUR, " +
"LUNCH_LATE_MINUTE, " +
"OT_HOUR, " +
"OT_MINUTE, " +
"LOG_LATE, " +
"LUNCH_LATE, " +
"LOG_DATE, " +
"PUNCH_CODE, " +
"DAY_TYPE, " +
"REMARKS, " +

"UPDATE_DATE, " +
"HEAD_OFFICE_ID, " +
"HEAD_OFFICE_NAME, " +
"BRANCH_OFFICE_ID, " +
"BRANCH_OFFICE_NAME, " +
"BRANCH_OFFICE_ADDRESS, " +
"(select employee_name from employee_basic where EMPLOYEE_ID = '" + objAttendenceReportModel.UpdateBy + "') UPDATE_BY, " +

"LATE_YN, " +
"late_status, " +
"PUNCTUAL_YN, " +
"PUNCTUAL_STATUS, " +
"LATE_HMS, " +
"total_duty_time, " +
"EARLY_OUT_TIME, " +
"finger_yn,  " +
"card_yn,  " +
"punch_type,  " +
"first_in_new, " +
"last_out_new, " +

"( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  ) " +
" FROM DUAL)total_employee, " +


  " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00')  " +
" FROM DUAL)total_present_employee, " +



  " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' ) " +
" FROM DUAL)total_absent_employee, " +

  " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' ) " +
" FROM DUAL)total_holiday_employee, " +



    " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' ) " +
" FROM DUAL)total_leave_employee, " +


      " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y' ) " +
" FROM DUAL)toal_late_employee, " +


        " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' ) " +
" FROM DUAL)total_punctual_employee, " +

  " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' ) " +
" FROM DUAL)total_gh, " +


"NB, " +
   "present_status, " +
   "absent_status, " +
   "leave_status, " +
   "WORKING_HOLIDAY_STATUS, " +
     "late_status_new, " +
     "PLELA_ALL, " +
   "late_hm, " +


                 " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' ) " +
" FROM DUAL)total_early_out, " +


           " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')   and late_yn = 'Y' ) " +
" FROM DUAL)total_early_late, " +

      " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' ) " +
" FROM DUAL)total_cl, " +


      " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' ) " +
" FROM DUAL)total_sl, " +

  " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' ) " +
" FROM DUAL)total_ml, " +

  " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' ) " +
" FROM DUAL)total_el, " +

  " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' ) " +
" FROM DUAL)total_pl, " +


  " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' ) " +
" FROM DUAL)total_lwp " +



"from VEW_RPT_DAILY_ATTENDANCE_SHEET a where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') ";


                        if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                        {
                            sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                        }
                        if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                        {
                            sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                        }

                        if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                        {
                            sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                        }

                        if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                        {
                            sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                        }

                        if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                        {
                            sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

                        }

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
                            objDataAdapter.Fill(ds, "VEW_RPT_DAILY_ATTENDANCE_SHEET");
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
        public DataSet DailyAttendanceMissingSheet(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +
                    "'Detail In Time or Out Time Missing Histroy from  ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                    "CARD_NO, " +
                    "EMPLOYEE_ID, " +
                    "EMPLOYEE_NAME, " +
                    "JOINING_DATE, " +
                    "DESIGNATION_NAME, " +
                    "UNIT_ID, " +
                    "UNIT_NAME, " +
                    "DEPARTMENT_ID, " +
                    "DEPARTMENT_NAME, " +
                    "SECTION_ID, " +
                    "SECTION_NAME, " +
                    "SUB_SECTION_ID, " +
                    "SUB_SECTION_NAME, " +
                    "PUNCH_CODE, " +
                    "DAY_TYPE, " +
                    "FIRST_IN, " +
                    "LAST_OUT, " +
                    "FIRST_LATE_HOUR, " +
                    "FIRST_LATE_MINUTE, " +
                    "LUNCH_OUT, " +
                    "LUNCH_IN, " +
                    "LUNCH_LATE_HOUR, " +
                    "LUNCH_LATE_MINUTE, " +
                    "OT_HOUR, " +
                    "OT_MINUTE, " +
                    "LOG_DATE, " +
                    "REMARKS, " +
                    "FIRST_LATE_SECOND, " +
                    "TOTAL_DUTY_TIME, " +
                    "EARLY_OUT_TIME, " +
                    "FINGER_YN, " +
                    "CARD_YN, " +
                    "WORKING_HOUR, " +
                    "WORKING_MINUTE, " +
                    "WORKING_SECOND, " +
                    "UPDATE_BY, " +
                    "UPDATE_DATE, " +
                    "HEAD_OFFICE_ID, " +
                    "HEAD_OFFICE_NAME, " +
                    "BRANCH_OFFICE_ID, " +
                    "BRANCH_OFFICE_NAME, " +
                    "BRANCH_OFFICE_ADDRESS " +
                    "from VEW_RPT_DAILY_ATTEN_MISSING  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') ";


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_DAILY_ATTEN_MISSING");
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
        public DataSet MonthlyAttendanceSheet(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = "SELECT " +

                            "'Attendacne History From  ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
"EMPLOYEE_ID, " +
"EMPLOYEE_NAME, " +
"JOINING_DATE, " +
"DESIGNATION_NAME, " +
"UNIT_ID, " +
"UNIT_NAME, " +
"DEPARTMENT_ID, " +
"DEPARTMENT_NAME, " +
"SECTION_ID, " +
"SECTION_NAME, " +
"SUB_SECTION_ID, " +
"SUB_SECTION_NAME, " +
"FIRST_IN, " +
"LAST_OUT, " +
"FIRST_LATE_HOUR, " +
"FIRST_LATE_MINUTE, " +
"LUNCH_OUT, " +
"LUNCH_IN, " +
"LUNCH_LATE_HOUR, " +
"LUNCH_LATE_MINUTE, " +
"OT_HOUR, " +
"OT_MINUTE, " +
"LOG_LATE, " +
"LUNCH_LATE, " +
"LOG_DATE, " +
"PUNCH_CODE, " +
"DAY_TYPE, " +
"REMARKS, " +
"UPDATE_DATE, " +
"HEAD_OFFICE_ID, " +
"HEAD_OFFICE_NAME, " +
"BRANCH_OFFICE_ID, " +
"BRANCH_OFFICE_NAME, " +
"BRANCH_OFFICE_ADDRESS, " +
"(select employee_name from employee_basic where EMPLOYEE_ID = '" + objAttendenceReportModel.UpdateBy + "') UPDATE_BY, " +
"LATE_YN, " +
"late_status, " +
"PUNCTUAL_YN, " +
"PUNCTUAL_STATUS, " +
"LATE_HMS, " +
"total_duty_time, " +
"EARLY_OUT_TIME, " +
"finger_yn,  " +
"card_yn,  " +
"punch_type,  " +
"first_in_new, " +
"last_out_new, " +

"( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')   and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)total_employee, " +


  " (SELECT 'Present : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)total_present_employee, " +



  " (SELECT 'Absent : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)total_absent_employee, " +

  " (SELECT 'W/H : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  day_type = 'H' and unit_id = '" + objAttendenceReportModel.UnitId + "' and first_in_new is null and last_out_new is null) " +
" FROM DUAL)total_holiday_employee, " +



    " (SELECT 'Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and unit_id = '" + objAttendenceReportModel.UnitId + "' ) " +
" FROM DUAL)total_leave_employee, " +


      " (SELECT 'Late : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)toal_late_employee, " +


        " (SELECT 'Punctual : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)total_punctual_employee, " +

"NB, " +
   "present_status, " +
   "absent_status, " +
   "leave_status, " +
   "WORKING_HOLIDAY_STATUS, " +
   "late_status_new, " +

   "PLELA_ALL, " +
   "late_hm, " +
   "card_no, " +

           " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)total_early_out, " +


           " (SELECT 'E/L : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and late_yn = 'Y' and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)total_early_late, " +




      " (SELECT 'CL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3'  and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)total_cl, " +


      " (SELECT 'SL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4'  and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)total_sl, " +

  " (SELECT 'ML : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4'  and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)total_ml, " +

  " (SELECT 'EL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10'  and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)total_el, " +

  " (SELECT 'PL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7'  and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)total_pl, " +


  " (SELECT 'LWP : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5'  and unit_id = '" + objAttendenceReportModel.UnitId + "') " +
" FROM DUAL)total_lwp " +


"from VEW_RPT_DAILY_ATTENDANCE_SHEET a  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') ";




                        if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                        {
                            sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                        }



                    }



                    else if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = "SELECT " +

   "'Attendacne History From  ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
"EMPLOYEE_ID, " +
"EMPLOYEE_NAME, " +
"JOINING_DATE, " +
"DESIGNATION_NAME, " +
"UNIT_ID, " +
"UNIT_NAME, " +
"DEPARTMENT_ID, " +
"DEPARTMENT_NAME, " +
"SECTION_ID, " +
"SECTION_NAME, " +
"SUB_SECTION_ID, " +
"SUB_SECTION_NAME, " +
"FIRST_IN, " +
"LAST_OUT, " +
"FIRST_LATE_HOUR, " +
"FIRST_LATE_MINUTE, " +
"LUNCH_OUT, " +
"LUNCH_IN, " +
"LUNCH_LATE_HOUR, " +
"LUNCH_LATE_MINUTE, " +
"OT_HOUR, " +
"OT_MINUTE, " +
"LOG_LATE, " +
"LUNCH_LATE, " +
"LOG_DATE, " +
"PUNCH_CODE, " +
"DAY_TYPE, " +
"REMARKS, " +

"UPDATE_DATE, " +
"HEAD_OFFICE_ID, " +
"HEAD_OFFICE_NAME, " +
"BRANCH_OFFICE_ID, " +
"BRANCH_OFFICE_NAME, " +
"BRANCH_OFFICE_ADDRESS, " +
"(select employee_name from employee_basic where EMPLOYEE_ID = '" + objAttendenceReportModel.UpdateBy + "') UPDATE_BY, " +

"LATE_YN, " +
"late_status, " +
"PUNCTUAL_YN, " +
"PUNCTUAL_STATUS, " +
"LATE_HMS, " +
"total_duty_time, " +
"EARLY_OUT_TIME, " +
"finger_yn,  " +
"card_yn,  " +
"punch_type,  " +
"first_in_new, " +
"last_out_new, " +

"( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')   and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)total_employee, " +


  " (SELECT 'Present : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)total_present_employee, " +



  " (SELECT 'Absent : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)total_absent_employee, " +

  " (SELECT 'W/H : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  day_type = 'H' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "' and first_in_new is null and last_out_new is null) " +
" FROM DUAL)total_holiday_employee, " +



    " (SELECT 'Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "' ) " +
" FROM DUAL)total_leave_employee, " +


      " (SELECT 'Late : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)toal_late_employee, " +


        " (SELECT 'Punctual : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)total_punctual_employee, " +
"NB, " +
   "present_status, " +
   "absent_status, " +
   "leave_status, " +
   "WORKING_HOLIDAY_STATUS, " +
     "late_status_new, " +
     "PLELA_ALL, " +
   "late_hm, " +
   "card_no, " +


           " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)total_early_out, " +


           " (SELECT 'E/L : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and late_yn = 'Y' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)total_early_late, " +

      " (SELECT 'CL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)total_cl, " +


      " (SELECT 'SL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)total_sl, " +

  " (SELECT 'ML : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)total_ml, " +

  " (SELECT 'EL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)total_el, " +

  " (SELECT 'PL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)total_pl, " +


  " (SELECT 'LWP : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and sub_Section_id = '" + objAttendenceReportModel.SubSectionId + "') " +
" FROM DUAL)total_lwp " +



"from VEW_RPT_DAILY_ATTENDANCE_SHEET a where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') ";




                        if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                        {
                            sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

                        }

                    }


                    else if (objAttendenceReportModel.DepartmentId != "")
                    {
                        sql = "SELECT " +

   "'Attendacne History From  ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
"EMPLOYEE_ID, " +
"EMPLOYEE_NAME, " +
"JOINING_DATE, " +
"DESIGNATION_NAME, " +
"UNIT_ID, " +
"UNIT_NAME, " +
"DEPARTMENT_ID, " +
"DEPARTMENT_NAME, " +
"SECTION_ID, " +
"SECTION_NAME, " +
"SUB_SECTION_ID, " +
"SUB_SECTION_NAME, " +
"FIRST_IN, " +
"LAST_OUT, " +
"FIRST_LATE_HOUR, " +
"FIRST_LATE_MINUTE, " +
"LUNCH_OUT, " +
"LUNCH_IN, " +
"LUNCH_LATE_HOUR, " +
"LUNCH_LATE_MINUTE, " +
"OT_HOUR, " +
"OT_MINUTE, " +
"LOG_LATE, " +
"LUNCH_LATE, " +
"LOG_DATE, " +
"PUNCH_CODE, " +
"DAY_TYPE, " +
"REMARKS, " +

"UPDATE_DATE, " +
"HEAD_OFFICE_ID, " +
"HEAD_OFFICE_NAME, " +
"BRANCH_OFFICE_ID, " +
"BRANCH_OFFICE_NAME, " +
"BRANCH_OFFICE_ADDRESS, " +
"(select employee_name from employee_basic where EMPLOYEE_ID = '" + objAttendenceReportModel.UpdateBy + "') UPDATE_BY, " +

"LATE_YN, " +
"late_status, " +
"PUNCTUAL_YN, " +
"PUNCTUAL_STATUS, " +
"LATE_HMS, " +
"total_duty_time, " +
"EARLY_OUT_TIME, " +
"finger_yn,  " +
"card_yn,  " +
"punch_type,  " +
"first_in_new, " +
"last_out_new, " +

"( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')   and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_employee, " +


  " (SELECT 'Present : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_present_employee, " +



  " (SELECT 'Absent : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_absent_employee, " +

  " (SELECT 'W/H : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  day_type = 'H' and department_id = '" + objAttendenceReportModel.DepartmentId + "' and first_in_new is null and last_out_new is null) " +
" FROM DUAL)total_holiday_employee, " +



    " (SELECT 'Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and department_id = '" + objAttendenceReportModel.DepartmentId + "' ) " +
" FROM DUAL)total_leave_employee, " +


      " (SELECT 'Late : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)toal_late_employee, " +


        " (SELECT 'Punctual : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_punctual_employee, " +
"NB, " +

   "present_status, " +
   "absent_status, " +
   "leave_status, " +
   "WORKING_HOLIDAY_STATUS, " +
     "late_status_new, " +
     "PLELA_ALL, " +
   "late_hm, " +
   "card_no, " +

           " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_early_out, " +


           " (SELECT 'E/L : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and late_yn = 'Y' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_early_late, " +

      " (SELECT 'CL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_cl, " +


      " (SELECT 'SL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_sl, " +

  " (SELECT 'ML : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_ml, " +

  " (SELECT 'EL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_el, " +

  " (SELECT 'PL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_pl, " +


  " (SELECT 'LWP : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and department_id = '" + objAttendenceReportModel.DepartmentId + "') " +
" FROM DUAL)total_lwp " +


"from VEW_RPT_DAILY_ATTENDANCE_SHEET a where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') ";


                        if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                        {
                            sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                        }
                    }

                    else
                    {
                        sql = "SELECT " +

"'Daily Attendence '|| ' - '|| to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
"EMPLOYEE_ID, " +
"EMPLOYEE_NAME, " +
"JOINING_DATE, " +
"DESIGNATION_NAME, " +
"UNIT_ID, " +
"UNIT_NAME, " +
"DEPARTMENT_ID, " +
"DEPARTMENT_NAME, " +
"SECTION_ID, " +
"SECTION_NAME, " +
"SUB_SECTION_ID, " +
"SUB_SECTION_NAME, " +
"FIRST_IN, " +
"LAST_OUT, " +
"FIRST_LATE_HOUR, " +
"FIRST_LATE_MINUTE, " +
"LUNCH_OUT, " +
"LUNCH_IN, " +
"LUNCH_LATE_HOUR, " +
"LUNCH_LATE_MINUTE, " +
"OT_HOUR, " +
"OT_MINUTE, " +
"LOG_LATE, " +
"LUNCH_LATE, " +
"LOG_DATE, " +
"PUNCH_CODE, " +
"DAY_TYPE, " +
"REMARKS, " +

"UPDATE_DATE, " +
"HEAD_OFFICE_ID, " +
"HEAD_OFFICE_NAME, " +
"BRANCH_OFFICE_ID, " +
"BRANCH_OFFICE_NAME, " +
"BRANCH_OFFICE_ADDRESS, " +
"(select employee_name from employee_basic where EMPLOYEE_ID = '" + objAttendenceReportModel.UpdateBy + "') UPDATE_BY, " +

"LATE_YN, " +
"late_status, " +
"PUNCTUAL_YN, " +
"PUNCTUAL_STATUS, " +
"LATE_HMS, " +
"total_duty_time, " +
"EARLY_OUT_TIME, " +
"finger_yn,  " +
"card_yn,  " +
"punch_type,  " +
"first_in_new, " +
"last_out_new, " +
"card_no, " +

"( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  ) " +
" FROM DUAL)total_employee, " +


  " (SELECT 'Present : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00')  " +
" FROM DUAL)total_present_employee, " +



  " (SELECT 'Absent : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' ) " +
" FROM DUAL)total_absent_employee, " +

  " (SELECT 'W/H : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and  day_type = 'H'  and first_in_new is null and last_out_new is null ) " +
" FROM DUAL)total_holiday_employee, " +



    " (SELECT 'Leave : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' ) " +
" FROM DUAL)total_leave_employee, " +


      " (SELECT 'Late : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y' ) " +
" FROM DUAL)toal_late_employee, " +


        " (SELECT 'Punctual : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' ) " +
" FROM DUAL)total_punctual_employee, " +
"NB, " +
   "present_status, " +
   "absent_status, " +
   "leave_status, " +
   "WORKING_HOLIDAY_STATUS, " +
     "late_status_new, " +
     "PLELA_ALL, " +
   "late_hm, " +


           " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' ) " +
" FROM DUAL)total_early_out, " +


           " (SELECT 'E/L : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and late_yn = 'Y' ) " +
" FROM DUAL)total_early_late, " +

      " (SELECT 'CL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' ) " +
" FROM DUAL)total_cl, " +


      " (SELECT 'SL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' ) " +
" FROM DUAL)total_sl, " +

  " (SELECT 'ML : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' ) " +
" FROM DUAL)total_ml, " +

  " (SELECT 'EL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' ) " +
" FROM DUAL)total_el, " +

  " (SELECT 'PL : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' ) " +
" FROM DUAL)total_pl, " +


  " (SELECT 'LWP : '|| (SELECT COUNT(*) " +
       "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
       "  WHERE employee_id = a.employee_id  and head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' ) " +
" FROM DUAL)total_lwp " +

"from VEW_RPT_DAILY_ATTENDANCE_SHEET a where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') ";


                        if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                        {
                            sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                        }
                        if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                        {
                            sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                        }

                        if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                        {
                            sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                        }

                        if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                        {
                            sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                        }

                        if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                        {
                            sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

                        }

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
                            objDataAdapter.Fill(ds, "VEW_RPT_DAILY_ATTENDANCE_SHEET");
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
        public DataSet DailyLateSheet(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +

                   "'Daily Late Attendence '|| '-  '|| to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') RPT_TITLE, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "JOINING_DATE, " +
                   "DESIGNATION_NAME, " +
                   "UNIT_ID, " +
                   "UNIT_NAME, " +
                   "DEPARTMENT_ID, " +
                   "DEPARTMENT_NAME, " +
                   "SECTION_ID, " +
                   "SECTION_NAME, " +
                   "SUB_SECTION_ID, " +
                   "SUB_SECTION_NAME, " +
                   "FIRST_IN, " +
                   "LAST_OUT, " +
                   "FIRST_LATE_HOUR, " +
                   "FIRST_LATE_MINUTE, " +
                   "LUNCH_OUT, " +
                   "LUNCH_IN, " +
                   "LUNCH_LATE_HOUR, " +
                   "LUNCH_LATE_MINUTE, " +
                   "OT_HOUR, " +
                   "OT_MINUTE, " +
                   "LOG_LATE, " +
                   "LUNCH_LATE, " +
                   "LOG_DATE, " +
                   "PUNCH_CODE, " +
                   "DAY_TYPE, " +
                   "REMARKS, " +

                   "UPDATE_DATE, " +
                   "HEAD_OFFICE_ID, " +
                   "HEAD_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ID, " +
                   "BRANCH_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ADDRESS, " +
                   "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objAttendenceReportModel.UpdateBy + "') UPDATE_BY, " +

                   "LATE_YN, " +
                   "late_status, " +
                   "PUNCTUAL_YN, " +
                   "PUNCTUAL_STATUS, " +
                   "LATE_HMS, " +
                   "total_duty_time, " +
                   "EARLY_OUT_TIME, " +

                   "shift_in_time, " +
                   "shift_out_time, " +
                   "late_hm " +

                   "from VEW_RPT_DAILY_LATE_SHEET  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') ";


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_DAILY_LATE_SHEET");
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
        public DataSet DailyAbsentSheet(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +

   "'Absent Information '|| 'From  '|| to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to ' || to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
   "EMPLOYEE_ID, " +
   "EMPLOYEE_NAME, " +
   "JOINING_DATE, " +
   "DESIGNATION_NAME, " +
   "UNIT_ID, " +
   "UNIT_NAME, " +
   "DEPARTMENT_ID, " +
   "DEPARTMENT_NAME, " +
   "SECTION_ID, " +
   "SECTION_NAME, " +
   "SUB_SECTION_ID, " +
   "SUB_SECTION_NAME, " +
   "FIRST_IN, " +
   "LAST_OUT, " +
   "FIRST_LATE_HOUR, " +
   "FIRST_LATE_MINUTE, " +
   "LUNCH_OUT, " +
   "LUNCH_IN, " +
   "LUNCH_LATE_HOUR, " +
   "LUNCH_LATE_MINUTE, " +
   "OT_HOUR, " +
   "OT_MINUTE, " +
   "LOG_LATE, " +
   "LUNCH_LATE, " +
   "LOG_DATE, " +
   "PUNCH_CODE, " +
   "DAY_TYPE, " +
   "REMARKS, " +

   "UPDATE_DATE, " +
   "HEAD_OFFICE_ID, " +
   "HEAD_OFFICE_NAME, " +
   "BRANCH_OFFICE_ID, " +
   "BRANCH_OFFICE_NAME, " +
   "BRANCH_OFFICE_ADDRESS, " +
   "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objAttendenceReportModel.UpdateBy + "') UPDATE_BY, " +

   "LATE_YN, " +
   "late_status, " +
   "PUNCTUAL_YN, " +
   "PUNCTUAL_STATUS, " +
   "LATE_HMS, " +
   "total_duty_time, " +
   "EARLY_OUT_TIME " +

   "from VEW_RPT_DAILY_ABSENT_SHEET  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') ";


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

                    }


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeTypeId))
                    {
                        sql = sql + "and employee_type_id = '" + objAttendenceReportModel.EmployeeTypeId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_DAILY_ABSENT_SHEET");
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
        public DataSet IndividualAttendanceSheet(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +

  "'Individual Attendence History '|| 'From  '|| to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to ' || to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
  "emp_title, " +
   "EMPLOYEE_ID, " +
   "EMPLOYEE_NAME, " +
   "JOINING_DATE, " +
   "DESIGNATION_NAME, " +
   "UNIT_ID, " +
   "UNIT_NAME, " +
   "DEPARTMENT_ID, " +
   "DEPARTMENT_NAME, " +
   "SECTION_ID, " +
   "SECTION_NAME, " +
   "SUB_SECTION_ID, " +
   "SUB_SECTION_NAME, " +
   "PUNCH_CODE, " +
   "LOG_DATE, " +
   "FIRST_IN, " +
   "LAST_OUT, " +
   "TOTAL_DUTY_TIME, " +
   "TOTAL_EARLY_OUT_TIME, " +
   "TOTAL_LATE_TIME, " +
   "LATE_YN, " +
   "PUNCTUAL_YN, " +
   "ABSENT_YN, " +
   "LEAVE_YN, " +
   "HOLIDAY_YN, " +
   "DAY_TYPE, " +
   "REMARKS, " +
   "UPDATE_DATE, " +
   "HEAD_OFFICE_ID, " +
   "BRANCH_OFFICE_ID, " +
   "LEAVE_TYPE_ID, " +
   "DAY_NAME, " +
   "TOTAL_LATE_DAY, " +
   "MONTH_DAY, " +
   "WORKING_DAY, " +
   "HEAD_OFFICE_NAME, " +
   "BRANCH_OFFICE_NAME, " +
   "BRANCH_OFFICE_ADDRESS, " +
   "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objAttendenceReportModel.UpdateBy + "') UPDATE_BY, " +
    "DAILY_DUTY_TIME, " +
    "DAILY_EARLY_OUT_TIME, " +
    "DAILY_LATE_TIME, " +
   "emp_id, " +
   "emp_name, " +
   "emp_joining_date, " +
   "emp_designation, " +
   "emp_department, " +
   "emp_date_of_birth, " +
   "SHIFT_IN_TIME, " +
   "SHIFT_OUT_TIME, " +
   "SHIFT_STATUS, " +
   "WORKING_HOUR, " +
   "WORKING_MINUTE, " +
   "WORKING_SECOND,  " +
   "TOTAL_PRESENT_DAY,  " +
   "TOTAL_ABSENT_DAY, " +
   "TOTAL_HOLIDAY, " +
   "TOTAL_WORKING_HOUR, " +
   "TOTAL_WORKING_DAY, " +

   "TOTAL_LATE_HOUR, " +
   "TOTAL_LATE_MINUTE, " +
   "TOTAL_LATE_SECOND, " +
   "TOTAL_LATE_HMS, " +
   "PUNCH_STATUS, " +
   "TOTAL_WORKING_HOUR, " +
   "TOTAL_WORKING_MINUTE, " +
   "TOTAL_WORKING_SECOND, " +
   "TOTAL_WORKING_STATUS, " +
   "AVG_DUTY_TIME," +
   "TOTAL_EARLY_OUT_HOUR, " +
   "TOTAL_EARLY_OUT_MINUTE, " +
   "TOTAL_EARLY_OUT_SECOND, " +
   "TOTAL_EARLY_OUT_STATUS, " +
   "color_status, " +

   "(SELECT COUNT(*) " +
    "                       FROM employee_leave " +
     "                     WHERE leave_type_id = '3' " +
      "                          AND leave_start_date BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') " +
       "                         AND employee_id = '" + objAttendenceReportModel.EmployeeId + "') total_cl,  " +

          "(SELECT COUNT(*) " +
    "                       FROM employee_leave " +
     "                     WHERE leave_type_id = '4' " +
      "                          AND leave_start_date BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') " +
       "                         AND employee_id = '" + objAttendenceReportModel.EmployeeId + "') total_sl,  " +

                 "(SELECT COUNT(*) " +
    "                       FROM employee_leave " +
     "                     WHERE leave_type_id = '5' " +
      "                          AND leave_start_date BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') " +
       "                         AND employee_id = '" + objAttendenceReportModel.EmployeeId + "') total_lwp,  " +


                        "(SELECT COUNT(*) " +
    "                       FROM employee_leave " +
     "                     WHERE leave_type_id = '6' " +
      "                          AND leave_start_date BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') " +
       "                         AND employee_id = '" + objAttendenceReportModel.EmployeeId + "') total_ml,  " +



                               "(SELECT COUNT(*) " +
    "                       FROM employee_leave " +
     "                     WHERE leave_type_id = '10' " +
      "                          AND leave_start_date BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') " +
       "                         AND employee_id = '" + objAttendenceReportModel.EmployeeId + "') total_el,  " +



                                      "(SELECT COUNT(*) " +
    "                       FROM employee_leave " +
     "                     WHERE leave_type_id = '7' " +
      "                          AND leave_start_date BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') " +
       "                         AND employee_id = '" + objAttendenceReportModel.EmployeeId + "') total_pl,  " +



      " (SELECT (SELECT COUNT(*) " +
      "    FROM employee_attendance " +
       "  WHERE leave_type_id = '9' " +
     "     AND log_date BETWEEN TO_DATE ('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') " +
                    "              AND TO_DATE ('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') " +
            "   AND employee_id = '" + objAttendenceReportModel.EmployeeId + "' ) " +
     "  + (SELECT COUNT(*) " +
         "   FROM employee_attendance " +
         "  WHERE holiday_type_id = '2' " +
            "     AND log_date BETWEEN TO_DATE ('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') " +
                    "              AND TO_DATE ('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') " +
                " AND employee_id = '" + objAttendenceReportModel.EmployeeId + "')  " +




 " FROM DUAL)total_govt_holiday, " +






                                      "(SELECT COUNT(*) " +
    "                       FROM vew_rpt_individual_attendance where " +
      "                           log_date BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') and WEEKLY_HOLIDAY_YN ='Y' " +
       "                         AND employee_id = '" + objAttendenceReportModel.EmployeeId + "' and leave_type_id is null and (shift_in_time is null and shift_out_time is null) ) total_weekly_holiday,  " +

         " TOTAL_WORKING_HOUR, " +
  "TOTAL_WORKING_MINUTE, " +
  "TOTAL_WORKING_SECOND," +
  "total_working_status, " +
  "AVG_DUTY_TIME, " +
  "TOTAL_EARLY_OUT_HOUR, " +
  "TOTAL_EARLY_OUT_MINUTE, " +
  "TOTAL_EARLY_OUT_SECOND, " +
  "total_early_out_status, " +
  "employee_pic, " +
  "EL_DUE                  , " +
  "CL_DUE                  , " +
  "SL_DUE                  ," +
  "TOTAL_DEDUCTED_DAY      ," +
  "TOTAL_EL_DUE, " +
  "TOTAL_ALTERNATIVE_HOLIDAY " +


   "from vew_rpt_individual_attendance  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";


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
                            objDataAdapter.Fill(ds, "vew_rpt_individual_attendance");
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
        public DataSet IndividualLateSheet(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
                   "'Late History Between '|| '-  '|| to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') RPT_TITLE, " +
                   "CARD_NO, " +
                   "RPT_TITLE, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "JOINING_DATE, " +
                   "DESIGNATION_NAME, " +
                   "UNIT_ID, " +
                   "UNIT_NAME, " +
                   "DEPARTMENT_ID, " +
                   "DEPARTMENT_NAME, " +
                   "SECTION_ID, " +
                   "SECTION_NAME, " +
                   "SUB_SECTION_ID, " +
                   "SUB_SECTION_NAME, " +
                   "FIRST_IN, " +
                   "LAST_OUT, " +
                   "FIRST_LATE_HOUR, " +
                   "FIRST_LATE_MINUTE, " +
                   "LUNCH_OUT, " +
                   "LUNCH_IN, " +
                   "LUNCH_LATE_HOUR, " +
                   "LUNCH_LATE_MINUTE, " +
                   "OT_HOUR, " +
                   "OT_MINUTE, " +
                   "LOG_LATE, " +
                   "LUNCH_LATE, " +
                   "LOG_DATE, " +
                   "PUNCH_CODE, " +
                   "DAY_TYPE, " +
                   "REMARKS, " +
                   "UPDATE_BY, " +
                   "UPDATE_DATE, " +
                   "HEAD_OFFICE_ID, " +
                   "HEAD_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ID, " +
                   "BRANCH_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ADDRESS, " +
                   "LATE_YN, " +
                   "LATE_STATUS, " +
                   "PUNCTUAL_YN, " +
                   "PUNCTUAL_STATUS, " +
                   "LATE_HMS, " +
                   "TOTAL_DUTY_TIME, " +
                   "EARLY_OUT_TIME, " +
                   "ABSENT_YN, " +
                   "SHIFT_IN_TIME, " +
                   "SHIFT_OUT_TIME, " +
                   "LATE_HM, " +
                   "EMP_ID, " +
                   "EMP_NAME, " +
                   "EMP_JOINING_DATE, " +
                   "EMP_DESIGNATION, " +
                   "EMP_DEPARTMENT, " +
                   "EMP_DATE_OF_BIRTH, " +
                   "file_size, " +
                   "DAY_NAME " +

                   "from VEW_RPT_INDIVIDUAL_LATE_SHEET  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";



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
                            objDataAdapter.Fill(ds, "VEW_RPT_INDIVIDUAL_LATE_SHEET");
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
        public DataSet IndividualDutyRoastingSheet(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                   "'Individual Duty Roasting from  ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                   "RPT_TITLE, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "DESIGNATION_NAME, " +
                   "JOINING_DATE, " +
                   "DEPARTMENT_NAME, " +
                   "UNIT_NAME, " +
                   "SECTION_NAME, " +
                   "SUB_SECTION_NAME, " +
                   "LOG_DATE, " +
                   "DAY_NAME, " +
                   "FIRST_IN_TIME, " +
                   "LAST_OUT_TIME, " +
                   "LUNCH_OUT_TIME, " +
                   "LUNCH_IN_TIME, " +
                   "CREATE_BY, " +
                   "CREATE_DATE, " +
                   "UPDATE_BY, " +
                   "UPDATE_DATE, " +
                   "HEAD_OFFICE_ID, " +
                   "HEAD_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ID, " +
                   "BRANCH_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ADDRESS, " +
                   "NEW_EMP_ID, " +
                   "NEW_EMP, " +
                   "NEW_JOINING_DATE, " +
                   "NEW_DEPARTMENT_NAME, " +
                   "NEW_ESIGNATION_NAME, " +
                   "NEW_DATE_OF_BRITH " +

                   "from VEW_RPT_INDIVIDUAL_ROASTING  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')   and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_INDIVIDUAL_ROASTING");
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
        public DataSet DetailAttendanceSheet(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                    "'Detail Attendence Histroy from  ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                    "EMP_IDENTITY, " +
                    "EMPLOYEE_ID, " +
                    "EMPLOYEE_NAME, " +
                    "JOINING_DATE, " +
                    "DESIGNATION_NAME, " +
                    "UNIT_ID, " +
                    "DEPARTMENT_ID, " +
                    "DEPARTMENT_NAME, " +
                    "SECTION_ID, " +
                    "SECTION_NAME, " +
                    "SUB_SECTION_ID, " +
                    "SUB_SECTION_NAME, " +
                    "LOG_DATE1, " +
                    "FIRST_IN1, " +
                    "LAST_OUT1, " +
                    "LOG_DATE2, " +
                    "FIRST_IN2, " +
                    "LAST_OUT2, " +
                    "LOG_DATE3, " +
                    "FIRST_IN3, " +
                    "LAST_OUT3, " +
                    "LOG_DATE4, " +
                    "FIRST_IN4, " +
                    "LAST_OUT4, " +
                    "LOG_DATE5, " +
                    "FIRST_IN5, " +
                    "LAST_OUT5, " +
                    "LOG_DATE6, " +
                    "FIRST_IN6, " +
                    "LAST_OUT6, " +
                    "LOG_DATE7, " +
                    "FIRST_IN7, " +
                    "LAST_OUT7, " +
                    "LOG_DATE8, " +
                    "FIRST_IN8, " +
                    "LAST_OUT8, " +
                    "LOG_DATE9, " +
                    "FIRST_IN9, " +
                    "LAST_OUT9, " +
                    "LOG_DATE10, " +
                    "FIRST_IN10, " +
                    "LAST_OUT10, " +
                    "LOG_DATE11, " +
                    "FIRST_IN11, " +
                    "LAST_OUT11, " +
                    "LOG_DATE12, " +
                    "FIRST_IN12," +
                    "LAST_OUT12, " +
                    "LOG_DATE13, " +
                    "FIRST_IN13, " +
                    "LAST_OUT13, " +
                    "LOG_DATE14, " +
                    "FIRST_IN14, " +
                    "LAST_OUT14, " +
                    "LOG_DATE15, " +
                    "FIRST_IN15, " +
                    "LAST_OUT15, " +
                    "LOG_DATE16, " +
                    "FIRST_IN16, " +
                    "LAST_OUT16, " +
                    "LOG_DATE17, " +
                    "FIRST_IN17, " +
                    "LAST_OUT17, " +
                    "LOG_DATE18, " +
                    "FIRST_IN18, " +
                    "LAST_OUT18, " +
                    "LOG_DATE19, " +
                    "FIRST_IN19, " +
                    "LAST_OUT19, " +
                    "LOG_DATE20, " +
                    "FIRST_IN20, " +
                    "LAST_OUT20, " +
                    "LOG_DATE21, " +
                    "FIRST_IN21, " +
                    "LAST_OUT21, " +
                    "LOG_DATE22, " +
                    "FIRST_IN22, " +
                    "LAST_OUT22, " +
                    "LOG_DATE23, " +
                    "FIRST_IN23, " +
                    "LAST_OUT23, " +
                    "LOG_DATE24, " +
                    "FIRST_IN24, " +
                    "LAST_OUT24, " +
                    "LOG_DATE25, " +
                    "FIRST_IN25, " +
                    "LAST_OUT25, " +
                    "LOG_DATE26, " +
                    "FIRST_IN26, " +
                    "LAST_OUT26, " +
                    "LOG_DATE27, " +
                    "FIRST_IN27, " +
                    "LAST_OUT27, " +
                    "LOG_DATE28, " +
                    "FIRST_IN28, " +
                    "LAST_OUT28, " +
                    "LOG_DATE29, " +
                    "FIRST_IN29, " +
                    "LAST_OUT29, " +
                    "LOG_DATE30, " +
                    "FIRST_IN30, " +
                    "LAST_OUT30, " +
                    "LOG_DATE31, " +
                    "FIRST_IN31, " +
                    "LAST_OUT31, " +
                    "UPDATE_BY,  " +
                    "UPDATE_DATE, " +
                    "HEAD_OFFICE_ID, " +
                    "HEAD_OFFICE_NAME, " +
                    "BRANCH_OFFICE_ID, " +
                    "BRANCH_OFFICE_NAME, " +
                    "BRANCH_OFFICE_ADDRESS " +



                    "from VEW_RPT_ATTENDACE_DETAIL  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE1 BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') ";


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_ATTENDACE_DETAIL");
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
        public DataSet PunctualAttendanceInTime(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                   "'Top ' || '" + objAttendenceReportModel.TotalEmployee + "' || '  Punctual In Employers  Between  ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                   "EMPLOYEE_ID, " +
                   "CARD_NO, " +
                   "EMPLOYEE_NAME, " +
                   "JOINING_DATE, " +
                   "DESIGNATION_NAME, " +
                   "PUNCH_CODE, " +

                   "TOTAL_DAY, " +
                   "UNIT_ID, " +
                   "DEPARTMENT_ID, " +
                   "SECTION_ID, " +
                   "SUB_SECTION_ID, " +
                   "CREATE_BY, " +
                   "CREATE_DATE, " +
                   "UPDATE_BY, " +
                   "UPDATE_DATE, " +
                   "HEAD_OFFICE_ID, " +
                   "HEAD_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ID, " +
                   "BRANCH_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ADDRESS, " +
                   "from_date, " +
                   "end_Date, " +
                   "section_name, " +
                   "sub_section_name, " +
                   "department_name " +
                   "from vew_rpt_puntual_sheet_in  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and FROM_DATE = to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and end_date = to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') and rownum <= '" + objAttendenceReportModel.TotalEmployee + "' ";


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "vew_rpt_puntual_sheet_in");
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
        public DataSet PunctualAttendanceOutTime(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +

                        "'Top ' || '" + objAttendenceReportModel.TotalEmployee + "' || '  Punctual Out Employers  Between  ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                       "EMPLOYEE_ID, " +
                       "CARD_NO, " +
                       "EMPLOYEE_NAME, " +
                       "JOINING_DATE, " +
                       "DESIGNATION_NAME, " +
                       "PUNCH_CODE, " +
                       "TOTAL_DAY, " +
                       "UNIT_ID, " +
                       "DEPARTMENT_ID, " +
                       "SECTION_ID, " +
                       "SUB_SECTION_ID, " +
                       "CREATE_BY, " +
                       "CREATE_DATE, " +
                       "UPDATE_BY, " +
                       "UPDATE_DATE, " +
                       "HEAD_OFFICE_ID, " +
                       "HEAD_OFFICE_NAME, " +
                       "BRANCH_OFFICE_ID, " +
                       "BRANCH_OFFICE_NAME, " +
                       "BRANCH_OFFICE_ADDRESS, " +
                       "from_date, " +
                       "end_Date, " +
                       "section_name, " +
                       "sub_section_name, " +
                       "department_name " +

                        "from VEW_RPT_PUNTUAL_SHEET_OUT  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and FROM_DATE = to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and end_date = to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') and rownum <= '" + objAttendenceReportModel.TotalEmployee + "' ";


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "vew_rpt_puntual_sheet_out");
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
        public DataSet TopLateSheet(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +
                   "'Top ' || '" + objAttendenceReportModel.TotalEmployee + "' || '  Late Employee  Between  ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                   "EMPLOYEE_ID, " +
                   "CARD_NO, " +
                   "EMPLOYEE_NAME, " +
                   "JOINING_DATE, " +
                   "DESIGNATION_NAME, " +
                   "PUNCH_CODE, " +
                   "TOTAL_DAY, " +
                   "UNIT_ID, " +
                   "DEPARTMENT_ID, " +
                   "SECTION_ID, " +
                   "SUB_SECTION_ID, " +
                   "CREATE_BY, " +
                   "CREATE_DATE, " +
                   "UPDATE_BY, " +
                   "UPDATE_DATE, " +
                   "HEAD_OFFICE_ID, " +
                   "HEAD_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ID, " +
                   "BRANCH_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ADDRESS, " +
                   "from_date, " +
                   "end_Date, " +
                   "late_hour, " +
                   "late_minute, " +
                   "late_second, " +
                   "total_late_time, " +
                   "SUB_SECTION_NAME, " +
                   "SECTION_NAME, " +
                   "DEPARTMENT_NAME " +
                    "from VEW_RPT_TOP_LATE_SHEET  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and FROM_DATE = to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and end_date = to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') and rownum <= '" + objAttendenceReportModel.TotalEmployee + "' ";


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_TOP_LATE_SHEET");
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
        public DataSet TopEarlyOutSheet(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +
                   "'Top ' || '" + objAttendenceReportModel.TotalEmployee + "' || '  Employers who left office early Between  ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                   "EMPLOYEE_ID, " +
                   "CARD_NO, " +
                   "EMPLOYEE_NAME, " +
                   "JOINING_DATE, " +
                   "DESIGNATION_NAME, " +
                   "PUNCH_CODE, " +
                   "TOTAL_DAY, " +
                   "TOTAL_EARLY_OUT_TIME, " +
                   "UNIT_ID, " +
                   "unit_name, " +
                   "DEPARTMENT_ID, " +
                   "department_name, " +
                   "SECTION_ID, " +
                   "section_name, " +
                   "SUB_SECTION_ID, " +
                   "sub_section_name, " +
                   "CREATE_BY, " +
                   "CREATE_DATE, " +
                   "UPDATE_BY, " +
                   "UPDATE_DATE, " +
                   "HEAD_OFFICE_ID, " +
                   "HEAD_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ID, " +
                   "BRANCH_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ADDRESS, " +
                   "from_date, " +
                   "end_Date, " +
                   "early_out_hour, " +
                   "early_out_minute, " +
                   "early_out_second " +
                   "from VEW_RPT_EARLY_OUT  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and FROM_DATE = to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and end_date = to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') and rownum <= '" + objAttendenceReportModel.TotalEmployee + "' ";


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_EARLY_OUT");
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
        public DataSet TopHigestDutySheet(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +
                   "'Top ' || '" + objAttendenceReportModel.TotalEmployee + "' || '  Highest Duty Time Employers  Between  ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' to '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                   "EMPLOYEE_ID, " +
                   "CARD_NO, " +
                   "EMPLOYEE_NAME, " +
                   "JOINING_DATE, " +
                   "DESIGNATION_NAME, " +
                   "PUNCH_CODE, " +
                   "TOTAL_DAY, " +
                   "TOTAL_DUTY_TIME, " +
                   "UNIT_ID, " +
                   "DEPARTMENT_ID, " +
                   "SECTION_ID, " +
                   "SUB_SECTION_ID, " +
                   "CREATE_BY, " +
                   "CREATE_DATE, " +
                   "UPDATE_BY, " +
                   "UPDATE_DATE, " +
                   "HEAD_OFFICE_ID, " +
                   "HEAD_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ID, " +
                   "BRANCH_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ADDRESS, " +
                   "from_date, " +
                   "end_Date, " +
                   "working_hour, " +
                   "working_minute, " +
                   "working_second, " +
                   "section_name, " +
                   "sub_section_name, " +
                   "department_name " +

                   "from VEW_RPT_MAXIMUM_DUTY  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and FROM_DATE = to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and end_date = to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy') and rownum <= '" + objAttendenceReportModel.TotalEmployee + "' ";


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_MAXIMUM_DUTY");
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
        public DataSet AttendanceSummary(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +

                   "'Atendance Summery Between ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' and '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                   "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "JOINING_DATE, " +
                   "DESIGNATION_NAME, " +
                   "UNIT_ID, " +
                   "UNIT_NAME, " +
                   "DEPARTMENT_ID, " +
                   "DEPARTMENT_NAME, " +
                   "SECTION_ID, " +
                   "SECTION_NAME, " +
                   "SUB_SECTION_ID, " +
                   "SUB_SECTION_NAME, " +
                   "TOTAL_WORKING_DAYS, " +
                   "TOTAL_FIRST_LATE, " +
                   "TOTAL_EL_LATE, " +
                   "TOTAL_LATE, " +
                   "TOTAL_DUTY_HOUR, " +
                   "AVG_DUTY_HOUR, " +
                   "TOTAL_ABSENT, " +
                   "TOTAL_CL, " +
                   "TOTAL_SL, " +
                   "TOTAL_EL, " +
                   "TOTAL_PL, " +
                   "TOTAL_ML, " +
                   "TOTAL_LWP, " +
                   "CL_DUE, " +
                   "SL_DUE, " +
                   "EL_DUE, " +
                   "TOTAL_EL_DUE, " +
                   "CREATE_BY, " +
                   "CREATE_DATE, " +
                   "UPDATE_BY, " +
                   "UPDATE_DATE, " +
                   "HEAD_OFFICE_ID, " +
                   "HEAD_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ID, " +
                   "BRANCH_OFFICE_NAME, " +
                   "BRANCH_OFFICE_ADDRESS, " +
                   "TOTAL_WH, " +
                   "TOTAL_GH, " +
                   "card_no, " +
                   "WEAVER_ON_TOTAL_LATE, " +
                   "TOTAL_DEDUCTED_DAY, " +
                   "log_date_from, " +
                   "log_date_to " +


                   "from VEW_RPT_ATTENDANCE_SUMMERY  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "'  ";


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeTypeId))
                    {
                        sql = sql + "and employee_type_id = '" + objAttendenceReportModel.EmployeeTypeId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_ATTENDANCE_SUMMERY");
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
        public DataSet AttendenceLogHistory(AttendenceReportModel objAttendenceReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
                    "'Atendance Summery Between ' || to_date( '" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') || ' and '|| to_date( '" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                    "RPT_TITLE, " +
                    "SL, " +
                    "EMPLOYEE_ID, " +
                    "EMPLOYEE_NAME, " +
                    "DESIGNATION_NAME, " +
                    "PUNCH_CODE, " +
                    "LOG_DATE, " +
                    "FIRST_IN, " +
                    "LAST_OUT, " +
                    "LUNCH_OUT, " +
                    "LUNCH_IN, " +
                    "ALL_YN_YN, " +
                    "UPDATE_BY, " +
                    "UPDATE_DATE, " +
                    "HEAD_OFFICE_ID, " +
                    "HEAD_OFFICE_NAME, " +
                    "BRANCH_OFFICE_ID, " +
                    "BRANCH_OFFICE_NAME, " +
                    "BRANCH_OFFICE_ADDRESS, " +
                    "REMARKS, " +
                    "CREATE_BY, " +
                    "CREATE_DATE, " +
                    "DEPARTMENT_ID, " +
                    "DEPARTMENT_NAME, " +
                    "UNIT_ID, " +
                    "SECTION_ID, " +
                    "SECTION_NAME, " +
                    "SUB_SECTION_ID, " +
                    "APPROVED_EMPLOYEE_ID, " +
                    "ACTIVE_YN, " +
                    "JOINING_DATE, " +
                    "CARD_NO, " +
                    "MANUAL_YN, " +
                    "PUNCH_STATUS, " +
                    "DAY_TYPE_ID, " +
                    "DAY_TYPE_NAME, " +
                    "ACTIVE_STATUS, " +
                    "MISSING_YN, " +
                    "ABSENT_YN, " +
                    "PUNTCH_TYPE_TATUS, " +
                    "UPDATE_EMP_NAME " +
                    "from VEW_RPT_ATTENDANCE_LOG  where head_office_id = '" + objAttendenceReportModel.HeadOfficeId + "' AND branch_office_id = '" + objAttendenceReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objAttendenceReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objAttendenceReportModel.ToDate + "', 'dd/mm/yyyy')  ";


                    if (!string.IsNullOrEmpty(objAttendenceReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objAttendenceReportModel.EmployeeId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objAttendenceReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objAttendenceReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objAttendenceReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objAttendenceReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objAttendenceReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_ATTENDANCE_LOG");
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
        #region Employee Job Confirmation Report
        public DataSet EmployeeJobConfirmationDetail(EmployeeModel objEmployeeModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +

   "'Employee Job Confirmation List '|| 'From  '|| to_date( '" + objEmployeeModel.SearchFromDate + "', 'dd/mm/yyyy') || ' to ' || to_date('" + objEmployeeModel.SearchToDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +


                        " EMPLOYEE_ID, " +
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
 "(select employee_name from EMPLOYEE_BASIC where EMPLOYEE_ID = '" + objEmployeeModel.UpdateBy + "') UPDATE_BY " +

                        "from VEW_EMPLOYEE_INFORMATION  where head_office_id = '" + objEmployeeModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeModel.BranchOfficeId + "' and active_yn = 'Y' ";


                    if (objEmployeeModel.EmployeeId.Length > 0)
                    {
                        sql = sql + "and employee_id = '" + objEmployeeModel.EmployeeId + "' ";

                    }

                    if (objEmployeeModel.DepartmentId.Length > 0)
                    {
                        sql = sql + "and department_id = '" + objEmployeeModel.DepartmentId + "' ";

                    }

                    if (objEmployeeModel.UnitId.Length > 0)
                    {
                        sql = sql + "and unit_id = '" + objEmployeeModel.UnitId + "' ";

                    }

                    if (objEmployeeModel.SectionId.Length > 0)
                    {
                        sql = sql + "and section_id = '" + objEmployeeModel.SectionId + "' ";

                    }

                    if (objEmployeeModel.SubSectionId.Length > 0)
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeModel.SubSectionId + "' ";

                    }


                    if (objEmployeeModel.SearchFromDate.Length > 6 && objEmployeeModel.SearchToDate.Length > 6)
                    {
                        sql = sql + "and JOB_CONFIRMATION_DATE between to_date( '" + objEmployeeModel.SearchFromDate + "', 'dd/mm/yyyy') and to_date( '" + objEmployeeModel.SearchToDate + "', 'dd/mm/yyyy')  ";

                    }



                    //sql = sql + " order by to_number(card_no)";






                    //sql = sql + " order by to_number(card_no)";


                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
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



        //name: mezba & date: 17.01.2019
        #region Employee Report

        public DataSet EmployeeBasicInformaiton(EmployeeReportModel objEmployeeReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
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
                           "RPT_TITLE, " +
                           "EMPLOYEEE_PIC, " +
                           "EMPLOYEEE_SIGNATURE, " +
                           "DEPARTMENT_NAME, " +
                        "(select employee_name from EMPLOYEE_BASIC where EMPLOYEE_ID = '" + objEmployeeReportModel.UpdateBy + "') UPDATE_BY " +
                        "from VEW_RPT_EMPLOYEE_INFORMATION  where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "'";


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_EMPLOYEE_INFORMATION");
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

        public DataSet MaleFemaleInformaiton(EmployeeReportModel objEmployeeReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
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
                           " 'Gender Type Information' RPT_TITLE, " +
                           "EMPLOYEEE_PIC, " +
                           "EMPLOYEEE_SIGNATURE, " +
                           "DEPARTMENT_NAME, " +
                        "(select employee_name from EMPLOYEE_BASIC where EMPLOYEE_ID = '" + objEmployeeReportModel.UpdateBy + "') UPDATE_BY " +
                        "from VEW_RPT_EMPLOYEE_INFORMATION  where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "'";


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.GenderId))
                    {
                        sql = sql + "and gender_id = '" + objEmployeeReportModel.GenderId + "' ";

                    }


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objEmployeeReportModel.EmployeeId + "' ";

                    }


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                    }


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                    }


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_EMPLOYEE_INFORMATION");
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

        public DataSet EmployeePaySlip(EmployeeReportModel objEmployeeReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
                           " EMPLOYEE_ID, " +
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
                           "RPT_TITLE, " +
                           "EMPLOYEEE_PIC, " +
                           "EMPLOYEEE_SIGNATURE, " +
                           "(select employee_name from EMPLOYEE_BASIC where EMPLOYEE_ID = '" + objEmployeeReportModel.UpdateBy + "') UPDATE_BY " +
                           "from VEW_EMPLOYEE_INFORMATION  where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "'";


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                    }


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_EMPLOYEE_INFORMATION");
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

        public DataSet EmployeeDetailInformaiton(EmployeeReportModel objEmployeeReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
                           " EMPLOYEE_ID, " +
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
                           "RPT_TITLE, " +
                           "EMPLOYEEE_PIC, " +
                           "EMPLOYEEE_SIGNATURE, " +
                           "department_name, " +

                    "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objEmployeeReportModel.UpdateBy + "') UPDATE_BY " +

                    "from VEW_RPT_EMPLOYEE_INFORMATION  where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "'";


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";
                    }


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                    }


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                    }


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_EMPLOYEE_INFORMATION");
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

        public DataSet EmployeeJobConfirmationInformation(EmployeeReportModel objEmployeeReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
                           "'Employee Job Confirmation List '|| 'From  '|| to_date( '" + objEmployeeReportModel.FromDate.Trim() + "', 'dd/mm/yyyy') || ' to ' || to_date('" + objEmployeeReportModel.ToDate.Trim() + "', 'dd/mm/yyyy')  RPT_TITLE, " +
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
                         "(select employee_name from EMPLOYEE_BASIC where EMPLOYEE_ID = '" + objEmployeeReportModel.UpdateBy + "') UPDATE_BY " +
                        "from VEW_EMPLOYEE_INFORMATION  where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "' and active_yn = 'Y' ";


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";

                    }
                    if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

                    }
                    if (!string.IsNullOrEmpty(objEmployeeReportModel.FromDate) && (!string.IsNullOrEmpty(objEmployeeReportModel.ToDate)))
                    {
                        sql = sql + "and JOB_CONFIRMATION_DATE between to_date( '" + objEmployeeReportModel.FromDate + "', 'dd/mm/yyyy') and to_date( '" + objEmployeeReportModel.ToDate + "', 'dd/mm/yyyy')  ";

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

        public DataSet EmployeeActiveList(EmployeeReportModel objEmployeeReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
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
                           "DEPARTMENT_NAME, " +
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
                           " 'Active Employee List' RPT_TITLE, " +
                           "EMPLOYEEE_PIC, " +
                           "EMPLOYEEE_SIGNATURE, " +
                          "(select employee_name from EMPLOYEE_BASIC where EMPLOYEE_ID = '" + objEmployeeReportModel.UpdateBy + "') UPDATE_BY " +
                          "from VEW_RPT_EMPLOYEE_INFORMATION  where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "' and active_yn = 'Y' ";


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_EMPLOYEE_INFORMATION");
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

        public DataSet EmployeeInActiveList(EmployeeReportModel objEmployeeReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
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
                           "DEPARTMENT_NAME, " +
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
                           "'Inactive Employee List Between ' || to_date('" + objEmployeeReportModel.FromDate.Trim() + "', 'dd/mm/yyyy')|| ' and '|| to_date('" + objEmployeeReportModel.ToDate.Trim() + "', 'dd/mm/yyyy') RPT_TITLE, " +
                           "EMPLOYEEE_PIC, " +
                           "EMPLOYEEE_SIGNATURE, " +
                        "(select employee_name from EMPLOYEE_BASIC where EMPLOYEE_ID = '" + objEmployeeReportModel.UpdateBy + "') UPDATE_BY " +

                        "from VEW_RPT_EMPLOYEE_INFORMATION  where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "' and RESIGN_DATE between to_date('" + objEmployeeReportModel.FromDate.Trim() + "', 'dd/mm/yyyy') and to_date('" + objEmployeeReportModel.ToDate.Trim() + "', 'dd/mm/yyyy') and  active_yn <> 'Y' ";


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_EMPLOYEE_INFORMATION");
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

        public DataSet EmployeeSalaryInformaiton(EmployeeReportModel objEmployeeReportModel)
        {


            DataSet ds = null;
            DataTable dt = new DataTable();


            string sql = "", strMsg = "", sql1 = "", sql2 = "";


            sql = "SELECT 'Y' " +
                    "from VEW_SALARY_PERMISSION where employee_id = '" + objEmployeeReportModel.UpdateBy + "' and permission_yn='Y' AND HEAD_OFFICE_ID = '" + objEmployeeReportModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID ='" + objEmployeeReportModel.BranchOfficeId + "' ";

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



            try
            {


                try
                {
                    if (strMsg == "Y")

                    {

                        sql1 = "SELECT " +

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
                           "RPT_TITLE, " +
                           "EMPLOYEEE_PIC, " +
                           "EMPLOYEEE_SIGNATURE, " +
                           "DEPARTMENT_NAME, " +

                        "(select employee_name from EMPLOYEE_BASIC where EMPLOYEE_ID = '" + objEmployeeReportModel.UpdateBy + "') UPDATE_BY " +

                        "from VEW_RPT_EMPLOYEE_SALARY_INFO  where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "'";


                        if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                        {
                            sql1 = sql1 + "and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";

                        }
                        if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                        {
                            sql1 = sql1 + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                        }
                        if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                        {
                            sql1 = sql1 + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                        }
                        if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                        {
                            sql1 = sql1 + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                        }

                        if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                        {
                            sql1 = sql1 + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

                        }

                        OracleCommand objOracleCommand = new OracleCommand(sql1);
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
                                objDataAdapter.Fill(ds, "VEW_RPT_EMPLOYEE_SALARY_INFO");
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


                    else
                    {
                        sql2 = "SELECT " +
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
                           "0 JOINING_SALARY, " +
                           "0 FIRST_SALARY, " +
                           "0 GROSS_SALARY, " +
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
                           "RPT_TITLE, " +
                           "EMPLOYEEE_PIC, " +
                           "EMPLOYEEE_SIGNATURE, " +
                           "DEPARTMENT_NAME, " +
                           "(select employee_name from EMPLOYEE_BASIC where EMPLOYEE_ID = '" + objEmployeeReportModel.UpdateBy + "') UPDATE_BY " +
                           "from VEW_RPT_EMPLOYEE_SALARY_INFO  where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "'";


                        if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                        {
                            sql2 = sql2 + "and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";

                        }

                        if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                        {
                            sql2 = sql2 + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                        }

                        if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                        {
                            sql2 = sql2 + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                        }

                        if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                        {
                            sql2 = sql2 + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                        }

                        if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                        {
                            sql2 = sql2 + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

                        }


                        OracleCommand objOracleCommand = new OracleCommand(sql2);
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
                                objDataAdapter.Fill(ds, "VEW_RPT_EMPLOYEE_SALARY_INFO");
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

        public DataSet LeaveIndividual(EmployeeReportModel objEmployeeReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +
                           "'Leave History for the year of '|| '" + objEmployeeReportModel.Year.Trim() + "' RPT_TITLE, " +
                           "EMPLOYEE_ID, " +
                           "EMPLOYEE_NAME, " +
                           "DESIGNATION_ID, " +
                           "DESIGNATION_NAME, " +
                           "LEAVE_TYPE_ID, " +
                           "LEAVE_TYPE_NAME, " +
                           "LEAVE_TYPE, " +
                           "LEAVE_YEAR, " +
                           "TOTAL_LEAVE, " +
                           "MAX_LEAVE, " +
                           "LEAVE_REMAIN, " +
                           "DEPARTMENT_NAME, " +
                           "UNIT_NAME, " +
                           "SECTION_NAME, " +
                           "SUB_SECTION_NAME, " +
                           "HEAD_OFFICE_ID, " +
                           "HEAD_OFFICE_NAME, " +
                           "BRANCH_OFFICE_ID, " +
                           "BRANCH_OFFICE_NAME, " +
                           "BRANCH_OFFICE_ADDRESS, " +
                           "CARD_NO, " +
                           "UNIT_ID, " +
                           "department_id, " +
                           "section_id, " +
                           "sub_section_id, " +
                           "new_emp_id, " +
                           "new_emp_name, " +
                           "new_emp_designation, " +
                           "new_emp_joining_date, " +
                           "new_emp_birth_date, " +
                           "new_emp_department, " +
                           "leave_start_date, " +
                           "leave_end_date, " +
                           "TOTAL_CL_TAKEN, " +
                           "CL_REMAINING, " +
                           "cl_max, " +
                           "TOTAL_SL_TAKEN, " +
                           "SL_REMAINING, " +
                           "sl_max, " +
                           "TOTAL_LWP_TAKEN, " +
                           "LWP_REMAINING," +
                           "lwp_max, " +
                           "TOTAL_ML_TAKEN, " +
                           "ML_REMAINING, " +
                           "ml_max, " +
                           "TOTAL_PL_TAKEN, " +
                           "PL_REMAINING, " +
                           "pl_max, " +
                           "TOTAL_FL_TAKEN, " +
                           "FL_REMAINING, " +
                           "fl_max, " +
                           "TOTAL_GH_TAKEN, " +
                           "GH_REMAINING, " +
                           "gh_max, " +
                           "TOTAL_EL_TAKEN, " +
                           "EL_REMAINING, " +
                           "EL_MAX, " +
                           "employee_pic, " +
                           "LEAVE_TYPE_NAME_SUM, " +
                           "LEAVE_TYPE_SUM_CL, " +
                           "LEAVE_TYPE_SUM_SL, " +
                           "LEAVE_TYPE_SUM_LWP, " +
                           "LEAVE_TYPE_SUM_ML, " +
                           "LEAVE_TYPE_SUM_PL, " +
                           "LEAVE_TYPE_SUM_FL, " +
                           "LEAVE_TYPE_SUM_GH, " +
                           "LEAVE_TYPE_SUM_EL, " +
                           "REMAIN_LEAVE_CL, " +
                           "REMAIN_LEAVE_SL, " +
                           "REMAIN_LEAVE_LWP, " +
                           "REMAIN_LEAVE_ML, " +
                           "REMAIN_LEAVE_PL, " +
                           "REMAIN_LEAVE_FL, " +
                           "REMAIN_LEAVE_GH, " +
                           "REMAIN_LEAVE_EL, " +
                           "cl_leave_name, " +
                           "sl_leave_name, " +
                           "el_leave_name, " +
                           "ml_leave_name, " +
                           "pl_leave_name, " +
                           "lwp_leave_name " +
                           "from VEW_RPT_LEAVE_INDIVIDUAL l where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "' and  leave_year = '" + objEmployeeReportModel.Year.Trim() + "' and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";



                    if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_LEAVE_INDIVIDUAL");
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

        public DataSet LeaveSummeary(EmployeeReportModel objEmployeeReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
                       "RPT_TITLE, " +
                       "EMPLOYEE_ID, " +
                       "EMPLOYEE_NAME, " +
                       "DESIGNATION_ID, " +
                       "DESIGNATION_NAME, " +
                       "LEAVE_TYPE_ID, " +
                       "LEAVE_TYPE_NAME, " +
                       "LEAVE_TYPE, " +
                       "LEAVE_YEAR, " +
                       "TOTAL_LEAVE, " +
                       "MAX_LEAVE, " +
                       "REMAIN_LEAVE, " +
                       "DEPARTMENT_NAME, " +
                       "UNIT_NAME, " +
                       "SECTION_NAME, " +
                       "SUB_SECTION_NAME, " +
                       "HEAD_OFFICE_ID, " +
                       "HEAD_OFFICE_NAME, " +
                       "BRANCH_OFFICE_ID, " +
                       "BRANCH_OFFICE_NAME, " +
                       "BRANCH_OFFICE_ADDRESS, " +
                       "CARD_NO, " +
                       "UNIT_ID, " +
                       "department_id, " +
                       "section_id, " +
                       "sub_section_id " +
                       "from VEW_RPT_LEAVE_SUMMERY  where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "' and LEAVE_YEAR = '" + objEmployeeReportModel.Year.Trim() + "' ";


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";
                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_LEAVE_SUMMERY");
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

        public DataSet DailyLeaveEntry(EmployeeReportModel objEmployeeReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    sql = "SELECT " +
                           "'Daily Leave Input Information '|| 'From  '|| to_date( '" + objEmployeeReportModel.FromDate.Trim() + "', 'dd/mm/yyyy') || ' to ' || to_date('" + objEmployeeReportModel.ToDate.Trim() + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                           "EMPLOYEE_ID, " +
                           "EMPLOYEE_NAME, " +
                           "DESIGNATION_ID, " +
                           "DESIGNATION_NAME, " +
                           "LEAVE_TYPE_ID, " +
                           "LEAVE_TYPE_NAME, " +
                           "LEAVE_TYPE, " +
                           "LEAVE_YEAR, " +
                           "TOTAL_LEAVE, " +
                           "MAX_LEAVE, " +
                           "LEAVE_REMAIN, " +
                           "DEPARTMENT_ID, " +
                           "DEPARTMENT_NAME, " +
                           "UNIT_ID, " +
                           "UNIT_NAME, " +
                           "SECTION_ID, " +
                           "SECTION_NAME, " +
                           "SUB_SECTION_ID, " +
                           "SUB_SECTION_NAME, " +
                           "HEAD_OFFICE_ID, " +
                           "HEAD_OFFICE_NAME, " +
                           "BRANCH_OFFICE_ID, " +
                           "BRANCH_OFFICE_NAME, " +
                           "BRANCH_OFFICE_ADDRESS, " +
                           "CARD_NO, " +
                           "NEW_EMP_ID, " +
                           "NEW_EMP_NAME, " +
                           "NEW_EMP_DESIGNATION, " +
                           "NEW_EMP_JOINING_DATE, " +
                           "NEW_EMP_BIRTH_DATE, " +
                           "NEW_EMP_DEPARTMENT, " +
                           "LEAVE_START_DATE, " +
                           "LEAVE_END_DATE, " +
                           "CREATE_DATE, " +
                           "CREATE_BY, " +
                           "UPDATE_DATE, " +
                           "UPDATE_BY " +
                            "from VEW_RPT_LEAVE_DAILY_INPUT l where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "' and LEAVE_START_DATE between  to_Date( '" + objEmployeeReportModel.FromDate.Trim() + "', 'dd/mm/yyyy') and  to_Date( '" + objEmployeeReportModel.ToDate.Trim() + "', 'dd/mm/yyyy')  ";


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";

                    }


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                    }


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_LEAVE_DAILY_INPUT");
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

        public DataSet EmployeeJoiningDateInformation(EmployeeReportModel objEmployeeReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
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
                           "DEPARTMENT_NAME, " +
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
                           "'Employee Joining History Between ' || to_date('" + objEmployeeReportModel.FromDate.Trim() + "', 'dd/mm/yyyy')|| ' and '|| to_date('" + objEmployeeReportModel.ToDate.Trim() + "', 'dd/mm/yyyy') RPT_TITLE, " +
                           "EMPLOYEEE_PIC, " +
                           "EMPLOYEEE_SIGNATURE, " +
                           "(select employee_name from EMPLOYEE_BASIC where EMPLOYEE_ID = '" + objEmployeeReportModel.UpdateBy + "') UPDATE_BY " +
                           "from VEW_RPT_EMPLOYEE_INFORMATION  where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "' ";


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.FromDate) && (!string.IsNullOrEmpty(objEmployeeReportModel.ToDate)))
                    {
                        sql = sql + "and joining_date between to_date ('" + objEmployeeReportModel.FromDate.Trim() + "', 'dd/mm/yyyy') and to_date ('" + objEmployeeReportModel.ToDate.Trim() + "', 'dd/mm/yyyy') ";

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
                            objDataAdapter.Fill(ds, "VEW_RPT_EMPLOYEE_INFORMATION");
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

        public DataSet EmployeeJobYearHistory(EmployeeReportModel objEmployeeReportModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";
                    sql = "SELECT " +
                       "EMPLOYEE_ID, " +
                       "CARD_NO, " +
                       "EMPLOYEE_NAME, " +
                       "DATE_OF_BIRTH, " +
                       "EMERGENCY_CONTACT_NO, " +
                       "UPDATE_BY, " +
                       "UPDATE_DATE, " +
                       "HEAD_OFFICE_ID, " +
                       "HEAD_OFFICE_NAME, " +
                       "BRANCH_OFFICE_ID, " +
                       "BRANCH_OFFICE_NAME, " +
                       "BRANCH_OFFICE_ADDRESS, " +
                       "JOINING_DATE, " +
                       "JOINING_DESIGNATION_ID, " +
                       "JOINING_DESIGNATION_NAME, " +
                       "PRESENT_DESIGNATION_ID, " +
                       "PRESENT_DESIGNATION_NAME, " +
                       "UNIT_NAME, " +
                       "UNIT_ID, " +
                       "DEPARTMENT_ID, " +
                       "DEPARTMENT_NAME, " +
                       "SECTION_NAME, " +
                       "SECTION_ID, " +
                       "SUB_SECTION_ID, " +
                       "SUB_SECTION_NAME, " +
                       "RPT_TITLE, " +
                       "EMPLOYEEE_PIC, " +
                       "EMPLOYEEE_SIGNATURE, " +
                       "CREATE_DATE, " +
                       "TOTAL_YEAR " +
                        "from vew_rpt_emp_job_year_history  where head_office_id = '" + objEmployeeReportModel.HeadOfficeId + "' AND branch_office_id = '" + objEmployeeReportModel.BranchOfficeId + "'";


                    if (!string.IsNullOrEmpty(objEmployeeReportModel.EmployeeId))
                    {
                        sql = sql + "and employee_id = '" + objEmployeeReportModel.EmployeeId.Trim() + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.UnitId))
                    {
                        sql = sql + "and unit_id = '" + objEmployeeReportModel.UnitId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.DepartmentId))
                    {
                        sql = sql + "and department_id = '" + objEmployeeReportModel.DepartmentId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SectionId))
                    {
                        sql = sql + "and section_id = '" + objEmployeeReportModel.SectionId + "' ";

                    }

                    if (!string.IsNullOrEmpty(objEmployeeReportModel.SubSectionId))
                    {
                        sql = sql + "and sub_section_id = '" + objEmployeeReportModel.SubSectionId + "' ";

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
                            objDataAdapter.Fill(ds, "vew_rpt_emp_job_year_history");
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


        #region Increment Report

        public DataSet IncrementReport(IncrementEntryModel objIncrementModel)
        {

            try
            {

                DataSet ds = null;
                DataTable dt = new DataTable();
                try
                {
                    string sql = "";

                    if (objIncrementModel.UnitId != null && objIncrementModel.DepartmentId != null && objIncrementModel.SectionId != null && objIncrementModel.SubSectionId != null)
                    {
                        sql = "SELECT " +

                              "'Increment Report on '|| ' - '|| '" + objIncrementModel.Year + "'  RPT_TITLE, " +


                              
   "EMPLOYEE_ID,"+
   "EMPLOYEE_NAME,"+
  " CARD_NO,"+
   "JOINING_DATE,"+
   "DESIGNATION_ID,"+
   "DESIGNATION_NAME,"+
   "DEPARTMENT_ID,"+
   "DEPARTMENT_NAME,"+
   "UNIT_ID,"+
   "UNIT_NAME,"+
   "SECTION_ID,"+
   "SECTION_NAME,"+
   "GRADE_NO,"+
   "HEAD_OFFICE_ID,"+
   "HEAD_OFFICE_NAME,"+
   "BRANCH_OFFICE_ID,"+
   "BRANCH_OFFICE_NAME,"+
   "BRANCH_OFFICE_ADDRESS," +
   "SUB_SECTION_ID,"+
   "SUB_SECTION_NAME,"+
   "GROSS_SALARY,"+
   "JOINING_SALARY,"+
   "TOTAL_YEAR,"+
   "TOTAL_MONTH,"+
   "INCREMENT_AMOUNT,"+
   "TOTAL_AMOUNT," +
   "INCREMENT_YEAR,"+

   "  'In Word : ' "+
   "         || (SELECT func_number_to_word(SUM(INCREMENT_AMOUNT)) " +
    "              FROM vew_rpt_increment_sheet " +
     "            WHERE increment_year ='"+objIncrementModel.Year+"' " +
      "                 AND unit_id = '"+objIncrementModel.UnitId+"' " +
       "                AND department_id = '" + objIncrementModel.DepartmentId + "' " +
         "                AND section_id = '" + objIncrementModel.SectionId + "' " +
           "                AND sub_section_id = '" + objIncrementModel.SubSectionId + "' " +

        "               AND head_office_id ='" + objIncrementModel.HeadOfficeId + "' " +
         "              AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "') " +
          "  || ' Only' PAYMENT_AMOUNT,  " +

 

   "  'In Word : ' " +
   "         || (SELECT func_number_to_word(SUM(INCREMENT_AMOUNT)) " +
    "              FROM vew_rpt_increment_sheet " +
     "            WHERE increment_year ='" + objIncrementModel.Year + "' " +
      "                 AND unit_id = '" + objIncrementModel.UnitId + "' " +
       "                AND department_id = '" + objIncrementModel.DepartmentId + "' " +
         "                AND section_id = '" + objIncrementModel.SectionId + "' " +
           "                AND sub_section_id = '" + objIncrementModel.SubSectionId + "' " +

        "               AND head_office_id ='" + objIncrementModel.HeadOfficeId + "' " +
         "              AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "') " +
          "  || ' Only' PAYMENT_AMOUNT_ALL " +




                              "from vew_rpt_increment_sheet a  where head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and increment_year = '" + objIncrementModel.Year + "' ";

                        if (objIncrementModel.UnitId != null)
                        {
                            sql = sql + "and unit_id = '" + objIncrementModel.UnitId + "' ";

                        }

                        if (objIncrementModel.DepartmentId != null)
                        {
                            sql = sql + "and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "' ";

                        }

                        if (objIncrementModel.SectionId != null)
                        {
                            sql = sql + "and section_id = '" + objIncrementModel.SectionId + "' ";

                        }

                        if (objIncrementModel.SubSectionId != null)
                        {
                            sql = sql + "and sub_section_id = '" + objIncrementModel.SubSectionId + "' ";

                        }

                    }


 //                   else if (objIncrementModel.UnitId != null && objIncrementModel.DepartmentId != null && objIncrementModel.SectionId != null)
 //                   {
 //                       sql = "SELECT " +

 //                            "'Increment Report on '|| ' - '|| '" + objIncrementModel.Year + "')  RPT_TITLE, " +


 //                             "RPT_TITLE," +
 // "EMPLOYEE_ID," +
 // "EMPLOYEE_NAME," +
 //" CARD_NO," +
 // "JOINING_DATE," +
 // "DESIGNATION_ID," +
 // "DESIGNATION_NAME," +
 // "DEPARTMENT_ID," +
 // "DEPARTMENT_NAME," +
 // "UNIT_ID," +
 // "UNIT_NAME," +
 // "SECTION_ID," +
 // "SECTION_NAME," +
 // "GRADE_NO," +
 // "HEAD_OFFICE_ID," +
 // "HEAD_OFFICE_NAME," +
 // "BRANCH_OFFICE_ID," +
 // "BRANCH_OFFICE_NAME," +
 // "BRANCH_OFFICE_ADDRESS," +
 // "SUB_SECTION_ID," +
 // "SUB_SECTION_NAME," +
 // "GROSS_SALARY," +
 // "JOINING_SALARY," +
 // "TOTAL_YEAR," +
 // "TOTAL_MONTH," +
 // "INCREMENT_AMOUNT," +
 // "TOTAL_AMOUNT," +
 // "INCREMENT_YEAR," +

 // "  'In Word : ' " +
 // "         || (SELECT func_number_to_word(SUM(INCREMENT_AMOUNT)) " +
 //  "              FROM vew_rpt_increment_sheet " +
 //   "            WHERE increment_year ='" + objIncrementModel.Year + "' " +
 //    "                 AND unit_id = '" + objIncrementModel.UnitId + "' " +
 //     "                AND department_id = '" + objIncrementModel.DepartmentId + "' " +
 //       "                AND section_id = '" + objIncrementModel.SectionId + "' " +
 //         "                AND sub_section_id = '" + objIncrementModel.SubSectionId + "' " +

 //      "               AND head_office_id ='" + objIncrementModel.HeadOfficeId + "' " +
 //       "              AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "') " +
 //        "  || ' Only' PAYMENT_AMOUNT,  " +



 // "  'In Word : ' " +
 // "         || (SELECT func_number_to_word(SUM(INCREMENT_AMOUNT)) " +
 //  "              FROM vew_rpt_increment_sheet " +
 //   "            WHERE increment_year ='" + objIncrementModel.Year + "' " +
 //    "                 AND unit_id = '" + objIncrementModel.UnitId + "' " +
 //     "                AND department_id = '" + objIncrementModel.DepartmentId + "' " +
 //       "                AND section_id = '" + objIncrementModel.SectionId + "' " +
 //         "                AND sub_section_id = '" + objIncrementModel.SubSectionId + "' " +

 //      "               AND head_office_id ='" + objIncrementModel.HeadOfficeId + "' " +
 //       "              AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "') " +
 //        "  || ' Only' PAYMENT_AMOUNT_ALL " +




 //                            "from vew_rpt_increment_sheet a  where head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and increment_year '" + objIncrementModel.Year + "' ";

 //                       if (objIncrementModel.UnitId != null)
 //                       {
 //                           sql = sql + "and unit_id = '" + objIncrementModel.UnitId + "' ";

 //                       }

 //                       if (objIncrementModel.DepartmentId != null)
 //                       {
 //                           sql = sql + "and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "' ";

 //                       }

 //                       if (objIncrementModel.SectionId != null)
 //                       {
 //                           sql = sql + "and section_id = '" + objIncrementModel.SectionId + "' ";

 //                       }



 //                   }

                    //else if (objIncrementModel.UnitId != null && objIncrementModel.DepartmentId != null)
                    //{
                    //    sql = "SELECT " +

                    //          "'Daily Attendence Report on '|| ' - '|| to_date( '" + objIncrementModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                    //          "EMPLOYEE_ID, " +
                    //          "EMPLOYEE_NAME, " +
                    //          "JOINING_DATE, " +
                    //          "DESIGNATION_NAME, " +
                    //          "UNIT_ID, " +
                    //          "UNIT_NAME, " +
                    //          "DEPARTMENT_ID, " +
                    //          "DEPARTMENT_NAME, " +
                    //          "SECTION_ID, " +
                    //          "SECTION_NAME, " +
                    //          "SUB_SECTION_ID, " +
                    //          "SUB_SECTION_NAME, " +
                    //          "FIRST_IN, " +
                    //          "LAST_OUT, " +
                    //          "FIRST_LATE_HOUR, " +
                    //          "FIRST_LATE_MINUTE, " +
                    //          "LUNCH_OUT, " +
                    //          "LUNCH_IN, " +
                    //          "LUNCH_LATE_HOUR, " +
                    //          "LUNCH_LATE_MINUTE, " +
                    //          "OT_HOUR, " +
                    //          "OT_MINUTE, " +
                    //          "LOG_LATE, " +
                    //          "LUNCH_LATE, " +
                    //          "LOG_DATE, " +
                    //          "PUNCH_CODE, " +
                    //          "DAY_TYPE, " +
                    //          "REMARKS, " +

                    //          "UPDATE_DATE, " +
                    //          "HEAD_OFFICE_ID, " +
                    //          "HEAD_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ID, " +
                    //          "BRANCH_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ADDRESS, " +
                    //          "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objIncrementModel.UpdateBy + "') UPDATE_BY, " +

                    //          "LATE_YN, " +
                    //          "late_status, " +
                    //          "PUNCTUAL_YN, " +
                    //          "PUNCTUAL_STATUS, " +
                    //          "LATE_HMS, " +
                    //          "total_duty_time, " +
                    //          "EARLY_OUT_TIME, " +
                    //          "finger_yn,  " +
                    //          "card_yn,  " +
                    //          "punch_type,  " +
                    //          "first_in_new, " +
                    //          "last_out_new, " +

                    //          "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')   and unit_id = '" + objIncrementModel.UnitId + "'  and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'    ) " +
                    //          " FROM DUAL)total_employee, " +


                    //          " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'     ) " +
                    //          " FROM DUAL)total_present_employee, " +



                    //          " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'    ) " +
                    //          " FROM DUAL)total_absent_employee, " +

                    //          " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'   ) " +
                    //          " FROM DUAL)total_holiday_employee, " +



                    //          " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'   ) " +
                    //          " FROM DUAL)total_leave_employee, " +


                    //          " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'    ) " +
                    //          " FROM DUAL)toal_late_employee, " +


                    //          " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'   ) " +
                    //          " FROM DUAL)total_punctual_employee, " +

                    //          " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'  ) " +
                    //          " FROM DUAL)total_gh, " +


                    //          "NB, " +
                    //          "present_status, " +
                    //          "absent_status, " +
                    //          "leave_status, " +
                    //          "WORKING_HOLIDAY_STATUS, " +
                    //          "late_status_new, " +

                    //          "PLELA_ALL, " +
                    //          "late_hm, " +


                    //          " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'    ) " +
                    //          " FROM DUAL)total_early_out, " +


                    //          " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'   ) " +
                    //          " FROM DUAL)total_early_late, " +

                    //          " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'   ) " +
                    //          " FROM DUAL)total_cl, " +


                    //          " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'     ) " +
                    //          " FROM DUAL)total_sl, " +

                    //          " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'    ) " +
                    //          " FROM DUAL)total_ml, " +

                    //          " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'     ) " +
                    //          " FROM DUAL)total_el, " +

                    //          " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'     ) " +
                    //          " FROM DUAL)total_pl, " +

                    //          " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'    ) " +
                    //          " FROM DUAL)total_lwp, " +


                    //          " (SELECT 'Alternative Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and ALTERNATIVE_HOLIDAY_YN = 'Y' and unit_id = '" + objIncrementModel.UnitId + "' and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "'   ) " +
                    //          " FROM DUAL)total_ah_employee " +

                    //          "from VEW_RPT_DAILY_ATTENDANCE_SHEET a  where head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy') ";

                    //    if (objIncrementModel.UnitId != null)
                    //    {
                    //        sql = sql + "and unit_id = '" + objIncrementModel.UnitId + "' ";

                    //    }

                    //    if (objIncrementModel.DepartmentId != null)
                    //    {
                    //        sql = sql + "and DEPARTMENT_ID = '" + objIncrementModel.DepartmentId + "' ";

                    //    }




                    //}


                    //else if (objIncrementModel.UnitId != null)
                    //{
                    //    sql = "SELECT " +

                    //          "'Daily Attendence Report on '|| ' - '|| to_date( '" + objIncrementModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                    //          "EMPLOYEE_ID, " +
                    //          "EMPLOYEE_NAME, " +
                    //          "JOINING_DATE, " +
                    //          "DESIGNATION_NAME, " +
                    //          "UNIT_ID, " +
                    //          "UNIT_NAME, " +
                    //          "DEPARTMENT_ID, " +
                    //          "DEPARTMENT_NAME, " +
                    //          "SECTION_ID, " +
                    //          "SECTION_NAME, " +
                    //          "SUB_SECTION_ID, " +
                    //          "SUB_SECTION_NAME, " +
                    //          "FIRST_IN, " +
                    //          "LAST_OUT, " +
                    //          "FIRST_LATE_HOUR, " +
                    //          "FIRST_LATE_MINUTE, " +
                    //          "LUNCH_OUT, " +
                    //          "LUNCH_IN, " +
                    //          "LUNCH_LATE_HOUR, " +
                    //          "LUNCH_LATE_MINUTE, " +
                    //          "OT_HOUR, " +
                    //          "OT_MINUTE, " +
                    //          "LOG_LATE, " +
                    //          "LUNCH_LATE, " +
                    //          "LOG_DATE, " +
                    //          "PUNCH_CODE, " +
                    //          "DAY_TYPE, " +
                    //          "REMARKS, " +

                    //          "UPDATE_DATE, " +
                    //          "HEAD_OFFICE_ID, " +
                    //          "HEAD_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ID, " +
                    //          "BRANCH_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ADDRESS, " +
                    //          "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objIncrementModel.UpdateBy + "') UPDATE_BY, " +

                    //          "LATE_YN, " +
                    //          "late_status, " +
                    //          "PUNCTUAL_YN, " +
                    //          "PUNCTUAL_STATUS, " +
                    //          "LATE_HMS, " +
                    //          "total_duty_time, " +
                    //          "EARLY_OUT_TIME, " +
                    //          "finger_yn,  " +
                    //          "card_yn,  " +
                    //          "punch_type,  " +
                    //          "first_in_new, " +
                    //          "last_out_new, " +

                    //          "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')   and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_employee, " +


                    //          " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_present_employee, " +



                    //          " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_absent_employee, " +

                    //          " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and unit_id = '" + objIncrementModel.UnitId + "' ) " +
                    //          " FROM DUAL)total_holiday_employee, " +



                    //          " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and unit_id = '" + objIncrementModel.UnitId + "' ) " +
                    //          " FROM DUAL)total_leave_employee, " +


                    //          " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)toal_late_employee, " +


                    //          " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_punctual_employee, " +

                    //          " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and unit_id = '" + objIncrementModel.UnitId + "' ) " +
                    //          " FROM DUAL)total_gh, " +


                    //          "NB, " +
                    //          "present_status, " +
                    //          "absent_status, " +
                    //          "leave_status, " +
                    //          "WORKING_HOLIDAY_STATUS, " +
                    //          "late_status_new, " +

                    //          "PLELA_ALL, " +
                    //          "late_hm, " +


                    //          " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_early_out, " +


                    //          " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y' and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_early_late, " +

                    //          " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_cl, " +


                    //          " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_sl, " +

                    //          " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_ml, " +

                    //          " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_el, " +

                    //          " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_pl, " +

                    //          " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_lwp, " +


                    //          " (SELECT 'Alternative Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and ALTERNATIVE_HOLIDAY_YN = 'Y' and unit_id = '" + objIncrementModel.UnitId + "') " +
                    //          " FROM DUAL)total_ah_employee " +

                    //          "from VEW_RPT_DAILY_ATTENDANCE_SHEET a  where head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy') ";

                    //    if (objIncrementModel.UnitId != null)
                    //    {
                    //        sql = sql + "and unit_id = '" + objIncrementModel.UnitId + "' ";

                    //    }

                    //}

                    //else if (objReportModel.SubSectionId != null)
                    //{
                    //    sql = "SELECT " +

                    //          "'Daily Attendence '|| ' - '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                    //          "EMPLOYEE_ID, " +
                    //          "EMPLOYEE_NAME, " +
                    //          "JOINING_DATE, " +
                    //          "DESIGNATION_NAME, " +
                    //          "UNIT_ID, " +
                    //          "UNIT_NAME, " +
                    //          "DEPARTMENT_ID, " +
                    //          "DEPARTMENT_NAME, " +
                    //          "SECTION_ID, " +
                    //          "SECTION_NAME, " +
                    //          "SUB_SECTION_ID, " +
                    //          "SUB_SECTION_NAME, " +
                    //          "FIRST_IN, " +
                    //          "LAST_OUT, " +
                    //          "FIRST_LATE_HOUR, " +
                    //          "FIRST_LATE_MINUTE, " +
                    //          "LUNCH_OUT, " +
                    //          "LUNCH_IN, " +
                    //          "LUNCH_LATE_HOUR, " +
                    //          "LUNCH_LATE_MINUTE, " +
                    //          "OT_HOUR, " +
                    //          "OT_MINUTE, " +
                    //          "LOG_LATE, " +
                    //          "LUNCH_LATE, " +
                    //          "LOG_DATE, " +
                    //          "PUNCH_CODE, " +
                    //          "DAY_TYPE, " +
                    //          "REMARKS, " +

                    //          "UPDATE_DATE, " +
                    //          "HEAD_OFFICE_ID, " +
                    //          "HEAD_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ID, " +
                    //          "BRANCH_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ADDRESS, " +
                    //          "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +

                    //          "LATE_YN, " +
                    //          "late_status, " +
                    //          "PUNCTUAL_YN, " +
                    //          "PUNCTUAL_STATUS, " +
                    //          "LATE_HMS, " +
                    //          "total_duty_time, " +
                    //          "EARLY_OUT_TIME, " +
                    //          "finger_yn,  " +
                    //          "card_yn,  " +
                    //          "punch_type,  " +
                    //          "first_in_new, " +
                    //          "last_out_new, " +

                    //          "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_employee, " +


                    //          " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_present_employee, " +



                    //          " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_absent_employee, " +

                    //          " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and sub_Section_id = '" + objReportModel.SubSectionId + "' ) " +
                    //          " FROM DUAL)total_holiday_employee, " +


                    //          " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and sub_Section_id = '" + objReportModel.SubSectionId + "' ) " +
                    //          " FROM DUAL)total_leave_employee, " +


                    //          " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)toal_late_employee, " +


                    //          " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_punctual_employee, " +

                    //          " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and sub_Section_id = '" + objReportModel.SubSectionId + "' ) " +
                    //          " FROM DUAL)total_gh, " +


                    //          "NB, " +
                    //          "present_status, " +
                    //          "absent_status, " +
                    //          "leave_status, " +
                    //          "WORKING_HOLIDAY_STATUS, " +
                    //          "late_status_new, " +
                    //          "PLELA_ALL, " +
                    //          "late_hm, " +



                    //          " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_early_out, " +


                    //          " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and late_yn = 'Y' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_early_late, " +

                    //          " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_cl, " +


                    //          " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_sl, " +

                    //          " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_ml, " +

                    //          " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_el, " +

                    //          " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_pl, " +


                    //          " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_lwp, " +

                    //         " (SELECT 'Alternative Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and ALTERNATIVE_HOLIDAY_YN = 'Y' and sub_Section_id = '" + objReportModel.SubSectionId + "') " +
                    //          " FROM DUAL)total_ah_employee " +



                    //          "from VEW_RPT_DAILY_ATTENDANCE_SHEET a where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') ";

                    //    if (objReportModel.SubSectionId != null)
                    //    {
                    //        sql = sql + "and sub_section_id = '" + objReportModel.SubSectionId + "' ";

                    //    }

                    //}

                    //else if (objReportModel.DepartmentId != null)
                    //{
                    //    sql = "SELECT " +

                    //          "'Daily Attendence '|| ' - '|| to_date( '" + objReportModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                    //          "EMPLOYEE_ID, " +
                    //          "EMPLOYEE_NAME, " +
                    //          "JOINING_DATE, " +
                    //          "DESIGNATION_NAME, " +
                    //          "UNIT_ID, " +
                    //          "UNIT_NAME, " +
                    //          "DEPARTMENT_ID, " +
                    //          "DEPARTMENT_NAME, " +
                    //          "SECTION_ID, " +
                    //          "SECTION_NAME, " +
                    //          "SUB_SECTION_ID, " +
                    //          "SUB_SECTION_NAME, " +
                    //          "FIRST_IN, " +
                    //          "LAST_OUT, " +
                    //          "FIRST_LATE_HOUR, " +
                    //          "FIRST_LATE_MINUTE, " +
                    //          "LUNCH_OUT, " +
                    //          "LUNCH_IN, " +
                    //          "LUNCH_LATE_HOUR, " +
                    //          "LUNCH_LATE_MINUTE, " +
                    //          "OT_HOUR, " +
                    //          "OT_MINUTE, " +
                    //          "LOG_LATE, " +
                    //          "LUNCH_LATE, " +
                    //          "LOG_DATE, " +
                    //          "PUNCH_CODE, " +
                    //          "DAY_TYPE, " +
                    //          "REMARKS, " +

                    //          "UPDATE_DATE, " +
                    //          "HEAD_OFFICE_ID, " +
                    //          "HEAD_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ID, " +
                    //          "BRANCH_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ADDRESS, " +
                    //          "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objReportModel.UpdateBy + "') UPDATE_BY, " +

                    //          "LATE_YN, " +
                    //          "late_status, " +
                    //          "PUNCTUAL_YN, " +
                    //          "PUNCTUAL_STATUS, " +
                    //          "LATE_HMS, " +
                    //          "total_duty_time, " +
                    //          "EARLY_OUT_TIME, " +
                    //          "finger_yn,  " +
                    //          "card_yn,  " +
                    //          "punch_type,  " +
                    //          "first_in_new, " +
                    //          "last_out_new, " +

                    //          "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_employee, " +

                    //          " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00' AND  department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_present_employee, " +

                    //          " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_absent_employee, " +

                    //          " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' and department_id = '" + objReportModel.DepartmentId + "' ) " +
                    //          " FROM DUAL)total_holiday_employee, " +

                    //          " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' and department_id = '" + objReportModel.DepartmentId + "' ) " +
                    //          " FROM DUAL)total_leave_employee, " +

                    //          " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y'  and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)toal_late_employee, " +


                    //          " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_punctual_employee, " +

                    //          " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' and department_id = '" + objReportModel.DepartmentId + "' ) " +
                    //          " FROM DUAL)total_gh, " +

                    //          "NB, " +

                    //          "present_status, " +
                    //          "absent_status, " +
                    //          "leave_status, " +
                    //          "WORKING_HOLIDAY_STATUS, " +
                    //          "late_status_new, " +
                    //          "PLELA_ALL, " +
                    //          "late_hm, " +


                    //          " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_early_out, " +

                    //          " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')   and late_yn = 'Y' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_early_late, " +

                    //          " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_cl, " +

                    //          " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_sl, " +

                    //          " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_ml, " +

                    //          " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_el, " +

                    //          " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_pl, " +

                    //          " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_lwp, " +

                    //          " (SELECT 'Alternative Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy')  and ALTERNATIVE_HOLIDAY_YN = 'Y' and department_id = '" + objReportModel.DepartmentId + "') " +
                    //          " FROM DUAL)total_ah_employee " +

                    //          "from VEW_RPT_DAILY_ATTENDANCE_SHEET a where head_office_id = '" + objReportModel.HeadOfficeId + "' AND branch_office_id = '" + objReportModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objReportModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objReportModel.ToDate + "', 'dd/mm/yyyy') ";


                    //    if (objReportModel.DepartmentId != null)
                    //    {
                    //        sql = sql + "and department_id = '" + objReportModel.DepartmentId + "' ";

                    //    }

                    //}

                    //else
                    //{
                    //    sql = "SELECT " +

                    //          "'Daily Attendence '|| ' - '|| to_date( '" + objIncrementModel.FromDate + "', 'dd/mm/yyyy')  RPT_TITLE, " +
                    //          "EMPLOYEE_ID, " +
                    //          "EMPLOYEE_NAME, " +
                    //          "JOINING_DATE, " +
                    //          "DESIGNATION_NAME, " +
                    //          "UNIT_ID, " +
                    //          "UNIT_NAME, " +
                    //          "DEPARTMENT_ID, " +
                    //          "DEPARTMENT_NAME, " +
                    //          "SECTION_ID, " +
                    //          "SECTION_NAME, " +
                    //          "SUB_SECTION_ID, " +
                    //          "SUB_SECTION_NAME, " +
                    //          "FIRST_IN, " +
                    //          "LAST_OUT, " +
                    //          "FIRST_LATE_HOUR, " +
                    //          "FIRST_LATE_MINUTE, " +
                    //          "LUNCH_OUT, " +
                    //          "LUNCH_IN, " +
                    //          "LUNCH_LATE_HOUR, " +
                    //          "LUNCH_LATE_MINUTE, " +
                    //          "OT_HOUR, " +
                    //          "OT_MINUTE, " +
                    //          "LOG_LATE, " +
                    //          "LUNCH_LATE, " +
                    //          "LOG_DATE, " +
                    //          "PUNCH_CODE, " +
                    //          "DAY_TYPE, " +
                    //          "REMARKS, " +

                    //          "UPDATE_DATE, " +
                    //          "HEAD_OFFICE_ID, " +
                    //          "HEAD_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ID, " +
                    //          "BRANCH_OFFICE_NAME, " +
                    //          "BRANCH_OFFICE_ADDRESS, " +
                    //          "(select employee_name from employee_basic where EMPLOYEE_ID = '" + objIncrementModel.UpdateBy + "') UPDATE_BY, " +

                    //          "LATE_YN, " +
                    //          "late_status, " +
                    //          "PUNCTUAL_YN, " +
                    //          "PUNCTUAL_STATUS, " +
                    //          "LATE_HMS, " +
                    //          "total_duty_time, " +
                    //          "EARLY_OUT_TIME, " +
                    //          "finger_yn,  " +
                    //          "card_yn,  " +
                    //          "punch_type,  " +
                    //          "first_in_new, " +
                    //          "last_out_new, " +

                    //          "( SELECT 'Total Employee : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  ) " +
                    //          " FROM DUAL)total_employee, " +


                    //          " (SELECT 'Total Present : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and  first_in_new <> '00:00:00' AND  last_out_new <> '00:00:00')  " +
                    //          " FROM DUAL)total_present_employee, " +



                    //          " (SELECT 'Total Absent : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and (absent_yn = 'A' or absent_yn = 'Y') and day_type = 'W' ) " +
                    //          " FROM DUAL)total_absent_employee, " +

                    //          " (SELECT 'Weekly Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and  weekly_holiday_yn = 'Y' ) " +
                    //          " FROM DUAL)total_holiday_employee, " +



                    //          " (SELECT 'Total Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_yn = 'Y' ) " +
                    //          " FROM DUAL)total_leave_employee, " +


                    //          " (SELECT 'Total Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and late_yn = 'Y' ) " +
                    //          " FROM DUAL)toal_late_employee, " +


                    //          " (SELECT 'Total Punctual : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and PUNCTUAL_YN = 'Y' ) " +
                    //          " FROM DUAL)total_punctual_employee, " +

                    //          " (SELECT 'Govt. Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and  HOLIDAY_TYPE_ID = '2' ) " +
                    //          " FROM DUAL)total_gh, " +


                    //          "NB, " +
                    //          "present_status, " +
                    //          "absent_status, " +
                    //          "leave_status, " +
                    //          "WORKING_HOLIDAY_STATUS, " +
                    //          "late_status_new, " +
                    //          "PLELA_ALL, " +
                    //          "late_hm, " +


                    //          " (SELECT 'Early Out : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and early_out_yn = 'Y' ) " +
                    //          " FROM DUAL)total_early_out, " +


                    //          " (SELECT 'Early Late : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET " +
                    //          "  WHERE head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')   and late_yn = 'Y' ) " +
                    //          " FROM DUAL)total_early_late, " +

                    //          " (SELECT 'Casual Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '3' ) " +
                    //          " FROM DUAL)total_cl, " +


                    //          " (SELECT 'Sick Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' ) " +
                    //          " FROM DUAL)total_sl, " +

                    //          " (SELECT 'Maternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '4' ) " +
                    //          " FROM DUAL)total_ml, " +

                    //          " (SELECT 'Eearn Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '10' ) " +
                    //          " FROM DUAL)total_el, " +

                    //          " (SELECT 'Paternity Leave : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '7' ) " +
                    //          " FROM DUAL)total_pl, " +


                    //          " (SELECT 'Leave without Pay : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy')  and leave_type_id = '5' ) " +
                    //          " FROM DUAL)total_lwp, " +

                    //          " (SELECT 'Alternative Holiday : '|| (SELECT COUNT(*) " +
                    //          "   FROM VEW_RPT_DAILY_ATTENDANCE_SHEET a " +
                    //          "  WHERE employee_id = a.employee_id  and head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy') AND ALTERNATIVE_HOLIDAY_YN = 'Y' ) " +
                    //          " FROM DUAL)total_ah_employee " +


                    //          "from VEW_RPT_DAILY_ATTENDANCE_SHEET a where head_office_id = '" + objIncrementModel.HeadOfficeId + "' AND branch_office_id = '" + objIncrementModel.BranchOfficeId + "' and LOG_DATE BETWEEN to_date('" + objIncrementModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objIncrementModel.ToDate + "', 'dd/mm/yyyy') ";


                    //    if (objIncrementModel.EmployeeId != null)
                    //    {
                    //        sql = sql + "and employee_id = '" + objIncrementModel.EmployeeId + "' ";

                    //    }
                    //    if (objIncrementModel.DepartmentId != null)
                    //    {
                    //        sql = sql + "and department_id = '" + objIncrementModel.DepartmentId + "' ";

                    //    }

                    //    if (objIncrementModel.UnitId != null)
                    //    {
                    //        sql = sql + "and unit_id = '" + objIncrementModel.UnitId + "' ";

                    //    }

                    //    if (objIncrementModel.SectionId != null)
                    //    {
                    //        sql = sql + "and section_id = '" + objIncrementModel.SectionId + "' ";

                    //    }

                    //    if (objIncrementModel.SubSectionId != null)
                    //    {
                    //        sql = sql + "and sub_section_id = '" + objIncrementModel.SubSectionId + "' ";

                    //    }

                    //}

                    //sql = sql + " order by to_number(card_no)";

                    //sql = sql + " order by to_number(card_no)";

                    OracleCommand objOracleCommand = new OracleCommand(sql);
                    OracleDataAdapter objDataAdapter = new OracleDataAdapter();
                    using (OracleConnection strConn = GetConnection())
                    {
                        try
                        {
                            objOracleCommand.Connection = strConn;
                            strConn.Open();
                            //strConn.BeginTransaction();

                            objDataAdapter = new OracleDataAdapter(objOracleCommand);
                            dt.Clear();
                            //objOracleCommand.ExecuteNonQuery();
                            ds = new System.Data.DataSet();
                            objDataAdapter.Fill(ds, "vew_rpt_increment_sheet");
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

    }
}
