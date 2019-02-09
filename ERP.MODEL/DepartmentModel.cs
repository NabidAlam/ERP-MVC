using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class DepartmentModel
    {
        public string DepartmentId { get; set; }
        [DisplayName("Department Code")]
        [Required(ErrorMessage = "Please, enter department code!")]
        public string DepartmentCode { get; set; }
        [DisplayName("Department  Name")]
        [Required(ErrorMessage = "Please, enter department name!")]
        public string DepartmentName { get; set; }
        [DisplayName("Department Name(Bangla)")]
        public string DepartmentNameBangla { get; set; }
        [DisplayName("Unit Name")]
        [Required(ErrorMessage = "Please, enter Unit!")]
        public string UnitId { get; set; }
        public string UnitName { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
