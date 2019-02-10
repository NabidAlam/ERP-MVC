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
    public class SelectionEntryDAL
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
        SelectionEntryModel objSelectionEntryModel = new SelectionEntryModel();
        //get country name to load insert table
        public string GetCountryNameById(SelectionEntryModel objSelectionEntryModel)
        {
            string vCountryName = "";
            string sql = "";
            sql = "SELECT " +
                  " COUNTRY_NAME " +
                  "FROM L_COUNTRY where head_office_id = '" + objSelectionEntryModel.HeadOfficeId.Trim() + "' and branch_office_id = '" + objSelectionEntryModel.BranchOfficeId.Trim() + "' and COUNTRY_ID = '" + objSelectionEntryModel.CountryId.Trim() + "' ";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        vCountryName = objReader.GetString(0);

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
            return vCountryName;
        }
        //save data 
        public string SaveSelectionEntrySave(SelectionEntryModel objSelectionEntryModel)
        {
            string strMsg = "";

            int x = objSelectionEntryModel.ArrReceived.Count();
            for (int i = 0; i < x; i++)
            {
                var arrayEUSS = "";
                var arrayEUAW = "";
                var arrayCountry = "";
                var arrayTranId = objSelectionEntryModel.ArrTranId[i];
                if (objSelectionEntryModel.ArrEUSS!=null)
                {
                   arrayEUSS = objSelectionEntryModel.ArrEUSS[i];
                }
                if (objSelectionEntryModel.ArrEUAW!=null)
                {
                    arrayEUAW = objSelectionEntryModel.ArrEUAW[i];
                }
                if (objSelectionEntryModel.ArrCountry != null)
                {
                   arrayCountry = objSelectionEntryModel.ArrCountry[i];
                }
                var arrayElasticity = objSelectionEntryModel.ArrElasticity[i];
                var arraySupply = objSelectionEntryModel.ArrSupply[i];
                var arrayReceived = objSelectionEntryModel.ArrReceived[i];

                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("pro_selection_entry_save");
                objOracleCommand.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(arrayTranId))
                {
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayTranId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objSelectionEntryModel.StyleNo))
                {
                    objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSelectionEntryModel.StyleNo.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objSelectionEntryModel.ModelNo))
                {
                    objOracleCommand.Parameters.Add("P_MODEL_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSelectionEntryModel.ModelNo.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_MODEL_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objSelectionEntryModel.TotalReceived))
                {
                    objOracleCommand.Parameters.Add("P_TOTAL_RECEIVED", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSelectionEntryModel.TotalReceived.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_TOTAL_RECEIVED", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(objSelectionEntryModel.CountryId))
                {
                    objOracleCommand.Parameters.Add("P_COUNTRY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSelectionEntryModel.CountryId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_COUNTRY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arrayEUSS))
                {
                    objOracleCommand.Parameters.Add("P_EUROPE_SS_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayEUSS.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_EUROPE_SS_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayEUAW))
                {
                    objOracleCommand.Parameters.Add("P_EUROPE_AW_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayEUAW.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_EUROPE_AW_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayCountry))
                {
                    objOracleCommand.Parameters.Add("P_COUNTRY_ORDER_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayCountry.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_COUNTRY_ORDER_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayElasticity))
                {
                    objOracleCommand.Parameters.Add("P_ELASTICITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayElasticity.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ELASTICITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arraySupply))
                {
                    objOracleCommand.Parameters.Add("P_SUPPLY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arraySupply.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_SUPPLY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arrayReceived))
                {
                    objOracleCommand.Parameters.Add("P_COUNTRY_RECEIVED_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayReceived.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_COUNTRY_RECEIVED_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                objOracleCommand.Parameters.Add("P_CREATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.BranchOfficeId;



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

            return strMsg;

        }
        //get data from main table
        public DataTable GetSelectionRecordMain(SelectionEntryModel objSelectionEntryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "ROWNUM SL, " +
                   "TO_CHAR(NVL(STYLE_NO, 'N/A'))STYLE_NO, " +
                   "TO_CHAR(NVL(MODEL_NO, 'N/A'))MODEL_NO, " +
                   "TO_CHAR(NVL(TOTAL_RECEIVED, '0'))TOTAL_RECEIVED, " +
                   "TO_CHAR(NVL(COUNTRY_ID, '0'))COUNTRY_ID, " +
                   "TO_CHAR(NVL(COUNTRY_NAME, 'N/A'))COUNTRY_NAME " +

                    "FROM VEW_SELECTION_ENTRY_MAIN WHERE HEAD_OFFICE_ID = '" + objSelectionEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objSelectionEntryModel.BranchOfficeId + "'  ";

            //if (!string.IsNullOrEmpty(objSelectionEntryModel.StyleNo))
            //{
            //    sql = sql + "and STYLE_NO = '" + objSelectionEntryModel.StyleNo + "'";
            //}
            //if (!string.IsNullOrEmpty(objSelectionEntryModel.ModelNo))
            //{
            //    sql = sql + "and MODEL_NO = '" + objSelectionEntryModel.ModelNo + "'";
            //}
            //if (!string.IsNullOrEmpty(objSelectionEntryModel.CountryName))
            //{
            //    sql = sql + "and COUNTRY_NAME = '" + objSelectionEntryModel.CountryName + "'";
            //}

            sql = sql + " ORDER BY SL";
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
        //get data from sub table
        public DataTable GetSelectionRecordSub(SelectionEntryModel objSelectionEntryModel)
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                   "ROWNUM SL, " +
                   "TO_CHAR(NVL(TRAN_ID, '0'))TRAN_ID, " +
                   "TO_CHAR(NVL(STYLE_NO, 'N/A'))STYLE_NO, " +
                   "TO_CHAR(NVL(MODEL_NO, 'N/A'))MODEL_NO, " +
                   "TO_CHAR(NVL(TOTAL_RECEIVED, '0'))TOTAL_RECEIVED, " +
                   "TO_CHAR(NVL(COUNTRY_ID, '0'))COUNTRY_ID, " +
                   "TO_CHAR(NVL(COUNTRY_NAME, 'N/A'))COUNTRY_NAME, " +
                   "TO_CHAR(NVL(EUROPE_SS_QUANTITY, '0'))EUROPE_SS_QUANTITY, " +
                   "TO_CHAR(NVL(EUROPE_AW_QUANTITY, '0'))EUROPE_AW_QUANTITY, " +
                   "TO_CHAR(NVL(COUNTRY_ORDER_QUANTITY, '0'))COUNTRY_ORDER_QUANTITY, " +
                   "TO_CHAR(NVL(ELASTICITY, 'N/A'))ELASTICITY, " +
                   "TO_CHAR(NVL(SUPPLY, 'N/A'))SUPPLY, " +
                   "TO_CHAR(NVL(COUNTRY_RECEIVED_QUANTITY, '0'))COUNTRY_RECEIVED_QUANTITY " +
                   "FROM VEW_SELECTION_ENTRY_SUB WHERE HEAD_OFFICE_ID = '" + objSelectionEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objSelectionEntryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objSelectionEntryModel.StyleNo))
            {
                sql = sql + "and STYLE_NO = '" + objSelectionEntryModel.StyleNo + "'";
            }
            if (!string.IsNullOrEmpty(objSelectionEntryModel.ModelNo))
            {
                sql = sql + "and MODEL_NO = '" + objSelectionEntryModel.ModelNo + "'";
            }
            sql = sql + " ORDER BY SL";
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
        //delete data
        public string DeleteSelectionEntry(SelectionEntryModel objSelectionEntryModel)
        {

            string vStrMsg = "";
            string[] TranIdArray = objSelectionEntryModel.GridTranId.Split(',');
            int x = TranIdArray.Count();
            for (int i = 0; i < x; i++)
            {
                var arrayTranId = TranIdArray[i];
                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("PRO_SELECTION_ENTRY_DELETE");
                objOracleCommand.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(arrayTranId))
                {
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayTranId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objSelectionEntryModel.StyleNo))
                {
                    objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSelectionEntryModel.StyleNo.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objSelectionEntryModel.ModelNo))
                {
                    objOracleCommand.Parameters.Add("P_MODEL_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSelectionEntryModel.ModelNo.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_MODEL_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.BranchOfficeId;
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
                        vStrMsg = objOracleCommand.Parameters["P_MESSAGE"].Value.ToString();
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
            return vStrMsg;
        }
        //Auto search selection entry
        public DataTable GetSelectionAutoSearch(SelectionEntryModel objSelectionEntryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "ROWNUM SL, " +
                   "TO_CHAR(NVL(STYLE_NO, 'N/A'))STYLE_NO, " +
                   "TO_CHAR(NVL(MODEL_NO, 'N/A'))MODEL_NO, " +
                   "TO_CHAR(NVL(TOTAL_RECEIVED, '0'))TOTAL_RECEIVED, " +
                   "TO_CHAR(NVL(COUNTRY_ID, '0'))COUNTRY_ID, " +
                   "TO_CHAR(NVL(COUNTRY_NAME, 'N/A'))COUNTRY_NAME " +

                    "FROM VEW_SELECTION_ENTRY_MAIN WHERE HEAD_OFFICE_ID = '" + objSelectionEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objSelectionEntryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objSelectionEntryModel.StyleNo))
            {
                sql = sql + "and STYLE_NO = '" + objSelectionEntryModel.StyleNo + "'";
            }
            if (!string.IsNullOrEmpty(objSelectionEntryModel.ModelNo))
            {
                sql = sql + "and MODEL_NO = '" + objSelectionEntryModel.ModelNo + "'";
            }
            if (!string.IsNullOrEmpty(objSelectionEntryModel.CountryName))
            {
                sql = sql + "and COUNTRY_NAME = '" + objSelectionEntryModel.CountryName + "'";
            }

            sql = sql + " ORDER BY SL";
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
        //save section entry pdf file
        public string SaveSeactionPdfFileUpload(SelectionEntryModel objSelectionEntryModel)
        {
            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("SELECTION_FILE_UPLOAD_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrEmpty(objSelectionEntryModel.GridTranId))
            {
                objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.GridTranId.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (!string.IsNullOrEmpty(objSelectionEntryModel.UploadDate))
            {
                objOracleCommand.Parameters.Add("P_UPLOAD_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.UploadDate.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_UPLOAD_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (!string.IsNullOrEmpty(objSelectionEntryModel.FileName))
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.FileName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (!string.IsNullOrEmpty(objSelectionEntryModel.CVSize))
            {
                byte[] array = System.Convert.FromBase64String(objSelectionEntryModel.CVSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Clob, ParameterDirection.Input).Value = null;

            }

            if (!string.IsNullOrEmpty(objSelectionEntryModel.FileExtension))
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.FileExtension.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSelectionEntryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_create_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSelectionEntryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSelectionEntryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSelectionEntryModel.BranchOfficeId;

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
        //get pdf upload record
        public DataTable GetSelectionFileUploadRecord(SelectionEntryModel objSelectionEntryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "TO_CHAR(UPLOAD_DATE, 'dd/mm/yyyy')UPLOAD_DATE, " +
                 "TO_CHAR (NVL (NO_OF_FILE,'0'))NO_OF_FILE " +
                " FROM VEW_SELECTION_FILE_UPLOAD where head_office_id = '" + objSelectionEntryModel.HeadOfficeId + "' AND branch_office_id = '" + objSelectionEntryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objSelectionEntryModel.UploadDate))
            {
                sql = sql + " and UPLOAD_DATE = TO_DATE('" + objSelectionEntryModel.UploadDate + "','dd/mm/yyyy') ";
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
        //Auto search pdf upload date
        public DataTable GetSelectionPdfAutoSearch(SelectionEntryModel objSelectionEntryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "TO_CHAR(UPLOAD_DATE, 'dd/mm/yyyy')UPLOAD_DATE " +
                    "FROM VEW_SELECTION_FILE_UPLOAD WHERE HEAD_OFFICE_ID = '" + objSelectionEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objSelectionEntryModel.BranchOfficeId + "'  ";        

            sql = sql + " ORDER BY UPLOAD_DATE DESC";
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
        //Datewise Pdf record
        public DataTable DateWiseSelectionFileUpload(SelectionEntryModel objSelectionEntryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "TO_CHAR (NVL (TRAN_ID,'0'))TRAN_ID, " +
                  "TO_CHAR (NVL (FILE_NAME,'N/A'))FILE_NAME, " +
                 "TO_CHAR(UPLOAD_DATE, 'dd/mm/yyyy')UPLOAD_DATE " +
                " FROM VEW_SELECTION_FILE_RECORD where head_office_id = '" + objSelectionEntryModel.HeadOfficeId + "' AND branch_office_id = '" + objSelectionEntryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objSelectionEntryModel.UploadDate))
            {
                sql = sql + " and UPLOAD_DATE = TO_DATE('" + objSelectionEntryModel.UploadDate + "','dd/mm/yyyy') ";
            }
            //if (!string.IsNullOrEmpty(objTechpackFileUploadModel.SearchBy))
            //{

            //    sql = sql + "and (lower(SEASON_YEAR) like lower( '%" + objTechpackFileUploadModel.SearchBy + "%')  or upper(SEASON_YEAR)like upper('%" + objTechpackFileUploadModel.SearchBy + "%') ) or (lower(STYLE_NO) like lower( '%" + objTechpackFileUploadModel.SearchBy + "%')  or upper(STYLE_NO)like upper('%" + objTechpackFileUploadModel.SearchBy + "%') or (lower(BUYER_NAME) like lower( '%" + objTechpackFileUploadModel.SearchBy + "%')  or upper(BUYER_NAME)like upper('%" + objTechpackFileUploadModel.SearchBy + "%') ) )";
            //}



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
        //PDF file View
        public SelectionEntryModel ViewPdfFile(SelectionEntryModel objSelectionEntryModel)
        {

            if (!string.IsNullOrEmpty(objSelectionEntryModel.UploadDate)&& !string.IsNullOrEmpty(objSelectionEntryModel.GridTranId))
            {

                using (OracleConnection strConn = GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {
                        cmd.CommandText = "SELECT FILE_SIZE,FILE_NAME FROM SELECTION_FILE_UPLOAD WHERE TRAN_ID ='" + objSelectionEntryModel.GridTranId + "' and UPLOAD_DATE =TO_DATE('" + objSelectionEntryModel.UploadDate + "','dd/mm/yyyy') ";
                        cmd.Connection = strConn;
                        strConn.Open();
                        using (OracleDataReader sdr = cmd.ExecuteReader())
                        {
                            sdr.Read();
                            objSelectionEntryModel.bytes = (byte[])sdr["FILE_SIZE"];
                            objSelectionEntryModel.FileName = sdr["FILE_NAME"].ToString();
                        }
                        strConn.Close();
                    }
                }

            }

            return objSelectionEntryModel;

        }
        //Delete pdf file
        public string DeleteSelectionUploadFile(SelectionEntryModel objSelectionEntryModel)
        {

            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_SELECTION_FILE_UPLOAD");
            objOracleCommand.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrEmpty(objSelectionEntryModel.GridTranId))
            {
                objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.GridTranId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }      
            if (!string.IsNullOrEmpty(objSelectionEntryModel.UploadDate))
            {
                objOracleCommand.Parameters.Add("UPLOAD_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSelectionEntryModel.UploadDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("UPLOAD_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSelectionEntryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSelectionEntryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objSelectionEntryModel.BranchOfficeId;

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

        //serach style no from purchase order
        public DataTable SearchStyleFromPO(SelectionEntryModel objSelectionEntryModel)
        {

            DataTable dt1 = new DataTable();
            string sql = "";
            sql = "SELECT " +
                   "rownum sl, " +
                   "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO, " +
                   "TO_CHAR (NVL (MODEL_NO,'N/A'))MODEL_NO, " +
                   "TO_CHAR (NVL (SEASON_YEAR,'0'))SEASON_YEAR " +               
              " FROM VEW_PURCHASE_ORDER_MERCHAN_SUB where head_office_id = '" + objSelectionEntryModel.HeadOfficeId + "' AND branch_office_id = '" + objSelectionEntryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objSelectionEntryModel.CurrentYear))
            {
                sql = sql + " and SEASON_YEAR = '" + objSelectionEntryModel.CurrentYear + "'   ";
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

    }
}
