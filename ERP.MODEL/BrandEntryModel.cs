using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class BrandEntryModel
    {

        //Brand Entry
        [Display(Name = "ID :")]
        public string BrandId { get; set; }
        [Display(Name = "Brand Name :")]
        [Required(ErrorMessage = "Please enter brand name.")]
        public string BrandName { get; set; }

        //Common 
        public string SerialNumber{get; set;}      
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

    }
}
