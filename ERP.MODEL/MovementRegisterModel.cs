using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class MovementRegisterModel
    {
        [DisplayName("ID : ")]
        public string EmployeeId { get; set; }

        [DisplayName("Employee Name : ")]
        public string EmployeeName { get; set; }
        public byte[] EmployeeImage { get; set; }

        public string LogDate { get; set; }
        public string DayName { get; set; }
        public string DesignationId { get; set; }

        [DisplayName("Designation : ")]
        public string DesignationName { get; set; }

        [DisplayName("Joining Date : ")]
        public string JoiningDate { get; set; }
        public string UnitId { get; set; }
        public string UnitName { get; set; }
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string SectionId { get; set; }
        public string SectionName { get; set; }
        public string SubSectionId { get; set; }
        public string SubSectionName { get; set; }
        public string MovementTypeId { get; set; }
        public string MovementTypeName { get; set; }
        public string FirstIn { get; set; }  //in-time
        public string FirstInTl { get; set; }   //time change by team leader
        public string FirstInHr { get; set; }
        public string LastOut { get; set; }  // out-time
        public string LastOutTl { get; set; }
        public string LastOutHr { get; set; }
        public string Remarks { get; set; }
        public string TeamLeaderApproveYesNo { get; set; }
        public string TimeChangeByTl { get; set; }
        public string TimeChangeByHr { get; set; }
        public string HrApproveYesNo { get; set; }
        public string HrApproveStatus { get; set; }
        public string TeamLeaderRemarks { get; set; }
        public string HrRemarks { get; set; }
        public string SubmitYesNo { get; set; }
        public string ApprovedDateHr { get; set; }
        public string TeamLeaderApproveStatus { get; set; }

        [DisplayName("From Date : ")]
        public string FromDate { get; set; }

        [DisplayName("To Date : ")]
        public string ToDate { get; set; }

        public string SerialNumber { get; set; }
        public string TranId { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SearchBy { get; set; }


        public List<MovementRegisterModel> MovementRegisterModelList { get; set; }
        public string HidMovementRegData { get; set; }
        public bool IsChecked { get; set; }

    }
}
