using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.MODEL;
using Oracle.ManagedDataAccess.Client;

namespace ERP.DAL
{
    public class SpecificationDAL
    {
        OracleTransaction trans = null;

        #region "Oracle Connection Check"

        private OracleConnection GetConnection()
        {
            var conString = ConfigurationManager.ConnectionStrings["DBConString"];
            string strConnString = conString.ConnectionString;
            return new OracleConnection(strConnString);
        }

        #endregion


        public SpecificationModel GetStyleWiseFromProductMain(SpecificationModel objDressSpecModel)
        {
            var sql = "SELECT " +
                        "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO, " +
                        "TO_CHAR (NVL (SEASON_YEAR,'0'))SEASON_YEAR, " +
                        "TO_CHAR (NVL (SEASON_ID,'0'))SEASON_ID, " +
                        "TO_CHAR (NVL (SEASON_NAME,'N/A'))SEASON_NAME " +
                        "FROM VEW_PRODUCT_MAIN where head_office_id = '" + objDressSpecModel.HeadOfficeId + "' AND branch_office_id = '" + objDressSpecModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrEmpty(objDressSpecModel.SeasonYear))
            {
                sql = sql + " and SEASON_YEAR = '" + objDressSpecModel.SeasonYear + "'   ";
            }
            if (!string.IsNullOrEmpty(objDressSpecModel.SeasonId))
            {
                sql = sql + " and SEASON_ID = '" + objDressSpecModel.SeasonId + "'   ";
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
                        objDressSpecModel.StyleNo = objReader.GetString(0);
                        objDressSpecModel.SeasonYear = objReader.GetString(1);
                        objDressSpecModel.SeasonId = objReader.GetString(2);
                        objDressSpecModel.SeasonName = objReader.GetString(3);
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

            return objDressSpecModel;
        }

        public DataTable SearchStyleFromProductMain(SpecificationModel objDressSpecModel)
        {
            DataTable dataTable = new DataTable();

            string sql = "SELECT " +
                            "ROWNUM SL, " +
                            "TO_CHAR (NVL (SEASON_ID,'0'))SEASON_ID, " +
                            "TO_CHAR (NVL (SEASON_NAME,'N/A'))SEASON_NAME, " +
                            "TO_CHAR (NVL (SEASON_YEAR,'0'))SEASON_YEAR, " +
                            "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NO, " +
                            "TO_CHAR (NVL (STYLE_NO,'N/A'))STYLE_NAME " +
                            "FROM VEW_PRODUCT_MAIN where head_office_id = '" + objDressSpecModel.HeadOfficeId + "' AND branch_office_id = '" + objDressSpecModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrWhiteSpace(objDressSpecModel.StyleSearch))
            {
                sql = sql + "and (lower(STYLE_NO) like lower( '%" + objDressSpecModel.StyleSearch + "%')  or upper(STYLE_NO)like upper('%" + objDressSpecModel.StyleSearch + "%') )";
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
                    objDataAdapter.Fill(dataTable);
                    dataTable.AcceptChanges();
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

            return dataTable;
        }
        
