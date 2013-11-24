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
		#region Auto Generated 

		public bool SaveTrServicemasterInfo(TrServicemasterEntity trServicemasterEntity, Database db, DbTransaction transaction)
		{
			string sql = "INSERT INTO TR_ServiceMaster ( ServiceName, Description) VALUES (  @Servicename,  @Description )";
			DbCommand dbCommand = db.GetSqlStringCommand(sql);

			db.AddInParameter(dbCommand, "Servicename", DbType.String, trServicemasterEntity.Servicename);
			db.AddInParameter(dbCommand, "Description", DbType.String, trServicemasterEntity.Description);

			db.ExecuteNonQuery(dbCommand, transaction);
			return true;		}

		public bool UpdateTrServicemasterInfo(TrServicemasterEntity trServicemasterEntity, Database db, DbTransaction transaction)
		{
			string sql = "UPDATE TR_ServiceMaster SET ServiceName= @Servicename, Description= @Description WHERE Id=@Id";
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "Id",DbType.String, trServicemasterEntity.Id);
			db.AddInParameter(dbCommand, "Servicename", DbType.String, trServicemasterEntity.Servicename);
			db.AddInParameter(dbCommand, "Description", DbType.String, trServicemasterEntity.Description);

			db.ExecuteNonQuery(dbCommand, transaction);
			return true;
		}

		public bool DeleteTrServicemasterInfoById(object param, Database db, DbTransaction transaction)
		{
			string sql = "DELETE FROM TR_ServiceMaster WHERE Id=@Id";
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "Id", DbType.String, param);

			db.ExecuteNonQuery(dbCommand, transaction);
			return true;
		}

		public TrServicemasterEntity GetSingleTrServicemasterRecordById(object param)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = "SELECT ID, ServiceName, Description FROM TR_ServiceMaster WHERE Id=@Id";
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "Id", DbType.String, param);
			TrServicemasterEntity trServicemasterEntity = null;
			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				if (dataReader.Read())
				{
					trServicemasterEntity = new TrServicemasterEntity();
					if (dataReader["ID"] != DBNull.Value)
					{
						trServicemasterEntity.Id = dataReader["ID"].ToString();
					}
					if (dataReader["ServiceName"] != DBNull.Value)
					{
						trServicemasterEntity.Servicename = dataReader["ServiceName"].ToString();
					}
					if (dataReader["Description"] != DBNull.Value)
					{
						trServicemasterEntity.Description = dataReader["Description"].ToString();
					}
				}
			}
			return trServicemasterEntity;
		}

		#endregion

	}
}

