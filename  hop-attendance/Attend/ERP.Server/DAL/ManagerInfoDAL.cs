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
   public partial class ManagerInfoDAL
    {
       public DataTable GetManagerInfoRecord(object param)
       {
           Database db = DatabaseFactory.CreateDatabase();
           ManagerinfoEntity objAtten = (ManagerinfoEntity)param;
           //string sql = "select CONVERT(DATE,ManagerPayroll.dbo.Attendance.Atten_Date,103) AS PDate, ManagerPayroll.dbo.Attendance.Emp_Id as EMPID, ManagerPayroll.dbo.Employee_Info.Ename AS EName, ManagerPayroll.dbo.Employee_Info.Designation AS Designation, ManagerPayroll.dbo.Employee_Info.SECTION AS DeptName, (substring(ManagerPayroll.dbo.Attendance.IN_time,1,2)+'.'+substring(ManagerPayroll.dbo.Attendance.IN_time,3,4)) AS Intime, (substring(ManagerPayroll.dbo.Attendance.Out_Time,1,2)+'.'+substring(ManagerPayroll.dbo.Attendance.Out_Time,3,4)) AS Outtime, ManagerPayroll.dbo.Attendance.Present_Absent as Status from ManagerPayroll.dbo.Attendance, ManagerPayroll.dbo.Employee_Info";
           string sql = "select ( CAST(DATEPART(YY,ManagerPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10))+LEFT('-' + CAST(DATEPART(mm, ManagerPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)+LEFT('-' + CAST(DATEPART(dd, ManagerPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
           sql=sql+" , ManagerPayroll.dbo.Attendance.Emp_Id as EMPID, ManagerPayroll.dbo.Employee_Info.Ename AS EName, ManagerPayroll.dbo.Employee_Info.Designation AS Designation, ManagerPayroll.dbo.Employee_Info.SECTION AS DeptName, (substring(ManagerPayroll.dbo.Attendance.IN_time,1,2)+'.'+substring(ManagerPayroll.dbo.Attendance.IN_time,3,4)) AS Intime, (substring(ManagerPayroll.dbo.Attendance.Out_Time,1,2)+'.'+substring(ManagerPayroll.dbo.Attendance.Out_Time,3,4)) AS Outtime, ManagerPayroll.dbo.Attendance.Present_Absent as Status from ManagerPayroll.dbo.Attendance, ManagerPayroll.dbo.Employee_Info";
           sql = sql + " Where ManagerPayroll.dbo.Attendance.Emp_Id=ManagerPayroll.dbo.Employee_Info.EmpID";
           sql = sql + " and ManagerPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT ManagerPayroll.dbo.Terminate_Resign.EmpID FROM ManagerPayroll.dbo.Terminate_Resign)";
           //sql01 = sql01 + " and EmpID NOT IN (SELECT EmpID FROM Terminate_Resign)";
           //sql = sql + " and ManagerPayroll.dbo.Employee_Info.Grade LIKE 'H%' AND ManagerPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) ORDER BY ManagerPayroll.dbo.Employee_Info.Section ASC";
           sql = sql + " and ManagerPayroll.dbo.Employee_Info.Grade LIKE '%[HE]%' AND ManagerPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) ";
           //sql = sql + " AND ManagerPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) ";
           //sql=sql+" ORDER BY CONVERT(DATE,ManagerPayroll.dbo.Attendance.Atten_Date,103) ASC, ManagerPayroll.dbo.Employee_Info.empid ASC";
           sql = sql + " ORDER BY ManagerPayroll.dbo.Employee_Info.SECTION ASC, ManagerPayroll.dbo.Attendance.Emp_Id ASC";
           DbCommand dbCommand = db.GetSqlStringCommand(sql);
           DataSet ds = db.ExecuteDataSet(dbCommand);
           return ds.Tables[0];
       }
       public DataTable GetAllManagerInfoRecord(object param)
       {
           Database db = DatabaseFactory.CreateDatabase();
           ManagerinfoEntity objAtten = (ManagerinfoEntity)param;
           string sql = "select ( CAST(DATEPART(YY,ManagerPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10))+LEFT('-' + CAST(DATEPART(mm, ManagerPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)+LEFT('-' + CAST(DATEPART(dd, ManagerPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
           sql=sql+"    , ManagerPayroll.dbo.Attendance.Emp_Id as EMPID, ManagerPayroll.dbo.Employee_Info.Ename AS EName, ManagerPayroll.dbo.Employee_Info.Designation AS Designation, ManagerPayroll.dbo.Employee_Info.SECTION AS DeptName, (substring(ManagerPayroll.dbo.Attendance.IN_time,1,2)+'.'+substring(ManagerPayroll.dbo.Attendance.IN_time,3,4)) AS Intime, (substring(ManagerPayroll.dbo.Attendance.Out_Time,1,2)+'.'+substring(ManagerPayroll.dbo.Attendance.Out_Time,3,4)) AS Outtime, ManagerPayroll.dbo.Attendance.Present_Absent as Status from ManagerPayroll.dbo.Attendance, ManagerPayroll.dbo.Employee_Info";
           sql = sql + " Where ManagerPayroll.dbo.Attendance.Emp_Id=ManagerPayroll.dbo.Employee_Info.EmpID";
           sql = sql + " and ManagerPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT ManagerPayroll.dbo.Terminate_Resign.EmpID FROM ManagerPayroll.dbo.Terminate_Resign)";
           //sql = sql + " and ManagerPayroll.dbo.Employee_Info.Grade LIKE 'H%' AND ManagerPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) ORDER BY ManagerPayroll.dbo.Employee_Info.Section ASC";
           sql = sql + " and ManagerPayroll.dbo.Employee_Info.Grade LIKE '%[HE]%' AND ManagerPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) AND ManagerPayroll.dbo.Attendance.Emp_Id= '" + objAtten.EMPID + "'";
           //sql = sql + " AND ManagerPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) AND ManagerPayroll.dbo.Attendance.Emp_Id= '" + objAtten.EMPID + "'";
           //sql = sql +" ORDER BY ManagerPayroll.dbo.Employee_Info.empid ASC";
           sql = sql + " ORDER BY CONVERT(DATE,ManagerPayroll.dbo.Attendance.Atten_Date,103) ASC";
           DbCommand dbCommand = db.GetSqlStringCommand(sql);
           DataSet ds = db.ExecuteDataSet(dbCommand);
           return ds.Tables[0];
       }
       public DataTable GetAllManagerRecord(object param)
       {
           Database db = DatabaseFactory.CreateDatabase();
           string sql = "SELECT ManagerPayroll.dbo.Employee_Info.EmpID AS EMPID, ManagerPayroll.dbo.Employee_Info.Ename AS EName from ManagerPayroll.dbo.Employee_Info";
           sql = sql + " WHERE ManagerPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT ManagerPayroll.dbo.Terminate_Resign.EmpID FROM ManagerPayroll.dbo.Terminate_Resign)";
           sql = sql + " ORDER BY ManagerPayroll.dbo.Employee_Info.EmpID ASC";
           DbCommand dbCommand = db.GetSqlStringCommand(sql);
           DataSet ds = db.ExecuteDataSet(dbCommand);
           return ds.Tables[0];
       }




       public DataTable GetManagerReportView(object param)
       {
           Database db = DatabaseFactory.CreateDatabase();
           ManagerinfoEntity objAtten = (ManagerinfoEntity)param;
           //string sql = "select CONVERT(DATE,ManagerPayroll.dbo.Attendance.Atten_Date,103) AS PDate, ManagerPayroll.dbo.Attendance.Emp_Id as EMPID, ManagerPayroll.dbo.Employee_Info.Ename AS EName, ManagerPayroll.dbo.Employee_Info.Designation AS Designation, ManagerPayroll.dbo.Employee_Info.SECTION AS DeptName, (substring(ManagerPayroll.dbo.Attendance.IN_time,1,2)+'.'+substring(ManagerPayroll.dbo.Attendance.IN_time,3,4)) AS Intime, (substring(ManagerPayroll.dbo.Attendance.Out_Time,1,2)+'.'+substring(ManagerPayroll.dbo.Attendance.Out_Time,3,4)) AS Outtime, ManagerPayroll.dbo.Attendance.Present_Absent as Status from ManagerPayroll.dbo.Attendance, ManagerPayroll.dbo.Employee_Info";
           string sql = "select ( CAST(DATEPART(YY,ManagerPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10))+LEFT('-' + CAST(DATEPART(mm, ManagerPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)+LEFT('-' + CAST(DATEPART(dd, ManagerPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
           sql = sql + " , ManagerPayroll.dbo.Attendance.Emp_Id as EMPID, ManagerPayroll.dbo.Employee_Info.Ename AS EName, ManagerPayroll.dbo.Employee_Info.Designation AS Designation, ManagerPayroll.dbo.Employee_Info.SECTION AS DeptName, (substring(ManagerPayroll.dbo.Attendance.IN_time,1,2)+'.'+substring(ManagerPayroll.dbo.Attendance.IN_time,3,4)) AS Intime, (substring(ManagerPayroll.dbo.Attendance.Out_Time,1,2)+'.'+substring(ManagerPayroll.dbo.Attendance.Out_Time,3,4)) AS Outtime, ManagerPayroll.dbo.Attendance.Present_Absent as Status from ManagerPayroll.dbo.Attendance, ManagerPayroll.dbo.Employee_Info";
           sql = sql + " Where ManagerPayroll.dbo.Attendance.Emp_Id=ManagerPayroll.dbo.Employee_Info.EmpID";
           sql = sql + " and ManagerPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT ManagerPayroll.dbo.Terminate_Resign.EmpID FROM ManagerPayroll.dbo.Terminate_Resign)";
           //sql01 = sql01 + " and EmpID NOT IN (SELECT EmpID FROM Terminate_Resign)";
           //sql = sql + " and ManagerPayroll.dbo.Employee_Info.Grade LIKE 'H%' AND ManagerPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) ORDER BY ManagerPayroll.dbo.Employee_Info.Section ASC";
           sql = sql + " and ManagerPayroll.dbo.Employee_Info.Grade LIKE '%[HE]%' AND ManagerPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'01/06/2013',103) AND convert(date,'09/06/2013',103) ";
           //sql = sql + " AND ManagerPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) ";
           //sql=sql+" ORDER BY CONVERT(DATE,ManagerPayroll.dbo.Attendance.Atten_Date,103) ASC, ManagerPayroll.dbo.Employee_Info.empid ASC";
           sql = sql + " ORDER BY ManagerPayroll.dbo.Employee_Info.SECTION ASC, ManagerPayroll.dbo.Attendance.Emp_Id ASC";
           DbCommand dbCommand = db.GetSqlStringCommand(sql);
           DataSet ds = db.ExecuteDataSet(dbCommand);
           return ds.Tables[0];
       }



    }
}
