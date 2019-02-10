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
    public class CostingDAL
    {
        CostingModel objCostingModel = new CostingModel();

        OracleTransaction trans = null;
        #region "Oracle Connection Check"
        private OracleConnection GetConnection()
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConString"];
            string strConnString = conString.ConnectionString;
            return new OracleConnection(strConnString);
        }
        #endregion

        public DataTable GetAllStyleByYear(CostingModel objCostingModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +

                   "STYLE_NO " +

                " FROM VEW_BILL_OF_MATERIAL_MAIN where head_office_id = '" + objCostingModel.HeadOfficeId + "' AND branch_office_id = '" + objCostingModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objCostingModel.SearchBy))
            {

                sql = sql + "and (lower(SEASON_YEAR) like lower( '%" + objCostingModel.SearchBy + "%')  or upper(SEASON_YEAR)like upper('%" + objCostingModel.SearchBy + "%'))";
            }

            sql = sql + " ORDER BY STYLE_NO";

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

        public CostingModel GetStyleWiseBOM(CostingModel objCostingModel)
        {
            string sql = "";
            sql = "SELECT " +

                    "TO_CHAR (NVL (BUYER_ID,'0'))BUYER_ID, " +
                    "TO_CHAR (NVL (SEASON_ID,'0'))SEASON_ID, " +
                    "TO_CHAR (NVL (SEASON_YEAR,'0'))SEASON_YEAR, " +
                    "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO " +
                   
                 " FROM VEW_BILL_OF_MATERIAL_MAIN where head_office_id = '" + objCostingModel.HeadOfficeId + "' AND branch_office_id = '" + objCostingModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objCostingModel.SeasonYear))
            {
                sql = sql + " and SEASON_YEAR = '" + objCostingModel.SeasonYear + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.SeasonId))
            {
                sql = sql + " and SEASON_ID = '" + objCostingModel.SeasonId + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objCostingModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.BuyerId))
            {
                sql = sql + " and BUYER_ID = '" + objCostingModel.BuyerId + "'   ";
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
                        objCostingModel.BuyerId = objReader.GetString(0);
                        objCostingModel.SeasonId = objReader.GetString(1);
                        objCostingModel.SeasonYear = objReader.GetString(2);
                        objCostingModel.StyleNo = objReader.GetString(3);
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

            return objCostingModel;
        }
        public CostingModel GetCostingEntryMainForEdit(CostingModel objCostingModel)
        {
            string sql = "";
            sql = "SELECT " +

                    "TO_CHAR (NVL (BUYER_ID,'0'))BUYER_ID, " +
                    "TO_CHAR (NVL (SEASON_ID,'0'))SEASON_ID, " +
                    "TO_CHAR (NVL (SEASON_YEAR,'0'))SEASON_YEAR, " +
                    "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO, " +
                    "TO_CHAR (NVL (MPC_CODE,'N/A'))MPC_CODE, " +
                    "TO_CHAR (NVL (CURRENCY_ID, '0'))CURRENCY_ID, " +
                    "TO_CHAR (NVL (PRODUCT_NAME, 'N/A'))PRODUCT_NAME, " +
                    "TO_CHAR (NVL (FACTORY_ID, '0'))FACTORY_ID, " +
                    "TO_CHAR (NVL (EXCHANGE_RATE, '0'))EXCHANGE_RATE, " +
                    "TO_CHAR(COTATION_DATE, 'dd/MM/yyyy') COTATION_DATE " +

                 " FROM VEW_COSTING_ENTRY_MAIN where head_office_id = '" + objCostingModel.HeadOfficeId + "' AND branch_office_id = '" + objCostingModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objCostingModel.SeasonYear))
            {
                sql = sql + " and SEASON_YEAR = '" + objCostingModel.SeasonYear + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.SeasonId))
            {
                sql = sql + " and SEASON_ID = '" + objCostingModel.SeasonId + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objCostingModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.BuyerId))
            {
                sql = sql + " and BUYER_ID = '" + objCostingModel.BuyerId + "'   ";
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
                        objCostingModel.BuyerId = objReader.GetString(0);
                        objCostingModel.SeasonId = objReader.GetString(1);
                        objCostingModel.SeasonYear = objReader.GetString(2);
                        objCostingModel.StyleNo = objReader.GetString(3);
                        objCostingModel.MpcCode = objReader.GetString(4);
                        objCostingModel.CurrencyId = objReader.GetString(5);
                        objCostingModel.ProductName = objReader.GetString(6);
                        objCostingModel.FactoryId = objReader.GetString(7);
                        objCostingModel.ExchangeRate = objReader.GetString(8);
                        objCostingModel.CotationDate = objReader.GetString(9);
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

            return objCostingModel;
        }
        public DataTable SearchStyleFromBOM(CostingModel objCostingModel)
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
                 "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO " +
                

              " FROM VEW_BILL_OF_MATERIAL_MAIN where head_office_id = '" + objCostingModel.HeadOfficeId + "' AND branch_office_id = '" + objCostingModel.BranchOfficeId + "'  ";


            if (!string.IsNullOrEmpty(objCostingModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objCostingModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.SeasonName))
            {
                sql = sql + " and SEASON_NAME = '" + objCostingModel.SeasonName + "'   ";
            }                    
            if (!string.IsNullOrEmpty(objCostingModel.SeasonYear))
            {
                sql = sql + " and SEASON_YEAR = '" + objCostingModel.SeasonYear + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.BuyerName))
            {
                sql = sql + " and BUYER_NAME = '" + objCostingModel.BuyerName + "'   ";
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

        public DataTable SearchStyleFromCosting(CostingModel objCostingModel)
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
                 "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO " +


              " FROM VEW_COSTING_ENTRY_MAIN where head_office_id = '" + objCostingModel.HeadOfficeId + "' AND branch_office_id = '" + objCostingModel.BranchOfficeId + "'  ";


            if (!string.IsNullOrEmpty(objCostingModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objCostingModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.SeasonName))
            {
                sql = sql + " and SEASON_NAME = '" + objCostingModel.SeasonName + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.SeasonYear))
            {
                sql = sql + " and SEASON_YEAR = '" + objCostingModel.SeasonYear + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.BuyerName))
            {
                sql = sql + " and BUYER_NAME = '" + objCostingModel.BuyerName + "'   ";
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
        public DataTable GetBomRecord(CostingModel objCostingModel)
        {

            DataTable dt2 = new DataTable();

            string sql = "";

            sql = "SELECT " +
                  "ITEM_NAME, " +                
                  "ITEM_DESCRIPTION, " +
                  "MODEL_CODE, " +                 
                  "UNIT_ID, " +                 
                  "HEAD_OFFICE_ID, " +
                  "BRANCH_OFFICE_ID " +
                  "FROM VEW_BILL_OF_MATERIAL_SUB WHERE HEAD_OFFICE_ID = '" + objCostingModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objCostingModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objCostingModel.StyleNo))
            {

                sql = sql + "and STYLE_NO = '" + objCostingModel.StyleNo + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.BuyerId))
            {

                sql = sql + "and BUYER_ID = '" + objCostingModel.BuyerId + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.SeasonYear))
            {

                sql = sql + "and SEASON_YEAR= '" + objCostingModel.SeasonYear + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.SeasonId))
            {

                sql = sql + "and SEASON_ID = '" + objCostingModel.SeasonId + "'";
            }


            sql = sql + " ORDER BY STYLE_NO";

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
        public string SaveCostingEntry(CostingModel objCostingModel)
        {


            string strMsg = "";
            int x = objCostingModel.Item.Count();
            for (int i = 0; i < x; i++)
            {
                var arrayItem = objCostingModel.Item[i];
                var arrayItemType = objCostingModel.ItemType[i];
                var arrayItemDescription = objCostingModel.ItemDescription[i];
                var arrayModelCode = objCostingModel.ModelCode[i];
                var arraySupplierId = objCostingModel.SupplierId[i];
                var arrayUnitId = objCostingModel.UnitId[i];
                var arrayUnitPrice = objCostingModel.UnitPrice[i];
                var arrayConsump = objCostingModel.Consump[i];
                var arrayWastage = objCostingModel.Wastage[i];
                var arrayPrice = objCostingModel.Price[i];
                var arrayRate = objCostingModel.Rate[i];
                var arrayTranId = objCostingModel.TranId[i];

                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("PRO_COSTING_ENTRY_SAVE");
                objOracleCommand.CommandType = CommandType.StoredProcedure;


                if (objCostingModel.StyleNo != "")
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.StyleNo;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (objCostingModel.BuyerId != "")
                {
                    objOracleCommand.Parameters.Add("p_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.BuyerId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

               
                if (objCostingModel.SeasonId != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.SeasonId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (objCostingModel.SeasonYear != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.SeasonYear;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (objCostingModel.MpcCode != "")
                {
                    objOracleCommand.Parameters.Add("p_MPC_CODE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.MpcCode;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_MPC_CODE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (objCostingModel.CurrencyId != "")
                {
                    objOracleCommand.Parameters.Add("p_CURRENCY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.CurrencyId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_CURRENCY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (objCostingModel.ProductName != "")
                {
                    objOracleCommand.Parameters.Add("p_PRODUCT_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.ProductName;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_PRODUCT_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (objCostingModel.FactoryId != "")
                {
                    objOracleCommand.Parameters.Add("p_FACTORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.FactoryId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_FACTORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (objCostingModel.ExchangeRate != "")
                {
                    objOracleCommand.Parameters.Add("p_EXCHANGE_RATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.ExchangeRate;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_EXCHANGE_RATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (objCostingModel.CotationDate != "")
                {
                    objOracleCommand.Parameters.Add("p_COTATION_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.CotationDate;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_COTATION_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (arrayItem != null)
                {
                    objOracleCommand.Parameters.Add("p_ITEM_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayItem;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_ITEM_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (arrayItemType != null)
                {
                    objOracleCommand.Parameters.Add("p_ITEM_TYPE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayItemType;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_ITEM_TYPE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (arrayItemDescription != null)
                {
                    objOracleCommand.Parameters.Add("p_ITEM_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayItemDescription;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_ITEM_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (arrayModelCode != null)
                {
                    objOracleCommand.Parameters.Add("p_MODEL_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayModelCode;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_MODEL_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (arraySupplierId != null)
                {
                    objOracleCommand.Parameters.Add("p_SUPPLIER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = arraySupplierId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SUPPLIER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (arrayUnitId != null)
                {
                    objOracleCommand.Parameters.Add("p_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayUnitId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }



                if (arrayUnitPrice != null)
                {
                    objOracleCommand.Parameters.Add("p_UNIT_PRICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayUnitPrice;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_UNIT_PRICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }



                if (arrayConsump != null)
                {
                    objOracleCommand.Parameters.Add("p_CONSUMP", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayConsump;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_CONSUMP", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (arrayWastage != null)
                {
                    objOracleCommand.Parameters.Add("p_WASTAGE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayWastage;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_WASTAGE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (arrayPrice != null)
                {
                    objOracleCommand.Parameters.Add("p_PRICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayPrice;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_PRICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (arrayRate != null)
                {
                    objOracleCommand.Parameters.Add("p_RATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayRate;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_RATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (arrayTranId != null)
                {
                    objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayTranId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCostingModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCostingModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCostingModel.BranchOfficeId;



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
        public DataTable GetCostingEntryRecord(CostingModel objCostingModel)
        {

            DataTable dt1 = new DataTable();

            string sql = "";

            sql = "SELECT " +
                 "ROWNUM SL, " +
                 "STYLE_NO, " +
                 "BUYER_ID, " +
                 "BUYER_NAME, " +              
                 "SEASON_ID, " +
                 "SEASON_NAME, " +
                 "SEASON_YEAR, " +
                 "MPC_CODE, " +
                  "CURRENCY_ID, " +
                    "CURRENCY_NAME, " +
                      "PRODUCT_NAME, " +
                        "FACTORY_ID, " +
                          "FACTORY_NAME, " +
                           "EXCHANGE_RATE, " +
                            "TO_CHAR(COTATION_DATE, 'dd/MM/yyyy') COTATION_DATE " +
                           

                "FROM VEW_COSTING_ENTRY_MAIN WHERE HEAD_OFFICE_ID = '" + objCostingModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objCostingModel.BranchOfficeId + "'  ";


            if (!string.IsNullOrEmpty(objCostingModel.SearchBy))
            {

                sql = sql + "and (lower(STYLE_NO) like lower( '%" + objCostingModel.SearchBy + "%')  or upper(STYLE_NO)like upper('%" + objCostingModel.SearchBy + "%') ) or (lower(SEASON_YEAR) like lower( '%" + objCostingModel.SearchBy + "%')  or upper(SEASON_YEAR)like upper('%" + objCostingModel.SearchBy + "%') or (lower(BUYER_NAME) like lower( '%" + objCostingModel.SearchBy + "%')  or upper(BUYER_NAME)like upper('%" + objCostingModel.SearchBy + "%') ) )";
            }

            if (!string.IsNullOrEmpty(objCostingModel.StyleNo))
            {

                sql = sql + "and STYLE_NO = '" + objCostingModel.StyleNo + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.BuyerId))
            {

                sql = sql + "and BUYER_ID = '" + objCostingModel.BuyerId + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.SeasonYear))
            {

                sql = sql + "and SEASON_YEAR= '" + objCostingModel.SeasonYear + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.SeasonId))
            {

                sql = sql + "and SEASON_ID = '" + objCostingModel.SeasonId + "'";
            }

           
            if (!string.IsNullOrEmpty(objCostingModel.BuyerName))
            {

                sql = sql + "and BUYER_NAME = '" + objCostingModel.BuyerName + "'";
            }

           
            if (!string.IsNullOrEmpty(objCostingModel.SeasonName))
            {

                sql = sql + "and SEASON_NAME = '" + objCostingModel.SeasonName + "'";
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
        public DataTable GetCostingEntryRecordForConfirm(CostingModel objCostingModel)
        {

            DataTable dt1 = new DataTable();

            string sql = "";

            sql = "SELECT " +
                 "ROWNUM SL, " +
                 "STYLE_NO, " +
                 "BUYER_ID, " +
                 "BUYER_NAME, " +
                 "SEASON_ID, " +
                 "SEASON_NAME, " +
                 "SEASON_YEAR, " +
                 "MPC_CODE, " +
                  "CURRENCY_ID, " +
                    "CURRENCY_NAME, " +
                      "PRODUCT_NAME, " +
                        "FACTORY_ID, " +
                          "FACTORY_NAME, " +
                           "EXCHANGE_RATE, " +
                            "TO_CHAR(COTATION_DATE, 'dd/MM/yyyy') COTATION_DATE, " +
                             "APPROVED_STATUS " +


                "FROM VEW_COSTING_ENTRY_APPROVED WHERE HEAD_OFFICE_ID = '" + objCostingModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objCostingModel.BranchOfficeId + "' AND APPROVED_YN = 'N'  ";


            if (!string.IsNullOrEmpty(objCostingModel.StyleSearch))
            {

                sql = sql + "and ((lower(STYLE_NO) like lower( '%" + objCostingModel.StyleSearch + "%')  or upper(STYLE_NO)like upper('%" + objCostingModel.StyleSearch + "%') ) or (lower(SEASON_YEAR) like lower( '%" + objCostingModel.StyleSearch + "%')  or upper(SEASON_YEAR)like upper('%" + objCostingModel.StyleSearch + "%') or (lower(BUYER_NAME) like lower( '%" + objCostingModel.StyleSearch + "%')  or upper(BUYER_NAME)like upper('%" + objCostingModel.StyleSearch + "%') ) ))";
            }

            if (!string.IsNullOrEmpty(objCostingModel.StyleNo))
            {

                sql = sql + "and STYLE_NO = '" + objCostingModel.StyleNo + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.BuyerName))
            {

                sql = sql + "and BUYER_NAME = '" + objCostingModel.BuyerName + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.SeasonYear))
            {

                sql = sql + "and SEASON_YEAR= '" + objCostingModel.SeasonYear + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.SeasonName))
            {

                sql = sql + "and SEASON_NAME = '" + objCostingModel.SeasonName + "'";
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
        public DataTable GetListRecordForEdit(CostingModel objCostingModel)
        {

            DataTable dt2 = new DataTable();

            string sql = "";

            sql = "SELECT " +
                 "ROWNUM SL, " +
                 "STYLE_NO, " +
                 "BUYER_ID, " +
                 "BUYER_NAME, " +
                 "SEASON_ID, " +
                 "SEASON_NAME, " +
                 "SEASON_YEAR, " +
                 "MPC_CODE, " +
                  "CURRENCY_ID, " +
                    "CURRENCY_NAME, " +
                      "PRODUCT_NAME, " +
                        "FACTORY_ID, " +
                          "FACTORY_NAME, " +
                           "EXCHANGE_RATE, " +
                            "TO_CHAR(COTATION_DATE, 'dd/MM/yyyy') COTATION_DATE, " +                          
                            "ITEM_NAME, " +
                            "ITEM_TYPE, " +
                            "ITEM_DESCRIPTION, " +
                            "MODEL_CODE, " +
                            "SUPPLIER_ID, " +
                            "SUPPLIER_NAME, " +
                            "SUPPLIER_NAME, " +
                             "UNIT_ID, " +
                            "UNIT_NAME, " +
                            "UNIT_PRICE, " +
                            "CONSUMP, " +
                            "WASTAGE, " +
                            "PRICE, " +
                            "RATE, " +
                            "TRAN_ID " +

                "FROM VEW_COSTING_ENTRY_SUB WHERE HEAD_OFFICE_ID = '" + objCostingModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objCostingModel.BranchOfficeId + "'  ";


            if (!string.IsNullOrEmpty(objCostingModel.StyleNo))
            {

                sql = sql + "and STYLE_NO = '" + objCostingModel.StyleNo + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.BuyerId))
            {

                sql = sql + "and BUYER_ID = '" + objCostingModel.BuyerId + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.SeasonYear))
            {

                sql = sql + "and SEASON_YEAR= '" + objCostingModel.SeasonYear + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.SeasonId))
            {

                sql = sql + "and SEASON_ID = '" + objCostingModel.SeasonId + "'";
            }


            sql = sql + " ORDER BY STYLE_NO";

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
        public string DeleteCostingEntry(CostingModel objCostingModel)
        {

            string strMsg = "";

            string[] TranIdArray = objCostingModel.GridTranId.Split(',');
            int x = TranIdArray.Count();

            for (int i = 0; i < x; i++)
            {

                var arrayTranId = TranIdArray[i];

                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("PRO_COSTING_ENTRY_DELETE");
                objOracleCommand.CommandType = CommandType.StoredProcedure;


                if (objCostingModel.StyleNo != "")
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.StyleNo;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (objCostingModel.BuyerId != "")
                {
                    objOracleCommand.Parameters.Add("p_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.BuyerId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (objCostingModel.SeasonId != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.SeasonId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (objCostingModel.SeasonYear != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.SeasonYear;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (arrayTranId != null)
                {
                    objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayTranId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCostingModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCostingModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCostingModel.BranchOfficeId;



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

        public string ApprovedCostingEntry(CostingModel objCostingModel)
        {
                string strMsg = "";
                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("PRO_COSTING_ENTRY_APPROVED");
                objOracleCommand.CommandType = CommandType.StoredProcedure;


                if (objCostingModel.StyleNo != "")
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.StyleNo;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (objCostingModel.BuyerId != "")
                {
                    objOracleCommand.Parameters.Add("p_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.BuyerId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (objCostingModel.SeasonId != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.SeasonId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (objCostingModel.SeasonYear != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.SeasonYear;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                


                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCostingModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCostingModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCostingModel.BranchOfficeId;



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

        public string UpdateCostingEntryForApproved(CostingModel objCostingModel)
        {


            string strMsg = "";
            int x = objCostingModel.Item.Count();
            for (int i = 0; i < x; i++)
            {
                var arrayItem = objCostingModel.Item[i];
                var arrayItemType = objCostingModel.ItemType[i];
                var arrayItemDescription = objCostingModel.ItemDescription[i];
                var arrayModelCode = objCostingModel.ModelCode[i];
                var arraySupplierId = objCostingModel.SupplierId[i];
                var arrayUnitId = objCostingModel.UnitId[i];
                var arrayUnitPrice = objCostingModel.UnitPrice[i];
                var arrayConsump = objCostingModel.Consump[i];
                var arrayWastage = objCostingModel.Wastage[i];
                var arrayPrice = objCostingModel.Price[i];
                var arrayRate = objCostingModel.Rate[i];
                var arrayTranId = objCostingModel.TranId[i];

                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("PRO_COSTING_ENTRY_UPDATE");
                objOracleCommand.CommandType = CommandType.StoredProcedure;


                if (objCostingModel.StyleNo != "")
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.StyleNo;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (objCostingModel.BuyerId != "")
                {
                    objOracleCommand.Parameters.Add("p_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.BuyerId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (objCostingModel.SeasonId != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.SeasonId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (objCostingModel.SeasonYear != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.SeasonYear;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                //if (objCostingModel.MpcCode != "")
                //{
                //    objOracleCommand.Parameters.Add("p_MPC_CODE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.MpcCode;
                //}
                //else
                //{
                //    objOracleCommand.Parameters.Add("p_MPC_CODE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                //}
                //if (objCostingModel.CurrencyId != "")
                //{
                //    objOracleCommand.Parameters.Add("p_CURRENCY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.CurrencyId;
                //}
                //else
                //{
                //    objOracleCommand.Parameters.Add("p_CURRENCY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                //}

                //if (objCostingModel.ProductName != "")
                //{
                //    objOracleCommand.Parameters.Add("p_PRODUCT_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.ProductName;
                //}
                //else
                //{
                //    objOracleCommand.Parameters.Add("p_PRODUCT_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                //}

                //if (objCostingModel.FactoryId != "")
                //{
                //    objOracleCommand.Parameters.Add("p_FACTORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.FactoryId;
                //}
                //else
                //{
                //    objOracleCommand.Parameters.Add("p_FACTORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                //}

                //if (objCostingModel.ExchangeRate != "")
                //{
                //    objOracleCommand.Parameters.Add("p_EXCHANGE_RATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.ExchangeRate;
                //}
                //else
                //{
                //    objOracleCommand.Parameters.Add("p_EXCHANGE_RATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                //}


                //if (objCostingModel.CotationDate != "")
                //{
                //    objOracleCommand.Parameters.Add("p_COTATION_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCostingModel.CotationDate;
                //}
                //else
                //{
                //    objOracleCommand.Parameters.Add("p_COTATION_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                //}

                if (arrayItem != null)
                {
                    objOracleCommand.Parameters.Add("p_ITEM_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayItem;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_ITEM_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (arrayItemType != null)
                {
                    objOracleCommand.Parameters.Add("p_ITEM_TYPE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayItemType;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_ITEM_TYPE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (arrayItemDescription != null)
                {
                    objOracleCommand.Parameters.Add("p_ITEM_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayItemDescription;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_ITEM_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (arrayModelCode != null)
                {
                    objOracleCommand.Parameters.Add("p_MODEL_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayModelCode;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_MODEL_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (arraySupplierId != null)
                {
                    objOracleCommand.Parameters.Add("p_SUPPLIER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = arraySupplierId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SUPPLIER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (arrayUnitId != null)
                {
                    objOracleCommand.Parameters.Add("p_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayUnitId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }



                if (arrayUnitPrice != null)
                {
                    objOracleCommand.Parameters.Add("p_UNIT_PRICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayUnitPrice;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_UNIT_PRICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }



                if (arrayConsump != null)
                {
                    objOracleCommand.Parameters.Add("p_CONSUMP", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayConsump;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_CONSUMP", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (arrayWastage != null)
                {
                    objOracleCommand.Parameters.Add("p_WASTAGE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayWastage;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_WASTAGE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (arrayPrice != null)
                {
                    objOracleCommand.Parameters.Add("p_PRICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayPrice;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_PRICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (arrayRate != null)
                {
                    objOracleCommand.Parameters.Add("p_RATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayRate;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_RATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (arrayTranId != null)
                {
                    objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayTranId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCostingModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCostingModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCostingModel.BranchOfficeId;



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

        public DataTable SearchPendingListStyle(CostingModel objCostingModel)
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
                 "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO " +

               "FROM VEW_COSTING_ENTRY_APPROVED WHERE HEAD_OFFICE_ID = '" + objCostingModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objCostingModel.BranchOfficeId + "' AND APPROVED_YN = 'N'  ";


            if (!string.IsNullOrEmpty(objCostingModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objCostingModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.SeasonName))
            {
                sql = sql + " and SEASON_NAME = '" + objCostingModel.SeasonName + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.SeasonYear))
            {
                sql = sql + " and SEASON_YEAR = '" + objCostingModel.SeasonYear + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.BuyerName))
            {
                sql = sql + " and BUYER_NAME = '" + objCostingModel.BuyerName + "'   ";
            }

            sql = sql + " ORDER BY SEASON_YEAR DESC ";

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


        public DataTable SearchApprovedListStyle(CostingModel objCostingModel)
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
                 "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO " +

               "FROM VEW_COSTING_ENTRY_APPROVED WHERE HEAD_OFFICE_ID = '" + objCostingModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objCostingModel.BranchOfficeId + "' AND APPROVED_YN = 'A'  ";


            if (!string.IsNullOrEmpty(objCostingModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objCostingModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.SeasonName))
            {
                sql = sql + " and SEASON_NAME = '" + objCostingModel.SeasonName + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.SeasonYear))
            {
                sql = sql + " and SEASON_YEAR = '" + objCostingModel.SeasonYear + "'   ";
            }
            if (!string.IsNullOrEmpty(objCostingModel.BuyerName))
            {
                sql = sql + " and BUYER_NAME = '" + objCostingModel.BuyerName + "'   ";
            }

            sql = sql + " ORDER BY SEASON_YEAR DESC ";

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

        public DataTable GetCostingEntryApprovedRecord(CostingModel objCostingModel)
        {

            DataTable dt1 = new DataTable();

            string sql = "";

            sql = "SELECT " +
                 "ROWNUM SL, " +
                 "STYLE_NO, " +
                 "BUYER_ID, " +
                 "BUYER_NAME, " +
                 "SEASON_ID, " +
                 "SEASON_NAME, " +
                 "SEASON_YEAR, " +
                 "MPC_CODE, " +
                  "CURRENCY_ID, " +
                    "CURRENCY_NAME, " +
                      "PRODUCT_NAME, " +
                        "FACTORY_ID, " +
                          "FACTORY_NAME, " +
                           "EXCHANGE_RATE, " +
                            "TO_CHAR(COTATION_DATE, 'dd/MM/yyyy') COTATION_DATE, " +
                             "APPROVED_STATUS " +


                "FROM VEW_COSTING_ENTRY_APPROVED WHERE HEAD_OFFICE_ID = '" + objCostingModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objCostingModel.BranchOfficeId + "' AND APPROVED_YN = 'A'  ";


            if (!string.IsNullOrEmpty(objCostingModel.StyleSearch))
            {

                sql = sql + "and ( (lower(STYLE_NO) like lower( '%" + objCostingModel.StyleSearch + "%')  or upper(STYLE_NO)like upper('%" + objCostingModel.StyleSearch + "%') ) or (lower(SEASON_YEAR) like lower( '%" + objCostingModel.StyleSearch + "%')  or upper(SEASON_YEAR)like upper('%" + objCostingModel.StyleSearch + "%') or (lower(BUYER_NAME) like lower( '%" + objCostingModel.StyleSearch + "%')  or upper(BUYER_NAME)like upper('%" + objCostingModel.StyleSearch + "%') ) ) )";
            }

            if (!string.IsNullOrEmpty(objCostingModel.StyleNo))
            {

                sql = sql + "and STYLE_NO = '" + objCostingModel.StyleNo + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.BuyerName))
            {

                sql = sql + "and BUYER_NAME = '" + objCostingModel.BuyerName + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.SeasonYear))
            {

                sql = sql + "and SEASON_YEAR= '" + objCostingModel.SeasonYear + "'";
            }

            if (!string.IsNullOrEmpty(objCostingModel.SeasonName))
            {

                sql = sql + "and SEASON_NAME = '" + objCostingModel.SeasonName + "'";
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
