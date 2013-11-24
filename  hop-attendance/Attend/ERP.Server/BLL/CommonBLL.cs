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
    public class CommonBLL
    {
        public object GetSaleCarDetailsInfo(object param)
        {
            object retObj = null;
            CommomDAL commomDAL = new CommomDAL();
            retObj = (object)commomDAL.GetSaleCarDetailsInfo(param);
            return retObj;
        }
    }
}
