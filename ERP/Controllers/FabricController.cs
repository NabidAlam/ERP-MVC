using System;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.DAL;
using ERP.MODEL;
using ERP.Utility;
using PagedList;

namespace ERP.Controllers
{
    public class FabricController : Controller
    {
        FabricDAL objFabricDAL = new FabricDAL();
        LookUpDAL objLookUpDAL = new LookUpDAL();
        ReportDAL objReportDAL = new ReportDAL();
        EmployeeDAL objEmployeeDAL = new EmployeeDAL();

        ReportDocument objReportDocument = new ReportDocument();
        ExportFormatType objExportFormatType = ExportFormatType.NoFormat;

        #region Common

        string strEmployeeId = "";
        string strDesignationId = "";
        string strSubSectionId = "";
        string strUnitId = "";
        string strHeadOfficeId = "";
        string strBranchOfficeId = "";
        string strSoftId = "";
        string strOldUrl = "";

        public void CheckUrl()
        {
            string strCurrentUrl = Request.Url.AbsoluteUri.ToString();

            if (strCurrentUrl.Contains("?"))
            {
                strCurrentUrl = strCurrentUrl.Substring(0, strCurrentUrl.IndexOf('?'));
            }


            if (strCurrentUrl != strOldUrl)
            {
                TempData["SearchValue"] = string.Empty;

                Session["strOldUrl"] = strCurrentUrl;
            }

        }

        public void LoadSession()
        {
            strEmployeeId = Session["strEmployeeId"].ToString();
            strSubSectionId = Session["strSubSectionId"].ToString();
            strDesignationId = Session["strDesignationId"].ToString();
            strUnitId = Session["strUnitId"].ToString();
            strHeadOfficeId = Session["strHeadOfficeId"].ToString();
            strBranchOfficeId = Session["strBranchOfficeId"].ToString();
            strSoftId = Session["strSoftId"].ToString();
            if (Session["strOldUrl"] != null)
            {
                strOldUrl = Session["strOldUrl"].ToString().Trim();
            }
        }

        #endregion


        #region Report

        public FileStreamResult ShowReport(string pReportType, string pFileDownloadName)
        {
            string vContentType;

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Clear();
            Response.Buffer = true;

            if (pReportType == "PDF")
            {
                objExportFormatType = ExportFormatType.PortableDocFormat;

                vContentType = "application/pdf";

                pFileDownloadName += ".pdf";
            }
            else if (pReportType == "Excel")
            {
                objExportFormatType = ExportFormatType.Excel;

                vContentType = "application/vnd.ms-excel";

                pFileDownloadName += ".xls";
            }
            else if (pReportType == "CSV")
            {
                objExportFormatType = ExportFormatType.CharacterSeparatedValues;

                vContentType = "application/vnd.ms-excel";

                pFileDownloadName += ".csv";
            }

            //else if (pReportType == "DOC")
            else
            {
                objExportFormatType = ExportFormatType.WordForWindows;

                vContentType = "application/doc";
                Response.AppendHeader("Content-Disposition", $"filename={pFileDownloadName}.doc");

                pFileDownloadName += ".doc";
            }

            Stream objStream = objReportDocument.ExportToStream(objExportFormatType);
            byte[] objBufferredData = new byte[objStream.Length];
            objStream.Read(objBufferredData, 0, Convert.ToInt32(objStream.Length - 1));

            Response.ContentType = vContentType;

            Response.BinaryWrite(objBufferredData);
            Response.Flush();
            Response.Close();
            objReportDocument.Close();
            objReportDocument.Dispose();

            return File(objStream, Response.ContentType, pFileDownloadName);
        }

        #endregion



        #region Fabric Requisition

