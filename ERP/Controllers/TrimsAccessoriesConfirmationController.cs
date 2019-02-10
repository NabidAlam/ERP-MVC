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
using System.Web.Script.Serialization;

namespace ERP.Controllers
{
    public class TrimsAccessoriesConfirmationController : Controller
    {
        LookUpDAL objLookUpDAL = new LookUpDAL();
        TrimAccessoriesDAL objAccessoriesDAL = new TrimAccessoriesDAL();
        TrimsAccessoriesOrderModel objAccessoriesOrderModel = new TrimsAccessoriesOrderModel();

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

            ViewBag.ItemDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetItemCPDDList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitTP_DDList(), "UNIT_ID", "UNIT_NAME");
            ViewBag.GetLocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLocationDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");
            ViewBag.GetOnlySupplierDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetOnlySupplierDDList(), "SUPPLIER_ID", "SUPPLIER_NAME");


        }
        #endregion

        public List<TrimsAccessoriesOrderModel> TrimListData(DataTable dt1)
        {
            List<TrimsAccessoriesOrderModel> TrimsDataBundle = new List<TrimsAccessoriesOrderModel>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                TrimsAccessoriesOrderModel objAccessoriesOrderModel = new TrimsAccessoriesOrderModel();
                objAccessoriesOrderModel.SerialNumber = dt1.Rows[i]["SL"].ToString();
                objAccessoriesOrderModel.GridOrderDate = dt1.Rows[i]["ORDER_DATE"].ToString();
                objAccessoriesOrderModel.GridItemId = dt1.Rows[i]["ITEM_ID"].ToString();
                objAccessoriesOrderModel.GridItemName = dt1.Rows[i]["ITEM_NAME"].ToString();
                objAccessoriesOrderModel.GridItemCode = dt1.Rows[i]["ITEM_CODE"].ToString();
                objAccessoriesOrderModel.GridOrderQty = dt1.Rows[i]["ORDER_QUANTITY"].ToString();
                objAccessoriesOrderModel.GridUnitId = dt1.Rows[i]["UNIT_ID"].ToString();
                objAccessoriesOrderModel.GridUnitName = dt1.Rows[i]["UNIT_NAME"].ToString();
                objAccessoriesOrderModel.GridDeliveryDate = dt1.Rows[i]["DELIVERY_DATE"].ToString();
                objAccessoriesOrderModel.GridRemarks = dt1.Rows[i]["REMARKS"].ToString();
                objAccessoriesOrderModel.GridTranId = dt1.Rows[i]["TRAN_ID"].ToString();


                objAccessoriesOrderModel.GridSupplierId = dt1.Rows[i]["SUPPLIER_ID"].ToString();
                objAccessoriesOrderModel.GridSupplierName = dt1.Rows[i]["SUPPLIER_NAME"].ToString();
                objAccessoriesOrderModel.GridStoreId = dt1.Rows[i]["STORE_ID"].ToString();
                objAccessoriesOrderModel.GridStoreName = dt1.Rows[i]["STORE_NAME"].ToString();
                objAccessoriesOrderModel.GridSyleNo = dt1.Rows[i]["STYLE_NO"].ToString();
                objAccessoriesOrderModel.GridUnitPrice = dt1.Rows[i]["UNIT_PRICE"].ToString();
                objAccessoriesOrderModel.Pending_YN = dt1.Rows[i]["PENDING_YN"].ToString();

                objAccessoriesOrderModel.SupplierDate = dt1.Rows[i]["SUPPLIER_DATE"].ToString();
                objAccessoriesOrderModel.RevisedDate = dt1.Rows[i]["REVISED_DATE"].ToString();
                objAccessoriesOrderModel.InHouseDate = dt1.Rows[i]["IN_HOUSE_DATE"].ToString();

               

                TrimsDataBundle.Add(objAccessoriesOrderModel);
            }

            return TrimsDataBundle;
        }


        [HttpGet]
        public ActionResult SearchTrimsEntryPending(TrimsAccessoriesOrderModel objAccessoriesOrderModel)
        {
            LoadSession();

            objAccessoriesOrderModel.UpdateBy = strEmployeeId;
            objAccessoriesOrderModel.HeadOfficeId = strHeadOfficeId;
            objAccessoriesOrderModel.BranchOfficeId = strBranchOfficeId;

            DataTable dt = objAccessoriesDAL.GetTrimsRecord(objAccessoriesOrderModel);
            ViewBag.TrimsList = TrimListData(dt);
            LoadDropDownList();

            ModelState.Clear();
            return View("SearchTrimsEntryPending");
        }


        [HttpGet]
        public ActionResult TrimsEntryAuthorizedList(TrimsAccessoriesOrderModel objAccessoriesOrderModel)
        {
            LoadSession();

            objAccessoriesOrderModel.UpdateBy = strEmployeeId;
            objAccessoriesOrderModel.HeadOfficeId = strHeadOfficeId;
            objAccessoriesOrderModel.BranchOfficeId = strBranchOfficeId;

            DataTable dt = objAccessoriesDAL.GetTrimsRecord(objAccessoriesOrderModel);
            ViewBag.TrimsList = TrimListData(dt);
            LoadDropDownList();

            ModelState.Clear();
            return View("TrimsEntryAuthorizedList");
        }

        [HttpGet]
        public ActionResult AuthorizeTrimsAccPending(TrimsAccessoriesOrderModel objAccessoriesOrderModel)
        {
            LoadSession();

            objAccessoriesOrderModel.UpdateBy = strEmployeeId;
            objAccessoriesOrderModel.HeadOfficeId = strHeadOfficeId;
            objAccessoriesOrderModel.BranchOfficeId = strBranchOfficeId;

            DataTable dt = objAccessoriesDAL.GetTrimsRecord(objAccessoriesOrderModel);
            ViewBag.TrimsList = TrimListData(dt);
            LoadDropDownList();

            ModelState.Clear();
            return View("AuthorizeTrimsAccPending");
        }

        public JsonResult AuthorizeTrimsAcc(string trimsPending)
        {
            LoadSession();

            string strDBMsg = "";


            var pending = new JavaScriptSerializer().Deserialize<List<TrimsAccessoriesOrderModel>>(trimsPending);
            foreach (var list in pending)
            {
                TrimsAccessoriesOrderModel objAccessoriesOrderModel = new TrimsAccessoriesOrderModel();

                objAccessoriesOrderModel.UpdateBy = strEmployeeId;
                objAccessoriesOrderModel.HeadOfficeId = strHeadOfficeId;
                objAccessoriesOrderModel.BranchOfficeId = strBranchOfficeId;

                objAccessoriesOrderModel.GridTranId = list.GridTranId;
                objAccessoriesOrderModel.SupplierDate = list.SupplierDate;
                objAccessoriesOrderModel.RevisedDate = list.RevisedDate;
                objAccessoriesOrderModel.InHouseDate = list.InHouseDate;                         

                strDBMsg = objAccessoriesDAL.AuthorizeTrimsAcc(objAccessoriesOrderModel);
            }

            TempData["OperationMessage"] = strDBMsg;

            return Json(strDBMsg, JsonRequestBehavior.AllowGet);
           
        }

    }
}