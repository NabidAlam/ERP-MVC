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
    public class NoticeBoardDAL
    {

        NoticeBoardModel objNoticeBoardModel = new NoticeBoardModel();
        NoticeBoardImageModel objNoticeBoardImageModel = new NoticeBoardImageModel();

        OracleTransaction trans = null;
        #region "Oracle Connection Check"
        private OracleConnection GetConnection()
        {
            var conString = System.Configuration.ConfigurationManager.ConnectionStrings["DBConString"];
            string strConnString = conString.ConnectionString;
            return new OracleConnection(strConnString);
        }
        #endregion



        public DataTable NoticeFileUpload(NoticeBoardModel objNoticeBoardModel)
        {
            //Master
            DataTable dt1 = new DataTable();
            string sql = "";

            //sql = "SELECT " +
            //      "rownum sl, " +
            //      "TO_CHAR (NVL (NOTICE_ID,'0'))NOTICE_ID, " +
            //      "TO_CHAR (NVL (NOTICE_TITLE,'N/A'))NOTICE_TITLE, " +
            //      "TO_CHAR (NVL (NOTICE_TYPE_ID,'0'))NOTICE_TYPE_ID, " +
            //      "TO_CHAR(NOTICE_DATE, 'dd/mm/yyyy') NOTICE_DATE, " +
            //      "TO_CHAR (NVL (FILE_NAME,'N/A'))FILE_NAME " +
            //      " FROM VEW_NOTICE_BOARD_UPLOAD where head_office_id = '" + objNoticeBoardModel.HeadOfficeId + "' AND branch_office_id = '" + objNoticeBoardModel.BranchOfficeId + "'  ";
            sql = "SELECT " +
                           "rownum sl, " +
                           "NOTICE_ID, " +
                           "TO_CHAR (NVL (NOTICE_TITLE,'N/A'))NOTICE_TITLE, " +
                           "NOTICE_TYPE_ID, " +
                            "NOTICE_TYPE_NAME, " +
                           "TO_CHAR(NOTICE_DATE, 'dd/mm/yyyy') NOTICE_DATE, " +
                           "FILE_NAME " +
                           " FROM VEW_NOTICE_BOARD_UPLOAD where head_office_id = '" + objNoticeBoardModel.HeadOfficeId + "' AND branch_office_id = '" + objNoticeBoardModel.BranchOfficeId + "'  ";

            //"FROM VEW_BILL_OF_MATERIAL_MAIN WHERE HEAD_OFFICE_ID = '" + objTrimsModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objTrimsModel.BranchOfficeId + "'  ";


            if (!string.IsNullOrWhiteSpace(objNoticeBoardModel.SearchBy))
            {
                sql += "and ( (lower(NOTICE_TYPE_NAME) like lower( '%" + objNoticeBoardModel.SearchBy + "%')  or upper(NOTICE_TYPE_NAME)like upper('%" + objNoticeBoardModel.SearchBy + "%') )" +
                          "or (TO_CHAR (NOTICE_DATE, 'dd/mm/yyyy') like '" + objNoticeBoardModel.SearchBy + "' or TO_CHAR (NOTICE_DATE, 'dd/mm/yyyy') like '" + objNoticeBoardModel.SearchBy + "')" +
                          "or (lower(NOTICE_TITLE) like lower( '%" + objNoticeBoardModel.SearchBy + "%')  or upper(NOTICE_TITLE)like upper('%" + objNoticeBoardModel.SearchBy + "%') ) )";
            }
           

         
           
            sql = sql + " ORDER BY NOTICE_DATE desc";

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


        public NoticeBoardModel GetNoticeById(NoticeBoardModel objNoticeBoardModel)
        {
            string vQuery = "SELECT " +
                          "rownum sl, " +
                          "NOTICE_ID, " +
                          "NOTICE_TITLE, " +
                          "NOTICE_TYPE_ID, " +
                          "NOTICE_TYPE_NAME, " +
                          "TO_CHAR(NOTICE_DATE, 'dd/mm/yyyy') NOTICE_DATE, " +
                          "FILE_NAME, " +
                          "FILE_SIZE, " +
                          "FILE_EXTENSION " +
                          " FROM VEW_NOTICE_BOARD_UPLOAD where head_office_id = '" + objNoticeBoardModel.HeadOfficeId + "' AND branch_office_id = '" + objNoticeBoardModel.BranchOfficeId + "'  ";


             //"FROM VEW_BILL_OF_MATERIAL_SUB WHERE HEAD_OFFICE_ID = '" + objTrimsModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objTrimsModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objNoticeBoardModel.NoticeId))
            {

                vQuery = vQuery + "and NOTICE_ID = '" + objNoticeBoardModel.NoticeId + "'";
            }

            if (!string.IsNullOrEmpty(objNoticeBoardModel.NoticeDate))
            {
                vQuery = vQuery + "and NOTICE_DATE = TO_DATE('" + objNoticeBoardModel.NoticeDate + "', 'dd/mm/yyyy')";
            }

            if (!string.IsNullOrEmpty(objNoticeBoardModel.NoticeTypeId))
            {

                vQuery = vQuery + "and NOTICE_TYPE_ID = '" + objNoticeBoardModel.NoticeTypeId + "'";
            }

            vQuery = vQuery + " ORDER BY NOTICE_DATE desc";



            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    if (objDataReader.HasRows)
                    {
                        objDataReader.Read();
                        objNoticeBoardModel.NoticeBoardImageModel = new NoticeBoardImageModel();
                       // objNoticeBoardImageModel = new NoticeBoardImageModel();
                        objNoticeBoardModel.SerialNumber = objDataReader.GetInt32(0).ToString();
                        objNoticeBoardModel.NoticeId = objDataReader.GetValue(1).ToString();
                        objNoticeBoardModel.NoticeTitle = objDataReader.GetValue(2).ToString();
                        objNoticeBoardModel.NoticeTypeId = objDataReader.GetValue(3).ToString();
                        //objNoticeBoardImageModel.NoticeBoardModel.NoticeTypeName = objDataReader.GetString(4).ToString();
                        objNoticeBoardModel.NoticeDate = objDataReader.GetValue(5).ToString();
                        objNoticeBoardModel.NoticeBoardImageModel.FileName = objDataReader.GetValue(6).ToString();
                        objNoticeBoardModel.NoticeBoardImageModel.FileExtension= objDataReader.GetValue(8).ToString();
                        objNoticeBoardModel.NoticeBoardImageModel.fileBytes= (byte[]) objDataReader.GetValue(7);
                        //objNoticeBoardImageModel.FabricUnitId = objDataReader.GetInt32(6).ToString();

                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objDataReader.Close();
                    objConnection.Close();
                }
            }

            return objNoticeBoardModel;
        }


        public DataTable NoticeFileUploadEdit(NoticeBoardModel objNoticeBoardModel)
        {

            DataTable dt2 = new DataTable();

            string sql = "";


            sql = "SELECT " +
                          "rownum sl, " +
                          "NOTICE_ID, " +
                          "NOTICE_TITLE, " +
                          "NOTICE_TYPE_ID, " +
                           "NOTICE_TYPE_NAME, " +
                          "TO_CHAR(NOTICE_DATE, 'dd/mm/yyyy') NOTICE_DATE, " +
                          "FILE_NAME " +
                          " FROM VEW_NOTICE_BOARD_UPLOAD where head_office_id = '" + objNoticeBoardModel.HeadOfficeId + "' AND branch_office_id = '" + objNoticeBoardModel.BranchOfficeId + "'  ";

            //"FROM VEW_BILL_OF_MATERIAL_SUB WHERE HEAD_OFFICE_ID = '" + objTrimsModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objTrimsModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objNoticeBoardModel.NoticeId))
            {

                sql = sql + "and NOTICE_ID = '" + objNoticeBoardModel.NoticeId + "'";
            }

            if (!string.IsNullOrEmpty(objNoticeBoardModel.NoticeDate))
            {
                sql = sql + "and NOTICE_DATE = TO_DATE('" + objNoticeBoardModel.NoticeDate + "', 'dd/mm/yyyy')";
            }

            if (!string.IsNullOrEmpty(objNoticeBoardModel.NoticeTypeId))
            {

                sql = sql + "and NOTICE_TYPE_ID = '" + objNoticeBoardModel.NoticeTypeId + "'";
            }

            sql = sql + " ORDER BY NOTICE_DATE desc";

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


        public NoticeBoardImageModel ViewPdfFile(NoticeBoardImageModel objNoticeBoardImageModel)
        {

            if (objNoticeBoardImageModel.NoticeId != null && objNoticeBoardImageModel.NoticeDate != null && objNoticeBoardImageModel.NoticeTypeId != null)
            {

                using (OracleConnection strConn = GetConnection())
                {
                    using (OracleCommand cmd = new OracleCommand())
                    {

                        cmd.CommandText = "SELECT FILE_SIZE,FILE_NAME FROM NOTICE_BOARD_IMAGE WHERE NOTICE_ID ='" + objNoticeBoardImageModel.NoticeId + "' and NOTICE_DATE =TO_DATE ('" + objNoticeBoardImageModel.NoticeDate + "', 'dd/mm/yyyy') and NOTICE_TYPE_ID ='" + objNoticeBoardImageModel.NoticeTypeId  + "' ";


                        cmd.Connection = strConn;
                        strConn.Open();
                        using (OracleDataReader sdr = cmd.ExecuteReader())
                        {
                            sdr.Read();
                            objNoticeBoardImageModel.bytes = (byte[])sdr["FILE_SIZE"];
                            objNoticeBoardImageModel.FileName = sdr["FILE_NAME"].ToString();
                        }
                        strConn.Close();
                    }
                }

            }

            return objNoticeBoardImageModel;

        }


        public string DeleteNoticeRecord(NoticeBoardImageModel objNoticeBoardImageModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_delete_notice_borad");
            objOracleCommand.CommandType = CommandType.StoredProcedure;


            if (!string.IsNullOrWhiteSpace(objNoticeBoardImageModel.NoticeId))
            {
                objOracleCommand.Parameters.Add("p_notice_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objNoticeBoardImageModel.NoticeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_notice_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objNoticeBoardImageModel.NoticeDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_notice_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardImageModel.NoticeDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_notice_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }





            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardImageModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardImageModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardImageModel.BranchOfficeId;



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




        public string SaveNotice(NoticeBoardModel objNoticeBoardModel)
        {


            string strMsg = "";


            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_notice_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;
            
            if (!string.IsNullOrWhiteSpace(objNoticeBoardModel.NoticeId))
            {
                objOracleCommand.Parameters.Add("p_notice_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objNoticeBoardModel.NoticeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_notice_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (!string.IsNullOrWhiteSpace(objNoticeBoardModel.NoticeTitle))
            {
                objOracleCommand.Parameters.Add("p_notice_title", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardModel.NoticeTitle;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_notice_title", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (objNoticeBoardModel.NoticeDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_notice_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardModel.NoticeDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_notice_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }

            if (!string.IsNullOrWhiteSpace(objNoticeBoardModel.NoticeTypeId))
            {
                objOracleCommand.Parameters.Add("p_notice_type_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardModel.NoticeTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_notice_type_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardModel.BranchOfficeId;



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
        public string SaveNoticeFile(NoticeBoardImageModel objNoticeBoardImageModel)
        {


            string strMsg = "";

            
            OracleTransaction objOracleTransaction = null;
            OracleCommand objOracleCommand = new OracleCommand("pro_notice_file_save");
            objOracleCommand.CommandType = CommandType.StoredProcedure;



            //pro_notice_file_save
            if (objNoticeBoardImageModel.NoticeId != "")
            {
                objOracleCommand.Parameters.Add("p_notice_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = objNoticeBoardImageModel.NoticeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_notice_id", OracleDbType.Varchar2, ParameterDirection.InputOutput).Value = null;
            }


            if (objNoticeBoardImageModel.NoticeDate.Length > 6)
            {
                objOracleCommand.Parameters.Add("p_notice_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardImageModel.NoticeDate;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_notice_date", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objNoticeBoardImageModel.NoticeTypeId != "")
            {
                objOracleCommand.Parameters.Add("p_notice_type_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardImageModel.NoticeTypeId;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_notice_type_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objNoticeBoardImageModel.FileName != "")
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardImageModel.FileName;
            }
            else
            {
                objOracleCommand.Parameters.Add("p_file_name", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }


            if (objNoticeBoardImageModel.FileSize != null)
            {
                byte[] array = System.Convert.FromBase64String(objNoticeBoardImageModel.FileSize);
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = array;

            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = null;

            }

            if (objNoticeBoardImageModel.FileExtension != "")
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardImageModel.FileExtension;
            }
            else
            {
                objOracleCommand.Parameters.Add("P_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = null;
            }




            objOracleCommand.Parameters.Add("p_update_by", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardImageModel.UpdateBy;
            objOracleCommand.Parameters.Add("p_head_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardImageModel.HeadOfficeId;
            objOracleCommand.Parameters.Add("p_branch_office_id", OracleDbType.Varchar2, ParameterDirection.Input).Value = objNoticeBoardImageModel.BranchOfficeId;



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





        //NOTICE BOARD FOR ALL
        public DataTable NoticeFileForAll(NoticeBoardModel objNoticeBoardModel)
        {
            //Master
            DataTable objDataTable = new DataTable();

            string vQuery = "SELECT " +
                            "rownum sl, " +
                            "NOTICE_ID, " +
                            "TO_CHAR (NVL (NOTICE_TITLE,'N/A'))NOTICE_TITLE, " +
                            "NOTICE_TYPE_ID, " +
                            "NOTICE_TYPE_NAME, " +
                            "TO_CHAR(NOTICE_DATE, 'dd/mm/yyyy') NOTICE_DATE, " +
                            "FILE_NAME " +
                            "FROM VEW_NOTICE_BOARD_UPLOAD where head_office_id = '" + objNoticeBoardModel.HeadOfficeId + "' AND branch_office_id = '" + objNoticeBoardModel.BranchOfficeId + "'  ";

            //vQuery = "SELECT * FROM VEW_NOTICE_BOARD_UPLOAD where head_office_id = '" + objNoticeBoardModel.HeadOfficeId + "' AND branch_office_id = '" + objNoticeBoardModel.BranchOfficeId + "'  ";

            //"FROM VEW_BILL_OF_MATERIAL_MAIN WHERE HEAD_OFFICE_ID = '" + objTrimsModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objTrimsModel.BranchOfficeId + "'  ";


            if (!string.IsNullOrWhiteSpace(objNoticeBoardModel.FromDate) && !string.IsNullOrWhiteSpace(objNoticeBoardModel.ToDate))
            {
                vQuery = vQuery + " and notice_date between to_date('" + objNoticeBoardModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objNoticeBoardModel.ToDate + "', 'dd/mm/yyyy') ";

            }

            if (!string.IsNullOrWhiteSpace(objNoticeBoardModel.btnAll))
            {
                vQuery = vQuery + " and NOTICE_TYPE_ID ='" + objNoticeBoardModel.btnAll + "' ";

            }

            if(!string.IsNullOrWhiteSpace(objNoticeBoardModel.btnBonusPoint))
            {
                vQuery = vQuery + " and NOTICE_TYPE_ID ='" + objNoticeBoardModel.btnBonusPoint + "' ";

            }

            if(!string.IsNullOrWhiteSpace(objNoticeBoardModel.btnHoliday))
            {
                vQuery = vQuery + " and NOTICE_TYPE_ID ='" + objNoticeBoardModel.btnHoliday + "' ";

            }

            if(!string.IsNullOrWhiteSpace(objNoticeBoardModel.btnIdCard))
            {
                vQuery = vQuery + " and NOTICE_TYPE_ID ='" + objNoticeBoardModel.btnIdCard + "' ";

            }

            if(!string.IsNullOrWhiteSpace(objNoticeBoardModel.btnKPI))
            {
                vQuery = vQuery + " and NOTICE_TYPE_ID ='" + objNoticeBoardModel.btnKPI + "' ";

            }

            if(!string.IsNullOrWhiteSpace(objNoticeBoardModel.btnPolicy))
            {
                vQuery = vQuery + " and NOTICE_TYPE_ID ='" + objNoticeBoardModel.btnPolicy + "' ";

            }

            if(!string.IsNullOrWhiteSpace(objNoticeBoardModel.btnRecruit))
            {
                vQuery = vQuery + " and NOTICE_TYPE_ID ='" + objNoticeBoardModel.btnRecruit + "' ";

            }

            if(!string.IsNullOrWhiteSpace(objNoticeBoardModel.btnTraining))
            {
                vQuery = vQuery + " and NOTICE_TYPE_ID ='" + objNoticeBoardModel.btnTraining + "' ";

            }



            vQuery = vQuery + " ORDER BY NOTICE_DATE desc";


            OracleCommand objCommand = new OracleCommand(vQuery);
            OracleDataAdapter objDataAdapter = new OracleDataAdapter(objCommand);

            using (OracleConnection objConnection = GetConnection())
            {
                try
                {
                    objCommand.Connection = objConnection;
                    objConnection.Open();
                    objDataAdapter.Fill(objDataTable);
                    objDataTable.AcceptChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objConnection.Close();
                }
            }

            return objDataTable;

        }



        public NoticeBoardModel GetLatestReport(NoticeBoardModel objNoticeBoardModel)
        {
            string vQuery = "SELECT " +
                            "rownum sl, " +
                            "NOTICE_ID, " +
                            "TO_CHAR (NVL (NOTICE_TITLE,'N/A'))NOTICE_TITLE, " +
                            "NOTICE_TYPE_ID, " +
                            "NOTICE_TYPE_NAME, " +
                            "TO_CHAR(NOTICE_DATE, 'dd/mm/yyyy') NOTICE_DATE, " +
                            "FILE_NAME, " +
                            "FILE_SIZE " +
                            "FROM VEW_LATEST_NOTICE where head_office_id = '" + objNoticeBoardModel.HeadOfficeId + "' AND branch_office_id = '" + objNoticeBoardModel.BranchOfficeId + "'  ";

            //if (!string.IsNullOrWhiteSpace(objNoticeBoardModel.FromDate) && !string.IsNullOrWhiteSpace(objNoticeBoardModel.ToDate))
            //{
            //    vQuery = vQuery + " and notice_date between to_date('" + objNoticeBoardModel.FromDate + "', 'dd/mm/yyyy') and to_date('" + objNoticeBoardModel.ToDate + "', 'dd/mm/yyyy') ";
            //}

            //vQuery = vQuery + " ORDER BY NOTICE_DATE desc";

            
            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    if (objDataReader.HasRows)
                    {
                        objDataReader.Read();
                        
                        //string vFileName = objDataReader["FILE_NAME"].ToString();

                        objNoticeBoardModel = new NoticeBoardModel
                        {
                            //NoticeBoardImageModel = new NoticeBoardImageModel
                            //{
                            //    FileSize = objDataReader["FILE_SIZE"].ToString()
                            //}
                            bytes = (byte[])objDataReader["FILE_SIZE"]

                        };
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objDataReader.Close();
                    objConnection.Close();
                }
            }

            return objNoticeBoardModel;

        }


    }
}
