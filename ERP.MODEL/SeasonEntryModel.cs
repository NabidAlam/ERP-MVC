using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ERP.MODEL;
using System.Data;
using System.ComponentModel.DataAnnotations;



namespace ERP.MODEL
{
    public class SeasonEntryModel
    {
       
        public string SeasonId { get; set; }

        [Display(Name = "Season Name :")]
        [Required(ErrorMessage = "Please enter season name.")]
        public string SeasonName { get; set; }
        public string ID { get; set; }
        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }


    }
}
