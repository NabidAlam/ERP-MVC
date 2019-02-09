using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.DAL;
using ERP.MODEL;
using System.Data;
using PagedList;
using ERP.Utility;
using System.Net;
using System.IO;

namespace ERP.Controllers
{
    public class TechpackFileUploadController : Controller
    {
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
            strEmployeeId = Session["strEmployeeId"].ToString().Trim();
            strSubSectionId = Session["strSubSectionId"].ToString().Trim();
            strDesignationId = Session["strDesignationId"].ToString().Trim();
            strUnitId = Session["strUnitId"].ToString().Trim();
            strHeadOfficeId = Session["strHeadOfficeId"].ToString().Trim();
            strBranchOfficeId = Session["strBranchOfficeId"].ToString().Trim();
            strSoftId = Session["strSoftId"].ToString().Trim();
            if (Session["strOldUrl"] != null)
            {
                strOldUrl = Session["strOldUrl"].ToString().Trim();
            }
        }
        public void emptyTextBoxValue()
        {
            objTechpackFileUploadModel.BuyerId = "";
            objTechpackFileUploadModel.SeasonId = "";
            objTechpackFileUploadModel.StyleNo = "";
            objTechpackFileUploadModel.SeasonYear = ViewBag.CurrentYear;
        }
        TechpackFileUploadModel objTechpackFileUploadModel = new TechpackFileUploadModel();
        TechpackFileUploadDAL objTechpackFileUploadDAL = new TechpackFileUploadDAL();
        LookUpDAL objLookUpDAL = new LookUpDAL();
        [HttpGet]
        public ActionResult TechpackFileUpload(string pSeasonYear, string pSeasonId, string pStyleNo, string pBuyerId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                ViewBag.CurrentYear=objLookUpDAL.getCurrentYear(); 
                ViewBag.CurrentDate = objLookUpDAL.currentDate();
                objTechpackFileUploadModel.UpdateBy = strEmployeeId;
                objTechpackFileUploadModel.HeadOfficeId = strHeadOfficeId;
                objTechpackFileUploadModel.BranchOfficeId = strBranchOfficeId;
                #region Pagination Search
                CheckUrl();
                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    bool exist = SearchBy.Contains("|");
                    if (exist)
                    {
                        
                        string a = SearchBy;
                        string Style = a.Substring(0, a.IndexOf(" | ")).Trim();
                        int PositionAfterStyle = a.IndexOf(" | ") + 2;

                        string b = a.Substring(PositionAfterStyle);
                        string Season = b.Substring(0, b.IndexOf(" | ")).Trim();
                        int PositionAfterSeason = b.IndexOf(" | ") + 2;

                        string c = b.Substring(PositionAfterSeason);
                        string Year = c.Substring(0, c.IndexOf(" | ")).Trim();
                        int PositionAfterYear = c.IndexOf(" | ") + 2;

                        string d = c.Substring(PositionAfterYear);
                        string Buyer = d.Substring(0, d.IndexOf(" | ")).Trim();
                        int PositionAfterBuyer =d.IndexOf(" | ") + 2;

                        string UploadDate = d.Substring(PositionAfterBuyer).Trim();

                        objTechpackFileUploadModel.StyleNo = Style;
                        objTechpackFileUploadModel.SeasonName = Season;
                        objTechpackFileUploadModel.SeasonYear = Year;
                        objTechpackFileUploadModel.BuyerName = Buyer;
                        objTechpackFileUploadModel.UploadDate = UploadDate;

                        if (ViewBag.CurrentYear != Year)
                        {
                            ViewBag.CurrentYear = Year;
                        }

                        TempData["SearchFlag"] = 1;
                        TempData["SearchPage"] = page;

                    }

                    else {

                        TempData["SearchFlag"] = 1;
                        TempData["SearchPage"] = page;
                        TempData["SearchValue"] = SearchBy;
                        objTechpackFileUploadModel.SearchBy = SearchBy;
                        }

                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objTechpackFileUploadModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }
                TempData["GetActionFlag"] = 1;
                TempData["GetActionPage"] = page;

                #endregion

