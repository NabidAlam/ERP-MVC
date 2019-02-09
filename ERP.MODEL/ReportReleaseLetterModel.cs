using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class ReportReleaseLetterModel
    {
        [Display(Name = "ID")]
        public string EmployeeId { get; set; }

        [Display(Name = "Date")]
        [Required(ErrorMessage = "Please select a date")]
        public string Date { get; set; }

        [Display(Name = "Report Type")]
        public string ReportType { get; set; }
        //public byte[] FileSize { get; set; }


       
       // public string EmployeeId { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string UpdateBy { get; set; }
    }
}
