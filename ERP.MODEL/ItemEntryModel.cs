using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class ItemEntryModel
    {
        //Item Entry
        [Display(Name = "ID :")]
        public string ItemId { get; set; }
        [Display(Name = "Item Name :")]
        [Required(ErrorMessage = "Please enter item name.")]
        public string ItemName { get; set; }

        //Common 
        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }
}
