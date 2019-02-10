using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class BuyerBankModel
    {
        public string BuyerBankId { get; set; }

        [Required(ErrorMessage = "Enter buyer bank name.")]
        [DisplayName("Bank Name")]
        public string BuyerBankName { get; set; }

        [Required(ErrorMessage = "Enter bank address.")]
        [DisplayName("Bank Address")]
        public string BankAddress { get; set; }

        [Required(ErrorMessage = "Enter buyer name.")]
        [DisplayName("Buyer Name")]
        public string BuyerId { get; set; }
        public string BuyerName { get; set; }

        [Required(ErrorMessage = "Enter swift no.")]
        [DisplayName("Swift No")]
        public string SwiftNo { get; set; }
        

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
