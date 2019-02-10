using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ERP.DAL;
using ERP.MODEL;
using ERP.Utility;
using PagedList;

namespace ERP.Controllers
{
    public class NoticeBoardController : Controller
    {
        NoticeBoardModel objNoticeBoardModel = new NoticeBoardModel();
        NoticeBoardImageModel objNoticeBoardImageModel = new NoticeBoardImageModel();
        NoticeBoardDAL objNoticeBoardDal = new NoticeBoardDAL();

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





        public void emptyTextBoxValue()
        {
            objNoticeBoardModel.NoticeTitle = "";
            objNoticeBoardModel.NoticeDate = "";
            objNoticeBoardModel.NoticeId = "";

        }



        public ActionResult NoticeFileUpload(string pViewFlag, string pDeleteFlag, string pNoticeId, string pNoticeDate, string pNoticeTypeId, string SearchBy = "")
        {
            //if (Session["strEmployeeId"] == null)
            //{
            //    return RedirectToAction("LogOut", "Login");
            //}
            //else
            //{

            LoadSession();

            objNoticeBoardImageModel.UpdateBy = strEmployeeId;
            objNoticeBoardImageModel.HeadOfficeId = strHeadOfficeId;
            objNoticeBoardImageModel.BranchOfficeId = strBranchOfficeId;

            objNoticeBoardModel.UpdateBy = strEmployeeId;
            objNoticeBoardModel.HeadOfficeId = strHeadOfficeId;
            objNoticeBoardModel.BranchOfficeId = strBranchOfficeId;


            if (!string.IsNullOrWhiteSpace(pNoticeId) && !string.IsNullOrWhiteSpace(pNoticeDate) && !string.IsNullOrWhiteSpace(pNoticeTypeId))
            {
                objNoticeBoardModel.NoticeId = pNoticeId.Trim();
                objNoticeBoardModel.NoticeDate = pNoticeDate.Trim();
                objNoticeBoardModel.NoticeTypeId = pNoticeTypeId.Trim();

                objNoticeBoardModel = objNoticeBoardDal.GetNoticeById(objNoticeBoardModel);
               // objNoticeBoardModel.NoticeTypeId = objNoticeBoardImageModel.NoticeTypeId;
                //objNoticeBoardModel.NoticeTitle = objNoticeBoardImageModel.Noti;
              //  objNoticeBoardModel.NoticeDate = objNoticeBoardImageModel.NoticeDate;

            }


            if (!string.IsNullOrWhiteSpace(pNoticeId) && !string.IsNullOrWhiteSpace(pNoticeDate) && !string.IsNullOrWhiteSpace(pNoticeTypeId))
            {

                objNoticeBoardImageModel.NoticeId = pNoticeId.Trim();
                objNoticeBoardImageModel.NoticeDate = pNoticeDate.Trim();
                objNoticeBoardImageModel.NoticeTypeId = pNoticeTypeId.Trim();



                if (!string.IsNullOrEmpty(pViewFlag) && pViewFlag == "1")
                {


                   
                    objNoticeBoardImageModel = objNoticeBoardDal.ViewPdfFile(objNoticeBoardImageModel);

                    var pdfByte = objNoticeBoardImageModel.bytes;
                    emptyTextBoxValue();
                    return File(pdfByte, "application/pdf");
                   
                }
                //if (!string.IsNullOrEmpty(pDeleteFlag) && pDeleteFlag == "1")
                if (pDeleteFlag == "1")
                {
                    string strDBMsg = "";
                    strDBMsg = objNoticeBoardDal.DeleteNoticeRecord(objNoticeBoardImageModel);
                    emptyTextBoxValue();
                }
            }

           
            DataTable dt = objNoticeBoardDal.NoticeFileUpload(objNoticeBoardModel);
            List<NoticeBoardModel> NoticeFileUploadList = NoticeBoardFileUpload(dt);

            ViewBag.NoticeFileUploadList = NoticeFileUploadList;
            ViewBag.GetNoticeTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetNoticeTypeDDList(), "NOTICE_TYPE_ID", "NOTICE_TYPE_NAME");

          
            return View(objNoticeBoardModel);
            // }
        }



