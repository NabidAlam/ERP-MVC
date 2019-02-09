using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ERP.MODEL
{
    public class PurchaseOrderModel
    {

        //public string InvoiceNumber { get; set; }  
        public string[] TranId { get; set; }
        // [Required(ErrorMessage = " ")]
        public string[] SeasonYear { get; set; }
        public string[] Season { get; set; }
        public string[] OrderCreationDate { get; set; }
        //[Required(ErrorMessage = " ")]
        public string[] OrderNumber { get; set; }    
        public string[] SupplierHandoverDate { get; set; }    
        public string[] OrdertypeId { get; set; }
        public string[] Model { get; set; }
        public string[] Description { get; set; }
        public string[] Item { get; set; }
        public string[] SizeId { get; set; }
        public string[] SizeName { get; set; }
        public string[] PCB { get; set; }
        public string[] UE { get; set; }
        public string[] Packaging { get; set; }
       // [Required(ErrorMessage = " ")]
        public string[] StyleNo { get; set; }
       // [Required(ErrorMessage = " ")]
        public string[] OrderedQty { get; set; }
        public string[] ShippedQty { get; set; }
        public string[] RemainningQty { get; set; }
       // [Required(ErrorMessage = " ")]
        public string[] UnitPrice { get; set; }
        public string[] TotalPrice { get; set; }
        public string[] PortOfDestination { get; set; }
        public string[] DeliveryDate { get; set; }
        public string[] ShipmentTypeId { get; set; }
        public string[] PortOfLandingId { get; set; }
        public string[] CurrencyId { get; set; }
        public string[] CurrencyName { get; set; }
        public string[] Status { get; set; }
        public string[] Remarks { get; set; }
        public string[] CopyYn { get; set; }
        public string[] DeleteYn { get; set; }
        public string[] RejectYn { get; set; }

        //forSingle value
        //[Required(ErrorMessage = "Please Enter Invoice No")]

        [Display(Name = "Invoice No : ")]
        public string rptInvoiceNumber { get; set; }
        [Display(Name = "Style No: ")]
        public string rptStyleNo { get; set; }

        // [Required(ErrorMessage = "Please Enter Order No")]
        [Display(Name = "Model No : ")]
        public string rptModelNo { get; set; }

        [Display(Name = "Order No : ")]
        public string rptOrderNumber { get; set; }
        [Display(Name = "Report Type")]
        public string ReportType { get; set; }

        [Display(Name = "Report For")]
        [Required(ErrorMessage = "Please, select one report type above !!")]
        public string ReportFor { get; set; }

        //[Required(ErrorMessage = " ")]
        [Display(Name = "Invoice No : ")]
        public string InvoiceNumber { get; set; }
        public string GridSeasonYear { get; set; }
        public string GridSeason { get; set; }
        public string GridTranId { get; set; }
        public string GridOrderCreationDate { get; set; }
        public string GridOrderNumber { get; set; }
        public string GridSupplierHandoverDate { get; set; }
        public string GridOrdertypeId { get; set; }
        public string GridOrdertypeName { get; set; }
        public string GridModel { get; set; }
        public string GridDescription { get; set; }
        public string GridItem { get; set; }
        public string GridSizeId { get; set; }
        public string GridSizeName { get; set; }
        public string GridPCB { get; set; }
        public string GridUE { get; set; }
        public string GridPackaging { get; set; }
        public string GridStyleNo { get; set; }
        public string GridOrderedQty { get; set; }
        public string GridShippedQty { get; set; }
        public string GridRemainningQty { get; set; }
        public string GridUnitPrice { get; set; }
        public string GridTotalPrice { get; set; }
        public string GridPortOfDestination { get; set; }
        public string GridDeliveryDate { get; set; }
        public string GridShipmentTypeId { get; set; }
        public string GridShipmentTypeName { get; set; }
        public string GridPortOfLandingId { get; set; }
        public string GridPortOfLandingName { get; set; }
        public string GridCurrencyId { get; set; }
        public string GridCurrencyName { get; set; }
        public string GridStatus { get; set; }
        public string GridRemarks { get; set; }
        public string GridCopyYn { get; set; }
        public string GridDeleteYn { get; set; }
        public string GridRejectYn { get; set; }
        public string GridApprovedYn { get; set; }

        //file upload
        public string DataUploadDir { get; set; }
        public string FileSaveFlag { get; set; }
        public string CVSize { get; set; }
        [Display(Name = "Select File :")]
        public string FileName { get; set; }        
        public string FileExtension { get; set; }
        [DataType(DataType.Upload)]
        [Display(Name = "Select File :")]
        //[Required(ErrorMessage = " ")]
        public HttpPostedFileBase files { get; set; }
        public byte[] bytes { get; set; }
        //Common 
        //public string TranId { get; set; }
        public string RowNumber { get; set; }
        public string SerialNumber { get; set; }
        [Display(Name = "From Date : ")]
        public string FromDate { get; set; }
        [Display(Name = "To Date : ")]
        public string Todate { get; set; }

        public string SearchBy { get; set; }
        public string InvoiceSearch { get; set; }
        public string UpdateBy { get; set; }
        public string UploadDate { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }
}
