using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

        //ReportDocument rd = new ReportDocument();
        //ExportFormatType formatType = ExportFormatType.NoFormat;

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
              
                model.OnlineOrderMain = _objTrimsDal.GetTrimsMainData(pOrderReceiveDate, pOrderNo, model.HeadOfficeId, model.BranchOfficeId);

              
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
                model.DiscountAmount = model.OnlineOrderMain.DiscountAmount;
                model.TotalAmount = model.OnlineOrderMain.TotalAmount;
                model.Delivered_YN = model.OnlineOrderMain.Delivered_YN;
                model.DeliveryCost = model.OnlineOrderMain.DeliveryCost;
                model.Remarks = model.OnlineOrderMain.Remarks;
                model.EmailAddress = model.OnlineOrderMain.EmailAddress;
                model.DeliveryProcessCost = model.OnlineOrderMain.DeliveryProcessCost;
                model.PaymentTypeId = model.OnlineOrderMain.PaymentTypeId;
              
                //model.TrimsSubs = _objTrimsDal.GetTrimsSubData(model);
                //model.OnlineOrderSubs = _objTrimsDal.LoadOnlineOrderRecordByOrderNo(model);

                
                //model.StyleNo = styleNumber;

                ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
                ViewBag.AccessoriesDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetAccessoriesDDList(), "ACCESSORIES_ID", "ACCESSORIES_NAME");
            }
         
            //    model.OnlineOrderSubMain = _objTrimsDal.GetTrimsGridDataList(strHeadOfficeId, strBranchOfficeId);
            model.OnlineOrderSubMain = _objTrimsDal.GetTrimsGridDataList(model.OrderReceiveDate, model.OrderDeliveryDate, model.CustomerHomeAddress, model.CustomerName, model.CellNo, model.OrderSourceId, model.WebAddress, model.Delivered_YN, model.OrderNo, strHeadOfficeId, strBranchOfficeId);


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

            if (string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.OrderReceiveDate) && string.IsNullOrWhiteSpace(objOnlineOrderStatusModel.OrderNo))
            {
                TempData["InformationMessage"] = "Please enter style number";
                return RedirectToAction("Index");
            }

            var model = _objTrimsDal.GetTrimsData(objOnlineOrderStatusModel.OrderNo, objOnlineOrderStatusModel.OrderReceiveDate, objOnlineOrderStatusModel.HeadOfficeId, objOnlineOrderStatusModel.BranchOfficeId);

            //var model = _objTrimsDal.GetTrimsData(objOnlineOrderStatusModel.StyleNo, objOnlineOrderStatusModel.SeasonYear,
            //    objOnlineOrderStatusModel.SeasonName, objOnlineOrderStatusModel.HeadOfficeId, objOnlineOrderStatusModel.BranchOfficeId);

            ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
            ViewBag.AccessoriesDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetAccessoriesDDList(), "ACCESSORIES_ID", "ACCESSORIES_NAME");

            //model.PoEntryGrids = _objPoEntryDal.GetPoEntryMainData();

            model.OnlineOrderMain = new OnlineOrderMain();
            model.OnlineOrderSubs = new List<OnlineOrderSub>();
            //model.OnlineOrderMains = _objTrimsDal.GetTrimsGridDataList(strHeadOfficeId, strBranchOfficeId);
            model.OnlineOrderSubs = _objTrimsDal.GetTrimsGridDataList(objOnlineOrderStatusModel.OrderReceiveDate,objOnlineOrderStatusModel.OrderDeliveryDate,objOnlineOrderStatusModel.CustomerHomeAddress,objOnlineOrderStatusModel.CustomerName,objOnlineOrderStatusModel.CellNo,objOnlineOrderStatusModel.OrderSourceId,objOnlineOrderStatusModel.WebAddress,objOnlineOrderStatusModel.Delivered_YN,objOnlineOrderStatusModel.OrderNo,strHeadOfficeId, strBranchOfficeId);

            model.OnlineOrderSubMain = _objTrimsDal.GetTrimsGridDataList(objOnlineOrderStatusModel.OrderReceiveDate, objOnlineOrderStatusModel.OrderDeliveryDate, objOnlineOrderStatusModel.CustomerHomeAddress, objOnlineOrderStatusModel.CustomerName, objOnlineOrderStatusModel.CellNo, objOnlineOrderStatusModel.OrderSourceId, objOnlineOrderStatusModel.WebAddress, objOnlineOrderStatusModel.Delivered_YN, objOnlineOrderStatusModel.OrderNo, strHeadOfficeId, strBranchOfficeId);

            return View(model);
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTrimsInformation(OnlineOrderStatusModel objTrimsModel)
        {
            for (int i = 0; i < objTrimsModel.TranId.Count(); i++)
            {
                if (objTrimsModel.StyleName[i] == null
                    || objTrimsModel.ProductDescription[i] == null
                   )
                {
                    TempData["Message"] = "All fields are required";
                    return RedirectToAction("Index");
                }
            }

            LoadSession();

            objTrimsModel.UpdateBy = strEmployeeId;
            objTrimsModel.HeadOfficeId = strHeadOfficeId;
            objTrimsModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                for (int i = 0; i < objTrimsModel.TranId.Count(); i++)
                {
                    objTrimsModel.TranIdS = objTrimsModel.TranId[i];
                    objTrimsModel.StyleNameS = objTrimsModel.StyleName[i];
                    objTrimsModel.ProductDescriptionS = objTrimsModel.ProductDescription[i];
                    objTrimsModel.SizeNameS = objTrimsModel.SizeName[i];
                    objTrimsModel.ColorNameS = objTrimsModel.ColorName[i];
                    objTrimsModel.ProductQuantityS = objTrimsModel.ProductQuantity[i];
                    objTrimsModel.ProductPriceS = objTrimsModel.ProductPrice[i];

                    string strMessage = _objTrimsDal.TrimsInformationSave(objTrimsModel);
                    TempData["Message"] = strMessage;
                }
            }

            return RedirectToAction("Index");
        }

        //public ActionResult DeleteTrimsSub(string[] TranId, string[] StyleNo, string[] SeasonYear, string[] SeasonId)
        //{
        //    string strMessage = null;

        //    LoadSession();
        //    for (int i = 0; i < TranId.Length; i++)
        //    {
        //        var trimsSubDelete = new TrimsSub
        //        {
        //            TranId = TranId[i],
        //            StyleNo = StyleNo[i],
        //            SeasoneYear = SeasonYear[i],
        //            SeasoneId = SeasonId[i],
        //            HeadOfficeId = strHeadOfficeId,
        //            BranchOfficeId = strBranchOfficeId
        //        };

        //        strMessage = _objTrimsDal.TrimsSubDelete(trimsSubDelete);
        //    }

        //    return Json(strMessage, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult TrimsDetailsReport(string seasonId, string seasonYear, string styleNumber)
        //{
        //    LoadSession();

        //    TrimsReport objTrimsReport = new TrimsReport();

        //    objTrimsReport.HeadOfficeId = strHeadOfficeId;
        //    objTrimsReport.BranchOfficeId = strBranchOfficeId;

        //    if (seasonId != null && seasonYear != null && styleNumber != null)
        //    {
        //        objTrimsReport.SeasoneId = seasonId;
        //        objTrimsReport.SeasoneYear = seasonYear;
        //        objTrimsReport.StyleNo = styleNumber;
        //    }

        //    string strPath = Path.Combine(Server.MapPath("~/Reports/rptTrimsDetail.rpt"));
        //    rd.Load(strPath);

        //    DataSet ds = new DataSet();

        //    ds = (_objReportDal.TrimsDetailSheet(objTrimsReport));
        //    rd.SetDataSource(ds);

        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();

        //    Response.Clear();
        //    Response.Buffer = true;

        //    formatType = ExportFormatType.PortableDocFormat;
        //    System.IO.Stream oStream = null;
        //    byte[] byteArray = null;

        //    Response.Buffer = false;
        //    Response.ClearContent();
        //    Response.ClearHeaders();

        //    oStream = rd.ExportToStream(formatType);
        //    byteArray = new byte[oStream.Length];
        //    oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
        //    Response.ClearContent();
        //    Response.ClearHeaders();
        //    Response.ContentType = "application/pdf";
        //    Response.BinaryWrite(byteArray);
        //    Response.Flush();
        //    Response.Close();
        //    rd.Close();
        //    rd.Dispose();

        //    return File(oStream, "application/pdf", "InventoryReport.pdf");
        //}
    }
}