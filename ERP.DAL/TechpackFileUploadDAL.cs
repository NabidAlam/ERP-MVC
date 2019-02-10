using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ERP.MODEL;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using System.IO;



namespace ERP.DAL
{
    public class TechpackFileUploadDAL
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

        TechpackFileUploadModel objTechpackFileUploadModel = new TechpackFileUploadModel();

        public DataTable TechpackFileUpload(TechpackFileUploadModel objTechpackFileUploadModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                 "TO_CHAR (NVL (BUYER_ID,'0'))BUYER_ID, " +
                 "TO_CHAR (NVL (BUYER_NAME,'N/A'))BUYER_NAME, " +
                 "TO_CHAR (NVL (SEASON_ID,'0'))SEASON_ID, " +
                 "TO_CHAR (NVL (SEASON_NAME,'N/A'))SEASON_NAME, " +
                 "TO_CHAR (NVL (SEASON_YEAR,'0'))SEASON_YEAR, " +
                 "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO, " +              
                 "TO_CHAR(UPLOAD_DATE, 'dd/mm/yyyy')UPLOAD_DATE, " +
                 "TO_CHAR (NVL (UPLOAD_FILES,'0'))UPLOAD_FILES " +

                " FROM VEW_TECHPACK_UPLOAD_RECORD where head_office_id = '" + objTechpackFileUploadModel.HeadOfficeId + "' AND branch_office_id = '" + objTechpackFileUploadModel.BranchOfficeId + "'  ";

           
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objTechpackFileUploadModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.SeasonName))
            {
                sql = sql + " and SEASON_NAME = '" + objTechpackFileUploadModel.SeasonName + "'   ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.SeasonYear))
            {
                sql = sql + " and SEASON_YEAR = '" + objTechpackFileUploadModel.SeasonYear + "'   ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.BuyerName))
            {
                sql = sql + " and BUYER_NAME = '" + objTechpackFileUploadModel.BuyerName + "'   ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.UploadDate))
            {
                sql = sql + " and UPLOAD_DATE = TO_DATE('" + objTechpackFileUploadModel.UploadDate + "','dd/mm/yyyy') ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.SearchBy))
            {

                sql = sql + "and (lower(SEASON_YEAR) like lower( '%" + objTechpackFileUploadModel.SearchBy + "%')  or upper(SEASON_YEAR)like upper('%" + objTechpackFileUploadModel.SearchBy + "%') ) or (lower(STYLE_NO) like lower( '%" + objTechpackFileUploadModel.SearchBy + "%')  or upper(STYLE_NO)like upper('%" + objTechpackFileUploadModel.SearchBy + "%') or (lower(BUYER_NAME) like lower( '%" + objTechpackFileUploadModel.SearchBy + "%')  or upper(BUYER_NAME)like upper('%" + objTechpackFileUploadModel.SearchBy + "%') ) )";
            }



            sql = sql + " ORDER BY sl";
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

        public string SaveTechpackFileUpload(TechpackFileUploadModel objTechpackFileUploadModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_techpack_upload_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objTechpackFileUploadModel.BuyerId != "")
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.BuyerId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objTechpackFileUploadModel.SeasonId != "")
            {
                objOracleCommand.Parameters.Add("P_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.SeasonId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objTechpackFileUploadModel.SeasonYear != "")
            {
                objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.SeasonYear;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objTechpackFileUploadModel.StyleNo != "")
            {
                objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.StyleNo;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objTechpackFileUploadModel.UploadDate != "")
            {
                objOracleCommand.Parameters.Add("P_UPLOAD_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.UploadDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_UPLOAD_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objTechpackFileUploadModel.FileName != "")
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.FileName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objTechpackFileUploadModel.CVSize != null)
            {
                byte[] array = System.Convert.FromBase64String(objTechpackFileUploadModel.CVSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;
                //objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Clob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Clob, ParameterDirection.Input).Value = null;

            }

            if (objTechpackFileUploadModel.FileExtension != "")
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.FileExtension;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTechpackFileUploadModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTechpackFileUploadModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTechpackFileUploadModel.BranchOfficeId;

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

        public TechpackFileUploadModel ViewPdfFile(TechpackFileUploadModel objTechpackFileUploadModel)
        {

            if (objTechpackFileUploadModel.TranId != null && objTechpackFileUploadModel.StyleNo != null && objTechpackFileUploadModel.SeasonId != null && objTechpackFileUploadModel.BuyerId != null && objTechpackFileUploadModel.SeasonYear != null)
            {

                using (OracleConnection strConn = GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.CommandText = "SELECT FILE_SIZE,FILE_NAME FROM TECH_PACK_UPLOAD_SUB WHERE TRAN_ID ='" + objTechpackFileUploadModel.TranId + "' and STYLE_NO ='" + objTechpackFileUploadModel.StyleNo + "' and SEASON_ID ='" + objTechpackFileUploadModel.SeasonId + "' and BUYER_ID ='" + objTechpackFileUploadModel.BuyerId + "' and SEASON_YEAR ='" + objTechpackFileUploadModel.SeasonYear + "' and UPLOAD_DATE =TO_DATE('" + objTechpackFileUploadModel.UploadDate + "','dd/mm/yyyy') ";
                        cmd.Connection = strConn;
                        strConn.Open();
                        using (OracleDataReader sdr = cmd.ExecuteReader())
                        {
                            sdr.Read();
                            objTechpackFileUploadModel.bytes = (byte[])sdr["FILE_SIZE"];
                            objTechpackFileUploadModel.FileName = sdr["FILE_NAME"].ToString();
                        }
                        strConn.Close();
                    }
                }

            }

            return objTechpackFileUploadModel;

        }

        public string DeleteTechpackUploadFile(TechpackFileUploadModel objTechpackFileUploadModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_techpack_file_delete");
            objOracleCommand.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.TranId))
            {
                objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.TranId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.BuyerId))
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.BuyerId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.SeasonId))
            {
                objOracleCommand.Parameters.Add("P_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.SeasonId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.SeasonYear))
            {
                objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.SeasonYear;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.StyleNo))
            {
                objOracleCommand.Parameters.Add("STYLE_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.StyleNo;
            }
            else
            {
                objOracleCommand.Parameters.Add("STYLE_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.UploadDate))
            {
                objOracleCommand.Parameters.Add("UPLOAD_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTechpackFileUploadModel.UploadDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("UPLOAD_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

           
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTechpackFileUploadModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTechpackFileUploadModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objTechpackFileUploadModel.BranchOfficeId;

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

        public DataTable SearchStyleFromTechpackUpload(TechpackFileUploadModel objTechpackFileUploadModel)
        {

            DataTable dt1 = new DataTable();
            string sql = "";
            sql = "SELECT " +
                "rownum sl, " +
                 "TO_CHAR (NVL (BUYER_ID,'0'))BUYER_ID, " +
                 "TO_CHAR (NVL (BUYER_NAME,'N/A'))BUYER_NAME, " +
                 "TO_CHAR (NVL (SEASON_ID,'0'))SEASON_ID, " +
                 "TO_CHAR (NVL (SEASON_NAME,'N/A'))SEASON_NAME, " +
                 "TO_CHAR (NVL (SEASON_YEAR,'0'))SEASON_YEAR, " +
                 "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO, " +
                 "TO_CHAR(UPLOAD_DATE, 'dd/mm/yyyy')UPLOAD_DATE, " +
                 "TO_CHAR (NVL (UPLOAD_FILES,'0'))UPLOAD_FILES " +

                " FROM VEW_TECHPACK_UPLOAD_RECORD where head_office_id = '" + objTechpackFileUploadModel.HeadOfficeId + "' AND branch_office_id = '" + objTechpackFileUploadModel.BranchOfficeId + "'  ";


            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objTechpackFileUploadModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.SeasonName))
            {
                sql = sql + " and SEASON_NAME = '" + objTechpackFileUploadModel.SeasonName + "'   ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.SeasonYear))
            {
                sql = sql + " and SEASON_YEAR = '" + objTechpackFileUploadModel.SeasonYear + "'   ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.BuyerName))
            {
                sql = sql + " and BUYER_NAME = '" + objTechpackFileUploadModel.BuyerName + "'   ";
            }

            sql = sql + " ORDER BY SEASON_YEAR DESC";

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

        //datewise all file show

        public DataTable DateWiseTechpackFileUpload(TechpackFileUploadModel objTechpackFileUploadModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "TO_CHAR (NVL (TRAN_ID,'0'))TRAN_ID, " +
                 "TO_CHAR (NVL (BUYER_ID,'0'))BUYER_ID, " +
                 "TO_CHAR (NVL (BUYER_NAME,'N/A'))BUYER_NAME, " +
                 "TO_CHAR (NVL (SEASON_ID,'0'))SEASON_ID, " +
                 "TO_CHAR (NVL (SEASON_NAME,'N/A'))SEASON_NAME, " +
                 "TO_CHAR (NVL (SEASON_YEAR,'0'))SEASON_YEAR, " +
                 "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO, " +
                  "TO_CHAR (NVL (FILE_NAME,'N/A'))FILE_NAME, " +
                 "TO_CHAR(UPLOAD_DATE, 'dd/mm/yyyy')UPLOAD_DATE " +
                " FROM VEW_TECHPACK_UPLOAD_SUB_RECORD where head_office_id = '" + objTechpackFileUploadModel.HeadOfficeId + "' AND branch_office_id = '" + objTechpackFileUploadModel.BranchOfficeId + "'  ";


            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objTechpackFileUploadModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.SeasonId))
            {
                sql = sql + " and SEASON_ID = '" + objTechpackFileUploadModel.SeasonId + "'   ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.SeasonYear))
            {
                sql = sql + " and SEASON_YEAR = '" + objTechpackFileUploadModel.SeasonYear + "'   ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.BuyerId))
            {
                sql = sql + " and BUYER_ID = '" + objTechpackFileUploadModel.BuyerId + "'   ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.UploadDate))
            {
                sql = sql + " and UPLOAD_DATE = TO_DATE('" + objTechpackFileUploadModel.UploadDate + "','dd/mm/yyyy') ";
            }
            if (!string.IsNullOrEmpty(objTechpackFileUploadModel.SearchBy))
            {

                sql = sql + "and (lower(SEASON_YEAR) like lower( '%" + objTechpackFileUploadModel.SearchBy + "%')  or upper(SEASON_YEAR)like upper('%" + objTechpackFileUploadModel.SearchBy + "%') ) or (lower(STYLE_NO) like lower( '%" + objTechpackFileUploadModel.SearchBy + "%')  or upper(STYLE_NO)like upper('%" + objTechpackFileUploadModel.SearchBy + "%') or (lower(BUYER_NAME) like lower( '%" + objTechpackFileUploadModel.SearchBy + "%')  or upper(BUYER_NAME)like upper('%" + objTechpackFileUploadModel.SearchBy + "%') ) )";
            }



            sql = sql + " ORDER BY sl";
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

    }
}
