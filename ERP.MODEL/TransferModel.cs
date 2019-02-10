using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ERP.MODEL;

namespace ERP.Controllers
{
    public class TransferModel
    {
        [Display(Name = "ID : ")]
        public string EmployeeId { get; set; }
        // public string EmployeeName { get; set; }      
        // public string JoiningDate { get; set; }
        // public string DesignationName { get; set; }
        [Display(Name = "Transfer Date : ")]
        public string TransferDate { get; set; }
        [Display(Name = "Leave Year : ")]
        public string Year { get; set; }
        [Display(Name = "Leave Type : ")]
        public string LeaveTypeId { get; set; }
        [Display(Name = "LeaveType : ")]
        public string LeaveTypeName { get; set; }
        [Display(Name = "Leave Balance : ")]
        public string LeaveBalance { get; set; }
        public string Remarks { get; set; }
        public EmployeeModel EmployeeModel { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
    }
}