using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class ShiftTimeModel
    {
        public string ShiftTimeId { get; set; }

        [DisplayName("Shift Name")]
        [Required(ErrorMessage = "Select shift")]
        public string ShiftId { get; set; }
        public string ShiftName { get; set; }

        [DisplayName("First In-Time")]
        [Required(ErrorMessage = "Enter first in-time")]
        public string FirstInTime { get; set; }

        [DisplayName("Last Out-Time")]
        [Required(ErrorMessage = "Enter last out-time")]
        public string LastOutTime { get; set; }

        [DisplayName("Lunch Out-Time")]
        [Required(ErrorMessage = "Enter lunch out-time")]
        public string LunchOutTime { get; set; }

        [DisplayName("Lunch In-Time")]
        [Required(ErrorMessage = "Enter lunch in-time")]
        public string LunchInTime { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
