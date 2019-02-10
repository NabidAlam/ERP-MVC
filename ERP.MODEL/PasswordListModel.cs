using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class PasswordListModel
    {

        [Display(Name = "ID : ")]
        public string EmployeeId { get; set; }




        [Display(Name = "Name : ")]
        public string EmployeeName { get; set; }





        [Display(Name = "Card No : ")]
        public string CardNo { get; set; }





        [Display(Name = "Unit : ")]
        public string UnitId { get; set; }




        [Display(Name = "Department : ")]
        public string DepartmentId { get; set; }





        [Display(Name = "Section : ")]
        public string SectionId { get; set; }




        [Display(Name = "Sub Section : ")]
        public string SubSectionId { get; set; }






        public List<PasswordListModel> EmployeeList { get; set; }

        public string JoiningDate { get; set; }

        public string Password { get; set; }
        public string SoftwareName { get; set; }
        public string DesignationName { get; set; }

        public string Status { get; set; }

        public byte[] ImageFileByte { get; set; }
        public string ImageFileNameBase64 { get; set; }

        [Display(Name = "Inactive : ")]
        public string ActiveStatus { get; set; }

        public string SearchBy { get; set; }

        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }
}
