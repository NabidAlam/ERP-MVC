using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class SalaryProcessCOModel
    {
        [Display(Name = "Year : ")]
        public string SalaryYear { get; set; }

        //[Display(Name = "Year : ")]
        ////[RegularExpression("^[0-9]*$", ErrorMessage = "Please enter the Year in number.")]
        //public string Year { get; set; }

        [Display(Name = "Month : ")]
        public string MonthId { get; set; }

        //[Display(Name = "Month : ")]

        //public string MonthName { get; set; }

        [Display(Name = "Month Day : ")]
        public string MonthDay { get; set; }


        [Display(Name = "Employee Id")]
        public string EmployeeId { get; set; }

        [Display(Name = "Employee Name")]
        public string EmployeeName { get; set; }


        [Display(Name = "Unit")]
        public string UnitId { get; set; }

        [Display(Name = "Department")]
        public string DepartmentId { get; set; }

        //[Display(Name = "Department")]
        //public string DepartmentName { get; set; }


        [Display(Name = "Section")]
        public string SectionId { get; set; }

        [Display(Name = "Sub Section")]
        public string SubSectionId { get; set; }

        [Display(Name = "Card Number")]
        public string CardNumber { get; set; }

        //[Display(Name = "Unit")]
        //public string UnitName { get; set; }

        [Display(Name = "Department")]
        public string Departmentname { get; set; }

        //[Display(Name = "Section Name")]
        //public string SectionName { get; set; }

        [Display(Name = "Joining Date")]
        public string JoiningDate { get; set; }

        [Display(Name = "Designation")]
        public string Designation { get; set; }


        public List<SalaryProcessCOModel> SalaryProcessCOList { get; set; }
        //list

        [Display(Name = "Working Day")]

        public string WorkingDay { get; set; }

  
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }

        
    }
}
