using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class BuyerModel
    {

        public string BuyerId { get; set; }
        [Required(ErrorMessage = "Enter Buyer Name")]
        [DisplayName("Buyer Name")]
        public string BuyerName { get; set; }
        [DisplayName("Contact No")]
        public string BuyerContactNo { get; set; }
        [DisplayName("Buyer Address")]
        [MaxLength(150)]
        public string BuyerAddress { get; set; }

        [DisplayName("Payment By")]
        public string BuyerPaymentBy { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Select Country")]
        public string CountryId { get; set; }
        public string CountryName { get; set; }

        [DisplayName("Email")]
        public string BuyerEmail { get; set; }

        public string SearchBy{ get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
