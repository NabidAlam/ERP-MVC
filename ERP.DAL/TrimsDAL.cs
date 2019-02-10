using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.MODEL;

namespace ERP.DAL
{
    public class TrimsDAL
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


        public DataTable CheckStyleNumber(string styleId)
        {
            DataTable dt = new DataTable();
            string sql = "";

            sql = "SELECT " +
              "SL, " +
               "STYLE_NO, " +
               "STYLE_NAME, " +
               "HEAD_OFFICE_ID, " +
               "BRANCH_OFFICE_ID, " +
               "SEASON_YEAR, " +
               "SEASON_ID, " +
               "SEASON_NAME " +
              "FROM VEW_PRODUCT_MAIN where STYLE_NO = '" + styleId + "' ";

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

        public TrimsModel GetTrimsData(string styleId, string seasonYear, string seasonName, string headOfficeId, string branchOfficeId)
        {
            TrimsModel objTrimsModel = new TrimsModel();

            string sql = "";

            sql = "SELECT " +
            "STYLE_NO," +
            "STYLE_NAME," +
            "HEAD_OFFICE_ID," +
            "BRANCH_OFFICE_ID," +
            "SEASON_YEAR," +
            "SEASON_ID," +
            "SEASON_NAME " +
                   " FROM VEW_PRODUCT_MAIN where STYLE_NO = '" + styleId + "' AND SEASON_YEAR = '" + seasonYear + "' AND SEASON_NAME = '" + seasonName + "' AND HEAD_OFFICE_ID = '" + headOfficeId + "' AND BRANCH_OFFICE_ID = '" + branchOfficeId + "' ";

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
                        objTrimsModel.StyleNo = objDataReader.GetValue(0).ToString();
                        objTrimsModel.StyleName = objDataReader.GetValue(1).ToString();
                        objTrimsModel.SeasonYear = objDataReader.GetValue(4).ToString();
                        objTrimsModel.SeasonName = objDataReader.GetValue(5).ToString();
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
            return objTrimsModel;
        }

        public List<TrimsMain> GetTrimsGridDataList(string headOfficeId, string branchOfficeId)
        {
            List<TrimsMain> objTrimsMainList = new List<TrimsMain>();

            string sql = "";

            sql = "SELECT " +
           "STYLE_NO, " +
           "STYLE_NAME, " +
           "SEASON_ID, " +
           "SEASON_NAME, " +
           "SEASON_YEAR, " +
           "INTERLING, " +
           "MAIN_LABEL, " +
           "CARE_LABEL, " +
           "SIZE_LABEL, " +
           "SEWING_THREAD, " +
           "HANG_TAG, " +
           "UPDATE_BY, " +
           "UPDATE_DATE, " +
           "CREATE_BY, " +
           "CREATE_DATE, " +
           "HEAD_OFFICE_ID, " +
           "BRANCH_OFFICE_ID " +
                   " FROM VEW_TRIMS_DETAIL_MAIN where HEAD_OFFICE_ID = '" + headOfficeId.Trim() + "' AND BRANCH_OFFICE_ID = '" + branchOfficeId.Trim() + "' ";

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
                        TrimsMain objTrimsMain = new TrimsMain();
                        objTrimsMain.StyleNo = objDataReader.GetValue(0).ToString();
                        objTrimsMain.StyleName = objDataReader.GetValue(1).ToString();
                        objTrimsMain.SeasonId = objDataReader.GetValue(2).ToString();
                        objTrimsMain.SeasonName = objDataReader.GetValue(3).ToString();
                        objTrimsMain.SeasonYear = objDataReader.GetValue(4).ToString();
                        objTrimsMain.Interling = objDataReader.GetValue(5).ToString();
                        objTrimsMain.MainLabel = objDataReader.GetValue(6).ToString();
                        objTrimsMain.CareLabel = objDataReader.GetValue(7).ToString();
                        objTrimsMain.SizeLabel = objDataReader.GetValue(8).ToString();
                        objTrimsMain.SewingThread = objDataReader.GetValue(9).ToString();
                        objTrimsMain.HangTag = objDataReader.GetValue(10).ToString();

                        objTrimsMainList.Add(objTrimsMain);
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
            return objTrimsMainList;
        }

        public TrimsMain GetTrimsMainData(string seasonId, string seasonYear, string styleNumber, string headOfficeId, string branchOfficeId)
        {
            TrimsMain objTrimsMain = new TrimsMain();

            string sql = "";

            sql = "SELECT " +
           "STYLE_NO, " +
           "STYLE_NAME, " +
           "SEASON_ID, " +
           "SEASON_NAME, " +
           "SEASON_YEAR, " +
           "INTERLING, " +
           "MAIN_LABEL, " +
           "CARE_LABEL, " +
           "SIZE_LABEL, " +
           "SEWING_THREAD, " +
           "HANG_TAG, " +
           "UPDATE_BY, " +
           "UPDATE_DATE, " +
           "CREATE_BY, " +
           "CREATE_DATE, " +
           "HEAD_OFFICE_ID, " +
           "BRANCH_OFFICE_ID " +
                   " FROM VEW_TRIMS_DETAIL_MAIN where SEASON_ID = '" + seasonId.Trim() + "' AND SEASON_YEAR = '" + seasonYear.Trim() + "' AND STYLE_NO = '" + styleNumber.Trim() + "' AND HEAD_OFFICE_ID = '" + headOfficeId.Trim() + "' AND BRANCH_OFFICE_ID = '" + branchOfficeId.Trim() + "' ";

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
                        objTrimsMain.StyleNo = objDataReader.GetValue(0).ToString();
                        objTrimsMain.StyleName = objDataReader.GetValue(1).ToString();
                        objTrimsMain.SeasonId = objDataReader.GetValue(2).ToString();
                        objTrimsMain.SeasonName = objDataReader.GetValue(3).ToString();
                        objTrimsMain.SeasonYear = objDataReader.GetValue(4).ToString();
                        objTrimsMain.Interling = objDataReader.GetValue(5).ToString();
                        objTrimsMain.MainLabel = objDataReader.GetValue(6).ToString();
                        objTrimsMain.CareLabel = objDataReader.GetValue(7).ToString();
                        objTrimsMain.SizeLabel = objDataReader.GetValue(8).ToString();
                        objTrimsMain.SewingThread  = objDataReader.GetValue(9).ToString();
                        objTrimsMain.HangTag = objDataReader.GetValue(10).ToString();
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
            return objTrimsMain;
        }

        public string TrimsSubDelete(TrimsSub objTrimsSub)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_delete_Trims_sub");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsSub.TranId) ? objTrimsSub.TranId : null;

            objOracleCommand.Parameters.Add("p_style_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsSub.StyleNo) ? objTrimsSub.StyleNo : null;

            objOracleCommand.Parameters.Add("p_season_year", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsSub.SeasoneYear) ? objTrimsSub.SeasoneYear : null;

            objOracleCommand.Parameters.Add("p_season_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsSub.SeasoneId) ? objTrimsSub.SeasoneId : null;

            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsSub.HeadOfficeId) ? objTrimsSub.HeadOfficeId : null;

            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsSub.BranchOfficeId) ? objTrimsSub.BranchOfficeId : null;


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

        public string TrimsInformationSave(TrimsModel objTrimsModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_trims_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.StyleNo) ? objTrimsModel.StyleNo : null;

            objOracleCommand.Parameters.Add("p_STYLE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.StyleName) ? objTrimsModel.StyleName : null;

            objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.SeasonId) ? objTrimsModel.SeasonId : null;

            objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.SeasonYear) ? objTrimsModel.SeasonYear : null;

