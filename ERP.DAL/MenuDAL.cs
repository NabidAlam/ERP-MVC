using System;
using System.Collections.Generic;
using ERP.MODEL;
using System.Linq;
using System.Text;
using Oracle.ManagedDataAccess.Client;
using System.Threading.Tasks;

namespace ERP.DAL
{
    public class MenuDAL
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

        public List<MenuModel> GetHeaderParentMenuList(MenuModel objMenuModel)
        {
            List<MenuModel> mainMenuList = new List<MenuModel>();

            string vQuery = "SELECT " +
                        " TO_CHAR (NVL (MENU_ID,'0')), " +
                        " TO_CHAR (NVL (MENU_NAME,'N/A')), " +
                        " TO_CHAR (NVL (MENU_URL,'N/A')) " +
                    "FROM vew_s_menu_main WHERE head_office_id = '" + objMenuModel.HeadOfficeId + "'   ";


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        MenuModel objMainMenu = new MenuModel();

                        objMainMenu.MenuId = objReader.GetString(0);
                        objMainMenu.MenuName = objReader.GetString(1);
                        objMainMenu.MenuURL = objReader.GetString(2);

                        mainMenuList.Add(objMainMenu);
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

            return mainMenuList;
        }
        public List<MenuModel> GetHeaderChildMenuList(MenuModel objMenuModel)
        {
            List<MenuModel> subMenuList = new List<MenuModel>();

            string vQuery = "SELECT " +
                        " TO_CHAR (NVL (MENU_ID,'0')), " +
                        " TO_CHAR (NVL (PARENT_ID,'0')), " +
                        " TO_CHAR (NVL (MENU_NAME,'N/A')), " +
                        " TO_CHAR (NVL (MENU_URL,'N/A')) " +
                    "FROM vew_s_menu_sub WHERE head_office_id = '" + objMenuModel.HeadOfficeId + "'  ";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        MenuModel objSubMenu = new MenuModel();

                        objSubMenu.MenuId = objReader.GetString(0);
                        objSubMenu.ParentId = objReader.GetString(1);
                        objSubMenu.MenuName = objReader.GetString(2);
                        objSubMenu.MenuURL = objReader.GetString(3);

                        subMenuList.Add(objSubMenu);
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

            return subMenuList;
        }



        public List<MenuModel> GetLeftParentMenuList(MenuModel objMenuModel)
        {
            List<MenuModel> mainMenuList = new List<MenuModel>();

            string vQuery = "SELECT " +
                            " TO_CHAR (NVL (MENU_ID,'0')), " +
                            " TO_CHAR (NVL (MENU_NAME,'N/A')), " +
                            " TO_CHAR (NVL (MENU_ICON,'N/A')), " +
                            " TO_CHAR (NVL (MENU_URL,'N/A')), " +
                            " TO_CHAR (NVL (SOFTWARE_ID,'0')), " +
                            " TO_CHAR (NVL (ACTIVE_YN,'N/A')), " +
                            " TO_CHAR (NVL (UPDATE_BY,'0')), " +
                            " TO_CHAR (NVL (HEAD_OFFICE_ID,'0')), " +
                            " TO_CHAR (NVL (BRANCH_OFFICE_ID,'0')), " +
                            " TO_CHAR (NVL (MANUAL_YN,'N/A')) " +
                            "FROM MENU_MAIN WHERE HEAD_OFFICE_ID = '" + objMenuModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objMenuModel.BranchOfficeId + "' AND SOFTWARE_ID = '" + objMenuModel.SoftwareId + "' AND EMPLOYEE_ID = '" + objMenuModel.EmployeeId + "'   ORDER BY MENU_ID";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        MenuModel objMainMenu = new MenuModel();

                        objMainMenu.MenuId = objReader.GetString(0);
                        objMainMenu.MenuName = objReader.GetString(1);
                        objMainMenu.MenuIcon = objReader.GetString(2);
                        objMainMenu.MenuURL = objReader.GetString(3);


                        mainMenuList.Add(objMainMenu);
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

            return mainMenuList;
        }
        public List<MenuModel> GetLeftChildMenuList(MenuModel objMenuModel)
        {
            List<MenuModel> subMenuList = new List<MenuModel>();

            string vQuery = "SELECT " +
                            " TO_CHAR (NVL (MENU_ID,'0')), " +
                            " TO_CHAR (NVL (PARENT_ID,'0')), " +
                            " TO_CHAR (NVL (MENU_NAME,'N/A')), " +
                            " TO_CHAR (NVL (MENU_URL,'N/A')), " +
                            " TO_CHAR (NVL (SOFTWARE_ID,'0')), " +
                            " TO_CHAR (NVL (ACTIVE_YN,'N/A')), " +
                            " TO_CHAR (NVL (UPDATE_BY,'0')), " +
                            " TO_CHAR (NVL (HEAD_OFFICE_ID,'0')), " +
                            " TO_CHAR (NVL (BRANCH_OFFICE_ID,'0')), " +
                            " TO_CHAR (NVL (MANUAL_YN,'N/A')) " +
                            "FROM MENU_SUB WHERE HEAD_OFFICE_ID = '" + objMenuModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objMenuModel.BranchOfficeId + "' AND SOFTWARE_ID = '" + objMenuModel.SoftwareId + "' AND EMPLOYEE_ID = '" + objMenuModel.EmployeeId + "'   ORDER BY MENU_ID";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objReader = objCommand.ExecuteReader();

                try
                {
                    while (objReader.Read())
                    {
                        MenuModel objSubMenu = new MenuModel();

                        objSubMenu.MenuId = objReader.GetString(0);
                        objSubMenu.ParentId = objReader.GetString(1);
                        objSubMenu.MenuName = objReader.GetString(2);
                        objSubMenu.MenuURL = objReader.GetString(3);

                        subMenuList.Add(objSubMenu);
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

            return subMenuList;
        }

        public MenuModel GetEmployeePhoto(MenuModel objMenuModel)
        {

            string sql = "";
            sql = "SELECT " +
               "FILE_NAME, " +
               "FILE_SIZE " +
               "FROM EMPLOYEE_IMAGE where employee_id = '" + objMenuModel.EmployeeId + "' and head_office_id = '" + objMenuModel.HeadOfficeId + "' AND branch_office_id = '" + objMenuModel.BranchOfficeId + "' ";


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

                        if (objDataReader.GetString(0) != null)
                        {
                            objMenuModel.ImageFileByte = (byte[])objDataReader.GetValue(1);
                            objMenuModel.ImageFileNameBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(objMenuModel.ImageFileByte);
                        }

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

            return objMenuModel;

        }
    }
}
