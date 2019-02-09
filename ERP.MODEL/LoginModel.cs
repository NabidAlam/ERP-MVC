using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace ERP.MODEL
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Please enter employee ID.")]
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    
        [Required(ErrorMessage = "Please enter password.")]
        public string EmployeePassword { get; set; }
        public string EmilAddress { get; set; }
        public string EmployeeImage { get; set; }
        public string MobileNo { get; set; }
        public string IPAddress { get; set; }
        public string DepartmentId { get; set; }
        public string UnitId { get; set; }
        public string SubSectionId { get; set; }
        public string DesignationId { get; set; }
        public string HeadOfficeName { get; set; }
        public string BranchOfficeName { get; set; }
        public string OldPassword { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string SoftwareId { get; set; }
        public string MacAddress { get; set; }
        public string SoftwareName { get; set; }
        public string Softversion { get; set; }
        public string Message { get; set; }
        public string ReleaseDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }


    }

    public class PassworChange
    {


        
        public string EmployeeId { get; set; }
        [Required(ErrorMessage = "Please enter new password.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Please enter confirm password.")]
        public string ConfirmPassword { get; set; }
        public string Message { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext context)
        {
            if (NewPassword != ConfirmPassword)
                yield return new ValidationResult("PASSWORD MISMATCH!!!");
        }


    }
}
