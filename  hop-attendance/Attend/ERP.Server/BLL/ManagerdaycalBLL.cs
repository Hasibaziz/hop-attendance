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
    public partial class ManagerdaycalBLL
    {
        public object GetManagerdaycalRecord(object param)
        {
            object retObj = null;
            ManagerdaycalDAL objDAL = new ManagerdaycalDAL();
            retObj = (object)objDAL.GetManagerdaycalRecord(param);
            return retObj;
        }

    }
}
