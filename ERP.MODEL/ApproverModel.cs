using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class ApproverModel
    {
        public string ApproverId { get; set; }

        [Display(Name = "Employee Id")]
        public string EmployeeId { get; set; }

        public List<string> EmployeeIdList { get; set; }


        [Display(Name = "Unit")]
        public string UnitId { get; set; }

        [Display(Name = "Department")]
        public string DepartmentId { get; set; }

        [Display(Name = "Section")]
        public string SectionId { get; set; }

        [Display(Name = "Sub Section")]
        public string SubSectionId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }
        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }
        public string Status { get; set; }


        public string JoiningDate { get; set; }
        public string DesignationName { get; set; }
        public string UnitName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string SubSectionName { get; set; }
        public byte[] EmployeeImage { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
