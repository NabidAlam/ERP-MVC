using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class EmployeeImageModel
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




        [Display(Name = "Punch Code : ")]
        public string PunchCode { get; set; }

        public List<EmployeeImageModel> EmployeeList { get; set; }

        public string DateOfBirth { get; set; }
        public string JoiningDate { get; set; }
        public string JobConfirmationDate { get; set; }
        public string PresentDesignationName { get; set; }
        public string UnitName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string SubSectionName { get; set; }
        public string GradeId { get; set; }
        public string Status { get; set; }

        public byte[] ImageFileByte { get; set; }
        public string ImageFileNameBase64 { get; set; }

        [Display(Name = "Inactive : ")]
        public string ActiveStatus { get; set; }

        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }
}
