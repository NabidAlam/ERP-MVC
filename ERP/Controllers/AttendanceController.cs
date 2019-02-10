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
using Microsoft.Ajax.Utilities;

namespace ERP.Controllers
{
    public class AttendanceController : Controller
    {
        // GET: Attendance
        public ActionResult Index()
        {
            return View();
        }

        #region Common

        private LookUpDAL objLookUpDAL = new LookUpDAL();
        private AttendanceDAL objAttendanceDal = new AttendanceDAL();
        private readonly ReportDAL objReportDAL = new ReportDAL();
        private readonly ReportDocument objReportDocument = new ReportDocument();
        private readonly ExportFormatType objExportFormatType = ExportFormatType.NoFormat;


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


        #endregion

        #region Employee Attendance

        private AttendanceProcessModel objAttendanceModel = new AttendanceProcessModel();

        [HttpGet]
        public ActionResult GetAttendanceProcessRecord()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objAttendanceModel.UpdateBy = strEmployeeId;
                objAttendanceModel.HeadOfficeId = strHeadOfficeId;
                objAttendanceModel.BranchOfficeId = strBranchOfficeId;

                //objAttendanceModel.UnitId = Convert.ToString(TempData["UnitId"]);
                //objAttendanceModel.DepartmentId = Convert.ToString(TempData["DeptId"]);
                //objAttendanceModel.SectionId = Convert.ToString(TempData["SectionId"]);
                //objAttendanceModel.SubSectionId = Convert.ToString(TempData["SubSectionId"]);


                if (TempData.ContainsKey("SaveAttendanceProcess") && (int)TempData["SaveAttendanceProcess"] == 1)
                {
                    objAttendanceModel.FromDate = Convert.ToString(TempData["FromDate"]);
                    objAttendanceModel.ToDate = Convert.ToString(TempData["ToDate"]);

                    objAttendanceModel.UnitId = Convert.ToString(TempData["UnitId"]);
                    if (objAttendanceModel.UnitId.IsNullOrWhiteSpace())
                    {
                        objAttendanceModel.UnitId = null;
                    }

                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                    objAttendanceModel.DepartmentId = Convert.ToString(TempData["DeptId"]);
                    if (objAttendanceModel.DepartmentId.IsNullOrWhiteSpace())
                    {
                        objAttendanceModel.DepartmentId = null;
                    }

                    objAttendanceModel.SectionId = Convert.ToString(TempData["SectionId"]);
                    if (objAttendanceModel.SectionId.IsNullOrWhiteSpace())
                    {
                        objAttendanceModel.SectionId = null;
                    }

                    objAttendanceModel.SubSectionId = Convert.ToString(TempData["SubSectionId"]);
                    if (objAttendanceModel.SubSectionId.IsNullOrWhiteSpace())
                    {
                        objAttendanceModel.SubSectionId = null;
                    }


                    if (objAttendanceModel.DepartmentId != null)
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objAttendanceModel.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objAttendanceModel.DepartmentId);
                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }


