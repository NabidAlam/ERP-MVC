using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
   public class DistrictModel
    {
        public string DistrictId { get; set; }

        [DisplayName("District Name")]
        [Required(ErrorMessage = "Please, enter district name!")]
        public string DistrictName { get; set; }

        [DisplayName("Division Name")]
        [Required(ErrorMessage = "Please select one division")]
        public string DivisionId { get; set; }
        public string DivisionName { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
