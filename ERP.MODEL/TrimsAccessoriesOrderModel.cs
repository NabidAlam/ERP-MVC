using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class TrimsAccessoriesOrderModel
    {
        [Display(Name = "Order Date:")]
        [Required(ErrorMessage = " ")]
        public string[] OrderDate { get; set; }   //update id

        [Display(Name = "Item Id:")]
        [Required(ErrorMessage = " ")]
        public string[] ItemId { get; set; }   //update id

        [Display(Name = "Item Name:")]
        [Required(ErrorMessage = " ")]
        public string[] ItemName { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Item Code:")]
        public string[] ItemCode { get; set; }  //update id

        [Required(ErrorMessage = " ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter Order Quantity in number.")]
        [Display(Name = "Order Quantity:")]
        public string[] OrderQty { get; set; }

        [Display(Name = "Unit:")]
        [Required(ErrorMessage = " ")]
        public string[] UnitId { get; set; }
        public string[] UnitName { get; set; }

        [Display(Name = "Delivery Date:")]
        [Required(ErrorMessage = " ")]
        public string[] DeliveryDate { get; set; }

        [Display(Name = "Remarks:")]
        public string[] Remarks { get; set; }    
        public string[] TranId { get; set; }   //update id

        public int RowNumber { get; set; }

        //Index
        public string GridTranId { get; set; }
        public string GridOrderDate { get; set; }
        public string GridItemId { get; set; }
        public string GridItemName { get; set; }

        public string GridItemCode { get; set; }
        public string GridOrderQty { get; set; }
        public string GridUnitId { get; set; }
        public string GridUnitName { get; set; }
        public string GridDeliveryDate { get; set; }
        public string GridRemarks { get; set; }



        //new update on 2:27pm 10 Dec 18
        //new end 
        [Display(Name = "Supplier:")]
        [Required(ErrorMessage = " ")]
        public string[] SupplierId { get; set; }

        [Display(Name = "Supplier:")]
        [Required(ErrorMessage = " ")]
        public string[] SupplierName { get; set; }

        [Display(Name = "Store:")]
        [Required(ErrorMessage = " ")]
        public string[] StoreId { get; set; }

        [Display(Name = "Store:")]
        [Required(ErrorMessage = " ")]
        public string[] StoreName { get; set; }

        [Display(Name = "Unit Price:")]
        [Required(ErrorMessage = " ")]
        public string[] UnitPrice { get; set; }

        [Display(Name = "Style No:")]
        [Required(ErrorMessage = " ")]
        public string[] SyleNo { get; set; }

        public string GridSupplierId { get; set; }
        public string GridSupplierName { get; set; }

        public string GridStoreId { get; set; }
        public string GridStoreName { get; set; }

        public string GridUnitPrice{ get; set; }
        public string GridSyleNo { get; set; }

        //Index End



        [StringLength(1)]
        public string Pending_YN { get; set; }

        [Required(ErrorMessage = " ")]
        public string InHouseDate { get; set; }
        public string SupplierDate { get; set; }
        public string RevisedDate { get; set; }



        public List<TrimsAccessoriesOrderModel> TrimsData { get; set; }
        public string SerialNumber { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string CreateBy { get; set; }  //UpdateBy
        public string CreateDate { get; set; } //SystemDate
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

    }
}
