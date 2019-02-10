using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class OfficeTimeSpecialModel
    {
        public string OfficeTimeId { get; set; }

        [Display(Name = "Unit")]
        [Required(ErrorMessage = "Enter unit")]
        public string UnitId { get; set; }

        
        [Display(Name = "First In Time")]
        [Required(ErrorMessage = "Enter in-time")]
        public string FirstInTime { get; set; }

        [Display(Name = "Last Out Time")]
        [Required(ErrorMessage = "Enter out-time")]
        public string LastOutTime { get; set; }

        [Display(Name = "Lunch In Time")]
        [Required(ErrorMessage = "Enter lunch in-time")]
        public string LunchInTime { get; set; }

        [Display(Name = "Lunch Out Time")]
        [Required(ErrorMessage = "Enter lunch out-time")]
        public string LunchOutTime { get; set; }

        [DisplayName("From Date")]
        public string FromDate { get; set; }

        public string UnitName { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}