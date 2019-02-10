using System;
using System.Data;
using ERP.MODEL;
using Oracle.ManagedDataAccess.Client;

namespace ERP.DAL
{
    public class ProductDAL
    {
        ProductModel objProductModel = new ProductModel();

        OracleTransaction trans = null;
        #region "Oracle Connection Check"
        private OracleConnection GetConnection()
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConString"];
            string strConnString = conString.ConnectionString;
            return new OracleConnection(strConnString);
        }
        #endregion

        #region "Save Product Details"
        public string SaveProductSizeRatio(ProductSizeRatioSave objProductSizeRatioSave)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_product_size_ratio_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductSizeRatioSave.StyleNumber) ? objProductSizeRatioSave.StyleNumber : null;

            objOracleCommand.Parameters.Add("p_STYLE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductSizeRatioSave.StyleName) ? objProductSizeRatioSave.StyleName : null;

            objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductSizeRatioSave.TranId) ? objProductSizeRatioSave.TranId : null;

            objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductSizeRatioSave.SeasondId) ? objProductSizeRatioSave.SeasondId : null;
                
            objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductSizeRatioSave.SeasonYear) ? objProductSizeRatioSave.SeasonYear : null;
            objOracleCommand.Parameters.Add("p_size_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductSizeRatioSave.SizeId) ? objProductSizeRatioSave.SizeId : null;
            objOracleCommand.Parameters.Add("p_size_quantity", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductSizeRatioSave.SizeQuantity) ? objProductSizeRatioSave.SizeQuantity : null;
            objOracleCommand.Parameters.Add("p_CREATE_BY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductSizeRatioSave.CreatedBy) ? objProductSizeRatioSave.CreatedBy : null;
            objOracleCommand.Parameters.Add("p_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductSizeRatioSave.UpdateBy) ? objProductSizeRatioSave.UpdateBy : null;
            objOracleCommand.Parameters.Add("p_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductSizeRatioSave.HeadOfficeId) ? objProductSizeRatioSave.HeadOfficeId : null;
            objOracleCommand.Parameters.Add("p_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductSizeRatioSave.BranchOfficeId) ? objProductSizeRatioSave.BranchOfficeId : null;

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

        public string SaveMeasurmentSheetInformation(MeasurmentSheetSave obMeasurmentSheetSave)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_product_measurement_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("p_style_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(obMeasurmentSheetSave.StyleNumber) ? obMeasurmentSheetSave.StyleNumber : null;

            objOracleCommand.Parameters.Add("P_season_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(obMeasurmentSheetSave.SeasonName) ? obMeasurmentSheetSave.SeasonName : null;

            objOracleCommand.Parameters.Add("p_season_year", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(obMeasurmentSheetSave.SeasonYear) ? obMeasurmentSheetSave.SeasonYear : null;

            objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(obMeasurmentSheetSave.FileName) ? obMeasurmentSheetSave.FileName : null;

            if (!string.IsNullOrWhiteSpace(obMeasurmentSheetSave.FileSize))
            {
                byte[] array = System.Convert.FromBase64String(obMeasurmentSheetSave.FileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(obMeasurmentSheetSave.FileExtension) ? obMeasurmentSheetSave.FileExtension : null;


            objOracleCommand.Parameters.Add("P_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = obMeasurmentSheetSave.UpdateBy;
            objOracleCommand.Parameters.Add("P_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = obMeasurmentSheetSave.HeadOfficeId;
            objOracleCommand.Parameters.Add("P_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = obMeasurmentSheetSave.BranchOfficeId;


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

        public string SaveImageInformation(ImageSave objImageSave)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_product_image_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("p_style_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objImageSave.StyleNumber) ? objImageSave.StyleNumber : null;

            objOracleCommand.Parameters.Add("P_season_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objImageSave.SeasonName) ? objImageSave.SeasonName : null;

            objOracleCommand.Parameters.Add("p_season_year", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objImageSave.SeasonYear) ? objImageSave.SeasonYear : null;

            objOracleCommand.Parameters.Add("P_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objImageSave.ImageFileName) ? objImageSave.ImageFileName : null;

            if (!string.IsNullOrWhiteSpace(objImageSave.ImageFileSize))
            {
                byte[] array = System.Convert.FromBase64String(objImageSave.ImageFileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objImageSave.ImageFileExtension) ? objImageSave.ImageFileExtension : null;


            objOracleCommand.Parameters.Add("P_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objImageSave.UpdateBy;
            objOracleCommand.Parameters.Add("P_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objImageSave.HeadOfficeId;
            objOracleCommand.Parameters.Add("P_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objImageSave.BranchOfficeId;


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

        public string SaveProductInformation(ProductModel objProductModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_PRODUCT_INFORMATION");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            objOracleCommand.Parameters.Add("P_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.TranIdS) ? objProductModel.TranIdS : null;

            objOracleCommand.Parameters.Add("P_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.StyleNumber) ? objProductModel.StyleNumber : null;

            objOracleCommand.Parameters.Add("P_STYLE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.StyleDescription) ? objProductModel.StyleDescription : null;

            objOracleCommand.Parameters.Add("P_CATEGORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.Catagory) ? objProductModel.Catagory : null;

            objOracleCommand.Parameters.Add("P_SUB_CATEGORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.SubCatagory) ? objProductModel.SubCatagory : null;

            objOracleCommand.Parameters.Add("P_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.SeasonName) ? objProductModel.SeasonName : null;

            objOracleCommand.Parameters.Add("P_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.SeasonYear) ? objProductModel.SeasonYear : null;

            objOracleCommand.Parameters.Add("P_FIT_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.SampleFit) ? objProductModel.SampleFit : null;

            objOracleCommand.Parameters.Add("P_SIZE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.SampleSize) ? objProductModel.SampleSize : null;

            objOracleCommand.Parameters.Add("P_WASH_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.WashType) ? objProductModel.WashType : null;

            objOracleCommand.Parameters.Add("P_PRODUCTION_QUANTITY", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.ProductionQuantity) ? objProductModel.ProductionQuantity : null;

            objOracleCommand.Parameters.Add("P_SHOP_IN_HOUSE_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.ShopDisplayDate) ? objProductModel.ShopDisplayDate : null;

            objOracleCommand.Parameters.Add("P_OCCASION_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.Occasion) ? objProductModel.Occasion : null;

            objOracleCommand.Parameters.Add("P_FABRIC_CODE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.FabricCodeS) ? objProductModel.FabricCodeS : null;

            objOracleCommand.Parameters.Add("P_MONTH_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.Month) ? objProductModel.Month : null;

            objOracleCommand.Parameters.Add("p_color_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.ColorNameS) ? objProductModel.ColorNameS : null;

            objOracleCommand.Parameters.Add("P_COLOR_WAY_NO_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.ColorWayNumberS) ? objProductModel.ColorWayNumberS : null;

            objOracleCommand.Parameters.Add("P_COLOR_WAY_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.ColorWayNameS) ? objProductModel.ColorWayNameS : null;

            objOracleCommand.Parameters.Add("P_COLOR_WAY_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.ColorWayTypeS) ? objProductModel.ColorWayTypeS : null;

            objOracleCommand.Parameters.Add("P_FABRIC_CONSUMPTION", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.ConsumptionS) ? objProductModel.ConsumptionS : null;

            objOracleCommand.Parameters.Add("P_FABRIC_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.FabricNameS) ? objProductModel.FabricNameS : null;

            objOracleCommand.Parameters.Add("p_SUPPLIER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.SupplyerS) ? objProductModel.SupplyerS : null;

            objOracleCommand.Parameters.Add("P_MERCHANDISER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objProductModel.MerchandiserName) ? objProductModel.MerchandiserName : null;

            if (!string.IsNullOrWhiteSpace(objProductModel.FabricSwatchS))
            {
                byte[] array = System.Convert.FromBase64String(objProductModel.FabricSwatchS);
                objOracleCommand.Parameters.Add("p_SWATCH_PIC", OracleDbType.Blob, ParameterDirection.Input).Value = array;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_SWATCH_PIC", OracleDbType.Blob, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("P_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProductModel.UpdateBy;
            objOracleCommand.Parameters.Add("P_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProductModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("P_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProductModel.BranchOfficeId;



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

        public string DeleteSizeRatio(SizeRatioDisplay objSizeRatioDelete)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_delete_size_ratio");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objSizeRatioDelete.TranId) ? objSizeRatioDelete.TranId : null;

            objOracleCommand.Parameters.Add("p_style_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objSizeRatioDelete.StyleNo) ? objSizeRatioDelete.StyleNo : null;

            objOracleCommand.Parameters.Add("p_season_year", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objSizeRatioDelete.SeasonYear) ? objSizeRatioDelete.SeasonYear : null;

            objOracleCommand.Parameters.Add("p_season_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objSizeRatioDelete.SeasonId) ? objSizeRatioDelete.SeasonId : null;


            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSizeRatioDelete.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSizeRatioDelete.BranchOfficeId;

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

        public string DeleteColorWay(ColorWayDisplay objColorWayDisplay)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_delete_product_sub");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            objOracleCommand.Parameters.Add("p_tran_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objColorWayDisplay.TranId) ? objColorWayDisplay.TranId : null;

            objOracleCommand.Parameters.Add("p_style_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objColorWayDisplay.StyleNo) ? objColorWayDisplay.StyleNo : null;

            objOracleCommand.Parameters.Add("p_season_year", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objColorWayDisplay.SeasonYear) ? objColorWayDisplay.SeasonYear : null;

            objOracleCommand.Parameters.Add("p_season_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(objColorWayDisplay.SeasonId) ? objColorWayDisplay.SeasonId : null;


            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objColorWayDisplay.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objColorWayDisplay.BranchOfficeId;

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
        #endregion

        #region "Get Data"
        public DataTable GetProductMainList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "SL, " +
   "STYLE_NO, " +
   "STYLE_NAME, " +
   "DESIGNER_ID, " +
   "DESIGNER_NAME, " +
   "MERCHANDISER_ID, " +
   "MERCHANDISER_NAME, " +
   "CREATE_BY, " +
   "CREATE_DATE, " +
   "UPDATE_BY, " +
   "UPDATE_DATE, " +
   "HEAD_OFFICE_ID, " +
   "BRANCH_OFFICE_ID, " +
   "SEASON_YEAR, " +
   "SEASON_ID, " +
   "SEASON_NAME, " +
   "REMARKS, " +
   "SEASON_STATUS, " +
   "MONTH_ID, " +
   "CATEGORY_ID, " +
   "SUB_CATEGORY_ID, " +
   "FIT_ID, " +
   "SIZE_ID, " +
   "WASH_TYPE_ID, " +
   "PRODUCTION_QUANTITY, " +
   "SHOP_IN_HOUSE_DATE " +
                  "FROM VEW_PRODUCT_MAIN order by SEASON_NAME ";

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

        public ProductModel GetProductMainData(ProductModel objProductModel)
        {
            string sql = "";
            sql = "SELECT " +

   "to_char(nvl(STYLE_NO,''))STYLE_NO, " +
   "to_char(nvl(STYLE_NAME,'N/A'))STYLE_NAME, " +
   "to_char(nvl(MERCHANDISER_ID,'0'))MERCHANDISER_ID, " +
   "to_char(nvl(SEASON_YEAR,'0'))SEASON_YEAR, " +
   "to_char(nvl(SEASON_ID,'0'))SEASON_ID, " +
   "to_char(nvl(MONTH_ID,'0'))MONTH_ID, " +
   "to_char(nvl(CATEGORY_ID,'0'))CATEGORY_ID, " +
   "to_char(nvl(SUB_CATEGORY_ID,'0'))SUB_CATEGORY_ID, " +
   "to_char(nvl(FIT_ID,'0'))FIT_ID, " +
   "to_char(nvl(SIZE_ID,'0'))SIZE_ID, " +
   "to_char(nvl(WASH_TYPE_ID,'0'))WASH_TYPE_ID, " +
   "to_char(nvl(PRODUCTION_QUANTITY,'0'))PRODUCTION_QUANTITY, " +
   " PRODUCT_IMAGE, " +
   " MEASURMENT_SHEET, " +
   " OCCASION_ID, " +
   "nvl(to_char(SHOP_IN_HOUSE_DATE, 'dd/mm/yyyy'), '')SHOP_IN_HOUSE_DATE " +

                   " FROM VEW_PRODUCT_MAIN where SEASON_ID = '"+ objProductModel.SeasonName + "' AND SEASON_YEAR = '" + objProductModel.SeasonYear + "' AND STYLE_NO = '" + objProductModel.StyleNumber + "' ";

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

                        objProductModel.StyleNumber = objDataReader.GetString(0);
                        objProductModel.StyleDescription = objDataReader.GetString(1);
                        objProductModel.MerchandiserName = objDataReader.GetString(2);
                        objProductModel.SeasonYear = objDataReader.GetString(3);
                        objProductModel.SeasonName = objDataReader.GetString(4);
                        objProductModel.Month = objDataReader.GetString(5);
                        objProductModel.Catagory = objDataReader.GetString(6);
                        objProductModel.SubCatagory = objDataReader.GetString(7);
                        objProductModel.SampleFit = objDataReader.GetString(8);
                        objProductModel.SampleSize = objDataReader.GetString(9);
                        objProductModel.WashType = objDataReader.GetString(10);
                        objProductModel.ProductionQuantity = objDataReader.GetString(11);
                        objProductModel.ProductImageBytes = (byte[]) objDataReader.GetValue(12);
                        objProductModel.MeasurmentSheetBytes = (byte[]) objDataReader.GetValue(13);
                        objProductModel.Occasion = objDataReader.GetValue(14).ToString();
                        objProductModel.ShopDisplayDate = objDataReader.GetString(15);
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

            return objProductModel;
        }

        public DataTable GetProductSubList(ProductModel objProductModel)
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "SL, " +
   "TRAN_ID, " +
   "STYLE_NO, " +
   "STYLE_NAME, " +
   "MERCHANDISER_ID, " +
   "MERCHANDISER_NAME, " +
   "COLOR_ID, " +
   "COLOR_NAME, " +
   "COLOR_WAY_NO_ID, " +
   "COLOR_WAY_NO_NAME, " +
   "COLOR_WAY_TYPE_ID, " +
  " COLOR_WAY_TYPE_NAME, " +
   "FIT_ID, " +
   "FIT_NAME, " +
   "SEASON_YEAR, " +
   "SEASON_ID, " +
   "SEASON_NAME, " +
   "REMARKS, " +
   "SEASON_STATUS, " +
   "FABRIC_TYPE_ID, " +
   "SUPPLIER_ID, " +
   "FABRIC_TYPE_NAME, " +
   "FABRIC_CODE, " +
   "FABRIC_CONSUMPTION, " +
   "COLOR_WAY_NAME, " +
   "SWATCH_PIC " +
                  "FROM VEW_PRODUCT_SUB where SEASON_ID = '" + objProductModel.SeasonName + "' AND SEASON_YEAR = '" + objProductModel.SeasonYear + "' AND STYLE_NO = '" + objProductModel.StyleNumber + "' ";

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

        public DataTable GetSizeRatioList(ProductModel objProductModel)
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "SL, " +
   "SL, " +
   "STYLE_NO, " +
   "STYLE_NAME, " +
   "SEASON_ID, " +
   "SEASON_YEAR, " +
   "SIZE_ID, " +
   "SIZE_QUANTITY, " +
   "CREATE_BY, " +
   "CREATE_DATE, " +
   "UPDATE_BY, " +
   "UPDATE_DATE, " +
   "HEAD_OFFICE_ID, " +
   "BRANCH_OFFICE_ID, " +
   "TRAN_ID " +
                  "FROM VEW_SIZE_RATIO where SEASON_ID = '" + objProductModel.SeasonName + "' AND SEASON_YEAR = '" + objProductModel.SeasonYear + "' AND STYLE_NO = '" + objProductModel.StyleNumber + "' ";

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

        public ImageSave DisplaySpecificationSheet(ImageSave objImageSave)
        {
            string sql = "";
            sql = "SELECT " +
                "STYLE_NO, " +
              "FILE_NAME, " +
              "FILE_SIZE , " +
              "FILE_EXTENSION, " +
              "UPDATE_BY, " +
              "HEAD_OFFICE_ID, " +
              "BRANCH_OFFICE_ID, " +
              "SEASON_ID, " +
              "SEASON_YEAR " +

                   " FROM PRODUCT_IMAGE where SEASON_ID = '" + objImageSave.SeasonName + "' AND SEASON_YEAR = '" + objImageSave.SeasonYear + "' AND STYLE_NO = '" + objImageSave.StyleNumber + "' ";

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

                        objImageSave.StyleNumber = objDataReader.GetValue(0).ToString();
                        objImageSave.ImageFileName = objDataReader.GetValue(1).ToString();
                        objImageSave.File = (byte[]) objDataReader.GetValue(2);
                        objImageSave.ImageFileExtension = objDataReader.GetValue(3).ToString();
                        objImageSave.UpdateBy = objDataReader.GetValue(4).ToString();
                        objImageSave.HeadOfficeId = objDataReader.GetValue(5).ToString();
                        objImageSave.BranchOfficeId = objDataReader.GetValue(6).ToString();
                        objImageSave.SeasonName = objDataReader.GetValue(7).ToString();
                        objImageSave.SeasonYear = objDataReader.GetValue(8).ToString();
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

            return objImageSave;
        }

        #endregion
    }
}
