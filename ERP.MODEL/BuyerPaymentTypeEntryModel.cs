using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class BuyerPaymentTypeEntryModel
    {
       
        public string PaymentTypeId { get; set; }

        [Display(Name = "Payment Type Name :")]
        [Required(ErrorMessage = "Please enter payment type.")]
        public string PaymentTypeName { get; set; }
        public string ID { get; set; }
        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }


    }
}
