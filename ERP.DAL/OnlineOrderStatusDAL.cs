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
    public class OnlineOrderStatusDAL
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



        public OnlineOrderStatusModel GetTrimsData(string pOrderReceiveDate, string pOrderNo, string headOfficeId, string branchOfficeId)
        {
            OnlineOrderStatusModel objTrimsMain = new OnlineOrderStatusModel();

            string sql = "";

            sql = "SELECT " +
                  "NVL (TO_CHAR (ORDER_DELIVER_DATE, 'dd/mm/yyyy'), ' ')ORDER_DELIVER_DATE, " +
                  "TO_CHAR (NVL (ORDER_SOURCE_ID, '0'))ORDER_SOURCE_ID, " +
                  "TO_CHAR (NVL (CUSTOMER_NAME, 'N/A'))CUSTOMER_NAME, " +
                  "TO_CHAR (NVL (CUSTOMER_HOME_ADDRESS, 'N/A'))CUSTOMER_HOME_ADDRESS, " +
                   "TO_CHAR (NVL (CUSTOMER_OFFICE_ADDRESS, 'N/A'))CUSTOMER_OFFICE_ADDRESS, " +
                  "TO_CHAR (NVL (TELEPHO_NO, '0'))TELEPHO_NO, " +
                  "TO_CHAR (NVL (CELL_NO, '0'))CELL_NO, " +
                  "TO_CHAR (NVL (WEB_ADDRESS, 'N/A'))WEB_ADDRESS, " +

                  "TO_CHAR (NVL (PROMOTION_CODE,'0'))PROMOTION_CODE, " +

                  "TO_CHAR (NVL (DISCOUNT_AMOUNT,'0'))DISCOUNT_AMOUNT, " +
                  "TO_CHAR (NVL (TOTAL_AMOUNT,'0'))TOTAL_AMOUNT, " +
                  "TO_CHAR (NVL (DELIVERED_YN,'0'))DELIVERED_YN, " +
                  "TO_CHAR (NVL (ORDER_NO, '0'))ORDER_NO, " +
                  "TO_CHAR (NVL (DELIVERY_COST, '0'))DELIVERY_COST, " +
                  "NVL (TO_CHAR (ORDER_RECEIVE_DATE, 'dd/mm/yyyy'), ' ')ORDER_RECEIVE_DATE, " +
                  "TO_CHAR (NVL (REMARKS, 'N/A'))REMARKS, " +
                  "TO_CHAR (NVL (EMAIL_ADDRESS, 'N/A'))EMAIL_ADDRESS, " +
                  "TO_CHAR (NVL (DELIVERY_PROCESS_COST, '0'))DELIVERY_PROCESS_COST " +
                  "from VEW_ONLINE_ORDER_MAIN where order_no = '" + pOrderNo + "'  and head_office_id = '" + headOfficeId + "' AND branch_office_id = '" + branchOfficeId + "' ";




            //  " FROM VEW_ONLINE_ORDER_MAIN where SEASON_ID = '" + seasonId.Trim() + "' AND SEASON_YEAR = '" + seasonYear.Trim() + "' AND STYLE_NO = '" + styleNumber.Trim() + "' AND HEAD_OFFICE_ID = '" + headOfficeId.Trim() + "' AND BRANCH_OFFICE_ID = '" + branchOfficeId.Trim() + "' ";

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


                        objTrimsMain.OrderDeliveryDate = objDataReader["ORDER_DELIVER_DATE"].ToString();
                        //objTrimsMain.OrderSourceName = objDataReader["ORDER_SOURCE_ID"].ToString();
                        objTrimsMain.OrderSourceId = objDataReader["ORDER_SOURCE_ID"].ToString();
                        objTrimsMain.CustomerHomeAddress = objDataReader["CUSTOMER_HOME_ADDRESS"].ToString();
                        objTrimsMain.CustomerOfficeAddress = objDataReader["CUSTOMER_OFFICE_ADDRESS"].ToString();
                        objTrimsMain.Telephone = objDataReader["TELEPHO_NO"].ToString();
                        objTrimsMain.CellNo = objDataReader["CELL_NO"].ToString();
                        objTrimsMain.WebAddress = objDataReader["WEB_ADDRESS"].ToString();
                        //objTrimsMain.Pro = objDataReader["PROMOTION_CODE"].ToString();
                     
                        
                       //objTrimsMain.DiscountAmount = objDataReader["DISCOUNT_AMOUNT"].ToString();
                       //objTrimsMain.TotalAmount = objDataReader["TOTAL_AMOUNT"].ToString();
                        objTrimsMain.Delivered_YN = objDataReader["DELIVERED_YN"].ToString();
                        objTrimsMain.OrderNo = objDataReader["ORDER_NO"].ToString();
                        objTrimsMain.DeliveryCost = objDataReader["DELIVERY_COST"].ToString();
                        objTrimsMain.OrderReceiveDate = objDataReader["ORDER_RECEIVE_DATE"].ToString();
                        objTrimsMain.Remarks = objDataReader["REMARKS"].ToString();
                        objTrimsMain.EmailAddress = objDataReader["EMAIL_ADDRESS"].ToString();
                        // objTrimsMain.Pro = objDataReader["DELIVERY_PROCESS_COST"].ToString();
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



        public OnlineOrderMain GetTrimsMainData(string pOrderReceiveDate, string pOrderNo, string headOfficeId, string branchOfficeId)
        {
            OnlineOrderMain objTrimsMain = new OnlineOrderMain();

            string sql = "";

                  sql = "SELECT " +
                         "NVL (TO_CHAR (ORDER_DELIVER_DATE, 'dd/mm/yyyy'), ' ')ORDER_DELIVER_DATE, " +
                         "TO_CHAR (NVL (ORDER_SOURCE_ID, '0'))ORDER_SOURCE_ID, " +
                         "TO_CHAR (NVL (CUSTOMER_NAME, 'N/A'))CUSTOMER_NAME, " +
                         "TO_CHAR (NVL (CUSTOMER_HOME_ADDRESS, 'N/A'))CUSTOMER_HOME_ADDRESS, " +
                         "TO_CHAR (NVL (CUSTOMER_OFFICE_ADDRESS, 'N/A'))CUSTOMER_OFFICE_ADDRESS, " +
                         "TO_CHAR (NVL (TELEPHO_NO, '0'))TELEPHO_NO, " +
                         "TO_CHAR (NVL (CELL_NO, '0'))CELL_NO, " +
                         "TO_CHAR (NVL (WEB_ADDRESS, 'N/A'))WEB_ADDRESS, " +

                         "TO_CHAR (NVL (PROMOTION_CODE,'0'))PROMOTION_CODE, " +

                         "TO_CHAR (NVL (DISCOUNT_AMOUNT,'0'))DISCOUNT_AMOUNT, " +
                         "TO_CHAR (NVL (TOTAL_AMOUNT,'0'))TOTAL_AMOUNT, " +
                         "TO_CHAR (NVL (DELIVERED_YN,'0'))DELIVERED_YN, " +
                         "TO_CHAR (NVL (ORDER_NO, '0'))ORDER_NO, " +
                         "TO_CHAR (NVL (DELIVERY_COST, '0'))DELIVERY_COST, " +
                         "NVL (TO_CHAR (ORDER_RECEIVE_DATE, 'dd/mm/yyyy'), ' ')ORDER_RECEIVE_DATE, " +
                         "TO_CHAR (NVL (REMARKS, 'N/A'))REMARKS, " +
                         "TO_CHAR (NVL (EMAIL_ADDRESS, 'N/A'))EMAIL_ADDRESS, " +
                         "TO_CHAR (NVL (DELIVERY_PROCESS_COST, '0'))DELIVERY_PROCESS_COST " +
                         "from VEW_ONLINE_ORDER_MAIN where order_no = '" + pOrderNo + "'  and head_office_id = '" + headOfficeId + "' AND branch_office_id = '" + branchOfficeId + "' ";




          //  " FROM VEW_ONLINE_ORDER_MAIN where SEASON_ID = '" + seasonId.Trim() + "' AND SEASON_YEAR = '" + seasonYear.Trim() + "' AND STYLE_NO = '" + styleNumber.Trim() + "' AND HEAD_OFFICE_ID = '" + headOfficeId.Trim() + "' AND BRANCH_OFFICE_ID = '" + branchOfficeId.Trim() + "' ";

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
                       

                        objTrimsMain.OrderDeliveryDate = objDataReader["ORDER_DELIVER_DATE"].ToString();
                        objTrimsMain.OrderSourceName = objDataReader["ORDER_SOURCE_ID"].ToString();
                        objTrimsMain.OrderSourceId = objDataReader["ORDER_SOURCE_ID"].ToString();
                        objTrimsMain.CustomerName = objDataReader["CUSTOMER_NAME"].ToString();

                        objTrimsMain.CustomerHomeAddress = objDataReader["CUSTOMER_HOME_ADDRESS"].ToString();
                        objTrimsMain.CustomerOfficeAddress = objDataReader["CUSTOMER_OFFICE_ADDRESS"].ToString();
                        objTrimsMain.Telephone = objDataReader["TELEPHO_NO"].ToString();
                        objTrimsMain.CellNo = objDataReader["CELL_NO"].ToString();
                        objTrimsMain.WebAddress = objDataReader["WEB_ADDRESS"].ToString();
                        //objTrimsMain.Pro = objDataReader["PROMOTION_CODE"].ToString();
                        objTrimsMain.DiscountAmount = objDataReader["DISCOUNT_AMOUNT"].ToString();
                        objTrimsMain.TotalAmount = objDataReader["TOTAL_AMOUNT"].ToString();
                        objTrimsMain.Delivered_YN = objDataReader["DELIVERED_YN"].ToString();
                        objTrimsMain.OrderNo = objDataReader["ORDER_NO"].ToString();
                        objTrimsMain.DeliveryCost = objDataReader["DELIVERY_COST"].ToString();
                        objTrimsMain.OrderReceiveDate = objDataReader["ORDER_RECEIVE_DATE"].ToString();
                        objTrimsMain.Remarks = objDataReader["REMARKS"].ToString();
                        objTrimsMain.EmailAddress = objDataReader["EMAIL_ADDRESS"].ToString();
                        objTrimsMain.DeliveryProcessCost = objDataReader["DELIVERY_PROCESS_COST"].ToString();
                       // objTrimsMain.PaymentTypeId = objDataReader["DELIVERY_PROCESS_COST"].ToString();
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

    
        public List<OnlineOrderSub> LoadOnlineOrderRecordByOrderNo(OnlineOrderStatusModel objOnlineOrderStatusModel)
        {

            List<OnlineOrderSub> objOnlineOrderSubList = new List<OnlineOrderSub>();


            string sql = "";
            sql = "SELECT " +
                "rownum sl, " +
                "PROMOTION_PERCENTAGE, " +
                 "TO_CHAR(ORDER_RECEIVE_DATE,'dd/mm/yyyy')ORDER_RECEIVE_DATE, " +
                "TO_CHAR(ORDER_DELIVER_DATE,'dd/mm/yyyy')ORDER_DELIVER_DATE, " +
                   "ORDER_SOURCE_ID," +
                   "ORDER_SOURCE_NAME, " +
                   "CUSTOMER_NAME, " +
                   "CUSTOMER_HOME_ADDRESS, " +
                   "CUSTOMER_OFFICE_ADDRESS, " +
                   "TELEPHO_NO, " +
                   "CELL_NO, " +
                   "WEB_ADDRESS, " +
                   "PRODUCT_DESCRIPTION, " +
                   "STYLE_NAME, " +
                   "COLOR_NAME, " +
                   "SIZE_NAME, " +
                   "PROMOTION_CODE, " +
                   "PRODUCT_QUANTITY, " +
                   "PRODUCT_PRICE, " +
                   "DISCOUNT_AMOUNT, " +
                   "TOTAL_AMOUNT, " +
                   "DELIVERED_YN, " +
                   "CREATE_BY, " +
                   "CREATE_DATE, " +
                  " UPDATE_BY, " +
                   "UPDATE_DATE, " +
                   "HEAD_OFFICE_ID," +
                   "BRANCH_OFFICE_ID, " +
                   "PRODUCT_PIC, " +
                   "delivery_cost, " +
                   "ORDER_NO, " +
                   "DELIVERED_STATUS, " +
                   "REMARKS, " +
                   "EMAIL_ADDRESS, " +
                   "DELIVERY_PROCESS_COST, " +
                   "TRAN_ID " +

                  " FROM VEW_ONLINE_ORDER_SUB where head_office_id = '" + objOnlineOrderStatusModel.HeadOfficeId + "' and branch_office_id = '" + objOnlineOrderStatusModel.BranchOfficeId + "'  and ORDER_NO = '" + objOnlineOrderStatusModel.OrderNo + "'  ";

            if(!string.IsNullOrEmpty(objOnlineOrderStatusModel.OrderReceiveDate))
            {
                sql = sql + "and ORDER_RECEIVE_DATE = to_date( '" + objOnlineOrderStatusModel.OrderReceiveDate + "', 'dd/mm/yyyy') ";
            }

            if(!string.IsNullOrEmpty(objOnlineOrderStatusModel.OrderDeliveryDate))
            {
                sql = sql + "and ORDER_DELIVER_DATE = to_date( '" + objOnlineOrderStatusModel.OrderDeliveryDate + "', 'dd/mm/yyyy') ";
            }




            sql = sql + " order by SL";

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

                        OnlineOrderSub subData = new OnlineOrderSub();

                        //STYLE	PRODUCT DESCRIPTION	SIZE	COLOR	QTY	MRP
                        subData.TranId = objDataReader["TRAN_ID"].ToString();
                        subData.OrderNo = objDataReader["ORDER_NO"].ToString();
                        subData.OrderReceiveDate = objDataReader["ORDER_RECEIVE_DATE"].ToString();


                        subData.StyleName = objDataReader["STYLE_NAME"].ToString();
                        subData.ProductDescription = objDataReader["PRODUCT_DESCRIPTION"].ToString();
                        subData.SizeName = objDataReader["SIZE_NAME"].ToString();
                        subData.ColorName = objDataReader["COLOR_NAME"].ToString();
                        subData.ProductQuantity = objDataReader["PRODUCT_QUANTITY"].ToString();
                        subData.ProductPrice = objDataReader["PRODUCT_PRICE"].ToString();

                        /* <td>
                                            @Html.TextBoxFor(m => m.PromotionCode, new { @class = "form-control", @autofocus = true, @Value = data.PromotionCode })
                                        </td>
                                        <td>
                                            @Html.TextBoxFor(m => m.PromotionPercentage, new { @class = "form-control PromotionPercentage", @autofocus = true, @Value = data.PromotionPercentage, @type = "number" })
                                        </td>



                                        <td>
                                            @Html.TextBoxFor(m => m.DiscountAmount, new { @class = "form-control DiscountAmount", @autofocus = true, @Value = data.DiscountAmount, @type = "number", @readonly = "readonly" })
                                        </td>


                                        <td>
                                            @Html.TextBoxFor(m => m.TotalAmount, new { @class = "form-control TotalAmount", @autofocus = true, @Value = data.TotalAmount, @type = "number", @readonly = "readonly" })
                                        </td>*/

                        //new
                        subData.PromoCode = objDataReader["PROMOTION_CODE"].ToString();
                        subData.PromotionPercentage = objDataReader["PROMOTION_PERCENTAGE"].ToString();
                        subData.DiscountAmount = objDataReader["DISCOUNT_AMOUNT"].ToString();
                        subData.TotalAmount = objDataReader["TOTAL_AMOUNT"].ToString();



                        // subData.ProductPicture = objDataReader["PRODUCT_PIC"] == DBNull.Value ? new byte[0] : (byte[])objDataReader["PRODUCT_PIC"];

                        objOnlineOrderSubList.Add(subData);
                        
                        // objOnlineOrderSub.OrderReceiveDate = objDataReader["ORDER_RECEIVE_DATE"].ToString();
                        //// objOnlineOrderSub. = objDataReader["ORDER_DELIVER_DATE"].ToString();
                        //// objOnlineOrderSub.O = objDataReader["ORDER_SOURCE_ID"].ToString();
                        //// objOnlineOrderSub. = objDataReader["ORDER_SOURCE_NAME"].ToString();
                        // objOnlineOrderSub. = objDataReader["CUSTOMER_HOME_ADDRESS"].ToString();
                        // objOnlineOrderSub.CustomerOfficeAddress = objDataReader["CUSTOMER_OFFICE_ADDRESS"].ToString();
                        // objOnlineOrderSub.Telephone = objDataReader["TELEPHO_NO"].ToString();
                        // objOnlineOrderSub.CellNo = objDataReader["CELL_NO"].ToString();
                        // objOnlineOrderSub.WebAddress = objDataReader["WEB_ADDRESS"].ToString();
                        // //objTrimsMain.Pro = objDataReader["PROMOTION_CODE"].ToString();
                        // objOnlineOrderSub.DiscountAmount = objDataReader["DISCOUNT_AMOUNT"].ToString();
                        // objOnlineOrderSub.TotalAmount = objDataReader["TOTAL_AMOUNT"].ToString();
                        // objOnlineOrderSub.Delivered_YN = objDataReader["DELIVERED_YN"].ToString();
                        // objOnlineOrderSub.OrderNo = objDataReader["ORDER_NO"].ToString();
                        // objOnlineOrderSub.DeliveryCost = objDataReader["DELIVERY_COST"].ToString();
                        // objOnlineOrderSub.OrderReceiveDate = objDataReader["ORDER_RECEIVE_DATE"].ToString();
                        // objOnlineOrderSub.Remarks = objDataReader["REMARKS"].ToString();
                        // objOnlineOrderSub.EmailAddress = objDataReader["EMAIL_ADDRESS"].ToString();
                        // objTrimsMain.Pro = objDataReader["DELIVERY_PROCESS_COST"].ToString();
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
            return objOnlineOrderSubList;
        }




        public string SaveOnlineOrderImage(OnlineOrderStatusModel objOnlineOrderStatusModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_online_image_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("P_ORDER_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOnlineOrderStatusModel.OrderNo.Length > 0 ? objOnlineOrderStatusModel.OrderNo : null;

            objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.SwatchFileName) ? objOnlineOrderStatusModel.SwatchFileName : null;
            objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = objOnlineOrderStatusModel.SwatchFileSize;
        
            objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.SwatchFileExtension) ? objOnlineOrderStatusModel.SwatchFileExtension : null;


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objOnlineOrderStatusModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objOnlineOrderStatusModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, 100, ParameterDirection.Input).Value = objOnlineOrderStatusModel.BranchOfficeId;

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


        public string SaveOnlineOrder(OnlineOrderStatusModel objOnlineOrderStatusModel)
        {
            string strMsg = "";
            /*
             p_tran_id                   IN     VARCHAR,
   p_order_no                         VARCHAR2,
   p_order_receive_date               VARCHAR2,
   p_order_deliver_date               VARCHAR2,
   p_order_source_id                  VARCHAR2,
   p_customer_name                    VARCHAR2,
   p_customer_home_address            VARCHAR2,
   p_customer_office_address          VARCHAR2,
   p_telepho_no                       VARCHAR2,
   p_cell_no                          VARCHAR2,
   p_web_address                      VARCHAR2,
   p_product_description              VARCHAR2,
   p_style_name                       VARCHAR2,
   p_color_name                       VARCHAR2,
   p_size_name                        VARCHAR2,
   p_promotion_code                   VARCHAR2,
   p_product_quantity                 VARCHAR2,
   p_product_price                    VARCHAR2,
   p_discount_amount                  VARCHAR2,
   p_total_amount                     VARCHAR2,
   P_delivered_yn                     VARCHAR2,
   p_delivery_cost                    VARCHAR2,
   p_remarks                          VARCHAR2,
   p_EMAIL_ADDRESS                    VARCHAR2,
   P_DELIVERY_PROCESS_COST            VARCHAR2,
   P_PAYMENT_TYPE_ID                  VARCHAR2,
   P_PROMOTION_PERCENTAGE             VARCHAR2,
   p_update_by                        VARCHAR2,
   p_head_office_id                   VARCHAR2,
   p_branch_office_id                 VARCHAR2,
   p_message                      OUT VARCHAR2)*/
            OracleCommand objOracleCommand = new OracleCommand("pro_online_order_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.TranIdS) ? objOnlineOrderStatusModel.TranIdS : null;

            objOracleCommand.Parameters.Add("p_order_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.OrderNo) ? objOnlineOrderStatusModel.OrderNo : null;

            objOracleCommand.Parameters.Add("p_order_receive_date", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.OrderReceiveDate) ? objOnlineOrderStatusModel.OrderReceiveDate : null;

            objOracleCommand.Parameters.Add("p_order_deliver_date", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.OrderDeliveryDate) ? objOnlineOrderStatusModel.OrderDeliveryDate : null;

            objOracleCommand.Parameters.Add("p_order_source_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.OrderSourceId) ? objOnlineOrderStatusModel.OrderSourceId : null;

            objOracleCommand.Parameters.Add("P_CUSTOMER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.CustomerName) ? objOnlineOrderStatusModel.CustomerName : null;

            objOracleCommand.Parameters.Add("p_customer_home_address", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.CustomerHomeAddress) ? objOnlineOrderStatusModel.CustomerHomeAddress : null;

            objOracleCommand.Parameters.Add("P_CUSTOMER_OFFICE_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.CustomerOfficeAddress) ? objOnlineOrderStatusModel.CustomerOfficeAddress : null;

            objOracleCommand.Parameters.Add("P_TELEPHO_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.Telephone) ? objOnlineOrderStatusModel.Telephone : null;

            objOracleCommand.Parameters.Add("p_CELL_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.CellNo) ? objOnlineOrderStatusModel.CellNo : null;

            objOracleCommand.Parameters.Add("P_WEB_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.WebAddress) ? objOnlineOrderStatusModel.WebAddress : null;

            objOracleCommand.Parameters.Add("P_PRODUCT_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.ProductDescriptionS) ? objOnlineOrderStatusModel.ProductDescriptionS : null;

            objOracleCommand.Parameters.Add("P_STYLE_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.StyleNameS) ? objOnlineOrderStatusModel.StyleNameS : null;

            objOracleCommand.Parameters.Add("P_COLOR_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.ColorNameS) ? objOnlineOrderStatusModel.ColorNameS : null;

            objOracleCommand.Parameters.Add("P_SIZE_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.SizeNameS) ? objOnlineOrderStatusModel.SizeNameS : null;

            objOracleCommand.Parameters.Add("P_PROMOTION_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.PromotionCodeS) ? objOnlineOrderStatusModel.PromotionCodeS : null;

            objOracleCommand.Parameters.Add("P_PRODUCT_QUANTITY", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.ProductQuantityS) ? objOnlineOrderStatusModel.ProductQuantityS : null;

            objOracleCommand.Parameters.Add("P_PRODUCT_PRICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.ProductPriceS) ? objOnlineOrderStatusModel.ProductPriceS : null;

            objOracleCommand.Parameters.Add("P_DISCOUNT_AMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.DiscountAmountS) ? objOnlineOrderStatusModel.DiscountAmountS : null;

        
            //PROMOTION_PERCENTAGE
         
            objOracleCommand.Parameters.Add("P_TOTAL_AMOUNT", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.TotalAmountS) ? objOnlineOrderStatusModel.TotalAmountS : null;




            if (objOnlineOrderStatusModel.Delivered_YN == "true")
            {
                objOracleCommand.Parameters.Add("P_delivered_yn", OracleDbType.Varchar2, ParameterDirection.Input).Value = "Y";

            }

            else
            {
                objOracleCommand.Parameters.Add("P_delivered_yn", OracleDbType.Varchar2, ParameterDirection.Input).Value = "N";

            }


            objOracleCommand.Parameters.Add("p_delivery_cost", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.DeliveryCost) ? objOnlineOrderStatusModel.DeliveryCost : null;

            objOracleCommand.Parameters.Add("p_remarks", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.TranIdS) ? objOnlineOrderStatusModel.Remarks : null;

            objOracleCommand.Parameters.Add("p_EMAIL_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.EmailAddress) ? objOnlineOrderStatusModel.EmailAddress : null;

            objOracleCommand.Parameters.Add("P_DELIVERY_PROCESS_COST", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.DeliveryProcessCost) ? objOnlineOrderStatusModel.DeliveryProcessCost : null;

            objOracleCommand.Parameters.Add("P_PAYMENT_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.PaymentTypeId) ? objOnlineOrderStatusModel.PaymentTypeId : null;
            objOracleCommand.Parameters.Add("P_PROMOTION_PERCENTAGE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.PromotionPercentageS) ? objOnlineOrderStatusModel.PromotionPercentageS : null;

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOnlineOrderStatusModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOnlineOrderStatusModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOnlineOrderStatusModel.BranchOfficeId;

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

        //public List<OnlineOrderMain> GetTrimsGridDataList(string headOfficeId, string branchOfficeId)
        //{
        //    List<OnlineOrderMain> objOnlineOrderMainList = new List<OnlineOrderMain>();

        //    string sql = "";
        //    sql = "SELECT " +
        //                 "NVL (TO_CHAR (ORDER_DELIVER_DATE, 'dd/mm/yyyy'), ' ')ORDER_DELIVER_DATE, " +
        //                 "TO_CHAR (NVL (ORDER_SOURCE_ID, '0'))ORDER_SOURCE_ID, " +
        //                 "TO_CHAR (NVL (CUSTOMER_NAME, 'N/A'))CUSTOMER_NAME, " +
        //                 "TO_CHAR (NVL (CUSTOMER_HOME_ADDRESS, 'N/A'))CUSTOMER_HOME_ADDRESS, " +
        //                  "TO_CHAR (NVL (CUSTOMER_OFFICE_ADDRESS, 'N/A'))CUSTOMER_OFFICE_ADDRESS, " +
        //                 "TO_CHAR (NVL (TELEPHO_NO, '0'))TELEPHO_NO, " +
        //                 "TO_CHAR (NVL (CELL_NO, '0'))CELL_NO, " +
        //                 "TO_CHAR (NVL (WEB_ADDRESS, 'N/A'))WEB_ADDRESS, " +

        //                 "TO_CHAR (NVL (PROMOTION_CODE,'0'))PROMOTION_CODE, " +

        //                 "TO_CHAR (NVL (DISCOUNT_AMOUNT,'0'))DISCOUNT_AMOUNT, " +
        //                 "TO_CHAR (NVL (TOTAL_AMOUNT,'0'))TOTAL_AMOUNT, " +
        //                 "TO_CHAR (NVL (DELIVERED_YN,'0'))DELIVERED_YN, " +
        //                 "TO_CHAR (NVL (ORDER_NO, '0'))ORDER_NO, " +
        //                 "TO_CHAR (NVL (DELIVERY_COST, '0'))DELIVERY_COST, " +
        //                 "NVL (TO_CHAR (ORDER_RECEIVE_DATE, 'dd/mm/yyyy'), ' ')ORDER_RECEIVE_DATE, " +
        //                 "TO_CHAR (NVL (REMARKS, 'N/A'))REMARKS, " +
        //                 "TO_CHAR (NVL (EMAIL_ADDRESS, 'N/A'))EMAIL_ADDRESS, " +
        //                 "TO_CHAR (NVL (DELIVERY_PROCESS_COST, '0'))DELIVERY_PROCESS_COST " +
        //                 "from VEW_ONLINE_ORDER_MAIN where head_office_id = '" + headOfficeId + "' AND branch_office_id = '" + branchOfficeId + "' ";

        //    //"from VEW_ONLINE_ORDER_MAIN where order_no = '" + pOrderNo + "'  and head_office_id = '" + headOfficeId + "' AND branch_office_id = '" + branchOfficeId + "' ";




        //    OracleCommand objCommand = new OracleCommand(sql);
        //    OracleDataReader objDataReader;

        //    using (OracleConnection strConn = GetConnection())
        //    {

        //        objCommand.Connection = strConn;
        //        strConn.Open();
        //        objDataReader = objCommand.ExecuteReader();
        //        try
        //        {
        //            while (objDataReader.Read())
        //            {
        //                OnlineOrderMain mainData = new OnlineOrderMain();
        //                mainData.OrderDeliveryDate = objDataReader["ORDER_DELIVER_DATE"].ToString();
        //                //  mainData.OrderSourceName = objDataReader["ORDER_SOURCE_ID"].ToString();
        //                mainData.OrderSourceId = objDataReader["ORDER_SOURCE_ID"].ToString();
        //                mainData.CustomerHomeAddress = objDataReader["CUSTOMER_HOME_ADDRESS"].ToString();
        //                mainData.CustomerOfficeAddress = objDataReader["CUSTOMER_OFFICE_ADDRESS"].ToString();
        //                mainData.Telephone = objDataReader["TELEPHO_NO"].ToString();
        //                mainData.CellNo = objDataReader["CELL_NO"].ToString();
        //                mainData.WebAddress = objDataReader["WEB_ADDRESS"].ToString();
        //                //objTrimsMain.Pro = objDataReader["PROMOTION_CODE"].ToString();
        //                mainData.DiscountAmount = objDataReader["DISCOUNT_AMOUNT"].ToString();
        //                mainData.TotalAmount = objDataReader["TOTAL_AMOUNT"].ToString();
        //                mainData.Delivered_YN = objDataReader["DELIVERED_YN"].ToString();
        //                mainData.OrderNo = objDataReader["ORDER_NO"].ToString();
        //                mainData.DeliveryCost = objDataReader["DELIVERY_COST"].ToString();
        //                mainData.OrderReceiveDate = objDataReader["ORDER_RECEIVE_DATE"].ToString();
        //                mainData.Remarks = objDataReader["REMARKS"].ToString();
        //                mainData.EmailAddress = objDataReader["EMAIL_ADDRESS"].ToString();
        //                // objTrimsMain.Pro = objDataReader["DELIVERY_PROCESS_COST"].ToString();

        //                objOnlineOrderMainList.Add(mainData);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            throw new Exception("Error : " + ex.Message);

        //        }

        //        finally
        //        {

        //            strConn.Close();
        //        }

        //    }
        //    return objOnlineOrderMainList;
        //}


        public List<OnlineOrderSub> GetTrimsGridDataList(string pOrderReceiveDate, string pOrderDeliveryDate, string pCustomerHomeAddress, string pCustomerName,string pCellNo, string pOrderSourceId, string pWebAddress, string pDelivered_YN, string pOrderNo, string headOfficeId, string branchOfficeId)
        {
            List<OnlineOrderSub> objOnlineOrderMainList = new List<OnlineOrderSub>();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +

                   "TO_CHAR(ORDER_RECEIVE_DATE,'dd/mm/yyyy')ORDER_RECEIVE_DATE, " +
                  "TO_CHAR(ORDER_DELIVER_DATE,'dd/mm/yyyy')ORDER_DELIVER_DATE, " +

     "ORDER_SOURCE_ID," +
     "ORDER_SOURCE_NAME, " +
     "CUSTOMER_NAME, " +
     "CUSTOMER_HOME_ADDRESS, " +
     "CUSTOMER_OFFICE_ADDRESS, " +
     "TELEPHO_NO, " +
     "CELL_NO, " +
     "WEB_ADDRESS, " +
     "PRODUCT_DESCRIPTION, " +
     "STYLE_NAME, " +
     "COLOR_NAME, " +
     "SIZE_NAME, " +
     "PROMOTION_CODE, " +
     "PRODUCT_QUANTITY, " +
     "PRODUCT_PRICE, " +
     "DISCOUNT_AMOUNT, " +
     "TOTAL_AMOUNT, " +
     "DELIVERED_YN, " +
     "CREATE_BY, " +
     "CREATE_DATE, " +
    " UPDATE_BY, " +
     "UPDATE_DATE, " +
     "HEAD_OFFICE_ID," +
     "BRANCH_OFFICE_ID, " +
     "PRODUCT_PIC, " +
     "delivery_cost, " +
     "ORDER_NO, " +
     "DELIVERED_STATUS, " +
     "REMARKS, " +
     "EMAIL_ADDRESS, " +
     "DELIVERY_PROCESS_COST, " +
     "TRAN_ID " +

                    " FROM VEW_ONLINE_ORDER_SUB where head_office_id = '" + headOfficeId + "' and branch_office_id = '" + branchOfficeId + "'   ";
            //"from VEW_ONLINE_ORDER_MAIN where order_no = '" + pOrderNo + "'  and head_office_id = '" + headOfficeId + "' AND branch_office_id = '" + branchOfficeId + "' ";



      //      string pOrderReceiveDate, string pOrderDeliveryDate, string pCustomerHomeAddress, string pCellNo, string pOrderSourceId, string pWebAddress, string pDelivered_YN, string pOrderNo, string headOfficeId, string branchOfficeId
            if (!string.IsNullOrEmpty(pOrderReceiveDate))
            {

                sql = sql + "and ORDER_RECEIVE_DATE = to_date( '" + pOrderReceiveDate + "', 'dd/mm/yyyy') ";
            }

            if (!string.IsNullOrEmpty(pOrderDeliveryDate))
            {

                sql = sql + "and ORDER_DELIVER_DATE = to_date( '" + pOrderDeliveryDate + "', 'dd/mm/yyyy') ";
            }



            if (!string.IsNullOrEmpty(pCustomerHomeAddress))
            {
                sql = sql + "and (lower(CUSTOMER_HOME_ADDRESS) like lower( '%" + pCustomerHomeAddress + "%')  or upper(CUSTOMER_HOME_ADDRESS)like upper('%" + pCustomerHomeAddress + "%')  or lower(CUSTOMER_OFFICE_ADDRESS) like lower( '%" + pCustomerHomeAddress + "%')  or upper(CUSTOMER_OFFICE_ADDRESS)like upper('%" + pCustomerHomeAddress + "%'))";

            }

            if (!string.IsNullOrEmpty(pCustomerName))
            {
                sql = sql + "and (lower(CUSTOMER_NAME) like lower( '%" + pCustomerName + "%')  or upper(CUSTOMER_NAME)like upper('%" + pCustomerName + "%')  )";

            }

            if (!string.IsNullOrEmpty(pCellNo))
            {
                sql = sql + "and (lower(CELL_NO) like lower( '%" + pCellNo + "%')  or upper(CELL_NO)like upper('%" + pCellNo + "%')  or lower(TELEPHO_NO) like lower( '%" + pCellNo + "%')  or upper(TELEPHO_NO)like upper('%" + pCellNo + "%'))";

            }


            if (!string.IsNullOrEmpty(pOrderSourceId))
            {

                sql = sql + "and ORDER_SOURCE_ID = '" + pOrderSourceId + "' ";
            }

            if (!string.IsNullOrEmpty(pOrderNo))
            {

                sql = sql + "and ORDER_NO = '" + pOrderNo + "' ";
            }

            if (pDelivered_YN=="true")
            {

                sql = sql + "and DELIVERED_YN = '" + pDelivered_YN + "' ";
            }




            sql = sql + " order by SL";


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
                     //ORDER NO	PRODUCT DESCRIPTION	STYLE	SIZE	COLOR	QTY	MRP	TOTAL AMOUNT	CUSTOMER NAME	HOME ADDRESS	
                     //CELL NO	TELEPHONE NO	Email	WEB ADDRESS	ORDER RECEIVE DATE	ORDER DELIVERY DATE	ORDER SOURCE	DELIVERY COST	PROMOTION CODE	DISCOUNT	REMARKS	STATUS	ID	Image
                        OnlineOrderSub mainData = new OnlineOrderSub();

                        mainData.OrderNo = objDataReader["ORDER_NO"].ToString();
                        mainData.ProductDescription = objDataReader["PRODUCT_DESCRIPTION"].ToString();
                        mainData.StyleName = objDataReader["STYLE_NAME"].ToString();
                        mainData.SizeName = objDataReader["SIZE_NAME"].ToString();
                        mainData.ColorName = objDataReader["COLOR_NAME"].ToString();
                        mainData.ProductQuantity = objDataReader["PRODUCT_QUANTITY"].ToString();
                        mainData.ProductPrice = objDataReader["PRODUCT_PRICE"].ToString();
                        mainData.TotalAmount = objDataReader["TOTAL_AMOUNT"].ToString();
                        mainData.CustomerName = objDataReader["CUSTOMER_NAME"].ToString();
                        mainData.CustomerHomeAddress = objDataReader["CUSTOMER_HOME_ADDRESS"].ToString();
                        mainData.CellNo = objDataReader["CELL_NO"].ToString();
                        mainData.Telephone = objDataReader["TELEPHO_NO"].ToString();
                        mainData.EmailAddress = objDataReader["EMAIL_ADDRESS"].ToString();
                        mainData.WebAddress = objDataReader["WEB_ADDRESS"].ToString();
                        mainData.OrderReceiveDate = objDataReader["ORDER_RECEIVE_DATE"].ToString();
                        mainData.OrderDeliveryDate = objDataReader["ORDER_DELIVER_DATE"].ToString();
                        mainData.OrderSourceId = objDataReader["ORDER_SOURCE_ID"].ToString();
                        mainData.OrderSourceName = objDataReader["ORDER_SOURCE_NAME"].ToString();

                        mainData.DeliveryCost = objDataReader["DELIVERY_COST"].ToString();
                        mainData.PromoCode = objDataReader["PROMOTION_CODE"].ToString();
                        mainData.DiscountAmount = objDataReader["DISCOUNT_AMOUNT"].ToString();
                        mainData.Remarks = objDataReader["REMARKS"].ToString();
                        mainData.Delivered_YN = objDataReader["DELIVERED_YN"].ToString();
                        mainData.ProductPicture = objDataReader["PRODUCT_PIC"] == DBNull.Value ? new byte[0] : (byte[])objDataReader["PRODUCT_PIC"];


                        //  mainData.OrderSourceName = objDataReader["ORDER_SOURCE_ID"].ToString();
                        //objTrimsMain.Pro = objDataReader["PROMOTION_CODE"].ToString();
                        // objTrimsMain.Pro = objDataReader["DELIVERY_PROCESS_COST"].ToString();

                        objOnlineOrderMainList.Add(mainData);
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
            return objOnlineOrderMainList;
        }



        public string TrimsSubDelete(OnlineOrderSub objOnlineOrderSub)
        {
            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_online_order_delete");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objOnlineOrderSub.TranId.Length > 0)
            {
                objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOnlineOrderSub.TranId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOnlineOrderSub.OrderNo.Length > 0)
            {
                objOracleCommand.Parameters.Add("p_order_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOnlineOrderSub.OrderNo;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_order_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOnlineOrderSub.OrderReceiveDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_order_receive_date", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOnlineOrderSub.OrderReceiveDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_order_receive_date", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOnlineOrderSub.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOnlineOrderSub.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOnlineOrderSub.BranchOfficeId;



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
