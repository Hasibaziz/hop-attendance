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
   public partial class WelformdaycalBLL
    {
       public object GetWelformdaycalRecord(object param)
       {
           object retObj = null;
           WelformdaycalDAL objDAL = new WelformdaycalDAL();
           retObj = (object)objDAL.GetWelformdaycalRecord(param);
           return retObj;
       }

    }
}
