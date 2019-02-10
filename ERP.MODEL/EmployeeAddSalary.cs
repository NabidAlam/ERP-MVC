using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class EmployeeAddSalary
    {
        [Display(Name = "Employee Id")]
        public string EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }


        [Display(Name = "Unit")]
        public string UnitId { get; set; }

        [Display(Name = "Department")]
        public string DepartmentId { get; set; }

        [Display(Name = "Section")]
        public string SectionId { get; set; }

        [Display(Name = "Sub Section")]
        public string SubSectionId { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }


        public string UnitName { get; set; }

        public string Departmentname { get; set; }

        public string SectionName { get; set; }

        public string JoiningDate { get; set; }
        public string Designation { get; set; }

        public string JoiningSalary { get; set; }
        public string GrossSalary { get; set; }

       public string Status { get; set; }

        public string Active_YN { get; set; }

        
        public List<EmployeeAddSalary> EmployeeAddSalaryList { get; set; }
        //list
        
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
        
    }
}
