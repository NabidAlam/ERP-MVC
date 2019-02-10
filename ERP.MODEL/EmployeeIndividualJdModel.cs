using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ERP.MODEL
{
    public class EmployeeIndividualJdModel
    {

        [DisplayName("ID : ")]
        public string EmployeeId { get; set; }

        [DisplayName("Name : ")]
        public string EmployeeName { get; set; }

        [DisplayName("Card No. : ")]
        public string EmployeeCardNo { get; set; }

        public string TeamLeaderId { get; set; }

        public string HrId { get; set; }

        [DisplayName("Unit : ")]
        [Required]
        public string UnitId { get; set; }
        public string UnitName { get; set; }

        [DisplayName("Department : ")]
        public string DepartmentId { get; set; }
        [DisplayName("Department : ")]
        public string DepartmentName { get; set; }

        [DisplayName("Section : ")]
        public string SectionId { get; set; }
        public string SectionName { get; set; }

        [DisplayName("Sub-Section : ")]
        public string SubSectionId { get; set; }
        public string SubSectionName { get; set; }

        [DisplayName("Designation : ")]
        public string DesignationName { get; set; }

        public string JoiningDate { get; set; }

        [DisplayName("To Date : ")]
        [Required(ErrorMessage = "Enter to date.")]
        public string ToDate { get; set; }

        [DisplayName("From Date : ")]
        [Required(ErrorMessage = "Enter from date.")]
        public string FromDate { get; set; }

        [DisplayName("Upload")]
        [DataType(DataType.Upload)]
        //[Required]

        public HttpPostedFileBase IndividualJdFile { get; set; }
        public string IndividualJdFileName { get; set; }
        public string IndividualJdFileExtension { get; set; }
        public string IndividualJdFileSize { get; set; }

        public byte[] EmployeeImage { get; set; }

        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }

        public List<EmployeeIndividualJdModel> IndividualJdList { get; set; }

        public string CheckedYN { get; set; }

        public bool IsChecked { get; set; }

        public string TranId { get; set; }

        public string TeamLeaderStatus { get; set; }

        public string HrStatus { get; set; }
    }
}
