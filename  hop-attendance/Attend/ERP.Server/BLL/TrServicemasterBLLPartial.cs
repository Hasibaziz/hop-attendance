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
	public partial class TrServicemasterBLL
	{
		public object GetAllTrServicemasterRecord(object param)
		{
			object retObj = null;
			TrServicemasterDAL trServicemasterDAL = new TrServicemasterDAL();
			retObj = (object)trServicemasterDAL.GetAllTrServicemasterRecord(param);
			return retObj;
		}

	}
}

