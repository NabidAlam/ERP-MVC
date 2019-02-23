using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.DAL;
using ERP.MODEL;
using ERP.Utility;

namespace ERP.Controllers
{
    public class OnlineOrderStatusController : Controller
    {
        readonly OnlineOrderStatusDAL _objTrimsDal = new OnlineOrderStatusDAL();
        readonly LookUpDAL _objLookUpDal = new LookUpDAL();
        // readonly ReportDAL _objReportDal = new ReportDAL();

        ReportDocument rd = new ReportDocument();
        ExportFormatType formatType = ExportFormatType.NoFormat;

        #region "Common"

        string strEmployeeId = "";
        string strDesignationId = "";
        string strSubSectionId = "";
        string strUnitId = "";
        string strHeadOfficeId = "";
        string strBranchOfficeId = "";
        string strSoftId = "";

        public void LoadSession()
        {
            strEmployeeId = Session["strEmployeeId"].ToString();
            strSubSectionId = Session["strSubSectionId"].ToString();
            strDesignationId = Session["strDesignationId"].ToString();
            strUnitId = Session["strUnitId"].ToString();
            strHeadOfficeId = Session["strHeadOfficeId"].ToString();
            strBranchOfficeId = Session["strBranchOfficeId"].ToString();
            strSoftId = Session["strSoftId"].ToString();
        }
        #endregion

        public ActionResult Index(string pOrderReceiveDate, string pOrderNo)
        {
            OnlineOrderStatusModel model = new OnlineOrderStatusModel();
            ViewBag.InformationMessage = TempData["InformationMessage"] as string;

            LoadSession();
            //LoadSession();
            model.HeadOfficeId = strHeadOfficeId;
            model.BranchOfficeId = strBranchOfficeId;


            model.OnlineOrderMain = new OnlineOrderMain();
            model.OnlineOrderMains = new List<OnlineOrderMain>();
            model.OnlineOrderSubs = new List<OnlineOrderSub>();
            //model.PoEntrySubs = new List<PoEntrySub>();

            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"] as string;
            }
            //WHERE order_receive_date =
            // TO_DATE(p_order_receive_date, 'dd/mm/yyyy')
            // AND order_no = p_order_no
            // AND head_office_id = p_head_office_id
            // AND branch_office_id = p_branch_office_id;

            if (pOrderReceiveDate != null && pOrderNo != null)
            {
                model.OrderReceiveDate = pOrderReceiveDate;
                model.OrderNo = pOrderNo;

                model.OnlineOrderMain = _objTrimsDal.GetTrimsMainData(pOrderReceiveDate, pOrderNo, model.HeadOfficeId,
                    model.BranchOfficeId);


                model.OnlineOrderSubs = _objTrimsDal.LoadOnlineOrderRecordByOrderNo(model);

                model.OrderNo = model.OnlineOrderMain.OrderNo;
                model.OrderReceiveDate = model.OnlineOrderMain.OrderReceiveDate;
                model.OrderDeliveryDate = model.OnlineOrderMain.OrderDeliveryDate;
                model.OrderSourceId = model.OnlineOrderMain.OrderSourceId;
                model.CustomerName = model.OnlineOrderMain.CustomerName;
                model.CustomerHomeAddress = model.OnlineOrderMain.CustomerHomeAddress;
                model.CustomerOfficeAddress = model.OnlineOrderMain.CustomerOfficeAddress;
                model.Telephone = model.OnlineOrderMain.Telephone;
                model.CellNo = model.OnlineOrderMain.CellNo;
                model.WebAddress = model.OnlineOrderMain.WebAddress;
                // model.DiscountAmount = model.OnlineOrderMain.DiscountAmount;
                // model.TotalAmount = model.OnlineOrderMain.TotalAmount;
                model.Delivered_YN = model.OnlineOrderMain.Delivered_YN;
                model.DeliveryCost = model.OnlineOrderMain.DeliveryCost;
                model.Remarks = model.OnlineOrderMain.Remarks;
                model.EmailAddress = model.OnlineOrderMain.EmailAddress;
                model.DeliveryProcessCost = model.OnlineOrderMain.DeliveryProcessCost;
                model.PaymentTypeId = model.OnlineOrderMain.PaymentTypeId;

                model.OrderStatus = model.OnlineOrderMain.OrderStatus;
                model.SwatchFileSize = model.OnlineOrderMain.SwatchFileSize;

                //model.TrimsSubs = _objTrimsDal.GetTrimsSubData(model);
                //model.OnlineOrderSubs = _objTrimsDal.LoadOnlineOrderRecordByOrderNo(model);


                //model.StyleNo = styleNumber;

                ViewBag.GetPaymentTypeDDList =
                    UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetPaymentTypeDDList(), "PAYMENT_TYPE_ID",
                        "PAYMENT_TYPE_NAME");
                ViewBag.GetOrderSourceDDList =
                    UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetOrderSourceDDList(), "ORDER_SOURCE_ID",
                        "ORDER_SOURCE_NAME");

             
               
