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
		#region Auto Generated 

		public object SaveTrServicemasterInfo(object param)
		{
			Database db = DatabaseFactory.CreateDatabase();
			object retObj = null;
			using (DbConnection connection = db.CreateConnection())
			{
				connection.Open();
				DbTransaction transaction = connection.BeginTransaction();
				try
				{
					TrServicemasterEntity trServicemasterEntity = (TrServicemasterEntity)param;
					TrServicemasterDAL trServicemasterDAL = new TrServicemasterDAL();
					retObj = (object)trServicemasterDAL.SaveTrServicemasterInfo(trServicemasterEntity, db, transaction);
					transaction.Commit();
				}
				catch
				{
					transaction.Rollback();
					throw;
				}
				finally
				{
					connection.Close();
				}
			}
			return retObj;
		}

		public object UpdateTrServicemasterInfo(object param)
		{
			Database db = DatabaseFactory.CreateDatabase();
			object retObj = null;
			using (DbConnection connection = db.CreateConnection())
			{
				connection.Open();
				DbTransaction transaction = connection.BeginTransaction();
				try
				{
					TrServicemasterEntity trServicemasterEntity = (TrServicemasterEntity)param;
					TrServicemasterDAL trServicemasterDAL = new TrServicemasterDAL();
					retObj = (object)trServicemasterDAL.UpdateTrServicemasterInfo(trServicemasterEntity, db, transaction);
					transaction.Commit();
				}
				catch
				{
					transaction.Rollback();
					throw;
				}
				finally
				{
					connection.Close();
				}
			}
			return retObj;
		}

		public object DeleteTrServicemasterInfoById(object param)
		{
			Database db = DatabaseFactory.CreateDatabase();
			object retObj = null;
			using (DbConnection connection = db.CreateConnection())
			{
				connection.Open();
				DbTransaction transaction = connection.BeginTransaction();
				try
				{
					TrServicemasterDAL trServicemasterDAL = new TrServicemasterDAL();
					retObj = (object)trServicemasterDAL.DeleteTrServicemasterInfoById(param , db, transaction);
					transaction.Commit();
				}
				catch
				{
					transaction.Rollback();
					throw;
				}
				finally
				{
					connection.Close();
				}
			}
			return retObj;
		}

		public object GetSingleTrServicemasterRecordById(object param)
		{
			object retObj = null;
			TrServicemasterDAL trServicemasterDAL = new TrServicemasterDAL();
			retObj = (object)trServicemasterDAL.GetSingleTrServicemasterRecordById(param);
			return retObj;
		}

		#endregion

	}
}

