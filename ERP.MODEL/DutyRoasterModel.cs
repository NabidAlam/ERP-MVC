using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class DutyRoasterModel
    {
        [DisplayName("ID")]
        [Required(ErrorMessage = "Enter employee id")]
        public string EmployeeId { get; set; }

        
        public string EmployeeName { get; set; }

        public string JoiningDate { get; set; }

        public string DesignationId { get; set; }
        public string DesignationName { get; set; }

        public string DepartmentId { get; set; }
        public string Departmentname { get; set; }

        public string DayId { get; set; }
        public string DayName { get; set; }
        public string Date { get; set; }
        public string InTime { get; set; }
        public string OutTime { get; set; }

        public string WeeklyHolidayId { get; set; }
        public string WeeklyHolidayName { get; set; }

        [DisplayName("From Date")]
        public string FromDate { get; set; }
        [DisplayName("To Date")]
        public string ToDate { get; set; }
        public string LogDate { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }

        public string RoasterSave { get; set; }
        public List<DutyRoasterModel> ListDutyRoaster { get; set; }
    }
}
