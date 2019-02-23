using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERP.MODEL
{
    public class OnlineOrderStatusModel
    {

        //public byte[] ProductImage { get; set; }
        //public string EditImageFileNameBase64 { get; set; }

        [Display(Name = "Product Image")]
        public string ProductImage { get; set; }

        [Display(Name = "Product Image")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase HttpPostedFileBase { get; set; }
        public byte[] SwatchFileSize { get; set; }
        public string SwatchFileName { get; set; }
        public string SwatchFileExtension { get; set; }
        public string OrderStatus { get; set; }


        [Display(Name = "Order No:")]
        // [Required(ErrorMessage = " ")]
        public string OrderNo { get; set; }

        [Display(Name = "Receive Date:")]
        public string OrderReceiveDate { get; set; }

        [Display(Name = "Delivery Date :")]
        public string OrderDeliveryDate { get; set; }

        [Display(Name = "Order Source:")]
        public string OrderSourceId { get; set; }

        [Display(Name = "Order Source:")]
        public string OrderSourceName { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }

        [Display(Name = "Address:")]
        public string CustomerHomeAddress { get; set; }

        [Display(Name = "Customer Office Address:")]
        // [Required(ErrorMessage = " ")]
        public string CustomerOfficeAddress { get; set; }

        [Display(Name = "Telephone No :")]
        // [Required(ErrorMessage = " ")]
        public string Telephone { get; set; }

        [Display(Name = "Cell No :")]
        public string CellNo { get; set; }

        [Display(Name = "Web Link :")]
        public string WebAddress { get; set; }

        // [Required(ErrorMessage = " ")]
        [Display(Name = "Discount Amount :")]
        public string[] DiscountAmount { get; set; }

        // [Required(ErrorMessage = " ")]
        [Display(Name = "Total Amount :")]
        public string[] TotalAmount { get; set; }

        [Display(Name = "Deliverd Y/N :")]
        public string Delivered_YN { get; set; }

        [Display(Name = "Delivery Cost :")]

        public string DeliveryCost { get; set; }
        public string Remarks { get; set; }
        [Display(Name = "Email :")]

        public string EmailAddress { get; set; }
        [Display(Name = "Delivery Process Cost :")]

        public string DeliveryProcessCost { get; set; }

        [Display(Name = "Promotion Code:")]
        public string[] PromotionCode { get; set; }

        [Display(Name = "Promotion (%):")]

        public string[] PromotionPercentage { get; set; }
        [Display(Name = "Payment Type:")]

        public string PaymentTypeId { get; set; }

        [Display(Name = "Payment Type:")]

        public string PaymentTypeName { get; set; }








        //SEARCH ATTRIBUTE
        [Display(Name = "Order No:")]
        // [Required(ErrorMessage = " ")]
        public string OrderNoSearch { get; set; }

        [Display(Name = "Receive Date:")]
        public string OrderReceiveDateSearch { get; set; }

        [Display(Name = "Delivery Date :")]
        public string OrderDeliveryDateSearch { get; set; }

        [Display(Name = "Order Source:")]
        public string OrderSourceIdSearch { get; set; }

        [Display(Name = "Order Source:")]
        public string OrderSourceNameSearch { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerNameSearch { get; set; }

        [Display(Name = "Address:")]
        public string CustomerHomeAddressSearch { get; set; }

        [Display(Name = "Customer Office Address:")]
        // [Required(ErrorMessage = " ")]
        public string CustomerOfficeAddressSearch { get; set; }

        [Display(Name = "Telephone No :")]
        // [Required(ErrorMessage = " ")]
        public string TelephoneSearch { get; set; }

        [Display(Name = "Cell No :")]
        public string CellNoSearch { get; set; }

        [Display(Name = "Web Link :")]
        public string WebAddressSearch { get; set; }
        [Display(Name = "Deliverd Y/N :")]
        public string Delivered_YNSearch { get; set; }

        //END SEARCH


        //Global

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string CreateBy { get; set; }  //UpdateBy
        public string CreateDate { get; set; } //SystemDate
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

        //Online Order Sub

        public string[] TranId { get; set; }
        //  [Required(ErrorMessage = " ")]
        public string[] ProductDescription { get; set; }
        public string[] StyleName { get; set; }
        //  [Required(ErrorMessage = " ")]
        public string[] ColorName { get; set; }
        //  [Required(ErrorMessage = " ")]
        public string[] SizeName { get; set; }
        public string[] ProductQuantity { get; set; }
        public string[] ProductPrice { get; set; }

        //Trims Sub Save
        public string TranIdS { get; set; }
        public string ProductDescriptionS { get; set; }
        public string StyleNameS { get; set; }
        public string ColorNameS { get; set; }
        public string SizeNameS { get; set; }
        public string ProductQuantityS { get; set; }
        public string ProductPriceS { get; set; }

        //new design
        //Promotion Code	Promotion(%)	Discount Amount	Total Amount
        public string PromotionCodeS { get; set; }
        public string PromotionPercentageS { get; set; }
        public string DiscountAmountS { get; set; }
        public string TotalAmountS { get; set; }



        public OnlineOrderMain OnlineOrderMain { get; set; }
        public List<OnlineOrderMain> OnlineOrderMains { get; set; }
        public List<OnlineOrderSub> OnlineOrderSubs { get; set; }
        public List<OnlineOrderSub> OnlineOrderSubMain { get; set; }


        public OnlineOrderSub OnlineOrderSubMainUpdate { get; set; }
    }



    public class OnlineOrderMain
    {


        public string OrderNo { get; set; }

        public string ProductDescription { get; set; }
        public string StyleName { get; set; }
        public string SizeName { get; set; }

        public string ColorName { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductPrice { get; set; }


        public string TotalAmount { get; set; }


        public string CustomerName { get; set; }

        public string CustomerHomeAddress { get; set; }

        public string PaymentTypeId { get; set; }
        public string DeliveryProcessCost { get; set; }


        //
        public string CustomerOfficeAddress { get; set; }

        //
        public string CellNo { get; set; }

        public string Telephone { get; set; }
        public string EmailAddress { get; set; }


        public string WebAddress { get; set; }
        public string OrderReceiveDate { get; set; }

        public string OrderDeliveryDate { get; set; }


        public string OrderSourceId { get; set; }
        public string OrderSourceName { get; set; }
        public string DeliveryCost { get; set; }
        public string DiscountAmount { get; set; }

        public string Remarks { get; set; }
        [Display(Name = "Deliverd Y/N :")]

        public string Delivered_YN { get; set; }

        public string OrderStatus { get; set; }
        public byte[] SwatchFileSize { get; set; }

        public string SwatchFileName { get; set; }
        public string SwatchFileExtension { get; set; }
        public string ProductImage { get; set; }


    }

    public class OnlineOrderSub
    {
        public string OrderNo { get; set; }
        public string OrderReceiveDate { get; set; }
        public string OrderDeliveryDate { get; set; }
        public string OrderSourceId { get; set; }
        public string OrderSourceName { get; set; }
        public string CustomerName { get; set; }

        public string CustomerHomeAddress { get; set; }
        public string CustomerOfficeAddress { get; set; }

        public string CellNo { get; set; }

        public string Telephone { get; set; }

        public string OrderStatus { get; set; }
        public string WebAddress { get; set; }
        public string DiscountAmount { get; set; }
        public string TotalAmount { get; set; }
        [Display(Name = "Deliverd Y/N :")]

        public string Delivered_YN { get; set; }
        public string DeliveryCost { get; set; }

        public string Remarks { get; set; }

        public string EmailAddress { get; set; }
        public string DeliveryProcessCost { get; set; }
        public string PaymentTypeId { get; set; }


        public string TranId { get; set; }



        public string ProductDescription { get; set; }
        public string StyleName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductPrice { get; set; }
        public string PromoCode { get; set; }
        public string PromotionPercentage { get; set; }


        public byte[] ProductPicture { get; set; }


        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }



      

    }

    public class OnlineOrderReport
    {
        public string OrderNo { get; set; }

        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }
}

