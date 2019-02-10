using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class SellerBankModel
    {
        public string SellerBankId { get; set; }

        [Required(ErrorMessage = "Enter bank name")]
        [DisplayName("Bank Name")]
        public string BankName { get; set; }

        [Required(ErrorMessage = "Enter swift no")]
        [DisplayName("Swift No")]
        public string SwiftNo { get; set; }

        [Required(ErrorMessage = "Enter bank address")]
        [DisplayName("Bank Address")]
        public string BankAddress { get; set; }

        [Required(ErrorMessage = "Select factory name")]
        [DisplayName("Factory Name")]
        public string FactroyId { get; set; }
        public string FactroyName { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }

    }
}
