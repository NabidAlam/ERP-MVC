using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.MODEL
{
    public class LookUpModel
    {
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FirstDate { get; set; }
        public string LastDate { get; set; }
        public string LeaveYear { get; set; } //for earn leave
        public string LimitDate { get; set; } // earn leave

        public string IncrementYear { get; set; } //for increment
        public string EffectDate { get; set; } // increment effect date 
    }
}
