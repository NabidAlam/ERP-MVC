using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class HolidayTypeModel
    {
        [Display(Name = "Id")]
        public string HolidayTypeId { get; set; }

        [Display(Name = "Holiday Type")]
        [Required(ErrorMessage = "Please provide Holiday Type Name")]
        public string HolidayTypeName { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
