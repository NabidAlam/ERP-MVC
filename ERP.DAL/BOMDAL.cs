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
    public class BOMDAL
    {
       
        BOMModel objBOMModel = new BOMModel();

        OracleTransaction trans = null;
        #region "Oracle Connection Check"
        private OracleConnection GetConnection()
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConString"];
            string strConnString = conString.ConnectionString;
            return new OracleConnection(strConnString);
        }
        #endregion

        public DataTable GetBomRecord(BOMModel objBOMModel)
        {

            DataTable dt1 = new DataTable();

            string sql = "";

            sql = "SELECT " +
                "ROWNUM SL, " +
                "STYLE_NO, " +
                "BUYER_ID, " +
                "BUYER_NAME, " +
                "SEASON_YEAR, " +
                "SEASON_ID, " +
                "SEASON_NAME, " +
                "R3_CODE, " +
                "TO_CHAR(LAST_UPDATE_DATE, 'dd/MM/yyyy') LAST_UPDATE_DATE, " +            
                "HEAD_OFFICE_ID, " +
                "BRANCH_OFFICE_ID " +
                "FROM VEW_BILL_OF_MATERIAL_MAIN WHERE HEAD_OFFICE_ID = '" + objBOMModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objBOMModel.BranchOfficeId + "'  ";


            if (!string.IsNullOrEmpty(objBOMModel.SearchBy))
            {

                sql = sql + "and (lower(STYLE_NO) like lower( '%" + objBOMModel.SearchBy + "%')  or upper(STYLE_NO)like upper('%" + objBOMModel.SearchBy + "%') ) or (lower(SEASON_YEAR) like lower( '%" + objBOMModel.SearchBy + "%')  or upper(SEASON_YEAR)like upper('%" + objBOMModel.SearchBy + "%') or (lower(BUYER_NAME) like lower( '%" + objBOMModel.SearchBy + "%')  or upper(BUYER_NAME)like upper('%" + objBOMModel.SearchBy + "%') ) )";
            }

            if (!string.IsNullOrEmpty(objBOMModel.SeasonYear))
            {
                sql = sql + "and SEASON_YEAR = '" + objBOMModel.SeasonYear + "'";
            }
            if (!string.IsNullOrEmpty(objBOMModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objBOMModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objBOMModel.SeasonName))
            {
                sql = sql + " and SEASON_NAME = '" + objBOMModel.SeasonName + "'   ";
            }
            if (!string.IsNullOrEmpty(objBOMModel.BuyerName))
            {
                sql = sql + " and BUYER_NAME = '" + objBOMModel.BuyerName + "'   ";
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

        public DataTable GetBomRecordForEdit(BOMModel objBOMModel)
        {

            DataTable dt2 = new DataTable();

            string sql = "";

            sql = "SELECT " +
                  "STYLE_NO, " +
                  "BUYER_ID, " +
                  "SEASON_ID, " +
                  "SEASON_YEAR, " +
                  "R3_CODE, " +
                  "TO_CHAR(LAST_UPDATE_DATE, 'dd/MM/yyyy') LAST_UPDATE_DATE, " +
                  "ITEM_NAME, " +
                  "ITEM_DESCRIPTION, " +
                  "MODEL_CODE, " +
                  "COMPONENT_NAME, " +
                  "COLOR_CODE, " +
                  "COLOR_NAME, " +
                  "FABRIC_QUANTITY, " +
                  "UNIT_ID, " +
                  "REMARKS, " +
                  "TRAN_ID, " +
                  "HEAD_OFFICE_ID, " +
                  "BRANCH_OFFICE_ID " +
                  "FROM VEW_BILL_OF_MATERIAL_SUB WHERE HEAD_OFFICE_ID = '" + objBOMModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objBOMModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objBOMModel.StyleNo))
            {

                sql = sql + "and STYLE_NO = '" + objBOMModel.StyleNo + "'";
            }

            if (!string.IsNullOrEmpty(objBOMModel.BuyerId))
            {

                sql = sql + "and BUYER_ID = '" + objBOMModel.BuyerId + "'";
            }

            //if (!string.IsNullOrEmpty(objBOMModel.ItemTypeId))
            //{

            //    sql = sql + "and ITEM_TYPE_ID = '" + objBOMModel.ItemTypeId + "'";
            //}

            if (!string.IsNullOrEmpty(objBOMModel.SeasonYear))
            {

                sql = sql + "and SEASON_YEAR= '" + objBOMModel.SeasonYear + "'";
            }

            if (!string.IsNullOrEmpty(objBOMModel.SeasonId))
            {

                sql = sql + "and SEASON_ID = '" + objBOMModel.SeasonId + "'";
            }


            if (objBOMModel.LastUpdateDate.Length > 6)
            {

                sql = sql + "and LAST_UPDATE_DATE= TO_DATE( '" + objBOMModel.LastUpdateDate + "', 'dd/MM/yyyy' )";
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
      
        public string SaveBomEntry(BOMModel objBOMModel)
        {


            string strMsg = "";


            int x = objBOMModel.ItemDescription.Count();
            for (int i = 0; i < x; i++)
            {
                var arrayItemName = objBOMModel.ItemName[i];
                var arrayItemDescription = objBOMModel.ItemDescription[i];
                var arrayModelCode = objBOMModel.ModelCode[i];
                var arrayComponentName = objBOMModel.ComponentName[i];
                var arrayColorId = objBOMModel.ColorId[i];
                var arrayColorName = objBOMModel.ColorName[i];
                var arrayFabricQuantity = objBOMModel.FabricQuantity[i];
                var arrayUnitId = objBOMModel.UnitId[i];
                var arrayRemarks = objBOMModel.Remarks[i];
                var arrayTranId = objBOMModel.TranId[i];

                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("pro_bill_of_material_save");
                objOracleCommand.CommandType = CommandType.StoredProcedure;


                if (objBOMModel.StyleNo != "")
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBOMModel.StyleNo;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (objBOMModel.BuyerId != "")
                {
                    objOracleCommand.Parameters.Add("p_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBOMModel.BuyerId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (objBOMModel.SeasonYear != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBOMModel.SeasonYear;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }



                if (objBOMModel.SeasonId != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBOMModel.SeasonId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }



                if (objBOMModel.R3Code != "")
                {
                    objOracleCommand.Parameters.Add("p_R3_CODE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBOMModel.R3Code;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_R3_CODE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }



                if (objBOMModel.LastUpdateDate != "")
                {
                    objOracleCommand.Parameters.Add("p_LAST_UPDATE_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBOMModel.LastUpdateDate;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_LAST_UPDATE_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }



                if (arrayItemName != "")
                {
                    objOracleCommand.Parameters.Add("p_ITEM_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayItemName;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_ITEM_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
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


                if (arrayComponentName != null)
                {
                    objOracleCommand.Parameters.Add("p_COMPONENT_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayComponentName;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_COMPONENT_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }



                if (arrayColorId != null)
                {
                    objOracleCommand.Parameters.Add("p_COLOR_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayColorId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_COLOR_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (arrayColorName != null)
                {
                    objOracleCommand.Parameters.Add("p_COLOR_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayColorName;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_COLOR_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }



                if (arrayFabricQuantity != null)
                {
                    objOracleCommand.Parameters.Add("p_FABRIC_QUANTITY", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayFabricQuantity;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_FABRIC_QUANTITY", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }



                if (arrayUnitId != null)
                {
                    objOracleCommand.Parameters.Add("p_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayUnitId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (arrayRemarks != null)
                {
                    objOracleCommand.Parameters.Add("p_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayRemarks;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                if (arrayTranId != null)
                {
                    objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayTranId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }


                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBOMModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBOMModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBOMModel.BranchOfficeId;



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

        public string DeleteBomEntry(BOMModel objBOMModel)
        {

            string strMsg = "";

            string[] TranIdArray = objBOMModel.GridTranId.Split(',');
            int x = TranIdArray.Count();

            for (int i = 0; i < x; i++)
            {
               
                var arrayTranId = TranIdArray[i];

                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("pro_bill_of_material_delete");
                objOracleCommand.CommandType = CommandType.StoredProcedure;


                if (objBOMModel.StyleNo != "")
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBOMModel.StyleNo;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                

                if (objBOMModel.BuyerId != "")
                {
                    objOracleCommand.Parameters.Add("p_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBOMModel.BuyerId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                
                if (objBOMModel.SeasonId != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBOMModel.SeasonId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (objBOMModel.SeasonYear != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBOMModel.SeasonYear;
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


                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBOMModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBOMModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBOMModel.BranchOfficeId;



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

        public DataTable GetAllStyleByYear(BOMModel objBOMModel)
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
                " FROM VEW_STYLE_ENQUIRY where head_office_id = '" + objBOMModel.HeadOfficeId + "' AND branch_office_id = '" + objBOMModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objBOMModel.SeasonYear))
            {
                sql = sql + "and SEASON_YEAR = '" + objBOMModel.SeasonYear + "'";
            }
            if (!string.IsNullOrEmpty(objBOMModel.StyleNo))
            {
                sql = sql + " and STYLE_NO = '" + objBOMModel.StyleNo + "'   ";
            }
            if (!string.IsNullOrEmpty(objBOMModel.SeasonName))
            {
                sql = sql + " and SEASON_NAME = '" + objBOMModel.SeasonName + "'   ";
            }           
            if (!string.IsNullOrEmpty(objBOMModel.BuyerName))
            {
                sql = sql + " and BUYER_NAME = '" + objBOMModel.BuyerName + "'   ";
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

        


    }
} 
