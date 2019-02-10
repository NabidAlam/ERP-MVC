using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERP.MODEL
{
    public class NoticeBoardImageModel
    {       
        public string NoticeId { get; set; }
        public string NoticeDate { get; set; }
        public string NoticeTypeId { get; set; }

        public string FileSize { get; set; }
      
        [Display(Name = "Select File :")]
        public string FileName { get; set; }
        public string FileExtension { get; set; }


        //[Required]
        //[DataType(DataType.Upload)]
        //[Display(Name = "Select File :")]
        //public HttpPostedFileBase files { get; set; }
        //public byte[] bytes { get; set; }

        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string CreateBy { get; set; }  //UpdateBy
        public string CreateDate { get; set; } //SystemDate
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        //public NoticeBoardModel NoticeBoardModel { get; set; }


        public byte[] fileBytes { get; set; }


        [DataType(DataType.Upload)]
        [Display(Name = "Select File :")]
        public HttpPostedFileBase files { get; set; }



        public byte[] bytes { get; set; }

    }
}
