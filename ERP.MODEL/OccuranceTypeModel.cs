using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class OccurrenceTypeModel
    {
        public string OccurenceTypeId { get; set; }
        [DisplayName("Occurence Type Name")]
        [Required(ErrorMessage = "Please, enter occurence type!")]
        public string OccurenceTypeName { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
