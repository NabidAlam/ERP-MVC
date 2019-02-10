using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class JobTypeModel
    {  //Nabid
        //Job Type Entry Form
        //[Display(Name = "ID :")]
        public string JobTypeId { get; set; }

        [Display(Name = "Job Type Name :")]
        [Required(ErrorMessage = "Please enter the Job type.")]
        public string JobTypeName { get; set; }
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
