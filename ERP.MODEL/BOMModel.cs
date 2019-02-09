using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ERP.MODEL
{
    public class BOMModel
    {

        public string StyleSearch { get; set; }

        [Display(Name = "Style No :")]
        [Required(ErrorMessage = " ")]
        public string StyleNo { get; set; }
        [Required(ErrorMessage = " ")]
        public string BuyerId { get; set; }
  
        [Display(Name = "Buyer Name :")]
        public string BuyerName { get; set; }

        [Display(Name = "Season Year :")]
        [Required(ErrorMessage = " ")]
        public string SeasonYear { get; set; }
        [Required(ErrorMessage = " ")]
        public string SeasonId { get; set; }

        [Display(Name = "Season Name :")]
        public string SeasonName { get; set; }

        [Display(Name = "R3 Code :")]
        [Required(ErrorMessage = " ")]
        public string R3Code { get; set; }


        [Display(Name = "Last Update Date :")]
        [Required(ErrorMessage = " ")]
        public string LastUpdateDate { get; set; }
        [Required(ErrorMessage = " ")]
        public string ItemTypeId { get; set; }

        [Display(Name = "Item Type Name :")]
        public string ItemTypeName { get; set; }
        public string GridItemName { get; set; }
        public string GridItemDescription { get; set; }
        public string GridModelCode { get; set; }
        public string GridComponentName { get; set; }
        public string GridColorId { get; set; }
        public string GridColorName { get; set; }
        public string GridFabricQuantity { get; set; }
        public string GridUnitId { get; set; }
        public string GridUnitName { get; set; }
        public string GridRemarks { get; set; }
        public string GridTranId { get; set; }
        public string[] ItemName { get; set; }
        public string[] ItemDescription { get; set; }
      
        public string[] ModelCode { get; set; }

        public string[] ComponentName { get; set; }
 
        public string[] ColorId { get; set; }
        public string[] ColorName { get; set; }

        public int RowNumber { get; set; }
      
        public string[] FabricQuantity { get; set; }

        public string[] UnitId { get; set; }
        public string UnitName { get; set; }
        public string[] Remarks { get; set; }
        public string[] TranId { get; set; } 
        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }


        //file upload
        public string CVSize { get; set; }
        [Display(Name = "Select File :")]
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Select File :")]
        [Required(ErrorMessage = " ")]
        public HttpPostedFileBase files { get; set; }
        public byte[] bytes { get; set; }


    }
}
