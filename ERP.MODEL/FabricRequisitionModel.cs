using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ERP.MODEL
{
    public class FabricRequisitionModel
    {
        public string FabricRequisitionId { get; set; }
        public string FabricRequisitionCode { get; set; }

        [Display(Name = "Fabric Code")]
        [Required(ErrorMessage = "Please provide fabric code")]
        public string FabricCode { get; set; }

        [Display(Name = "Requisition Date")]
        public string RequisitionDate { get; set; }

        [Display(Name = "Require Date")]
        [Required(ErrorMessage = "Please select a date")]
        public string RequireDate { get; set; }

        [Display(Name = "Fabric Type")]
        [Required(ErrorMessage = "Please select a fabric type")]
        public string FabricTypeId { get; set; }
        public string FabricTypeName { get; set; }

        [Display(Name = "Fabric Unit")]
        [Required(ErrorMessage = "Please select a fabric unit")]
        public string FabricUnitId { get; set; }
        public string FabricUnitName { get; set; }

        [Display(Name = "Width")]
        [Required(ErrorMessage = "Please provide width")]
        public string FabricWidth { get; set; }

        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "Please provide quantity")]
        public string RequisitionQuantity { get; set; }

        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "Please provide unit price")]
        public string UnitPrice { get; set; }

        [Display(Name = "Fabric Description")]
        [Required(ErrorMessage = "Please provide fabric description")]
        public string FabricDescription { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please select a category")]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Please select a supplier")]
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }

        [Display(Name = "Status")]
        public string ApprovalStatus { get; set; }

        [Display(Name = "Remarks")]
        public string Remarks { get; set; }

        [Display(Name = "Swatch Image")]
        public string SwatchImage { get; set; }

        [Display(Name = "Swatch Image")]
        [DataType(DataType.Upload)]
        //[RegularExpression(@"([a-zA-Z0-9\s_\\.\-\(\):])+(.jpg|.jpeg|.png|.bmp)$", ErrorMessage = "Allowed image files jpg, jpeg, png, bmp")]
        //[FileExtensions(Extensions = "jpg,jpeg,png,bmp", ErrorMessage = "Allowed image files jpg, jpeg, png, bmp")]
        public HttpPostedFileBase HttpPostedFileBase { get; set; }
        public byte[] SwatchFileSize { get; set; }
        public string SwatchFileName { get; set; }
        public string SwatchFileExtension { get; set; }

        public string PurchaseDateStatus { get; set; }



        public string SearchBy { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }
}