        public List<NoticeBoardModel> NoticeBoardFileUpload(DataTable dt1)
        {
            List<NoticeBoardModel> NoticeFileUploadDataBundle = new List<NoticeBoardModel>();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                NoticeBoardModel objNoticeBoardModel = new NoticeBoardModel();
                objNoticeBoardModel.NoticeBoardImageModel = new NoticeBoardImageModel();
                objNoticeBoardModel.SerialNumber = dt1.Rows[i]["sl"].ToString();
                objNoticeBoardModel.NoticeId = dt1.Rows[i]["NOTICE_ID"].ToString();
                objNoticeBoardModel.NoticeTitle = dt1.Rows[i]["NOTICE_TITLE"].ToString();
                objNoticeBoardModel.NoticeTypeId = dt1.Rows[i]["NOTICE_TYPE_ID"].ToString();
                objNoticeBoardModel.NoticeTypeName = dt1.Rows[i]["NOTICE_TYPE_NAME"].ToString();

                objNoticeBoardModel.NoticeDate = dt1.Rows[i]["NOTICE_DATE"].ToString();
                objNoticeBoardModel.NoticeBoardImageModel.FileName = dt1.Rows[i]["FILE_NAME"].ToString();

                NoticeFileUploadDataBundle.Add(objNoticeBoardModel);
            }

