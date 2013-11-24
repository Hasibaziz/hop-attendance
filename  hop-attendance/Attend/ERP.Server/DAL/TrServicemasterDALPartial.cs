using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ERP.Domain.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace ERP.Server.DAL
{
	public partial class TrServicemasterDAL
	{
		public DataTable GetAllTrServicemasterRecord(object param)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = "SELECT ID, ServiceName, Description FROM TR_ServiceMaster";
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			DataSet ds = db.ExecuteDataSet(dbCommand);
			return ds.Tables[0];
		}

	}
}

