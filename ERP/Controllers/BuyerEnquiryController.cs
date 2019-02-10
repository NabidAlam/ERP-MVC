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

namespace ERP.Controllers
{
    public class BuyerEnquiryController : Controller
    {
          
        LookUpDAL objLookUpDAL = new LookUpDAL();
        BuyerEnquiryModel objBuyerEnquiryModel = new BuyerEnquiryModel();
        BuyerEnquiryDAL objBuyerEnquiryDAL = new BuyerEnquiryDAL();
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

            objBuyerEnquiryModel.BuyerId = "0";
            objBuyerEnquiryModel.SeasonId = "0";
            objBuyerEnquiryModel.SeasonYear = ViewBag.CurrentYear;
            objBuyerEnquiryModel.StyleNo = "";
            objBuyerEnquiryModel.BrandId = "0";
            objBuyerEnquiryModel.ItemId = "0";
            objBuyerEnquiryModel.OrderQuantity = "";
            objBuyerEnquiryModel.FOB = "";
            objBuyerEnquiryModel.CurrencyTypeId = "0";
        }

        [HttpGet]
        public ActionResult GetBuyerEnquiryRecord(string pEditFlag, string pDeleteFlag, string pSeasonYear, string pSeasonId, string pStyleNo, string pBuyerId,int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
              
                ViewBag.CurrentYear= objLookUpDAL.getCurrentYear();
                objBuyerEnquiryModel.UpdateBy = strEmployeeId;
                objBuyerEnquiryModel.HeadOfficeId = strHeadOfficeId;
                objBuyerEnquiryModel.BranchOfficeId = strBranchOfficeId;

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

                        string Buyer = c.Substring(PositionAfterYear).Trim();


                        objBuyerEnquiryModel.StyleNo = Style;
                        objBuyerEnquiryModel.SeasonName = Season;
                        objBuyerEnquiryModel.SeasonYear = Year;
                        objBuyerEnquiryModel.BuyerName = Buyer;
                        if (ViewBag.CurrentYear != Year)
                        {
                            ViewBag.CurrentYear = Year;
                        }

                    }
                    else {

                        objBuyerEnquiryModel.SearchBy = SearchBy;

                        TempData["SearchFlag"] = 1;
                        TempData["SearchPage"] = page;
                        TempData["SearchValue"] = SearchBy;
                    }


                    
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objBuyerEnquiryModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pSeasonYear) && !string.IsNullOrWhiteSpace(pSeasonId) && !string.IsNullOrWhiteSpace(pStyleNo) && !string.IsNullOrWhiteSpace(pBuyerId))
                {
                    objBuyerEnquiryModel.SeasonYear = pSeasonYear.Trim();
                    objBuyerEnquiryModel.SeasonId = pSeasonId.Trim();
                    objBuyerEnquiryModel.StyleNo = pStyleNo.Trim();
                    objBuyerEnquiryModel.BuyerId = pBuyerId.Trim();
                    objBuyerEnquiryModel.UpdateBy = strEmployeeId;
                    objBuyerEnquiryModel.HeadOfficeId = strHeadOfficeId;
                    objBuyerEnquiryModel.BranchOfficeId = strBranchOfficeId;
                    if (!string.IsNullOrWhiteSpace(pEditFlag) && pEditFlag=="1")
                    {
                        objBuyerEnquiryModel = objBuyerEnquiryDAL.GetBuyerEnquiryById(objBuyerEnquiryModel);
                        string currentYear = objLookUpDAL.getCurrentYear();
                        if (currentYear!= objBuyerEnquiryModel.SeasonYear)
                        {
                            ViewBag.CurrentYear = objBuyerEnquiryModel.SeasonYear;
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(pDeleteFlag) && pDeleteFlag=="1")
                    {
                        string strDBMsg = "";
                        strDBMsg = objBuyerEnquiryDAL.DeleteBuyerEnquiryRecord(objBuyerEnquiryModel);
                        TempData["OperationMessage"] = strDBMsg;
                        emptyTextBoxValue();


                    }

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

                DataTable dt = objBuyerEnquiryDAL.GetBuyerEnquiryRecord(objBuyerEnquiryModel);
                List<BuyerEnquiryModel> BuyerEnquiryList = BuyerEnquiryListData(dt);
                ViewBag.BuyerEnquiryList = BuyerEnquiryList.ToPagedList(page, pageSize);
                ViewBag.ItemDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetItemDDList(), "ITEM_ID", "ITEM_NAME");
                ViewBag.BrandDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetBrandDDList(), "BRAND_ID", "BRAND_NAME");
                ViewBag.BuyerDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetBuyerDDList(), "BUYER_ID", "BUYER_NAME");
                ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
                ViewBag.CurrencyDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCurrencyDDList(), "CURRENCY_ID", "CURRENCY_NAME","1");
                emptyTextBoxValue();
                return View(objBuyerEnquiryModel);
            }
        }
        public List<BuyerEnquiryModel> BuyerEnquiryListData(DataTable dt)
        {
            List<BuyerEnquiryModel> BuyerEnquiryDataBundle = new List<BuyerEnquiryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BuyerEnquiryModel objBuyerEnquiryModel = new BuyerEnquiryModel();
                objBuyerEnquiryModel.SerialNumber = dt.Rows[i]["sl"].ToString();
                objBuyerEnquiryModel.BuyerId = dt.Rows[i]["BUYER_ID"].ToString();
                objBuyerEnquiryModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objBuyerEnquiryModel.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                objBuyerEnquiryModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objBuyerEnquiryModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objBuyerEnquiryModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objBuyerEnquiryModel.BrandId = dt.Rows[i]["BRAND_ID"].ToString();
                objBuyerEnquiryModel.BrandName = dt.Rows[i]["BRAND_NAME"].ToString();
                objBuyerEnquiryModel.ItemId = dt.Rows[i]["ITEM_ID"].ToString();
                objBuyerEnquiryModel.ItemName = dt.Rows[i]["ITEM_NAME"].ToString();
                objBuyerEnquiryModel.OrderQuantity = dt.Rows[i]["ORDER_QUANTITY"].ToString();
                objBuyerEnquiryModel.FOB = dt.Rows[i]["FOB"].ToString();
                objBuyerEnquiryModel.CurrencyTypeId = dt.Rows[i]["CURRENCY_ID"].ToString();
                objBuyerEnquiryModel.CurrencyTypeName = dt.Rows[i]["CURRENCY_NAME"].ToString();
                BuyerEnquiryDataBundle.Add(objBuyerEnquiryModel);
            }

            return BuyerEnquiryDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBuyerEnquiry(BuyerEnquiryModel objBuyerEnquiryModel)
        {
            LoadSession();
            objBuyerEnquiryModel.UpdateBy = strEmployeeId;
            objBuyerEnquiryModel.HeadOfficeId = strHeadOfficeId;
            objBuyerEnquiryModel.BranchOfficeId = strBranchOfficeId;
            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objBuyerEnquiryDAL.SaveBuyerEnquiry(objBuyerEnquiryModel);
                TempData["OperationMessage"] = strDBMsg;
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

            emptyTextBoxValue();
            return RedirectToAction("GetBuyerEnquiryRecord");
        }
        
        public ActionResult ClearBuyerEnquiryEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("GetBuyerEnquiryRecord");
        }

        public ActionResult GetStyleNo(string year)
        {
            LoadSession();
            objBuyerEnquiryModel.UpdateBy = strEmployeeId;
            objBuyerEnquiryModel.HeadOfficeId = strHeadOfficeId;
            objBuyerEnquiryModel.BranchOfficeId = strBranchOfficeId;
            //objBuyerEnquiryModel.SeasonYear = year;
            DataTable dt = objBuyerEnquiryDAL.GetAllStyleByYear(objBuyerEnquiryModel);
            List<BuyerEnquiryModel> StyleList = StyleListDataByYear(dt);
            return Json(StyleList, JsonRequestBehavior.AllowGet);
        }

        public List<BuyerEnquiryModel> StyleListDataByYear(DataTable dt)
        {
            List<BuyerEnquiryModel> StyleListData = new List<BuyerEnquiryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BuyerEnquiryModel objBuyerEnquiryModel = new BuyerEnquiryModel();
              
                objBuyerEnquiryModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                // objBOMModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objBuyerEnquiryModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objBuyerEnquiryModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objBuyerEnquiryModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objBuyerEnquiryModel.StyleSearch = dt.Rows[i]["STYLE_NO"] + " | " + dt.Rows[i]["SEASON_NAME"] + " | " + dt.Rows[i]["SEASON_YEAR"] + " | " + dt.Rows[i]["BUYER_NAME"];

                StyleListData.Add(objBuyerEnquiryModel);
            }

            return StyleListData;
        }
    }
}