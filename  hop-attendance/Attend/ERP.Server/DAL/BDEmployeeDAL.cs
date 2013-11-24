using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using ERP.Domain.Model;
using Microsoft.Practices.EnterpriseLibrary.Data;
using ERP.Conn;

namespace ERP.Server.DAL
{
   public partial class BDEmployeeDAL
    {
       public DataTable GetBDEmployeeRecord(object param)
       {
           Malconn conn = new Malconn();
           
           //Database db = DatabaseFactory.CreateDatabase();
           BDEmployeeEntity objAtten = (BDEmployeeEntity)param;
           //string sql = "select CONVERT(DATE, MBDPayroll.dbo.Attendance.Atten_Date,103) AS PDate";
           string sql = "select ( CAST(DATEPART(YY, MBDPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10))+ LEFT('-' + CAST(DATEPART(mm, MBDPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4) + LEFT('-' + CAST(DATEPART(dd, MBDPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
           sql=sql+"    , MBDPayroll.dbo.Attendance.Emp_Id as EMPID, MBDPayroll.dbo.Employee_Info.Ename AS EName, MBDPayroll.dbo.Employee_Info.Designation AS Designation, MBDPayroll.dbo.Employee_Info.SECTION AS DeptName, (substring(MBDPayroll.dbo.Attendance.IN_time,1,2)+'.'+substring(MBDPayroll.dbo.Attendance.IN_time,3,4)) AS Intime, (substring(MBDPayroll.dbo.Attendance.Out_Time,1,2)+'.'+substring(MBDPayroll.dbo.Attendance.Out_Time,3,4)) AS Outtime, MBDPayroll.dbo.Attendance.Present_Absent as Status from MBDPayroll.dbo.Attendance, MBDPayroll.dbo.Employee_Info";
           sql = sql + " Where MBDPayroll.dbo.Attendance.Emp_Id=MBDPayroll.dbo.Employee_Info.EmpID";
           sql = sql + " AND MBDPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT MBDPayroll.dbo.Terminate_Resign.EmpID FROM MBDPayroll.dbo.Terminate_Resign)";
           sql = sql + " AND MBDPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)";
           //sql = sql + " ORDER BY MBDPayroll.dbo.Employee_Info.Section ASC,MBDPayroll.dbo.Employee_Info.Designation DESC, CONVERT(DATE,MBDPayroll.dbo.Attendance.Atten_Date,103) ASC";
           sql = sql + " ORDER BY MBDPayroll.dbo.Employee_Info.Section ASC, MBDPayroll.dbo.Attendance.Emp_Id ASC, CONVERT(DATE,MBDPayroll.dbo.Attendance.Atten_Date,103) ASC";
           DataSet ds =new DataSet();
           ds = conn.ExecuteSelectSQL(sql,null, false);
           //DbCommand dbCommand = db.GetSqlStringCommand(sql);
           //DataSet ds = db.ExecuteDataSet(dbCommand);
           //return conn.ExecuteQueryData(sql);  
           return ds.Tables[0];
       }

       public DataTable GetBDEmployeewiseRecord(object param)
       {
           Malconn conn = new Malconn();

           //Database db = DatabaseFactory.CreateDatabase();
           BDEmployeeEntity objAtten = (BDEmployeeEntity)param;
           //string sql = "select CONVERT(DATE, MBDPayroll.dbo.Attendance.Atten_Date,103) AS PDate";
           string sql = "select ( CAST(DATEPART(YY, MBDPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10))+ LEFT('-' + CAST(DATEPART(mm, MBDPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4) + LEFT('-' + CAST(DATEPART(dd, MBDPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
           sql=sql+"    , MBDPayroll.dbo.Attendance.Emp_Id as EMPID, MBDPayroll.dbo.Employee_Info.Ename AS EName, MBDPayroll.dbo.Employee_Info.Designation AS Designation, MBDPayroll.dbo.Employee_Info.SECTION AS DeptName, (substring(MBDPayroll.dbo.Attendance.IN_time,1,2)+'.'+substring(MBDPayroll.dbo.Attendance.IN_time,3,4)) AS Intime, (substring(MBDPayroll.dbo.Attendance.Out_Time,1,2)+'.'+substring(MBDPayroll.dbo.Attendance.Out_Time,3,4)) AS Outtime, MBDPayroll.dbo.Attendance.Present_Absent as Status from MBDPayroll.dbo.Attendance, MBDPayroll.dbo.Employee_Info";
           sql = sql + " Where MBDPayroll.dbo.Attendance.Emp_Id=MBDPayroll.dbo.Employee_Info.EmpID";
           sql = sql + " AND MBDPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT MBDPayroll.dbo.Terminate_Resign.EmpID FROM MBDPayroll.dbo.Terminate_Resign)";
           sql = sql + " AND MBDPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) AND MBDPayroll.dbo.Employee_Info.EmpID= '" + objAtten.EMPID + "'";
           sql = sql + " ORDER BY CONVERT(DATE,MBDPayroll.dbo.Attendance.Atten_Date,103) ASC";
           DataSet ds = new DataSet();
           ds = conn.ExecuteSelectSQL(sql, null, false);
           //DbCommand dbCommand = db.GetSqlStringCommand(sql);
           //DataSet ds = db.ExecuteDataSet(dbCommand);
           //return conn.ExecuteQueryData(sql);  
           return ds.Tables[0];
       }

       public DataTable GetAllBDEmployeewiseRecord(object param)
       {
           Malconn conn = new Malconn();
           string sql = "SELECT Employee_Info.EmpID AS EMPID, Employee_Info.Ename AS EName from MBDPayroll.dbo.Employee_Info WHERE MBDPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT Terminate_Resign.EmpID FROM MBDPayroll.dbo.Terminate_Resign)";
           sql = sql + " ORDER BY MBDPayroll.dbo.Employee_Info.EmpID ASC";
           DataSet ds = new DataSet();
           ds = conn.ExecuteSelectSQL(sql, null, false);           
           return ds.Tables[0];
       }


    }
}
