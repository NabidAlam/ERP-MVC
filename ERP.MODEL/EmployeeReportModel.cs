﻿using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class EmployeeReportModel
    {


        [Display(Name = "From Date : ")]
        public string FromDate { get; set; }




        [Display(Name = "To Date : ")]
        public string ToDate { get; set; }





        [Display(Name = "Employee ID :")]
        public string EmployeeId { get; set; }






        [Display(Name = "Unit :")]
        public string UnitId { get; set; }





        [Display(Name = "Department :")]
        public string DepartmentId { get; set; }







        [Display(Name = "Section :")]
        public string SectionId { get; set; }






        [Display(Name = "Sub Section :")]
        public string SubSectionId { get; set; }






        [Display(Name = "Year :")]
        public string Year { get; set; }






        [Display(Name = "Month : ")]
        public string MonthId { get; set; }





        [Display(Name = "Gender : ")]
        public string GenderId { get; set; }





        [Display(Name = "Report Type")]
        public string ReportType { get; set; }



        
        [Required(ErrorMessage = "Please select report type")]
        public string ReportFor { get; set; }



        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string UpdateBy { get; set; }
    }
}
