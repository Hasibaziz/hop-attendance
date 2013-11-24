using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using ERP.Domain.Model;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;


using System.Web.Mvc;
using System.Configuration;
using ERP.Models;


namespace ERP.DAL
{
    public partial class USERS
    {
        public DataTable GetUserInfo(LoginModel param)
        {

            Database db = DatabaseFactory.CreateDatabase();
            DbConnection connection = db.CreateConnection();


            string sql = "SELECT USERID, UserName, Password, UserType FROM Usersatten.dbo.USERLOGIN WHERE UserName='" + param.UserName + "' and Password='" + param.Password + "' ";
            DbCommand dbCommand = db.GetSqlStringCommand(sql);

            dbCommand.Connection = connection;
            connection.Open();
            DataSet ds = db.ExecuteDataSet(dbCommand);
            //IDataReader ds = db.ExecuteReader(dbCommand);
            connection.Close();
            return ds.Tables[0];
        }
    }
}