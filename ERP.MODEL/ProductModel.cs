using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;

namespace ERP.MODEL
{
    public class ProductModel
    {
        public int Id { get; set; }

        public string[] TranId { get; set; }
        public string[] TranIdSr { get; set; }

        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

        [Display(Name = "Season Name :")]
        [Required(ErrorMessage = " ")]
        public string SeasonName { get; set; }

        [Display(Name = "Season Year :")]
        [Required(ErrorMessage = " ")]
        public string SeasonYear { get; set; }

        [Display(Name = "Style Number :")]
        [Required(ErrorMessage = " ")]
        public string StyleNumber { get; set; }

        [Display(Name = "Style Description :")]
        [Required(ErrorMessage = " ")]
        public string StyleDescription { get; set; }

        [Display(Name = "Sample Size :")]
        [Required(ErrorMessage = " ")]
        public string SampleSize { get; set; }

        [Display(Name = "Sample Fit :")]
        [Required(ErrorMessage = " ")]
        public string SampleFit { get; set; }

        [Display(Name = "Month :")]
        [Required(ErrorMessage = " ")]
        public string Month { get; set; }

        [Display(Name = "Production Quantity :")]
        [Required(ErrorMessage = " ")]
        public string ProductionQuantity { get; set; }

        [Display(Name = "Shop Display Date :")]
        [Required(ErrorMessage = " ")]
        public string ShopDisplayDate { get; set; }

        [Display(Name = "Wash Type :")]
        [Required(ErrorMessage = " ")]
        public string WashType { get; set; }

        [Display(Name = "Occasion :")]
        public string Occasion { get; set; }

        [Display(Name = "Specification Sheet : ")]
        [DataType(DataType.Upload)]
        //[Required(ErrorMessage = "Required")]
        public HttpPostedFileBase Image { get; set; }
        public byte[] ProductImageBytes { get; set; }
        public string Productimage { get; set; }  

        [Display(Name = "Measurment Sheet : ")]
        [DataType(DataType.Upload)]
        //[Required(ErrorMessage = "Required")]
        public byte[] MeasurmentSheetBytes { get; set; }
        public HttpPostedFileBase MeasurmentSheet { get; set; }

        [Display(Name = "Catagory :")]
        [Required(ErrorMessage = " ")]
        public string Catagory { get; set; }

        [Display(Name = "Sub Catagory :")]
        [Required(ErrorMessage = " ")]
        public string SubCatagory { get; set; }

        [Display(Name = "Merchandiser Name :")]
        [Required(ErrorMessage = " ")]
        public string MerchandiserName { get; set; }

        [Required(ErrorMessage = " ")]
        public string[] SizeName { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] SizeValue { get; set; }
        public string[] SizeId { get; set; }

        public string[] ColorWayId { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] ColorWayNumber { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] ColorWayName { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] ColorWayType { get; set; }
        public string[] ColorId { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] ColorName { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] FabricName { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] FabricCode { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] Supplyer { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] Consumption { get; set; }
        public string[] FabricSwatch { get; set; }
        //[Required(ErrorMessage = "Required")]
        public HttpPostedFileBase[] FabricSwatches { get; set; }

        public string ColorWayNumberS { get; set; }
        public string ColorWayNameS { get; set; }
        public string ColorWayTypeS { get; set; }
        public string ColorNameS { get; set; }
        public string FabricNameS { get; set; }
        public string FabricCodeS { get; set; }
        public string SupplyerS { get; set; }
        public string ConsumptionS { get; set; }
        public string FabricSwatchS { get; set; }
        public string TranIdS { get; set; }

        public List<ViewProduct> ViewProducts { get; set; }

        public List<ColorWayDisplay> ColorWayDisplays { get; set; }

        public List<SizeRatioDisplay> SizeRatioDisplays { get; set; }

    }

    public class SizeRatioDisplay
    {
        public string TranId { get; set; }
        public string SizeName { get; set; }
        public string SizeValue { get; set; }
        public string SizeId { get; set; }

        public string StyleNo { get; set; }
        public string SeasonYear { get; set; }
        public string SeasonId { get; set; }

        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }

    public class ColorWayDisplay
    {
        public string TranId { get; set; }
        public string ColorWayNumber { get; set; }
        public string ColorWayName { get; set; }
        public string ColorWayType { get; set; }
        public string ColorName { get; set; }
        public string FabricName { get; set; }
        public string FabricCode { get; set; }
        public string Supplyer { get; set; }
        public string Consumption { get; set; }
        public string FabricSwatch { get; set; }
        public byte[] Photo { get; set; }
        public string PhotoString { get; set; }

        public string StyleNo { get; set; }
        public string SeasonYear { get; set; }
        public string SeasonId { get; set; }

        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

        public byte?[] PhotoCheck { get; set; }
    }

    public class ImageSave
    {
        public string ImageFileName { get; set; }
        public string ImageFileSize { get; set; }
        public string ImageFileExtension { get; set; }
        public byte[] File { get; set; }

        public string StyleNumber { get; set; }
        public string SeasonName { get; set; }
        public string SeasonYear { get; set; }

        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }

    public class MeasurmentSheetSave
    {
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public string FileExtension { get; set; }

        public string StyleNumber { get; set; }
        public string SeasonName { get; set; }
        public string SeasonYear { get; set; }

        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }

    public class ProductSizeRatioSave
    {
        public string StyleNumber { get; set; }
        public string StyleName { get; set; }
        public string TranId { get; set; }
        public string SeasondId { get; set; }
        public string SeasonYear { get; set; }
        public string SizeId { get; set; }
        public string SizeQuantity { get; set; }

        public string CreatedBy { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }

    public class ViewProduct
    {
        public string SeasonId { get; set; }

        public string SeasonName { get; set; }

        public string SeasonYear { get; set; }

        public string StyleNumber { get; set; }

        public string StyleDescription { get; set; }

        public string Month { get; set; }

        public string ProductionQuantity { get; set; }

        public string MerchandiserName { get; set; }

        public string DesignerName { get; set; }


    }

    public class ProductReport
    {
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

        public string SeasonId { get; set; }
        public string SeasonName { get; set; }
        public string SeasonYear { get; set; }
        public string StyleNumber { get; set; }
    }
}
