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
   public partial class EmployeewiseBLL
    {
       public object GetEmployeewiseRecord(object param)
       {
           object retObj = null;
           EmployeewiseDAL objDAL = new EmployeewiseDAL();
           retObj = (object)objDAL.GetEmployeewiseRecord(param);
           return retObj;
       }
       public object GetAllEmployeewiseRecord(object param)
       {
           object retObj = null;
           EmployeewiseDAL objAllDAL = new EmployeewiseDAL();
           retObj = (object)objAllDAL.GetAllEmployeewiseRecord(param);
           return retObj;
       }
    }
}
