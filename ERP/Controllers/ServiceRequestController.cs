using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.Shared;
using ERP.MODEL;
using ERP.Utility;
using ERP.DAL;
using CrystalDecisions.CrystalReports.Engine;

namespace ERP.Controllers
{
    public class ServiceRequestController : Controller
    {
        LookUpDAL objLookUpDAL = new LookUpDAL();
        ReportDAL objReportDAL = new ReportDAL();

        ReportDocument objReportDocument = new ReportDocument();
        ExportFormatType objExportFormatType = ExportFormatType.NoFormat;

        #region Common

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

        public ActionResult AppointmentLetterReport()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

               
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AppointmentLetterReport(ReportAppointmentLetterModel objAppointmentLetterModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
               // objAppointmentLetterModel.EmployeeId = strEmployeeId;
                objAppointmentLetterModel.UpdateBy = strEmployeeId;
                objAppointmentLetterModel.HeadOfficeId = strHeadOfficeId;
                objAppointmentLetterModel.BranchOfficeId = strBranchOfficeId;

                objReportDAL.SaveJobDescription(objAppointmentLetterModel);
                DataSet objDataSet = objReportDAL.AppointmentLetter(objAppointmentLetterModel);

                string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptAppointmentLetter.rpt"));
                objReportDocument.Load(vReportPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport(objAppointmentLetterModel.ReportType, "ReportAppointmentLetter");

                return RedirectToAction("AppointmentLetterReport", "ServiceRequest");
            }
        }





        public ActionResult ConfirmationLetterReport()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmationLetterReport(ReportConfirmationLetterModel objConfirmationLetterModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                // objAppointmentLetterModel.EmployeeId = strEmployeeId;
                objConfirmationLetterModel.UpdateBy = strEmployeeId;
                objConfirmationLetterModel.HeadOfficeId = strHeadOfficeId;
                objConfirmationLetterModel.BranchOfficeId = strBranchOfficeId;

                DataSet objDataSet = objReportDAL.ConfirmationLetter(objConfirmationLetterModel);

                string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptConfirmationLetter.rpt"));
                objReportDocument.Load(vReportPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport(objConfirmationLetterModel.ReportType, "ConfirmationLetter");

                return RedirectToAction("ConfirmationLetterReport", "ServiceRequest");
            }
        }




        public ActionResult ReleaseLetterReport()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReleaseLetterReport(ReportReleaseLetterModel objReleaseLetterModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                // objAppointmentLetterModel.EmployeeId = strEmployeeId;
                objReleaseLetterModel.UpdateBy = strEmployeeId;
                objReleaseLetterModel.HeadOfficeId = strHeadOfficeId;
                objReleaseLetterModel.BranchOfficeId = strBranchOfficeId;

                DataSet objDataSet = objReportDAL.DisplayResignLetter(objReleaseLetterModel);

                string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptReleaseLetter.rpt"));
                objReportDocument.Load(vReportPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport(objReleaseLetterModel.ReportType, "ReleaseLetter");

                return RedirectToAction("ReleaseLetterReport", "ServiceRequest");
            }
        }



        public FileStreamResult ShowReport(string pReportType, string pFileDownloadName)
        {
            string vContentType;

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Clear();
            Response.Buffer = true;

            if (pReportType == "PDF")
            {
                objExportFormatType = ExportFormatType.PortableDocFormat;

                vContentType = "application/pdf";

                pFileDownloadName += ".pdf";
            }
            else if (pReportType == "Excel")
            {
                objExportFormatType = ExportFormatType.Excel;

                vContentType = "application/vnd.ms-excel";

                pFileDownloadName += ".xls";
            }
            else if (pReportType == "CSV")
            {
                objExportFormatType = ExportFormatType.CharacterSeparatedValues;

                vContentType = "application/vnd.ms-excel";

                pFileDownloadName += ".csv";
            }

            //else if (pReportType == "DOC")
            else
            {
                objExportFormatType = ExportFormatType.WordForWindows;

                vContentType = "application/doc";
                Response.AppendHeader("Content-Disposition", $"filename={pFileDownloadName}.doc");

                pFileDownloadName += ".doc";
            }

            Stream objStream = objReportDocument.ExportToStream(objExportFormatType);
            byte[] objBufferredData = new byte[objStream.Length];
            objStream.Read(objBufferredData, 0, Convert.ToInt32(objStream.Length - 1));

            Response.ContentType = vContentType;

            Response.BinaryWrite(objBufferredData);
            Response.Flush();
            Response.Close();
            objReportDocument.Close();
            objReportDocument.Dispose();

            return File(objStream, Response.ContentType, pFileDownloadName);
        }




        //CONFIRMATION LETTER REPORT - NABID - 15 DEC 18




    }
}