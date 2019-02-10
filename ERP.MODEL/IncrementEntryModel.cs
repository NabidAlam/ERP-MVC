using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class IncrementEntryModel
    {
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
        public string EmployeeGrade { get; set; }

        public string JoiningDate { get; set; }

        public string Year { get; set; }
        public string TotalMonth { get; set; }

        public string JoiningSalary { get; set; }
        public string GrossSalary { get; set; }
        public string IncrementAmount { get; set; }
        public string TotalAmount { get; set; }

        public List<IncrementEntryModel> IncrementEntryList { get; set; }
        public bool IsChecked { get; set; }

        public string ReportType { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }

    }
}
