
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;


namespace ERP.MODEL
{
    public class EmployeeModel
    {
        [Display(Name = "ID : ")]
        [Required(ErrorMessage = "Please enter ID")]
        public string EmployeeId { get; set; }

        [DisplayName("Password : ")]
        public string Password { get; set; }
        public string Message { get; set; } // valid user check

        public string PermissionYn { get; set;}

        [Display(Name = "Name : ")]
        [Required(ErrorMessage = "Please enter employee name")]
        public string EmployeeName { get; set; }



        [Display(Name = "Name (Bangla) : ")]
        public string EmployeeNameBangla { get; set; }



        [Display(Name = "Father's Name : ")]
        [Required(ErrorMessage = "Please enter father's name")]
        public string FatherName { get; set; }


        [Display(Name = "Date of Birth : ")]
        [Required(ErrorMessage = "Please enter date of birth")]
        public string DateOfBirth { get; set; }



        [Display(Name = "Punch Code : ")]
        public string PunchCode { get; set; }


        [Display(Name = "Gender : ")]
        [Required(ErrorMessage = "Please select gender")]
        public string GenderId { get; set; }


        [Display(Name = "Spouse Name : ")]
        public string SpouseName { get; set; }


        [Display(Name = "Country : ")]
        [Required(ErrorMessage = "Please select country")]
        public string CountryId { get; set; }


        [Display(Name = "District : ")]
        [Required(ErrorMessage = "Please select district")]
        public string DistrictId { get; set; }


        [Display(Name = "National ID No : ")]
        public string NIDNo { get; set; }


        [Display(Name = "Emergency Contact : ")]
        public string EmergencyContactNo { get; set; }


        [Display(Name = "Passport No : ")]
        public string PassportNo { get; set; }


        [Display(Name = "Present Address : ")]
        public string PresentAddress { get; set; }



        [Display(Name = "Card No :")]
        public string CardNo { get; set; }



        [Display(Name = "Mother's Name : ")]
        public string MotherName { get; set; }



        [Display(Name = "Parent's Contact No : ")]
        public string ParentContactNo { get; set; }


        [Display(Name = "Blood Group : ")]
        //[Required(ErrorMessage = "Please select blood group")]
        public string BloodGroupId { get; set; }




        [Display(Name = "Marital Status : ")]
        //[Required(ErrorMessage = "Please select marital status")]
        public string MaritalStatusId { get; set; }




        [Display(Name = "Religion : ")]
        [Required(ErrorMessage = "Please select religion")]
        public string ReligionId { get; set; }




        [Display(Name = "Division : ")]
        [Required(ErrorMessage = "Please select division")]
        public string DivisionId { get; set; }



        [Display(Name = "TIN No : ")]
        public string TINNo { get; set; }



        [Display(Name = "Contact No : ")]
        public string ContactNo { get; set; }


        [Display(Name = "Email Address : ")]
        //[EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string EmailAddress { get; set; }



        [Display(Name = "Driving License No : ")]

        public string DrivingLicenseNo { get; set; }



        [Display(Name = "Permanent Address : ")]

        public string PermanentAddress { get; set; }


        [Display(Name = "Employeement Type : ")]
        [Required(ErrorMessage = "Please select employeement type")]
        public string EmployeementType { get; set; }



        [Display(Name = "Employee Type : ")]
        [Required(ErrorMessage = "Please select employee type")]
        public string EmployeeTypeId { get; set; }



        [Display(Name = "Joining Date : ")]
        [Required(ErrorMessage = "Please enter joining date")]
        public string JoiningDate { get; set; }



        [Display(Name = "Resign Date : ")]
        public string ResignDate { get; set; }

        [Display(Name = "Joining Designation : ")]
        [Required(ErrorMessage = "Please select joining designation")]
        public string JoiningDesignationId { get; set; }




        [Display(Name = "Unit : ")]
        [Required(ErrorMessage = "Please select unit")]
        public string UnitId { get; set; }
        public string UnitName { get; set; }




        [Display(Name = "Section : ")]
        [Required(ErrorMessage = "Please select section")]
        public string SectionId { get; set; }
        public string SectionName { get; set; }




        [Display(Name = "Grade : ")]
        //[Required(ErrorMessage = "Please select grade")]
        public string GradeId { get; set; }



        [Display(Name = "Local Guardian Name : ")]
        public string LocalGuardianName { get; set; }



        [Display(Name = "Local Guardian Phone : ")]
        public string LocalGuardianPhone { get; set; }


        [Display(Name = "Joining Salary : ")]
        [Required(ErrorMessage = "Please enter joining salary")]
        public string JoiningSalary { get; set; }



