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
   public partial class AppearldaycalBLL
    {
       public object GetAppearldaycalRecord(object param)
       {
           object retObj = null;
           AppearldaycalDAL objDAL = new AppearldaycalDAL();
           retObj = (object)objDAL.GetAppearldaycalRecord(param);
           return retObj;
       }
    }
}
