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
    public class TrimAccessoriesDAL
    {
        TrimsAccessoriesOrderModel objAccessoriesOrderModel = new TrimsAccessoriesOrderModel();

        OracleTransaction trans = null;
        #region "Oracle Connection Check"
        private OracleConnection GetConnection()
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConString"];
            string strConnString = conString.ConnectionString;
            return new OracleConnection(strConnString);
        }
        #endregion


        public DataTable GetTrimsRecord(TrimsAccessoriesOrderModel objAccessoriesOrderModel)
        {
            //Master
            DataTable dt1 = new DataTable();
            string sql = "";

            sql = "SELECT " +
             "ROWNUM SL, " +
             "TO_CHAR(ORDER_DATE, 'dd/mm/yyyy') ORDER_DATE, " +
             "ITEM_ID, " +
             "ITEM_NAME, " +
             "ITEM_CODE, " +
             "ORDER_QUANTITY, " +
             "UNIT_ID, " +
             "UNIT_NAME, " +
             "TO_CHAR(DELIVERY_DATE, 'dd/mm/yyyy') DELIVERY_DATE, " +
             "REMARKS, " +
             
             "SUPPLIER_ID, " +
             "SUPPLIER_NAME, " +
             "STORE_ID, " +
             "STORE_NAME, " +
             "UNIT_PRICE, " +
             "STYLE_NO, " +

             "PENDING_YN, " +      //new update for confirmation on 12 Dec 18
                
             "TO_CHAR(SUPPLIER_DATE, 'dd/mm/yyyy') SUPPLIER_DATE," +
             "TO_CHAR(REVISED_DATE, 'dd/mm/yyyy') REVISED_DATE," +
             "TO_CHAR(IN_HOUSE_DATE, 'dd/mm/yyyy') IN_HOUSE_DATE," +

             "TRAN_ID, " +
             "UPDATE_BY, " +
             "UPDATE_DATE, " +
             "CREATE_BY, " +
             "CREATE_DATE, " +
             "HEAD_OFFICE_ID, " +
             "BRANCH_OFFICE_ID " +
             "FROM VEW_TRIMS_ACCESSORIES_ORDER WHERE HEAD_OFFICE_ID = '" + objAccessoriesOrderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objAccessoriesOrderModel.BranchOfficeId + "'  ";

            //"FROM VEW_BILL_OF_MATERIAL_MAIN WHERE HEAD_OFFICE_ID = '" + objTrimsModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objTrimsModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrWhiteSpace(objAccessoriesOrderModel.SearchBy))
            {

                sql = sql + "and ((lower(ITEM_NAME) like lower( '%" + objAccessoriesOrderModel.SearchBy.Trim() + "%')  " +
                      "or upper(ITEM_NAME)like upper('%" + objAccessoriesOrderModel.SearchBy.Trim() + "%')) " +
                      "or (TO_CHAR (ORDER_DATE, 'dd/mm/yyyy') like '" + objAccessoriesOrderModel.SearchBy.Trim() + "' or TO_CHAR (ORDER_DATE, 'dd/mm/yyyy') like '" + objAccessoriesOrderModel.SearchBy.Trim() + "')" +
                      "or (lower(STORE_NAME) like lower( '%" + objAccessoriesOrderModel.SearchBy.Trim() + "%')  " +
                      "or upper(STORE_NAME)like upper('%" + objAccessoriesOrderModel.SearchBy.Trim() + "%')) " +
                      "or (TO_CHAR (DELIVERY_DATE, 'dd/mm/yyyy') like '" + objAccessoriesOrderModel.SearchBy.Trim() + "' or TO_CHAR (DELIVERY_DATE, 'dd/mm/yyyy') like '" + objAccessoriesOrderModel.SearchBy.Trim() + "')" +
                      "or (lower(ITEM_NAME) like lower( '%" + objAccessoriesOrderModel.SearchBy.Trim() + "%')  " +
                      "or upper(ITEM_NAME)like upper('%" + objAccessoriesOrderModel.SearchBy.Trim() + "%')) " +
                      "or (lower(SUPPLIER_NAME) like lower( '%" + objAccessoriesOrderModel.SearchBy.Trim() + "%')  " +
                      "or upper(SUPPLIER_NAME)like upper('%" + objAccessoriesOrderModel.SearchBy.Trim() + "%')) " +
                      "or (lower(STYLE_NO) like lower( '%" + objAccessoriesOrderModel.SearchBy.Trim() + "%')  " +
                      "or upper(STYLE_NO)like upper('%" + objAccessoriesOrderModel.SearchBy.Trim() + "%')) " +
                      "or (lower(ITEM_CODE) like lower( '%" + objAccessoriesOrderModel.SearchBy.Trim() + "%')  " +
                      "or upper(ITEM_CODE)like upper('%" + objAccessoriesOrderModel.SearchBy.Trim() + "%')))";
            }
            //else
            //{
            //    sql = sql + "and (ORDER_DATE like TO_DATE( '" + objAccessoriesOrderModel.SearchBy + "', 'dd/mm/yyyy')  " +
            //          "or ORDER_DATE like TO_DATE('" + objAccessoriesOrderModel.SearchBy + "', 'dd/mm/yyyy') )" +
            //              "or (DELIVERY_DATE like TO_DATE( '" + objAccessoriesOrderModel.SearchBy + "', 'dd/mm/yyyy')  " +
            //              "or DELIVERY_DATE like TO_DATE( '" + objAccessoriesOrderModel.SearchBy + "', 'dd/mm/yyyy')))";
            //}


            /*"or (TO_CHAR (PURCHASE_DATE, 'dd/mm/yyyy') like '" + objFabricModel.SearchBy + "' or TO_CHAR (PURCHASE_DATE, 'dd/mm/yyyy') like '" + objFabricModel.SearchBy + "')" +*/

            //sql = sql + " ORDER BY sl";
            sql = sql + " ORDER BY ORDER_DATE DESC";

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

        public DataTable GetTrimsRecordForEdit(TrimsAccessoriesOrderModel objAccessoriesOrderModel)
        {

            DataTable dt2 = new DataTable();

            string sql = "";

            sql = "SELECT " +
            "ROWNUM SL, " +
            "TO_CHAR(ORDER_DATE, 'dd/mm/yyyy') ORDER_DATE," +
            "ITEM_ID, " +
            "ITEM_NAME, " +
            "ITEM_CODE, " +
            "ORDER_QUANTITY, " +
            "UNIT_ID, " +
            "UNIT_NAME, " +
            "TO_CHAR(DELIVERY_DATE, 'dd/mm/yyyy') DELIVERY_DATE," +
            "REMARKS, " +
            "TRAN_ID, " +
          
            "SUPPLIER_ID, " +
            "SUPPLIER_NAME, " +
            "STORE_ID, " +
            "STORE_NAME, " +
            "UNIT_PRICE, " +
            "STYLE_NO, " +

            "UPDATE_BY, " +
            "UPDATE_DATE, " +
            "CREATE_BY, " +
            "CREATE_DATE, " +
            "HEAD_OFFICE_ID, " +
            "BRANCH_OFFICE_ID " +
            "FROM VEW_TRIMS_ACCESSORIES_ORDER WHERE HEAD_OFFICE_ID = '" + objAccessoriesOrderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objAccessoriesOrderModel.BranchOfficeId + "'  ";

            //if (!string.IsNullOrEmpty(objAccessoriesOrderModel.GridOrderDate))               
            //{

            //    sql = sql + "and ORDER_DATE =To_Date( '" + objAccessoriesOrderModel.GridOrderDate + "', 'dd/mm/yy')";
            //}

            //if (!string.IsNullOrEmpty(objAccessoriesOrderModel.GridItemId))
            //{

            //    sql = sql + "and ITEM_ID= '" + objAccessoriesOrderModel.GridItemId + "'";
            //}

            //if (!string.IsNullOrEmpty(objAccessoriesOrderModel.GridItemCode))
            //{

            //    sql = sql + "and ITEM_CODE = '" + objAccessoriesOrderModel.GridItemCode + "'";
            //}


            if (!string.IsNullOrEmpty(objAccessoriesOrderModel.GridItemId))
            {

                sql = sql + "and TRAN_ID= '" + objAccessoriesOrderModel.GridTranId + "'";
            }


            sql = sql + " ORDER BY ORDER_DATE DESC";

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



        public string SaveTrimsEntry(TrimsAccessoriesOrderModel objAccessoriesOrderModel)
        {


            string strMsg = "";


            int x = objAccessoriesOrderModel.ItemCode.Count();
            for (int i = 0; i < x; i++)
            {
                var arrayOrderDate = objAccessoriesOrderModel.OrderDate[i];
                var arrayItemId = objAccessoriesOrderModel.ItemId[i];
                var arrayItemCode = objAccessoriesOrderModel.ItemCode[i];

                var arrayOrderQty = objAccessoriesOrderModel.OrderQty[i];

                var arrayUnitId = objAccessoriesOrderModel.UnitId[i];

                var arrayDeliveryDate = objAccessoriesOrderModel.DeliveryDate[i];

                var arrayRemarks = objAccessoriesOrderModel.Remarks[i];
                var arrayTranId = objAccessoriesOrderModel.TranId[i];

                var arraySupplierId = objAccessoriesOrderModel.SupplierId[i];
                var arrayStoreId = objAccessoriesOrderModel.StoreId[i];
                var arrayUnitPrice = objAccessoriesOrderModel.UnitPrice[i];
                var arrayStyleNo = objAccessoriesOrderModel.SyleNo[i];


                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("pro_trims_accessories_save");
                objOracleCommand.CommandType = CommandType.StoredProcedure;

                //p_arrayOrderDate p_arrayItemId p_arrayItemCode p_arrayOrderQty p_arrayUnitId p_arrayDeliveryDate p_arrayRemarks p_TRAN_ID
                objOracleCommand.Parameters.Add("p_arrayOrderDate", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayOrderDate.Length > 0 ? arrayOrderDate : null;
                objOracleCommand.Parameters.Add("p_arrayItemId", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayItemId.Length > 0 ? arrayItemId : null;
                objOracleCommand.Parameters.Add("p_arrayItemCode", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayItemCode.Length > 0 ? arrayItemCode : null;
                objOracleCommand.Parameters.Add("p_arrayOrderQty", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayOrderQty.Length > 0 ? arrayOrderQty : null;
                objOracleCommand.Parameters.Add("p_arrayUnitId", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayUnitId.Length > 0 ? arrayUnitId : null;
                objOracleCommand.Parameters.Add("p_arrayDeliveryDate", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayDeliveryDate.Length > 0 ? arrayDeliveryDate : null;
                objOracleCommand.Parameters.Add("p_arrayRemarks", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayRemarks.Length > 0 ? arrayRemarks : null;


                objOracleCommand.Parameters.Add("p_SupplierId", OracleDbType.Varchar2, ParameterDirection.Input).Value = arraySupplierId.Length > 0 ? arraySupplierId : null;

                objOracleCommand.Parameters.Add("p_StoreId", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayStoreId.Length > 0 ? arrayStoreId : null;

                objOracleCommand.Parameters.Add("p_UnitPrice", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayUnitPrice.Length > 0 ? arrayUnitPrice : null;

                objOracleCommand.Parameters.Add("p_StyleNo", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayStyleNo.Length > 0 ? arrayStyleNo : null;


                objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = arrayTranId.Length > 0 ? arrayTranId : null;
                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAccessoriesOrderModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAccessoriesOrderModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAccessoriesOrderModel.BranchOfficeId;
                // objOracleCommand.Parameters.Add("p_create_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objTrimsModel.UpdateBy;
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
            }
            return strMsg;
        }

        public string DeleteTrimsAccEntry(TrimsAccessoriesOrderModel objAccessoriesOrderModel)
        {
            string vMessage = "";

            OracleCommand objCommand = new OracleCommand("PRO_TRIMS_ACCESSORIES_DELETE") { CommandType = CommandType.StoredProcedure };

            objCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objAccessoriesOrderModel.GridTranId) ? objAccessoriesOrderModel.GridTranId : null;

            //objCommand.Parameters.Add("p_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricModel.UpdateBy;
            objCommand.Parameters.Add("p_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAccessoriesOrderModel.HeadOfficeId;
            objCommand.Parameters.Add("p_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAccessoriesOrderModel.BranchOfficeId;

            objCommand.Parameters.Add("p_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

            using (OracleConnection objConnection = GetConnection())
            {
                try
                {
                    objCommand.Connection = objConnection;
                    objConnection.Open();
                    trans = objConnection.BeginTransaction();
                    objCommand.ExecuteNonQuery();
                    trans.Commit();
                    objConnection.Close();
                    vMessage = objCommand.Parameters["P_MESSAGE"].Value.ToString();
                }
                catch (Exception ex)
                {
                    trans.Rollback();
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    trans.Dispose();
                    objConnection.Close();
                }
            }

            return vMessage;
        }





        //after adding three datetime column
        public string AuthorizeTrimsAcc(TrimsAccessoriesOrderModel objAccessoriesOrderModel)
        {
            string strMsg = "";
            OracleCommand objOracleCommand = new OracleCommand("pro_trims_acc_authorize");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            
            //if (objAccessoriesOrderModel.InHouseDate != "")
            if (!string.IsNullOrEmpty(objAccessoriesOrderModel.InHouseDate))
            {
                objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value =
                    objAccessoriesOrderModel.GridTranId != "" ? objAccessoriesOrderModel.GridTranId : null;
                objOracleCommand.Parameters.Add("P_SupplierDate", OracleDbType.Varchar2, ParameterDirection.Input).Value
                    = objAccessoriesOrderModel.SupplierDate != "" ? objAccessoriesOrderModel.SupplierDate : null;
                objOracleCommand.Parameters.Add("P_RevisedDate", OracleDbType.Varchar2, ParameterDirection.Input).Value
                    = objAccessoriesOrderModel.RevisedDate ?? null;
                objOracleCommand.Parameters.Add("P_InHouseDate", OracleDbType.Varchar2, ParameterDirection.Input).Value
                    = objAccessoriesOrderModel.InHouseDate ?? null;
                objOracleCommand.Parameters.Add("P_Pending_YN", OracleDbType.Varchar2, ParameterDirection.Input).Value =
                    'Y';


            }

            else
            {
                objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAccessoriesOrderModel.GridTranId != "" ? objAccessoriesOrderModel.GridTranId : null;
                objOracleCommand.Parameters.Add("P_SupplierDate", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAccessoriesOrderModel.SupplierDate != "" ? objAccessoriesOrderModel.SupplierDate : null;
                objOracleCommand.Parameters.Add("P_RevisedDate", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAccessoriesOrderModel.RevisedDate ?? null;
                objOracleCommand.Parameters.Add("P_InHouseDate", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAccessoriesOrderModel.InHouseDate ?? null;
                objOracleCommand.Parameters.Add("P_Pending_YN", OracleDbType.Varchar2, ParameterDirection.Input).Value = 'N';

            }


            //if (objAccessoriesOrderModel.InHouseDate != null)
            //{
            //    objOracleCommand.Parameters.Add("P_InHouseDate", OracleDbType.Varchar2, ParameterDirection.Input).Value = objAccessoriesOrderModel.InHouseDate;
            //}
            //else
            //{
            //    objOracleCommand.Parameters.Add("P_InHouseDate", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            //}

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAccessoriesOrderModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAccessoriesOrderModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objAccessoriesOrderModel.BranchOfficeId;




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





    }
}
