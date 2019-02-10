using System;
using System.Collections.Generic;
using System.Data;
using ERP.MODEL;
using Oracle.ManagedDataAccess.Client;

namespace ERP.DAL
{
    public class PoEntryDAL
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

        //Save and Delete Data
        public string SavePoEntry(PoEntryModel objPoEntryModel)
        {
            string strMsg;

            OracleCommand objOracleCommand = new OracleCommand("PRO_PO_INFO_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.TranIdS) ? objPoEntryModel.TranIdS : null;

            objOracleCommand.Parameters.Add("P_PO_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.PoNumber) ? objPoEntryModel.PoNumber : null;

            objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.StyleNo) ? objPoEntryModel.StyleNo : null;

            objOracleCommand.Parameters.Add("P_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.SeasoneName) ? objPoEntryModel.SeasoneName : null;

            objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.SeasoneYear) ? objPoEntryModel.SeasoneYear : null;

            objOracleCommand.Parameters.Add("P_COST_PRICE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.CostPrice) ? objPoEntryModel.CostPrice : null;

            objOracleCommand.Parameters.Add("P_RETAIL_PRICE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.RetailPrice) ? objPoEntryModel.RetailPrice : null;

            objOracleCommand.Parameters.Add("P_FABRIC_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.FabricQuantity) ? objPoEntryModel.FabricQuantity : null;

            objOracleCommand.Parameters.Add("P_FIT_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.Fit) ? objPoEntryModel.Fit : null;

            objOracleCommand.Parameters.Add("P_store_delivery_date", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.StoreDeleveryDate) ? objPoEntryModel.StoreDeleveryDate : null;

            objOracleCommand.Parameters.Add("P_size_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.SizeIdS) ? objPoEntryModel.SizeIdS : null;

            objOracleCommand.Parameters.Add("P_size_value", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.SizeValueS) ? objPoEntryModel.SizeValueS : null;


            objOracleCommand.Parameters.Add("P_EMBROIDARY_YN", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.EmbroidaryS) ? objPoEntryModel.EmbroidaryS : null;
            objOracleCommand.Parameters.Add("P_KARCHUPI_YN", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.KarchupiS) ? objPoEntryModel.KarchupiS : null;
            objOracleCommand.Parameters.Add("P_PRINT_YN", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.PrintS) ? objPoEntryModel.PrintS : null;
            objOracleCommand.Parameters.Add("P_WASH_YN", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.WashS) ? objPoEntryModel.WashS : null;


            objOracleCommand.Parameters.Add("P_OCCASION_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.Occasion) ? objPoEntryModel.Occasion : null;



            objOracleCommand.Parameters.Add("P_COLOR_WAY_NO_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.ColorWayNumberS) ? objPoEntryModel.ColorWayNumberS : null;

            objOracleCommand.Parameters.Add("P_COLOR_WAY_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.ColorWayNameS) ? objPoEntryModel.ColorWayNameS : null;

            objOracleCommand.Parameters.Add("P_INSERT_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.InsertDate) ? objPoEntryModel.InsertDate : null;

            




            objOracleCommand.Parameters.Add("P_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.UpdateBy) ? objPoEntryModel.UpdateBy : null;
            objOracleCommand.Parameters.Add("p_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.HeadOfficeId) ? objPoEntryModel.HeadOfficeId : null;
            objOracleCommand.Parameters.Add("p_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntryModel.BranchOfficeId) ? objPoEntryModel.BranchOfficeId : null;

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

        public string SavePoComment(PoComments objPoComments)
        {
            string strMsg;

            OracleCommand objOracleCommand = new OracleCommand("pro_PO_COMMENT_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.TranId) ? objPoComments.TranId : null;

            objOracleCommand.Parameters.Add("P_PO_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.PoNumber) ? objPoComments.PoNumber : null;

            objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.StyleNo) ? objPoComments.StyleNo : null;

            objOracleCommand.Parameters.Add("P_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.SeasoneId) ? objPoComments.SeasoneId : null;

            objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.SeasoneYear) ? objPoComments.SeasoneYear : null;

            objOracleCommand.Parameters.Add("P_PO_COMMENT", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.PoComment) ? objPoComments.PoComment : null;


            objOracleCommand.Parameters.Add("P_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.UpdateBy) ? objPoComments.UpdateBy : null;
            objOracleCommand.Parameters.Add("P_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.HeadOfficeId) ? objPoComments.HeadOfficeId : null;
            objOracleCommand.Parameters.Add("P_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.BranchOfficeId) ? objPoComments.BranchOfficeId : null;

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

        public string PoEntrySubDelete(PoEntrySub objPoEntrySub)
        {
            string strMsg;

            OracleCommand objOracleCommand = new OracleCommand("pro_delete_po_sub");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntrySub.TranId) ? objPoEntrySub.TranId : null;

            objOracleCommand.Parameters.Add("p_style_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntrySub.StyleNo) ? objPoEntrySub.StyleNo : null;

            objOracleCommand.Parameters.Add("p_season_year", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntrySub.SeasoneYear) ? objPoEntrySub.SeasoneYear : null;

            objOracleCommand.Parameters.Add("p_season_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntrySub.SeasoneId) ? objPoEntrySub.SeasoneId : null;

            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntrySub.HeadOfficeId) ? objPoEntrySub.HeadOfficeId : null;

            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoEntrySub.BranchOfficeId) ? objPoEntrySub.BranchOfficeId : null;


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

        public string PoCommentDelete(PoComments objPoComments)
        {
            string strMsg;

            OracleCommand objOracleCommand = new OracleCommand("pro_delete_po_comment");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.TranId) ? objPoComments.TranId : null;

            objOracleCommand.Parameters.Add("p_style_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.StyleNo) ? objPoComments.StyleNo : null;

            objOracleCommand.Parameters.Add("p_season_year", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.SeasoneYear) ? objPoComments.SeasoneYear : null;

            objOracleCommand.Parameters.Add("p_season_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.SeasoneId) ? objPoComments.SeasoneId : null;

            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.HeadOfficeId) ? objPoComments.HeadOfficeId : null;

            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objPoComments.BranchOfficeId) ? objPoComments.BranchOfficeId : null;


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


        //Get Data
        public DataTable GetStyleNumberList(string query = null)
        {
            DataTable dt = new DataTable();
            string sql;

            if (query != null)
            {
                sql = "SELECT " +
                    "SL, " +
                    "STYLE_NO, " +
                    "STYLE_NAME, " +
                    "HEAD_OFFICE_ID, " +
                    "BRANCH_OFFICE_ID, " +
                    "SEASON_YEAR, " +
                    "SEASON_ID, " +
                    "SEASON_NAME " +
                    "FROM VEW_PRODUCT_MAIN where STYLE_NO LIKE '"+ query.ToUpper() + "%' OR STYLE_NO LIKE '" + query.ToLower() + "%' ";
            }
            else
            {
                sql = "SELECT " +
                  "SL, " +
                   "STYLE_NO, " +
                   "STYLE_NAME, " +
                   "HEAD_OFFICE_ID, " +
                   "BRANCH_OFFICE_ID, " +
                   "SEASON_YEAR, " +
                   "SEASON_ID, " +
                   "SEASON_NAME " +
                  "FROM VEW_PRODUCT_MAIN ";
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
        
        public DataTable CheckStyleNumber(string styleId)
        {
            DataTable dt = new DataTable();

            var sql = "SELECT " +
                         "SL, " +
                         "STYLE_NO, " +
                         "STYLE_NAME, " +
                         "HEAD_OFFICE_ID, " +
                         "BRANCH_OFFICE_ID, " +
                         "SEASON_YEAR, " +
                         "SEASON_ID, " +
                         "SEASON_NAME " +
                         "FROM VEW_PRODUCT_MAIN where STYLE_NO = '"+styleId+"' ";

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

        public PoEntryModel GetPoEntryData(string styleId, string seasonYear, string seasonName, string headOfficeId, string branchOfficeId)
        {
            PoEntryModel objPoEntryModel = new PoEntryModel();

            var sql = "SELECT " +
                         "STYLE_NO," +
                         "STYLE_NAME," +
                         "HEAD_OFFICE_ID," +
                         "BRANCH_OFFICE_ID," +
                         "SEASON_YEAR," +
                         "SEASON_ID," +
                         "SEASON_NAME," +
                         "FIT_ID," +
                         "FIT_NAME,"+
                         "SIZE_ID," +
                         "OCCASION_ID," +
                         "PO_NO " +
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
                        objPoEntryModel.StyleNo = objDataReader.GetValue(0).ToString();
                        objPoEntryModel.StyleName = objDataReader.GetValue(1).ToString();
                        objPoEntryModel.SeasoneYear = objDataReader.GetValue(4).ToString();
                        objPoEntryModel.SeasoneName = objDataReader.GetValue(5).ToString();
                        objPoEntryModel.Fit = objDataReader.GetValue(7).ToString();
                        objPoEntryModel.Occasion = objDataReader.GetValue(10).ToString();
                        objPoEntryModel.PoNumber = objDataReader.GetValue(11).ToString();                 
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
            return objPoEntryModel;
        }

        public PoEntryMain GetPoMainData(string seasonId, string seasonYear, string styleNumber, string headOfficeId, string branchOfficeId)
        {
            PoEntryMain objPoEntryMainModel = new PoEntryMain();

            var sql = "SELECT " +
                         "PO_NO," +
                         "STYLE_NO," +
                         "STYLE_NAME," +
                         "SEASON_ID," +
                         "SEASON_NAME," +
                         "SEASON_YEAR," +
                         "COST_PRICE," +
                         "RETAIL_PRICE," +
                         "FABRIC_QUANTITY," +
                         "FIT_ID," +
                         "STORE_DELIVERY_DATE," +
                         "EMBROIDARY_YN," +
                         "KARCHUPI_YN," +
                         "PRINT_YN," +
                         "WASH_YN," +
                         "INSERT_DATE," +
                         "OCCASION_ID, " +
                         "CREATE_BY," +
                         "CREATE_DATE," +
                         "UPDATE_BY," +
                         "UPDATE_DATE," +
                         "HEAD_OFFICE_ID," +
                         "BRANCH_OFFICE_ID " +
                         " FROM VEW_PO_MAIN where SEASON_ID = '" + seasonId.Trim() + "' AND SEASON_YEAR = '" + seasonYear.Trim() + "' AND STYLE_NO = '" + styleNumber.Trim() + "' AND HEAD_OFFICE_ID = '" + headOfficeId + "' AND BRANCH_OFFICE_ID = '" + branchOfficeId + "' ";

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
                        objPoEntryMainModel.PoNumber = objDataReader.GetValue(0).ToString();
                        objPoEntryMainModel.StyleNo = objDataReader.GetValue(1).ToString();
                        objPoEntryMainModel.StyleName = objDataReader.GetValue(2).ToString();
                        objPoEntryMainModel.SeasoneId = objDataReader.GetValue(3).ToString();
                        objPoEntryMainModel.SeasoneName = objDataReader.GetValue(4).ToString();
                        objPoEntryMainModel.SeasoneYear = objDataReader.GetValue(5).ToString();
                        objPoEntryMainModel.CostPrice = objDataReader.GetValue(6).ToString();
                        objPoEntryMainModel.RetailPrice = objDataReader.GetValue(7).ToString();
                        objPoEntryMainModel.FabricQuantity = objDataReader.GetValue(8).ToString();
                        objPoEntryMainModel.FitId = objDataReader.GetValue(9).ToString();
                        objPoEntryMainModel.StoreDeleveryDate = objDataReader.GetDateTime(10).ToString("dd/MM/yyyy");

                        objPoEntryMainModel.Embroidary = objDataReader.GetValue(11).ToString() == "Y";
                        objPoEntryMainModel.Karchupi = objDataReader.GetValue(12).ToString() == "Y";
                        objPoEntryMainModel.Print = objDataReader.GetValue(13).ToString() == "Y";
                        objPoEntryMainModel.Wash = objDataReader.GetValue(14).ToString() == "Y";
                        objPoEntryMainModel.InsertDate = objDataReader.GetDateTime(15).ToString("dd/MM/yyyy");
                        objPoEntryMainModel.Occasion = objDataReader.GetValue(16).ToString();
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
            return objPoEntryMainModel;
        }

        public List<PoEntryGrid> GetPoEntryMainData()
        {
            List<PoEntryGrid> objPoEntryModel = new List<PoEntryGrid>();

            var sql = "SELECT " +
                         "PO_NO," +
                         "STYLE_NO," +
                         "SEASON_ID," +
                         "SEASON_NAME," +
                         "SEASON_YEAR," +
                         "COST_PRICE," +
                         "RETAIL_PRICE," +
                         "FABRIC_QUANTITY," +
                         "STORE_DELIVERY_DATE," +
                         "CREATE_BY," +
                         "CREATE_DATE," +
                         "UPDATE_BY," +
                         "UPDATE_DATE," +
                         "HEAD_OFFICE_ID," +
                         "BRANCH_OFFICE_ID " +
                         " FROM VEW_PO_MAIN ORDER BY SEASON_YEAR ";

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
                        PoEntryGrid model = new PoEntryGrid();

                        model.PoNumber = objDataReader.GetValue(0).ToString();
                        model.StyleNo = objDataReader.GetValue(1).ToString();
                        model.SeasoneId = objDataReader.GetValue(2).ToString();
                        model.SeasoneName = objDataReader.GetValue(3).ToString();
                        model.SeasoneYear = objDataReader.GetValue(4).ToString();
                        model.CostPrice = objDataReader.GetValue(5).ToString();
                        model.RetailPrice = objDataReader.GetValue(6).ToString();
                        model.FabricQuantity = objDataReader.GetValue(7).ToString();
                        model.StoreDeleveryDate = objDataReader.GetValue(8).ToString();

                        objPoEntryModel.Add(model);
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
            return objPoEntryModel;
        }

        public List<PoEntrySub> GetPoSubData(string seasonId, string seasonYear, string styleNumber, string headOfficeId, string branchOfficeId)
        {
            List<PoEntrySub> objPoEntrySubModel = new List<PoEntrySub>();

            var sql = "SELECT " +
                         "PO_NO," +
                         "STYLE_NO," +
                         "SEASON_ID," +
                         "SEASON_YEAR," +
                         "COLOR_ID," +
                         "FABRIC_TYPE_ID," +
                         "FABRIC_CODE," +
                         "COLOR_WAY_NO_ID, "+
                         "COLOR_WAY_NAME, "+
                         "SIZE_ID," +
                         "SIZE_VALUE," +
                         "CREATE_BY," +
                         "CREATE_DATE," +
                         "UPDATE_BY," +
                         "UPDATE_DATE," +
                         "HEAD_OFFICE_ID," +
                         "BRANCH_OFFICE_ID," +
                         "TRAN_ID " +
                         " FROM VEW_PO_SUB where SEASON_ID = '" + seasonId.Trim() + "' AND SEASON_YEAR = '" + seasonYear.Trim() + "' AND STYLE_NO = '" + styleNumber.Trim() + "' AND HEAD_OFFICE_ID = '" + headOfficeId + "' AND BRANCH_OFFICE_ID = '" + branchOfficeId + "' ";

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
                        PoEntrySub model = new PoEntrySub();

                        model.PoNumber = objDataReader.GetValue(0).ToString();
                        model.StyleNo = objDataReader.GetValue(1).ToString();
                        model.SeasoneId = objDataReader.GetValue(2).ToString();
                        model.SeasoneYear = objDataReader.GetValue(3).ToString();
                        model.ColorWayNumber = objDataReader.GetValue(7).ToString();
                        model.ColorWayName = objDataReader.GetValue(8).ToString();
                        model.SizeId = objDataReader.GetValue(9).ToString();
                        model.SizeValue = objDataReader.GetValue(10).ToString();
                        model.TranId = objDataReader.GetValue(17).ToString();

                        objPoEntrySubModel.Add(model);
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
            return objPoEntrySubModel;
        }

        public List<PoComments> GetPoCommentList(string seasonId, string seasonYear, string styleNumber, string headOfficeId, string branchOfficeId)
        {
            List<PoComments> objPoCommentses = new List<PoComments>();

            var sql = "SELECT " +
                         "TRAN_ID, " +
                         "PO_NO, " +
                         "STYLE_NO, " +
                         "SEASON_ID, " +
                         "SEASON_YEAR, " +
                         "PO_COMMENT, " +
                         "HEAD_OFFICE_ID, " +
                         "BRANCH_OFFICE_ID " +
                         " FROM VIEW_PO_COMMENT where SEASON_ID = '" + seasonId.Trim() + "' AND SEASON_YEAR = '" + seasonYear.Trim() + "' AND STYLE_NO = '" + styleNumber.Trim() + "' AND HEAD_OFFICE_ID = '" + headOfficeId + "' AND BRANCH_OFFICE_ID = '" + branchOfficeId + "' ";

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
                        PoComments model = new PoComments();

                        model.TranId = objDataReader.GetValue(0).ToString();
                        model.PoNumber = objDataReader.GetValue(1).ToString();
                        model.StyleNo = objDataReader.GetValue(2).ToString();
                        model.SeasoneId = objDataReader.GetValue(3).ToString();
                        model.SeasoneYear = objDataReader.GetValue(4).ToString();
                        model.PoComment = objDataReader.GetValue(5).ToString();
                        model.HeadOfficeId = objDataReader.GetValue(6).ToString();
                        model.BranchOfficeId = objDataReader.GetValue(7).ToString();


                        objPoCommentses.Add(model);
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
            return objPoCommentses;
        }

        public DataTable GetColorDDList(string styleNo, string seasonId, string seasonYear, string headOfficeId, string branchOfficeId)
        {
            DataTable dt = new DataTable();
            var sql = "SELECT " +
                         "COLOR_WAY_NO_ID, " +
                         "COLOR_WAY_NUMBER " +
                         "FROM VEW_COLOR_WAY_ID where STYLE_NO = '" + styleNo + "' AND SEASON_ID = '"+ seasonId + "' AND SEASON_YEAR = '" + seasonYear + "' AND HEAD_OFFICE_ID = '" + headOfficeId + "' AND BRANCH_OFFICE_ID = '" + branchOfficeId + "' ";

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

        public DataTable GetColorWayDDList(string styleNo, string seasonId, string seasonYear, string headOfficeId, string branchOfficeId)
        {
            DataTable dt = new DataTable();
            var sql = "SELECT " +
                         "color_way_name " +
                         "FROM vew_color_way_name where STYLE_NO = '" + styleNo + "' AND SEASON_ID = '" + seasonId + "' AND SEASON_YEAR = '" + seasonYear + "' AND HEAD_OFFICE_ID = '" + headOfficeId + "' AND BRANCH_OFFICE_ID = '" + branchOfficeId + "'  order by color_way_name ";

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

        public DataTable GetSizeDDList(string styleNo, string seasonId, string seasonYear)
        {
            DataTable dt = new DataTable();
            var sql = "SELECT " +
                         "SIZE_ID, " +
                         "SIZE_NAME, " +
                         "SEASON_YEAR," +
                         "SEASON_ID," +
                         "STYLE_NO " +
                         "FROM VEW_PO_ENTRY_DATA_PRODUCT_SUB where STYLE_NO = '" + styleNo + "' AND SEASON_ID = '" + seasonId + "' AND SEASON_YEAR = '" + seasonYear + "'  order by SIZE_NAME ";

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
    }
}
