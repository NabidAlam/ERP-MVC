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

namespace ERP.Controllers
{
    public class BOMController : Controller
    {
        LookUpDAL objLookUpDAL = new LookUpDAL();     
        BOMDAL objBOMDAL = new BOMDAL();
        BOMModel objBOMModel = new BOMModel();
        BuyerEntryModel objBuyerEntryModel = new BuyerEntryModel();
        ItemEntryModel objItemEntryModel = new ItemEntryModel();
        SeasonEntryModel objSeasonEntryModel = new SeasonEntryModel();
        UnitMerchandiserModel objUnitMerchandiserModel = new UnitMerchandiserModel();     
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
            ViewBag.BuyerDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetBuyerDDList(), "BUYER_ID", "BUYER_NAME");
            ViewBag.ItemDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetItemDDList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
            ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMerchandiserUnitDDList(), "UNIT_ID", "UNIT_NAME");
        }
        #endregion
        #region "Bom Entry"
        [HttpGet]
        public ActionResult BOMEntry(BOMModel objBOMModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();
                objBOMModel.UpdateBy = strEmployeeId;
                objBOMModel.HeadOfficeId = strHeadOfficeId;
                objBOMModel.BranchOfficeId = strBranchOfficeId;
                LoadDropDownList();
                objBOMModel.SeasonYear = ViewBag.CurrentYear;
                DataTable dt1 = objBOMDAL.GetBomRecord(objBOMModel);
                ViewBag.BOMList = BOMListData(dt1);
                ViewBag.Year = objLookUpDAL.getCurrentYear();
                return View(objBOMModel);
            }

        }
        public List<BOMModel> BOMListData(DataTable dt1)
        {
            List<BOMModel> BOMDataBundle = new List<BOMModel>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                BOMModel objBOMModel = new BOMModel();
                objBOMModel.SerialNumber = dt1.Rows[i]["SL"].ToString();
                objBOMModel.StyleNo = dt1.Rows[i]["STYLE_NO"].ToString();
                objBOMModel.BuyerId = dt1.Rows[i]["BUYER_ID"].ToString();
                objBOMModel.BuyerName = dt1.Rows[i]["BUYER_NAME"].ToString();
                objBOMModel.SeasonYear = dt1.Rows[i]["SEASON_YEAR"].ToString();
                objBOMModel.SeasonId = dt1.Rows[i]["SEASON_ID"].ToString();
                objBOMModel.SeasonName = dt1.Rows[i]["SEASON_NAME"].ToString();
                objBOMModel.R3Code = dt1.Rows[i]["R3_CODE"].ToString();
                objBOMModel.LastUpdateDate = dt1.Rows[i]["LAST_UPDATE_DATE"].ToString();
                BOMDataBundle.Add(objBOMModel);
            }

            return BOMDataBundle;
        }
        public List<BOMModel> BOMListDataForEdit(DataTable dt2)
        {
            List<BOMModel> BOMDataBundle = new List<BOMModel>();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                BOMModel objBOMModel = new BOMModel();
               
                objBOMModel.StyleNo = dt2.Rows[i]["STYLE_NO"].ToString();
                objBOMModel.BuyerId = dt2.Rows[i]["BUYER_ID"].ToString();
                objBOMModel.SeasonId = dt2.Rows[i]["SEASON_ID"].ToString();
                objBOMModel.SeasonYear = dt2.Rows[i]["SEASON_YEAR"].ToString();
                objBOMModel.R3Code = dt2.Rows[i]["R3_CODE"].ToString();
                objBOMModel.LastUpdateDate = dt2.Rows[i]["LAST_UPDATE_DATE"].ToString();
                objBOMModel.GridItemName = dt2.Rows[i]["ITEM_NAME"].ToString();
                objBOMModel.GridItemDescription = dt2.Rows[i]["ITEM_DESCRIPTION"].ToString();
                objBOMModel.GridModelCode = dt2.Rows[i]["MODEL_CODE"].ToString();
                objBOMModel.GridComponentName = dt2.Rows[i]["COMPONENT_NAME"].ToString();
                objBOMModel.GridColorId = dt2.Rows[i]["COLOR_CODE"].ToString();
                objBOMModel.GridColorName = dt2.Rows[i]["COLOR_NAME"].ToString();
                objBOMModel.GridFabricQuantity = dt2.Rows[i]["FABRIC_QUANTITY"].ToString();
                objBOMModel.GridUnitId = dt2.Rows[i]["UNIT_ID"].ToString();
                objBOMModel.GridRemarks = dt2.Rows[i]["REMARKS"].ToString();
                objBOMModel.GridTranId = dt2.Rows[i]["TRAN_ID"].ToString();
                BOMDataBundle.Add(objBOMModel);
            }

            return BOMDataBundle;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBOMEntry(HttpPostedFileBase files,BOMModel objBOMModel)
        {
            string strDBMsg = "";
            LoadSession();
            objBOMModel.UpdateBy = strEmployeeId;
            objBOMModel.HeadOfficeId = strHeadOfficeId;
            objBOMModel.BranchOfficeId = strBranchOfficeId;
             String FileExt = Path.GetExtension(files.FileName).ToUpper();
            if (FileExt == ".XLSX")
            {
                
            

            }
            else
            {
                ViewBag.FileStatus = "Invalid file format.Please select excel file";
               
            }
            strDBMsg = objBOMDAL.SaveBomEntry(objBOMModel);
            TempData["OperationMessage"] = strDBMsg;
            
            LoadDropDownList();

            DataTable dt1 = objBOMDAL.GetBomRecord(objBOMModel);
            ViewBag.BOMList = BOMListData(dt1);

            DataTable dt2 = objBOMDAL.GetBomRecordForEdit(objBOMModel);
            ViewBag.BOMListForEdit = BOMListDataForEdit(dt2);
            return View("BOMEntry");
        }
        [HttpGet]
        public ActionResult SearchBOMEntry(BOMModel objBOMModel)
        {
            LoadSession();
            objBOMModel.UpdateBy = strEmployeeId;
            objBOMModel.HeadOfficeId = strHeadOfficeId;
            objBOMModel.BranchOfficeId = strBranchOfficeId;
            ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();
            if (!string.IsNullOrWhiteSpace(objBOMModel.SearchBy))
            {
                bool exist = objBOMModel.SearchBy.Contains("|");
                if (exist)
                {
                    string a = objBOMModel.SearchBy;
                    string Style = a.Substring(0, a.IndexOf(" | ")).Trim();
                    int PositionAfterStyle = a.IndexOf(" | ") + 2;

                    string b = a.Substring(PositionAfterStyle);
                    string Season = b.Substring(0, b.IndexOf(" | ")).Trim();
                    int PositionAfterSeason = b.IndexOf(" | ") + 2;

                    string c = b.Substring(PositionAfterSeason);
                    string Year = c.Substring(0, c.IndexOf(" | ")).Trim();
                    int PositionAfterYear = c.IndexOf(" | ") + 2;

                    string Buyer = c.Substring(PositionAfterYear).Trim();


                    objBOMModel.StyleNo = Style;
                    objBOMModel.SeasonName = Season;
                    objBOMModel.SeasonYear = Year;
                    objBOMModel.BuyerName = Buyer;
                    objBOMModel.SearchBy = "";
                    if (ViewBag.CurrentYear != Year)
                    {
                        ViewBag.CurrentYear = Year;
                    }

                }
            }

            DataTable dt = objBOMDAL.GetBomRecord(objBOMModel);
            ViewBag.BOMList = BOMListData(dt);
            LoadDropDownList();
            ModelState.Clear();
           

            return View("BOMEntry");
        }
        [HttpGet]
        public ActionResult EditBomEntry(string tranId, string styleNo, string buyerId, string seasonYear, string seasonId, string lastUpdateDate, string R3Code) 
        {
            try
            {
                if (Session["strEmployeeId"] == null)
                {
                    return RedirectToAction("LogOut", "Login");
                }
                else
                {
                    LoadSession();
                    string currentYear = ViewBag.CurrentYear;
                    objBOMModel.StyleNo = styleNo;
                    objBOMModel.BuyerId = buyerId;
                   // objBOMModel.ItemTypeId = ItemTypeId;
                    if (currentYear != seasonYear)
                    {
                        ViewBag.CurrentYear = seasonYear;
                    }
                    objBOMModel.SeasonYear = seasonYear;
                    objBOMModel.SeasonId = seasonId;
                    objBOMModel.LastUpdateDate = lastUpdateDate;
                    objBOMModel.R3Code = R3Code;
                    objBOMModel.UpdateBy = strEmployeeId;
                    objBOMModel.HeadOfficeId = strHeadOfficeId;
                    objBOMModel.BranchOfficeId = strBranchOfficeId;
                    LoadDropDownList();
                    DataTable dt1 = objBOMDAL.GetBomRecord(objBOMModel);
                    ViewBag.BOMList = BOMListData(dt1);
                    DataTable dt2 = objBOMDAL.GetBomRecordForEdit(objBOMModel);
                    ViewBag.BOMListForEdit = BOMListDataForEdit(dt2);
                    return View("BOMEntry", objBOMModel);

                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        public ActionResult ClearBomEntry()
        {
            ModelState.Clear();

            CurrentYear();

            TempData["SearchValueFromSearchAction"] = "";
            return RedirectToAction("BOMEntry");
        }
        public void CurrentYear()
        {
            objBOMModel.SeasonYear = ViewBag.Year;

        }
        public JsonResult DeleteBomEntry(BOMModel objBOMModel) {
            LoadSession();
            string strDBMsg = "";
            objBOMModel.UpdateBy = strEmployeeId;
            objBOMModel.HeadOfficeId = strHeadOfficeId;
            objBOMModel.BranchOfficeId = strBranchOfficeId;
            strDBMsg = objBOMDAL.DeleteBomEntry(objBOMModel);
            return Json(strDBMsg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStyleNo(string year)
        {
            LoadSession();
            objBOMModel.UpdateBy = strEmployeeId;
            objBOMModel.HeadOfficeId = strHeadOfficeId;
            objBOMModel.BranchOfficeId = strBranchOfficeId;
            objBOMModel.SeasonYear = year;
            DataTable dt = objBOMDAL.GetAllStyleByYear(objBOMModel);
            List<BOMModel> StyleList = StyleListDataByYear(dt);
            return Json(StyleList, JsonRequestBehavior.AllowGet);
        }
        public List<BOMModel> StyleListDataByYear(DataTable dt)
        {
            List<BOMModel> StyleListData = new List<BOMModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BOMModel objBOMModel = new BOMModel();

                objBOMModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objBOMModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objBOMModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objBOMModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objBOMModel.StyleSearch = dt.Rows[i]["STYLE_NO"] + " | " + dt.Rows[i]["SEASON_NAME"] + " | " + dt.Rows[i]["SEASON_YEAR"] + " | " + dt.Rows[i]["BUYER_NAME"];
                StyleListData.Add(objBOMModel);
            }

            return StyleListData;
        }
        [HttpGet]
        public ActionResult SearchStyleFromBuyerEnquiry(BOMModel objBOMModel)
        {
            LoadSession();
            ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();
            LoadDropDownList();
            objBOMModel.UpdateBy = strEmployeeId;
            objBOMModel.HeadOfficeId = strHeadOfficeId;
            objBOMModel.BranchOfficeId = strBranchOfficeId;

            if (!string.IsNullOrWhiteSpace(objBOMModel.StyleSearch)) 
            {
                
                bool exist = objBOMModel.StyleSearch.Contains("|");
                if (exist)
                {
                    string a = objBOMModel.StyleSearch;
                    string Style = a.Substring(0, a.IndexOf(" | ")).Trim();
                    int PositionAfterStyle = a.IndexOf(" | ") + 2;

                    string b = a.Substring(PositionAfterStyle);
                    string Season = b.Substring(0, b.IndexOf(" | ")).Trim();
                    int PositionAfterSeason = b.IndexOf(" | ") + 2;

                    string c = b.Substring(PositionAfterSeason);
                    string Year = c.Substring(0, c.IndexOf(" | ")).Trim();
                    int PositionAfterYear = c.IndexOf(" | ") + 2;

                    string Buyer = c.Substring(PositionAfterYear).Trim();


                    objBOMModel.StyleNo = Style;
                    objBOMModel.SeasonName = Season;
                    objBOMModel.SeasonYear = Year;
                    objBOMModel.BuyerName = Buyer;

                    DataTable dt = objBOMDAL.GetAllStyleByYear(objBOMModel);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        objBOMModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                        objBOMModel.BuyerId = dt.Rows[i]["BUYER_ID"].ToString();
                        objBOMModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                        objBOMModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                        objBOMModel.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                        objBOMModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                    }
                    if (ViewBag.CurrentYear != Year)
                    {
                        ViewBag.CurrentYear = Year;
                    }
            }
           }

            //objBOMModel.SeasonYear = ViewBag.CurrentYear;
            DataTable dt1 = objBOMDAL.GetBomRecord(objBOMModel);
            ViewBag.BOMList = BOMListData(dt1);
            //GetTheBOM-Record Table
            objBOMModel.SeasonYear = objLookUpDAL.getCurrentYear();
            DataTable dt2 = objBOMDAL.GetBomRecord(objBOMModel);
            ViewBag.BOMList = BOMListData(dt2);

            return View("BOMEntry", objBOMModel);
        }
        public List<BOMModel> StyleList(DataTable dt1)
        {
            List<BOMModel> StyleListDataBundle = new List<BOMModel>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                BOMModel objBOMModel = new BOMModel();
                objBOMModel.SerialNumber = dt1.Rows[i]["SL"].ToString();
                objBOMModel.StyleNo = dt1.Rows[i]["STYLE_NO"].ToString();
                objBOMModel.BuyerId = dt1.Rows[i]["BUYER_ID"].ToString();
                objBOMModel.BuyerName = dt1.Rows[i]["BUYER_NAME"].ToString();
                objBOMModel.SeasonYear = dt1.Rows[i]["SEASON_YEAR"].ToString();
                objBOMModel.SeasonId = dt1.Rows[i]["SEASON_ID"].ToString();
                objBOMModel.SeasonName = dt1.Rows[i]["SEASON_NAME"].ToString();                             
                StyleListDataBundle.Add(objBOMModel);
            }

            return StyleListDataBundle;
        }


        //BOM Record Search
        public ActionResult GetStyleNoForBOM(string year)
        {
            LoadSession();
            objBOMModel.UpdateBy = strEmployeeId;
            objBOMModel.HeadOfficeId = strHeadOfficeId;
            objBOMModel.BranchOfficeId = strBranchOfficeId;
            objBOMModel.SeasonYear = year;
            DataTable dt = objBOMDAL.GetBomRecord(objBOMModel);
            List<BOMModel> StyleList1 = BOMStyleListDataByYear(dt);
            return Json(StyleList1, JsonRequestBehavior.AllowGet);
        }
        public List<BOMModel> BOMStyleListDataByYear(DataTable dt)
        {
            List<BOMModel> StyleListData = new List<BOMModel>();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BOMModel objBOMModel = new BOMModel();

                objBOMModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objBOMModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objBOMModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objBOMModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objBOMModel.StyleSearch = dt.Rows[i]["STYLE_NO"] + " | " + dt.Rows[i]["SEASON_NAME"] + " | " + dt.Rows[i]["SEASON_YEAR"] + " | " + dt.Rows[i]["BUYER_NAME"];
                StyleListData.Add(objBOMModel);
            }
            return StyleListData;
        }


            #endregion
        }
}