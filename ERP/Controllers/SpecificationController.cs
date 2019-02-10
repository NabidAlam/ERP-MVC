using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.DAL;
using ERP.MODEL;
using ERP.Utility;
using PagedList;

namespace ERP.Controllers
{
    public class SpecificationController : Controller
    {
        LookUpDAL objLookUpDAL = new LookUpDAL();

        SpecificationDAL objSpecificationDAL = new SpecificationDAL();
        //DressSpecModel objDressSpecModel = new DressSpecModel();

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
        #endregion



        public List<SpecificationModel> GetDressSpecListByDataTable(DataTable dt)
        {
            List<SpecificationModel> styleList = new List<SpecificationModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SpecificationModel objDressSpecModel = new SpecificationModel();

                objDressSpecModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objDressSpecModel.StyleName = dt.Rows[i]["STYLE_NAME"].ToString();
                objDressSpecModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objDressSpecModel.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                objDressSpecModel.SpecificationDate = dt.Rows[i]["SPECIFICATION_DATE"].ToString();

                if (dt.Columns.Contains("SL") && dt.Columns.Contains("SEASON_NAME"))
                {
                    objDressSpecModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                    objDressSpecModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                }
                else
                {
                    objDressSpecModel.GridDescription = dt.Rows[i]["SPEC_DESCRIPTION"].ToString();
                    objDressSpecModel.GridSizeId = dt.Rows[i]["SIZE_ID"].ToString();
                    objDressSpecModel.GridSizeValue = dt.Rows[i]["SIZE_VALUE"].ToString();
                    objDressSpecModel.GridTranId = dt.Rows[i]["TRAN_ID"].ToString();
                }

                styleList.Add(objDressSpecModel);
            }

            return styleList;
        }

        public List<SpecificationModel> GetStyleListByDataTable(DataTable dt)
        {
            List<SpecificationModel> dressSpecList = new List<SpecificationModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SpecificationModel objDressSpecModel = new SpecificationModel();
                objDressSpecModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objDressSpecModel.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objDressSpecModel.StyleName = dt.Rows[i]["STYLE_NAME"].ToString();
                objDressSpecModel.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objDressSpecModel.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                objDressSpecModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();

                dressSpecList.Add(objDressSpecModel);
            }

