using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class BuyerEnquiryModel
    {

       
        [Display(Name = "Customer :")]
        [Required(ErrorMessage = "Please select buyer name.")]
        public string BuyerId { get; set; }
        public string BuyerName { get; set; }
       
        
        [Display(Name = "Season Name :")]
        [Required(ErrorMessage = "Please select season name.")]
        public string SeasonId { get; set; }
        public string SeasonName { get; set; }

        [Display(Name = "Season Year :")]
        [Required(ErrorMessage = "Please enter season year.")]
        public string SeasonYear { get; set; }

        [Display(Name = "Style :")]
        [Required(ErrorMessage = "Please enter style name.")]
        public string StyleNo { get; set; }

      
        [Display(Name = "Brand :")]
        [Required(ErrorMessage = "Please select brand name.")]
        public string BrandId { get; set; }
        public string BrandName;

       
        [Display(Name = "Item :")]
        [Required(ErrorMessage = "Please select item name.")]
        public string ItemId { get; set; }
        public string ItemName { get; set; }


        [Display(Name = "Order Qty :")]
        [Required(ErrorMessage = "Please enter Order Qty.")]
        public string OrderQuantity { get; set; }

        [Display(Name = "FOB :")]
        [Required(ErrorMessage = "Please enter FOB.")]
        public string FOB { get; set; }
        
        [Display(Name = "CURRENCY :")]
        [Required(ErrorMessage = "Please select currency.")]
        public string CurrencyTypeId { get; set; }
        public string CurrencyTypeName { get; set; }


        //Common 
        public string SerialNumber { get; set; }
        public string StyleSearch { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

    }
}
