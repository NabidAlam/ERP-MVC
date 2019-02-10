using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace ERP.MODEL
{
    public class SignupModel
    {

        [Required(ErrorMessage = "Please enter employee ID.")]
        public string EmployeeId { get; set; }
        

        [Required(ErrorMessage = "Please enter email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string EmailAddress { get; set; }
   

        [Required(ErrorMessage = "Please enter mobile no.")]
        public string MobileNo { get; set; }



        [Required(ErrorMessage = "Please enter password.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "Minimum 8 characters required.")]
        public string Password { get; set; }



        [Required(ErrorMessage = "Please enter confirm password.")]
        [Compare("Password", ErrorMessage = "Confirm password do not match with password.")]
        public string ConfirmPassword { get; set; }
      

    }
}
