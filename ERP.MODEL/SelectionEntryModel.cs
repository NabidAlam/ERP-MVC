using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ERP.MODEL
{
    public class SelectionEntryModel
    {
        [Display(Name ="Style No : ")]
        [Required(ErrorMessage ="Please enter style no ")]
        public string StyleNo { get; set; }
        [Display(Name ="Model No : ")]
        [Required(ErrorMessage = "Please enter model no")]
        public string ModelNo { get; set; }
        [Display(Name ="Total Received : ")]
        [Required(ErrorMessage = "Please enter style no")]
        public string TotalReceived { get; set; }
        [Display(Name ="Country Id : ")]
        [Required(ErrorMessage = "Please select one country")]
        public string CountryId { get; set; }
        [Display(Name ="Country Name : ")]
        public string CountryName { get; set; }
        public string StyleSearch { get; set; }
        public string StyleSearchByYear { get; set; }
        //Save data from table
        public string[] ArrTranId { get; set; }
        public string[] ArrEUSS { get; set; }
        public string[] ArrEUAW { get; set; }
        public string[] ArrElasticity { get; set; }
        public string[] ArrSupply { get; set; }
        public string[] ArrReceived { get; set; }
        public string[] ArrCountry { get; set; }

        //get single value
        public string GridTranId { get; set; }
        public string GridStyleNo { get; set; }
        public string GridModelNo { get; set; }
        public string GridTotalReceived { get; set; }
        public string GridEUSS { get; set; }
        public string GridEUAW { get; set; }
        public string GridElasticity { get; set; }
        public string GridSupply { get; set; }
        public string GridReceived { get; set; }
        public string GridCountry { get; set; }
        //pdf upload
        [Display(Name = "Date :")]
        [Required(ErrorMessage = "Please enter upload date.")]
        public string UploadDate { get; set; }
        public string UploadFiles { get; set; }
        public string CVSize { get; set; }
        [Display(Name = "Select File :")]
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        [Required]
        [DataType(DataType.Upload)]
        [Display(Name = "Select File :")]
        public HttpPostedFileBase files { get; set; }
        public byte[] bytes { get; set; }

        public string POSearch { get; set; }
        [Display(Name = "Season Year :")]
        public string CurrentYear { get; set; }
        public string SearchBy { get; set; }
        public string FileSearch { get; set; }
        public string UpdateBy { get; set; }
        public string CreateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }

        //Reports
        [Display(Name = "Report Type")]
        public string ReportType { get; set; }

        [Display(Name = "Report For")]
        [Required(ErrorMessage = "Please, select one report type above !!")]
        public string ReportFor { get; set; }
        [Display(Name = "Style No : ")]
        public string rptStyleNo { get; set; }
        [Display(Name = "Model No : ")]
        public string rptModelNo { get; set; }
        [Display(Name = "Country : ")]
        public string rptCountryId { get; set; }
        public string rptCountryName { get; set; }



    }
}
