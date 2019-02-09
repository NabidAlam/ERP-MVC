using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using ERP.DAL;
using ERP.MODEL;
using ERP.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ERP.Controllers
{
    public class ProductDetailsController : Controller
    {
        LookUpDAL objLookUpDAL = new LookUpDAL();
        //BOMModel objBOMModel = new BOMModel();

        ReportDocument rd = new ReportDocument();
        ExportFormatType formatType = ExportFormatType.NoFormat;

        #region "Common"

        string strEmployeeId = "";
        string strDesignationId = "";
        string strSubSectionId = "";
        string strUnitId = "";
        string strHeadOfficeId = "";
        string strBranchOfficeId = "";
        string strSoftId = "";

        public void LoadSession()
        {
            strEmployeeId = Session["strEmployeeId"].ToString();
            strSubSectionId = Session["strSubSectionId"].ToString();
            strDesignationId = Session["strDesignationId"].ToString();
            strUnitId = Session["strUnitId"].ToString();
            strHeadOfficeId = Session["strHeadOfficeId"].ToString();
            strBranchOfficeId = Session["strBranchOfficeId"].ToString();
            strSoftId = Session["strSoftId"].ToString();
        }
        #endregion

        // GET: ProductDetails
        public ActionResult Index(string seasonId, string seasonYear, string styleNumber)
        {
            ProductDAL objProductDal = new ProductDAL();

            ProductModel model = new ProductModel
            {
                ViewProducts = ProductListData(objProductDal.GetProductMainList())

            };
            model.ColorWayDisplays = new List<ColorWayDisplay>();
            model.SizeRatioDisplays = new List<SizeRatioDisplay>();

            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }

            LoadSession();

            //objBOMModel.UpdateBy = strEmployeeId;
            //objBOMModel.HeadOfficeId = strHeadOfficeId;
            //objBOMModel.BranchOfficeId = strBranchOfficeId;

            ViewBag.RequiredMessage = TempData["ImageNulMessage"] as string;

            if (string.IsNullOrWhiteSpace(ViewBag.RequiredMessage))
            {
                ViewBag.RequiredMessage = TempData["MSNulMessage"] as string;
            }
            if(string.IsNullOrWhiteSpace(ViewBag.RequiredMessage))
            {
                ViewBag.RequiredMessage = TempData["FSMessage"] as string;
            }
            if(string.IsNullOrWhiteSpace(ViewBag.RequiredMessage))
            {
                ViewBag.RequiredMessage = TempData["RequiredMessage"] as string;
            }
            if(string.IsNullOrWhiteSpace(ViewBag.RequiredMessage))
            {
                ViewBag.InformationMessage = TempData["InformationMessage"] as string;
            } 
            
            ViewBag.SeasonDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSeasonDDList(), "SEASON_ID", "SEASON_NAME");
            ViewBag.SizeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSizeDDList(), "SIZE_ID", "SIZE_NAME");
            ViewBag.FitDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetFitDDList(), "FIT_ID", "FIT_NAME");
            ViewBag.MonthDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMonthDDList(), "MONTH_ID", "MONTH_NAME");
            ViewBag.ColorWayNumberDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetColorWayNumberDDList(), "COLOR_WAY_NO_ID", "COLOR_WAY_NO_NAME");
            ViewBag.ColorWayTypeDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetColorWayTypeDDList(), "COLOR_WAY_TYPE_ID", "COLOR_WAY_TYPE_NAME");
            ViewBag.MerchandiserNameDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetMerchandiserNameDDList(), "EMPLOYEE_ID", "EMPLOYEE_NAME");
            ViewBag.ColorDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetColorDDList(), "COLOR_ID", "COLOR_NAME");
            ViewBag.FabricDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetFabricDDList(), "FABRIC_TYPE_ID", "FABRIC_TYPE_NAME");
            ViewBag.WashIdDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetWashTypeDDList(), "WASH_ID", "WASH_NAME");
            ViewBag.CategoryDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetCategoryDDList(), "CATAGORY_ID", "CATAGORY_NAME");
            ViewBag.SupplirDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSupplirDDList(), "SUPPLIER_ID", "SUPPLIER_NAME");
            ViewBag.OccasionDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetOccasionDDList(), "OCCASION_ID", "OCCASION_NAME");

            if (seasonId != null && seasonYear != null && styleNumber != null)
            {
                if (string.IsNullOrWhiteSpace(ViewBag.RequiredMessage))
                {
                    ViewBag.InformationMessage = null;
                }

                model.SeasonName = seasonId;
                model.SeasonYear = seasonYear;
                model.StyleNumber = styleNumber;

                model = objProductDal.GetProductMainData(model);

                model.ColorWayDisplays = ProductSubListData(objProductDal.GetProductSubList(model));

                ViewBag.SubCategoryDDList = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubCategoryDDList(Convert.ToInt32(model.Catagory)), "SUB_CATEGORY_ID", "SUB_CATEGORY_NAME");

                model.SizeRatioDisplays = SizeRatioListData(objProductDal.GetSizeRatioList(model));

                //model.Productimage = "data:image/png;base64," + Convert.ToBase64String(model.ProductImageBytes);
            }
            else
            {
                model.SeasonYear = DateTime.Now.Year.ToString();
            }
            

            return View(model);
        }
        public JsonResult GetSubCatagory(int categoryId)
        {
            var list = UtilityClass.GetSelectListByDataTable(objLookUpDAL.GetSubCategoryDDList(categoryId), "SUB_CATEGORY_ID", "SUB_CATEGORY_NAME");
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveProductDetails(HttpPostedFileBase image, HttpPostedFileBase measurmentSheet, HttpPostedFileBase[] fabricSwatches, ProductModel objProductModel)
        {
            if (Session["strEmployeeId"] == null)
            {
                return RedirectToAction("LogOut", "Login");
            }

            LoadSession();

            objProductModel.UpdateBy = strEmployeeId;
            objProductModel.HeadOfficeId = strHeadOfficeId;
            objProductModel.BranchOfficeId = strBranchOfficeId;

            if (ModelState.IsValid)
            {
                for (int i = 0; i < objProductModel.ColorWayId.Count(); i++)
                {
                    if (objProductModel.ColorWayNumber[i] == "" && objProductModel.ColorWayType[i] == "" &&
                        objProductModel.ColorName[i] == "" && objProductModel.FabricName[i] == "" &&
                        objProductModel.FabricCode[i] == "" && objProductModel.Consumption[i] == "")
                    {
                        TempData["ValidationError"] = objProductModel as ProductModel;
                        return RedirectToAction("Index", new { seasonId = objProductModel.SeasonName, seasonYear = objProductModel.SeasonYear, styleNumber = objProductModel.StyleNumber });
                    }
                }
                for (int i = 0; i < objProductModel.SizeId.Count(); i++)
                {
                    if (objProductModel.SizeName[i] == "" && objProductModel.SizeValue[i] == "")
                    {
                        TempData["ValidationError"] = objProductModel as ProductModel;
                        return RedirectToAction("Index", new { seasonId = objProductModel.SeasonName, seasonYear = objProductModel.SeasonYear, styleNumber = objProductModel.StyleNumber });
                    }
                }

                ProductDAL objProductDal = new ProductDAL();
                ImageSave objImageSave = new ImageSave();
                MeasurmentSheetSave objMeasurmentSheetSave = new MeasurmentSheetSave();

                if (image != null)
                {
                    var extension = Path.GetExtension(image.FileName);

                    if (extension != null)
                    {
                        String imageExtension = extension.ToUpper();
                        if (imageExtension == ".PDF")
                        {
                            Stream str = image.InputStream;
                            BinaryReader Br = new BinaryReader(str);
                            Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                            string imageSize = Convert.ToBase64String(FileDet);

                            objImageSave.ImageFileSize = imageSize;
                            objImageSave.ImageFileName = image.FileName;
                            objImageSave.ImageFileExtension = imageExtension;
                            objImageSave.StyleNumber = objProductModel.StyleNumber;
                            objImageSave.SeasonName = objProductModel.SeasonName;
                            objImageSave.SeasonYear = objProductModel.SeasonYear;
                            objImageSave.UpdateBy = strEmployeeId;
                            objImageSave.HeadOfficeId = strHeadOfficeId;
                            objImageSave.BranchOfficeId = strBranchOfficeId;
                        }
                        else
                        {
                            TempData["ImageNulMessage"] = "Specification Sheet can not be null";
                            return RedirectToAction("Index", new { seasonId = objProductModel.SeasonName, seasonYear = objProductModel.SeasonYear, styleNumber = objProductModel.StyleNumber });
                        }
                    }
                }
                else
                {
                    if (objProductModel.TranId != null)
                    {

                        objImageSave.ImageFileSize = Convert.ToBase64String(objProductModel.ProductImageBytes);
                        objImageSave.StyleNumber = objProductModel.StyleNumber;
                        objImageSave.SeasonName = objProductModel.SeasonName;
                        objImageSave.SeasonYear = objProductModel.SeasonYear;
                        objImageSave.UpdateBy = strEmployeeId;
                        objImageSave.HeadOfficeId = strHeadOfficeId;
                        objImageSave.BranchOfficeId = strBranchOfficeId;
                    }
                    else
                    {
                        TempData["ImageNulMessage"] = "Specification Sheet can not be null";
                        return RedirectToAction("Index", new { seasonId = objProductModel.SeasonName, seasonYear = objProductModel.SeasonYear, styleNumber = objProductModel.StyleNumber });
                    }
                }

                if (measurmentSheet != null)
                {
                    var extension = Path.GetExtension(measurmentSheet.FileName);

                    if (extension != null)
                    {
                        String fileExtension = extension.ToUpper();
                        if (fileExtension == ".XLSX" || fileExtension == ".XLS" || fileExtension == ".XLSB")
                        {
                            Stream str = measurmentSheet.InputStream;
                            BinaryReader Br = new BinaryReader(str);
                            Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                            string fileSize = Convert.ToBase64String(FileDet);

                            objMeasurmentSheetSave.FileSize = fileSize;
                            objMeasurmentSheetSave.FileName = measurmentSheet.FileName;
                            objMeasurmentSheetSave.FileExtension = extension;
                            objMeasurmentSheetSave.StyleNumber = objProductModel.StyleNumber;
                            objMeasurmentSheetSave.SeasonName = objProductModel.SeasonName;
                            objMeasurmentSheetSave.SeasonYear = objProductModel.SeasonYear;
                        }
                    }
                }
                else
                {
                    if (objProductModel.TranId != null)
                    {
                        objMeasurmentSheetSave.FileSize = Convert.ToBase64String(objProductModel.MeasurmentSheetBytes);
                        objMeasurmentSheetSave.StyleNumber = objProductModel.StyleNumber;
                        objMeasurmentSheetSave.SeasonName = objProductModel.SeasonName;
                        objMeasurmentSheetSave.SeasonYear = objProductModel.SeasonYear;
                    }
                    else
                    {
                        TempData["MSNulMessage"] = "Measurment Sheet can not be null";
                        return RedirectToAction("Index", new { seasonId = objProductModel.SeasonName, seasonYear = objProductModel.SeasonYear, styleNumber = objProductModel.StyleNumber });
                    }
                }

                for (int i = 0; i < objProductModel.ColorWayId.Count(); i++)
                {
                    if (objProductModel.ColorWayNumber[i] != "" && objProductModel.ColorWayType[i] != "" &&
                        objProductModel.ColorName[i] != "" && objProductModel.FabricName[i] != "" &&
                        objProductModel.FabricCode[i] != "" && objProductModel.Consumption[i] != "")
                    {
                        if (fabricSwatches[i] != null)
                        {
                            var extension = Path.GetExtension(fabricSwatches[i].FileName);
                            if (extension != null)
                            {
                                String ImageExtension = extension.ToUpper();
                                if (ImageExtension == ".JPG" || ImageExtension == ".JPEG" || ImageExtension == ".PNG")
                                {
                                    Stream str = fabricSwatches[i].InputStream;
                                    BinaryReader Br = new BinaryReader(str);
                                    Byte[] FileDet = Br.ReadBytes((Int32)str.Length);
                                    string imageSize = Convert.ToBase64String(FileDet);

                                    objProductModel.FabricSwatchS = imageSize;
                                }
                            }
                        }
                        else
                        {
                            if (objProductModel.FabricSwatch != null)
                            {
                                if(objProductModel.FabricSwatch[i].Length < 1)
                                    objProductModel.FabricSwatchS = objProductModel.FabricSwatch[i];
                            } 
                            else
                                objProductModel.FabricSwatchS = null;
                        }

                        objProductModel.FabricNameS = objProductModel.FabricName[i];
                        objProductModel.ColorNameS = objProductModel.ColorName[i];
                        objProductModel.ColorWayNumberS = objProductModel.ColorWayNumber[i];
                        objProductModel.ColorWayTypeS = objProductModel.ColorWayType[i];
                        objProductModel.ConsumptionS = objProductModel.Consumption[i];
                        objProductModel.FabricCodeS = objProductModel.FabricCode[i];
                        objProductModel.SupplyerS = objProductModel.Supplyer[i];
                        objProductModel.TranIdS = objProductModel.TranId[i];
                        objProductModel.ColorWayNameS = objProductModel.ColorWayName[i];
                    }
                    else
                    {
                        TempData["RequiredMessage"] = "All fild Required";
                        return RedirectToAction("Index");
                    }

                    string strDbMsg = objProductDal.SaveProductInformation(objProductModel);

                    TempData["InformationMessage"] = strDbMsg;
                }

                for (int i = 0; i < objProductModel.SizeId.Count(); i++)
                {
                    ProductSizeRatioSave objProductSizeRatioSave = new ProductSizeRatioSave();
                    if (objProductModel.SizeName[i] != "" && objProductModel.SizeValue[i] != "")
                    {
                        objProductSizeRatioSave.StyleNumber = objProductModel.StyleNumber;
                        objProductSizeRatioSave.StyleName = objProductModel.StyleDescription;
                        objProductSizeRatioSave.TranId = objProductModel.TranIdSr[i];
                        objProductSizeRatioSave.SeasondId = objProductModel.SeasonName;
                        objProductSizeRatioSave.SeasonYear = objProductModel.SeasonYear;
                        objProductSizeRatioSave.SizeId = objProductModel.SizeName[i];
                        objProductSizeRatioSave.SizeQuantity = objProductModel.SizeValue[i];

                        objProductSizeRatioSave.CreatedBy = strEmployeeId;
                        objProductSizeRatioSave.UpdateBy = strEmployeeId;
                        objProductSizeRatioSave.HeadOfficeId = strHeadOfficeId;
                        objProductSizeRatioSave.BranchOfficeId = strBranchOfficeId;
                    }
                    else
                    {
                        TempData["RequiredMessage"] = "All fild Required";
                        return RedirectToAction("Index");
                    }

                    string saveSizRatioMessage = objProductDal.SaveProductSizeRatio(objProductSizeRatioSave);
                    TempData["SizeRatioMessage"] = saveSizRatioMessage;
                }

                string saveImgMassege = objProductDal.SaveImageInformation(objImageSave);
                TempData["ImageMessage"] = saveImgMassege;

                objMeasurmentSheetSave.UpdateBy = strEmployeeId;
                objMeasurmentSheetSave.HeadOfficeId = strHeadOfficeId;
                objMeasurmentSheetSave.BranchOfficeId = strBranchOfficeId;
                string saveMeasurmentSheet = objProductDal.SaveMeasurmentSheetInformation(objMeasurmentSheetSave);
                TempData["MeasurmentSheetMessage"] = saveMeasurmentSheet;
            }

            return RedirectToAction("Index");
        }

        public JsonResult DeleteSizeRatio(string[] TranId, string[] StyleNo, string[] SeasonYear, string[] SeasonId)
        {
            ProductDAL objProductDal = new ProductDAL();
            string strMessage = null;

            LoadSession();
            for (int i = 0; i < TranId.Length; i++)
            {
                var sizeRatio = new SizeRatioDisplay
                {
                    TranId = TranId[i],
                    StyleNo = StyleNo[i],
                    SeasonYear = SeasonYear[i],
                    SeasonId = SeasonId[i],
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };
                strMessage = objProductDal.DeleteSizeRatio(sizeRatio);
            }

            return Json(strMessage, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteColorWay(string[] TranId, string[] StyleNo, string[] SeasonYear, string[] SeasonId)
        {
            ProductDAL objProductDal = new ProductDAL();
            string strMessage = null;

            LoadSession();
            for (int i = 0; i < TranId.Length; i++)
            {
                var colorWay = new ColorWayDisplay
                {
                    TranId = TranId[i],
                    StyleNo = StyleNo[i],
                    SeasonYear = SeasonYear[i],
                    SeasonId = SeasonId[i],
                    HeadOfficeId = strHeadOfficeId,
                    BranchOfficeId = strBranchOfficeId
                };
                strMessage = objProductDal.DeleteColorWay(colorWay);
            }

            return Json(strMessage, JsonRequestBehavior.AllowGet);
        }

        public List<ViewProduct> ProductListData(DataTable dt)
        {
            List<ViewProduct> viewProductList = new List<ViewProduct>();

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ViewProduct objViewProductl = new ViewProduct();
                objViewProductl.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                objViewProductl.SeasonName = dt.Rows[i]["SEASON_NAME"].ToString();
                objViewProductl.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objViewProductl.StyleNumber = dt.Rows[i]["STYLE_NO"].ToString();
                objViewProductl.StyleDescription = dt.Rows[i]["STYLE_NAME"].ToString();
                objViewProductl.Month = dt.Rows[i]["MONTH_ID"].ToString();
                objViewProductl.ProductionQuantity = dt.Rows[i]["PRODUCTION_QUANTITY"].ToString();
                objViewProductl.MerchandiserName = dt.Rows[i]["MERCHANDISER_NAME"].ToString();
                objViewProductl.DesignerName = dt.Rows[i]["DESIGNER_NAME"].ToString();

                viewProductList.Add(objViewProductl);
            }

            return viewProductList;
        }

        public List<ColorWayDisplay> ProductSubListData(DataTable dt)
        {
            List<ColorWayDisplay> viewProductSubList = new List<ColorWayDisplay>();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                ColorWayDisplay objViewProductl = new ColorWayDisplay();
                objViewProductl.TranId = dt.Rows[i]["TRAN_ID"].ToString();
                objViewProductl.ColorWayNumber = dt.Rows[i]["COLOR_WAY_NO_ID"].ToString();
                objViewProductl.ColorWayType = dt.Rows[i]["COLOR_WAY_TYPE_ID"].ToString();
                objViewProductl.ColorName = dt.Rows[i]["COLOR_ID"].ToString();
                objViewProductl.FabricName = dt.Rows[i]["FABRIC_TYPE_ID"].ToString();
                objViewProductl.FabricCode = dt.Rows[i]["FABRIC_CODE"].ToString();
                objViewProductl.Supplyer = dt.Rows[i]["SUPPLIER_ID"].ToString();
                objViewProductl.Consumption = dt.Rows[i]["FABRIC_CONSUMPTION"].ToString();
                objViewProductl.ColorWayName = dt.Rows[i]["COLOR_WAY_NAME"].ToString();

                if(dt.Rows[i]["SWATCH_PIC"] != null && !string.IsNullOrWhiteSpace(dt.Rows[i]["SWATCH_PIC"].ToString()))
                    objViewProductl.Photo = (byte[]) dt.Rows[i]["SWATCH_PIC"];
                else
                    objViewProductl.Photo = null;

                objViewProductl.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objViewProductl.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();
                objViewProductl.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();

                if(objViewProductl.Photo != null)
                    objViewProductl.PhotoString = Convert.ToBase64String(objViewProductl.Photo);
                if (objViewProductl.Photo != null)
                    objViewProductl.FabricSwatch = "data:image/png;base64," + Convert.ToBase64String(objViewProductl.Photo);


                viewProductSubList.Add(objViewProductl);
            }

            return viewProductSubList;
        }

        public List<SizeRatioDisplay> SizeRatioListData(DataTable dt)
        {
            List<SizeRatioDisplay> viewSizeRatioDisplays = new List<SizeRatioDisplay>();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SizeRatioDisplay objSizeRatioDisplay = new SizeRatioDisplay();
                objSizeRatioDisplay.TranId = dt.Rows[i]["TRAN_ID"].ToString();
                objSizeRatioDisplay.SizeName = dt.Rows[i]["SIZE_ID"].ToString();
                objSizeRatioDisplay.SizeValue = dt.Rows[i]["SIZE_QUANTITY"].ToString();
                objSizeRatioDisplay.StyleNo = dt.Rows[i]["STYLE_NO"].ToString();
                objSizeRatioDisplay.SeasonId = dt.Rows[i]["SEASON_ID"].ToString();
                objSizeRatioDisplay.SeasonYear = dt.Rows[i]["SEASON_YEAR"].ToString();

                viewSizeRatioDisplays.Add(objSizeRatioDisplay);
            }

            return viewSizeRatioDisplays;
        }

        public ActionResult ProductDetailsReport(string seasonId, string seasonYear, string styleNumber)
        {
            LoadSession();

            ProductReport objProductModel = new ProductReport();
            ProductDAL objProductDal = new ProductDAL();
            ReportDAL objReportDal = new ReportDAL();

            objProductModel.UpdateBy = strEmployeeId;
            objProductModel.HeadOfficeId = strHeadOfficeId;
            objProductModel.BranchOfficeId = strBranchOfficeId;

            if (seasonId != null && seasonYear != null && styleNumber != null)
            {
                objProductModel.SeasonId = seasonId;
                objProductModel.SeasonYear = seasonYear;
                objProductModel.StyleNumber = styleNumber;
            }

            string strPath = Path.Combine(Server.MapPath("~/Reports/rptProductDetails.rpt"));
            rd.Load(strPath);

            DataSet ds = new DataSet();

            ds = (objReportDal.rdoProductDetailSheet(objProductModel));
            rd.SetDataSource(ds);

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            Response.Clear();
            Response.Buffer = true;

            formatType = ExportFormatType.PortableDocFormat;
            System.IO.Stream oStream = null;
            byte[] byteArray = null;

            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();

            oStream = rd.ExportToStream(formatType);
            byteArray = new byte[oStream.Length];
            oStream.Read(byteArray, 0, Convert.ToInt32(oStream.Length - 1));
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.BinaryWrite(byteArray);
            Response.Flush();
            Response.Close();
            rd.Close();
            rd.Dispose();

            return File(oStream, "application/pdf", "InventoryReport.pdf");
        }

        public ActionResult DisplaySpecificationSheet(string seasonId, string seasonYear, string styleNumber)
        {
            LoadSession();

            ImageSave objImageSave = new ImageSave();
            ProductDAL objProductDal = new ProductDAL();

            objImageSave.UpdateBy = strEmployeeId;
            objImageSave.HeadOfficeId = strHeadOfficeId;
            objImageSave.BranchOfficeId = strBranchOfficeId;

            if (seasonId != null && seasonYear != null && styleNumber != null)
            {
                objImageSave.SeasonName = seasonId;
                objImageSave.SeasonYear = seasonYear;
                objImageSave.StyleNumber = styleNumber;

                objImageSave = objProductDal.DisplaySpecificationSheet(objImageSave);

                byte[] data = objImageSave.File;

                return File(data, "application/pdf");
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}