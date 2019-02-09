using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class ShipmentInfoMODEL
    {
        //Shipment Info
        [Display(Name = "ID :")]
        public string ShipmentInfoId { get; set; }
        [Display(Name = "Shipment Name :")]
        [Required(ErrorMessage = "Please enter the shipment to name.")]
        public string ShipmentInfoName { get; set; }

        [Display(Name = "Address :")]
        [Required(ErrorMessage = "Please enter address.")]
        public string ShipmentInfoIdAddress { get; set; }

        [Display(Name = "Email :")]
        [Required(ErrorMessage = "Please enter email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; }
        [Display(Name = "Mobile No :")]
        public string MobileNo { get; set; }
        [Display(Name = "Tel No :")]
        public string PhoneNo { get; set; }
        [Display(Name = "Fax No :")]
        public string FaxNo { get; set; }
        [Display(Name = "Contact Person :")]
        public string ContactPerson { get; set; }
        //Common 
        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }
}
