using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class SubCategoryModel
    {
        public string SubCategoryId { get; set; }

        [Required(ErrorMessage = "Enter sub-category name")]
        [DisplayName("Sub Category Name")]
        public string SubCategoryName { get; set; }

        [Required(ErrorMessage = "Enter category")]
        [DisplayName("Category")]
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
