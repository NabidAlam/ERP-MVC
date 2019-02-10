using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.DAL;
using ERP.MODEL;
using ERP.Utility;

namespace ERP.Controllers
{
    public class  TransferController : Controller
    {
        LookUpDAL objLookUpDAL = new LookUpDAL();
        TransferModel  objTransferModel = new TransferModel();
        TransferDAL  objTransferDAL = new TransferDAL();

        #region "Common"
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
            if (Request.Url == null) return;
            var strCurrentUrl = Request.Url.AbsoluteUri.ToString();
            if (strCurrentUrl.Contains("?"))
            {
                strCurrentUrl = strCurrentUrl.Substring(0, strCurrentUrl.IndexOf('?'));
            }
            if (strCurrentUrl == strOldUrl) return;
            TempData["SearchValue"] = string.Empty;

            Session["strOldUrl"] = strCurrentUrl;
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


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeLeaveSaveForTransfer(TransferModel objTransferModel)
        {
            LoadSession();

            //objLeaveRequestModel.EmployeeId = objLeaveRequestModel.EmployeeId;
            objTransferModel.UpdateBy = strEmployeeId;
            objTransferModel.HeadOfficeId = strHeadOfficeId;
            objTransferModel.BranchOfficeId = strBranchOfficeId;



            if (ModelState.IsValid)
            {
                string strDBMsg = objTransferDAL.EmployeeLeaveSaveForTransfer(objTransferModel);
                TempData["OperationMessage"] = strDBMsg;

            }

            return RedirectToAction("EmpLeaveEntry", objTransferModel);
            //return View("LeaveRequest");
        }



        //[HttpGet]
        //public ActionResult EmpLeaveEntry(TransferModel objTransferModel)
        //{
        //    if (Session["strEmployeeId"] == null)
        //    {
        //        return RedirectToAction("LogOut", "Login");
        //    }
        //    else
        //    {

        //        LoadSession();

        //        objTransferModel.UpdateBy = strEmployeeId;
        //        objTransferModel.HeadOfficeId = strHeadOfficeId;
        //        objTransferModel.BranchOfficeId = strBranchOfficeId;
        //        objTransferModel.EmployeeId = objTransferModel.EmployeeId;
        //        objTransferModel.Year = DateTime.Now.ToString("yyyy");

        //        objTransferModel.EmployeeModel = objTransferDAL.SearchEmployeeInfoById(objTransferModel).EmployeeModel;

        //        ViewBag.LoadEmpLeaveEntryRecord = objTransferDAL.LoadEmpLBRecordForTF(objTransferModel);


        //        //ViewBag.GetEmployeeRecordById = objEntryForTransferDal.SearchEmployeeInfoById(objLeaveEntryForTransferModel);


        //        ViewBag.LeaveTypeByDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLeaveTypeDDList(), "LEAVE_TYPE_ID", "LEAVE_TYPE_NAME");

        //        //ViewBag.Year = DateTime.Now.ToString("yyyy");
        //        //objLeaveEntryForTransferModel.Year = DateTime.Now.ToString("yyyy");


        //        return View("EmpLeaveEntry", objTransferModel);
        //    }
        //}
        [HttpGet]
        public ActionResult EmpLeaveEntry(TransferModel objLeaveEntryForTransferModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {

                LoadSession();

                objLeaveEntryForTransferModel.UpdateBy = strEmployeeId;
                objLeaveEntryForTransferModel.HeadOfficeId = strHeadOfficeId;
                objLeaveEntryForTransferModel.BranchOfficeId = strBranchOfficeId;
                objLeaveEntryForTransferModel.EmployeeId = objLeaveEntryForTransferModel.EmployeeId;


                if (objLeaveEntryForTransferModel.Year == null)
                {
                    objLeaveEntryForTransferModel.Year = DateTime.Now.ToString("yyyy");

                }


                //if (objLeaveEntryForTransferModel.EmployeeId != null && objLeaveEntryForTransferModel.Year == null)
                //{
                //    objLeaveEntryForTransferModel.EmployeeModel = objEntryForTransferDal.SearchEmployeeInfoById(objLeaveEntryForTransferModel).EmployeeModel;
                //    ViewBag.LoadEmpLeaveEntryRecord = objEntryForTransferDal.LoadEmpLBRecordForTF(objLeaveEntryForTransferModel);


                //}

                if (objLeaveEntryForTransferModel.Year != null && objLeaveEntryForTransferModel.EmployeeId != null)
                {
                    objLeaveEntryForTransferModel.EmployeeModel = objTransferDAL.SearchEmployeeInfoById(objLeaveEntryForTransferModel).EmployeeModel;
                    ViewBag.LoadEmpLeaveEntryRecord = objTransferDAL.LoadEmpLBRecordForTF(objLeaveEntryForTransferModel);

                }

                //else
                //{
                //   // objLeaveEntryForTransferModel.EmployeeModel = objEntryForTransferDal.SearchEmployeeInfoById(objLeaveEntryForTransferModel).EmployeeModel;

                //}

                //objLeaveEntryForTransferModel.EmployeeModel = objEntryForTransferDal.SearchEmployeeInfoById(objLeaveEntryForTransferModel).EmployeeModel;

                //ViewBag.LoadEmpLeaveEntryRecord = objEntryForTransferDal.LoadEmpLBRecordForTF(objLeaveEntryForTransferModel);


                //ViewBag.GetEmployeeRecordById = objEntryForTransferDal.SearchEmployeeInfoById(objLeaveEntryForTransferModel);


                ViewBag.LeaveTypeByDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLeaveTypeDDList(), "LEAVE_TYPE_ID", "LEAVE_TYPE_NAME");

                //ViewBag.Year = DateTime.Now.ToString("yyyy");
                //objLeaveEntryForTransferModel.Year = DateTime.Now.ToString("yyyy");


                return View("EmpLeaveEntry", objLeaveEntryForTransferModel);
            }
        }



