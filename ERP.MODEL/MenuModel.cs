using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class MenuModel
    {
        public string MenuId { get; set; }
        public string ParentId { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public string SoftwareId { get; set; }
        public string ActiveYN { get; set; }
        public string UpdateBy { get; set; }
        public string UpdateDate { get; set; }
        public string HeadOfficeId { get; set; }
        public string BranchOfficeId { get; set; }
        public string EmployeeId { get; set; }
        public string MenuIcon { get; set; }

        public byte[] ImageFileByte { get; set; }
        public string ImageFileNameBase64 { get; set; }
    } 
}