            return NoticeFileUploadDataBundle;
        }

    

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaveNoticeFileUpload(HttpPostedFileBase files, NoticeBoardModel objNoticeBoardModel)
        {
            LoadSession();
            string strDBMsg = "";
            NoticeBoardImageModel objNoticeBoardImageModel = new NoticeBoardImageModel();

            objNoticeBoardModel.UpdateBy = strEmployeeId;
            objNoticeBoardModel.HeadOfficeId = strHeadOfficeId;
            objNoticeBoardModel.BranchOfficeId = strBranchOfficeId;

            objNoticeBoardImageModel.UpdateBy = strEmployeeId;
            objNoticeBoardImageModel.HeadOfficeId = strHeadOfficeId;
            objNoticeBoardImageModel.BranchOfficeId = strBranchOfficeId;

            if (files!=null)
            {
                String FileExt = Path.GetExtension(files.FileName).ToUpper();

                if (FileExt == ".PDF")
                {
                    Stream str = files.InputStream;
                    BinaryReader Br = new BinaryReader(str);
                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                    objNoticeBoardImageModel.FileName = files.FileName;
                    //objTechpackFileUploadModel.FileContent = FileDet;
                    string fileSize = Convert.ToBase64String(FileDet);
                    objNoticeBoardImageModel.FileSize = fileSize;
                    objNoticeBoardImageModel.FileExtension = FileExt;
                    objNoticeBoardDal.SaveNotice(objNoticeBoardModel);

                    objNoticeBoardImageModel.NoticeTypeId = objNoticeBoardModel.NoticeTypeId;
                    objNoticeBoardImageModel.NoticeDate = objNoticeBoardModel.NoticeDate;

                    objNoticeBoardDal.SaveNoticeFile(objNoticeBoardImageModel);
                    //strDBMsg = objNoticeBoardDal.SaveNoticeFile(objNoticeBoardModel);
                    //TempData["OperationMessage"] = strDBMsg;
                    //emptyTextBoxValue();
                }

                else
                {
                    ViewBag.FileStatus = "Invalid file format.";
                    return View();
                }
            }
           
            else
            {
                if (!string.IsNullOrWhiteSpace(objNoticeBoardModel.NoticeId) &&
                    !string.IsNullOrWhiteSpace(objNoticeBoardModel.NoticeTypeId) &&
                    !string.IsNullOrWhiteSpace(objNoticeBoardModel.NoticeDate))
                {

                    objNoticeBoardImageModel.NoticeId = objNoticeBoardModel.NoticeId;
                    objNoticeBoardImageModel.NoticeDate = objNoticeBoardModel.NoticeDate;
                    objNoticeBoardImageModel.NoticeTypeId = objNoticeBoardModel.NoticeTypeId;
                    objNoticeBoardImageModel.FileName = objNoticeBoardModel.NoticeBoardImageModel.FileName;
                    objNoticeBoardImageModel.FileSize =
                        Convert.ToBase64String(objNoticeBoardModel.NoticeBoardImageModel.fileBytes);
                    objNoticeBoardImageModel.FileExtension = objNoticeBoardModel.NoticeBoardImageModel.FileExtension;
                    objNoticeBoardImageModel.UpdateBy = objNoticeBoardModel.UpdateBy;
                    objNoticeBoardImageModel.HeadOfficeId = objNoticeBoardModel.HeadOfficeId;
                    objNoticeBoardImageModel.BranchOfficeId = objNoticeBoardModel.BranchOfficeId;

                    objNoticeBoardDal.SaveNotice(objNoticeBoardModel);
                    objNoticeBoardDal.SaveNoticeFile(objNoticeBoardImageModel);

                }

             

             
            }

            return RedirectToAction("NoticeFileUpload");
        }




        [HttpGet]
        public ActionResult SearchNoticeEntry(NoticeBoardModel objNoticeBoardModel)
        {
            LoadSession();

            objNoticeBoardModel.UpdateBy = strEmployeeId;
            objNoticeBoardModel.HeadOfficeId = strHeadOfficeId;
            objNoticeBoardModel.BranchOfficeId = strBranchOfficeId;

            DataTable dt = objNoticeBoardDal.NoticeFileUpload(objNoticeBoardModel);
            ViewBag.NoticeFileUploadList = NoticeBoardFileUpload(dt);
            ViewBag.GetNoticeTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetNoticeTypeDDList(), "NOTICE_TYPE_ID", "NOTICE_TYPE_NAME");


           
            return View("NoticeFileUpload", objNoticeBoardModel);
        }



        //Notice for all 

        [HttpGet]
        public ActionResult NoticeFileForAll(NoticeBoardModel objNoticeBoardModel)
        {
            LoadSession();

            objNoticeBoardModel.UpdateBy = strEmployeeId;
            objNoticeBoardModel.HeadOfficeId = strHeadOfficeId;
            objNoticeBoardModel.BranchOfficeId = strBranchOfficeId;

            DataTable dt = objNoticeBoardDal.NoticeFileForAll(objNoticeBoardModel);
            ViewBag.NoticeFileUploadList = NoticeBoardFileUpload(dt);
            ViewBag.GetNoticeTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetNoticeTypeDDList(), "NOTICE_TYPE_ID", "NOTICE_TYPE_NAME");



            return View("NoticeFileForAll", objNoticeBoardModel);
            //return View("NoticeFileForAll");
        }


        //[HttpPost]
        //public ActionResult ViewPDFinPage()
        //{
        //    string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"500px\" height=\"300px\">";
        //    embed += "If you are unable to view file, you can download from <a href = \"{0}\">here</a>";
        //    embed += " or download <a target = \"_blank\" href = \"http://get.adobe.com/reader/\">Adobe PDF Reader</a> to view the file.";
        //    embed += "</object>";
        //    TempData["Embed"] = string.Format(embed, VirtualPathUtility.ToAbsolute("D:\\Projects\\Projects\\15 Dec 18 Report\\15-12-2018\\ERP\\ERP\\Views\\NoticeBoard\\yyy.pdf"));

        //    return RedirectToAction("NoticeFileForAll");
        //}

        public ActionResult DisplayPDF(NoticeBoardModel objNoticeBoardModel)
        {

            LoadSession();

            objNoticeBoardModel.UpdateBy = strEmployeeId;
            objNoticeBoardModel.HeadOfficeId = strHeadOfficeId;
            objNoticeBoardModel.BranchOfficeId = strBranchOfficeId;

            objNoticeBoardModel = objNoticeBoardDal.GetLatestReport(objNoticeBoardModel);

            //Response.AddHeader("Content-Disposition", "inline; filename=Master-Agreement.pdf");
            return File(objNoticeBoardModel.bytes, "application/pdf");
        }


        public ActionResult ClearNoticeFileUploadEntry()
        {
            ModelState.Clear();
            TempData["SearchValue"] = "";
            return RedirectToAction("NoticeFileUpload");
        }
    }
}