using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class AssignEmployeeShiftModel
    {
        public string TranId { get; set; }
        [DisplayName("Id : ")]
        public string EmployeeId { get; set; }

        [DisplayName("Name : ")]
        public string EmployeeName { get; set; }

        [DisplayName("Card No. : ")]
        public string EmployeeCardNo { get; set; }

        [DisplayName("Unit : ")]
        //[Required(ErrorMessage = "Select Unit")]
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

        [DisplayName("Shift : ")]        
        public string ShiftTypeId { get; set; }
        public string ShiftTypeName { get; set; }

        [DisplayName("From Date : ")]
        [Required(ErrorMessage ="Enter to-date.")]
        public string FirstDate { get; set; }

        [DisplayName("To Date : ")]
        [Required(ErrorMessage = "Enter from date.")]
        public string LastDate { get; set; }

        public string LogDate { get; set; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }

        public List<AssignEmployeeShiftModel> AssignEmployeeShiftList { get; set; }
        public bool IsChecked { get; set; }
        public string ActiveStatus { get; set; }
        public string EmployeeGrade { get; set; }
        public byte[] EmpoyeeImage { get; set; }
    }
}
