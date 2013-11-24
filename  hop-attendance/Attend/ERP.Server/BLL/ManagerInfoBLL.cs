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
   public partial class ManagerInfoBLL
    {
       public object GetManagerInfoRecord(object param)
       {
           object retObj = null;
           ManagerInfoDAL objDAL = new ManagerInfoDAL();
           retObj = (object)objDAL.GetManagerInfoRecord(param);
           return retObj;
       }
       public object GetAllManagerInfoRecord(object param)
       {
           object retObj = null;
           ManagerInfoDAL objAllDAL = new ManagerInfoDAL();
           retObj = (object)objAllDAL.GetAllManagerInfoRecord(param);
           return retObj;
       }
       public object GetAllManagerRecord(object param)
       {
           object retObj = null;
           ManagerInfoDAL objAllDAL = new ManagerInfoDAL();
           retObj = (object)objAllDAL.GetAllManagerRecord(param);
           return retObj;
       }

       public object GetManagerReportView(object param)
       {
           object retObj = null;
           ManagerInfoDAL objAllDAL = new ManagerInfoDAL();
           retObj = (object)objAllDAL.GetManagerReportView(param);
           return retObj;
       }
    }
}
