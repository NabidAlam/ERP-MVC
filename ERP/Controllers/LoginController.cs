using System.Web.Mvc;
using ERP.MODEL;
using ERP.DAL;
using System;


namespace ERP.Controllers
{
    public class LoginController : Controller
    {
        LoginDAL objLoginDAL = new LoginDAL();

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
        public ActionResult Login(string flag)
        {
            if (flag =="0")
            {
                ViewBag.Message = "WRONG INFORMATION, PLEASE TRY AGAIN !!"; ;
            }
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CheckValidUser(LoginModel objLoginModel)
        {
            string strDBMsg = string.Empty;

            if (ModelState.IsValid)
            {

                string strUserId = objLoginModel.EmployeeId;
                string strPassword = objLoginModel.EmployeePassword;
                string strIpAddress = "";

                objLoginModel = objLoginDAL.CheckValidUser(strUserId, strPassword, strIpAddress);

                if (objLoginModel.Message == "TRUE")
                {
                    Session["strEmployeeId"] = objLoginModel.EmployeeId;
                    Session["strDesignationId"] = objLoginModel.DesignationId;
                    Session["strUnitId"] = objLoginModel.UnitId;
                    Session["strSubSectionId"] = objLoginModel.SubSectionId;
                    Session["strHeadOfficeId"] = objLoginModel.HeadOfficeId;
                    Session["strBranchOfficeId"] = objLoginModel.BranchOfficeId;
                    Session["strHeadOfficeName"] = objLoginModel.HeadOfficeName;
                    Session["strBranchOfficeName"] = objLoginModel.BranchOfficeName;
                    Session["strSoftId"] = objLoginModel.SoftwareId;
                    Session["strEmployeeName"] = objLoginModel.EmployeeName;
                    Session["strEmployeePass"] = objLoginModel.EmployeePassword;

                    return RedirectToAction("Dashboard", "Dashboard");
                }
                else
                {
                    string flag = "0";
                    return RedirectToAction("Login", new { flag });
                }
            }

            return RedirectToAction("Login");

        }
        public ActionResult PasswordChange(PassworChange objPassworChange)
        {
            string strDBMsg = string.Empty;
            string strMsg = string.Empty;
            if (ModelState.IsValid)
            {
                LoadSession();

                string strNewPassword = objPassworChange.NewPassword;
                string strConfirmPassword = objPassworChange.ConfirmPassword;


                if(strNewPassword != strConfirmPassword)
                {

                    strMsg = "PASSWORD MISMATCH!!!";
                    ViewBag.Message = strMsg;
                }
                else
                {
                     strMsg = objLoginDAL.ChangePassword(strEmployeeId, strNewPassword, strConfirmPassword, strHeadOfficeId, strBranchOfficeId);

                    if (strMsg == "PASSWORD SUCCESSFULLY CHANGED!!!")
                    {
                        ViewBag.Message = strMsg;
                    }
                    else
                    {
                        ViewBag.Message = strMsg;
                    }


                }

               
            }

            return View("ChangePassword");

        }

        public ActionResult LogOut()
        {

            Session["strEmployeeId"] = null;
            Session["strDesignationId"] = null;
            Session["strUnitId"] = null;
            Session["strSubSectionId"] = null;
            Session["strHeadOfficeId"] = null;
            Session["strBranchOfficeId"] = null;
            Session["strHeadOfficeName"] = null;
            Session["strBranchOfficeName"] = null;
            Session["strSoftId"] = null;
            Session["strEmployeeName"] = null;
            Session["strEmployeePass"] = null;

            return RedirectToAction("Login");
        }

        public ActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewAccount(SignupModel objSignupModel)
        {
          
            string strDBMsg = "";

            if (ModelState.IsValid)
            {
                strDBMsg = objLoginDAL.CreateNewAccount(objSignupModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            ModelState.Clear();
            return RedirectToAction("Signup");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgetPasswordRequest(ForgotPasswordModel objForgotPasswordModel)
        {

            string strDBMsg = "";

            if (ModelState.IsValid)
            {
                strDBMsg = objLoginDAL.ForgetPasswordRequest(objForgotPasswordModel);
                TempData["OperationMessage"] = strDBMsg;
            }

            ModelState.Clear();
            return RedirectToAction("ForgotPassword");
        }
    }
}