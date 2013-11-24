using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Collections;
using System.Configuration;


using System.Collections.Generic;
using System.Linq;




namespace ERP.Conn
{
    public class Malconn
    {
        SqlConnection iConnection = new SqlConnection();
        SqlCommand iCommand = new SqlCommand();
        SqlDataAdapter iDataAdapter = new SqlDataAdapter();
        //SqlDataReader iDataReader;

        DataSet iDataSet = new DataSet();


        public bool Connect_data()
        {
            bool isConnect = false;
            try
            {
                //iConnection.ConnectionString = "Data Source=USER-PC//HASIB;Initial Catalog=NUBD;Persist Security Info=True;User ID=sa;Password=123";
                iConnection.ConnectionString = ConfigurationManager.ConnectionStrings["OBCDBConnectionString"].ToString();
                // Data Source=USER-PC\HASIB;Initial Catalog=nubd;Integrated Security=True
                iConnection.Open();
                isConnect = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return isConnect;

        }
        public DataTable ExecuteQueryData01(string SQLstat)
        {
            DataTable dtResult = new DataTable();

            try
            {
                if (iDataAdapter == null)
                {
                    throw new System.ObjectDisposedException(GetType().FullName);
                }

                SqlCommand comm = iDataAdapter.SelectCommand;
                comm.CommandText = SQLstat;
                comm.CommandType = CommandType.Text;

                iDataAdapter.Fill(dtResult);
            }
            catch (Exception ex)
            {
                throw (ex);
            }

            return dtResult;
        } //ExecuteStoredProcedureDataTable

        public DataTable ExecuteQueryData(string strProcedureName)
        {
            //if (iDataAdapter == null)
            //{
            //    throw new System.ObjectDisposedException(GetType().FullName);
            //} // end if

            DataTable dtResult = new DataTable();
            try
            {
                if (Connect_data())
                {
                    //Connect_data conn = new Connect_data();
                    iCommand.Connection = iConnection;
                    //iCommand.CommandText = strProcedureName;
                    iCommand.CommandType = CommandType.Text;
                                  
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    if (iConnection != null && iConnection.State == ConnectionState.Open)
            //    {
            //        iConnection.Close();
            //    }
            //}
            return dtResult;  ///for DataTable                       
        } // end ExecuteStoredProcedureDataTabl

        public DataSet ExecuteSelectSQL(String SQL, List<SqlParameter> parameters, bool isStoredProcedure)
        {
            DataSet ds = new DataSet();
            try
            {               
                if (Connect_data())
                {
                    //conn.Open();
                    
                    SqlCommand command = new SqlCommand();
                    command.Connection = iConnection;
                    command.CommandText = SQL;
                    command.CommandType = isStoredProcedure ? CommandType.StoredProcedure : CommandType.Text;
                    if (parameters != null && parameters.Count > 0)
                        command.Parameters.AddRange(parameters.ToArray());
                
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(ds);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (iConnection  != null && (iConnection .State == ConnectionState.Open))
                {
                    iConnection .Close();
                }
            }
            return ds;
        }

    }
}