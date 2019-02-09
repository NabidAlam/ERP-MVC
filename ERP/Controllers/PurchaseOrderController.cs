using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.DAL;
using ERP.MODEL;
using System.Data;
using PagedList;
using System.Net;
using ERP.Utility;
using System.IO;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace ERP.Controllers
{
    public class PurchaseOrderController : Controller
    {
        PurchaseOrderModel objPurchaseOrderModel = new PurchaseOrderModel();
        PurchaseOrderDAL objPurchaseOrderDAL = new PurchaseOrderDAL();
        LookUpDAL objLookUpDAL = new LookUpDAL();
        ReportDAL objReportDAL = new ReportDAL();
        ReportDocument objReportDocument = new ReportDocument();
        ExportFormatType objExportFormatType = ExportFormatType.NoFormat;

        #region "Common"
        string strEmployeeId = "";
        string strDesignationId = "";
        string strSubSectionId = "";
        string strUnitId = "";
        string strHeadOfficeId = "";
        string strBranchOfficeId = "";
        string strSoftId = "";
        string strOldUrl = "";
        StreamReader objStreamReader;
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

        public void LoadDropDownList()
        {
            ViewBag.SizeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSizeDDList(), "SIZE_ID", "SIZE_NAME");
            ViewBag.CurrencyDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCurrencyDDList(), "CURRENCY_ID", "CURRENCY_NAME");
            ViewBag.OrderTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetOrderTypeDDList(), "ORDER_TYPE_ID", "ORDER_TYPE_NAME");
            ViewBag.ModeOfShipmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetModeOfShipmentDDList(), "MODE_OF_SHIPMENT_ID", "MODE_OF_SHIPMENT_NAME");
            ViewBag.LodingPortDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetPortOfLandingtDDList(), "PORT_OF_LOADING_ID", "PORT_OF_LOADING_NAME");


        }

        public void getDirectoryName()
        {

            objPurchaseOrderModel = objLookUpDAL.getDirectoryName();

        }

        #endregion

        #region"Purchase Order Entry"

        public ActionResult PurchaseOrderEntry(string pId, string pEditFlag, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                LoadDropDownList();
                ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();
                objPurchaseOrderModel.UpdateBy = strEmployeeId;
                objPurchaseOrderModel.HeadOfficeId = strHeadOfficeId;
                objPurchaseOrderModel.BranchOfficeId = strBranchOfficeId;
                int vRowFound;
                #region Pagination Search
                CheckUrl();
                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    bool exist = SearchBy.Contains("|");
                    if (exist)
                    {
                        string a = SearchBy;
                        string Invoice = a.Substring(0, a.IndexOf(" | ")).Trim();
                        int PositionAfterInvoice = a.IndexOf(" | ") + 2;

                        string b = a.Substring(PositionAfterInvoice);
                        string FileName = b.Substring(0, b.IndexOf(" | ")).Trim();
                        int PositionAfterFileName = b.IndexOf(" | ") + 2;

                        string UploadDate = b.Substring(PositionAfterFileName).Trim();

                        objPurchaseOrderModel.InvoiceNumber = Invoice;
                        objPurchaseOrderModel.FileName = FileName;
                        objPurchaseOrderModel.UploadDate = UploadDate;
                    }
                    else
                    {
                        TempData["SearchFlag"] = 1;
                        TempData["SearchPage"] = page;
                        TempData["SearchValue"] = SearchBy;
                    }

                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objPurchaseOrderModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }
                if (!string.IsNullOrWhiteSpace(pId))
                {
                    if (!string.IsNullOrEmpty(pEditFlag) && pEditFlag == "1")
                    {
                        if (!string.IsNullOrEmpty(pId))
                        {
                            objPurchaseOrderModel.InvoiceNumber = pId.Trim();

                        }
                        DataTable dt = objPurchaseOrderDAL.GetPurchaseOrderRecord(objPurchaseOrderModel);
                        vRowFound = dt.Rows.Count;
                        ViewBag.RowNo = vRowFound;
                        List<PurchaseOrderModel> PurchaseOrderList = PurchaseOrderListData(dt);
                        ViewBag.PurchaseOrderEntryListEdit = PurchaseOrderList;
                       
                    }
                    page = (int)TempData["GetActionPage"];
                    TempData.Keep("GetActionPage");
                }

                if (TempData.ContainsKey("SaveActionFlag") && (int)TempData["SaveActionFlag"] == 1)
                {
                    if (TempData.ContainsKey("InvoiceNumber"))
                    {
                        objPurchaseOrderModel.InvoiceNumber = TempData["InvoiceNumber"].ToString();
                    }
                    page = (int)TempData["SaveActionPage"];
                    TempData.Keep("SaveActionPage");
                    DataTable dt = objPurchaseOrderDAL.GetPurchaseOrderRecord(objPurchaseOrderModel);
                    vRowFound = dt.Rows.Count;
                    ViewBag.RowNo = vRowFound;
                    List<PurchaseOrderModel> PurchaseOrderList = PurchaseOrderListData(dt);
                    ViewBag.PurchaseOrderEntryListEdit = PurchaseOrderList;
                }


                if (TempData.ContainsKey("DeleteActionFlag") && (int)TempData["DeleteActionFlag"] == 1)
                {
                    if (TempData.ContainsKey("InvoiceNumber"))
                    {
                        objPurchaseOrderModel.InvoiceNumber = TempData["InvoiceNumber"].ToString();
                    }
                    page = (int)TempData["DeleteActionPage"];
                    TempData.Keep("DeleteActionPage");
                    DataTable dt = objPurchaseOrderDAL.GetPurchaseOrderRecord(objPurchaseOrderModel);
                    vRowFound = dt.Rows.Count;
                    ViewBag.RowNo = vRowFound;
                    List<PurchaseOrderModel> PurchaseOrderList = PurchaseOrderListData(dt);
                    ViewBag.PurchaseOrderEntryListEdit = PurchaseOrderList;
                }


                TempData["GetActionFlag"] = 1;
                TempData["GetActionPage"] = page;

                #endregion
                //show all data to last table from main
                DataTable dt1 = objPurchaseOrderDAL.GetPurchaseOrderRecordMain (objPurchaseOrderModel);
                List<PurchaseOrderModel> PurchaseOrderListMain = PurchaseOrderListDataMain(dt1);              
                ViewBag.PurchaseOrderEntryList = PurchaseOrderListMain.ToPagedList(page, pageSize);
                return View(objPurchaseOrderModel);
            }

        }

        public List<PurchaseOrderModel> PurchaseOrderListData(DataTable dt)
        {
            List<PurchaseOrderModel> PurchaseOrderListDataBundle = new List<PurchaseOrderModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PurchaseOrderModel objPurchaseOrderModel = new PurchaseOrderModel();
                objPurchaseOrderModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objPurchaseOrderModel.InvoiceNumber = dt.Rows[i]["INVOICE_NO"].ToString();
                objPurchaseOrderModel.GridTranId = dt.Rows[i]["TRAN_ID"].ToString();
                objPurchaseOrderModel.GridOrderCreationDate = dt.Rows[i]["ORDER_CREATION_DATE"].ToString();
                objPurchaseOrderModel.GridOrderNumber = dt.Rows[i]["ORDER_NO"].ToString();
                objPurchaseOrderModel.GridSupplierHandoverDate = dt.Rows[i]["HAND_OVER_DATE"].ToString();
                objPurchaseOrderModel.GridOrdertypeId = dt.Rows[i]["ORDER_TYPE_ID"].ToString();
                objPurchaseOrderModel.GridOrdertypeName = dt.Rows[i]["ORDER_TYPE_NAME"].ToString();
                objPurchaseOrderModel.GridModel = dt.Rows[i]["MODEL_NO"].ToString();
                objPurchaseOrderModel.GridDescription = dt.Rows[i]["ITEM_DESCRIPTION"].ToString();
                objPurchaseOrderModel.GridItem = dt.Rows[i]["ITEM_CODE"].ToString();
                objPurchaseOrderModel.GridSizeId = dt.Rows[i]["SIZE_ID"].ToString();
                objPurchaseOrderModel.GridSizeName = dt.Rows[i]["SIZE_NAME"].ToString();
                objPurchaseOrderModel.GridPCB = dt.Rows[i]["PCB_VALUE"].ToString();
                objPurchaseOrderModel.GridUE = dt.Rows[i]["UE_VALUE"].ToString();
                objPurchaseOrderModel.GridPackaging = dt.Rows[i]["PACKAGING_VALUE"].ToString();
                objPurchaseOrderModel.GridStyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objPurchaseOrderModel.GridOrderedQty = dt.Rows[i]["ORDER_QUANTITY"].ToString();
                objPurchaseOrderModel.GridShippedQty = dt.Rows[i]["SHIP_QUANTITY"].ToString();
                objPurchaseOrderModel.GridRemainningQty = dt.Rows[i]["REMAIN_QUANTITY"].ToString();
                objPurchaseOrderModel.GridUnitPrice = dt.Rows[i]["UNIT_PRICE"].ToString();
                objPurchaseOrderModel.GridTotalPrice = dt.Rows[i]["TOTAL_PRICE"].ToString();
                objPurchaseOrderModel.GridPortOfDestination = dt.Rows[i]["PORT_OF_DISTINATION"].ToString();
                objPurchaseOrderModel.GridDeliveryDate = dt.Rows[i]["DELIVERY_DATE"].ToString();
                objPurchaseOrderModel.GridShipmentTypeId = dt.Rows[i]["MODE_OF_SHIPMENT_ID"].ToString();
                objPurchaseOrderModel.GridShipmentTypeName = dt.Rows[i]["MODE_OF_SHIPMENT_NAME"].ToString();
                objPurchaseOrderModel.GridPortOfLandingId = dt.Rows[i]["PORT_OF_LOADING_ID"].ToString();
                objPurchaseOrderModel.GridPortOfLandingName = dt.Rows[i]["PORT_OF_LOADING_NAME"].ToString();
                objPurchaseOrderModel.GridCurrencyId = dt.Rows[i]["CURRENCY_ID"].ToString();
                objPurchaseOrderModel.GridCurrencyName = dt.Rows[i]["CURRENCY_NAME"].ToString();
                objPurchaseOrderModel.GridStatus = dt.Rows[i]["STATUS"].ToString(); 
                objPurchaseOrderModel.GridRemarks = dt.Rows[i]["REMARKS"].ToString();
                objPurchaseOrderModel.GridCopyYn = dt.Rows[i]["COPY_YN"].ToString();
                objPurchaseOrderModel.GridDeleteYn = dt.Rows[i]["DELETE_YN"].ToString();
                objPurchaseOrderModel.GridRejectYn = dt.Rows[i]["REJECT_YN"].ToString();
                objPurchaseOrderModel.GridSeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objPurchaseOrderModel.GridSeason = dt.Rows[i]["SEASON"].ToString();
                PurchaseOrderListDataBundle.Add(objPurchaseOrderModel);
            }

            return PurchaseOrderListDataBundle;
        }

        public List<PurchaseOrderModel> PurchaseOrderListDataMain(DataTable dt1) 
        {
            List<PurchaseOrderModel> PurchaseOrderListDataBundle = new List<PurchaseOrderModel>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                PurchaseOrderModel objPurchaseOrderModel = new PurchaseOrderModel();
                objPurchaseOrderModel.SerialNumber = dt1.Rows[i]["SL"].ToString();
                objPurchaseOrderModel.InvoiceNumber = dt1.Rows[i]["INVOICE_NO"].ToString();
                objPurchaseOrderModel.FileName = dt1.Rows[i]["FILE_NAME"].ToString();
                objPurchaseOrderModel.UploadDate = dt1.Rows[i]["CREATE_DATE"].ToString();
                PurchaseOrderListDataBundle.Add(objPurchaseOrderModel);
            }

            return PurchaseOrderListDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePOEntry(HttpPostedFileBase files, PurchaseOrderModel objPurchaseOrderModel)
        {
            string strDBMsg = "";
            LoadSession();
            getDirectoryName();
            objPurchaseOrderModel.UpdateBy = strEmployeeId;
            objPurchaseOrderModel.HeadOfficeId = strHeadOfficeId;
            objPurchaseOrderModel.BranchOfficeId = strBranchOfficeId;
            if (files != null)
            {
                String FileExt = Path.GetExtension(files.FileName).ToUpper();
                if (FileExt == ".XLSX")
                {
                    objPurchaseOrderModel.FileName = files.FileName;

                    string strFileName = files.FileName;

                    if (System.IO.File.Exists(strFileName))
                    {
                        System.IO.File.Delete(strFileName);
                    }
                    string strFileSavePath = Server.MapPath("~/DATA_CAPTURE/");
                    string strPath = Server.MapPath("~/DATA_CAPTURE/" + files.FileName);
                    files.SaveAs(strPath);

                    MoveFile(strPath, strFileName);

                    strDBMsg = objPurchaseOrderDAL.POFileProcess(objPurchaseOrderModel);
                    if (strDBMsg != "UPDATED SUCCESSFULLY" && strDBMsg != "INSERTED SUCCESSFULLY" && !string.IsNullOrEmpty(strDBMsg))
                    {
                        string input = strDBMsg;
                        string subStr = input.Substring(26);
                        objPurchaseOrderModel.InvoiceNumber = subStr;
                        TempData["InvoiceNumber"] = objPurchaseOrderModel.InvoiceNumber.Trim();
                        string a = strDBMsg;
                        string strMsg = a.Remove(a.Length - 6);
                        TempData["OperationMessage"] = strMsg;
                            
                    }
                    

                }
                else
                {
                    ViewBag.FileStatus = "Invalid file format.Please select excel file";

                }
            }
            else {
                    if (!string.IsNullOrEmpty(objPurchaseOrderModel.InvoiceNumber))
                    {

                        objPurchaseOrderModel = objPurchaseOrderDAL.GetFileName(objPurchaseOrderModel);

                    }
                    strDBMsg = objPurchaseOrderDAL.SavePOEntry(objPurchaseOrderModel);
                    TempData["InvoiceNumber"] = objPurchaseOrderModel.InvoiceNumber.Trim();
                    if (strDBMsg != "UPDATED SUCCESSFULLY" && strDBMsg != "INSERTED SUCCESSFULLY" && !string.IsNullOrEmpty(strDBMsg))
                    {
                        string input = strDBMsg;
                        string subStr = input.Substring(21);
                        objPurchaseOrderModel.InvoiceNumber = subStr;
                        string a = strDBMsg;
                        string strMsg = a.Remove(a.Length - 6);
                        TempData["OperationMessage"] = strMsg;


                    }
                    else {

                        TempData["OperationMessage"] = strDBMsg;
                    }
              }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion
            return RedirectToAction("PurchaseOrderEntry");
            //return View("PurchaseOrderEntry", objPurchaseOrderModel);
        }

        public JsonResult DeletePOEntry(PurchaseOrderModel objPurchaseOrderModel)
        {
            LoadSession();
            string strDBMsg = "";
            string strMsg = "";
            objPurchaseOrderModel.UpdateBy = strEmployeeId;
            objPurchaseOrderModel.HeadOfficeId = strHeadOfficeId;
            objPurchaseOrderModel.BranchOfficeId = strBranchOfficeId;
            strDBMsg = objPurchaseOrderDAL.DeletePOEntry(objPurchaseOrderModel);
            string[] TranIdArray = objPurchaseOrderModel.GridTranId.Split(',');
            int x = TranIdArray.Count();

            for (int i = 0; i < x; i++)
            {
                var arrayTranId = TranIdArray[i];
                if (!string.IsNullOrEmpty(arrayTranId))
                {
                    objPurchaseOrderModel.GridTranId = arrayTranId.Trim();
                    objPurchaseOrderModel = objPurchaseOrderDAL.LoadPODeleteHistory(objPurchaseOrderModel);
                    strMsg = objPurchaseOrderDAL.SavePODeleteHistory(objPurchaseOrderModel); 
                }
            }

                #region Pagination Search

                int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["DeleteActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["DeleteActionFlag"] = 1;
            TempData["DeletestrDBMsg"] = strDBMsg;


            #endregion
            TempData["InvoiceNumber"] = objPurchaseOrderModel.InvoiceNumber.Trim();
            return Json(strDBMsg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ClearPOEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            TempData["InvoiceNumber"] = "";
            return RedirectToAction("PurchaseOrderEntry");
            
        }
        public void MoveFile(string strPath, string strFileName)
        {


            try
            {

                string sourceFile = strPath;
                string strTargetSource = objPurchaseOrderModel.DataUploadDir;
                string destinationFile = objPurchaseOrderModel.DataUploadDir + strFileName;

                if (!Directory.Exists(strTargetSource))
                {
                    Directory.CreateDirectory(strTargetSource);
                }
                // To move a file or folder to a new location:
                //System.IO.File.Move(sourceFile, destinationFile);

                if (System.IO.File.Exists(destinationFile) == true)
                {


                    System.IO.File.Delete(destinationFile);
                    System.IO.File.Copy(sourceFile, destinationFile);

                }
                else
                {

                    System.IO.File.Copy(sourceFile, destinationFile);

                }

                objStreamReader = new StreamReader(strPath, true);
                objStreamReader.Dispose();
                objStreamReader.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();



            }
            catch (Exception ex)
            {
                objStreamReader = new StreamReader(strPath, true);
                objStreamReader.Dispose();
                objStreamReader.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();

            }

        }

        //Auto complete Invoice Search
        public ActionResult GetInvoiceNo()
        {
            LoadSession();
            objPurchaseOrderModel.UpdateBy = strEmployeeId;
            objPurchaseOrderModel.HeadOfficeId = strHeadOfficeId;
            objPurchaseOrderModel.BranchOfficeId = strBranchOfficeId;
            DataTable dt = objPurchaseOrderDAL.GetPurchaseOrderRecordMain(objPurchaseOrderModel);
            List<PurchaseOrderModel> InvoiceList = InvoiceListData(dt);
            return Json(InvoiceList, JsonRequestBehavior.AllowGet);
        }
        public List<PurchaseOrderModel> InvoiceListData(DataTable dt)
        {
            List<PurchaseOrderModel> InvoiceListDataBind = new List<PurchaseOrderModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PurchaseOrderModel objPurchaseOrderModel = new PurchaseOrderModel();
                objPurchaseOrderModel.InvoiceNumber = dt.Rows[i]["INVOICE_NO"].ToString();
                objPurchaseOrderModel.FileName = dt.Rows[i]["FILE_NAME"].ToString();
                objPurchaseOrderModel.UploadDate = dt.Rows[i]["CREATE_DATE"].ToString();
                objPurchaseOrderModel.InvoiceSearch = dt.Rows[i]["INVOICE_NO"] + " | " + dt.Rows[i]["FILE_NAME"] + " | " + dt.Rows[i]["CREATE_DATE"];
                InvoiceListDataBind.Add(objPurchaseOrderModel);
            }

            return InvoiceListDataBind;
        }
        #endregion

        #region"Purchase Order Delete Approved"

        //Delete Approved from Authority
        public ActionResult GetInvoiceNoForApproved()
        {
            LoadSession();
            objPurchaseOrderModel.UpdateBy = strEmployeeId;
            objPurchaseOrderModel.HeadOfficeId = strHeadOfficeId;
            objPurchaseOrderModel.BranchOfficeId = strBranchOfficeId;
            DataTable dt = objPurchaseOrderDAL.GetInvoiceNoForApproved(objPurchaseOrderModel);
            List<PurchaseOrderModel> InvoiceList = InvoiceNoForApprovedListData(dt);
            return Json(InvoiceList, JsonRequestBehavior.AllowGet);
        }

        public List<PurchaseOrderModel> InvoiceNoForApprovedListData(DataTable dt)
        {
            List<PurchaseOrderModel> InvoiceListDataBind = new List<PurchaseOrderModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PurchaseOrderModel objPurchaseOrderModel = new PurchaseOrderModel();
                objPurchaseOrderModel.InvoiceNumber = dt.Rows[i]["INVOICE_NO"].ToString();
                objPurchaseOrderModel.GridStyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objPurchaseOrderModel.GridOrderNumber = dt.Rows[i]["ORDER_NO"].ToString();
                objPurchaseOrderModel.GridModel = dt.Rows[i]["MODEL_NO"].ToString();
                objPurchaseOrderModel.InvoiceSearch = dt.Rows[i]["INVOICE_NO"] + " | " + dt.Rows[i]["STYLE_NO"] + " | " + dt.Rows[i]["ORDER_NO"]+" | "+ dt.Rows[i]["MODEL_NO"];
                InvoiceListDataBind.Add(objPurchaseOrderModel);
            }

            return InvoiceListDataBind;
        }

        public ActionResult PurchaseOrderDeleteApproved(PurchaseOrderModel objPurchaseOrderModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objPurchaseOrderModel.UpdateBy = strEmployeeId;
                objPurchaseOrderModel.HeadOfficeId = strHeadOfficeId;
                objPurchaseOrderModel.BranchOfficeId = strBranchOfficeId;

                if (!string.IsNullOrWhiteSpace(objPurchaseOrderModel.SearchBy))
                {
                    bool exist = objPurchaseOrderModel.SearchBy.Contains("|");
                    if (exist)
                    {
                        string a = objPurchaseOrderModel.SearchBy.Trim();
                        string Invoice = a.Substring(0, a.IndexOf(" | ")).Trim();
                        int PositionAfterInvoice = a.IndexOf(" | ") + 2;

                        string b = a.Substring(PositionAfterInvoice);
                        string Style = b.Substring(0, b.IndexOf(" | ")).Trim();
                        int PositionAfterstyle = b.IndexOf(" | ") + 2;

                        string c = b.Substring(PositionAfterstyle);
                        string OrderNo = c.Substring(0, c.IndexOf(" | ")).Trim();
                        int PositionAfterOrderNo = c.IndexOf(" | ") + 2;

                        string ModelNo = c.Substring(PositionAfterOrderNo).Trim();

                        objPurchaseOrderModel.InvoiceNumber = Invoice;
                        objPurchaseOrderModel.GridStyleNo = Style;
                        objPurchaseOrderModel.GridOrderNumber = OrderNo;
                        objPurchaseOrderModel.GridModel = ModelNo;

                    }
                   
                }


                DataTable dt = objPurchaseOrderDAL.PurchaseOrderDeleteApproved(objPurchaseOrderModel);
                List<PurchaseOrderModel> PurchaseOrderListMain = PurchaseOrderDeleteApprovedList(dt);
                ViewBag.PODataDeletedList = PurchaseOrderListMain;
                if (TempData.ContainsKey("DeleteApprovedMsg"))
                {
                    string deleteMsg = TempData["DeleteApprovedMsg"].ToString();
                    TempData["OperationMessage"] = deleteMsg;

                }
                return View(objPurchaseOrderModel);
            }

        }
        public ActionResult PurchaseOrderApprovedDelete(PurchaseOrderModel objPurchaseOrderModel)
        {
            
                LoadSession();
                string strDBMsg = "";
                objPurchaseOrderModel.UpdateBy = strEmployeeId;
                objPurchaseOrderModel.HeadOfficeId = strHeadOfficeId;
                objPurchaseOrderModel.BranchOfficeId = strBranchOfficeId;
                string[] TranIdArray = objPurchaseOrderModel.GridTranId.Split(',');
                string[] InvoiceArray = objPurchaseOrderModel.InvoiceNumber.Split(',');
                int x = TranIdArray.Count();
                for (int i = 0; i < x; i++)
                {
                    objPurchaseOrderModel.GridTranId = TranIdArray[i].Trim();
                    objPurchaseOrderModel.InvoiceNumber = InvoiceArray[i].Trim();
                    strDBMsg = objPurchaseOrderDAL.PurchaseOrderApprovedDelete(objPurchaseOrderModel);
                    TempData["DeleteApprovedMsg"] = strDBMsg;
            }

                return Json(strDBMsg, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PurchaseOrderApprovedReject(PurchaseOrderModel objPurchaseOrderModel)
        {

            LoadSession();
            string strDBMsg = "";
            objPurchaseOrderModel.UpdateBy = strEmployeeId;
            objPurchaseOrderModel.HeadOfficeId = strHeadOfficeId;
            objPurchaseOrderModel.BranchOfficeId = strBranchOfficeId;
            string[] TranIdArray = objPurchaseOrderModel.GridTranId.Split(',');
            string[] InvoiceArray = objPurchaseOrderModel.InvoiceNumber.Split(',');
            int x = TranIdArray.Count();
            for (int i = 0; i < x; i++)
            {
                objPurchaseOrderModel.GridTranId = TranIdArray[i].Trim();
                objPurchaseOrderModel.InvoiceNumber = InvoiceArray[i].Trim();
                strDBMsg = objPurchaseOrderDAL.PurchaseOrderApprovedReject(objPurchaseOrderModel);
                TempData["DeleteApprovedMsg"] = strDBMsg;
            }

            return Json(strDBMsg, JsonRequestBehavior.AllowGet);
        }
        public List<PurchaseOrderModel> PurchaseOrderDeleteApprovedList(DataTable dt)
        {
            List<PurchaseOrderModel> PurchaseOrderDeleteApprovedListDataBundle = new List<PurchaseOrderModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PurchaseOrderModel objPurchaseOrderModel = new PurchaseOrderModel();
                objPurchaseOrderModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objPurchaseOrderModel.InvoiceNumber = dt.Rows[i]["INVOICE_NO"].ToString();
                objPurchaseOrderModel.GridTranId = dt.Rows[i]["TRAN_ID"].ToString();
                objPurchaseOrderModel.GridOrderCreationDate = dt.Rows[i]["ORDER_CREATION_DATE"].ToString();
                objPurchaseOrderModel.GridOrderNumber = dt.Rows[i]["ORDER_NO"].ToString();
                objPurchaseOrderModel.GridSupplierHandoverDate = dt.Rows[i]["HAND_OVER_DATE"].ToString();
                objPurchaseOrderModel.GridOrdertypeId = dt.Rows[i]["ORDER_TYPE_ID"].ToString();
                objPurchaseOrderModel.GridOrdertypeName = dt.Rows[i]["ORDER_TYPE_NAME"].ToString();
                objPurchaseOrderModel.GridModel = dt.Rows[i]["MODEL_NO"].ToString();
                objPurchaseOrderModel.GridDescription = dt.Rows[i]["ITEM_DESCRIPTION"].ToString();
                objPurchaseOrderModel.GridItem = dt.Rows[i]["ITEM_CODE"].ToString();
                objPurchaseOrderModel.GridSizeId = dt.Rows[i]["SIZE_ID"].ToString();
                objPurchaseOrderModel.GridSizeName = dt.Rows[i]["SIZE_NAME"].ToString().Trim();
                objPurchaseOrderModel.GridPCB = dt.Rows[i]["PCB_VALUE"].ToString();
                objPurchaseOrderModel.GridUE = dt.Rows[i]["UE_VALUE"].ToString();
                objPurchaseOrderModel.GridPackaging = dt.Rows[i]["PACKAGING_VALUE"].ToString();
                objPurchaseOrderModel.GridStyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objPurchaseOrderModel.GridOrderedQty = dt.Rows[i]["ORDER_QUANTITY"].ToString();
                objPurchaseOrderModel.GridShippedQty = dt.Rows[i]["SHIP_QUANTITY"].ToString();
                objPurchaseOrderModel.GridRemainningQty = dt.Rows[i]["REMAIN_QUANTITY"].ToString();
                objPurchaseOrderModel.GridUnitPrice = dt.Rows[i]["UNIT_PRICE"].ToString();
                objPurchaseOrderModel.GridTotalPrice = dt.Rows[i]["TOTAL_PRICE"].ToString();
                objPurchaseOrderModel.GridPortOfDestination = dt.Rows[i]["PORT_OF_DISTINATION"].ToString();
                objPurchaseOrderModel.GridDeliveryDate = dt.Rows[i]["DELIVERY_DATE"].ToString();
                objPurchaseOrderModel.GridShipmentTypeId = dt.Rows[i]["MODE_OF_SHIPMENT_ID"].ToString();
                objPurchaseOrderModel.GridShipmentTypeName = dt.Rows[i]["MODE_OF_SHIPMENT_NAME"].ToString();
                objPurchaseOrderModel.GridPortOfLandingId = dt.Rows[i]["PORT_OF_LOADING_ID"].ToString();
                objPurchaseOrderModel.GridPortOfLandingName = dt.Rows[i]["PORT_OF_LOADING_NAME"].ToString();
                objPurchaseOrderModel.GridCurrencyId = dt.Rows[i]["CURRENCY_ID"].ToString();
                objPurchaseOrderModel.GridCurrencyName = dt.Rows[i]["CURRENCY_NAME"].ToString();
                objPurchaseOrderModel.GridStatus = dt.Rows[i]["STATUS"].ToString();
                objPurchaseOrderModel.GridRemarks = dt.Rows[i]["REMARKS"].ToString();
                objPurchaseOrderModel.GridCopyYn = dt.Rows[i]["COPY_YN"].ToString();
                objPurchaseOrderModel.GridDeleteYn = dt.Rows[i]["DELETE_YN"].ToString();
                objPurchaseOrderModel.GridApprovedYn = dt.Rows[i]["APPROVED_STATUS"].ToString();
                PurchaseOrderDeleteApprovedListDataBundle.Add(objPurchaseOrderModel);

            }

            return PurchaseOrderDeleteApprovedListDataBundle;
        }



        #endregion

        #region"PO Report"
        public ActionResult PurchaseOrderReport()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                string viebagmsg = ViewBag.errormsg;
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PurchaseOrderReport(PurchaseOrderModel objPurchaseOrderModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objPurchaseOrderModel.UpdateBy = strEmployeeId;
                objPurchaseOrderModel.HeadOfficeId = strHeadOfficeId;
                objPurchaseOrderModel.BranchOfficeId = strBranchOfficeId;
                string reportType = objPurchaseOrderModel.ReportFor;
                if (!string.IsNullOrEmpty(reportType) && reportType == "DETAIL")
                {
                    
                    DataSet objDataSet = objReportDAL.GetPOData(objPurchaseOrderModel);
                    string ReportPath = Path.Combine(Server.MapPath("~/Reports/rptPOInformation.rpt"));
                    objReportDocument.Load(ReportPath);
                    objReportDocument.SetDataSource(objDataSet);
                    objReportDocument.SetDatabaseLogon("erp", "erp");
                    ShowReport(objPurchaseOrderModel.ReportType, "POReport");
                }
                if (!string.IsNullOrEmpty(reportType) && reportType == "SUM")
                {
                    string vStrMsg = objPurchaseOrderDAL.SaveSumDataForSummaryReport(objPurchaseOrderModel);
                    DataSet objDataSet = objReportDAL.GetPOSumData(objPurchaseOrderModel);
                    string ReportPath = Path.Combine(Server.MapPath("~/Reports/rptPOSummaryInformation.rpt"));
                    objReportDocument.Load(ReportPath);
                    objReportDocument.SetDataSource(objDataSet);
                    objReportDocument.SetDatabaseLogon("erp", "erp");
                    ShowReport(objPurchaseOrderModel.ReportType, "POReport");
                }
                if (!string.IsNullOrEmpty(reportType) && reportType == "DELETE")
                {                    
                    DataSet objDataSet = objReportDAL.GetPODataDeleteHistory(objPurchaseOrderModel);
                    string ReportPath = Path.Combine(Server.MapPath("~/Reports/rptDeletedPOInformation.rpt"));
                    objReportDocument.Load(ReportPath);
                    objReportDocument.SetDataSource(objDataSet);
                    objReportDocument.SetDatabaseLogon("erp", "erp");
                    ShowReport(objPurchaseOrderModel.ReportType, "POReport");
                }

                if (!string.IsNullOrEmpty(reportType) && reportType == "REJECT")
                {
                    DataSet objDataSet = objReportDAL.GetPODataRejectHistory(objPurchaseOrderModel);
                    string ReportPath = Path.Combine(Server.MapPath("~/Reports/rptRejectedPOInformation.rpt"));
                    objReportDocument.Load(ReportPath);
                    objReportDocument.SetDataSource(objDataSet);
                    objReportDocument.SetDatabaseLogon("erp", "erp");
                    ShowReport(objPurchaseOrderModel.ReportType, "POReport");
                }
                //if (string.IsNullOrEmpty(reportType))
                //{
                //    TempData["OperationMessage"] = "Please select one option";
                //}

                return RedirectToAction("PurchaseOrderReport");
            }
        }
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


    }
}