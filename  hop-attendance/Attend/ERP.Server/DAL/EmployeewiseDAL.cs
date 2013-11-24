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
   public partial class EmployeewiseDAL
    {
       public DataTable GetEmployeewiseRecord(object param)
       {
           Database db = DatabaseFactory.CreateDatabase();
           EmployeewiseEntity objAtten = (EmployeewiseEntity)param;
           //string sql = "SELECT ID, SrviceNameID, DetailsName, GovFee, ServiceFee, OthersFee, FixedFigure, CC, Sit, UserID, EnterDate, UpdateDate FROM TR_ServiceDetails";
           //string sql = "select CONVERT(DATE,Attendance.Atten_Date,103) AS PDate";
           string sql = "select ( CAST(DATEPART(YY, Attendance.Atten_Date) AS VARCHAR(10))+ LEFT('-' + CAST(DATEPART(mm, Attendance.Atten_Date) AS VARCHAR(10)), 4) + LEFT('-' + CAST(DATEPART(dd, Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
           sql=sql+"  , Attendance.Emp_Id as EMPID, Employee_Info.Ename AS EName, Employee_Info.Designation AS Designation, Employee_Info.SECTION AS DeptName,(substring(Attendance.IN_time,1,2)+'.'+substring(Attendance.IN_time,3,4)) AS Intime, (substring(Attendance.Out_Time,1,2)+'.'+substring(Attendance.Out_Time,3,4)) AS Outtime, Attendance.Present_Absent as Status from Attendance, Employee_Info";
           sql = sql + " WHERE Employee_Info.EmpID NOT IN (SELECT Terminate_Resign.EmpID FROM Terminate_Resign)";
           sql = sql + " AND Attendance.Emp_Id=Employee_Info.EmpID";
           sql = sql + " AND Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) AND Attendance.Emp_Id= '" + objAtten.EMPID + "' ";
           sql = sql + " ORDER BY CONVERT(DATE,Attendance.Atten_Date,103) ASC";
           //sql = sql + " ORDER BY Attendance.Atten_Date ASC";
           DbCommand dbCommand = db.GetSqlStringCommand(sql);
           DataSet ds = db.ExecuteDataSet(dbCommand);
           return ds.Tables[0];
       }
       public DataTable GetAllEmployeewiseRecord(object param)
       {
           Database db = DatabaseFactory.CreateDatabase();
           //EmployeewiseEntity objAtten = (EmployeewiseEntity)param;
           //string sql = "select Attendance.Emp_Id as EMPID, Employee_Info.Ename AS EName, Employee_Info.SECTION AS DeptName from Attendance, Employee_Info";
           //sql = sql + " Where Attendance.Emp_Id=Employee_Info.EmpID";
           //sql = sql + " AND Attendance.Atten_Date BETWEEN convert(datetime,'" + objAtten.StartDate + "',103) AND convert(datetime,'" + objAtten.EndDate + "',103) AND Attendance.Emp_Id= '" + objAtten.EMPID + "' ";
           string sql = "SELECT Employee_Info.EmpID AS EMPID, Employee_Info.Ename AS EName from Employee_Info WHERE Employee_Info.EmpID NOT IN (SELECT Terminate_Resign.EmpID FROM Terminate_Resign) AND Employee_Info.Grade LIKE 'H%' ";
           sql = sql + " ORDER BY Employee_Info.EmpID ASC";
           DbCommand dbCommand = db.GetSqlStringCommand(sql);
           DataSet ds = db.ExecuteDataSet(dbCommand);
           return ds.Tables[0];
       }

    }
}