        [HttpGet]
        public ActionResult FabricRequisitionEntry(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                FabricRequisitionModel objFabricRequisitionModel = new FabricRequisitionModel
                {
                    RequisitionDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objFabricRequisitionModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objFabricRequisitionModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objFabricRequisitionModel.FabricRequisitionId = pId;
                    objFabricRequisitionModel = objFabricDAL.GetFabricRequisitionById(objFabricRequisitionModel);

                    page = (int)TempData["GetActionPage"];
                    TempData.Keep("GetActionPage");
                }

                if (TempData.ContainsKey("SaveActionFlag") && (int)TempData["SaveActionFlag"] == 1)
                {
                    page = (int)TempData["SaveActionPage"];
                    TempData.Keep("SaveActionPage");
                }

                TempData["GetActionFlag"] = 1;
                TempData["GetActionPage"] = page;

                #endregion

                ViewBag.CategoryDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCategoryDDListForFabric(), "CATEGORY_ID", "CATEGORY_NAME");
                ViewBag.SupplierDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSupplierDDList(), "SUPPLIER_ID", "SUPPLIER_NAME");
                ViewBag.FabricTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetFabricTypeDDList(), "FABRIC_TYPE_ID", "FABRIC_TYPE_NAME");
                ViewBag.FabricUnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetFabricUnitDDList(), "UNIT_ID", "UNIT_NAME");

                ViewBag.FabricRequisitionList = objFabricDAL.GetAllFabricRequisition(objFabricRequisitionModel).ToPagedList(page, pageSize);

                return View(objFabricRequisitionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FabricRequisitionEntry(FabricRequisitionModel objFabricRequisitionModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objFabricRequisitionModel.UpdateBy = strEmployeeId;
                objFabricRequisitionModel.HeadOfficeId = strHeadOfficeId;
                objFabricRequisitionModel.BranchOfficeId = strBranchOfficeId;

                objFabricRequisitionModel.SwatchFileSize = UtilityClass.ConvertImageToByteArray(objFabricRequisitionModel.HttpPostedFileBase);
                objFabricRequisitionModel.SwatchFileName = objFabricRequisitionModel.HttpPostedFileBase?.FileName;
                objFabricRequisitionModel.SwatchFileExtension = objFabricRequisitionModel.HttpPostedFileBase?.ContentType;

                if (ModelState.IsValid)
                {
                    TempData["OperationMessage"] = objFabricDAL.SaveFabricRequisition(objFabricRequisitionModel);
                }

                #region Pagination Search

                int page = (int)TempData["GetActionPage"];

                if (page >= 1)
                {
                    TempData["SaveActionPage"] = page;
                    TempData.Keep("GetActionPage");
                }

                TempData["SaveActionFlag"] = 1;

                #endregion

                return RedirectToAction("FabricRequisitionEntry", "Fabric");
            }
        }


        public ActionResult ClearFabricRequisition()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("FabricRequisitionEntry", "Fabric");
        }

        #endregion

        #region Fabric Requisition Status

        [HttpGet]
        public ActionResult FabricRequisitionStatus(FabricRequisitionModel objFabricRequisitionModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objFabricRequisitionModel.UpdateBy = strEmployeeId;
                objFabricRequisitionModel.HeadOfficeId = strHeadOfficeId;
                objFabricRequisitionModel.BranchOfficeId = strBranchOfficeId;

                ViewBag.FabricRequisitionList = objFabricDAL.GetAllFabricRequisitionForStatus(objFabricRequisitionModel);

                return View();
            }
        }

        [HttpGet]
        public ActionResult FabricRequisitionReport(FabricRequisitionModel objFabricRequisitionModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objFabricRequisitionModel.UpdateBy = strEmployeeId;
                objFabricRequisitionModel.HeadOfficeId = strHeadOfficeId;
                objFabricRequisitionModel.BranchOfficeId = strBranchOfficeId;

                DataSet objDataSet = objReportDAL.GetFabricRequisitionById(objFabricRequisitionModel);

                string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptFabricRequisition.rpt"));
                objReportDocument.Load(vReportPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                return ShowReport("PDF", "Fabric Requisition");
            }
        }

        [HttpGet]
        public ActionResult FabricRequisitionPurchaseReport(FabricRequisitionModel objFabricRequisitionModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objFabricRequisitionModel.UpdateBy = strEmployeeId;
                objFabricRequisitionModel.HeadOfficeId = strHeadOfficeId;
                objFabricRequisitionModel.BranchOfficeId = strBranchOfficeId;

                DataSet objDataSet = objReportDAL.GetFabricRequisitionPurchaseById(objFabricRequisitionModel);

                string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptFabricRequisitionPurchase.rpt"));
                objReportDocument.Load(vReportPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                return ShowReport("PDF", "Fabric Requisition Purchase");
            }
        }

        public ActionResult ClearFabricRequisitionStatus()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("FabricRequisitionStatus", "Fabric");
        }

        #endregion

        #region Fabric Requisition Approval

        [HttpGet]
        public ActionResult FabricRequisitionApproval(string SearchBy)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                FabricRequisitionModel objFabricRequisitionModel = new FabricRequisitionModel
                {
                    SearchBy = SearchBy,
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                ViewBag.FabricRequisitionStatusList = objFabricDAL.GetAllFabricRequisitionForApproval(objFabricRequisitionModel);

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FabricRequisitionApproval(FabricRequisitionModel objFabricRequisitionModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objFabricRequisitionModel.UpdateBy = strEmployeeId;
                objFabricRequisitionModel.HeadOfficeId = strHeadOfficeId;
                objFabricRequisitionModel.BranchOfficeId = strBranchOfficeId;

                if (!string.IsNullOrWhiteSpace(objFabricRequisitionModel.FabricRequisitionId))
                {
                    TempData["OperationMessage"] = objFabricDAL.ApproveFabricRequisition(objFabricRequisitionModel);
                }

                return RedirectToAction("FabricRequisitionApproval", "Fabric", new { objFabricRequisitionModel.SearchBy });
            }
        }

        public ActionResult ClearFabricRequisitionApproval()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("FabricRequisitionApproval", "Fabric");
        }

        #endregion

        #region Fabric Purchase

        [HttpGet]
        public ActionResult FabricPurchase(string pFabricRequisitionId, string SearchBy)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                FabricPurchaseModel objFabricPurchaseModel = new FabricPurchaseModel
                {
                    FabricRequisitionId = pFabricRequisitionId,
                    PurchaseDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    SearchBy = SearchBy,
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                if (!string.IsNullOrWhiteSpace(objFabricPurchaseModel.FabricRequisitionId))
                {
                    objFabricPurchaseModel = objFabricDAL.GetFabricPurchaseByRequisitionId(objFabricPurchaseModel);
                }


                ViewBag.SupplierDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSupplierDDList(), "SUPPLIER_ID", "SUPPLIER_NAME");
                ViewBag.LocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLocationDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");
                ViewBag.FabricTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetFabricTypeDDList(), "FABRIC_TYPE_ID", "FABRIC_TYPE_NAME");
                ViewBag.FabricUnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetFabricUnitDDList(), "UNIT_ID", "UNIT_NAME");
                ViewBag.LocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLocationDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");
                ViewBag.CategoryDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCategoryDDListForFabric(), "CATEGORY_ID", "CATEGORY_NAME");
                ViewBag.DesignerDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDesignerDDList(), "EMPLOYEE_ID", "EMPLOYEE_NAME");
                ViewBag.LabTestDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLabTestDDList(), "CRITERIA_ID", "CRITERIA_NAME");

                ViewBag.FabricPurchaseList = objFabricDAL.GetAllFabricPurchase(objFabricPurchaseModel);

                return View(objFabricPurchaseModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FabricPurchase(FabricPurchaseModel objFabricPurchaseModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objFabricPurchaseModel.UpdateBy = strEmployeeId;
                objFabricPurchaseModel.HeadOfficeId = strHeadOfficeId;
                objFabricPurchaseModel.BranchOfficeId = strBranchOfficeId;

                if (ModelState.IsValid)
                {
                    TempData["OperationMessage"] = objFabricDAL.SaveFabricPurchase(objFabricPurchaseModel);
                }

                return RedirectToAction("FabricPurchase", "Fabric");
            }
        }

        [HttpGet]
        public ActionResult ClearFabricPurchase()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("FabricPurchase", "Fabric");
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetFabricRequisitionCodes(string pSearchBy)
        {
            LoadSession();

            FabricPurchaseModel objFabricPurchaseModel = new FabricPurchaseModel
            {
                SearchBy = pSearchBy,
                HeadOfficeId = strHeadOfficeId,
                BranchOfficeId = strBranchOfficeId
            };

            return Json(objFabricDAL.GetAllFabricRequisitionCodes(objFabricPurchaseModel), JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Fabric Purchase Update

        [HttpGet]
        public ActionResult UpdateFabricPurchase(string pFabricPurchaseId, string SearchBy)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                FabricPurchaseModel objFabricPurchaseModel = new FabricPurchaseModel
                {
                    FabricPurchaseId = pFabricPurchaseId,
                    PurchaseDate = DateTime.Now.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture),
                    SearchBy = SearchBy,
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                if (!string.IsNullOrWhiteSpace(objFabricPurchaseModel.FabricPurchaseId))
                {
                    objFabricPurchaseModel = objFabricDAL.GetFabricPurchaseById(objFabricPurchaseModel);
                }

                ViewBag.SupplierDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSupplierDDList(), "SUPPLIER_ID", "SUPPLIER_NAME");
                ViewBag.LocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLocationDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");
                ViewBag.FabricTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetFabricTypeDDList(), "FABRIC_TYPE_ID", "FABRIC_TYPE_NAME");
                ViewBag.FabricUnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetFabricUnitDDList(), "UNIT_ID", "UNIT_NAME");
                ViewBag.LocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLocationDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");
                ViewBag.CategoryDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCategoryDDListForFabric(), "CATEGORY_ID", "CATEGORY_NAME");
                ViewBag.DesignerDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDesignerDDList(), "EMPLOYEE_ID", "EMPLOYEE_NAME");
                ViewBag.LabTestDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLabTestDDList(), "CRITERIA_ID", "CRITERIA_NAME");

                ViewBag.FabricPurchaseList = objFabricDAL.GetAllFabricPurchase(objFabricPurchaseModel);

                return View(objFabricPurchaseModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateFabricPurchase(FabricPurchaseModel objFabricPurchaseModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objFabricPurchaseModel.UpdateBy = strEmployeeId;
                objFabricPurchaseModel.HeadOfficeId = strHeadOfficeId;
                objFabricPurchaseModel.BranchOfficeId = strBranchOfficeId;

                if (ModelState.IsValid)
                {
                    TempData["OperationMessage"] = objFabricDAL.SaveFabricPurchase(objFabricPurchaseModel);
                }

                return RedirectToAction("UpdateFabricPurchase", "Fabric");
            }
        }

        [HttpGet]
        public ActionResult ClearUpdateFabricPurchase()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("UpdateFabricPurchase", "Fabric");
        }

        #endregion

        #region Fabric Authorizer

        [HttpGet]
        public ActionResult AuthorizeFabricAuthorizer(string pEmployeeId, string pFabricAuthorizerId, string SearchBy)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                FabricAuthorizerModel objFabricAuthorizerModel = new FabricAuthorizerModel
                {
                    FabricAuthorizerId = pFabricAuthorizerId,
                    SearchBy = SearchBy,
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                if (!string.IsNullOrWhiteSpace(objFabricAuthorizerModel.FabricAuthorizerId))
                {
                    objFabricAuthorizerModel = objFabricDAL.GetFabricAuthorizerById(objFabricAuthorizerModel);
                }

                if (!string.IsNullOrWhiteSpace(pEmployeeId))
                {
                    objFabricAuthorizerModel.EmployeeModel = objEmployeeDAL.GetEmployeeProfileById(pEmployeeId, strHeadOfficeId, strBranchOfficeId);

                    if (objFabricAuthorizerModel.EmployeeModel == null)
                    {
                        TempData["OperationMessage"] = "No employee exists by " + pEmployeeId;
                    }
                }

                ViewBag.FabricAuthorizerList = objFabricDAL.GetAllFabricAuthorizer(objFabricAuthorizerModel);

                return View(objFabricAuthorizerModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AuthorizeFabricAuthorizer(FabricAuthorizerModel objFabricAuthorizerModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objFabricAuthorizerModel.UpdateBy = strEmployeeId;
                objFabricAuthorizerModel.HeadOfficeId = strHeadOfficeId;
                objFabricAuthorizerModel.BranchOfficeId = strBranchOfficeId;

                if (ModelState.IsValidField("EmployeeId"))
                {
                    TempData["OperationMessage"] = objFabricDAL.SaveFabricAuthorizer(objFabricAuthorizerModel);
                }

                return RedirectToAction("AuthorizeFabricAuthorizer", "Fabric", new { objFabricAuthorizerModel.SearchBy });
            }
        }

        #endregion

        #region Fabric Delivery

        [HttpGet]
        public ActionResult FabricDeliver(string SearchBy)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                FabricPurchaseModel objFabricPurchaseModel = new FabricPurchaseModel
                {
                    SearchBy = SearchBy,
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                ViewBag.SupplierDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSupplierDDList(), "SUPPLIER_ID", "SUPPLIER_NAME");
                ViewBag.LocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLocationDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");
                ViewBag.LabTestDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLabTestDDList(), "CRITERIA_ID", "CRITERIA_NAME");

                ViewBag.FabricPurchaseList = objFabricDAL.GetAllFabricPurchase(objFabricPurchaseModel);

                return View(objFabricPurchaseModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FabricDeliver(FabricPurchaseModel objFabricPurchaseModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objFabricPurchaseModel.UpdateBy = strEmployeeId;
                objFabricPurchaseModel.HeadOfficeId = strHeadOfficeId;
                objFabricPurchaseModel.BranchOfficeId = strBranchOfficeId;

                if (objFabricPurchaseModel.FabricPurchaseIdList != null && objFabricPurchaseModel.FabricPurchaseIdList.Count > 0)
                {
                    TempData["OperationMessage"] = objFabricDAL.DeliverFabric(objFabricPurchaseModel);
                }

                return RedirectToAction("FabricDeliver", "Fabric", new { objFabricPurchaseModel.SearchBy });
            }
        }

        #endregion

        #region Fabric Receive

        [HttpGet]
        public ActionResult FabricReceive(string SearchBy)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                FabricPurchaseModel objFabricPurchaseModel = new FabricPurchaseModel
                {
                    SearchBy = SearchBy,
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                ViewBag.SupplierDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSupplierDDList(), "SUPPLIER_ID", "SUPPLIER_NAME");
                ViewBag.LocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLocationDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");
                ViewBag.LabTestDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLabTestDDList(), "CRITERIA_ID", "CRITERIA_NAME");

                ViewBag.FabricPurchaseList = objFabricDAL.GetAllFabricDelivered(objFabricPurchaseModel);

                return View(objFabricPurchaseModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FabricReceive(FabricPurchaseModel objFabricPurchaseModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objFabricPurchaseModel.UpdateBy = strEmployeeId;
                objFabricPurchaseModel.HeadOfficeId = strHeadOfficeId;
                objFabricPurchaseModel.BranchOfficeId = strBranchOfficeId;

                if (objFabricPurchaseModel.FabricPurchaseIdList?.Count > 0)
                {
                    TempData["OperationMessage"] = objFabricDAL.ReceiveFabric(objFabricPurchaseModel);
                }

                return RedirectToAction("FabricReceive", "Fabric", new { objFabricPurchaseModel.SearchBy });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FabricMismatch(FabricPurchaseModel objFabricPurchaseModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objFabricPurchaseModel.UpdateBy = strEmployeeId;
                objFabricPurchaseModel.HeadOfficeId = strHeadOfficeId;
                objFabricPurchaseModel.BranchOfficeId = strBranchOfficeId;

                if (objFabricPurchaseModel.FabricPurchaseIdList != null && objFabricPurchaseModel.FabricPurchaseIdList.Count > 0)
                {
                    TempData["OperationMessage"] = objFabricDAL.ReturnMismatchFabric(objFabricPurchaseModel);
                }

                return RedirectToAction("FabricReceive", "Fabric", new { objFabricPurchaseModel.SearchBy });
            }
        }

        #endregion

    }
}