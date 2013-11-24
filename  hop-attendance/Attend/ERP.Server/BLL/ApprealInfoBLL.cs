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
    public partial class ApprealInfoBLL
    {
        public object GetApprealInfoRecord(object param)
        {
            object retObj = null;
            ApprealInfoDAL objDAL = new ApprealInfoDAL();
            retObj = (object)objDAL.GetApprealInfoRecord(param);
            return retObj;
        }
        public object GetApprealEMPWiseRecord(object param)
        {
            object retObj = null;
            ApprealInfoDAL objDAL = new ApprealInfoDAL();
            retObj = (object)objDAL.GetApprealEMPWiseRecord(param);
            return retObj;
        }
        public object GetAllAppearlRecord(object param)
        {
            object retObj = null;
            ApprealInfoDAL objDAL = new ApprealInfoDAL();
            retObj = (object)objDAL.GetAllAppearlRecord(param);
            return retObj;
        }
    }
}
