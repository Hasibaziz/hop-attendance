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
    public partial class DeptwiseallDAL
    {
        public DataTable GetNTDeptRecordDAL(object param)
        {
            Database db = DatabaseFactory.CreateDatabase();
            EmployeewiseEntity objAtten = (EmployeewiseEntity)param;
            //string sql = "SELECT ID, SrviceNameID, DetailsName, GovFee, ServiceFee, OthersFee, FixedFigure, CC, Sit, UserID, EnterDate, UpdateDate FROM TR_ServiceDetails";
            //string sql = "select CONVERT(DATE, Attendance.Atten_Date,103) AS PDate";
            string sql = "select ( CAST(DATEPART(YY, Attendance.Atten_Date) AS VARCHAR(10))+ LEFT('-' + CAST(DATEPART(mm, Attendance.Atten_Date) AS VARCHAR(10)), 4) + LEFT('-' + CAST(DATEPART(dd, Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
            sql=sql+"    , Attendance.Emp_Id as EMPID, Employee_Info.Ename AS EName, Employee_Info.Designation AS Designation, Employee_Info.SECTION AS DeptName,(substring(Attendance.IN_time,1,2)+'.'+substring(Attendance.IN_time,3,4)) AS Intime, (substring(Attendance.Out_Time,1,2)+'.'+substring(Attendance.Out_Time,3,4)) AS Outtime, Attendance.Present_Absent as Status from Attendance, Employee_Info";
            sql = sql + " WHERE Employee_Info.EmpID NOT IN (SELECT Terminate_Resign.EmpID FROM Terminate_Resign)";
            sql = sql + " AND Attendance.Emp_Id=Employee_Info.EmpID and Employee_Info.Grade NOT LIKE 'W%'";
            sql = sql + " AND Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) AND Attendance.SECTION LIKE '" + objAtten.DeptName + "' ";
            sql = sql + " ORDER BY Attendance.Emp_Id, CONVERT(DATE,Attendance.Atten_Date,103)  ASC";
            //sql = sql + " ORDER BY Attendance.Atten_Date ASC";
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds.Tables[0];
        }
        public DataTable GetWFDeptRecordDAL(object param)
        {
            Database db = DatabaseFactory.CreateDatabase();
            WelformEntity objAtten = (WelformEntity)param;
            //string sql = "SELECT ID, SrviceNameID, DetailsName, GovFee, ServiceFee, OthersFee, FixedFigure, CC, Sit, UserID, EnterDate, UpdateDate FROM TR_ServiceDetails";
            //string sql = "select convert(date,Attendance.Atten_Date,103) AS PDate";
            //string sql = "select convert(date,Attendance.Atten_Date,103) AS PDate";
            string sql = "select ( CAST(DATEPART(YY, Attendance.Atten_Date) AS VARCHAR(10))+ LEFT('-' + CAST(DATEPART(mm, Attendance.Atten_Date) AS VARCHAR(10)), 4) + LEFT('-' + CAST(DATEPART(dd, Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
            sql=sql+" , Attendance.Emp_Id as EMPID, Employee_Info.Ename AS EName, Employee_Info.Designation AS Designation, Employee_Info.SECTION AS DeptName, (substring(Attendance.IN_time,1,2)+'.'+substring(Attendance.IN_time,3,4)) AS Intime, (substring(Attendance.Out_Time,1,2)+'.'+substring(Attendance.Out_Time,3,4)) AS Outtime, Attendance.Present_Absent as Status from Attendance, Employee_Info";
            sql = sql + " Where Attendance.Emp_Id=Employee_Info.EmpID";
            sql = sql + " and EmpID NOT IN (SELECT EmpID FROM Terminate_Resign)";
            sql = sql + " and Employee_Info.Grade LIKE 'W%' AND Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) AND Employee_Info.SECTION= '" + objAtten.DeptName + "' ORDER BY Attendance.Atten_Date ASC";
          
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds.Tables[0];
        }
        public DataTable GetAPPDeptRecordDAL(object param)
        {
            Database db = DatabaseFactory.CreateDatabase();
            ApprealInfoEntity objAtten = (ApprealInfoEntity)param;
            //string sql = "select CONVERT(DATE, MAppPayroll.dbo.Attendance.Atten_Date,103) AS PDate";
            string sql = "select ( CAST(DATEPART(YY, MAppPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10))+ LEFT('-' + CAST(DATEPART(mm, MAppPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4) + LEFT('-' + CAST(DATEPART(dd, MAppPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
            sql=sql+"  , MAppPayroll.dbo.Attendance.Emp_Id as EMPID, MAppPayroll.dbo.Employee_Info.Ename AS EName, MAppPayroll.dbo.Employee_Info.Designation AS Designation, MAppPayroll.dbo.Employee_Info.SECTION AS DeptName, (substring(MAppPayroll.dbo.Attendance.IN_time,1,2)+'.'+substring(MAppPayroll.dbo.Attendance.IN_time,3,4)) AS Intime, (substring(MAppPayroll.dbo.Attendance.Out_Time,1,2)+'.'+substring(MAppPayroll.dbo.Attendance.Out_Time,3,4)) AS Outtime, MAppPayroll.dbo.Attendance.Present_Absent as Status from MAppPayroll.dbo.Attendance, MAppPayroll.dbo.Employee_Info";
            sql = sql + " Where MAppPayroll.dbo.Attendance.Emp_Id=MAppPayroll.dbo.Employee_Info.EmpID";
            sql = sql + " AND MAppPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT MAppPayroll.dbo.Terminate_Resign.EmpID FROM MAppPayroll.dbo.Terminate_Resign)";
            sql = sql + " AND MAppPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) AND MAppPayroll.dbo.Attendance.SECTION= '" + objAtten.DeptName + "'";
            sql = sql + " ORDER BY CONVERT(DATE,MAppPayroll.dbo.Attendance.Atten_Date,103) ASC";
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds.Tables[0];
        }
        public DataTable GetMGRDeptRecordDAL(object param)
        {
            Database db = DatabaseFactory.CreateDatabase();
            ManagerinfoEntity objAtten = (ManagerinfoEntity)param;
            //string sql = "select CONVERT(DATE, ManagerPayroll.dbo.Attendance.Atten_Date,103) AS PDate";
            string sql = "select ( CAST(DATEPART(YY, ManagerPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10))+ LEFT('-' + CAST(DATEPART(mm, ManagerPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4) + LEFT('-' + CAST(DATEPART(dd, ManagerPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
            sql=sql+" , ManagerPayroll.dbo.Attendance.Emp_Id as EMPID, ManagerPayroll.dbo.Employee_Info.Ename AS EName, ManagerPayroll.dbo.Employee_Info.Designation AS Designation, ManagerPayroll.dbo.Employee_Info.SECTION AS DeptName, (substring(ManagerPayroll.dbo.Attendance.IN_time,1,2)+'.'+substring(ManagerPayroll.dbo.Attendance.IN_time,3,4)) AS Intime, (substring(ManagerPayroll.dbo.Attendance.Out_Time,1,2)+'.'+substring(ManagerPayroll.dbo.Attendance.Out_Time,3,4)) AS Outtime, ManagerPayroll.dbo.Attendance.Present_Absent as Status from ManagerPayroll.dbo.Attendance, ManagerPayroll.dbo.Employee_Info";
            sql = sql + " Where ManagerPayroll.dbo.Attendance.Emp_Id=ManagerPayroll.dbo.Employee_Info.EmpID";
            sql = sql + " and ManagerPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT ManagerPayroll.dbo.Terminate_Resign.EmpID FROM ManagerPayroll.dbo.Terminate_Resign)";
            sql = sql + " and ManagerPayroll.dbo.Employee_Info.Grade LIKE 'H%' AND ManagerPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) AND ManagerPayroll.dbo.Attendance.SECTION= '" + objAtten.DeptName + "'";
            sql = sql + " ORDER BY ManagerPayroll.dbo.Employee_Info.Designation  DESC";
            //sql = sql + " ORDER BY CONVERT(DATE,ManagerPayroll.dbo.Attendance.Atten_Date,103) ASC";
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds.Tables[0];
        }
        public DataTable GetBDDeptRecord(object param)
        {
            Malconn conn = new Malconn();

            //Database db = DatabaseFactory.CreateDatabase();
            BDEmployeeEntity objAtten = (BDEmployeeEntity)param;
            //string sql = "select CONVERT(DATE, MBDPayroll.dbo.Attendance.Atten_Date,103) AS PDate";
            string sql = "select ( CAST(DATEPART(YY, MBDPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10))+ LEFT('-' + CAST(DATEPART(mm, MBDPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4) + LEFT('-' + CAST(DATEPART(dd, MBDPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
            sql=sql+"    , MBDPayroll.dbo.Attendance.Emp_Id as EMPID, MBDPayroll.dbo.Employee_Info.Ename AS EName, MBDPayroll.dbo.Employee_Info.Designation AS Designation, MBDPayroll.dbo.Employee_Info.SECTION AS DeptName, (substring(MBDPayroll.dbo.Attendance.IN_time,1,2)+'.'+substring(MBDPayroll.dbo.Attendance.IN_time,3,4)) AS Intime, (substring(MBDPayroll.dbo.Attendance.Out_Time,1,2)+'.'+substring(MBDPayroll.dbo.Attendance.Out_Time,3,4)) AS Outtime, MBDPayroll.dbo.Attendance.Present_Absent as Status from MBDPayroll.dbo.Attendance, MBDPayroll.dbo.Employee_Info";
            sql = sql + " Where MBDPayroll.dbo.Attendance.Emp_Id=MBDPayroll.dbo.Employee_Info.EmpID";
            sql = sql + " AND MBDPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT MBDPayroll.dbo.Terminate_Resign.EmpID FROM MBDPayroll.dbo.Terminate_Resign)";
            sql = sql + " AND MBDPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) AND MBDPayroll.dbo.Employee_Info.SECTION= '" + objAtten.DeptName + "'";
            sql = sql + " ORDER BY CONVERT(DATE,MBDPayroll.dbo.Attendance.Atten_Date,103) ASC";
            DataSet ds = new DataSet();
            ds = conn.ExecuteSelectSQL(sql, null, false);
            //DbCommand dbCommand = db.GetSqlStringCommand(sql);
            //DataSet ds = db.ExecuteDataSet(dbCommand);
            //return conn.ExecuteQueryData(sql);  
            return ds.Tables[0];
        }

        public DataTable AG_GetAllDeptRecord(object param)
        {
            Database db = DatabaseFactory.CreateDatabase();
            string sql = "select DISTINCT  [ManagerPayroll].[dbo].[Employee_Info].section as DeptName from [ManagerPayroll].[dbo].[Employee_Info]";
            sql = sql + " ORDER BY [ManagerPayroll].[dbo].[Employee_Info].section ASC";
            DbCommand dbCommand = db.GetSqlStringCommand(sql);
            DataSet ds = db.ExecuteDataSet(dbCommand);
            return ds.Tables[0];
        }
    }
}
