using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.DAL;
using ERP.MODEL;
using ERP.Utility;
using System.Net;
using Microsoft.Ajax.Utilities;

namespace ERP.Controllers
{
    public class LeaveRequestController : Controller
    {

        LeaveRequestDAL objLeaveRequestDal = new LeaveRequestDAL();
        LeaveRequestModel objLeaveRequestModel = new LeaveRequestModel();
        LookUpDAL objLookUpDAL = new LookUpDAL();


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
        public void LoadDropDownList()
        {
            ViewBag.LeaveTypeByDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLeaveTypeDDList(), "LEAVE_TYPE_ID", "LEAVE_TYPE_NAME");
        }
        [HttpGet]
        public ActionResult LeaveRequest(LeaveRequestModel objLeaveRequestModel)
        {
            LoadSession();



            //ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();

            //objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);

            objLeaveRequestModel.EmployeeId = strEmployeeId;
            objLeaveRequestModel.UpdateBy = strEmployeeId;
            objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
            objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;

            objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);


            if (TempData.ContainsKey("LeaveYear"))
            {
                objLeaveRequestModel.CurrentYear = TempData["LeaveYear"].ToString();

            }
            else
            {
                objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");

            }
            



            objLeaveRequestDal.IndividualLeaveProess(objLeaveRequestModel);

            ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);
            ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);


            //List<String> data = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);

            // var countLoadEmployeeGrid = ViewBag.LoadEmployeeLeaveSummeryRecordList;
            if (!(objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel)).Any())
            {
                objLeaveRequestDal.IndividualLeaveProessTemp(objLeaveRequestModel);
                ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryTempRecord(objLeaveRequestModel);

            }

            LoadDropDownList();

            return View("LeaveRequest", objLeaveRequestModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveLeaveEntry(LeaveRequestModel objLeaveRequestModel)
        {
            LoadSession();



            objLeaveRequestModel.EmployeeId = strEmployeeId;
            objLeaveRequestModel.UpdateBy = strEmployeeId;
            objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
            objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;


            // ViewBag.LeaveYear = TempData["LeaveYear"].ToString();

            if(TempData.ContainsKey("LeaveYear"))
            {
                objLeaveRequestModel.CurrentYear = TempData["LeaveYear"].ToString();
            }
            else
            {

                objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");
            }
             



            if (ModelState.IsValid)
            {
                string strDBMsg = objLeaveRequestDal.SaveLeaveEntry(objLeaveRequestModel);
                TempData["OperationMessage"] = strDBMsg;

            }

            ViewBag.LeaveTypeByDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLeaveTypeDDList(), "LEAVE_TYPE_ID", "LEAVE_TYPE_NAME");



            return RedirectToAction("LeaveRequest", "LeaveRequest");
            //return View("LeaveRequest");
        }
        public List<LeaveRequestModel> LeaveRequestListDataForEdit(DataTable dt2)
        {
            List<LeaveRequestModel> leaveRequestDataBundle = new List<LeaveRequestModel>();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                LeaveRequestModel objLeaveRequestModel = new LeaveRequestModel();

                // objAccessoriesOrderModel.SerialNumber = dt2.Rows[i]["SL"].ToString();
                //objLeaveRequestModel.EmployeeId = dt2.Rows[i]["EMPLOYEE_ID"].ToString();
                objLeaveRequestModel.LeaveTypeId = dt2.Rows[i]["LEAVE_TYPE_ID"].ToString();
                objLeaveRequestModel.LeaveStartDate = dt2.Rows[i]["leave_from_date"].ToString();
                objLeaveRequestModel.LeaveEndDate = dt2.Rows[i]["leave_to_date"].ToString();
                objLeaveRequestModel.Remarks = dt2.Rows[i]["REMARKS"].ToString();
                objLeaveRequestModel.TotalNoOfLeave = dt2.Rows[i]["TOTAL_NO_OF_LEAVE"].ToString();
                objLeaveRequestModel.CurrentYear = dt2.Rows[i]["LEAVE_YEAR"].ToString();
                leaveRequestDataBundle.Add(objLeaveRequestModel);
            }

            return leaveRequestDataBundle;
        }
        [HttpGet]
        public ActionResult EditLeaveEntry(string pLeaveTypeId, string pLeaveFromDate, string pLeaveToDate, string pRemarks, string pLeaveYear)
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
                    objLeaveRequestModel.EmployeeId = strEmployeeId;
                    objLeaveRequestModel.LeaveTypeId = pLeaveTypeId;
                    objLeaveRequestModel.LeaveFromDate = pLeaveFromDate;
                    objLeaveRequestModel.LeaveToDate = pLeaveToDate;
                    objLeaveRequestModel.CurrentYear = pLeaveYear;
                    //objLeaveRequestModel.CurrentYear = objLookUpDAL.getCurrentYear();

                    objLeaveRequestModel.UpdateBy = strEmployeeId;

                    objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
                    objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;

                    ViewBag.LeaveTypeByDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLeaveTypeDDList(), "LEAVE_TYPE_ID", "LEAVE_TYPE_NAME");



                    DataTable dt2 = objLeaveRequestDal.GetLeaveRequestForEdit(objLeaveRequestModel);
                    ViewBag.TrimsListForEdit = LeaveRequestListDataForEdit(dt2);


                    objLeaveRequestModel.CurrentYear = pLeaveYear;

                    ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);
                    ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);

                    objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);

                    //


                    LoadDropDownList();
                    //ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);

                    //ViewBag.GetAllEmployeeLeaveRequestRecord = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);

                    //ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);
                    //ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();

                    if (!string.IsNullOrEmpty(pLeaveYear))
                    {

                        objLeaveRequestModel.CurrentYear = pLeaveYear;
                    }
                    else
                    {

                        objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");
                    }
                        

                    return View("LeaveRequest", objLeaveRequestModel);

                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        public ActionResult DeleteEmpLeaveRecord(string pId, string fromDate, string toDate)
        {
            LeaveRequestModel objLeaveRequestModel = new LeaveRequestModel { EmployeeId = pId, LeaveFromDate = fromDate, LeaveToDate = toDate };

            LoadSession();

            objLeaveRequestModel.UpdateBy = strEmployeeId;
            objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
            objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;

            string vDbMessage = objLeaveRequestDal.DeleteEmpLeaveRecord(objLeaveRequestModel);
            //string vDbMessage = $"{ pId } { strEmployeeId } { strHeadOfficeId }  { strBranchOfficeId }";

            TempData["OperationMessage"] = vDbMessage;

            return RedirectToAction("LeaveRequest", "LeaveRequest");
        }
        public ActionResult DeleteEmpLeaveSearchRecord(string pId, string fromDate, string toDate)
        {
            LeaveRequestModel objLeaveRequestModel = new LeaveRequestModel { EmployeeId = pId, LeaveFromDate = fromDate, LeaveToDate = toDate };

            LoadSession();

            objLeaveRequestModel.UpdateBy = strEmployeeId;
            objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
            objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;

            string vDbMessage = objLeaveRequestDal.DeleteEmpLeaveRecord(objLeaveRequestModel);
            //string vDbMessage = $"{ pId } { strEmployeeId } { strHeadOfficeId }  { strBranchOfficeId }";

            TempData["OperationMessage"] = vDbMessage;

            return RedirectToAction("SearchEmployee", "LeaveRequest", objLeaveRequestModel);
        }
        [HttpGet]
        public ActionResult EmployeeLeaveSearch(LeaveRequestModel objLeaveRequestModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {

                LoadSession();

                objLeaveRequestModel.UpdateBy = strEmployeeId;
                objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
                objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;
                objLeaveRequestModel.EmployeeId = strEmployeeId;

                string strLeaveYear = objLeaveRequestModel.CurrentYear;
                TempData["LeaveYear"] = strLeaveYear;
                TempData.Keep("LeaveYear");

               objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);
                //objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");
                objLeaveRequestModel.UpdateBy = strEmployeeId;

                objLeaveRequestModel.CurrentYear = strLeaveYear;
                objLeaveRequestDal.IndividualLeaveProess(objLeaveRequestModel);

                ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);
                ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);



                if (!(objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel)).Any())
                {
                    objLeaveRequestDal.IndividualLeaveProessTemp(objLeaveRequestModel);
                    ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryTempRecord(objLeaveRequestModel);

                }




                //objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");


                LoadDropDownList();


                return View("LeaveRequest", objLeaveRequestModel);
            }
        }
        public ActionResult ClearLeaveRequestEntry()
        {
            ModelState.Clear();
            TempData["SearchValueFromSearchAction"] = "";
            return RedirectToAction("LeaveRequest");
        }
        public ActionResult ClearLeaveSearchRequestEntry()
        {
            ModelState.Clear();
            TempData["SearchValueFromSearchAction"] = "";
            return RedirectToAction("SearchEmployee");
        }
        #region Approval 

        // Approval of Employee'SaveLeaveEntry Leave Request By Team Leader

        [HttpGet]
        public ActionResult ApproveLeaveRequestByTeamLeader()

        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                LeaveRequestModel objLeaveRequestModel = new LeaveRequestModel
                {
                    TeamLeaderId = strEmployeeId,
                    //CheckedYN = "Y",
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,
                    LeaveStartDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    LeaveEndDate = objLookUpDAL.getFirstLastDay().LastDate
                };


                objLeaveRequestModel.IndividualLeaveRequestList = objLeaveRequestDal.GetLeaveRequestPendingListForTL(objLeaveRequestModel);  //new List<EmployeeIndividualJdModel>();

                return View(objLeaveRequestModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveLeaveRequestByTeamLeader(LeaveRequestModel objLeaveRequestModel)
        {

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objLeaveRequestModel.TeamLeaderId = strEmployeeId;
                //objLeaveRequestModel.CheckedYN = "Y";

                objLeaveRequestModel.UpdateBy = strEmployeeId;
                objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
                objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;

                objLeaveRequestModel.LeaveStartDate = objLookUpDAL.getFirstLastDay().FirstDate;
                objLeaveRequestModel.LeaveEndDate = objLookUpDAL.getFirstLastDay().LastDate;

                if (objLeaveRequestModel.IndividualLeaveRequestList != null)
                {
                    string successMsg = "";
                    objLeaveRequestModel.IndividualLeaveRequestList.RemoveAll(x => !x.IsChecked);

                    for (int i = 0; i < objLeaveRequestModel.IndividualLeaveRequestList.Count; i++)
                    {
                        objLeaveRequestModel.IndividualLeaveRequestList[i].UpdateBy = strEmployeeId;
                        objLeaveRequestModel.IndividualLeaveRequestList[i].HeadOfficeId = strHeadOfficeId;
                        objLeaveRequestModel.IndividualLeaveRequestList[i].BranchOfficeId = strBranchOfficeId;
                        objLeaveRequestModel.LeaveFromDate = objLeaveRequestModel.IndividualLeaveRequestList[i].LeaveFromDate;
                        objLeaveRequestModel.LeaveToDate = objLeaveRequestModel.IndividualLeaveRequestList[i].LeaveToDate;
                        successMsg = objLeaveRequestDal.ApprovedEmpLeaveRequestByTL(objLeaveRequestModel.IndividualLeaveRequestList[i]);
                        TempData["OperationMessage"] = successMsg;
                    }
                }



                objLeaveRequestModel.IndividualLeaveRequestList = objLeaveRequestDal.GetLeaveRequestPendingListForTL(objLeaveRequestModel);

                return View(objLeaveRequestModel);
            }
        }

        #endregion
        #region Approval by HR
        // Approval of Employee's Leave Request By HR

        [HttpGet]
        public ActionResult ApproveEmployeeLeaveRequestByHR()

        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                LeaveRequestModel objLeaveRequestModel = new LeaveRequestModel
                {
                    TeamLeaderId = strEmployeeId,
                    //CheckedYN = "Y",
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,
                    LeaveStartDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    LeaveEndDate = objLookUpDAL.getFirstLastDay().LastDate
                };


                objLeaveRequestModel.IndividualLeaveRequestList = objLeaveRequestDal.GetLeaveRequestPendingListForHR(objLeaveRequestModel);  //new List<EmployeeIndividualJdModel>();

                return View(objLeaveRequestModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApproveEmployeeLeaveRequestByHR(LeaveRequestModel objLeaveRequestModel)
        {

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objLeaveRequestModel.TeamLeaderId = strEmployeeId;
                //objLeaveRequestModel.CheckedYN = "Y";

                objLeaveRequestModel.UpdateBy = strEmployeeId;
                objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
                objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;
                objLeaveRequestModel.LeaveStartDate = objLookUpDAL.getFirstLastDay().FirstDate;
                objLeaveRequestModel.LeaveEndDate = objLookUpDAL.getFirstLastDay().LastDate;

                if (objLeaveRequestModel.IndividualLeaveRequestList != null)
                {
                    string successMsg = "";
                    objLeaveRequestModel.IndividualLeaveRequestList.RemoveAll(x => !x.IsChecked);

                    for (int i = 0; i < objLeaveRequestModel.IndividualLeaveRequestList.Count; i++)
                    {
                        objLeaveRequestModel.IndividualLeaveRequestList[i].UpdateBy = strEmployeeId;
                        objLeaveRequestModel.IndividualLeaveRequestList[i].HeadOfficeId = strHeadOfficeId;
                        objLeaveRequestModel.IndividualLeaveRequestList[i].BranchOfficeId = strBranchOfficeId;
                        objLeaveRequestModel.LeaveFromDate = objLeaveRequestModel.IndividualLeaveRequestList[i].LeaveFromDate;
                        objLeaveRequestModel.LeaveToDate = objLeaveRequestModel.IndividualLeaveRequestList[i].LeaveToDate;
                        successMsg = objLeaveRequestDal.ApprovedEmpLeaveRequestByHR(objLeaveRequestModel.IndividualLeaveRequestList[i]);
                        TempData["OperationMessage"] = successMsg;
                    }
                }



                objLeaveRequestModel.IndividualLeaveRequestList = objLeaveRequestDal.GetLeaveRequestPendingListForHR(objLeaveRequestModel);

                return View(objLeaveRequestModel);
            }
        }
        #endregion
        [HttpGet]
        public ActionResult LeaveRequestApprovedByTeamLeader()

        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                LeaveRequestModel objLeaveRequestModel = new LeaveRequestModel
                {
                    TeamLeaderId = strEmployeeId,
                    //CheckedYN = "Y",
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,
                    LeaveFromDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    LeaveToDate = objLookUpDAL.getFirstLastDay().LastDate
                };


                objLeaveRequestModel.IndividualLeaveRequestList = objLeaveRequestDal.GetLeaveRequestApprovedListForTL(objLeaveRequestModel);  //new List<EmployeeIndividualJdModel>();

                return View(objLeaveRequestModel);
            }
        }
        [HttpGet]
        public ActionResult LeaveRequestApprovedByHR()

        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();


                LeaveRequestModel objLeaveRequestModel = new LeaveRequestModel
                {
                    TeamLeaderId = strEmployeeId,
                    //CheckedYN = "Y",
                    UpdateBy = strEmployeeId,
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,
                    LeaveFromDate = objLookUpDAL.getFirstLastDay().FirstDate,
                    LeaveToDate = objLookUpDAL.getFirstLastDay().LastDate
                };


                objLeaveRequestModel.IndividualLeaveRequestList = objLeaveRequestDal.GetLeaveRequestApprovedListForHR(objLeaveRequestModel);  //new List<EmployeeIndividualJdModel>();

                return View(objLeaveRequestModel);
            }
        }
        //[HttpGet]
        //public ActionResult LeaveRequestbyHR(LeaveRequestModel objLeaveRequestModel)
        //{
        //    LoadSession();



        //    //ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();

        //    //objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);

        //    objLeaveRequestModel.EmployeeId = strEmployeeId;
        //    objLeaveRequestModel.UpdateBy = strEmployeeId;
        //    objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
        //    objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;

        //    //objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);

        //    objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");



        //    objLeaveRequestDal.IndividualLeaveProess(objLeaveRequestModel);

        //    //ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);
        //    //ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);



        //    //if (!(objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel)).Any())
        //    //{
        //    //    objLeaveRequestDal.IndividualLeaveProessTemp(objLeaveRequestModel);
        //    //    ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryTempRecord(objLeaveRequestModel);

        //    //}

        //    LoadDropDownList();

        //    return View("LeaveRequestSavebyHR", objLeaveRequestModel);
        //}




        //[HttpGet]
        //public ActionResult LeaveRequestSavebyHR(LeaveRequestModel objLeaveRequestModel)
        //{
        //    LoadSession();



        //    //ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();

        //    //objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);

        //    objLeaveRequestModel.EmployeeId = objLeaveRequestModel.EmployeeId;
        //    objLeaveRequestModel.UpdateBy = strEmployeeId;
        //    objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
        //    objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;

        //    objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);

        //    objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");



        //    objLeaveRequestDal.IndividualLeaveProess(objLeaveRequestModel);

        //    ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);
        //    ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);



        //    if (!(objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel)).Any())
        //    {
        //        objLeaveRequestDal.IndividualLeaveProessTemp(objLeaveRequestModel);
        //        ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryTempRecord(objLeaveRequestModel);

        //    }

        //    LoadDropDownList();

        //    return View("LeaveRequestSavebyHR", objLeaveRequestModel);
        //}



        //[HttpGet]
        //public ActionResult EmployeeSearchByHR(string searchEmployeeId)
        //{
        //    if (Session["strEmployeeId"] == null)
        //    {
        //        return RedirectToAction("LogOut", "Login");
        //    }
        //    else
        //    {

        //        LoadSession();

        //        objLeaveRequestModel.UpdateBy = strEmployeeId;
        //        objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
        //        objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;
        //        objLeaveRequestModel.EmployeeId = searchEmployeeId;

        //        //objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);
        //        //objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");
        //        //objLeaveRequestModel.UpdateBy = strEmployeeId;
        //        objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");

        //        objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);

        //        //objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");



        //        objLeaveRequestDal.IndividualLeaveProess(objLeaveRequestModel);

        //        ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);
        //        ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);

        //        //objLeaveRequestDal.IndividualLeaveProess(objLeaveRequestModel);

        //        //ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);
        //        //ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);



        //        if (!(objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel)).Any())
        //        {
        //            objLeaveRequestDal.IndividualLeaveProessTemp(objLeaveRequestModel);
        //            ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryTempRecord(objLeaveRequestModel);

        //        }




        //        //old
        //        //objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");


        //        LoadDropDownList();


        //        return View("LeaveRequestbyHR", objLeaveRequestModel);
        //    }
        //}
        [HttpGet]
        public ActionResult EditLeaveEntrybyHR(string pEmployeeId, string pLeaveTypeId, string pLeaveFromDate, string pLeaveToDate, string pRemarks, string pLeaveYear)
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
                    objLeaveRequestModel.EmployeeId = pEmployeeId;
                    objLeaveRequestModel.LeaveTypeId = pLeaveTypeId;
                    objLeaveRequestModel.LeaveFromDate = pLeaveFromDate;
                    objLeaveRequestModel.LeaveToDate = pLeaveToDate;

                    //objLeaveRequestModel.CurrentYear = objLookUpDAL.getCurrentYear();

                    objLeaveRequestModel.UpdateBy = strEmployeeId;

                    objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
                    objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;
                    objLeaveRequestModel.CurrentYear = pLeaveYear;

                    ViewBag.LeaveTypeByDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLeaveTypeDDList(), "LEAVE_TYPE_ID", "LEAVE_TYPE_NAME");



                    DataTable dt2 = objLeaveRequestDal.GetLeaveRequestForEdit(objLeaveRequestModel);
                    ViewBag.TrimsListForEdit = LeaveRequestListDataForEdit(dt2);


                    

                    ViewBag.LeaveYear = pLeaveYear;

                    ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);
                    ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);

                    objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);

                    //objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");


                    LoadDropDownList();
                    //ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);

                    //ViewBag.GetAllEmployeeLeaveRequestRecord = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);

                    //ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);
                    //ViewBag.CurrentYear = objLookUpDAL.getCurrentYear();


                    //return View("LeaveRequestbyHR", objLeaveRequestModel);

                    objLeaveRequestModel.CurrentYear = pLeaveYear;

                    return View("SearchEmployee", objLeaveRequestModel);

                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveLeaveEntryByHR(LeaveRequestModel objLeaveRequestModel)
        {
            LoadSession();

            //objLeaveRequestModel.EmployeeId = objLeaveRequestModel.EmployeeId;
            objLeaveRequestModel.UpdateBy = strEmployeeId;
            objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
            objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;
            //objLeaveRequestModel.CurrentYear =


            // objLeaveRequestModel.CurrentYear = objLookUpDAL.getCurrentYear();

            if (TempData.ContainsKey("LeaveYearByHR"))
            {

                objLeaveRequestModel.CurrentYear = TempData["LeaveYearByHR"].ToString();
                //TempData.Keep("LeaveYearByHR");
            }
            else
            {
                objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");


            }

            if (ModelState.IsValid)
            {
                string strDBMsg = objLeaveRequestDal.SaveLeaveEntrybyHR(objLeaveRequestModel);
                TempData["OperationMessage"] = strDBMsg;

            }




            objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);



            objLeaveRequestDal.IndividualLeaveProess(objLeaveRequestModel);

            ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);
            ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);



            //objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");


            ViewBag.LeaveTypeByDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLeaveTypeDDList(), "LEAVE_TYPE_ID", "LEAVE_TYPE_NAME");

            if (TempData.ContainsKey("LeaveYearByHR"))
            {
                objLeaveRequestModel.CurrentYear = TempData["LeaveYearByHR"].ToString();
            }
                //return RedirectToAction("LeaveRequestSavebyHR", objLeaveRequestModel);
                return RedirectToAction("SearchEmployee", objLeaveRequestModel);
            //return View("LeaveRequest");
        }
        [HttpGet]
        public ActionResult SearchEmployee(LeaveRequestModel objLeaveRequestModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {

                LoadSession();

                objLeaveRequestModel.UpdateBy = strEmployeeId;
                objLeaveRequestModel.HeadOfficeId = strHeadOfficeId;
                objLeaveRequestModel.BranchOfficeId = strBranchOfficeId;            
                objLeaveRequestModel.EmployeeId = objLeaveRequestModel.EmployeeId;



                //if (objLeaveRequestModel.CurrentYear==null)
                //{
                //    //objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);
                //    //ViewBag.GetAllLeaveRequestEmployeeRecord = objLeaveRequestModel;

                //}

                //if (TempData.ContainsKey("EditLeaveEntrybyHR") && (int)TempData["EditLeaveEntrybyHR"] == 1)
                //{
                //    objLeaveRequestModel.CurrentYear = Convert.ToString(TempData["LeaveYear"]);
                //}
                //else
                //{
                    
                    objLeaveRequestModel.CurrentYear = objLeaveRequestModel.CurrentYear ?? DateTime.Now.ToString("yyyy");
                    ViewBag.LeaveYear = objLeaveRequestModel.CurrentYear;
                    TempData["LeaveYearByHR"] = objLeaveRequestModel.CurrentYear;
                    
                    string strLeaveYear = objLeaveRequestModel.CurrentYear; 


                //objLeaveRequestModel.UpdateBy = strEmployeeId;


                objLeaveRequestDal.IndividualLeaveProess(objLeaveRequestModel);

                ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);
                ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);



                if (!(objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel)).Any())
                {
                    objLeaveRequestDal.IndividualLeaveProessTemp(objLeaveRequestModel);
                    ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryTempRecord(objLeaveRequestModel);

                }

                LoadDropDownList();


                //objLeaveRequestModel.TransferDate=objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel).TransferDate;
                objLeaveRequestModel =objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);

                if(!string.IsNullOrEmpty(strLeaveYear))
                {
                    objLeaveRequestModel.CurrentYear = strLeaveYear;

                }
                else
                {
                    objLeaveRequestModel.CurrentYear = DateTime.Now.ToString("yyyy");

                }

                


                return View("SearchEmployee", objLeaveRequestModel);
            }
        }


    }
}