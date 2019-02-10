using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.DAL;
using ERP.MODEL;
using ERP.Utility;

namespace ERP.Controllers
{
    public class SalaryController : Controller
    {

        #region Common

        LookUpDAL objLookUpDAL = new LookUpDAL();
        SalaryDAL objSalaryDAL = new SalaryDAL();

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

        #endregion

        #region Report

        ReportDAL objReportDAL = new ReportDAL();
        ReportDocument objReportDocument = new ReportDocument();
        ExportFormatType objExportFormatType = ExportFormatType.NoFormat;

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

        #region Working Day

        [HttpGet]
        public ActionResult WorkingDayEntry()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                WorkingDayModel objWorkingDayModel = new WorkingDayModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");

                objWorkingDayModel.SalaryYear = DateTime.Now.ToString("yyyy");
                return View(objWorkingDayModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WorkingDayEntry(WorkingDayModel objWorkingDayModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objWorkingDayModel.UpdateBy = strEmployeeId;
                objWorkingDayModel.HeadOfficeId = strHeadOfficeId;
                objWorkingDayModel.BranchOfficeId = strBranchOfficeId;

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                if (objWorkingDayModel.UnitId != null)
                {
                    if (objWorkingDayModel.DepartmentId != null) //check dept id is selected or not,  if not then bypass to else
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objWorkingDayModel.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objWorkingDayModel.DepartmentId);

                        if (objWorkingDayModel.SectionId != null) //check section id is selected or not, if not then bypass to else 
                        {

                            ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objWorkingDayModel.DepartmentId,
                                strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objWorkingDayModel.SectionId);

                            if (objWorkingDayModel.SubSectionId != null)//check sub-section id is selected or not, if not then bypass to else 
                            {

                                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objWorkingDayModel.SectionId,
                                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objWorkingDayModel.SubSectionId);

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
                ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");


                string vMessage = objSalaryDAL.AddCOStaffRecordForMonthlySalary(objWorkingDayModel);

                if (vMessage.Length > 30)
                {
                    TempData["OperationMessage"] = vMessage;
                    objWorkingDayModel.WorkingDayList = null;
                }
                else
                {
                    objWorkingDayModel.WorkingDayList = objSalaryDAL.GetAllCOEmployeesForMonthlySalary(objWorkingDayModel);
                }

                return View(objWorkingDayModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CalculateAttendance(WorkingDayModel objWorkingDayModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objWorkingDayModel.UpdateBy = strEmployeeId;
                objWorkingDayModel.HeadOfficeId = strHeadOfficeId;
                objWorkingDayModel.BranchOfficeId = strBranchOfficeId;

                TempData["OperationMessage"] = objSalaryDAL.CalculateWorkingDayCOForMonthlySalary(objWorkingDayModel);

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objWorkingDayModel.UnitId,
                    strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objWorkingDayModel.DepartmentId);

                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objWorkingDayModel.DepartmentId,
                    strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objWorkingDayModel.SectionId);

                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objWorkingDayModel.SectionId,
                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objWorkingDayModel.SubSectionId);

                ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");


                objWorkingDayModel.WorkingDayList = objSalaryDAL.GetAllCOEmployeesForMonthlySalary(objWorkingDayModel);

                return View("~/Views/Salary/WorkingDayEntry.cshtml", objWorkingDayModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateAttendance(WorkingDayModel objWorkingDayModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objWorkingDayModel.UpdateBy = strEmployeeId;
                objWorkingDayModel.HeadOfficeId = strHeadOfficeId;
                objWorkingDayModel.BranchOfficeId = strBranchOfficeId;

                TempData["OperationMessage"] = objSalaryDAL.UpdateWorkingDayCOForMonthlySalary(objWorkingDayModel);

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objWorkingDayModel.UnitId,
                    strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objWorkingDayModel.DepartmentId);

                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objWorkingDayModel.DepartmentId,
                    strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objWorkingDayModel.SectionId);

                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objWorkingDayModel.SectionId,
                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objWorkingDayModel.SubSectionId);

                ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");


                objWorkingDayModel.WorkingDayList = objSalaryDAL.GetAllCOEmployeesForMonthlySalary(objWorkingDayModel);

