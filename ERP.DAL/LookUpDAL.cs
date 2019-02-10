using System;
using System.Collections.Generic;
using ERP.MODEL;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ERP.DAL
{
    public class LookUpDAL
    {
        LookUpModel objLookUpModel = new LookUpModel();

        OracleTransaction trans = null;

        #region Oracle Connection Check

        private OracleConnection GetConnection()
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConString"];
            string strConnString = conString.ConnectionString;
            return new OracleConnection(strConnString);
        }

        #endregion

        #region Common Dropdown List

        public DataTable GetShiftTypeDDList()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT " +
                         "SHIFT_ID, " +
                         "SHIFT_NAME " +
                         "FROM VEW_L_Shift order by SHIFT_ID";

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

        //Leave Request
        public DataTable GetLeaveTypeDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "LEAVE_TYPE_ID, " +
                  "LEAVE_TYPE_NAME " +
                  "FROM L_LEAVE_TYPE ";

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

        #region Inventory Product Details

        public DataTable GetOccasionDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "OCCASION_ID, " +
                  "OCCASION_NAME " +
                  "FROM VEW_OCCASION ";

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

        public DataTable GetSupplirDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "SUPPLIER_ID, " +
                  "SUPPLIER_NAME " +
                  "FROM VEW_SUPPLIER ";

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
        public DataTable GetBranchOfficeId()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "BRANCH_OFFICE_ID, " +
                  "BRANCH_OFFICE_NAME " +
                  "FROM BRANCH_OFFICE  order by BRANCH_OFFICE_ID ";

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
        public DataTable GetWashTypeDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "WASH_ID, " +
                  "WASH_NAME " +
                  "FROM VEW_WASH  order by WASH_ID ";

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
        public DataTable GetSubCategoryDDList(int catagoryId)
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "CATEGORY_ID, " +
                  "SUB_CATEGORY_ID, " +
                  "SUB_CATEGORY_NAME " +
                  "FROM VEW_SUB_CATEGORY WHERE CATEGORY_ID = " + catagoryId + "  order by SUB_CATEGORY_NAME ";

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


        public DataTable GetCategoryDDListForFabric()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT " +
                         "CATEGORY_ID, " +
                         "CATEGORY_NAME " +
                         "FROM L_CATEGORY order by CATEGORY_NAME";

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


        public DataTable GetCategoryDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "CATAGORY_ID, " +
                  "CATAGORY_NAME " +
                  "FROM VEW_CATAGORY  order by CATAGORY_NAME ";

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

        public DataTable GetFabricDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "FABRIC_TYPE_ID, " +
                  "FABRIC_TYPE_NAME " +
                  "FROM VEW_FABRIC  order by FABRIC_TYPE_ID ";

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

        public DataTable GetColorDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "COLOR_ID, " +
                  "COLOR_NAME " +
                  "FROM VEW_COLOR  order by COLOR_NAME ";

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

        public DataTable GetColorWayTypeDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "COLOR_WAY_TYPE_ID, " +
                  "COLOR_WAY_TYPE_NAME " +
                  "FROM VEW_L_COLOR_WAY_TYPE  order by COLOR_WAY_TYPE_ID ";

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

        public DataTable GetMerchandiserNameDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "EMPLOYEE_ID, " +
                  "EMPLOYEE_NAME " +
                  "FROM VEW_MERCHANDISER_INFO ";

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

        public DataTable GetColorWayNumberDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "COLOR_WAY_NO_ID, " +
                  "COLOR_WAY_NO_NAME " +
                  "FROM VEW_L_COLOR_WAY_NO  order by COLOR_WAY_NO_ID ";

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

        public DataTable GetMonthDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "MONTH_ID, " +
                  "MONTH_NAME " +
                  "FROM VEW_MONTH  order by MONTH_ID ";

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

        public DataTable GetGenderId()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "GENDER_ID, " +
                  "GENDER_NAME " +
                  "FROM L_GENDER  order by GENDER_NAME ";

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
        public DataTable GetFitDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "FIT_ID, " +
                  "FIT_NAME " +
                  "FROM VEW_FIT  order by FIT_NAME ";

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

        public DataTable GetSizeDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "SIZE_ID, " +
                  "SIZE_NAME " +
                  "FROM VEW_SIZE  order by SIZE_NAME ";

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

        #endregion

        public DataTable GetCategoryId()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "CATEGORY_ID, " +
                  "CATEGORY_NAME " +

                  "FROM L_CATEGORY order by CATEGORY_ID ";

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

        public DataTable GetBuyerDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "BUYER_ID, " +
                  "BUYER_NAME " +
                  "FROM L_BUYER  order by BUYER_NAME ";

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

        public DataTable GetItemDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "ITEM_ID, " +
                  "ITEM_NAME " +
                  "FROM L_ITEM_TP  order by ITEM_NAME ";

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

        public DataTable GetSeasonDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "SEASON_ID, " +
                  "SEASON_NAME " +
                  "FROM L_SEASON  order by SEASON_NAME ";

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

        public DataTable GetMerchandiserUnitDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "UNIT_ID, " +
                  "UNIT_NAME " +
                  "FROM L_UNIT_MERCHANDISER  order by UNIT_NAME ";

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

        public DataTable GetCurrencyDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "CURRENCY_ID, " +
                  "CURRENCY_NAME " +
                  "FROM L_CURRENCY  order by CURRENCY_NAME ";

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

        public DataTable GetBrandDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "BRAND_ID, " +
                  "BRAND_NAME " +
                  "FROM L_BRAND  order by BRAND_NAME ";

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

        public DataTable GetDepartmendDDList()
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "DEPARTMENT_ID, " +
                  "DEPARTMENT_NAME " +
                  "FROM L_DEPARTMENT";
            sql = sql + " ORDER BY DEPARTMENT_NAME";

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

        public DataTable GetSectionDDList(string pHeadOfficeId, string pBranchOfficeId)
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "SECTION_ID, " +
                  "SECTION_NAME " +
                  //"FROM L_SECTION order by SECTION_NAME";
                  "FROM L_SECTION where head_office_id = '" + pHeadOfficeId + "' and branch_office_id = '" + pBranchOfficeId + "' ORDER BY SECTION_NAME";

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

        public DataTable GetUnitDDList(string pHeadOfficeId, string pBranchOfficeId)
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "UNIT_ID, " +
                  "UNIT_NAME " +
                  //"FROM L_UNIT order by UNIT_NAME";
                  "FROM L_UNIT where head_office_id = '" + pHeadOfficeId + "' and branch_office_id = '" + pBranchOfficeId + "' ORDER BY UNIT_NAME";

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

        public DataTable GetHolidayTypeDDList()
        {
            DataTable dt = new DataTable();
            string sql = "SELECT HOLIDAY_TYPE_ID, HOLIDAY_TYPE_NAME FROM l_holiday_type order by HOLIDAY_TYPE_NAME";

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

        public DataTable GetSupplierDDList()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT " +
                        "SUPPLIER_ID, " +
                        "SUPPLIER_NAME " +
                        "FROM L_SUPPLIER_CP order by SUPPLIER_NAME";

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
        public DataTable GetSupplierDDListForTP()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT " +
                        "SUPPLIER_ID, " +
                        "SUPPLIER_NAME " +
                        "FROM L_SUPPLIER order by SUPPLIER_NAME";

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

        public DataTable GetFabricTypeDDList()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT " +
                         "FABRIC_TYPE_ID, " +
                         "FABRIC_TYPE_NAME " +
                         "FROM L_FABRIC_TYPE order by FABRIC_TYPE_NAME";

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

        public DataTable GetFabricUnitDDList()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT " +
                         "UNIT_ID, " +
                         "UNIT_NAME " +
                         "FROM L_FABRIC_UNIT order by UNIT_NAME";

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

        public DataTable GetLocationDDList()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT " +
                         "BRANCH_OFFICE_ID, " +
                         "BRANCH_OFFICE_NAME " +
                         "FROM BRANCH_OFFICE order by BRANCH_OFFICE_NAME";

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

        //public DataTable GetCategoryDDList()
        //{
        //    DataTable dt = new DataTable();

        //    string sql = "SELECT " +
        //                 "CATEGORY_ID, " +
        //                 "CATEGORY_NAME " +
        //                 "FROM L_CATEGORY order by CATEGORY_NAME";

        //    OracleCommand objCommand = new OracleCommand(sql);
        //    OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);

        //    using (OracleConnection strConn = GetConnection())
        //    {
        //        try
        //        {
        //            objCommand.Connection = strConn;
        //            strConn.Open();
        //            objDataAdapter.Fill(dt);
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

        //    return dt;
        //}

        public DataTable GetDesignerDDList()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT " +
                         "EMPLOYEE_ID, " +
                         "EMPLOYEE_NAME " +
                         "FROM VEW_DESIGNER_INFO order by EMPLOYEE_NAME";

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

        public DataTable GetLabTestDDList()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT " +
                         "CRITERIA_ID, " +
                         "CRITERIA_NAME " +
                         "FROM L_CRITERIA_STORE order by CRITERIA_NAME";

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

        public DataTable GetDayTypeDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "DAY_TYPE_ID, " +
                  "DAY_TYPE_NAME " +

                  "FROM L_DAY_TYPE order by DAY_TYPE_ID ";

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


        public DataTable GetMonthSalaryDDList()
        {
            DataTable objDataTable = new DataTable();
            string vQuery = "SELECT " +
                            "MONTH_ID, " +
                            "MONTH_NAME " +
                            "FROM L_MONTH order by MONTH_ID ";

            OracleCommand objCommand = new OracleCommand(vQuery);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);

            using (OracleConnection objConncetion = GetConnection())
            {
                try
                {
                    objCommand.Connection = objConncetion;
                    objConncetion.Open();
                    objDataAdapter.Fill(objDataTable);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objConncetion.Close();
                }
            }

            return objDataTable;
        }

        public DataTable GetEidId()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "EID_ID, " +
                  "EID_NAME " +
                  "FROM L_EID  order by EID_ID ";

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

        public DataTable GetCurrentMonthId()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql =

                "SELECT " +
                "MONTH_ID, " +
                "MONTH_NAME " +
                "FROM L_MONTH order by MONTH_ID ";

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

        public DataTable GetMovementRegisterType()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql =

                "SELECT " +
                "MOVEMENT_TYPE_ID, " +
                "MOVEMENT_TYPE_NAME " +
                "FROM L_MOVEMENT_TYPE order by MOVEMENT_TYPE_ID ";

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

        #endregion

        #region Blood Group
        public DataTable GetBloodGroupRecord(BloodGroupModel objLookUpModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "BLOOD_GROUP_ID, " +
                  "BLOOD_GROUP_NAME " +
                  "FROM vew_L_BLOOD_GROUP where head_office_id = '" + objLookUpModel.HeadOfficeId.Trim() + "' AND branch_office_id = '" + objLookUpModel.BranchOfficeId.Trim() + "'  ";

            if (!string.IsNullOrEmpty(objLookUpModel.SearchBy))
            {
                sql = sql + "and (lower(BLOOD_GROUP_NAME) like lower( '%" + objLookUpModel.SearchBy.Trim() + "%')  or upper(BLOOD_GROUP_NAME)like upper('%" + objLookUpModel.SearchBy.Trim() + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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
        public string SaveBloodGroup(BloodGroupModel objLookUpModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_blood_group_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objLookUpModel.BloodGroupId != "")
            {
                objOracleCommand.Parameters.Add("p_blood_group_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objLookUpModel.BloodGroupId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_blood_group_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objLookUpModel.BloodGroupName.Trim() != "")
            {
                objOracleCommand.Parameters.Add("p_blood_group_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLookUpModel.BloodGroupName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_blood_group_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLookUpModel.UpdateBy.Trim();
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLookUpModel.HeadOfficeId.Trim();
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLookUpModel.BranchOfficeId.Trim();


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
        public BloodGroupModel GetBloodGroupById(BloodGroupModel objLookUpModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (BLOOD_GROUP_ID,'0')), " +
                  " TO_CHAR (NVL (BLOOD_GROUP_NAME,'N/A')) " +
                  "FROM L_BLOOD_GROUP where head_office_id = '" + objLookUpModel.HeadOfficeId + "' and branch_office_id = '" + objLookUpModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objLookUpModel.BloodGroupId))
            {
                sql = sql + " and BLOOD_GROUP_ID = '" + objLookUpModel.BloodGroupId + "'   ";
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
                        objLookUpModel.BloodGroupId = objReader.GetString(0);
                        objLookUpModel.BloodGroupName = objReader.GetString(1);
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
            return objLookUpModel;
        }

        #endregion

        #region Department

        public DataTable GetDepartmentRecord(DepartmentModel objDepartmentLookUp)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "DEPARTMENT_ID, " +
                  "UNIT_ID, " +
                  "UNIT_NAME, " +
                  "DEPARTMENT_NAME, " +
                  "DEPARTMENT_NAME_BANGLA, " +
                  "DEPARTMENT_CODE " +

                  " FROM vew_department_record where head_office_id = '" + objDepartmentLookUp.HeadOfficeId.Trim() +
                  "' and branch_office_id = '" + objDepartmentLookUp.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objDepartmentLookUp.SearchBy))
            {

                sql = sql + "and (lower(department_name) like lower( '%" + objDepartmentLookUp.SearchBy.Trim() + "%')  " +
                      "or upper(department_name)like upper('%" + objDepartmentLookUp.SearchBy.Trim() + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        // Save department Informati on
        public string SaveDepartmentInfo(DepartmentModel objDepartmentModel)
        {
            string strMsg = "";
            
            OracleCommand objOracleCommand = new OracleCommand("pro_department_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objDepartmentModel.UnitId != "")
            {
                objOracleCommand.Parameters.Add("P_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDepartmentModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objDepartmentModel.DepartmentCode != "")
            {
                objOracleCommand.Parameters.Add("p_department_code", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDepartmentModel.DepartmentCode.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_code", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objDepartmentModel.DepartmentId != "")
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDepartmentModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objDepartmentModel.DepartmentName != "")
            {
                objOracleCommand.Parameters.Add("p_department_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDepartmentModel.DepartmentName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objDepartmentModel.DepartmentNameBangla != "")
            {
                objOracleCommand.Parameters.Add("p_department_name_bangla", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDepartmentModel.DepartmentNameBangla;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_name_bangla", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDepartmentModel.UpdateBy.Trim();
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDepartmentModel.HeadOfficeId.Trim();
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDepartmentModel.BranchOfficeId.Trim();

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

        //Get Department Info By Id
        public DepartmentModel GetDepartmentById(DepartmentModel objDepartmentLookUpModel)
        {
            string sql = "";
            sql = "select DEPARTMENT_ID,Department_Code,Department_Name, Department_Name_Bangla, Unit_Id " +
                  "FROM L_DEPARTMENT  where head_office_id = '" + objDepartmentLookUpModel.HeadOfficeId.Trim() +
                  "' and branch_office_id = '" + objDepartmentLookUpModel.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objDepartmentLookUpModel.DepartmentId))
            {
                sql = sql + " and DEPARTMENT_ID = '" + objDepartmentLookUpModel.DepartmentId.Trim() + "'   ";
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
                        objDepartmentLookUpModel.DepartmentId = objReader["Department_Id"].ToString();
                        objDepartmentLookUpModel.DepartmentCode = objReader["DEPARTMENT_CODE"].ToString();
                        objDepartmentLookUpModel.DepartmentName = objReader["Department_Name"].ToString();
                        objDepartmentLookUpModel.UnitId = objReader["UNIT_ID"].ToString();
                        objDepartmentLookUpModel.DepartmentNameBangla = objReader["Department_Name_Bangla"].ToString();
                        objDepartmentLookUpModel.UnitId = objReader["Unit_Id"].ToString();
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
            return objDepartmentLookUpModel;
        }

        #endregion

        #region Gender

        public DataTable GetGenderRecord(GenderModel objGenderLookUpModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  " GENDER_ID, " +
                  "GENDER_NAME " +

                  "FROM vew_L_GENDER where head_office_id = '" + objGenderLookUpModel.HeadOfficeId.Trim() + "'    ";

            if (!string.IsNullOrEmpty(objGenderLookUpModel.SearchBy))
            {
                sql = sql + "and (lower(GENDER_NAME) like lower( '%" + objGenderLookUpModel.SearchBy.Trim() + "%')  or upper(GENDER_NAME)like upper('%" + objGenderLookUpModel.SearchBy.Trim() + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public string SaveGenderInfo(GenderModel objGenderLookUpModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_gender_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objGenderLookUpModel.GenderId != "")
            {
                objOracleCommand.Parameters.Add("p_gender_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objGenderLookUpModel.GenderId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_gender_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objGenderLookUpModel.GenderName.Trim() != "")
            {
                objOracleCommand.Parameters.Add("p_Gender_Name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objGenderLookUpModel.GenderName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_Gender_Name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objGenderLookUpModel.UpdateBy.Trim();
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objGenderLookUpModel.HeadOfficeId.Trim();
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objGenderLookUpModel.BranchOfficeId.Trim();

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

        public GenderModel GetGenderById(GenderModel objGenderLookUpModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (Gender_ID,'0')), " +
                  " TO_CHAR (NVL (Gender_NAME,'N/A')) " +
                  "FROM L_Gender where head_office_id = '" + objGenderLookUpModel.HeadOfficeId.Trim() + "' and branch_office_id = '" + objGenderLookUpModel.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objGenderLookUpModel.GenderId))
            {
                sql = sql + " and Gender_ID = '" + objGenderLookUpModel.GenderId.Trim() + "'   ";
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
                        objGenderLookUpModel.GenderId = objReader.GetString(0);
                        objGenderLookUpModel.GenderName = objReader.GetString(1);
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

            return objGenderLookUpModel;
        }

        #endregion

        #region Marital Status

        public DataTable GetMaritalStatusRecord(MaritalStatusModel objMaritalStatusLookUpModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  " MARITAL_STATUS_ID, " +
                  "MARITAL_STATUS_NAME " +
                  "FROM vew_L_MARITAL_STATUS where head_office_id = '" + objMaritalStatusLookUpModel.HeadOfficeId.Trim() + "'     ";

            if (!string.IsNullOrEmpty(objMaritalStatusLookUpModel.SearchBy))
            {
                sql = sql + "and (lower(MARITAL_STATUS_NAME) like lower( '%" + objMaritalStatusLookUpModel.SearchBy.Trim() + "%') " +
                      " or upper(MARITAL_STATUS_NAME)like upper('%" + objMaritalStatusLookUpModel.SearchBy.Trim() + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public string SaveMaritalStatusInfo(MaritalStatusModel objMaritalStatusLookUpModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_marital_status_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objMaritalStatusLookUpModel.MaritalStatusId != "")
            {
                objOracleCommand.Parameters.Add("p_marital_status_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objMaritalStatusLookUpModel.MaritalStatusId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_marital_status_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objMaritalStatusLookUpModel.MaritalStatusName.Trim() != "")
            {
                objOracleCommand.Parameters.Add("p_marital_status_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMaritalStatusLookUpModel.MaritalStatusName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_marital_status_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMaritalStatusLookUpModel.UpdateBy.Trim();
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMaritalStatusLookUpModel.HeadOfficeId.Trim();
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMaritalStatusLookUpModel.BranchOfficeId.Trim();

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

        public MaritalStatusModel GetMaritalStatusById(MaritalStatusModel objMaritalStatusLookUpModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (MARITAL_STATUS_ID,'0')), " +
                  " TO_CHAR (NVL (MARITAL_STATUS_NAME,'N/A')) " +
                  "FROM L_MARITAL_STATUS where head_office_id = '" + objMaritalStatusLookUpModel.HeadOfficeId.Trim() +
                  "' and branch_office_id = '" + objMaritalStatusLookUpModel.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objMaritalStatusLookUpModel.MaritalStatusId))
            {
                sql = sql + " and MARITAL_STATUS_ID = '" + objMaritalStatusLookUpModel.MaritalStatusId.Trim() + "'   ";
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
                        objMaritalStatusLookUpModel.MaritalStatusId = objReader.GetString(0);
                        objMaritalStatusLookUpModel.MaritalStatusName = objReader.GetString(1);
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
            return objMaritalStatusLookUpModel;
        }

        #endregion

        #region District 

        public DataTable GetDistrictRecord(DistrictModel objDistrictLookUpModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  " DISTRICT_ID, " +
                  "DISTRICT_NAME, " +
                  "DIVISION_ID," +
                  "division_name " +

                  "FROM vew_district_record where head_office_id = '" + objDistrictLookUpModel.HeadOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objDistrictLookUpModel.SearchBy))
            {
                sql = sql + "and (lower(DISTRICT_NAME) like lower( '%" + objDistrictLookUpModel.SearchBy.Trim() + "%')  or upper(DISTRICT_NAME)like upper('%" + objDistrictLookUpModel.SearchBy.Trim() + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public string SaveDistrictInfo(DistrictModel objDistrictLookUpModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_district_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objDistrictLookUpModel.DistrictId != "")
            {
                objOracleCommand.Parameters.Add("p_district_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDistrictLookUpModel.DistrictId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_district_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objDistrictLookUpModel.DistrictName != "")
            {
                objOracleCommand.Parameters.Add("p_district_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDistrictLookUpModel.DistrictName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_district_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objDistrictLookUpModel.DivisionId != "")
            {
                objOracleCommand.Parameters.Add("p_division_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDistrictLookUpModel.DivisionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_division_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDistrictLookUpModel.UpdateBy.Trim();
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDistrictLookUpModel.HeadOfficeId.Trim();
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDistrictLookUpModel.BranchOfficeId.Trim();

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

        public DistrictModel GetDistrictById(DistrictModel objDistrictLookUpModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " DISTRICT_ID, " +
                  "DISTRICT_NAME, " +
                  "DIVISION_ID " +

                  "FROM L_DISTRICT where head_office_id = '" + objDistrictLookUpModel.HeadOfficeId.Trim() +
                  "' and branch_office_id = '" + objDistrictLookUpModel.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objDistrictLookUpModel.DistrictId))
            {
                sql = sql + " and DISTRICT_ID = '" + objDistrictLookUpModel.DistrictId.Trim() + "'   ";
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
                        objDistrictLookUpModel.DistrictId = objReader["DISTRICT_ID"].ToString().Trim();
                        objDistrictLookUpModel.DistrictName = objReader["DISTRICT_NAME"].ToString().Trim();
                        objDistrictLookUpModel.DivisionId = objReader["DIVISION_ID"].ToString().Trim();
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
            return objDistrictLookUpModel;
        }

        #endregion

        #region Division

        public DataTable GetDivisionRecord(DivisionModel objDivisionLookUpModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  " DIVISION_ID, " +
                  "DIVISION_NAME " +

                  "FROM vew_L_DIVISION where head_office_id = '" + objDivisionLookUpModel.HeadOfficeId.Trim() + "'  ";

            if (!string.IsNullOrEmpty(objDivisionLookUpModel.SearchBy))
            {
                sql = sql + "and (lower(DIVISION_NAME) like lower( '%" + objDivisionLookUpModel.SearchBy.Trim() + "%')  or " +
                      "upper(DIVISION_NAME)like upper('%" + objDivisionLookUpModel.SearchBy.Trim() + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public string SaveDivisionInfo(DivisionModel objDivisionLookUpModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_division_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objDivisionLookUpModel.DivisionId != "")
            {
                objOracleCommand.Parameters.Add("p_division_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDivisionLookUpModel.DivisionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_division_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objDivisionLookUpModel.DivisionName != "")
            {
                objOracleCommand.Parameters.Add("p_division_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDivisionLookUpModel.DivisionName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_division_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDivisionLookUpModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDivisionLookUpModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDivisionLookUpModel.BranchOfficeId;

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

        public DivisionModel GetDivisionById(DivisionModel objDivisionLookUpModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (DIVISION_ID,'0')), " +
                  " TO_CHAR (NVL (DIVISION_NAME,'N/A')) " +
                  "FROM L_DIVISION where head_office_id = '" + objDivisionLookUpModel.HeadOfficeId.Trim() +
                  "' and branch_office_id = '" + objDivisionLookUpModel.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objDivisionLookUpModel.DivisionId))
            {
                sql = sql + " and DIVISION_ID = '" + objDivisionLookUpModel.DivisionId.Trim() + "'   ";
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
                        objDivisionLookUpModel.DivisionId = objReader.GetString(0);
                        objDivisionLookUpModel.DivisionName = objReader.GetString(1);
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
            return objDivisionLookUpModel;
        }

        #endregion

        #region Country

        public DataTable GetCountryRecord(CountryModel objCountryLookUpModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  " COUNTRY_ID, " +
                  "COUNTRY_NAME " +

                  "FROM vew_L_COUNTRY where head_office_id = '" + objCountryLookUpModel.HeadOfficeId.Trim() + "'    ";

            if (!string.IsNullOrEmpty(objCountryLookUpModel.SearchBy))
            {
                sql = sql + "and (lower(COUNTRY_NAME) like lower( '%" + objCountryLookUpModel.SearchBy.Trim() + "%')  or " +
                      "upper(COUNTRY_NAME)like upper('%" + objCountryLookUpModel.SearchBy.Trim() + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public string SaveCountryInfo(CountryModel objCountryLookUpModel)
        {
            string strMsg = "";
            
            OracleCommand objOracleCommand = new OracleCommand("pro_country_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objCountryLookUpModel.CountryId != "")
            {
                objOracleCommand.Parameters.Add("p_country_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCountryLookUpModel.CountryId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_country_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objCountryLookUpModel.CountryName != "")
            {
                objOracleCommand.Parameters.Add("p_country_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCountryLookUpModel.CountryName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_country_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCountryLookUpModel.UpdateBy.Trim();
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCountryLookUpModel.HeadOfficeId.Trim();
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCountryLookUpModel.BranchOfficeId.Trim();

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

        public CountryModel GetCountryById(CountryModel objCountryLookUpModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (COUNTRY_ID,'0')), " +
                  " TO_CHAR (NVL (COUNTRY_NAME,'N/A')) " +
                  "FROM L_COUNTRY where head_office_id = '" + objCountryLookUpModel.HeadOfficeId.Trim() +
                  "' and branch_office_id = '" + objCountryLookUpModel.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objCountryLookUpModel.CountryId))
            {
                sql = sql + " and COUNTRY_ID = '" + objCountryLookUpModel.CountryId.Trim() + "'   ";
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
                        objCountryLookUpModel.CountryId = objReader.GetString(0);
                        objCountryLookUpModel.CountryName = objReader.GetString(1);
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
            return objCountryLookUpModel;
        }

        #endregion

        #region Religion

        public DataTable GetReligionRecord(ReligionModel objReligionLookUpModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  " RELIGION_ID, " +
                  "RELIGION_NAME " +

                  "FROM vew_L_RELIGION where head_office_id = '" + objReligionLookUpModel.HeadOfficeId.Trim() + "'    ";

            if (!string.IsNullOrEmpty(objReligionLookUpModel.SearchBy))
            {
                sql = sql + "and (lower(RELIGION_NAME) like lower( '%" + objReligionLookUpModel.SearchBy.Trim() + "%')  or" +
                      " upper(RELIGION_NAME)like upper('%" + objReligionLookUpModel.SearchBy.Trim() + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public string SaveReligionInfo(ReligionModel objReligionLookUpModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_religion_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objReligionLookUpModel.ReligionId != "")
            {
                objOracleCommand.Parameters.Add("p_religion_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objReligionLookUpModel.ReligionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_religion_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objReligionLookUpModel.ReligionName != "")
            {
                objOracleCommand.Parameters.Add("p_religion_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objReligionLookUpModel.ReligionName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_religion_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objReligionLookUpModel.UpdateBy.Trim();
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objReligionLookUpModel.HeadOfficeId.Trim();
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objReligionLookUpModel.BranchOfficeId.Trim();

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

        public ReligionModel GetReligionById(ReligionModel objReligionLookUpModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (RELIGION_ID,'0')), " +
                  " TO_CHAR (NVL (RELIGION_NAME,'N/A')) " +
                  "FROM L_RELIGION where head_office_id = '" + objReligionLookUpModel.HeadOfficeId.Trim() +
                  "' and branch_office_id = '" + objReligionLookUpModel.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objReligionLookUpModel.ReligionId))
            {
                sql = sql + " and RELIGION_ID = '" + objReligionLookUpModel.ReligionId.Trim() + "'   ";
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
                        objReligionLookUpModel.ReligionId = objReader.GetString(0);
                        objReligionLookUpModel.ReligionName = objReader.GetString(1);
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
            return objReligionLookUpModel;
        }

        #endregion

        #region Occurance Type

        public DataTable GetOccurenceTypeRecord(OccurrenceTypeModel objOccuranceTypeLookUpModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  " OCCURENCE_TYPE_ID, " +
                  "OCCURENCE_TYPE_NAME " +

                  "FROM vew_L_OCCURENCE_TYPE where head_office_id = '" + objOccuranceTypeLookUpModel.HeadOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objOccuranceTypeLookUpModel.SearchBy))
            {
                sql = sql + "and (lower(OCCURENCE_TYPE_NAME) like lower( '%" + objOccuranceTypeLookUpModel.SearchBy + "%')  " +
                      "or upper(OCCURENCE_TYPE_NAME)like upper('%" + objOccuranceTypeLookUpModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public string SaveOccurenceTypeInfo(OccurrenceTypeModel objOccuranceTypeLookUpModel)
        {
            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_occurence_type_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objOccuranceTypeLookUpModel.OccurenceTypeId != "")
            {
                objOracleCommand.Parameters.Add("p_occurence_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOccuranceTypeLookUpModel.OccurenceTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_occurence_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOccuranceTypeLookUpModel.OccurenceTypeId != "")
            {
                objOracleCommand.Parameters.Add("p_occurence_type_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOccuranceTypeLookUpModel.OccurenceTypeName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_occurence_type_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOccuranceTypeLookUpModel.UpdateBy.Trim();
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOccuranceTypeLookUpModel.HeadOfficeId.Trim();
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOccuranceTypeLookUpModel.BranchOfficeId.Trim();

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

        public OccurrenceTypeModel GetOccuranceTypeById(OccurrenceTypeModel objOccuranceTypeLookUpModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (OCCURENCE_TYPE_ID,'0')), " +
                  " TO_CHAR (NVL (OCCURENCE_TYPE_NAME,'N/A')) " +
                  "FROM L_OCCURENCE_TYPE where head_office_id = '" + objOccuranceTypeLookUpModel.HeadOfficeId.Trim() +
                  "' and branch_office_id = '" + objOccuranceTypeLookUpModel.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objOccuranceTypeLookUpModel.OccurenceTypeId))
            {
                sql = sql + " and OCCURENCE_TYPE_ID = '" + objOccuranceTypeLookUpModel.OccurenceTypeId.Trim() + "'   ";
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
                        objOccuranceTypeLookUpModel.OccurenceTypeId = objReader.GetString(0);
                        objOccuranceTypeLookUpModel.OccurenceTypeName = objReader.GetString(1);
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
            return objOccuranceTypeLookUpModel;
        }

        #endregion

        #region JOB TYPE ENTRY

        public DataTable GetJobTypeRecord(JobTypeModel objJobTypeModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                "rownum sl," +
                " JOB_TYPE_ID, " +
                "JOB_TYPE_NAME " +

                  "FROM vew_JOB_TYPE where head_office_id = '" + objJobTypeModel.HeadOfficeId + "' AND branch_office_id = '" + objJobTypeModel.BranchOfficeId + "'";


            if (!string.IsNullOrEmpty(objJobTypeModel.SearchBy))
            {

                sql = sql + "and (lower(JOB_TYPE_NAME) like lower( '%" + objJobTypeModel.SearchBy + "%')  or upper(JOB_TYPE_NAME)like upper('%" + objJobTypeModel.SearchBy + "%') )";
            }


            sql = sql + " ORDER BY sl ";


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

        public string SaveJobType(JobTypeModel objJobTypeModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_job_type_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objJobTypeModel.JobTypeId != "")
            {
                objOracleCommand.Parameters.Add("p_job_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objJobTypeModel.JobTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_job_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objJobTypeModel.JobTypeName != "")
            {
                objOracleCommand.Parameters.Add("p_job_type_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objJobTypeModel.JobTypeName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_job_type_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objJobTypeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objJobTypeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objJobTypeModel.BranchOfficeId;



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

        public JobTypeModel GetJobTypeById(JobTypeModel objJobTypeModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (JOB_TYPE_ID,'0')), " +
                   " TO_CHAR (NVL (JOB_TYPE_NAME,'N/A')) " +
                  "FROM L_JOB_TYPE where head_office_id = '" + objJobTypeModel.HeadOfficeId + "' and branch_office_id = '" + objJobTypeModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objJobTypeModel.JobTypeId))
            {
                sql = sql + " and JOB_TYPE_ID = '" + objJobTypeModel.JobTypeId + "'   ";
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

                        objJobTypeModel.JobTypeId = objReader.GetString(0);
                        objJobTypeModel.JobTypeName = objReader.GetString(1);


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

            return objJobTypeModel;
        }

        #endregion

        #region EMPLOYEE TYPE INFORMATION

        public DataTable GetEmployeeTypeRecord(EmployeeTypeInfoModel objEmployeeTypeInfoModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                 "rownum sl," +
                " EMPLOYEE_TYPE_ID, " +
                "EMPLOYEE_TYPE_NAME " +

                  "FROM vew_EMPLOYEE_TYPE where head_office_id = '" + objEmployeeTypeInfoModel.HeadOfficeId + "'AND branch_office_id = '" + objEmployeeTypeInfoModel.BranchOfficeId + "'";


            if (!string.IsNullOrEmpty(objEmployeeTypeInfoModel.SearchBy))
            {

                sql = sql + "and (lower(EMPLOYEE_TYPE_NAME) like lower( '%" + objEmployeeTypeInfoModel.SearchBy + "%')  or upper(EMPLOYEE_TYPE_NAME)like upper('%" + objEmployeeTypeInfoModel.SearchBy + "%') )";
            }


            sql = sql + " ORDER BY sl ";



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








        public string SaveEmployeeType(EmployeeTypeInfoModel objEmployeeTypeInfoModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_employee_type_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objEmployeeTypeInfoModel.EmployeeTypeId != "")
            {
                objOracleCommand.Parameters.Add("p_employee_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objEmployeeTypeInfoModel.EmployeeTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objEmployeeTypeInfoModel.EmployeeTypeName != "")
            {
                objOracleCommand.Parameters.Add("p_employee_type_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objEmployeeTypeInfoModel.EmployeeTypeName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_employee_type_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeTypeInfoModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeTypeInfoModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objEmployeeTypeInfoModel.BranchOfficeId;



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


        public EmployeeTypeInfoModel GetEmployeeTypeById(EmployeeTypeInfoModel objEmployeeTypeInfoModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (EMPLOYEE_TYPE_ID,'0')), " +
                   " TO_CHAR (NVL (EMPLOYEE_TYPE_NAME,'N/A')) " +
                  "FROM L_EMPLOYEE_TYPE where head_office_id = '" + objEmployeeTypeInfoModel.HeadOfficeId + "' and branch_office_id = '" + objEmployeeTypeInfoModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objEmployeeTypeInfoModel.EmployeeTypeId))
            {
                sql = sql + " and EMPLOYEE_TYPE_ID = '" + objEmployeeTypeInfoModel.EmployeeTypeId + "'   ";
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

                        objEmployeeTypeInfoModel.EmployeeTypeId = objReader.GetString(0);
                        objEmployeeTypeInfoModel.EmployeeTypeName = objReader.GetString(1);


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

            return objEmployeeTypeInfoModel;
        }

        #endregion

        #region PAYMENT TYPE INFO


        public DataTable GetPaymentTypeRecord(PaymentTypeModel objPaymentTypeModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                 "rownum sl," +
                " PAYMENT_TYPE_ID, " +
                "PAYMENT_TYPE_NAME " +

                  "FROM vew_PAYMENT_TYPE where head_office_id = '" + objPaymentTypeModel.HeadOfficeId + "'and branch_office_id = '" + objPaymentTypeModel.BranchOfficeId + "'";


            if (!string.IsNullOrEmpty(objPaymentTypeModel.SearchBy))
            {

                sql = sql + "and (lower(PAYMENT_TYPE_NAME) like lower( '%" + objPaymentTypeModel.SearchBy + "%')  or upper(PAYMENT_TYPE_NAME)like upper('%" + objPaymentTypeModel.SearchBy + "%') )";
            }


            sql = sql + " ORDER BY sl ";



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

        public string SavePaymentType(PaymentTypeModel objPaymentTypeModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_payment_type_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objPaymentTypeModel.PaymentTypeId != "")
            {
                objOracleCommand.Parameters.Add("p_payment_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPaymentTypeModel.PaymentTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_payment_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objPaymentTypeModel.PaymentTypeName != "")
            {
                objOracleCommand.Parameters.Add("p_payment_type_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPaymentTypeModel.PaymentTypeName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_payment_type_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPaymentTypeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPaymentTypeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPaymentTypeModel.BranchOfficeId;



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


        public PaymentTypeModel GetPaymentTypeById(PaymentTypeModel objPaymentTypeModel)
        {

            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (PAYMENT_TYPE_ID,'0')), " +
                   " TO_CHAR (NVL (PAYMENT_TYPE_NAME,'N/A')) " +
                  "FROM L_PAYMENT_TYPE where head_office_id = '" + objPaymentTypeModel.HeadOfficeId + "' and branch_office_id = '" + objPaymentTypeModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objPaymentTypeModel.PaymentTypeId))
            {
                sql = sql + " and PAYMENT_TYPE_ID = '" + objPaymentTypeModel.PaymentTypeId + "'   ";
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

                        objPaymentTypeModel.PaymentTypeId = objReader.GetString(0);
                        objPaymentTypeModel.PaymentTypeName = objReader.GetString(1);


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

            return objPaymentTypeModel;
        }
        #endregion

        #region DESIGNATION INFO


        public DataTable GetDesignationRecord(DesignationModel objDesignationModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl," +
                  "DESIGNATION_ID, " +
                  "DESIGNATION_NAME, " +
                  "DESIGNATION_NAME_BANGLA " +
                  "FROM vew_DESIGNATION where head_office_id = '" + objDesignationModel.HeadOfficeId + "'and branch_office_id = '" + objDesignationModel.BranchOfficeId + "'";


            if (!string.IsNullOrEmpty(objDesignationModel.SearchBy))
            {

                sql = sql + "and (lower(DESIGNATION_NAME) like lower( '%" + objDesignationModel.SearchBy + "%')  or upper(DESIGNATION_NAME)like upper('%" + objDesignationModel.SearchBy + "%') )";
            }


            sql = sql + " ORDER BY sl ";



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

        public string SaveDesignationType(DesignationModel objDesignationModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_designation_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objDesignationModel.DesignationId != "")
            {
                objOracleCommand.Parameters.Add("p_designation_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDesignationModel.DesignationId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_designation_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objDesignationModel.DesignationName != "")
            {
                objOracleCommand.Parameters.Add("p_designation_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDesignationModel.DesignationName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_designation_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objDesignationModel.DesignationNameBangla != "")
            {
                objOracleCommand.Parameters.Add("p_designation_name_bangla", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDesignationModel.DesignationNameBangla;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_designation_name_bangla", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }




            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDesignationModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDesignationModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDesignationModel.BranchOfficeId;



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


        public DesignationModel GetDesignationTypeById(DesignationModel objDesignationModel)
        {

            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (DESIGNATION_ID,'0')), " +
                   " TO_CHAR (NVL (DESIGNATION_NAME,'N/A')), " +
                     " TO_CHAR (NVL (DESIGNATION_NAME_BANGLA,'N/A')) " +
                  "FROM L_DESIGNATION where head_office_id = '" + objDesignationModel.HeadOfficeId + "' and branch_office_id = '" + objDesignationModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objDesignationModel.DesignationId))
            {
                sql = sql + " and DESIGNATION_ID = '" + objDesignationModel.DesignationId + "'   ";
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

                        objDesignationModel.DesignationId = objReader.GetString(0);
                        objDesignationModel.DesignationName = objReader.GetString(1);
                        objDesignationModel.DesignationNameBangla = objReader.GetString(2);

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

            return objDesignationModel;
        }





        #endregion

        #region UNIT INFO
        public DataTable GetUnitRecord(UnitModel objUnitModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                 "rownum sl," +
                 "UNIT_CODE, " +
                 "UNIT_ID, " +
                 "UNIT_NAME, " +
                 "UNIT_NAME_BANGLA " +

                 "FROM vew_UNIT where head_office_id = '" + objUnitModel.HeadOfficeId + "' and branch_office_id = '" + objUnitModel.BranchOfficeId + "'   ";


            if (!string.IsNullOrEmpty(objUnitModel.SearchBy))
            {

                sql = sql + "and (lower(UNIT_NAME) like lower( '%" + objUnitModel.SearchBy + "%')  or upper(UNIT_NAME)like upper('%" + objUnitModel.SearchBy + "%') )";
            }


            sql = sql + " ORDER BY sl ";

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

        public string SaveUnitType(UnitModel objUnitModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_unit_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objUnitModel.UnitCode != "")
            {
                objOracleCommand.Parameters.Add("p_unit_code", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objUnitModel.UnitCode;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_code", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objUnitModel.UnitId != "")
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objUnitModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objUnitModel.UnitName != "")
            {
                objOracleCommand.Parameters.Add("p_unit_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objUnitModel.UnitName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objUnitModel.UnitNameBangla != "")
            {
                objOracleCommand.Parameters.Add("p_unit_name_bangla", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objUnitModel.UnitNameBangla;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_name_bangla", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objUnitModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objUnitModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objUnitModel.BranchOfficeId;


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


        public UnitModel GetUnitTypeById(UnitModel objUnitModel)
        {

            string sql = "";
            sql = "SELECT " +
                 " TO_CHAR (NVL (UNIT_CODE,'N/A')), " +
                  " TO_CHAR (NVL (UNIT_ID,'0')), " +
                   " TO_CHAR (NVL (UNIT_NAME,'N/A')), " +
                    " TO_CHAR (NVL (UNIT_NAME_BANGLA,'N/A')) " +
                     "FROM L_UNIT where head_office_id = '" + objUnitModel.HeadOfficeId + "' and branch_office_id = '" + objUnitModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objUnitModel.UnitId))
            {
                sql = sql + " and UNIT_ID = '" + objUnitModel.UnitId + "'   ";
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
                        objUnitModel.UnitCode = objReader.GetString(0);
                        objUnitModel.UnitId = objReader.GetString(1);
                        objUnitModel.UnitName = objReader.GetString(2);
                        objUnitModel.UnitNameBangla = objReader.GetString(3);

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

            return objUnitModel;
        }
        #endregion

        #region SECTION INFO

        public DataTable GetSectionRecord(SectionModel objSectionModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                    "rownum sl, " +
                    "SECTION_ID, " +
                    "SECTION_NAME, " +
                    "SECTION_NAME_BANGLA, " +
                    "DEPARTMENT_ID, " +
                    "department_name, " +
                    "section_code " +

                  "FROM vew_section_record where head_office_id = '" + objSectionModel.HeadOfficeId + "' and branch_office_id = '" + objSectionModel.BranchOfficeId + "'   ";

            //if (!string.IsNullOrWhiteSpace(objSectionModel.SearchBy))
            //{
            //    sql = sql + "and (lower(SECTION_NAME) like lower( '%" + objSectionModel.SearchBy + "%')  or upper(SECTION_NAME)like upper('%" + objSectionModel.SearchBy + "%') )";
            //}

            if (!string.IsNullOrWhiteSpace(objSectionModel.SearchBy))
            {
                sql = sql + "and (lower(SECTION_NAME) like lower( '%" + objSectionModel.SearchBy +
                      "%')  or upper(SECTION_NAME)like upper('%" + objSectionModel.SearchBy + "%') )" +
                      "or (lower(section_code) like lower( '%" + objSectionModel.SearchBy +
                      "%')  or upper(section_code)like upper('%" + objSectionModel.SearchBy + "%') )" +
                      "or (lower(department_name) like lower( '%" + objSectionModel.SearchBy +
                      "%')  or upper(department_name)like upper('%" + objSectionModel.SearchBy + "%') )";

            }

            sql = sql + " ORDER BY sl ";

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


        public string SaveSectionType(SectionModel objSectionModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_section_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;



            if (objSectionModel.DepartmentId != "")
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSectionModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objSectionModel.SectionCode != "")
            {
                objOracleCommand.Parameters.Add("p_section_code", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSectionModel.SectionCode;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_code", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objSectionModel.SectionId != "")
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSectionModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objSectionModel.SectionName != "")
            {
                objOracleCommand.Parameters.Add("p_section_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSectionModel.SectionName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objSectionModel.SectionNameBangla != "")
            {
                objOracleCommand.Parameters.Add("p_section_name_bangla", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSectionModel.SectionNameBangla;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_name_bangla", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSectionModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSectionModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSectionModel.BranchOfficeId;


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


        public SectionModel GetSectionTypeById(SectionModel objSectionModel)
        {

            string sql = "";
            sql = "SELECT " +
                 " TO_CHAR (NVL (section_code,'N/A')), " +
                  " TO_CHAR (NVL (SECTION_ID,'0')), " +
                   " TO_CHAR (NVL (SECTION_NAME,'N/A')), " +
                    " TO_CHAR (NVL (SECTION_NAME_BANGLA,'N/A')), " +
                       " TO_CHAR (NVL (DEPARTMENT_ID,'0')) " +
                     "FROM vew_section_record where head_office_id = '" + objSectionModel.HeadOfficeId + "' and branch_office_id = '" + objSectionModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objSectionModel.SectionId))
            {
                sql = sql + " and SECTION_ID = '" + objSectionModel.SectionId + "'   ";
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
                        objSectionModel.SectionCode = objReader.GetString(0);
                        objSectionModel.SectionId = objReader.GetString(1);
                        objSectionModel.SectionName = objReader.GetString(2);
                        objSectionModel.SectionNameBangla = objReader.GetString(3);
                        objSectionModel.DepartmentId = objReader.GetString(4);
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

            return objSectionModel;
        }

        #endregion

        #region GRADE INFO
        public DataTable GetGradeRecord(GradeInfoModel objGradeInfoModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                 "rownum sl," +
                 "GRADE_ID, " +
                 "GRADE_NO " +
                 "FROM vew_GRADE where head_office_id = '" + objGradeInfoModel.HeadOfficeId + "' and branch_office_id = '" + objGradeInfoModel.BranchOfficeId + "'   ";


            if (!string.IsNullOrEmpty(objGradeInfoModel.SearchBy))
            {

                sql = sql + "and (lower(GRADE_NO) like lower( '%" + objGradeInfoModel.SearchBy + "%')  or upper(GRADE_NO)like upper('%" + objGradeInfoModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public string SaveGradeType(GradeInfoModel objGradeInfoModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_grade_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objGradeInfoModel.GradeId != "")
            {
                objOracleCommand.Parameters.Add("p_grade_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objGradeInfoModel.GradeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_grade_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objGradeInfoModel.GradeNo != "")
            {
                objOracleCommand.Parameters.Add("p_grade_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objGradeInfoModel.GradeNo;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_grade_no", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objGradeInfoModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objGradeInfoModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objGradeInfoModel.BranchOfficeId;


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


        public GradeInfoModel GetGradeTypeById(GradeInfoModel objGradeInfoModel)
        {

            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (GRADE_ID,'0')), " +
                   " TO_CHAR (NVL (GRADE_NO,'0')) " +
                     "FROM L_GRADE where head_office_id = '" + objGradeInfoModel.HeadOfficeId + "' and branch_office_id = '" + objGradeInfoModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objGradeInfoModel.GradeId))
            {
                sql = sql + " and GRADE_ID = '" + objGradeInfoModel.GradeId + "'   ";
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
                        objGradeInfoModel.GradeId = objReader.GetString(0);
                        objGradeInfoModel.GradeNo = objReader.GetString(1);

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

            return objGradeInfoModel;
        }
        #endregion

        #region SHIFT TYPE INFO


        public DataTable GetShiftTypeRecord(ShiftTypeModel objShiftTypeModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl," +
                  "SHIFT_ID, " +
                  "SHIFT_NAME " +
                  "FROM vew_SHIFT where head_office_id = '" + objShiftTypeModel.HeadOfficeId + "'and branch_office_id = '" + objShiftTypeModel.BranchOfficeId + "'";


            if (!string.IsNullOrEmpty(objShiftTypeModel.SearchBy))
            {

                sql = sql + "and (lower(SHIFT_NAME) like lower( '%" + objShiftTypeModel.SearchBy + "%')  or upper(SHIFT_NAME)like upper('%" + objShiftTypeModel.SearchBy + "%') )";
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

        public string SaveShiftType(ShiftTypeModel objShiftTypeModel)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_shift_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;



            if (objShiftTypeModel.ShiftId != "")
            {
                objOracleCommand.Parameters.Add("p_shift_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShiftTypeModel.ShiftId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_shift_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objShiftTypeModel.ShiftName != "")
            {
                objOracleCommand.Parameters.Add("p_shift_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShiftTypeModel.ShiftName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_shift_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }




            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objShiftTypeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objShiftTypeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objShiftTypeModel.BranchOfficeId;



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


        public ShiftTypeModel GetShiftTypeById(ShiftTypeModel objShiftTypeModel)
        {

            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (SHIFT_ID,'0')), " +
                  " TO_CHAR (NVL (SHIFT_NAME,'N/A')) " +
                  " FROM L_SHIFT where head_office_id = '" + objShiftTypeModel.HeadOfficeId + "' and branch_office_id = '" + objShiftTypeModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objShiftTypeModel.ShiftId))
            {
                sql = sql + " and SHIFT_ID = '" + objShiftTypeModel.ShiftId + "'   ";
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

                        objShiftTypeModel.ShiftId = objReader.GetString(0);
                        objShiftTypeModel.ShiftName = objReader.GetString(1);


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

            return objShiftTypeModel;
        }
        #endregion

        #region JOB LOCATION INFO

        public DataTable GetJobLocationRecord(JobLocationInfo obJobLocationInfo)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl," +
                  "JOB_LOCATION_ID, " +
                  "JOB_LOCATION " +
                  "FROM vew_JOB_LOCATION where head_office_id = '" + obJobLocationInfo.HeadOfficeId + "'and branch_office_id = '" + obJobLocationInfo.BranchOfficeId + "'";


            if (!string.IsNullOrEmpty(obJobLocationInfo.SearchBy))
            {

                sql = sql + "and (lower(JOB_LOCATION) like lower( '%" + obJobLocationInfo.SearchBy + "%')  or upper(JOB_LOCATION)like upper('%" + obJobLocationInfo.SearchBy + "%') )";
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

        public string SaveJobLocation(JobLocationInfo obJobLocationInfo)
        {

            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_job_location_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;




            if (obJobLocationInfo.JobLocationId != "")
            {
                objOracleCommand.Parameters.Add("p_job_location_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = obJobLocationInfo.JobLocationId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_job_location_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (obJobLocationInfo.JobLocationName != "")
            {
                objOracleCommand.Parameters.Add("p_job_location_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = obJobLocationInfo.JobLocationName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_job_location_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = obJobLocationInfo.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = obJobLocationInfo.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = obJobLocationInfo.BranchOfficeId;



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

        public JobLocationInfo GetJobLocationById(JobLocationInfo obJobLocationInfo)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (JOB_LOCATION_ID,'0')), " +
                  " TO_CHAR (NVL (JOB_LOCATION,'N/A')) " +
                  " FROM L_JOB_LOCATION where head_office_id = '" + obJobLocationInfo.HeadOfficeId + "' and branch_office_id = '" + obJobLocationInfo.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(obJobLocationInfo.JobLocationId))
            {
                sql = sql + " and JOB_LOCATION_ID = '" + obJobLocationInfo.JobLocationId + "'   ";
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
                        obJobLocationInfo.JobLocationId = objReader.GetString(0);
                        obJobLocationInfo.JobLocationName = objReader.GetString(1);
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

            return obJobLocationInfo;
        }

        #endregion

        #region Probation Period

        public DataTable GetProbationPeriodRecord(ProbationPeriodModel objProbationPeriodModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                "rownum sl, " +
                "PROBATION_PERIOD_ID, " +
                "PROBATION_PERIOD " +

                  "FROM vew_PROBATION_PERIOD where head_office_id = '" + objProbationPeriodModel.HeadOfficeId + "' AND branch_office_id = '" + objProbationPeriodModel.BranchOfficeId + "'";

            if (!string.IsNullOrEmpty(objProbationPeriodModel.SearchBy))
            {
                sql = sql + "and (lower(PROBATION_PERIOD) like lower( '%" + objProbationPeriodModel.SearchBy + "%')  or upper(PROBATION_PERIOD)like upper('%" + objProbationPeriodModel.SearchBy + "%') )";
            }
            sql = sql + " ORDER BY sl ";

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

        public ProbationPeriodModel GetProbationPeriodById(ProbationPeriodModel objProbationPeriodModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (PROBATION_PERIOD_ID,'0')), " +
                   " TO_CHAR (NVL (PROBATION_PERIOD,'N/A')) " +
                  "FROM L_PROBATION_PERIOD where head_office_id = '" + objProbationPeriodModel.HeadOfficeId + "' and branch_office_id = '" + objProbationPeriodModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objProbationPeriodModel.ProbationPeriodId))
            {
                sql = sql + " and PROBATION_PERIOD_ID = '" + objProbationPeriodModel.ProbationPeriodId + "'   ";
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
                        objProbationPeriodModel.ProbationPeriodId = objReader.GetString(0);
                        objProbationPeriodModel.ProbationPeriodName = objReader.GetString(1);
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

            return objProbationPeriodModel;
        }

        public string SaveProbationPeriod(ProbationPeriodModel objProbationPeriodModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_probation_period_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objProbationPeriodModel.ProbationPeriodId != "")
            {
                objOracleCommand.Parameters.Add("p_probation_period_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objProbationPeriodModel.ProbationPeriodId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_probation_period_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objProbationPeriodModel.ProbationPeriodName != "")
            {
                objOracleCommand.Parameters.Add("p_probation_period", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objProbationPeriodModel.ProbationPeriodName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_probation_period", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProbationPeriodModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProbationPeriodModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objProbationPeriodModel.BranchOfficeId;

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

            return strMsg;
        }

        #endregion

        #region Major Subject

        public DataTable GetMajorSubjectRecord(MajorSubjectModel objMajorSubjectModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                    "rownum sl, " +
                    "MAJOR_SUBJECT_ID, " +
                    "MAJOR_SUBJECT_NAME " +
                    "FROM Vew_MAJOR_SUBJECT where head_office_id = '" + objMajorSubjectModel.HeadOfficeId + "'     ";

            if (!string.IsNullOrEmpty(objMajorSubjectModel.SearchBy))
            {
                sql = sql + "and (lower(MAJOR_SUBJECT_NAME) like lower( '%" + objMajorSubjectModel.SearchBy + "%')  or upper(MAJOR_SUBJECT_NAME)like upper('%" + objMajorSubjectModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public MajorSubjectModel GetMajorSubjectById(MajorSubjectModel objMajorSubjectModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (MAJOR_SUBJECT_ID,'0')), " +
                   "TO_CHAR (NVL (MAJOR_SUBJECT_NAME,'N/A')) " +
                  "FROM L_MAJOR_SUBJECT where head_office_id = '" + objMajorSubjectModel.HeadOfficeId + "' and branch_office_id = '" + objMajorSubjectModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objMajorSubjectModel.MajorSubjectId))
            {
                sql = sql + " and MAJOR_SUBJECT_ID = '" + objMajorSubjectModel.MajorSubjectId + "'   ";
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
                        objMajorSubjectModel.MajorSubjectId = objReader.GetString(0);
                        objMajorSubjectModel.MajorSubjectName = objReader.GetString(1);
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

            return objMajorSubjectModel;
        }

        public string SaveMajorSubjectInfo(MajorSubjectModel objMajorSubjectModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_major_subject_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objMajorSubjectModel.MajorSubjectId != "")
            {
                objOracleCommand.Parameters.Add("P_MAJOR_SUBJECT_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objMajorSubjectModel.MajorSubjectId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_MAJOR_SUBJECT_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objMajorSubjectModel.MajorSubjectName != "")
            {
                objOracleCommand.Parameters.Add("P_MAJOR_SUBJECT_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objMajorSubjectModel.MajorSubjectName;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_MAJOR_SUBJECT_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMajorSubjectModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMajorSubjectModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objMajorSubjectModel.BranchOfficeId;

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

            return strMsg;
        }

        #endregion

        #region Degree Entry

        public DataTable GetDegreeRecord(DegreeModel objDegreeModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                    "rownum sl, " +
                    " DEGREE_ID, " +
                    "DEGREE_NAME " +

                  "FROM VEW_DEGREE where head_office_id = '" + objDegreeModel.HeadOfficeId + "'    ";

            if (!string.IsNullOrWhiteSpace(objDegreeModel.SearchBy))
            {
                sql = sql + "and (lower(DEGREE_NAME) like lower( '%" + objDegreeModel.SearchBy + "%')  or upper(DEGREE_NAME)like upper('%" + objDegreeModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public DegreeModel GetDegreeById(DegreeModel objDegreeModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (DEGREE_ID,'0')), " +
                   "TO_CHAR (NVL (DEGREE_NAME,'N/A')) " +
                  "FROM L_DEGREE where head_office_id = '" + objDegreeModel.HeadOfficeId + "' and branch_office_id = '" + objDegreeModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objDegreeModel.DegreeId))
            {
                sql = sql + " and DEGREE_ID = '" + objDegreeModel.DegreeId + "'   ";
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
                        objDegreeModel.DegreeId = objReader.GetString(0);
                        objDegreeModel.DegreeName = objReader.GetString(1);
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

            return objDegreeModel;
        }

        public string SaveDegreeInfo(DegreeModel objDegreeModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_degree_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objDegreeModel.DegreeId != "")
            {
                objOracleCommand.Parameters.Add("p_degree_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDegreeModel.DegreeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_degree_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objDegreeModel.DegreeName != "")
            {
                objOracleCommand.Parameters.Add("p_degree_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDegreeModel.DegreeName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_degree_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDegreeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDegreeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDegreeModel.BranchOfficeId;

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

            return strMsg;
        }

        #endregion

        #region Sub Section Entry

        public DataTable GetSubSectionRecord(SubSectionModel objSubSectionModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                    "rownum sl, " +
                    "SUB_SECTION_ID, " +
                    "SUB_SECTION_CODE, " +
                    "SUB_SECTION_NAME, " +
                    "SUB_SECTION_NAME_BANGLA, " +
                    "SECTION_NAME, " +
                    "SECTION_ID " +

                  "FROM vew_sub_section_record where head_office_id = '" + objSubSectionModel.HeadOfficeId + "' and branch_office_id = '" + objSubSectionModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objSubSectionModel.SearchBy))
            {
                sql = sql + "and (lower(SUB_SECTION_NAME) like lower( '%" + objSubSectionModel.SearchBy + "%')  or upper(SUB_SECTION_NAME)like upper('%" + objSubSectionModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public SubSectionModel GetSubSectionById(SubSectionModel objSubSectionModel)
        {
            string sql = "";
            sql = "SELECT " +
                    "TO_CHAR (NVL (SECTION_ID, '0')), " +
                    "TO_CHAR (NVL (SUB_SECTION_NAME, 'N/A')), " +
                    "TO_CHAR (NVL (SUB_SECTION_NAME_BANGLA, 'N/A'))," +
                    "TO_CHAR (NVL (SUB_SECTION_CODE, '0')), " +
                    "TO_CHAR (NVL (SUB_SECTION_ID, '0')) " +
                    "from VEW_SUB_SECTION_RECORD where SUB_SECTION_ID = '" + objSubSectionModel.SubSectionId + "' AND HEAD_OFFICE_ID = '" + objSubSectionModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID ='" + objSubSectionModel.BranchOfficeId + "' ";

            //if (!string.IsNullOrEmpty(objSubSectionModel.SubSectionId))
            //{
            //    sql = sql + " and SUB_SECTION_ID = '" + objSubSectionModel.SubSectionId + "'   ";
            //}

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(sql, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        objSubSectionModel.SectionId = objReader.GetString(0);
                        objSubSectionModel.SubSectionName = objReader.GetString(1);
                        objSubSectionModel.SubSectionNameBangla = objReader.GetString(2);
                        objSubSectionModel.SubSectionCode = objReader.GetString(3);
                        objSubSectionModel.SubSectionId = objReader.GetString(4);
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

            return objSubSectionModel;
        }

        public string SaveSubSectionInfo(SubSectionModel objSubSectionModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_sub_section_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objSubSectionModel.SectionId != "")
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSubSectionModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objSubSectionModel.SubSectionCode != "")
            {
                objOracleCommand.Parameters.Add("p_sub_section_code", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSubSectionModel.SubSectionCode;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_code", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objSubSectionModel.SubSectionId != "")
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSubSectionModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objSubSectionModel.SubSectionName != "")
            {
                objOracleCommand.Parameters.Add("p_sub_section_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSubSectionModel.SubSectionName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objSubSectionModel.SubSectionNameBangla != "")
            {
                objOracleCommand.Parameters.Add("p_sub_section_name_bangla", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSubSectionModel.SubSectionNameBangla;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_name_bangla", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSubSectionModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSubSectionModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSubSectionModel.BranchOfficeId;

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

            return strMsg;
        }

        #endregion

        #region Leave Type

        public DataTable GetLeaveTypeRecord(LeaveTypeModel objLeaveTypeModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                    "rownum sl, " +
                    " LEAVE_TYPE_ID, " +
                    "LEAVE_TYPE_NAME, " +
                    "MAX_LEAVE " +

                  "FROM VEW_LEAVE_TYPE where head_office_id = '" + objLeaveTypeModel.HeadOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objLeaveTypeModel.SearchBy))
            {
                sql = sql + "and ( (lower(LEAVE_TYPE_NAME) like lower( '%" + objLeaveTypeModel.SearchBy + "%')  or upper(LEAVE_TYPE_NAME)like upper('%" + objLeaveTypeModel.SearchBy + "%')  or (lower(LEAVE_TYPE_ID) like lower( '%" + objLeaveTypeModel.SearchBy + "%')  or upper(LEAVE_TYPE_ID)like upper('%" + objLeaveTypeModel.SearchBy + "%'))))";
            }

            sql = sql + " ORDER BY sl ";

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

        public LeaveTypeModel GetLeaveTypeById(LeaveTypeModel objLeaveTypeModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (LEAVE_TYPE_ID,'0')), " +
                   "TO_CHAR (NVL (LEAVE_TYPE_NAME,'N/A')), " +
                   "TO_CHAR (NVL (MAX_LEAVE,'0')) " +
                  "FROM L_LEAVE_TYPE where head_office_id = '" + objLeaveTypeModel.HeadOfficeId + "' and branch_office_id = '" + objLeaveTypeModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objLeaveTypeModel.LeaveTypeId))
            {
                sql = sql + " and LEAVE_TYPE_ID = '" + objLeaveTypeModel.LeaveTypeId + "'   ";
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
                        objLeaveTypeModel.LeaveTypeId = objReader.GetString(0);
                        objLeaveTypeModel.LeaveTypeName = objReader.GetString(1);
                        objLeaveTypeModel.MaxLeave = objReader.GetString(2);
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

            return objLeaveTypeModel;
        }

        public string SaveLeaveTypeInfo(LeaveTypeModel objLeaveTypeModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_leave_type_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objLeaveTypeModel.LeaveTypeId != "")
            {
                objOracleCommand.Parameters.Add("p_leave_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objLeaveTypeModel.LeaveTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_leave_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objLeaveTypeModel.LeaveTypeName != "")
            {
                objOracleCommand.Parameters.Add("p_leave_type_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objLeaveTypeModel.LeaveTypeName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_leave_type_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objLeaveTypeModel.MaxLeave != "")
            {
                objOracleCommand.Parameters.Add("p_max_leave", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objLeaveTypeModel.MaxLeave;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_max_leave", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveTypeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveTypeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objLeaveTypeModel.BranchOfficeId;

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

            return strMsg;
        }

        #endregion

        #region Office Time Entry

        public DataTable GetOfficeTimeRecord(OfficeTimeModel objOfficeTimeModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                "rownum SL, " +
                "UNIT_NAME, " +
                "department_name, " +
                "section_name, " +
                "sub_section_name, " +
                "FIRST_IN_TIME, " +
                "LAST_OUT_TIME, " +
                "LUNCH_OUT_TIME, " +
                "LUNCH_IN_TIME, " +
                "office_time_id " +
                "FROM VEW_OFFICE_TIME where head_office_id = '" + objOfficeTimeModel.HeadOfficeId + "' and branch_office_id = '" + objOfficeTimeModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objOfficeTimeModel.SearchBy))
            {
                sql = sql + "and ( (lower(UNIT_NAME) like lower( '%" + objOfficeTimeModel.SearchBy + "%')  or upper(UNIT_NAME)like upper('%" + objOfficeTimeModel.SearchBy + "%') )" +
                            "or (lower(department_name) like lower( '%" + objOfficeTimeModel.SearchBy + "%')  or upper(department_name)like upper('%" + objOfficeTimeModel.SearchBy + "%') )" +
                            "or (lower(section_name) like lower( '%" + objOfficeTimeModel.SearchBy + "%')  or upper(section_name)like upper('%" + objOfficeTimeModel.SearchBy + "%') )" +
                            "or (lower(sub_section_name) like lower( '%" + objOfficeTimeModel.SearchBy + "%')  or upper(sub_section_name)like upper('%" + objOfficeTimeModel.SearchBy + "%') ) )";
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

        public OfficeTimeModel GetOfficeTimeById(OfficeTimeModel objOfficeTimeModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (office_time_id,'0')), " +
                  "TO_CHAR (NVL (unit_id,'0')), " +
                  "TO_CHAR (NVL (department_id,'0')), " +
                  "TO_CHAR (NVL (section_id,'0')), " +
                  "TO_CHAR (NVL (sub_section_id,'0')), " +
                  "TO_CHAR (NVL (first_in_time,'N/A')), " +
                  "TO_CHAR (NVL (last_out_time,'N/A')), " +
                  "TO_CHAR (NVL (lunch_out_time,'N/A')), " +
                  "TO_CHAR (NVL (lunch_in_time,'N/A')) " +
                  "FROM OFFICE_TIME where head_office_id = '" + objOfficeTimeModel.HeadOfficeId + "' and branch_office_id = '" + objOfficeTimeModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objOfficeTimeModel.OfficeTimeId))
            {
                sql = sql + " and office_time_id = '" + objOfficeTimeModel.OfficeTimeId + "' ";
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
                        objOfficeTimeModel.OfficeTimeId = objReader.GetString(0);
                        objOfficeTimeModel.UnitId = objReader.GetString(1);
                        objOfficeTimeModel.DepartmentId = objReader.GetString(2);
                        objOfficeTimeModel.SectionId = objReader.GetString(3);
                        objOfficeTimeModel.SubSectionId = objReader.GetString(4);
                        objOfficeTimeModel.FirstInTime = objReader.GetString(5);
                        objOfficeTimeModel.LastOutTime = objReader.GetString(6);
                        objOfficeTimeModel.LunchOutTime = objReader.GetString(7);
                        objOfficeTimeModel.LunchInTime = objReader.GetString(8);
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

            return objOfficeTimeModel;
        }

        public DataTable GetDepartmentDDListByUnitId(string pUnitId, string vHeadOfficeId, string vBranchOfficeId)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (DEPARTMENT_ID,'0')) DEPARTMENT_ID, " +
                  "TO_CHAR (NVL (DEPARTMENT_NAME,'N/A')) DEPARTMENT_NAME " +
                  "FROM L_DEPARTMENT where head_office_id = '" + vHeadOfficeId + "' and branch_office_id = '" + vBranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(pUnitId))
            {
                sql = sql + " and UNIT_ID = '" + pUnitId + "' ORDER BY DEPARTMENT_NAME";
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

        public DataTable GetSectionDDListByDepartmentId(string pDepartmentId, string vHeadOfficeId, string vBranchOfficeId)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (SECTION_ID,'0')) SECTION_ID, " +
                  "TO_CHAR (NVL (SECTION_NAME,'N/A')) SECTION_NAME " +
                  "FROM L_SECTION where head_office_id = '" + vHeadOfficeId + "' and branch_office_id = '" + vBranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(pDepartmentId))
            {
                sql = sql + " and DEPARTMENT_ID = '" + pDepartmentId + "' ORDER BY SECTION_NAME";
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

        public DataTable GetSubSectionDDListBySectionId(string pSectionId, string vHeadOfficeId, string vBranchOfficeId)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (SUB_SECTION_ID,'0')) SUB_SECTION_ID, " +
                  "TO_CHAR (NVL (SUB_SECTION_NAME,'N/A')) SUB_SECTION_NAME " +
                  "FROM L_SUB_SECTION where head_office_id = '" + vHeadOfficeId + "' and branch_office_id = '" + vBranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(pSectionId))
            {
                sql = sql + " and SECTION_ID = '" + pSectionId + "' ORDER BY SUB_SECTION_NAME";
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

        public string SaveOfficeTime(OfficeTimeModel objOfficeTimeModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_office_time_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objOfficeTimeModel.OfficeTimeId != "")
            {
                objOracleCommand.Parameters.Add("p_office_time_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.OfficeTimeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_office_time_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.UnitId != "")
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.DepartmentId != "")
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.DepartmentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_department_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.SectionId != "")
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.SectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_section_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.SubSectionId != "")
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.SubSectionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_sub_section_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.FirstInTime != "")
            {
                objOracleCommand.Parameters.Add("p_first_in_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.FirstInTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_first_in_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.LastOutTime != "")
            {
                objOracleCommand.Parameters.Add("p_last_out_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.LastOutTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_last_out_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.LunchOutTime != "")
            {
                objOracleCommand.Parameters.Add("p_lunch_out_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.LunchOutTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_lunch_out_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.LunchInTime != "")
            {
                objOracleCommand.Parameters.Add("p_lunch_in_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.LunchInTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_lunch_in_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.CheckAll == "Y")
            //if (objOfficeTimeModel.CheckAll)
            {
                objOracleCommand.Parameters.Add("p_chk_all", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.CheckAll;
                //objOracleCommand.Parameters.Add("p_chk_all", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = "Y";
            }
            else
            {
                objOracleCommand.Parameters.Add("p_chk_all", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOfficeTimeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOfficeTimeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOfficeTimeModel.BranchOfficeId;

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

            return strMsg;
        }

        #endregion

        #region Supervisor Entry

        public List<SupervisorModel> GetAllSupervisorList(string pHeadOfficeId, string pBranchOfficeId)
        {
            List<SupervisorModel> supervisorList = new List<SupervisorModel>();

            string vQuery = "SELECT rownum SL, EMPLOYEE_ID, EMPLOYEE_NAME, " +
                            "TO_CHAR(JOINING_DATE, 'dd/mm/yyyy') JOINING_DATE, " +
                            "DESIGNATION_NAME, UNIT_NAME, DEPARTMENT_NAME, SECTION_NAME, SUB_SECTION_NAME, ACTIVE_STATUS, EMPLOYEE_IMAGE " +
                            "FROM VEW_SUPERVISOR_RECORD " +
                            "WHERE HEAD_OFFICE_ID = '" + pHeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + pBranchOfficeId + "'";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        SupervisorModel objSupervisorModel = new SupervisorModel();

                        objSupervisorModel.SerialNumber = objReader.GetInt32(0).ToString();
                        objSupervisorModel.EmployeeId = objReader.GetString(1);
                        objSupervisorModel.EmployeeName = objReader.GetString(2);
                        objSupervisorModel.JoiningDate = objReader.GetString(3);
                        objSupervisorModel.DesignationName = objReader.GetString(4);
                        objSupervisorModel.UnitName = objReader.GetString(5);
                        objSupervisorModel.DepartmentName = objReader.GetString(6);
                        objSupervisorModel.SectionName = objReader.GetString(7);
                        objSupervisorModel.SubSectionName = objReader.GetString(8);
                        objSupervisorModel.Status = objReader.GetString(9);

                        objSupervisorModel.EmployeeImage = objReader["EMPLOYEE_IMAGE"] == DBNull.Value ? new byte[0] : (byte[])objReader[10];

                        supervisorList.Add(objSupervisorModel);
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

            return supervisorList;
        }

        public string SaveSupervisorInfo(SupervisorModel objSupervisorModel)
        {
            string strMsg = "";

            if (objSupervisorModel.EmployeeIdList != null && objSupervisorModel.EmployeeIdList.Count > 0)
            {
                foreach (string employeeId in objSupervisorModel.EmployeeIdList)
                {
                    OracleTransaction objOracleTransaction = null;
                    OracleCommand objOracleCommand = new OracleCommand("pro_supervisor_save");
                    objOracleCommand.CommandType = CommandType.StoredProcedure;

                    objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(employeeId) ? employeeId : null;

                    objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupervisorModel.UpdateBy;
                    objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupervisorModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupervisorModel.BranchOfficeId;

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
            }
            else
            {
                strMsg = "No checked employee found.";
            }

            return strMsg;
        }

        #endregion

        #region Approver Entry

        public List<ApproverModel> GetAllApproverList(string pHeadOfficeId, string pBranchOfficeId)
        {
            List<ApproverModel> approverList = new List<ApproverModel>();

            string vQuery = "SELECT rownum SL, EMPLOYEE_ID, EMPLOYEE_NAME, " +
                            "TO_CHAR(JOINING_DATE, 'dd/mm/yyyy') JOINING_DATE, " +
                            "DESIGNATION_NAME, UNIT_NAME, DEPARTMENT_NAME, SECTION_NAME, SUB_SECTION_NAME, ACTIVE_STATUS, EMPLOYEE_IMAGE " +
                            "FROM VEW_APPROVER_RECORD " +
                            "WHERE HEAD_OFFICE_ID = '" + pHeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + pBranchOfficeId + "'";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        ApproverModel objApproverModel = new ApproverModel();

                        objApproverModel.SerialNumber = objReader.GetInt32(0).ToString();
                        objApproverModel.EmployeeId = objReader.GetString(1);
                        objApproverModel.EmployeeName = objReader.GetString(2);
                        objApproverModel.JoiningDate = objReader.GetString(3);
                        objApproverModel.DesignationName = objReader.GetString(4);
                        objApproverModel.UnitName = objReader.GetString(5);
                        objApproverModel.DepartmentName = objReader.GetString(6);
                        objApproverModel.SectionName = objReader.GetString(7);
                        objApproverModel.SubSectionName = objReader.GetString(8);
                        objApproverModel.Status = objReader.GetString(9);

                        objApproverModel.EmployeeImage = objReader["EMPLOYEE_IMAGE"] == DBNull.Value ? new byte[0] : (byte[])objReader[10];

                        approverList.Add(objApproverModel);
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

            return approverList;
        }

        public string SaveApproverInfo(ApproverModel objApproverModel)
        {
            string strMsg = "";

            if (objApproverModel.EmployeeIdList != null && objApproverModel.EmployeeIdList.Count > 0)
            {
                foreach (string employeeId in objApproverModel.EmployeeIdList)
                {
                    OracleTransaction objOracleTransaction = null;
                    OracleCommand objOracleCommand = new OracleCommand("PRO_APPROVER_SAVE");
                    objOracleCommand.CommandType = CommandType.StoredProcedure;

                    objOracleCommand.Parameters.Add("P_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = !string.IsNullOrWhiteSpace(employeeId) ? employeeId : null;

                    objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objApproverModel.UpdateBy;
                    objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objApproverModel.HeadOfficeId;
                    objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objApproverModel.BranchOfficeId;

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
            }
            else
            {
                strMsg = "No checked employee found.";
            }

            return strMsg;
        }

        #endregion

        #region Holiday Type

        public DataTable GetHolidayTypeRecord(HolidayTypeModel objHolidayTypeModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                    "rownum sl, " +
                    "HOLIDAY_TYPE_ID, " +
                    "HOLIDAY_TYPE_NAME " +

                  "FROM VEW_HOLIDAY_TYPE where head_office_id = '" + objHolidayTypeModel.HeadOfficeId + "'    ";

            if (!string.IsNullOrWhiteSpace(objHolidayTypeModel.SearchBy))
            {
                sql = sql + "and (lower(HOLIDAY_TYPE_NAME) like lower( '%" + objHolidayTypeModel.SearchBy + "%')  or upper(HOLIDAY_TYPE_NAME)like upper('%" + objHolidayTypeModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public HolidayTypeModel GetHolidayTypeById(HolidayTypeModel objHolidayTypeModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (HOLIDAY_TYPE_ID,'0')), " +
                   "TO_CHAR (NVL (HOLIDAY_TYPE_NAME,'N/A')) " +
                  "FROM L_HOLIDAY_TYPE where head_office_id = '" + objHolidayTypeModel.HeadOfficeId + "' and branch_office_id = '" + objHolidayTypeModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objHolidayTypeModel.HolidayTypeId))
            {
                sql = sql + " and HOLIDAY_TYPE_ID = '" + objHolidayTypeModel.HolidayTypeId + "'   ";
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
                        objHolidayTypeModel.HolidayTypeId = objReader.GetString(0);
                        objHolidayTypeModel.HolidayTypeName = objReader.GetString(1);
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

            return objHolidayTypeModel;
        }

        public string SaveHolidayType(HolidayTypeModel objHolidayTypeModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_holiday_type_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objHolidayTypeModel.HolidayTypeId != "")
            {
                objOracleCommand.Parameters.Add("p_holiday_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objHolidayTypeModel.HolidayTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_holiday_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objHolidayTypeModel.HolidayTypeName != "")
            {
                objOracleCommand.Parameters.Add("p_holiday_type_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objHolidayTypeModel.HolidayTypeName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_holiday_type_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objHolidayTypeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objHolidayTypeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objHolidayTypeModel.BranchOfficeId;

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

            return strMsg;
        }

        #endregion

        #region Holiday

        public DataTable GetHolidayRecord(HolidayModel objHolidayModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                    "rownum sl, " +
                    "HOLIDAY_ID, " +
                    "holiday_type_name, " +
                    "to_char(holiday_START_DATE, 'dd/mm/yyyy') holiday_START_DATE, " +
                    "to_char(holiday_END_DATE, 'dd/mm/yyyy') holiday_END_DATE, " +
                    "remarks " +
                    "FROM vew_holidyay_info where head_office_id = '" + objHolidayModel.HeadOfficeId + "' and branch_office_id = '" + objHolidayModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objHolidayModel.SearchBy))
            {
                sql = sql + "and ( (lower(holiday_type_name) like lower( '%" + objHolidayModel.SearchBy + "%')  or upper(holiday_type_name)like upper('%" + objHolidayModel.SearchBy + "%')  or (lower(holiday_id) like lower( '%" + objHolidayModel.SearchBy + "%')  or upper(holiday_id)like upper('%" + objHolidayModel.SearchBy + "%') )))";
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

        public HolidayModel GetHolidayById(HolidayModel objHolidayModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (HOLIDAY_ID,'0')), " +
                  "TO_CHAR (NVL (HOLIDAY_TYPE_ID,'0')), " +
                  "TO_CHAR(HOLIDAY_START_DATE, 'dd/mm/yyyy'), " +
                  "TO_CHAR(HOLIDAY_END_DATE, 'dd/mm/yyyy'), " +
                  "TO_CHAR (NVL (REMARKS,'N/A')) " +
                  "FROM HOLIDAY where head_office_id = '" + objHolidayModel.HeadOfficeId + "' and branch_office_id = '" + objHolidayModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objHolidayModel.HolidayId))
            {
                sql = sql + " and HOLIDAY_ID = '" + objHolidayModel.HolidayId + "'   ";
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
                        objHolidayModel.HolidayId = objReader.GetString(0);
                        objHolidayModel.HolidayTypeId = objReader.GetString(1);
                        objHolidayModel.FromDate = objReader.GetString(2);
                        objHolidayModel.ToDate = objReader.GetString(3);
                        objHolidayModel.Remarks = objReader.GetString(4);
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

            return objHolidayModel;
        }

        public string SaveHoliday(HolidayModel objHolidayModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_holiday_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objHolidayModel.HolidayId != "")
            {
                objOracleCommand.Parameters.Add("p_holiday_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objHolidayModel.HolidayId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_holiday_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objHolidayModel.HolidayTypeId != "")
            {
                objOracleCommand.Parameters.Add("p_holiday_type_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objHolidayModel.HolidayTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_holiday_type_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objHolidayModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_holiday_start_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objHolidayModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_holiday_start_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objHolidayModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_holiday_end_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objHolidayModel.ToDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_holiday_end_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objHolidayModel.Remarks != "")
            {
                objOracleCommand.Parameters.Add("p_remarks", OracleDbType.Varchar2, ParameterDirection.Input).Value = objHolidayModel.Remarks;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_remarks", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objHolidayModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objHolidayModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objHolidayModel.BranchOfficeId;

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

            return strMsg;
        }

        #endregion

        //Merchandising
        #region ".xlsx File Upload Directory"

        public PurchaseOrderModel getDirectoryName()
        {
            PurchaseOrderModel objPurchaseOrderModel = new PurchaseOrderModel();


            string sql = "";
            sql = "SELECT " +

                  "NVL(UPLOAD_DIR_NAME,'N/A') " +
                  "FROM S_DATA_UPLOAD_DIR  ";

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

                        objPurchaseOrderModel.DataUploadDir = objDataReader.GetString(0);

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

            return objPurchaseOrderModel;

        }

        #endregion

        #region"Buyer Payment Type"
        public DataTable GetBuyerPaymentTypeEntryRecord(BuyerPaymentTypeEntryModel objBuyerPaymentTypeEntryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";

            sql = "SELECT " +
                "ROWNUM SL, " +
                "PAYMENT_TYPE_ID, " +
                "PAYMENT_TYPE_NAME, " +
                "HEAD_OFFICE_ID, " +
                "BRANCH_OFFICE_ID " +
                "FROM VEW_BUYER_PAYMENT_TYPE WHERE HEAD_OFFICE_ID = '" + objBuyerPaymentTypeEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objBuyerPaymentTypeEntryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objBuyerPaymentTypeEntryModel.SearchBy))
            {

                sql = sql + "AND (LOWER(PAYMENT_TYPE_NAME) LIKE LOWER( '%" + objBuyerPaymentTypeEntryModel.SearchBy + "%')  OR UPPER(PAYMENT_TYPE_NAME) LIKE UPPER ('%" + objBuyerPaymentTypeEntryModel.SearchBy + "%') )";
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
        public string SaveBuyerPaymentTypeEntry(BuyerPaymentTypeEntryModel objBuyerPaymentTypeEntryModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_BUYER_PAYMENT_TYPE_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objBuyerPaymentTypeEntryModel.PaymentTypeId != "")
            {
                objOracleCommand.Parameters.Add("p_payment_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBuyerPaymentTypeEntryModel.PaymentTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_payment_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objBuyerPaymentTypeEntryModel.PaymentTypeName != "")
            {
                objOracleCommand.Parameters.Add("p_payment_type_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerPaymentTypeEntryModel.PaymentTypeName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_payment_type_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerPaymentTypeEntryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerPaymentTypeEntryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerPaymentTypeEntryModel.BranchOfficeId;



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
        public BuyerPaymentTypeEntryModel GetBuyerPaymentTypeEntryById(BuyerPaymentTypeEntryModel objBuyerPaymentTypeEntryModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (PAYMENT_TYPE_ID,'0')), " +
                  "TO_CHAR (NVL (PAYMENT_TYPE_NAME,'N/A')) " +
                  "FROM L_BUYER_PAYMENT_TYPE WHERE HEAD_OFFICE_ID = '" + objBuyerPaymentTypeEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objBuyerPaymentTypeEntryModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objBuyerPaymentTypeEntryModel.PaymentTypeId))
            {
                sql = sql + " AND PAYMENT_TYPE_ID = '" + objBuyerPaymentTypeEntryModel.PaymentTypeId + "'   ";
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

                        objBuyerPaymentTypeEntryModel.PaymentTypeId = objReader.GetString(0);
                        objBuyerPaymentTypeEntryModel.PaymentTypeName = objReader.GetString(1);

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

            return objBuyerPaymentTypeEntryModel;
        }
        #endregion

        #region"Seasson"
        public DataTable GetSeasonEntryRecord(SeasonEntryModel objSeasonEntryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";

            sql = "SELECT " +
                "ROWNUM SL, " +
                "SEASON_ID, " +
                "SEASON_NAME, " +
                "HEAD_OFFICE_ID, " +
                "BRANCH_OFFICE_ID " +
                "FROM VEW_SEASON WHERE HEAD_OFFICE_ID = '" + objSeasonEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objSeasonEntryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objSeasonEntryModel.SearchBy))
            {

                sql = sql + "AND (LOWER(SEASON_NAME) LIKE LOWER( '%" + objSeasonEntryModel.SearchBy + "%')  OR UPPER(SEASON_NAME) LIKE UPPER ('%" + objSeasonEntryModel.SearchBy + "%') )";
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
        public string SaveSeasonEntry(SeasonEntryModel objSeasonEntryModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_SEASON_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objSeasonEntryModel.SeasonId != "")
            {
                objOracleCommand.Parameters.Add("p_season_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSeasonEntryModel.SeasonId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_season_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objSeasonEntryModel.SeasonName != "")
            {
                objOracleCommand.Parameters.Add("p_season_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSeasonEntryModel.SeasonName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_season_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSeasonEntryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSeasonEntryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSeasonEntryModel.BranchOfficeId;



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
        public SeasonEntryModel GetSeasonEntryById(SeasonEntryModel objSeasonEntryModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (SEASON_ID,'0')), " +
                  "TO_CHAR (NVL (SEASON_NAME,'N/A')) " +
                  "FROM L_SEASON WHERE HEAD_OFFICE_ID = '" + objSeasonEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objSeasonEntryModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objSeasonEntryModel.SeasonId))
            {
                sql = sql + " AND SEASON_ID = '" + objSeasonEntryModel.SeasonId + "'   ";
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

                        objSeasonEntryModel.SeasonId = objReader.GetString(0);
                        objSeasonEntryModel.SeasonName = objReader.GetString(1);

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

            return objSeasonEntryModel;
        }
        #endregion

        #region"Currency"
        public DataTable GetCurrencyEntryRecord(CurrencyEntryModel objCurrencyEntryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";

            sql = "SELECT " +
                "ROWNUM SL, " +
                "CURRENCY_ID, " +
                "CURRENCY_NAME, " +
                "HEAD_OFFICE_ID, " +
                "BRANCH_OFFICE_ID " +
                "FROM VEW_CURRENCY WHERE HEAD_OFFICE_ID = '" + objCurrencyEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objCurrencyEntryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objCurrencyEntryModel.SearchBy))
            {

                sql = sql + "AND (LOWER(CURRENCY_NAME) LIKE LOWER( '%" + objCurrencyEntryModel.SearchBy + "%')  OR UPPER(CURRENCY_NAME) LIKE UPPER ('%" + objCurrencyEntryModel.SearchBy + "%') )";
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
        public string SaveCurrencyEntry(CurrencyEntryModel objCurrencyEntryModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_CURRENCY_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objCurrencyEntryModel.CurrencyId != "")
            {
                objOracleCommand.Parameters.Add("p_currency_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCurrencyEntryModel.CurrencyId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_currency_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objCurrencyEntryModel.CurrencyName != "")
            {
                objOracleCommand.Parameters.Add("p_currency_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCurrencyEntryModel.CurrencyName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_currency_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCurrencyEntryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCurrencyEntryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCurrencyEntryModel.BranchOfficeId;



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
        public CurrencyEntryModel GetCurrencyEntryById(CurrencyEntryModel objCurrencyEntryModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (CURRENCY_ID,'0')), " +
                  "TO_CHAR (NVL (CURRENCY_NAME,'N/A')) " +
                  "FROM L_CURRENCY WHERE HEAD_OFFICE_ID = '" + objCurrencyEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objCurrencyEntryModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objCurrencyEntryModel.CurrencyId))
            {
                sql = sql + " AND CURRENCY_ID = '" + objCurrencyEntryModel.CurrencyId + "'   ";
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

                        objCurrencyEntryModel.CurrencyId = objReader.GetString(0);
                        objCurrencyEntryModel.CurrencyName = objReader.GetString(1);

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

            return objCurrencyEntryModel;
        }
        #endregion

        #region"Buyer"
        public DataTable GetBuyerEntryRecord(BuyerEntryModel objBuyerEntryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";

            sql = "SELECT " +
                "ROWNUM SL, " +
                "BUYER_ID, " +
                "BUYER_NAME, " +
                "COUNTRY_ID, " +
                "COUNTRY_NAME, " +
                "CONTACT_NO, " +
                "EMAIL_ADDRESS, " +
                "BUYER_ADDRESS, " +
                "PAYMENT_TYPE_ID, " +
                "PAYMENT_TYPE_NAME, " +
                "UPDATE_BY, " +
                "HEAD_OFFICE_ID, " +
                "BRANCH_OFFICE_ID " +
                "FROM VEW_BUYER_RECORD WHERE HEAD_OFFICE_ID = '" + objBuyerEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objBuyerEntryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objBuyerEntryModel.SearchBy))
            {
                sql = sql + "AND (LOWER(BUYER_NAME) LIKE LOWER( '%" + objBuyerEntryModel.SearchBy + "%')  OR UPPER(BUYER_NAME) LIKE UPPER ('%" + objBuyerEntryModel.SearchBy + "%') OR LOWER(COUNTRY_NAME) LIKE LOWER( '%" + objBuyerEntryModel.SearchBy + "%')  OR UPPER(COUNTRY_NAME) LIKE UPPER ('%" + objBuyerEntryModel.SearchBy + "%') )";

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
        public DataTable GetCountryDDList(BuyerEntryModel objBuyerEntryModel)
        {

            DataTable dtCountry = new DataTable();

            string sql = "";

            sql = "SELECT " +
                "COUNTRY_ID, " +
                "COUNTRY_NAME " +
                "FROM L_COUNTRY WHERE HEAD_OFFICE_ID = '" + objBuyerEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objBuyerEntryModel.BranchOfficeId + "'  ";

            sql = sql + " ORDER BY COUNTRY_NAME";




            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);
            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(dtCountry);
                    dtCountry.AcceptChanges();
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


            return dtCountry;

        }
        public DataTable GetPaymentDDList(BuyerEntryModel objBuyerEntryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";

            sql = "SELECT " +
                "PAYMENT_TYPE_ID, " +
                "PAYMENT_TYPE_NAME " +
                "FROM L_BUYER_PAYMENT_TYPE WHERE HEAD_OFFICE_ID = '" + objBuyerEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objBuyerEntryModel.BranchOfficeId + "'  ";

            sql = sql + " ORDER BY PAYMENT_TYPE_NAME ";


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
        public string SaveBuyerEntry(BuyerEntryModel objBuyerEntryModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("PRO_BUYER_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objBuyerEntryModel.BuyerId != "")
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBuyerEntryModel.BuyerId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objBuyerEntryModel.BuyerName != "")
            {
                objOracleCommand.Parameters.Add("P_BUYER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEntryModel.BuyerName;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BUYER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objBuyerEntryModel.CountryId != "")
            {
                objOracleCommand.Parameters.Add("P_COUNTRY_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEntryModel.CountryId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_COUNTRY_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objBuyerEntryModel.ContactNo != "")
            {
                objOracleCommand.Parameters.Add("P_CONTACT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEntryModel.ContactNo;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_CONTACT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objBuyerEntryModel.EmailAddress != "")
            {
                objOracleCommand.Parameters.Add("P_EMAIL_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEntryModel.EmailAddress;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_EMAIL_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objBuyerEntryModel.BuyerAddress != "")
            {
                objOracleCommand.Parameters.Add("P_BUYER_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEntryModel.BuyerAddress;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BUYER_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            if (objBuyerEntryModel.PaymentBy != "")
            {
                objOracleCommand.Parameters.Add("P_PAYMENT_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEntryModel.PaymentBy;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_PAYMENT_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEntryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEntryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerEntryModel.BranchOfficeId;



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
        public BuyerEntryModel GetBuyerEntryById(BuyerEntryModel objBuyerEntryModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (BUYER_ID,'0')), " +
                  "TO_CHAR (NVL (BUYER_NAME,'N/A')), " +
                  "TO_CHAR (NVL (COUNTRY_ID,'0')), " +
                  "TO_CHAR (NVL (CONTACT_NO,'N/A')), " +
                  "TO_CHAR (NVL (EMAIL_ADDRESS,'N/A')), " +
                  "TO_CHAR (NVL (BUYER_ADDRESS,'N/A')), " +
                  "TO_CHAR (NVL (PAYMENT_TYPE_ID,'0')) " +
                  "FROM VEW_BUYER_RECORD WHERE HEAD_OFFICE_ID = '" + objBuyerEntryModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objBuyerEntryModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objBuyerEntryModel.BuyerId))
            {
                sql = sql + " AND BUYER_ID = '" + objBuyerEntryModel.BuyerId + "'   ";
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

                        objBuyerEntryModel.BuyerId = objReader.GetString(0);
                        objBuyerEntryModel.BuyerName = objReader.GetString(1);
                        objBuyerEntryModel.CountryId = objReader.GetString(2);
                        objBuyerEntryModel.ContactNo = objReader.GetString(3);
                        objBuyerEntryModel.EmailAddress = objReader.GetString(4);
                        objBuyerEntryModel.BuyerAddress = objReader.GetString(5);
                        objBuyerEntryModel.PaymentBy = objReader.GetString(6);

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

            return objBuyerEntryModel;
        }
        #endregion

        #region Color

        public DataTable GetColorRecord(ColorModel objColorLookUpModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl," +
                  " COLOR_ID, " +
                  "COLOR_NAME " +

                  "FROM view_L_Color where head_office_id = '" + objColorLookUpModel.HeadOfficeId + "' and branch_office_id = '" + objColorLookUpModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objColorLookUpModel.SearchBy))
            {
                sql = sql + "and (lower(COLOR_NAME) like lower( '%" + objColorLookUpModel.SearchBy + "%')  or " +
                      "upper(COLOR_NAME)like upper('%" + objColorLookUpModel.SearchBy + "%') )";
            }
            sql = sql + " ORDER BY sl ";

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

        public string SaveColorInfo(ColorModel objColorLookUpModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_color_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objColorLookUpModel.ColorId != "")
            {
                objOracleCommand.Parameters.Add("p_color_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objColorLookUpModel.ColorId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_color_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objColorLookUpModel.ColorName != "")
            {
                objOracleCommand.Parameters.Add("p_color_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objColorLookUpModel.ColorName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_color_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objColorLookUpModel.UpdateBy.Trim();
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objColorLookUpModel.HeadOfficeId.Trim();
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objColorLookUpModel.BranchOfficeId.Trim();

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

        public ColorModel GetColorById(ColorModel objColorLookUpModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (COLOR_ID,'0')), " +
                  " TO_CHAR (NVL (COLOR_NAME,'N/A')) " +
                  "FROM L_COLOR where head_office_id = '" + objColorLookUpModel.HeadOfficeId.Trim() + "' and branch_office_id = '" + objColorLookUpModel.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objColorLookUpModel.ColorId.Trim()))
            {
                sql = sql + " and COLOR_ID = '" + objColorLookUpModel.ColorId.Trim() + "'   ";
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
                        objColorLookUpModel.ColorId = objReader.GetString(0);
                        objColorLookUpModel.ColorName = objReader.GetString(1);
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
            return objColorLookUpModel;
        }

        #endregion

        #region Unit Merchandiser


        public DataTable GetUnitMerchandiserRecord(UnitMerchandiserModel objUnitMerchandiserLookUpModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl," +
                  " UNIT_ID, " +
                  "UNIT_NAME " +

                  "FROM view_L_Unit_Merchandiser where head_office_id = '" + objUnitMerchandiserLookUpModel.HeadOfficeId + "' and branch_office_id = '" + objUnitMerchandiserLookUpModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objUnitMerchandiserLookUpModel.SearchBy))
            {
                sql = sql + "and (lower(UNIT_NAME) like lower( '%" + objUnitMerchandiserLookUpModel.SearchBy + "%')  or " +
                      "upper(UNIT_NAME)like upper('%" + objUnitMerchandiserLookUpModel.SearchBy + "%') )";
            }
            sql = sql + " ORDER BY sl ";

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

        public string SaveUnitMerchandiserInfo(UnitMerchandiserModel objUnitMerchandiserLookUpModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_unit_merchandiser_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objUnitMerchandiserLookUpModel.UnitMerchandiserId != "")
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objUnitMerchandiserLookUpModel.UnitMerchandiserId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objUnitMerchandiserLookUpModel.UnitMerchandiserName != "")
            {
                objOracleCommand.Parameters.Add("p_unit_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objUnitMerchandiserLookUpModel.UnitMerchandiserName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objUnitMerchandiserLookUpModel.UpdateBy.Trim();
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objUnitMerchandiserLookUpModel.HeadOfficeId.Trim();
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objUnitMerchandiserLookUpModel.BranchOfficeId.Trim();

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

        public UnitMerchandiserModel GetUnitMerchandiserById(UnitMerchandiserModel objMerchandiserLookUpModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (UNIT_ID,'0')), " +
                  " TO_CHAR (NVL (UNIT_NAME,'N/A')) " +
                  "FROM L_UNIT_MERCHANDISER where head_office_id = '" + objMerchandiserLookUpModel.HeadOfficeId.Trim() + "' and branch_office_id = '" + objMerchandiserLookUpModel.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objMerchandiserLookUpModel.UnitMerchandiserId.Trim()))
            {
                sql = sql + " and UNIT_ID = '" + objMerchandiserLookUpModel.UnitMerchandiserId.Trim() + "'   ";
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
                        objMerchandiserLookUpModel.UnitMerchandiserId = objReader.GetString(0);
                        objMerchandiserLookUpModel.UnitMerchandiserName = objReader.GetString(1);
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
            return objMerchandiserLookUpModel;
        }

        #endregion

        #region "Brand Entry"

        public DataTable GetBrandEntryRecord(BrandEntryModel objBrandEntryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "BRAND_ID, " +
                "BRAND_NAME " +

                "FROM Vew_BRAND where head_office_id = '" + objBrandEntryModel.HeadOfficeId + "' AND branch_office_id = '" + objBrandEntryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objBrandEntryModel.SearchBy))
            {

                sql = sql + "and (lower(BRAND_NAME) like lower( '%" + objBrandEntryModel.SearchBy + "%')  or upper(BRAND_NAME)like upper('%" + objBrandEntryModel.SearchBy + "%') )";
            }


            sql = sql + " ORDER BY sl ";




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
        public string SaveBrandEntry(BrandEntryModel objBrandEntryModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_brand_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objBrandEntryModel.BrandId != "")
            {
                objOracleCommand.Parameters.Add("p_brand_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBrandEntryModel.BrandId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_brand_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (objBrandEntryModel.BrandName != "")
            {
                objOracleCommand.Parameters.Add("p_brand_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBrandEntryModel.BrandName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_brand_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBrandEntryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBrandEntryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBrandEntryModel.BranchOfficeId;
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
        public BrandEntryModel GetBrandEntryRecordById(BrandEntryModel objBrandEntryModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (BRAND_ID,'0')), " +
                   " TO_CHAR (NVL (BRAND_NAME,'N/A')) " +
                  "FROM L_BRAND where head_office_id = '" + objBrandEntryModel.HeadOfficeId + "' and branch_office_id = '" + objBrandEntryModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objBrandEntryModel.BrandId))
            {
                sql = sql + " and BRAND_ID = '" + objBrandEntryModel.BrandId + "'   ";
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
                        objBrandEntryModel.BrandId = objReader.GetString(0);
                        objBrandEntryModel.BrandName = objReader.GetString(1);


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

            return objBrandEntryModel;
        }
        #endregion

        #region "Item Entry"

        public DataTable GetItemEntryRecord(ItemEntryModel objItemEntryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "ITEM_ID, " +
                "ITEM_NAME " +

                "FROM Vew_ITEM_TP where head_office_id = '" + objItemEntryModel.HeadOfficeId + "' AND branch_office_id = '" + objItemEntryModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objItemEntryModel.SearchBy))
            {

                sql = sql + "and (lower(ITEM_NAME) like lower( '%" + objItemEntryModel.SearchBy + "%')  or upper(ITEM_NAME)like upper('%" + objItemEntryModel.SearchBy + "%') )";
            }


            sql = sql + " ORDER BY sl ";




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
        public string SaveItemEntry(ItemEntryModel objItemEntryModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_item_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objItemEntryModel.ItemId != "")
            {
                objOracleCommand.Parameters.Add("p_item_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objItemEntryModel.ItemId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_item_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objItemEntryModel.ItemName != "")
            {
                objOracleCommand.Parameters.Add("p_item_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objItemEntryModel.ItemName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_item_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objItemEntryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objItemEntryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objItemEntryModel.BranchOfficeId;



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
        public ItemEntryModel GetItemEntryRecordById(ItemEntryModel objItemEntryModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (ITEM_ID,'0')), " +
                   " TO_CHAR (NVL (ITEM_NAME,'N/A')) " +
                  "FROM L_ITEM_TP where head_office_id = '" + objItemEntryModel.HeadOfficeId + "' and branch_office_id = '" + objItemEntryModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objItemEntryModel.ItemId))
            {
                sql = sql + " and ITEM_ID = '" + objItemEntryModel.ItemId + "'   ";
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

                        objItemEntryModel.ItemId = objReader.GetString(0);
                        objItemEntryModel.ItemName = objReader.GetString(1);


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

            return objItemEntryModel;
        }

        #endregion

        #region"Shipment Info"
        public DataTable GetShipmentInfoRecord(ShipmentInfoMODEL objShipmentInfoMODEL)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +

                 "rownum sl, " +
                  "TO_CHAR (NVL (SHIPMENT_INFO_ID,'0'))SHIPMENT_INFO_ID, " +
                  "TO_CHAR (NVL (SHIPMENT_INFO_NAME,'N/A'))SHIPMENT_INFO_NAME, " +
                  "TO_CHAR (NVL (SHIPMENT_INFO_ADDRESS,'N/A'))SHIPMENT_INFO_ADDRESS, " +
                  "TO_CHAR (NVL (MOBILE_NO,'N/A'))MOBILE_NO, " +
                  "TO_CHAR (NVL (PHONE_NO,'N/A'))PHONE_NO, " +
                  "TO_CHAR (NVL (FAX_NO,'N/A'))FAX_NO, " +
                  "TO_CHAR (NVL (EMAIL_ADDRESS,'N/A'))EMAIL_ADDRESS, " +
                  "TO_CHAR (NVL (CONTACT_PERSON,'N/A'))CONTACT_PERSON " +

                "FROM VEW_SHIPMENT_INFO where head_office_id = '" + objShipmentInfoMODEL.HeadOfficeId + "' AND branch_office_id = '" + objShipmentInfoMODEL.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objShipmentInfoMODEL.SearchBy))
            {

                sql = sql + "and (lower(SHIPMENT_INFO_NAME) like lower( '%" + objShipmentInfoMODEL.SearchBy + "%')  or upper(SHIPMENT_INFO_NAME)like upper('%" + objShipmentInfoMODEL.SearchBy + "%') )";
            }


            sql = sql + " ORDER BY sl ";




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
        public string SaveShipmentinfoEntry(ShipmentInfoMODEL objShipmentInfoMODEL)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_shipment_info_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (!string.IsNullOrWhiteSpace(objShipmentInfoMODEL.ShipmentInfoId))
            {
                objOracleCommand.Parameters.Add("P_SHIPMENT_INFO_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShipmentInfoMODEL.ShipmentInfoId.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SHIPMENT_INFO_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (!string.IsNullOrWhiteSpace(objShipmentInfoMODEL.ShipmentInfoName))
            {
                objOracleCommand.Parameters.Add("P_SHIPMENT_INFO_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShipmentInfoMODEL.ShipmentInfoName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SHIPMENT_INFO_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (!string.IsNullOrWhiteSpace(objShipmentInfoMODEL.ShipmentInfoIdAddress))
            {
                objOracleCommand.Parameters.Add("SHIPMENT_INFO_ADDRESS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShipmentInfoMODEL.ShipmentInfoIdAddress.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("SHIPMENT_INFO_ADDRESS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (!string.IsNullOrWhiteSpace(objShipmentInfoMODEL.MobileNo))
            {
                objOracleCommand.Parameters.Add("P_MOBILE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShipmentInfoMODEL.MobileNo.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_MOBILE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (!string.IsNullOrWhiteSpace(objShipmentInfoMODEL.PhoneNo))
            {
                objOracleCommand.Parameters.Add("P_PHONE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShipmentInfoMODEL.PhoneNo.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_PHONE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (!string.IsNullOrWhiteSpace(objShipmentInfoMODEL.FaxNo))
            {
                objOracleCommand.Parameters.Add("P_FAX_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShipmentInfoMODEL.FaxNo.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FAX_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (!string.IsNullOrWhiteSpace(objShipmentInfoMODEL.EmailAddress))
            {
                objOracleCommand.Parameters.Add("P_EMAIL_ADDRESS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShipmentInfoMODEL.EmailAddress.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_EMAIL_ADDRESS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (!string.IsNullOrWhiteSpace(objShipmentInfoMODEL.ContactPerson))
            {
                objOracleCommand.Parameters.Add("P_CONTACT_PERSON", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShipmentInfoMODEL.ContactPerson.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_CONTACT_PERSON", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objShipmentInfoMODEL.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objShipmentInfoMODEL.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objShipmentInfoMODEL.BranchOfficeId;



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
        public ShipmentInfoMODEL GetShipmentInfoRecordById(ShipmentInfoMODEL objShipmentInfoMODEL)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (SHIPMENT_INFO_ID,'0'))SHIPMENT_INFO_ID, " +
                  "TO_CHAR (NVL (SHIPMENT_INFO_NAME,'N/A'))SHIPMENT_INFO_NAME, " +
                  "TO_CHAR (NVL (SHIPMENT_INFO_ADDRESS,'N/A'))SHIPMENT_INFO_ADDRESS, " +
                  "TO_CHAR (NVL (MOBILE_NO,'N/A'))MOBILE_NO, " +
                  "TO_CHAR (NVL (PHONE_NO,'N/A'))PHONE_NO, " +
                  "TO_CHAR (NVL (FAX_NO,'N/A'))FAX_NO, " +
                  "TO_CHAR (NVL (EMAIL_ADDRESS,'N/A'))EMAIL_ADDRESS, " +
                  "TO_CHAR (NVL (CONTACT_PERSON,'N/A'))CONTACT_PERSON " +

                 "FROM VEW_SHIPMENT_INFO where head_office_id = '" + objShipmentInfoMODEL.HeadOfficeId + "' AND branch_office_id = '" + objShipmentInfoMODEL.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objShipmentInfoMODEL.ShipmentInfoId))
            {
                sql = sql + " and SHIPMENT_INFO_ID = '" + objShipmentInfoMODEL.ShipmentInfoId + "'   ";
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

                        objShipmentInfoMODEL.ShipmentInfoId = objReader.GetString(0);
                        objShipmentInfoMODEL.ShipmentInfoName = objReader.GetString(1);
                        objShipmentInfoMODEL.ShipmentInfoIdAddress = objReader.GetString(2);
                        objShipmentInfoMODEL.MobileNo = objReader.GetString(3);
                        objShipmentInfoMODEL.PhoneNo = objReader.GetString(4);
                        objShipmentInfoMODEL.FaxNo = objReader.GetString(5);
                        objShipmentInfoMODEL.EmailAddress = objReader.GetString(6);
                        objShipmentInfoMODEL.ContactPerson = objReader.GetString(7);


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

            return objShipmentInfoMODEL;
        }

        #endregion

        #region"Consignee Bank"
        public DataTable GetConsigneeBankInfoRecord(ConsigneeBankInfo objConsigneeBankInfo)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +

                 "rownum sl, " +
                  "TO_CHAR (NVL (BANK_ID,'0'))BANK_ID, " +
                  "TO_CHAR (NVL (BANK_NAME,'N/A'))BANK_NAME, " +
                  "TO_CHAR (NVL (BANK_ADDRESS,'N/A'))BANK_ADDRESS, " +
                  "TO_CHAR (NVL (MOBILE_NO,'N/A'))MOBILE_NO, " +
                  "TO_CHAR (NVL (PHONE_NO,'N/A'))PHONE_NO, " +
                  "TO_CHAR (NVL (FAX_NO,'N/A'))FAX_NO, " +
                  "TO_CHAR (NVL (EMAIL_ADDRESS,'N/A'))EMAIL_ADDRESS, " +
                  "TO_CHAR (NVL (CONTACT_PERSON,'N/A'))CONTACT_PERSON " +

                "FROM VEW_CONSIGNEE_BANK where head_office_id = '" + objConsigneeBankInfo.HeadOfficeId + "' AND branch_office_id = '" + objConsigneeBankInfo.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objConsigneeBankInfo.SearchBy))
            {

                sql = sql + "and (lower(BANK_NAME) like lower( '%" + objConsigneeBankInfo.SearchBy + "%')  or upper(BANK_NAME)like upper('%" + objConsigneeBankInfo.SearchBy + "%') )";
            }


            sql = sql + " ORDER BY sl ";




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
        public string SaveCosigneeBankEntry(ConsigneeBankInfo objConsigneeBankInfo)
        {
            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_consignee_bank_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (!string.IsNullOrWhiteSpace(objConsigneeBankInfo.BankId))
            {
                objOracleCommand.Parameters.Add("P_BANK_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objConsigneeBankInfo.BankId.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BANK_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (!string.IsNullOrWhiteSpace(objConsigneeBankInfo.BankName))
            {
                objOracleCommand.Parameters.Add("P_BANK_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objConsigneeBankInfo.BankName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BANK_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (!string.IsNullOrWhiteSpace(objConsigneeBankInfo.BankAddress))
            {
                objOracleCommand.Parameters.Add("P_BANK_ADDRESS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objConsigneeBankInfo.BankAddress.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BANK_ADDRESS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (!string.IsNullOrWhiteSpace(objConsigneeBankInfo.MobileNo))
            {
                objOracleCommand.Parameters.Add("P_MOBILE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objConsigneeBankInfo.MobileNo.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_MOBILE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (!string.IsNullOrWhiteSpace(objConsigneeBankInfo.PhoneNo))
            {
                objOracleCommand.Parameters.Add("P_PHONE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objConsigneeBankInfo.PhoneNo.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_PHONE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (!string.IsNullOrWhiteSpace(objConsigneeBankInfo.FaxNo))
            {
                objOracleCommand.Parameters.Add("P_FAX_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objConsigneeBankInfo.FaxNo.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FAX_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (!string.IsNullOrWhiteSpace(objConsigneeBankInfo.EmailAddress))
            {
                objOracleCommand.Parameters.Add("P_EMAIL_ADDRESS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objConsigneeBankInfo.EmailAddress.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_EMAIL_ADDRESS", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (!string.IsNullOrWhiteSpace(objConsigneeBankInfo.ContactPerson))
            {
                objOracleCommand.Parameters.Add("P_CONTACT_PERSON", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objConsigneeBankInfo.ContactPerson.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_CONTACT_PERSON", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objConsigneeBankInfo.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objConsigneeBankInfo.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objConsigneeBankInfo.BranchOfficeId;



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

        public ConsigneeBankInfo ConsigneeBankInfoRecordById(ConsigneeBankInfo objConsigneeBankInfo)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (BANK_ID,'0'))BANK_ID, " +
                  "TO_CHAR (NVL (BANK_NAME,'N/A'))BANK_NAME, " +
                  "TO_CHAR (NVL (BANK_ADDRESS,'N/A'))BANK_ADDRESS, " +
                  "TO_CHAR (NVL (MOBILE_NO,'N/A'))MOBILE_NO, " +
                  "TO_CHAR (NVL (PHONE_NO,'N/A'))PHONE_NO, " +
                  "TO_CHAR (NVL (FAX_NO,'N/A'))FAX_NO, " +
                  "TO_CHAR (NVL (EMAIL_ADDRESS,'N/A'))EMAIL_ADDRESS, " +
                  "TO_CHAR (NVL (CONTACT_PERSON,'N/A'))CONTACT_PERSON " +

                 "FROM VEW_CONSIGNEE_BANK where head_office_id = '" + objConsigneeBankInfo.HeadOfficeId + "' AND branch_office_id = '" + objConsigneeBankInfo.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objConsigneeBankInfo.BankId))
            {
                sql = sql + " and BANK_ID = '" + objConsigneeBankInfo.BankId + "'   ";
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

                        objConsigneeBankInfo.BankId = objReader.GetString(0);
                        objConsigneeBankInfo.BankName = objReader.GetString(1);
                        objConsigneeBankInfo.BankAddress = objReader.GetString(2);
                        objConsigneeBankInfo.MobileNo = objReader.GetString(3);
                        objConsigneeBankInfo.PhoneNo = objReader.GetString(4);
                        objConsigneeBankInfo.FaxNo = objReader.GetString(5);
                        objConsigneeBankInfo.EmailAddress = objReader.GetString(6);
                        objConsigneeBankInfo.ContactPerson = objReader.GetString(7);


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

            return objConsigneeBankInfo;
        }


        #endregion

        #region"Order Type"
        public DataTable GetOrderTypeDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "ORDER_TYPE_ID, " +
                  "ORDER_TYPE_NAME " +

                  "FROM VIEW_L_ORDER_TYPE order by ORDER_TYPE_NAME ";

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
        #endregion

        #region"Mode Of Shipment"
        public DataTable GetModeOfShipmentDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "MODE_OF_SHIPMENT_ID, " +
                  "MODE_OF_SHIPMENT_NAME " +

                  "FROM VIEW_L_MODE_OF_SHIPMENT order by MODE_OF_SHIPMENT_NAME ";

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
        #endregion

        #region"Port Of Landing"
        public DataTable GetPortOfLandingtDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "PORT_OF_LOADING_ID, " +
                  "PORT_OF_LOADING_NAME " +

                  "FROM VIEW_L_PORT_OF_LOADING order by PORT_OF_LOADING_NAME ";

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
        #endregion

        #region"Delivery Place"
        public DataTable GetDeliveryPlaceEntryRecord(DeliveryPlaceModel objDeliveryPlaceModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +

                 "rownum sl, " +
                  "TO_CHAR (NVL (COUNTRY_ID,'0'))COUNTRY_ID, " +
                  "TO_CHAR (NVL (COUNTRY_NAME,'N/A'))COUNTRY_NAME, " +
                  "TO_CHAR (NVL (DELIVERY_PLACE_ID,'0'))DELIVERY_PLACE_ID, " +
                  "TO_CHAR (NVL (DELIVERY_PLACE_NAME,'N/A'))DELIVERY_PLACE_NAME " +


                "FROM VEW_L_DELIVERY_PLACE where head_office_id = '" + objDeliveryPlaceModel.HeadOfficeId + "' AND branch_office_id = '" + objDeliveryPlaceModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objDeliveryPlaceModel.SearchBy))
            {

                sql = sql + "and (lower(COUNTRY_NAME) like lower( '%" + objDeliveryPlaceModel.SearchBy + "%')  or upper(COUNTRY_NAME)like upper('%" + objDeliveryPlaceModel.SearchBy + "%') or (lower(COUNTRY_NAME) like lower( '%" + objDeliveryPlaceModel.SearchBy + "%')  or upper(COUNTRY_NAME)like upper('%" + objDeliveryPlaceModel.SearchBy + "%')))";
            }


            sql = sql + " ORDER BY sl ";




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
        public DeliveryPlaceModel GetDeliveryPlaceEntryById(DeliveryPlaceModel objDeliveryPlaceModel)
        {
            string sql = "";
            sql = "SELECT " +
                 "TO_CHAR (NVL (COUNTRY_ID,'0'))COUNTRY_ID, " +
                  "TO_CHAR (NVL (COUNTRY_NAME,'N/A'))COUNTRY_NAME, " +
                  "TO_CHAR (NVL (DELIVERY_PLACE_ID,'0'))DELIVERY_PLACE_ID, " +
                  "TO_CHAR (NVL (DELIVERY_PLACE_NAME,'N/A'))DELIVERY_PLACE_NAME " +

                  "FROM VEW_L_DELIVERY_PLACE where head_office_id = '" + objDeliveryPlaceModel.HeadOfficeId + "' AND branch_office_id = '" + objDeliveryPlaceModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objDeliveryPlaceModel.DeliveryPlaceId))
            {
                sql = sql + " and DELIVERY_PLACE_ID = '" + objDeliveryPlaceModel.DeliveryPlaceId + "' ";
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

                        objDeliveryPlaceModel.CountryId = objReader.GetString(0);
                        objDeliveryPlaceModel.CountryName = objReader.GetString(1);
                        objDeliveryPlaceModel.DeliveryPlaceId = objReader.GetString(2);
                        objDeliveryPlaceModel.DeliveryPlaceName = objReader.GetString(3);

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

            return objDeliveryPlaceModel;
        }
        public string SaveDeliveryPlaceEntry(DeliveryPlaceModel objDeliveryPlaceModel)
        {
            string strMsg = "";
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_delivery_place_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (!string.IsNullOrWhiteSpace(objDeliveryPlaceModel.CountryId))
            {
                objOracleCommand.Parameters.Add("P_COUNTRY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDeliveryPlaceModel.CountryId.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_COUNTRY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }
            if (!string.IsNullOrWhiteSpace(objDeliveryPlaceModel.DeliveryPlaceId))
            {
                objOracleCommand.Parameters.Add("P_DELIVERY_PLACE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDeliveryPlaceModel.DeliveryPlaceId.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_DELIVERY_PLACE_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (!string.IsNullOrWhiteSpace(objDeliveryPlaceModel.DeliveryPlaceName))
            {
                objOracleCommand.Parameters.Add("P_DELIVERY_PLACE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDeliveryPlaceModel.DeliveryPlaceName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_DELIVERY_PLACE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDeliveryPlaceModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDeliveryPlaceModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDeliveryPlaceModel.BranchOfficeId;



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


        public DataTable GetCountryDDListForMerchandising()
        {

            DataTable dt = new DataTable();

            string sql = "";

            sql = "SELECT " +
                "COUNTRY_ID, " +
                "COUNTRY_NAME " +
                "FROM VEW_L_COUNTRY ORDER BY COUNTRY_NAME ";

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


        #region IncrementSetup

        public DataTable GetIncrementSetupRecord(IncrementSetupModel objIncrementSetupModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl," +
                  " increment_year, " +
                  "to_char(effect_Date, 'dd/mm/yyyy')effect_Date " +

                  "FROM vew_s_inc_setup_record where head_office_id = '" + objIncrementSetupModel.HeadOfficeId + "' and branch_office_id = '" + objIncrementSetupModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objIncrementSetupModel.SearchBy))
            {
                sql = sql + "and (lower(increment_year) like lower( '%" + objIncrementSetupModel.SearchBy + "%')  or " +
                      "upper(increment_year)like upper('%" + objIncrementSetupModel.SearchBy + "%') )";
            }
            sql = sql + " ORDER BY sl ";

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

        public string SaveIncrementSetupInfo(IncrementSetupModel objIncrementSetupModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_increment_setup_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objIncrementSetupModel.IncrementYear != "")
            {
                objOracleCommand.Parameters.Add("p_increment_year", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objIncrementSetupModel.IncrementYear;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_increment_year", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objIncrementSetupModel.EffectDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_effect_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementSetupModel.EffectDate.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_effect_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementSetupModel.UpdateBy.Trim();
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementSetupModel.HeadOfficeId.Trim();
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objIncrementSetupModel.BranchOfficeId.Trim();

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

        public IncrementSetupModel GetIncrementSetupRecordByYear(IncrementSetupModel objIncrementSetupModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR (NVL (increment_year,'0')), " +
                  " NVL (TO_CHAR (effect_Date,'dd/mm/yyyy'),'')effect_Date " +
                  "FROM vew_s_inc_setup_record where head_office_id = '" + objIncrementSetupModel.HeadOfficeId.Trim() + "' and branch_office_id = '" + objIncrementSetupModel.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objIncrementSetupModel.IncrementYear.Trim()))
            {
                sql = sql + " and increment_year = '" + objIncrementSetupModel.IncrementYear.Trim() + "'   ";
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
                        objIncrementSetupModel.IncrementYear = objReader.GetString(0);
                        objIncrementSetupModel.EffectDate = objReader.GetString(1);
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
            return objIncrementSetupModel;
        }


        #endregion

        #region "Salary Month Day Setup"
        public DataTable GetMonthId(SalaryMonthModel objSalaryMonthModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                "month_id, " +
                "month_name  " +
             

                  "FROM l_month where head_office_id = '" + objSalaryMonthModel.HeadOfficeId.Trim() + "' and branch_office_id = '" + objSalaryMonthModel.BranchOfficeId.Trim() + "'   ";

            
            //sql = sql + " ORDER BY sl ";

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
        public SalaryMonthModel GetMonthlyWorkingDaySetupRecord(SalaryMonthModel objSalaryMonthModel)
        {
            string sql = "";
            sql = "SELECT " +
                "  TO_CHAR (NVL (SALARY_YEAR, '')), " +
                "to_char(from_date, 'dd/mm/yyyy')from_date, " +
                "to_char(to_date, 'dd/mm/yyyy')to_date, " +
                " to_char(nvl(month_id, ''))month_id " +

                  "FROM vew_search_ms_record where head_office_id = '" + objSalaryMonthModel.HeadOfficeId.Trim() + "' and branch_office_id = '" + objSalaryMonthModel.BranchOfficeId.Trim() + "'   ";

            if (!string.IsNullOrEmpty(objSalaryMonthModel.SalaryYear.Trim()))
            {
                sql = sql + " and SALARY_YEAR = '" + objSalaryMonthModel.SalaryYear.Trim() + "'   ";
            }

            if (!string.IsNullOrEmpty(objSalaryMonthModel.MonthId))
            {
                sql = sql + " and month_id = '" + objSalaryMonthModel.MonthId + "'   ";
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
                        objSalaryMonthModel.SalaryYear = objReader.GetString(0);
                        objSalaryMonthModel.FromDate = objReader.GetString(1);
                        objSalaryMonthModel.ToDate = objReader.GetString(2);
                        objSalaryMonthModel.MonthId = objReader.GetString(3);

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
            return objSalaryMonthModel;
        }
        public DataTable GetMonthlyWorkingDayRecord(SalaryMonthModel objSalaryMonthModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                "rownum sl, "+
                " SALARY_YEAR, " +
                " MONTH_NAME, " +
                "to_char(from_date, 'dd/mm/yyyy')from_date, " +
                "to_char(to_date, 'dd/mm/yyyy')to_date, " +
                "month_id "+

                  "FROM vew_search_ms_record where head_office_id = '" + objSalaryMonthModel.HeadOfficeId + "' and branch_office_id = '" + objSalaryMonthModel.BranchOfficeId+ "'   ";

            if (!string.IsNullOrEmpty(objSalaryMonthModel.SalaryYear.Trim()))
            {
                sql = sql + " and SALARY_YEAR = '" + objSalaryMonthModel.SalaryYear + "'   ";
            }

            if (!string.IsNullOrEmpty(objSalaryMonthModel.MonthId))
            {
                sql = sql + " and month_id = '" + objSalaryMonthModel.MonthId + "'   ";
            }

            sql = sql + " ORDER BY sl ";

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
        public string SaveSalaryMonthSetupInfo(SalaryMonthModel objSalaryMonthModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_month_day_setup_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objSalaryMonthModel.SalaryYear != "")
            {
                objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSalaryMonthModel.SalaryYear;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_salary_year", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objSalaryMonthModel.MonthId != "")
            {
                objOracleCommand.Parameters.Add("p_month_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSalaryMonthModel.MonthId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_month_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objSalaryMonthModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryMonthModel.FromDate.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objSalaryMonthModel.ToDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryMonthModel.ToDate.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_to_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryMonthModel.UpdateBy.Trim();
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryMonthModel.HeadOfficeId.Trim();
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSalaryMonthModel.BranchOfficeId.Trim();

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


        #endregion
        public LookUpModel getCurrentDate()
        {



            string sql = "", strMsg;
            sql = "SELECT " +

                  "TO_CHAR(SYSDATE,'dd/mm/yyyy')fromdate, " +
                   "TO_CHAR(SYSDATE,'dd/mm/yyyy')todate " +


                  "FROM dual  ";




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

                        objLookUpModel.FromDate = objDataReader.GetString(0);
                        objLookUpModel.ToDate = objDataReader.GetString(1);

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


            return objLookUpModel;

        }

        public string currentDate()
        {


            string strMsg = "";
            string sql = "";
            sql = "SELECT " +

                  "TO_CHAR(SYSDATE,'dd/mm/yyyy')CurrentDate " +



                  "FROM dual  ";




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

                        strMsg = objDataReader.GetString(0);
                        //objLookUpModel.ToDate = objDataReader.GetString(1);

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


            return strMsg;

        }
        public LookUpModel getFirstLastDay()
        {



            string sql = "";
            sql = "SELECT " +

                  "FIRST_DAY, " +
                  "LAST_DAY " +


                  "FROM vew_first_last_day  ";




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

                        objLookUpModel.FirstDate = objDataReader.GetString(0);
                        objLookUpModel.LastDate = objDataReader.GetString(1);

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


            return objLookUpModel;

        }
        public string getCurrentYear()
        {



            string sql = "", strMsg = "";
            sql = "SELECT " +

                  "TO_CHAR(SYSDATE,'yyyy')YEAR " +



                  "FROM dual  ";




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

                        strMsg = objDataReader.GetString(0);


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


            return strMsg;

        }
        public DataTable GetAccessoriesDDList()
        {
            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "ACCESSORIES_ID, " +
                  "ACCESSORIES_NAME " +
                  "FROM L_ACCESSORIES  order by ACCESSORIES_NAME ";

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

        public LookUpModel GetELLimitDate()
        {
            string sql = "";
            sql = "SELECT " +
                  "to_char(nvl(leave_year, '0')), " +
                  "NVL(TO_CHAR(limit_date,'dd/mm/yyyy'), '')limit_date " +
                  "FROM vew_el_setup  ";

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
                        objLookUpModel.LeaveYear = objDataReader.GetString(0);
                        objLookUpModel.LimitDate = objDataReader.GetString(1);

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

            return objLookUpModel;

        }

        public LookUpModel GetIncrEffectDate()
        {
            string sql = "";
            sql = "SELECT " +
                  "to_char(nvl(increment_year, '0')), " +
                  "NVL(TO_CHAR(effect_date,'dd/mm/yyyy'), '')limit_date " +
                  "FROM VEW_Incr_SETUP  ";

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
                        objLookUpModel.IncrementYear = objDataReader.GetString(0);
                        objLookUpModel.EffectDate = objDataReader.GetString(1);

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

            return objLookUpModel;

        }


        #region Buyer Entry

        // fetch all Buyer Information-----
        public DataTable GetBuyerRecord(BuyerModel objBuyerModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl," +
                  " buyer_id, " +
                  "buyer_name, " +
                  "COUNTRY_ID, " +
                  "country_name, " +
                  "contact_no, " +
                  "email_address, " +
                  "buyer_address, " +
                  "PAYMENT_TYPE_ID, " +
                  "PAYMENT_TYPE_NAME "+

                  "FROM VEW_BUYER_REOCRD where head_office_id = '" + objBuyerModel.HeadOfficeId + "' and branch_office_id = '" + objBuyerModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objBuyerModel.SearchBy))
            {

                sql = sql + "and (lower(buyer_name) like lower( '%" + objBuyerModel.SearchBy + "%')  or upper(buyer_name)like upper('%" + objBuyerModel.SearchBy + "%')  OR" +
                      " lower(buyer_id) like lower( '%" + objBuyerModel.SearchBy + "%')  or upper(buyer_id)like upper('%" + objBuyerModel.SearchBy + "%') )";
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

        // Save Buyer Information
        public string SaveBuyerInfo(BuyerModel objBuyerModel)
        {
            string strMsg = "";
            
            OracleCommand objOracleCommand = new OracleCommand("PRO_BUYER_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objBuyerModel.BuyerId != "")
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBuyerModel.BuyerId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objBuyerModel.BuyerName != "")
            {
                objOracleCommand.Parameters.Add("P_BUYER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerModel.BuyerName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BUYER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objBuyerModel.CountryId != "")
            {
                objOracleCommand.Parameters.Add("P_COUNTRY_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerModel.CountryId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_COUNTRY_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objBuyerModel.BuyerContactNo != "")
            {
                objOracleCommand.Parameters.Add("P_CONTACT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerModel.BuyerContactNo;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_CONTACT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objBuyerModel.BuyerEmail != "")
            {
                objOracleCommand.Parameters.Add("P_EMAIL_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerModel.BuyerEmail;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_EMAIL_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objBuyerModel.BuyerAddress != "")
            {
                objOracleCommand.Parameters.Add("P_BUYER_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerModel.BuyerAddress;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BUYER_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objBuyerModel.BuyerPaymentBy != "")
            {
                objOracleCommand.Parameters.Add("P_PAYMENT_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerModel.BuyerPaymentBy;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_PAYMENT_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBuyerModel.BranchOfficeId;



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

        // Get Buyer Information by Id 
        public BuyerModel GetBuyerById(BuyerModel objBuyerModel)
        {

            string sql = "";
            sql = "SELECT " +

                  "NVL(BUYER_NAME,'N/A'), " +
                  "TO_CHAR (NVL (COUNTRY_ID,'0')), " +
                  "TO_CHAR (NVL (CONTACT_NO,'0')), " +
                  "NVL(EMAIL_ADDRESS,'N/A'), " +
                  "NVL(BUYER_ADDRESS,'N/A'), " +
                  "NVL(PAYMENT_TYPE_NAME,'N/A') " +

                  "FROM L_BUYER WHERE   Head_office_id ='" + objBuyerModel.HeadOfficeId + "'  AND BRANCH_OFFICE_ID = '" + objBuyerModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objBuyerModel.BuyerId))
            {
                sql = sql + " AND BUYER_ID = '" + objBuyerModel.BuyerId + "'  ";
            }

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

                        objBuyerModel.BuyerName = objDataReader.GetString(0);
                        objBuyerModel.CountryId = objDataReader.GetString(1);
                        objBuyerModel.BuyerContactNo = objDataReader.GetString(2);
                        objBuyerModel.BuyerEmail = objDataReader.GetString(3);
                        objBuyerModel.BuyerAddress = objDataReader.GetString(4);
                        objBuyerModel.BuyerPaymentBy = objDataReader.GetString(5);

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
            return objBuyerModel;
        }

        #endregion

        #region Supplier Information

        // fetch all supplier information
        public DataTable GetSupplierRecord(SupplierModel objSupplierModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  " SUPPLIER_ID, " +
                  "rownum sl," +
                  "SUPPLIER_NAME, " +
                  "country_id, " +
                  "country_name, " +
                  "contact_no, " +
                  "email_address, " +
                  "SWIFT_CODE, " +
                  "BANK_NAME, " +
                  "supplier_address " +

                  "FROM vew_supplier where head_office_id = '" + objSupplierModel.HeadOfficeId + "' and branch_office_id = '" + objSupplierModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objSupplierModel.SearchBy))
            {

                sql = sql + "and (lower(SUPPLIER_NAME) like lower( '%" + objSupplierModel.SearchBy + "%')  or upper(SUPPLIER_NAME)like upper('%" + objSupplierModel.SearchBy + "%') )";
            }


            sql = sql + " ORDER BY sl ";

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

        // save supplier information----
        public string SaveSupplierInfo(SupplierModel objSupplierModel)
        {


            string strMsg = "";

            
            OracleCommand objOracleCommand = new OracleCommand("PRO_SUPPLIER_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objSupplierModel.SupplierId != "")
            {
                objOracleCommand.Parameters.Add("P_SUPPLIER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSupplierModel.SupplierId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SUPPLIER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objSupplierModel.SupplierName != "")
            {
                objOracleCommand.Parameters.Add("P_SUPPLIER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupplierModel.SupplierName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SUPPLIER_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objSupplierModel.CountryId != "")
            {
                objOracleCommand.Parameters.Add("P_COUNTRY_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupplierModel.CountryId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_COUNTRY_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objSupplierModel.SupplierContactNo != "")
            {
                objOracleCommand.Parameters.Add("P_CONTACT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupplierModel.SupplierContactNo;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_CONTACT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objSupplierModel.SupplierEmail != "")
            {
                objOracleCommand.Parameters.Add("P_EMAIL_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupplierModel.SupplierEmail;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_EMAIL_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objSupplierModel.SupplierAddress != "")
            {
                objOracleCommand.Parameters.Add("P_SUPPLIER_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupplierModel.SupplierAddress;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SUPPLIER_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objSupplierModel.BankName != "")
            {
                objOracleCommand.Parameters.Add("P_BANK_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupplierModel.BankName;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BANK_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objSupplierModel.SwiftCode != "")
            {
                objOracleCommand.Parameters.Add("P_SWIFT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupplierModel.SwiftCode;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SWIFT_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupplierModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupplierModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSupplierModel.BranchOfficeId;


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

        // fetch supplier information id wise
        public SupplierModel GetSupplierById(SupplierModel objSupplierModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "NVL(SUPPLIER_NAME,'N/A'), " +
                  "TO_CHAR (NVL (COUNTRY_ID,'0')), " +
                  "TO_CHAR (NVL (CONTACT_NO,'0')), " +
                  "NVL(EMAIL_ADDRESS,'N/A'), " +
                  "NVL(SUPPLIER_ADDRESS,'N/A'), " +
                  "NVL(BANK_NAME,'N/A') ," +
                  "NVL(SWIFT_CODE,'N/A') " +

                  "FROM L_SUPPLIER WHERE   Head_office_id ='" + objSupplierModel.HeadOfficeId + "'  AND BRANCH_OFFICE_ID = '" + objSupplierModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objSupplierModel.SupplierId))
            {
                sql = sql + " AND SUPPLIER_ID = '" + objSupplierModel.SupplierId + "'  ";
            }

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
                        objSupplierModel.SupplierName = objDataReader.GetString(0);
                        objSupplierModel.CountryId = objDataReader.GetString(1);
                        objSupplierModel.SupplierContactNo = objDataReader.GetString(2);
                        objSupplierModel.SupplierEmail = objDataReader.GetString(3);
                        objSupplierModel.SupplierAddress = objDataReader.GetString(4);
                        objSupplierModel.BankName = objDataReader.GetString(5);
                        objSupplierModel.SwiftCode = objDataReader.GetString(6);
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
            return objSupplierModel;
        }

        #endregion

        #region Mode of Payment 

        // Fetch all payment mode
        public DataTable GetModeofPaymentRecord(ModeofPaymentModel objPaymentModeModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  " MODE_OF_PAYMENT_ID, " +
                  "rownum sl," +
                  "MODE_OF_PAYMENT_NAME " +

                  "FROM view_L_MODE_OF_PAYMENT where head_office_id = '" + objPaymentModeModel.HeadOfficeId + "' and branch_office_id = '" + objPaymentModeModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objPaymentModeModel.SearchBy))
            {

                sql = sql + "and (lower(MODE_OF_PAYMENT_NAME) like lower( '%" + objPaymentModeModel.SearchBy + "%')  or upper(MODE_OF_PAYMENT_NAME)like upper('%" + objPaymentModeModel.SearchBy + "%') )";
            }


            sql = sql + " ORDER BY sl ";

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

        // Save mode of payment
        public string SaveModeOfPaymentInfo(ModeofPaymentModel objModeofPaymentModel)
        {
            string strMsg = "";

            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_mode_of_payment_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objModeofPaymentModel.ModeofPaymentId != "")
            {
                objOracleCommand.Parameters.Add("p_mode_of_payment_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objModeofPaymentModel.ModeofPaymentId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_mode_of_payment_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objModeofPaymentModel.ModeofPaymentName != "")
            {
                objOracleCommand.Parameters.Add("p_mode_of_payment_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objModeofPaymentModel.ModeofPaymentName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_mode_of_payment_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModeofPaymentModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModeofPaymentModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModeofPaymentModel.BranchOfficeId;



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

        // fetch mode of payment by Id
        public ModeofPaymentModel GetModeofPaymentById(ModeofPaymentModel objMode)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR(NVL(MODE_OF_PAYMENT_ID,'0')), " +
                  " TO_CHAR(NVL(MODE_OF_PAYMENT_NAME,'N/A')) " +

                  "FROM L_MODE_OF_PAYMENT WHERE   Head_office_id ='" + objMode.HeadOfficeId + "'  AND BRANCH_OFFICE_ID = '" + objMode.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objMode.ModeofPaymentId))
            {
                sql = sql + " AND MODE_OF_PAYMENT_ID = '" + objMode.ModeofPaymentId + "'  ";
            }

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
                        objMode.ModeofPaymentId = objDataReader.GetString(0);
                        objMode.ModeofPaymentName = objDataReader.GetString(1);
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
            return objMode;
        }

        #endregion

        #region Item Entry

        //fetch all item----
        public DataTable GetItemRecord(ItemModel objItemModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  " ITEM_ID, " +
                  " ROWNUM sl, " +
                  "ITEM_NAME " +

                  "FROM VIEW_L_ITEM where head_office_id = '" + objItemModel.HeadOfficeId + "' and branch_office_id = '" + objItemModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objItemModel.SearchBy))
            {

                sql = sql + "and (lower(ITEM_NAME) like lower( '%" + objItemModel.SearchBy + "%')  or upper(ITEM_NAME)like upper('%" + objItemModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";


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

        // save item information----

        public string SaveItemInfo(ItemModel objModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_item_save_com");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objModel.ItemId != "")
            {
                objOracleCommand.Parameters.Add("p_item_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objModel.ItemId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_item_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objModel.ItemName != "")
            {
                objOracleCommand.Parameters.Add("p_item_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objModel.ItemName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_item_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.BranchOfficeId;

            objOracleCommand.Parameters.Add("P_MESSAGE", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output; // catch output message from procedure

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

        // fetch item information id wise

        public ItemModel GetItemById(ItemModel objMode)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR(NVL(ITEM_ID,'0')), " +
                  " TO_CHAR(NVL(ITEM_NAME,'N/A')) " +

                  "FROM L_ITEM WHERE   Head_office_id ='" + objMode.HeadOfficeId + "'  AND BRANCH_OFFICE_ID = '" + objMode.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objMode.ItemId))
            {
                sql = sql + " AND ITEM_ID = '" + objMode.ItemId + "'  ";
            }

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
                        objMode.ItemId = objDataReader.GetString(0);
                        objMode.ItemName = objDataReader.GetString(1);
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
            return objMode;
        }

        #endregion

        #region Payment Mode

        public DataTable GetPaymentModeRecord(PaymentMode objPaymentMode)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                " PAYMENT_MODE_ID, " +
                " rownum sl, " +
                "PAYMENT_MODE_NAME " +

                  "FROM VIEW_L_PAYMENT_MODE where head_office_id = '" + objPaymentMode.HeadOfficeId + "' and branch_office_id = '" + objPaymentMode.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objPaymentMode.SearchBy))
            {

                sql = sql + "and (lower(PAYMENT_MODE_NAME) like lower( '%" + objPaymentMode.SearchBy + "%')  or upper(PAYMENT_MODE_NAME)like upper('%" + objPaymentMode.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        // save payment mode---- 
        public string SavePaymentModeInfo(PaymentMode objPaymentMode)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_payment_mode_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objPaymentMode.PaymentModeId != "")
            {
                objOracleCommand.Parameters.Add("p_payment_mode_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPaymentMode.PaymentModeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_payment_mode_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objPaymentMode.PaymentModeName != "")
            {
                objOracleCommand.Parameters.Add("p_payment_mode_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objPaymentMode.PaymentModeName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_payment_mode_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPaymentMode.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPaymentMode.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objPaymentMode.BranchOfficeId;



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

        //fetch payment mode id wise
        public PaymentMode GetPaymentModeById(PaymentMode objMode)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR(NVL(PAYMENT_MODE_ID,'0')), " +
                  " TO_CHAR(NVL(PAYMENT_MODE_NAME,'N/A')) " +

                  "FROM L_PAYMENT_MODE WHERE   Head_office_id ='" + objMode.HeadOfficeId + "'  AND BRANCH_OFFICE_ID = '" + objMode.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objMode.PaymentModeId))
            {
                sql = sql + " AND PAYMENT_MODE_ID = '" + objMode.PaymentModeId + "'  ";
            }

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
                        objMode.PaymentModeId = objDataReader.GetString(0);
                        objMode.PaymentModeName = objDataReader.GetString(1);
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
            return objMode;
        }

        #endregion

        #region Negotiation Term

        //fetch all negotiation term---
        public DataTable GetNegotiationRecord(NegotiationTermModel objTermModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  " NEGOTIATION_TERM_ID, " +
                  " rownum sl, " +
                  "NEGOTIATION_TERM_NAME " +

                  "FROM VIEW_L_NEGOTIATION_TERM where head_office_id = '" + objTermModel.HeadOfficeId + "' and branch_office_id = '" + objTermModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objTermModel.SearchBy))
            {

                sql = sql + "and (lower(NEGOTIATION_TERM_NAME) like lower( '%" + objTermModel.SearchBy + "%')  or upper(NEGOTIATION_TERM_NAME)like upper('%" + objTermModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";


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

        //save negotiation 
        public string SaveNegotiationTermInfo(NegotiationTermModel objModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_negotiation_term_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objModel.NegotitaionId != "")
            {
                objOracleCommand.Parameters.Add("P_NEGOTIATION_TERM_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objModel.NegotitaionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_NEGOTIATION_TERM_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objModel.NegotiationName != "")
            {
                objOracleCommand.Parameters.Add("P_NEGOTIATION_TERM_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objModel.NegotiationName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_NEGOTIATION_TERM_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }



            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.BranchOfficeId;



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

        //fectch negotiation term id wise
        public NegotiationTermModel GetNegotiationTermById(NegotiationTermModel objModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR(NVL(NEGOTIATION_TERM_ID,'0')), " +
                  " TO_CHAR(NVL(NEGOTIATION_TERM_NAME,'N/A')) " +

                  "FROM L_NEGOTIATION_TERM where Head_office_id ='" + objModel.HeadOfficeId + "'  AND BRANCH_OFFICE_ID = '" + objModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objModel.NegotitaionId))
            {
                sql = sql + " AND NEGOTIATION_TERM_ID = '" + objModel.NegotitaionId + "'  ";
            }

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
                        objModel.NegotitaionId = objDataReader.GetString(0);
                        objModel.NegotiationName = objDataReader.GetString(1);
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
            return objModel;
        }

        #endregion

        #region Buyer Bank Information

        public DataTable GetBuyerBankRecord(BuyerBankModel objBankModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  " BANK_ID, " +
                  " rownum sl, " +
                  "BANK_NAME, " +
                  "BUYER_NAME, " +
                  "SWIFT_NO, " +
                  "BANK_ADDRESS " +

                  "FROM vew_buyer_bank_info where head_office_id = '" + objBankModel.HeadOfficeId + "' and branch_office_id = '" + objBankModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objBankModel.SearchBy))
            {

                sql = sql + "and (lower(BANK_NAME) like lower( '%" + objBankModel.SearchBy + "%')  or upper(BANK_NAME)like upper('%" + objBankModel.SearchBy + "%') )";
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

        //save buyer bank information
        public string SaveBuyerBankInfo(BuyerBankModel objBankModel)
        {

            string strMsg = "";
            
            OracleCommand objOracleCommand = new OracleCommand("PRO_BUYER_BANK_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objBankModel.BuyerBankId != "")
            {
                objOracleCommand.Parameters.Add("P_BANK_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBankModel.BuyerBankId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BANK_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objBankModel.BuyerId != "")
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBankModel.BuyerId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BUYER_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objBankModel.BuyerBankName != "")
            {
                objOracleCommand.Parameters.Add("P_BANK_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBankModel.BuyerBankName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BANK_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objBankModel.SwiftNo != "")
            {
                objOracleCommand.Parameters.Add("P_SWIFT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBankModel.SwiftNo.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SWIFT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objBankModel.BankAddress != "")
            {
                objOracleCommand.Parameters.Add("P_BANK_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBankModel.BankAddress.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BANK_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBankModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBankModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBankModel.BranchOfficeId;

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

        //fetch buher bank information  id wise
        public BuyerBankModel GetBuyerBankById(BuyerBankModel objBankModel)
        {

            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR(NVL(BANK_ID,'0')), " +
                  "NVL(BANK_NAME,'N/A'), " +
                  "NVL(SWIFT_NO,'N/A'), " +
                  "TO_CHAR (NVL (BUYER_ID,'0')), " +
                  "NVL(BANK_ADDRESS,'N/A') " +

                  "FROM BUYER_BANK_INFO WHERE   Head_office_id ='" + objBankModel.HeadOfficeId + "'  AND BRANCH_OFFICE_ID = '" + objBankModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objBankModel.BuyerBankId))
            {
                sql = sql + " AND BANK_ID = '" + objBankModel.BuyerBankId + "'  ";
            }

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

                        objBankModel.BuyerBankId = objDataReader.GetString(0);
                        objBankModel.BuyerBankName = objDataReader.GetString(1);
                        objBankModel.SwiftNo = objDataReader.GetString(2);
                        objBankModel.BuyerId = objDataReader.GetString(3);
                        objBankModel.BankAddress = objDataReader.GetString(4);


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
            return objBankModel;
        }

        #endregion

        #region Seller/Notifier Bank

        public DataTable GetSellerBankRecord(SellerBankModel objBank)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  " BANK_ID, " +
                  " rownum sl, " +
                  "BANK_NAME, " +
                  "branch_office_name, " +
                  "SWIFT_NO, " +
                  "BANK_ADDRESS " +

                  "FROM vew_seller_bank_info where head_office_id = '" + objBank.HeadOfficeId + "' and branch_office_id = '" + objBank.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objBank.SearchBy))
            {

                sql = sql + "and (lower(BANK_NAME) like lower( '%" + objBank.SearchBy + "%')  or upper(BANK_NAME)like upper('%" + objBank.SearchBy + "%') )";
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

        public string SaveSellerBankInfo(SellerBankModel objBank)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("PRO_SELLER_BANK_SAVE");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objBank.SellerBankId != "")
            {
                objOracleCommand.Parameters.Add("P_BANK_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBank.SellerBankId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BANK_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objBank.BankName != "")
            {
                objOracleCommand.Parameters.Add("P_BANK_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBank.BankName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BANK_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objBank.SwiftNo != "")
            {
                objOracleCommand.Parameters.Add("P_SWIFT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBank.SwiftNo.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SWIFT_NO", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objBank.BankAddress != "")
            {
                objOracleCommand.Parameters.Add("P_BANK_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBank.BankAddress;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_BANK_ADDRESS", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objBank.FactroyId != "")
            {
                objOracleCommand.Parameters.Add("FACTORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objBank.FactroyId;
            }
            else
            {
                objOracleCommand.Parameters.Add("FACTORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBank.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBank.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objBank.BranchOfficeId;


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

        public SellerBankModel GetSellerBankById(SellerBankModel objBank)
        {

            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR(NVL(BANK_ID,'0')), " +
                  "NVL(BANK_NAME,'N/A'), " +
                  "NVL(SWIFT_NO,'N/A'), " +
                  "TO_CHAR (NVL (FACTORY_ID,'0')), " +
                  "NVL(BANK_ADDRESS,'N/A') " +

                  "FROM SELLER_BANK_INFO WHERE   Head_office_id ='" + objBank.HeadOfficeId + "'  AND BRANCH_OFFICE_ID = '" + objBank.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objBank.SellerBankId))
            {
                sql = sql + " AND BANK_ID = '" + objBank.SellerBankId + "'  ";
            }

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
                        objBank.SellerBankId = objDataReader.GetString(0);
                        objBank.BankName = objDataReader.GetString(1);
                        objBank.SwiftNo = objDataReader.GetString(2);
                        objBank.FactroyId = objDataReader.GetString(3);
                        objBank.BankAddress = objDataReader.GetString(4);
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
            return objBank;
        }

        #endregion

        #region Season

        public DataTable GetSeasonRecord(SeasonModel objSeasonModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  " SEASON_ID, " +
                  " rownum sl, " +
                  "SEASON_NAME " +

                  "FROM VIEW_L_SEASON where head_office_id = '" + objSeasonModel.HeadOfficeId + "' and branch_office_id = '" + objSeasonModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objSeasonModel.SearchBy))
            {
                sql = sql + "and (lower(SEASON_NAME) like lower( '%" + objSeasonModel.SearchBy + "%')  or upper(SEASON_NAME)like upper('%" + objSeasonModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public string SaveSeasonInfo(SeasonModel objModel)
        {
            string strMsg = "";
            
            OracleCommand objOracleCommand = new OracleCommand("pro_season_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objModel.SeasonId != "")
            {
                objOracleCommand.Parameters.Add("p_season_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objModel.SeasonId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_season_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objModel.SeasonName != "")
            {
                objOracleCommand.Parameters.Add("p_season_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objModel.SeasonName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_season_name", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.BranchOfficeId;

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

        public SeasonModel GetSeasonById(SeasonModel objModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " TO_CHAR(NVL(SEASON_ID,'0')), " +
                  " TO_CHAR(NVL(SEASON_NAME,'N/A')) " +

                  "FROM L_SEASON where Head_office_id ='" + objModel.HeadOfficeId + "'  AND BRANCH_OFFICE_ID = '" + objModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objModel.SeasonId))
            {
                sql = sql + " AND SEASON_ID = '" + objModel.SeasonId + "'  ";
            }

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
                        objModel.SeasonId = objDataReader.GetString(0);
                        objModel.SeasonName = objDataReader.GetString(1);
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
            return objModel;
        }

        #endregion

        #region Import Origin

        public DataTable GetImportOriginRecord(ImportOriginModel objOriginModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  " IMPORT_ORIGIN_ID, " +
                  " rownum sl, " +
                  "IMPORT_ORIGIN_NAME " +

                  "FROM VIEW_L_IMPORT_ORIGIN where head_office_id = '" + objOriginModel.HeadOfficeId + "' and branch_office_id = '" + objOriginModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objOriginModel.SearchBy))
            {

                sql = sql + "and (lower(IMPORT_ORIGIN_NAME) like lower( '%" + objOriginModel.SearchBy + "%')  or upper(IMPORT_ORIGIN_NAME)like upper('%" + objOriginModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        // save 
        public string SaveImportOriginInfo(ImportOriginModel objImportOriginModel)
        {
            string strMsg = "";
            
            OracleCommand objOracleCommand = new OracleCommand("pro_import_origin_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objImportOriginModel.ImportOriginId != "")
            {
                objOracleCommand.Parameters.Add("p_import_origin_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objImportOriginModel.ImportOriginId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_import_origin_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objImportOriginModel.ImportOriginName != "")
            {
                objOracleCommand.Parameters.Add("p_import_origin_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objImportOriginModel.ImportOriginName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_import_origin_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }
            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objImportOriginModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objImportOriginModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objImportOriginModel.BranchOfficeId;

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

        //fetch Import Origin data id wise
        public ImportOriginModel GetImportOriginById(ImportOriginModel objModel)
        {
            string sql = "";
            sql = "SELECT " +
                  " IMPORT_ORIGIN_ID, " +
                  "IMPORT_ORIGIN_NAME " +

                  "FROM L_IMPORT_ORIGIN WHERE   Head_office_id ='" + objModel.HeadOfficeId + "'  AND BRANCH_OFFICE_ID = '" + objModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objModel.ImportOriginId))
            {
                sql = sql + " AND IMPORT_ORIGIN_ID = '" + objModel.ImportOriginId + "'  ";
            }

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
                        objModel.ImportOriginId = objDataReader.GetString(0);
                        objModel.ImportOriginName = objDataReader.GetString(1);
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
            return objModel;
        }

        #endregion

        #region Size Entry

        public DataTable GetCpSizeRecord(SizeModel objSizeModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  " SIZE_ID, " +
                  " rownum sl, " +
                  "SIZE_NAME, " +
                  "SIZE_VALUE " +

                  "FROM VIEW_L_SIZE where head_office_id = '" + objSizeModel.HeadOfficeId + "' and branch_office_id = '" + objSizeModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objSizeModel.SearchBy))
            {

                sql = sql + "and (lower(SIZE_NAME) like lower( '%" + objSizeModel.SearchBy + "%')  or " +
                      "upper(SIZE_NAME)like upper('%" + objSizeModel.SearchBy + "%') or lower(SIZE_VALUE) like lower( '%" + objSizeModel.SearchBy + "%')  or" +
                      " upper(SIZE_VALUE)like upper('%" + objSizeModel.SearchBy + "%') )";
            }


            sql = sql + " ORDER BY sl ";


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

        public string SaveCpSizeInfo(SizeModel objSizeModel)
        {
            string strMsg = "";
            
            OracleCommand objOracleCommand = new OracleCommand("pro_cp_size_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objSizeModel.SizeId != "")
            {
                objOracleCommand.Parameters.Add("p_size_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSizeModel.SizeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_size_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objSizeModel.SizeName != "")
            {
                objOracleCommand.Parameters.Add("p_size_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSizeModel.SizeName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_size_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objSizeModel.SizeValue != "")
            {
                objOracleCommand.Parameters.Add("p_size_value", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSizeModel.SizeValue;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_size_value", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSizeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSizeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSizeModel.BranchOfficeId;



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

        public SizeModel GetCpSizeById(SizeModel objModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "SIZE_ID," +
                  "SIZE_NAME, " +
                  "SIZE_VALUE " +

                  "FROM L_SIZE WHERE   Head_office_id ='" + objModel.HeadOfficeId + "' " +
                  " AND BRANCH_OFFICE_ID = '" + objModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objModel.SizeId))
            {
                sql = sql + " AND SIZE_ID = '" + objModel.SizeId + "'  ";
            }

            OracleCommand objCommand = new OracleCommand(sql);

            using (OracleConnection strConn = GetConnection())
            {
                objCommand.Connection = strConn;
                strConn.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objModel.SizeId = objDataReader["SIZE_ID"].ToString();
                        objModel.SizeName = objDataReader["SIZE_NAME"].ToString();
                        objModel.SizeValue = objDataReader["SIZE_VALUE"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    objDataReader.Close();
                    strConn.Close();
                }
            }
            return objModel;
        }

        #endregion

        #region Category Entry

        public DataTable GetProductCategoryRecord(CategoryModel objCategoryModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "rownum sl," +
                  "CATEGORY_ID, " +
                  "CATEGORY_NAME " +

                  "FROM VIEW_CATEGORY where head_office_id = '" + objCategoryModel.HeadOfficeId + "' and branch_office_id = '" + objCategoryModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objCategoryModel.SearchBy))
            {

                sql = sql + "and (lower(CATEGORY_NAME) like lower( '%" + objCategoryModel.SearchBy + "%')  or upper(CATEGORY_NAME)like upper('%" + objCategoryModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public string SaveProductCategoryInfo(CategoryModel objCategoryModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_category_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objCategoryModel.CategoryId != "")
            {
                objOracleCommand.Parameters.Add("P_CATEGORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objCategoryModel.CategoryId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_CATEGORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objCategoryModel.CategoryName != "")
            {
                objOracleCommand.Parameters.Add("P_CATEGORY_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCategoryModel.CategoryName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_CATEGORY_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCategoryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCategoryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objCategoryModel.BranchOfficeId;



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

        public CategoryModel GetProductCategoryById(CategoryModel objModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "CATEGORY_ID," +
                  "CATEGORY_NAME " +

                  "FROM L_CATEGORY WHERE   Head_office_id ='" + objModel.HeadOfficeId + "' " +
                  " AND BRANCH_OFFICE_ID = '" + objModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objModel.CategoryId))
            {
                sql = sql + " AND CATEGORY_ID = '" + objModel.CategoryId + "'  ";
            }

            OracleCommand objCommand = new OracleCommand(sql);

            using (OracleConnection strConn = GetConnection())
            {
                objCommand.Connection = strConn;
                strConn.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objModel.CategoryId = objDataReader["CATEGORY_ID"].ToString();
                        objModel.CategoryName = objDataReader["CATEGORY_NAME"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    objDataReader.Close();
                    strConn.Close();
                }
            }
            return objModel;
        }

        #endregion
        #region Occasion Entry

        public DataTable GetOccasionRecord(OccasionModel objOccasionModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                   "rownum sl," +
                  "OCCASION_ID, " +
                  "OCCASION_NAME " +

                  "FROM vew_occasion where head_office_id = '" + objOccasionModel.HeadOfficeId + "' and branch_office_id = '" + objOccasionModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objOccasionModel.SearchBy))
            {

                sql = sql + "and (lower(OCCASION_NAME) like lower( '%" + objOccasionModel.SearchBy + "%')  or upper(OCCASION_NAME)like upper('%" + objOccasionModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public string SaveOccasionInfo(OccasionModel objOccasionModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_ocassion_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objOccasionModel.OccasionId != "")
            {
                objOracleCommand.Parameters.Add("P_OCCASION_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOccasionModel.OccasionId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_OCCASION_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objOccasionModel.OccasionName != "")
            {
                objOracleCommand.Parameters.Add("P_OCCASION_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOccasionModel.OccasionName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_OCCASION_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOccasionModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOccasionModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOccasionModel.BranchOfficeId;



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

        public OccasionModel GetOccasionRecordById(OccasionModel objOccasionModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "OCCASION_ID," +
                  "OCCASION_NAME " +

                  "FROM vew_occasion WHERE   Head_office_id ='" + objOccasionModel.HeadOfficeId + "' " +
                  " AND BRANCH_OFFICE_ID = '" + objOccasionModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objOccasionModel.OccasionId))
            {
                sql = sql + " AND OCCASION_ID = '" + objOccasionModel.OccasionId + "'  ";
            }

            OracleCommand objCommand = new OracleCommand(sql);

            using (OracleConnection strConn = GetConnection())
            {
                objCommand.Connection = strConn;
                strConn.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objOccasionModel.OccasionId = objDataReader["OCCASION_ID"].ToString();
                        objOccasionModel.OccasionName = objDataReader["OCCASION_NAME"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    objDataReader.Close();
                    strConn.Close();
                }
            }
            return objOccasionModel;
        }

        #endregion

        #region Fit Entry

        public DataTable GetFitRecord(FitEntryModel objFitEntryModel)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                "rownum sl," +
                  "FIT_ID, " +
                  "FIT_NAME " +

                  "FROM VIEW_L_FIT where head_office_id = '" + objFitEntryModel.HeadOfficeId + "' and branch_office_id = '" + objFitEntryModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objFitEntryModel.SearchBy))
            {

                sql = sql + "and (lower(FIT_NAME) like lower( '%" + objFitEntryModel.SearchBy + "%')  " +
                      "or upper(FIT_NAME)like upper('%" + objFitEntryModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY SL ";

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

        public string SaveFitInfo(FitEntryModel objFitEntryModel)
        {
            string strMsg = "";
            OracleCommand objOracleCommand = new OracleCommand("pro_fit_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objFitEntryModel.FitId != "")
            {
                objOracleCommand.Parameters.Add("p_fit_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objFitEntryModel.FitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_fit_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objFitEntryModel.FitName != "")
            {
                objOracleCommand.Parameters.Add("p_fit_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFitEntryModel.FitName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_fit_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFitEntryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFitEntryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFitEntryModel.BranchOfficeId;

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

        public FitEntryModel GetFitById(FitEntryModel objModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "FIT_ID," +
                  "FIT_NAME " +

                  "FROM L_FIT WHERE   Head_office_id ='" + objModel.HeadOfficeId + "' " +
                  " AND BRANCH_OFFICE_ID = '" + objModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objModel.FitId))
            {
                sql = sql + " AND FIT_ID = '" + objModel.FitId + "'  ";
            }

            OracleCommand objCommand = new OracleCommand(sql);

            using (OracleConnection strConn = GetConnection())
            {
                objCommand.Connection = strConn;
                strConn.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objModel.FitId = objDataReader["FIT_ID"].ToString();
                        objModel.FitName = objDataReader["FIT_NAME"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    objDataReader.Close();
                    strConn.Close();
                }
            }
            return objModel;
        }


        #endregion

        #region Sub Category

        public DataTable GetProductSubCategoryRecord(SubCategoryModel objSubCategoryModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum sl, " +
                  "SUB_CATEGORY_ID, " +
                  "CATEGORY_NAME, " +
                  "SUB_CATEGORY_NAME " +

                  "FROM vew_sub_category where head_office_id = '" + objSubCategoryModel.HeadOfficeId + "' and branch_office_id = '" + objSubCategoryModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objSubCategoryModel.SearchBy))
            {

                sql = sql + "and (lower(SUB_CATEGORY_NAME) like lower( '%" + objSubCategoryModel.SearchBy + "%')  or " +
                      "upper(SUB_CATEGORY_NAME)like upper('%" + objSubCategoryModel.SearchBy + "%') OR " +
                      "lower(CATEGORY_NAME) like lower( '%" + objSubCategoryModel.SearchBy + "%')  or " +
                      "upper(CATEGORY_NAME)like upper('%" + objSubCategoryModel.SearchBy + "%') )";
            }

            sql = sql + " ORDER BY sl ";

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

        public string SaveProductSubCategoryInfo(SubCategoryModel objSubCategoryModel)
        {
            string strMsg = "";
            
            OracleCommand objOracleCommand = new OracleCommand("pro_sub_category_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objSubCategoryModel.SubCategoryId != "")
            {
                objOracleCommand.Parameters.Add("P_SUB_CATEGORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSubCategoryModel.SubCategoryId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SUB_CATEGORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objSubCategoryModel.CategoryId != "")
            {
                objOracleCommand.Parameters.Add("P_CATEGORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objSubCategoryModel.CategoryId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_CATEGORY_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objSubCategoryModel.SubCategoryName != "")
            {
                objOracleCommand.Parameters.Add("P_SUB_CATEGORY_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSubCategoryModel.SubCategoryName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SUB_CATEGORY_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSubCategoryModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSubCategoryModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objSubCategoryModel.BranchOfficeId;

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

        public SubCategoryModel GetSubCategoryById(SubCategoryModel objModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "SUB_CATEGORY_ID," +
                  "CATEGORY_ID," +
                  "SUB_CATEGORY_NAME " +

                  "FROM l_sub_category WHERE   Head_office_id ='" + objModel.HeadOfficeId + "' " +
                  " AND BRANCH_OFFICE_ID = '" + objModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objModel.SubCategoryId))
            {
                sql = sql + " AND SUB_CATEGORY_ID = '" + objModel.SubCategoryId + "'  ";
            }

            OracleCommand objCommand = new OracleCommand(sql);

            using (OracleConnection strConn = GetConnection())
            {
                objCommand.Connection = strConn;
                strConn.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objModel.SubCategoryId = objDataReader["SUB_CATEGORY_ID"].ToString();
                        objModel.SubCategoryName = objDataReader["SUB_CATEGORY_NAME"].ToString();
                        objModel.CategoryId = objDataReader["CATEGORY_ID"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    objDataReader.Close();
                    strConn.Close();
                }
            }
            return objModel;
        }

        #endregion

        #region Fabric 

        public DataTable GetFabricTypeRecord(FabricTypeModel objModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  " rownum sl, " +
                  " FABRIC_TYPE_ID, " +
                  "FABRIC_TYPE_NAME " +

                  "FROM view_l_fabric_type where head_office_id = '" + objModel.HeadOfficeId + "' and branch_office_id = '" + objModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objModel.SearchBy))
            {

                sql = sql + "and (lower(FABRIC_TYPE_NAME) like lower( '%" + objModel.SearchBy + "%')  or " +
                      "upper(FABRIC_TYPE_NAME)like upper('%" + objModel.SearchBy + "%') )";
            }
            sql = sql + " ORDER BY sl ";

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

        //Save fabric---
        public string SaveFabrciTypeInfo(FabricTypeModel objModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_fabric_type_save_cp");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objModel.FabricTypeId != "")
            {
                objOracleCommand.Parameters.Add("p_fabric_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objModel.FabricTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_fabric_type_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objModel.FabricTypeName != "")
            {
                objOracleCommand.Parameters.Add("p_fabric_type_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.FabricTypeName.Trim();
            }
            else
            {
                objOracleCommand.Parameters.Add("p_fabric_type_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objModel.BranchOfficeId;

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


        //fetch fabric type id wise
        public FabricTypeModel GetFabricById(FabricTypeModel objModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "FABRIC_TYPE_ID," +
                  "FABRIC_TYPE_NAME " +

                  "FROM L_FABRIC_TYPE WHERE   Head_office_id ='" + objModel.HeadOfficeId + "' " +
                  " AND BRANCH_OFFICE_ID = '" + objModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objModel.FabricTypeId))
            {
                sql = sql + " AND FABRIC_TYPE_ID = '" + objModel.FabricTypeId + "'  ";
            }

            OracleCommand objCommand = new OracleCommand(sql);

            using (OracleConnection strConn = GetConnection())
            {
                objCommand.Connection = strConn;
                strConn.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objModel.FabricTypeId = objDataReader["FABRIC_TYPE_ID"].ToString();
                        objModel.FabricTypeName = objDataReader["FABRIC_TYPE_NAME"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }

                finally
                {
                    objDataReader.Close();
                    strConn.Close();
                }
            }
            return objModel;
        }

        #endregion


        #region Employee Entry 
        public DataTable GetCountryDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "COUNTRY_ID, " +
                  "COUNTRY_NAME " +
                  "FROM L_COUNTRY ORDER BY COUNTRY_NAME ";

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
        public DataTable GetDivisionDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "DIVISION_ID, " +
                  "DIVISION_NAME " +
                  "FROM L_DIVISION ORDER BY DIVISION_NAME ";

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
        public DataTable GetDistrictDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "DISTRICT_ID, " +
                  "DISTRICT_NAME " +
                  "FROM L_DISTRICT ORDER BY DISTRICT_NAME ";

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
        public DataTable GetGenderDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "GENDER_ID, " +
                  "GENDER_NAME " +
                  "FROM L_GENDER ORDER BY GENDER_NAME ";

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
        public DataTable GetBloodGroupDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "BLOOD_GROUP_ID, " +
                  "BLOOD_GROUP_NAME " +
                  "FROM L_BLOOD_GROUP ORDER BY BLOOD_GROUP_NAME ";

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
        public DataTable GetMaritalStatusDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "MARITAL_STATUS_ID, " +
                  "MARITAL_STATUS_NAME " +
                  "FROM L_MARITAL_STATUS ORDER BY MARITAL_STATUS_NAME ";

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
        public DataTable GetReligionDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "RELIGION_ID, " +
                  "RELIGION_NAME " +
                  "FROM L_RELIGION ORDER BY RELIGION_NAME ";

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
        public DataTable GetEmployeementTypeDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "OCCURENCE_TYPE_ID, " +
                  "OCCURENCE_TYPE_NAME " +
                  "FROM L_OCCURENCE_TYPE ORDER BY OCCURENCE_TYPE_NAME ";

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
        public DataTable GetEmployeeTypeDDList(string strHeadOfficeId, string strBranchOfficeId)
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "EMPLOYEE_TYPE_ID, " +
                  "EMPLOYEE_TYPE_NAME " +
                  "FROM L_EMPLOYEE_TYPE where head_office_id = '"+ strHeadOfficeId + "' and branch_office_id = '"+strBranchOfficeId+"' ORDER BY EMPLOYEE_TYPE_NAME ";

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
        public DataTable GetEmployeeDesignationDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "DESIGNATION_ID, " +
                  "DESIGNATION_NAME " +
                  "FROM L_DESIGNATION ORDER BY DESIGNATION_NAME";

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

        public DataTable GetDesignationDDList(string strHeadOfficeId, string strBranchOfficeId)
        {
            DataTable objDataTable = new DataTable();

            string sql = "SELECT " +
                         "DESIGNATION_ID, " +
                         "DESIGNATION_NAME " +
                         "FROM L_DESIGNATION ORDER BY DESIGNATION_NAME";

            OracleCommand objCommand = new OracleCommand(sql);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);

            using (OracleConnection strConn = GetConnection())
            {
                try
                {
                    objCommand.Connection = strConn;
                    strConn.Open();
                    objDataAdapter.Fill(objDataTable);
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

            return objDataTable;
        }

        public DataTable GetUnitDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "UNIT_ID, " +
                  "UNIT_NAME " +
                  "FROM L_UNIT ORDER BY UNIT_NAME ";

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
        public DataTable GetSectionDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "SECTION_ID, " +
                  "SECTION_NAME " +
                  "FROM L_SECTION ORDER BY SECTION_NAME ";

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
        public DataTable GetGradeDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "GRADE_ID, " +
                  "GRADE_NO " +
                  "FROM L_GRADE ORDER BY GRADE_NO ";

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
        public DataTable GetProbationPeriodDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "PROBATION_PERIOD_ID, " +
                  "PROBATION_PERIOD " +
                  "FROM L_PROBATION_PERIOD ORDER BY PROBATION_PERIOD ";

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
        public DataTable GetSupervisorDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "SUPERVISOR_EMPLOYEE_ID, " +
                  "SUPERVISOR_EMPLOYEE_NAME " +
                  "FROM VEW_GET_DEPARTMENT_ID ORDER BY SUPERVISOR_EMPLOYEE_NAME ";

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
        public DataTable GetJobTypeDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "JOB_TYPE_ID, " +
                  "JOB_TYPE_NAME " +
                  "FROM L_JOB_TYPE ORDER BY JOB_TYPE_NAME ";

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
        public DataTable GetPaymentTypeDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "PAYMENT_TYPE_ID, " +
                  "PAYMENT_TYPE_NAME " +
                  "FROM L_PAYMENT_TYPE ORDER BY PAYMENT_TYPE_NAME ";

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
        public DataTable GetDepartmentDDList(string strHeadOfficeId, string strBranchOfficeId)
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "DEPARTMENT_ID, " +
                  "DEPARTMENT_NAME " +
                  "FROM L_DEPARTMENT where head_office_id = '"+ strHeadOfficeId + "' and branch_office_id = '"+strBranchOfficeId+"' ORDER BY DEPARTMENT_NAME ";

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
        public DataTable GetSubSectionDDList(string strHeadOfficeId, string strBranchOfficeId)
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "SUB_SECTION_ID, " +
                  "SUB_SECTION_NAME " +
                  "FROM L_SUB_SECTION where head_office_id = '"+strHeadOfficeId+"' and branch_office_id = '"+strBranchOfficeId+"' ORDER BY SUB_SECTION_NAME ";

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
        public DataTable GetShiftDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "SHIFT_ID, " +
                  "SHIFT_NAME " +
                  "FROM L_SHIFT ORDER BY SHIFT_NAME ";

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
        public DataTable GetJobLocationDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "JOB_LOCATION_ID, " +
                  "JOB_LOCATION " +
                  "FROM L_JOB_LOCATION ORDER BY JOB_LOCATION ";

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
        public DataTable GetApprovedByDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "APPROVED_EMPLOYEE_ID, " +
                  "APPROVED_EMPLOYEE_NAME " +
                  "FROM VEW_APPROVED_EMPLOYEE ORDER BY APPROVED_EMPLOYEE_NAME ";

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
        public DataTable GetWeeklyHolidayDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "WEEKLY_HOLIDAY_ID, " +
                  "UPPER(WEEKLY_HOLIDAY_NAME) WEEKLY_HOLIDAY_NAME " +
                  "FROM L_WEEKLY_HOLIDAY ORDER BY WEEKLY_HOLIDAY_NAME ";

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
        public DataTable GetDegreeDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "DEGREE_ID, " +
                  "DEGREE_NAME " +
                  "FROM L_DEGREE ORDER BY DEGREE_NAME ";

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
        public DataTable GetMajorSubjectDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "MAJOR_SUBJECT_ID, " +
                  "MAJOR_SUBJECT_NAME " +
                  "FROM L_MAJOR_SUBJECT ORDER BY MAJOR_SUBJECT_NAME ";

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

        /*
        public DataTable GetDepartmentDDListByUnitId(string pUnitId, string vHeadOfficeId, string vBranchOfficeId)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (DEPARTMENT_ID,'0')) DEPARTMENT_ID, " +
                  "TO_CHAR (NVL (DEPARTMENT_NAME,'N/A')) DEPARTMENT_NAME " +
                  "FROM L_DEPARTMENT where head_office_id = '" + vHeadOfficeId + "' and branch_office_id = '" + vBranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(pUnitId))
            {
                sql = sql + " and UNIT_ID = '" + pUnitId + "' ORDER BY DEPARTMENT_NAME";
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
        public DataTable GetSectionDDListByDepartmentId(string pDepartmentId, string vHeadOfficeId, string vBranchOfficeId)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (SECTION_ID,'0')) SECTION_ID, " +
                  "TO_CHAR (NVL (SECTION_NAME,'N/A')) SECTION_NAME " +
                  "FROM L_SECTION where head_office_id = '" + vHeadOfficeId + "' and branch_office_id = '" + vBranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(pDepartmentId))
            {
                sql = sql + " and DEPARTMENT_ID = '" + pDepartmentId + "' ORDER BY SECTION_NAME";
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

        public DataTable GetSubSectionDDListBySectionId(string pSectionId, string vHeadOfficeId, string vBranchOfficeId)
        {
            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (SUB_SECTION_ID,'0')) SUB_SECTION_ID, " +
                  "TO_CHAR (NVL (SUB_SECTION_NAME,'N/A')) SUB_SECTION_NAME " +
                  "FROM L_SUB_SECTION where head_office_id = '" + vHeadOfficeId + "' and branch_office_id = '" + vBranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(pSectionId))
            {
                sql = sql + " and SECTION_ID = '" + pSectionId + "' ORDER BY SUB_SECTION_NAME";
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
        */
        #endregion

        #region Shift Time Set

        public DataTable GetShiftTimeRecord(ShiftTimeModel objShiftTimeModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum SL, " +
                  " SHIFT_TIME_ID, " +
                  "SHIFT_NAME, " +
                  "FIRST_IN_TIME, " +
                  "LAST_OUT_TIME, " +
                  "LUNCH_OUT_TIME, " +
                  "LUNCH_IN_TIME " +


                  "FROM VEW_SHIFT_SETUP where head_office_id = '" + objShiftTimeModel.HeadOfficeId + "' and " +
                  "branch_office_id = '" + objShiftTimeModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objShiftTimeModel.SearchBy))
            {

                sql = sql + "and (lower(SHIFT_NAME) like lower( '%" + objShiftTimeModel.SearchBy + "%')  or " +
                      "upper(SHIFT_NAME)like upper('%" + objShiftTimeModel.SearchBy + "%')  or " +
                      "lower(SHIFT_TIME_ID) like lower( '%" + objShiftTimeModel.SearchBy + "%')  or " +
                      "upper(SHIFT_TIME_ID)like upper('%" + objShiftTimeModel.SearchBy + "%'))";
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

        public string SaveShiftTimeInfo(ShiftTimeModel objShiftTimeModel)
        {
            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_shift_setup");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (objShiftTimeModel.ShiftTimeId != "")
            {
                objOracleCommand.Parameters.Add("P_SHIFT_TIME_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShiftTimeModel.ShiftTimeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_SHIFT_TIME_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objShiftTimeModel.ShiftId != "")
            {
                objOracleCommand.Parameters.Add("p_shift_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShiftTimeModel.ShiftId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_shift_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objShiftTimeModel.FirstInTime != "")
            {
                objOracleCommand.Parameters.Add("p_first_in_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShiftTimeModel.FirstInTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_first_in_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objShiftTimeModel.LastOutTime != "")
            {
                objOracleCommand.Parameters.Add("p_last_out_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShiftTimeModel.LastOutTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_last_out_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objShiftTimeModel.LunchOutTime != "")
            {
                objOracleCommand.Parameters.Add("p_lunch_out_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShiftTimeModel.LunchOutTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_lunch_out_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objShiftTimeModel.LunchInTime != "")
            {
                objOracleCommand.Parameters.Add("p_lunch_in_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objShiftTimeModel.LunchInTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_lunch_in_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objShiftTimeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objShiftTimeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objShiftTimeModel.BranchOfficeId;


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

        public ShiftTimeModel GetShiftTimeById(ShiftTimeModel objModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "SHIFT_TIME_ID, " +
                  "FIRST_IN_TIME, " +
                  "LAST_OUT_TIME, " +
                  "LUNCH_OUT_TIME, " +
                  "LUNCH_IN_TIME, " +
                  "SHIFT_ID " +
                  "FROM S_SHIFT_SETUP WHERE HEAD_OFFICE_ID = '" + objModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objModel.BranchOfficeId + "'   ";

            if (!string.IsNullOrEmpty(objModel.ShiftTimeId))
            {
                sql = sql + " AND SHIFT_TIME_ID = '" + objModel.ShiftTimeId + "'   ";
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

                        objModel.ShiftTimeId = objReader["SHIFT_TIME_ID"].ToString();
                        objModel.FirstInTime = objReader["FIRST_IN_TIME"].ToString();
                        objModel.LastOutTime = objReader["LAST_OUT_TIME"].ToString();
                        objModel.LunchOutTime = objReader["LUNCH_OUT_TIME"].ToString();
                        objModel.LunchInTime = objReader["LUNCH_IN_TIME"].ToString();
                        objModel.ShiftId = objReader["SHIFT_ID"].ToString();

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

            return objModel;
        }
        #endregion

        #region Office Time Special case

        public DataTable GetOfficeTimeRecordSpecial(OfficeTimeSpecialModel objOfficeTimeModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                  "rownum SL, " +
                  " UNIT_NAME, " +
                  "to_char(from_date, 'dd/mm/yyyy')from_date, " +
                  "FIRST_IN_TIME, " +
                  "LAST_OUT_TIME, " +
                  "LUNCH_OUT_TIME, " +
                  "LUNCH_IN_TIME, " +
                  "office_time_id " +

                  "FROM VEW_OFFICE_TIME_SPECIAL where head_office_id = '" + objOfficeTimeModel.HeadOfficeId + "' and branch_office_id = '" + objOfficeTimeModel.BranchOfficeId + "'   ";


            if (!string.IsNullOrEmpty(objOfficeTimeModel.SearchBy))
            {
                //sql = sql + "and unit_id = '" + objOfficeTimeModel.UnitId + "' ";

                sql += "and ((LOWER(UNIT_NAME) LIKE LOWER('%" + objOfficeTimeModel.SearchBy + "%')OR UPPER(UNIT_NAME) LIKE UPPER ('%" + objOfficeTimeModel.SearchBy + "%') OR " +
                       "TO_CHAR (from_date, 'dd/mm/yyyy') LIKE ('%" + objOfficeTimeModel.SearchBy + "%')OR TO_CHAR (from_date, 'dd/mm/yyyy') LIKE('%" + objOfficeTimeModel.SearchBy + "%')))";
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

        public string SaveOfficeTimeInfoSpecial(OfficeTimeSpecialModel objOfficeTimeModel)
        {

            string strMsg = "";

            OracleCommand objOracleCommand = new OracleCommand("pro_office_time_special_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;

            if (objOfficeTimeModel.OfficeTimeId != "")
            {
                objOracleCommand.Parameters.Add("p_office_time_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.OfficeTimeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_office_time_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.UnitId != "")
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.UnitId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_unit_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.FromDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.FromDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_from_date", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.FirstInTime != "")
            {
                objOracleCommand.Parameters.Add("p_first_in_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.FirstInTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_first_in_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objOfficeTimeModel.LastOutTime != "")
            {
                objOracleCommand.Parameters.Add("p_last_out_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.LastOutTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_last_out_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objOfficeTimeModel.LunchOutTime != "")
            {
                objOracleCommand.Parameters.Add("p_lunch_out_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.LunchOutTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_lunch_out_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            if (objOfficeTimeModel.LunchInTime != "")
            {
                objOracleCommand.Parameters.Add("p_lunch_in_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objOfficeTimeModel.LunchInTime;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_lunch_in_time", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }

            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOfficeTimeModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOfficeTimeModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objOfficeTimeModel.BranchOfficeId;

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

        public OfficeTimeSpecialModel GetOfficeTimeSpecialById(OfficeTimeSpecialModel objOfficeTimeModel)
        {
            string sql = "";
            sql = "SELECT " +
                  "TO_CHAR (NVL (office_time_id,'0')), " +
                  "TO_CHAR (NVL (unit_id,'0')), " +
                  "to_char(from_date, 'dd/mm/yyyy')from_date," +
                  "TO_CHAR (NVL (first_in_time,'00:00')), " +
                  "TO_CHAR (NVL (last_out_time,'00:00')), " +
                  "TO_CHAR (NVL (lunch_out_time,'00:00')), " +
                  "TO_CHAR (NVL (lunch_in_time,'00:00')) " +
                  "FROM OFFICE_TIME_SPECIAL where head_office_id = '" + objOfficeTimeModel.HeadOfficeId + "' and branch_office_id = '" + objOfficeTimeModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objOfficeTimeModel.OfficeTimeId))
            {
                sql = sql + " and office_time_id = '" + objOfficeTimeModel.OfficeTimeId + "' ";
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
                        objOfficeTimeModel.OfficeTimeId = objReader.GetString(0);
                        objOfficeTimeModel.UnitId = objReader.GetString(1);
                        objOfficeTimeModel.FromDate = objReader.GetString(2);
                        objOfficeTimeModel.FirstInTime = objReader.GetString(3);
                        objOfficeTimeModel.LastOutTime = objReader.GetString(4);
                        objOfficeTimeModel.LunchOutTime = objReader.GetString(5);
                        objOfficeTimeModel.LunchInTime = objReader.GetString(6);
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

            return objOfficeTimeModel;
        }

        #endregion

        #region BranchOfficeList

        public DataTable GetBranchOfficeDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "BRANCH_OFFICE_ID, " +
                  "BRANCH_OFFICE_NAME " +
                  "FROM BRANCH_OFFICE  order by BRANCH_OFFICE_NAME ";

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

        #endregion


        #region Nabid combined on 24 Dec 18

        //Nabid Notice Type DD
        public DataTable GetNoticeTypeDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "NOTICE_TYPE_ID, " +
                  "NOTICE_TYPE_NAME " +
                  "FROM L_NOTICE_TYPE  order by NOTICE_TYPE_NAME ";

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

        public DataTable GetItemCPDDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "ITEM_ID, " +
                  "ITEM_NAME " +
                  "FROM L_ITEM_CP  order by ITEM_NAME ";


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



        public DataTable GetUnitTP_DDList()
        {

            DataTable dt = new DataTable();
            string sql = "";
            sql = "SELECT " +
                  "UNIT_ID, " +
                  "UNIT_NAME " +
                  "FROM L_UNIT_TP  order by UNIT_NAME ";


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


        public DataTable GetOnlySupplierDDList()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT " +
                        "SUPPLIER_ID, " +
                        "SUPPLIER_NAME " +
                        "FROM L_SUPPLIER order by SUPPLIER_NAME";

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




        public DataTable GetOrderSourceDDList()
        {
            DataTable dt = new DataTable();

            string sql = "SELECT " +
                        "ORDER_SOURCE_ID, " +
                        "ORDER_SOURCE_NAME " +
                        "FROM L_ORDER_SOURCE order by ORDER_SOURCE_NAME";

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
        #endregion


    }
}
