using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ERP.MODEL
{
    public class OccasionModel
    {

        public string OccasionId { get; set; }

        [Required(ErrorMessage = "Enter Occasion name.")]
        [DisplayName("Occasion")]
        public string OccasionName { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }


    }
}
