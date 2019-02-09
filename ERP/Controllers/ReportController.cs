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
    public class ReportController : Controller
    {
        #region Common

        private LookUpDAL objLookUpDAL = new LookUpDAL();
        private readonly EmployeeDAL objEmployeeDAL = new EmployeeDAL();
        private readonly ReportDAL objReportDAL = new ReportDAL();
        private readonly ReportDocument objReportDocument = new ReportDocument();
        private ExportFormatType objExportFormatType = ExportFormatType.NoFormat;
        private readonly EmployeeModel objEmployeeModel = new EmployeeModel();
        private readonly EmployeeDataById objEmployeeDataById = new EmployeeDataById();

        private string strEmployeeId = "";
        private string strDesignationId = "";
        private string strSubSectionId = "";
        private string strUnitId = "";
        private string strHeadOfficeId = "";
        private string strBranchOfficeId = "";
        private string strSoftId = "";
        private string strOldUrl = "";

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

            ViewBag.CountryDDList =
                UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCountryDDList(), "COUNTRY_ID", "COUNTRY_NAME");
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
            ViewBag.GradeDDList =
                UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetGradeDDList(), "GRADE_ID", "GRADE_NO");
            ViewBag.ProbationPeriodDDList =
                UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetProbationPeriodDDList(), "PROBATION_PERIOD_ID",
                    "PROBATION_PERIOD");
            ViewBag.SupervisorDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSupervisorDDList(),
                "SUPERVISOR_EMPLOYEE_ID", "SUPERVISOR_EMPLOYEE_NAME");
            ViewBag.JobTypeDDList =
                UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetJobTypeDDList(), "JOB_TYPE_ID", "JOB_TYPE_NAME");
            ViewBag.PaymentTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetPaymentTypeDDList(),
                "PAYMENT_TYPE_ID", "PAYMENT_TYPE_NAME");
            ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDList(strHeadOfficeId, strBranchOfficeId),
                "DEPARTMENT_ID", "DEPARTMENT_NAME");
            ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDList(strHeadOfficeId, strBranchOfficeId),
                "SUB_SECTION_ID", "SUB_SECTION_NAME");
            ViewBag.ShiftDDList =
                UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetShiftDDList(), "SHIFT_ID", "SHIFT_NAME");
            ViewBag.JobLocationDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetJobLocationDDList(),
                "JOB_LOCATION_ID", "JOB_LOCATION");
            ViewBag.ApprovedByDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetApprovedByDDList(),
                "APPROVED_EMPLOYEE_ID", "APPROVED_EMPLOYEE_NAME");
            ViewBag.HolidayDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetWeeklyHolidayDDList(),
                "WEEKLY_HOLIDAY_ID", "WEEKLY_HOLIDAY_NAME");
            ViewBag.DegreeDDList =
                UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDegreeDDList(), "DEGREE_ID", "DEGREE_NAME");
            ViewBag.MajorSubjectDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMajorSubjectDDList(),
                "MAJOR_SUBJECT_ID", "MAJOR_SUBJECT_NAME");
        }

        #endregion

        #region User Defined Methods
        
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

        #region Salary Report

        public ActionResult SalaryReport()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                SalaryReportModel objSalaryReportModel = new SalaryReportModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };


                ViewBag.UnitDDList =
                    UtilityClass.GetSelectListByDataTable(
                        objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                ViewBag.MonthDDList =
                    UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthDDList(), "MONTH_ID", "MONTH_NAME");

                return View(objSalaryReportModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult SalaryReport(SalaryReportModel objSalaryReportModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objSalaryReportModel.UpdateBy = strEmployeeId;
                objSalaryReportModel.HeadOfficeId = strHeadOfficeId;
                objSalaryReportModel.BranchOfficeId = strBranchOfficeId;

                if (objSalaryReportModel.ReportFor == "MWDCO")
                {
                    GenerateCoMonthlyWorkingDayReport(objSalaryReportModel);
                }
                else if (objSalaryReportModel.ReportFor == "SS")
                {
                    GenerateMonthlySalarySheetReport(objSalaryReportModel);
                }
                else if (objSalaryReportModel.ReportFor == "PS")
                {
                    GenerateMonthlyPaySlipReport(objSalaryReportModel);
                }

                else if (objSalaryReportModel.ReportFor == "BSS")
                {
                    GenerateMonthlyBankSalarySheetReport(objSalaryReportModel);
                }
                else if (objSalaryReportModel.ReportFor == "CBSS")
                {
                    GenerateMonthlyCashBankSalarySheetReport(objSalaryReportModel);
                }
                else if (objSalaryReportModel.ReportFor == "HSS")
                {
                    GenerateMonthlyHourlySalarySheetReport(objSalaryReportModel);
                }
                else if (objSalaryReportModel.ReportFor == "WSS")
                {
                    GenerateMonthlyWorkerSalarySheetReport(objSalaryReportModel);
                }
                else if (objSalaryReportModel.ReportFor == "WPS")
                {
                    GenerateMonthlyWorkerPaySlipReport(objSalaryReportModel);
                }
                else if (objSalaryReportModel.ReportFor == "IY")
                {
                    GenerateMonthlyIncrementYearlyReport(objSalaryReportModel);
                }


                ViewBag.UnitDDList =
                    UtilityClass.GetSelectListByDataTable(
                        objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                ViewBag.MonthDDList =
                    UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthDDList(), "MONTH_ID", "MONTH_NAME");

                return View(objSalaryReportModel);
            }
        }


        #region User Defined Function
        private void GenerateCoMonthlyWorkingDayReport(SalaryReportModel objReport)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptWorkingDayListCO.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.CoMonthlyWorkingDayList(objReport));

            //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objReport.ReportType, "WorkingDayList");
        }

        private void GenerateMonthlySalarySheetReport(SalaryReportModel objReport)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptMonthlySalarySheetCO.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.CoMonthlySalarySheet(objReport));

            //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objReport.ReportType, "MonthlySalarySheet");
        }

        private void GenerateMonthlyPaySlipReport(SalaryReportModel objReport)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/PaySlipCO.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.CoMonthlySalarySlip(objReport));

            //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objReport.ReportType, "MonthlySalarySheet");
        }


        private void GenerateMonthlyBankSalarySheetReport(SalaryReportModel objReport)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptMonthlyBankSalaryCO.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.CoMonthlyBankSalarySheet(objReport));

            //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objReport.ReportType, "MonthlyBankSalarySheet");
        }


        private void GenerateMonthlyCashBankSalarySheetReport(SalaryReportModel objReport)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptMonthlyCashSalarySheetCO.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.CoMonthlyCashSalarySheet(objReport));

            //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objReport.ReportType, "MonthlyCashBankSalarySheet");
        }


        private void GenerateMonthlyHourlySalarySheetReport(SalaryReportModel objReport)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptMonthlyHourlySalarySheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.HourlyMonthlySalarySheet(objReport));

            //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objReport.ReportType, "MonthlyCashBankSalarySheet");
        }

        private void GenerateMonthlyWorkerSalarySheetReport(SalaryReportModel objReport)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptSalarySheetWorker.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.WorkerSalarySheet(objReport));

            //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objReport.ReportType, "MonthlyCashBankSalarySheet");
        }

        private void GenerateMonthlyWorkerPaySlipReport(SalaryReportModel objReport)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptWorkerPaySlip.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.WorkerPaySlip(objReport));

            //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objReport.ReportType, "MonthlyCashBankSalarySheet");
        }

        private void GenerateMonthlyIncrementYearlyReport(SalaryReportModel objReport)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptIncrementHistory.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.IncrementYearlyCo(objReport));

            //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objReport.ReportType, "MonthlyCashBankSalarySheet");
        }


        #endregion

        #endregion

        #region Bonus Report 

        [HttpGet]
        public ActionResult BonusReport()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                BonusReportModel objSalaryReportModel = new BonusReportModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                ViewBag.EidDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetEidId(), "EID_ID", "EID_NAME");

                return View(objSalaryReportModel);
            }
        }

        #endregion

        #region Report for Salary Certificate

        private void GenerateSalaryCertificateReport(SalaryCertificateModel objReport)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptSalaryCertificate.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.GetSalaryCertificate(objReport));

            //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objReport.ReportType, "WorkingDayList");
        }


        // GET: Report
        public ActionResult ReportSalaryCertificate()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                SalaryCertificateModel objSalaryCertificate = new SalaryCertificateModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,

                    SalaryCertificateList = new List<SalaryCertificateModel>()
                };

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCurrentMonthId(), "MONTH_ID", "MONTH_NAME");

                return View(objSalaryCertificate);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportSalaryCertificate(SalaryCertificateModel objSalaryCertificate)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objSalaryCertificate.UpdateBy = strEmployeeId;
                objSalaryCertificate.HeadOfficeId = strHeadOfficeId;
                objSalaryCertificate.BranchOfficeId = strBranchOfficeId;

                if (objSalaryCertificate.SalaryCertificateList != null)
                {
                    objSalaryCertificate.SalaryCertificateList.RemoveAll(x => !x.IsChecked);


                    for (int i = 0; i < objSalaryCertificate.SalaryCertificateList.Count; i++)
                    {
                        objSalaryCertificate.EmployeeId = objSalaryCertificate.SalaryCertificateList[i].EmployeeId;

                        string strmsg = objReportDAL.AddEmpRecordForSC(objSalaryCertificate);
                        string msg = objReportDAL.ProcessEmpRecordForSC(objSalaryCertificate);

                        //GenerateSalaryCertificateReport(objSalaryCertificate);
                    }

                    for (int i = 0; i < objSalaryCertificate.SalaryCertificateList.Count; i++)
                    {
                        GenerateSalaryCertificateReport(objSalaryCertificate);
                    }
                }

                if (objSalaryCertificate.UnitId != null)
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    if (objSalaryCertificate.DepartmentId != null)
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(
                            objLookUpDAL.GetDepartmentDDListByUnitId(objSalaryCertificate.UnitId,
                                strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME",
                            objSalaryCertificate.DepartmentId);
                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }

                    if (objSalaryCertificate.SectionId != null)
                    {
                        ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(
                            objLookUpDAL.GetSectionDDListByDepartmentId(objSalaryCertificate.DepartmentId,
                                strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME",
                            objSalaryCertificate.SectionId);
                    }
                    else
                    {
                        ViewBag.SectionDDList = null;
                    }

                    if (objSalaryCertificate.SubSectionId != null)
                    {
                        ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(
                            objLookUpDAL.GetSubSectionDDListBySectionId(objSalaryCertificate.SectionId,
                                strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME",
                            objSalaryCertificate.SubSectionId);
                    }
                    else
                    {
                        ViewBag.SubSectionDDList = null;
                    }
                    objSalaryCertificate.SalaryCertificateList = objReportDAL.GetEmpRecordForSC(objSalaryCertificate);


                    ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCurrentMonthId(), "MONTH_ID", "MONTH_NAME");

                    return View(objSalaryCertificate);

                }
                else
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    objSalaryCertificate.SalaryCertificateList = objReportDAL.GetEmpRecordForSC(objSalaryCertificate);


                    ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCurrentMonthId(), "MONTH_ID", "MONTH_NAME");

                    return View(objSalaryCertificate);
                }
            }
        }

        #endregion

        #region Earn Leave Calculated Report

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EarnLeaveReport(EmployeeEarnLeaveModel objEarnLeaveReportModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objEarnLeaveReportModel.UpdateBy = strEmployeeId;
                objEarnLeaveReportModel.HeadOfficeId = strHeadOfficeId;
                objEarnLeaveReportModel.BranchOfficeId = strBranchOfficeId;

                string strPath = Path.Combine(Server.MapPath("~/Reports/rptEarnLeave.rpt"));
                objReportDocument.Load(strPath);

                DataSet objDataSet = (objReportDAL.GetYearlyEarnLeaveReport(objEarnLeaveReportModel));

                //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
                objReportDocument.Load(strPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport(objEarnLeaveReportModel.ReportType, "EarnLeaveReport");


                return null;
            }
        }

        #endregion

        
        #region Attendance Report

        public ActionResult AttendanceReport()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                ViewBag.EmployeeTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetEmployeeTypeDDList(strHeadOfficeId, strBranchOfficeId), "EMPLOYEE_TYPE_ID", "EMPLOYEE_TYPE_NAME");


                AttendenceReportModel objAttendenceReportModel = new AttendenceReportModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                return View(objAttendenceReportModel);
            }
        }


        #region "User Define Function for Atendance"
        public string IndividualAttendanceProcess(AttendenceReportModel objAttendenceReportModel)
        {
            
            string strMsg = objReportDAL.ProcessIndividualAttendance(objAttendenceReportModel);
            return strMsg;

        }

        public string AttendanceSummaryProcess(AttendenceReportModel objAttendenceReportModel)
        {

            string strMsg = objReportDAL.AttendanceSummaryProcess(objAttendenceReportModel);
            return strMsg;

        }

        public string AttendanceProcessHigestDutyTime(AttendenceReportModel objAttendenceReportModel)
        {

            string strMsg = objReportDAL.AttendanceProcessHigestDutyTime(objAttendenceReportModel);
            return strMsg;

        }

        public string AttendanceProcessEarlyOut(AttendenceReportModel objAttendenceReportModel)
        {

            string strMsg = objReportDAL.AttendanceProcessEarlyOut(objAttendenceReportModel);
            return strMsg;

        }

        public string AttendanceProcessTopLateEmployee(AttendenceReportModel objAttendenceReportModel)
        {

            string strMsg = objReportDAL.AttendanceProcessTopLateEmployee(objAttendenceReportModel);
            return strMsg;

        }

        public string AttendanceProcessPIN(AttendenceReportModel objAttendenceReportModel)
        {

            string strMsg = objReportDAL.AttendanceProcessPIN(objAttendenceReportModel);
            return strMsg;

        }

        public string AttendanceProcessPOut(AttendenceReportModel objAttendenceReportModel)
        {

            string strMsg = objReportDAL.AttendanceProcessPOut(objAttendenceReportModel);
            return strMsg;

        }

        #endregion


        [HttpPost]
        [ValidateAntiForgeryToken]


        public ActionResult AttendanceReport(AttendenceReportModel objAttendenceReportModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objAttendenceReportModel.UpdateBy = strEmployeeId;
                objAttendenceReportModel.HeadOfficeId = strHeadOfficeId;
                objAttendenceReportModel.BranchOfficeId = strBranchOfficeId;

                if (objAttendenceReportModel.ReportFor == "1")
                {
                    GenerateDailyAttendanceReport(objAttendenceReportModel);
                }
                else if (objAttendenceReportModel.ReportFor == "2")
                {
                    GenerateDailyAttendanceMissingReport(objAttendenceReportModel);
                }

                else if (objAttendenceReportModel.ReportFor == "3")
                {
                    GenerateMonthlyAttendanceReport2(objAttendenceReportModel);
                }

                else if(objAttendenceReportModel.ReportFor == "4")
                {
                    GenerateLateAttendanceReport(objAttendenceReportModel);
                }

                else if (objAttendenceReportModel.ReportFor == "5")
                {
                    GenerateAbseAttendanceReport(objAttendenceReportModel);
                }

                else if (objAttendenceReportModel.ReportFor == "6")
                {
                    IndividualAttendanceProcess(objAttendenceReportModel);
                    GenerateIndividualAttendanceReport(objAttendenceReportModel);
                }


                else if (objAttendenceReportModel.ReportFor == "7")
                {
                    GenerateIndividualLateReport(objAttendenceReportModel);
                }

                else if (objAttendenceReportModel.ReportFor == "8")
                {
                    GenerateIndividualRoastingReport(objAttendenceReportModel);
                }

                else if (objAttendenceReportModel.ReportFor == "9")
                {
                    GenerateAttendanceHistoryReport(objAttendenceReportModel);
                }

                else if (objAttendenceReportModel.ReportFor == "10")
                {
                    AttendanceProcessPIN(objAttendenceReportModel);
                    GeneratePunctualReport(objAttendenceReportModel);
                }

                else if (objAttendenceReportModel.ReportFor == "11")
                {
                    AttendanceProcessPOut(objAttendenceReportModel);
                    GeneratePunctualOutReport(objAttendenceReportModel);
                }

                if (objAttendenceReportModel.ReportFor == "12")
                {
                    AttendanceProcessTopLateEmployee(objAttendenceReportModel);
                    GenerateTopLateReport(objAttendenceReportModel);
                }


                else if (objAttendenceReportModel.ReportFor == "13")
                {
                    AttendanceProcessEarlyOut(objAttendenceReportModel);
                    GenerateTopEarlyOutReport(objAttendenceReportModel);
                }


                else if (objAttendenceReportModel.ReportFor == "14")
                {
                    AttendanceProcessHigestDutyTime(objAttendenceReportModel);
                    GenerateAverageDutyReport(objAttendenceReportModel);
                }


                else if (objAttendenceReportModel.ReportFor == "15")
                {
                    AttendanceSummaryProcess(objAttendenceReportModel);
                    GenerateAttendanceSummaryReport(objAttendenceReportModel);
                }


                else if (objAttendenceReportModel.ReportFor == "16")
                {
                    GenerateAttendanceLogReport(objAttendenceReportModel);
                }

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                ViewBag.EmployeeTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetEmployeeTypeDDList(strHeadOfficeId, strBranchOfficeId), "EMPLOYEE_TYPE_ID", "EMPLOYEE_TYPE_NAME");

                return View(objAttendenceReportModel);
            }
        }
        private void GenerateDailyAttendanceReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptDailyAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.DailyAttendanceSheet(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Daily Attendance");

            //return RedirectToAction("AttendanceReport", "Report");
        }
        private void GenerateDailyAttendanceMissingReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptDailyAttendanceMissingSheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.DailyAttendanceMissingSheet(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Daily Attendance Missing");
        }
        private void GenerateMonthlyAttendanceReport2(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptMonthlyAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.MonthlyAttendanceSheet(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Monthly Attendance Missing");
        }
        private void GenerateLateAttendanceReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptDailyLateSheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.DailyLateSheet(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Late Attendance Sheet");
        }
        private void GenerateAbseAttendanceReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptDailyAbsentSheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.DailyAbsentSheet(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Absent Attendance Sheet");
        }
        private void GenerateIndividualAttendanceReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.IndividualAttendanceSheet(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Individual Attendance Sheet");
        }
        private void GenerateIndividualLateReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualLateSheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.IndividualLateSheet(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Individual Late Sheet");
        }
        private void GenerateIndividualRoastingReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptDutyRoastingSheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.IndividualDutyRoastingSheet(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Individual Roasting  Sheet");
        }
        private void GenerateAttendanceHistoryReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptAttendanceDetail.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.DetailAttendanceSheet(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Attendance History Detail Report");
        }
        private void GeneratePunctualReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptPuntualReportIn.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.PunctualAttendanceInTime(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Punctual Report");
        }
        private void GeneratePunctualOutReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptPuntualReportOut.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.PunctualAttendanceOutTime(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Punctual Out Report");
        }
        private void GenerateTopLateReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptTopLateSheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.TopLateSheet(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Top Late Report");
        }
        private void GenerateTopEarlyOutReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptEarlyOutTopSheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.TopEarlyOutSheet(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Top Early Out Report");
        }
        private void GenerateAverageDutyReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptTopHigestDutySheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.TopHigestDutySheet(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Average Duty Report");
        }
        private void GenerateAttendanceSummaryReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptAttendanceSummery.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.AttendanceSummary(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Attendance Summary Report");
        }
        private void GenerateAttendanceLogReport(AttendenceReportModel objAttendenceReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptAttendanceLogSheet.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.AttendenceLogHistory(objAttendenceReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objAttendenceReportModel.ReportType, "Attendance Log History Report");
        }
        #endregion


        #region "User Defined Function for Employee Report"

        public string IndividualLeaveProess(EmployeeReportModel objEmployeeReportModel)
        {

            string strMsg = objReportDAL.IndividualLeaveProess(objEmployeeReportModel);
            return strMsg;
        }


        #endregion

        #region Employee Report


        public void LoadEmployeeReportDownList()
        {
            ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
            ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthDDList(), "MONTH_ID", "MONTH_NAME");
            ViewBag.GenderDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetGenderId(), "GENDER_ID", "GENDER_NAME");
        }
        public ActionResult EmployeeReport()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                LoadEmployeeReportDownList();


                EmployeeReportModel objEmployeeReportModel = new EmployeeReportModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                return View(objEmployeeReportModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult EmployeeReport(EmployeeReportModel objEmployeeReportModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                LoadEmployeeReportDownList();

                objEmployeeReportModel.UpdateBy = strEmployeeId;
                objEmployeeReportModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeReportModel.BranchOfficeId = strBranchOfficeId;

                if (objEmployeeReportModel.ReportFor == "1")
                {
                    GenerateEmployeeBasicInformationReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "2")
                {
                    GenerateEmployeeMaleFemaleInformationReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "3")
                {
                    GenerateEmployeeSlipInformationReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "4")
                {
                    GenerateEmployeeDetailInformationReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "5")
                {
                    GenerateEmployeeCvInformationReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "6")
                {
                    GenerateEmployeeJobConfirmationReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "7")
                {
                    GenerateEmployeeActiveInformationReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "8")
                {
                    GenerateEmployeeInActiveInformationReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "9")
                {
                    GenerateEmployeeSalaryInformationReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "10")
                {
                    IndividualLeaveProess(objEmployeeReportModel);
                    GenerateEmployeeLeaveInformationReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "11")
                {
                    GenerateEmployeeLeaveSummaryReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "12")
                {
                    GenerateEmployeeDailyLeaveReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "13")
                {
                    GenerateEmployeeJoiningReport(objEmployeeReportModel);
                }
                else if (objEmployeeReportModel.ReportFor == "14")
                {
                    GenerateEmployeeJobYearReport(objEmployeeReportModel);
                }
                return View(objEmployeeReportModel);
            }
        }


        private void GenerateEmployeeBasicInformationReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptEmployeeBasicInformation.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.EmployeeBasicInformaiton(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Basic Information Report");
        }
        private void GenerateEmployeeMaleFemaleInformationReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptMaleFemaleInformation.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.MaleFemaleInformaiton(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Male Female Information Report");
        }
        private void GenerateEmployeeSlipInformationReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptEmployeeSlip.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.EmployeePaySlip(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Pay Slip Information Report");
        }
        private void GenerateEmployeeDetailInformationReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptEmployeeDetailInfo.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.EmployeeDetailInformaiton(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Detail Information Report");
        }


        //cv not include DAL & report
        private void GenerateEmployeeCvInformationReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath(""));
            objReportDocument.Load(strPath);


            DataSet objDataSet = (objReportDAL.EmployeeDetailInformaiton(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee CV Information Report");
        }



        private void GenerateEmployeeJobConfirmationReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptEmployeeJobConfirmationDetails.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.EmployeeJobConfirmationInformation(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Job Confirmation Report");
        }
        private void GenerateEmployeeActiveInformationReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptActiveEmployeeList.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.EmployeeActiveList(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Active Information Report");
        }
        private void GenerateEmployeeInActiveInformationReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptInActiveEmployeeList.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.EmployeeInActiveList(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Inactive Information Report");
        }
        private void GenerateEmployeeSalaryInformationReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptEmployeeJobSalary.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.EmployeeSalaryInformaiton(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Salary Information Report");
        }
        private void GenerateEmployeeLeaveInformationReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptLeaveIndividual.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.LeaveIndividual(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Leave Information Report");
        }
        private void GenerateEmployeeLeaveSummaryReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptLeaveSummary.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.LeaveSummeary(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Leave Summary Report");
        }
        private void GenerateEmployeeDailyLeaveReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptDailyLeaveEntry.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.DailyLeaveEntry(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Daily Leave Report");
        }
        private void GenerateEmployeeJoiningReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptActiveEmployeeList.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.EmployeeJoiningDateInformation(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Joining Information Report");
        }
        private void GenerateEmployeeJobYearReport(EmployeeReportModel objEmployeeReportModel)
        {
            string strPath = Path.Combine(Server.MapPath("~/Reports/rptEmployeeJobYearHistory.rpt"));
            objReportDocument.Load(strPath);

            DataSet objDataSet = (objReportDAL.EmployeeJobYearHistory(objEmployeeReportModel));


            objReportDocument.Load(strPath);
            objReportDocument.SetDataSource(objDataSet);
            objReportDocument.SetDatabaseLogon("erp", "erp");

            ShowReport(objEmployeeReportModel.ReportType, "Employee Job Year Information Report");
        }


        #endregion




        #region Employee Attendance Report 

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

                ReportModel objReportModel = new ReportModel
                {
                    FromDate = form["FromDate"],
                    ToDate = form["ToDate"],
                    UnitId = form["UnitId"],
                    DepartmentId = form["DepartmentId"]
                };

                if (objReportModel.UnitId == "")
                {
                    objReportModel.UnitId = null;
                }

                if (objReportModel.DepartmentId == "")
                {
                    objReportModel.DepartmentId = null;
                }

                objReportModel.SectionId = form["SectionId"];
                if (objReportModel.SectionId == "")
                {
                    objReportModel.SectionId = null;
                }

                objReportModel.SubSectionId = form["SubSectionId"];
                if (objReportModel.SubSectionId == "")
                {
                    objReportModel.SubSectionId = null;
                }
                objReportModel.ReportType = form["ReportType"];

                objReportModel.EmployeeId = form["EmployeeId"];
                if (objReportModel.EmployeeId == "")
                {
                    objReportModel.EmployeeId = null;
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

                ReportModel objReportModel = new ReportModel
                {
                    FromDate = form["FromDate"],
                    ToDate = form["ToDate"],
                    UnitId = form["UnitId"],
                    DepartmentId = form["DepartmentId"],
                    SectionId = form["SectionId"],
                    SubSectionId = form["SubSectionId"],
                    ReportType = form["ReportType"],

                    EmployeeId = form["EmployeeId"]
                };
                objReportModel.EmployeeId = form["EmployeeId"];

                if (objReportModel.EmployeeId == "")
                {
                    objReportModel.EmployeeId = null;
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

                ReportModel objReportModel = new ReportModel
                {
                    FromDate = form["FromDate"],
                    ToDate = form["ToDate"],
                    UnitId = form["UnitId"],
                    DepartmentId = form["DepartmentId"],
                    SectionId = form["SectionId"],
                    SubSectionId = form["SubSectionId"],
                    ReportType = form["ReportType"],

                    EmployeeId = form["EmployeeId"]
                };
                objReportModel.EmployeeId = form["EmployeeId"];
                if (objReportModel.EmployeeId == "")
                {
                    objReportModel.EmployeeId = null;
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

                ReportModel objReportModel = new ReportModel
                {
                    FromDate = form["FromDate"],
                    ToDate = form["ToDate"],
                    UnitId = form["UnitId"],
                    DepartmentId = form["DepartmentId"],
                    SectionId = form["SectionId"],
                    SubSectionId = form["SubSectionId"],
                    ReportType = form["ReportType"],

                    EmployeeId = form["EmployeeId"]
                };
                objReportModel.EmployeeId = form["EmployeeId"];
                if (objReportModel.EmployeeId == "")
                {
                    objReportModel.EmployeeId = null;
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

                ReportModel objReportModel = new ReportModel
                {
                    FromDate = form["FromDate"],
                    ToDate = form["ToDate"],
                    UnitId = form["UnitId"],
                    DepartmentId = form["DepartmentId"],
                    SectionId = form["SectionId"],
                    SubSectionId = form["SubSectionId"],
                    ReportType = form["ReportType"],

                    EmployeeId = form["EmployeeId"]
                };
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


        #region Increment Report

        public ActionResult IncrementReport(IncrementEntryModel objIncrementModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                objIncrementModel.HeadOfficeId = strHeadOfficeId;
                objIncrementModel.BranchOfficeId = strBranchOfficeId;
               

                string strPath = Path.Combine(Server.MapPath("~/Reports/rptIncrementSheet.rpt"));
                objReportDocument.Load(strPath);

                DataSet objDataSet = (objReportDAL.IncrementReport(objIncrementModel));

                //string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptIndividualAttendanceSheet.rpt"));
                objReportDocument.Load(strPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport(objIncrementModel.ReportType, "IncrementReport");

                return null;
            }
        }

        #endregion
    }
}