                DataTable dt = objTechpackFileUploadDAL.TechpackFileUpload(objTechpackFileUploadModel);
                List<TechpackFileUploadModel> TechpackFileUploadList = TechpackFileUpload(dt);
                ViewBag.TechpackFileUploadList = TechpackFileUploadList.ToPagedList(page, pageSize);
                ViewBag.BuyerDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetBuyerDDList(), "BUYER_ID", "BUYER_NAME");
                ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
                
                return View(objTechpackFileUploadModel);
            }
        }
        public List<TechpackFileUploadModel> TechpackFileUpload(DataTable dt)
        {
            List<TechpackFileUploadModel> TechpackFileUploadDataBundle = new List<TechpackFileUploadModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TechpackFileUploadModel objTechpackFileUploadModel = new TechpackFileUploadModel();
                objTechpackFileUploadModel.SerialNumber = dt.Rows[i]["sl"].ToString();
                objTechpackFileUploadModel.BuyerId = dt.Rows[i]["BUYER_ID"].ToString();
                objTechpackFileUploadModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objTechpackFileUploadModel.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                objTechpackFileUploadModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objTechpackFileUploadModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objTechpackFileUploadModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objTechpackFileUploadModel.UploadDate = dt.Rows[i]["UPLOAD_DATE"].ToString();
                objTechpackFileUploadModel.UploadFiles = dt.Rows[i]["UPLOAD_FILES"].ToString();

                TechpackFileUploadDataBundle.Add(objTechpackFileUploadModel);
            }

            return TechpackFileUploadDataBundle;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTechpackFileUpload(HttpPostedFileBase files, TechpackFileUploadModel objTechpackFileUploadModel)
        {
            LoadSession();
            string strDBMsg = "";
            objTechpackFileUploadModel.UpdateBy = strEmployeeId;
            objTechpackFileUploadModel.HeadOfficeId = strHeadOfficeId;
            objTechpackFileUploadModel.BranchOfficeId = strBranchOfficeId;
            String FileExt = Path.GetExtension(files.FileName).ToUpper();
            if (FileExt == ".PDF")
            {
                Stream str = files.InputStream;
                BinaryReader Br = new BinaryReader(str);
                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                //FileDetailsModel Fd = new Models.FileDetailsModel();
                objTechpackFileUploadModel.FileName = files.FileName;
                //objTechpackFileUploadModel.FileContent = FileDet;
                string fileSize = Convert.ToBase64String(FileDet);
                objTechpackFileUploadModel.CVSize = fileSize;
                objTechpackFileUploadModel.FileExtension = FileExt;
                strDBMsg = objTechpackFileUploadDAL.SaveTechpackFileUpload(objTechpackFileUploadModel);
                TempData["OperationMessage"] = strDBMsg;
                //emptyTextBoxValue();
            }
            else
            {
                ViewBag.FileStatus = "Invalid file format.";
                return View();
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

           
            return RedirectToAction("TechpackFileUpload");
        }
        public ActionResult ClearTechpackFileUploadEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("TechpackFileUpload");
        }
        public ActionResult GetStyleNo(string year)
        {
            LoadSession();
            objTechpackFileUploadModel.UpdateBy = strEmployeeId;
            objTechpackFileUploadModel.HeadOfficeId = strHeadOfficeId;
            objTechpackFileUploadModel.BranchOfficeId = strBranchOfficeId;
            objTechpackFileUploadModel.SeasonYear = year;
            DataTable dt = objTechpackFileUploadDAL.SearchStyleFromTechpackUpload(objTechpackFileUploadModel);
            List<TechpackFileUploadModel> StyleList = StyleListDataByYear(dt);
            return Json(StyleList, JsonRequestBehavior.AllowGet);
        }
        public List<TechpackFileUploadModel> StyleListDataByYear(DataTable dt)
        {
            List<TechpackFileUploadModel> StyleListData = new List<TechpackFileUploadModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TechpackFileUploadModel objTechpackFileUploadModel = new TechpackFileUploadModel();
                objTechpackFileUploadModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objTechpackFileUploadModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objTechpackFileUploadModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objTechpackFileUploadModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objTechpackFileUploadModel.UploadDate = dt.Rows[i]["UPLOAD_DATE"].ToString();
                objTechpackFileUploadModel.StyleSearch = dt.Rows[i]["STYLE_NO"] + " | " + dt.Rows[i]["SEASON_NAME"] + " | " + dt.Rows[i]["SEASON_YEAR"] + " | " + dt.Rows[i]["BUYER_NAME"] + " | " + dt.Rows[i]["UPLOAD_DATE"];
                StyleListData.Add(objTechpackFileUploadModel);
            }

            return StyleListData;
        }
        //View Datewise all pdf files
        [HttpGet]
        public ActionResult DateWiseTechpackFileUpload(string pShowFlag,string pViewFlag, string pDeleteFlag,string pTranId, string pSeasonYear, string pSeasonId, string pStyleNo, string pBuyerId,string pUploadDate)
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
                objTechpackFileUploadModel.UpdateBy = strEmployeeId;
                objTechpackFileUploadModel.HeadOfficeId = strHeadOfficeId;
                objTechpackFileUploadModel.BranchOfficeId = strBranchOfficeId;
                CheckUrl();

                if (!string.IsNullOrWhiteSpace(pSeasonYear) && !string.IsNullOrWhiteSpace(pSeasonId) && !string.IsNullOrWhiteSpace(pStyleNo) && !string.IsNullOrWhiteSpace(pBuyerId) && !string.IsNullOrWhiteSpace(pTranId) && !string.IsNullOrWhiteSpace(pUploadDate))
                {
                    objTechpackFileUploadModel.TranId = pTranId.Trim();
                    objTechpackFileUploadModel.SeasonYear = pSeasonYear.Trim();
                    objTechpackFileUploadModel.SeasonId = pSeasonId.Trim();
                    objTechpackFileUploadModel.StyleNo = pStyleNo.Trim();
                    objTechpackFileUploadModel.BuyerId = pBuyerId.Trim();
                    objTechpackFileUploadModel.UploadDate = pUploadDate.Trim(); 


                    if (!string.IsNullOrEmpty(pViewFlag) && pViewFlag == "1")
                    {
                        objTechpackFileUploadModel = objTechpackFileUploadDAL.ViewPdfFile(objTechpackFileUploadModel);
                        var pdfByte = objTechpackFileUploadModel.bytes;
                        return File(pdfByte, "application/pdf");
                        
                    }
                    if (!string.IsNullOrEmpty(pDeleteFlag) && pDeleteFlag == "1")
                    {
                        string strDBMsg = "";
                        strDBMsg = objTechpackFileUploadDAL.DeleteTechpackUploadFile(objTechpackFileUploadModel);
                        TempData["OperationMessage"] = strDBMsg;
                        //emptyTextBoxValue();
                    }

                  
                }

                if (!string.IsNullOrWhiteSpace(pShowFlag) && pShowFlag == "1")
                {
                    if (!string.IsNullOrWhiteSpace(pStyleNo))
                    {
                        objTechpackFileUploadModel.StyleNo = pStyleNo; 
                    }
                    if (!string.IsNullOrWhiteSpace(pSeasonId))
                    {
                        objTechpackFileUploadModel.SeasonId = pSeasonId;
                    }
                    if (!string.IsNullOrWhiteSpace(pSeasonYear))
                    {
                        objTechpackFileUploadModel.SeasonYear = pSeasonYear;
                    }
                    if (!string.IsNullOrWhiteSpace(pBuyerId))
                    {
                        objTechpackFileUploadModel.BuyerId = pBuyerId;
                    }
                    if (!string.IsNullOrWhiteSpace(pUploadDate))
                    {
                        objTechpackFileUploadModel.UploadDate = pUploadDate;
                       
                    }
                   
                }
                ViewBag.ShowCriteria = "  STYLE-" + objTechpackFileUploadModel.StyleNo + " | YEAR-" + objTechpackFileUploadModel.SeasonYear + " | DATE-" + objTechpackFileUploadModel.UploadDate;
                DataTable dt = objTechpackFileUploadDAL.DateWiseTechpackFileUpload(objTechpackFileUploadModel);
                List<TechpackFileUploadModel> TechpackFileUploadList = DateWiseTechpackFileUpload(dt);               
                ViewBag.TechpackFileUploadList = TechpackFileUploadList;
                ViewBag.BuyerDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetBuyerDDList(), "BUYER_ID", "BUYER_NAME");
                ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");

                return View(objTechpackFileUploadModel);
            }
        }
        public List<TechpackFileUploadModel> DateWiseTechpackFileUpload(DataTable dt)
        {
            List<TechpackFileUploadModel> DateWiseTechpackFileUploadDataBundle = new List<TechpackFileUploadModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TechpackFileUploadModel objTechpackFileUploadModel = new TechpackFileUploadModel();
                objTechpackFileUploadModel.SerialNumber = dt.Rows[i]["sl"].ToString();
                objTechpackFileUploadModel.TranId = dt.Rows[i]["TRAN_ID"].ToString();
                objTechpackFileUploadModel.BuyerId = dt.Rows[i]["BUYER_ID"].ToString();
                objTechpackFileUploadModel.BuyerId = dt.Rows[i]["BUYER_ID"].ToString();
                objTechpackFileUploadModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objTechpackFileUploadModel.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                objTechpackFileUploadModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objTechpackFileUploadModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objTechpackFileUploadModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objTechpackFileUploadModel.FileName = dt.Rows[i]["FILE_NAME"].ToString();
                objTechpackFileUploadModel.UploadDate = dt.Rows[i]["UPLOAD_DATE"].ToString();
               
                DateWiseTechpackFileUploadDataBundle.Add(objTechpackFileUploadModel);
            }

            return DateWiseTechpackFileUploadDataBundle;
        }
    }
}