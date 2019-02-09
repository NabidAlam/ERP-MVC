using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace ERP.MODEL
{
    public class AttendanceProcessModel
    {
        [Required(ErrorMessage = "Select from date")]
        [DisplayName("From Date")]
        public string FromDate { get; set; }

        [Required(ErrorMessage = "Select to date")]
        [DisplayName("To Date")]
        public string ToDate { get; set; }

        [DisplayName("Upload")]
        public string FileName { get; set; }
        public string FileExtension { get; set; }

        [DisplayName("Unit")]
        public string UnitId { get; set; }
        public string UnitName { get; set; }

        [DisplayName("Department")]
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [DisplayName("Section")]
        public string SectionId { get; set; }
        public string SectionName { get; set; }

        [DisplayName("Sub Section")]
        public string SubSectionId { get; set; }
        public string SubSectionName { get; set; }



        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }

        public string DataUploadDir { get; set; }
    }
}
