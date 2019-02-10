using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class SpecificationModel
    {
        [Display(Name = "Style No:")]
        [Required(ErrorMessage = " ")]
        public string StyleNo { get; set; }
        [Display(Name = "Style Name:")]
        public string StyleName { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Season:")]
        public string SeasonId { get; set; }

        public string SeasonName { get; set; }

        [Display(Name = "Season Year:")]
        [Required(ErrorMessage = " ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter the Season in number.")]
        public string SeasonYear { get; set; }

        [Display(Name = "Date:")]
        [Required(ErrorMessage = "Please select a date")]
        public string SpecificationDate { get; set; }



        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string CreateBy { get; set; }
        public string CreateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }


        //Detail
        public string[] TranId { get; set; }
        public string[] SizeId { get; set; }
        public string[] Description { get; set; }
        public string[] SizeValue { get; set; }



        public int RowNumber { get; set; }
        public string SerialNumber { get; set; }

        //Detail End


        //Index
        public string GridTranId { get; set; }
        public string GridSizeId { get; set; }
        public string GridDescription { get; set; }
        public string GridSizeValue { get; set; }
        //Index End

        public string StyleSearch { get; set; }

    }
}
