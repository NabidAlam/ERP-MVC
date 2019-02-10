using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
        public class DeliveryPlaceModel
        {
        [DisplayName("Country Name : ")]
        [Required(ErrorMessage = "Please, select country!")]
        public string CountryId { get; set; }        
        public string CountryName { get; set; }
        public string DeliveryPlaceId { get; set; }
        [DisplayName("Delivery Place : ")]
        [Required(ErrorMessage = "Please, enter delivery place name!")]
        public string DeliveryPlaceName { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }

    }
}
