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
    public partial class WelformattenBLL
    {
        public object GetWelformInfoRecord(object param)
        {
            object retObj = null;
            WelformInfoDAL objDAL = new WelformInfoDAL();
            retObj = (object)objDAL.GetWelformInfoRecord(param);
            return retObj;
        }
        public object GetWelformWiseRecord(object param)
        {
            object retObj = null;
            WelformInfoDAL objDAL = new WelformInfoDAL();
            retObj = (object)objDAL.GetWelformWiseRecord(param);
            return retObj;
        }
        public object GetAllWelformWiseRecord(object param)
        {
            object retObj = null;
            WelformInfoDAL objDAL = new WelformInfoDAL();
            retObj = (object)objDAL.GetAllWelformWiseRecord(param);
            return retObj;
        }
    }
}
