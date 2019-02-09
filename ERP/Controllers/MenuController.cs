using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ERP.MODEL;
using ERP.DAL;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class MenuController : Controller
    {
        MenuDAL objMenuDAL = new MenuDAL();

        [ChildActionOnly]
        public ActionResult HeaderMenu()
        {
            MenuModel objMenuModel = new MenuModel();
            objMenuModel.BranchOfficeId = Session["strBranchOfficeId"].ToString().Trim();
            objMenuModel.HeadOfficeId = Session["strHeadOfficeId"].ToString().Trim();
            objMenuModel.EmployeeId = Session["strEmployeeId"].ToString().Trim();
            objMenuModel.SoftwareId = Session["strSoftId"].ToString().Trim();

            ViewBag.MainMenuList = objMenuDAL.GetHeaderParentMenuList(objMenuModel);
            ViewBag.SubMenuList = objMenuDAL.GetHeaderChildMenuList(objMenuModel);
            ViewBag.EmployeeImage = objMenuDAL.GetEmployeePhoto(objMenuModel);
            return PartialView("_Header");
        }

        [ChildActionOnly]
        public ActionResult LeftMenu()
        {
            MenuModel objMenuModel = new MenuModel();
            objMenuModel.BranchOfficeId = Session["strBranchOfficeId"].ToString().Trim();
            objMenuModel.HeadOfficeId = Session["strHeadOfficeId"].ToString().Trim();
            objMenuModel.EmployeeId = Session["strEmployeeId"].ToString().Trim();
            objMenuModel.SoftwareId = Session["strSoftId"].ToString().Trim();
          
            ViewBag.MainMenuList = objMenuDAL.GetLeftParentMenuList(objMenuModel);
            ViewBag.SubMenuList = objMenuDAL.GetLeftChildMenuList(objMenuModel);
   

            return PartialView("_Nav");
        }
    }
}