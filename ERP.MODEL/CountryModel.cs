using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class CountryModel
    {
        public string CountryId { get; set; }

        [DisplayName("Country Name")]
        [Required(ErrorMessage = "Please, enter country name!")]
        public string CountryName { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}