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
    public class CurrencyEntryModel
    {
       
        public string CurrencyId { get; set; }

        [Display(Name = "Currency Name :")]
        [Required(ErrorMessage = "Please enter currency name.")]
        public string CurrencyName { get; set; }
        public string ID { get; set; }
        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }


    }
}
