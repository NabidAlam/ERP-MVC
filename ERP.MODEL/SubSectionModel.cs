using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class SubSectionModel
    {
        [Display(Name = "Id")]
        public string SubSectionId { get; set; }

        [Display(Name = "Sub Section Code")]
        [Required(ErrorMessage = "Please provide Sub Section Code")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Sub Section Code must be a numeric value")]
        //Have to be only number
        public string SubSectionCode { get; set; }

        [Display(Name = "Sub Section Name")]
        [Required(ErrorMessage = "Please provide Sub Section Name")]
        public string SubSectionName { get; set; }

        [Display(Name = "Section Name (Bangla)")]
        public string SubSectionNameBangla { get; set; }

        [Display(Name = "Section Name")]
        [Required(ErrorMessage = "Please select a Section Name")]
        public string SectionId { get; set; }

        public string SectionName { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
