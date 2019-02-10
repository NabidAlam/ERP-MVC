using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class CostingModel
    {
        public string SeachResult { get; set; }
        public string StyleSearch { get; set; }
        public string BomItemName { get; set; }
        public string BomItemId { get; set; }
        public string GridTranId { get; set; }
        public string year { get; set; }


        [Display(Name = "Style No :")]
        [Required(ErrorMessage = " ")]
        public string StyleNo { get; set; }

        [Required(ErrorMessage = " ")]
        public string BuyerId { get; set; }

        [Display(Name = "Buyer Name :")]
        public string BuyerName { get; set; }


        [Display(Name = "Season Year :")]
        [Required(ErrorMessage = " ")]
        public string SeasonYear { get; set; }
        [Required(ErrorMessage = " ")]
        public string SeasonId { get; set; }

        [Display(Name = "Season Name :")]
        public string SeasonName { get; set; }

        [Display(Name = "Currency :")]
       // [Required(ErrorMessage = " ")]
        public string CurrencyId { get; set; }
        public string CurrencyName { get; set; }

        [Display(Name = "Product Name :")]
       // [Required(ErrorMessage = " ")]
        public string ProductName { get; set; }

       [Display(Name = "Factory :")]
       [Required(ErrorMessage = " ")]
       public string FactoryId { get; set; }
        public string FactoryName { get; set; }

        [Display(Name = "Exchange Rate :")]
        //[Required(ErrorMessage = " ")]
        public string ExchangeRate { get; set; }

        [Display(Name = "Cotation Date :")]
       // [Required(ErrorMessage = " ")]
        public string CotationDate { get; set; }
        [Display(Name = "MPC Code :")]
        // [Required(ErrorMessage = " ")]
        public string MpcCode { get; set; }


        //GridArray
        [Required(ErrorMessage = " ")]
        public string[] Item { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] ItemType { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] ItemDescription { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] ModelCode { get; set; }

        [Required(ErrorMessage = " ")]
        public string[] SupplierId { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] UnitId { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] Consump { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] Wastage { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] UnitPrice { get; set; }

        [Required(ErrorMessage = " ")]
        public string[] Price { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] Rate { get; set; }
        public string[] TranId { get; set; }

        //GridVariable
        public string GridItemId { get; set; }
        public string GridItemName { get; set; }
        public string GridItemType { get; set; }      
        public string GridItemDescription { get; set; }
        public string GridModelCode { get; set; }
        public string GridSupplierId { get; set; }
        public string GridSupplierName { get; set; }
        public string GridUnitId { get; set; }
        public string GridUnitPrice { get; set; }
        public string GridUnitName { get; set; }
        public string GridConsump { get; set; }       
        public string GridWastage { get; set; }      
        public string GridPrice { get; set; }       
        public string GridRate { get; set; }
        public int RowNumber { get; set; }

        //Common
        public string SerialNo { get; set; }
        public string SearchBy { get; set; }
        public string SearchByFlag { get; set; }
        public string Status { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }




    }
}
