using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class TrimsModel
    {
        [Display(Name = "Style No:")]
        [Required(ErrorMessage = " ")]
        public string StyleNo { get; set; }

        [Display(Name = "Style Name:")]
        public string StyleName { get; set; }

        [Display(Name = "Season:")]
        public string SeasonId { get; set; }

        public string SeasonName { get; set; }

        [Display(Name = "Season Year:")]
        [Required(ErrorMessage = " ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter the Season in number.")]
        public string SeasonYear { get; set; }

        [Display(Name = "Interling:")]
        [Required(ErrorMessage = " ")]
        public string Interling { get; set; }

        [Display(Name = "Main Label:")]
        [Required(ErrorMessage = " ")]
        public string MainLabel { get; set; }

        [Display(Name = "Care Label:")]
        [Required(ErrorMessage = " ")]
        public string CareLabel { get; set; }

        [Display(Name = "Size Label:")]
        [Required(ErrorMessage = " ")]
        public string SizeLabel { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Sewing Thread:")]
        public string SewingThread { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Hang Tag:")]
        public string HangTag { get; set; }

        public string DisplayInfo { get; set; }

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
        [Required(ErrorMessage = " ")]
        public string[] Accessories { get; set; }
        public string[] TrimsCode { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] PerGarmentsQuantity { get; set; }
        [Required(ErrorMessage = " ")]
        public string[] TotalQuantity { get; set; }

        //Trims Sub Save
        public string TranIdS { get; set; }
        public string AccessoriesS { get; set; }
        public string TrimsCodeS { get; set; }
        public string PerGarmentsQuantityS { get; set; }
        public string TotalQuantityS { get; set; }

        public TrimsMain TrimsMain { get; set; }
        public List<TrimsMain> TrimsMains { get; set; }
        public List<TrimsSub> TrimsSubs { get; set; }
    }

    public class TrimsMain
    {
        public string StyleNo { get; set; }
        public string StyleName { get; set; }
        public string SeasonId { get; set; }
        public string SeasonName { get; set; }
        public string SeasonYear { get; set; }
        public string Interling { get; set; }
        public string MainLabel { get; set; }
        public string CareLabel { get; set; }
        public string SizeLabel { get; set; }
        public string SewingThread { get; set; }
        public string HangTag { get; set; }
    }

    public class TrimsSub
    {
        public string StyleNo { get; set; }
        public string SeasoneYear { get; set; }
        public string SeasoneId { get; set; }

        public string TranId { get; set; }


        public string Accessories { get; set; }
        public string TrimsCode { get; set; }
        public string PerGarmentsQuantity { get; set; }
        public string TotalQuantity { get; set; }

        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

    }


    public class TrimsReport
    {
        public string StyleNo { get; set; }
        public string SeasoneYear { get; set; }
        public string SeasoneId { get; set; }

        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }
}
