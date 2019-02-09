using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.MODEL;
using Oracle.ManagedDataAccess.Client;

namespace ERP.DAL
{
    public class FabricDAL
    {
        #region Oracle Connection

        OracleTransaction trans = null;

        private OracleConnection GetConnection()
        {            
            return new OracleConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBConString"].ConnectionString);
        }

        #endregion



        #region Fabric Requisition

        public string SaveFabricRequisition(FabricRequisitionModel objFabricRequisitionModel)
        {
            using (OracleConnection objConnection = GetConnection())
            {
                using (OracleCommand objCommand = new OracleCommand { CommandText = "PRO_FABRIC_REQUISITION_SAVE", CommandType = CommandType.StoredProcedure })
                {
                    objCommand.Parameters.Add("p_FABRIC_REQUISITION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.FabricRequisitionId) ? objFabricRequisitionModel.FabricRequisitionId : null;
                    //objCommand.Parameters.Add("p_FABRIC_REQUISITION_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.FabricRequisitionCode) ? objFabricRequisitionModel.FabricRequisitionCode : null;
                    //objCommand.Parameters.Add("p_REQUISITION_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricRequisitionModel.RequisitionDate.Length > 6 ? objFabricRequisitionModel.RequisitionDate : null;
                    objCommand.Parameters.Add("p_FABRIC_CODE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.FabricCode) ? objFabricRequisitionModel.FabricCode : null;
                    objCommand.Parameters.Add("p_REQUIRE_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricRequisitionModel.RequireDate.Length > 6 ? objFabricRequisitionModel.RequireDate : null;
                    objCommand.Parameters.Add("p_FABRIC_TYPE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.FabricTypeId) ? objFabricRequisitionModel.FabricTypeId : null;
                    objCommand.Parameters.Add("p_FABRIC_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.FabricUnitId) ? objFabricRequisitionModel.FabricUnitId : null;
                    objCommand.Parameters.Add("p_FABRIC_WIDTH", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.FabricWidth) ? objFabricRequisitionModel.FabricWidth : null;
                    objCommand.Parameters.Add("p_QUANTITY", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.RequisitionQuantity) ? objFabricRequisitionModel.RequisitionQuantity : null;
                    objCommand.Parameters.Add("p_UNIT_PRICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.UnitPrice) ? objFabricRequisitionModel.UnitPrice : null;
                    objCommand.Parameters.Add("p_FABRIC_DESCRIPTION", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.FabricDescription) ? objFabricRequisitionModel.FabricDescription : null;
                    objCommand.Parameters.Add("p_CATEGORY_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.CategoryId) ? objFabricRequisitionModel.CategoryId : null;
                    objCommand.Parameters.Add("p_SUPPLIER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.SupplierId) ? objFabricRequisitionModel.SupplierId : null;
                    //objCommand.Parameters.Add("p_APPROVAL_STATUS", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.ApprovalStatus) ? objFabricRequisitionModel.ApprovalStatus : null;
                    objCommand.Parameters.Add("p_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.Remarks) ? objFabricRequisitionModel.Remarks : null;
                    /////
                    objCommand.Parameters.Add("p_SWATCH_FILE_NAME", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.SwatchFileName) ? objFabricRequisitionModel.SwatchFileName : null;

                    objCommand.Parameters.Add("p_SWATCH_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = objFabricRequisitionModel.SwatchFileSize;
                    //objCommand.Parameters.Add("p_SWATCH_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = objFabricRequisitionModel.SwatchFileSize ?? null;
                    //objCommand.Parameters.Add("p_SWATCH_FILE_SIZE", OracleDbType.Blob, ParameterDirection.Input).Value = objFabricRequisitionModel.SwatchFileSize != null ? objFabricRequisitionModel.SwatchFileSize : null;

                    objCommand.Parameters.Add("p_SWATCH_FILE_EXTENSION", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.SwatchFileExtension) ? objFabricRequisitionModel.SwatchFileExtension : null;
                    /////
                    objCommand.Parameters.Add("p_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricRequisitionModel.UpdateBy;
                    objCommand.Parameters.Add("p_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricRequisitionModel.HeadOfficeId;
                    objCommand.Parameters.Add("p_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricRequisitionModel.BranchOfficeId;

                    objCommand.Parameters.Add("p_Message", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;


                    try
                    {
                        objCommand.Connection = objConnection;
                        objConnection.Open();
                        trans = objConnection.BeginTransaction();
                        objCommand.ExecuteNonQuery();
                        trans.Commit();
                        objConnection.Close();

                        return objCommand.Parameters["p_Message"].Value.ToString();
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
            }
        }

        public List<FabricRequisitionModel> GetAllFabricRequisition(FabricRequisitionModel objFabricRequisitionModel)
        {
            List<FabricRequisitionModel> fabricRequisitionList = new List<FabricRequisitionModel>();

            string vQuery = "SELECT " +
                            "FABRIC_REQUISITION_ID, " +
                            "FABRIC_REQUISITION_CODE, " +
                            "FABRIC_CODE, " +
                            "TO_CHAR(REQUISITION_DATE, 'dd/mm/yyyy') REQUISITION_DATE, " +
                            "TO_CHAR(REQUIRE_DATE, 'dd/mm/yyyy') REQUIRE_DATE, " +
                            "FABRIC_TYPE_ID, " +
                            "FABRIC_TYPE_NAME, " +
                            "FABRIC_UNIT_ID, " +
                            "FABRIC_UNIT_NAME, " +
                            "FABRIC_WIDTH, " +
                            "QUANTITY, " +
                            "UNIT_PRICE, " +
                            //"FABRIC_DESCRIPTION, " +
                            "CATEGORY_ID, " +
                            "CATEGORY_NAME, " +
                            "SUPPLIER_ID, " +
                            "SUPPLIER_NAME, " +
                            "APPROVAL_STATUS, " +
                            //"REMARKS, " +
                            //"PURCHASE_DATE_STATUS " +
                            "TO_CHAR(PURCHASE_DATE_STATUS, 'dd/mm/yyyy') PURCHASE_DATE_STATUS " +
                            //"CREATE_BY, " +
                            //"CREATE_DATE, " +
                            //"UPDATE_BY, " +
                            //"UPDATE_DATE, " +
                            //"HEAD_OFFICE_ID, " +
                            //"BRANCH_OFFICE_ID " +

                            "FROM VEW_FABRIC_REQUISITION WHERE HEAD_OFFICE_ID = '" + objFabricRequisitionModel.HeadOfficeId + "' and BRANCH_OFFICE_ID = '" + objFabricRequisitionModel.BranchOfficeId + "'  AND APPROVAL_STATUS IN ('P', 'R')";


            if (!string.IsNullOrWhiteSpace(objFabricRequisitionModel.SearchBy))
            {
                vQuery += "and ( (lower(FABRIC_REQUISITION_CODE) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_REQUISITION_CODE)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_CODE) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_CODE)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (TO_CHAR (REQUISITION_DATE, 'dd/mm/yyyy') like '" + objFabricRequisitionModel.SearchBy + "' or TO_CHAR (REQUISITION_DATE, 'dd/mm/yyyy') like '" + objFabricRequisitionModel.SearchBy + "')" +
                          "or (TO_CHAR (REQUIRE_DATE, 'dd/mm/yyyy') like '" + objFabricRequisitionModel.SearchBy + "' or TO_CHAR (REQUIRE_DATE, 'dd/mm/yyyy') like '" + objFabricRequisitionModel.SearchBy + "')" +
                          "or (lower(FABRIC_TYPE_NAME) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_TYPE_NAME)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_UNIT_NAME) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_UNIT_NAME)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_WIDTH) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_WIDTH)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(QUANTITY) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(QUANTITY)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(UNIT_PRICE) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(UNIT_PRICE)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(CATEGORY_NAME) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(CATEGORY_NAME)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(SUPPLIER_NAME) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(SUPPLIER_NAME)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(APPROVAL_STATUS) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(APPROVAL_STATUS)like upper('%" + objFabricRequisitionModel.SearchBy + "%') ) )";
            }
            else
            {
                vQuery += "AND TO_CHAR (REQUISITION_DATE, 'DD/MM/YYYY') = TO_CHAR (SYSDATE, 'DD/MM/YYYY')";
            }


            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objFabricRequisitionModel = new FabricRequisitionModel();

                        objFabricRequisitionModel.FabricRequisitionId = objDataReader["FABRIC_REQUISITION_ID"].ToString();
                        objFabricRequisitionModel.FabricRequisitionCode = objDataReader["FABRIC_REQUISITION_CODE"].ToString();
                        objFabricRequisitionModel.FabricCode = objDataReader["FABRIC_CODE"].ToString();
                        objFabricRequisitionModel.RequisitionDate = objDataReader["REQUISITION_DATE"].ToString();
                        objFabricRequisitionModel.RequireDate = objDataReader["REQUIRE_DATE"].ToString();
                        objFabricRequisitionModel.FabricTypeId = objDataReader["FABRIC_TYPE_ID"].ToString();
                        objFabricRequisitionModel.FabricTypeName = objDataReader["FABRIC_TYPE_NAME"].ToString();
                        objFabricRequisitionModel.FabricUnitId = objDataReader["FABRIC_UNIT_ID"].ToString();
                        objFabricRequisitionModel.FabricUnitName = objDataReader["FABRIC_UNIT_NAME"].ToString();
                        objFabricRequisitionModel.FabricWidth = objDataReader["FABRIC_WIDTH"].ToString();
                        objFabricRequisitionModel.RequisitionQuantity = objDataReader["QUANTITY"].ToString();
                        objFabricRequisitionModel.UnitPrice = objDataReader["UNIT_PRICE"].ToString();
                        //objFabricRequisitionModel.FabricDescription = objDataReader["FABRIC_DESCRIPTION"].ToString();
                        objFabricRequisitionModel.CategoryId = objDataReader["CATEGORY_ID"].ToString();
                        objFabricRequisitionModel.CategoryName = objDataReader["CATEGORY_NAME"].ToString();
                        objFabricRequisitionModel.SupplierId = objDataReader["SUPPLIER_ID"].ToString();
                        objFabricRequisitionModel.SupplierName = objDataReader["SUPPLIER_NAME"].ToString();
                        objFabricRequisitionModel.ApprovalStatus = objDataReader["APPROVAL_STATUS"].ToString();
                        //objFabricRequisitionModel.Remarks = objDataReader["REMARKS"].ToString();
                        objFabricRequisitionModel.PurchaseDateStatus = objDataReader["PURCHASE_DATE_STATUS"].ToString();
                        //objFabricRequisitionModel.CreateBy = objDataReader["CREATE_BY"].ToString();
                        //objFabricRequisitionModel.CreateDate = objDataReader["CREATE_DATE"].ToString();
                        //objFabricRequisitionModel.UpdateBy = objDataReader["UPDATE_BY"].ToString();
                        //objFabricRequisitionModel.UpdateBy = objDataReader["UPDATE_DATE"].ToString();
                        //objFabricRequisitionModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                        //objFabricRequisitionModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();

                        fabricRequisitionList.Add(objFabricRequisitionModel);
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

            return fabricRequisitionList;
        }

        public FabricRequisitionModel GetFabricRequisitionById(FabricRequisitionModel objFabricRequisitionModel)
        {
            string vQuery = "SELECT " +
                            "FABRIC_REQUISITION_ID, " +
                            "FABRIC_REQUISITION_CODE, " +
                            "FABRIC_CODE, " +
                            "TO_CHAR(REQUISITION_DATE, 'dd/mm/yyyy') REQUISITION_DATE, " +
                            "TO_CHAR(REQUIRE_DATE, 'dd/mm/yyyy') REQUIRE_DATE, " +
                            "FABRIC_TYPE_ID, " +
                            "FABRIC_TYPE_NAME, " +
                            "FABRIC_UNIT_ID, " +
                            "FABRIC_UNIT_NAME, " +
                            "FABRIC_WIDTH, " +
                            "QUANTITY, " +
                            "UNIT_PRICE, " +
                            "FABRIC_DESCRIPTION, " +
                            "CATEGORY_ID, " +
                            "CATEGORY_NAME, " +
                            "SUPPLIER_ID, " +
                            "SUPPLIER_NAME, " +
                            "APPROVAL_STATUS, " +
                            "REMARKS, " +
                            "SWATCH_FILE_NAME, " +
                            "SWATCH_FILE_SIZE, " +
                            "SWATCH_FILE_EXTENSION, " +
                            "CREATE_BY, " +
                            "CREATE_DATE, " +
                            "UPDATE_BY, " +
                            "UPDATE_DATE, " +
                            "HEAD_OFFICE_ID, " +
                            "BRANCH_OFFICE_ID " +
                            "FROM VEW_FABRIC_REQUISITION WHERE HEAD_OFFICE_ID = '" + objFabricRequisitionModel.HeadOfficeId + "' and BRANCH_OFFICE_ID = '" + objFabricRequisitionModel.BranchOfficeId + "' ";

            if (!string.IsNullOrEmpty(objFabricRequisitionModel.FabricRequisitionId))
            {
                vQuery += " and FABRIC_REQUISITION_ID = '" + objFabricRequisitionModel.FabricRequisitionId + "'";
            }

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

                        objFabricRequisitionModel = new FabricRequisitionModel();

                        objFabricRequisitionModel.FabricRequisitionId = objDataReader["FABRIC_REQUISITION_ID"].ToString();
                        objFabricRequisitionModel.FabricRequisitionCode = objDataReader["FABRIC_REQUISITION_CODE"].ToString();
                        objFabricRequisitionModel.FabricCode = objDataReader["FABRIC_CODE"].ToString();
                        objFabricRequisitionModel.RequisitionDate = objDataReader["REQUISITION_DATE"].ToString();
                        objFabricRequisitionModel.RequireDate = objDataReader["REQUIRE_DATE"].ToString();
                        objFabricRequisitionModel.FabricTypeId = objDataReader["FABRIC_TYPE_ID"].ToString();
                        objFabricRequisitionModel.FabricTypeName = objDataReader["FABRIC_TYPE_NAME"].ToString();
                        objFabricRequisitionModel.FabricUnitId = objDataReader["FABRIC_UNIT_ID"].ToString();
                        objFabricRequisitionModel.FabricUnitName = objDataReader["FABRIC_UNIT_NAME"].ToString();
                        objFabricRequisitionModel.FabricWidth = objDataReader["FABRIC_WIDTH"].ToString();
                        objFabricRequisitionModel.RequisitionQuantity = objDataReader["QUANTITY"].ToString();
                        objFabricRequisitionModel.UnitPrice = objDataReader["UNIT_PRICE"].ToString();
                        objFabricRequisitionModel.FabricDescription = objDataReader["FABRIC_DESCRIPTION"].ToString();
                        objFabricRequisitionModel.CategoryId = objDataReader["CATEGORY_ID"].ToString();
                        objFabricRequisitionModel.CategoryName = objDataReader["CATEGORY_NAME"].ToString();
                        objFabricRequisitionModel.SupplierId = objDataReader["SUPPLIER_ID"].ToString();
                        objFabricRequisitionModel.SupplierName = objDataReader["SUPPLIER_NAME"].ToString();
                        objFabricRequisitionModel.ApprovalStatus = objDataReader["APPROVAL_STATUS"].ToString();
                        objFabricRequisitionModel.Remarks = objDataReader["REMARKS"].ToString();

                        objFabricRequisitionModel.SwatchFileName = objDataReader["SWATCH_FILE_NAME"].ToString();
                        objFabricRequisitionModel.SwatchFileSize = objDataReader["SWATCH_FILE_SIZE"] == DBNull.Value ? new byte[0] : (byte[])objDataReader["SWATCH_FILE_SIZE"];
                        objFabricRequisitionModel.SwatchFileExtension = objDataReader["SWATCH_FILE_EXTENSION"].ToString();
                        objFabricRequisitionModel.SwatchImage = "data:image/jpeg;base64," + Convert.ToBase64String(objFabricRequisitionModel.SwatchFileSize);

                        objFabricRequisitionModel.CreateBy = objDataReader["CREATE_BY"].ToString();
                        objFabricRequisitionModel.CreateDate = objDataReader["CREATE_DATE"].ToString();
                        objFabricRequisitionModel.UpdateBy = objDataReader["UPDATE_BY"].ToString();
                        objFabricRequisitionModel.UpdateBy = objDataReader["UPDATE_DATE"].ToString();
                        objFabricRequisitionModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                        objFabricRequisitionModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();
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

            return objFabricRequisitionModel;
        }

        #endregion

        #region Fabric Requisition Status

        public List<FabricRequisitionModel> GetAllFabricRequisitionForStatus(FabricRequisitionModel objFabricRequisitionModel)
        {
            List<FabricRequisitionModel> fabricRequisitionList = new List<FabricRequisitionModel>();

            string vQuery = "SELECT " +
                            "FABRIC_REQUISITION_ID, " +
                            "FABRIC_REQUISITION_CODE, " +
                            "FABRIC_CODE, " +
                            "TO_CHAR(REQUISITION_DATE, 'dd/mm/yyyy') REQUISITION_DATE, " +
                            "TO_CHAR(REQUIRE_DATE, 'dd/mm/yyyy') REQUIRE_DATE, " +
                            "FABRIC_TYPE_ID, " +
                            "FABRIC_TYPE_NAME, " +
                            "FABRIC_UNIT_ID, " +
                            "FABRIC_UNIT_NAME, " +
                            "FABRIC_WIDTH, " +
                            "QUANTITY, " +
                            "UNIT_PRICE, " +
                            //"FABRIC_DESCRIPTION, " +
                            "CATEGORY_ID, " +
                            "CATEGORY_NAME, " +
                            "SUPPLIER_ID, " +
                            "SUPPLIER_NAME, " +
                            "APPROVAL_STATUS, " +
                            //"REMARKS, " +
                            //"PURCHASE_DATE_STATUS " +
                            "TO_CHAR(PURCHASE_DATE_STATUS, 'dd/mm/yyyy') PURCHASE_DATE_STATUS " +
                            //"CREATE_BY, " +
                            //"CREATE_DATE, " +
                            //"UPDATE_BY, " +
                            //"UPDATE_DATE, " +
                            //"HEAD_OFFICE_ID, " +
                            //"BRANCH_OFFICE_ID " +

                            "FROM VEW_FABRIC_REQUISITION WHERE HEAD_OFFICE_ID = '" + objFabricRequisitionModel.HeadOfficeId + "' and BRANCH_OFFICE_ID = '" + objFabricRequisitionModel.BranchOfficeId + "' ";


            if (!string.IsNullOrWhiteSpace(objFabricRequisitionModel.SearchBy))
            {
                vQuery += "and ( (lower(FABRIC_REQUISITION_CODE) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_REQUISITION_CODE)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_CODE) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_CODE)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (TO_CHAR (REQUISITION_DATE, 'dd/mm/yyyy') like '" + objFabricRequisitionModel.SearchBy + "' or TO_CHAR (REQUISITION_DATE, 'dd/mm/yyyy') like '" + objFabricRequisitionModel.SearchBy + "')" +
                          "or (TO_CHAR (REQUIRE_DATE, 'dd/mm/yyyy') like '" + objFabricRequisitionModel.SearchBy + "' or TO_CHAR (REQUIRE_DATE, 'dd/mm/yyyy') like '" + objFabricRequisitionModel.SearchBy + "')" +
                          "or (lower(FABRIC_TYPE_NAME) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_TYPE_NAME)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_UNIT_NAME) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_UNIT_NAME)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_WIDTH) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_WIDTH)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(QUANTITY) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(QUANTITY)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(UNIT_PRICE) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(UNIT_PRICE)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(CATEGORY_NAME) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(CATEGORY_NAME)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(SUPPLIER_NAME) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(SUPPLIER_NAME)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(APPROVAL_STATUS) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(APPROVAL_STATUS)like upper('%" + objFabricRequisitionModel.SearchBy + "%') ) )";
            }
            else
            {
                vQuery += "AND TO_CHAR (REQUISITION_DATE, 'DD/MM/YYYY') = TO_CHAR (SYSDATE, 'DD/MM/YYYY')";
            }

            //vQuery += " ORDER BY sl";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objFabricRequisitionModel = new FabricRequisitionModel();

                        objFabricRequisitionModel.FabricRequisitionId = objDataReader["FABRIC_REQUISITION_ID"].ToString();
                        objFabricRequisitionModel.FabricRequisitionCode = objDataReader["FABRIC_REQUISITION_CODE"].ToString();
                        objFabricRequisitionModel.FabricCode = objDataReader["FABRIC_CODE"].ToString();
                        objFabricRequisitionModel.RequisitionDate = objDataReader["REQUISITION_DATE"].ToString();
                        objFabricRequisitionModel.RequireDate = objDataReader["REQUIRE_DATE"].ToString();
                        objFabricRequisitionModel.FabricTypeId = objDataReader["FABRIC_TYPE_ID"].ToString();
                        objFabricRequisitionModel.FabricTypeName = objDataReader["FABRIC_TYPE_NAME"].ToString();
                        objFabricRequisitionModel.FabricUnitId = objDataReader["FABRIC_UNIT_ID"].ToString();
                        objFabricRequisitionModel.FabricUnitName = objDataReader["FABRIC_UNIT_NAME"].ToString();
                        objFabricRequisitionModel.FabricWidth = objDataReader["FABRIC_WIDTH"].ToString();
                        objFabricRequisitionModel.RequisitionQuantity = objDataReader["QUANTITY"].ToString();
                        objFabricRequisitionModel.UnitPrice = objDataReader["UNIT_PRICE"].ToString();
                        //objFabricRequisitionModel.FabricDescription = objDataReader["FABRIC_DESCRIPTION"].ToString();
                        objFabricRequisitionModel.CategoryId = objDataReader["CATEGORY_ID"].ToString();
                        objFabricRequisitionModel.CategoryName = objDataReader["CATEGORY_NAME"].ToString();
                        objFabricRequisitionModel.SupplierId = objDataReader["SUPPLIER_ID"].ToString();
                        objFabricRequisitionModel.SupplierName = objDataReader["SUPPLIER_NAME"].ToString();
                        objFabricRequisitionModel.ApprovalStatus = objDataReader["APPROVAL_STATUS"].ToString();
                        //objFabricRequisitionModel.Remarks = objDataReader["REMARKS"].ToString();
                        objFabricRequisitionModel.PurchaseDateStatus = objDataReader["PURCHASE_DATE_STATUS"].ToString();
                        //objFabricRequisitionModel.CreateBy = objDataReader["CREATE_BY"].ToString();
                        //objFabricRequisitionModel.CreateDate = objDataReader["CREATE_DATE"].ToString();
                        //objFabricRequisitionModel.UpdateBy = objDataReader["UPDATE_BY"].ToString();
                        //objFabricRequisitionModel.UpdateBy = objDataReader["UPDATE_DATE"].ToString();
                        //objFabricRequisitionModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                        //objFabricRequisitionModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();

                        fabricRequisitionList.Add(objFabricRequisitionModel);
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

            return fabricRequisitionList;
        }

        #endregion

        #region Fabric Requisition Approval

        public List<FabricRequisitionModel> GetAllFabricRequisitionForApproval(FabricRequisitionModel objFabricRequisitionModel)
        {
            List<FabricRequisitionModel> fabricRequisitionList = new List<FabricRequisitionModel>();

            string vQuery = "SELECT " +
                            "FABRIC_REQUISITION_ID, " +
                            "FABRIC_REQUISITION_CODE, " +
                            "FABRIC_CODE, " +
                            "TO_CHAR(REQUISITION_DATE, 'dd/mm/yyyy') REQUISITION_DATE, " +
                            "TO_CHAR(REQUIRE_DATE, 'dd/mm/yyyy') REQUIRE_DATE, " +
                            "FABRIC_TYPE_ID, " +
                            "FABRIC_TYPE_NAME, " +
                            "FABRIC_UNIT_ID, " +
                            "FABRIC_UNIT_NAME, " +
                            "FABRIC_WIDTH, " +
                            "QUANTITY, " +
                            "UNIT_PRICE, " +
                            //"FABRIC_DESCRIPTION, " +
                            "CATEGORY_ID, " +
                            "CATEGORY_NAME, " +
                            "SUPPLIER_ID, " +
                            "SUPPLIER_NAME, " +
                            "APPROVAL_STATUS, " +
                            //"REMARKS, " +
                            //"PURCHASE_DATE_STATUS " +
                            "TO_CHAR(PURCHASE_DATE_STATUS, 'dd/mm/yyyy') PURCHASE_DATE_STATUS " +
                            //"CREATE_BY, " +
                            //"CREATE_DATE, " +
                            //"UPDATE_BY, " +
                            //"UPDATE_DATE, " +
                            //"HEAD_OFFICE_ID, " +
                            //"BRANCH_OFFICE_ID " +

                            "FROM VEW_FABRIC_REQUISITION WHERE HEAD_OFFICE_ID = '" + objFabricRequisitionModel.HeadOfficeId + "' and BRANCH_OFFICE_ID = '" + objFabricRequisitionModel.BranchOfficeId + "'  AND APPROVAL_STATUS = 'P' ";


            if (!string.IsNullOrWhiteSpace(objFabricRequisitionModel.SearchBy))
            {
                vQuery += "and ( (lower(FABRIC_REQUISITION_CODE) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_REQUISITION_CODE)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_CODE) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_CODE)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (TO_CHAR (REQUISITION_DATE, 'dd/mm/yyyy') like '" + objFabricRequisitionModel.SearchBy + "' or TO_CHAR (REQUISITION_DATE, 'dd/mm/yyyy') like '" + objFabricRequisitionModel.SearchBy + "')" +
                          "or (TO_CHAR (REQUIRE_DATE, 'dd/mm/yyyy') like '" + objFabricRequisitionModel.SearchBy + "' or TO_CHAR (REQUIRE_DATE, 'dd/mm/yyyy') like '" + objFabricRequisitionModel.SearchBy + "')" +
                          "or (lower(FABRIC_TYPE_NAME) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_TYPE_NAME)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_UNIT_NAME) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_UNIT_NAME)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_WIDTH) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(FABRIC_WIDTH)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(QUANTITY) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(QUANTITY)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(UNIT_PRICE) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(UNIT_PRICE)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(CATEGORY_NAME) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(CATEGORY_NAME)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(SUPPLIER_NAME) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(SUPPLIER_NAME)like upper('%" + objFabricRequisitionModel.SearchBy + "%') )" +
                          "or (lower(APPROVAL_STATUS) like lower( '%" + objFabricRequisitionModel.SearchBy + "%')  or upper(APPROVAL_STATUS)like upper('%" + objFabricRequisitionModel.SearchBy + "%') ) )";
            }
            else
            {
                vQuery += "AND TO_CHAR (REQUISITION_DATE, 'DD/MM/YYYY') = TO_CHAR (SYSDATE, 'DD/MM/YYYY')";
            }

            //vQuery += " ORDER BY sl";

            using (OracleConnection objConnection = GetConnection())
            {
                OracleCommand objCommand = new OracleCommand(vQuery, objConnection);
                objConnection.Open();
                OracleDataReader objDataReader = objCommand.ExecuteReader();

                try
                {
                    while (objDataReader.Read())
                    {
                        objFabricRequisitionModel = new FabricRequisitionModel();

                        objFabricRequisitionModel.FabricRequisitionId = objDataReader["FABRIC_REQUISITION_ID"].ToString();
                        objFabricRequisitionModel.FabricRequisitionCode = objDataReader["FABRIC_REQUISITION_CODE"].ToString();
                        objFabricRequisitionModel.FabricCode = objDataReader["FABRIC_CODE"].ToString();
                        objFabricRequisitionModel.RequisitionDate = objDataReader["REQUISITION_DATE"].ToString();
                        objFabricRequisitionModel.RequireDate = objDataReader["REQUIRE_DATE"].ToString();
                        objFabricRequisitionModel.FabricTypeId = objDataReader["FABRIC_TYPE_ID"].ToString();
                        objFabricRequisitionModel.FabricTypeName = objDataReader["FABRIC_TYPE_NAME"].ToString();
                        objFabricRequisitionModel.FabricUnitId = objDataReader["FABRIC_UNIT_ID"].ToString();
                        objFabricRequisitionModel.FabricUnitName = objDataReader["FABRIC_UNIT_NAME"].ToString();
                        objFabricRequisitionModel.FabricWidth = objDataReader["FABRIC_WIDTH"].ToString();
                        objFabricRequisitionModel.RequisitionQuantity = objDataReader["QUANTITY"].ToString();
                        objFabricRequisitionModel.UnitPrice = objDataReader["UNIT_PRICE"].ToString();
                        //objFabricRequisitionModel.FabricDescription = objDataReader["FABRIC_DESCRIPTION"].ToString();
                        objFabricRequisitionModel.CategoryId = objDataReader["CATEGORY_ID"].ToString();
                        objFabricRequisitionModel.CategoryName = objDataReader["CATEGORY_NAME"].ToString();
                        objFabricRequisitionModel.SupplierId = objDataReader["SUPPLIER_ID"].ToString();
                        objFabricRequisitionModel.SupplierName = objDataReader["SUPPLIER_NAME"].ToString();
                        objFabricRequisitionModel.ApprovalStatus = objDataReader["APPROVAL_STATUS"].ToString();
                        //objFabricRequisitionModel.Remarks = objDataReader["REMARKS"].ToString();
                        objFabricRequisitionModel.PurchaseDateStatus = objDataReader["PURCHASE_DATE_STATUS"].ToString();
                        //objFabricRequisitionModel.CreateBy = objDataReader["CREATE_BY"].ToString();
                        //objFabricRequisitionModel.CreateDate = objDataReader["CREATE_DATE"].ToString();
                        //objFabricRequisitionModel.UpdateBy = objDataReader["UPDATE_BY"].ToString();
                        //objFabricRequisitionModel.UpdateBy = objDataReader["UPDATE_DATE"].ToString();
                        //objFabricRequisitionModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                        //objFabricRequisitionModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();

                        fabricRequisitionList.Add(objFabricRequisitionModel);
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

            return fabricRequisitionList;
        }

        public string ApproveFabricRequisition(FabricRequisitionModel objFabricRequisitionModel)
        {
            using (OracleConnection objConnection = GetConnection())
            {
                using (OracleCommand objCommand = new OracleCommand { CommandText = "PRO_FABRIC_REQUISITION_APPROVE", CommandType = CommandType.StoredProcedure })
                {
                    objCommand.Parameters.Add("p_FABRIC_REQUISITION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.FabricRequisitionId) ? objFabricRequisitionModel.FabricRequisitionId : null;
                    objCommand.Parameters.Add("p_APPROVAL_STATUS_APR", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricRequisitionModel.ApprovalStatus) ? objFabricRequisitionModel.ApprovalStatus : null;

                    objCommand.Parameters.Add("p_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricRequisitionModel.UpdateBy;
                    objCommand.Parameters.Add("p_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricRequisitionModel.HeadOfficeId;
                    objCommand.Parameters.Add("p_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricRequisitionModel.BranchOfficeId;

                    objCommand.Parameters.Add("p_Message", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;


                    try
                    {
                        objCommand.Connection = objConnection;
                        objConnection.Open();
                        trans = objConnection.BeginTransaction();
                        objCommand.ExecuteNonQuery();
                        trans.Commit();
                        objConnection.Close();

                        return objCommand.Parameters["p_Message"].Value.ToString();
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
            }
        }

        #endregion

        #region Fabric Purchase

        public IEnumerable<FabricPurchaseModel> GetAllFabricPurchase(FabricPurchaseModel objFabricPurchaseModel)
        {
            string vQuery = "SELECT " +
                            "FABRIC_PURCHASE_ID, " +
                            "FABRIC_REQUISITION_ID, " +
                            "FABRIC_REQUISITION_CODE, " +
                            "TO_CHAR(PURCHASE_DATE, 'dd/mm/yyyy') PURCHASE_DATE, " +
                            "SUPPLIER_ID, " +
                            "SUPPLIER_NAME, " +
                            "LOCATION_ID, " +
                            "LOCATION_NAME, " +
                            "FABRIC_UNIT_ID, " +
                            "FABRIC_UNIT_NAME, " +
                            "FABRIC_WIDTH, " +
                            "QUANTITY, " +
                            "UNIT_PRICE, " +
                            "LAB_TEST_ID, " +
                            "LAB_TEST_NAME, " +
                            "REMARKS, " +
                            "DELIVER_RECEIVE_STATUS, " +
                            "CREATE_BY, " +
                            "CREATE_DATE, " +
                            "UPDATE_BY, " +
                            "UPDATE_DATE, " +
                            "HEAD_OFFICE_ID, " +
                            "BRANCH_OFFICE_ID " +
                            "FROM VEW_FABRIC_PURCHASE WHERE HEAD_OFFICE_ID = '" + objFabricPurchaseModel.HeadOfficeId + "' and BRANCH_OFFICE_ID = '" + objFabricPurchaseModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objFabricPurchaseModel.SearchBy))
            {
                vQuery += "and ( (lower(FABRIC_REQUISITION_CODE) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(FABRIC_REQUISITION_CODE)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (TO_CHAR (PURCHASE_DATE, 'dd/mm/yyyy') like '" + objFabricPurchaseModel.SearchBy + "' or TO_CHAR (PURCHASE_DATE, 'dd/mm/yyyy') like '" + objFabricPurchaseModel.SearchBy + "')" +
                          "or (lower(SUPPLIER_NAME) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(SUPPLIER_NAME)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (lower(LOCATION_NAME) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(LOCATION_NAME)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_UNIT_NAME) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(FABRIC_UNIT_NAME)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_WIDTH) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(FABRIC_WIDTH)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (lower(QUANTITY) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(QUANTITY)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (lower(UNIT_PRICE) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(UNIT_PRICE)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (lower(LAB_TEST_NAME) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(LAB_TEST_NAME)like upper('%" + objFabricPurchaseModel.SearchBy + "%') ) )";
            }
            else
            {
                vQuery += "AND PURCHASE_DATE = TO_DATE(SYSDATE)";
            }

            //vQuery += "AND PURCHASE_DATE = TO_DATE(SYSDATE)";

            using (OracleConnection objConnection = GetConnection())
            {
                using (OracleCommand objCommand = new OracleCommand(vQuery, objConnection) { CommandText = vQuery, Connection = objConnection, CommandType = CommandType.Text })
                {
                    objConnection.Open();

                    using (OracleDataReader objDataReader = objCommand.ExecuteReader())
                    {
                        try
                        {
                            List<FabricPurchaseModel> objFabricPurchaseList = new List<FabricPurchaseModel>();

                            while (objDataReader.Read())
                            {
                                objFabricPurchaseModel = new FabricPurchaseModel();

                                objFabricPurchaseModel.FabricPurchaseId = objDataReader["FABRIC_PURCHASE_ID"].ToString();
                                objFabricPurchaseModel.FabricRequisitionId = objDataReader["FABRIC_REQUISITION_ID"].ToString();
                                objFabricPurchaseModel.FabricRequisitionCode = objDataReader["FABRIC_REQUISITION_CODE"].ToString();
                                objFabricPurchaseModel.PurchaseDate = objDataReader["PURCHASE_DATE"].ToString();
                                objFabricPurchaseModel.SupplierId = objDataReader["SUPPLIER_ID"].ToString();
                                objFabricPurchaseModel.SupplierName = objDataReader["SUPPLIER_NAME"].ToString();
                                objFabricPurchaseModel.LocationId = objDataReader["LOCATION_ID"].ToString();
                                objFabricPurchaseModel.LocationName = objDataReader["LOCATION_NAME"].ToString();
                                objFabricPurchaseModel.FabricUnitId = objDataReader["FABRIC_UNIT_ID"].ToString();
                                objFabricPurchaseModel.FabricUnitName = objDataReader["FABRIC_UNIT_NAME"].ToString();
                                objFabricPurchaseModel.FabricWidth = objDataReader["FABRIC_WIDTH"].ToString();
                                objFabricPurchaseModel.Quantity = objDataReader["QUANTITY"].ToString();
                                objFabricPurchaseModel.UnitPrice = objDataReader["UNIT_PRICE"].ToString();
                                objFabricPurchaseModel.LabTestId = objDataReader["LAB_TEST_ID"].ToString();
                                objFabricPurchaseModel.LabTestName = objDataReader["LAB_TEST_NAME"].ToString();
                                objFabricPurchaseModel.Remarks = objDataReader["REMARKS"].ToString();
                                objFabricPurchaseModel.DeliverReceiveStatus = objDataReader["DELIVER_RECEIVE_STATUS"].ToString();
                                objFabricPurchaseModel.CreateBy = objDataReader["CREATE_BY"].ToString();
                                objFabricPurchaseModel.CreateDate = objDataReader["CREATE_DATE"].ToString();
                                objFabricPurchaseModel.UpdateBy = objDataReader["UPDATE_BY"].ToString();
                                objFabricPurchaseModel.UpdateDate = objDataReader["UPDATE_DATE"].ToString();
                                objFabricPurchaseModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                                objFabricPurchaseModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();

                                objFabricPurchaseList.Add(objFabricPurchaseModel);
                            }

                            return objFabricPurchaseList;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Exception: " + ex.Message);
                        }
                        finally
                        {
                            objDataReader.Dispose();
                            objCommand.Dispose();
                            objConnection.Dispose();
                        }
                    }
                }
            }
        }

        public IEnumerable<object> GetAllFabricRequisitionCodes(FabricPurchaseModel objFabricPurchaseModel)
        {
            //string vQuery = "SELECT " +
            //                "FABRIC_REQUISITION_ID, " +
            //                "FABRIC_REQUISITION_CODE " +
            //                "FROM VEW_FABRIC_REQUISITION WHERE HEAD_OFFICE_ID = '" + objFabricPurchaseModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objFabricPurchaseModel.BranchOfficeId + "' AND APPROVAL_STATUS = 'A' " +
            //                "AND FABRIC_REQUISITION_ID NOT IN (SELECT FABRIC_REQUISITION_ID FROM FABRIC_PURCHASE)" +
            //                "AND (LOWER(FABRIC_REQUISITION_CODE) LIKE LOWER( '%" + objFabricPurchaseModel.SearchBy + "%')  OR UPPER(FABRIC_REQUISITION_CODE) LIKE UPPER('%" + objFabricPurchaseModel.SearchBy + "%') )";

            string vQuery = "SELECT * " +
                            "FROM VEW_FABRIC_REQ_APPROVED WHERE HEAD_OFFICE_ID = '" + objFabricPurchaseModel.HeadOfficeId + "' AND BRANCH_OFFICE_ID = '" + objFabricPurchaseModel.BranchOfficeId + "' " +
                            "AND (LOWER(FABRIC_REQUISITION_CODE) LIKE LOWER( '%" + objFabricPurchaseModel.SearchBy + "%')  OR UPPER(FABRIC_REQUISITION_CODE) LIKE UPPER('%" + objFabricPurchaseModel.SearchBy + "%') )";


            using (OracleConnection objConnection = GetConnection())
            {
                using (OracleCommand objCommand = new OracleCommand(vQuery, objConnection))
                {
                    objConnection.Open();

                    using (OracleDataReader objDataReader = objCommand.ExecuteReader())
                    {
                        try
                        {
                            List<object> objFabricRequisitionCollection = new List<object>();

                            while (objDataReader.Read())
                            {
                                objFabricRequisitionCollection.Add(new
                                {
                                    Id = objDataReader["FABRIC_REQUISITION_ID"].ToString(),
                                    Code = objDataReader["FABRIC_REQUISITION_CODE"].ToString()
                                });
                            }

                            return objFabricRequisitionCollection;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Exception: " + ex.Message);
                        }
                        finally
                        {
                            objDataReader.Dispose();
                            objCommand.Dispose();
                            objConnection.Dispose();
                        }
                    }
                }
            }
        }

        public FabricPurchaseModel GetFabricPurchaseByRequisitionId(FabricPurchaseModel objFabricPurchaseModel)
        {
            string vQuery = "SELECT " +
                            "FABRIC_REQUISITION_ID, " +
                            "FABRIC_REQUISITION_CODE, " +
                            "FABRIC_UNIT_ID, " +
                            "FABRIC_UNIT_NAME, " +
                            "FABRIC_WIDTH, " +
                            "QUANTITY, " +
                            "UNIT_PRICE, " +
                            "SUPPLIER_ID, " +
                            "SUPPLIER_NAME, " +
                            "REMARKS, " +
                            "CREATE_BY, " +
                            "CREATE_DATE, " +
                            "UPDATE_BY, " +
                            "UPDATE_DATE, " +
                            "HEAD_OFFICE_ID, " +
                            "BRANCH_OFFICE_ID " +
                            "FROM VEW_FABRIC_REQUISITION WHERE HEAD_OFFICE_ID = '" + objFabricPurchaseModel.HeadOfficeId + "' and BRANCH_OFFICE_ID = '" + objFabricPurchaseModel.BranchOfficeId + "' " +
                            "AND APPROVAL_STATUS = 'A' AND FABRIC_REQUISITION_ID = '" + objFabricPurchaseModel.FabricRequisitionId + "'";


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

                        objFabricPurchaseModel = new FabricPurchaseModel();

                        objFabricPurchaseModel.FabricRequisitionId = objDataReader["FABRIC_REQUISITION_ID"].ToString();
                        objFabricPurchaseModel.FabricRequisitionCode = objDataReader["FABRIC_REQUISITION_CODE"].ToString();
                        objFabricPurchaseModel.FabricUnitId = objDataReader["FABRIC_UNIT_ID"].ToString();
                        objFabricPurchaseModel.FabricUnitName = objDataReader["FABRIC_UNIT_NAME"].ToString();
                        objFabricPurchaseModel.FabricWidth = objDataReader["FABRIC_WIDTH"].ToString();
                        objFabricPurchaseModel.Quantity = objDataReader["QUANTITY"].ToString();
                        objFabricPurchaseModel.UnitPrice = objDataReader["UNIT_PRICE"].ToString();
                        objFabricPurchaseModel.SupplierId = objDataReader["SUPPLIER_ID"].ToString();
                        objFabricPurchaseModel.SupplierName = objDataReader["SUPPLIER_NAME"].ToString();
                        objFabricPurchaseModel.CreateBy = objDataReader["CREATE_BY"].ToString();
                        objFabricPurchaseModel.CreateDate = objDataReader["CREATE_DATE"].ToString();
                        objFabricPurchaseModel.UpdateBy = objDataReader["UPDATE_BY"].ToString();
                        objFabricPurchaseModel.UpdateBy = objDataReader["UPDATE_DATE"].ToString();
                        objFabricPurchaseModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                        objFabricPurchaseModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objDataReader.Dispose();
                    objCommand.Dispose();
                    objConnection.Dispose();
                }
            }

            return objFabricPurchaseModel;
        }

        public FabricPurchaseModel GetFabricPurchaseById(FabricPurchaseModel objFabricPurchaseModel)
        {
            string vQuery = "SELECT " +
                            "FABRIC_PURCHASE_ID, " +
                            "FABRIC_REQUISITION_ID, " +
                            "FABRIC_REQUISITION_CODE, " +
                            "TO_CHAR(PURCHASE_DATE, 'dd/mm/yyyy') PURCHASE_DATE, " +
                            "FABRIC_UNIT_ID, " +
                            "FABRIC_UNIT_NAME, " +
                            "FABRIC_WIDTH, " +
                            "QUANTITY, " +
                            "UNIT_PRICE, " +
                            "SUPPLIER_ID, " +
                            "SUPPLIER_NAME, " +
                            "LOCATION_ID, " +
                            "LOCATION_NAME, " +
                            "LAB_TEST_ID, " +
                            "LAB_TEST_NAME, " +
                            "REMARKS, " +
                            "DELIVER_RECEIVE_STATUS, " +
                            "CREATE_BY, " +
                            "CREATE_DATE, " +
                            "UPDATE_BY, " +
                            "UPDATE_DATE, " +
                            "HEAD_OFFICE_ID, " +
                            "BRANCH_OFFICE_ID " +
                            "FROM VEW_FABRIC_PURCHASE WHERE HEAD_OFFICE_ID = '" + objFabricPurchaseModel.HeadOfficeId + "' and BRANCH_OFFICE_ID = '" + objFabricPurchaseModel.BranchOfficeId + "' " +
                            "AND FABRIC_PURCHASE_ID = '" + objFabricPurchaseModel.FabricPurchaseId + "'";


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

                        objFabricPurchaseModel = new FabricPurchaseModel();

                        objFabricPurchaseModel.FabricPurchaseId = objDataReader["FABRIC_PURCHASE_ID"].ToString();
                        objFabricPurchaseModel.FabricRequisitionId = objDataReader["FABRIC_REQUISITION_ID"].ToString();
                        objFabricPurchaseModel.FabricRequisitionCode = objDataReader["FABRIC_REQUISITION_CODE"].ToString();
                        objFabricPurchaseModel.PurchaseDate = objDataReader["PURCHASE_DATE"].ToString();
                        objFabricPurchaseModel.FabricUnitId = objDataReader["FABRIC_UNIT_ID"].ToString();
                        objFabricPurchaseModel.FabricUnitName = objDataReader["FABRIC_UNIT_NAME"].ToString();
                        objFabricPurchaseModel.FabricWidth = objDataReader["FABRIC_WIDTH"].ToString();
                        objFabricPurchaseModel.Quantity = objDataReader["QUANTITY"].ToString();
                        objFabricPurchaseModel.UnitPrice = objDataReader["UNIT_PRICE"].ToString();
                        objFabricPurchaseModel.SupplierId = objDataReader["SUPPLIER_ID"].ToString();
                        objFabricPurchaseModel.SupplierName = objDataReader["SUPPLIER_NAME"].ToString();
                        objFabricPurchaseModel.LocationId = objDataReader["LOCATION_ID"].ToString();
                        objFabricPurchaseModel.LocationName = objDataReader["LOCATION_NAME"].ToString();
                        objFabricPurchaseModel.LabTestId = objDataReader["LAB_TEST_ID"].ToString();
                        objFabricPurchaseModel.LabTestName = objDataReader["LAB_TEST_NAME"].ToString();
                        objFabricPurchaseModel.Remarks = objDataReader["REMARKS"].ToString();
                        objFabricPurchaseModel.DeliverReceiveStatus = objDataReader["DELIVER_RECEIVE_STATUS"].ToString();
                        objFabricPurchaseModel.CreateBy = objDataReader["CREATE_BY"].ToString();
                        objFabricPurchaseModel.CreateDate = objDataReader["CREATE_DATE"].ToString();
                        objFabricPurchaseModel.UpdateBy = objDataReader["UPDATE_BY"].ToString();
                        objFabricPurchaseModel.UpdateBy = objDataReader["UPDATE_DATE"].ToString();
                        objFabricPurchaseModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                        objFabricPurchaseModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error : " + ex.Message);
                }
                finally
                {
                    objDataReader.Dispose();
                    objCommand.Dispose();
                    objConnection.Dispose();
                }
            }

            return objFabricPurchaseModel;
        }

        public string SaveFabricPurchase(FabricPurchaseModel objFabricPurchaseModel)
        {
            using (OracleConnection objConnection = GetConnection())
            {
                using (OracleCommand objCommand = new OracleCommand { CommandText = "PRO_FABRIC_PURCHASE_SAVE", CommandType = CommandType.StoredProcedure, Connection = objConnection })
                {
                    objCommand.Parameters.Add("p_FABRIC_PURCHASE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricPurchaseModel.FabricPurchaseId) ? objFabricPurchaseModel.FabricPurchaseId : null;
                    objCommand.Parameters.Add("p_FABRIC_REQUISITION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricPurchaseModel.FabricRequisitionId) ? objFabricPurchaseModel.FabricRequisitionId : null;
                    objCommand.Parameters.Add("p_PURCHASE_DATE", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.PurchaseDate.Length >= 10 ? objFabricPurchaseModel.PurchaseDate : null;
                    objCommand.Parameters.Add("p_SUPPLIER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricPurchaseModel.SupplierId) ? objFabricPurchaseModel.SupplierId : null;
                    objCommand.Parameters.Add("p_LOCATION_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricPurchaseModel.LocationId) ? objFabricPurchaseModel.LocationId : null;
                    objCommand.Parameters.Add("p_FABRIC_UNIT_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricPurchaseModel.FabricUnitId) ? objFabricPurchaseModel.FabricUnitId : null;
                    objCommand.Parameters.Add("p_FABRIC_WIDTH", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricPurchaseModel.FabricWidth) ? objFabricPurchaseModel.FabricWidth : null;
                    objCommand.Parameters.Add("p_QUANTITY", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricPurchaseModel.Quantity) ? objFabricPurchaseModel.Quantity : null;
                    objCommand.Parameters.Add("p_UNIT_PRICE", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricPurchaseModel.UnitPrice) ? objFabricPurchaseModel.UnitPrice : null;
                    objCommand.Parameters.Add("p_LAB_TEST_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricPurchaseModel.LabTestId) ? objFabricPurchaseModel.LabTestId : null;
                    objCommand.Parameters.Add("p_REMARKS", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricPurchaseModel.Remarks) ? objFabricPurchaseModel.Remarks : null;

                    objCommand.Parameters.Add("p_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.UpdateBy;
                    objCommand.Parameters.Add("p_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.HeadOfficeId;
                    objCommand.Parameters.Add("p_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.BranchOfficeId;

                    objCommand.Parameters.Add("p_Message", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;

                    objConnection.Open();

                    using (trans = objConnection.BeginTransaction())
                    {
                        try
                        {
                            objCommand.ExecuteNonQuery();
                            trans.Commit();

                            return objCommand.Parameters["p_Message"].Value.ToString();
                        }
                        catch (Exception ex)
                        {
                            trans.Rollback();
                            throw new Exception("Error: " + ex.Message);
                        }
                        finally
                        {
                            trans.Dispose();
                            objCommand.Dispose();
                            objConnection.Dispose();
                        }
                    }
                }
            }
        }


        #endregion

        #region Fabric Authorizer

        public IEnumerable<FabricAuthorizerModel> GetAllFabricAuthorizer(FabricAuthorizerModel objFabricAuthorizerModel)
        {
            string vQuery = "SELECT " +
                            "FABRIC_AUTHORIZER_ID, " +
                            "EMPLOYEE_ID, " +
                            "EMPLOYEE_NAME, " +
                            "EMPLOYEE_IMAGE_FILE_NAME, " +
                            "EMPLOYEE_IMAGE_FILE_SIZE, " +
                            "EMPLOYEE_IMAGE_FILE_EXTENSION, " +
                            "UPDATE_BY, " +
                            "UPDATE_DATE, " +
                            "HEAD_OFFICE_ID, " +
                            "BRANCH_OFFICE_ID " +
                            "FROM VEW_FABRIC_AUTHORIZER WHERE HEAD_OFFICE_ID = '" + objFabricAuthorizerModel.HeadOfficeId + "' and BRANCH_OFFICE_ID = '" + objFabricAuthorizerModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objFabricAuthorizerModel.SearchBy))
            {
                vQuery += "and ( (lower(FABRIC_AUTHORIZER_ID) like lower( '%" + objFabricAuthorizerModel.SearchBy + "%')  or upper(FABRIC_AUTHORIZER_ID)like upper('%" + objFabricAuthorizerModel.SearchBy + "%') )" +
                          "or (lower(EMPLOYEE_ID) like lower( '%" + objFabricAuthorizerModel.SearchBy + "%')  or upper(EMPLOYEE_ID)like upper('%" + objFabricAuthorizerModel.SearchBy + "%') )" +
                          "or (lower(EMPLOYEE_NAME) like lower( '%" + objFabricAuthorizerModel.SearchBy + "%')  or upper(EMPLOYEE_NAME)like upper('%" + objFabricAuthorizerModel.SearchBy + "%') ) )";
            }


            using (OracleConnection objConnection = GetConnection())
            {
                using (OracleCommand objCommand = new OracleCommand(vQuery, objConnection) { CommandText = vQuery, Connection = objConnection, CommandType = CommandType.Text })
                {
                    objConnection.Open();

                    using (OracleDataReader objDataReader = objCommand.ExecuteReader())
                    {
                        try
                        {
                            List<FabricAuthorizerModel> objFabricAuthorizerList = new List<FabricAuthorizerModel>();

                            while (objDataReader.Read())
                            {
                                objFabricAuthorizerModel = new FabricAuthorizerModel();
                                objFabricAuthorizerModel.EmployeeModel = new EmployeeModel();

                                objFabricAuthorizerModel.FabricAuthorizerId = objDataReader["FABRIC_AUTHORIZER_ID"].ToString();
                                objFabricAuthorizerModel.EmployeeModel.EmployeeId = objDataReader["EMPLOYEE_ID"].ToString();
                                objFabricAuthorizerModel.EmployeeModel.EmployeeName = objDataReader["EMPLOYEE_NAME"].ToString();
                                objFabricAuthorizerModel.EmployeeModel.EmployeeImage = objDataReader["EMPLOYEE_IMAGE_FILE_SIZE"] == DBNull.Value ? new byte[0] : (byte[])objDataReader["EMPLOYEE_IMAGE_FILE_SIZE"];
                                objFabricAuthorizerModel.UpdateBy = objDataReader["UPDATE_BY"].ToString();
                                objFabricAuthorizerModel.UpdateDate = objDataReader["UPDATE_DATE"].ToString();
                                objFabricAuthorizerModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                                objFabricAuthorizerModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();

                                objFabricAuthorizerList.Add(objFabricAuthorizerModel);
                            }

                            return objFabricAuthorizerList;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Exception: " + ex.Message);
                        }
                        finally
                        {
                            objDataReader.Dispose();
                            objCommand.Dispose();
                            objConnection.Dispose();
                        }
                    }
                }
            }
        }

        public FabricAuthorizerModel GetFabricAuthorizerById(FabricAuthorizerModel objFabricAuthorizerModel)
        {
            string vQuery = "SELECT " +
                            "FABRIC_AUTHORIZER_ID, " +
                            "EMPLOYEE_ID, " +
                            "EMPLOYEE_NAME, " +
                            "EMAIL_ID, " +
                            "CONTACT_NO, " +
                            "PRESENT_DESIGNATION_NAME, " +
                            "UNIT_NAME, " +
                            "PRESENT_ADDRESS, " +
                            "EMPLOYMENT_STATUS, " +
                            "EMPLOYEE_IMAGE_FILE_NAME, " +
                            "EMPLOYEE_IMAGE_FILE_SIZE, " +
                            "EMPLOYEE_IMAGE_FILE_EXTENSION, " +
                            "UPDATE_BY, " +
                            "UPDATE_DATE, " +
                            "HEAD_OFFICE_ID, " +
                            "BRANCH_OFFICE_ID " +
                            "FROM VEW_FABRIC_AUTHORIZER WHERE HEAD_OFFICE_ID = '" + objFabricAuthorizerModel.HeadOfficeId + "' and BRANCH_OFFICE_ID = '" + objFabricAuthorizerModel.BranchOfficeId + "' " +
                            "AND FABRIC_AUTHORIZER_ID = '" + objFabricAuthorizerModel.FabricAuthorizerId + "'";


            using (OracleConnection objConnection = GetConnection())
            {
                using (OracleCommand objCommand = new OracleCommand { CommandText = vQuery, Connection = objConnection, CommandType = CommandType.Text })
                {
                    objConnection.Open();

                    using (OracleDataReader objDataReader = objCommand.ExecuteReader())
                    {
                        try
                        {
                            if (objDataReader.HasRows)
                            {
                                objDataReader.Read();

                                objFabricAuthorizerModel = new FabricAuthorizerModel();
                                objFabricAuthorizerModel.EmployeeModel = new EmployeeModel();

                                objFabricAuthorizerModel.FabricAuthorizerId = objDataReader["FABRIC_AUTHORIZER_ID"].ToString();
                                objFabricAuthorizerModel.EmployeeModel.EmployeeId = objDataReader["EMPLOYEE_ID"].ToString();
                                objFabricAuthorizerModel.EmployeeModel.EmployeeName = objDataReader["EMPLOYEE_NAME"].ToString();
                                objFabricAuthorizerModel.EmployeeModel.EmailAddress = objDataReader["EMAIL_ID"].ToString();
                                objFabricAuthorizerModel.EmployeeModel.ContactNo = objDataReader["CONTACT_NO"].ToString();
                                objFabricAuthorizerModel.EmployeeModel.PresentDesignationId = objDataReader["PRESENT_DESIGNATION_NAME"].ToString();
                                objFabricAuthorizerModel.EmployeeModel.UnitId = objDataReader["UNIT_NAME"].ToString();
                                objFabricAuthorizerModel.EmployeeModel.PresentAddress = objDataReader["PRESENT_ADDRESS"].ToString();
                                objFabricAuthorizerModel.EmployeeModel.EmploymentStatus = objDataReader["EMPLOYMENT_STATUS"].ToString();
                                objFabricAuthorizerModel.EmployeeModel.EmployeeImage = objDataReader["EMPLOYEE_IMAGE_FILE_SIZE"] == DBNull.Value ? new byte[0] : (byte[])objDataReader["EMPLOYEE_IMAGE_FILE_SIZE"];
                                objFabricAuthorizerModel.UpdateBy = objDataReader["UPDATE_BY"].ToString();
                                objFabricAuthorizerModel.UpdateDate = objDataReader["UPDATE_DATE"].ToString();
                                objFabricAuthorizerModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                                objFabricAuthorizerModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error : " + ex.Message);
                        }
                        finally
                        {
                            objDataReader.Dispose();
                            objCommand.Dispose();
                            objConnection.Dispose();
                        }
                    }
                }
            }

            return objFabricAuthorizerModel;
        }

        public string SaveFabricAuthorizer(FabricAuthorizerModel objFabricAuthorizerModel)
        {
            using (OracleConnection objConnection = GetConnection())
            {
                using (OracleCommand objCommand = new OracleCommand { CommandText = "PRO_FABRIC_AUTHORIZER", CommandType = CommandType.StoredProcedure })
                {
                    objCommand.Parameters.Add("p_FABRIC_AUTHORIZER_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricAuthorizerModel.FabricAuthorizerId) ? objFabricAuthorizerModel.FabricAuthorizerId : null;
                    objCommand.Parameters.Add("p_EMPLOYEE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(objFabricAuthorizerModel.EmployeeModel.EmployeeId) ? objFabricAuthorizerModel.EmployeeModel.EmployeeId : null;

                    objCommand.Parameters.Add("p_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricAuthorizerModel.UpdateBy;
                    objCommand.Parameters.Add("p_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricAuthorizerModel.HeadOfficeId;
                    objCommand.Parameters.Add("p_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricAuthorizerModel.BranchOfficeId;

                    objCommand.Parameters.Add("p_Message", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;


                    try
                    {
                        objCommand.Connection = objConnection;
                        objConnection.Open();
                        trans = objConnection.BeginTransaction();
                        objCommand.ExecuteNonQuery();
                        trans.Commit();
                        objConnection.Close();

                        return objCommand.Parameters["p_Message"].Value.ToString();
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
            }
        }

        #endregion

        #region Fabric Delivery

        public string DeliverFabric(FabricPurchaseModel objFabricPurchaseModel)
        {
            string vMessage = null;

            using (OracleConnection objConnection = GetConnection())
            {
                using (OracleCommand objCommand = new OracleCommand { CommandText = "PRO_FABRIC_DELIVER", CommandType = CommandType.StoredProcedure, Connection = objConnection })
                {
                    objConnection.Open();

                    try
                    {
                        foreach (string pFabricPurchaseId in objFabricPurchaseModel.FabricPurchaseIdList)
                        {
                            objCommand.Parameters.Clear();

                            objCommand.Parameters.Add("p_FABRIC_PURCHASE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(pFabricPurchaseId) ? pFabricPurchaseId : null;

                            objCommand.Parameters.Add("p_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.UpdateBy;
                            objCommand.Parameters.Add("p_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.HeadOfficeId;
                            objCommand.Parameters.Add("p_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.BranchOfficeId;

                            objCommand.Parameters.Add("p_Message", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;


                            try
                            {
                                trans = objConnection.BeginTransaction();
                                objCommand.ExecuteNonQuery();
                                trans.Commit();

                                vMessage = objCommand.Parameters["p_Message"].Value.ToString();
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                throw new Exception("Error : " + ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        trans.Dispose();
                        objCommand.Dispose();
                        objConnection.Close();
                        objConnection.Dispose();
                    }
                }
            }

            return vMessage;
        }

        #endregion

        #region Fabric Receive

        public IEnumerable<FabricPurchaseModel> GetAllFabricDelivered(FabricPurchaseModel objFabricPurchaseModel)
        {
            string vQuery = "SELECT " +
                            "FABRIC_PURCHASE_ID, " +
                            "FABRIC_REQUISITION_ID, " +
                            "FABRIC_REQUISITION_CODE, " +
                            "TO_CHAR(PURCHASE_DATE, 'dd/mm/yyyy') PURCHASE_DATE, " +
                            "SUPPLIER_ID, " +
                            "SUPPLIER_NAME, " +
                            "LOCATION_ID, " +
                            "LOCATION_NAME, " +
                            "FABRIC_UNIT_ID, " +
                            "FABRIC_UNIT_NAME, " +
                            "FABRIC_WIDTH, " +
                            "QUANTITY, " +
                            "UNIT_PRICE, " +
                            "LAB_TEST_ID, " +
                            "LAB_TEST_NAME, " +
                            "REMARKS, " +
                            "DELIVER_RECEIVE_STATUS, " +
                            "CREATE_BY, " +
                            "CREATE_DATE, " +
                            "UPDATE_BY, " +
                            "UPDATE_DATE, " +
                            "HEAD_OFFICE_ID, " +
                            "BRANCH_OFFICE_ID " +
                            "FROM VEW_FABRIC_DELIVERED WHERE HEAD_OFFICE_ID = '" + objFabricPurchaseModel.HeadOfficeId + "' and BRANCH_OFFICE_ID = '" + objFabricPurchaseModel.BranchOfficeId + "' ";

            if (!string.IsNullOrWhiteSpace(objFabricPurchaseModel.SearchBy))
            {
                vQuery += "and ( (lower(FABRIC_REQUISITION_CODE) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(FABRIC_REQUISITION_CODE)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (TO_CHAR (PURCHASE_DATE, 'dd/mm/yyyy') like '" + objFabricPurchaseModel.SearchBy + "' or TO_CHAR (PURCHASE_DATE, 'dd/mm/yyyy') like '" + objFabricPurchaseModel.SearchBy + "')" +
                          "or (lower(SUPPLIER_NAME) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(SUPPLIER_NAME)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (lower(LOCATION_NAME) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(LOCATION_NAME)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_UNIT_NAME) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(FABRIC_UNIT_NAME)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (lower(FABRIC_WIDTH) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(FABRIC_WIDTH)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (lower(QUANTITY) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(QUANTITY)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (lower(UNIT_PRICE) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(UNIT_PRICE)like upper('%" + objFabricPurchaseModel.SearchBy + "%') )" +
                          "or (lower(LAB_TEST_NAME) like lower( '%" + objFabricPurchaseModel.SearchBy + "%')  or upper(LAB_TEST_NAME)like upper('%" + objFabricPurchaseModel.SearchBy + "%') ) )";
            }
            else
            {
                vQuery += "AND DELIVER_RECEIVE_STATUS = 'D'";
            }


            using (OracleConnection objConnection = GetConnection())
            {
                using (OracleCommand objCommand = new OracleCommand(vQuery, objConnection) { CommandText = vQuery, Connection = objConnection, CommandType = CommandType.Text })
                {
                    objConnection.Open();

                    using (OracleDataReader objDataReader = objCommand.ExecuteReader())
                    {
                        try
                        {
                            List<FabricPurchaseModel> objFabricPurchaseList = new List<FabricPurchaseModel>();

                            while (objDataReader.Read())
                            {
                                objFabricPurchaseModel = new FabricPurchaseModel();

                                objFabricPurchaseModel.FabricPurchaseId = objDataReader["FABRIC_PURCHASE_ID"].ToString();
                                objFabricPurchaseModel.FabricRequisitionId = objDataReader["FABRIC_REQUISITION_ID"].ToString();
                                objFabricPurchaseModel.FabricRequisitionCode = objDataReader["FABRIC_REQUISITION_CODE"].ToString();
                                objFabricPurchaseModel.PurchaseDate = objDataReader["PURCHASE_DATE"].ToString();
                                objFabricPurchaseModel.SupplierId = objDataReader["SUPPLIER_ID"].ToString();
                                objFabricPurchaseModel.SupplierName = objDataReader["SUPPLIER_NAME"].ToString();
                                objFabricPurchaseModel.LocationId = objDataReader["LOCATION_ID"].ToString();
                                objFabricPurchaseModel.LocationName = objDataReader["LOCATION_NAME"].ToString();
                                objFabricPurchaseModel.FabricUnitId = objDataReader["FABRIC_UNIT_ID"].ToString();
                                objFabricPurchaseModel.FabricUnitName = objDataReader["FABRIC_UNIT_NAME"].ToString();
                                objFabricPurchaseModel.FabricWidth = objDataReader["FABRIC_WIDTH"].ToString();
                                objFabricPurchaseModel.Quantity = objDataReader["QUANTITY"].ToString();
                                objFabricPurchaseModel.UnitPrice = objDataReader["UNIT_PRICE"].ToString();
                                objFabricPurchaseModel.LabTestId = objDataReader["LAB_TEST_ID"].ToString();
                                objFabricPurchaseModel.LabTestName = objDataReader["LAB_TEST_NAME"].ToString();
                                objFabricPurchaseModel.Remarks = objDataReader["REMARKS"].ToString();
                                objFabricPurchaseModel.DeliverReceiveStatus = objDataReader["DELIVER_RECEIVE_STATUS"].ToString();
                                objFabricPurchaseModel.CreateBy = objDataReader["CREATE_BY"].ToString();
                                objFabricPurchaseModel.CreateDate = objDataReader["CREATE_DATE"].ToString();
                                objFabricPurchaseModel.UpdateBy = objDataReader["UPDATE_BY"].ToString();
                                objFabricPurchaseModel.UpdateDate = objDataReader["UPDATE_DATE"].ToString();
                                objFabricPurchaseModel.HeadOfficeId = objDataReader["HEAD_OFFICE_ID"].ToString();
                                objFabricPurchaseModel.BranchOfficeId = objDataReader["BRANCH_OFFICE_ID"].ToString();

                                objFabricPurchaseList.Add(objFabricPurchaseModel);
                            }

                            return objFabricPurchaseList;
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Exception: " + ex.Message);
                        }
                        finally
                        {
                            objDataReader.Dispose();
                            objCommand.Dispose();
                            objConnection.Dispose();
                        }
                    }
                }
            }
        }

        public string ReceiveFabric(FabricPurchaseModel objFabricPurchaseModel)
        {
            string vMessage = null;

            using (OracleConnection objConnection = GetConnection())
            {
                using (OracleCommand objCommand = new OracleCommand { CommandText = "PRO_FABRIC_RECEIVE", CommandType = CommandType.StoredProcedure, Connection = objConnection })
                {
                    objConnection.Open();

                    try
                    {
                        foreach (string pFabricPurchaseId in objFabricPurchaseModel.FabricPurchaseIdList)
                        {
                            objCommand.Parameters.Clear();

                            objCommand.Parameters.Add("p_FABRIC_PURCHASE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(pFabricPurchaseId) ? pFabricPurchaseId : null;

                            objCommand.Parameters.Add("p_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.UpdateBy;
                            objCommand.Parameters.Add("p_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.HeadOfficeId;
                            objCommand.Parameters.Add("p_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.BranchOfficeId;

                            objCommand.Parameters.Add("p_Message", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;


                            try
                            {
                                trans = objConnection.BeginTransaction();
                                objCommand.ExecuteNonQuery();
                                trans.Commit();

                                vMessage = objCommand.Parameters["p_Message"].Value.ToString();
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                throw new Exception("Error : " + ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        trans.Dispose();
                        objCommand.Dispose();
                        objConnection.Close();
                        objConnection.Dispose();
                    }
                }
            }

            return vMessage;
        }

        public string ReturnMismatchFabric(FabricPurchaseModel objFabricPurchaseModel)
        {
            string vMessage = null;

            using (OracleConnection objConnection = GetConnection())
            {
                using (OracleCommand objCommand = new OracleCommand { CommandText = "PRO_FABRIC_MISMATCH", CommandType = CommandType.StoredProcedure, Connection = objConnection })
                {
                    objConnection.Open();

                    try
                    {
                        foreach (string pFabricPurchaseId in objFabricPurchaseModel.FabricPurchaseIdList)
                        {
                            objCommand.Parameters.Clear();

                            objCommand.Parameters.Add("p_FABRIC_PURCHASE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = !string.IsNullOrWhiteSpace(pFabricPurchaseId) ? pFabricPurchaseId : null;

                            objCommand.Parameters.Add("p_UPDATE_BY", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.UpdateBy;
                            objCommand.Parameters.Add("p_HEAD_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.HeadOfficeId;
                            objCommand.Parameters.Add("p_BRANCH_OFFICE_ID", OracleDbType.Varchar2, ParameterDirection.Input).Value = objFabricPurchaseModel.BranchOfficeId;

                            objCommand.Parameters.Add("p_Message", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output;


                            try
                            {
                                trans = objConnection.BeginTransaction();
                                objCommand.ExecuteNonQuery();
                                trans.Commit();

                                vMessage = objCommand.Parameters["p_Message"].Value.ToString();
                            }
                            catch (Exception ex)
                            {
                                trans.Rollback();
                                throw new Exception("Error : " + ex.Message);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        throw new Exception("Error : " + ex.Message);
                    }
                    finally
                    {
                        trans.Dispose();
                        objCommand.Dispose();
                        objConnection.Close();
                        objConnection.Dispose();
                    }
                }
            }

            return vMessage;
        }

        #endregion


    }
}
