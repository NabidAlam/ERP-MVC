using System.ComponentModel.DataAnnotations;

namespace ERP.MODEL
{
    public class OfficeTimeModel
    {
        public string OfficeTimeId { get; set; }

        [Display(Name = "Unit")]
        public string UnitId { get; set; }

        [Display(Name = "Department")]
        public string DepartmentId { get; set; }

        [Display(Name = "Section")]
        public string SectionId { get; set; }

        [Display(Name = "Sub Section")]
        public string SubSectionId { get; set; }

        [Display(Name = "First In Time")]
        public string FirstInTime { get; set; }

        [Display(Name = "Last Out Time")]
        public string LastOutTime { get; set; }

        [Display(Name = "Lunch In Time")]
        public string LunchInTime { get; set; }

        [Display(Name = "Lunch Out Time")]
        public string LunchOutTime { get; set; }

        [Display(Name = "All")]
        public string CheckAll { get; set; }


        public string UnitName { get; set; }
        public string DepartmentName { get; set; }
        public string SectionName { get; set; }
        public string SubSectionName { get; set; }


        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string SerialNumber { get; set; }
    }
}
