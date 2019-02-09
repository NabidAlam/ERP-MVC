using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class ReportModel
    {
        [Display(Name = "From Date")]
        [Required(ErrorMessage = "Please enter from date")]
        public string FromDate { get; set; }

        [Display(Name = "To Date")]
        [Required(ErrorMessage = "Please enter to date")]
        public string ToDate { get; set; }
        public string LeaveYear { get; set; }
        public string DepartmentId { get; set; }
        public string UnitId { get; set; }
        public string SectionId { get; set; }
        public string SubSectionId { get; set; }

        [Display(Name = "Report Type")]
        public string ReportType { get; set; }
        public byte[] FileSize { get; set; }


        public string EmployeeId { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string UpdateBy { get; set; }

    }
}
