using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model
{
    public class AttendanceEntity
    {
        public string SL { get; set; }
        public string PDate { set; get; }
        public string EMPID { set; get; }
        public string EName { set; get; }
        public string Designation { set; get; }
        public string DeptName { set; get; }
        public string Intime { set; get; }
        public string Outtime { set; get; }
        public string Status { set; get; }
        public string StartDate { set; get; }
        public string EndDate { set; get; }
    }
}
