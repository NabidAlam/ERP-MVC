using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
   public class ReligionModel
    {
        public string ReligionId { get; set; }
        [DisplayName("Religion Name")]
        [Required(ErrorMessage = "Please, enter religion name!")]
        public string ReligionName { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