            objOracleCommand.Parameters.Add("p_INTERLING", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.Interling) ? objTrimsModel.Interling : null;

            objOracleCommand.Parameters.Add("p_MAIN_LABEL", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.MainLabel) ? objTrimsModel.MainLabel : null;

            objOracleCommand.Parameters.Add("p_CARE_LABEL", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.CareLabel) ? objTrimsModel.CareLabel : null;

            objOracleCommand.Parameters.Add("p_SIZE_LABEL", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.SizeLabel) ? objTrimsModel.SizeLabel : null;

            objOracleCommand.Parameters.Add("p_SEWING_THREAD", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.SewingThread) ? objTrimsModel.SewingThread : null;

            objOracleCommand.Parameters.Add("p_HANG_TAG", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.HangTag) ? objTrimsModel.HangTag : null;

            objOracleCommand.Parameters.Add("p_ACCESSORIES_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.AccessoriesS) ? objTrimsModel.AccessoriesS : null;

            objOracleCommand.Parameters.Add("p_TRIMS_CODE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.TrimsCodeS) ? objTrimsModel.TrimsCodeS : null;

            objOracleCommand.Parameters.Add("p_PER_GARMENTS_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.PerGarmentsQuantityS) ? objTrimsModel.PerGarmentsQuantityS : null;

            objOracleCommand.Parameters.Add("p_TOTAL_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.TotalQuantityS) ? objTrimsModel.TotalQuantityS : null;

            objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.TranIdS) ? objTrimsModel.TranIdS : null;




            objOracleCommand.Parameters.Add("P_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.UpdateBy) ? objTrimsModel.UpdateBy : null;
            objOracleCommand.Parameters.Add("p_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.HeadOfficeId) ? objTrimsModel.HeadOfficeId : null;
            objOracleCommand.Parameters.Add("p_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objTrimsModel.BranchOfficeId) ? objTrimsModel.BranchOfficeId : null;

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

        public List<TrimsSub> GetTrimsSubData(TrimsModel objTrimsModel)
        {
            List<TrimsSub> objTrimsSubMainList = new List<TrimsSub>();

            string sql = "";

            sql = "SELECT " +
           "ACCESSORIES_ID, " +
           "ACCESSORIES_NAME, " +
           "TRIMS_CODE, " +
           "TRAN_ID, " +
           "PER_GARMENTS_QUANTITY, " +
           "TOTAL_QUANTITY, " +
           "STYLE_NO, " +
           "SEASON_ID, " +
           "SEASON_YEAR, " +
           "BRANCH_OFFICE_ID " +
                   " FROM VEW_TRIMS_DETAIL_SUB where HEAD_OFFICE_ID = '" + objTrimsModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objTrimsModel.BranchOfficeId + "' AND STYLE_NO = '" + objTrimsModel.StyleNo + "' AND SEASON_ID = '" + objTrimsModel.SeasonId + "' AND SEASON_YEAR = '" + objTrimsModel.SeasonYear + "' ";

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
                        TrimsSub trimsSub = new TrimsSub();
                        trimsSub.Accessories = objDataReader.GetValue(0).ToString();
                        trimsSub.TrimsCode = objDataReader.GetValue(2).ToString();
                        trimsSub.TranId = objDataReader.GetValue(3).ToString();
                        trimsSub.PerGarmentsQuantity = objDataReader.GetValue(4).ToString();
                        trimsSub.TotalQuantity = objDataReader.GetValue(5).ToString();
                        trimsSub.StyleNo = objDataReader.GetValue(6).ToString();
                        trimsSub.SeasoneId = objDataReader.GetValue(7).ToString();
                        trimsSub.SeasoneYear = objDataReader.GetValue(8).ToString();

                        objTrimsSubMainList.Add(trimsSub);
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
            return objTrimsSubMainList;
        }
    }
}
