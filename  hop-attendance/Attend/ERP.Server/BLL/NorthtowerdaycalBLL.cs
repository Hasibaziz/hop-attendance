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
   public partial class NorthtowerdaycalBLL
    {
       public object GetNorthtowerdaycalRecord(object param)
       {
           object retObj = null;
           NorthtowerdaycalDAL objDAL = new NorthtowerdaycalDAL();
           retObj = (object)objDAL.GetNorthtowerdaycalRecord(param);
           return retObj;
       }
    }
}
