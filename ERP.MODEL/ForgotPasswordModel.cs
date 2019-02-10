using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace ERP.MODEL
{
    public class ForgotPasswordModel
    {

        [Required(ErrorMessage = "Please enter employee ID.")]
        public string EmployeeId { get; set; }
        

        [Required(ErrorMessage = "Please enter email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string EmailAddress { get; set; }
   

        [Required(ErrorMessage = "Please enter mobile no.")]
        public string MobileNo { get; set; }


    }
}
