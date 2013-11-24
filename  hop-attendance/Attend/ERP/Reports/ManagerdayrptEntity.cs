using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Reports
{
    public class ManagerdayrptEntity
    {
        public string EMPID { get; set; }
        public string ENAME { get; set; }
        public string SECTION { get; set; }
        public string JDate { get; set; }
        public string Status { get; set; }
        public string TTDay { get; set; }
        public string Holiday { get; set; }
        public string Present { get; set; }
        public string Absent { get; set; }
        public string CL { get; set; }
        public string SL { get; set; }
        public string ML { get; set; }
        public string EL { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}