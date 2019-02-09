using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERP.MODEL
{
    public class NoticeBoardModel
    {
 
        public string NoticeId { get; set; }

        [Display(Name = "Title:")]
        [Required(ErrorMessage = " ")]
        public string NoticeTitle { get; set; }

        [Display(Name = "Date:")]
        [Required(ErrorMessage = " ")]
        public string NoticeDate { get; set; }

        [Display(Name = "Notice Type:")]
        [Required(ErrorMessage = " ")]
        public string NoticeTypeId { get; set; } //L_NOTICE_TYPE Table
        public string NoticeTypeName { get; set; } //L_NOTICE_TYPE Table
        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string CreateBy { get; set; }  //UpdateBy
        public string CreateDate { get; set; } //SystemDate
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }


        public NoticeBoardImageModel NoticeBoardImageModel { get; set; }

        [DataType(DataType.Upload)]
        [Display(Name = "Select File :")]
        [RegularExpression(@"([a-zA-Z0-9\s_\\.\-:])+(.pdf)$", ErrorMessage = "Only PDF file is allowed.")]
        public HttpPostedFileBase files { get; set; }
        public byte[] bytes { get; set; }





        //Notice File for All
        [Display(Name = "From Date:")]
        [Required(ErrorMessage = " ")]
        public string FromDate { get; set; }

        [Display(Name = "To Date:")]
        [Required(ErrorMessage = " ")]
        public string ToDate { get; set; }


        public string btnAll { get; set; }
        public string btnPolicy { get; set; }
        public string btnKPI { get; set; }
        public string btnHoliday { get; set; }
        public string btnIdCard { get; set; }
        public string btnTraining { get; set; }
        public string btnBonusPoint { get; set; }
        public string btnRecruit { get; set; }


      

    }
}
