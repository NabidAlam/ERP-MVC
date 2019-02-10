using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class ReportFabricModel
    {
        [Display(Name = "From Date")]
        [Required(ErrorMessage = "Please select a date")]
        public string FromDate { get; set; }

        [Display(Name = "To Date")]
        [Required(ErrorMessage = "Please select a date")]
        public string ToDate { get; set; }

        [Display(Name = "Supplier")]
        public string SupplierId { get; set; }

        [Display(Name = "Fabric Code")]
        public string FabricCode { get; set; }

        [Display(Name = "Report Type")]
        public string ReportType { get; set; }
        public byte[] FileSize { get; set; }


        public string UnitId { get; set; }
        public string DepartmentId { get; set; }
        public string SectionId { get; set; }
        public string SubSectionId { get; set; }

        public string EmployeeId { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string UpdateBy { get; set; }
    }
}
