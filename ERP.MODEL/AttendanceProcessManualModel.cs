using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class AttendanceProcessManualModel
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
        public string DayTypeId { get; set; }

        public string JoiningDate { get; set; }

        [DisplayName("From Date")]
        public string FromDate { get; set; }

        [DisplayName("To Date")]
        public string ToDate { get; set; }
        public string LogDate { get; set; }

        [DisplayName("In-Time")]
        public string InTime { get; set; }
        
        [DisplayName("Out-Time")]
        public string OutTime { get; set; }

        [DisplayName("Lunch-In")]
        public string LunchInTime { get; set; }

        [DisplayName("Lunch-Out")]
        public string LunchOutTime { get; set; }

        [DisplayName("Inactive")]
        public bool InActiveYN { get; set; }
        public string InActive { get; set; }

        [DisplayName("Missing")]
        public bool MissingYN { get; set; }
        public string Missing { get; set; }

        [DisplayName("Absent")]
        public bool AbsentYN { get; set; }
        public string Absent { get; set; }

        [DisplayName("All")]
        public bool AllYN { get; set; }
        public string All { get; set; }

        public string LeaveStatus { get; set; }
        public string AttendanceStatus { get; set; }
        public string CorrectedAttendance { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }


        public string SaveApprovalAttendance { get; set; }

        public List<AttendanceProcessManualModel> AttendanceProcessManualList { get; set; }

        public bool IsChecked { get; set; }
    }
}