        public List<TransferModel> EmpLeaveListDataForEdit(DataTable dt1)
        {
            List<TransferModel> leaveRequestDataBundle = new List<TransferModel>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                TransferModel objLeaveEntryForTransferModel = new TransferModel();

                // objAccessoriesOrderModel.SerialNumber = dt2.Rows[i]["SL"].ToString();
                objLeaveEntryForTransferModel.EmployeeId = dt1.Rows[i]["EMPLOYEE_ID"].ToString();
                objLeaveEntryForTransferModel.LeaveTypeId = dt1.Rows[i]["LEAVE_TYPE_ID"].ToString();
                objLeaveEntryForTransferModel.LeaveBalance = dt1.Rows[i]["MAX_LEAVE"].ToString();
                objLeaveEntryForTransferModel.Remarks = dt1.Rows[i]["REMARKS"].ToString();
                objLeaveEntryForTransferModel.Year = dt1.Rows[i]["LEAVE_YEAR"].ToString();
                leaveRequestDataBundle.Add(objLeaveEntryForTransferModel);
            }

            return leaveRequestDataBundle;
        }

        [HttpGet]
        public ActionResult EditLeaveEntry(string pLeaveTypeId, string pEmployeeId, string pYear)
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
                    objTransferModel.EmployeeId = pEmployeeId;
                    objTransferModel.LeaveTypeId = pLeaveTypeId;
                    objTransferModel.Year = pYear;


                    objTransferModel.UpdateBy = strEmployeeId;

                    objTransferModel.HeadOfficeId = strHeadOfficeId;
                    objTransferModel.BranchOfficeId = strBranchOfficeId;

                    ViewBag.LeaveTypeByDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLeaveTypeDDList(), "LEAVE_TYPE_ID", "LEAVE_TYPE_NAME");



                    DataTable dt1 = objTransferDAL.GetEmpLeaveTransferEdit(objTransferModel);
                    ViewBag.LoadLeaveTransferEdit = EmpLeaveListDataForEdit(dt1);
                    ViewBag.LoadEmpLeaveEntryRecord = objTransferDAL.LoadEmpLBRecordForTF(objTransferModel);
                    ViewBag.GetEmployeeRecordById = objTransferDAL.SearchEmployeeInfoById(objTransferModel);

                    //ViewBag.LoadEmpLeaveEntryRecord = objEntryForTransferDal.EmployeeLeaveSaveForTransfer(objLeaveEntryForTransferModel);

                    //objLeaveRequestModel.CurrentYear = pLeaveYear;

                    //ViewBag.GetAllLeaveRequestRecordList = objLeaveRequestDal.GetAllLeaveRequestRecord(objLeaveRequestModel);
                    //ViewBag.LoadEmployeeLeaveSummeryRecordList = objLeaveRequestDal.LoadEmployeeLeaveSummeryRecord(objLeaveRequestModel);

                    //objLeaveRequestModel = objLeaveRequestDal.GetAllLeaveRequestEmployeeRecord(objLeaveRequestModel);





                    return View("EmpLeaveEntry", objTransferModel);

                }
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }




        //EmpTransferById Form

        [HttpGet]
        public ActionResult GetEmployeeTransferById(EmployeeTransferByIdModel objLEmployeeTransferById, string pSubmit)   //View Page
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {

                LoadSession();

                objLEmployeeTransferById.UpdateBy = strEmployeeId;
                objLEmployeeTransferById.HeadOfficeId = strHeadOfficeId;
                objLEmployeeTransferById.BranchOfficeId = strBranchOfficeId;
                objLEmployeeTransferById.EmployeeId = objLEmployeeTransferById.EmployeeId;

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                if (objLEmployeeTransferById.UnitId != null)
                {
                    if (objLEmployeeTransferById.DepartmentId != null) //check dept id is selected or not,  if not then bypass to else
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objLEmployeeTransferById.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objLEmployeeTransferById.DepartmentId);

                        if (objLEmployeeTransferById.SectionId != null) //check section id is selected or not, if not then bypass to else 
                        {

                            ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objLEmployeeTransferById.DepartmentId,
                                strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objLEmployeeTransferById.SectionId);

                            if (objLEmployeeTransferById.SubSectionId != null)//check sub-section id is selected or not, if not then bypass to else 
                            {

                                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objLEmployeeTransferById.SectionId,
                                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objLEmployeeTransferById.SubSectionId);

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


                if (objLEmployeeTransferById.TransferYear == null)
                {
                    objLEmployeeTransferById.TransferYear = DateTime.Now.ToString("yyyy");

                }


               

                //if (objLeaveEntryForTransferModel.EmployeeId != null && objLeaveEntryForTransferModel.Year == null)
                //{
                //    objLeaveEntryForTransferModel.EmployeeModel = objEntryForTransferDal.SearchEmployeeInfoById(objLeaveEntryForTransferModel).EmployeeModel;
                //    ViewBag.LoadEmpLeaveEntryRecord = objEntryForTransferDal.LoadEmpLBRecordForTF(objLeaveEntryForTransferModel);


                //}

                if (objLEmployeeTransferById.TransferYear != null && objLEmployeeTransferById.EmployeeId != null)
                {
                    objLEmployeeTransferById.EmployeeModel = objTransferDAL.SearchEmployeeTransferById(objLEmployeeTransferById).EmployeeModel;
                    DataTable dt2 = objTransferDAL.LoadEmpTransferRecord(objLEmployeeTransferById);
                    ViewBag.LoadTransferEntryRecord = EmpLeaveTransferListData(dt2);

                }


                if (pSubmit == "1")
                {
                    objTransferDAL.EmployeeTransferProcess(objLEmployeeTransferById);
                }
                //else
                //{
                //   // objLeaveEntryForTransferModel.EmployeeModel = objEntryForTransferDal.SearchEmployeeInfoById(objLeaveEntryForTransferModel).EmployeeModel;

                //}

                //objLeaveEntryForTransferModel.EmployeeModel = objEntryForTransferDal.SearchEmployeeInfoById(objLeaveEntryForTransferModel).EmployeeModel;

                //ViewBag.LoadEmpLeaveEntryRecord = objEntryForTransferDal.LoadEmpLBRecordForTF(objLeaveEntryForTransferModel);


                //ViewBag.GetEmployeeRecordById = objEntryForTransferDal.SearchEmployeeInfoById(objLeaveEntryForTransferModel);


                ViewBag.LeaveTypeByDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetLeaveTypeDDList(), "LEAVE_TYPE_ID", "LEAVE_TYPE_NAME");

                //ViewBag.Year = DateTime.Now.ToString("yyyy");
                //objLeaveEntryForTransferModel.Year = DateTime.Now.ToString("yyyy");


                return View("GetEmployeeTransferById", objLEmployeeTransferById);
            }
        }

        public List<EmployeeTransferByIdModel> EmpLeaveTransferListData(DataTable dt2)
        {
            List<EmployeeTransferByIdModel> transferList = new List<EmployeeTransferByIdModel>();

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                //EMPLOYEE ID	TRANSFER YEAR	TRANSFER MONTH	UNIT OLD	DEPARTMENT OLD	SECTION OLD	SUB SECTION OLD	UNIT NEW	DEPARTMENT NEW	SECTION NEW	SUB SECTION NEW
                EmployeeTransferByIdModel objEmployeeTransferByIdModel = new EmployeeTransferByIdModel();

                // objAccessoriesOrderModel.SerialNumber = dt2.Rows[i]["SL"].ToString();
                objEmployeeTransferByIdModel.EmployeeId = dt2.Rows[i]["EMPLOYEE_ID"].ToString();
                objEmployeeTransferByIdModel.TransferYear = dt2.Rows[i]["TRANSFER_YEAR"].ToString();
                objEmployeeTransferByIdModel.TransferMonth = dt2.Rows[i]["TRANSFER_MONTH"].ToString();
                objEmployeeTransferByIdModel.UnitOld = dt2.Rows[i]["UNIT_NAME"].ToString();
                objEmployeeTransferByIdModel.DepartmentOld = dt2.Rows[i]["DEPARTMENT_NAME"].ToString();
                objEmployeeTransferByIdModel.SectionOld = dt2.Rows[i]["SECTION_NAME"].ToString();
                objEmployeeTransferByIdModel.SubSectionOld = dt2.Rows[i]["SUB_SECTION_NAME"].ToString();
                objEmployeeTransferByIdModel.UnitNameNew = dt2.Rows[i]["UNIT_NAME_NEW"].ToString();
                objEmployeeTransferByIdModel.DepartmentNameNew = dt2.Rows[i]["DEPARTMENT_NAME_NEW"].ToString();
                objEmployeeTransferByIdModel.SectionIdNameNew = dt2.Rows[i]["SECTION_NAME_NEW"].ToString();
                objEmployeeTransferByIdModel.SubSectionIdNameNew = dt2.Rows[i]["SUB_SECTION_NAME_NEW"].ToString();
                transferList.Add(objEmployeeTransferByIdModel);
            }

            return transferList;
        }



    }
}