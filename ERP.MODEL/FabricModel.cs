using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ERP.MODEL
{
    public class FabricModel
    {
        public string FabricId { get; set; }

        [Display(Name = "Purchase Date")]
        [Required(ErrorMessage = "Please select a date")]
        public string PurchaseDate { get; set; }

        [Display(Name = "Supplier")]
        [Required(ErrorMessage = "Please select a supplier")]
        public string SupplierId { get; set; }
        public string SupplierName { get; set; }

        [Display(Name = "Fabric Type")]
        [Required(ErrorMessage = "Please select a fabric type")]
        public string FabricTypeId { get; set; }
        public string FabricTypeName { get; set; }

        [Display(Name = "Fabric Unit")]
        [Required(ErrorMessage = "Please select a fabric unit")]
        public string FabricUnitId { get; set; }
        public string FabricUnitName { get; set; }

        [Display(Name = "Fabric Code")]
        [Required(ErrorMessage = "Please provide a unique fabric code")]
        [Remote("DoesFabricCodeExists", "Fabric", AdditionalFields = "FabricId", ErrorMessage = "Fabric Code already exists")]
        public string FabricCode { get; set; }
        public string Width { get; set; }

        [Display(Name = "Order Quantity")]
        [Required(ErrorMessage = "Please provide order quantity")]
        public string OrderQuantity { get; set; }

        [Display(Name = "Received Quantity")]
        [Required(ErrorMessage = "Please provide received quantity")]
        public string ReceivedQuantity { get; set; }

        [Display(Name = "Location")]
        [Required(ErrorMessage = "Please select a location")]
        public string LocationId { get; set; }
        public string LocationName { get; set; }

        [Required(ErrorMessage = "Please provide price")]
        public string Price { get; set; }

        [Display(Name = "Total Amount")]
        public string TotalAmount { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "Please select a category")]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }

        [Display(Name = "Designer")]
        [Required(ErrorMessage = "Please select a designer")]
        public string DesignerId { get; set; }
        public string DesignerName { get; set; }

        [Display(Name = "Sample Quantity")]
        public string SampleQuantity { get; set; }

        [Display(Name = "Other Quantity")]
        public string OtherQuantity { get; set; }

        [Display(Name = "Bulk Quantity")]
        public string BulkQuantity { get; set; }

        [Display(Name = "Total Quantity")]
        public string TotalQuantity { get; set; }

        [Display(Name = "Balance Quantity")]
        public string BalanceQuantity { get; set; }

        [Display(Name = "Lab Test")]
        [Required(ErrorMessage = "Please select a lab test")]
        public string LabTestId { get; set; }
        public string LabTestName { get; set; }



        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; }
    }
}
