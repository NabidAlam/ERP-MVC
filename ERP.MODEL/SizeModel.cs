using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class SizeModel
    {
        public string SizeId { get; set; }

        [Required(ErrorMessage = "Enter size name.")]
        [DisplayName("Display Name")]
        public string SizeName { get; set; }

        [Required(ErrorMessage = "Enter size value")]
        [DisplayName("Size Value")]
        public string SizeValue { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