                model.OnlineOrderSubMain = _objTrimsDal.GetTrimsGridDataList(model.OrderReceiveDate,
                       model.OrderDeliveryDate,
                       model.CustomerHomeAddress,
                       model.CustomerName, model.CellNo,
                       model.OrderSourceId, model.WebAddress,
                       model.Delivered_YN, model.OrderNo,
                       strHeadOfficeId, strBranchOfficeId);

            }

            //    model.OnlineOrderSubMain = _objTrimsDal.GetTrimsGridDataList(strHeadOfficeId, strBranchOfficeId);


            else
            {
                model.OnlineOrderSubMain = null;
                model.OnlineOrderSubs = null;
            }

            //model.OnlineOrderSubMain = _objTrimsDal.GetTrimsGridDataList(model.OrderReceiveDate, model.OrderDeliveryDate, model.CustomerHomeAddress, model.CustomerName, model.CellNo, model.OrderSourceId, model.WebAddress, model.Delivered_YN, model.OrderNo, strHeadOfficeId, strBranchOfficeId);
          
            //model.OnlineOrderSubMain = null;
            ViewBag.GetPaymentTypeDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetPaymentTypeDDList(), "PAYMENT_TYPE_ID", "PAYMENT_TYPE_NAME");
            ViewBag.GetOrderSourceDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetOrderSourceDDList(), "ORDER_SOURCE_ID", "ORDER_SOURCE_NAME");


            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(OnlineOrderStatusModel objOnlineOrderStatusModel)
        {
            LoadSession();
            objOnlineOrderStatusModel.HeadOfficeId = strHeadOfficeId;
            objOnlineOrderStatusModel.BranchOfficeId = strBranchOfficeId;
            //string strReceiveDate, string strDeliverDate, string strAddress, string StrCustomerName, string strContactNo, string strSourceId, string strWebAddress, string strCheckedYN, string strOrderNo, string strHeadOfficeId, string strBranchOfficeId

            //if (string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.OrderReceiveDate) && string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.OrderNo))
            //{
            //    TempData["InformationMessage"] = "Please enter style number";
            //    return RedirectToAction("Index");
            //}

            var model = _objTrimsDal.GetTrimsData(objOnlineOrderStatusModel.OrderReceiveDate, objOnlineOrderStatusModel.OrderNo, objOnlineOrderStatusModel.HeadOfficeId, objOnlineOrderStatusModel.BranchOfficeId);

            //var model = _objTrimsDal.GetTrimsData(objOnlineOrderStatusModel.StyleNo, objOnlineOrderStatusModel.SeasonYear,
            //    objOnlineOrderStatusModel.SeasonName, objOnlineOrderStatusModel.HeadOfficeId, objOnlineOrderStatusModel.BranchOfficeId);


            ViewBag.GetPaymentTypeDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetPaymentTypeDDList(), "PAYMENT_TYPE_ID", "PAYMENT_TYPE_NAME");
            ViewBag.GetOrderSourceDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetOrderSourceDDList(), "ORDER_SOURCE_ID", "ORDER_SOURCE_NAME");

            //model.PoEntryGrids = _objPoEntryDal.GetPoEntryMainData();

            model.OnlineOrderMain = new OnlineOrderMain();
            model.OnlineOrderSubs = new List<OnlineOrderSub>();
            //model.OnlineOrderMains = _objTrimsDal.GetTrimsGridDataList(strHeadOfficeId, strBranchOfficeId);


            //model.OnlineOrderSubs = null;

            model.OnlineOrderSubMain =
                   _objTrimsDal.GetTrimsGridDataList(objOnlineOrderStatusModel.OrderReceiveDateSearch,
                       objOnlineOrderStatusModel.OrderDeliveryDateSearch,
                       objOnlineOrderStatusModel.CustomerHomeAddressSearch,
                       objOnlineOrderStatusModel.CustomerNameSearch, objOnlineOrderStatusModel.CellNoSearch,
                       objOnlineOrderStatusModel.OrderSourceIdSearch, objOnlineOrderStatusModel.WebAddressSearch,
                       objOnlineOrderStatusModel.Delivered_YNSearch, objOnlineOrderStatusModel.OrderNoSearch,
                       strHeadOfficeId, strBranchOfficeId);

            if (model.OnlineOrderSubMain != null)
            {
               
                model.OnlineOrderSubs = null;

            }
            else
            {
               
                model.OnlineOrderSubs =
                  _objTrimsDal.GetTrimsGridDataList(objOnlineOrderStatusModel.OrderReceiveDateSearch,
                      objOnlineOrderStatusModel.OrderDeliveryDateSearch,
                      objOnlineOrderStatusModel.CustomerHomeAddressSearch,
                      objOnlineOrderStatusModel.CustomerNameSearch, objOnlineOrderStatusModel.CellNoSearch,
                      objOnlineOrderStatusModel.OrderSourceIdSearch, objOnlineOrderStatusModel.WebAddressSearch,
                      objOnlineOrderStatusModel.Delivered_YNSearch, objOnlineOrderStatusModel.OrderNoSearch,
                      strHeadOfficeId, strBranchOfficeId);

            }


            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveOnlineOrder(OnlineOrderStatusModel objOnlineOrderStatusModel)
        {

            LoadSession();

            for (int i = 0; i < objOnlineOrderStatusModel.TranId.Count(); i++)
            {
                if (objOnlineOrderStatusModel.StyleName[i] == null
                    || objOnlineOrderStatusModel.ProductDescription[i] == null
                   )
                {
                    TempData["Message"] = "All fields are required";
                    return RedirectToAction("Index");
                }
            }

            //LoadSession();

            objOnlineOrderStatusModel.UpdateBy = strEmployeeId;
            objOnlineOrderStatusModel.HeadOfficeId = strHeadOfficeId;
            objOnlineOrderStatusModel.BranchOfficeId = strBranchOfficeId;
            objOnlineOrderStatusModel.SwatchFileSize = UtilityClass.ConvertImageToByteArray(objOnlineOrderStatusModel.HttpPostedFileBase);
            objOnlineOrderStatusModel.SwatchFileName = objOnlineOrderStatusModel.HttpPostedFileBase?.FileName;
            objOnlineOrderStatusModel.SwatchFileExtension = objOnlineOrderStatusModel.HttpPostedFileBase?.ContentType;

            _objTrimsDal.SaveOnlineOrderImage(objOnlineOrderStatusModel);

            if (ModelState.IsValid)
            {
                for (int i = 0; i < objOnlineOrderStatusModel.TranId.Count(); i++)
                {  //Promotion Code	Promotion(%)	Discount Amount	Total Amount
                    objOnlineOrderStatusModel.TranIdS = objOnlineOrderStatusModel.TranId[i];
                    objOnlineOrderStatusModel.StyleNameS = objOnlineOrderStatusModel.StyleName[i];
                    objOnlineOrderStatusModel.ProductDescriptionS = objOnlineOrderStatusModel.ProductDescription[i];
                    objOnlineOrderStatusModel.SizeNameS = objOnlineOrderStatusModel.SizeName[i];
                    objOnlineOrderStatusModel.ColorNameS = objOnlineOrderStatusModel.ColorName[i];
                    objOnlineOrderStatusModel.ProductQuantityS = objOnlineOrderStatusModel.ProductQuantity[i];
                    objOnlineOrderStatusModel.ProductPriceS = objOnlineOrderStatusModel.ProductPrice[i];
                    objOnlineOrderStatusModel.PromotionCodeS = objOnlineOrderStatusModel.PromotionCode[i];
                    objOnlineOrderStatusModel.PromotionPercentageS = objOnlineOrderStatusModel.PromotionPercentage[i];
                    objOnlineOrderStatusModel.DiscountAmountS = objOnlineOrderStatusModel.DiscountAmount[i];
                    objOnlineOrderStatusModel.TotalAmountS = objOnlineOrderStatusModel.TotalAmount[i];

                    objOnlineOrderStatusModel.PromotionPercentageS = objOnlineOrderStatusModel.PromotionPercentage[i];

                    string strMessage = _objTrimsDal.SaveOnlineOrder(objOnlineOrderStatusModel);


                    TempData["Message"] = strMessage;
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteTrimsSub(string[] TranId, string[] ReceiveDate, string[] OrderNo)
        {
            string strMessage = null;

            LoadSession();
            for (int i = 0; i < TranId.Length; i++)
            {
                var trimsSubDelete = new OnlineOrderSub
                {
                    TranId = TranId[i],
                    OrderReceiveDate = ReceiveDate[i],
                    OrderNo = OrderNo[i],
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };


                strMessage = _objTrimsDal.TrimsSubDelete(trimsSubDelete);
            }

            return Json(strMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult OnlineOrderDetailsReport(string pOrderNo)
        {
            LoadSession();

            OnlineOrderReport objOnlineOrderReport = new OnlineOrderReport();

            objOnlineOrderReport.HeadOfficeId = strHeadOfficeId;
            objOnlineOrderReport.BranchOfficeId = strBranchOfficeId;

            if (pOrderNo != null)
            {
                objOnlineOrderReport.OrderNo = pOrderNo;
              
            }

            string strPath = Path.Combine(Server.MapPath("~/Reports/rptOnlineOrderStatement.rpt"));
            rd.Load(strPath);

            DataSet ds = new DataSet();

            ds = (_objTrimsDal.OnlineOrderDetailSheet(objOnlineOrderReport));
            rd.SetDataSource(ds);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Response.Clear();
            Response.Buffer = true;

            formatType = ExportFormatType.PortableDocFormat;
            System.IO.Stream oStream = null;
            byte[] byteArray = null;

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            oStream = rd.ExportToStream(formatType);
            byteArray = new byte[oStream.Length];
            oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(byteArray);
            Response.Flush();
            Response.Close();
            rd.Close();
            rd.Dispose();

            return File(oStream, "application/pdf", "InventoryReport.pdf");
        }
    }
}