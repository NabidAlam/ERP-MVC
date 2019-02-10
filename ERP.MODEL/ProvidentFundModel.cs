
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ERP.MODEL
{
    public class ProvidentFundModel
    {
        [Display(Name = "Employee ID : ")]
        public string EmployeeId { get; set; }


        [Display(Name = "Employee Name : ")]
        public string EmployeeName { get; set; }


        [Display(Name = "Designation : ")]
        public string Designation { get; set; }


        [Display(Name = "Joining Date : ")]
        public string JoiningDate { get; set; }

        public byte[] EmployeeImage { get; set; }
        public string EmployeeImageBase64 { get; set; }


        public string NomineeName { get; set; }
        public string NomineeAddress { get; set; }
        public string NomineeRelation { get; set; }
        public string UnderAge { get; set; }
        public string HandiCap { get; set; }
        public string Mon { get; set; }
        public string Percentage { get; set; }
        public string GuardianName { get; set; }
        public string GuardianAddress { get; set; }
        public string TranId { get; set; }
        public string DeleteEmployeeId { get; set; }
        public string DeleteTranId { get; set; }


        [Display(Name = "ID : ")]
        public string SearchEmployeeId { get; set; }


        [Display(Name = "Name : ")]
        public string SearchEmployeeName { get; set; }

        [Display(Name = "Card No : ")]
        public string SearchCardNo { get; set; }

        [Display(Name = "Unit : ")]
        public string SearchUnitId { get; set; }

        [Display(Name = "Department : ")]
        public string SearchDepartmentId { get; set; }

        [Display(Name = "Section : ")]
        public string SearchSectionId { get; set; }

        [Display(Name = "Sub Section : ")]
        public string SearchSubSectionId { get; set; }

        [Display(Name = "Punch Code : ")]
        public string SearchPunchCode { get; set; }
        public string SearchBy { get; set; }
        public string SearchInactiveYesNo { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }
}
