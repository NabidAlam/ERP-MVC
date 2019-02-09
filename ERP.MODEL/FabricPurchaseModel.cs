using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class FabricPurchaseModel
    {
        public string FabricPurchaseId { get; set; }

        [Required(ErrorMessage = "Please provide requisition id")]
        public string FabricRequisitionId { get; set; }

        [Display(Name = "Requisition Code")]
        [Required(ErrorMessage = "Please provide requisition code")]
        public string FabricRequisitionCode { get; set; }

        [Display(Name = "Purchase Date")]
        [Required(ErrorMessage = "Please select a date")]
        [RegularExpression(@"(0[1-9]|[12][0-9]|3[01])[/](0[1-9]|1[012])[/]\d{4}", ErrorMessage = "Date format must be as dd/mm/yyyy")]
        public string PurchaseDate { get; set; }

        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Please select a supplier")]
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Please select a location")]
        public string LocationId { get; set; }
        public string LocationName { get; set; }

        [Display(Name = "Fabric Unit")]
        [Required(ErrorMessage = "Please select a fabric unit")]
        public string FabricUnitId { get; set; }
        public string FabricUnitName { get; set; }

        [Display(Name = "Width")]
        [Required(ErrorMessage = "Please provide width")]
        public string FabricWidth { get; set; }
        [Required(ErrorMessage = "Please provide quantity")]
        public string Quantity { get; set; }
        [Display(Name = "Unit Price")]
        [Required(ErrorMessage = "Please provide unit price")]
        public string UnitPrice { get; set; }

        [Display(Name = "Lab Test")]
        [Required(ErrorMessage = "Please select a lab test")]
        public string LabTestId { get; set; }
        public string LabTestName { get; set; }

        public string Remarks { get; set; }

        public string DeliverReceiveStatus { get; set; }

        
        public List<string> FabricPurchaseIdList { get; set; }



        public string SearchBy { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }
}
