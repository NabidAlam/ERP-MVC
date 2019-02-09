using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class IncrementSetupModel
    {

        [Display(Name = "Year :")]
        [Required(ErrorMessage = "Please enter year.")]
        public string IncrementYear { get; set; }

        [Display(Name = "Effect Date :")]
        [Required(ErrorMessage = "Please enter effect date.")]
        public string EffectDate { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }


    }
}