                return View("~/Views/Salary/WorkingDayEntry.cshtml", objWorkingDayModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ViewWorkingDayReport(WorkingDayModel objWorkingDayModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objWorkingDayModel.UpdateBy = strEmployeeId;
                objWorkingDayModel.HeadOfficeId = strHeadOfficeId;
                objWorkingDayModel.BranchOfficeId = strBranchOfficeId;

                DataSet objDataSet = objReportDAL.GetCOMonthlyWorkingDayData(objWorkingDayModel);

                string vReportPath = Path.Combine(Server.MapPath("~/Reports/rptWorkingDayListCO.rpt"));
                objReportDocument.Load(vReportPath);
                objReportDocument.SetDataSource(objDataSet);
                objReportDocument.SetDatabaseLogon("erp", "erp");

                ShowReport("PDF", "Working Day Report");

                return null;
            }
        }

        #endregion

        #region Salary AAT

        [HttpGet]
        public ActionResult SalaryAATCOEntry()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                WorkingDayModel objWorkingDayModel = new WorkingDayModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");

                return View(objWorkingDayModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalaryAATCOEntry(WorkingDayModel objWorkingDayModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objWorkingDayModel.UpdateBy = strEmployeeId;
                objWorkingDayModel.HeadOfficeId = strHeadOfficeId;
                objWorkingDayModel.BranchOfficeId = strBranchOfficeId;

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                if (objWorkingDayModel.UnitId != null)
                {
                    if (objWorkingDayModel.DepartmentId != null) //check dept id is selected or not,  if not then bypass to else
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objWorkingDayModel.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objWorkingDayModel.DepartmentId);

                        if (objWorkingDayModel.SectionId != null) //check section id is selected or not, if not then bypass to else 
                        {

                            ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objWorkingDayModel.DepartmentId,
                                strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objWorkingDayModel.SectionId);

                            if (objWorkingDayModel.SubSectionId != null)//check sub-section id is selected or not, if not then bypass to else 
                            {

                                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objWorkingDayModel.SectionId,
                                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objWorkingDayModel.SubSectionId);

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

                ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");

                objSalaryDAL.AddCOStaffRecordForAAT(objWorkingDayModel);

                objWorkingDayModel.WorkingDayList = objSalaryDAL.GetAllCOEmployeesForAAT(objWorkingDayModel);

                return View(objWorkingDayModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateSalaryAATCO(WorkingDayModel objWorkingDayModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objWorkingDayModel.UpdateBy = strEmployeeId;
                objWorkingDayModel.HeadOfficeId = strHeadOfficeId;
                objWorkingDayModel.BranchOfficeId = strBranchOfficeId;

                TempData["OperationMessage"] = objSalaryDAL.UpdateSalaryAATInfoCO(objWorkingDayModel);

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                if (objWorkingDayModel.UnitId != null)
                {
                    if (objWorkingDayModel.DepartmentId != null) //check dept id is selected or not,  if not then bypass to else
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objWorkingDayModel.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objWorkingDayModel.DepartmentId);

                        if (objWorkingDayModel.SectionId != null) //check section id is selected or not, if not then bypass to else 
                        {

                            ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objWorkingDayModel.DepartmentId,
                                strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objWorkingDayModel.SectionId);

                            if (objWorkingDayModel.SubSectionId != null)//check sub-section id is selected or not, if not then bypass to else 
                            {

                                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objWorkingDayModel.SectionId,
                                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objWorkingDayModel.SubSectionId);
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


                objWorkingDayModel.WorkingDayList = objSalaryDAL.GetAllCOEmployeesForAAT(objWorkingDayModel);

                return View("~/Views/Salary/SalaryAATCOEntry.cshtml", objWorkingDayModel);
            }
        }


        #endregion

        #region Salary Process



        #endregion

        #region Employee Earn Leave 

        [HttpGet]
        public ActionResult EmployeeEarnLeave()
        {

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                EmployeeEarnLeaveModel objEarnLeaveModel = new EmployeeEarnLeaveModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,

                    SalaryYear = objLookUpDAL.GetELLimitDate().LeaveYear,
                    Date = objLookUpDAL.GetELLimitDate().LimitDate,
                    EmployeeEarnLeaveList = new List<EmployeeEarnLeaveModel>()
                };

                objEarnLeaveModel.UnitId = Convert.ToString(TempData["UnitId"]);
                if (objEarnLeaveModel.UnitId == "")
                {
                    objEarnLeaveModel.UnitId = null;
                }

                if (objEarnLeaveModel.UnitId != null)
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    objEarnLeaveModel.DepartmentId = Convert.ToString(TempData["DepartmentId"]);
                    //
                    // 

                    if (objEarnLeaveModel.DepartmentId == "")
                    {
                        objEarnLeaveModel.DepartmentId = null;
                    }
                    if (objEarnLeaveModel.DepartmentId != null)
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objEarnLeaveModel.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objEarnLeaveModel.DepartmentId);
                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }

                    objEarnLeaveModel.SectionId = Convert.ToString(TempData["SectionId"]);
                    if (objEarnLeaveModel.SectionId == "")
                    {
                        objEarnLeaveModel.SectionId = null;
                    }
                    if (objEarnLeaveModel.SectionId != null)
                    {
                        ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objEarnLeaveModel.DepartmentId,
                            strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objEarnLeaveModel.SectionId);
                    }
                    else
                    {
                        ViewBag.SectionDDList = null;
                    }


                    objEarnLeaveModel.SubSectionId = Convert.ToString(TempData["SubSectionId"]);
                    if (objEarnLeaveModel.SubSectionId == "")
                    {
                        objEarnLeaveModel.SubSectionId = null;
                    }
                    if (objEarnLeaveModel.SubSectionId != null)
                    {
                        ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objEarnLeaveModel.SectionId,
                            strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objEarnLeaveModel.SubSectionId);
                    }
                    else
                    {
                        ViewBag.SubSectionDDList = null;
                    }

                    objEarnLeaveModel.EmployeeEarnLeaveList = objSalaryDAL.GetEarnLeaveRecord(objEarnLeaveModel);
                }
                else
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                }


                //ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                return View(objEarnLeaveModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeEarnLeave(EmployeeEarnLeaveModel objEarnLeaveModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objEarnLeaveModel.UpdateBy = strEmployeeId;
                objEarnLeaveModel.HeadOfficeId = strHeadOfficeId;
                objEarnLeaveModel.BranchOfficeId = strBranchOfficeId;

                objEarnLeaveModel.SalaryYear = objLookUpDAL.GetELLimitDate().LeaveYear;
                objEarnLeaveModel.Date = objLookUpDAL.GetELLimitDate().LimitDate;

                if (objEarnLeaveModel.UnitId != null)
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    if (objEarnLeaveModel.DepartmentId != null)
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objEarnLeaveModel.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objEarnLeaveModel.DepartmentId);
                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }

                    if (objEarnLeaveModel.SectionId != null)
                    {
                        ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objEarnLeaveModel.DepartmentId,
                            strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objEarnLeaveModel.SectionId);
                    }
                    else
                    {
                        ViewBag.SectionDDList = null;
                    }

                    if (objEarnLeaveModel.SubSectionId != null)
                    {
                        ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objEarnLeaveModel.SectionId,
                            strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objEarnLeaveModel.SubSectionId);
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

                objSalaryDAL.AddEarnLeaveEmployee(objEarnLeaveModel);  // fetch employee data before process---

                objEarnLeaveModel.EmployeeEarnLeaveList = objSalaryDAL.GetEarnLeaveRecord(objEarnLeaveModel);


                //ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                return View(objEarnLeaveModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessEmployeeEarnLeave(EmployeeEarnLeaveModel objEarnLeaveModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objEarnLeaveModel.UpdateBy = strEmployeeId;
                objEarnLeaveModel.HeadOfficeId = strHeadOfficeId;
                objEarnLeaveModel.BranchOfficeId = strBranchOfficeId;

                objEarnLeaveModel.SalaryYear = objLookUpDAL.GetELLimitDate().LeaveYear;
                objEarnLeaveModel.Date = objLookUpDAL.GetELLimitDate().LimitDate;

                string vMessage = "";
                vMessage = objSalaryDAL.EarnLeaveProcess(objEarnLeaveModel);  // process data
                TempData["OperationMessage"] = vMessage;

                TempData["UnitId"] = objEarnLeaveModel.UnitId;
                TempData["DepartmentId"] = objEarnLeaveModel.DepartmentId;
                TempData["SectionId"] = objEarnLeaveModel.SectionId;
                TempData["SubSectionId"] = objEarnLeaveModel.SubSectionId;

                return RedirectToAction("EmployeeEarnLeave");
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmployeeEarnLeave(EmployeeEarnLeaveModel objEarnLeaveModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objEarnLeaveModel.UpdateBy = strEmployeeId;
                objEarnLeaveModel.HeadOfficeId = strHeadOfficeId;
                objEarnLeaveModel.BranchOfficeId = strBranchOfficeId;

                objEarnLeaveModel.SalaryYear = objLookUpDAL.GetELLimitDate().LeaveYear;
                objEarnLeaveModel.Date = objLookUpDAL.GetELLimitDate().LimitDate;

                string vMessage = "";
                if (objEarnLeaveModel.EmployeeEarnLeaveList != null)
                {
                    vMessage = objSalaryDAL.SaveEarnLearnInfo(objEarnLeaveModel);
                }
                TempData["OperationMessage"] = vMessage;

                TempData["UnitId"] = objEarnLeaveModel.UnitId;
                TempData["DepartmentId"] = objEarnLeaveModel.DepartmentId;
                TempData["SectionId"] = objEarnLeaveModel.SectionId;
                TempData["SubSectionId"] = objEarnLeaveModel.SubSectionId;

                return RedirectToAction("EmployeeEarnLeave");
            }
        }

        #endregion
		
		
	    #region ADVANCE ENTRY

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdvanceLoanRecordSave(LoanAdvanceModel objLoanAdvanceModel)
        {
            LoadSession();

            //objLeaveRequestModel.EmployeeId = objLeaveRequestModel.EmployeeId;
            objLoanAdvanceModel.UpdateBy = strEmployeeId;
            objLoanAdvanceModel.HeadOfficeId = strHeadOfficeId;
            objLoanAdvanceModel.BranchOfficeId = strBranchOfficeId;
            
            if (ModelState.IsValid)
            {
                string strDBMsg = objSalaryDAL.AdvanceLoanRecordSave(objLoanAdvanceModel);
                TempData["OperationMessage"] = strDBMsg;

            }

            return RedirectToAction("LoanAdvanceEntry", objLoanAdvanceModel);
            //return View("LeaveRequest");
        }



        [HttpGet]
        public ActionResult LoanAdvanceEntry(LoanAdvanceModel objLoanAdvanceModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {

                LoadSession();

                objLoanAdvanceModel.UpdateBy = strEmployeeId;
                objLoanAdvanceModel.HeadOfficeId = strHeadOfficeId;
                objLoanAdvanceModel.BranchOfficeId = strBranchOfficeId;
                objLoanAdvanceModel.EmployeeId = objLoanAdvanceModel.EmployeeId;

                objLoanAdvanceModel.Year = DateTime.Now.ToString("yyyy");




                ViewBag.LoadAdvanceRecord = objSalaryDAL.LoadAdvanceRecord(objLoanAdvanceModel);
                ViewBag.GetEmployeeRecordById = objSalaryDAL.GetEmployeeRecordById(objLoanAdvanceModel);
               // ViewBag.GetMonth = objSalaryDAL.GetMonth(objLoanAdvanceModel);

                objLoanAdvanceModel.EmployeeModel = objSalaryDAL.GetEmployeeRecordById(objLoanAdvanceModel).EmployeeModel;

                ViewBag.MonthList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthDDList(), "MONTH_ID", "MONTH_NAME");

                //ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);



                //if (!(objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel)).Any())
                //{
                //    objLeaveRequestDal.IndividualLeaveProessTemp(objLeaveRequestModel);
                //    ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryTempRecord(objLeaveRequestModel);

                //}

                //LoadDropDownList();




                return View("LoanAdvanceEntry", objLoanAdvanceModel);
            }
        }



        #endregion

        #region PF ADVANCE ENTRY

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdvancePFRecordSave(PFAdvanceModel objPfAdvanceModel)
        {
            LoadSession();

            //objLeaveRequestModel.EmployeeId = objLeaveRequestModel.EmployeeId;
            objPfAdvanceModel.UpdateBy = strEmployeeId;
            objPfAdvanceModel.HeadOfficeId = strHeadOfficeId;
            objPfAdvanceModel.BranchOfficeId = strBranchOfficeId;



            if (ModelState.IsValid)
            {
                string strDBMsg = objSalaryDAL.AdvancePFRecordSave(objPfAdvanceModel);
                TempData["OperationMessage"] = strDBMsg;

            }

            return RedirectToAction("PFAdvanceEntry", objPfAdvanceModel);
            //return View("LeaveRequest");
        }



        [HttpGet]
        public ActionResult PFAdvanceEntry(PFAdvanceModel objPfAdvanceModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {

                LoadSession();

                objPfAdvanceModel.UpdateBy = strEmployeeId;
                objPfAdvanceModel.HeadOfficeId = strHeadOfficeId;
                objPfAdvanceModel.BranchOfficeId = strBranchOfficeId;
                objPfAdvanceModel.EmployeeId = objPfAdvanceModel.EmployeeId;

                objPfAdvanceModel.Year = DateTime.Now.ToString("yyyy");

                ViewBag.LoadAdvanceRecord = objSalaryDAL.LoadPFAdvanceRecord(objPfAdvanceModel);
                ViewBag.GetEmployeeRecordById = objSalaryDAL.PFGetEmployeeRecordById(objPfAdvanceModel);
              
                objPfAdvanceModel.EmployeeModel = objSalaryDAL.PFGetEmployeeRecordById(objPfAdvanceModel).EmployeeModel;

                ViewBag.MonthList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthDDList(), "MONTH_ID", "MONTH_NAME");

            


                return View("PFAdvanceEntry", objPfAdvanceModel);
            }
        }



        #endregion


        //[HttpGet]
        //public ActionResult AddEmployeeSalary()
        //{
        //    if (Session["strEmployeeId"] == null)
        //    {
        //        return RedirectToAction("LogOut", "Login");
        //    }
        //    else
        //    {
        //        LoadSession();

        //        EmployeeAddSalary objEmployeeAddSalary = new EmployeeAddSalary
        //        {
        //            UpdateBy = strEmployeeId,
        //            HeadOfficeId = strHeadOfficeId,
        //            BranchOfficeId = strBranchOfficeId
        //        };

        //        ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
        //        //ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");

        //        return View(objEmployeeAddSalary);
        //    }
        //}

        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult AddEmployeeSalary(EmployeeAddSalary objEmployeeAddSalary)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objEmployeeAddSalary.UpdateBy = strEmployeeId;
                objEmployeeAddSalary.HeadOfficeId = strHeadOfficeId;
                objEmployeeAddSalary.BranchOfficeId = strBranchOfficeId;

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objEmployeeAddSalary.UnitId,
                    strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objEmployeeAddSalary.DepartmentId);

                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objEmployeeAddSalary.DepartmentId,
                    strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objEmployeeAddSalary.SectionId);

                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objEmployeeAddSalary.SectionId,
                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objEmployeeAddSalary.SubSectionId);

                ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");


                //string vMessage = objSalaryDAL.SaveEmpGrossSalary(objEmployeeAddSalary);

                //if (vMessage.Length > 30)
                //{
                //    TempData["OperationMessage"] = vMessage;
                //    objEmployeeAddSalary.EmployeeAddSalaryList = null;
                //}
                //else
                //{
                    objEmployeeAddSalary.EmployeeAddSalaryList = objSalaryDAL.LoadSalaryRecordIsNull(objEmployeeAddSalary);
               // }

                return View("AddEmployeeSalary",objEmployeeAddSalary);
            }
        }



        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UpdateSalary(EmployeeAddSalary objEmployeeAddSalary)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objEmployeeAddSalary.UpdateBy = strEmployeeId;
                objEmployeeAddSalary.HeadOfficeId = strHeadOfficeId;
                objEmployeeAddSalary.BranchOfficeId = strBranchOfficeId;

                //TempData["OperationMessage"] = objSalaryDAL.UpdateWorkingDayCOForMonthlySalary(objEmployeeAddSalary);

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objEmployeeAddSalary.UnitId,
                    strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objEmployeeAddSalary.DepartmentId);

                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objEmployeeAddSalary.DepartmentId,
                    strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objEmployeeAddSalary.SectionId);

                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objEmployeeAddSalary.SectionId,
                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objEmployeeAddSalary.SubSectionId);

                ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");

                TempData["OperationMessage"] = objSalaryDAL.SaveEmpGrossSalary(objEmployeeAddSalary);
                objEmployeeAddSalary.EmployeeAddSalaryList = objSalaryDAL.LoadSalaryRecordIsNull(objEmployeeAddSalary);

                return View("~/Views/Salary/AddEmployeeSalary.cshtml", objEmployeeAddSalary);
            }
        }





        //SALARY PROCESS CO
        //SALARY PROCESS CO
        [HttpGet]
        //[ValidateAntiForgeryToken]
        public ActionResult SalaryProcessCO(SalaryProcessCOModel objSalaryProcessCoModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                if (objSalaryProcessCoModel.SalaryYear == null)
                {
                    objSalaryProcessCoModel.SalaryYear = DateTime.Now.ToString("yyyy");

                }

                objSalaryProcessCoModel.UpdateBy = strEmployeeId;
                objSalaryProcessCoModel.HeadOfficeId = strHeadOfficeId;
                objSalaryProcessCoModel.BranchOfficeId = strBranchOfficeId;
                //objSalaryProcessCoModel.EmployeeId = strEmployeeId;

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objSalaryProcessCoModel.UnitId,
                    strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objSalaryProcessCoModel.DepartmentId);

                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objSalaryProcessCoModel.DepartmentId,
                    strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objSalaryProcessCoModel.SectionId);

                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objSalaryProcessCoModel.SectionId,
                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objSalaryProcessCoModel.SubSectionId);

                //ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");

                ViewBag.MonthList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");

                //string vMessage = objSalaryDAL.SaveEmpGrossSalary(objEmployeeAddSalary);

                //if (vMessage.Length > 30)
                //{
                //    TempData["OperationMessage"] = vMessage;
                //    objEmployeeAddSalary.EmployeeAddSalaryList = null;
                //}
                //else
                //{
                objSalaryProcessCoModel.SalaryProcessCOList = objSalaryDAL.LoadCORecordForSalary(objSalaryProcessCoModel);
                // }

                return View("SalaryProcessCO", objSalaryProcessCoModel);
            }
        }



        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult UpdateSalaryProcessCO(SalaryProcessCOModel objSalaryProcessCoModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objSalaryProcessCoModel.UpdateBy = strEmployeeId;
                objSalaryProcessCoModel.HeadOfficeId = strHeadOfficeId;
                objSalaryProcessCoModel.BranchOfficeId = strBranchOfficeId;
                //if (objSalaryProcessCoModel.SalaryYear == null)
                //{
                //    objSalaryProcessCoModel.SalaryYear = DateTime.Now.ToString("yyyy");

                //}
                //TempData["OperationMessage"] = objSalaryDAL.UpdateWorkingDayCOForMonthlySalary(objEmployeeAddSalary);

                //ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                //ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objSalaryProcessCoModel.UnitId,
                //    strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objSalaryProcessCoModel.DepartmentId);

                //ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objSalaryProcessCoModel.DepartmentId,
                //    strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objSalaryProcessCoModel.SectionId);

                //ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objSalaryProcessCoModel.SectionId,
                //    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objSalaryProcessCoModel.SubSectionId);

                ////ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");
                //ViewBag.MonthList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthSalaryDDList(), "MONTH_ID", "MONTH_NAME");

                TempData["OperationMessage"] = objSalaryDAL.ProcessCOMonthlySalary(objSalaryProcessCoModel);

                //objSalaryProcessCoModel.EmployeeId = strEmployeeId;
                //objSalaryProcessCoModel.SalaryProcessCOList = objSalaryDAL.LoadCORecordForSalary(objSalaryProcessCoModel);

                //return View("~/Views/Salary/SalaryProcessCO.cshtml", objSalaryProcessCoModel);
                return RedirectToAction("SalaryProcessCO", "Salary", objSalaryProcessCoModel);
            }
        }



    }
}