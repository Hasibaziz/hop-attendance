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
   public partial class BDEmployeedaycalBLL
    {
       public object GetBDEmployeeRecord(object param)
       {
           object retObj = null;
           BDEmployeedaycalDAL objDAL = new BDEmployeedaycalDAL();
           retObj = (object)objDAL.GetBDEmployeedaycalRecord(param);
           return retObj;
       }
    }
}
