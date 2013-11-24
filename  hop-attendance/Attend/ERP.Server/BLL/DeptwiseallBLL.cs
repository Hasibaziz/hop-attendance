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
    public partial class DeptwiseallBLL
    {
        public object GetNTDeptRecord(object param)
        {
            object retObj = null;
            DeptwiseallDAL objDAL = new DeptwiseallDAL();
            retObj = (object)objDAL.GetNTDeptRecordDAL(param);
            return retObj;
        }
        public object GetWFDeptRecord(object param)
        {
            object retObj = null;
            DeptwiseallDAL objDAL = new DeptwiseallDAL();
            retObj = (object)objDAL.GetWFDeptRecordDAL(param);
            return retObj;
        }
        public object GetAPPDeptRecord(object param)
        {
            object retObj = null;
            DeptwiseallDAL objDAL = new DeptwiseallDAL();
            retObj = (object)objDAL.GetAPPDeptRecordDAL(param);
            return retObj;
        }
        public object GetMGRDeptRecord(object param)
        {
            object retObj = null;
            DeptwiseallDAL objDAL = new DeptwiseallDAL();
            retObj = (object)objDAL.GetMGRDeptRecordDAL(param);
            return retObj;
        }
        public object GetBDDeptRecord(object param)
        {
            object retObj = null;
            DeptwiseallDAL objDAL = new DeptwiseallDAL();
            retObj = (object)objDAL.GetBDDeptRecord(param);
            return retObj;
        }

        public object GetAllDeptRecord(object param)
        {
            object retObj = null;
            DeptwiseallDAL objDAL = new DeptwiseallDAL();
            retObj = (object)objDAL.AG_GetAllDeptRecord(param);
            return retObj;
        }
    }
}
