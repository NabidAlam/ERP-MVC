using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ERP.MODEL
{
    public class TechpackFileUploadModel
    {

        [Display(Name = "Buyer :")]
        [Required(ErrorMessage = "Please select buyer name.")]
        public string BuyerId { get; set; }
        public string BuyerName { get; set; }


        [Display(Name = "Season Name :")]
        [Required(ErrorMessage = "Please select season name.")]
        public string SeasonId { get; set; }
        public string SeasonName { get; set; }

        [Display(Name = "Season Year :")]
        [Required(ErrorMessage = "Please enter season year.")]
        public string SeasonYear { get; set; }

        [Display(Name = "Style :")]
        [Required(ErrorMessage = "Please enter style name.")]
        public string StyleNo { get; set; }

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




        //Common 
        public string TranId { get; set; }
        public string StyleSearch { get; set; }
        public string ShowCriteria { get; set; }
        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

    }
}
