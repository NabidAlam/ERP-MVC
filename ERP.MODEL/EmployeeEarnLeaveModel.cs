using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class EmployeeEarnLeaveModel
    {
        [DisplayName("ID : ")]
        public string EmployeeId { get; set; }

        [DisplayName("Name : ")]
        public string EmployeeName { get; set; }

        [DisplayName("Card No : ")]
        public string EmployeeCard { get; set; }

        public string JoiningDate { get; set; }
        public string DesignationName { get; set; }

        [DisplayName("Unit : ")]
        public string UnitId { get; set; }
        public string UnitName { get; set; }

        [DisplayName("Department : ")]
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [DisplayName("Section : ")]
        public string SectionId { get; set; }
        public string SectionName { get; set; }

        [DisplayName("Sub Section : ")]
        public string SubSectionId { get; set; }
        public string SubSectionName { get; set; }

        [DisplayName("Year : ")]
        [Required(ErrorMessage ="Enter year.")]
        public string SalaryYear { get; set; }

        [DisplayName("Date : ")]
        [Required(ErrorMessage = "Enter date.")]
        public string Date { get; set; }

        public string EarnLeaveDay { get; set; }
        public string Amount { get; set; }
        public string TotalMonth { get; set; }



        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }

        public string ReportType { get; set; }

        public List<EmployeeEarnLeaveModel> EmployeeEarnLeaveList { get; set; }
    }
}
