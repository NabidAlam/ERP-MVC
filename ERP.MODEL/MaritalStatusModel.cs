using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
   public class MaritalStatusModel
    {
        public string MaritalStatusId { get; set; }
        [DisplayName("Marital Status Name")]
        [Required(ErrorMessage = "Please, enter marital status!")]
        public string MaritalStatusName { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
