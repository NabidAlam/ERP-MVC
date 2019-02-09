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
    public class PurchaseOrderDAL
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
        //edit data
        public DataTable GetPurchaseOrderRecord(PurchaseOrderModel objPurchaseOrderModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql =  "SELECT " +
                   "ROWNUM SL, " +
                   "TO_CHAR(NVL(TRAN_ID, '0')) TRAN_ID, " +
                   "TO_CHAR(NVL(INVOICE_NO, '0'))INVOICE_NO, " +
                   "TO_CHAR (ORDER_CREATION_DATE, 'dd/MM/yyyy') ORDER_CREATION_DATE, " +
                   "TO_CHAR(NVL(ORDER_NO,'N/A'))ORDER_NO, " +
                   "TO_CHAR (HAND_OVER_DATE, 'dd/MM/yyyy') HAND_OVER_DATE," +
                   "TO_CHAR(NVL(ORDER_TYPE_ID,'0'))ORDER_TYPE_ID, " +
                   "TO_CHAR(NVL(ORDER_TYPE_NAME,'N/A'))ORDER_TYPE_NAME, " +
                   "TO_CHAR(NVL(MODEL_NO,'N/A'))MODEL_NO, " +
                   "TO_CHAR(NVL(ITEM_DESCRIPTION,'N/A'))ITEM_DESCRIPTION, " +
                   "TO_CHAR(NVL(ITEM_CODE,'N/A'))ITEM_CODE, " +
                   "TO_CHAR(NVL(SIZE_ID,'0'))SIZE_ID, " +
                   "TO_CHAR(NVL(SIZE_NAME,'N/A'))SIZE_NAME, " +
                   "TO_CHAR(NVL(PCB_VALUE,'0'))PCB_VALUE, " +
                   "TO_CHAR(NVL(UE_VALUE,'0'))UE_VALUE, " +
                   "TO_CHAR(NVL(PACKAGING_VALUE,'N/A'))PACKAGING_VALUE, " +
                   "TO_CHAR(NVL(STYLE_NO,'0'))STYLE_NO, " +
                   "TO_CHAR(NVL(ORDER_QUANTITY,'0'))ORDER_QUANTITY, " +
                   "TO_CHAR(NVL(SHIP_QUANTITY,'0'))SHIP_QUANTITY, " +
                   "TO_CHAR(NVL(REMAIN_QUANTITY,'0'))REMAIN_QUANTITY, " +
                   "TO_CHAR(NVL(UNIT_PRICE,'0'))UNIT_PRICE, " +
                   "TO_CHAR(NVL(TOTAL_PRICE,'0'))TOTAL_PRICE, " +
                   "TO_CHAR(NVL(PORT_OF_DISTINATION,'0'))PORT_OF_DISTINATION, " +
                   "TO_CHAR (DELIVERY_DATE, 'dd/MM/yyyy') DELIVERY_DATE," +
                   "TO_CHAR(NVL(MODE_OF_SHIPMENT_ID,'0'))MODE_OF_SHIPMENT_ID, " +
                   "TO_CHAR(NVL(MODE_OF_SHIPMENT_NAME,'N/A'))MODE_OF_SHIPMENT_NAME, " +
                   "TO_CHAR(NVL(PORT_OF_LOADING_ID,'0'))PORT_OF_LOADING_ID, " +
                   "TO_CHAR(NVL(PORT_OF_LOADING_NAME,'0'))PORT_OF_LOADING_NAME, " +
                   "TO_CHAR(NVL(CURRENCY_ID,'0'))CURRENCY_ID, " +
                   "TO_CHAR(NVL(CURRENCY_NAME,'N/A'))CURRENCY_NAME, " +
                   "TO_CHAR(NVL(STATUS,'N/A'))STATUS, " +
                   "TO_CHAR(NVL(REMARKS,'N/A'))REMARKS, " +
                   "TO_CHAR(NVL(COPY_YN,'N'))COPY_YN, " +
                   "TO_CHAR(NVL(DELETE_YN,'N'))DELETE_YN, " +
                   "TO_CHAR(NVL(REJECT_YN,'N'))REJECT_YN, " +
                   "TO_CHAR(NVL(SEASON_YEAR,'0'))SEASON_YEAR, " +
                   "TO_CHAR(NVL(SEASON,'N/A'))SEASON " +
                    "FROM VEW_PURCHASE_ORDER_MERCHAN_SUB WHERE HEAD_OFFICE_ID = '" + objPurchaseOrderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objPurchaseOrderModel.BranchOfficeId + "' ";

            //if (!string.IsNullOrEmpty(objPurchaseOrderModel.SearchBy))
            //{

            //    sql = sql + "and (lower(STYLE_NO) like lower( '%" + objPurchaseOrderModel.SearchBy + "%')  or upper(STYLE_NO)like upper('%" + objPurchaseOrderModel.SearchBy + "%') ) or (lower(SEASON_YEAR) like lower( '%" + objPurchaseOrderModel.SearchBy + "%')  or upper(SEASON_YEAR)like upper('%" + objPurchaseOrderModel.SearchBy + "%') or (lower(BUYER_NAME) like lower( '%" + objPurchaseOrderModel.SearchBy + "%')  or upper(BUYER_NAME)like upper('%" + objPurchaseOrderModel.SearchBy + "%') ) )";
            //}

            if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
            {
                sql = sql + "and INVOICE_NO = '" + objPurchaseOrderModel.InvoiceNumber + "'";
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
        //get data from main
        public DataTable GetPurchaseOrderRecordMain(PurchaseOrderModel objPurchaseOrderModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "ROWNUM SL, " +
                 
                   "TO_CHAR(NVL(INVOICE_NO, '0'))INVOICE_NO, " +
                   "TO_CHAR (CREATE_DATE, 'dd/MM/yyyy') CREATE_DATE, " +
                   "TO_CHAR(NVL(FILE_NAME,'N/A'))FILE_NAME " +
                    "FROM VEW_PO_MERCHAN_MAIN WHERE HEAD_OFFICE_ID = '" + objPurchaseOrderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objPurchaseOrderModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
            {
                sql = sql + "and INVOICE_NO = '" + objPurchaseOrderModel.InvoiceNumber + "'";
            }
            if (!string.IsNullOrEmpty(objPurchaseOrderModel.FileName))
            {
                sql = sql + "and FILE_NAME = '" + objPurchaseOrderModel.FileName + "'";
            }
            //if (!string.IsNullOrEmpty(objPurchaseOrderModel.UploadDate))
            //{
            //    sql = sql + "and CREATE_DATE = TO_DATE( '" + objPurchaseOrderModel.UploadDate + "', 'dd/MM/yyyy' ) ";
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
        public string SavePOEntry(PurchaseOrderModel objPurchaseOrderModel)
        {
            string strMsg = "";

            int x = objPurchaseOrderModel.OrderNumber.Count();
            for (int i = 0; i < x; i++)
            {

                var arrayTranId = objPurchaseOrderModel.TranId[i];
                var arrayOrderCreationDate= objPurchaseOrderModel.OrderCreationDate[i]; 
                var arrayOrderNumber= objPurchaseOrderModel.OrderNumber[i]; 
                var arraySupplierHandoverDate = objPurchaseOrderModel.SupplierHandoverDate[i]; 
                var arrayOrdertypeId = objPurchaseOrderModel.OrdertypeId[i]; 
                var arrayModel = objPurchaseOrderModel.Model[i]; 
                var arrayDescription = objPurchaseOrderModel.Description[i]; 
                var arrayItem = objPurchaseOrderModel.Item[i]; 
                var arraySizeId = objPurchaseOrderModel.SizeId[i]; 
               // var arraySizeName = objPurchaseOrderModel.TranId[i]; 
                var arrayPCB = objPurchaseOrderModel.PCB[i]; 
                var arrayUE = objPurchaseOrderModel.UE[i]; 
                var arrayPackaging = objPurchaseOrderModel.Packaging[i]; 
                var arrayStyleNo = objPurchaseOrderModel.StyleNo[i]; 
                var arrayOrderedQty = objPurchaseOrderModel.OrderedQty[i]; 
                var arrayShippedQty = objPurchaseOrderModel.ShippedQty[i]; 
                var arrayRemainningQty = objPurchaseOrderModel.RemainningQty[i]; 
                var arrayUnitPrice = objPurchaseOrderModel.UnitPrice[i]; 
                var arrayTotalPrice = objPurchaseOrderModel.TotalPrice[i]; 
                var arrayPortOfDestination = objPurchaseOrderModel.PortOfDestination[i]; 
                var arrayDeliveryDate = objPurchaseOrderModel.DeliveryDate[i]; 
                var arrayShipmentTypeId = objPurchaseOrderModel.ShipmentTypeId[i]; 
                var arrayPortOfLandingId = objPurchaseOrderModel.PortOfLandingId[i]; 
                var arrayCurrencyId = objPurchaseOrderModel.CurrencyId[i];
                var arrayStatus = objPurchaseOrderModel.Status[i];
                var arrayRemarks = objPurchaseOrderModel.Remarks[i];
                var arrayCopyYn = objPurchaseOrderModel.CopyYn[i];
                var arraySeasonYear = objPurchaseOrderModel.SeasonYear[i];
                var arraySeason = objPurchaseOrderModel.Season[i];
                var arrayDeleteYn = objPurchaseOrderModel.DeleteYn[i];
                //var arrayCurrencyName = objPurchaseOrderModel.TranId[i]; 

                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("PRO_PO_ORDER_SAVE");
                objOracleCommand.CommandType = CommandType.StoredProcedure;


                if (!string.IsNullOrEmpty(arrayTranId))
                {
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayTranId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
                {
                    objOracleCommand.Parameters.Add("P_INVOICE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.InvoiceNumber.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_INVOICE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.FileName))
                {
                    objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.FileName.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arrayOrderCreationDate))
                {
                    objOracleCommand.Parameters.Add("P_ORDER_CREATION_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayOrderCreationDate.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ORDER_CREATION_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arrayOrderNumber))
                {
                    objOracleCommand.Parameters.Add("P_ORDER_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayOrderNumber.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ORDER_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arraySupplierHandoverDate))
                {
                    objOracleCommand.Parameters.Add("P_HAND_OVER_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arraySupplierHandoverDate.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_HAND_OVER_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arrayOrdertypeId))
                {
                    objOracleCommand.Parameters.Add("P_ORDER_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayOrdertypeId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ORDER_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayModel))
                {
                    objOracleCommand.Parameters.Add("P_MODEL_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayModel.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_MODEL_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayDescription))
                {
                    objOracleCommand.Parameters.Add("P_ITEM_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayDescription.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ITEM_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arrayItem))
                {
                    objOracleCommand.Parameters.Add("P_ITEM_CODE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayItem.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ITEM_CODE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arraySizeId))
                {
                    objOracleCommand.Parameters.Add("P_SIZE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arraySizeId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_SIZE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayPCB))
                {
                    objOracleCommand.Parameters.Add("P_PCB_VALUE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayPCB.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_PCB_VALUE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayUE))
                {
                    objOracleCommand.Parameters.Add("P_UE_VALUE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayUE.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_UE_VALUE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayPackaging))
                {
                    objOracleCommand.Parameters.Add("P_PACKAGING_VALUE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayPackaging.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_PACKAGING_VALUE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayStyleNo))
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayStyleNo.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayOrderedQty))
                {
                    objOracleCommand.Parameters.Add("P_ORDER_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayOrderedQty.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ORDER_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arrayShippedQty))
                {
                    objOracleCommand.Parameters.Add("P_SHIP_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayShippedQty.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_SHIP_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arrayRemainningQty))
                {
                    objOracleCommand.Parameters.Add("P_REMAIN_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayRemainningQty.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_REMAIN_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayUnitPrice))
                {
                    objOracleCommand.Parameters.Add("P_UNIT_PRICE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayUnitPrice.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_UNIT_PRICE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayTotalPrice))
                {
                    objOracleCommand.Parameters.Add("P_TOTAL_PRICE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayTotalPrice.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_TOTAL_PRICE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayPortOfDestination))
                {
                    objOracleCommand.Parameters.Add("P_PORT_OF_DISTINATION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayPortOfDestination.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_PORT_OF_DISTINATION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayDeliveryDate))
                {
                    objOracleCommand.Parameters.Add("P_DELIVERY_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayDeliveryDate.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_DELIVERY_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arrayShipmentTypeId))
                {
                    objOracleCommand.Parameters.Add("P_MODE_OF_SHIPMENT_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayShipmentTypeId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_MODE_OF_SHIPMENT_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayPortOfLandingId))
                {
                    objOracleCommand.Parameters.Add("P_PORT_OF_LOADING_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayPortOfLandingId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_PORT_OF_LOADING_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayCurrencyId))
                {
                    objOracleCommand.Parameters.Add("P_CURRENCY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayCurrencyId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_CURRENCY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (!string.IsNullOrEmpty(arrayStatus))
                {
                    objOracleCommand.Parameters.Add("P_STATUS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayStatus.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_STATUS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arrayRemarks))
                {
                    objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayRemarks.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(arrayCopyYn))
                {
                    objOracleCommand.Parameters.Add("P_COPY_YN", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayCopyYn.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_COPY_YN", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arraySeasonYear))
                {
                    objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arraySeasonYear.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arraySeason))
                {
                    objOracleCommand.Parameters.Add("P_SEASON", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arraySeason.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_SEASON", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(arrayDeleteYn))
                {
                    objOracleCommand.Parameters.Add("P_DELETE_YN", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayDeleteYn.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_DELETE_YN", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                


                objOracleCommand.Parameters.Add("P_CREATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.BranchOfficeId;



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

                        if (strMsg != "UPDATED SUCCESSFULLY" && strMsg != "INSERTED SUCCESSFULLY" && !string.IsNullOrEmpty(strMsg))
                        {
                            string input = strMsg;
                            string subStr = input.Substring(21);
                            objPurchaseOrderModel.InvoiceNumber = subStr;

                        }
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
        public PurchaseOrderModel LoadPODeleteHistory(PurchaseOrderModel objPurchaseOrderModel)
        {
            string sql = "";

            sql = "SELECT " +
                    "TO_CHAR(NVL(TRAN_ID, '0')) TRAN_ID, " +
                    "TO_CHAR(NVL(INVOICE_NO, '0'))INVOICE_NO, " +
                    "NVL(TO_CHAR (ORDER_CREATION_DATE, 'dd/MM/yyyy'), ' ') ORDER_CREATION_DATE, " +
                    "TO_CHAR(NVL(ORDER_NO,'N/A'))ORDER_NO, " +
                    "NVL(TO_CHAR (HAND_OVER_DATE, 'dd/MM/yyyy'), ' ') HAND_OVER_DATE," +
                    "TO_CHAR(NVL(ORDER_TYPE_ID,'0'))ORDER_TYPE_ID, " +
                    "TO_CHAR(NVL(MODEL_NO,'N/A'))MODEL_NO, " +
                    "TO_CHAR(NVL(ITEM_DESCRIPTION,'N/A'))ITEM_DESCRIPTION, " +
                    "TO_CHAR(NVL(ITEM_CODE,'N/A'))ITEM_CODE, " +
                    "TO_CHAR(NVL(SIZE_ID,'0'))SIZE_ID, " +
                    "TO_CHAR(NVL(PCB_VALUE,'0'))PCB_VALUE, " +
                    "TO_CHAR(NVL(UE_VALUE,'0'))UE_VALUE, " +
                    "TO_CHAR(NVL(PACKAGING_VALUE,'N/A'))PACKAGING_VALUE, " +
                    "TO_CHAR(NVL(STYLE_NO,'0'))STYLE_NO, " +
                    "TO_CHAR(NVL(ORDER_QUANTITY,'0'))ORDER_QUANTITY, " +
                    "TO_CHAR(NVL(SHIP_QUANTITY,'0'))SHIP_QUANTITY, " +
                    "TO_CHAR(NVL(REMAIN_QUANTITY,'0'))REMAIN_QUANTITY, " +
                    "TO_CHAR(NVL(UNIT_PRICE,'0'))UNIT_PRICE, " +
                    "TO_CHAR(NVL(TOTAL_PRICE,'0'))TOTAL_PRICE, " +
                    "TO_CHAR(NVL(PORT_OF_DISTINATION,'0'))PORT_OF_DISTINATION, " +
                    "NVL(TO_CHAR (DELIVERY_DATE, 'dd/MM/yyyy'), ' ') DELIVERY_DATE," +
                    "TO_CHAR(NVL(MODE_OF_SHIPMENT_ID,'0'))MODE_OF_SHIPMENT_ID, " +
                    "TO_CHAR(NVL(PORT_OF_LOADING_ID,'0'))PORT_OF_LOADING_ID, " +
                    "TO_CHAR(NVL(CURRENCY_ID,'0'))CURRENCY_ID, " +
                    "TO_CHAR(NVL(STATUS,'N/A'))STATUS, " +
                    "TO_CHAR(NVL(REMARKS,'N/A'))REMARKS, " +
                    "TO_CHAR(NVL(COPY_YN,'N'))COPY_YN, " +
                    "TO_CHAR(NVL(DELETE_YN,'N'))DELETE_YN " +
                     "FROM VEW_PURCHASE_ORDER_MERCHAN_SUB WHERE HEAD_OFFICE_ID = '" + objPurchaseOrderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objPurchaseOrderModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridTranId))
            {
                sql = sql + "and TRAN_ID = '" + objPurchaseOrderModel.GridTranId + "'";
            }
            if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
            {
                sql = sql + "and INVOICE_NO = '" + objPurchaseOrderModel.InvoiceNumber + "'";
            }

            sql = sql + " ORDER BY TRAN_ID";
            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        objPurchaseOrderModel.GridTranId = objReader.GetString(0);
                        objPurchaseOrderModel.InvoiceNumber = objReader.GetString(1);
                        objPurchaseOrderModel.GridOrderCreationDate = objReader.GetString(2);
                        objPurchaseOrderModel.GridOrderNumber = objReader.GetString(3);
                        objPurchaseOrderModel.GridSupplierHandoverDate = objReader.GetString(4);
                        objPurchaseOrderModel.GridOrdertypeId = objReader.GetString(5);
                        objPurchaseOrderModel.GridModel = objReader.GetString(6);
                        objPurchaseOrderModel.GridDescription = objReader.GetString(7);
                        objPurchaseOrderModel.GridItem = objReader.GetString(8);
                        objPurchaseOrderModel.GridSizeId = objReader.GetString(9);
                        objPurchaseOrderModel.GridPCB = objReader.GetString(10);
                        objPurchaseOrderModel.GridUE = objReader.GetString(11);
                        objPurchaseOrderModel.GridPackaging = objReader.GetString(12);
                        objPurchaseOrderModel.GridStyleNo = objReader.GetString(13);
                        objPurchaseOrderModel.GridOrderedQty = objReader.GetString(14);
                        objPurchaseOrderModel.GridShippedQty = objReader.GetString(15);
                        objPurchaseOrderModel.GridRemainningQty = objReader.GetString(16);
                        objPurchaseOrderModel.GridUnitPrice = objReader.GetString(17);
                        objPurchaseOrderModel.GridTotalPrice = objReader.GetString(18);
                        objPurchaseOrderModel.GridPortOfDestination = objReader.GetString(19);
                        objPurchaseOrderModel.GridDeliveryDate = objReader.GetString(20);
                        objPurchaseOrderModel.GridShipmentTypeId = objReader.GetString(21);
                        objPurchaseOrderModel.GridPortOfLandingId = objReader.GetString(22);
                        objPurchaseOrderModel.GridCurrencyId = objReader.GetString(23);
                        objPurchaseOrderModel.GridStatus = objReader.GetString(24);
                        objPurchaseOrderModel.GridRemarks = objReader.GetString(25);
                        objPurchaseOrderModel.GridCopyYn = objReader.GetString(26);
                        objPurchaseOrderModel.GridDeleteYn = objReader.GetString(27);
                        
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

            return objPurchaseOrderModel;
        }
        public string SavePODeleteHistory(PurchaseOrderModel objPurchaseOrderModel)
        {
            string strMsg = "";
           
                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("PRO_PO_ORDER_SAVE_HISTORY");
                objOracleCommand.CommandType = CommandType.StoredProcedure;


                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridTranId))
                {
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridTranId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
                {
                    objOracleCommand.Parameters.Add("P_INVOICE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.InvoiceNumber.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_INVOICE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.FileName))
                {
                    objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.FileName.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridOrderCreationDate))
                {
                    objOracleCommand.Parameters.Add("P_ORDER_CREATION_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridOrderCreationDate.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ORDER_CREATION_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridOrderNumber))
                {
                    objOracleCommand.Parameters.Add("P_ORDER_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridOrderNumber.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ORDER_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridSupplierHandoverDate))
                {
                    objOracleCommand.Parameters.Add("P_HAND_OVER_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridSupplierHandoverDate.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_HAND_OVER_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridOrdertypeId))
                {
                    objOracleCommand.Parameters.Add("P_ORDER_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridOrdertypeId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ORDER_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridModel))
                {
                    objOracleCommand.Parameters.Add("P_MODEL_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridModel.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_MODEL_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridDescription))
                {
                    objOracleCommand.Parameters.Add("P_ITEM_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridDescription.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ITEM_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridItem))
                {
                    objOracleCommand.Parameters.Add("P_ITEM_CODE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridItem.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ITEM_CODE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridSizeId))
                {
                    objOracleCommand.Parameters.Add("P_SIZE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridSizeId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_SIZE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridPCB))
                {
                    objOracleCommand.Parameters.Add("P_PCB_VALUE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridPCB.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_PCB_VALUE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridUE))
                {
                    objOracleCommand.Parameters.Add("P_UE_VALUE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridUE.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_UE_VALUE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridPackaging))
                {
                    objOracleCommand.Parameters.Add("P_PACKAGING_VALUE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridPackaging.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_PACKAGING_VALUE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridStyleNo))
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridStyleNo.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridOrderedQty))
                {
                    objOracleCommand.Parameters.Add("P_ORDER_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridOrderedQty.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_ORDER_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridShippedQty))
                {
                    objOracleCommand.Parameters.Add("P_SHIP_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridShippedQty.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_SHIP_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridRemainningQty))
                {
                    objOracleCommand.Parameters.Add("P_REMAIN_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridRemainningQty.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_REMAIN_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridUnitPrice))
                {
                    objOracleCommand.Parameters.Add("P_UNIT_PRICE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridUnitPrice.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_UNIT_PRICE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridTotalPrice))
                {
                    objOracleCommand.Parameters.Add("P_TOTAL_PRICE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridTotalPrice.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_TOTAL_PRICE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridPortOfDestination))
                {
                    objOracleCommand.Parameters.Add("P_PORT_OF_DISTINATION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridPortOfDestination.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_PORT_OF_DISTINATION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridDeliveryDate))
                {
                    objOracleCommand.Parameters.Add("P_DELIVERY_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridDeliveryDate.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_DELIVERY_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridShipmentTypeId))
                {
                    objOracleCommand.Parameters.Add("P_MODE_OF_SHIPMENT_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridShipmentTypeId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_MODE_OF_SHIPMENT_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridPortOfLandingId))
                {
                    objOracleCommand.Parameters.Add("P_PORT_OF_LOADING_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridPortOfLandingId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_PORT_OF_LOADING_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridCurrencyId))
                {
                    objOracleCommand.Parameters.Add("P_CURRENCY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridCurrencyId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_CURRENCY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }


                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridStatus))
                {
                    objOracleCommand.Parameters.Add("P_STATUS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridStatus.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_STATUS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridRemarks))
                {
                    objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridRemarks.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_REMARKS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridCopyYn))
                {
                    objOracleCommand.Parameters.Add("P_COPY_YN", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridCopyYn.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_COPY_YN", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                
                objOracleCommand.Parameters.Add("P_CREATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.BranchOfficeId;

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

                        if (strMsg != "UPDATED SUCCESSFULLY" && strMsg != "INSERTED SUCCESSFULLY" && !string.IsNullOrEmpty(strMsg))
                        {
                            string input = strMsg;
                            string subStr = input.Substring(21);
                            objPurchaseOrderModel.InvoiceNumber = subStr;

                        }
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
        public string DeletePOEntry(PurchaseOrderModel objPurchaseOrderModel)
        {

                string strMsg = "";
                string[] TranIdArray = objPurchaseOrderModel.GridTranId.Split(',');
                int x = TranIdArray.Count();
                for (int i = 0; i < x; i++)
                {
                var arrayTranId = TranIdArray[i];
                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("PO_MERCHAN_DELETE_SUB");
                objOracleCommand.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(arrayTranId))
                {
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = arrayTranId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
                {
                    objOracleCommand.Parameters.Add("P_INVOICE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.InvoiceNumber.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_INVOICE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.BranchOfficeId;

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
        public string POFileProcess(PurchaseOrderModel objPurchaseOrderModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_po_temp_data");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
            {
                objOracleCommand.Parameters.Add("p_invoice_no", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.InvoiceNumber.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_invoice_no", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (!string.IsNullOrEmpty(objPurchaseOrderModel.FileName))
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.FileName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objPurchaseOrderModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objPurchaseOrderModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objPurchaseOrderModel.BranchOfficeId;

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
        //get file name
        public PurchaseOrderModel GetFileName(PurchaseOrderModel objPurchaseOrderModel)
        {
            string sql = "";
            sql = "SELECT " +
                   "TO_CHAR(NVL(FILE_NAME,'N/A'))FILE_NAME " +
                    "FROM VEW_PO_MERCHAN_MAIN WHERE HEAD_OFFICE_ID = '" + objPurchaseOrderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objPurchaseOrderModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
            {
                sql = sql + "and INVOICE_NO = '" + objPurchaseOrderModel.InvoiceNumber + "'";
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
                        objPurchaseOrderModel.FileName = objReader.GetString(0);
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

            return objPurchaseOrderModel;
        }
        //Purchase Order Delete Approved
        public DataTable GetInvoiceNoForApproved(PurchaseOrderModel objPurchaseOrderModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "ROWNUM SL, " +

                   "TO_CHAR(NVL(INVOICE_NO, '0'))INVOICE_NO, " +
                   "TO_CHAR(NVL(STYLE_NO, '0'))STYLE_NO, " +
                   "TO_CHAR(NVL(ORDER_NO, '0'))ORDER_NO, " +
                   "TO_CHAR(NVL(MODEL_NO, '0'))MODEL_NO " +
                   

                    "FROM VEW_PO_MERCHAN_SUB_HISTORY WHERE HEAD_OFFICE_ID = '" + objPurchaseOrderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objPurchaseOrderModel.BranchOfficeId + "' AND APPROVED_STATUS = 'PENDING'  ";

            //if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
            //{
            //    sql = sql + "and INVOICE_NO = '" + objPurchaseOrderModel.InvoiceNumber + "'";
            //}
            //if (!string.IsNullOrEmpty(objPurchaseOrderModel.FileName))
            //{
            //    sql = sql + "and FILE_NAME = '" + objPurchaseOrderModel.FileName + "'";
            //}
            //if (!string.IsNullOrEmpty(objPurchaseOrderModel.UploadDate))
            //{
            //    sql = sql + "and CREATE_DATE = TO_DATE( '" + objPurchaseOrderModel.UploadDate + "', 'dd/MM/yyyy' ) ";
            //}

            sql = sql + " ORDER BY INVOICE_NO";
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
        public DataTable PurchaseOrderDeleteApproved(PurchaseOrderModel objPurchaseOrderModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "ROWNUM SL, " +
                   "TO_CHAR(NVL(TRAN_ID, '0')) TRAN_ID, " +
                   "TO_CHAR(NVL(INVOICE_NO, '0'))INVOICE_NO, " +
                   "TO_CHAR (ORDER_CREATION_DATE, 'dd/MM/yyyy') ORDER_CREATION_DATE, " +
                   "TO_CHAR(NVL(ORDER_NO,'N/A'))ORDER_NO, " +
                   "TO_CHAR (HAND_OVER_DATE, 'dd/MM/yyyy') HAND_OVER_DATE," +
                   "TO_CHAR(NVL(ORDER_TYPE_ID,'0'))ORDER_TYPE_ID, " +
                   "TO_CHAR(NVL(ORDER_TYPE_NAME,'N/A'))ORDER_TYPE_NAME, " +
                   "TO_CHAR(NVL(MODEL_NO,'N/A'))MODEL_NO, " +
                   "TO_CHAR(NVL(ITEM_DESCRIPTION,'N/A'))ITEM_DESCRIPTION, " +
                   "TO_CHAR(NVL(ITEM_CODE,'N/A'))ITEM_CODE, " +
                   "TO_CHAR(NVL(SIZE_ID,'0'))SIZE_ID, " +
                   "TO_CHAR(NVL(SIZE_NAME,'N/A'))SIZE_NAME, " +
                   "TO_CHAR(NVL(PCB_VALUE,'0'))PCB_VALUE, " +
                   "TO_CHAR(NVL(UE_VALUE,'0'))UE_VALUE, " +
                   "TO_CHAR(NVL(PACKAGING_VALUE,'N/A'))PACKAGING_VALUE, " +
                   "TO_CHAR(NVL(STYLE_NO,'0'))STYLE_NO, " +
                   "TO_CHAR(NVL(ORDER_QUANTITY,'0'))ORDER_QUANTITY, " +
                   "TO_CHAR(NVL(SHIP_QUANTITY,'0'))SHIP_QUANTITY, " +
                   "TO_CHAR(NVL(REMAIN_QUANTITY,'0'))REMAIN_QUANTITY, " +
                   "TO_CHAR(NVL(UNIT_PRICE,'0'))UNIT_PRICE, " +
                   "TO_CHAR(NVL(TOTAL_PRICE,'0'))TOTAL_PRICE, " +
                   "TO_CHAR(NVL(PORT_OF_DISTINATION,'0'))PORT_OF_DISTINATION, " +
                   "TO_CHAR (DELIVERY_DATE, 'dd/MM/yyyy') DELIVERY_DATE," +
                   "TO_CHAR(NVL(MODE_OF_SHIPMENT_ID,'0'))MODE_OF_SHIPMENT_ID, " +
                   "TO_CHAR(NVL(MODE_OF_SHIPMENT_NAME,'N/A'))MODE_OF_SHIPMENT_NAME, " +
                   "TO_CHAR(NVL(PORT_OF_LOADING_ID,'0'))PORT_OF_LOADING_ID, " +
                   "TO_CHAR(NVL(PORT_OF_LOADING_NAME,'0'))PORT_OF_LOADING_NAME, " +
                   "TO_CHAR(NVL(CURRENCY_ID,'0'))CURRENCY_ID, " +
                   "TO_CHAR(NVL(CURRENCY_NAME,'N/A'))CURRENCY_NAME, " +
                   "TO_CHAR(NVL(STATUS,'N/A'))STATUS, " +
                   "TO_CHAR(NVL(REMARKS,'N/A'))REMARKS, " +
                   "TO_CHAR(NVL(COPY_YN,'N'))COPY_YN, " +
                   "TO_CHAR(NVL(DELETE_YN,'N'))DELETE_YN, " +
                   "TO_CHAR(NVL(APPROVED_STATUS,'N/A'))APPROVED_STATUS " +
                   "FROM VEW_PO_MERCHAN_SUB_HISTORY WHERE HEAD_OFFICE_ID = '" + objPurchaseOrderModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objPurchaseOrderModel.BranchOfficeId + "' AND DELETE_YN='N' ";

            if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
            {
                sql = sql + "and INVOICE_NO='"+ objPurchaseOrderModel.InvoiceNumber+ "'";
            }
            if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridStyleNo))
            {
                sql = sql + "and STYLE_NO='" + objPurchaseOrderModel.GridStyleNo + "' ";
            }
            if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridOrderNumber))
            {
                sql = sql + "and ORDER_NO='" + objPurchaseOrderModel.GridOrderNumber + "' ";
            }
            if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridModel))
            {
                sql = sql + "and MODEL_NO='" + objPurchaseOrderModel.GridModel + "' ";
            }
            sql = sql + " ORDER BY TRAN_ID";

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
        public string PurchaseOrderApprovedDelete(PurchaseOrderModel objPurchaseOrderModel)
        {
                string strMsg = "";
                OracleTransaction objOracleTransaction = null;
                OracleCommand objOracleCommand = new OracleCommand("PO_MERCHAN_APPROVED_DELETE");
                objOracleCommand.CommandType = CommandType.StoredProcedure;
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridTranId))
                {
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridTranId.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
                {
                    objOracleCommand.Parameters.Add("P_INVOICE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.InvoiceNumber.Trim();
                }
                else
                {
                    objOracleCommand.Parameters.Add("P_INVOICE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.BranchOfficeId;

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
        public string PurchaseOrderApprovedReject(PurchaseOrderModel objPurchaseOrderModel)
        {
            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PO_MERCHAN_APPROVED_REJECT");
            objOracleCommand.CommandType = CommandType.StoredProcedure;
            if (!string.IsNullOrEmpty(objPurchaseOrderModel.GridTranId))
            {
                objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.GridTranId.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
            {
                objOracleCommand.Parameters.Add("P_INVOICE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.InvoiceNumber.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_INVOICE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.BranchOfficeId;

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
        //For summary report
        public string SaveSumDataForSummaryReport(PurchaseOrderModel objPurchaseOrderModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_rpt_po_merchan_sum");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (!string.IsNullOrEmpty(objPurchaseOrderModel.rptStyleNo))
            {
                objOracleCommand.Parameters.Add("p_style_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.rptStyleNo.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_style_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (!string.IsNullOrEmpty(objPurchaseOrderModel.rptOrderNumber))
            {
                objOracleCommand.Parameters.Add("p_order_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.rptOrderNumber.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_order_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (!string.IsNullOrEmpty(objPurchaseOrderModel.rptModelNo))
            {
                objOracleCommand.Parameters.Add("p_model_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPurchaseOrderModel.rptModelNo.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_model_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPurchaseOrderModel.BranchOfficeId;

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

    }
}
