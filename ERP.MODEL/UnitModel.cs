using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class UnitModel
    {
        //[Display(Name = "ID :")]
        public string UnitId { get; set; }

        [Display(Name = "Unit Name:")]
        [Required(ErrorMessage = "Please enter the Unit.")]
        public string UnitName { get; set; }
        [Display(Name = "Unit Name (Bangla) :")]
        public string UnitNameBangla { get; set; }

        [Display(Name = "Unit Code")]
        [Required(ErrorMessage = "Please enter the Unit Code.")]

        public string UnitCode { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
