using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Domain.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using ERP.Server.DAL;

namespace ERP.Server.BLL
{
   public partial class AttendanceBLL
    {
       public object GetAttendanceInfoRecord(object param)
       {
           object retObj = null;
           AttendanceDAL objDAL = new AttendanceDAL();
           retObj = (object)objDAL.GetAttendanceInfoRecord(param);
           return retObj;
       }
      
    }
}
