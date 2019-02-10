using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class ProbationPeriodModel
    {
        [Display(Name = "Id")]
        public string ProbationPeriodId { get; set; }

        [Display(Name = "Probation Period Name")]
        [Required(ErrorMessage = "Please provide Probation Period")]
        public string ProbationPeriodName { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
