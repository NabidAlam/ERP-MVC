using ERP.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.MODEL;
using ERP.Utility;
using PagedList;

namespace ERP.Controllers
{
    public class TrimsAccessoriesController : Controller
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
        #endregion

        #region "Trims Entry"

        [HttpGet]
        public ActionResult TrimsAccessories(TrimsAccessoriesOrderModel objAccessoriesOrderModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                //ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();


                objAccessoriesOrderModel.UpdateBy = strEmployeeId;
                objAccessoriesOrderModel.HeadOfficeId = strHeadOfficeId;
                objAccessoriesOrderModel.BranchOfficeId = strBranchOfficeId;

                //L_ITEM_CP  ITEM_ID           NUMBER,
                //ITEM_NAME VARCHAR2(100 BYTE),
                ViewBag.ItemDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetItemCPDDList(), "ITEM_ID", "ITEM_NAME");
                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitTP_DDList(), "UNIT_ID", "UNIT_NAME");
                ViewBag.GetLocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLocationDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");
                ViewBag.GetOnlySupplierDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetOnlySupplierDDList(), "SUPPLIER_ID", "SUPPLIER_NAME");

                DataTable dt1 = objAccessoriesDAL.GetTrimsRecord(objAccessoriesOrderModel);
                ViewBag.TrimsList = TrimListData(dt1);


                //ViewBag.Year = objLookUpDAL.getCurrentYear();




                return View(objAccessoriesOrderModel);
            }

        }




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



                TrimsDataBundle.Add(objAccessoriesOrderModel);
            }

            return TrimsDataBundle;
        }
        public List<TrimsAccessoriesOrderModel> TrimListDataForEdit(DataTable dt2)
        {
            List<TrimsAccessoriesOrderModel> TrimsDataBundle = new List<TrimsAccessoriesOrderModel>();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                TrimsAccessoriesOrderModel objAccessoriesOrderModel = new TrimsAccessoriesOrderModel();

                // objAccessoriesOrderModel.SerialNumber = dt2.Rows[i]["SL"].ToString();

                objAccessoriesOrderModel.GridOrderDate = dt2.Rows[i]["ORDER_DATE"].ToString();
                objAccessoriesOrderModel.GridItemId = dt2.Rows[i]["ITEM_ID"].ToString();
                objAccessoriesOrderModel.GridItemName = dt2.Rows[i]["ITEM_NAME"].ToString();
                objAccessoriesOrderModel.GridItemCode = dt2.Rows[i]["ITEM_CODE"].ToString();
                objAccessoriesOrderModel.GridOrderQty = dt2.Rows[i]["ORDER_QUANTITY"].ToString();
                objAccessoriesOrderModel.GridUnitId = dt2.Rows[i]["UNIT_ID"].ToString();
                objAccessoriesOrderModel.GridUnitName = dt2.Rows[i]["UNIT_NAME"].ToString();

                objAccessoriesOrderModel.GridDeliveryDate = dt2.Rows[i]["DELIVERY_DATE"].ToString();
                objAccessoriesOrderModel.GridRemarks = dt2.Rows[i]["REMARKS"].ToString();

                objAccessoriesOrderModel.GridTranId = dt2.Rows[i]["TRAN_ID"].ToString();

                objAccessoriesOrderModel.GridSupplierId = dt2.Rows[i]["SUPPLIER_ID"].ToString();
                objAccessoriesOrderModel.GridSupplierName = dt2.Rows[i]["SUPPLIER_NAME"].ToString();
                objAccessoriesOrderModel.GridStoreId = dt2.Rows[i]["STORE_ID"].ToString();
                objAccessoriesOrderModel.GridStoreName = dt2.Rows[i]["STORE_NAME"].ToString();
                objAccessoriesOrderModel.GridSyleNo = dt2.Rows[i]["STYLE_NO"].ToString();
                objAccessoriesOrderModel.GridUnitPrice = dt2.Rows[i]["UNIT_PRICE"].ToString();


                TrimsDataBundle.Add(objAccessoriesOrderModel);
            }

            return TrimsDataBundle;
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTrimsEntry(TrimsAccessoriesOrderModel objAccessoriesOrderModel)
        {

            LoadSession();

            objAccessoriesOrderModel.UpdateBy = strEmployeeId;
            objAccessoriesOrderModel.HeadOfficeId = strHeadOfficeId;
            objAccessoriesOrderModel.BranchOfficeId = strBranchOfficeId;

            //if (ModelState.IsValid)
            //{
                string strDBMsg = "";
                strDBMsg = objAccessoriesDAL.SaveTrimsEntry(objAccessoriesOrderModel);
                TempData["OperationMessage"] = strDBMsg;
            //}


            ViewBag.ItemDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetItemCPDDList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitTP_DDList(), "UNIT_ID", "UNIT_NAME");
            ViewBag.GetLocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLocationDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");
            ViewBag.GetOnlySupplierDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetOnlySupplierDDList(), "SUPPLIER_ID", "SUPPLIER_NAME");


            DataTable dt1 = objAccessoriesDAL.GetTrimsRecord(objAccessoriesOrderModel);
            ViewBag.TrimsList = TrimListData(dt1);


            ModelState.Clear();
            return View("TrimsAccessories");
        }
        [HttpGet]
        public ActionResult SearchTrimsEntry(TrimsAccessoriesOrderModel objAccessoriesOrderModel)
        {
            LoadSession();

            objAccessoriesOrderModel.UpdateBy = strEmployeeId;
            objAccessoriesOrderModel.HeadOfficeId = strHeadOfficeId;
            objAccessoriesOrderModel.BranchOfficeId = strBranchOfficeId;

            DataTable dt = objAccessoriesDAL.GetTrimsRecord(objAccessoriesOrderModel);
            ViewBag.TrimsList = TrimListData(dt);
            ViewBag.ItemDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetItemCPDDList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitTP_DDList(), "UNIT_ID", "UNIT_NAME");
            ViewBag.GetLocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLocationDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");
            ViewBag.GetOnlySupplierDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetOnlySupplierDDList(), "SUPPLIER_ID", "SUPPLIER_NAME");


            ModelState.Clear();
            return View("TrimsAccessories");
        }

        [HttpGet]
        public ActionResult EditTrimsEntry(string tranId, string pOrderDate, string pItem, string pItemCode)
       {  //string pOrderDate, string pItem, string pStyleName, string pItemCode, string pOrderQty, string pUnit, string pDeliveryDate, string pRemarks

            try
            {
                if (Session["strEmployeeId"] == null)
                {
                    return RedirectToAction("LogOut", "Login");
                }
                else
                {

                    LoadSession();
                    objAccessoriesOrderModel.GridOrderDate = pOrderDate;
                    objAccessoriesOrderModel.GridItemId = pItem;
                    objAccessoriesOrderModel.GridItemCode = pItemCode;
                    objAccessoriesOrderModel.GridTranId = tranId;


                    objAccessoriesOrderModel.UpdateBy = strEmployeeId;
                    objAccessoriesOrderModel.CreateBy = strEmployeeId;

                    objAccessoriesOrderModel.HeadOfficeId = strHeadOfficeId;
                    objAccessoriesOrderModel.BranchOfficeId = strBranchOfficeId;

                    ViewBag.ItemDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetItemCPDDList(), "ITEM_ID", "ITEM_NAME");
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitTP_DDList(), "UNIT_ID", "UNIT_NAME");

                    ViewBag.GetLocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLocationDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");
                    ViewBag.GetOnlySupplierDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetOnlySupplierDDList(), "SUPPLIER_ID", "SUPPLIER_NAME");



                    DataTable dt1 = objAccessoriesDAL.GetTrimsRecord(objAccessoriesOrderModel);
                    ViewBag.TrimsList = TrimListData(dt1);


                    DataTable dt2 = objAccessoriesDAL.GetTrimsRecordForEdit(objAccessoriesOrderModel);
                    ViewBag.TrimsListForEdit = TrimListDataForEdit(dt2);


                    return View("TrimsAccessories", objAccessoriesOrderModel);

                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult ClearTrimsEntry()
        {
            ModelState.Clear();
            TempData["SearchValueFromSearchAction"] = "";
            return RedirectToAction("TrimsAccessories");
        }

       public ActionResult DeleteTrimsAccEntry(string pId)
        {
            TrimsAccessoriesOrderModel objAccessoriesOrderModel = new TrimsAccessoriesOrderModel { GridTranId = pId };

            LoadSession();

            //objFabricModel.UpdateBy = strEmployeeId;
            objAccessoriesOrderModel.HeadOfficeId = strHeadOfficeId;
            objAccessoriesOrderModel.BranchOfficeId = strBranchOfficeId;

            string vDbMessage = objAccessoriesDAL.DeleteTrimsAccEntry(objAccessoriesOrderModel);
            //string vDbMessage = $"{ pId } { strEmployeeId } { strHeadOfficeId }  { strBranchOfficeId }";

            TempData["OperationMessage"] = vDbMessage;

            return RedirectToAction("TrimsAccessories", "TrimsAccessories");
        }





        //pending list
        [HttpGet]
        public ActionResult SearchTrimsEntryPending(TrimsAccessoriesOrderModel objAccessoriesOrderModel)
        {
            LoadSession();

            objAccessoriesOrderModel.UpdateBy = strEmployeeId;
            objAccessoriesOrderModel.HeadOfficeId = strHeadOfficeId;
            objAccessoriesOrderModel.BranchOfficeId = strBranchOfficeId;

            DataTable dt = objAccessoriesDAL.GetTrimsRecord(objAccessoriesOrderModel);
            ViewBag.TrimsList = TrimListData(dt);
            ViewBag.ItemDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetItemCPDDList(), "ITEM_ID", "ITEM_NAME");
            ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitTP_DDList(), "UNIT_ID", "UNIT_NAME");
            ViewBag.GetLocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLocationDDList(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");
            ViewBag.GetOnlySupplierDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetOnlySupplierDDList(), "SUPPLIER_ID", "SUPPLIER_NAME");


            ModelState.Clear();
            return View("TrimsAccessoriesPending");
        }
        #endregion
    }
}