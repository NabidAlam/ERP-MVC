using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class JobLocationInfo
    {
        public string JobLocationId { get; set; }

        [Display(Name = "Job Location Name :")]
        [Required(ErrorMessage = "Please enter the Job Location.")]
        public string JobLocationName { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
