using System;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class LeaveTypeModel
    {
        [Display(Name = "Id")]
        public string LeaveTypeId { get; set; }

        [Display(Name = "Leave Type Name")]
        [Required(ErrorMessage = "Please provide Leave Type Name")]
        public string LeaveTypeName { get; set; }

        [Display(Name = "Max Leave")]
        [Required(ErrorMessage = "Please provide Max Leave")]
        //[Range(0, int.MaxValue, ErrorMessage = "Max Leave value must be in between 0 to 2147483647")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Max Leave must be a positive integer number")]
        public string MaxLeave { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