                    if (objAttendanceModel.SectionId != null)
                    {
                        ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objAttendanceModel.DepartmentId,
                            strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objAttendanceModel.SectionId);
                    }
                    else
                    {
                        ViewBag.SectionDDList = null;
                    }


                    if (objAttendanceModel.SubSectionId != null)
                    {
                        ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objAttendanceModel.SectionId,
                            strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objAttendanceModel.SubSectionId);
                    }
                    else
                    {
                        ViewBag.SubSectionDDList = null;
                    }
                }

                else
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    objAttendanceModel.FromDate = objLookUpDAL.getCurrentDate().FromDate;
                    objAttendanceModel.ToDate = objLookUpDAL.getCurrentDate().ToDate;

                }


                return View(objAttendanceModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveAttendanceProcess(AttendanceProcessModel objProcessModel)
        {
            LoadSession();
            objProcessModel.UpdateBy = strEmployeeId;
            objProcessModel.HeadOfficeId = strHeadOfficeId;
            objProcessModel.BranchOfficeId = strBranchOfficeId;

            TempData["UnitId"] = objProcessModel.UnitId;
            TempData["DeptId"] = objProcessModel.DepartmentId;
            TempData["SectionId"] = objProcessModel.SectionId;
            TempData["SubSectionId"] = objProcessModel.SubSectionId;

            TempData["FromDate"] = objProcessModel.FromDate;
            TempData["ToDate"] = objProcessModel.ToDate;

            string strDBMsg = "";

            if (ModelState.IsValid)
            {
               
                strDBMsg = objAttendanceDal.SaveAttendenceProcessDaily(objProcessModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            TempData["SaveAttendanceProcess"] = 1;



             


            return RedirectToAction("GetAttendanceProcessRecord");
        }


        [HttpPost]
        public ActionResult UploadAttendenceFile(FormCollection form)
        {
            string fromDate = form["fromDate"];
            string toDate = form["toDate"];
            string strMsg = "";

            HttpFileCollectionBase files = Request.Files;

            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                string fname;

                strMsg = "got";
                if(strMsg == "got")
                {
                    strMsg = "DATA UPLOADED SUCCESSULLY";

                }
                else
                {
                    strMsg = "did not get";

                }

                // Checking for Internet Explorer  
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;

                    if (file != null && file.ContentLength > 0)
                    {
                        string contentType = file.ContentType;

                        if (System.IO.File.Exists(fname))
                        {
                            System.IO.File.Delete(fname);
                        }

                        string filePath = Server.MapPath("~/DATA_CAPTURE/");
                        //fname = Path.Combine(Server.MapPath("~/DATA_CAPTURE/" + fname));
                        string strPath = Server.MapPath("~/DATA_CAPTURE/" + fname);
                        file.SaveAs(strPath);
                        MoveFile(fname, strPath);
                        AttendenceFileProcess(fname, fromDate, toDate);
                    }
                }
            }

            return Json(strMsg, JsonRequestBehavior.AllowGet);
        }

        private StreamReader objStreamReader;

        public void MoveFile(string strFileName, string strPath)
        {
            try
            {
                string sourceFile = strPath;
                objAttendanceModel = objAttendanceDal.GetDirectoryName(strHeadOfficeId, strBranchOfficeId);

                string strTargetSource = objAttendanceModel.DataUploadDir;

                string destinationFile = objAttendanceModel.DataUploadDir + strFileName;

                if (!Directory.Exists(strTargetSource))
                {
                    Directory.CreateDirectory(strTargetSource);
                }

                // To move a file or folder to a new location:
                //System.IO.File.Move(sourceFile, destinationFile);

                if (System.IO.File.Exists(destinationFile))
                {
                    System.IO.File.Delete(destinationFile);
                    System.IO.File.Copy(sourceFile, destinationFile);
                }
                else
                {
                    System.IO.File.Copy(sourceFile, destinationFile);
                }

                objStreamReader = new StreamReader(strPath, true);
                objStreamReader.Dispose();
                objStreamReader.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            catch (Exception)
            {
                objStreamReader = new StreamReader(strPath, true);
                objStreamReader.Dispose();
                objStreamReader.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }

        }


        public void AttendenceFileProcess(string strFileName, string fromDate, string toDate)
        {
            LoadSession();

            objAttendanceModel.FileName = strFileName;
            objAttendanceModel.FromDate = fromDate;
            objAttendanceModel.ToDate = toDate;

            objAttendanceModel.UpdateBy = strEmployeeId;
            objAttendanceModel.HeadOfficeId = strHeadOfficeId;
            objAttendanceModel.BranchOfficeId = strBranchOfficeId;

            string vMsg = "";
            vMsg =  objAttendanceDal.AttendenceFileProcess(objAttendanceModel);
            TempData["OperationMessage"] = vMsg;
            //Message(vMsg);
        }

        public void Message(string strMsg)
        {
            
        }

        #endregion

        #region Attendance Approval Process


        public List<AttendanceApprovalModel> AttendanceApprovalListData(DataTable dt)
        {
            List<AttendanceApprovalModel> approvalDataBundle = new List<AttendanceApprovalModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                AttendanceApprovalModel objApprovalModel = new AttendanceApprovalModel
                {
                    EmployeeId = dt.Rows[i]["EMPLOYEE_ID"].ToString(),

                    EmployeeName = dt.Rows[i]["EMPLOYEE_NAME"].ToString(),

                    JoiningDate = dt.Rows[i]["JOINING_DATE"].ToString(),

                    DesignationName = dt.Rows[i]["DESIGNATION_NAME"].ToString(),

                    DepartmentName = dt.Rows[i]["DEPARTMENT_NAME"].ToString(),

                    DayType = dt.Rows[i]["DAY_TYPE"].ToString(),

                    LogDate = dt.Rows[i]["log_date"].ToString(),

                    InTime = dt.Rows[i]["FIRST_IN"].ToString(),

                    OutTime = dt.Rows[i]["LAST_OUT"].ToString(),

                    DutyTime = dt.Rows[i]["TOTAL_DUTY_TIME"].ToString(),

                    ApprovalStatus = dt.Rows[i]["APPROVED_STATUS"].ToString(),

                    SerialNumber = dt.Rows[i]["sl"].ToString()
                };

                approvalDataBundle.Add(objApprovalModel);
            }

            return approvalDataBundle;
        }

        public ActionResult EmployeeAttendanceApproval()
        {
            AttendanceApprovalModel objAttendanceApprovalModel = new AttendanceApprovalModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                // objDutyRoasterModel.EmployeeId = strEmployeeId;

                objAttendanceApprovalModel.UpdateBy = strEmployeeId;
                objAttendanceApprovalModel.HeadOfficeId = strHeadOfficeId;
                objAttendanceApprovalModel.BranchOfficeId = strBranchOfficeId;

                if (TempData.ContainsKey("SearchApprovalAttendance") && (int)TempData["SearchApprovalAttendance"] == 1)
                {
                    objAttendanceApprovalModel.ActiveYN = Convert.ToString(TempData["EmployeActive"]);

                    objAttendanceApprovalModel.FromDate = Convert.ToString(TempData["FromDateForRoaster"]);
                    objAttendanceApprovalModel.ToDate = Convert.ToString(TempData["ToDateForRoaster"]);

                    objAttendanceApprovalModel.UnitId = Convert.ToString(TempData["UnitId"]);
                    if (objAttendanceApprovalModel.UnitId.IsNullOrWhiteSpace())
                    {
                        objAttendanceApprovalModel.UnitId = null;
                    }

                    objAttendanceApprovalModel.DepartmentId = Convert.ToString(TempData["DeptId"]);
                    if (objAttendanceApprovalModel.DepartmentId.IsNullOrWhiteSpace())
                    {
                        objAttendanceApprovalModel.DepartmentId = null;
                    }

                    if (objAttendanceApprovalModel.DepartmentId != null)
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(
                            objLookUpDAL.GetDepartmentDDListByUnitId(objAttendanceApprovalModel.UnitId,
                                strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME",
                            objAttendanceApprovalModel.DepartmentId);
                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }

                    objAttendanceApprovalModel.SectionId = Convert.ToString(TempData["SectionId"]);
                    if (objAttendanceApprovalModel.SectionId.IsNullOrWhiteSpace())
                    {
                        objAttendanceApprovalModel.SectionId = null;
                    }

                    if (objAttendanceApprovalModel.SectionId != null)
                    {
                        ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(
                            objLookUpDAL.GetSectionDDListByDepartmentId(objAttendanceApprovalModel.DepartmentId,
                                strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME",
                            objAttendanceApprovalModel.SectionId);
                    }
                    else
                    {
                        ViewBag.SectionDDList = null;
                    }


                    objAttendanceApprovalModel.SubSectionId = Convert.ToString(TempData["SubSectionId"]);
                    if (objAttendanceApprovalModel.SubSectionId.IsNullOrWhiteSpace())
                    {
                        objAttendanceApprovalModel.SubSectionId = null;
                    }

                    if (objAttendanceApprovalModel.SubSectionId != null)
                    {
                        ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(
                            objLookUpDAL.GetSubSectionDDListBySectionId(objAttendanceApprovalModel.SectionId,
                                strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME",
                            objAttendanceApprovalModel.SubSectionId);
                    }
                    else
                    {
                        ViewBag.SubSectionDDList = null;
                    }
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    ViewBag.FromDate = objAttendanceApprovalModel.FromDate; //objLookUpDAL.GetCurrentDate().FromDate;
                    ViewBag.ToDate = objAttendanceApprovalModel.ToDate; //objLookUpDAL.GetCurrentDate().ToDate;

                    DataTable dt = objAttendanceDal.GetEmployeeAttendanceRecordForApproved(objAttendanceApprovalModel);

                    List<AttendanceApprovalModel> approvalList = AttendanceApprovalListData(dt);
                    objAttendanceApprovalModel.ListAttendanceApprovalModels = approvalList;
                }
                else
                {
                    objAttendanceApprovalModel.ListAttendanceApprovalModels = new List<AttendanceApprovalModel>();
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                }

                return View(objAttendanceApprovalModel);
            }
        }


        [HttpPost]
        public ActionResult EmployeeAttendanceApproval(AttendanceApprovalModel objApprovalModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objApprovalModel.UpdateBy = strEmployeeId;
                objApprovalModel.HeadOfficeId = strHeadOfficeId;
                objApprovalModel.BranchOfficeId = strBranchOfficeId;

                if (objApprovalModel.ListAttendanceApprovalModels != null)
                {
                    string strDBMsg = "";
                    objApprovalModel.ListAttendanceApprovalModels.RemoveAll(a => !a.IsChecked);
                    strDBMsg = objAttendanceDal.ProcessAttendanceApproved(objApprovalModel);
                    TempData["OperationMessage"] = strDBMsg;

                    ModelState.Clear();
                }


                objApprovalModel.ActiveYN = objApprovalModel.CheckedYN ? 'N'.ToString() : 'Y'.ToString();


                objApprovalModel.ActiveYN = objApprovalModel.CheckedYN ? 'N'.ToString() : 'Y'.ToString();

                if (objApprovalModel.UnitId != null)
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    if (objApprovalModel.DepartmentId != null)
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objApprovalModel.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objApprovalModel.DepartmentId);

                        if (objApprovalModel.SectionId != null)
                        {
                            ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objApprovalModel.DepartmentId,
                                strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objApprovalModel.SectionId);

                            if (objApprovalModel.SubSectionId != null)
                            {
                                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objApprovalModel.SectionId,
                                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objApprovalModel.SubSectionId);
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
                }
                else
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                }

                DataTable dt = objAttendanceDal.GetEmployeeAttendanceRecordForApproved(objApprovalModel);
                objApprovalModel.ListAttendanceApprovalModels = AttendanceApprovalListData(dt);

                ViewBag.FromDate = objApprovalModel.FromDate; //objLookUpDAL.GetCurrentDate().FromDate;
                ViewBag.ToDate = objApprovalModel.ToDate; //objLookUpDAL.GetCurrentDate().ToDate;

                TempData["SearchApprovalAttendance"] = 1;

                return View(objApprovalModel);
            }
        }

        //[HttpPost]
        //public ActionResult ApproveAttendance(AttendanceApprovalModel objApprovalModel)
        //{
        //    LoadSession();

        //    string strDBMsg = "";

        //    List<AttendanceApprovalModel> pending = new JavaScriptSerializer().Deserialize<List<AttendanceApprovalModel>>(objApprovalModel.PendingApprovalAttendance);

        //    objApprovalModel.ActiveYN = objApprovalModel.CheckedYN ? 'N'.ToString() : 'Y'.ToString();

        //    TempData["EmployeActive"] = objApprovalModel.ActiveYN;

        //    TempData["FromDateForRoaster"] = objApprovalModel.FromDate;
        //    TempData["ToDateForRoaster"] = objApprovalModel.ToDate;

        //    TempData["UnitId"] = objApprovalModel.UnitId;
        //    TempData["DeptId"] = objApprovalModel.DepartmentId;
        //    TempData["SectionId"] = objApprovalModel.SectionId;
        //    TempData["SubSectionId"] = objApprovalModel.SubSectionId;

        //    foreach (AttendanceApprovalModel list in pending)
        //    {
        //        //AttendanceApprovalModel objApprovalModel = new AttendanceApprovalModel();

        //        objApprovalModel.UpdateBy = strEmployeeId;
        //        objApprovalModel.HeadOfficeId = strHeadOfficeId;
        //        objApprovalModel.BranchOfficeId = strBranchOfficeId;

        //        objApprovalModel.EmployeeId = list.EmployeeId;
        //        objApprovalModel.LogDate = list.LogDate;


        //        strDBMsg = objAttendanceDal.ProcessAttendanceApproved(objApprovalModel);
        //    }

        //    TempData["OperationMessage"] = strDBMsg;
        //    TempData["SearchApprovalAttendance"] = 1;

        //    return RedirectToAction("GetAttendanceApprovalRecord");
        //}

        [HttpPost]
        public ActionResult DeleteApproveAttendance(AttendanceApprovalModel objApprovalModel)
        {
            LoadSession();

            string strDBMsg = "";

            objApprovalModel.ActiveYN = objApprovalModel.CheckedYN ? 'N'.ToString() : 'Y'.ToString();

            TempData["EmployeActive"] = objApprovalModel.ActiveYN;

            TempData["FromDateForRoaster"] = objApprovalModel.FromDate;
            TempData["ToDateForRoaster"] = objApprovalModel.ToDate;

            TempData["UnitId"] = objApprovalModel.UnitId;
            TempData["DeptId"] = objApprovalModel.DepartmentId;
            TempData["SectionId"] = objApprovalModel.SectionId;
            TempData["SubSectionId"] = objApprovalModel.SubSectionId;

            ///* List<AttendanceApprovalModel> pending = new JavaScriptSerializer().Deserialize<List<AttendanceApprovalModel>>*/(objApprovalModel.PendingApprovalAttendance);

            //foreach (AttendanceApprovalModel list in pending)
            //{
            //    //AttendanceApprovalModel objApprovalModel = new AttendanceApprovalModel();

            //    objApprovalModel.UpdateBy = strEmployeeId;
            //    objApprovalModel.HeadOfficeId = strHeadOfficeId;
            //    objApprovalModel.BranchOfficeId = strBranchOfficeId;

            //    objApprovalModel.EmployeeId = list.EmployeeId;
            //    objApprovalModel.LogDate = list.LogDate;

            //    strDBMsg = objAttendanceDal.DeleteAttendanceApproved(objApprovalModel);
            //}

            TempData["OperationMessage"] = strDBMsg;

            return Json(strDBMsg, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Attendance Process Manual

        [HttpGet]
        public ActionResult EmploeeManualAttendanceProcess()

        {
            AttendanceProcessManualModel objProcessManualModel = new AttendanceProcessManualModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                // objDutyRoasterModel.EmployeeId = strEmployeeId;
                objProcessManualModel.UpdateBy = strEmployeeId;
                objProcessManualModel.HeadOfficeId = strHeadOfficeId;
                objProcessManualModel.BranchOfficeId = strBranchOfficeId;



                #region search criteria

                if (TempData.ContainsKey("ManualAttendance") && (int)TempData["ManualAttendance"] == 1)
                {
                    objProcessManualModel.InActive = Convert.ToString(TempData["Inactive"]);
                    if (objProcessManualModel.InActive == "N")
                    {
                        objProcessManualModel.InActiveYN = true;
                    }

                    objProcessManualModel.Missing = Convert.ToString(TempData["Missing"]);

                    if (objProcessManualModel.Missing == "N")
                    {
                        objProcessManualModel.MissingYN = true;
                    }

                    objProcessManualModel.Absent = Convert.ToString(TempData["Absent"]);

                    if (objProcessManualModel.Absent == "N")
                    {
                        objProcessManualModel.AbsentYN = true;
                    }

                    objProcessManualModel.All = Convert.ToString(TempData["AllCheck"]);
                    if (objProcessManualModel.All == "Y")
                    {
                        objProcessManualModel.AllYN = true;
                    }

                    objProcessManualModel.EmployeeId = Convert.ToString(TempData["EmpId"]);
                    objProcessManualModel.EmployeeName = Convert.ToString(TempData["EmpName"]);
                    objProcessManualModel.EmployeeCardNo = Convert.ToString(TempData["EmpCard"]);

                    objProcessManualModel.FromDate = Convert.ToString(TempData["FromDate"]);
                    objProcessManualModel.ToDate = Convert.ToString(TempData["ToDate"]);

                    objProcessManualModel.InTime = Convert.ToString(TempData["InTime"]);
                    objProcessManualModel.OutTime = Convert.ToString(TempData["OutTime"]);

                    objProcessManualModel.LunchInTime = Convert.ToString(TempData["LunchInTime"]);
                    objProcessManualModel.LunchOutTime = Convert.ToString(TempData["LunchOutTime"]);

                    objProcessManualModel.UnitId = Convert.ToString(TempData["UnitId"]);

                    if (objProcessManualModel.UnitId.IsNullOrWhiteSpace())
                    {
                        objProcessManualModel.UnitId = null;
                    }
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    objProcessManualModel.DepartmentId = Convert.ToString(TempData["DepartmentId"]);
                    if (objProcessManualModel.DepartmentId.IsNullOrWhiteSpace())
                    {
                        objProcessManualModel.DepartmentId = null;
                    }

                    objProcessManualModel.SectionId = Convert.ToString(TempData["SectionId"]);
                    if (objProcessManualModel.SectionId.IsNullOrWhiteSpace())
                    {
                        objProcessManualModel.SectionId = null;
                    }

                    objProcessManualModel.SubSectionId = Convert.ToString(TempData["SubSectionId"]);
                    if (objProcessManualModel.SubSectionId.IsNullOrWhiteSpace())
                    {
                        objProcessManualModel.SubSectionId = null;
                    }


                    // selected value of dropdown--

                    if (objProcessManualModel.DepartmentId != null)
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objProcessManualModel.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objProcessManualModel.DepartmentId);
                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }


                    if (objProcessManualModel.SectionId != null)
                    {
                        ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objProcessManualModel.DepartmentId,
                            strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objProcessManualModel.SectionId);
                    }
                    else
                    {
                        ViewBag.SectionDDList = null;
                    }


                    if (objProcessManualModel.SubSectionId != null)
                    {
                        ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objProcessManualModel.SectionId,
                            strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objProcessManualModel.SubSectionId);
                    }
                    else
                    {
                        ViewBag.SubSectionDDList = null;
                    }

                    objProcessManualModel.EmployeeId = Convert.ToString(TempData["EmpId"]);
                    if (objProcessManualModel.EmployeeId.IsNullOrWhiteSpace())
                    {
                        objProcessManualModel.EmployeeId = null;
                    }

                    objProcessManualModel.AttendanceProcessManualList = (List<AttendanceProcessManualModel>)TempData["AttendanceList"];
                    ViewBag.DayTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDayTypeDDList(), "DAY_TYPE_ID", "DAY_TYPE_NAME", objProcessManualModel.DayTypeId);
                }

                #endregion

                else
                {
                    objProcessManualModel.FromDate = objLookUpDAL.getCurrentDate().FromDate;
                    objProcessManualModel.ToDate = objLookUpDAL.getFirstLastDay().ToDate;
                    objProcessManualModel.AttendanceProcessManualList = new List<AttendanceProcessManualModel>();

                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    ViewBag.DayTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDayTypeDDList(), "DAY_TYPE_ID", "DAY_TYPE_NAME");
                }

                return View(objProcessManualModel);
            }
        }

        [HttpPost]
        public ActionResult EmploeeManualAttendanceProcess(AttendanceProcessManualModel objProcessManualModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objProcessManualModel.UpdateBy = strEmployeeId;
                objProcessManualModel.HeadOfficeId = strHeadOfficeId;
                objProcessManualModel.BranchOfficeId = strBranchOfficeId;

                //objProcessManualModel.FromDate = objLookUpDAL.getCurrentDate().FromDate;
                //objProcessManualModel.ToDate = objLookUpDAL.getFirstLastDay().ToDate;


                string strDBMsg = "";

                //if (ModelState.IsValid)
                //{
                    if (objProcessManualModel.AttendanceProcessManualList != null)
                    {
                        objProcessManualModel.AttendanceProcessManualList.RemoveAll(a => !a.IsChecked);

                        strDBMsg = objAttendanceDal.SaveAttendenceManualEntry(objProcessManualModel);
                        TempData["OperationMessage"] = strDBMsg;
                    }
                    ModelState.Clear();
                //}


                objProcessManualModel.Missing = objProcessManualModel.MissingYN ? 'N'.ToString() : 'Y'.ToString();
                objProcessManualModel.Absent = objProcessManualModel.AbsentYN ? 'N'.ToString() : 'Y'.ToString();
                objProcessManualModel.InActive = objProcessManualModel.InActiveYN ? 'N'.ToString() : 'Y'.ToString();


                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                if (objProcessManualModel.UnitId != null)
                {
                    ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objProcessManualModel.UnitId, strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objProcessManualModel.DepartmentId);

                    if (objProcessManualModel.DepartmentId != null)
                    {
                        ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objProcessManualModel.DepartmentId, strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objProcessManualModel.SectionId);

                        if (objProcessManualModel.SectionId != null)
                        {
                            ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objProcessManualModel.SectionId, strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objProcessManualModel.SubSectionId);
                        }
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


                if (objProcessManualModel.Missing == "N")
                {
                    strDBMsg = objAttendanceDal.AddAttendanceRecordMissing(objProcessManualModel);
                    objProcessManualModel.AttendanceProcessManualList = objAttendanceDal.GetMissingEmployeeAttendance(objProcessManualModel);
                    //TempData["OperationMessage"] = strDBMsg;
                }
                else if (objProcessManualModel.Absent == "N")
                {
                    strDBMsg = objAttendanceDal.AddAttendanceRecordAbsent(objProcessManualModel);
                    objProcessManualModel.AttendanceProcessManualList = objAttendanceDal.GetAbsentEmployeeAttendance(objProcessManualModel);
                    //TempData["OperationMessage"] = strDBMsg;
                }
                else
                {
                    strDBMsg = objAttendanceDal.AddRecordAttendanceManual(objProcessManualModel);
                    objProcessManualModel.AttendanceProcessManualList = objAttendanceDal.GetEmployeeRecordForAttendanceManual(objProcessManualModel);
                }

                ViewBag.DayTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDayTypeDDList(), "DAY_TYPE_ID", "DAY_TYPE_NAME");

                return View(objProcessManualModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessEmployeeAttendanceManual(AttendanceProcessManualModel objProcessManualModel)
        {
            LoadSession();
            objProcessManualModel.UpdateBy = strEmployeeId;
            objProcessManualModel.HeadOfficeId = strHeadOfficeId;
            objProcessManualModel.BranchOfficeId = strBranchOfficeId;

            //objProcessManualModel.FromDate = objLookUpDAL.getCurrentDate().FromDate;
            //objProcessManualModel.ToDate = objLookUpDAL.getFirstLastDay().ToDate;


            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objAttendanceDal.AttendenceProcessDaily(objProcessManualModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            objProcessManualModel.Missing = objProcessManualModel.MissingYN ? 'N'.ToString() : 'Y'.ToString();
            objProcessManualModel.Absent = objProcessManualModel.AbsentYN ? 'N'.ToString() : 'Y'.ToString();
            objProcessManualModel.InActive = objProcessManualModel.InActiveYN ? 'N'.ToString() : 'Y'.ToString();

            if (objProcessManualModel.Missing == "N")
            {
                TempData["AttendanceList"] = objAttendanceDal.GetMissingEmployeeAttendance(objProcessManualModel);
                //TempData["OperationMessage"] = strDBMsg;
            }
            else if (objProcessManualModel.Absent == "N")
            {
                TempData["AttendanceList"] = objAttendanceDal.GetAbsentEmployeeAttendance(objProcessManualModel);
                //TempData["OperationMessage"] = strDBMsg;
            }
            else
            {
                TempData["AttendanceList"] = objAttendanceDal.GetEmployeeRecordForAttendanceManual(objProcessManualModel);
            }

            TempData["EmpId"] = objProcessManualModel.EmployeeId;
            TempData["EmpCard"] = objProcessManualModel.EmployeeCardNo;
            TempData["EmpName"] = objProcessManualModel.EmployeeName;
            TempData["FromDate"] = objProcessManualModel.FromDate;
            TempData["ToDate"] = objProcessManualModel.ToDate;
            TempData["InTime"] = objProcessManualModel.InTime;
            TempData["OutTime"] = objProcessManualModel.OutTime;
            TempData["LunchInTime"] = objProcessManualModel.LunchInTime;
            TempData["LunchOutTime"] = objProcessManualModel.LunchOutTime;


            TempData["UnitId"] = objProcessManualModel.UnitId;
            TempData["DepartmentId"] = objProcessManualModel.DepartmentId;
            TempData["SectionId"] = objProcessManualModel.SectionId;
            TempData["SubSectionId"] = objProcessManualModel.SubSectionId;
            TempData["Missing"] = objProcessManualModel.Missing;
            TempData["Absent"] = objProcessManualModel.Absent;
            TempData["Inactive"] = objProcessManualModel.InActive;


            TempData["ManualAttendance"] = 1;

            return RedirectToAction("EmploeeManualAttendanceProcess");
        }

        #endregion

        #region Employee Shift Assign

        public ActionResult AssignEmployeeShift()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                AssignEmployeeShiftModel objAssignShiftModel = new AssignEmployeeShiftModel
                {
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,

                    FirstDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    LastDate = objLookUpDAL.getFirstLastDay().LastDate,

                    AssignEmployeeShiftList = new List<AssignEmployeeShiftModel>()
                };

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                ViewBag.ShiftTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetShiftTypeDDList(), "SHIFT_ID", "SHIFT_NAME");

                return View(objAssignShiftModel);
            }
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult AssignEmployeeShift(AssignEmployeeShiftModel objAssignShiftModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objAssignShiftModel.UpdateBy = strEmployeeId;
                objAssignShiftModel.HeadOfficeId = strHeadOfficeId;
                objAssignShiftModel.BranchOfficeId = strBranchOfficeId;

                string vMessage = "";
                if (objAssignShiftModel.AssignEmployeeShiftList != null)
                {
                    objAssignShiftModel.AssignEmployeeShiftList.RemoveAll(s => !s.IsChecked);
                    vMessage = objAttendanceDal.SaveAssignEmployeeShift(objAssignShiftModel);
                    TempData["OperationMessage"] = vMessage;
                    ModelState.Clear();
                }



                objAssignShiftModel.AssignEmployeeShiftList = objAttendanceDal.GetEmployeeRecordForAssignShift(objAssignShiftModel);

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                if (objAssignShiftModel.UnitId != null)
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    if (objAssignShiftModel.DepartmentId != null)
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objAssignShiftModel.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objAssignShiftModel.DepartmentId);
                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }

                    if (objAssignShiftModel.SectionId != null)
                    {
                        ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objAssignShiftModel.DepartmentId,
                            strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objAssignShiftModel.SectionId);
                    }
                    else
                    {
                        ViewBag.SectionDDList = null;
                    }

                    if (objAssignShiftModel.SubSectionId != null)
                    {
                        ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objAssignShiftModel.SectionId,
                            strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objAssignShiftModel.SubSectionId);
                    }
                    else
                    {
                        ViewBag.SubSectionDDList = null;
                    }
                }

                ViewBag.ShiftTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetShiftTypeDDList(), "SHIFT_ID", "SHIFT_NAME");

                return View(objAssignShiftModel);
            }
        }

        public ActionResult DisplayAssignedEmployee()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                AssignEmployeeShiftModel objAssignShiftModel = new AssignEmployeeShiftModel();

                objAssignShiftModel.UpdateBy = strEmployeeId;
                objAssignShiftModel.HeadOfficeId = strHeadOfficeId;
                objAssignShiftModel.BranchOfficeId = strBranchOfficeId;

                objAssignShiftModel.FirstDate = objLookUpDAL.getFirstLastDay().FirstDate;
                objAssignShiftModel.LastDate = objLookUpDAL.getFirstLastDay().LastDate;

                objAssignShiftModel.AssignEmployeeShiftList = new List<AssignEmployeeShiftModel>();

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");



                ViewBag.ShiftTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetShiftTypeDDList(), "SHIFT_ID", "SHIFT_NAME");

                return View(objAssignShiftModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DisplayAssignedEmployee(AssignEmployeeShiftModel objAssignShiftModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                objAssignShiftModel.UpdateBy = strEmployeeId;
                objAssignShiftModel.HeadOfficeId = strHeadOfficeId;
                objAssignShiftModel.BranchOfficeId = strBranchOfficeId;

                objAssignShiftModel.FirstDate = objLookUpDAL.getFirstLastDay().FirstDate;
                objAssignShiftModel.LastDate = objLookUpDAL.getFirstLastDay().LastDate;


                objAssignShiftModel.AssignEmployeeShiftList = objAttendanceDal.GetAssignedEmployeeList(objAssignShiftModel);

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");


                if (objAssignShiftModel.UnitId != null)
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    if (objAssignShiftModel.DepartmentId != null)
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objAssignShiftModel.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objAssignShiftModel.DepartmentId);
                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }

                    if (objAssignShiftModel.SectionId != null)
                    {
                        ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objAssignShiftModel.DepartmentId,
                            strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objAssignShiftModel.SectionId);
                    }
                    else
                    {
                        ViewBag.SectionDDList = null;
                    }

                    if (objAssignShiftModel.SubSectionId != null)
                    {
                        ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objAssignShiftModel.SectionId,
                            strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objAssignShiftModel.SubSectionId);
                    }
                    else
                    {
                        ViewBag.SubSectionDDList = null;
                    }
                }


                ViewBag.ShiftTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetShiftTypeDDList(), "SHIFT_ID", "SHIFT_NAME");
                return View(objAssignShiftModel);
            }
        }

        #endregion
    }
}