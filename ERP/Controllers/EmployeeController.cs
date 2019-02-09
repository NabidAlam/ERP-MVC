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
using PagedList;
using ERP.Utility;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using System.Text;
using System.Security.Cryptography;
using System.Collections;

namespace ERP.Controllers
{
    public class EmployeeController : Controller
    {
        #region Common

        LookUpDAL objLookUpDAL = new LookUpDAL();
        EmployeeDAL objEmployeeDAL = new EmployeeDAL();
        ReportDAL objReportDAL = new ReportDAL();
        ReportDocument objReportDocument = new ReportDocument();
        ExportFormatType objExportFormatType = ExportFormatType.NoFormat;
        EmployeeModel objEmployeeModel = new EmployeeModel();
        EmployeeDataById objEmployeeDataById = new EmployeeDataById();


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
        public void LoadDropDownList()
        {

            ViewBag.CountryDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCountryDDList(), "COUNTRY_ID", "COUNTRY_NAME");
            ViewBag.DivisionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDivisionDDList(), "DIVISION_ID", "DIVISION_NAME");
            ViewBag.DistrictDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDistrictDDList(), "DISTRICT_ID", "DISTRICT_NAME");
            ViewBag.GenderDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetGenderDDList(), "GENDER_ID", "GENDER_NAME");
            ViewBag.BloodGroupDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetBloodGroupDDList(), "BLOOD_GROUP_ID", "BLOOD_GROUP_NAME");
            ViewBag.MaritalStatusDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMaritalStatusDDList(), "MARITAL_STATUS_ID", "MARITAL_STATUS_NAME");
            ViewBag.ReligionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetReligionDDList(), "RELIGION_ID", "RELIGION_NAME");
            ViewBag.EmployeementTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetEmployeementTypeDDList(), "OCCURENCE_TYPE_ID", "OCCURENCE_TYPE_NAME");
            ViewBag.EmployeeTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetEmployeeTypeDDList(strHeadOfficeId, strBranchOfficeId), "EMPLOYEE_TYPE_ID", "EMPLOYEE_TYPE_NAME");
            ViewBag.DesignationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetEmployeeDesignationDDList(), "DESIGNATION_ID", "DESIGNATION_NAME");
            ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
            ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDList(strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME");
            ViewBag.GradeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetGradeDDList(), "GRADE_ID", "GRADE_NO");
            ViewBag.ProbationPeriodDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetProbationPeriodDDList(), "PROBATION_PERIOD_ID", "PROBATION_PERIOD");
            ViewBag.SupervisorDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSupervisorDDList(), "SUPERVISOR_EMPLOYEE_ID", "SUPERVISOR_EMPLOYEE_NAME");
            ViewBag.JobTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetJobTypeDDList(), "JOB_TYPE_ID", "JOB_TYPE_NAME");
            ViewBag.PaymentTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetPaymentTypeDDList(), "PAYMENT_TYPE_ID", "PAYMENT_TYPE_NAME");
            ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDList(strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME");
            ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDList(strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME");
            ViewBag.ShiftDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetShiftDDList(), "SHIFT_ID", "SHIFT_NAME");
            ViewBag.JobLocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetJobLocationDDList(), "JOB_LOCATION_ID", "JOB_LOCATION");
            ViewBag.ApprovedByDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetApprovedByDDList(), "APPROVED_EMPLOYEE_ID", "APPROVED_EMPLOYEE_NAME");
            ViewBag.HolidayDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetWeeklyHolidayDDList(), "WEEKLY_HOLIDAY_ID", "WEEKLY_HOLIDAY_NAME");
            ViewBag.DegreeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDegreeDDList(), "DEGREE_ID", "DEGREE_NAME");
            ViewBag.MajorSubjectDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMajorSubjectDDList(), "MAJOR_SUBJECT_ID", "MAJOR_SUBJECT_NAME");
        }

        #endregion
        #region "Home"
        public ActionResult Home()
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



                    objEmployeeModel = objEmployeeDAL.GetEmployeeProfileById(strEmployeeId, strHeadOfficeId, strBranchOfficeId);

                    if (objEmployeeModel == null)
                    {
                        objEmployeeModel = new EmployeeModel();
                        TempData["OperationMessage"] = "No employee found.";
                    }

                    return View(objEmployeeModel);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        #endregion





        #region Employee Profile
        public ActionResult EmployeeProfile()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                EmployeeModel objEmployeeModel = new EmployeeModel();

                //objEmployeeModel.JoiningSalary = EncryptBp();

                objEmployeeModel = objEmployeeDAL.GetEmployeeProfileById(strEmployeeId, strHeadOfficeId, strBranchOfficeId);

                objEmployeeModel.FirstSalary = EncryptBp(objEmployeeModel.FirstSalary);
                objEmployeeModel.GrossSalary = EncryptBp(objEmployeeModel.GrossSalary);
                objEmployeeModel.JoiningSalary = EncryptBp(objEmployeeModel.JoiningSalary);

                objEmployeeModel.EmployeeId = strEmployeeId;

                //objEmployeeModel.

                if (objEmployeeModel == null)
                {
                    objEmployeeModel = new EmployeeModel();
                    TempData["OperationMessage"] = "No employee found.";
                }

                return View(objEmployeeModel);
            }
        }

        [HttpPost]
        public ActionResult EmployeeProfile(EmployeeModel objEmployeeModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objEmployeeModel = objEmployeeDAL.CheckValidPassword(objEmployeeModel);
                if (objEmployeeModel.Message == "TRUE")
                {
                    objEmployeeModel = objEmployeeDAL.GetEmployeeProfileById(strEmployeeId, strHeadOfficeId, strBranchOfficeId);

                    //objEmployeeModel.FirstSalary = DecryptBP(objEmployeeModel.FirstSalary);
                    //objEmployeeModel.GrossSalary = DecryptBP(objEmployeeModel.GrossSalary);
                    //objEmployeeModel.JoiningSalary = DecryptBP(objEmployeeModel.JoiningSalary);
                }

                ViewBag.ButtonDisable = 1;
                return View(objEmployeeModel);
            }
        }

        [HttpGet]
        public ActionResult SearchEmployee()
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
        public ActionResult SearchEmployee(EmployeeModel objEmployeeModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                if (!string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId))
                {
                    objEmployeeModel = objEmployeeDAL.GetEmployeeProfileById(objEmployeeModel.EmployeeId, strHeadOfficeId, strBranchOfficeId);

                    if (objEmployeeModel == null)
                    {
                        objEmployeeModel = new EmployeeModel();
                        TempData["OperationMessage"] = "No employee found.";
                    }

                    return View(objEmployeeModel);
                }
                else
                {
                    TempData["OperationMessage"] = "Please provide an employee id";
                    return RedirectToAction("SearchEmployee", "Employee");
                }
            }
        }


        #endregion

        #region Employee All Report

        [HttpGet]
        public ActionResult AttendenceReport()
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
        public ActionResult LeaveReport()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                ReportModel objReportModel = new ReportModel();
                objReportModel.LeaveYear = DateTime.Now.ToString("yyyy");
                return View(objReportModel);
            }
        }


        [HttpPost]
        public ActionResult AttendenceReport(ReportModel objReportModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objReportModel.EmployeeId = strEmployeeId;
                objReportModel.UpdateBy = strEmployeeId;
                objReportModel.HeadOfficeId = strHeadOfficeId;
                objReportModel.BranchOfficeId = strBranchOfficeId;

                string vProcessedStatus = objReportDAL.ProcessIndividualAttendance(objReportModel);

                objReportModel.HeadOfficeId = strHeadOfficeId;
                objReportModel.BranchOfficeId = strBranchOfficeId;


                DataSet objDataSet = objReportDAL.GetIndividualAttendanceData(objReportModel);

                string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
                objReportDocument.Load(vReportPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport(objReportModel.ReportType, "AttendanceDetail");

                return View();


            }
        }

        [HttpPost]
        public ActionResult LeaveReport(ReportModel objReportModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objReportModel.EmployeeId = strEmployeeId;
                objReportModel.UpdateBy = strEmployeeId;
                objReportModel.HeadOfficeId = strHeadOfficeId;
                objReportModel.BranchOfficeId = strBranchOfficeId;

                //objReportModel.LeaveYear = DateTime.Now.ToString("yyyy");

                string vProcessedStatus = objReportDAL.IndividualLeaveProess(objReportModel);

                objReportModel.HeadOfficeId = strHeadOfficeId;
                objReportModel.BranchOfficeId = strBranchOfficeId;


                DataSet objDataSet = objReportDAL.GetLeaveIndividual(objReportModel);

                string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptLeaveIndividual.rpt"));
                objReportDocument.Load(vReportPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport(objReportModel.ReportType, "LeaveDetail");

                return View();

            }
        }

        public FileStreamResult ShowReport(string pReportType, string pFileDownloadName)
        {

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            Response.Clear();
            Response.Buffer = true;



            if (pReportType == "PDF")
            {
                objExportFormatType = ExportFormatType.PortableDocFormat;

                Stream oStream = objReportDocument.ExportToStream(objExportFormatType);
                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ContentType = "application/pdf";

                pFileDownloadName += ".pdf";

                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                objReportDocument.Close();
                objReportDocument.Dispose();

                return File(oStream, Response.ContentType, pFileDownloadName);
            }
            else if (pReportType == "Excel")
            {
                objExportFormatType = ExportFormatType.Excel;

                Stream oStream = objReportDocument.ExportToStream(objExportFormatType);
                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ContentType = "application/vnd.ms-excel";

                pFileDownloadName += ".xls";

                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                objReportDocument.Close();
                objReportDocument.Dispose();

                return File(oStream, Response.ContentType, pFileDownloadName);
            }
            else if (pReportType == "CSV")
            {
                objExportFormatType = ExportFormatType.CharacterSeparatedValues;

                Stream oStream = objReportDocument.ExportToStream(objExportFormatType);
                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ContentType = "text/csv";

                pFileDownloadName += ".csv";

                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                objReportDocument.Close();
                objReportDocument.Dispose();

                return File(oStream, Response.ContentType, pFileDownloadName);
            }
            else if (pReportType == "TXT")
            {
                objExportFormatType = ExportFormatType.RichText;

                Stream oStream = objReportDocument.ExportToStream(objExportFormatType);
                byte[] byteArray = new byte[oStream.Length];
                oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));

                Response.ContentType = "text/plain";

                pFileDownloadName += ".txt";

                Response.BinaryWrite(byteArray);
                Response.Flush();
                Response.Close();
                objReportDocument.Close();
                objReportDocument.Dispose();

                return File(oStream, Response.ContentType, pFileDownloadName);
            }

            return null;
        }

        #endregion

        #region Employee Entry

        //mezba 1
        public ActionResult EmployeeEntry(EmployeeModel objEmployeeModel, string SearchActiveYN, string pEmployeeId = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                ModelState.Clear();
                LoadSession();
                LoadDropDownList();

                objEmployeeModel.UpdateBy = strEmployeeId;
                objEmployeeModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeModel.BranchOfficeId = strBranchOfficeId;



                if (objEmployeeModel.SearchBy == "1")
                {
                    objEmployeeModel.SearchInactiveYN = SearchActiveYN.Contains("true") ? "N" : "Y";
                    DataTable dt = objEmployeeDAL.LoadEmployeeData(objEmployeeModel);
                    ViewBag.EmployeeList = EmployeeListData(dt);
                }



                if (!string.IsNullOrWhiteSpace(pEmployeeId))
                {
                    objEmployeeModel.EmployeeId = pEmployeeId;
                    objEmployeeModel = objEmployeeDAL.SearchEmployeeInformation(objEmployeeModel);

                    string strPermissionYn = objEmployeeDAL.PermissionYn(strEmployeeId, strHeadOfficeId, strBranchOfficeId);

                    if (strPermissionYn == "" || strPermissionYn == null)
                    {
                        strPermissionYn = "N";

                    }

                    if (strPermissionYn != "Y")
                    {
                        string strJoiningSalary = objEmployeeModel.JoiningSalary;
                        string strGrossSalary = objEmployeeModel.JoiningSalary;
                        string strFirstSalary = objEmployeeModel.JoiningSalary;

                        if(strJoiningSalary == null)
                        {
                            strJoiningSalary = "0";

                        }

                        if (strGrossSalary == null)
                        {
                            strGrossSalary = "0";

                        }

                        if (strFirstSalary == null)
                        {
                            strFirstSalary = "0";

                        }

                        objEmployeeModel.JoiningSalary = EncryptBp(strJoiningSalary);
                        objEmployeeModel.GrossSalary = EncryptBp(strGrossSalary);
                        objEmployeeModel.FirstSalary = EncryptBp(strFirstSalary);
                        
                    }


                    objEmployeeModel = objEmployeeDAL.SearchEmployeePhoto(objEmployeeModel);
                    objEmployeeModel = objEmployeeDAL.SearchEmployeeSignature(objEmployeeModel);
                    objEmployeeModel = objEmployeeDAL.SearchEmployeeCv(objEmployeeModel);

                    DataTable dt1 = objEmployeeDAL.LoadEmployePreviousJobData(objEmployeeModel);
                    DataTable dt2 = objEmployeeDAL.LoadEmployeEducationData(objEmployeeModel);

                    if (dt1.Rows.Count > 0)
                    {
                        ViewBag.EmployePreviousJobData = EmployePreviousJobData(dt1);
                    }
                    if (dt2.Rows.Count > 0)
                    {
                        ViewBag.EmployeEducationData = EmployeEducationData(dt2);
                    }

                    DataTable dt = objEmployeeDAL.LoadEmployeeData(objEmployeeModel);
                    ViewBag.EmployeeList = EmployeeListData(dt);

                }
                return View(objEmployeeModel);
            }
        }
        public List<EmployeeModel> EmployeeListData(DataTable dt)
        {
            List<EmployeeModel> EmployeeDataBundle = new List<EmployeeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EmployeeModel objEmployeeModel = new EmployeeModel();

                objEmployeeModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objEmployeeModel.EmployeeId = dt.Rows[i]["EMPLOYEE_ID"].ToString();
                objEmployeeModel.EmployeeName = dt.Rows[i]["EMPLOYEE_NAME"].ToString();
                objEmployeeModel.DateOfBirth = dt.Rows[i]["DATE_OF_BIRTH"].ToString();
                objEmployeeModel.JoiningDate = dt.Rows[i]["JOINING_DATE"].ToString();
                objEmployeeModel.JobConfirmationDate = dt.Rows[i]["JOB_CONFIRMATION_DATE"].ToString();
                objEmployeeModel.PresentDesignationName = dt.Rows[i]["DESIGNATION_NAME"].ToString();
                objEmployeeModel.DepartmentName = dt.Rows[i]["DEPARTMENT_NAME"].ToString();
                objEmployeeModel.UnitName = dt.Rows[i]["UNIT_NAME"].ToString();
                objEmployeeModel.SectionName = dt.Rows[i]["SECTION_NAME"].ToString();
                objEmployeeModel.SubSectionName = dt.Rows[i]["SUB_SECTION_NAME"].ToString();
                objEmployeeModel.GradeId = dt.Rows[i]["GRADE_NO"].ToString();
                objEmployeeModel.Status = dt.Rows[i]["ACTIVE_STATUS"].ToString();

                if (dt.Rows[i]["EMPLOYEE_IMAGE_NAME"].ToString() != ""  )
                {
                    objEmployeeModel.ImageFileByte = (byte[])dt.Rows[i]["EMPLOYEE_IMAGE_SIZE"];
                    objEmployeeModel.ImageFileNameBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(objEmployeeModel.ImageFileByte);
                }

                EmployeeDataBundle.Add(objEmployeeModel);
            }
            return EmployeeDataBundle;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmployeeEntry(HttpPostedFileBase Image, HttpPostedFileBase Signature, HttpPostedFileBase CV, HttpPostedFileBase[] ExperienceCertificate, HttpPostedFileBase[] ClearanceCertificate, HttpPostedFileBase[] EducationCertificate, EmployeeModel objEmployeeModel)
        {
            LoadSession();

            objEmployeeModel.ActiveYN = objEmployeeModel.ActiveYN.Contains("true") ? "Y" : "N";
            objEmployeeModel.UpdateBy = strEmployeeId;
            objEmployeeModel.HeadOfficeId = strHeadOfficeId;
            objEmployeeModel.BranchOfficeId = strBranchOfficeId;

            string strDBMsg = "";

            if(objEmployeeModel.EmployeeId == null || objEmployeeModel.EmployeeId =="")
            {
                string strMsg = "Please enter employee id!!!";
                ViewBag.AlertMsg = strMsg;
                return View();
            }



            if (ModelState.IsValid)
            {
                bool IsNumberGrossSalary = Regex.IsMatch(objEmployeeModel.GrossSalary, @"^\d+$");
                if (IsNumberGrossSalary == true)
                {

                    objEmployeeModel.GrossSalary = objEmployeeModel.GrossSalary;
                }
                else
                {
                    string strGrossSalary = DecryptBP(objEmployeeModel.GrossSalary);
                    objEmployeeModel.GrossSalary = strGrossSalary;
                }

                bool IsNumberFirstSalary = Regex.IsMatch(objEmployeeModel.FirstSalary, @"^\d+$");
                if (IsNumberFirstSalary == true)
                {

                    objEmployeeModel.FirstSalary = objEmployeeModel.FirstSalary;
                }
                else
                {
                    string strFirstSalary = DecryptBP(objEmployeeModel.FirstSalary);
                    objEmployeeModel.FirstSalary = strFirstSalary;
                }



                bool IsNumberJoiningSalary = Regex.IsMatch(objEmployeeModel.JoiningSalary, @"^\d+$");
                if (IsNumberJoiningSalary == true)
                {

                    objEmployeeModel.JoiningSalary = objEmployeeModel.JoiningSalary;
                }
                else
                {
                    string strJoiningSalary = DecryptBP(objEmployeeModel.JoiningSalary);
                    objEmployeeModel.JoiningSalary  = strJoiningSalary;
                }


                strDBMsg = objEmployeeDAL.SaveEmployeeInformation(objEmployeeModel);
                TempData["OperationMessage"] = strDBMsg;

                if (Image != null)
                {
                    String ImageExtension = Path.GetExtension(Image.FileName).ToUpper();
                    if (ImageExtension == ".JPG" || ImageExtension == ".JPEG" || ImageExtension == ".PNG")
                    {
                        Stream str = Image.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                        string ImageSize = Convert.ToBase64String(FileDet);

                        objEmployeeModel.ImageFileName = Image.FileName;
                        objEmployeeModel.ImageFileSize = ImageSize;
                        objEmployeeModel.ImageFileExtension = ImageExtension;
                        objEmployeeDAL.SaveEmployeeImage(objEmployeeModel);
                    }
                }


                if (Signature != null)
                {
                    String SignatureExtension = Path.GetExtension(Signature.FileName).ToUpper();
                    if (SignatureExtension == ".JPG" || SignatureExtension == ".JPEG" || SignatureExtension == ".PNG")
                    {
                        Stream str = Signature.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                        string SignatureSize = Convert.ToBase64String(FileDet);

                        objEmployeeModel.SignatureFileName = Signature.FileName;
                        objEmployeeModel.SignatureFileSize = SignatureSize;
                        objEmployeeModel.SignatureFileExtension = SignatureExtension;
                        objEmployeeDAL.SaveEmployeeSignature(objEmployeeModel);
                    }
                }



                if (CV != null)
                {
                    String CVExtension = Path.GetExtension(CV.FileName).ToUpper();
                    if (CVExtension == ".DOC" || CVExtension == ".DOCX" || CVExtension == ".PDF")
                    {
                        Stream str = CV.InputStream;
                        BinaryReader Br = new BinaryReader(str);
                        Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                        string CVSize = Convert.ToBase64String(FileDet);

                        objEmployeeModel.CVFileName = CV.FileName;
                        objEmployeeModel.CVFileSize = CVSize;
                        objEmployeeModel.CVFileExtension = CVExtension;
                        objEmployeeDAL.SaveEmployeeCv(objEmployeeModel);
                    }
                }

                objEmployeeDAL.SaveEmployeePreviousJobInformation(objEmployeeModel);
                objEmployeeDAL.SaveEmployeeEducationInformation(objEmployeeModel);


                for (int i = 0; i < ExperienceCertificate.Count(); i++)
                {
                    if (ExperienceCertificate[i] != null)
                    {

                        String ExperienceCertificateExtension = Path.GetExtension(ExperienceCertificate[i].FileName).ToUpper();
                        if (ExperienceCertificateExtension == ".PDF")
                        {
                            Stream str = ExperienceCertificate[i].InputStream;
                            BinaryReader Br = new BinaryReader(str);
                            Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                            string ExperienceCertificateSize = Convert.ToBase64String(FileDet);

                            objEmployeeModel.ExpCertifiateFileName = ExperienceCertificate[i].FileName;
                            objEmployeeModel.ExpCertificateFileSize = ExperienceCertificateSize;
                            objEmployeeModel.ExpCertificateFileExtension = ExperienceCertificateExtension;
                            objEmployeeModel.PrevJobResignDate = objEmployeeModel.PreviousJobResignDate[i];
                            objEmployeeDAL.SaveEmployeeExperienceCertificate(objEmployeeModel);
                        }

                    }

                }

                for (int i = 0; i < ClearanceCertificate.Count(); i++)
                {
                    if (ClearanceCertificate[i] != null)
                    {

                        String ClearanceCertificateExtension = Path.GetExtension(ClearanceCertificate[i].FileName).ToUpper();
                        if (ClearanceCertificateExtension == ".PDF")
                        {
                            Stream str = ClearanceCertificate[i].InputStream;
                            BinaryReader Br = new BinaryReader(str);
                            Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                            string ClearanceCertificateSize = Convert.ToBase64String(FileDet);

                            objEmployeeModel.ClrCertifiateFileName = ClearanceCertificate[i].FileName;
                            objEmployeeModel.ClrCertificateFileSize = ClearanceCertificateSize;
                            objEmployeeModel.ClrCertificateFileExtension = ClearanceCertificateExtension;
                            objEmployeeModel.PrevJobResignDate = objEmployeeModel.PreviousJobResignDate[i];
                            objEmployeeDAL.SaveEmployeeClearenceCertificate(objEmployeeModel);
                        }

                    }

                }



                for (int i = 0; i < EducationCertificate.Count(); i++)
                {
                    if (EducationCertificate[i] != null)
                    {

                        String EducationCertificateExtension = Path.GetExtension(EducationCertificate[i].FileName).ToUpper();
                        if (EducationCertificateExtension == ".PDF")
                        {
                            Stream str = EducationCertificate[i].InputStream;
                            BinaryReader Br = new BinaryReader(str);
                            Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                            string EducationCertificateSize = Convert.ToBase64String(FileDet);

                            objEmployeeModel.CertifiateFileName = EducationCertificate[i].FileName;
                            objEmployeeModel.CertificateFileSize = EducationCertificateSize;
                            objEmployeeModel.CertificateFileExtension = EducationCertificateExtension;
                            objEmployeeModel.PassingYear = objEmployeeModel.Year[i];
                            objEmployeeDAL.SaveEmployeeEducationCertificate(objEmployeeModel);
                        }

                    }

                }

            }


            ModelState.Clear();
            return RedirectToAction("EmployeeEntry", new { EditValue = objEmployeeModel.SearchBy });
        }

        [HttpGet]
        public ActionResult EditEmployee(string pEmployeeId)
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
                    objEmployeeModel.EmployeeId = pEmployeeId;
                    objEmployeeModel.UpdateBy = strEmployeeId;
                    objEmployeeModel.HeadOfficeId = strHeadOfficeId;
                    objEmployeeModel.BranchOfficeId = strBranchOfficeId;

                    if (!string.IsNullOrWhiteSpace(objEmployeeModel.EmployeeId))
                    {

                        objEmployeeModel = objEmployeeDAL.SearchEmployeeInformation(objEmployeeModel);
                        objEmployeeModel = objEmployeeDAL.SearchEmployeePhoto(objEmployeeModel);
                        objEmployeeModel = objEmployeeDAL.SearchEmployeeSignature(objEmployeeModel);
                        objEmployeeModel = objEmployeeDAL.SearchEmployeeCv(objEmployeeModel);

                        DataTable dt1 = objEmployeeDAL.LoadEmployePreviousJobData(objEmployeeModel);
                        DataTable dt2 = objEmployeeDAL.LoadEmployeEducationData(objEmployeeModel);

                        if (dt1.Rows.Count > 0)
                        {
                            ViewBag.EmployePreviousJobData = EmployePreviousJobData(dt1);
                        }
                        if (dt2.Rows.Count > 0)
                        {
                            ViewBag.EmployeEducationData = EmployeEducationData(dt2);
                        }



                    }

                    DataTable dt = objEmployeeDAL.LoadEmployeeDataForEdit(objEmployeeModel);
                    ViewBag.EmployeeList = EmployeeListData(dt);

                    LoadDropDownList();
                    return View("EmployeeEntry", objEmployeeModel);
                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public List<EmployeeModel> EmployePreviousJobData(DataTable dt1)
        {
            List<EmployeeModel> EmployePreviousJobDataBundle = new List<EmployeeModel>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                EmployeeModel objemEmployeeModel = new EmployeeModel();

                objemEmployeeModel.EmployeeId = dt1.Rows[i]["EMPLOYEE_ID"].ToString();
                objemEmployeeModel.GridOrganizationName = dt1.Rows[i]["ORGANIZATION_NAME"].ToString();
                objemEmployeeModel.GridPreviousJobDesignationId = dt1.Rows[i]["DESIGNATION_ID"].ToString();
                objemEmployeeModel.GridPreviousJobSalary = dt1.Rows[i]["GROSS_SALARY"].ToString();
                objemEmployeeModel.GridPreviousJobJoiningDate = dt1.Rows[i]["JOINING_DATE"].ToString();
                objemEmployeeModel.GridPreviousJobResignDate = dt1.Rows[i]["RESIGN_DATE"].ToString();
                objemEmployeeModel.GridPreviousJobCertificate = dt1.Rows[i]["CERTIFICATE_FILE_NAME"].ToString();
                objemEmployeeModel.GridPreviousJobClearance = dt1.Rows[i]["CLEARANCE_FILE_NAME"].ToString();


                EmployePreviousJobDataBundle.Add(objemEmployeeModel);
            }
            return EmployePreviousJobDataBundle;
        }
        public List<EmployeeModel> EmployeEducationData(DataTable dt2)
        {
            List<EmployeeModel> EmployeEducationDataBundle = new List<EmployeeModel>();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                EmployeeModel objEmployeeModel = new EmployeeModel();

                objEmployeeModel.EmployeeId = dt2.Rows[i]["EMPLOYEE_ID"].ToString();
                objEmployeeModel.GridInstituteName = dt2.Rows[i]["INSTITUTE_NAME"].ToString();
                objEmployeeModel.GridDegreeId = dt2.Rows[i]["DEGREE_ID"].ToString();
                objEmployeeModel.GridMajorSubjectId = dt2.Rows[i]["MAJOR_SUBJECT_ID"].ToString();
                objEmployeeModel.GridYear = dt2.Rows[i]["PASSING_YEAR"].ToString();
                objEmployeeModel.GridCGPA = dt2.Rows[i]["CGPA"].ToString();
                objEmployeeModel.GridCertificateFileName = dt2.Rows[i]["FILE_NAME"].ToString();

                EmployeEducationDataBundle.Add(objEmployeeModel);
            }
            return EmployeEducationDataBundle;
        }

        public ActionResult GetRetirementDate(EmployeeModel objEmployeeModel)
        {
            LoadSession();

            objEmployeeModel.UpdateBy = strEmployeeId;
            objEmployeeModel.HeadOfficeId = strHeadOfficeId;
            objEmployeeModel.BranchOfficeId = strBranchOfficeId;

            objEmployeeModel = objEmployeeDAL.GetRetirementDate(objEmployeeModel);

            return Json(objEmployeeModel.RetirementDate, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetJobConfirmationDate(EmployeeModel objEmployeeModel)
        {
            LoadSession();

            objEmployeeModel.UpdateBy = strEmployeeId;
            objEmployeeModel.HeadOfficeId = strHeadOfficeId;
            objEmployeeModel.BranchOfficeId = strBranchOfficeId;

            objEmployeeModel = objEmployeeDAL.GetJobConfirmationDate(objEmployeeModel);

            return Json(objEmployeeModel.JobConfirmationDate, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeCV(string employeeId)
        {
            LoadSession();
            objEmployeeModel.EmployeeId = employeeId;
            objEmployeeModel.UpdateBy = strEmployeeId;
            objEmployeeModel.HeadOfficeId = strHeadOfficeId;
            objEmployeeModel.BranchOfficeId = strBranchOfficeId;

            objEmployeeModel = objEmployeeDAL.SearchEmployeeCv(objEmployeeModel);

            return File(objEmployeeModel.EditCVFileByte, "application/pdf");
        }
        public ActionResult EmployeeEducationCertificate(string employeeId, string year)
        {
            LoadSession();
            objEmployeeModel.EmployeeId = employeeId;
            objEmployeeModel.EditGridYear = year;
            objEmployeeModel.UpdateBy = strEmployeeId;
            objEmployeeModel.HeadOfficeId = strHeadOfficeId;
            objEmployeeModel.BranchOfficeId = strBranchOfficeId;

            objEmployeeModel = objEmployeeDAL.SearchEducationCertificate(objEmployeeModel);

            return File(objEmployeeModel.EditEducationFileByte, "application/pdf");
        }
        public ActionResult EmployeeJobCertificate(string employeeId, string date)
        {
            LoadSession();
            objEmployeeModel.EmployeeId = employeeId;
            objEmployeeModel.EditGridResignDate = date;
            objEmployeeModel.UpdateBy = strEmployeeId;
            objEmployeeModel.HeadOfficeId = strHeadOfficeId;
            objEmployeeModel.BranchOfficeId = strBranchOfficeId;

            objEmployeeModel = objEmployeeDAL.SearchJobCertificate(objEmployeeModel);

            return File(objEmployeeModel.EditJobCertificateFileByte, "application/pdf");
        }
        public ActionResult EmployeeJobClearance(string employeeId, string date)
        {
            LoadSession();
            objEmployeeModel.EmployeeId = employeeId;
            objEmployeeModel.EditGridResignDate = date;
            objEmployeeModel.UpdateBy = strEmployeeId;
            objEmployeeModel.HeadOfficeId = strHeadOfficeId;
            objEmployeeModel.BranchOfficeId = strBranchOfficeId;

            objEmployeeModel = objEmployeeDAL.SearchJobClearance(objEmployeeModel);

            return File(objEmployeeModel.EditJobClearanceFileByte, "application/pdf");
        }
        public JsonResult DeleteEmployeeEducation(EmployeeModel objEmployeeModel)
        {
            LoadSession();
            string strDBMsg = "";
            objEmployeeModel.UpdateBy = strEmployeeId;
            objEmployeeModel.HeadOfficeId = strHeadOfficeId;
            objEmployeeModel.BranchOfficeId = strBranchOfficeId;
            objEmployeeDAL.DeleteEmployeeEducationCertificate(objEmployeeModel);
            strDBMsg = objEmployeeDAL.DeleteEmployeeEducation(objEmployeeModel);
            return Json(strDBMsg, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteEmployeePreviousJob(EmployeeModel objEmployeeModel)
        {
            LoadSession();
            string strDBMsg = "";
            objEmployeeModel.UpdateBy = strEmployeeId;
            objEmployeeModel.HeadOfficeId = strHeadOfficeId;
            objEmployeeModel.BranchOfficeId = strBranchOfficeId;
            objEmployeeDAL.DeleteEmpPrvJobClrCertificate(objEmployeeModel);
            objEmployeeDAL.DeleteEmpPrvJobExpCertificate(objEmployeeModel);
            strDBMsg = objEmployeeDAL.DeleteEmployeePreviousJob(objEmployeeModel);
            return Json(strDBMsg, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EmployeeIdAutocomplete(string term)
        {

            LoadSession();
            objEmployeeDataById.HeadOfficeId = strHeadOfficeId;
            objEmployeeDataById.BranchOfficeId = strBranchOfficeId;


            DataTable dt = objEmployeeDAL.GetEmployeeId(objEmployeeDataById);

            string[] items = new string[dt.Rows.Count];

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                items[i] = dt.Rows[i]["EMPLOYEE_ID"].ToString();
            }

            var filteredItems = items.Where(
               item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
            );

            return Json(filteredItems, JsonRequestBehavior.AllowGet);


        }

        #endregion

       

        #region Attendance Report

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DailyAttendenceReport(FormCollection form)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                ReportModel objReportModel = new ReportModel();

                objReportModel.FromDate = form["FromDate"];
                objReportModel.ToDate = form["ToDate"];
                objReportModel.UnitId = form["UnitId"];
                objReportModel.DepartmentId = form["DepartmentId"];
                objReportModel.SectionId = form["SectionId"];
                objReportModel.SubSectionId = form["SubSectionId"];
                objReportModel.ReportType = form["ReportType"];
                objReportModel.EmployeeId = form["EmployeeId"];

                if (objReportModel.EmployeeId == null)
                {
                    objReportModel.EmployeeId = "";
                }


                if (objReportModel.UnitId == null)
                {
                    objReportModel.UnitId = "";
                }

                if (objReportModel.DepartmentId == null)
                {
                    objReportModel.DepartmentId = "";
                }

                if (objReportModel.SectionId == null)
                {
                    objReportModel.SectionId = "";
                }

                if (objReportModel.SubSectionId == null)
                {
                    objReportModel.SubSectionId = "";
                }


                objReportModel.UpdateBy = strEmployeeId;
                objReportModel.HeadOfficeId = strHeadOfficeId;
                objReportModel.BranchOfficeId = strBranchOfficeId;


                string strPath = Path.Combine(Server.MapPath("~/Reports/rptDailyAttendanceSheet.rpt"));
                objReportDocument.Load(strPath);

                DataSet objDataSet = (objReportDAL.DailyAttendanceSheet(objReportModel));

                //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
                objReportDocument.Load(strPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport(objReportModel.ReportType, "DailyAttendance");

                return null;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LateReport(FormCollection form)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                ReportModel objReportModel = new ReportModel();

                objReportModel.FromDate = form["FromDate"];
                objReportModel.ToDate = form["ToDate"];
                objReportModel.UnitId = form["UnitId"];
                objReportModel.DepartmentId = form["DepartmentId"];
                objReportModel.SectionId = form["SectionId"];
                objReportModel.SubSectionId = form["SubSectionId"];
                objReportModel.ReportType = form["ReportType"];

                objReportModel.EmployeeId = form["EmployeeId"];
                
                if (objReportModel.EmployeeId == null)
                {
                    objReportModel.EmployeeId = "";
                }

                if (objReportModel.UnitId == null)
                {
                    objReportModel.UnitId = "";
                }

                if (objReportModel.DepartmentId == null)
                {
                    objReportModel.DepartmentId = "";
                }

                if (objReportModel.SectionId == null)
                {
                    objReportModel.SectionId = "";
                }

                if (objReportModel.SubSectionId == null)
                {
                    objReportModel.SubSectionId = "";
                }
                objReportModel.UpdateBy = strEmployeeId;
                objReportModel.HeadOfficeId = strHeadOfficeId;
                objReportModel.BranchOfficeId = strBranchOfficeId;


                string strPath = Path.Combine(Server.MapPath("~/Reports/rptDailyLateSheet.rpt"));
                objReportDocument.Load(strPath);

                DataSet objDataSet = (objReportDAL.DailyLateSheet(objReportModel));

                //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
                objReportDocument.Load(strPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport(objReportModel.ReportType, "LateAttendance");

                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AbsentReport(FormCollection form)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                ReportModel objReportModel = new ReportModel();

                objReportModel.FromDate = form["FromDate"];
                objReportModel.ToDate = form["ToDate"];
                objReportModel.UnitId = form["UnitId"];
                objReportModel.DepartmentId = form["DepartmentId"];
                objReportModel.SectionId = form["SectionId"];
                objReportModel.SubSectionId = form["SubSectionId"];
                objReportModel.ReportType = form["ReportType"];

                objReportModel.EmployeeId = form["EmployeeId"];
                objReportModel.EmployeeId = form["EmployeeId"];

                if (objReportModel.UnitId == null)
                {
                    objReportModel.UnitId = "";
                }

                if (objReportModel.DepartmentId == null)
                {
                    objReportModel.DepartmentId = "";
                }

                if (objReportModel.SectionId == null)
                {
                    objReportModel.SectionId = "";
                }

                if (objReportModel.SubSectionId == null)
                {
                    objReportModel.SubSectionId = "";
                }


                objReportModel.UpdateBy = strEmployeeId;
                objReportModel.HeadOfficeId = strHeadOfficeId;
                objReportModel.BranchOfficeId = strBranchOfficeId;


                string strPath = Path.Combine(Server.MapPath("~/Reports/rptDailyAbsentSheet.rpt"));
                objReportDocument.Load(strPath);

                DataSet objDataSet = (objReportDAL.DailyAbsentSheet(objReportModel));

                //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
                objReportDocument.Load(strPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport(objReportModel.ReportType, "LateAttendance");

                return null;
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MissingAttendanceReport(FormCollection form)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                ReportModel objReportModel = new ReportModel();

                objReportModel.FromDate = form["FromDate"];
                objReportModel.ToDate = form["ToDate"];
                objReportModel.UnitId = form["UnitId"];
                objReportModel.DepartmentId = form["DepartmentId"];
                objReportModel.SectionId = form["SectionId"];
                objReportModel.SubSectionId = form["SubSectionId"];
                objReportModel.ReportType = form["ReportType"];

                objReportModel.EmployeeId = form["EmployeeId"];
              
                if (objReportModel.EmployeeId == null)
                {
                    objReportModel.EmployeeId = "";
                }

                if (objReportModel.UnitId == null)
                {
                    objReportModel.UnitId = "";
                }

                if (objReportModel.DepartmentId == null)
                {
                    objReportModel.DepartmentId = "";
                }

                if (objReportModel.SectionId == null)
                {
                    objReportModel.SectionId = "";
                }

                if (objReportModel.SubSectionId == null)
                {
                    objReportModel.SubSectionId = "";
                }

                objReportModel.UpdateBy = strEmployeeId;
                objReportModel.HeadOfficeId = strHeadOfficeId;
                objReportModel.BranchOfficeId = strBranchOfficeId;


                string strPath = Path.Combine(Server.MapPath("~/Reports/rptDailyAttendanceMissingSheet.rpt"));
                objReportDocument.Load(strPath);

                DataSet objDataSet = (objReportDAL.DailyAttendanceMissingSheet(objReportModel));

                //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
                objReportDocument.Load(strPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport(objReportModel.ReportType, "LateAttendance");

                return null;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndividualAttendanceReport(FormCollection form)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                ReportModel objReportModel = new ReportModel();

                objReportModel.FromDate = form["FromDate"];
                objReportModel.ToDate = form["ToDate"];
                objReportModel.UnitId = form["UnitId"];
                objReportModel.DepartmentId = form["DepartmentId"];
                objReportModel.SectionId = form["SectionId"];
                objReportModel.SubSectionId = form["SubSectionId"];
                objReportModel.ReportType = form["ReportType"];

                objReportModel.EmployeeId = form["EmployeeId"];
                objReportModel.EmployeeId = form["EmployeeId"];
                if (objReportModel.EmployeeId == "")
                {
                    objReportModel.EmployeeId = null;
                }
                objReportModel.UpdateBy = strEmployeeId;
                objReportModel.HeadOfficeId = strHeadOfficeId;
                objReportModel.BranchOfficeId = strBranchOfficeId;


                string strPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
                objReportDocument.Load(strPath);

                DataSet objDataSet = (objReportDAL.IndividualAttendanceSheetForManualAttendance(objReportModel));

                //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
                objReportDocument.Load(strPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport(objReportModel.ReportType, "LateAttendance");

                return null;
            }
        }
        #endregion

        #region Duty Roaster



        public List<DutyRoasterModel> RoasterListData(DataTable dt)
        {
            List<DutyRoasterModel> roasterDataBundle = new List<DutyRoasterModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DutyRoasterModel objRoasterModel = new DutyRoasterModel();

                objRoasterModel.EmployeeId = dt.Rows[i]["EMPLOYEE_ID"].ToString();

                objRoasterModel.EmployeeName = dt.Rows[i]["EMPLOYEE_NAME"].ToString();

                objRoasterModel.JoiningDate = dt.Rows[i]["JOINING_DATE"].ToString();

                objRoasterModel.DesignationName = dt.Rows[i]["DESIGNATION_NAME"].ToString();

                objRoasterModel.Departmentname = dt.Rows[i]["DEPARTMENT_NAME"].ToString();

                objRoasterModel.DayName = dt.Rows[i]["day_name"].ToString();

                objRoasterModel.Date = dt.Rows[i]["log_date"].ToString();

                objRoasterModel.InTime = dt.Rows[i]["FIRST_IN_TIME"].ToString();

                objRoasterModel.OutTime = dt.Rows[i]["LAST_OUT_TIME"].ToString();

                objRoasterModel.WeeklyHolidayId = dt.Rows[i]["weekly_holiday_id"].ToString();

                objRoasterModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                roasterDataBundle.Add(objRoasterModel);
            }

            return roasterDataBundle;
        }

        public ActionResult GetDutyRoasterRecord()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                DutyRoasterModel objDutyRoasterModel = new DutyRoasterModel();

                objDutyRoasterModel.UpdateBy = strEmployeeId;
                objDutyRoasterModel.HeadOfficeId = strHeadOfficeId;
                objDutyRoasterModel.BranchOfficeId = strBranchOfficeId;

                if (TempData.ContainsKey("DutyRoaster") && (int)TempData["DutyRoaster"] == 1)
                {
                    objDutyRoasterModel.EmployeeId = Convert.ToString(TempData["EmployeeIdForRoaster"]);
                    objDutyRoasterModel.FromDate = Convert.ToString(TempData["FromDateForRoaster"]);
                    objDutyRoasterModel.ToDate = Convert.ToString(TempData["ToDateForRoaster"]);


                    DataTable dt = objEmployeeDAL.GetDutyRoasterRecord(objDutyRoasterModel);
                    objDutyRoasterModel.ListDutyRoaster = RoasterListData(dt);
                }
                else
                {
                    objDutyRoasterModel.ListDutyRoaster = new List<DutyRoasterModel>();
                }

                ViewBag.FirstDate = objLookUpDAL.getFirstLastDay().FirstDate;
                ViewBag.LastDate = objLookUpDAL.getFirstLastDay().LastDate;

                ViewBag.WeeklyHolidayDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetWeeklyHolidayDDList(), "WEEKLY_HOLIDAY_ID", "WEEKLY_HOLIDAY_NAME");

                return View(objDutyRoasterModel);
            }
        }

        [HttpPost]
        public ActionResult GetDutyRoasterRecord(DutyRoasterModel objRoasterModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                string strDBMsg = "";
                objRoasterModel.UpdateBy = strEmployeeId;
                objRoasterModel.HeadOfficeId = strHeadOfficeId;
                objRoasterModel.BranchOfficeId = strBranchOfficeId;

                DataTable dt = objEmployeeDAL.GetDutyRoasterRecord(objRoasterModel);
                objRoasterModel.ListDutyRoaster = RoasterListData(dt);

                ViewBag.FirstDate = objLookUpDAL.getFirstLastDay().FirstDate;
                ViewBag.LastDate = objLookUpDAL.getFirstLastDay().LastDate;

                ViewBag.WeeklyHolidayDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetWeeklyHolidayDDList(), "WEEKLY_HOLIDAY_ID", "WEEKLY_HOLIDAY_NAME");
                return View(objRoasterModel);
            }
        }

        [HttpPost]
        public ActionResult SaveDutyRoaster(DutyRoasterModel objRoasterModel)  //string duty, string emp
        {
            LoadSession();

            string strDBMsg = "";

            TempData["EmployeeIdForRoaster"] = objRoasterModel.EmployeeId;
            TempData["FromDateForRoaster"] = objRoasterModel.FromDate;
            TempData["ToDateForRoaster"] = objRoasterModel.ToDate;

            var dutyRoaster = new JavaScriptSerializer().Deserialize<List<DutyRoasterModel>>(objRoasterModel.RoasterSave);

            foreach (var roaster in dutyRoaster)
            {
                //DutyRoasterModel objRoasterModel = new DutyRoasterModel();

                objRoasterModel.UpdateBy = strEmployeeId;
                objRoasterModel.HeadOfficeId = strHeadOfficeId;
                objRoasterModel.BranchOfficeId = strBranchOfficeId;

                objRoasterModel.EmployeeId = objRoasterModel.EmployeeId;
                objRoasterModel.LogDate = roaster.LogDate;
                objRoasterModel.InTime = roaster.InTime;
                objRoasterModel.OutTime = roaster.OutTime;
                objRoasterModel.WeeklyHolidayId = roaster.WeeklyHolidayId;

                strDBMsg = objEmployeeDAL.SaveDutyRoaster(objRoasterModel);
            }
            //
            TempData["OperationMessage"] = strDBMsg;
            TempData["DutyRoaster"] = 1;

            return RedirectToAction("GetDutyRoasterRecord");
        }

        #endregion

       
        #region Employee Data Correction

        [HttpGet]
        public ActionResult EmployeeDataCorrection()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                EmployeeDataCorrectionModel objEmployeeDataCorrectionModel = new EmployeeDataCorrectionModel();

                objEmployeeDataCorrectionModel.UpdateBy = strEmployeeId;
                objEmployeeDataCorrectionModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeDataCorrectionModel.BranchOfficeId = strBranchOfficeId;

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

              
                objEmployeeDataCorrectionModel.EmployeeDataCorrectionList = new List<EmployeeDataCorrectionModel>();

                return View(objEmployeeDataCorrectionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeDataCorrection(EmployeeDataCorrectionModel objEmployeeDataCorrectionModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objEmployeeDataCorrectionModel.UpdateBy = strEmployeeId;
                objEmployeeDataCorrectionModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeDataCorrectionModel.BranchOfficeId = strBranchOfficeId;


                if (objEmployeeDataCorrectionModel.EmployeeDataCorrectionList != null)
                {
                    objEmployeeDataCorrectionModel.EmployeeDataCorrectionList.RemoveAll(x => !x.IsChecked);
                    if(objEmployeeDataCorrectionModel.EmployeeDataCorrectionList.Any())
                    {
                        string vMessage = objEmployeeDAL.UpdateEmployeeData(objEmployeeDataCorrectionModel);
                        TempData["OperationMessage"] = vMessage;
                    }
                    
                
                }


                objEmployeeDataCorrectionModel.Status = objEmployeeDataCorrectionModel.Status.Contains("false") ? "Y" : "N";


                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objEmployeeDataCorrectionModel.UnitId,
                strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objEmployeeDataCorrectionModel.DepartmentId);

                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objEmployeeDataCorrectionModel.DepartmentId,
                    strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objEmployeeDataCorrectionModel.SectionId);

                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objEmployeeDataCorrectionModel.SectionId,
                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objEmployeeDataCorrectionModel.SubSectionId);


                ViewBag.EmployeeTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetEmployeeTypeDDList(strHeadOfficeId, strBranchOfficeId), "EMPLOYEE_TYPE_ID", "EMPLOYEE_TYPE_NAME");
                ViewBag.WeeklyHolidayDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetWeeklyHolidayDDList(), "WEEKLY_HOLIDAY_ID", "WEEKLY_HOLIDAY_NAME");
                ViewBag.DesignationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDesignationDDList(strHeadOfficeId, strBranchOfficeId), "DESIGNATION_ID", "DESIGNATION_NAME");






                objEmployeeDataCorrectionModel.EmployeeDataCorrectionList = objEmployeeDAL.GetAllEmployeesForDataCorrection(objEmployeeDataCorrectionModel);
                return View(objEmployeeDataCorrectionModel);
            }
        }

        #endregion

        #region Employee ID Card Process


        //mezba 2
        public ActionResult EmployeeIdCard(EmployeeIdCardModel objEmployeeIdCardModel, string searchActiveYN)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {

                LoadSession();

                objEmployeeIdCardModel.UpdateBy = strEmployeeId;
                objEmployeeIdCardModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeIdCardModel.BranchOfficeId = strBranchOfficeId;

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
               
                if (objEmployeeIdCardModel.UnitId != null)
                {
                    if (objEmployeeIdCardModel.DepartmentId != null) //check dept id is selected or not,  if not then bypass to else
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDList(strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME");

                        if (objEmployeeIdCardModel.SectionId != null) //check section id is selected or not, if not then bypass to else 
                        {
                            ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDList(strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME");

                            if (objEmployeeIdCardModel.SubSectionId != null)//check sub-section id is selected or not, if not then bypass to else 
                            {
                                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDList(strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME");
                            }
                            else
                            {
                                ViewBag.SubSectionDDList = null;
                            }

                        }
                        else
                        {
                            ViewBag.SectionDDList = null;
                        }

                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }

                } // end filtering and checking dropdown----


                if (objEmployeeIdCardModel.SearchBy == "1")
                {
                    DataTable dt = objEmployeeDAL.LoadEmployeeDataForIdCard(objEmployeeIdCardModel);
                    objEmployeeIdCardModel.EmployeeList = EmployeeListDataForIdCard(dt);
                }


                return View(objEmployeeIdCardModel);
            }
        }

        //mezba 3
        public List<EmployeeIdCardModel> EmployeeListDataForIdCard(DataTable dt)
        {
            List<EmployeeIdCardModel> EmployeeDataBundleForIdCard = new List<EmployeeIdCardModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EmployeeIdCardModel objEmployeeIdCardModel = new EmployeeIdCardModel();

                objEmployeeIdCardModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objEmployeeIdCardModel.EmployeeId = dt.Rows[i]["EMPLOYEE_ID"].ToString();
                objEmployeeIdCardModel.EmployeeName = dt.Rows[i]["EMPLOYEE_NAME"].ToString();
                objEmployeeIdCardModel.DateOfBirth = dt.Rows[i]["DATE_OF_BIRTH"].ToString();
                objEmployeeIdCardModel.JoiningDate = dt.Rows[i]["JOINING_DATE"].ToString();
                objEmployeeIdCardModel.JobConfirmationDate = dt.Rows[i]["JOB_CONFIRMATION_DATE"].ToString();
                objEmployeeIdCardModel.PresentDesignationName = dt.Rows[i]["DESIGNATION_NAME"].ToString();
                objEmployeeIdCardModel.DepartmentName = dt.Rows[i]["DEPARTMENT_NAME"].ToString();
                objEmployeeIdCardModel.UnitName = dt.Rows[i]["UNIT_NAME"].ToString();
                objEmployeeIdCardModel.SectionName = dt.Rows[i]["SECTION_NAME"].ToString();
                objEmployeeIdCardModel.SubSectionName = dt.Rows[i]["SUB_SECTION_NAME"].ToString();
                objEmployeeIdCardModel.GradeId = dt.Rows[i]["GRADE_NO"].ToString();
                objEmployeeIdCardModel.Status = dt.Rows[i]["ACTIVE_STATUS"].ToString();

                if (dt.Rows[i]["EMPLOYEE_IMAGE_NAME"].ToString() != "")
                {
                    objEmployeeIdCardModel.ImageFileByte = (byte[])dt.Rows[i]["EMPLOYEE_IMAGE_SIZE"];
                    objEmployeeIdCardModel.ImageFileNameBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(objEmployeeIdCardModel.ImageFileByte);
                }

                EmployeeDataBundleForIdCard.Add(objEmployeeIdCardModel);
            }
            return EmployeeDataBundleForIdCard;
        }

        [HttpPost]
        public ActionResult EmployeeIdCardProcess(EmployeeModel objEmployeeModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                if (objEmployeeModel.EmployeeList != null)
                {
                    objEmployeeModel.EmployeeList.RemoveAll(x => !x.IsChecked);

                    List<EmployeeModel> employeeList = objEmployeeModel.EmployeeList;
                }


                LoadDropDownList();
                ReportModel objReportModel = new ReportModel();

                objEmployeeModel.UpdateBy = strEmployeeId;
                objEmployeeModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeModel.BranchOfficeId = strBranchOfficeId;

                objReportModel.UpdateBy = strEmployeeId;
                objReportModel.HeadOfficeId = strHeadOfficeId;
                objReportModel.BranchOfficeId = strBranchOfficeId;


                objEmployeeDAL.DeleteIDCard(objEmployeeModel);
                objEmployeeDAL.ProcessIDCard(objEmployeeModel);


                string strPath = Path.Combine(Server.MapPath("~/Reports/rptIDCard.rpt"));
                objReportDocument.Load(strPath);

                DataSet objDataSet = (objReportDAL.DisplayIdCard(objReportModel));


                objReportDocument.Load(strPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");
                string ReportType = "PDF";
                ShowReport(ReportType, "IdCard");

                return View();

            }
        }

        #endregion

        #region Employee Search

        // encrypted method---
        public string EncryptBp(string strGrossSalary)
        {

            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(strGrossSalary);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    strGrossSalary = Convert.ToBase64String(ms.ToArray());
                }
            }


            return strGrossSalary;

        }


        //decrypted method
        public string DecryptBP(string strGrossSalary)
        {

            string EncryptionKey = "MAKV2SPBNI99212";
            strGrossSalary = strGrossSalary.Replace(" ", "+");


            byte[] cipherBytes = Convert.FromBase64String(strGrossSalary);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    strGrossSalary = Encoding.Unicode.GetString(ms.ToArray());
                }
            }


            return strGrossSalary;



        }



        public ActionResult EmployeeSearch(EmployeeModel objEmployeeModel, HttpPostedFileBase Image, HttpPostedFileBase Signature, HttpPostedFileBase CV, HttpPostedFileBase[] ExperienceCertificate, HttpPostedFileBase[] ClearanceCertificate, HttpPostedFileBase[] EducationCertificate, string SearchActiveYN, string pEmployeeId = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                ModelState.Clear();
                LoadSession();
                LoadDropDownList();


                objEmployeeModel.UpdateBy = strEmployeeId;
                objEmployeeModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeModel.BranchOfficeId = strBranchOfficeId;

                if (objEmployeeModel.SearchBy == "1")
                {

                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objEmployeeModel.SearchUnitId,
                    strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objEmployeeModel.SearchDepartmentId);

                    ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objEmployeeModel.SearchDepartmentId,
                        strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objEmployeeModel.SearchSectionId);

                    ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objEmployeeModel.SearchSectionId,
                        strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objEmployeeModel.SearchSubSectionId);

                    objEmployeeModel.SearchInactiveYN = SearchActiveYN.Contains("true") ? "N" : "Y";
                    DataTable dt = objEmployeeDAL.LoadEmployeeData(objEmployeeModel);
                    ViewBag.EmployeeList = EmployeeListData(dt);
                }


                if (!string.IsNullOrWhiteSpace(pEmployeeId))
                {

                    objEmployeeModel.EmployeeId = pEmployeeId;



                    objEmployeeModel = objEmployeeDAL.SearchEmployeeInformation(objEmployeeModel);

                    string strPermissionYn = objEmployeeDAL.PermissionYn(strEmployeeId, strHeadOfficeId, strBranchOfficeId);
                    if (strPermissionYn == "" || strPermissionYn == null)
                    {
                        strPermissionYn = "N";

                    }

                    if (strPermissionYn != "Y")
                    {
                        string strJoiningSalary = objEmployeeModel.JoiningSalary;
                        string strGrossSalary = objEmployeeModel.GrossSalary;
                        string strFirstSalary = objEmployeeModel.FirstSalary;

                        if (strJoiningSalary == null)
                        {
                            strJoiningSalary = "0";

                        }

                        if (strGrossSalary == null)
                        {
                            strGrossSalary = "0";

                        }

                        if (strFirstSalary == null)
                        {
                            strFirstSalary = "0";

                        }
                        objEmployeeModel.JoiningSalary = EncryptBp(strJoiningSalary);
                        objEmployeeModel.GrossSalary = EncryptBp(strGrossSalary);
                        objEmployeeModel.FirstSalary = EncryptBp(strFirstSalary);
                    }

                    objEmployeeModel.SearchInactiveYN = "Y";
                    DataTable dt = objEmployeeDAL.LoadEmployeeData(objEmployeeModel);
                    ViewBag.EmployeeList = EmployeeListData(dt);


                    objEmployeeModel = objEmployeeDAL.SearchEmployeeInformation(objEmployeeModel);
                    objEmployeeModel = objEmployeeDAL.SearchEmployeePhoto(objEmployeeModel);
                    objEmployeeModel = objEmployeeDAL.SearchEmployeeSignature(objEmployeeModel);
                    objEmployeeModel = objEmployeeDAL.SearchEmployeeCv(objEmployeeModel);


                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objEmployeeModel.SearchUnitId,
                    strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objEmployeeModel.SearchDepartmentId);

                    ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objEmployeeModel.SearchDepartmentId,
                        strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objEmployeeModel.SearchSectionId);

                    ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objEmployeeModel.SearchSectionId,
                        strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objEmployeeModel.SearchSubSectionId);


                    DataTable dt1 = objEmployeeDAL.LoadEmployePreviousJobData(objEmployeeModel);
                    DataTable dt2 = objEmployeeDAL.LoadEmployeEducationData(objEmployeeModel);

                    if (dt1.Rows.Count > 0)
                    {
                        ViewBag.EmployePreviousJobData = EmployePreviousJobData(dt1);
                    }
                    if (dt2.Rows.Count > 0)
                    {
                        ViewBag.EmployeEducationData = EmployeEducationData(dt2);
                    }


                }

                if (Request.Form["btnUpdate"] != null)
                {


                    string strDBMsg = "";

                    objEmployeeModel.ActiveYN = objEmployeeModel.ActiveYN.Contains("true") ? "Y" : "N";

                    if (ModelState.IsValid)
                    {
                        strDBMsg = objEmployeeDAL.SaveEmployeeInformation(objEmployeeModel);
                        TempData["OperationMessage"] = strDBMsg;

                        if (Image != null)
                        {
                            String ImageExtension = Path.GetExtension(Image.FileName).ToUpper();
                            if (ImageExtension == ".JPG" || ImageExtension == ".JPEG" || ImageExtension == ".PNG")
                            {
                                Stream str = Image.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                                string ImageSize = Convert.ToBase64String(FileDet);

                                objEmployeeModel.ImageFileName = Image.FileName;
                                objEmployeeModel.ImageFileSize = ImageSize;
                                objEmployeeModel.ImageFileExtension = ImageExtension;
                                objEmployeeDAL.SaveEmployeeImage(objEmployeeModel);
                            }
                        }

                        if (Signature != null)
                        {
                            String SignatureExtension = Path.GetExtension(Signature.FileName).ToUpper();
                            if (SignatureExtension == ".JPG" || SignatureExtension == ".JPEG" || SignatureExtension == ".PNG")
                            {
                                Stream str = Signature.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                                string SignatureSize = Convert.ToBase64String(FileDet);

                                objEmployeeModel.SignatureFileName = Signature.FileName;
                                objEmployeeModel.SignatureFileSize = SignatureSize;
                                objEmployeeModel.SignatureFileExtension = SignatureExtension;
                                objEmployeeDAL.SaveEmployeeSignature(objEmployeeModel);
                            }
                        }


                        if (CV != null)
                        {
                            String CVExtension = Path.GetExtension(CV.FileName).ToUpper();
                            if (CVExtension == ".DOC" || CVExtension == ".DOCX" || CVExtension == ".PDF")
                            {
                                Stream str = CV.InputStream;
                                BinaryReader Br = new BinaryReader(str);
                                Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                                string CVSize = Convert.ToBase64String(FileDet);

                                objEmployeeModel.CVFileName = CV.FileName;
                                objEmployeeModel.CVFileSize = CVSize;
                                objEmployeeModel.CVFileExtension = CVExtension;
                                objEmployeeDAL.SaveEmployeeCv(objEmployeeModel);
                            }
                        }

                        objEmployeeDAL.SaveEmployeePreviousJobInformation(objEmployeeModel);
                        objEmployeeDAL.SaveEmployeeEducationInformation(objEmployeeModel);

                        for (int i = 0; i < ExperienceCertificate.Count(); i++)
                        {
                            if (ExperienceCertificate[i] != null)
                            {

                                String ExperienceCertificateExtension = Path.GetExtension(ExperienceCertificate[i].FileName).ToUpper();
                                if (ExperienceCertificateExtension == ".PDF")
                                {
                                    Stream str = ExperienceCertificate[i].InputStream;
                                    BinaryReader Br = new BinaryReader(str);
                                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                                    string ExperienceCertificateSize = Convert.ToBase64String(FileDet);

                                    objEmployeeModel.ExpCertifiateFileName = ExperienceCertificate[i].FileName;
                                    objEmployeeModel.ExpCertificateFileSize = ExperienceCertificateSize;
                                    objEmployeeModel.ExpCertificateFileExtension = ExperienceCertificateExtension;
                                    objEmployeeModel.PrevJobResignDate = objEmployeeModel.PreviousJobResignDate[i];
                                    objEmployeeDAL.SaveEmployeeExperienceCertificate(objEmployeeModel);
                                }

                            }

                        }

                        for (int i = 0; i < ClearanceCertificate.Count(); i++)
                        {
                            if (ClearanceCertificate[i] != null)
                            {

                                String ClearanceCertificateExtension = Path.GetExtension(ClearanceCertificate[i].FileName).ToUpper();
                                if (ClearanceCertificateExtension == ".PDF")
                                {
                                    Stream str = ClearanceCertificate[i].InputStream;
                                    BinaryReader Br = new BinaryReader(str);
                                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                                    string ClearanceCertificateSize = Convert.ToBase64String(FileDet);

                                    objEmployeeModel.ClrCertifiateFileName = ClearanceCertificate[i].FileName;
                                    objEmployeeModel.ClrCertificateFileSize = ClearanceCertificateSize;
                                    objEmployeeModel.ClrCertificateFileExtension = ClearanceCertificateExtension;
                                    objEmployeeModel.PrevJobResignDate = objEmployeeModel.PreviousJobResignDate[i];
                                    objEmployeeDAL.SaveEmployeeClearenceCertificate(objEmployeeModel);
                                }

                            }

                        }

                        for (int i = 0; i < EducationCertificate.Count(); i++)
                        {
                            if (EducationCertificate[i] != null)
                            {

                                String EducationCertificateExtension = Path.GetExtension(EducationCertificate[i].FileName).ToUpper();
                                if (EducationCertificateExtension == ".PDF")
                                {
                                    Stream str = EducationCertificate[i].InputStream;
                                    BinaryReader Br = new BinaryReader(str);
                                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                                    string EducationCertificateSize = Convert.ToBase64String(FileDet);

                                    objEmployeeModel.CertifiateFileName = EducationCertificate[i].FileName;
                                    objEmployeeModel.CertificateFileSize = EducationCertificateSize;
                                    objEmployeeModel.CertificateFileExtension = EducationCertificateExtension;
                                    objEmployeeModel.PassingYear = objEmployeeModel.Year[i];
                                    objEmployeeDAL.SaveEmployeeEducationCertificate(objEmployeeModel);
                                }

                            }

                        }



                        objEmployeeModel = objEmployeeDAL.SearchEmployeeInformation(objEmployeeModel);
                        objEmployeeModel = objEmployeeDAL.SearchEmployeePhoto(objEmployeeModel);
                        objEmployeeModel = objEmployeeDAL.SearchEmployeeSignature(objEmployeeModel);
                        objEmployeeModel = objEmployeeDAL.SearchEmployeeCv(objEmployeeModel);

                        DataTable dt1 = objEmployeeDAL.LoadEmployePreviousJobData(objEmployeeModel);
                        DataTable dt2 = objEmployeeDAL.LoadEmployeEducationData(objEmployeeModel);

                        if (dt1.Rows.Count > 0)
                        {
                            ViewBag.EmployePreviousJobData = EmployePreviousJobData(dt1);
                        }

                        if (dt2.Rows.Count > 0)
                        {
                            ViewBag.EmployeEducationData = EmployeEducationData(dt2);
                        }


                        objEmployeeModel.SearchInactiveYN = "Y";
                        DataTable dt = objEmployeeDAL.LoadEmployeeData(objEmployeeModel);
                        ViewBag.EmployeeList = EmployeeListData(dt);

                    }

                }


                return View(objEmployeeModel);
            }
        }

        /*public ActionResult EmployeeSearch(EmployeeModel objEmployeeModel, string SearchActiveYN, string pEmployeeId = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                ModelState.Clear();
                LoadSession();
                LoadDropDownList();

                objEmployeeModel.UpdateBy = strEmployeeId;
                objEmployeeModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeModel.BranchOfficeId = strBranchOfficeId;

                if (objEmployeeModel.SearchBy == "1")
                {
                    objEmployeeModel.SearchInactiveYN = SearchActiveYN.Contains("true") ? "N" : "Y";
                    DataTable dt = objEmployeeDAL.LoadEmployeeData(objEmployeeModel);
                    ViewBag.EmployeeList = EmployeeListData(dt);
                }


                if (!string.IsNullOrWhiteSpace(pEmployeeId))
                {
                    objEmployeeModel.EmployeeId = pEmployeeId;

                   

                    objEmployeeModel = objEmployeeDAL.SearchEmployeeInformation(objEmployeeModel);

                    string strPermissionYn = objEmployeeDAL.PermissionYn(strEmployeeId, strHeadOfficeId, strBranchOfficeId);
                    if (strPermissionYn == "" || strPermissionYn == null)
                    {
                        strPermissionYn = "N";

                    }

                    if (strPermissionYn != "Y")
                    {
                        string strJoiningSalary = objEmployeeModel.JoiningSalary;
                        string strGrossSalary = objEmployeeModel.GrossSalary;
                        string strFirstSalary = objEmployeeModel.FirstSalary;

                        if (strJoiningSalary == null)
                        {
                            strJoiningSalary = "0";

                        }

                        if (strGrossSalary == null)
                        {
                            strGrossSalary = "0";

                        }

                        if (strFirstSalary == null)
                        {
                            strFirstSalary = "0";

                        }
                        objEmployeeModel.JoiningSalary = EncryptBp(strJoiningSalary);
                        objEmployeeModel.GrossSalary = EncryptBp(strGrossSalary);
                        objEmployeeModel.FirstSalary = EncryptBp(strFirstSalary);
                    }



                    objEmployeeModel = objEmployeeDAL.SearchEmployeePhoto(objEmployeeModel);
                    objEmployeeModel = objEmployeeDAL.SearchEmployeeSignature(objEmployeeModel);
                    objEmployeeModel = objEmployeeDAL.SearchEmployeeCv(objEmployeeModel);

                    DataTable dt1 = objEmployeeDAL.LoadEmployePreviousJobData(objEmployeeModel);
                    DataTable dt2 = objEmployeeDAL.LoadEmployeEducationData(objEmployeeModel);

                    if (dt1.Rows.Count > 0)
                    {
                        ViewBag.EmployePreviousJobData = EmployePreviousJobData(dt1);
                    }
                    if (dt2.Rows.Count > 0)
                    {
                        ViewBag.EmployeEducationData = EmployeEducationData(dt2);
                    }

                    DataTable dt = objEmployeeDAL.LoadEmployeeData(objEmployeeModel);
                    ViewBag.EmployeeList = EmployeeListData(dt);

                }



                return View(objEmployeeModel);
            }
        }

        */
        #endregion

        #region Employee Image Download

        //mezba 5
        public ActionResult EmployeeImage(EmployeeImageModel objEmployeeImageModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {

                LoadSession();

                objEmployeeImageModel.UpdateBy = strEmployeeId;
                objEmployeeImageModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeImageModel.BranchOfficeId = strBranchOfficeId;

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                if (objEmployeeImageModel.UnitId != null)
                {
                    if (objEmployeeImageModel.DepartmentId != null) //check dept id is selected or not,  if not then bypass to else
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDList(strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME");

                        if (objEmployeeImageModel.SectionId != null) //check section id is selected or not, if not then bypass to else 
                        {
                            ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDList(strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME");

                            if (objEmployeeImageModel.SubSectionId != null)//check sub-section id is selected or not, if not then bypass to else 
                            {
                                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDList(strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME");
                            }
                            else
                            {
                                ViewBag.SubSectionDDList = null;
                            }

                        }
                        else
                        {
                            ViewBag.SectionDDList = null;
                        }

                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }

                } // end filtering and checking dropdown----


                if (objEmployeeImageModel.SearchBy == "1")
                {
                    objEmployeeImageModel.Status = objEmployeeImageModel.ActiveStatus.Contains("true") ? "N" : "Y";
                    DataTable dt = objEmployeeDAL.LoadEmployeeImageData(objEmployeeImageModel);
                    objEmployeeImageModel.EmployeeList = EmployeeListDataForImage(dt);
                }


                return View(objEmployeeImageModel);
            }
        }

        //mezba 6
        public List<EmployeeImageModel> EmployeeListDataForImage(DataTable dt)
        {
            List<EmployeeImageModel> EmployeeDataBundleForImage = new List<EmployeeImageModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EmployeeImageModel objEmployeeImageModel = new EmployeeImageModel();

                objEmployeeImageModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objEmployeeImageModel.EmployeeId = dt.Rows[i]["EMPLOYEE_ID"].ToString();
                objEmployeeImageModel.EmployeeName = dt.Rows[i]["EMPLOYEE_NAME"].ToString();
                objEmployeeImageModel.DateOfBirth = dt.Rows[i]["DATE_OF_BIRTH"].ToString();
                objEmployeeImageModel.JoiningDate = dt.Rows[i]["JOINING_DATE"].ToString();
                objEmployeeImageModel.JobConfirmationDate = dt.Rows[i]["JOB_CONFIRMATION_DATE"].ToString();
                objEmployeeImageModel.PresentDesignationName = dt.Rows[i]["DESIGNATION_NAME"].ToString();
                objEmployeeImageModel.DepartmentName = dt.Rows[i]["DEPARTMENT_NAME"].ToString();
                objEmployeeImageModel.UnitName = dt.Rows[i]["UNIT_NAME"].ToString();
                objEmployeeImageModel.SectionName = dt.Rows[i]["SECTION_NAME"].ToString();
                objEmployeeImageModel.SubSectionName = dt.Rows[i]["SUB_SECTION_NAME"].ToString();
                objEmployeeImageModel.GradeId = dt.Rows[i]["GRADE_NO"].ToString();
                objEmployeeImageModel.Status = dt.Rows[i]["ACTIVE_STATUS"].ToString();

                if (dt.Rows[i]["EMPLOYEE_IMAGE_NAME"].ToString() != "")
                {
                    objEmployeeImageModel.ImageFileByte = (byte[])dt.Rows[i]["EMPLOYEE_IMAGE_SIZE"];
                    objEmployeeImageModel.ImageFileNameBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(objEmployeeImageModel.ImageFileByte);
                }

                EmployeeDataBundleForImage.Add(objEmployeeImageModel);
            }
            return EmployeeDataBundleForImage;
        }



        #endregion

        #region Employee Job Confirmation List


        //mezba 7
        public ActionResult EmployeeJobConfirmationList(EmployeeJobConfirmationModel objEmployeeJobConfirmationModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {

                LoadSession();

                objEmployeeJobConfirmationModel.UpdateBy = strEmployeeId;
                objEmployeeJobConfirmationModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeJobConfirmationModel.BranchOfficeId = strBranchOfficeId;

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDList(strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME");
                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDList(strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME");
                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDList(strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME");



                DateTime now = DateTime.Now;

                string firstDayofMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToString("dd/MM/yyyy");
                string lastDayofMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1).ToString("dd/MM/yyyy");


                objEmployeeJobConfirmationModel.FromDate = objEmployeeJobConfirmationModel.FromDate ?? firstDayofMonth.ToString();
                objEmployeeJobConfirmationModel.ToDate = objEmployeeJobConfirmationModel.ToDate ?? lastDayofMonth.ToString();


                DataTable dt = objEmployeeDAL.LoadEmployeeJobConfirmationRecord(objEmployeeJobConfirmationModel);
                objEmployeeJobConfirmationModel.EmployeeList = EmployeeJobConfirmationListData(dt);


                if (Request.Form["viewButton"] != null)
                {
                    GenerateJobConfirmationReport(objEmployeeJobConfirmationModel);
                }


                return View(objEmployeeJobConfirmationModel);
            }
        }

        //mezba 8
        public List<EmployeeJobConfirmationModel> EmployeeJobConfirmationListData(DataTable dt)
        {
            List<EmployeeJobConfirmationModel> EmployeeJobConfirmationDataBundle = new List<EmployeeJobConfirmationModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EmployeeJobConfirmationModel objEmployeeJobConfirmationModel = new EmployeeJobConfirmationModel();

                objEmployeeJobConfirmationModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objEmployeeJobConfirmationModel.EmployeeId = dt.Rows[i]["EMPLOYEE_ID"].ToString();
                objEmployeeJobConfirmationModel.EmployeeName = dt.Rows[i]["EMPLOYEE_NAME"].ToString();
                objEmployeeJobConfirmationModel.DateOfBirth = dt.Rows[i]["DATE_OF_BIRTH"].ToString();
                objEmployeeJobConfirmationModel.JoiningDate = dt.Rows[i]["JOINING_DATE"].ToString();
                objEmployeeJobConfirmationModel.JobConfirmationDate = dt.Rows[i]["JOB_CONFIRMATION_DATE"].ToString();
                objEmployeeJobConfirmationModel.PresentDesignationName = dt.Rows[i]["JOINING_DESIGNATION_NAME"].ToString();
                objEmployeeJobConfirmationModel.DepartmentName = dt.Rows[i]["DEPARTMENT_NAME"].ToString();
                objEmployeeJobConfirmationModel.UnitName = dt.Rows[i]["UNIT_NAME"].ToString();
                objEmployeeJobConfirmationModel.SectionName = dt.Rows[i]["SECTION_NAME"].ToString();
                objEmployeeJobConfirmationModel.SubSectionName = dt.Rows[i]["SUB_SECTION_NAME"].ToString();
                objEmployeeJobConfirmationModel.GradeId = dt.Rows[i]["GRADE_NO"].ToString();
                objEmployeeJobConfirmationModel.Status = dt.Rows[i]["ACTIVE_YN"].ToString();

                if (dt.Rows[i]["EMPLOYEEE_PIC"].ToString() != "")
                {
                    objEmployeeJobConfirmationModel.ImageFileByte = (byte[])dt.Rows[i]["EMPLOYEEE_PIC"];
                    objEmployeeJobConfirmationModel.ImageFileNameBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(objEmployeeJobConfirmationModel.ImageFileByte);
                }

                EmployeeJobConfirmationDataBundle.Add(objEmployeeJobConfirmationModel);
            }
            return EmployeeJobConfirmationDataBundle;
        }

        //mezba 9
        private void GenerateJobConfirmationReport(EmployeeJobConfirmationModel objEmployeeJobConfirmationModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptEmployeeJobConfirmationDetails.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objEmployeeDAL.EmployeeJobConfirmationDetail(objEmployeeJobConfirmationModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeJobConfirmationModel.ReportType, "Job Confirmation");
        }


        public ActionResult EmployeeJobConfirmationListDisplay(EmployeeModel objEmployeeModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {

                LoadSession();
                


                objEmployeeModel.UpdateBy = strEmployeeId;
                objEmployeeModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeModel.BranchOfficeId = strBranchOfficeId;



                string strPath = Path.Combine(Server.MapPath("~/Reports/rptEmployeeJobConfirmationDetails.rpt"));
                objReportDocument.Load(strPath);

                DataSet objDataSet = (objReportDAL.EmployeeJobConfirmationDetail(objEmployeeModel));


                objReportDocument.Load(strPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");
                string ReportType = "PDF";
                ShowReport(ReportType, "IdCard");

                return View();
            }
        }
        #endregion

        #region Employee Resign Entry

        public ActionResult EmployeeResignEntry(string pEmployeeId, int page = 1, int pageSize = 50, string searchBy = "", string flag = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                EmployeeResignModel objEmployeeResignModel = new EmployeeResignModel();

                objEmployeeResignModel.UpdateBy = strEmployeeId;
                objEmployeeResignModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeResignModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(searchBy))
                {
                    objEmployeeResignModel.SearchBy = searchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = searchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objEmployeeResignModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pEmployeeId) && flag == "1")
                {
                    objEmployeeResignModel.EmployeeId = pEmployeeId;
                    objEmployeeResignModel = objEmployeeDAL.ResignEmployeePhoto(objEmployeeResignModel);
                    objEmployeeResignModel = objEmployeeDAL.LoadDataToMain(objEmployeeResignModel);

                    page = (int)TempData["GetActionPage"];
                    TempData.Keep("GetActionPage");
                }


                if (!string.IsNullOrWhiteSpace(pEmployeeId) && flag == "2")
                {
                    objEmployeeResignModel.EmployeeId = pEmployeeId;
                    objEmployeeResignModel = objEmployeeDAL.ResignEmployeePhoto(objEmployeeResignModel);
                    objEmployeeResignModel = objEmployeeDAL.ResignEmployeeInformation(objEmployeeResignModel);


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

                DataTable dt = objEmployeeDAL.LoadAllResignRecord(objEmployeeResignModel);
                var resignList = ResignListData(dt);
                ViewBag.ResignList = resignList.ToPagedList(page, pageSize);

                return View(objEmployeeResignModel);
            }
        }

        public ActionResult ClearEmployeeResign()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("EmployeeResignEntry");
        }

        
        public List<EmployeeResignModel> ResignListData(DataTable dt)
        {
            List<EmployeeResignModel> employeeResignDataBundle = new List<EmployeeResignModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EmployeeResignModel objEmployeeResignModel = new EmployeeResignModel();

                objEmployeeResignModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objEmployeeResignModel.EmployeeId = dt.Rows[i]["EMPLOYEE_ID"].ToString();
                objEmployeeResignModel.EmployeeName = dt.Rows[i]["EMPLOYEE_NAME"].ToString();
                objEmployeeResignModel.ResignDesignation = dt.Rows[i]["JOINING_DESIGNATION_NAME"].ToString();
                objEmployeeResignModel.JoiningDate = dt.Rows[i]["JOINING_DATE"].ToString();
                objEmployeeResignModel.ResignDate = dt.Rows[i]["resign_date"].ToString();
                objEmployeeResignModel.ResignCause = dt.Rows[i]["resign_cause"].ToString();
                objEmployeeResignModel.ResignRemarks = dt.Rows[i]["remarks"].ToString();

                employeeResignDataBundle.Add(objEmployeeResignModel);
            }
            return employeeResignDataBundle;
        }
        public ActionResult SaveEmployeeResignEntry(EmployeeResignModel objEmployeeResignModel)
        {
            LoadSession();
            string strDBMsg = "";

            objEmployeeResignModel.UpdateBy = strEmployeeId;
            objEmployeeResignModel.HeadOfficeId = strHeadOfficeId;
            objEmployeeResignModel.BranchOfficeId = strBranchOfficeId;


            if (ModelState.IsValid)
            {
                strDBMsg = objEmployeeDAL.SaveEmployeeEntryResign(objEmployeeResignModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            ModelState.Clear();
            return RedirectToAction("EmployeeResignEntry");
        }

        #endregion

        #region Team Leader Entry

        //mezba 10
        public ActionResult TeamLeaderEntry(TeamLeaderModel objTeamLeaderModel, int page = 1, int pageSize = 20)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                objTeamLeaderModel.UpdateBy = strEmployeeId;
                objTeamLeaderModel.HeadOfficeId = strHeadOfficeId;
                objTeamLeaderModel.BranchOfficeId = strBranchOfficeId;

                if (objTeamLeaderModel.UnitId != null)
                {
                    if (objTeamLeaderModel.DepartmentId != null) //check dept id is selected or not,  if not then bypass to else
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDList(strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME");

                        if (objTeamLeaderModel.SectionId != null) //check section id is selected or not, if not then bypass to else 
                        {
                            ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDList(strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME");

                            if (objTeamLeaderModel.SubSectionId != null)//check sub-section id is selected or not, if not then bypass to else 
                            {
                                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDList(strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME");
                            }
                            else
                            {
                                ViewBag.SubSectionDDList = null;
                            }

                        }
                        else
                        {
                            ViewBag.SectionDDList = null;
                        }

                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }

                } // end filtering and checking dropdown----



                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(objTeamLeaderModel.SearchBy))
                {

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = objTeamLeaderModel.SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objTeamLeaderModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }





                if (TempData.ContainsKey("SaveActionFlag") && (int)TempData["SaveActionFlag"] == 1)
                {
                    page = (int)TempData["SaveActionPage"];
                    TempData.Keep("SaveActionPage");
                }

                TempData["GetActionFlag"] = 1;
                TempData["GetActionPage"] = page;

                #endregion


                if (objTeamLeaderModel.Flag == "1")
                {


                    DataTable dt1 = objEmployeeDAL.LoadEmployeeDataForTeamLeader(objTeamLeaderModel);
                    objTeamLeaderModel.EmployeeList = EmployeeListForTeamLeaderData(dt1);
                }




                DataTable dt2 = objEmployeeDAL.LoadTeamLeaderRecord(objTeamLeaderModel);
                var teamLeader = TeamLeaderListData(dt2);
                ViewBag.TeamLeaderList = teamLeader.ToPagedList(page, pageSize);


                return View(objTeamLeaderModel);
            }
        }


        //mezba 11
        public List<TeamLeaderModel> EmployeeListForTeamLeaderData(DataTable dt1)
        {
            List<TeamLeaderModel> EmployeeDataBundleForTeamLeader = new List<TeamLeaderModel>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                TeamLeaderModel objTeamLeaderModel = new TeamLeaderModel();

                objTeamLeaderModel.SerialNumber = dt1.Rows[i]["SL"].ToString();
                objTeamLeaderModel.EmployeeId = dt1.Rows[i]["EMPLOYEE_ID"].ToString();
                objTeamLeaderModel.EmployeeName = dt1.Rows[i]["EMPLOYEE_NAME"].ToString();
                objTeamLeaderModel.DateOfBirth = dt1.Rows[i]["DATE_OF_BIRTH"].ToString();
                objTeamLeaderModel.JoiningDate = dt1.Rows[i]["JOINING_DATE"].ToString();
                objTeamLeaderModel.JobConfirmationDate = dt1.Rows[i]["JOB_CONFIRMATION_DATE"].ToString();
                objTeamLeaderModel.PresentDesignationName = dt1.Rows[i]["DESIGNATION_NAME"].ToString();
                objTeamLeaderModel.DepartmentName = dt1.Rows[i]["DEPARTMENT_NAME"].ToString();
                objTeamLeaderModel.UnitName = dt1.Rows[i]["UNIT_NAME"].ToString();
                objTeamLeaderModel.SectionName = dt1.Rows[i]["SECTION_NAME"].ToString();
                objTeamLeaderModel.SubSectionName = dt1.Rows[i]["SUB_SECTION_NAME"].ToString();
                objTeamLeaderModel.GradeId = dt1.Rows[i]["GRADE_NO"].ToString();
                objTeamLeaderModel.Status = dt1.Rows[i]["ACTIVE_STATUS"].ToString();

                if (dt1.Rows[i]["EMPLOYEE_IMAGE_NAME"].ToString() != "")
                {
                    objTeamLeaderModel.ImageFileByte = (byte[])dt1.Rows[i]["EMPLOYEE_IMAGE_SIZE"];
                    objTeamLeaderModel.ImageFileNameBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(objTeamLeaderModel.ImageFileByte);
                }

                EmployeeDataBundleForTeamLeader.Add(objTeamLeaderModel);
            }
            return EmployeeDataBundleForTeamLeader;
        }


        //mezba 12
        public List<TeamLeaderModel> TeamLeaderListData(DataTable dt2)
        {
            List<TeamLeaderModel> teamLeaderDataBundle = new List<TeamLeaderModel>();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                TeamLeaderModel objTeamLeaderModel = new TeamLeaderModel();

                objTeamLeaderModel.SerialNumber = dt2.Rows[i]["SL"].ToString();
                objTeamLeaderModel.EmployeeId = dt2.Rows[i]["EMPLOYEE_ID"].ToString();
                objTeamLeaderModel.EmployeeName = dt2.Rows[i]["EMPLOYEE_NAME"].ToString();
                objTeamLeaderModel.JoiningDate = dt2.Rows[i]["JOINING_DATE"].ToString();
                objTeamLeaderModel.PresentDesignationName = dt2.Rows[i]["DESIGNATION_NAME"].ToString();
                objTeamLeaderModel.UnitName = dt2.Rows[i]["UNIT_NAME"].ToString();
                objTeamLeaderModel.DepartmentName = dt2.Rows[i]["DEPARTMENT_NAME"].ToString();
                objTeamLeaderModel.SectionName = dt2.Rows[i]["SECTION_NAME"].ToString();
                objTeamLeaderModel.SubSectionName = dt2.Rows[i]["SUB_SECTION_NAME"].ToString();
                objTeamLeaderModel.Status = dt2.Rows[i]["ACTIVE_STATUS"].ToString();
                objTeamLeaderModel.ImageFileNameBase64 = dt2.Rows[i]["EMPLOYEE_IMAGE"].ToString();


                if (dt2.Rows[i]["EMPLOYEE_IMAGE"].ToString() != "")
                {
                    objTeamLeaderModel.ImageFileByte = (byte[])dt2.Rows[i]["EMPLOYEE_IMAGE"];
                    objTeamLeaderModel.ImageFileNameBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(objTeamLeaderModel.ImageFileByte);
                }

                teamLeaderDataBundle.Add(objTeamLeaderModel);
            }
            return teamLeaderDataBundle;
        }

        //mezba 13
        [HttpPost]
        public ActionResult TeamLeaderSave(TeamLeaderModel objTeamLeaderModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objTeamLeaderModel.UpdateBy = strEmployeeId;
                objTeamLeaderModel.HeadOfficeId = strHeadOfficeId;
                objTeamLeaderModel.BranchOfficeId = strBranchOfficeId;


                if (objTeamLeaderModel.EmployeeList != null)
                {
                    objTeamLeaderModel.EmployeeList.RemoveAll(x => !x.IsChecked);

                    List<TeamLeaderModel> employeeList = objTeamLeaderModel.EmployeeList;
                }

                string strDBMsg = "";
                strDBMsg = objEmployeeDAL.TeamLeaderEmployeeSave(objTeamLeaderModel);
                TempData["OperationMessage"] = strDBMsg;
                return RedirectToAction("TeamLeaderEntry");

            }
        }
        public ActionResult ClearTeamLeaderEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("TeamLeaderEntry");
        }
        #endregion

        #region Team Leader Hierarchy Entry

        //mezba 14
        public ActionResult TeamLeaderHierarchyEntry(TeamLeaderHierarchyModel objTeamLeaderHierarchyModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objTeamLeaderHierarchyModel.UpdateBy = strEmployeeId;
                objTeamLeaderHierarchyModel.HeadOfficeId = strHeadOfficeId;
                objTeamLeaderHierarchyModel.BranchOfficeId = strBranchOfficeId;

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDList(strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME");
                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDList(strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME");
                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDList(strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME");
                ViewBag.TeamLeaderDDList = UtilityClass.GetSelectListByDataTable(objEmployeeDAL.GetTeamLeaderName(objTeamLeaderHierarchyModel.HeadOfficeId, objTeamLeaderHierarchyModel.BranchOfficeId), "EMPLOYEE_ID", "EMPLOYEE_NAME");


                if (objTeamLeaderHierarchyModel.Flag == "1")
                {
                    DataTable dt1 = objEmployeeDAL.LoadEmployeeDataForTeamLeaderHierarchy(objTeamLeaderHierarchyModel);
                    objTeamLeaderHierarchyModel.EmployeeList = EmployeeListDataForTeamLeaderHierarchy(dt1);
                }


                if (objTeamLeaderHierarchyModel.Flag == "2")
                {
                    DataTable dt2 = objEmployeeDAL.ShowSubordinateEmpRecordForTeamHierarchy(objTeamLeaderHierarchyModel);
                    objTeamLeaderHierarchyModel.TeamLeaderSubordinateList = TeamLeaderHierarchyListData(dt2);
                }

                return View(objTeamLeaderHierarchyModel);
            }
        }


        //mezba 15
        public List<TeamLeaderHierarchyModel> EmployeeListDataForTeamLeaderHierarchy(DataTable dt1)
        {
            List<TeamLeaderHierarchyModel> EmployeeDataBundleForTeamLeaderHierarchy = new List<TeamLeaderHierarchyModel>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                TeamLeaderHierarchyModel objTeamLeaderHierarchyModel = new TeamLeaderHierarchyModel();

                objTeamLeaderHierarchyModel.SerialNumber = dt1.Rows[i]["SL"].ToString();
                objTeamLeaderHierarchyModel.EmployeeId = dt1.Rows[i]["EMPLOYEE_ID"].ToString();
                objTeamLeaderHierarchyModel.EmployeeName = dt1.Rows[i]["EMPLOYEE_NAME"].ToString();
                objTeamLeaderHierarchyModel.DateOfBirth = dt1.Rows[i]["DATE_OF_BIRTH"].ToString();
                objTeamLeaderHierarchyModel.JoiningDate = dt1.Rows[i]["JOINING_DATE"].ToString();
                objTeamLeaderHierarchyModel.JobConfirmationDate = dt1.Rows[i]["JOB_CONFIRMATION_DATE"].ToString();
                objTeamLeaderHierarchyModel.PresentDesignationName = dt1.Rows[i]["DESIGNATION_NAME"].ToString();
                objTeamLeaderHierarchyModel.DepartmentName = dt1.Rows[i]["DEPARTMENT_NAME"].ToString();
                objTeamLeaderHierarchyModel.UnitName = dt1.Rows[i]["UNIT_NAME"].ToString();
                objTeamLeaderHierarchyModel.SectionName = dt1.Rows[i]["SECTION_NAME"].ToString();
                objTeamLeaderHierarchyModel.SubSectionName = dt1.Rows[i]["SUB_SECTION_NAME"].ToString();
                objTeamLeaderHierarchyModel.GradeId = dt1.Rows[i]["GRADE_NO"].ToString();
                objTeamLeaderHierarchyModel.Status = dt1.Rows[i]["ACTIVE_STATUS"].ToString();

                if (dt1.Rows[i]["EMPLOYEE_IMAGE_NAME"].ToString() != "")
                {
                    objTeamLeaderHierarchyModel.ImageFileByte = (byte[])dt1.Rows[i]["EMPLOYEE_IMAGE_SIZE"];
                    objTeamLeaderHierarchyModel.ImageFileNameBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(objTeamLeaderHierarchyModel.ImageFileByte);
                }

                EmployeeDataBundleForTeamLeaderHierarchy.Add(objTeamLeaderHierarchyModel);
            }
            return EmployeeDataBundleForTeamLeaderHierarchy;
        }


        //mezba 16
        public List<TeamLeaderHierarchyModel> TeamLeaderHierarchyListData(DataTable dt2)
        {
            List<TeamLeaderHierarchyModel> teamLeaderHierarchyDataBundle = new List<TeamLeaderHierarchyModel>();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                TeamLeaderHierarchyModel objTeamLeaderHierarchyModel = new TeamLeaderHierarchyModel();

                objTeamLeaderHierarchyModel.SerialNumber = dt2.Rows[i]["SL"].ToString();
                objTeamLeaderHierarchyModel.EmployeeId = dt2.Rows[i]["EMPLOYEE_ID"].ToString();
                objTeamLeaderHierarchyModel.TeamLeaderName = dt2.Rows[i]["TEAM_LEADER_NAME"].ToString();
                objTeamLeaderHierarchyModel.SubordinateEmployeeName = dt2.Rows[i]["SUBORDINATE_EMPLOYEE_NAME"].ToString();
                objTeamLeaderHierarchyModel.SubordinateEmployeeId = dt2.Rows[i]["SUBORDINATE_EMPLOYEE_ID"].ToString();
                objTeamLeaderHierarchyModel.JoiningDate = dt2.Rows[i]["JOINING_DATE"].ToString();
                objTeamLeaderHierarchyModel.DesignationName = dt2.Rows[i]["DESIGNATION_NAME"].ToString();
                objTeamLeaderHierarchyModel.UnitName = dt2.Rows[i]["UNIT_NAME"].ToString();
                objTeamLeaderHierarchyModel.DepartmentName = dt2.Rows[i]["department_name"].ToString();
                objTeamLeaderHierarchyModel.SectionName = dt2.Rows[i]["SECTION_NAME"].ToString();
                objTeamLeaderHierarchyModel.SubSectionName = dt2.Rows[i]["SUB_SECTION_NAME"].ToString();
                objTeamLeaderHierarchyModel.ActiveStatus = dt2.Rows[i]["ACTIVE_STATUS"].ToString();



                if (dt2.Rows[i]["EMPLOYEE_IMAGE"].ToString() != "")
                {
                    objTeamLeaderHierarchyModel.EmployeeImageByte = (byte[])dt2.Rows[i]["EMPLOYEE_IMAGE"];
                    objTeamLeaderHierarchyModel.EmployeeImage = "data:image/jpeg;base64," + Convert.ToBase64String(objTeamLeaderHierarchyModel.EmployeeImageByte);
                }

                teamLeaderHierarchyDataBundle.Add(objTeamLeaderHierarchyModel);
            }
            return teamLeaderHierarchyDataBundle;
        }


        //mezba 17
        [HttpPost]
        public ActionResult TeamLeaderHierarchySave(TeamLeaderHierarchyModel objTeamLeaderHierarchyModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objTeamLeaderHierarchyModel.UpdateBy = strEmployeeId;
                objTeamLeaderHierarchyModel.HeadOfficeId = strHeadOfficeId;
                objTeamLeaderHierarchyModel.BranchOfficeId = strBranchOfficeId;


                if (objTeamLeaderHierarchyModel.EmployeeList != null)
                {
                    objTeamLeaderHierarchyModel.EmployeeList.RemoveAll(x => !x.IsChecked);

                    List<TeamLeaderHierarchyModel> employeeList = objTeamLeaderHierarchyModel.EmployeeList;
                }

                string strDBMsg = "";
                strDBMsg = objEmployeeDAL.TeamLeaderHierarchyEmployeeSave(objTeamLeaderHierarchyModel);
                TempData["OperationMessage"] = strDBMsg;
                return RedirectToAction("TeamLeaderHierarchyEntry");

            }
        }
        public ActionResult ClearTeamLeaderHierarchyEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("TeamLeaderHierarchyEntry");
        }
        #endregion

        #region Employee Movement Register Request 

        [HttpGet]
        public ActionResult EmployeeMovementRegisterRequest(string eId, string delId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                MovementRegisterModel objMovementRegisModel = new MovementRegisterModel
                {
                    EmployeeId = strEmployeeId,
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,
                    ToDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    FromDate = objLookUpDAL.getFirstLastDay().LastDate
                };

                ViewBag.MovementRegister = objEmployeeDAL.GetEmployeeDetailsMovementRegister(objMovementRegisModel);  //employee detail information

                /*objMovementRegisModel.MovementRegisterModelList*/
                List<MovementRegisterModel> list = objEmployeeDAL.GetMovementRegisterRecord(objMovementRegisModel);
                ViewBag.MovementRegisterList = list.ToPagedList(page, pageSize);  // movement register list


                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objMovementRegisModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objMovementRegisModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(eId))
                {
                    objMovementRegisModel.TranId = eId;
                    objMovementRegisModel = objEmployeeDAL.GetEmployeeDetailsMovementRegisterbyId(objMovementRegisModel);

                    page = (int)TempData["GetActionPage"];
                    TempData.Keep("GetActionPage");
                }
                string delMsg = "";
                if (!string.IsNullOrWhiteSpace(delId))
                {
                    objMovementRegisModel.TranId = delId;
                    delMsg = objEmployeeDAL.DeleteIndividualMovementRegister(objMovementRegisModel);

                    TempData["OperationMessage"] = delMsg;

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


                ViewBag.MovementTypeDdl = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMovementRegisterType(), "MOVEMENT_TYPE_ID", "MOVEMENT_TYPE_NAME");


                return View(objMovementRegisModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeMovementRegisterRequest(MovementRegisterModel objMovementReg)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objMovementReg.EmployeeId = strEmployeeId;

                objMovementReg.UpdateBy = strEmployeeId;
                objMovementReg.HeadOfficeId = strHeadOfficeId;
                objMovementReg.BranchOfficeId = strBranchOfficeId;
                objMovementReg.ToDate = objLookUpDAL.getFirstLastDay().FirstDate;
                objMovementReg.FromDate = objLookUpDAL.getFirstLastDay().LastDate;


                //List<MovementRegisterModel> movementReg = new JavaScriptSerializer().Deserialize<List<MovementRegisterModel>>(objMovementReg.HidMovementRegData);

                string strDBMsg = "";

                //foreach (MovementRegisterModel mmReg in movementReg)
                //{

                //    objMovementReg.UpdateBy = strEmployeeId;
                //    objMovementReg.HeadOfficeId = strHeadOfficeId;
                //    objMovementReg.BranchOfficeId = strBranchOfficeId;

                //    objMovementReg.EmployeeId = strEmployeeId;
                //    objMovementReg.LogDate = mmReg.LogDate;
                //    objMovementReg.MovementTypeId = mmReg.MovementTypeId;
                //    objMovementReg.LastOut = mmReg.LastOut;
                //    objMovementReg.FirstIn = mmReg.FirstIn;
                //    objMovementReg.Remarks = mmReg.Remarks;


                //}

                strDBMsg = objEmployeeDAL.SaveIndividualMovementRegister(objMovementReg);
                TempData["OperationMessage"] = strDBMsg;

                #region Pagination Search

                int page = (int)TempData["GetActionPage"];

                if (page >= 1 || page != null)
                {
                    TempData["SaveActionPage"] = page;
                    TempData.Keep("GetActionPage");
                }

                TempData["SaveActionFlag"] = 1;

                #endregion



                ViewBag.MovementTypeDdl = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMovementRegisterType(), "MOVEMENT_TYPE_ID", "MOVEMENT_TYPE_NAME");

                return RedirectToAction("EmployeeMovementRegisterRequest");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteEmployeeMovementRegisterRequest(string delId)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                MovementRegisterModel objMovementReg = new MovementRegisterModel();

                objMovementReg.EmployeeId = strEmployeeId;
                objMovementReg.TranId = delId;
                objMovementReg.UpdateBy = strEmployeeId;
                objMovementReg.HeadOfficeId = strHeadOfficeId;
                objMovementReg.BranchOfficeId = strBranchOfficeId;
                objMovementReg.ToDate = objLookUpDAL.getFirstLastDay().FirstDate;
                objMovementReg.FromDate = objLookUpDAL.getFirstLastDay().LastDate;

                string strDBMsg = "";

                strDBMsg = objEmployeeDAL.DeleteIndividualMovementRegister(objMovementReg);
                TempData["OperationMessage"] = strDBMsg;

                #region Pagination Search

                int page = (int)TempData["GetActionPage"];

                if (page >= 1 || page != null)
                {
                    TempData["SaveActionPage"] = page;
                    TempData.Keep("GetActionPage");
                }

                TempData["SaveActionFlag"] = 1;

                #endregion



                ViewBag.MovementTypeDdl = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMovementRegisterType(), "MOVEMENT_TYPE_ID", "MOVEMENT_TYPE_NAME");

                return RedirectToAction("EmployeeMovementRegisterRequest");
            }
        }



        public ActionResult ClearMovementRegister()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("EmployeeMovementRegisterRequest", "Employee");
        }
        #endregion

        #region Employee Movement Register Pending/Approved List for Team Leader--

        public ActionResult EmployeeMovementRegisterPendingListForTeamLeader()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                MovementRegisterModel objMovementReg = new MovementRegisterModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,

                    ToDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    FromDate = objLookUpDAL.getFirstLastDay().LastDate
                };

                objMovementReg.MovementRegisterModelList = objEmployeeDAL.GetMovementRegisterPendingListForTeamLeader(objMovementReg);

                return View(objMovementReg);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeMovementRegisterPendingListForTeamLeader(MovementRegisterModel objMovementReg)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objMovementReg.UpdateBy = strEmployeeId;
                objMovementReg.HeadOfficeId = strHeadOfficeId;
                objMovementReg.BranchOfficeId = strBranchOfficeId;

                objMovementReg.ToDate = objLookUpDAL.getFirstLastDay().FirstDate;
                objMovementReg.FromDate = objLookUpDAL.getFirstLastDay().LastDate;

                objMovementReg.MovementRegisterModelList = objEmployeeDAL.GetMovementRegisterPendingListForTeamLeader(objMovementReg);

                return View(objMovementReg);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmployeeMovementRegisterPendingListForTeamLeader(MovementRegisterModel objMovementReg)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objMovementReg.UpdateBy = strEmployeeId;
                objMovementReg.HeadOfficeId = strHeadOfficeId;
                objMovementReg.BranchOfficeId = strBranchOfficeId;

                objMovementReg.ToDate = objLookUpDAL.getFirstLastDay().FirstDate;
                objMovementReg.FromDate = objLookUpDAL.getFirstLastDay().LastDate;

                if (objMovementReg.MovementRegisterModelList != null)
                {
                    objMovementReg.MovementRegisterModelList.RemoveAll(x => !x.IsChecked);

                    foreach (MovementRegisterModel t in objMovementReg.MovementRegisterModelList)
                    {
                        objMovementReg.EmployeeId = t.EmployeeId;
                        objMovementReg.TranId = t.TranId;
                        objMovementReg.FirstInTl = t.FirstInTl;
                        objMovementReg.LastOutTl = t.LastOutTl;
                        objMovementReg.LogDate = t.LogDate;
                        objMovementReg.TeamLeaderRemarks = t.TeamLeaderRemarks;

                        string vMessage = objEmployeeDAL.ApproveMovementRegisterByTeamLeader(objMovementReg);

                        TempData["OperationMessage"] = vMessage;
                    }
                }


                return RedirectToAction("EmployeeMovementRegisterPendingListForTeamLeader");
            }
        }

        public ActionResult EmployeeMovementRegisterApprovedListForTeamLeader()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                MovementRegisterModel objMovementReg = new MovementRegisterModel
                {

                    //objMovementReg.EmployeeId = strEmployeeId;
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,

                    ToDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    FromDate = objLookUpDAL.getFirstLastDay().LastDate
                };

                objMovementReg.MovementRegisterModelList = objEmployeeDAL.GetMovementRegisterApprovedListForTeamLealder(objMovementReg);

                return View(objMovementReg);
            }
        }


        #endregion

        #region Employee Movement Register Pending/ Approved List for HR 

        public ActionResult EmployeeMovementRegisterPendingListForHr()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                MovementRegisterModel objMovementReg = new MovementRegisterModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,

                    ToDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    FromDate = objLookUpDAL.getFirstLastDay().LastDate
                };

                objMovementReg.MovementRegisterModelList = objEmployeeDAL.GetEmployeeMovementRegisterPendingListforHr(objMovementReg);

                return View(objMovementReg);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeMovementRegisterPendingListForHr(MovementRegisterModel objMovementReg)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objMovementReg.UpdateBy = strEmployeeId;
                objMovementReg.HeadOfficeId = strHeadOfficeId;
                objMovementReg.BranchOfficeId = strBranchOfficeId;

                objMovementReg.ToDate = objLookUpDAL.getFirstLastDay().FirstDate;
                objMovementReg.FromDate = objLookUpDAL.getFirstLastDay().LastDate;

                objMovementReg.MovementRegisterModelList = objEmployeeDAL.GetEmployeeMovementRegisterPendingListforHr(objMovementReg);

                return View(objMovementReg);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmployeeMovementRegisterPendingListForHr(MovementRegisterModel objMovementReg)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                //objMovementReg.EmployeeId = strEmployeeId;

                objMovementReg.UpdateBy = strEmployeeId;
                objMovementReg.HeadOfficeId = strHeadOfficeId;
                objMovementReg.BranchOfficeId = strBranchOfficeId;

                objMovementReg.ToDate = objLookUpDAL.getFirstLastDay().FirstDate;
                objMovementReg.FromDate = objLookUpDAL.getFirstLastDay().LastDate;

                if (objMovementReg.MovementRegisterModelList != null)
                {
                    objMovementReg.MovementRegisterModelList.RemoveAll(x => !x.IsChecked);

                    foreach (MovementRegisterModel t in objMovementReg.MovementRegisterModelList)
                    {
                        objMovementReg.EmployeeId = t.EmployeeId;
                        objMovementReg.TranId = t.TranId;
                        objMovementReg.FirstInHr = t.FirstInHr;
                        objMovementReg.LastOutHr = t.LastOutHr;
                        objMovementReg.LogDate = t.LogDate;
                        objMovementReg.HrRemarks = t.HrRemarks;

                        string vMessage = objEmployeeDAL.ApproveMovementRegisterHr(objMovementReg);

                        TempData["OperationMessage"] = vMessage;
                    }
                }

                return RedirectToAction("EmployeeMovementRegisterPendingListForHr");
            }
        }


        public ActionResult EmployeeMovementRegisterApprovedListForHr()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                MovementRegisterModel objMovementReg = new MovementRegisterModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,

                    ToDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    FromDate = objLookUpDAL.getFirstLastDay().LastDate
                };

                objMovementReg.MovementRegisterModelList = objEmployeeDAL.GetEmployeeMovementRegisterApprovedListforHr(objMovementReg);

                return View(objMovementReg);
            }
        }



        #endregion

        #region Employee Provident Fund Entry


        //mezba 18
        public ActionResult ProvidentFundEntry(ProvidentFundModel objProvidentFundModel, string pEmpId)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {

                LoadSession();
                objProvidentFundModel.UpdateBy = strEmployeeId;
                objProvidentFundModel.HeadOfficeId = strHeadOfficeId;
                objProvidentFundModel.BranchOfficeId = strBranchOfficeId;

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDList(strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME");
                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDList(strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME");
                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDList(strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME");


                if (objProvidentFundModel.SearchBy == "1")
                {
                    objProvidentFundModel.SearchInactiveYesNo = "Y";
                    DataTable dt = objEmployeeDAL.LoadEmployeeDataProvidentFund(objProvidentFundModel);
                    ViewBag.EmployeeList = EmployeeListData(dt);
                }


                if (!string.IsNullOrWhiteSpace(pEmpId))
                {
                    objProvidentFundModel.EmployeeId = pEmpId;
                    objProvidentFundModel = objEmployeeDAL.SearchEmployeeInformationProvidentFund(objProvidentFundModel);
                    ViewBag.SaveFlag = "1";

                    objProvidentFundModel.SearchInactiveYesNo = "Y";
                    DataTable dt = objEmployeeDAL.LoadEmployeeDataProvidentFund(objProvidentFundModel);
                    DataTable dt2 = objEmployeeDAL.ShowEmployeProvidentRecord(objProvidentFundModel);

                    ViewBag.EmployeeList = EmployeeListData(dt);
                    if (dt2.Rows.Count > 0)
                    {
                        ViewBag.EmployeeProvidentFundList = EmployeeProvidentFundListData(dt2);
                    }

                }


                return View(objProvidentFundModel);
            }
        }


        public JsonResult SaveProvidentFundEntry(ProvidentFundModel objProvidentFundModel)
        {
            LoadSession();
            string strDBMsg = "";

            objProvidentFundModel.UpdateBy = strEmployeeId;
            objProvidentFundModel.HeadOfficeId = strHeadOfficeId;
            objProvidentFundModel.BranchOfficeId = strBranchOfficeId;

            strDBMsg = objEmployeeDAL.SaveEmployeeProfidentFundInformation(objProvidentFundModel);
            return Json(strDBMsg, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteProvidentFundEntry(ProvidentFundModel objProvidentFundModel)
        {
            LoadSession();
            string strDBMsg = "";

            objProvidentFundModel.UpdateBy = strEmployeeId;
            objProvidentFundModel.HeadOfficeId = strHeadOfficeId;
            objProvidentFundModel.BranchOfficeId = strBranchOfficeId;

            strDBMsg = objEmployeeDAL.DeleteEmployeeProfidentFundInformation(objProvidentFundModel);
            return Json(strDBMsg, JsonRequestBehavior.AllowGet);
        }


        public List<ProvidentFundModel> EmployeeProvidentFundListData(DataTable dt2)
        {
            List<ProvidentFundModel> providentFundDataBundle = new List<ProvidentFundModel>();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                ProvidentFundModel objProvidentFundModel = new ProvidentFundModel();

                objProvidentFundModel.NomineeName = dt2.Rows[i]["NOMINEE_NAME"].ToString();
                objProvidentFundModel.NomineeAddress = dt2.Rows[i]["NOMINEE_ADDRESS"].ToString();
                objProvidentFundModel.NomineeRelation = dt2.Rows[i]["RELATIONSHIP_WITH_NOMINEE"].ToString();
                objProvidentFundModel.UnderAge = dt2.Rows[i]["ADULT_YN"].ToString();
                objProvidentFundModel.HandiCap = dt2.Rows[i]["HANDICAP_YN"].ToString();
                objProvidentFundModel.Mon = dt2.Rows[i]["NO_OF_NOMINEE"].ToString();
                objProvidentFundModel.Percentage = dt2.Rows[i]["NOMINEE_PERCENTAGE"].ToString();
                objProvidentFundModel.GuardianName = dt2.Rows[i]["NOMINEE_GUARDIAN_NAME"].ToString();
                objProvidentFundModel.GuardianAddress = dt2.Rows[i]["NOMINEE_GUARDIAN_ADDRESS"].ToString();
                objProvidentFundModel.TranId = dt2.Rows[i]["TRAN_ID"].ToString();

                providentFundDataBundle.Add(objProvidentFundModel);
            }
            return providentFundDataBundle;
        }
        #endregion

        #region Employee Important Files Upload

        private string UploadFilesToDatabase(HttpPostedFileBase fileBase)
        {
            Stream str = fileBase.InputStream;
            BinaryReader Br = new BinaryReader(str);
            byte[] fileDet = Br.ReadBytes((int)str.Length);
            string imageSize = Convert.ToBase64String(fileDet);

            return imageSize;
        }

        public ActionResult EmployeeFileUpload()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                EmployeeFileUploadModel objFileUpload = new EmployeeFileUploadModel
                {

                    // objDutyRoasterModel.EmployeeId = strEmployeeId;
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,



                    EmployeeDocUploadList = new List<EmployeeFileUploadModel>()
                };
                // objFileUpload.EmployeeDocJlUploadList = new List<EmployeeFileUpload>();

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                return View(objFileUpload);
            }
        }

        [HttpPost]
        public ActionResult EmployeeFileUpload(EmployeeFileUploadModel objFileUpload)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                // objDutyRoasterModel.EmployeeId = strEmployeeId;
                objFileUpload.UpdateBy = strEmployeeId;
                objFileUpload.HeadOfficeId = strHeadOfficeId;
                objFileUpload.BranchOfficeId = strBranchOfficeId;

                //ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                if (objFileUpload.StatusJd != null)
                {
                    string fileUploadTab = Regex.Replace(objFileUpload.StatusJd, @"[\n \r]", "");


                    if (fileUploadTab == "JobDescription")
                    {
                        if (objFileUpload.EmployeeDocUploadList != null)
                        {
                            objFileUpload.EmployeeDocUploadList.RemoveAll(x => !x.IsChecked);

                            foreach (EmployeeFileUploadModel t in objFileUpload.EmployeeDocUploadList)
                            {
                                string s = Path.GetExtension(t.JdFile.FileName);
                                if (s != null)
                                {
                                    string extension = s.ToUpper();
                                    if (extension == ".DOCX" || extension == ".DOC")
                                    {
                                        //foreach (EmployeeFileUpload upload in objFileUpload.EmployeeDocUploadList)
                                        {
                                            //if (upload.IsChecked)
                                            {
                                                //Stream str = t.JdFile.InputStream;
                                                //BinaryReader Br = new BinaryReader(str);
                                                //Byte[] fileDet = Br.ReadBytes((Int32)str.Length);
                                                //string imageSize = Convert.ToBase64String(fileDet);

                                                objFileUpload.EmployeeId = t.EmployeeId;
                                                objFileUpload.JdFileName = t.JdFile.FileName;
                                                objFileUpload.JdFileSize = UploadFilesToDatabase(t.JdFile);
                                                objFileUpload.JdFileExtension = s;

                                                string vMessage = objEmployeeDAL.SaveEmpJd(objFileUpload);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (fileUploadTab == "JoiningLetter")
                    {
                        if (objFileUpload.EmployeeDocUploadList != null)
                        {
                            objFileUpload.EmployeeDocUploadList.RemoveAll(x => !x.IsCheckedJl);

                            foreach (EmployeeFileUploadModel t in objFileUpload.EmployeeDocUploadList)
                            {
                                string s = Path.GetExtension(t.JlFile.FileName);
                                if (s != null)
                                {
                                    string extension = s.ToUpper();
                                    if (extension == ".PDF")
                                    {
                                        // foreach (EmployeeFileUpload upload in objFileUpload.EmployeeDocUploadList)
                                        {
                                            //if (upload.IsChecked)
                                            {
                                                //Stream str = t.JlFile.InputStream;
                                                //BinaryReader Br = new BinaryReader(str);
                                                //Byte[] fileDet = Br.ReadBytes((Int32)str.Length);
                                                //string imageSize = Convert.ToBase64String(fileDet);

                                                objFileUpload.EmployeeId = t.EmployeeId;
                                                objFileUpload.JlFileName = t.JlFile.FileName;
                                                objFileUpload.JlFileSize = UploadFilesToDatabase(t.JlFile);
                                                objFileUpload.JlFileExtension = s;

                                                string vMessage = objEmployeeDAL.SaveEmpJl(objFileUpload);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (fileUploadTab == "AppointmentLetter")
                    {
                        if (objFileUpload.EmployeeDocUploadList != null)
                        {
                            objFileUpload.EmployeeDocUploadList.RemoveAll(x => !x.IsCheckedAl);

                            foreach (EmployeeFileUploadModel t in objFileUpload.EmployeeDocUploadList)
                            {
                                string s = Path.GetExtension(t.AlFile.FileName);
                                if (s != null)
                                {
                                    string extension = s.ToUpper();
                                    if (extension == ".PDF")
                                    {
                                        // foreach (EmployeeFileUpload upload in objFileUpload.EmployeeDocUploadList)
                                        {
                                            // if (upload.IsChecked)
                                            {
                                                //Stream str = t.AlFile.InputStream;
                                                //BinaryReader Br = new BinaryReader(str);
                                                //Byte[] fileDet = Br.ReadBytes((Int32)str.Length);
                                                //string imageSize = Convert.ToBase64String(fileDet);

                                                objFileUpload.EmployeeId = t.EmployeeId;
                                                objFileUpload.AlFileName = t.AlFile.FileName;
                                                objFileUpload.AlFileSize = UploadFilesToDatabase(t.AlFile);
                                                objFileUpload.AlFileExtension = s;

                                                string vMessage = objEmployeeDAL.SaveEmpAl(objFileUpload);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                    else if (fileUploadTab == "NID/BirthCertificate")
                    {
                        if (objFileUpload.EmployeeDocUploadList != null)
                        {
                            objFileUpload.EmployeeDocUploadList.RemoveAll(x => !x.IsCheckedNid);

                            foreach (EmployeeFileUploadModel t in objFileUpload.EmployeeDocUploadList)
                            {
                                string s = Path.GetExtension(t.NidFile.FileName);
                                if (s != null)
                                {
                                    string extension = s.ToUpper();
                                    if (extension == ".PDF")
                                    {
                                        // foreach (EmployeeFileUpload upload in objFileUpload.EmployeeDocUploadList)
                                        {
                                            // if (upload.IsChecked)
                                            {
                                                //Stream str = t.NidFile.InputStream;
                                                //BinaryReader Br = new BinaryReader(str);
                                                //Byte[] fileDet = Br.ReadBytes((Int32)str.Length);
                                                //string imageSize = Convert.ToBase64String(fileDet);

                                                objFileUpload.EmployeeId = t.EmployeeId;
                                                objFileUpload.NidFileName = t.NidFile.FileName;
                                                objFileUpload.NidFileSize = UploadFilesToDatabase(t.NidFile);
                                                objFileUpload.NidFileExtension = s;

                                                string vMessage = objEmployeeDAL.SaveEmpNid(objFileUpload);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (fileUploadTab == "NomieePhoto")
                    {
                        if (objFileUpload.EmployeeDocUploadList != null)
                        {
                            objFileUpload.EmployeeDocUploadList.RemoveAll(x => !x.IsCheckedNp);

                            foreach (EmployeeFileUploadModel t in objFileUpload.EmployeeDocUploadList)
                            {
                                string s = Path.GetExtension(t.NpFile.FileName);
                                if (s != null)
                                {
                                    string extension = s.ToUpper();
                                    if (extension == ".JPEG" || extension == ".JPG" || extension == ".PNG")
                                    {
                                        // foreach (EmployeeFileUpload upload in objFileUpload.EmployeeDocUploadList)
                                        {
                                            // if (upload.IsChecked)
                                            {
                                                //Stream str = t.NpFile.InputStream;
                                                //BinaryReader Br = new BinaryReader(str);
                                                //Byte[] fileDet = Br.ReadBytes((Int32)str.Length);
                                                //string imageSize = Convert.ToBase64String(fileDet);

                                                objFileUpload.EmployeeId = t.EmployeeId;
                                                objFileUpload.NpFileName = t.NpFile.FileName;
                                                objFileUpload.NpFileSize = UploadFilesToDatabase(t.NpFile);
                                                objFileUpload.NpFileExtension = s;

                                                string vMessage = objEmployeeDAL.SaveEmpNp(objFileUpload);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (objFileUpload.EmployeeDocUploadList != null)
                        {
                            objFileUpload.EmployeeDocUploadList.RemoveAll(x => !x.IsCheckedPf);

                            foreach (EmployeeFileUploadModel t in objFileUpload.EmployeeDocUploadList)
                            {
                                string s = Path.GetExtension(t.PfFile.FileName);
                                if (s != null)
                                {
                                    string extension = s.ToUpper();
                                    if (extension == ".PDF")
                                    {
                                        //foreach (EmployeeFileUpload upload in objFileUpload.EmployeeDocUploadList)
                                        {
                                            //if (upload.IsChecked)
                                            {
                                                //Stream str = t.PfFile.InputStream;
                                                //BinaryReader Br = new BinaryReader(str);
                                                //Byte[] fileDet = Br.ReadBytes((Int32)str.Length);
                                                //string imageSize = Convert.ToBase64String(fileDet);

                                                objFileUpload.EmployeeId = t.EmployeeId;
                                                objFileUpload.PfFileName = t.PfFile.FileName;
                                                objFileUpload.PfFileSize = UploadFilesToDatabase(t.PfFile);
                                                objFileUpload.PfFileExtension = s;

                                                string vMessage = objEmployeeDAL.SaveEmpPf(objFileUpload);
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (objFileUpload.UnitId != null)
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    if (objFileUpload.DepartmentId != null)
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objFileUpload.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objFileUpload.DepartmentId);
                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }

                    if (objFileUpload.SectionId != null)
                    {
                        ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objFileUpload.DepartmentId,
                            strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objFileUpload.SectionId);
                    }
                    else
                    {
                        ViewBag.SectionDDList = null;
                    }

                    if (objFileUpload.SubSectionId != null)
                    {
                        ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objFileUpload.SectionId,
                            strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objFileUpload.SubSectionId);
                    }
                    else
                    {
                        ViewBag.SubSectionDDList = null;
                    }
                }
                else
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                }

                objFileUpload.ActiveYn = objFileUpload.Active ? 'N'.ToString() : 'Y'.ToString();
                //objFileUpload.EmployeeId = null;
                //objFileUpload.IsChecked = false;
                //objFileUpload.IsCheckedJl = false;
                //objFileUpload.IsCheckedAl = false;
                //objFileUpload.IsCheckedNid = false;
                //objFileUpload.IsCheckedNp = false;
                //objFileUpload.IsCheckedPf = false;


                objFileUpload.EmployeeDocUploadList = objEmployeeDAL.LoadEmployeeRecordForJd(objFileUpload);
                //objFileUpload.EmployeeDocJlUploadList = objEmployeeDAL.LoadEmployeeRecordForJd(objFileUpload);

                return View(objFileUpload);
            }
        }

        [HttpGet]
        public ActionResult DownLoadEmployeeFiles(string empId, string tabVal)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                EmployeeFileUploadModel objFileUpload = new EmployeeFileUploadModel
                {

                    // objDutyRoasterModel.EmployeeId = strEmployeeId;
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,

                    EmployeeId = empId
                };
                DataTable dt = new DataTable();


                if (tabVal == "jd")
                {
                    dt = objEmployeeDAL.DownloadEmpJd(objFileUpload);

                    HttpContext.Response.Clear();
                    string name = dt.Rows[(0)]["FILE_NAME"].ToString();
                    byte[] documentBytes = (byte[])dt.Rows[(0)]["FILE_SIZE"];
                    string fileExtension = dt.Rows[(0)]["FILE_EXTENSION"].ToString();

                    //context.Response.ContentType = "application / vnd.openxmlformats - officedocument.wordprocessingml.document";

                    HttpContext.Response.ContentType = "application/octet-stream";
                    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=\"" + name + "" + fileExtension + "\"");
                    HttpContext.Response.AddHeader("Content-Length", documentBytes.ToString());
                    HttpContext.Response.BinaryWrite(documentBytes);
                    HttpContext.Response.Flush();
                    HttpContext.Response.Close();
                    HttpContext.Response.End();

                    return File(documentBytes, fileExtension, name);

                }
                else if (tabVal == "jl")
                {
                    dt = objEmployeeDAL.DownloadEmpJl(objFileUpload);

                    HttpContext.Response.Clear();
                    string name = dt.Rows[(0)]["FILE_NAME"].ToString();
                    byte[] documentBytes = (byte[])dt.Rows[(0)]["FILE_SIZE"];
                    string fileExtension = dt.Rows[(0)]["FILE_EXTENSION"].ToString();

                    return File(documentBytes, "application/pdf");
                }
                else if (tabVal == "al")
                {
                    dt = objEmployeeDAL.DownloadEmpAl(objFileUpload);

                    HttpContext.Response.Clear();
                    string name = dt.Rows[(0)]["FILE_NAME"].ToString();
                    byte[] documentBytes = (byte[])dt.Rows[(0)]["FILE_SIZE"];
                    string fileExtension = dt.Rows[(0)]["FILE_EXTENSION"].ToString();


                    return File(documentBytes, "application/pdf");
                }
                else if (tabVal == "nid")
                {
                    dt = objEmployeeDAL.DownloadEmpNid(objFileUpload);

                    HttpContext.Response.Clear();
                    string name = dt.Rows[(0)]["FILE_NAME"].ToString();
                    byte[] documentBytes = (byte[])dt.Rows[(0)]["FILE_SIZE"];
                    string fileExtension = dt.Rows[(0)]["FILE_EXTENSION"].ToString();

                    return File(documentBytes, "application/pdf");
                }
                else if (tabVal == "np")
                {
                    dt = objEmployeeDAL.DownloadEmpNp(objFileUpload);

                    HttpContext.Response.Clear();
                    string name = dt.Rows[(0)]["FILE_NAME"].ToString();
                    byte[] documentBytes = (byte[])dt.Rows[(0)]["FILE_SIZE"];
                    string fileExtension = dt.Rows[(0)]["FILE_EXTENSION"].ToString();

                    HttpContext.Response.ContentType = "application/octet-stream";
                    HttpContext.Response.AddHeader("content-disposition", "attachment; filename=\"" + name + "" + fileExtension + "\"");
                    HttpContext.Response.AddHeader("Content-Length", documentBytes.ToString());
                    HttpContext.Response.BinaryWrite(documentBytes);
                    HttpContext.Response.Flush();
                    HttpContext.Response.Close();
                    HttpContext.Response.End();

                    return File(documentBytes, fileExtension, name);
                }
                else if (tabVal == "pf")
                {
                    dt = objEmployeeDAL.DownloadEmpPf(objFileUpload);

                    HttpContext.Response.Clear();
                    string name = dt.Rows[(0)]["FILE_NAME"].ToString();
                    byte[] documentBytes = (byte[])dt.Rows[(0)]["FILE_SIZE"];
                    string fileExtension = dt.Rows[(0)]["FILE_EXTENSION"].ToString();

                    return File(documentBytes, "application/pdf");
                }
                else
                {
                    return null;
                }
            }
        }


        // Individual Jd

        public ActionResult IndividualEmployeeJd()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                EmployeeIndividualJdModel objIndividualJd = new EmployeeIndividualJdModel
                {
                    EmployeeId = strEmployeeId,
                    CheckedYN = "Y",

                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };


                ViewBag.Details = objEmployeeDAL.GetEmployeeRecordForJD(objIndividualJd);
                ViewBag.ApproveStatus = objEmployeeDAL.GetApproveStatusJD(objIndividualJd); // approve status of employee's job description

                return View(objIndividualJd);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IndividualEmployeeJd(EmployeeIndividualJdModel objIndividualJd)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objIndividualJd.UpdateBy = strEmployeeId;
                objIndividualJd.HeadOfficeId = strHeadOfficeId;
                objIndividualJd.BranchOfficeId = strBranchOfficeId;

                if (objIndividualJd.IndividualJdFile != null)
                {
                    string CVExtension = Path.GetExtension(objIndividualJd.IndividualJdFile.FileName).ToUpper();

                    if (CVExtension == ".DOC" || CVExtension == ".DOCX")
                    {
                        objIndividualJd.IndividualJdFileName = objIndividualJd.IndividualJdFile.FileName;
                        objIndividualJd.IndividualJdFileSize = UploadFilesToDatabase(objIndividualJd.IndividualJdFile);
                        objIndividualJd.IndividualJdFileExtension = CVExtension;

                        string msg = objEmployeeDAL.SaveEmpIndividualJD(objIndividualJd);
                        TempData["OperationMessage"] = msg;
                    }
                }

                return RedirectToAction("IndividualEmployeeJd");
            }
        }

        [HttpGet]
        public ActionResult DownLoadEmployeeIndividualJd(string empId)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                EmployeeIndividualJdModel objIndividual = new EmployeeIndividualJdModel
                {

                    // objDutyRoasterModel.EmployeeId = strEmployeeId;
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,

                    EmployeeId = empId
                };
                DataTable dt = new DataTable();

                dt = objEmployeeDAL.DownloadEmpIndividualJd(objIndividual);
                if (dt.Rows.Count > 0)
                {
                    HttpContext.Response.Clear();
                    string name = dt.Rows[(0)]["FILE_NAME"].ToString();
                    byte[] documentBytes = (byte[])dt.Rows[(0)]["FILE_SIZE"];
                    string fileExtension = dt.Rows[(0)]["FILE_EXTENSION"].ToString();

                    //context.Response.ContentType = "application / vnd.openxmlformats - officedocument.wordprocessingml.document";

                    HttpContext.Response.ContentType = "application/octet-stream";
                    HttpContext.Response.AddHeader("content-disposition",
                        "attachment; filename=\"" + name + "" + fileExtension + "\"");
                    HttpContext.Response.AddHeader("Content-Length", documentBytes.ToString());
                    HttpContext.Response.BinaryWrite(documentBytes);
                    HttpContext.Response.Flush();
                    HttpContext.Response.Close();
                    HttpContext.Response.End();

                    return File(documentBytes, fileExtension, name);
                }
                else
                {
                    TempData["OperationMessage"] = "There is no file to download.";
                    return RedirectToAction("IndividualEmployeeJd");
                }


            }
        }

        [HttpGet]
        public ActionResult DownLoadEmployeeIndividualJdBeforeApprovalTeamLead(string empId)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                EmployeeIndividualJdModel objIndividual = new EmployeeIndividualJdModel
                {


                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,

                    EmployeeId = empId
                };
                DataTable dt = new DataTable();

                dt = objEmployeeDAL.DownloadEmpIndividualJdBeforeApprovalForTl(objIndividual);

                HttpContext.Response.Clear();
                string name = dt.Rows[(0)]["FILE_NAME"].ToString();
                byte[] documentBytes = (byte[])dt.Rows[(0)]["FILE_SIZE"];
                string fileExtension = dt.Rows[(0)]["FILE_EXTENSION"].ToString();

                //context.Response.ContentType = "application / vnd.openxmlformats - officedocument.wordprocessingml.document";

                HttpContext.Response.ContentType = "application/octet-stream";
                HttpContext.Response.AddHeader("content-disposition",
                    "attachment; filename=\"" + name + "" + fileExtension + "\"");
                HttpContext.Response.AddHeader("Content-Length", documentBytes.ToString());
                HttpContext.Response.BinaryWrite(documentBytes);
                HttpContext.Response.Flush();
                HttpContext.Response.Close();
                HttpContext.Response.End();

                return File(documentBytes, fileExtension, name);

            }
        }

        [HttpGet]
        public ActionResult DownLoadEmployeeIndividualJdBeforeApprovalHr(string empId)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                EmployeeIndividualJdModel objIndividual = new EmployeeIndividualJdModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,

                    EmployeeId = empId
                };
                DataTable dt = new DataTable();

                dt = objEmployeeDAL.DownloadEmpIndividualJdBeforeApprovalForHr(objIndividual);

                HttpContext.Response.Clear();
                string name = dt.Rows[(0)]["FILE_NAME"].ToString();
                byte[] documentBytes = (byte[])dt.Rows[(0)]["FILE_SIZE"];
                string fileExtension = dt.Rows[(0)]["FILE_EXTENSION"].ToString();

                //context.Response.ContentType = "application / vnd.openxmlformats - officedocument.wordprocessingml.document";

                HttpContext.Response.ContentType = "application/octet-stream";
                HttpContext.Response.AddHeader("content-disposition",
                    "attachment; filename=\"" + name + "" + fileExtension + "\"");
                HttpContext.Response.AddHeader("Content-Length", documentBytes.ToString());
                HttpContext.Response.BinaryWrite(documentBytes);
                HttpContext.Response.Flush();
                HttpContext.Response.Close();
                HttpContext.Response.End();

                return File(documentBytes, fileExtension, name);

            }
        }


        #endregion

        #region Approval for Job Description

        // Approval of Employee's Job Description By Team Leader

        [HttpGet]
        public ActionResult ApproveEmployeeJdByTeamLeader()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                EmployeeIndividualJdModel objIndividualJd = new EmployeeIndividualJdModel
                {
                    TeamLeaderId = strEmployeeId,
                    CheckedYN = "Y",

                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,
                    ToDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    FromDate = objLookUpDAL.getFirstLastDay().LastDate
                };


                objIndividualJd.IndividualJdList = objEmployeeDAL.GetJDPendingListForTL(objIndividualJd);  //new List<EmployeeIndividualJdModel>();

                return View(objIndividualJd);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveEmployeeJdByTeamLeader(EmployeeIndividualJdModel objIndividualJd)
        {

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objIndividualJd.TeamLeaderId = strEmployeeId;
                objIndividualJd.CheckedYN = "Y";

                objIndividualJd.UpdateBy = strEmployeeId;
                objIndividualJd.HeadOfficeId = strHeadOfficeId;
                objIndividualJd.BranchOfficeId = strBranchOfficeId;

                if (objIndividualJd.IndividualJdList != null)
                {
                    string successMsg = "";
                    objIndividualJd.IndividualJdList.RemoveAll(x => !x.IsChecked);

                    for (int i = 0; i < objIndividualJd.IndividualJdList.Count; i++)
                    {
                        objIndividualJd.IndividualJdList[i].UpdateBy = strEmployeeId;
                        objIndividualJd.IndividualJdList[i].HeadOfficeId = strHeadOfficeId;
                        objIndividualJd.IndividualJdList[i].BranchOfficeId = strBranchOfficeId;

                        successMsg = objEmployeeDAL.ApprovedEmpJDByTL(objIndividualJd.IndividualJdList[i]);
                        TempData["OperationMessage"] = successMsg;
                    }
                }



                objIndividualJd.IndividualJdList = objEmployeeDAL.GetJDPendingListForTL(objIndividualJd);

                return View(objIndividualJd);
            }
        }

        // Approval of Employee's Job Description By H R
        [HttpGet]
        public ActionResult ApproveEmployeeJdByHR()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                EmployeeIndividualJdModel objIndividualJd = new EmployeeIndividualJdModel
                {
                    HrId = strEmployeeId,
                    CheckedYN = "Y",

                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,
                    ToDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    FromDate = objLookUpDAL.getFirstLastDay().LastDate
                };

                //load pending list of employee's uploaded jd for team leader
                objIndividualJd.IndividualJdList = objEmployeeDAL.GetJDPendingListForHR(objIndividualJd);//new List<EmployeeIndividualJdModel>();

                return View(objIndividualJd);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveEmployeeJdByHR(EmployeeIndividualJdModel objIndividualJd)
        {

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objIndividualJd.UpdateBy = strEmployeeId;
                objIndividualJd.HeadOfficeId = strHeadOfficeId;
                objIndividualJd.BranchOfficeId = strBranchOfficeId;

                if (objIndividualJd.IndividualJdList != null)
                {
                    string successMsg = "";
                    objIndividualJd.IndividualJdList.RemoveAll(x => !x.IsChecked);

                    for (int i = 0; i < objIndividualJd.IndividualJdList.Count; i++)
                    {
                        objIndividualJd.IndividualJdList[i].UpdateBy = strEmployeeId;
                        objIndividualJd.IndividualJdList[i].HeadOfficeId = strHeadOfficeId;
                        objIndividualJd.IndividualJdList[i].BranchOfficeId = strBranchOfficeId;

                        successMsg = objEmployeeDAL.ApprovedEmpJDByHR(objIndividualJd.IndividualJdList[i]);
                        TempData["OperationMessage"] = successMsg;
                    }
                }


                //load pending list of employee's uploaded jd for hr
                objIndividualJd.IndividualJdList = objEmployeeDAL.GetJDPendingListForHR(objIndividualJd);

                return View(objIndividualJd);
            }
        }

        [HttpGet]
        public ActionResult JdApprovedListForTeamLead()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                EmployeeIndividualJdModel objIndividualJd = new EmployeeIndividualJdModel
                {
                    HrId = strEmployeeId,
                    CheckedYN = "Y",

                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,
                    ToDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    FromDate = objLookUpDAL.getFirstLastDay().LastDate
                };

                //load pending list of employee's uploaded jd for team leader
                objIndividualJd.IndividualJdList = objEmployeeDAL.GetJDApprovedListForTL(objIndividualJd);//new List<EmployeeIndividualJdModel>();

                return View(objIndividualJd);
            }
        }


        [HttpPost]
        public ActionResult JdApprovedListForTeamLead(EmployeeIndividualJdModel objIndividualJd)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objIndividualJd = new EmployeeIndividualJdModel
                {
                    HrId = strEmployeeId,
                    CheckedYN = "Y",

                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,
                    ToDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    FromDate = objLookUpDAL.getFirstLastDay().LastDate
                };

                //load pending list of employee's uploaded jd for team leader
                objIndividualJd.IndividualJdList = objEmployeeDAL.GetJDApprovedListForTL(objIndividualJd);//new List<EmployeeIndividualJdModel>();

                return View(objIndividualJd);
            }
        }

        [HttpGet]
        public ActionResult JdApprovedListForHr()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                EmployeeIndividualJdModel objIndividualJd = new EmployeeIndividualJdModel
                {
                    HrId = strEmployeeId,
                    CheckedYN = "Y",

                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,
                    ToDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    FromDate = objLookUpDAL.getFirstLastDay().LastDate
                };

                //load approved list of employee's uploaded jd for team leader
                objIndividualJd.IndividualJdList = objEmployeeDAL.GetJDApprovedListForHr(objIndividualJd);//new List<EmployeeIndividualJdModel>();

                return View(objIndividualJd);
            }
        }


        [HttpPost]
        public ActionResult JdApprovedListForHr(EmployeeIndividualJdModel objIndividualJd)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objIndividualJd = new EmployeeIndividualJdModel
                {
                    HrId = strEmployeeId,
                    CheckedYN = "Y",

                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,
                    ToDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    FromDate = objLookUpDAL.getFirstLastDay().LastDate
                };

                //load approved list of employee's uploaded jd for h r.
                objIndividualJd.IndividualJdList = objEmployeeDAL.GetJDApprovedListForHr(objIndividualJd);//new List<EmployeeIndividualJdModel>();

                return View(objIndividualJd);
            }
        }



        #endregion


    }
}