        [Display(Name = "Gross Salary : ")]
        [Required(ErrorMessage = "Please enter gross salary")]
        public string GrossSalary { get; set; }



        [Display(Name = "Bkash Account No : ")]
        public string BkashAccountNo { get; set; }



        [Display(Name = "Supervisor Name : ")]
        //[Required(ErrorMessage = "Please select supervisor name")]
        public string SuperVisorId { get; set; }



        [Display(Name = "Reference By : ")]
        public string ReferenceBy { get; set; }


        [Display(Name = "Job Type : ")]
        [Required(ErrorMessage = "Please select job type")]
        public string JobTypeId { get; set; }



        [Display(Name = "Payment Type : ")]
        [Required(ErrorMessage = "Please select payment type")]
        public string PaymentTypeId { get; set; }




        [Display(Name = "Job Confirmation Date : ")]
        public string JobConfirmationDate { get; set; }



        [Display(Name = "Retirement Date : ")]
        public string RetirementDate { get; set; }




        [Display(Name = "Present Designation : ")]
        [Required(ErrorMessage = "Please select present designation")]

      
        public string PresentDesignationId { get; set; }
        [Display(Name = "Designation : ")]
        public string PresentDesignationName { get; set; }


        [Display(Name = "Department : ")]
        [Required(ErrorMessage = "Please select department")]
        public string DepartmentId { get; set; }
        public string DepartmentName { get; set; }


        [Display(Name = "Sub Section : ")]
        [Required(ErrorMessage = "Please select sub section")]
        public string SubSectionId { get; set; }
        public string SubSectionName { get; set; }



        [Display(Name = "Shift : ")]
        [Required(ErrorMessage = "Please select shift")]
        public string ShiftId { get; set; }



        [Display(Name = "Job Location : ")]
        [Required(ErrorMessage = "Please select job location")]
        public string JobLocationId { get; set; }




        [Display(Name = "Local Guardian NID No : ")]
        public string LocalGuardianNID { get; set; }


        [Display(Name = "Tax File No :	")]
        public string TaxFileNo { get; set; }




        [Display(Name = "First Salary :	")]
        public string FirstSalary { get; set; }




        [Display(Name = "Account No : ")]
        public string AccountNo { get; set; }



        [Display(Name = "Approved By : ")]
        //[Required(ErrorMessage = "Please select approved by")]
        public string ApprovedById { get; set; }


        [Display(Name = "Weekly Holiday : ")]
        //[Required(ErrorMessage = "Please select holiday")]
        public string HolidayId { get; set; }


        [Display(Name = "Organization Name : ")]
        public string[] OrganizationName { get; set; }



