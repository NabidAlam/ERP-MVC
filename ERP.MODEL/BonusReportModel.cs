using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class BonusReportModel
    {
        [Display(Name = "Year : ")]
        [Required(ErrorMessage = "Please enter from year.")]
        public string EidYear { get; set; }

        [Display(Name = "Eid : ")]
        [Required(ErrorMessage = "Please select eid.")]
        public string EidId { get; set; }
        public string EidName { get; set; }

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

        [Display(Name = "Report Type")]
        public string ReportFor { get; set; }


        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string UpdateBy { get; set; }
    }
}
