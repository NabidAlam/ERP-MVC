using ERP.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.MODEL;
using ERP.Utility;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.IO;

namespace ERP.Controllers
{
    public class TrimsController : Controller
    {
        readonly TrimsDAL _objTrimsDal = new TrimsDAL();
        readonly LookUpDAL _objLookUpDal = new LookUpDAL();
        readonly ReportDAL _objReportDal = new ReportDAL();

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
            TrimsModel model = new TrimsModel();
            ViewBag.InformationMessage = TempData["InformationMessage"] as string;

            LoadSession();
            LoadSession();
            model.HeadOfficeId = strHeadOfficeId;
            model.BranchOfficeId = strBranchOfficeId;


            model.TrimsMain = new TrimsMain();
            model.TrimsSubs = new List<TrimsSub>();
            model.TrimsMains = new List<TrimsMain>();
            //model.PoEntrySubs = new List<PoEntrySub>();

            if (TempData["Message"] != null)
            {
                ViewBag.Message = TempData["Message"] as string;
            }

            if (seasonId != null && seasonYear != null && styleNumber != null)
            {
                model.SeasonName = seasonId;
                model.SeasonYear = seasonYear;
                model.StyleNo = styleNumber;

                model.TrimsMain = _objTrimsDal.GetTrimsMainData(seasonId, seasonYear, styleNumber, model.HeadOfficeId, model.BranchOfficeId);

                model.StyleNo = model.TrimsMain.StyleNo;
                model.StyleName = model.TrimsMain.StyleName;
                model.SeasonId = model.TrimsMain.SeasonId;
                model.SeasonName = model.TrimsMain.SeasonName;
                model.SeasonYear = model.TrimsMain.SeasonYear;
                model.Interling = model.TrimsMain.Interling;
                model.MainLabel = model.TrimsMain.MainLabel;
                model.CareLabel = model.TrimsMain.CareLabel;
                model.SizeLabel = model.TrimsMain.SizeLabel;
                model.SewingThread = model.TrimsMain.SewingThread;
                model.HangTag = model.TrimsMain.HangTag;

                model.TrimsSubs = _objTrimsDal.GetTrimsSubData(model);
                model.StyleNo = styleNumber;

                ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
                ViewBag.AccessoriesDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetAccessoriesDDList(), "ACCESSORIES_ID", "ACCESSORIES_NAME");
            }

            model.TrimsMains = _objTrimsDal.GetTrimsGridDataList(strHeadOfficeId, strBranchOfficeId);

            return View(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(TrimsModel objTrimsModel)
        {
            LoadSession();
            objTrimsModel.HeadOfficeId = strHeadOfficeId;
            objTrimsModel.BranchOfficeId = strBranchOfficeId;

            if (objTrimsModel.DisplayInfo != null)
            {
                string[] data = objTrimsModel.DisplayInfo.Split('|');

                if (data.Count() < 2)
                {
                    TempData["InformationMessage"] = "Please enter style number";
                    return RedirectToAction("Index");
                }
                if (data[0] != null)
                    objTrimsModel.StyleNo = data[0].Trim();
                if (data[1] != null)
                    objTrimsModel.SeasonYear = data[1].Trim();
                if (data[2] != null)
                    objTrimsModel.SeasonName = data[2].Trim();
            }

            if (string.IsNullOrWhiteSpace(objTrimsModel.StyleNo)
                && string.IsNullOrWhiteSpace(objTrimsModel.SeasonYear)
                && string.IsNullOrWhiteSpace(objTrimsModel.SeasonName))
            {
                TempData["InformationMessage"] = "Please enter style number";
                return RedirectToAction("Index");
            }
            if (!string.IsNullOrWhiteSpace(objTrimsModel.StyleNo))
            {
                var result = StyleListData(_objTrimsDal.CheckStyleNumber(objTrimsModel.StyleNo));

                if (result != null && (result.Count == 0))
                {
                    TempData["InformationMessage"] = "Please enter valid style number";
                    return RedirectToAction("Index");
                }
            }

            var model = _objTrimsDal.GetTrimsData(objTrimsModel.StyleNo, objTrimsModel.SeasonYear,
                objTrimsModel.SeasonName, objTrimsModel.HeadOfficeId, objTrimsModel.BranchOfficeId);

            ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
            ViewBag.AccessoriesDDList = UtilityClass.GetSelectListByDataTable(_objLookUpDal.GetAccessoriesDDList(), "ACCESSORIES_ID", "ACCESSORIES_NAME");

            //model.PoEntryGrids = _objPoEntryDal.GetPoEntryMainData();

            model.TrimsMain = new TrimsMain();
            model.TrimsSubs = new List<TrimsSub>();
            model.TrimsMains = _objTrimsDal.GetTrimsGridDataList(strHeadOfficeId, strBranchOfficeId);


            return View(model);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveTrimsInformation(TrimsModel objTrimsModel)
        {
            for (int i = 0; i < objTrimsModel.TranId.Count(); i++)
            {
                if (objTrimsModel.Accessories[i] == null
                    || objTrimsModel.PerGarmentsQuantity[i] == null
                    || objTrimsModel.TotalQuantity[i] == null)
                {
                    TempData["Message"] = "All fild is required";
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
                    objTrimsModel.AccessoriesS = objTrimsModel.Accessories[i];
                    objTrimsModel.TrimsCodeS = objTrimsModel.TrimsCode[i];
                    objTrimsModel.PerGarmentsQuantityS = objTrimsModel.PerGarmentsQuantity[i];
                    objTrimsModel.TotalQuantityS = objTrimsModel.TotalQuantity[i];

                    string strMessage = _objTrimsDal.TrimsInformationSave(objTrimsModel);
                    TempData["Message"] = strMessage;
                }
            }

            return RedirectToAction("Index");
        }

        public ActionResult DeleteTrimsSub(string[] TranId, string[] StyleNo, string[] SeasonYear, string[] SeasonId)
        {
            string strMessage = null;

            LoadSession();
            for (int i = 0; i < TranId.Length; i++)
            {
                var trimsSubDelete = new TrimsSub
                {
                    TranId = TranId[i],
                    StyleNo = StyleNo[i],
                    SeasoneYear = SeasonYear[i],
                    SeasoneId = SeasonId[i],
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                strMessage = _objTrimsDal.TrimsSubDelete(trimsSubDelete);
            }

            return Json(strMessage, JsonRequestBehavior.AllowGet);
        }

        public ActionResult TrimsDetailsReport(string seasonId, string seasonYear, string styleNumber)
        {
            LoadSession();

            TrimsReport objTrimsReport = new TrimsReport();

            objTrimsReport.HeadOfficeId = strHeadOfficeId;
            objTrimsReport.BranchOfficeId = strBranchOfficeId;

            if (seasonId != null && seasonYear != null && styleNumber != null)
            {
                objTrimsReport.SeasoneId = seasonId;
                objTrimsReport.SeasoneYear = seasonYear;
                objTrimsReport.StyleNo = styleNumber;
            }

            string strPath = Path.Combine(Server.MapPath("~/Reports/rptTrimsDetail.rpt"));
            rd.Load(strPath);

            DataSet ds = new DataSet();

            ds = (_objReportDal.TrimsDetailSheet(objTrimsReport));
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