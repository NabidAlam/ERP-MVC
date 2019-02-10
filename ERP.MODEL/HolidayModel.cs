using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class HolidayModel
    {
        [Display(Name = "Id")]
        public string HolidayId { get; set; }

        [Display(Name = "Holiday Type")]
        [Required(ErrorMessage = "Please select Holiday Type")]
        public string HolidayTypeId { get; set; }

        public string HolidayTypeName { get; set; }

        [Display(Name = "From Date")]
        //[DataType(DataType.Date, ErrorMessage = "From Date must be a valid date")]
        [Required(ErrorMessage = "Please provide From Date")]
        public string FromDate { get; set; }

        [Display(Name = "To Date")]
        //[DataType(DataType.Date, ErrorMessage = "To Date must be a valid date")]
        [Required(ErrorMessage = "Please provide To Date")]
        public string ToDate { get; set; }
        public string Remarks { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
