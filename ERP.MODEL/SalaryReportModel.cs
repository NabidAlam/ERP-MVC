using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class SalaryReportModel
    {
        [Display(Name = "Year : ")]
        [Required(ErrorMessage = "Please enter from year.")]
        public string SalaryYear { get; set; }

        [Display(Name = "Month : ")]
        [Required(ErrorMessage = "Please select Month.")]
        public string MonthId { get; set; }
        public string MonthName { get; set; }

        [DisplayName("Unit : ")]
        public string UnitId { get; set; }

        [DisplayName("Department : ")]
        public string DepartmentId { get; set; }

        [DisplayName("Section : ")]
        public string SectionId { get; set; }

        [DisplayName("Sub-Section : ")]
        public string SubSectionId { get; set; }

        [Display(Name = "Report Type")]
        public string ReportType { get; set; }

        public byte[] FileSize { get; set; }

        [DisplayName("Employee Id : ")]
        public string EmployeeId { get; set; }

        
        [Required(ErrorMessage = "Please select report type")]
        public string ReportFor { get; set; }


        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string UpdateBy { get; set; }
    }
}
