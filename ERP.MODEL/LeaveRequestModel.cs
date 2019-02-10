using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class LeaveRequestModel
    {
        [Display(Name = "ID : ")]

        public string EmployeeId { get; set; }
        public string LeaveTypeId { get; set; }
        public string LeaveStartDate { get; set; }
        public string LeaveEndDate { get; set; }
        public string ApprovedEmployeeId { get; set; }
        public string Remarks { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

        public string LeaveTypeName { get; set; }
        public string LeaveEntitled { get; set; }
        public string LeaveTaken { get; set; }
        public string LeaveRemain { get; set; }

        [Display(Name = "Year : ")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Please enter the Year in number.")]
        public string CurrentYear { get; set; }

        public string TotalNoOfLeave { get; set; }



        public EmployeeModel EmployeeModel { get; set; }



        public string[] GridEmployeeId { get; set; }   //update id

        //[Display(Name = "Leave Type:")]
        //[Required(ErrorMessage = " ")]
        public string[] GridLeaveTypeId { get; set; }   //update id
        public string[] GridLeaveStartDate { get; set; }
        public string[] GridLeaveEndDate { get; set; }
        public string[] GridApprovedEmployeeId { get; set; }
        public string[] GridRemarks { get; set; }






        //Approved by TL and HR
        public string TeamLeaderId { get; set; }

        //public string HrId { get; set; }
        public List<LeaveRequestModel> IndividualLeaveRequestList { get; set; }



        [Display(Name = "From : ")]

        public string LeaveFromDate { get; set; }

        [Display(Name = "To : ")]

        public string LeaveToDate { get; set; }


        public string CheckedYN { get; set; }

        public bool IsChecked { get; set; }

        public string HR_Status { get; set; }

        public string TL_Status { get; set; }

        public string HR_Flag { get; set; }

        public string TL_Flag { get; set; }

        public string SearchBy { get; set; }


        [Display(Name = "Transfer Date : ")]

        public string TransferDate { get; set; }

    }
}