        public string SaveDressSpecEntry(SpecificationModel objDressSpecModel)
        {
            string strMsg = null;

            int count = objDressSpecModel.SizeId.Length;
            int x = objDressSpecModel.SizeId.Count();
            for (int i = 0; i < x; i++)
            {
                var sizeId = objDressSpecModel.SizeId[i];
                var description = objDressSpecModel.Description[i];
                var sizeValue = objDressSpecModel.SizeValue[i];
                var tranId = objDressSpecModel.TranId[i];

                OracleCommand objOracleCommand = new OracleCommand("pro_dress_spec_save");
                objOracleCommand.CommandType = CommandType.StoredProcedure;

                if (objDressSpecModel.StyleNo != "")
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDressSpecModel.StyleNo;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (objDressSpecModel.StyleName != "")
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDressSpecModel.StyleName;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NAME", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (objDressSpecModel.SeasonId != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDressSpecModel.SeasonId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (objDressSpecModel.SeasonYear != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDressSpecModel.SeasonYear;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                
                if (objDressSpecModel.SpecificationDate.Length > 6)
                {
                    objOracleCommand.Parameters.Add("p_SPECIFICATION_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDressSpecModel.SpecificationDate;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SPECIFICATION_DATE", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }
                
                if (sizeId != null)
                {
                    objOracleCommand.Parameters.Add("p_SIZE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = sizeId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SIZE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (description != null)
                {
                    objOracleCommand.Parameters.Add("p_SPEC_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.Input).Value = description;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SPEC_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (sizeValue != null)
                {
                    objOracleCommand.Parameters.Add("p_SIZE_VALUE", OracleDbType.Varchar2, ParameterDirection.Input).Value = sizeValue;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SIZE_VALUE", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                if (tranId.Length > 0)
                {
                    objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = tranId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }

                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDressSpecModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDressSpecModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDressSpecModel.BranchOfficeId;
                
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
                        //trans.Dispose();
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        trans.Dispose();
                        strConn.Close();
                    }
                }
            }

            return strMsg;
        }

        public DataTable GetDressSpecRecord(SpecificationModel objDressSpecModel)
        {
            DataTable dataTable = new DataTable();

            string sql = "SELECT " +
                         "ROWNUM SL, " +
                         "STYLE_NO, " +
                         "STYLE_NAME, " +
                         "SEASON_ID, " +
                         "SEASON_NAME, " +
                         "SEASON_YEAR, " +
                         "TO_CHAR(SPECIFICATION_DATE, 'dd/mm/yyyy') SPECIFICATION_DATE, " +
                         "CREATE_BY, " +
                         "CREATE_DATE, " +
                         "UPDATE_BY, " +
                         "UPDATE_DATE, " +
                         "HEAD_OFFICE_ID, " +
                         "BRANCH_OFFICE_ID " +
                         "FROM VEW_DRESS_SPEC_MAIN WHERE HEAD_OFFICE_ID = '" + objDressSpecModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objDressSpecModel.BranchOfficeId + "'  ";

            if (!string.IsNullOrWhiteSpace(objDressSpecModel.SearchBy))
            {
                sql = sql + "and (lower(STYLE_NO) like lower( '%" + objDressSpecModel.SearchBy + "%')  " +
                            "or upper(STYLE_NO)like upper('%" + objDressSpecModel.SearchBy + "%')) " +

                            "or (lower(STYLE_NAME) like lower( '%" + objDressSpecModel.SearchBy + "%')  " +
                            "or upper(STYLE_NAME)like upper('%" + objDressSpecModel.SearchBy + "%')) " +

                            "or (lower(SEASON_NAME) like lower( '%" + objDressSpecModel.SearchBy + "%')  " +
                            "or upper(SEASON_NAME)like upper('%" + objDressSpecModel.SearchBy + "%')) " +

                            //"or (lower(SPECIFICATION_DATE) like lower( '%" + objDressSpecModel.SearchBy + "%')  " +
                            //"or upper(SPECIFICATION_DATE)like upper('%" + objDressSpecModel.SearchBy + "%')) " +

                            "or (lower(SEASON_YEAR) like lower( '%" + objDressSpecModel.SearchBy + "%')  " +
                            "or upper(SEASON_YEAR)like upper('%" + objDressSpecModel.SearchBy + "%'))";
            }
            
            if (!string.IsNullOrWhiteSpace(objDressSpecModel.SeasonYear))
            {
                sql = sql + "and SEASON_YEAR= '" + objDressSpecModel.SeasonYear + "'";
            }

            if (!string.IsNullOrWhiteSpace(objDressSpecModel.SeasonId))
            {
                sql = sql + "and SEASON_ID = '" + objDressSpecModel.SeasonId + "'";
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
                    objDataAdapter.Fill(dataTable);
                    dataTable.AcceptChanges();
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

            return dataTable;
        }

        public DataTable GetDressSpecDetailRecord(SpecificationModel objDressSpecModel)
        {
            DataTable dataTable = new DataTable();

            string sql = "SELECT " +
                         "STYLE_NO, " +
                         "STYLE_NAME, " +
                         "SEASON_ID, " +
                         "SEASON_YEAR, " +
                         "SIZE_ID, " +
                         "TO_CHAR(SPECIFICATION_DATE, 'dd/mm/yyyy') SPECIFICATION_DATE, " +
                         "SPEC_DESCRIPTION, " +
                         "SIZE_VALUE, " +
                         "TRAN_ID, " +
                         "CREATE_BY, " +
                         "CREATE_DATE, " +
                         "UPDATE_BY, " +
                         "UPDATE_DATE, " +
                         "HEAD_OFFICE_ID, " +
                         "BRANCH_OFFICE_ID " +
                         "FROM product_spec_sub WHERE HEAD_OFFICE_ID = '" + objDressSpecModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objDressSpecModel.BranchOfficeId + "' ";
            
            if (!string.IsNullOrWhiteSpace(objDressSpecModel.StyleNo))
            {
                sql = sql + "and STYLE_NO = '" + objDressSpecModel.StyleNo + "'";
            }

            if (!string.IsNullOrWhiteSpace(objDressSpecModel.SeasonYear))
            {
                sql = sql + "and SEASON_YEAR= '" + objDressSpecModel.SeasonYear + "'";
            }

            if (!string.IsNullOrWhiteSpace(objDressSpecModel.SeasonId))
            {
                sql = sql + "and SEASON_ID = '" + objDressSpecModel.SeasonId + "'";
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
                    objDataAdapter.Fill(dataTable);
                    dataTable.AcceptChanges();
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

            return dataTable;
        }


        public string DeleteTrimsEntry(SpecificationModel objDressSpecModel)
        {
            string strMsg = "";

            //string[] tranIdArray = objDressSpecModel.GridTranId.Split(',');
            //int x = tranIdArray.Count();
            int x = objDressSpecModel.TranId.Count();

            for (int i = 0; i < x; i++)
            {
                //var arrayTranId = tranIdArray[i];
                string tranId = objDressSpecModel.TranId[i];

                OracleCommand objOracleCommand = new OracleCommand("PRO_DRESS_SPEC_DELETE");
                objOracleCommand.CommandType = CommandType.StoredProcedure;

                if (objDressSpecModel.StyleNo != "")
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDressSpecModel.StyleNo;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_STYLE_NO", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (objDressSpecModel.SeasonYear != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDressSpecModel.SeasonYear;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_YEAR", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (objDressSpecModel.SeasonId != "")
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objDressSpecModel.SeasonId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_SEASON_ID", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
                }

                if (tranId != null)
                {
                    objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = tranId;
                }
                else
                {
                    objOracleCommand.Parameters.Add("p_TRAN_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
                }
                
                objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDressSpecModel.UpdateBy;
                objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDressSpecModel.HeadOfficeId;
                objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objDressSpecModel.BranchOfficeId;

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
                        trans.Dispose();
                        strConn.Close();
                    }
                }
            }

            return strMsg;
        }

    }
}