        [Display(Name = "Image : ")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Image { get; set; }
        public byte[] ImageFileByte { get; set; }
        public byte[] EditImageFileByte { get; set; }
        public string ImageFileName { get; set; }
        public string ImageFileSize { get; set; }
        public string ImageFileExtension { get; set; }
        public string ImageFileNameBase64 { get; set; }
        public string EditImageFileNameBase64 { get; set; }


        [Display(Name = "Signature : ")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase Signature { get; set; }
        public byte[] SignatureFileByte { get; set; }
        public byte[] EditSignatureFileByte { get; set; }
        public string SignatureFileName { get; set; }
        public string SignatureFileSize { get; set; }
        public string SignatureFileExtension { get; set; }
        public string EditSignatureFileNameBase64 { get; set; }


        [Display(Name = "CV : ")]
        [DataType(DataType.Upload)]
        public HttpPostedFileBase CV { get; set; }
        public byte[] CVFileByte { get; set; }
        public byte[] EditCVFileByte { get; set; }
        public string CVFileName { get; set; }
        public string CVFileSize { get; set; }
        public string CVFileExtension { get; set; }


        [Display(Name = "Active : ")]
        public string ActiveYN { get; set; }
        public string InactiveYN { get; set; }
        public string Status { get; set; }


        [Display(Name = "Probation Period : ")]
        [Required(ErrorMessage = "Please select probation period ")]
        public string ProbationPeriodId { get; set; }



        [Display(Name = "Joining Date : ")]
        public string[] PreviousJobJoiningDate { get; set; }


        [Display(Name = "Designation : ")]
        public string[] PreviousJobDesignationId { get; set; }



        [Display(Name = "Salary : ")]
        public string[] PreviousJobSalary { get; set; }



        [Display(Name = "Resign Date : ")]
        public string[] PreviousJobResignDate { get; set; }
        public string PrevJobResignDate { get; set; }





        [DataType(DataType.Upload)]
        public HttpPostedFileBase[] EducationCertificate { get; set; }
        public string CertifiateFileName { get; set; }
        public string CertificateFileSize { get; set; }
        public string CertificateFileExtension { get; set; }
        public byte[] EditEducationFileByte { get; set; }





        [DataType(DataType.Upload)]
        public HttpPostedFileBase[] ExperienceCertificate { get; set; }
        public string ExpCertifiateFileName { get; set; }
        public string ExpCertificateFileSize { get; set; }
        public string ExpCertificateFileExtension { get; set; }


        [DataType(DataType.Upload)]
        public HttpPostedFileBase[] ClearanceCertificate { get; set; }
        public string ClrCertifiateFileName { get; set; }
        public string ClrCertificateFileSize { get; set; }
        public string ClrCertificateFileExtension { get; set; }




        [Display(Name = "Institute Name : ")]
        public string[] InstituteName { get; set; }


        [Display(Name = "Degree : ")]
        public string[] DegreeId { get; set; }


        [Display(Name = "Major Subject : ")]
        public string[] MajorSubjectId { get; set; }


        [Display(Name = "Year : ")]
        public string[] Year { get; set; }
        public string PassingYear { get; set; }

        [Display(Name = "CGPA / GPA / Division  : ")]
        public string[] CGPA { get; set; }


        public string[] TranId { get; set; }
        public string EditGridResignDate { get; set; }
        public byte[] EditJobCertificateFileByte { get; set; }
        public byte[] EditJobClearanceFileByte { get; set; }


        public string GridTranId { get; set; }
        public string GridInstituteName { get; set; }
        public string GridDegreeId { get; set; }
        public string GridMajorSubjectId { get; set; }
        public string GridYear { get; set; }
        public string GridCGPA { get; set; }
        public string GridCertificateFileName { get; set; }
        public byte[] GridCertificateByte { get; set; }
        public string EditGridYear { get; set; }


        public string GridOrganizationName { get; set; }
        public string GridPreviousJobJoiningDate { get; set; }
        public string GridPreviousJobDesignationId { get; set; }
        public string GridPreviousJobSalary { get; set; }
        public string GridPreviousJobResignDate { get; set; }
        public string GridPreviousJobCertificate { get; set; }
        public string GridPreviousJobClearance { get; set; }


        public string SearchEmployeeId { get; set; }
        public string SearchEmployeeName { get; set; }
        public string SearchCardNo { get; set; }
        public string SearchUnitId { get; set; }
        public string SearchDepartmentId { get; set; }
        public string SearchSectionId { get; set; }
        public string SearchSubSectionId { get; set; }
        public string SearchPunchCode { get; set; }
        public string SearchInactiveYN { get; set; }


        public string DeleteEmployeeId { get; set; }
        public string DeleteEducationYear { get; set; }
        public string DeleteResignDate { get; set; }

        public string Msg { get; set; }
        public string SerialNumber { get; set; }
        public string SearchBy { get; set; }
        public string UpdateBy { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

    
        public string LocalGuardianNIDNo { get; set; }
        public string SupervisorName { get; set; }
        public string ApproverEmployeeName { get; set; }
        public string LocalGuardianContactNo { get; set; }
        public string EmployeeTitle { get; set; }
        public byte[] EmployeeImage { get; set; }
        public string Degree { get; set; }
        public string MajorSubject { get; set; }
        public string PreviousInstituteName { get; set; }
        public string PreviousDegreeName { get; set; }
        public string PreviousMajorSubjectName { get; set; }
        public string PreviousCGPA { get; set; }
        public string PreviousPassingYear { get; set; }

        public string PreviousOrganizationName { get; set; }
        public string PreviousJoiningDate { get; set; }
        public string PreviousDesignationName { get; set; }
        public string PreviousResignDate { get; set; }
        public string PreviousGrossSalary { get; set; }
        public string EmploymentStatus { get; set; }
        public List<string> EmployeeIdList { get; set; }
        public List<EmployeeModel> EmployeeList { get; set; }
        public bool IsChecked { get; set; }
        public string SearchFromDate { get; set; }
        public string SearchToDate { get; set; }

        //[Required(ErrorMessage = "Please select team leader")]
        public string TeamLeaderId { get; set; }




        //name: mezba & date: 09.01.2019
        [Display(Name = "Transfer Date : ")]
        public string TransferDate { get; set; }

    }



    public class EmployeeDataById
    {
        public string SearchEmployeeId { get; set; }
        public string EmployeeId { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }

    }


}
