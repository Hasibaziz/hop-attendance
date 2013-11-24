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
		#region Auto Generated 

		public object SaveTrServicedetailsInfo(object param)
		{
			Database db = DatabaseFactory.CreateDatabase();
			object retObj = null;
			using (DbConnection connection = db.CreateConnection())
			{
				connection.Open();
				DbTransaction transaction = connection.BeginTransaction();
				try
				{
					TrServicedetailsEntity trServicedetailsEntity = (TrServicedetailsEntity)param;
					TrServicedetailsDAL trServicedetailsDAL = new TrServicedetailsDAL();
					retObj = (object)trServicedetailsDAL.SaveTrServicedetailsInfo(trServicedetailsEntity, db, transaction);
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

		public object UpdateTrServicedetailsInfo(object param)
		{
			Database db = DatabaseFactory.CreateDatabase();
			object retObj = null;
			using (DbConnection connection = db.CreateConnection())
			{
				connection.Open();
				DbTransaction transaction = connection.BeginTransaction();
				try
				{
					TrServicedetailsEntity trServicedetailsEntity = (TrServicedetailsEntity)param;
					TrServicedetailsDAL trServicedetailsDAL = new TrServicedetailsDAL();
					retObj = (object)trServicedetailsDAL.UpdateTrServicedetailsInfo(trServicedetailsEntity, db, transaction);
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

		public object DeleteTrServicedetailsInfoById(object param)
		{
			Database db = DatabaseFactory.CreateDatabase();
			object retObj = null;
			using (DbConnection connection = db.CreateConnection())
			{
				connection.Open();
				DbTransaction transaction = connection.BeginTransaction();
				try
				{
					TrServicedetailsDAL trServicedetailsDAL = new TrServicedetailsDAL();
					retObj = (object)trServicedetailsDAL.DeleteTrServicedetailsInfoById(param , db, transaction);
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

		public object GetSingleTrServicedetailsRecordById(object param)
		{
			object retObj = null;
			TrServicedetailsDAL trServicedetailsDAL = new TrServicedetailsDAL();
			retObj = (object)trServicedetailsDAL.GetSingleTrServicedetailsRecordById(param);
			return retObj;
		}

		#endregion

	}
}

