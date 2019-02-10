using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class AttendenceReportModel
    {
        //name: mezba & date: 07.01.2019
        [Display(Name = "From Date : ")]
        [Required(ErrorMessage = "Please select a date")]
        public string FromDate { get; set; }




        [Display(Name = "To Date : ")]
        [Required(ErrorMessage = "Please select a date")]
        public string ToDate { get; set; }


        [Display(Name = "Report Type")]
        public string ReportType { get; set; }

        
        [Required(ErrorMessage = "Please select report type")]
        public string ReportFor { get; set; }
        public byte[] FileSize { get; set; }


        [Display(Name = "Employee ID :")]
        public string EmployeeId { get; set; }

        [Display(Name = "Unit :")]
        public string UnitId { get; set; }


        [Display(Name = "Department :")]
        public string DepartmentId { get; set; }


        [Display(Name = "Section :")]
        public string SectionId { get; set; }


        [Display(Name = "Sub Section :")]
        public string SubSectionId { get; set; }


        [Display(Name = "Employeement Type :")]
        public string EmployeeTypeId { get; set; }


        [Display(Name = "No Of Employee :")]
        public string TotalEmployee { get; set; }

        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string UpdateBy { get; set; }
    }
}
