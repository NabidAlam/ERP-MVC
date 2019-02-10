using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class WorkingDayModel
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

        [Display(Name = "Year")]
        [Required(ErrorMessage = "Please provide a year")]
        [RegularExpression(@"^[0-9]{4,4}$", ErrorMessage = "Please provide a valid year")]
        public string SalaryYear { get; set; }

        [Display(Name = "Month")]
        [Required(ErrorMessage = "Please select a month")]
        public string MonthId { get; set; }


        [Display(Name = "From Date")]
        [Required(ErrorMessage = "Please enter from date")]
        [RegularExpression(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", ErrorMessage = "Date format must be DD/MM/YYYY")]
        public string FromDate { get; set; }

        [Display(Name = "To Date")]
        [Required(ErrorMessage = "Please enter to date")]
        [RegularExpression(@"^([0-2][0-9]|(3)[0-1])(\/)(((0)[0-9])|((1)[0-2]))(\/)\d{4}$", ErrorMessage = "Date format must be DD/MM/YYYY")]
        public string ToDate { get; set; }


        //[Display(Name = "Working Day")]
        //[Required(ErrorMessage = "Please provide working day")]
        ////[Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        //[RegularExpression(@"^[0-9]{1,6}$", ErrorMessage = "Please provide a positive number")]
        public string WorkingDay { get; set; }


        public string JoiningDate { get; set; }
        public string DesignationName { get; set; }
        public string DepartmentName { get; set; }
        public string MonthDay { get; set; }

        [RegularExpression(@"^(\s*|\d+)$", ErrorMessage = "Please provide a positive amount")]
        public string Advance { get; set; }

        [RegularExpression(@"^(\s*|\d+)$", ErrorMessage = "Please provide a positive amount")]
        public string Arrear { get; set; }

        [RegularExpression(@"^(\s*|\d+)$", ErrorMessage = "Please provide a positive amount")]
        public string Tax { get; set; }


        public List<WorkingDayModel> WorkingDayList { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
        
    }
}
