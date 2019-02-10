using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class DesignationModel
    {
        //[Display(Name = "ID :")]
        public string DesignationId { get; set; }

        [Display(Name = "Designation Name :")]
        [Required(ErrorMessage = "Please enter the Designation type.")]     
        public string DesignationName { get; set; }
        [Display(Name = "Designation Name (Bangla) :")]
        public string DesignationNameBangla { get; set; }
        //Common 
        //public string ID { get; set; }
        //public string ButtonText { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
