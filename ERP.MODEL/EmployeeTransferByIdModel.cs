using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class EmployeeTransferByIdModel
    {

        [Display(Name = "Year : ")]
        public string TransferYear { get; set; }

        //[Display(Name = "Year : ")]
        ////[RegularExpression("^[0-9]*$", ErrorMessage = "Please enter the Year in number.")]
        //public string Year { get; set; }

        [Display(Name = "Month : ")]
        public string MonthId { get; set; }

        //[Display(Name = "Month : ")]

        //public string MonthName { get; set; }

        [Display(Name = "Month Day : ")]
        public string MonthDay { get; set; }


        [Display(Name = "Employee Id")]
        public string EmployeeId { get; set; }

    

        [Display(Name = "Unit")]
        public string UnitId { get; set; }
        public string UnitOld { get; set; }


        [Display(Name = "Department")]
        public string DepartmentId { get; set; }
        public string DepartmentOld { get; set; }

        //[Display(Name = "Department")]
        //public string DepartmentName { get; set; }


        [Display(Name = "Section")]
        public string SectionId { get; set; }
        public string SectionOld { get; set; }


        [Display(Name = "Sub Section")]
        public string SubSectionId { get; set; }
        public string SubSectionOld { get; set; }

        //[Display(Name = "Unit")]
        //public string UnitName { get; set; }

        //[Display(Name = "Department")]
        //public string Departmentname { get; set; }

        //[Display(Name = "Section Name")]
        //public string SectionName { get; set; }


        //EMPLOYEE ID	TRANSFER YEAR	TRANSFER MONTH	UNIT OLD	DEPARTMENT OLD	SECTION OLD	SUB SECTION OLD	UNIT NEW	DEPARTMENT NEW	SECTION NEW	SUB SECTION NEW
        [Display(Name = "Month : ")]
        public string TransferMonth { get; set; }

        [Display(Name = "Unit New")]
        public string UnitNameNew { get; set; }

        [Display(Name = "Department New")]
        public string DepartmentNameNew { get; set; }

        //[Display(Name = "Department")]
        //public string DepartmentName { get; set; }


        [Display(Name = "Section New")]
        public string SectionIdNameNew { get; set; }

        [Display(Name = "Sub Section New")]
        public string SubSectionIdNameNew { get; set; }

        [Display(Name = "Transfer Date : ")]

        public string TransferDate { get; set; }

        
        public EmployeeModel EmployeeModel { get; set; }

        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }



    }
}