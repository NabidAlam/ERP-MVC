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
    public class BuyerEnquiryDAL
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

        public DataTable GetBuyerEnquiryRecord(BuyerEnquiryModel objBuyerEnquiryModel)
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
                   "TO_CHAR (NVL (BRAND_ID,'0'))BRAND_ID, " +
                   "TO_CHAR (NVL (BRAND_NAME,'N/A'))BRAND_NAME, " +
                   "TO_CHAR (NVL (ITEM_ID,'0'))ITEM_ID, " +
                   "TO_CHAR (NVL (ITEM_NAME,'N/A'))ITEM_NAME, " +
                   "TO_CHAR (NVL (ORDER_QUANTITY,'0'))ORDER_QUANTITY, " +
                   "TO_CHAR (NVL (FOB,'0'))FOB, " +
                   "TO_CHAR (NVL (CURRENCY_ID,'0'))CURRENCY_ID, " +
                   "TO_CHAR (NVL (CURRENCY_NAME,'N/A'))CURRENCY_NAME " +

                " FROM VEW_STYLE_ENQUIRY where head_office_id = '" + objBuyerEnquiryModel.HeadOfficeId + "' AND branch_office_id = '" + objBuyerEnquiryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.SeasonYear))
            {
                sql = sql + "and SEASON_YEAR = '" + objBuyerEnquiryModel.SeasonYear + "'";
            }
            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objBuyerEnquiryModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.SeasonName))
            {
                sql = sql + " and SEASON_NAME = '" + objBuyerEnquiryModel.SeasonName + "'   ";
            }
            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.BuyerName))
            {
                sql = sql + " and BUYER_NAME = '" + objBuyerEnquiryModel.BuyerName + "'   ";
            }

            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.SearchBy))
            {

                sql = sql + "and (lower(SEASON_YEAR) like lower( '%" + objBuyerEnquiryModel.SearchBy + "%')  or upper(SEASON_YEAR)like upper('%" + objBuyerEnquiryModel.SearchBy + "%') ) or (lower(STYLE_NO) like lower( '%" + objBuyerEnquiryModel.SearchBy + "%')  or upper(STYLE_NO)like upper('%" + objBuyerEnquiryModel.SearchBy + "%') or (lower(BUYER_NAME) like lower( '%" + objBuyerEnquiryModel.SearchBy + "%')  or upper(BUYER_NAME)like upper('%" + objBuyerEnquiryModel.SearchBy + "%') ) )";
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
        public string SaveBuyerEnquiry(BuyerEnquiryModel objBuyerEnquiryModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_style_enquiry_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objBuyerEnquiryModel.BuyerId != "")
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBuyerEnquiryModel.BuyerId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objBuyerEnquiryModel.SeasonId != "")
            {
                objOracleCommand.Parameters.Add("P_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEnquiryModel.SeasonId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objBuyerEnquiryModel.SeasonYear != "")
            {
                objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBuyerEnquiryModel.SeasonYear.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objBuyerEnquiryModel.StyleNo != "")
            {
                objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBuyerEnquiryModel.StyleNo.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objBuyerEnquiryModel.BrandId != "")
            {
                objOracleCommand.Parameters.Add("P_BRAND_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBuyerEnquiryModel.BrandId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BRAND_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objBuyerEnquiryModel.ItemId != "")
            {
                objOracleCommand.Parameters.Add("P_ITEM_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBuyerEnquiryModel.ItemId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_ITEM_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objBuyerEnquiryModel.OrderQuantity != "")
            {
                objOracleCommand.Parameters.Add("P_ORDER_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBuyerEnquiryModel.OrderQuantity.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_ORDER_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objBuyerEnquiryModel.FOB != "")
            {
                objOracleCommand.Parameters.Add("P_FOB", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBuyerEnquiryModel.FOB.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FOB", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (objBuyerEnquiryModel.CurrencyTypeId != "")
            {
                objOracleCommand.Parameters.Add("P_CURRENCY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBuyerEnquiryModel.CurrencyTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FOB", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEnquiryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEnquiryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEnquiryModel.BranchOfficeId;



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
        public BuyerEnquiryModel GetBuyerEnquiryById(BuyerEnquiryModel objBuyerEnquiryModel)
        {
            string sql = "";
            sql = "SELECT " +
                   
                    "TO_CHAR (NVL (BUYER_ID,'0'))BUYER_ID, " +
                    "TO_CHAR (NVL (SEASON_ID,'0'))SEASON_ID, " +
                    "TO_CHAR (NVL (SEASON_YEAR,'0'))SEASON_YEAR, " +
                    "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO, " +
                    "TO_CHAR (NVL (BRAND_ID,'0'))BRAND_ID, " +
                    "TO_CHAR (NVL (ITEM_ID,'0'))ITEM_ID, " +
                    "TO_CHAR (NVL (ORDER_QUANTITY,'0'))ORDER_QUANTITY, " +
                    "TO_CHAR (NVL (FOB,'0'))FOB, " +
                    "TO_CHAR (NVL (CURRENCY_ID,'0'))CURRENCY_ID " +

                 " FROM VEW_STYLE_ENQUIRY where head_office_id = '" + objBuyerEnquiryModel.HeadOfficeId + "' AND branch_office_id = '" + objBuyerEnquiryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.SeasonYear))
            {
                sql = sql + " and SEASON_YEAR = '" + objBuyerEnquiryModel.SeasonYear + "'   ";
            }
            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.SeasonId))
            {
                sql = sql + " and SEASON_ID = '" + objBuyerEnquiryModel.SeasonId + "'   ";
            }
            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objBuyerEnquiryModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.BuyerId))
            {
                sql = sql + " and BUYER_ID = '" + objBuyerEnquiryModel.BuyerId + "'   ";
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
                        objBuyerEnquiryModel.BuyerId = objReader.GetString(0);
                        objBuyerEnquiryModel.SeasonId = objReader.GetString(1);
                        objBuyerEnquiryModel.SeasonYear = objReader.GetString(2);
                        objBuyerEnquiryModel.StyleNo = objReader.GetString(3);
                        objBuyerEnquiryModel.BrandId =objReader.GetString(4);
                        objBuyerEnquiryModel.ItemId = objReader.GetString(5);
                        objBuyerEnquiryModel.OrderQuantity = objReader.GetString(6);
                        objBuyerEnquiryModel.FOB = objReader.GetString(7);
                        objBuyerEnquiryModel.CurrencyTypeId = objReader.GetString(8);


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

            return objBuyerEnquiryModel;
        }
        public string DeleteBuyerEnquiryRecord(BuyerEnquiryModel objBuyerEnquiryModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_style_enquiry_delete");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objBuyerEnquiryModel.SeasonYear != "")
            {
                objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEnquiryModel.SeasonYear.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objBuyerEnquiryModel.SeasonId != "")
            {
                objOracleCommand.Parameters.Add("P_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEnquiryModel.SeasonId.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }
            if (objBuyerEnquiryModel.StyleNo != "")
            {
                objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEnquiryModel.StyleNo.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }
            if (objBuyerEnquiryModel.BuyerId != "")
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEnquiryModel.BuyerId.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objBuyerEnquiryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objBuyerEnquiryModel.BranchOfficeId;

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

        public DataTable GetAllStyleByYear(BuyerEnquiryModel objBuyerEnquiryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                 "TO_CHAR (NVL (BUYER_ID,'0'))BUYER_ID, " +
                 "TO_CHAR (NVL (BUYER_NAME,'N/A'))BUYER_NAME, " +
                 "TO_CHAR (NVL (SEASON_ID,'0'))SEASON_ID, " +
                 "TO_CHAR (NVL (SEASON_NAME,'N/A'))SEASON_NAME, " +
                 "TO_CHAR (NVL (SEASON_YEAR,'0'))SEASON_YEAR, " +
                 "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO " +
                " FROM VEW_STYLE_ENQUIRY where head_office_id = '" + objBuyerEnquiryModel.HeadOfficeId + "' AND branch_office_id = '" + objBuyerEnquiryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.SeasonYear))
            {
                sql = sql + "and SEASON_YEAR = '" + objBuyerEnquiryModel.SeasonYear + "'";
            }
            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objBuyerEnquiryModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.SeasonName))
            {
                sql = sql + " and SEASON_NAME = '" + objBuyerEnquiryModel.SeasonName + "'   ";
            }
            if (!string.IsNullOrEmpty(objBuyerEnquiryModel.BuyerName))
            {
                sql = sql + " and BUYER_NAME = '" + objBuyerEnquiryModel.BuyerName + "'   ";
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
