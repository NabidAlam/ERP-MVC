using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class DegreeModel
    {
        [Display(Name = "Id")]
        public string DegreeId { get; set; }

        [Display(Name = "Degree Name")]
        [Required(ErrorMessage = "Please provide Degree Name")]
        public string DegreeName { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
