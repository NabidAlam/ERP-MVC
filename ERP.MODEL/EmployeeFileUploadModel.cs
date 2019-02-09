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
   public class EmployeeFileUploadModel
    {
        [DisplayName("ID : ")]
        public string EmployeeId { get; set; }

        [DisplayName("Name : ")]
        public string EmployeeName { get; set; }

        [DisplayName("Card No. : ")]
        public string EmployeeCardNo { get; set; }

        [DisplayName("Unit : ")]
        [Required]
        public string UnitId { get; set; }
        public string UnitName { get; set; }

        [DisplayName("Department : ")]
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [DisplayName("Section : ")]
        public string SectionId { get; set; }
        public string SectionName { get; set; }

        [DisplayName("Sub-Section : ")]
        public string SubSectionId { get; set; }
        public string SubSectionName { get; set; }

        public string DesignationName { get; set; }
       
        public string JoiningDate { get; set; }

        public bool Active { get; set; }
        public string ActiveYn { get; set; }

        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }

        //Job Description
        [DisplayName("Upload")]
        [DataType(DataType.Upload)]
        //[Required]
        
        public HttpPostedFileBase JdFile { get; set; }
        public string JdFileName { get; set; }
        public string JdFileExtension { get; set; }
        public string JdFileSize { get; set; }

        //Joining Letter
        [DisplayName("Upload")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase JlFile { get; set; }
        public string JlFileName { get; set; }
        public string JlFileExtension { get; set; }
        public string JlFileSize { get; set; }

        //Appointment Letter
        [DisplayName("Upload")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase AlFile { get; set; }
        public string AlFileName { get; set; }
        public string AlFileExtension { get; set; }
        public string AlFileSize { get; set; }


        //Nid or Birth Certificate
        [DisplayName("Upload")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase NidFile { get; set; }
        public string NidFileName { get; set; }
        public string NidFileExtension { get; set; }
        public string NidFileSize { get; set; }


        //Nominee Photo
        [DisplayName("Upload")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase NpFile { get; set; }
        public string NpFileName { get; set; }
        public string NpFileExtension { get; set; }
        public string NpFileSize { get; set; }


        //Provident Fund
        [DisplayName("Upload")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase PfFile { get; set; }
        public string PfFileName { get; set; }
        public string PfFileExtension { get; set; }
        public string PfFileSize { get; set; }


        public List<EmployeeFileUploadModel> EmployeeDocUploadList { get; set; }
        public bool IsChecked { get; set; }
        public bool IsCheckedJl { get; set; }
        public bool IsCheckedAl { get; set; }
        public bool IsCheckedNid { get; set; }
        public bool IsCheckedNp { get; set; }
        public bool IsCheckedPf { get; set; }


        public string StatusJd { get; set; }
        public string StatusJl { get; set; }

        //public string StatusJd { get; set; }
        //public string StatusJd { get; set; }
    }
}
