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

namespace ERP.Controllers
{
    public class DeliveryPlaceController : Controller
    {

        #region "Common"

        LookUpDAL objLookUpDAL = new LookUpDAL();
        EmployeeDAL objEmployeeDAL = new EmployeeDAL();

        string strEmployeeId = "";
        string strDesignationId = "";
        string strSubSectionId = "";
        string strUnitId = "";
        string strHeadOfficeId = "";
        string strBranchOfficeId = "";
        string strSoftId = "";
        string strOldUrl = "";

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
        public void LoadEmptySession()
        {
            Session["EditFlag"] = null;
            Session["CreateFlag"] = null;
            Session["SearchFlag"] = null;
            Session["SearchForEditFlag"] = null;
            Session["SearchValue"] = null;
            Session["EditPageNumber"] = null;
            Session["UpdateSearchValue"] = null;
        }
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
        public void LoadDropDownList() {

            ViewBag.CountryDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCountryDDListForMerchandising(), "COUNTRY_ID", "COUNTRY_NAME");

        }

        #endregion

        DeliveryPlaceModel objDeliveryPlaceModel = new DeliveryPlaceModel();
        [HttpGet]
        public ActionResult DeliveryPlaceEntry(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                LoadDropDownList();
                objDeliveryPlaceModel.UpdateBy = strEmployeeId;
                objDeliveryPlaceModel.HeadOfficeId = strHeadOfficeId;
                objDeliveryPlaceModel.BranchOfficeId = strBranchOfficeId;
                #region Pagination Search
                CheckUrl();
                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objDeliveryPlaceModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy.Trim();
                }
                if (TempData.ContainsKey("SearchValue"))
                {
                    objDeliveryPlaceModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }
                if (!string.IsNullOrWhiteSpace(pId))
                {

                    objDeliveryPlaceModel.DeliveryPlaceId = pId;
                    objDeliveryPlaceModel = objLookUpDAL.GetDeliveryPlaceEntryById(objDeliveryPlaceModel);
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
                DataTable dt = objLookUpDAL.GetDeliveryPlaceEntryRecord(objDeliveryPlaceModel);
                List<DeliveryPlaceModel> DeliveryPlaceList = DeliveryPlaceListData(dt);
                ViewBag.DeliveryPlaceList = DeliveryPlaceList.ToPagedList(page, pageSize);
                return View(objDeliveryPlaceModel);
            }
        }
        public List<DeliveryPlaceModel> DeliveryPlaceListData(DataTable dt)
        {
            List<DeliveryPlaceModel> DeliveryPlaceListDataBundle = new List<DeliveryPlaceModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DeliveryPlaceModel objDeliveryPlaceModel = new DeliveryPlaceModel();
                objDeliveryPlaceModel.SerialNumber = dt.Rows[i]["sl"].ToString();
                objDeliveryPlaceModel.CountryId = dt.Rows[i]["COUNTRY_ID"].ToString();
                objDeliveryPlaceModel.CountryName = dt.Rows[i]["COUNTRY_NAME"].ToString();
                objDeliveryPlaceModel.DeliveryPlaceId = dt.Rows[i]["DELIVERY_PLACE_ID"].ToString();
                objDeliveryPlaceModel.DeliveryPlaceName = dt.Rows[i]["DELIVERY_PLACE_NAME"].ToString();
                DeliveryPlaceListDataBundle.Add(objDeliveryPlaceModel);
            }

            return DeliveryPlaceListDataBundle;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDeliveryPlaceEntry(DeliveryPlaceModel objDeliveryPlaceModel)
        {
            LoadSession();
            objDeliveryPlaceModel.UpdateBy = strEmployeeId;
            objDeliveryPlaceModel.HeadOfficeId = strHeadOfficeId;
            objDeliveryPlaceModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveDeliveryPlaceEntry(objDeliveryPlaceModel);
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

            return RedirectToAction("DeliveryPlaceEntry");
        }
        public ActionResult ClearDeliveryPlaceEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("DeliveryPlaceEntry");
        }

    }
}