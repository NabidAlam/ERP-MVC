
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;


namespace ERP.MODEL
{
    public class EmployeeResignModel
    {
        
        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }


        [Display(Name = "Employee ID: ")]
        [Required(ErrorMessage = "Please enter employee ID")]
        public string EmployeeId { get; set; }


        public string EmployeeName { get; set; }



        [Display(Name = "Joining Date: ")]
        public string JoiningDate { get; set; }



        [Display(Name = "Resign Date: ")]
        [Required(ErrorMessage = "Please enter resign date")]
        public string ResignDate { get; set; }





        [Display(Name = "Resign Cause: ")]
        [Required(ErrorMessage = "Please enter resign cause")]
        public string ResignCause { get; set; }




        [Display(Name = "Resign Remarks: ")]
        public string ResignRemarks { get; set; }




        [Display(Name = "Designation: ")]
        public string ResignDesignation { get; set; }


        public byte[] EditImageFileByte { get; set; }
        public string EditImageFileNameBase64 { get; set; }
    }
    
}
