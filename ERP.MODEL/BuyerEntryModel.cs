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
    public class BuyerEntryModel
    {
       
        public string BuyerId { get; set; }

        [Display(Name = "Buyer Name :")]
        [Required(ErrorMessage = "Please enter buye name.")]
        public string BuyerName { get; set; }

        [Display(Name = "Country Name :")]
        public string CountryId { get; set; }
        
        public string CountryName { get; set; }

        [Display(Name = "Contact No :")]
        public string ContactNo { get; set; }

        [Display(Name = "Email Address :")]
        public string EmailAddress { get; set; }

        [Display(Name = "Buyer Address :")]
        public string BuyerAddress { get; set; }

        [Display(Name = "Payment By :")]
        public string PaymentBy { get; set; }
        public string PaymentByName { get; set; }
        public string ID { get; set; }
        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }


    }
}
