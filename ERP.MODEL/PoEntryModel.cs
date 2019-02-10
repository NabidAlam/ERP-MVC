using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class PoEntryModel
    {
        [Display(Name = "Po Number")]
        [Required(ErrorMessage = " ")]
        public string PoNumber { get; set; }

        [Display(Name = "Style Number")]
        [Required(ErrorMessage = " ")]
        public string StyleNo { get; set; }

        [Display(Name = "Seasone Year")]
        [Required(ErrorMessage = " ")]
        public string SeasoneYear { get; set; }

        [Display(Name = "Seasone Name")]
        [Required(ErrorMessage = " ")]
        public string SeasoneName { get; set; }

        [Display(Name = "Style Name")]
        [Required(ErrorMessage = " ")]
        public string StyleName { get; set; }

        [Display(Name = "Cost Price")]
        [Required(ErrorMessage = " ")]
        public string CostPrice { get; set; }

        [Display(Name = "Retail Price")]
        [Required(ErrorMessage = " ")]
        public string RetailPrice { get; set; }

        [Display(Name = "Fabric Quantity")]
        [Required(ErrorMessage = " ")]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Enter only numeric number")]
        public string FabricQuantity { get; set; }

        [Display(Name = "Fit")]
        [Required(ErrorMessage = " ")]
        public string Fit { get; set; }

        [Display(Name = "Store Delevery Date")]
        [Required(ErrorMessage = " ")]
        public string StoreDeleveryDate { get; set; }

        [Display(Name = "Insert Date")]
        [Required(ErrorMessage = " ")]
        public string InsertDate { get; set; }

        public bool Embroidary { get; set; }
        public bool Karchupi { get; set; }
        public bool Print { get; set; }
        public bool Wash { get; set; }

        [Display(Name = "Occasion :")]
        public string Occasion { get; set; }

        public string EmbroidaryS { get; set; }
        public string KarchupiS { get; set; }
        public string PrintS { get; set; }
        public string WashS { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name = "Color Way Number")]
        public string[] ColorWayNumber { get; set; }
        [Required(ErrorMessage = " ")]
        [Display(Name = "Color Way Name")]
        public string[] ColorWayName { get; set; }
        [Required(ErrorMessage = " ")]
        [Display(Name = "Size Name")]
        public string[] SizeId { get; set; }
        [Display(Name = "Size Value")]
        [Required(ErrorMessage = " ")]
        public string[] SizeValue { get; set; }
        public string[] TranId { get; set; }


        public string ColorWayNumberS { get; set; }
        public string ColorWayNameS { get; set; }
        public string SizeIdS { get; set; }
        public string SizeValueS { get; set; }
        public string TranIdS { get; set; }

        public string[] CommentTranId { get; set; }
        public string[] PoComment { get; set; }


        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

        public string DisplayInfo { get; set; }

        public List<PoEntryGrid> PoEntryGrids { get; set; }

        public PoEntryMain PoEntryMain { get; set; }

        public List<PoEntrySub> PoEntrySubs { get; set; }

        public List<PoComments> PoCommentses { get; set; }
    }

    public class StyleNumber
    {
        public string StyleNo { get; set; }
        public string StyleName { get; set; }
        

        public string SeasonYear { get; set; }
        public string SeasonId { get; set; }
        public string SeasonName { get; set; }

        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

        public string DisplayInfo { get; set; }
    }

    public class PoEntryGrid
    {
        public string PoNumber { get; set; }
        public string StyleNo { get; set; }
        public string SeasoneYear { get; set; }
        public string SeasoneId { get; set; }
        public string SeasoneName { get; set; }
        public string StyleName { get; set; }
        public string CostPrice { get; set; }
        public string RetailPrice { get; set; }
        public string FabricQuantity { get; set; }
        public string Fit { get; set; }
        public string StoreDeleveryDate { get; set; }
    }

    public class PoEntryMain
    {
        public string PoNumber { get; set; }
        public string StyleNo { get; set; }
        public string SeasoneYear { get; set; }
        public string SeasoneId { get; set; }
        public string SeasoneName { get; set; }
        public string StyleName { get; set; }
        public string CostPrice { get; set; }
        public string RetailPrice { get; set; }
        public string FabricQuantity { get; set; }
        public string FitId { get; set; }
        public string StoreDeleveryDate { get; set; }
        public string InsertDate { get; set; }
        public string Occasion { get; set; }

        public bool Embroidary { get; set; }
        public bool Karchupi { get; set; }
        public bool Print { get; set; }
        public bool Wash { get; set; }
    }

    public class PoEntrySub
    {
        public string PoNumber { get; set; }
        public string StyleNo { get; set; }
        public string SeasoneYear { get; set; }
        public string SeasoneId { get; set; }

        public string ColorWayNumber { get; set; }
        public string ColorWayName { get; set; }

        public string SizeId { get; set; }
        public string SizeValue { get; set; }
        public string TranId { get; set; }

        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }

    public class PoComments
    {
        public string TranId { get; set; }
        public string PoNumber { get; set; }
        public string StyleNo { get; set; }
        public string SeasoneYear { get; set; }
        public string SeasoneId { get; set; }
        public string PoComment { get; set; }

        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }

    public class PoReport
    {
        public string PoNumber { get; set; }
        public string StyleNo { get; set; }
        public string SeasoneYear { get; set; }
        public string SeasoneId { get; set; }

        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }
}
