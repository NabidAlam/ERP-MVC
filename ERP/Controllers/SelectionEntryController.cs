using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.DAL;
using ERP.MODEL;
using ERP.Utility;
using System.Data;
using PagedList;
using System.Net;
using System.IO;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace ERP.Controllers
{
    public class SelectionEntryController : Controller
    {
        SelectionEntryModel objSelectionEntryModel = new SelectionEntryModel();
        SelectionEntryDAL objSelectionEntryDAL = new SelectionEntryDAL();
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
            ViewBag.CountryDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCountryDDList(), "COUNTRY_ID", "COUNTRY_NAME");            
        }
        #endregion

        #region"Selection Entry"
        public ActionResult SelectionEntry(string pStyleNo, string pModelNo, string pCountryId, string pTotalreceived, string pEditFlag, int page = 1, int pageSize = 10, string SearchBy = "", string FileSearch = "", string pdf = "",string selection="",string POSearch="")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                LoadDropDownList();
                string vSearchFlag = "";
                string vEditFlag = "";
                string vDeleteFlag = "";
                objSelectionEntryModel.UpdateBy = strEmployeeId;
                objSelectionEntryModel.HeadOfficeId = strHeadOfficeId;
                objSelectionEntryModel.BranchOfficeId = strBranchOfficeId;
                ViewBag.CurrentDate = objLookUpDAL.currentDate();
                objSelectionEntryModel.CurrentYear=DateTime.Now.ToString("yyyy");
                //Load Country Name Dropdown list change event
                if (TempData.ContainsKey("CountryName"))
                {
                    ViewBag.CountryName = TempData["CountryName"].ToString();
                    if (TempData.ContainsKey("CountryId"))
                    {
                        objSelectionEntryModel.CountryId = TempData["CountryId"].ToString();
                    }
                    if (TempData.ContainsKey("StyleNo"))
                    {
                        objSelectionEntryModel.StyleNo=TempData["StyleNo"].ToString();
                    }
                    if (TempData.ContainsKey("ModelNo"))
                    {
                        objSelectionEntryModel.ModelNo = TempData["ModelNo"].ToString();
                    }
                    if (TempData.ContainsKey("TotalReceived"))
                    {
                        objSelectionEntryModel.TotalReceived = TempData["TotalReceived"].ToString();
                    }                                 
                }                        
                #region Multiple Pagination Search
                CheckUrl();

                //get all style record
                if (!string.IsNullOrWhiteSpace(POSearch))
                {
                    bool exist = POSearch.Contains("|");
                    if (exist)
                    {
                        string vA = POSearch;
                        string StyleNo = vA.Substring(0, vA.IndexOf(" | ")).Trim();
                        int PositionAfterStyleNo = vA.IndexOf(" | ") + 2;
                        string ModelNo = vA.Substring(PositionAfterStyleNo).Trim();
                        objSelectionEntryModel.StyleNo = StyleNo;
                        objSelectionEntryModel.ModelNo = ModelNo;
                    }

                }

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    bool exist = SearchBy.Contains("|");
                    if (exist)
                    {
                        string vA = SearchBy;
                        string StyleNo = vA.Substring(0, vA.IndexOf(" | ")).Trim();
                        int PositionAfterStyleNo = vA.IndexOf(" | ") + 2;
                        string vB = vA.Substring(PositionAfterStyleNo);
                        string ModelNo = vB.Substring(0, vB.IndexOf(" | ")).Trim();
                        int PositionAfterModelNo = vB.IndexOf(" | ") + 2;
                        string CountryName = vB.Substring(PositionAfterModelNo).Trim();

                        objSelectionEntryModel.StyleNo = StyleNo;
                        objSelectionEntryModel.ModelNo = ModelNo;
                        objSelectionEntryModel.CountryName = CountryName;
                        vSearchFlag = "1";
                        
                    }
                    
                }
             
                //Load data from sub table to edit
                if (!string.IsNullOrEmpty(pEditFlag) && pEditFlag == "1")
                {
                    if (!string.IsNullOrEmpty(pStyleNo))
                    {
                        objSelectionEntryModel.StyleNo = pStyleNo.Trim();
                    }
                    if (!string.IsNullOrEmpty(pModelNo))
                    {
                        objSelectionEntryModel.ModelNo = pModelNo.Trim();
                    }
                    if (!string.IsNullOrEmpty(pTotalreceived))
                    {
                        objSelectionEntryModel.TotalReceived = pTotalreceived.Trim();
                    }
                    if (!string.IsNullOrEmpty(pCountryId))
                    {
                        objSelectionEntryModel.CountryId = pCountryId.Trim();
                    }
                    DataTable dt1 = objSelectionEntryDAL.GetSelectionRecordSub(objSelectionEntryModel);
                    ViewBag.SelectionEntryListEdit = SelectionEntryListDataSub(dt1);
                    page = (int)TempData["GetActionPage"];
                    TempData.Keep("GetActionPage");
                    //if search then edit
                    if (TempData.ContainsKey("SearchFlag"))
                    {
                        TempData.Keep("SearchFlag");
                        vSearchFlag = "1";
                        vEditFlag = "1";
                    }
                    
                }
                if (TempData.ContainsKey("SaveActionFlag") && (int)TempData["SaveActionFlag"] == 1)
                {
                    page = (int)TempData["SaveActionPage"];
                    TempData.Keep("SaveActionPage");
                }
                if (TempData.ContainsKey("DeleteActionFlag"))
                {
                    vDeleteFlag = TempData["DeleteActionFlag"].ToString();
                    if (!string.IsNullOrEmpty(vDeleteFlag) && vDeleteFlag == "1")
                    {
                        if (TempData.ContainsKey("DeletedStyleNo"))
                        {
                            objSelectionEntryModel.StyleNo = TempData["DeletedStyleNo"].ToString();
                        }
                        if (TempData.ContainsKey("DeletedModelNo"))
                        {
                            objSelectionEntryModel.ModelNo = TempData["DeletedModelNo"].ToString();
                        }
                        if (TempData.ContainsKey("DeletedTotalReceived"))
                        {
                            objSelectionEntryModel.TotalReceived = TempData["DeletedTotalReceived"].ToString();
                        }
                        if (TempData.ContainsKey("DeletedCountryId"))
                        {
                            objSelectionEntryModel.CountryId = TempData["DeletedCountryId"].ToString();
                        }
                        DataTable dt1 = objSelectionEntryDAL.GetSelectionRecordSub(objSelectionEntryModel);
                        ViewBag.SelectionEntryListEdit = SelectionEntryListDataSub(dt1);
                        if (TempData.ContainsKey("SearchFlag"))
                        {
                            TempData.Keep("SearchFlag");
                            vSearchFlag = "1";
                            vEditFlag = "1";
                            if (TempData.ContainsKey("DeletedAllDataDelete"))
                            {
                                string vAllDataDelete = TempData["DeletedAllDataDelete"].ToString();
                                if (!string.IsNullOrEmpty(vAllDataDelete)&& vAllDataDelete== "ALL DATA DELETED")
                                {
                                    vSearchFlag = "1";
                                    vEditFlag = "";
                                }
                               
                            }
                        }
                    }
                }
               
                //Load data from main table
                if (!string.IsNullOrEmpty(vSearchFlag) && vSearchFlag == "1")
                {
                    page = 1;
                    DataTable dt1 = objSelectionEntryDAL.GetSelectionAutoSearch(objSelectionEntryModel);
                    List<SelectionEntryModel> SelectionEntryListMain = SelectionEntryListDataMain(dt1);
                    ViewBag.SelectionEntryList = SelectionEntryListMain.ToPagedList(page, pageSize);
                    if (string.IsNullOrEmpty(vEditFlag))
                    {
                        objSelectionEntryModel.StyleNo = "";
                        objSelectionEntryModel.ModelNo = "";
                        objSelectionEntryModel.TotalReceived = "";
                        objSelectionEntryModel.CountryId = "";
                    }                   
                        TempData["SearchFlag"] = 1;
                }
                else {


                    if (!string.IsNullOrEmpty(selection))
                    {
                        DataTable dt = objSelectionEntryDAL.GetSelectionRecordMain(objSelectionEntryModel);
                        List<SelectionEntryModel> SelectionEntryListMain = SelectionEntryListDataMain(dt);
                        ViewBag.SelectionEntryList = SelectionEntryListMain.ToPagedList(page, pageSize);
                    }
                    else {

                        DataTable dt = objSelectionEntryDAL.GetSelectionRecordMain(objSelectionEntryModel);
                        List<SelectionEntryModel> SelectionEntryListMain = SelectionEntryListDataMain(dt);
                        ViewBag.SelectionEntryList = SelectionEntryListMain.ToPagedList(1, 10);
                    }
                       
                }

                //pdf pagination

                if (!string.IsNullOrEmpty(FileSearch)) {

                    objSelectionEntryModel.UploadDate = FileSearch.Trim();
                    DataTable dt2 = objSelectionEntryDAL.GetSelectionFileUploadRecord(objSelectionEntryModel);
                    List<SelectionEntryModel> SelectionFileUploadList = SelectionFileUpload(dt2);
                    ViewBag.SelectionFileUploadList = SelectionFileUploadList.ToPagedList(1, 10);


                }
                else {

                    if (!string.IsNullOrEmpty(pdf))
                    {
                        DataTable dt2 = objSelectionEntryDAL.GetSelectionFileUploadRecord(objSelectionEntryModel);
                        List<SelectionEntryModel> SelectionFileUploadList = SelectionFileUpload(dt2);
                        ViewBag.SelectionFileUploadList = SelectionFileUploadList.ToPagedList(page, pageSize);
                    }
                    else
                    {

                        DataTable dt2 = objSelectionEntryDAL.GetSelectionFileUploadRecord(objSelectionEntryModel);
                        List<SelectionEntryModel> SelectionFileUploadList = SelectionFileUpload(dt2);
                        ViewBag.SelectionFileUploadList = SelectionFileUploadList.ToPagedList(1, 10);
                    }
                }
                TempData["GetActionFlag"] = 1;
                TempData["GetActionPage"] = page;
                #endregion
            }
            return View(objSelectionEntryModel);
        }
        public List<SelectionEntryModel> SelectionEntryListDataMain(DataTable dt)
        {
            List<SelectionEntryModel> SelectionEntryListDataBundle = new List<SelectionEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectionEntryModel objSelectionEntryModel = new SelectionEntryModel();
                objSelectionEntryModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objSelectionEntryModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objSelectionEntryModel.ModelNo = dt.Rows[i]["MODEL_NO"].ToString();
                objSelectionEntryModel.TotalReceived = dt.Rows[i]["TOTAL_RECEIVED"].ToString();
                objSelectionEntryModel.CountryId = dt.Rows[i]["COUNTRY_ID"].ToString();
                objSelectionEntryModel.CountryName = dt.Rows[i]["COUNTRY_NAME"].ToString();
                SelectionEntryListDataBundle.Add(objSelectionEntryModel);
            }

            return SelectionEntryListDataBundle;
        }
        public List<SelectionEntryModel> SelectionEntryListDataSub(DataTable dt)
        {
            List<SelectionEntryModel> SelectionEntryListDataBundle = new List<SelectionEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectionEntryModel objSelectionEntryModel = new SelectionEntryModel();
                objSelectionEntryModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objSelectionEntryModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objSelectionEntryModel.ModelNo = dt.Rows[i]["MODEL_NO"].ToString();
                objSelectionEntryModel.TotalReceived = dt.Rows[i]["TOTAL_RECEIVED"].ToString();
                objSelectionEntryModel.CountryId = dt.Rows[i]["COUNTRY_ID"].ToString();
                ViewBag.CountryName = dt.Rows[i]["COUNTRY_NAME"].ToString();
                objSelectionEntryModel.GridEUSS = dt.Rows[i]["EUROPE_SS_QUANTITY"].ToString();
                objSelectionEntryModel.GridEUAW = dt.Rows[i]["EUROPE_AW_QUANTITY"].ToString();
                objSelectionEntryModel.GridCountry = dt.Rows[i]["COUNTRY_ORDER_QUANTITY"].ToString();
                objSelectionEntryModel.GridElasticity = dt.Rows[i]["ELASTICITY"].ToString();
                objSelectionEntryModel.GridSupply = dt.Rows[i]["SUPPLY"].ToString();
                objSelectionEntryModel.GridReceived = dt.Rows[i]["COUNTRY_RECEIVED_QUANTITY"].ToString();
                objSelectionEntryModel.GridTranId = dt.Rows[i]["TRAN_ID"].ToString();
                SelectionEntryListDataBundle.Add(objSelectionEntryModel);
            }
            return SelectionEntryListDataBundle;
        }
        public ActionResult CountryName(SelectionEntryModel objSelectionEntryModel)
        {
            LoadSession();
            string vStyleNo = "";
            string vModelNo = "";
            string vTotalReceived = "";
            string vCountryId = "";
            objSelectionEntryModel.UpdateBy = strEmployeeId;
            objSelectionEntryModel.HeadOfficeId = strHeadOfficeId;
            objSelectionEntryModel.BranchOfficeId = strBranchOfficeId;
            if (!string.IsNullOrEmpty(objSelectionEntryModel.StyleNo))
            {
                vStyleNo = objSelectionEntryModel.StyleNo.Trim();
            }
            if (!string.IsNullOrEmpty(objSelectionEntryModel.ModelNo))
            {
                vModelNo = objSelectionEntryModel.ModelNo.Trim();
            }
            if (!string.IsNullOrEmpty(objSelectionEntryModel.TotalReceived))
            {
                vTotalReceived = objSelectionEntryModel.TotalReceived.Trim();
            }
            if (!string.IsNullOrEmpty(objSelectionEntryModel.CountryId))
            {
                vCountryId = objSelectionEntryModel.CountryId.Trim();
            }
                string vCountryName = objSelectionEntryDAL.GetCountryNameById(objSelectionEntryModel);
                TempData["CountryName"] = vCountryName;
                TempData["CountryId"] = vCountryId;
                TempData["StyleNo"] = vStyleNo;
                TempData["ModelNo"] = vModelNo;
                TempData["TotalReceived"] = vTotalReceived;
            return Json(objSelectionEntryModel,JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSelectionEntry(SelectionEntryModel objSelectionEntryModel)
        {
                string strDBMsg = "";
                LoadSession();
                objSelectionEntryModel.UpdateBy = strEmployeeId;
                objSelectionEntryModel.HeadOfficeId = strHeadOfficeId;
                objSelectionEntryModel.BranchOfficeId = strBranchOfficeId;            
                strDBMsg = objSelectionEntryDAL.SaveSelectionEntrySave(objSelectionEntryModel);
                TempData["OperationMessage"] = strDBMsg;
                #region Pagination Search
                int page = (int)TempData["GetActionPage"];
                if (page >= 1 || page != 0)
                {
                    TempData["SaveActionPage"] = page;
                    TempData.Keep("GetActionPage");
                }
                TempData["SaveActionFlag"] = 1;
                #endregion
                return RedirectToAction("SelectionEntry");
            
        }
        //Delete data
        public JsonResult DeleteSelectionEntry(SelectionEntryModel objSelectionEntryModel)
        {
            LoadSession();
            string vStrDBMsg = "";
            objSelectionEntryModel.UpdateBy = strEmployeeId;
            objSelectionEntryModel.HeadOfficeId = strHeadOfficeId;
            objSelectionEntryModel.BranchOfficeId = strBranchOfficeId;
            vStrDBMsg = objSelectionEntryDAL.DeleteSelectionEntry(objSelectionEntryModel);
            if (!string.IsNullOrEmpty(objSelectionEntryModel.StyleNo))
            {
                TempData["DeletedStyleNo"] = objSelectionEntryModel.StyleNo;
            }
            if (!string.IsNullOrEmpty(objSelectionEntryModel.ModelNo))
            {
                TempData["DeletedModelNo"] = objSelectionEntryModel.ModelNo;
            }
            if (!string.IsNullOrEmpty(objSelectionEntryModel.TotalReceived))
            {
                TempData["DeletedTotalReceived"] = objSelectionEntryModel.TotalReceived;
            }
            if (!string.IsNullOrEmpty(objSelectionEntryModel.CountryId))
            {
                TempData["DeletedCountryId"] = objSelectionEntryModel.CountryId;
            }

            if (!string.IsNullOrEmpty(vStrDBMsg))
            {
                TempData["DeletedAllDataDelete"] = vStrDBMsg;
            }
            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["DeleteActionPage"] = page;
                TempData.Keep("GetActionPage");
            }
            
            TempData["DeleteActionFlag"] = 1;
            #endregion
            return Json(vStrDBMsg, JsonRequestBehavior.AllowGet);
        }
        //Clear Tempdata
        public ActionResult ClearSelectionEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            TempData["CountryName"] = "";
            TempData["CountryId"] = "";
            TempData["StyleNo"] = "";
            TempData["ModelNo"] = "";
            TempData["TotalReceived"] = "";
            TempData["DeleteActionFlag"] = "";
            return RedirectToAction("SelectionEntry");

        }
        //Auto complete style Search with year
        public ActionResult StyleNoByYear(string year)
        {
            LoadSession();
            objSelectionEntryModel.UpdateBy = strEmployeeId;
            objSelectionEntryModel.HeadOfficeId = strHeadOfficeId;
            objSelectionEntryModel.BranchOfficeId = strBranchOfficeId;
            objSelectionEntryModel.CurrentYear = year;
            DataTable dt = objSelectionEntryDAL.SearchStyleFromPO(objSelectionEntryModel);
            List<SelectionEntryModel> StyleList = StyleListDataByYear(dt);
            return Json(StyleList, JsonRequestBehavior.AllowGet);
        }
        public List<SelectionEntryModel> StyleListDataByYear(DataTable dt)
        {
            List<SelectionEntryModel> StyleListData = new List<SelectionEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectionEntryModel objSelectionEntryModel = new SelectionEntryModel();
                objSelectionEntryModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objSelectionEntryModel.ModelNo = dt.Rows[i]["MODEL_NO"].ToString();
                objSelectionEntryModel.StyleSearchByYear = dt.Rows[i]["STYLE_NO"] + " | " + dt.Rows[i]["MODEL_NO"];
                StyleListData.Add(objSelectionEntryModel);
            }
            return StyleListData;
        }
        //Auto complete style Search
        public ActionResult GetStyleNo()
       {
            LoadSession();
            objSelectionEntryModel.UpdateBy = strEmployeeId;
            objSelectionEntryModel.HeadOfficeId = strHeadOfficeId;
            objSelectionEntryModel.BranchOfficeId = strBranchOfficeId;
            DataTable dt = objSelectionEntryDAL.GetSelectionAutoSearch(objSelectionEntryModel);
            List<SelectionEntryModel> StyleNoList = StyleNoListData(dt);
            return Json(StyleNoList, JsonRequestBehavior.AllowGet);
        }
        public List<SelectionEntryModel> StyleNoListData(DataTable dt)
        {
            List<SelectionEntryModel> StyleNoListDataBind = new List<SelectionEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectionEntryModel objSelectionEntryModel = new SelectionEntryModel();
                objSelectionEntryModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objSelectionEntryModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objSelectionEntryModel.ModelNo = dt.Rows[i]["MODEL_NO"].ToString();
                objSelectionEntryModel.TotalReceived = dt.Rows[i]["TOTAL_RECEIVED"].ToString();
                objSelectionEntryModel.CountryId = dt.Rows[i]["COUNTRY_ID"].ToString();
                objSelectionEntryModel.CountryName = dt.Rows[i]["COUNTRY_NAME"].ToString();
                objSelectionEntryModel.StyleSearch = dt.Rows[i]["STYLE_NO"] + " | " + dt.Rows[i]["MODEL_NO"] + " | " + dt.Rows[i]["COUNTRY_NAME"];
                StyleNoListDataBind.Add(objSelectionEntryModel);
            }

            return StyleNoListDataBind;
        }
        //save pdf file
        public ActionResult SectionPdfFileUpload(HttpPostedFileBase files, SelectionEntryModel objSelectionEntryModel)
        {
            LoadSession();
            string strDBMsg = "";
            objSelectionEntryModel.UpdateBy = strEmployeeId;
            objSelectionEntryModel.HeadOfficeId = strHeadOfficeId;
            objSelectionEntryModel.BranchOfficeId = strBranchOfficeId;
            String FileExt = Path.GetExtension(files.FileName).ToUpper();
            if (FileExt == ".PDF")
            {
                Stream str = files.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);               
                objSelectionEntryModel.FileName = files.FileName;               
                string fileSize = Convert.ToBase64String(FileDet);
                objSelectionEntryModel.CVSize = fileSize;
                objSelectionEntryModel.FileExtension = FileExt;
                strDBMsg = objSelectionEntryDAL.SaveSeactionPdfFileUpload(objSelectionEntryModel);
                TempData["OperationMessage"] = strDBMsg;               
            }
            else
            {
                ViewBag.FileStatus = "Invalid file format.";
                return View();
            }
            return RedirectToAction("SectionPdfFileUploadRecord");
            
        }
        //get uploaded pdf record
        public ActionResult SectionPdfFileUploadRecord(string pEditFlag, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            LoadSession();
            objSelectionEntryModel.UpdateBy = strEmployeeId;
            objSelectionEntryModel.HeadOfficeId = strHeadOfficeId;
            objSelectionEntryModel.BranchOfficeId = strBranchOfficeId;
            DataTable dt2 = objSelectionEntryDAL.GetSelectionFileUploadRecord(objSelectionEntryModel);
            List<SelectionEntryModel> SelectionFileUploadList = SelectionFileUpload(dt2);
            ViewBag.SelectionFileUploadList = SelectionFileUploadList.ToPagedList(page, pageSize);
            return RedirectToAction("SelectionEntry");

        }
        public List<SelectionEntryModel> SelectionFileUpload(DataTable dt)
        {
            List<SelectionEntryModel> SelectionFileUploadDataBundle = new List<SelectionEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectionEntryModel objSelectionEntryModel = new SelectionEntryModel();
                objSelectionEntryModel.SerialNumber = dt.Rows[i]["sl"].ToString();
                objSelectionEntryModel.UploadDate = dt.Rows[i]["UPLOAD_DATE"].ToString(); 
                objSelectionEntryModel.UploadFiles = dt.Rows[i]["NO_OF_FILE"].ToString();
                SelectionFileUploadDataBundle.Add(objSelectionEntryModel);
            }

            return SelectionFileUploadDataBundle;
        }
        //Auto complete Upload Date Search
        public ActionResult GetUploadDate()
        {
            LoadSession();
            objSelectionEntryModel.UpdateBy = strEmployeeId;
            objSelectionEntryModel.HeadOfficeId = strHeadOfficeId;
            objSelectionEntryModel.BranchOfficeId = strBranchOfficeId;
            DataTable dt = objSelectionEntryDAL.GetSelectionPdfAutoSearch(objSelectionEntryModel);
            List<SelectionEntryModel> UploadDateList = UploadDateListData(dt);
            return Json(UploadDateList, JsonRequestBehavior.AllowGet);
        }
        public List<SelectionEntryModel> UploadDateListData(DataTable dt)
        {
            List<SelectionEntryModel> UploadDateListDataBind = new List<SelectionEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectionEntryModel objSelectionEntryModel = new SelectionEntryModel();
                objSelectionEntryModel.UploadDate = dt.Rows[i]["UPLOAD_DATE"].ToString();
                UploadDateListDataBind.Add(objSelectionEntryModel);
            }

            return UploadDateListDataBind;
        }
        //View Datewise all pdf files
        [HttpGet]
        public ActionResult DateWiseSelectionFileUpload(string pShowFlag, string pViewFlag, string pDeleteFlag, string pTranId, string pUploadDate)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();
                ViewBag.CurrentDate = objLookUpDAL.currentDate();
                objSelectionEntryModel.UpdateBy = strEmployeeId;
                objSelectionEntryModel.HeadOfficeId = strHeadOfficeId;
                objSelectionEntryModel.BranchOfficeId = strBranchOfficeId;
                CheckUrl();

                if (!string.IsNullOrWhiteSpace(pTranId) && !string.IsNullOrWhiteSpace(pUploadDate))
                {
                    objSelectionEntryModel.GridTranId = pTranId.Trim();
                    objSelectionEntryModel.UploadDate = pUploadDate.Trim();


                    if (!string.IsNullOrEmpty(pViewFlag) && pViewFlag == "1")
                    {
                        objSelectionEntryModel = objSelectionEntryDAL.ViewPdfFile(objSelectionEntryModel);
                        var pdfByte = objSelectionEntryModel.bytes;
                        return File(pdfByte, "application/pdf");

                    }
                    if (!string.IsNullOrEmpty(pDeleteFlag) && pDeleteFlag == "1")
                    {
                        string strDBMsg = "";
                        strDBMsg = objSelectionEntryDAL.DeleteSelectionUploadFile(objSelectionEntryModel);
                        TempData["OperationMessage"] = strDBMsg;

                    }


                }

                if (!string.IsNullOrWhiteSpace(pShowFlag) && pShowFlag == "1")
                {
                    
                    if (!string.IsNullOrWhiteSpace(pUploadDate))
                    {
                        objSelectionEntryModel.UploadDate = pUploadDate;

                    }
                }
                ViewBag.ShowCriteria = " DATE-" + objSelectionEntryModel.UploadDate;
                DataTable dt = objSelectionEntryDAL.DateWiseSelectionFileUpload(objSelectionEntryModel);
                List<SelectionEntryModel> SelectionFileUploadList = DateWiseSelectionFileUpload(dt);
                ViewBag.SelectionFileUploadList = SelectionFileUploadList;
               
                return View(objSelectionEntryModel);
            }
        }
        public List<SelectionEntryModel> DateWiseSelectionFileUpload(DataTable dt)
        {
            List<SelectionEntryModel> DateWiseSelectionFileUploadDataBundle = new List<SelectionEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SelectionEntryModel objSelectionEntryModel = new SelectionEntryModel();
                objSelectionEntryModel.SerialNumber = dt.Rows[i]["sl"].ToString();
                objSelectionEntryModel.GridTranId = dt.Rows[i]["TRAN_ID"].ToString();
                objSelectionEntryModel.FileName = dt.Rows[i]["FILE_NAME"].ToString();
                objSelectionEntryModel.UploadDate = dt.Rows[i]["UPLOAD_DATE"].ToString();

                DateWiseSelectionFileUploadDataBundle.Add(objSelectionEntryModel);
            }

            return DateWiseSelectionFileUploadDataBundle;
        }


        #endregion

        #region"selection report"

        public ActionResult SelectionReport()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                LoadDropDownList();
                string viebagmsg = ViewBag.errormsg;
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SelectionReport(SelectionEntryModel objSelectionEntryModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objSelectionEntryModel.UpdateBy = strEmployeeId;
                objSelectionEntryModel.HeadOfficeId = strHeadOfficeId;
                objSelectionEntryModel.BranchOfficeId = strBranchOfficeId;
                string reportType = objSelectionEntryModel.ReportFor;
                if (!string.IsNullOrEmpty(reportType) && reportType == "DETAIL")
                {

                    DataSet objDataSet = objReportDAL.GetSelectionData(objSelectionEntryModel);
                    string ReportPath = Path.Combine(Server.MapPath("~/Reports/rptSelectionEntryInformation.rpt"));
                    objReportDocument.Load(ReportPath);
                    objReportDocument.SetDataSource(objDataSet);
                    objReportDocument.SetDatabaseLogon("erp", "erp");
                    ShowReport(objSelectionEntryModel.ReportType, "POReport");
                }
                //if (!string.IsNullOrEmpty(reportType) && reportType == "SUM")
                //{
                //    string vStrMsg = objPurchaseOrderDAL.SaveSumDataForSummaryReport(objPurchaseOrderModel);
                //    DataSet objDataSet = objReportDAL.GetPOSumData(objPurchaseOrderModel);
                //    string ReportPath = Path.Combine(Server.MapPath("~/Reports/rptPOSummaryInformation.rpt"));
                //    objReportDocument.Load(ReportPath);
                //    objReportDocument.SetDataSource(objDataSet);
                //    objReportDocument.SetDatabaseLogon("erp", "erp");
                //    ShowReport(objPurchaseOrderModel.ReportType, "POReport");
                //}
                
                return RedirectToAction("SelectionReport");
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
