using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class SupplierModel
    {
        public string SupplierId { get; set; }

        [Required(ErrorMessage = "Enter Supplier Name")]
        [DisplayName("Supplier Name")]
        public string SupplierName { get; set; }

        [DisplayName("Bank Name")]
        public string BankName { get; set; }

        [DisplayName("SwiftCode")]
        public string SwiftCode { get; set; }

        [DisplayName("Country")]
        [Required(ErrorMessage = "Select Country")]
        public string CountryId { get; set; }
        public string CountryName { get; set; }

        [DisplayName("Contact No")]
        public string SupplierContactNo { get; set; }
        
        [DisplayName("Email")]
        public string SupplierEmail { get; set; }

        [DisplayName("Supplier Address")]
        [MaxLength(150)]
        public string SupplierAddress { get; set; }
        
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
