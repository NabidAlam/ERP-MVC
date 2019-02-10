using System.Collections.Generic;
using System.ComponentModel;

namespace ERP.MODEL
{
    public class AttendanceApprovalModel
    {
        [DisplayName("Id")]
        public string EmployeeId { get; set; }
        [DisplayName("Name")]
        public string EmployeeName { get; set; }

        [DisplayName("Card No.")]
        public string EmployeeCardNo { get; set; }

        [DisplayName("Unit")]
        public string UnitId { get; set; }
        public string UnitName { get; set; }

        [DisplayName("Department")]
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        [DisplayName("Section")]
        public string SectionId { get; set; }
        public string SectionName { get; set; }

        [DisplayName("Sub-Section")]
        public string SubSectionId { get; set; }
        public string SubSectionName { get; set; }

        public string DesignationName { get; set; }
        public string DayType { get; set; }

        public string JoiningDate { get; set; }
        [DisplayName("From Date")]
        public string FromDate { get; set; }

        [DisplayName("To Date")]
        public string ToDate { get; set; }
        public string LogDate { get; set; }

        public string InTime { get; set; }
        public string OutTime { get; set; }
        public string DutyTime { get; set; }

        [DisplayName("Inactive")]
        public bool CheckedYN { get; set; }
        public string ActiveYN { get; set; }

        public string ApprovalStatus { get; set; }

        public List<AttendanceApprovalModel> ListAttendanceApprovalModels { get; set; }
        public bool IsChecked { get; set; }

        //public string PendingApprovalAttendance { set; get; }

        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }

    }   
}