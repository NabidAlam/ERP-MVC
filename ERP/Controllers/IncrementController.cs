using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.DAL;
using ERP.MODEL;
using ERP.Utility;

namespace ERP.Controllers
{
    public class IncrementController : Controller
    {

        #region Common

        private LookUpDAL objLookUpDAL = new LookUpDAL();
        private readonly SalaryDAL objSalaryDAL = new SalaryDAL();
        private IncrementDAL objIncrementDAL = new IncrementDAL();
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
        // GET: Increment
        public ActionResult Index()
        {
            return View();
        }

        #region Increment Entry

        public ActionResult IncrementEntry()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                IncrementEntryModel objIncrementModel = new IncrementEntryModel
                {

                    //objIncrementModel.UpdateBy = strEmployeeId;
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId,
                    Year = objLookUpDAL.GetIncrEffectDate().IncrementYear,

                    IncrementEntryList = new List<IncrementEntryModel>()
                };



                if (TempData.ContainsKey("ProcessIncr") && (int)TempData["ProcessIncr"] == 1)
                {
                    IncrementEntryModel model = TempData["IncrementEntryModel"] as IncrementEntryModel;

                    objIncrementModel.UnitId = model.UnitId;




                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    if (objIncrementModel.UnitId != null)
                    {
                        objIncrementModel.DepartmentId = model.DepartmentId;
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objIncrementModel.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objIncrementModel.DepartmentId);

                        if (objIncrementModel.DepartmentId != null)
                        {
                            objIncrementModel.SectionId = model.SectionId;
                            ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objIncrementModel.DepartmentId,
                                strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objIncrementModel.SectionId);

                            if (objIncrementModel.SectionId != null)
                            {
                                objIncrementModel.SubSectionId = model.SubSectionId;
                                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objIncrementModel.SectionId,
                                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objIncrementModel.SubSectionId);
                            }
                            else
                            {
                                objIncrementModel.SectionId = null;
                            }
                        }
                        else
                        {
                            objIncrementModel.DepartmentId = null;
                        }
                    }

                    objIncrementModel.IncrementEntryList = objIncrementDAL.GetIncrementEntryList(objIncrementModel);//load table with employee
                }
                else
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                }




                return View(objIncrementModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IncrementEntry(IncrementEntryModel objIncrementModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objIncrementModel.UpdateBy = strEmployeeId;
                objIncrementModel.HeadOfficeId = strHeadOfficeId;
                objIncrementModel.BranchOfficeId = strBranchOfficeId;
                objIncrementModel.Year = objLookUpDAL.GetIncrEffectDate().IncrementYear;

                if (objIncrementModel.IncrementEntryList != null)
                {
                    string vMessage;
                    objIncrementModel.IncrementEntryList.RemoveAll(i => !i.IsChecked);
                    vMessage = objIncrementDAL.SaveEmployeeIncrement(objIncrementModel); // save increment amount
                    TempData["OperationMessage"] = vMessage;
                    //ModelState.Clear();

                }

                else
                {
                    objIncrementDAL.EmployeeIncrement(objIncrementModel); // processed employee information

                }

                objIncrementModel.IncrementEntryList = objIncrementDAL.GetIncrementEntryList(objIncrementModel);//load table with employee information

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                if (objIncrementModel.UnitId != null)
                {
                    ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                    if (objIncrementModel.DepartmentId != null)
                    {
                        ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objIncrementModel.UnitId,
                            strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objIncrementModel.DepartmentId);
                    }
                    else
                    {
                        ViewBag.DepartmentDDList = null;
                    }

                    if (objIncrementModel.SectionId != null)
                    {
                        ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objIncrementModel.DepartmentId,
                            strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objIncrementModel.SectionId);
                    }
                    else
                    {
                        ViewBag.SectionDDList = null;
                    }

                    if (objIncrementModel.SubSectionId != null)
                    {
                        ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objIncrementModel.SectionId,
                            strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objIncrementModel.SubSectionId);
                    }
                    else
                    {
                        ViewBag.SubSectionDDList = null;
                    }
                }

                return View(objIncrementModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ProcessIncrementEntry(IncrementEntryModel objIncrementModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objIncrementModel.UpdateBy = strEmployeeId;
                objIncrementModel.HeadOfficeId = strHeadOfficeId;
                objIncrementModel.BranchOfficeId = strBranchOfficeId;
                objIncrementModel.Year = objLookUpDAL.GetIncrEffectDate().IncrementYear;

                string vMsg = "";

                vMsg = objIncrementDAL.ProcessEmployeeIncrement(objIncrementModel);
                TempData["OperationMessage"] = vMsg;

                TempData["IncrementEntryModel"] = objIncrementModel;

                TempData["ProcessIncr"] = 1;
                return RedirectToAction("IncrementEntry");
            }

        }
        #endregion
    }
}