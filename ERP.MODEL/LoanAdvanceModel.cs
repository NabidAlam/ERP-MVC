using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class LoanAdvanceModel
    {
        public string Serial { get; set; }
        [Display(Name = "Employee : ")]

        public string EmployeeId { get; set; }

        [Display(Name = "Year : ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter the Year in number.")]
        public string Year { get; set; }

        [Display(Name = "Month : ")]
        public string MonthId { get; set; }

        [Display(Name = "Month : ")]

        public string MonthName { get; set; }

        [Display(Name = "Loan : ")]
        public string LoanAmount { get; set; }

        [Display(Name = "Deduction : ")]

        public string DeductionAmount { get; set; }
        [Display(Name = "Remaining : ")]

        public string RemainingAmount { get; set; }
        public string Status { get; set; }

        public string Remarks { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

        public EmployeeModel EmployeeModel { get; set; }


    }
}
