using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class GradeInfoModel
    {
        public string GradeId { get; set; }

        [Display(Name = "Grade No:")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Grade No.")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter the Grade No. in number.")]
        //[Required(ErrorMessage = "Please enter the Grade No.")]
        public string GradeNo { get; set; }      
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
