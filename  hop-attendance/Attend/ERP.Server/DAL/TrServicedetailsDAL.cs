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
	public partial class TrServicedetailsDAL
	{
		#region Auto Generated 

		public bool SaveTrServicedetailsInfo(TrServicedetailsEntity trServicedetailsEntity, Database db, DbTransaction transaction)
		{
			string sql = "INSERT INTO TR_ServiceDetails ( SrviceNameID, DetailsName, GovFee, ServiceFee, OthersFee, FixedFigure, CC, Sit, UserID, EnterDate, UpdateDate) VALUES (  @Srvicenameid,  @Detailsname,  @Govfee,  @Servicefee,  @Othersfee,  @Fixedfigure,  @Cc,  @Sit,  @Userid,  @Enterdate,  @Updatedate )";
			DbCommand dbCommand = db.GetSqlStringCommand(sql);

			db.AddInParameter(dbCommand, "Srvicenameid", DbType.String, trServicedetailsEntity.Srvicenameid);
			db.AddInParameter(dbCommand, "Detailsname", DbType.String, trServicedetailsEntity.Detailsname);
			db.AddInParameter(dbCommand, "Govfee", DbType.String, trServicedetailsEntity.Govfee);
			db.AddInParameter(dbCommand, "Servicefee", DbType.String, trServicedetailsEntity.Servicefee);
			db.AddInParameter(dbCommand, "Othersfee", DbType.String, trServicedetailsEntity.Othersfee);
			db.AddInParameter(dbCommand, "Fixedfigure", DbType.String, trServicedetailsEntity.Fixedfigure);
			db.AddInParameter(dbCommand, "Cc", DbType.String, trServicedetailsEntity.Cc);
			db.AddInParameter(dbCommand, "Sit", DbType.String, trServicedetailsEntity.Sit);
			db.AddInParameter(dbCommand, "Userid", DbType.String, trServicedetailsEntity.Userid);
			db.AddInParameter(dbCommand, "Enterdate", DbType.String, trServicedetailsEntity.Enterdate);
			db.AddInParameter(dbCommand, "Updatedate", DbType.String, trServicedetailsEntity.Updatedate);

			db.ExecuteNonQuery(dbCommand, transaction);
			return true;		}

		public bool UpdateTrServicedetailsInfo(TrServicedetailsEntity trServicedetailsEntity, Database db, DbTransaction transaction)
		{
			string sql = "UPDATE TR_ServiceDetails SET SrviceNameID= @Srvicenameid, DetailsName= @Detailsname, GovFee= @Govfee, ServiceFee= @Servicefee, OthersFee= @Othersfee, FixedFigure= @Fixedfigure, CC= @Cc, Sit= @Sit, UserID= @Userid, EnterDate= @Enterdate, UpdateDate= @Updatedate WHERE Id=@Id";
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "Id",DbType.String, trServicedetailsEntity.Id);
			db.AddInParameter(dbCommand, "Srvicenameid", DbType.String, trServicedetailsEntity.Srvicenameid);
			db.AddInParameter(dbCommand, "Detailsname", DbType.String, trServicedetailsEntity.Detailsname);
			db.AddInParameter(dbCommand, "Govfee", DbType.String, trServicedetailsEntity.Govfee);
			db.AddInParameter(dbCommand, "Servicefee", DbType.String, trServicedetailsEntity.Servicefee);
			db.AddInParameter(dbCommand, "Othersfee", DbType.String, trServicedetailsEntity.Othersfee);
			db.AddInParameter(dbCommand, "Fixedfigure", DbType.String, trServicedetailsEntity.Fixedfigure);
			db.AddInParameter(dbCommand, "Cc", DbType.String, trServicedetailsEntity.Cc);
			db.AddInParameter(dbCommand, "Sit", DbType.String, trServicedetailsEntity.Sit);
			db.AddInParameter(dbCommand, "Userid", DbType.String, trServicedetailsEntity.Userid);
			db.AddInParameter(dbCommand, "Enterdate", DbType.String, trServicedetailsEntity.Enterdate);
			db.AddInParameter(dbCommand, "Updatedate", DbType.String, trServicedetailsEntity.Updatedate);

			db.ExecuteNonQuery(dbCommand, transaction);
			return true;
		}

		public bool DeleteTrServicedetailsInfoById(object param, Database db, DbTransaction transaction)
		{
			string sql = "DELETE FROM TR_ServiceDetails WHERE Id=@Id";
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "Id", DbType.String, param);

			db.ExecuteNonQuery(dbCommand, transaction);
			return true;
		}

		public TrServicedetailsEntity GetSingleTrServicedetailsRecordById(object param)
		{
			Database db = DatabaseFactory.CreateDatabase();
			string sql = "SELECT ID, SrviceNameID, DetailsName, GovFee, ServiceFee, OthersFee, FixedFigure, CC, Sit, UserID, EnterDate, UpdateDate FROM TR_ServiceDetails WHERE Id=@Id";
			DbCommand dbCommand = db.GetSqlStringCommand(sql);
			db.AddInParameter(dbCommand, "Id", DbType.String, param);
			TrServicedetailsEntity trServicedetailsEntity = null;
			using (IDataReader dataReader = db.ExecuteReader(dbCommand))
			{
				if (dataReader.Read())
				{
					trServicedetailsEntity = new TrServicedetailsEntity();
					if (dataReader["ID"] != DBNull.Value)
					{
						trServicedetailsEntity.Id = dataReader["ID"].ToString();
					}
					if (dataReader["SrviceNameID"] != DBNull.Value)
					{
						trServicedetailsEntity.Srvicenameid = dataReader["SrviceNameID"].ToString();
					}
					if (dataReader["DetailsName"] != DBNull.Value)
					{
						trServicedetailsEntity.Detailsname = dataReader["DetailsName"].ToString();
					}
					if (dataReader["GovFee"] != DBNull.Value)
					{
						trServicedetailsEntity.Govfee = dataReader["GovFee"].ToString();
					}
					if (dataReader["ServiceFee"] != DBNull.Value)
					{
						trServicedetailsEntity.Servicefee = dataReader["ServiceFee"].ToString();
					}
					if (dataReader["OthersFee"] != DBNull.Value)
					{
						trServicedetailsEntity.Othersfee = dataReader["OthersFee"].ToString();
					}
					if (dataReader["FixedFigure"] != DBNull.Value)
					{
						trServicedetailsEntity.Fixedfigure =Convert.ToBoolean( dataReader["FixedFigure"].ToString());
					}
					if (dataReader["CC"] != DBNull.Value)
					{
						trServicedetailsEntity.Cc = dataReader["CC"].ToString();
					}
					if (dataReader["Sit"] != DBNull.Value)
					{
						trServicedetailsEntity.Sit = dataReader["Sit"].ToString();
					}
					if (dataReader["UserID"] != DBNull.Value)
					{
						trServicedetailsEntity.Userid = dataReader["UserID"].ToString();
					}
					if (dataReader["EnterDate"] != DBNull.Value)
					{
						trServicedetailsEntity.Enterdate = dataReader["EnterDate"].ToString();
					}
					if (dataReader["UpdateDate"] != DBNull.Value)
					{
						trServicedetailsEntity.Updatedate = dataReader["UpdateDate"].ToString();
					}
				}
			}
			return trServicedetailsEntity;
		}

		#endregion

	}
}

