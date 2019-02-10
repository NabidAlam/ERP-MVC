using ERP.MODEL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.DAL;
using ERP.Utility;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;

namespace ERP.Controllers
{
    public class PoEntryController : Controller
    {
        readonly PoEntryDAL _objPoEntryDal = new PoEntryDAL();
        readonly LookUpDAL _objLookUpDal = new LookUpDAL();

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

        public ActionResult Index(string seasonId, string seasonYear, string styleNumber)
        {
            PoEntryModel model = new PoEntryModel();

            LoadSession();
            model.HeadOfficeId = strHeadOfficeId;
            model.BranchOfficeId = strBranchOfficeId;

            ViewBag.InformationMessage = TempData["InformationMessage"] as string;

            model.PoEntryGrids = new List<PoEntryGrid>();
            model.PoEntryMain = new PoEntryMain();
            model.PoEntrySubs = new List<PoEntrySub>();
            model.PoCommentses = new List<PoComments>();

            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"] as string;
            }

            // For Edit Start
            if (seasonId != null && seasonYear != null && styleNumber != null)
            {
                model.SeasoneName = seasonId;
                model.SeasoneYear = seasonYear;
                model.StyleNo = styleNumber;

                //Initial PO Main Data
                model.PoEntryMain = _objPoEntryDal.GetPoMainData(seasonId, seasonYear, styleNumber, model.HeadOfficeId, model.BranchOfficeId);

                model.PoNumber = model.PoEntryMain.PoNumber;
                model.StyleName = model.PoEntryMain.StyleName;
                model.Fit = model.PoEntryMain.FitId;
                model.StoreDeleveryDate = model.PoEntryMain.StoreDeleveryDate;
                model.CostPrice = model.PoEntryMain.CostPrice;
                model.RetailPrice = model.PoEntryMain.RetailPrice;
                model.FabricQuantity = model.PoEntryMain.FabricQuantity;
                model.InsertDate = model.PoEntryMain.InsertDate;
                model.Occasion = model.PoEntryMain.Occasion;

                model.Embroidary = model.PoEntryMain.Embroidary;
                model.Karchupi = model.PoEntryMain.Karchupi;
                model.Print = model.PoEntryMain.Print;
                model.Wash = model.PoEntryMain.Wash;

                //Initial PO Sub Data
                model.PoEntrySubs = _objPoEntryDal.GetPoSubData(seasonId, seasonYear, styleNumber, model.HeadOfficeId, model.BranchOfficeId);
                //Initial PO Comment Data
                model.PoCommentses = _objPoEntryDal.GetPoCommentList(seasonId, seasonYear, styleNumber,
                    model.HeadOfficeId, model.BranchOfficeId);
                model.StyleNo = styleNumber;

                //Load Dropdown
                ViewBag.SizeDDList = UtilityClass.GetSelectListByDataTable(_objPoEntryDal.GetSizeDDList(model.StyleNo, model.SeasoneName, model.SeasoneYear), "SIZE_ID", "SIZE_NAME");
                ViewBag.ColorWayNumberDDList = UtilityClass.GetSelectListByDataTable(_objPoEntryDal.GetColorDDList(model.StyleNo, model.SeasoneName, model.SeasoneYear, model.HeadOfficeId, model.BranchOfficeId), "COLOR_WAY_NO_ID", "COLOR_WAY_NUMBER");
                ViewBag.ColorWayNameDDList = UtilityClass.GetSelectListByDataTable(_objPoEntryDal.GetColorWayDDList(model.StyleNo, model.SeasoneName, model.SeasoneYear, model.HeadOfficeId, model.BranchOfficeId), "COLOR_WAY_NAME", "COLOR_WAY_NAME");
                ViewBag.FitDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetFitDDList(), "FIT_ID", "FIT_NAME");
                ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
                ViewBag.OccasionDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetOccasionDDList(), "OCCASION_ID", "OCCASION_NAME");
            }
            // For Edit End

            //Load Data for edit and Report
            model.PoEntryGrids = _objPoEntryDal.GetPoEntryMainData();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(PoEntryModel objPoEntryModel)
        {
            //Check for validation
            if (objPoEntryModel.DisplayInfo != null)
            {
                string[] data = objPoEntryModel.DisplayInfo.Split('|');

                if (data.Count() < 2)
                {
                    TempData["InformationMessage"] = "Please enter style number";
                    return RedirectToAction("Index");
                }
                if (data[0] != null)
                    objPoEntryModel.StyleNo = data[0].Trim();
                if (data[1] != null)
                    objPoEntryModel.SeasoneYear = data[1].Trim();
                if (data[2] != null)
                    objPoEntryModel.SeasoneName = data[2].Trim();
            }

            if (string.IsNullOrWhiteSpace(objPoEntryModel.StyleNo) 
                && string.IsNullOrWhiteSpace(objPoEntryModel.SeasoneYear) 
                && string.IsNullOrWhiteSpace(objPoEntryModel.SeasoneName))
            {
                TempData["InformationMessage"] = "Please enter style number";
                return RedirectToAction("Index");
            }
            if (!string.IsNullOrWhiteSpace(objPoEntryModel.StyleNo))
            {
                var result = StyleListData(_objPoEntryDal.CheckStyleNumber(objPoEntryModel.StyleNo));

                if (result != null && (result.Count == 0))
                {
                    TempData["InformationMessage"] = "Please enter valid style number";
                    return RedirectToAction("Index");
                }
            }
            //validation End

            LoadSession();
            objPoEntryModel.HeadOfficeId = strHeadOfficeId;
            objPoEntryModel.BranchOfficeId = strBranchOfficeId;

            var model = _objPoEntryDal.GetPoEntryData(objPoEntryModel.StyleNo, objPoEntryModel.SeasoneYear,
                objPoEntryModel.SeasoneName, objPoEntryModel.HeadOfficeId, objPoEntryModel.BranchOfficeId);

            model.HeadOfficeId = strHeadOfficeId;
            model.BranchOfficeId = strBranchOfficeId;

            ViewBag.SizeDDList = UtilityClass.GetSelectListByDataTable(_objPoEntryDal.GetSizeDDList(model.StyleNo, model.SeasoneName, model.SeasoneYear), "SIZE_ID", "SIZE_NAME");
            ViewBag.ColorWayNumberDDList = UtilityClass.GetSelectListByDataTable(_objPoEntryDal.GetColorDDList(model.StyleNo, model.SeasoneName, model.SeasoneYear, model.HeadOfficeId, model.BranchOfficeId), "COLOR_WAY_NO_ID", "COLOR_WAY_NUMBER");
            ViewBag.ColorWayNameDDList = UtilityClass.GetSelectListByDataTable(_objPoEntryDal.GetColorWayDDList(model.StyleNo, model.SeasoneName, model.SeasoneYear, model.HeadOfficeId, model.BranchOfficeId), "COLOR_WAY_NAME", "COLOR_WAY_NAME");
            ViewBag.FitDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetFitDDList(), "FIT_ID", "FIT_NAME");
            ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
            ViewBag.OccasionDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetOccasionDDList(), "OCCASION_ID", "OCCASION_NAME");

            model.PoEntryGrids = _objPoEntryDal.GetPoEntryMainData();

            model.PoEntryMain = new PoEntryMain();
            model.PoEntrySubs = new List<PoEntrySub>();
            model.PoCommentses = new List<PoComments>();



            return View(model);
        }
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePoData(PoEntryModel objPoEntryModel)
        {
            for (int i = 0; i < objPoEntryModel.TranId.Count(); i++)
            {
                if (objPoEntryModel.ColorWayNumber[i] == null
                    || objPoEntryModel.ColorWayName[i] == null
                    || objPoEntryModel.SizeId[i] == null
                    || objPoEntryModel.SizeValue[i] == null)
                {
                    TempData["Message"] = "All fild is required";
                    return RedirectToAction("Index");
                }
            }
            LoadSession();

            objPoEntryModel.UpdateBy = strEmployeeId;
            objPoEntryModel.HeadOfficeId = strHeadOfficeId;
            objPoEntryModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                objPoEntryModel.EmbroidaryS = objPoEntryModel.Embroidary ? "Y" : "N";
                objPoEntryModel.KarchupiS = objPoEntryModel.Karchupi ? "Y" : "N";
                objPoEntryModel.PrintS = objPoEntryModel.Print ? "Y" : "N";
                objPoEntryModel.WashS = objPoEntryModel.Wash ? "Y" : "N";

                //For PO Sub Save
                for (int i = 0; i < objPoEntryModel.TranId.Count(); i++)
                {
                    objPoEntryModel.TranIdS = objPoEntryModel.TranId[i];
                    objPoEntryModel.ColorWayNumberS = objPoEntryModel.ColorWayNumber[i];
                    objPoEntryModel.ColorWayNameS = objPoEntryModel.ColorWayName[i];
                    objPoEntryModel.SizeIdS = objPoEntryModel.SizeId[i];
                    objPoEntryModel.SizeValueS = objPoEntryModel.SizeValue[i];

                    string strMessage = _objPoEntryDal.SavePoEntry(objPoEntryModel);
                    TempData["Message"] = strMessage;
                }

                // For PO Comment Save
                PoComments objPoConnents = new PoComments();

                objPoConnents.PoNumber = objPoEntryModel.PoNumber;
                objPoConnents.StyleNo = objPoEntryModel.StyleNo;
                objPoConnents.SeasoneYear = objPoEntryModel.SeasoneYear;
                objPoConnents.SeasoneId = objPoEntryModel.SeasoneName;

                objPoConnents.UpdateBy = objPoEntryModel.UpdateBy;
                objPoConnents.HeadOfficeId = objPoEntryModel.HeadOfficeId;
                objPoConnents.BranchOfficeId = objPoEntryModel.BranchOfficeId;

                for (int i = 0; i < objPoEntryModel.CommentTranId.Count(); i++)
                {
                    objPoConnents.TranId = objPoEntryModel.CommentTranId[i];
                    objPoConnents.PoComment = objPoEntryModel.PoComment[i];

                    string strMessage = _objPoEntryDal.SavePoComment(objPoConnents);
                }
            }
            return RedirectToAction("Index");
        }

        public ActionResult GetStyleNo(string query = null)
        {
            List<StyleNumber> result = null;

            result = StyleListData(!string.IsNullOrWhiteSpace(query?.Trim()) ? _objPoEntryDal.GetStyleNumberList(query.Trim()) : _objPoEntryDal.GetStyleNumberList());

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public List<StyleNumber> StyleListData(DataTable dt)
        {
            List<StyleNumber> viewStyleList = new List<StyleNumber>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                StyleNumber objStyleNumber = new StyleNumber();
                objStyleNumber.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                objStyleNumber.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objStyleNumber.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objStyleNumber.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objStyleNumber.HeadOfficeId = dt.Rows[i]["HEAD_OFFICE_ID"].ToString();
                objStyleNumber.BranchOfficeId = dt.Rows[i]["BRANCH_OFFICE_ID"].ToString();
                objStyleNumber.StyleName = dt.Rows[i]["STYLE_NAME"].ToString();

                objStyleNumber.DisplayInfo =
                    $"{dt.Rows[i]["STYLE_NO"]} | {dt.Rows[i]["SEASON_YEAR"]} | {dt.Rows[i]["SEASON_NAME"]}";

                viewStyleList.Add(objStyleNumber);
            }

            return viewStyleList;
        }

        public JsonResult DeletePoSub(string[] TranId, string[] StyleNo, string[] SeasonYear, string[] SeasonId)
        {
            string strMessage = null;

            LoadSession();
            for (int i = 0; i < TranId.Length; i++)
            {
                var poEntrySubDelete = new PoEntrySub
                {
                    TranId = TranId[i],
                    StyleNo = StyleNo[i],
                    SeasoneYear = SeasonYear[i],
                    SeasoneId = SeasonId[i],
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                strMessage = _objPoEntryDal.PoEntrySubDelete(poEntrySubDelete);
            }

            return Json(strMessage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeletePoComment(string[] TranId, string[] StyleNo, string[] SeasonYear, string[] SeasonId)
        {
            string strMessage = null;

            LoadSession();
            for (int i = 0; i < TranId.Length; i++)
            {
                var poCommentDelete = new PoComments
                {
                    TranId = TranId[i],
                    StyleNo = StyleNo[i],
                    SeasoneYear = SeasonYear[i],
                    SeasoneId = SeasonId[i],
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                strMessage = _objPoEntryDal.PoCommentDelete(poCommentDelete);
            }

            return Json(strMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PoDetailsReport(string seasonId, string seasonYear, string styleNumber, string poNumber)
        {
            LoadSession();

            PoReport objPoReport = new PoReport();
            ReportDAL objReportDal = new ReportDAL();

            objPoReport.UpdateBy = strEmployeeId;
            objPoReport.HeadOfficeId = strHeadOfficeId;
            objPoReport.BranchOfficeId = strBranchOfficeId;

            if (seasonId != null && seasonYear != null && styleNumber != null && poNumber != null)
            {
                objPoReport.SeasoneId = seasonId;
                objPoReport.SeasoneYear = seasonYear;
                objPoReport.StyleNo = styleNumber;
                objPoReport.PoNumber = poNumber;
            }

            string strPath = Path.Combine(Server.MapPath("~/Reports/rptPoDetails.rpt"));
            rd.Load(strPath);

            DataSet ds = new DataSet();

            ds = (objReportDal.rdoPoDetailsSheet(objPoReport));
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