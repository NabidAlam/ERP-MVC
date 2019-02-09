using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.DAL;
using ERP.MODEL;
using System.Data;
using PagedList;
using System.Net;
using ERP.Utility;

namespace ERP.Controllers
{
    public class LookUpController : Controller
    {
        #region "Common"

        LookUpDAL objLookUpDAL = new LookUpDAL();
        EmployeeDAL objEmployeeDAL = new EmployeeDAL();

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

        #region Blood Group

        BloodGroupModel objBloodGroupModel = new BloodGroupModel();

        [HttpGet]
        public ActionResult GetBloodGroupRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objBloodGroupModel.UpdateBy = strEmployeeId;
                objBloodGroupModel.HeadOfficeId = strHeadOfficeId;
                objBloodGroupModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objBloodGroupModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objBloodGroupModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objBloodGroupModel.BloodGroupId = pId;
                    objBloodGroupModel = objLookUpDAL.GetBloodGroupById(objBloodGroupModel);

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

                DataTable dt = objLookUpDAL.GetBloodGroupRecord(objBloodGroupModel);

                var bloodGroupList = BloodGroupListData(dt);
                ViewBag.BloodGroupList = bloodGroupList.ToPagedList(page, pageSize);

                return View(objBloodGroupModel);
            }
        }

        public List<BloodGroupModel> BloodGroupListData(DataTable dt)
        {
            List<BloodGroupModel> BloodGroupDataBundle = new List<BloodGroupModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BloodGroupModel objLookUpModel = new BloodGroupModel();
                objLookUpModel.BloodGroupId = dt.Rows[i]["BLOOD_GROUP_ID"].ToString();
                objLookUpModel.BloodGroupName = dt.Rows[i]["BLOOD_GROUP_NAME"].ToString();
                objLookUpModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                BloodGroupDataBundle.Add(objLookUpModel);
            }

            return BloodGroupDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBloodGroup(BloodGroupModel objLookUpModel)
        {
            LoadSession();
            objLookUpModel.UpdateBy = strEmployeeId;
            objLookUpModel.HeadOfficeId = strHeadOfficeId;
            objLookUpModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveBloodGroup(objLookUpModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion


            return RedirectToAction("GetBloodGroupRecord");
        }

        public ActionResult ClearBloodGroup()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetBloodGroupRecord");
        }

        #endregion

        #region Department

        DepartmentModel objDepartmentModel = new DepartmentModel();
        UnitModel objUnitModel = new UnitModel();

        [HttpGet]
        public ActionResult GetDepartmentRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objDepartmentModel.UpdateBy = strEmployeeId;
                objDepartmentModel.HeadOfficeId = strHeadOfficeId;
                objDepartmentModel.BranchOfficeId = strBranchOfficeId;

                objUnitModel.UpdateBy = strEmployeeId;
                objUnitModel.HeadOfficeId = strHeadOfficeId;
                objUnitModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objDepartmentModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objDepartmentModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objDepartmentModel.DepartmentId = pId;
                    objDepartmentModel = objLookUpDAL.GetDepartmentById(objDepartmentModel);

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

                DataTable dt = objLookUpDAL.GetDepartmentRecord(objDepartmentModel);

                var departmentList = DepartmentListData(dt);
                ViewBag.DepartmentList = departmentList.ToPagedList(page, pageSize);
                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitRecord(objUnitModel), "UNIT_ID", "UNIT_NAME");

                return View(objDepartmentModel);
            }

        }

        public List<DepartmentModel> DepartmentListData(DataTable dt)
        {
            List<DepartmentModel> DepartmentDataBundle = new List<DepartmentModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DepartmentModel objDeptDepartmentLookUp = new DepartmentModel();

                objDeptDepartmentLookUp.DepartmentId = dt.Rows[i]["DEPARTMENT_ID"].ToString();
                objDeptDepartmentLookUp.DepartmentCode = dt.Rows[i]["DEPARTMENT_CODE"].ToString();
                objDeptDepartmentLookUp.DepartmentName = dt.Rows[i]["DEPARTMENT_NAME"].ToString();
                objDeptDepartmentLookUp.UnitId = dt.Rows[i]["UNIT_ID"].ToString();
                objDeptDepartmentLookUp.UnitName = dt.Rows[i]["UNIT_NAME"].ToString();
                objDeptDepartmentLookUp.DepartmentNameBangla = dt.Rows[i]["DEPARTMENT_NAME_BANGLA"].ToString();

                objDeptDepartmentLookUp.SerialNumber = dt.Rows[i]["sl"].ToString();

                DepartmentDataBundle.Add(objDeptDepartmentLookUp);
            }

            return DepartmentDataBundle;
        }

        //Save Department Start
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDepartmentInfo(DepartmentModel objDepartmentLookUp)
        {
            LoadSession();

            objDepartmentLookUp.UpdateBy = strEmployeeId;
            objDepartmentLookUp.HeadOfficeId = strHeadOfficeId;
            objDepartmentLookUp.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveDepartmentInfo(objDepartmentLookUp);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion


            return RedirectToAction("GetDepartmentRecord");
        }

        public ActionResult ClearDepartment()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetDepartmentRecord");
        }

        #endregion

        #region Gender

        GenderModel objGenderModel = new GenderModel();

        public ActionResult GetGenderRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                CheckUrl();

                objGenderModel.UpdateBy = strEmployeeId;
                objGenderModel.HeadOfficeId = strHeadOfficeId;
                objGenderModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objGenderModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objGenderModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objGenderModel.GenderId = pId;
                    objGenderModel = objLookUpDAL.GetGenderById(objGenderModel);

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

                DataTable dt = objLookUpDAL.GetGenderRecord(objGenderModel);

                var genderList = GenderListData(dt);
                ViewBag.GenderList = genderList.ToPagedList(page, pageSize);
                return View(objGenderModel);
            }
        }

        public List<GenderModel> GenderListData(DataTable dt)
        {
            List<GenderModel> GenderDataBundle = new List<GenderModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GenderModel objGender = new GenderModel();


                objGender.GenderId = dt.Rows[i]["Gender_ID"].ToString();
                objGender.GenderName = dt.Rows[i]["Gender_NAME"].ToString();
                objGender.SerialNumber = dt.Rows[i]["sl"].ToString();
                GenderDataBundle.Add(objGender);
            }
            return GenderDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveGenderInfo(GenderModel objGenderModel)
        {
            LoadSession();
            objGenderModel.UpdateBy = strEmployeeId;
            objGenderModel.HeadOfficeId = strHeadOfficeId;
            objGenderModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveGenderInfo(objGenderModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion


            return RedirectToAction("GetGenderRecord");
        }

        public ActionResult ClearGenderInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetGenderRecord");
        }

        #endregion

        #region Marital Status

        MaritalStatusModel objMaritalStatusModel = new MaritalStatusModel();

        public ActionResult GetMaritalStatusRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objMaritalStatusModel.UpdateBy = strEmployeeId;
                objMaritalStatusModel.HeadOfficeId = strHeadOfficeId;
                objMaritalStatusModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objMaritalStatusModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objMaritalStatusModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objMaritalStatusModel.MaritalStatusId = pId;
                    objMaritalStatusModel = objLookUpDAL.GetMaritalStatusById(objMaritalStatusModel);

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


                DataTable dt = objLookUpDAL.GetMaritalStatusRecord(objMaritalStatusModel);
                var maritalStatusList = MaritalStatusListData(dt);
                ViewBag.MaritalStatus = maritalStatusList.ToPagedList(page, pageSize);
                return View(objMaritalStatusModel);
            }
        }

        public List<MaritalStatusModel> MaritalStatusListData(DataTable dt)
        {
            List<MaritalStatusModel> MaritalStatusDataBundle = new List<MaritalStatusModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MaritalStatusModel objMaritalStatus = new MaritalStatusModel();

                objMaritalStatus.MaritalStatusId = dt.Rows[i]["MARITAL_STATUS_ID"].ToString();
                objMaritalStatus.MaritalStatusName = dt.Rows[i]["MARITAL_STATUS_NAME"].ToString();
                objMaritalStatus.SerialNumber = dt.Rows[i]["sl"].ToString();

                MaritalStatusDataBundle.Add(objMaritalStatus);
            }

            return MaritalStatusDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMaritalStatusInfo(MaritalStatusModel objMaritalStatusModel)
        {
            LoadSession();
            objMaritalStatusModel.UpdateBy = strEmployeeId;
            objMaritalStatusModel.HeadOfficeId = strHeadOfficeId;
            objMaritalStatusModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveMaritalStatusInfo(objMaritalStatusModel);
                TempData["OperationMessage"] = strDBMsg;
            }
            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion


            return RedirectToAction("GetMaritalStatusRecord");
        }

        public ActionResult ClearMaritalStatusInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetMaritalStatusRecord");
        }

        #endregion

        #region District

        DistrictModel objDistrictModel = new DistrictModel();

        [HttpGet]
        public ActionResult GetDistrictRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objDistrictModel.UpdateBy = strEmployeeId;
                objDistrictModel.HeadOfficeId = strHeadOfficeId;
                objDistrictModel.BranchOfficeId = strBranchOfficeId;

                objDivisionModel.UpdateBy = strEmployeeId;
                objDivisionModel.HeadOfficeId = strHeadOfficeId;
                objDivisionModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objDistrictModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objDistrictModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objDistrictModel.DistrictId = pId;
                    objDistrictModel = objLookUpDAL.GetDistrictById(objDistrictModel);

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


                DataTable dt = objLookUpDAL.GetDistrictRecord(objDistrictModel);
                var districtList = DistrictListData(dt);
                ViewBag.DistrictList = districtList.ToPagedList(page, pageSize);
                ViewBag.DivisionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDivisionRecord(objDivisionModel), "DIVISION_ID", "DIVISION_NAME");

                return View(objDistrictModel);
            }
        }

        public List<DistrictModel> DistrictListData(DataTable dt)
        {
            List<DistrictModel> DistrictDataBundle = new List<DistrictModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DistrictModel objDistrict = new DistrictModel();

                objDistrict.DistrictId = dt.Rows[i]["DISTRICT_ID"].ToString();
                objDistrict.DistrictName = dt.Rows[i]["DISTRICT_NAME"].ToString();
                objDistrict.DivisionId = dt.Rows[i]["DIVISION_ID"].ToString();
                objDistrict.DivisionName = dt.Rows[i]["DIVISION_NAME"].ToString();
                objDistrict.SerialNumber = dt.Rows[i]["sl"].ToString();

                DistrictDataBundle.Add(objDistrict);
            }
            return DistrictDataBundle;
        }

        //Save Blood Group Start
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDistrictInfo(DistrictModel objDistrictModel)
        {
            LoadSession();
            objDistrictModel.UpdateBy = strEmployeeId;
            objDistrictModel.HeadOfficeId = strHeadOfficeId;
            objDistrictModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveDistrictInfo(objDistrictModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetDistrictRecord");
        }

        public ActionResult ClearDistrict()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";

            return RedirectToAction("GetDistrictRecord");
        }

        #endregion

        #region Division

        DivisionModel objDivisionModel = new DivisionModel();

        public ActionResult GetDivisionRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objDivisionModel.UpdateBy = strEmployeeId;
                objDivisionModel.HeadOfficeId = strHeadOfficeId;
                objDivisionModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objDivisionModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objDivisionModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objDivisionModel.DivisionId = pId;
                    objDivisionModel = objLookUpDAL.GetDivisionById(objDivisionModel);

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

                DataTable dt = objLookUpDAL.GetDivisionRecord(objDivisionModel);
                var divisionList = DivisionListData(dt);
                ViewBag.Division = divisionList.ToPagedList(page, pageSize);

                return View(objDivisionModel);
            }
        }

        public List<DivisionModel> DivisionListData(DataTable dt)
        {
            List<DivisionModel> DivisionDataBundle = new List<DivisionModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DivisionModel objDivision = new DivisionModel();

                objDivision.DivisionId = dt.Rows[i]["DIVISION_ID"].ToString();
                objDivision.DivisionName = dt.Rows[i]["DIVISION_NAME"].ToString();
                objDivision.SerialNumber = dt.Rows[i]["sl"].ToString();

                DivisionDataBundle.Add(objDivision);
            }

            return DivisionDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDivisionInfo(DivisionModel objDivisionModel)
        {
            LoadSession();
            objMaritalStatusModel.UpdateBy = strEmployeeId;
            objDivisionModel.HeadOfficeId = strHeadOfficeId;
            objDivisionModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveDivisionInfo(objDivisionModel);
                TempData["OperationMessage"] = strDBMsg;
            }
            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetDivisionRecord");
        }

        public ActionResult ClearDivisionInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetDivisionRecord");
        }

        #endregion

        #region Country 

        CountryModel objCountryModel = new CountryModel();

        public ActionResult GetCountryRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objCountryModel.UpdateBy = strEmployeeId;
                objCountryModel.HeadOfficeId = strHeadOfficeId;
                objCountryModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objCountryModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objCountryModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objCountryModel.CountryId = pId;
                    objCountryModel = objLookUpDAL.GetCountryById(objCountryModel);

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

                DataTable dt = objLookUpDAL.GetCountryRecord(objCountryModel);

                var countryList = CountryListData(dt);
                ViewBag.Country = countryList.ToPagedList(page, pageSize);

                return View(objCountryModel);
            }

        }

        public List<CountryModel> CountryListData(DataTable dt)
        {
            List<CountryModel> countryDataBundle = new List<CountryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CountryModel objCountry = new CountryModel();

                objCountry.CountryId = dt.Rows[i]["COUNTRY_ID"].ToString();
                objCountry.CountryName = dt.Rows[i]["COUNTRY_NAME"].ToString();
                objCountry.SerialNumber = dt.Rows[i]["sl"].ToString();

                countryDataBundle.Add(objCountry);
            }

            return countryDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCountryInfo(CountryModel objCountryModel)
        {
            LoadSession();
            objCountryModel.UpdateBy = strEmployeeId;
            objCountryModel.HeadOfficeId = strHeadOfficeId;
            objCountryModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveCountryInfo(objCountryModel);
                TempData["OperationMessage"] = strDBMsg;
            }
            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion
            return RedirectToAction("GetCountryRecord");
        }

        [HttpGet]
        public ActionResult EditCountryInfo(string pId)
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

                    objCountryModel.CountryId = pId.Trim();
                    objCountryModel.UpdateBy = strEmployeeId;
                    objCountryModel.HeadOfficeId = strHeadOfficeId;
                    objCountryModel.BranchOfficeId = strBranchOfficeId;

                    int vPageNumber = 1;

                    if (!string.IsNullOrWhiteSpace(objCountryModel.CountryId))
                    {
                        objCountryModel = objLookUpDAL.GetCountryById(objCountryModel);
                    }
                    if (Convert.ToInt32(TempData["SearchActionFlag"]) == 1)
                    {
                        TempData.Keep("SearchActionFlag");
                        vPageNumber = Convert.ToInt32(TempData["PageNumberFromSearch"]);
                        TempData.Keep("PageNumberFromSearch");
                        objCountryModel.SearchBy = TempData["SearchValueFromSearchAction"].ToString();
                        TempData.Keep("SearchValueFromSearchAction");
                    }

                    if (Convert.ToInt32(TempData["GetActionFlag"]) == 1)
                    {
                        TempData.Keep("GetActionFlag");
                        vPageNumber = Convert.ToInt32(TempData["PageNumberFromSearch"]);
                        TempData.Keep("PageNumberFromSearch");
                    }
                    TempData["EditActionFlag"] = 1;
                    TempData["PageNumberFromEditAction"] = vPageNumber;
                    DataTable dt = objLookUpDAL.GetCountryRecord(objCountryModel);
                    var countryList = CountryListData(dt);
                    ViewBag.Country = countryList.ToPagedList(vPageNumber, 10);

                    Session["EditPageNumber"] = vPageNumber;
                    Session["EditFlag"] = 1;

                    return View("GetCountryRecord", objCountryModel);
                }
            }

            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        public ActionResult ClearCountryInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetCountryRecord");
        }

        #endregion

        #region Religion

        ReligionModel objReligionModel = new ReligionModel();

        public ActionResult GetReligionRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objReligionModel.UpdateBy = strEmployeeId;
                objReligionModel.HeadOfficeId = strHeadOfficeId;
                objReligionModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objReligionModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objReligionModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objReligionModel.ReligionId = pId;
                    objReligionModel = objLookUpDAL.GetReligionById(objReligionModel);

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


                DataTable dt = objLookUpDAL.GetReligionRecord(objReligionModel);

                var religionList = ReligionListData(dt);
                ViewBag.Religion = religionList.ToPagedList(page, pageSize);

                return View(objReligionModel);
            }
        }

        public List<ReligionModel> ReligionListData(DataTable dt)
        {
            List<ReligionModel> religionDataBundle = new List<ReligionModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ReligionModel objReligion = new ReligionModel();

                objReligion.ReligionId = dt.Rows[i]["RELIGION_ID"].ToString();
                objReligion.ReligionName = dt.Rows[i]["RELIGION_NAME"].ToString();
                objReligion.SerialNumber = dt.Rows[i]["sl"].ToString();

                religionDataBundle.Add(objReligion);
            }

            return religionDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveReligionInfo(ReligionModel objReligionModel)
        {
            LoadSession();
            objReligionModel.UpdateBy = strEmployeeId;
            objReligionModel.HeadOfficeId = strHeadOfficeId;
            objReligionModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveReligionInfo(objReligionModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetReligionRecord");
        }

        public ActionResult ClearReligionInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetReligionRecord");
        }

        #endregion

        #region Occurance Type

        OccurrenceTypeModel objOccurrenceTypeModel = new OccurrenceTypeModel();

        public ActionResult GetOccurrenceTypeRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objOccurrenceTypeModel.UpdateBy = strEmployeeId;
                objOccurrenceTypeModel.HeadOfficeId = strHeadOfficeId;
                objOccurrenceTypeModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objOccurrenceTypeModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objOccurrenceTypeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objOccurrenceTypeModel.OccurenceTypeId = pId;
                    objOccurrenceTypeModel = objLookUpDAL.GetOccuranceTypeById(objOccurrenceTypeModel);

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

                DataTable dt = objLookUpDAL.GetOccurenceTypeRecord(objOccurrenceTypeModel);

                var occurenceTypeList = OccuranceTypeListData(dt);
                ViewBag.OccurenceType = occurenceTypeList.ToPagedList(page, pageSize);
                return View(objOccurrenceTypeModel);
            }
        }

        public List<OccurrenceTypeModel> OccuranceTypeListData(DataTable dt)
        {
            List<OccurrenceTypeModel> occurenceTypeDataBundle = new List<OccurrenceTypeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OccurrenceTypeModel objOccurenceType = new OccurrenceTypeModel();

                objOccurenceType.OccurenceTypeId = dt.Rows[i]["Occurence_Type_ID"].ToString();
                objOccurenceType.OccurenceTypeName = dt.Rows[i]["Occurence_Type_NAME"].ToString();
                objOccurenceType.SerialNumber = dt.Rows[i]["sl"].ToString();

                occurenceTypeDataBundle.Add(objOccurenceType);
            }
            return occurenceTypeDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveOccuranceTypeInfo(OccurrenceTypeModel objOccurrenceTypeModel)
        {
            LoadSession();
            objOccurrenceTypeModel.UpdateBy = strEmployeeId;
            objOccurrenceTypeModel.HeadOfficeId = strHeadOfficeId;
            objOccurrenceTypeModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveOccurenceTypeInfo(objOccurrenceTypeModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetOccurrenceTypeRecord");
        }

        public ActionResult ClearOccurenceTypeInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetOccurrenceTypeRecord");
        }

        #endregion

        #region JOB TYPE ENTRY 

        public List<JobTypeModel> JobTypeListData(DataTable dt)
        {
            List<JobTypeModel> JobTypeDataBundle = new List<JobTypeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JobTypeModel objJobTypeModel = new JobTypeModel();
                objJobTypeModel.JobTypeId = dt.Rows[i]["JOB_TYPE_ID"].ToString();
                objJobTypeModel.JobTypeName = dt.Rows[i]["JOB_TYPE_NAME"].ToString();
                objJobTypeModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                JobTypeDataBundle.Add(objJobTypeModel);
            }

            return JobTypeDataBundle;
        }

        [HttpGet]
        public ActionResult GetJobTypeRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            JobTypeModel objJobTypeModel = new JobTypeModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();



                objJobTypeModel.UpdateBy = strEmployeeId;
                objJobTypeModel.HeadOfficeId = strHeadOfficeId;
                objJobTypeModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objJobTypeModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objJobTypeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objJobTypeModel.JobTypeId = pId;
                    objJobTypeModel = objLookUpDAL.GetJobTypeById(objJobTypeModel);

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

                DataTable dt = objLookUpDAL.GetJobTypeRecord(objJobTypeModel);
                var JobTypeList = JobTypeListData(dt);
                ViewBag.JobTypeList = JobTypeList.ToPagedList(page, pageSize);
                return View(objJobTypeModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveJobType(JobTypeModel objJobTypeModel)
        {
            LoadSession();
            objJobTypeModel.UpdateBy = strEmployeeId;
            objJobTypeModel.HeadOfficeId = strHeadOfficeId;
            objJobTypeModel.BranchOfficeId = strBranchOfficeId;
            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveJobType(objJobTypeModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion
            return RedirectToAction("GetJobTypeRecord");
        }


        public ActionResult ClearJobType()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("GetJobTypeRecord");
        }

        #endregion

        #region EMPLOYEE TYPE INFO
        public List<EmployeeTypeInfoModel> EmployeeTypeListData(DataTable dt)
        {
            List<EmployeeTypeInfoModel> EmployeeTypeDataBundle = new List<EmployeeTypeInfoModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                EmployeeTypeInfoModel objEmployeeTypeInfoModel = new EmployeeTypeInfoModel();
                objEmployeeTypeInfoModel.EmployeeTypeId = dt.Rows[i]["EMPLOYEE_TYPE_ID"].ToString();
                objEmployeeTypeInfoModel.EmployeeTypeName = dt.Rows[i]["EMPLOYEE_TYPE_NAME"].ToString();
                objEmployeeTypeInfoModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                EmployeeTypeDataBundle.Add(objEmployeeTypeInfoModel);
            }

            return EmployeeTypeDataBundle;
        }

        [HttpGet]
        public ActionResult GetEmployeeTypeRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            EmployeeTypeInfoModel objEmployeeTypeInfoModel = new EmployeeTypeInfoModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objEmployeeTypeInfoModel.UpdateBy = strEmployeeId;
                objEmployeeTypeInfoModel.HeadOfficeId = strHeadOfficeId;
                objEmployeeTypeInfoModel.BranchOfficeId = strBranchOfficeId;
                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objEmployeeTypeInfoModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objEmployeeTypeInfoModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objEmployeeTypeInfoModel.EmployeeTypeId = pId;
                    objEmployeeTypeInfoModel = objLookUpDAL.GetEmployeeTypeById(objEmployeeTypeInfoModel);

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

                DataTable dt = objLookUpDAL.GetEmployeeTypeRecord(objEmployeeTypeInfoModel);
                var EmployeeTypeList = EmployeeTypeListData(dt);
                ViewBag.EmployeeTypeList = EmployeeTypeList.ToPagedList(page, pageSize);
                return View(objEmployeeTypeInfoModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveEmployeeType(EmployeeTypeInfoModel objEmployeeTypeInfoModel)
        {
            LoadSession();
            objEmployeeTypeInfoModel.UpdateBy = strEmployeeId;
            objEmployeeTypeInfoModel.HeadOfficeId = strHeadOfficeId;
            objEmployeeTypeInfoModel.BranchOfficeId = strBranchOfficeId;
            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveEmployeeType(objEmployeeTypeInfoModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetEmployeeTypeRecord");
        }


        public ActionResult ClearEmployeeType()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("GetEmployeeTypeRecord");
        }

        #endregion

        #region PAYMENT TYPE INFO

        public List<PaymentTypeModel> PaymentTypeListData(DataTable dt)
        {
            List<PaymentTypeModel> PaymentTypeDataBundle = new List<PaymentTypeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PaymentTypeModel objPaymentTypeModel = new PaymentTypeModel();
                objPaymentTypeModel.PaymentTypeId = dt.Rows[i]["PAYMENT_TYPE_ID"].ToString();
                objPaymentTypeModel.PaymentTypeName = dt.Rows[i]["PAYMENT_TYPE_NAME"].ToString();
                objPaymentTypeModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                PaymentTypeDataBundle.Add(objPaymentTypeModel);
            }

            return PaymentTypeDataBundle;
        }

        [HttpGet]
        public ActionResult GetPaymentTypeRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            PaymentTypeModel objPaymentTypeModel = new PaymentTypeModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objPaymentTypeModel.UpdateBy = strEmployeeId;
                objPaymentTypeModel.HeadOfficeId = strHeadOfficeId;
                objPaymentTypeModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objPaymentTypeModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objPaymentTypeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objPaymentTypeModel.PaymentTypeId = pId;
                    objPaymentTypeModel = objLookUpDAL.GetPaymentTypeById(objPaymentTypeModel);

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
                DataTable dt = objLookUpDAL.GetPaymentTypeRecord(objPaymentTypeModel);
                var PaymentTypeList = PaymentTypeListData(dt);
                ViewBag.PaymentTypeList = PaymentTypeList.ToPagedList(page, pageSize);
                return View(objPaymentTypeModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePaymentType(PaymentTypeModel objPaymentTypeModel)
        {
            LoadSession();
            objPaymentTypeModel.UpdateBy = strEmployeeId;
            objPaymentTypeModel.HeadOfficeId = strHeadOfficeId;
            objPaymentTypeModel.BranchOfficeId = strBranchOfficeId;
            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SavePaymentType(objPaymentTypeModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search
            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetPaymentTypeRecord");
        }


        public ActionResult ClearPaymentType()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("GetPaymentTypeRecord");
        }


        #endregion

        #region DESIGNATION TYPE INFO

        public List<DesignationModel> DesignationTypeListData(DataTable dt)
        {
            List<DesignationModel> DesignationTypeDataBundle = new List<DesignationModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DesignationModel objDesignationModel = new DesignationModel();
                objDesignationModel.DesignationId = dt.Rows[i]["DESIGNATION_ID"].ToString();
                objDesignationModel.DesignationName = dt.Rows[i]["DESIGNATION_NAME"].ToString();
                objDesignationModel.DesignationNameBangla = dt.Rows[i]["DESIGNATION_NAME_BANGLA"].ToString() ?? "";
                objDesignationModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                DesignationTypeDataBundle.Add(objDesignationModel);
            }

            return DesignationTypeDataBundle;
        }

        [HttpGet]
        public ActionResult GetDesignationRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            DesignationModel objDesignationModel = new DesignationModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objDesignationModel.UpdateBy = strEmployeeId;
                objDesignationModel.HeadOfficeId = strHeadOfficeId;
                objDesignationModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objDesignationModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objDesignationModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objDesignationModel.DesignationId = pId;
                    objDesignationModel = objLookUpDAL.GetDesignationTypeById(objDesignationModel);

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

                DataTable dt = objLookUpDAL.GetDesignationRecord(objDesignationModel);
                var DesignationTypeList = DesignationTypeListData(dt);
                ViewBag.DesignationTypeList = DesignationTypeList.ToPagedList(page, pageSize);
                return View(objDesignationModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDesignationType(DesignationModel objDesignationModel)
        {
            LoadSession();
            objDesignationModel.UpdateBy = strEmployeeId;
            objDesignationModel.HeadOfficeId = strHeadOfficeId;
            objDesignationModel.BranchOfficeId = strBranchOfficeId;
            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveDesignationType(objDesignationModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetDesignationRecord");
        }


        public ActionResult ClearDesignationType()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("GetDesignationRecord");
        }


        #endregion

        #region UNIT TYPE INFO

        public List<UnitModel> UnitTypeListData(DataTable dt)
        {
            List<UnitModel> UnitTypeDataBundle = new List<UnitModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UnitModel objUnitModel = new UnitModel();
                objUnitModel.UnitCode = dt.Rows[i]["UNIT_CODE"].ToString();
                objUnitModel.UnitId = dt.Rows[i]["UNIT_ID"].ToString();
                objUnitModel.UnitName = dt.Rows[i]["UNIT_NAME"].ToString();
                objUnitModel.UnitNameBangla = dt.Rows[i]["UNIT_NAME_BANGLA"].ToString();

                objUnitModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                UnitTypeDataBundle.Add(objUnitModel);
            }

            return UnitTypeDataBundle;
        }

        [HttpGet]
        public ActionResult GetUnitRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            UnitModel objUnitModel = new UnitModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objUnitModel.UpdateBy = strEmployeeId;
                objUnitModel.HeadOfficeId = strHeadOfficeId;
                objUnitModel.BranchOfficeId = strBranchOfficeId;
                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objUnitModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objUnitModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objUnitModel.UnitId = pId;
                    objUnitModel = objLookUpDAL.GetUnitTypeById(objUnitModel);

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

                DataTable dt = objLookUpDAL.GetUnitRecord(objUnitModel);

                var UnitTypeList = UnitTypeListData(dt);
                ViewBag.UnitTypeList = UnitTypeList.ToPagedList(page, pageSize);
                return View(objUnitModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUnitType(UnitModel objUnitModel)
        {
            LoadSession();
            objUnitModel.UpdateBy = strEmployeeId;
            objUnitModel.HeadOfficeId = strHeadOfficeId;
            objUnitModel.BranchOfficeId = strBranchOfficeId;
            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveUnitType(objUnitModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetUnitRecord");
        }


        public ActionResult ClearUnitType()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("GetUnitRecord");
        }
        #endregion

        #region SECTION TYPE INFO

        public List<SectionModel> SectionTypeListData(DataTable dt)
        {
            List<SectionModel> SectionTypeDataBundle = new List<SectionModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SectionModel objSectionModel = new SectionModel();
                objSectionModel.SectionId = dt.Rows[i]["SECTION_ID"].ToString();
                objSectionModel.SectionName = dt.Rows[i]["SECTION_NAME"].ToString();
                objSectionModel.SectionNameBangla = dt.Rows[i]["SECTION_NAME_BANGLA"].ToString();
                objSectionModel.DepartmentId = dt.Rows[i]["DEPARTMENT_ID"].ToString();
                objSectionModel.DepartmentName = dt.Rows[i]["DEPARTMENT_NAME"].ToString();
                objSectionModel.SectionCode = dt.Rows[i]["SECTION_CODE"].ToString();
                objSectionModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                SectionTypeDataBundle.Add(objSectionModel);

            }

            return SectionTypeDataBundle;
        }

        [HttpGet]
        public ActionResult GetSectionRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            SectionModel objSectionModel = new SectionModel();
            DepartmentModel objDepartmentLookUp = new DepartmentModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objSectionModel.UpdateBy = strEmployeeId;
                objSectionModel.HeadOfficeId = strHeadOfficeId;
                objSectionModel.BranchOfficeId = strBranchOfficeId;

                objDepartmentLookUp.UpdateBy = strEmployeeId;
                objDepartmentLookUp.HeadOfficeId = strHeadOfficeId;
                objDepartmentLookUp.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objSectionModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objSectionModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objSectionModel.SectionId = pId;
                    objSectionModel = objLookUpDAL.GetSectionTypeById(objSectionModel);

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

                DataTable dt = objLookUpDAL.GetSectionRecord(objSectionModel);

                var SectionTypeList = SectionTypeListData(dt);
                ViewBag.SectionTypeList = SectionTypeList.ToPagedList(page, pageSize);
                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmendDDList(), "DEPARTMENT_ID", "DEPARTMENT_NAME");

                return View(objSectionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSectionType(SectionModel objSectionModel)
        {
            LoadSession();
            objSectionModel.UpdateBy = strEmployeeId;
            objSectionModel.HeadOfficeId = strHeadOfficeId;
            objSectionModel.BranchOfficeId = strBranchOfficeId;
            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveSectionType(objSectionModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetSectionRecord");
        }


        public ActionResult ClearSectionType()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";
            return RedirectToAction("GetSectionRecord");
        }


        #endregion

        #region GRADE TYPE INFO

        public List<GradeInfoModel> GradeTypeListData(DataTable dt)
        {
            List<GradeInfoModel> GradeTypeDataBundle = new List<GradeInfoModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GradeInfoModel objGradeInfoModel = new GradeInfoModel();

                objGradeInfoModel.GradeId = dt.Rows[i]["GRADE_ID"].ToString();
                objGradeInfoModel.GradeNo = dt.Rows[i]["GRADE_NO"].ToString();
                objGradeInfoModel.SerialNumber = dt.Rows[i]["sl"].ToString();
                GradeTypeDataBundle.Add(objGradeInfoModel);
            }
            return GradeTypeDataBundle;
        }

        [HttpGet]
        public ActionResult GetGradeRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            GradeInfoModel objGradeInfoModel = new GradeInfoModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objGradeInfoModel.UpdateBy = strEmployeeId;
                objGradeInfoModel.HeadOfficeId = strHeadOfficeId;
                objGradeInfoModel.BranchOfficeId = strBranchOfficeId;
                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objGradeInfoModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objGradeInfoModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objGradeInfoModel.GradeId = pId;
                    objGradeInfoModel = objLookUpDAL.GetGradeTypeById(objGradeInfoModel);

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

                DataTable dt = objLookUpDAL.GetGradeRecord(objGradeInfoModel);
                var GradeTypeList = GradeTypeListData(dt);
                ViewBag.GradeTypeList = GradeTypeList.ToPagedList(page, pageSize);

                return View(objGradeInfoModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveGradeType(GradeInfoModel objGradeInfoModel)
        {
            LoadSession();
            objGradeInfoModel.UpdateBy = strEmployeeId;
            objGradeInfoModel.HeadOfficeId = strHeadOfficeId;
            objGradeInfoModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveGradeType(objGradeInfoModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetGradeRecord");
        }

        public ActionResult ClearGradeType()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";
            return RedirectToAction("GetGradeRecord");
        }


        #endregion

        #region SHIFT TYPE INFO

        public List<ShiftTypeModel> ShiftTypeListData(DataTable dt)
        {
            List<ShiftTypeModel> ShiftTypeDataBundle = new List<ShiftTypeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ShiftTypeModel objShiftTypeModel = new ShiftTypeModel();
                objShiftTypeModel.ShiftId = dt.Rows[i]["SHIFT_ID"].ToString();
                objShiftTypeModel.ShiftName = dt.Rows[i]["SHIFT_NAME"].ToString();
                objShiftTypeModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                ShiftTypeDataBundle.Add(objShiftTypeModel);
            }

            return ShiftTypeDataBundle;
        }

        [HttpGet]
        public ActionResult GetShiftTypeRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            ShiftTypeModel objShiftTypeModel = new ShiftTypeModel();
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objShiftTypeModel.UpdateBy = strEmployeeId;
                objShiftTypeModel.HeadOfficeId = strHeadOfficeId;
                objShiftTypeModel.BranchOfficeId = strBranchOfficeId;
                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objShiftTypeModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objShiftTypeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objShiftTypeModel.ShiftId = pId;
                    objShiftTypeModel = objLookUpDAL.GetShiftTypeById(objShiftTypeModel);

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

                DataTable dt = objLookUpDAL.GetShiftTypeRecord(objShiftTypeModel);
                var ShiftTypeList = ShiftTypeListData(dt);
                ViewBag.ShiftTypeList = ShiftTypeList.ToPagedList(page, pageSize);
                return View(objShiftTypeModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveShiftType(ShiftTypeModel objShiftTypeModel)
        {
            LoadSession();
            objShiftTypeModel.UpdateBy = strEmployeeId;
            objShiftTypeModel.HeadOfficeId = strHeadOfficeId;
            objShiftTypeModel.BranchOfficeId = strBranchOfficeId;
            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveShiftType(objShiftTypeModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetShiftTypeRecord");
        }

        public ActionResult ClearShiftType()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";
            return RedirectToAction("GetShiftTypeRecord");
        }

        #endregion

        #region JOB LOCATION INFO

        public List<JobLocationInfo> JobLocationListData(DataTable dt)
        {
            List<JobLocationInfo> JobLocationDataBundle = new List<JobLocationInfo>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                JobLocationInfo objJobLocationInfo = new JobLocationInfo();
                objJobLocationInfo.JobLocationId = dt.Rows[i]["JOB_LOCATION_ID"].ToString();
                objJobLocationInfo.JobLocationName = dt.Rows[i]["JOB_LOCATION"].ToString();
                objJobLocationInfo.SerialNumber = dt.Rows[i]["sl"].ToString();

                JobLocationDataBundle.Add(objJobLocationInfo);
            }

            return JobLocationDataBundle;
        }

        [HttpGet]
        public ActionResult GetJobLocationRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            JobLocationInfo objJobLocationInfo = new JobLocationInfo();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objJobLocationInfo.UpdateBy = strEmployeeId;
                objJobLocationInfo.HeadOfficeId = strHeadOfficeId;
                objJobLocationInfo.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objJobLocationInfo.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objJobLocationInfo.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objJobLocationInfo.JobLocationId = pId;
                    objJobLocationInfo = objLookUpDAL.GetJobLocationById(objJobLocationInfo);

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


                DataTable dt = objLookUpDAL.GetJobLocationRecord(objJobLocationInfo);

                var JobLocationList = JobLocationListData(dt);
                ViewBag.JobLocationList = JobLocationList.ToPagedList(page, pageSize);

                return View(objJobLocationInfo);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveJobLocation(JobLocationInfo objJobLocationInfo)
        {
            LoadSession();
            objJobLocationInfo.UpdateBy = strEmployeeId;
            objJobLocationInfo.HeadOfficeId = strHeadOfficeId;
            objJobLocationInfo.BranchOfficeId = strBranchOfficeId;
            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveJobLocation(objJobLocationInfo);
                TempData["OperationMessage"] = strDBMsg;
            }
            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetJobLocationRecord");
        }



        public ActionResult ClearJobLocation()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";
            return RedirectToAction("GetJobLocationRecord");
        }

        #endregion




        #region Probation Period Information

        public List<ProbationPeriodModel> GetProbationPeriodListByDataTable(DataTable dt)
        {
            List<ProbationPeriodModel> probationPeriodList = new List<ProbationPeriodModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ProbationPeriodModel objProbationPeriod = new ProbationPeriodModel();
                objProbationPeriod.ProbationPeriodId = dt.Rows[i]["PROBATION_PERIOD_ID"].ToString();
                objProbationPeriod.ProbationPeriodName = dt.Rows[i]["PROBATION_PERIOD"].ToString();
                objProbationPeriod.SerialNumber = dt.Rows[i]["sl"].ToString();

                probationPeriodList.Add(objProbationPeriod);
            }

            return probationPeriodList;
        }

        [HttpGet]
        public ActionResult GetProbationPeriodEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            ProbationPeriodModel objProbationPeriodModel = new ProbationPeriodModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objProbationPeriodModel.UpdateBy = strEmployeeId;
                objProbationPeriodModel.HeadOfficeId = strHeadOfficeId;
                objProbationPeriodModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objProbationPeriodModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objProbationPeriodModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objProbationPeriodModel.ProbationPeriodId = pId;
                    objProbationPeriodModel = objLookUpDAL.GetProbationPeriodById(objProbationPeriodModel);

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

                DataTable dt = objLookUpDAL.GetProbationPeriodRecord(objProbationPeriodModel);
                List<ProbationPeriodModel> probationPeriodList = GetProbationPeriodListByDataTable(dt);
                ViewBag.ProbationPeriodList = probationPeriodList.ToPagedList(page, pageSize);

                return View(objProbationPeriodModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveProbationPeriod(ProbationPeriodModel objProbationPeriodModel)
        {
            LoadSession();

            objProbationPeriodModel.UpdateBy = strEmployeeId;
            objProbationPeriodModel.HeadOfficeId = strHeadOfficeId;
            objProbationPeriodModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string vDbMessage = objLookUpDAL.SaveProbationPeriod(objProbationPeriodModel);
                TempData["OperationMessage"] = vDbMessage;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetProbationPeriodEntryRecord");
        }

        public ActionResult ClearProbationPeriod()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetProbationPeriodEntryRecord");
        }

        #endregion

        #region Major Subject

        public List<MajorSubjectModel> GetMajorSubjectListByDataTable(DataTable dt)
        {
            List<MajorSubjectModel> majorSubjectList = new List<MajorSubjectModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                MajorSubjectModel objMajorSubject = new MajorSubjectModel();
                objMajorSubject.MajorSubjectId = dt.Rows[i]["MAJOR_SUBJECT_ID"].ToString();
                objMajorSubject.MajorSubjectName = dt.Rows[i]["MAJOR_SUBJECT_NAME"].ToString();
                objMajorSubject.SerialNumber = dt.Rows[i]["sl"].ToString();

                majorSubjectList.Add(objMajorSubject);
            }

            return majorSubjectList;
        }

        [HttpGet]
        public ActionResult GetMajorSubjectEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            MajorSubjectModel objMajorSubject = new MajorSubjectModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objMajorSubject.UpdateBy = strEmployeeId;
                objMajorSubject.HeadOfficeId = strHeadOfficeId;
                objMajorSubject.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objMajorSubject.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objMajorSubject.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objMajorSubject.MajorSubjectId = pId;
                    objMajorSubject = objLookUpDAL.GetMajorSubjectById(objMajorSubject);

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

                DataTable dt = objLookUpDAL.GetMajorSubjectRecord(objMajorSubject);
                List<MajorSubjectModel> majorSubjectList = GetMajorSubjectListByDataTable(dt);
                ViewBag.MajorSubjectList = majorSubjectList.ToPagedList(page, pageSize);

                return View(objMajorSubject);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveMajorSubject(MajorSubjectModel objMajorSubjectModel)
        {
            LoadSession();

            objMajorSubjectModel.UpdateBy = strEmployeeId;
            objMajorSubjectModel.HeadOfficeId = strHeadOfficeId;
            objMajorSubjectModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string vDbMessage = objLookUpDAL.SaveMajorSubjectInfo(objMajorSubjectModel);
                TempData["OperationMessage"] = vDbMessage;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetMajorSubjectEntryRecord");
        }

        public ActionResult ClearMajorSubject()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetMajorSubjectEntryRecord");
        }

        #endregion

        #region Degree Entry

        public List<DegreeModel> GetDegreeListByDataTable(DataTable dt)
        {
            List<DegreeModel> degreeList = new List<DegreeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DegreeModel objDegreeModel = new DegreeModel();
                objDegreeModel.DegreeId = dt.Rows[i]["DEGREE_ID"].ToString();
                objDegreeModel.DegreeName = dt.Rows[i]["DEGREE_NAME"].ToString();
                objDegreeModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                degreeList.Add(objDegreeModel);
            }

            return degreeList;
        }

        [HttpGet]
        public ActionResult GetDegreeEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            DegreeModel objDegreeModel = new DegreeModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objDegreeModel.UpdateBy = strEmployeeId;
                objDegreeModel.HeadOfficeId = strHeadOfficeId;
                objDegreeModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objDegreeModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objDegreeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objDegreeModel.DegreeId = pId;
                    objDegreeModel = objLookUpDAL.GetDegreeById(objDegreeModel);

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

                DataTable dt = objLookUpDAL.GetDegreeRecord(objDegreeModel);
                List<DegreeModel> degreeList = GetDegreeListByDataTable(dt);
                ViewBag.DegreeList = degreeList.ToPagedList(page, pageSize);

                return View(objDegreeModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveDegree(DegreeModel objDegreeModel)
        {
            LoadSession();

            objDegreeModel.UpdateBy = strEmployeeId;
            objDegreeModel.HeadOfficeId = strHeadOfficeId;
            objDegreeModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveDegreeInfo(objDegreeModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            if (Convert.ToInt32(TempData["SearchActionFlag"]) == 1 && Convert.ToInt32(TempData["EditActionFlag"]) == 1)
            {
                int editPageNumber = Convert.ToInt32(TempData["PageNumberFromSearch"]);
                TempData["UpdatePageNumber"] = editPageNumber;
                TempData["UpdateFlag"] = 1;
                //-----------------------
                ////only for setUp Forms
                string a = objDegreeModel.DegreeName;
                string b = TempData["SearchValueFromSearchAction"].ToString();

                if (a.Contains(b))
                {
                    TempData["SearchValueFromSearchAction"] = b;
                    TempData.Keep("SearchValueFromSearchAction");
                }
                else
                {
                    TempData["SearchValueFromSearchAction"] = a;
                    TempData.Keep("SearchValueFromSearchAction");
                }
                //------------------------
            }
            else
            {
                int editPageNumber = Convert.ToInt32(TempData["PageNumberFromSearch"]);
                TempData["UpdatePageNumber"] = editPageNumber;
                TempData["UpdateFlag"] = 1;

                //-----------------------
                ////only for setUp Forms
                string a = objDegreeModel.DegreeName;
                TempData["SearchValueFromSearchAction"] = a;
                TempData.Keep("SearchValueFromSearchAction");
                //------------------------ 
            }

            return RedirectToAction("GetDegreeEntryRecord");
        }

        public ActionResult ClearDegree()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetDegreeEntryRecord");
        }

        #endregion

        #region Sub Section Entry

        public List<SubSectionModel> GetSubSectionListByDataTable(DataTable dt)
        {
            List<SubSectionModel> subSectionList = new List<SubSectionModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SubSectionModel objSubSectionModel = new SubSectionModel();
                objSubSectionModel.SubSectionId = dt.Rows[i]["SUB_SECTION_ID"].ToString();
                objSubSectionModel.SubSectionCode = dt.Rows[i]["SUB_SECTION_CODE"].ToString();
                objSubSectionModel.SubSectionName = dt.Rows[i]["SUB_SECTION_NAME"].ToString();
                objSubSectionModel.SubSectionNameBangla = dt.Rows[i]["SUB_SECTION_NAME_BANGLA"].ToString();
                objSubSectionModel.SectionId = dt.Rows[i]["SECTION_ID"].ToString();
                objSubSectionModel.SectionName = dt.Rows[i]["SECTION_NAME"].ToString();
                objSubSectionModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                subSectionList.Add(objSubSectionModel);
            }

            return subSectionList;
        }

        [HttpGet]
        public ActionResult GetSubSectionEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            SubSectionModel objSubSectionModel = new SubSectionModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objSubSectionModel.UpdateBy = strEmployeeId;
                objSubSectionModel.HeadOfficeId = strHeadOfficeId;
                objSubSectionModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objSubSectionModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objSubSectionModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objSubSectionModel.SubSectionId = pId;
                    objSubSectionModel = objLookUpDAL.GetSubSectionById(objSubSectionModel);

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

                DataTable dt = objLookUpDAL.GetSubSectionRecord(objSubSectionModel);
                List<SubSectionModel> subSectionList = GetSubSectionListByDataTable(dt);
                ViewBag.SubSectionList = subSectionList.ToPagedList(page, pageSize);
                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDList(strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME");

                return View(objSubSectionModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSubSection(SubSectionModel objSubSectionModel)
        {
            LoadSession();

            objSubSectionModel.UpdateBy = strEmployeeId;
            objSubSectionModel.HeadOfficeId = strHeadOfficeId;
            objSubSectionModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string vDbMessage = objLookUpDAL.SaveSubSectionInfo(objSubSectionModel);
                TempData["OperationMessage"] = vDbMessage;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetSubSectionEntryRecord");
        }

        public ActionResult ClearSubSection()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetSubSectionEntryRecord");
        }

        #endregion

        #region Leave Type

        public List<LeaveTypeModel> GetLeaveTypeListByDataTable(DataTable dt)
        {
            List<LeaveTypeModel> leaveTypeList = new List<LeaveTypeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                LeaveTypeModel objLeaveTypeModel = new LeaveTypeModel();
                objLeaveTypeModel.LeaveTypeId = dt.Rows[i]["LEAVE_TYPE_ID"].ToString();
                objLeaveTypeModel.LeaveTypeName = dt.Rows[i]["LEAVE_TYPE_NAME"].ToString();
                objLeaveTypeModel.MaxLeave = dt.Rows[i]["MAX_LEAVE"].ToString();
                objLeaveTypeModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                leaveTypeList.Add(objLeaveTypeModel);
            }

            return leaveTypeList;
        }

        [HttpGet]
        public ActionResult GetLeaveTypeEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            LeaveTypeModel objLeaveTypeModel = new LeaveTypeModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objLeaveTypeModel.UpdateBy = strEmployeeId;
                objLeaveTypeModel.HeadOfficeId = strHeadOfficeId;
                objLeaveTypeModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objLeaveTypeModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objLeaveTypeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objLeaveTypeModel.LeaveTypeId = pId;
                    objLeaveTypeModel = objLookUpDAL.GetLeaveTypeById(objLeaveTypeModel);

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

                DataTable dt = objLookUpDAL.GetLeaveTypeRecord(objLeaveTypeModel);
                List<LeaveTypeModel> leaveTypeList = GetLeaveTypeListByDataTable(dt);
                ViewBag.LeaveTypeList = leaveTypeList.ToPagedList(page, pageSize);

                return View(objLeaveTypeModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveLeaveType(LeaveTypeModel objLeaveTypeModel)
        {
            LoadSession();

            objLeaveTypeModel.UpdateBy = strEmployeeId;
            objLeaveTypeModel.HeadOfficeId = strHeadOfficeId;
            objLeaveTypeModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string vDbMessage = objLookUpDAL.SaveLeaveTypeInfo(objLeaveTypeModel);
                TempData["OperationMessage"] = vDbMessage;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetLeaveTypeEntryRecord");
        }

        public ActionResult ClearLeaveType()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetLeaveTypeEntryRecord");
        }

        #endregion

        #region Office Time

        private List<OfficeTimeModel> GetOfficeTimeListByDataTable(DataTable dt)
        {
            List<OfficeTimeModel> officeTimeList = new List<OfficeTimeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OfficeTimeModel objOfficeTimeModel = new OfficeTimeModel();
                objOfficeTimeModel.OfficeTimeId = dt.Rows[i]["office_time_id"].ToString();
                objOfficeTimeModel.UnitName = dt.Rows[i]["UNIT_NAME"].ToString();
                objOfficeTimeModel.DepartmentName = dt.Rows[i]["department_name"].ToString();
                objOfficeTimeModel.SectionName = dt.Rows[i]["section_name"].ToString();
                objOfficeTimeModel.SubSectionName = dt.Rows[i]["SUB_SECTION_NAME"].ToString();
                objOfficeTimeModel.FirstInTime = dt.Rows[i]["FIRST_IN_TIME"].ToString();
                objOfficeTimeModel.LastOutTime = dt.Rows[i]["LAST_OUT_TIME"].ToString();
                objOfficeTimeModel.LunchOutTime = dt.Rows[i]["LUNCH_OUT_TIME"].ToString();
                objOfficeTimeModel.LunchInTime = dt.Rows[i]["LUNCH_IN_TIME"].ToString();
                objOfficeTimeModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                officeTimeList.Add(objOfficeTimeModel);
            }

            return officeTimeList;
        }

        [HttpGet]
        public ActionResult GetOfficeTimeEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            OfficeTimeModel objOfficeTimeModel = new OfficeTimeModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objOfficeTimeModel.UpdateBy = strEmployeeId;
                objOfficeTimeModel.HeadOfficeId = strHeadOfficeId;
                objOfficeTimeModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objOfficeTimeModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objOfficeTimeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objOfficeTimeModel.OfficeTimeId = pId;
                    objOfficeTimeModel = objLookUpDAL.GetOfficeTimeById(objOfficeTimeModel);

                    ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objOfficeTimeModel.UnitId,
                        strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objOfficeTimeModel.DepartmentId);
                    ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objOfficeTimeModel.DepartmentId,
                        strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objOfficeTimeModel.SectionId);
                    ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objOfficeTimeModel.SectionId,
                        strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objOfficeTimeModel.SubSectionId);

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

                DataTable dt = objLookUpDAL.GetOfficeTimeRecord(objOfficeTimeModel);
                List<OfficeTimeModel> officeTimeList = GetOfficeTimeListByDataTable(dt);
                ViewBag.OfficeTimeList = officeTimeList.ToPagedList(page, pageSize);
                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                return View(objOfficeTimeModel);
            }
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetDepartmentDdlByUnitId(string pUnitId)
        {
            LoadSession();

            DataTable departmentDataTable = objLookUpDAL.GetDepartmentDDListByUnitId(pUnitId, strHeadOfficeId, strBranchOfficeId);

            List<object> objectList = new List<object>();

            if (departmentDataTable.Rows.Count > 0)
            {
                objectList.Add(new { DepartmentId = "", DepartmentName = "Please select one" });

                foreach (DataRow dataRow in departmentDataTable.Rows)
                {
                    objectList.Add(new { DepartmentId = dataRow["DEPARTMENT_ID"], DepartmentName = dataRow["DEPARTMENT_NAME"] });
                }
            }

            return Json(objectList, JsonRequestBehavior.AllowGet);
        }


        [ValidateAntiForgeryToken]
        public JsonResult GetSectionDdlByDepartmentId(string pDepartmentId)
        {
            LoadSession();

            DataTable sectionDataTable = objLookUpDAL.GetSectionDDListByDepartmentId(pDepartmentId, strHeadOfficeId, strBranchOfficeId);

            List<object> objectList = new List<object>();

            if (sectionDataTable.Rows.Count > 0)
            {
                objectList.Add(new { SectionId = "", SectionName = "Please select one" });

                foreach (DataRow dataRow in sectionDataTable.Rows)
                {
                    objectList.Add(new { SectionId = dataRow["SECTION_ID"], SectionName = dataRow["SECTION_NAME"] });
                }
            }

            return Json(objectList, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        public JsonResult GetSubSectionDdlBySectionId(string pSectionId)
        {
            LoadSession();

            DataTable subSectionDataTable = objLookUpDAL.GetSubSectionDDListBySectionId(pSectionId, strHeadOfficeId, strBranchOfficeId);

            List<object> objectList = new List<object>();

            if (subSectionDataTable.Rows.Count > 0)
            {
                objectList.Add(new { SubSectionId = "", SubSectionName = "Please select one" });

                foreach (DataRow dataRow in subSectionDataTable.Rows)
                {
                    objectList.Add(new { SubSectionId = dataRow["SUB_SECTION_ID"], SubSectionName = dataRow["SUB_SECTION_NAME"] });
                }
            }

            return Json(objectList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveOfficeTime(OfficeTimeModel objOfficeTimeModel)
        {
            LoadSession();

            objOfficeTimeModel.UpdateBy = strEmployeeId;
            objOfficeTimeModel.HeadOfficeId = strHeadOfficeId;
            objOfficeTimeModel.BranchOfficeId = strBranchOfficeId;

            objOfficeTimeModel.CheckAll = objOfficeTimeModel.CheckAll.Contains("true") ? "Y" : "N";

            if (ModelState.IsValid)
            {
                string vDbMessage = objLookUpDAL.SaveOfficeTime(objOfficeTimeModel);
                TempData["OperationMessage"] = vDbMessage;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetOfficeTimeEntryRecord");
        }

        public ActionResult ClearOfficeTime()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetOfficeTimeEntryRecord");
        }

        #endregion

        #region Supervisor Entry

        //private List<SupervisorModel> GetSupervisorListByDataTable(DataTable dt)
        //{
        //    List<SupervisorModel> officeTimeList = new List<SupervisorModel>();

        //    for (int i = 0; i < dt.Rows.Count; i++)
        //    {
        //        SupervisorModel objOfficeTimeModel = new SupervisorModel();
        //        objOfficeTimeModel.EmployeeId = dt.Rows[i]["EMPLOYEE_ID"].ToString();
        //        objOfficeTimeModel.EmployeeName = dt.Rows[i]["EMPLOYEE_NAME"].ToString();
        //        objOfficeTimeModel.JoiningDate = dt.Rows[i]["JOINING_DATE"].ToString();
        //        objOfficeTimeModel.DesignationName = dt.Rows[i]["DESIGNATION_NAME"].ToString();
        //        objOfficeTimeModel.UnitName = dt.Rows[i]["UNIT_NAME"].ToString();
        //        objOfficeTimeModel.DepartmentName = dt.Rows[i]["DEPARTMENT_NAME"].ToString();
        //        objOfficeTimeModel.SectionName = dt.Rows[i]["SECTION_NAME"].ToString();
        //        objOfficeTimeModel.SubSectionName = dt.Rows[i]["SUB_SECTION_NAME"].ToString();
        //        objOfficeTimeModel.Status = dt.Rows[i]["active_status"].ToString();

        //        objOfficeTimeModel.EmployeeImage = !string.IsNullOrWhiteSpace(dt.Rows[i]["EMPLOYEE_IMAGE"].ToString()) ? (byte[])dt.Rows[i]["EMPLOYEE_IMAGE"] : new byte[0];

        //        objOfficeTimeModel.SerialNumber = dt.Rows[i]["sl"].ToString();

        //        officeTimeList.Add(objOfficeTimeModel);
        //    }

        //    return officeTimeList;
        //}

        [HttpGet]
        public ActionResult GetSupervisorEntryRecord()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                ViewBag.EmployeeList = new List<SupervisorModel>();
                ViewBag.SupervisorList = objLookUpDAL.GetAllSupervisorList(strHeadOfficeId, strBranchOfficeId);

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetSupervisorEntryRecord(SupervisorModel objSupervisorModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objSupervisorModel.UpdateBy = strEmployeeId;
                objSupervisorModel.HeadOfficeId = strHeadOfficeId;
                objSupervisorModel.BranchOfficeId = strBranchOfficeId;

                objSupervisorModel.Status = objSupervisorModel.Status.Contains("false") ? "Y" : "N";

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objSupervisorModel.UnitId,
                    strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objSupervisorModel.DepartmentId);
                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objSupervisorModel.DepartmentId,
                    strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objSupervisorModel.SectionId);
                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objSupervisorModel.SectionId,
                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objSupervisorModel.SubSectionId);

                ViewBag.EmployeeList = objEmployeeDAL.GetAllEmployeesForSupervisor(objSupervisorModel);
                ViewBag.SupervisorList = objLookUpDAL.GetAllSupervisorList(strHeadOfficeId, strBranchOfficeId);

                return View(objSupervisorModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSupervisor(SupervisorModel objSupervisorModel)
        {

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objSupervisorModel.UpdateBy = strEmployeeId;
                objSupervisorModel.HeadOfficeId = strHeadOfficeId;
                objSupervisorModel.BranchOfficeId = strBranchOfficeId;

                if (ModelState.IsValid)
                {
                    string vDbMessage = objLookUpDAL.SaveSupervisorInfo(objSupervisorModel);
                    TempData["OperationMessage"] = vDbMessage;
                }

                return RedirectToAction("GetSupervisorEntryRecord");
            }
        }

        public ActionResult ClearSupervisor()
        {
            ModelState.Clear();

            return RedirectToAction("GetSupervisorEntryRecord");
        }

        #endregion

        #region Approver Entry

        [HttpGet]
        public ActionResult GetApproverEntryRecord()
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                ViewBag.EmployeeList = new List<ApproverModel>();
                ViewBag.ApproverList = objLookUpDAL.GetAllApproverList(strHeadOfficeId, strBranchOfficeId);

                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetApproverEntryRecord(ApproverModel objApproverModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objApproverModel.UpdateBy = strEmployeeId;
                objApproverModel.HeadOfficeId = strHeadOfficeId;
                objApproverModel.BranchOfficeId = strBranchOfficeId;

                objApproverModel.Status = objApproverModel.Status.Contains("false") ? "Y" : "N";

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                ViewBag.DepartmentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetDepartmentDDListByUnitId(objApproverModel.UnitId,
                    strHeadOfficeId, strBranchOfficeId), "DEPARTMENT_ID", "DEPARTMENT_NAME", objApproverModel.DepartmentId);
                ViewBag.SectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSectionDDListByDepartmentId(objApproverModel.DepartmentId,
                    strHeadOfficeId, strBranchOfficeId), "SECTION_ID", "SECTION_NAME", objApproverModel.SectionId);
                ViewBag.SubSectionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubSectionDDListBySectionId(objApproverModel.SectionId,
                    strHeadOfficeId, strBranchOfficeId), "SUB_SECTION_ID", "SUB_SECTION_NAME", objApproverModel.SubSectionId);

                ViewBag.EmployeeList = objEmployeeDAL.GetAllEmployeesForApprover(objApproverModel);
                ViewBag.ApproverList = objLookUpDAL.GetAllApproverList(strHeadOfficeId, strBranchOfficeId);

                return View(objApproverModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveApprover(ApproverModel objApproverModel)
        {

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objApproverModel.UpdateBy = strEmployeeId;
                objApproverModel.HeadOfficeId = strHeadOfficeId;
                objApproverModel.BranchOfficeId = strBranchOfficeId;

                if (ModelState.IsValid)
                {
                    string vDbMessage = objLookUpDAL.SaveApproverInfo(objApproverModel);
                    TempData["OperationMessage"] = vDbMessage;
                }

                return RedirectToAction("GetApproverEntryRecord");
            }
        }

        public ActionResult ClearApprover()
        {
            ModelState.Clear();

            return RedirectToAction("GetApproverEntryRecord");
        }

        #endregion

        #region Holiday Type

        private List<HolidayTypeModel> GetHolidayTypeListByDataTable(DataTable dt)
        {
            List<HolidayTypeModel> holidayTypeList = new List<HolidayTypeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HolidayTypeModel holidayTypeModel = new HolidayTypeModel();
                holidayTypeModel.HolidayTypeId = dt.Rows[i]["holiday_type_id"].ToString();
                holidayTypeModel.HolidayTypeName = dt.Rows[i]["holiday_type_NAME"].ToString();
                holidayTypeModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                holidayTypeList.Add(holidayTypeModel);
            }

            return holidayTypeList;
        }

        [HttpGet]
        public ActionResult GetHolidayTypeEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            HolidayTypeModel objHolidayTypeModel = new HolidayTypeModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objHolidayTypeModel.UpdateBy = strEmployeeId;
                objHolidayTypeModel.HeadOfficeId = strHeadOfficeId;
                objHolidayTypeModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objHolidayTypeModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objHolidayTypeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objHolidayTypeModel.HolidayTypeId = pId;
                    objHolidayTypeModel = objLookUpDAL.GetHolidayTypeById(objHolidayTypeModel);

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

                DataTable dt = objLookUpDAL.GetHolidayTypeRecord(objHolidayTypeModel);
                List<HolidayTypeModel> probationPeriodList = GetHolidayTypeListByDataTable(dt);
                ViewBag.HolidayTypeList = probationPeriodList.ToPagedList(page, pageSize);

                return View(objHolidayTypeModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveHolidayType(HolidayTypeModel objHolidayTypeModel)
        {
            LoadSession();

            objHolidayTypeModel.UpdateBy = strEmployeeId;
            objHolidayTypeModel.HeadOfficeId = strHeadOfficeId;
            objHolidayTypeModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string vDbMessage = objLookUpDAL.SaveHolidayType(objHolidayTypeModel);
                TempData["OperationMessage"] = vDbMessage;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetHolidayTypeEntryRecord");
        }

        public ActionResult ClearHolidayType()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetHolidayTypeEntryRecord");
        }

        #endregion

        #region Holiday

        private List<HolidayModel> GetHolidayListByDataTable(DataTable dt)
        {
            List<HolidayModel> holidayList = new List<HolidayModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                HolidayModel objHolidayModel = new HolidayModel();
                objHolidayModel.HolidayId = dt.Rows[i]["HOLIDAY_ID"].ToString();
                objHolidayModel.HolidayTypeName = dt.Rows[i]["holiday_type_name"].ToString();
                objHolidayModel.FromDate = dt.Rows[i]["holiday_START_DATE"].ToString();
                objHolidayModel.ToDate = dt.Rows[i]["holiday_END_DATE"].ToString();
                objHolidayModel.Remarks = dt.Rows[i]["remarks"].ToString();
                objHolidayModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                holidayList.Add(objHolidayModel);
            }

            return holidayList;
        }

        [HttpGet]
        public ActionResult GetHolidayEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            HolidayModel objHolidayModel = new HolidayModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objHolidayModel.UpdateBy = strEmployeeId;
                objHolidayModel.HeadOfficeId = strHeadOfficeId;
                objHolidayModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objHolidayModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objHolidayModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objHolidayModel.HolidayId = pId;
                    objHolidayModel = objLookUpDAL.GetHolidayById(objHolidayModel);

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

                DataTable dt = objLookUpDAL.GetHolidayRecord(objHolidayModel);
                List<HolidayModel> holidayList = GetHolidayListByDataTable(dt);
                ViewBag.HolidayList = holidayList.ToPagedList(page, pageSize);
                ViewBag.HolidayTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetHolidayTypeDDList(), "HOLIDAY_TYPE_ID", "HOLIDAY_TYPE_NAME");

                return View(objHolidayModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveHoliday(HolidayModel objHolidayModel)
        {
            LoadSession();

            objHolidayModel.UpdateBy = strEmployeeId;
            objHolidayModel.HeadOfficeId = strHeadOfficeId;
            objHolidayModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string vDbMessage = objLookUpDAL.SaveHoliday(objHolidayModel);
                TempData["OperationMessage"] = vDbMessage;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetHolidayEntryRecord");
        }

        public ActionResult ClearHoliday()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetHolidayEntryRecord");
        }

        #endregion


        //Merchandising

        #region "Buyer Payment Type Entry"
        BuyerPaymentTypeEntryModel objBuyerPaymentTypeEntryModel = new BuyerPaymentTypeEntryModel();
        [HttpGet]
        public ActionResult GetBuyerPaymentTypeEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objBuyerPaymentTypeEntryModel.UpdateBy = strEmployeeId;
                objBuyerPaymentTypeEntryModel.HeadOfficeId = strHeadOfficeId;
                objBuyerPaymentTypeEntryModel.BranchOfficeId = strBranchOfficeId;
                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objBuyerPaymentTypeEntryModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objBuyerPaymentTypeEntryModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objBuyerPaymentTypeEntryModel.PaymentTypeId = pId;
                    objBuyerPaymentTypeEntryModel = objLookUpDAL.GetBuyerPaymentTypeEntryById(objBuyerPaymentTypeEntryModel);

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
                DataTable dt = objLookUpDAL.GetBuyerPaymentTypeEntryRecord(objBuyerPaymentTypeEntryModel);
                List<BuyerPaymentTypeEntryModel> BuyerPaymentTypeEntryList = BuyerPaymentTypeEntryListData(dt);
                // var BuyerPaymentTypeEntryList = BuyerPaymentTypeEntryListData(dt);
                ViewBag.BuyerPaymentTypeEntryList = BuyerPaymentTypeEntryList.ToPagedList(page, pageSize);
                return View(objBuyerPaymentTypeEntryModel);
            }
        }
        public List<BuyerPaymentTypeEntryModel> BuyerPaymentTypeEntryListData(DataTable dt)
        {
            List<BuyerPaymentTypeEntryModel> BuyerPaymentTypeEntryDataBundle = new List<BuyerPaymentTypeEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BuyerPaymentTypeEntryModel objBuyerPaymentTypeEntryModel = new BuyerPaymentTypeEntryModel();
                objBuyerPaymentTypeEntryModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objBuyerPaymentTypeEntryModel.PaymentTypeId = dt.Rows[i]["PAYMENT_TYPE_ID"].ToString();
                objBuyerPaymentTypeEntryModel.PaymentTypeName = dt.Rows[i]["PAYMENT_TYPE_NAME"].ToString();


                BuyerPaymentTypeEntryDataBundle.Add(objBuyerPaymentTypeEntryModel);
            }

            return BuyerPaymentTypeEntryDataBundle;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBuyerPaymentTypeEntry(BuyerPaymentTypeEntryModel objBuyerPaymentTypeEntryModel)
        {

            LoadSession();
            objBuyerPaymentTypeEntryModel.UpdateBy = strEmployeeId;
            objBuyerPaymentTypeEntryModel.HeadOfficeId = strHeadOfficeId;
            objBuyerPaymentTypeEntryModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveBuyerPaymentTypeEntry(objBuyerPaymentTypeEntryModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion


            return RedirectToAction("GetBuyerPaymentTypeEntryRecord");
        }
        public ActionResult ClearBuyerPaymentTypeEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("GetBuyerPaymentTypeEntryRecord");
        }
        #endregion

        #region "Season Entry"
        SeasonEntryModel objSeasonEntryModel = new SeasonEntryModel();
        [HttpGet]
        public ActionResult GetSeasonEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objSeasonEntryModel.UpdateBy = strEmployeeId;
                objSeasonEntryModel.HeadOfficeId = strHeadOfficeId;
                objSeasonEntryModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objSeasonEntryModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objSeasonEntryModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objSeasonEntryModel.SeasonId = pId;
                    objSeasonEntryModel = objLookUpDAL.GetSeasonEntryById(objSeasonEntryModel);

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

                DataTable dt = objLookUpDAL.GetSeasonEntryRecord(objSeasonEntryModel);
                List<SeasonEntryModel> SeasonEntryList = SeasonEntryListData(dt);
                ViewBag.SeasonEntryList = SeasonEntryList.ToPagedList(page, pageSize);
                return View(objSeasonEntryModel);
            }
        }
        public List<SeasonEntryModel> SeasonEntryListData(DataTable dt)
        {
            List<SeasonEntryModel> SeasonEntryDataBundle = new List<SeasonEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SeasonEntryModel objSeasonEntryModel = new SeasonEntryModel();
                objSeasonEntryModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objSeasonEntryModel.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                objSeasonEntryModel.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();


                SeasonEntryDataBundle.Add(objSeasonEntryModel);
            }

            return SeasonEntryDataBundle;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSeasonEntry(SeasonEntryModel objSeasonEntryModel)
        {

            LoadSession();
            objSeasonEntryModel.UpdateBy = strEmployeeId;
            objSeasonEntryModel.HeadOfficeId = strHeadOfficeId;
            objSeasonEntryModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveSeasonEntry(objSeasonEntryModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetSeasonEntryRecord");
        }

        public ActionResult ClearSeasonEntry()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";
            return RedirectToAction("GetSeasonEntryRecord");
        }
        #endregion

        #region "Currency Entry"
        CurrencyEntryModel objCurrencyEntryModel = new CurrencyEntryModel();
        [HttpGet]
        public ActionResult GetCurrencyEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objCurrencyEntryModel.UpdateBy = strEmployeeId;
                objCurrencyEntryModel.HeadOfficeId = strHeadOfficeId;
                objCurrencyEntryModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objCurrencyEntryModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objCurrencyEntryModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objCurrencyEntryModel.CurrencyId = pId;
                    objCurrencyEntryModel = objLookUpDAL.GetCurrencyEntryById(objCurrencyEntryModel);

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

                DataTable dt = objLookUpDAL.GetCurrencyEntryRecord(objCurrencyEntryModel);
                List<CurrencyEntryModel> CurrencyEntryList = CurrencyEntryListData(dt);
                ViewBag.CurrencyEntryList = CurrencyEntryList.ToPagedList(page, pageSize);
                return View(objCurrencyEntryModel);
            }
        }
        public List<CurrencyEntryModel> CurrencyEntryListData(DataTable dt)
        {
            List<CurrencyEntryModel> CurrencyEntryDataBundle = new List<CurrencyEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CurrencyEntryModel objCurrencyEntryModel = new CurrencyEntryModel();
                objCurrencyEntryModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objCurrencyEntryModel.CurrencyId = dt.Rows[i]["CURRENCY_ID"].ToString();
                objCurrencyEntryModel.CurrencyName = dt.Rows[i]["CURRENCY_NAME"].ToString();


                CurrencyEntryDataBundle.Add(objCurrencyEntryModel);
            }

            return CurrencyEntryDataBundle;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCurrencyEntry(CurrencyEntryModel objCurrencyEntryModel)
        {

            LoadSession();
            objCurrencyEntryModel.UpdateBy = strEmployeeId;
            objCurrencyEntryModel.HeadOfficeId = strHeadOfficeId;
            objCurrencyEntryModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveCurrencyEntry(objCurrencyEntryModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetCurrencyEntryRecord");
        }
        public ActionResult ClearCurrencyEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("GetCurrencyEntryRecord");
        }
        #endregion

        #region "Buyer Entry"
        BuyerEntryModel objBuyerEntryModel = new BuyerEntryModel();
        [HttpGet]
        public ActionResult GetBuyerEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objBuyerEntryModel.UpdateBy = strEmployeeId;
                objBuyerEntryModel.HeadOfficeId = strHeadOfficeId;
                objBuyerEntryModel.BranchOfficeId = strBranchOfficeId;
                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objBuyerEntryModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objBuyerEntryModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objBuyerEntryModel.BuyerId = pId;
                    objBuyerEntryModel = objLookUpDAL.GetBuyerEntryById(objBuyerEntryModel);

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

                DataTable dt = objLookUpDAL.GetBuyerEntryRecord(objBuyerEntryModel);
                List<BuyerEntryModel> BuyerEntryList = BuyerEntryListData(dt);
                ViewBag.BuyerEntryList = BuyerEntryList.ToPagedList(page, pageSize);
                ViewBag.CountryDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCountryDDList(objBuyerEntryModel), "COUNTRY_ID", "COUNTRY_NAME");
                ViewBag.PaymentDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetPaymentDDList(objBuyerEntryModel), "PAYMENT_TYPE_ID", "PAYMENT_TYPE_NAME");
                return View(objBuyerEntryModel);
            }
        }
        public List<BuyerEntryModel> BuyerEntryListData(DataTable dt)
        {
            List<BuyerEntryModel> BuyerEntryDataBundle = new List<BuyerEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BuyerEntryModel objBuyerEntryModel = new BuyerEntryModel();
                objBuyerEntryModel.SerialNumber = dt.Rows[i]["SL"].ToString();
                objBuyerEntryModel.BuyerId = dt.Rows[i]["BUYER_ID"].ToString();
                objBuyerEntryModel.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objBuyerEntryModel.CountryId = dt.Rows[i]["COUNTRY_ID"].ToString();
                objBuyerEntryModel.CountryName = dt.Rows[i]["COUNTRY_NAME"].ToString();
                objBuyerEntryModel.ContactNo = dt.Rows[i]["CONTACT_NO"].ToString();
                objBuyerEntryModel.EmailAddress = dt.Rows[i]["EMAIL_ADDRESS"].ToString();
                objBuyerEntryModel.BuyerAddress = dt.Rows[i]["BUYER_ADDRESS"].ToString();
                objBuyerEntryModel.PaymentBy = dt.Rows[i]["PAYMENT_BY"].ToString();
                objBuyerEntryModel.PaymentByName = dt.Rows[i]["PAYMENT_TYPE_NAME"].ToString();
                objBuyerEntryModel.PaymentByName = dt.Rows[i]["PAYMENT_TYPE_NAME"].ToString();

                BuyerEntryDataBundle.Add(objBuyerEntryModel);
            }

            return BuyerEntryDataBundle;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBuyerEntry(BuyerEntryModel objBuyerEntryModel)
        {

            LoadSession();
            objBuyerEntryModel.UpdateBy = strEmployeeId;
            objBuyerEntryModel.HeadOfficeId = strHeadOfficeId;
            objBuyerEntryModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveBuyerEntry(objBuyerEntryModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetBuyerEntryRecord");
        }
        public ActionResult ClearBuyerEntry()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";
            return RedirectToAction("GetBuyerEntryRecord");
        }
        #endregion

        #region Color

        private ColorModel objColorModel = new ColorModel();

        [HttpGet]
        public ActionResult GetColorRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objColorModel.UpdateBy = strEmployeeId;
                objColorModel.HeadOfficeId = strHeadOfficeId;
                objColorModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objColorModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objColorModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objColorModel.ColorId = pId;
                    objColorModel = objLookUpDAL.GetColorById(objColorModel);

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

                DataTable dt = objLookUpDAL.GetColorRecord(objColorModel);
                List<ColorModel> colorGroupList = ColorListData(dt);
                ViewBag.ColorList = colorGroupList.ToPagedList(page, pageSize);
                return View(objColorModel);
            }
        }
        public List<ColorModel> ColorListData(DataTable dt)
        {
            List<ColorModel> ColorDataBundle = new List<ColorModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ColorModel objColorModel = new ColorModel();

                objColorModel.ColorId = dt.Rows[i]["COLOR_ID"].ToString();
                objColorModel.ColorName = dt.Rows[i]["COLOR_NAME"].ToString();
                objColorModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                ColorDataBundle.Add(objColorModel);
            }
            return ColorDataBundle;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveColor(ColorModel objColorLookUpModel)
        {
            LoadSession();
            objColorLookUpModel.UpdateBy = strEmployeeId;
            objColorLookUpModel.HeadOfficeId = strHeadOfficeId;
            objColorLookUpModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveColorInfo(objColorLookUpModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetColorRecord");
        }
        public ActionResult ClearColor()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetColorRecord");
        }

        #endregion

        #region Unit Merchandiser

        UnitMerchandiserModel objUnitMerchandiserModel = new UnitMerchandiserModel();

        [HttpGet]
        public ActionResult GetUnitMerchandiserRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objUnitMerchandiserModel.UpdateBy = strEmployeeId;
                objUnitMerchandiserModel.HeadOfficeId = strHeadOfficeId;
                objUnitMerchandiserModel.BranchOfficeId = strBranchOfficeId;


                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objUnitMerchandiserModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objUnitMerchandiserModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objUnitMerchandiserModel.UnitMerchandiserId = pId;
                    objUnitMerchandiserModel = objLookUpDAL.GetUnitMerchandiserById(objUnitMerchandiserModel);

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

                DataTable dt = objLookUpDAL.GetUnitMerchandiserRecord(objUnitMerchandiserModel);
                List<UnitMerchandiserModel> unitMerchandiserList = UnitMerchandiserListData(dt);
                ViewBag.UnitMerchandiserList = unitMerchandiserList.ToPagedList(page, pageSize);
                return View(objUnitMerchandiserModel);
            }
        }
        public List<UnitMerchandiserModel> UnitMerchandiserListData(DataTable dt)
        {
            List<UnitMerchandiserModel> objUnitMerchandiserLookUpModel = new List<UnitMerchandiserModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UnitMerchandiserModel objUnitMerchantModel = new UnitMerchandiserModel();

                objUnitMerchantModel.UnitMerchandiserId = dt.Rows[i]["UNIT_ID"].ToString();
                objUnitMerchantModel.UnitMerchandiserName = dt.Rows[i]["UNIT_NAME"].ToString();
                objUnitMerchantModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                objUnitMerchandiserLookUpModel.Add(objUnitMerchantModel);
            }
            return objUnitMerchandiserLookUpModel;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveUnitMerchandiser(UnitMerchandiserModel objUnitMerchandiserModel)
        {
            LoadSession();
            objUnitMerchandiserModel.UpdateBy = strEmployeeId;
            objUnitMerchandiserModel.HeadOfficeId = strHeadOfficeId;
            objUnitMerchandiserModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveUnitMerchandiserInfo(objUnitMerchandiserModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetUnitMerchandiserRecord");
        }


        public ActionResult ClearUnitMerchandiser()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetUnitMerchandiserRecord");
        }


        #endregion

        #region "Brand Entry"
        BrandEntryModel objBrandEntryModel = new BrandEntryModel();

        [HttpGet]
        public ActionResult GetBrandEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objBrandEntryModel.UpdateBy = strEmployeeId;
                objBrandEntryModel.HeadOfficeId = strHeadOfficeId;
                objBrandEntryModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objBrandEntryModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objBrandEntryModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objBrandEntryModel.BrandId = pId;
                    objBrandEntryModel = objLookUpDAL.GetBrandEntryRecordById(objBrandEntryModel);

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

                DataTable dt = objLookUpDAL.GetBrandEntryRecord(objBrandEntryModel);
                List<BrandEntryModel> BrandEntryList = BrandEntryListData(dt);
                ViewBag.BrandEntryList = BrandEntryList.ToPagedList(page, pageSize);
                return View(objBrandEntryModel);
            }
        }
        public List<BrandEntryModel> BrandEntryListData(DataTable dt)
        {
            List<BrandEntryModel> BrandEntryDataBundle = new List<BrandEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BrandEntryModel objBrandEntryModel = new BrandEntryModel();
                objBrandEntryModel.SerialNumber = dt.Rows[i]["sl"].ToString();
                objBrandEntryModel.BrandId = dt.Rows[i]["BRAND_ID"].ToString();
                objBrandEntryModel.BrandName = dt.Rows[i]["BRAND_NAME"].ToString();


                BrandEntryDataBundle.Add(objBrandEntryModel);
            }

            return BrandEntryDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBrandEntry(BrandEntryModel objBrandEntryModel)
        {
            LoadSession();
            objBrandEntryModel.UpdateBy = strEmployeeId;
            objBrandEntryModel.HeadOfficeId = strHeadOfficeId;
            objBrandEntryModel.BranchOfficeId = strBranchOfficeId;
            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveBrandEntry(objBrandEntryModel);
                TempData["OperationMessage"] = strDBMsg;
            }


            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion


            return RedirectToAction("GetBrandEntryRecord");
        }
        public ActionResult ClearBrandEntry()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";
            return RedirectToAction("GetBrandEntryRecord");
        }
        #endregion

        #region "Item Entry"
        ItemEntryModel objItemEntryModel = new ItemEntryModel();

        [HttpGet]
        public ActionResult GetItemEntryRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objItemEntryModel.UpdateBy = strEmployeeId;
                objItemEntryModel.HeadOfficeId = strHeadOfficeId;
                objItemEntryModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objItemEntryModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objItemEntryModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objItemEntryModel.ItemId = pId;
                    objItemEntryModel = objLookUpDAL.GetItemEntryRecordById(objItemEntryModel);

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

                DataTable dt = objLookUpDAL.GetItemEntryRecord(objItemEntryModel);
                var ItemEntryList = ItemEntryListData(dt);
                ViewBag.ItemEntryList = ItemEntryList.ToPagedList(page, pageSize);
                return View(objItemEntryModel);
            }
        }

        public List<ItemEntryModel> ItemEntryListData(DataTable dt)
        {
            List<ItemEntryModel> ItemEntryDataBundle = new List<ItemEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ItemEntryModel objItemEntryModel = new ItemEntryModel();
                objItemEntryModel.SerialNumber = dt.Rows[i]["sl"].ToString();
                objItemEntryModel.ItemId = dt.Rows[i]["ITEM_ID"].ToString();
                objItemEntryModel.ItemName = dt.Rows[i]["ITEM_NAME"].ToString();


                ItemEntryDataBundle.Add(objItemEntryModel);
            }

            return ItemEntryDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveItemEntry(ItemEntryModel objItemEntryModel)
        {
            LoadSession();
            objItemEntryModel.UpdateBy = strEmployeeId;
            objItemEntryModel.HeadOfficeId = strHeadOfficeId;
            objItemEntryModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveItemEntry(objItemEntryModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetItemEntryRecord");
        }
        public ActionResult ClearItemEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("GetItemEntryRecord");
        }
        #endregion

        #region "Shipment Info"
        ShipmentInfoMODEL objShipmentInfoMODEL = new ShipmentInfoMODEL();
        [HttpGet]
        public ActionResult GetShipmentInfoRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objShipmentInfoMODEL.UpdateBy = strEmployeeId;
                objShipmentInfoMODEL.HeadOfficeId = strHeadOfficeId;
                objShipmentInfoMODEL.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objShipmentInfoMODEL.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objShipmentInfoMODEL.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {

                    objShipmentInfoMODEL.ShipmentInfoId = pId;
                    objShipmentInfoMODEL = objLookUpDAL.GetShipmentInfoRecordById(objShipmentInfoMODEL);
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

                DataTable dt = objLookUpDAL.GetShipmentInfoRecord(objShipmentInfoMODEL);
                List<ShipmentInfoMODEL> ShipmentList = ShipmentListData(dt);
                ViewBag.ShipmentInfoList = ShipmentList.ToPagedList(page, pageSize);
                return View(objShipmentInfoMODEL);

            }
        }
        public List<ShipmentInfoMODEL> ShipmentListData(DataTable dt)
        {
            List<ShipmentInfoMODEL> ShipmentListDataBundle = new List<ShipmentInfoMODEL>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ShipmentInfoMODEL objShipmentInfoMODEL = new ShipmentInfoMODEL();
                objShipmentInfoMODEL.SerialNumber = dt.Rows[i]["sl"].ToString();
                objShipmentInfoMODEL.ShipmentInfoId = dt.Rows[i]["SHIPMENT_INFO_ID"].ToString();
                objShipmentInfoMODEL.ShipmentInfoName = dt.Rows[i]["SHIPMENT_INFO_NAME"].ToString();
                objShipmentInfoMODEL.ShipmentInfoIdAddress = dt.Rows[i]["SHIPMENT_INFO_ADDRESS"].ToString();
                objShipmentInfoMODEL.EmailAddress = dt.Rows[i]["EMAIL_ADDRESS"].ToString();
                objShipmentInfoMODEL.MobileNo = dt.Rows[i]["MOBILE_NO"].ToString();
                objShipmentInfoMODEL.PhoneNo = dt.Rows[i]["PHONE_NO"].ToString();
                objShipmentInfoMODEL.FaxNo = dt.Rows[i]["FAX_NO"].ToString();
                objShipmentInfoMODEL.ContactPerson = dt.Rows[i]["CONTACT_PERSON"].ToString();


                ShipmentListDataBundle.Add(objShipmentInfoMODEL);
            }

            return ShipmentListDataBundle;
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveShipmentInfoEntry(ShipmentInfoMODEL objShipmentInfoMODEL)
        {
            LoadSession();
            objShipmentInfoMODEL.UpdateBy = strEmployeeId;
            objShipmentInfoMODEL.HeadOfficeId = strHeadOfficeId;
            objShipmentInfoMODEL.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveShipmentinfoEntry(objShipmentInfoMODEL);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetShipmentInfoRecord");
        }
        public ActionResult ClearShipmentInfoEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("GetShipmentInfoRecord");
        }


        #endregion

        #region"Consignee Bank"

        ConsigneeBankInfo objConsigneeBankInfo = new ConsigneeBankInfo();
        [HttpGet]
        public ActionResult GetConsigneeBankInfoRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objConsigneeBankInfo.UpdateBy = strEmployeeId;
                objConsigneeBankInfo.HeadOfficeId = strHeadOfficeId;
                objConsigneeBankInfo.BranchOfficeId = strBranchOfficeId;
                #region Pagination Search
                CheckUrl();
                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objConsigneeBankInfo.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy.Trim();
                }
                if (TempData.ContainsKey("SearchValue"))
                {
                    objConsigneeBankInfo.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }
                if (!string.IsNullOrWhiteSpace(pId))
                {

                    objConsigneeBankInfo.BankId = pId;
                    objConsigneeBankInfo = objLookUpDAL.ConsigneeBankInfoRecordById(objConsigneeBankInfo);
                    if (objConsigneeBankInfo.EmailAddress == "N/A")
                    {
                        objConsigneeBankInfo.EmailAddress = "";
                    }
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
                DataTable dt = objLookUpDAL.GetConsigneeBankInfoRecord(objConsigneeBankInfo);
                List<ConsigneeBankInfo> CosigneeBankList = CosigneeBankListData(dt);
                ViewBag.CosigneeBankInfoList = CosigneeBankList.ToPagedList(page, pageSize);
                return View(objConsigneeBankInfo);
            }
        }
        public List<ConsigneeBankInfo> CosigneeBankListData(DataTable dt)
        {
            List<ConsigneeBankInfo> CosigneeBankListDataBundle = new List<ConsigneeBankInfo>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ConsigneeBankInfo objConsigneeBankInfo = new ConsigneeBankInfo();
                objConsigneeBankInfo.SerialNumber = dt.Rows[i]["sl"].ToString();
                objConsigneeBankInfo.BankId = dt.Rows[i]["BANK_ID"].ToString();
                objConsigneeBankInfo.BankName = dt.Rows[i]["BANK_NAME"].ToString();
                objConsigneeBankInfo.BankAddress = dt.Rows[i]["BANK_ADDRESS"].ToString();
                objConsigneeBankInfo.EmailAddress = dt.Rows[i]["EMAIL_ADDRESS"].ToString();
                objConsigneeBankInfo.MobileNo = dt.Rows[i]["MOBILE_NO"].ToString();
                objConsigneeBankInfo.PhoneNo = dt.Rows[i]["PHONE_NO"].ToString();
                objConsigneeBankInfo.FaxNo = dt.Rows[i]["FAX_NO"].ToString();
                objConsigneeBankInfo.ContactPerson = dt.Rows[i]["CONTACT_PERSON"].ToString();
                CosigneeBankListDataBundle.Add(objConsigneeBankInfo);
            }

            return CosigneeBankListDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCosigneeBankInfoEntry(ConsigneeBankInfo objConsigneeBankInfo)
        {
            LoadSession();
            objConsigneeBankInfo.UpdateBy = strEmployeeId;
            objConsigneeBankInfo.HeadOfficeId = strHeadOfficeId;
            objConsigneeBankInfo.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveCosigneeBankEntry(objConsigneeBankInfo);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetConsigneeBankInfoRecord");
        }

        public ActionResult ClearCosigneeBankInfoEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("GetConsigneeBankInfoRecord");
        }

        #endregion
        #region IncremetnSetup

        private IncrementSetupModel objIncrementSetupModel = new IncrementSetupModel();

        [HttpGet]
        public ActionResult GetIncrementSetupRecord(string pId, int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objIncrementSetupModel.UpdateBy = strEmployeeId;
                objIncrementSetupModel.HeadOfficeId = strHeadOfficeId;
                objIncrementSetupModel.BranchOfficeId = strBranchOfficeId;

                objIncrementSetupModel.IncrementYear = DateTime.Now.ToString("yyyy");

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objColorModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objIncrementSetupModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objIncrementSetupModel.IncrementYear = pId;
                    objIncrementSetupModel = objLookUpDAL.GetIncrementSetupRecordByYear(objIncrementSetupModel);

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

                DataTable dt = objLookUpDAL.GetIncrementSetupRecord(objIncrementSetupModel);

                List<IncrementSetupModel> IncrementSetupGroupList = IncrementSetupListData(dt);
                ViewBag.IncrementSetupList = IncrementSetupGroupList.ToPagedList(page, pageSize);
                return View(objIncrementSetupModel);
            }
        }
        public List<IncrementSetupModel> IncrementSetupListData(DataTable dt)
        {
            List<IncrementSetupModel> IncrementSetupDataBundle = new List<IncrementSetupModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IncrementSetupModel objIncrementSetupModel = new IncrementSetupModel();

                objIncrementSetupModel.IncrementYear = dt.Rows[i]["increment_year"].ToString();
                objIncrementSetupModel.EffectDate = dt.Rows[i]["effect_Date"].ToString();
                objIncrementSetupModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                IncrementSetupDataBundle.Add(objIncrementSetupModel);
            }
            return IncrementSetupDataBundle;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveIncrementSetupInfo(IncrementSetupModel objIncrementSetupModel)
        {
            LoadSession();
            objIncrementSetupModel.UpdateBy = strEmployeeId;
            objIncrementSetupModel.HeadOfficeId = strHeadOfficeId;
            objIncrementSetupModel.BranchOfficeId = strBranchOfficeId;

            objIncrementSetupModel.IncrementYear = DateTime.Now.ToString("yyyy");

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveIncrementSetupInfo(objIncrementSetupModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetIncrementSetupRecord");
        }
        public ActionResult ClearIncrementSetup()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetIncrementSetupRecord");
        }

        #endregion
        #region MonthlyWorkingDateSetup

        private SalaryMonthModel objSalaryMonthModel = new SalaryMonthModel();

        [HttpGet]
        public ActionResult GetMonthlySalaryDaySetupRecord(string pId, string pMonthId,  int page = 1, int pageSize = 10, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objSalaryMonthModel.UpdateBy = strEmployeeId;
                objSalaryMonthModel.HeadOfficeId = strHeadOfficeId;
                objSalaryMonthModel.BranchOfficeId = strBranchOfficeId;

                objSalaryMonthModel.SalaryYear = DateTime.Now.ToString("yyyy");

                #region Pagination Search

                CheckUrl();
              
              

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objSalaryMonthModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objSalaryMonthModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId) && !string.IsNullOrWhiteSpace(pMonthId))
                {
                    objSalaryMonthModel.SalaryYear = pId;
                    objSalaryMonthModel.MonthId = pMonthId;

                    objSalaryMonthModel = objLookUpDAL.GetMonthlyWorkingDaySetupRecord(objSalaryMonthModel);

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

              
                DataTable dt = objLookUpDAL.GetMonthlyWorkingDayRecord(objSalaryMonthModel);

                List<SalaryMonthModel> SalaryMonthWorkingDayList = SalaryMonthSetupList(dt);
                ViewBag.SalaryMonthSetupList = SalaryMonthWorkingDayList.ToPagedList(page, pageSize);


                ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthId(objSalaryMonthModel), "month_id", "month_name");

                return View(objSalaryMonthModel);
            }
        }
        public List<SalaryMonthModel> SalaryMonthSetupList(DataTable dt)
        {
            List<SalaryMonthModel> SalaryMonthSetupDataBundle = new List<SalaryMonthModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SalaryMonthModel objSalaryMonthModel = new SalaryMonthModel();

                objSalaryMonthModel.SalaryYear = dt.Rows[i]["salary_year"].ToString();
              
                objSalaryMonthModel.FromDate = dt.Rows[i]["from_date"].ToString();
                objSalaryMonthModel.ToDate = dt.Rows[i]["to_date"].ToString();

                objSalaryMonthModel.SerialNumber = dt.Rows[i]["sl"].ToString();
                objSalaryMonthModel.MonthId = dt.Rows[i]["month_id"].ToString();

                SalaryMonthSetupDataBundle.Add(objSalaryMonthModel);
            }
            return SalaryMonthSetupDataBundle;
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSalaryMonthSetupInfo(SalaryMonthModel objSalaryMonthModel)
        {
            LoadSession();
            objSalaryMonthModel.UpdateBy = strEmployeeId;
            objSalaryMonthModel.HeadOfficeId = strHeadOfficeId;
            objSalaryMonthModel.BranchOfficeId = strBranchOfficeId;

            //objIncrementSetupModel.IncrementYear = DateTime.Now.ToString("yyyy");

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveSalaryMonthSetupInfo(objSalaryMonthModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != 0)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetMonthlySalaryDaySetupRecord");
        }
        public ActionResult ClearMonthlyWorkingDaySetup()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetMonthlySalaryDaySetupRecord");
        }

        #endregion


        //commercial part

        #region Buyer Entry

        BuyerModel objBuyerModel = new BuyerModel();

        public ActionResult GetBuyerRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objBuyerModel.UpdateBy = strEmployeeId;
                objBuyerModel.HeadOfficeId = strHeadOfficeId;
                objBuyerModel.BranchOfficeId = strBranchOfficeId;

                objCountryModel.UpdateBy = strEmployeeId;
                objCountryModel.HeadOfficeId = strHeadOfficeId;
                objCountryModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objBuyerModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objBuyerModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objBuyerModel.BuyerId = pId;
                    objBuyerModel = objLookUpDAL.GetBuyerById(objBuyerModel);

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


                DataTable dt = objLookUpDAL.GetBuyerRecord(objBuyerModel);

                var buyerList = BuyerListData(dt);
                ViewBag.Buyer = buyerList.ToPagedList(page, pageSize);
                ViewBag.CountryDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCountryRecord(objCountryModel), "COUNTRY_ID", "COUNTRY_NAME");

                return View(objBuyerModel);
            }
        }

        public List<BuyerModel> BuyerListData(DataTable dt)
        {
            List<BuyerModel> buyerDataBundle = new List<BuyerModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BuyerModel objBuyer = new BuyerModel();

                objBuyer.BuyerId = dt.Rows[i]["BUYER_ID"].ToString();
                objBuyer.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objBuyer.BuyerContactNo = dt.Rows[i]["CONTACT_NO"].ToString();
                objBuyer.BuyerAddress = dt.Rows[i]["BUYER_ADDRESS"].ToString();
                objBuyer.BuyerPaymentBy = dt.Rows[i]["PAYMENT_BY"].ToString();
                objBuyer.CountryId = dt.Rows[i]["COUNTRY_ID"].ToString();
                objBuyer.CountryName = dt.Rows[i]["COUNTRY_NAME"].ToString();
                objBuyer.BuyerEmail = dt.Rows[i]["EMAIL_ADDRESS"].ToString();
                objBuyer.SerialNumber = dt.Rows[i]["sl"].ToString();

                buyerDataBundle.Add(objBuyer);
            }

            return buyerDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBuyerInfo(BuyerModel objBuyer)
        {
            LoadSession();
            objBuyer.UpdateBy = strEmployeeId;
            objBuyer.HeadOfficeId = strHeadOfficeId;
            objBuyer.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveBuyerInfo(objBuyer);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetBuyerRecord");
        }

        public ActionResult ClearBuyerInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetBuyerRecord");
        }

        #endregion

        #region Supplier Entry

        SupplierModel objSupplierModel = new SupplierModel();

        public ActionResult GetSupplierRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objSupplierModel.UpdateBy = strEmployeeId;
                objSupplierModel.HeadOfficeId = strHeadOfficeId;
                objSupplierModel.BranchOfficeId = strBranchOfficeId;

                objCountryModel.UpdateBy = strEmployeeId;
                objCountryModel.HeadOfficeId = strHeadOfficeId;
                objCountryModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objSupplierModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objSupplierModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objSupplierModel.SupplierId = pId;
                    objSupplierModel = objLookUpDAL.GetSupplierById(objSupplierModel);

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


                DataTable dt = objLookUpDAL.GetSupplierRecord(objSupplierModel);

                var supplierList = SupplierListData(dt);
                ViewBag.Supplier = supplierList.ToPagedList(page, pageSize);
                ViewBag.CountryDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCountryRecord(objCountryModel), "COUNTRY_ID", "COUNTRY_NAME");

                return View(objSupplierModel);
            }
        }

        public List<SupplierModel> SupplierListData(DataTable dt)
        {
            List<SupplierModel> supplierDataBundle = new List<SupplierModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SupplierModel objSupplier = new SupplierModel();

                objSupplier.SupplierId = dt.Rows[i]["SUPPLIER_ID"].ToString();
                objSupplier.SupplierName = dt.Rows[i]["SUPPLIER_NAME"].ToString();
                objSupplier.SupplierContactNo = dt.Rows[i]["CONTACT_NO"].ToString();
                objSupplier.SupplierAddress = dt.Rows[i]["SUPPLIER_ADDRESS"].ToString();
                objSupplier.CountryId = dt.Rows[i]["COUNTRY_ID"].ToString();
                objSupplier.CountryName = dt.Rows[i]["COUNTRY_NAME"].ToString();
                objSupplier.SupplierEmail = dt.Rows[i]["EMAIL_ADDRESS"].ToString();
                objSupplier.SwiftCode = dt.Rows[i]["SWIFT_CODE"].ToString();
                objSupplier.BankName = dt.Rows[i]["BANK_NAME"].ToString();
                objSupplier.SerialNumber = dt.Rows[i]["sl"].ToString();

                supplierDataBundle.Add(objSupplier);
            }

            return supplierDataBundle;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSupplierInfo(SupplierModel objModel)
        {
            LoadSession();
            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveSupplierInfo(objModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetSupplierRecord");
        }

        public ActionResult ClearSupplierInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetSupplierRecord");
        }

        #endregion

        #region Mode of Payment

        ModeofPaymentModel objModeofPaymentModel = new ModeofPaymentModel();

        public List<ModeofPaymentModel> ModeofPaymentListData(DataTable dt)
        {
            List<ModeofPaymentModel> paymentModeDataBundle = new List<ModeofPaymentModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ModeofPaymentModel objPayment = new ModeofPaymentModel();

                objPayment.ModeofPaymentId = dt.Rows[i]["MODE_OF_PAYMENT_ID"].ToString();
                objPayment.ModeofPaymentName = dt.Rows[i]["MODE_OF_PAYMENT_NAME"].ToString();
                objPayment.SerialNumber = dt.Rows[i]["sl"].ToString();


                paymentModeDataBundle.Add(objPayment);
            }

            return paymentModeDataBundle;
        }

        public ActionResult GetModeofPaymentRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objModeofPaymentModel.UpdateBy = strEmployeeId;
                objModeofPaymentModel.HeadOfficeId = strHeadOfficeId;
                objModeofPaymentModel.BranchOfficeId = strBranchOfficeId;


                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objModeofPaymentModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objModeofPaymentModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objModeofPaymentModel.ModeofPaymentId = pId;
                    objModeofPaymentModel = objLookUpDAL.GetModeofPaymentById(objModeofPaymentModel);

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


                DataTable dt = objLookUpDAL.GetModeofPaymentRecord(objModeofPaymentModel);

                var paymentModeList = ModeofPaymentListData(dt);
                ViewBag.PaymentMode = paymentModeList.ToPagedList(page, pageSize);

                return View(objModeofPaymentModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveModeofPaymentInfo(ModeofPaymentModel objModel)
        {
            LoadSession();
            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveModeOfPaymentInfo(objModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetModeofPaymentRecord");
        }

        public ActionResult ClearModeofPaymentInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetModeofPaymentRecord");
        }


        #endregion

        #region Item Entry Info

        ItemModel objItemModel = new ItemModel();

        public List<ItemModel> ItemListData(DataTable dt)
        {
            List<ItemModel> itemDataBundle = new List<ItemModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ItemModel objItem = new ItemModel();

                objItem.ItemId = dt.Rows[i]["ITEM_ID"].ToString();
                objItem.ItemName = dt.Rows[i]["ITEM_NAME"].ToString();
                objItem.SerialNumber = dt.Rows[i]["sl"].ToString();


                itemDataBundle.Add(objItem);
            }

            return itemDataBundle;
        }

        public ActionResult GetItemRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objItemModel.UpdateBy = strEmployeeId;
                objItemModel.HeadOfficeId = strHeadOfficeId;
                objItemModel.BranchOfficeId = strBranchOfficeId;


                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objItemModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objItemModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objItemModel.ItemId = pId;
                    objItemModel = objLookUpDAL.GetItemById(objItemModel);

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


                DataTable dt = objLookUpDAL.GetItemRecord(objItemModel);

                var itemList = ItemListData(dt);
                ViewBag.Item = itemList.ToPagedList(page, pageSize);

                return View(objItemModel);
            }
        }

        //      Save action for item---

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveItemInfo(ItemModel objModel)
        {
            LoadSession();
            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveItemInfo(objModel); // perform save 
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetItemRecord");
        }

        // clearing page or refresh action
        public ActionResult ClearItemInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetItemRecord");
        }


        #endregion

        #region Payment Mode

        PaymentMode objPaymentModeModel = new PaymentMode();

        public List<PaymentMode> PaymentModeListData(DataTable dt)
        {
            List<PaymentMode> paymentModeDataBundle = new List<PaymentMode>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PaymentMode objPayment = new PaymentMode();

                objPayment.PaymentModeId = dt.Rows[i]["PAYMENT_MODE_ID"].ToString();
                objPayment.PaymentModeName = dt.Rows[i]["PAYMENT_MODE_NAME"].ToString();
                objPayment.SerialNumber = dt.Rows[i]["sl"].ToString();


                paymentModeDataBundle.Add(objPayment);
            }

            return paymentModeDataBundle;
        }

        public ActionResult GetPaymentModeRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objPaymentModeModel.UpdateBy = strEmployeeId;
                objPaymentModeModel.HeadOfficeId = strHeadOfficeId;
                objPaymentModeModel.BranchOfficeId = strBranchOfficeId;


                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objPaymentModeModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objPaymentModeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objPaymentModeModel.PaymentModeId = pId;
                    objPaymentModeModel = objLookUpDAL.GetPaymentModeById(objPaymentModeModel);

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


                DataTable dt = objLookUpDAL.GetPaymentModeRecord(objPaymentModeModel);

                var paymentModeList = PaymentModeListData(dt);
                ViewBag.PaymentMode = paymentModeList.ToPagedList(page, pageSize);

                return View(objPaymentModeModel);
            }
        }

        //      Save action for item---

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SavePaymentModeInfo(PaymentMode objModel)
        {
            LoadSession();
            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SavePaymentModeInfo(objModel); // perform save 
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetPaymentModeRecord");
        }

        // clearing page or refresh action
        public ActionResult ClearPaymentModeInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetPaymentModeRecord");
        }


        #endregion

        #region Negotiation Term

        NegotiationTermModel objNegotiationTermModel = new NegotiationTermModel();

        public List<NegotiationTermModel> NegotiationTermListData(DataTable dt)
        {
            List<NegotiationTermModel> termDataBundle = new List<NegotiationTermModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                NegotiationTermModel objTerm = new NegotiationTermModel();

                objTerm.NegotitaionId = dt.Rows[i]["NEGOTIATION_TERM_ID"].ToString();
                objTerm.NegotiationName = dt.Rows[i]["NEGOTIATION_TERM_NAME"].ToString();
                objTerm.SerialNumber = dt.Rows[i]["sl"].ToString();

                termDataBundle.Add(objTerm);
            }

            return termDataBundle;
        }

        public ActionResult GetNegotiationTermRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objNegotiationTermModel.UpdateBy = strEmployeeId;
                objNegotiationTermModel.HeadOfficeId = strHeadOfficeId;
                objNegotiationTermModel.BranchOfficeId = strBranchOfficeId;


                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objNegotiationTermModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objNegotiationTermModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objNegotiationTermModel.NegotitaionId = pId;
                    objNegotiationTermModel = objLookUpDAL.GetNegotiationTermById(objNegotiationTermModel);

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


                DataTable dt = objLookUpDAL.GetNegotiationRecord(objNegotiationTermModel);

                var termModeList = NegotiationTermListData(dt);
                ViewBag.NegotiationTerm = termModeList.ToPagedList(page, pageSize);

                return View("GetNegotiationTermRecord", objNegotiationTermModel);
            }
        }


        // save negotiation term information----
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNegotiationTermInfo(NegotiationTermModel objModel)
        {
            LoadSession();
            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveNegotiationTermInfo(objModel); // save 
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetNegotiationTermRecord");
        }

        public ActionResult ClearNegotiationTermInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetNegotiationTermRecord");
        }


        #endregion

        #region Buyer Bank Entry

        BuyerBankModel objBuyerBankModel = new BuyerBankModel();

        public List<BuyerBankModel> BuyerBankListData(DataTable dt)
        {
            List<BuyerBankModel> buyerDataBundle = new List<BuyerBankModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                BuyerBankModel objBuyerBank = new BuyerBankModel();

                objBuyerBank.BuyerBankId = dt.Rows[i]["BANK_ID"].ToString();
                objBuyerBank.BuyerBankName = dt.Rows[i]["BANK_NAME"].ToString();
                objBuyerBank.SwiftNo = dt.Rows[i]["SWIFT_NO"].ToString();
                objBuyerBank.BankAddress = dt.Rows[i]["BANK_ADDRESS"].ToString();
                objBuyerBank.BuyerName = dt.Rows[i]["BUYER_NAME"].ToString();
                objBuyerBank.SerialNumber = dt.Rows[i]["sl"].ToString();

                buyerDataBundle.Add(objBuyerBank);
            }
            return buyerDataBundle;
        }

        public ActionResult GetBuyerBankRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objBuyerBankModel.UpdateBy = strEmployeeId;
                objBuyerBankModel.HeadOfficeId = strHeadOfficeId;
                objBuyerBankModel.BranchOfficeId = strBranchOfficeId;

                objBuyerModel.UpdateBy = strEmployeeId;
                objBuyerModel.HeadOfficeId = strHeadOfficeId;
                objBuyerModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objBuyerBankModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objBuyerBankModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objBuyerBankModel.BuyerBankId = pId;
                    objBuyerBankModel = objLookUpDAL.GetBuyerBankById(objBuyerBankModel);

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


                DataTable dt = objLookUpDAL.GetBuyerBankRecord(objBuyerBankModel);

                var buyerBankList = BuyerBankListData(dt);
                ViewBag.BuyerBank = buyerBankList.ToPagedList(page, pageSize);
                ViewBag.BuyerDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetBuyerRecord(objBuyerModel), "BUYER_ID", "BUYER_NAME");

                return View(objBuyerBankModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveBuyerBankInfo(BuyerBankModel objBank)
        {
            LoadSession();
            objBank.UpdateBy = strEmployeeId;
            objBank.HeadOfficeId = strHeadOfficeId;
            objBank.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveBuyerBankInfo(objBank);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetBuyerBankRecord");
        }

        public ActionResult ClearBuyerBankInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetBuyerBankRecord");
        }

        #endregion

        #region Seller Bank Info

        SellerBankModel objSellerBankModel = new SellerBankModel();
        public List<SellerBankModel> SellerBankListData(DataTable dt)
        {
            List<SellerBankModel> buyerDataBundle = new List<SellerBankModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SellerBankModel objSellerBank = new SellerBankModel();

                objSellerBank.SellerBankId = dt.Rows[i]["BANK_ID"].ToString();
                objSellerBank.BankName = dt.Rows[i]["BANK_NAME"].ToString();
                objSellerBank.SwiftNo = dt.Rows[i]["SWIFT_NO"].ToString();
                objSellerBank.BankAddress = dt.Rows[i]["BANK_ADDRESS"].ToString();
                objSellerBank.FactroyName = dt.Rows[i]["BRANCH_OFFICE_NAME"].ToString();
                objSellerBank.SerialNumber = dt.Rows[i]["sl"].ToString();

                buyerDataBundle.Add(objSellerBank);
            }
            return buyerDataBundle;
        }

        public ActionResult GetSellerBankRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objSellerBankModel.UpdateBy = strEmployeeId;
                objSellerBankModel.HeadOfficeId = strHeadOfficeId;
                objSellerBankModel.BranchOfficeId = strBranchOfficeId;

                //objBuyerModel.UpdateBy = strEmployeeId;
                //objBuyerModel.HeadOfficeId = strHeadOfficeId;
                //objBuyerModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objSellerBankModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objSellerBankModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objSellerBankModel.SellerBankId = pId;
                    objSellerBankModel = objLookUpDAL.GetSellerBankById(objSellerBankModel);

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


                DataTable dt = objLookUpDAL.GetSellerBankRecord(objSellerBankModel);

                var sellerBankList = SellerBankListData(dt);
                ViewBag.SellerBank = sellerBankList.ToPagedList(page, pageSize);
                ViewBag.SellerDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetBranchOfficeId(), "BRANCH_OFFICE_ID", "BRANCH_OFFICE_NAME");

                return View(objSellerBankModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSellerBankInfo(SellerBankModel objBank)
        {
            try
            {
                LoadSession();
                objBank.UpdateBy = strEmployeeId;
                objBank.HeadOfficeId = strHeadOfficeId;
                objBank.BranchOfficeId = strBranchOfficeId;

                if (ModelState.IsValid)
                {
                    string strDBMsg = "";
                    strDBMsg = objLookUpDAL.SaveSellerBankInfo(objBank);
                    TempData["OperationMessage"] = strDBMsg;
                }

                #region Pagination Search

                int page = (int)TempData["GetActionPage"];

                if (page >= 1 || page != null)
                {
                    TempData["SaveActionPage"] = page;
                    TempData.Keep("GetActionPage");
                }

                TempData["SaveActionFlag"] = 1;

                #endregion

                return RedirectToAction("GetSellerBankRecord");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public ActionResult ClearSellBankInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetSellerBankRecord");
        }

        #endregion

        #region Import Origin

        ImportOriginModel objOriginModel = new ImportOriginModel();

        public List<ImportOriginModel> ImportOriginListData(DataTable dt)
        {
            List<ImportOriginModel> importOriginDataBundle = new List<ImportOriginModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ImportOriginModel objImport = new ImportOriginModel();

                objImport.ImportOriginId = dt.Rows[i]["IMPORT_ORIGIN_ID"].ToString();
                objImport.ImportOriginName = dt.Rows[i]["IMPORT_ORIGIN_NAME"].ToString();
                objImport.SerialNumber = dt.Rows[i]["sl"].ToString();


                importOriginDataBundle.Add(objImport);
            }

            return importOriginDataBundle;
        }

        public ActionResult GetImportOriginRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objOriginModel.UpdateBy = strEmployeeId;
                objOriginModel.HeadOfficeId = strHeadOfficeId;
                objOriginModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objOriginModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objOriginModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objOriginModel.ImportOriginId = pId;
                    objOriginModel = objLookUpDAL.GetImportOriginById(objOriginModel);

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

                DataTable dt = objLookUpDAL.GetImportOriginRecord(objOriginModel);

                var importList = ImportOriginListData(dt);
                ViewBag.Import = importList.ToPagedList(page, pageSize);

                return View(objOriginModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveImportOriginInfo(ImportOriginModel objModel)
        {
            LoadSession();
            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveImportOriginInfo(objModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetImportOriginRecord");
        }

        public ActionResult ClearImportOriginInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetImportOriginRecord");
        }


        #endregion

        #region Season

        SeasonModel objSeasonModel = new SeasonModel();
        public List<SeasonModel> SeasonListData(DataTable dt)
        {
            List<SeasonModel> seasonDataBundle = new List<SeasonModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SeasonModel objSeason = new SeasonModel();

                objSeason.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                objSeason.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objSeason.SerialNumber = dt.Rows[i]["sl"].ToString();


                seasonDataBundle.Add(objSeason);
            }

            return seasonDataBundle;
        }

        public ActionResult GetSeasonRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objSeasonModel.UpdateBy = strEmployeeId;
                objSeasonModel.HeadOfficeId = strHeadOfficeId;
                objSeasonModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objSeasonModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objSeasonModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objSeasonModel.SeasonId = pId;
                    objSeasonModel = objLookUpDAL.GetSeasonById(objSeasonModel);

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

                DataTable dt = objLookUpDAL.GetSeasonRecord(objSeasonModel);

                var seasonList = SeasonListData(dt);
                ViewBag.Season = seasonList.ToPagedList(page, pageSize);

                return View(objSeasonModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSeasonInfo(SeasonModel objModel)
        {
            LoadSession();
            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveSeasonInfo(objModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetSeasonRecord");
        }

        public ActionResult ClearSeasonInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetSeasonRecord");
        }

        #endregion

        #region Size

        SizeModel objSizeModel = new SizeModel();
        public List<SizeModel> SizeListData(DataTable dt)
        {
            List<SizeModel> sizeDataBundle = new List<SizeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SizeModel objSize = new SizeModel();

                objSize.SizeId = dt.Rows[i]["SIZE_ID"].ToString();
                objSize.SizeName = dt.Rows[i]["SIZE_NAME"].ToString();
                objSize.SizeValue = dt.Rows[i]["SIZE_VALUE"].ToString();
                objSize.SerialNumber = dt.Rows[i]["sl"].ToString();

                sizeDataBundle.Add(objSize);
            }

            return sizeDataBundle;
        }

        public ActionResult GetCpSizeRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objSizeModel.UpdateBy = strEmployeeId;
                objSizeModel.HeadOfficeId = strHeadOfficeId;
                objSizeModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objSizeModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objSizeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objSizeModel.SizeId = pId;
                    objSizeModel = objLookUpDAL.GetCpSizeById(objSizeModel);

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

                DataTable dt = objLookUpDAL.GetCpSizeRecord(objSizeModel);

                var cpSizeList = SizeListData(dt);
                ViewBag.CpSize = cpSizeList.ToPagedList(page, pageSize);

                return View(objSizeModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCpSizeInfo(SizeModel objModel)
        {
            LoadSession();
            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveCpSizeInfo(objModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetCpSizeRecord");
        }

        public ActionResult ClearCpSizeInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetCpSizeRecord");
        }

        #endregion

        #region Product Category

        CategoryModel objCategoryModel = new CategoryModel();
        public List<CategoryModel> CategoryListData(DataTable dt)
        {
            List<CategoryModel> categoryDataBundle = new List<CategoryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CategoryModel objCategory = new CategoryModel();

                objCategory.CategoryId = dt.Rows[i]["CATEGORY_ID"].ToString();
                objCategory.CategoryName = dt.Rows[i]["CATEGORY_NAME"].ToString();
                objCategory.SerialNumber = dt.Rows[i]["sl"].ToString();

                categoryDataBundle.Add(objCategory);
            }

            return categoryDataBundle;
        }

        public ActionResult GetCategoryRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objCategoryModel.UpdateBy = strEmployeeId;
                objCategoryModel.HeadOfficeId = strHeadOfficeId;
                objCategoryModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objCategoryModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objCategoryModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objCategoryModel.CategoryId = pId;
                    objCategoryModel = objLookUpDAL.GetProductCategoryById(objCategoryModel);

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

                DataTable dt = objLookUpDAL.GetProductCategoryRecord(objCategoryModel);

                var categoryList = CategoryListData(dt);
                ViewBag.Category = categoryList.ToPagedList(page, pageSize);

                return View(objCategoryModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveCategoryInfo(CategoryModel objModel)
        {
            LoadSession();
            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveProductCategoryInfo(objModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetCategoryRecord");
        }

        public ActionResult ClearCategoryInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetCategoryRecord");
        }

        #endregion
        #region Occasion

        OccasionModel objOccasionModel = new OccasionModel();
        public List<OccasionModel> OccasionListData(DataTable dt)
        {
            List<OccasionModel> OccasionDataBundle = new List<OccasionModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
               
                objOccasionModel.OccasionId = dt.Rows[i]["OCCASION_ID"].ToString();
                objOccasionModel.OccasionName = dt.Rows[i]["OCCASION_NAME"].ToString();
                objOccasionModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                OccasionDataBundle.Add(objOccasionModel);
            }

            return OccasionDataBundle;
        }

        public ActionResult GetOccasionRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objOccasionModel.UpdateBy = strEmployeeId;
                objOccasionModel.HeadOfficeId = strHeadOfficeId;
                objOccasionModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objOccasionModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objOccasionModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objOccasionModel.OccasionId = pId;
                    objOccasionModel = objLookUpDAL.GetOccasionRecordById(objOccasionModel);

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

                DataTable dt = objLookUpDAL.GetOccasionRecord(objOccasionModel);

                var OccasionList = OccasionListData(dt);
                ViewBag.Occasion = OccasionList.ToPagedList(page, pageSize);

                return View(objOccasionModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveOccasionInfo(OccasionModel objOccasionModel)
        {
            LoadSession();
            objOccasionModel.UpdateBy = strEmployeeId;
            objOccasionModel.HeadOfficeId = strHeadOfficeId;
            objOccasionModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveOccasionInfo(objOccasionModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetOccasionRecord");
        }

        public ActionResult ClearOccasionInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetOccasionRecord");
        }

        #endregion


        #region Fit Entry

        FitEntryModel objFitEntryModel = new FitEntryModel();
        public List<FitEntryModel> FitListData(DataTable dt)
        {
            List<FitEntryModel> fitDataBundle = new List<FitEntryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FitEntryModel objFit = new FitEntryModel();

                objFit.FitId = dt.Rows[i]["FIT_ID"].ToString();
                objFit.FitName = dt.Rows[i]["FIT_NAME"].ToString();
                objFit.SerialNumber = dt.Rows[i]["sl"].ToString();

                fitDataBundle.Add(objFit);
            }

            return fitDataBundle;
        }

        public ActionResult GetFitRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objFitEntryModel.UpdateBy = strEmployeeId;
                objFitEntryModel.HeadOfficeId = strHeadOfficeId;
                objFitEntryModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objFitEntryModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objFitEntryModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objFitEntryModel.FitId = pId;
                    objFitEntryModel = objLookUpDAL.GetFitById(objFitEntryModel);

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

                DataTable dt = objLookUpDAL.GetFitRecord(objFitEntryModel);

                var fitList = FitListData(dt);
                ViewBag.Fit = fitList.ToPagedList(page, pageSize);

                return View(objFitEntryModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveFitInfo(FitEntryModel objModel)
        {
            LoadSession();
            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveFitInfo(objModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetFitRecord");
        }

        public ActionResult ClearFitInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetFitRecord");
        }

        #endregion

        #region Sub Category

        SubCategoryModel objSubCategoryModel = new SubCategoryModel();
        public List<SubCategoryModel> SubCategoryFitListData(DataTable dt)
        {
            List<SubCategoryModel> subCatDataBundle = new List<SubCategoryModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SubCategoryModel objModel = new SubCategoryModel();

                objModel.SubCategoryId = dt.Rows[i]["SUB_CATEGORY_ID"].ToString();
                objModel.SubCategoryName = dt.Rows[i]["SUB_CATEGORY_NAME"].ToString();
                objModel.CategoryName = dt.Rows[i]["CATEGORY_NAME"].ToString();
                objModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                subCatDataBundle.Add(objModel);
            }

            return subCatDataBundle;
        }

        public ActionResult GetSubCategoryRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objSubCategoryModel.UpdateBy = strEmployeeId;
                objSubCategoryModel.HeadOfficeId = strHeadOfficeId;
                objSubCategoryModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objSubCategoryModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objSubCategoryModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objSubCategoryModel.SubCategoryId = pId;
                    objSubCategoryModel = objLookUpDAL.GetSubCategoryById(objSubCategoryModel);

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

                DataTable dt = objLookUpDAL.GetProductSubCategoryRecord(objSubCategoryModel);

                var subCatList = SubCategoryFitListData(dt);
                ViewBag.SubCat = subCatList.ToPagedList(page, pageSize);
                ViewBag.CategoryDDL = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCategoryId(), "CATEGORY_ID", "CATEGORY_NAME");

                return View(objSubCategoryModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveSubCategoryInfo(SubCategoryModel objModel)
        {
            LoadSession();
            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveProductSubCategoryInfo(objModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetSubCategoryRecord");
        }

        public ActionResult ClearSubCategoryInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetSubCategoryRecord");
        }

        #endregion


        #region Fit Entry

        FabricTypeModel objFabricTypeModel = new FabricTypeModel();
        public List<FabricTypeModel> FabricTypeListData(DataTable dt)
        {
            List<FabricTypeModel> fabricTypeDataBundle = new List<FabricTypeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                FabricTypeModel objFabric = new FabricTypeModel();

                objFabric.FabricTypeId = dt.Rows[i]["FABRIC_TYPE_ID"].ToString();
                objFabric.FabricTypeName = dt.Rows[i]["FABRIC_TYPE_NAME"].ToString();
                objFabric.SerialNumber = dt.Rows[i]["sl"].ToString();

                fabricTypeDataBundle.Add(objFabric);
            }

            return fabricTypeDataBundle;
        }

        public ActionResult GetFabricTypeRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();
                objFabricTypeModel.UpdateBy = strEmployeeId;
                objFabricTypeModel.HeadOfficeId = strHeadOfficeId;
                objFabricTypeModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objFabricTypeModel.SearchBy = SearchBy.Trim();

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objFabricTypeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objFabricTypeModel.FabricTypeId = pId;
                    objFabricTypeModel = objLookUpDAL.GetFabricById(objFabricTypeModel);

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

                DataTable dt = objLookUpDAL.GetFabricTypeRecord(objFabricTypeModel);

                var fabricTypeList = FabricTypeListData(dt);
                ViewBag.FabricType = fabricTypeList.ToPagedList(page, pageSize);

                return View(objFabricTypeModel);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveFabricTypeInfo(FabricTypeModel objModel)
        {
            LoadSession();
            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveFabrciTypeInfo(objModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetFabricTypeRecord");
        }

        public ActionResult ClearFabricTypeInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetFabricTypeRecord");
        }

        #endregion



        #region Shift Time Set

        private ShiftTimeModel objShiftTimeModel = new ShiftTimeModel();

        public List<ShiftTimeModel> ShiftTimeListData(DataTable dt)
        {
            List<ShiftTimeModel> shiftTimeDataBundle = new List<ShiftTimeModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ShiftTimeModel objModel = new ShiftTimeModel();

                objModel.ShiftTimeId = dt.Rows[i]["SHIFT_TIME_ID"].ToString();
                objModel.FirstInTime = dt.Rows[i]["FIRST_IN_TIME"].ToString();
                objModel.LastOutTime = dt.Rows[i]["LAST_OUT_TIME"].ToString();
                objModel.LunchOutTime = dt.Rows[i]["LUNCH_OUT_TIME"].ToString();
                objModel.LunchInTime = dt.Rows[i]["LUNCH_IN_TIME"].ToString();
                objModel.ShiftName = dt.Rows[i]["SHIFT_NAME"].ToString();

                objModel.SerialNumber = dt.Rows[i]["SL"].ToString();

                shiftTimeDataBundle.Add(objModel);
            }

            return shiftTimeDataBundle;
        }

        [HttpGet]
        public ActionResult GetShiftTimeRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objShiftTimeModel.UpdateBy = strEmployeeId;
                objShiftTimeModel.HeadOfficeId = strHeadOfficeId;
                objShiftTimeModel.BranchOfficeId = strBranchOfficeId;


                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objShiftTimeModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objShiftTimeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objShiftTimeModel.ShiftTimeId = pId;
                    objShiftTimeModel = objLookUpDAL.GetShiftTimeById(objShiftTimeModel);

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

                DataTable dt = objLookUpDAL.GetShiftTimeRecord(objShiftTimeModel);

                var shiftTimeList = ShiftTimeListData(dt);
                ViewBag.ShiftTime = shiftTimeList.ToPagedList(page, pageSize);
                ViewBag.ShiftTimeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetShiftDDList(), "SHIFT_ID", "SHIFT_NAME");

                return View(objShiftTimeModel);
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveShiftTimeInfo(ShiftTimeModel objModel)
        {
            LoadSession();

            objModel.UpdateBy = strEmployeeId;
            objModel.HeadOfficeId = strHeadOfficeId;
            objModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                string strDBMsg = "";
                strDBMsg = objLookUpDAL.SaveShiftTimeInfo(objModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion


            return RedirectToAction("GetShiftTimeRecord");
        }

        public ActionResult ClearShiftTimeInfo()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetShiftTimeRecord");
        }

        #endregion


        #region Office Time Special

        private List<OfficeTimeSpecialModel> GetOfficeTimeSpecialListByDataTable(DataTable dt)
        {
            List<OfficeTimeSpecialModel> officeTimeList = new List<OfficeTimeSpecialModel>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                OfficeTimeSpecialModel objOfficeTimeModel = new OfficeTimeSpecialModel();

                objOfficeTimeModel.OfficeTimeId = dt.Rows[i]["office_time_id"].ToString();
                objOfficeTimeModel.UnitName = dt.Rows[i]["UNIT_NAME"].ToString();
                objOfficeTimeModel.FromDate = dt.Rows[i]["from_date"].ToString();
                objOfficeTimeModel.FirstInTime = dt.Rows[i]["FIRST_IN_TIME"].ToString();
                objOfficeTimeModel.LastOutTime = dt.Rows[i]["LAST_OUT_TIME"].ToString();
                objOfficeTimeModel.LunchOutTime = dt.Rows[i]["LUNCH_OUT_TIME"].ToString();
                objOfficeTimeModel.LunchInTime = dt.Rows[i]["LUNCH_IN_TIME"].ToString();
                objOfficeTimeModel.SerialNumber = dt.Rows[i]["sl"].ToString();

                officeTimeList.Add(objOfficeTimeModel);
            }

            return officeTimeList;
        }

        [HttpGet]
        public ActionResult GetOfficeTimeSpecialEntryRecord(string pId, int page = 1, int pageSize = 100, string SearchBy = "")
        {
            OfficeTimeSpecialModel objOfficeTimeModel = new OfficeTimeSpecialModel();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }
            else
            {
                LoadSession();

                objOfficeTimeModel.UpdateBy = strEmployeeId;
                objOfficeTimeModel.HeadOfficeId = strHeadOfficeId;
                objOfficeTimeModel.BranchOfficeId = strBranchOfficeId;

                #region Pagination Search

                CheckUrl();

                if (!string.IsNullOrWhiteSpace(SearchBy))
                {
                    objOfficeTimeModel.SearchBy = SearchBy;

                    TempData["SearchFlag"] = 1;
                    TempData["SearchPage"] = page;
                    TempData["SearchValue"] = SearchBy;
                }

                if (TempData.ContainsKey("SearchValue"))
                {
                    objOfficeTimeModel.SearchBy = TempData["SearchValue"].ToString();
                    TempData.Keep("SearchValue");
                }

                if (!string.IsNullOrWhiteSpace(pId))
                {
                    objOfficeTimeModel.OfficeTimeId = pId;
                    objOfficeTimeModel = objLookUpDAL.GetOfficeTimeSpecialById(objOfficeTimeModel);

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

                DataTable dt = objLookUpDAL.GetOfficeTimeRecordSpecial(objOfficeTimeModel);
                List<OfficeTimeSpecialModel> officeTimeSpecialList = GetOfficeTimeSpecialListByDataTable(dt);

                ViewBag.OfficeTimeSpecialList = officeTimeSpecialList.ToPagedList(page, pageSize);

                ViewBag.UnitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetUnitDDList(strHeadOfficeId, strBranchOfficeId), "UNIT_ID", "UNIT_NAME");

                return View(objOfficeTimeModel);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveOfficeTimeSpecial(OfficeTimeSpecialModel objOfficeTimeModel)
        {
            LoadSession();

            objOfficeTimeModel.UpdateBy = strEmployeeId;
            objOfficeTimeModel.HeadOfficeId = strHeadOfficeId;
            objOfficeTimeModel.BranchOfficeId = strBranchOfficeId;


            if (ModelState.IsValid)
            {
                string vDbMessage = objLookUpDAL.SaveOfficeTimeInfoSpecial(objOfficeTimeModel);
                TempData["OperationMessage"] = vDbMessage;
            }

            #region Pagination Search

            int page = (int)TempData["GetActionPage"];

            if (page >= 1 || page != null)
            {
                TempData["SaveActionPage"] = page;
                TempData.Keep("GetActionPage");
            }

            TempData["SaveActionFlag"] = 1;

            #endregion

            return RedirectToAction("GetOfficeTimeSpecialEntryRecord");
        }

        public ActionResult ClearOfficeTimeSplecial()
        {
            ModelState.Clear();

            TempData["SearchValue"] = "";

            return RedirectToAction("GetOfficeTimeSpecialEntryRecord");
        }

        #endregion
    }
}