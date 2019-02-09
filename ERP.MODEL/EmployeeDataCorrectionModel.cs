using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class EmployeeDataCorrectionModel
    {
        [Display(Name = "Employee Id")]
        public string EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }


        [Display(Name = "Unit")]
        //[Required]
        public string UnitId { get; set; }

        [Display(Name = "Department")]
        //[Required]
        public string DepartmentId { get; set; }

        [Display(Name = "Section")]
        //[Required]
        public string SectionId { get; set; }

        [Display(Name = "Sub Section")]
        //[Required]
        public string SubSectionId { get; set; }
        
        
        public string Status { get; set; }
        [Display(Name = "Null")]
        public string IsNull { get; set; }
        //[Required]
        public string PunchCode { get; set; }
        //[Required]
        public string OldId { get; set; }
        //[Required]
        public string CardNumber { get; set; }

        [Display(Name = "Employment Type")]
        //[Required]
        public string EmployeeTypeId { get; set; }

        [Display(Name = "Holiday")]
        //[Required]
        public string WeeklyHolidayId { get; set; }

        [Display(Name = "Designation")]
        //[Required]
        public string DesignationId { get; set; }

        public bool IsChecked { get; set; }


        public List<EmployeeDataCorrectionModel> EmployeeDataCorrectionList { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
