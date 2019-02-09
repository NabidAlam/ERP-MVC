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
    public class SecurityDAL
    {
        OracleTransaction trans = null;

        #region Oracle Connection Check

        private OracleConnection GetConnection()
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConString"];
            string strConnString = conString.ConnectionString;
            return new OracleConnection(strConnString);
        }

        #endregion
        public DataTable LoadEmployeePasswordList(PasswordListModel objPasswordListModel)
        {

            DataTable dt = new DataTable();

            string sql = "";
            sql = "SELECT " +
                "rownum sl, " +
                  "EMPLOYEE_ID, " +
                   "EMPLOYEE_NAME, " +
                   "designation_name, " +
                   "TO_CHAR(JOINING_DATE,'dd/mm/yyyy')JOINING_DATE, " +
                   "EMPLOYEE_PASSWORD, " +
                    "SOFTWARE_NAME, " +
                   "EMPLOYEE_PIC " +
                  " FROM VEW_EMPLOYEE_PASSWORD_LIST where head_office_id = '" + objPasswordListModel.HeadOfficeId + "'   ";

            if (!string.IsNullOrWhiteSpace(objPasswordListModel.EmployeeId))
            {

                sql = sql + "and employee_id = '" + objPasswordListModel.EmployeeId + "' ";
            }

            if (!string.IsNullOrWhiteSpace(objPasswordListModel.EmployeeName))
            {
                sql = sql + "and (lower(employee_name) like lower( '%" + objPasswordListModel.EmployeeName + "%')  or upper(employee_name)like upper('%" + objPasswordListModel.EmployeeName + "%') )";

            }

            if (!string.IsNullOrWhiteSpace(objPasswordListModel.DepartmentId))
            {

                sql = sql + "and department_id = '" + objPasswordListModel.DepartmentId + "' ";
            }

            if (!string.IsNullOrWhiteSpace(objPasswordListModel.UnitId))
            {

                sql = sql + "and unit_id = '" + objPasswordListModel.UnitId + "' ";
            }

            if(!string.IsNullOrWhiteSpace(objPasswordListModel.SubSectionId))
            {

                sql = sql + "and sub_section_id = '" + objPasswordListModel.SubSectionId + "' ";
            }

            if (!string.IsNullOrWhiteSpace(objPasswordListModel.SectionId))
            {

                sql = sql + "and section_id = '" + objPasswordListModel.SectionId + "' ";
            }


            sql = sql + " order by SL";

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
