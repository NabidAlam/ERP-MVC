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



        [Display(Name = "Order No:")]
        [Required(ErrorMessage = " ")]
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

        [Display(Name = "Interling:")]
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
        public string DiscountAmount { get; set; }

       // [Required(ErrorMessage = " ")]
        [Display(Name = "Total Amount :")]
        public string TotalAmount { get; set; }
       
       // [Display(Name = "Deliverd Y/N :")]
        public string Delivered_YN { get; set; }
        public string DeliveryCost { get; set; }
        public string Remarks { get; set; }
        public string EmailAddress { get; set; }
        public string DeliveryProcessCost { get; set; }

    //    [Display(Name = "Promotion Code:")]
        public string PromotionCode { get; set; }
        public string PromotionPercentage { get; set; }

        public string PaymentTypeId { get; set; }
        public string PaymentTypeName { get; set; }


        //Global

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string CreateBy { get; set; }  //UpdateBy
        public string CreateDate { get; set; } //SystemDate
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

        //Trims Sub
      
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

        public OnlineOrderMain OnlineOrderMain { get; set; }
        public List<OnlineOrderMain> OnlineOrderMains { get; set; }
        public List<OnlineOrderSub> OnlineOrderSubs { get; set; }
        public List<OnlineOrderSub> OnlineOrderSubMain { get; set; }
    }


    //INVOICE	ORDER NO	PRODUCT DESCRIPTION	STYLE	SIZE	COLOR	QTY	MRP	TOTAL AMOUNT	CUSTOMER NAME	HOME ADDRESS	
    //CELL NO	TELEPHONE NO	Email	WEB ADDRESS	ORDER RECEIVE DATE	ORDER DELIVERY DATE	ORDER SOURCE	DELIVERY COST	PROMOTION CODE	DISCOUNT	REMARKS	STATUS	ID	Image

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
        public string Delivered_YN { get; set; }


    }


    /*order_receive_date =
                TO_DATE (p_order_receive_date, 'dd/mm/yyyy')
             AND order_no = p_order_no
             AND tran_id = p_tran_id
             AND head_office_id = p_head_office_id
             AND branch_office_id = p_branch_office_id;
             
        
        
        
        tran_id,
                                    order_no,
                                    order_receive_date,
                                    order_deliver_date,
                                    order_source_id,
                                    customer_name,
                                    customer_home_address,
                                    customer_office_address,
                                    telepho_no,
                                    cell_no,
                                    web_address,
                                    discount_amount,
                                    total_amount,
                                    delivered_yn,
                                    delivery_cost,
                                    remarks,
                                    EMAIL_ADDRESS,
                                    DELIVERY_PROCESS_COST,
                                    PAYMENT_TYPE_ID,
                                    PRODUCT_DESCRIPTION,
                                    STYLE_NAME,
                                    COLOR_NAME,
                                    SIZE_NAME,
                                    PRODUCT_QUANTITY,
                                    PRODUCT_PRICE,
                                    create_by,
                                    create_date,
                                    update_by,
                                    update_date,
                                    head_office_id,
                                    branch_office_id)
        */

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
   

        public string WebAddress { get; set; }
        public string DiscountAmount { get; set; }
        public string TotalAmount { get; set; }
        public string Delivered_YN { get; set; }
        public string DeliveryCost { get; set; }

        public string Remarks { get; set; }

        public string EmailAddress { get; set; }
        public string DeliveryProcessCost { get; set; }
        public string PaymentTypeId { get; set; }


        public string TranId { get; set; }


        //public string OrderReceiveDate { get; set; }

        //public string OrderNo { get; set; }
        //public string TranId { get; set; }


        public string ProductDescription { get; set; }
        public string StyleName { get; set; }
        public string ColorName { get; set; }
        public string SizeName { get; set; }
        public string ProductQuantity { get; set; }
        public string ProductPrice { get; set; }
        public string PromoCode { get; set; }

        public byte[] ProductPicture { get; set; }


        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

    }
}

