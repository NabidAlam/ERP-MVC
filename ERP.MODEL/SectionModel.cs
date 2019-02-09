using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class SectionModel
    {      
        public string SectionId { get; set; }

        [Display(Name = "Section Name")]
        [Required(ErrorMessage = "Please enter the Section Name.")]
        [StringLength(100)]
        public string SectionName { get; set; }

        [StringLength(100)]
        [Display(Name = "Section Name (Bangla)")]
        public string SectionNameBangla { get; set; }

        [Display(Name = "Section Code")]
        [StringLength(20)]
        [Required(ErrorMessage = "Please enter the Section Code.")]
        public string SectionCode { get; set; }


        [Display(Name = "Department")]      
        [Required(ErrorMessage = "Please select a Department Name.")]
        public string DepartmentId { get; set; }

        public string DepartmentName { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
