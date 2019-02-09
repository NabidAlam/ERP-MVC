using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class BloodGroupModel
    {
        [Display(Name = "ID :")]
        public string BloodGroupId { get; set; }

        [Display(Name = "Blood Group Name :")]
        [Required(ErrorMessage = "Please enter the blood group name.")]
        public string BloodGroupName { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
