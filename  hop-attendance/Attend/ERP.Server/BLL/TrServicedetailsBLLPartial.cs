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
	public partial class TrServicedetailsBLL
	{
		public object GetAllTrServicedetailsRecord(object param)
		{
			object retObj = null;
			TrServicedetailsDAL trServicedetailsDAL = new TrServicedetailsDAL();
			retObj = (object)trServicedetailsDAL.GetAllTrServicedetailsRecord(param);
			return retObj;
		}

	}
}

