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
   public partial class BDEmployeeBLL
    {
      
       public object GetBDEmployeeRecord(object param)
       {
           object retObj = null;
           BDEmployeeDAL objDAL = new BDEmployeeDAL();
           retObj = (object)objDAL.GetBDEmployeeRecord(param);
           return retObj;
       }
       public object GetBDEmployeewiseRecord(object param)
       {
           object retObj = null;
           BDEmployeeDAL objDAL = new BDEmployeeDAL();
           retObj = (object)objDAL.GetBDEmployeewiseRecord(param);
           return retObj;
       }
       public object GetAllBDEmployeewiseRecord(object param)
       {
           object retObj = null;
           BDEmployeeDAL objDAL = new BDEmployeeDAL();
           retObj = (object)objDAL.GetAllBDEmployeewiseRecord(param);
           return retObj;
       }

    }
}
