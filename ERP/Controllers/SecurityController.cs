using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.DAL;
using ERP.MODEL;
using ERP.Utility;

namespace ERP.Controllers
{
    public class SecurityController : Controller
    {
        LookUpDAL objLookUpDAL = new LookUpDAL();
        SecurityDAL objSecurityDAL = new SecurityDAL();


   

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



        public ActionResult EmployeePasswordList(PasswordListModel objPasswordListModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {

                LoadSession();

                objPasswordListModel.UpdateBy = strEmployeeId;
                objPasswordListModel.HeadOfficeId = strHeadOfficeId;
                objPasswordListModel.BranchOfficeId = strBranchOfficeId;

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");
                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDList(strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME");
                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDList(strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME");
                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDList(strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME");



               
                

                DataTable dt = objSecurityDAL.LoadEmployeePasswordList(objPasswordListModel);
                objPasswordListModel.EmployeeList = EmployeeJobConfirmationListData(dt);


             


                return View(objPasswordListModel);
            }
        }



       

      
        public List<PasswordListModel> EmployeeJobConfirmationListData(DataTable dt)
        {
            List<PasswordListModel> EmployeeJobConfirmationDataBundle = new List<PasswordListModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PasswordListModel objPasswordListModel = new PasswordListModel();

                objPasswordListModel.EmployeeId = dt.Rows[i]["EMPLOYEE_ID"].ToString();
                objPasswordListModel.EmployeeName = dt.Rows[i]["EMPLOYEE_NAME"].ToString();
                objPasswordListModel.Password = dt.Rows[i]["EMPLOYEE_PASSWORD"].ToString();
                objPasswordListModel.SoftwareName = dt.Rows[i]["SOFTWARE_NAME"].ToString();
                objPasswordListModel.JoiningDate = dt.Rows[i]["JOINING_DATE"].ToString();

                if (dt.Rows[i]["EMPLOYEE_PIC"].ToString() != "")
                {
                    objPasswordListModel.ImageFileByte = (byte[])dt.Rows[i]["EMPLOYEE_PIC"];
                    objPasswordListModel.ImageFileNameBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(objPasswordListModel.ImageFileByte);
                }

                EmployeeJobConfirmationDataBundle.Add(objPasswordListModel);
            }
            return EmployeeJobConfirmationDataBundle;
        }
    }
}