using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class SalaryMonthModel
    {
       
        [Display(Name = "Year :")]
        [Required(ErrorMessage = "Please enter year.")]

        public string SalaryYear { get; set; }

        //[Display(Name = "Month :")]
        ////[Required(ErrorMessage = "Please select month.")]
        //public string SalaryMonth { get; set; }

        [Display(Name = "From Date :")]
        [Required(ErrorMessage = "Please select from date.")]
        public string FromDate { get; set; }

        [Display(Name = "To Date :")]
        [Required(ErrorMessage = "Please select to date.")]
        public string ToDate { get; set; }

        [Display(Name = "Month :")]
        [Required(ErrorMessage = "Please select month.")]
        public string MonthId { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }


    }
}