            return dressSpecList;
        }



        [HttpGet]
        public ActionResult SpecEntry()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                SpecificationModel objDressSpecModel = new SpecificationModel();

                objDressSpecModel.UpdateBy = strEmployeeId;
                objDressSpecModel.HeadOfficeId = strHeadOfficeId;
                objDressSpecModel.BranchOfficeId = strBranchOfficeId;

                ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();


                ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
                ViewBag.SizeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSizeDDList(), "SIZE_ID", "SIZE_NAME");

                DataTable dt = objSpecificationDAL.GetDressSpecRecord(objDressSpecModel);
                ViewBag.DressSpecList = GetDressSpecListByDataTable(dt);

                ViewBag.Year = objLookUpDAL.getCurrentYear();

                return View(objDressSpecModel);
            }
        }


        public ActionResult ClearDressSpecEntry()
        {
            ModelState.Clear();

            TempData["SearchValueFromSearchAction"] = "";

            return RedirectToAction("SpecEntry");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDressSpecEntry(SpecificationModel objDressSpecModel)
        {
            LoadSession();

            objDressSpecModel.UpdateBy = strEmployeeId;
            objDressSpecModel.HeadOfficeId = strHeadOfficeId;
            objDressSpecModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDbMsg = objSpecificationDAL.SaveDressSpecEntry(objDressSpecModel);
                TempData["OperationMessage"] = strDbMsg;
            }


            //ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
            //ViewBag.SizeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSizeDDList(), "SIZE_ID", "SIZE_NAME");

            //DataTable objDataTable = objSpecificationDAL.GetDressSpecRecord(objDressSpecModel);
            //ViewBag.DressSpecList = TrimListData(objDataTable);

            ModelState.Clear();

            //return View("DressSpecEntry");
            return RedirectToAction("SpecEntry", "Specification");
        }

        [HttpGet]
        public ActionResult SearchStyleFromProductMain(SpecificationModel objDressSpecModel, string pSelectFlag, string pStyleNo, string pStyleName, string pSeasonYear, string pSeasonId, int page = 1, int pageSize = 10)
        {
            LoadSession();

            ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();

            objDressSpecModel.UpdateBy = strEmployeeId;
            objDressSpecModel.HeadOfficeId = strHeadOfficeId;
            objDressSpecModel.BranchOfficeId = strBranchOfficeId;

            ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
            ViewBag.SizeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSizeDDList(), "SIZE_ID", "SIZE_NAME");

            #region Pagination Search

            CheckUrl();

            if (!string.IsNullOrWhiteSpace(objDressSpecModel.StyleSearch))
            {
                TempData["SearchFlag"] = 1;
                TempData["SearchPage"] = page;
                TempData["SearchValue"] = objDressSpecModel.StyleSearch;
            }

            if (TempData.ContainsKey("SearchValue"))
            {
                objDressSpecModel.StyleSearch = TempData["SearchValue"].ToString();
                TempData.Keep("SearchValue");
            }

            if (!string.IsNullOrWhiteSpace(pSeasonYear) && !string.IsNullOrWhiteSpace(pSeasonId))
            {
                objDressSpecModel.StyleNo = pStyleNo.Trim();
                objDressSpecModel.StyleName = pStyleName.Trim();

                objDressSpecModel.SeasonYear = pSeasonYear.Trim();
                objDressSpecModel.SeasonId = pSeasonId.Trim();
                objDressSpecModel.UpdateBy = strEmployeeId;
                objDressSpecModel.HeadOfficeId = strHeadOfficeId;
                objDressSpecModel.BranchOfficeId = strBranchOfficeId;

                if (!string.IsNullOrWhiteSpace(pSelectFlag) && pSelectFlag == "1")
                {
                    objDressSpecModel = objSpecificationDAL.GetStyleWiseFromProductMain(objDressSpecModel);
                    if (objDressSpecModel.SeasonYear.Length > 3)
                    {
                        ViewBag.CurrentYear = objDressSpecModel.SeasonYear;
                    }
                    else
                    {
                        ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();
                    }

                    ViewBag.HideFlag = 1;
                }

                page = (int)TempData["GetActionPage"];
                TempData.Keep("GetActionPage");
            }
            TempData["GetActionFlag"] = 1;
            TempData["GetActionPage"] = page;

            #endregion

            DataTable dt = objSpecificationDAL.SearchStyleFromProductMain(objDressSpecModel);
            ViewBag.StyleList = GetStyleListByDataTable(dt).ToPagedList(page, pageSize);

            return View("SpecEntry", objDressSpecModel);
        }

        [HttpGet]
        public ActionResult SearchDressSpecEntry(SpecificationModel objDressSpecModel)
        {
            LoadSession();

            objDressSpecModel.UpdateBy = strEmployeeId;
            objDressSpecModel.HeadOfficeId = strHeadOfficeId;
            objDressSpecModel.BranchOfficeId = strBranchOfficeId;

            DataTable dt = objSpecificationDAL.GetDressSpecRecord(objDressSpecModel);
            ViewBag.DressSpecList = GetDressSpecListByDataTable(dt);

            ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
            ViewBag.SizeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSizeDDList(), "SIZE_ID", "SIZE_NAME");

            ModelState.Clear();

            return View("SpecEntry");
        }

        [HttpGet]
        public ActionResult EditDressSpecEntry(string pStyleNo, string pStyleName, string pSeasonId, string pSeasonYear, string pSpecificationDate)
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

                    SpecificationModel objDressSpecModel = new SpecificationModel();

                    string currentYear = ViewBag.CurrentYear;

                    objDressSpecModel.StyleNo = pStyleNo;
                    objDressSpecModel.StyleName = pStyleName;
                    objDressSpecModel.SeasonId = pSeasonId;

                    if (currentYear != pSeasonYear)
                    {
                        ViewBag.CurrentYear = pSeasonYear;
                    }

                    objDressSpecModel.SeasonYear = pSeasonYear;
                    objDressSpecModel.SpecificationDate = pSpecificationDate;

                    objDressSpecModel.CreateBy = strEmployeeId;
                    objDressSpecModel.UpdateBy = strEmployeeId;
                    objDressSpecModel.HeadOfficeId = strHeadOfficeId;
                    objDressSpecModel.BranchOfficeId = strBranchOfficeId;

                    ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
                    ViewBag.SizeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSizeDDList(), "SIZE_ID", "SIZE_NAME");
                    
                    DataTable dt = objSpecificationDAL.GetDressSpecRecord(objDressSpecModel);
                    ViewBag.DressSpecList = GetDressSpecListByDataTable(dt);

                    DataTable dt2 = objSpecificationDAL.GetDressSpecDetailRecord(objDressSpecModel);
                    ViewBag.DressSpecListForEdit = GetDressSpecListByDataTable(dt2);


                    return View("SpecEntry", objDressSpecModel);

                }
            }
            catch (Exception ex)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [ValidateAntiForgeryToken]
        public JsonResult DeleteDressSpecEntry(SpecificationModel objDressSpecModel)
        {
            LoadSession();

            objDressSpecModel.UpdateBy = strEmployeeId;
            objDressSpecModel.HeadOfficeId = strHeadOfficeId;
            objDressSpecModel.BranchOfficeId = strBranchOfficeId;

            string strDbMsg = objSpecificationDAL.DeleteTrimsEntry(objDressSpecModel);

            return Json(strDbMsg, JsonRequestBehavior.AllowGet);
        }


    }
}