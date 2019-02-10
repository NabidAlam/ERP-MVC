using ERP.DAL;
using ERP.MODEL;
using ERP.Utility;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class CostingController : Controller
    {
        CostingModel objCostingModel = new CostingModel();
        LookUpDAL objLookUpDAL = new LookUpDAL();
        CostingDAL objCostingDAL = new CostingDAL();
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
           // ViewBag.ItemDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetItemDDList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
            ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMerchandiserUnitDDList(), "UNIT_ID", "UNIT_NAME");
            ViewBag.SupplierDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSupplierDDListForTP(), "SUPPLIER_ID", "SUPPLIER_NAME");
            ViewBag.CurrencyDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCurrencyDDList(), "CURRENCY_ID", "CURRENCY_NAME", "1");
            ViewBag.BranchOfficeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetBranchOfficeDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");

        }
        #endregion
        [HttpGet]
        public ActionResult CostingEntry(CostingModel objCostingModel,string EditFlag, string styleNo,string buyerId, string seasonId,string seasonYear, string DeleteFlag)
        {

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();               
                ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();
                objCostingModel.UpdateBy = strEmployeeId;
                objCostingModel.HeadOfficeId = strHeadOfficeId;
                objCostingModel.BranchOfficeId = strBranchOfficeId;
                LoadDropDownList();

                if (!string.IsNullOrWhiteSpace(objCostingModel.StyleSearch))
                {
                    bool exist = objCostingModel.StyleSearch.Contains("|");
                    if (exist)
                    {
                        string a = objCostingModel.StyleSearch;
                        string Style = a.Substring(0, a.IndexOf(" | ")).Trim();
                        int PositionAfterStyle = a.IndexOf(" | ") + 2;

                        string b = a.Substring(PositionAfterStyle);
                        string Season = b.Substring(0, b.IndexOf(" | ")).Trim();
                        int PositionAfterSeason = b.IndexOf(" | ") + 2;

                        string c = b.Substring(PositionAfterSeason);
                        string Year = c.Substring(0, c.IndexOf(" | ")).Trim();
                        int PositionAfterYear = c.IndexOf(" | ") + 2;

                        string Buyer = c.Substring(PositionAfterYear).Trim();


                        objCostingModel.StyleNo = Style;
                        objCostingModel.SeasonName = Season;
                        objCostingModel.SeasonYear = Year;
                        objCostingModel.BuyerName = Buyer;

                        DataTable dt = objCostingDAL.SearchStyleFromBOM(objCostingModel);
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            objCostingModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                            objCostingModel.BuyerId = dt.Rows[i]["BUYER_ID"].ToString();
                            objCostingModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                            objCostingModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                            objCostingModel.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                            objCostingModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                        }

                        DataTable dt2 = objCostingDAL.GetBomRecord(objCostingModel);
                        ViewBag.CostEntryList = BOMList(dt2);
                        if (ViewBag.CurrentYear != Year)
                        {
                            ViewBag.CurrentYear = Year;
                        }

                }
                }
                if (!string.IsNullOrWhiteSpace(styleNo)&& !string.IsNullOrWhiteSpace(buyerId) && !string.IsNullOrWhiteSpace(seasonId) && !string.IsNullOrWhiteSpace(seasonYear))
                {
                    if (EditFlag == "1")
                    {
                        objCostingModel.StyleNo = styleNo;
                        objCostingModel.SeasonId = seasonId;
                        objCostingModel.SeasonYear = seasonYear;
                        objCostingModel.BuyerId = buyerId;
                        objCostingModel = objCostingDAL.GetCostingEntryMainForEdit(objCostingModel);
                        DataTable dt = objCostingDAL.GetListRecordForEdit(objCostingModel);
                        ViewBag.CostEntryList = CostingEntryListDataForEdit(dt);
                        ViewBag.CurrentYear = seasonYear;
                    }
                }
                if (!string.IsNullOrEmpty(objCostingModel.SearchBy))
                {
                    bool exist = objCostingModel.SearchBy.Contains("|");
                    if (exist)
                    {
                        objCostingModel.SearchByFlag = "1";
                        string a = objCostingModel.SearchBy;
                        string Style = a.Substring(0, a.IndexOf(" | ")).Trim();
                        int PositionAfterStyle = a.IndexOf(" | ") + 2;

                        string b = a.Substring(PositionAfterStyle);
                        string Season = b.Substring(0, b.IndexOf(" | ")).Trim();
                        int PositionAfterSeason = b.IndexOf(" | ") + 2;

                        string c = b.Substring(PositionAfterSeason);
                        string Year = c.Substring(0, c.IndexOf(" | ")).Trim();
                        int PositionAfterYear = c.IndexOf(" | ") + 2;

                        string Buyer = c.Substring(PositionAfterYear).Trim();


                        objCostingModel.StyleNo = Style;
                        objCostingModel.SeasonName = Season;
                        objCostingModel.SeasonYear = Year;
                        objCostingModel.BuyerName = Buyer;
                        objCostingModel.SearchBy = "";
                        if (ViewBag.CurrentYear != Year)
                        {
                            ViewBag.CurrentYear = Year;
                        }

                    }
                  
                }
                else {

                    string currentYear = objLookUpDAL.getCurrentYear();
                    objCostingModel.SeasonYear = currentYear;

                }
               
                DataTable dt1 = objCostingDAL.GetCostingEntryRecord(objCostingModel);
                ViewBag.CostingEntryList = CostingEntryListData(dt1);
                if (!string.IsNullOrWhiteSpace(objCostingModel.SearchByFlag)&& objCostingModel.SearchByFlag=="1")
                {
                    objCostingModel.StyleNo = "";
                }
                return View(objCostingModel);
            }
        }
        public List<CostingModel> CostingEntryListDataForEdit(DataTable dt)
        {
            List<CostingModel> CostingEntryDataBundle = new List<CostingModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CostingModel objCostingModel = new CostingModel();
                objCostingModel.SerialNo = dt.Rows[i]["SL"].ToString();
                objCostingModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objCostingModel.BuyerId = dt.Rows[i]["BUYER_ID"].ToString();
                objCostingModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objCostingModel.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                objCostingModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objCostingModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objCostingModel.MpcCode = dt.Rows[i]["MPC_CODE"].ToString();
                objCostingModel.CurrencyName = dt.Rows[i]["CURRENCY_NAME"].ToString();
                objCostingModel.ProductName = dt.Rows[i]["PRODUCT_NAME"].ToString();
                objCostingModel.FactoryId = dt.Rows[i]["FACTORY_ID"].ToString();
                objCostingModel.FactoryName = dt.Rows[i]["FACTORY_NAME"].ToString();
                objCostingModel.ExchangeRate = dt.Rows[i]["EXCHANGE_RATE"].ToString();
                objCostingModel.CotationDate = dt.Rows[i]["COTATION_DATE"].ToString();
               // objCostingModel.GridItemId = dt.Rows[i]["ITEM_ID"].ToString();
                objCostingModel.GridItemName = dt.Rows[i]["ITEM_NAME"].ToString();
                objCostingModel.GridItemType = dt.Rows[i]["ITEM_TYPE"].ToString();
                objCostingModel.GridItemDescription = dt.Rows[i]["ITEM_DESCRIPTION"].ToString();
                objCostingModel.GridModelCode = dt.Rows[i]["MODEL_CODE"].ToString();
                objCostingModel.GridSupplierId = dt.Rows[i]["SUPPLIER_ID"].ToString();
                objCostingModel.GridSupplierName = dt.Rows[i]["SUPPLIER_NAME"].ToString();
                objCostingModel.GridUnitId = dt.Rows[i]["UNIT_ID"].ToString();
                objCostingModel.GridUnitName = dt.Rows[i]["UNIT_NAME"].ToString();
                objCostingModel.GridUnitPrice = dt.Rows[i]["UNIT_PRICE"].ToString();
                objCostingModel.GridConsump = dt.Rows[i]["CONSUMP"].ToString();
                objCostingModel.GridWastage = dt.Rows[i]["WASTAGE"].ToString();
                objCostingModel.GridPrice = dt.Rows[i]["PRICE"].ToString();
                objCostingModel.GridRate = dt.Rows[i]["RATE"].ToString();
                objCostingModel.GridTranId = dt.Rows[i]["TRAN_ID"].ToString();
                CostingEntryDataBundle.Add(objCostingModel);
            }

            return CostingEntryDataBundle;
        }
        public List<CostingModel> CostingEntryListData(DataTable dt1)
        {
            List<CostingModel> CostingEntryDataBundle = new List<CostingModel>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                CostingModel objCostingModel = new CostingModel();
                objCostingModel.SerialNo = dt1.Rows[i]["SL"].ToString();
                objCostingModel.StyleNo = dt1.Rows[i]["STYLE_NO"].ToString();
                objCostingModel.BuyerId = dt1.Rows[i]["BUYER_ID"].ToString();
                objCostingModel.BuyerName = dt1.Rows[i]["BUYER_NAME"].ToString();
                objCostingModel.SeasonId = dt1.Rows[i]["SEASON_ID"].ToString();
                objCostingModel.SeasonName = dt1.Rows[i]["SEASON_NAME"].ToString();
                objCostingModel.SeasonYear = dt1.Rows[i]["SEASON_YEAR"].ToString();
                objCostingModel.MpcCode = dt1.Rows[i]["MPC_CODE"].ToString();
                objCostingModel.CurrencyName = dt1.Rows[i]["CURRENCY_NAME"].ToString();
                objCostingModel.ProductName = dt1.Rows[i]["PRODUCT_NAME"].ToString();
                objCostingModel.FactoryId = dt1.Rows[i]["FACTORY_ID"].ToString();
                objCostingModel.FactoryName = dt1.Rows[i]["FACTORY_NAME"].ToString();
                objCostingModel.ExchangeRate = dt1.Rows[i]["EXCHANGE_RATE"].ToString();
                objCostingModel.CotationDate = dt1.Rows[i]["COTATION_DATE"].ToString();
                //objCostingModel.GridItemId = dt1.Rows[i]["ITEM_ID"].ToString();
                //objCostingModel.GridItemName = dt1.Rows[i]["ITEM_NAME"].ToString();
                //objCostingModel.GridItemType = dt1.Rows[i]["ITEM_TYPE"].ToString();
                //objCostingModel.GridItemDescription = dt1.Rows[i]["ITEM_DESCRIPTION"].ToString();
                //objCostingModel.GridModelCode = dt1.Rows[i]["MODEL_CODE"].ToString();
                //objCostingModel.GridSupplierId = dt1.Rows[i]["SUPPLIER_ID"].ToString();
                //objCostingModel.GridSupplierName = dt1.Rows[i]["SUPPLIER_NAME"].ToString();
                //objCostingModel.GridUnitId = dt1.Rows[i]["UNIT_ID"].ToString();
                //objCostingModel.GridUnitName = dt1.Rows[i]["UNIT_NAME"].ToString();
                //objCostingModel.GridUnitPrice = dt1.Rows[i]["UNIT_PRICE"].ToString();
                //objCostingModel.GridConsump = dt1.Rows[i]["CONSUMP"].ToString();
                //objCostingModel.GridWastage = dt1.Rows[i]["WASTAGE"].ToString();
                //objCostingModel.GridPrice = dt1.Rows[i]["PRICE"].ToString();
                //objCostingModel.GridRate = dt1.Rows[i]["RATE"].ToString();
                //objCostingModel.GridTranId = dt1.Rows[i]["TRAN_ID"].ToString();
                CostingEntryDataBundle.Add(objCostingModel);
            }

            return CostingEntryDataBundle;
        }
        public List<CostingModel> BOMList(DataTable dt2)
        {
            List<CostingModel> BOMDataBundle = new List<CostingModel>();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                CostingModel objCostingModel = new CostingModel();
                objCostingModel.GridItemName = dt2.Rows[i]["ITEM_NAME"].ToString();
                objCostingModel.GridItemType = dt2.Rows[i]["ITEM_DESCRIPTION"].ToString();
                objCostingModel.GridModelCode = dt2.Rows[i]["MODEL_CODE"].ToString();
                objCostingModel.GridUnitId = dt2.Rows[i]["UNIT_ID"].ToString();              
                BOMDataBundle.Add(objCostingModel);
            }
            return BOMDataBundle;
        }
        public ActionResult GetStyleNo(string year)
        {
            LoadSession();
            objCostingModel.UpdateBy = strEmployeeId;
            objCostingModel.HeadOfficeId = strHeadOfficeId;
            objCostingModel.BranchOfficeId = strBranchOfficeId;
            objCostingModel.SeasonYear = year;
            DataTable dt = objCostingDAL.SearchStyleFromBOM(objCostingModel);
            List<CostingModel> StyleList = StyleListDataByYear(dt);
            return Json(StyleList, JsonRequestBehavior.AllowGet);
        }
        public List<CostingModel> StyleListDataByYear(DataTable dt)
        {
            List<CostingModel> StyleListData = new List<CostingModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CostingModel objCostingModel = new CostingModel();
                objCostingModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objCostingModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objCostingModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objCostingModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objCostingModel.StyleSearch = dt.Rows[i]["STYLE_NO"]+" | " + dt.Rows[i]["SEASON_NAME"]+" | "+ dt.Rows[i]["SEASON_YEAR"] + " | " + dt.Rows[i]["BUYER_NAME"];
                StyleListData.Add(objCostingModel);
            }

            return StyleListData;
        }
        public ActionResult GetCostingStyleNo()
        {
            LoadSession();
            objCostingModel.UpdateBy = strEmployeeId;
            objCostingModel.HeadOfficeId = strHeadOfficeId;
            objCostingModel.BranchOfficeId = strBranchOfficeId;
            //objCostingModel.SeasonYear = year;
           

            DataTable dt = objCostingDAL.SearchStyleFromCosting(objCostingModel);
            List<CostingModel> StyleList = StyleListDataFromCosting(dt);
            return Json(StyleList, JsonRequestBehavior.AllowGet);
        }
        public List<CostingModel> StyleListDataFromCosting(DataTable dt)
        {
            List<CostingModel> StyleListData = new List<CostingModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CostingModel objCostingModel = new CostingModel();
                objCostingModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objCostingModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objCostingModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objCostingModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objCostingModel.StyleSearch = dt.Rows[i]["STYLE_NO"] + " | " + dt.Rows[i]["SEASON_NAME"] + " | " + dt.Rows[i]["SEASON_YEAR"] + " | " + dt.Rows[i]["BUYER_NAME"];
                StyleListData.Add(objCostingModel);
            }

            return StyleListData;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCostingEntry(CostingModel objCostingModel)
        {
            LoadSession();
            objCostingModel.UpdateBy = strEmployeeId;
            objCostingModel.HeadOfficeId = strHeadOfficeId;
            objCostingModel.BranchOfficeId = strBranchOfficeId;            
            string strDBMsg = "";
            strDBMsg = objCostingDAL.SaveCostingEntry(objCostingModel);
            TempData["OperationMessage"] = strDBMsg;      
            LoadDropDownList();          
            ModelState.Clear();
            return RedirectToAction("CostingEntry");
        }
        public ActionResult ClearBomEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("CostingEntry");
            //LoadDropDownList();
            //return View("CostingEntry");
        }
        public ActionResult DeleteCostingEntry(CostingModel objCostingModel)
        {
            LoadSession();
            string strDBMsg = "";
            objCostingModel.UpdateBy = strEmployeeId;
            objCostingModel.HeadOfficeId = strHeadOfficeId;
            objCostingModel.BranchOfficeId = strBranchOfficeId;
            strDBMsg = objCostingDAL.DeleteCostingEntry(objCostingModel);
            return Json(strDBMsg, JsonRequestBehavior.AllowGet);
        }
    }
}