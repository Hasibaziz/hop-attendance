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
   public partial class ApprealInfoDAL
    {
       public DataTable GetApprealInfoRecord(object param)
       {
           Database db = DatabaseFactory.CreateDatabase();
           ApprealInfoEntity objAtten = (ApprealInfoEntity)param;
           //string sql = "SELECT ID, SrviceNameID, DetailsName, GovFee, ServiceFee, OthersFee, FixedFigure, CC, Sit, UserID, EnterDate, UpdateDate FROM TR_ServiceDetails";
           //103-dd/mm/yyyy
           //string sql = "select CONVERT(DATE, MAppPayroll.dbo.Attendance.Atten_Date,103) AS PDate";
           string sql = "select ( CAST(DATEPART(YY,  MAppPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10))+ LEFT('/' + CAST(DATEPART(mm,  MAppPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4) + LEFT('/' + CAST(DATEPART(dd,  MAppPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
           sql=sql+"    , MAppPayroll.dbo.Attendance.Emp_Id as EMPID, MAppPayroll.dbo.Employee_Info.Ename AS EName, MAppPayroll.dbo.Employee_Info.Designation AS Designation, MAppPayroll.dbo.Employee_Info.SECTION AS DeptName, (substring(MAppPayroll.dbo.Attendance.IN_time,1,2)+'.'+substring(MAppPayroll.dbo.Attendance.IN_time,3,4)) AS Intime, (substring(MAppPayroll.dbo.Attendance.Out_Time,1,2)+'.'+substring(MAppPayroll.dbo.Attendance.Out_Time,3,4)) AS Outtime, MAppPayroll.dbo.Attendance.Present_Absent as Status from MAppPayroll.dbo.Attendance, MAppPayroll.dbo.Employee_Info";
           sql = sql + " Where MAppPayroll.dbo.Attendance.Emp_Id=MAppPayroll.dbo.Employee_Info.EmpID";
           sql = sql + " AND MAppPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT MAppPayroll.dbo.Terminate_Resign.EmpID FROM MAppPayroll.dbo.Terminate_Resign)";
           sql = sql + " AND MAppPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)";
           //sql = sql + " ORDER BY MAppPayroll.dbo.Employee_Info.Section ASC, MAppPayroll.dbo.Employee_Info.Designation DESC, CONVERT(DATE,MAppPayroll.dbo.Attendance.Atten_Date,103) ASC";
           sql = sql + " ORDER BY MAppPayroll.dbo.Employee_Info.Section ASC, MAppPayroll.dbo.Attendance.Emp_Id ASC, CONVERT(DATE,MAppPayroll.dbo.Attendance.Atten_Date,103) ASC";
           DbCommand dbCommand = db.GetSqlStringCommand(sql);
           DataSet ds = db.ExecuteDataSet(dbCommand);
           return ds.Tables[0];
       }
       public DataTable GetApprealEMPWiseRecord(object param)
       {
           Database db = DatabaseFactory.CreateDatabase();
           ApprealInfoEntity objAtten = (ApprealInfoEntity)param;
           //string sql = "SELECT ID, SrviceNameID, DetailsName, GovFee, ServiceFee, OthersFee, FixedFigure, CC, Sit, UserID, EnterDate, UpdateDate FROM TR_ServiceDetails";
           //103-dd/mm/yyyy
           //string sql = "select CONVERT(DATE, MAppPayroll.dbo.Attendance.Atten_Date,103) AS PDate";
           string sql = "select ( CAST(DATEPART(YY, MAppPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10))+ LEFT('/0' + CAST(DATEPART(mm, MAppPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4) + LEFT('/0' + CAST(DATEPART(dd, MAppPayroll.dbo.Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
           sql=sql+"    , MAppPayroll.dbo.Attendance.Emp_Id as EMPID, MAppPayroll.dbo.Employee_Info.Ename AS EName, MAppPayroll.dbo.Employee_Info.Designation AS Designation, MAppPayroll.dbo.Employee_Info.SECTION AS DeptName, (substring(MAppPayroll.dbo.Attendance.IN_time,1,2)+'.'+substring(MAppPayroll.dbo.Attendance.IN_time,3,4)) AS Intime, (substring(MAppPayroll.dbo.Attendance.Out_Time,1,2)+'.'+substring(MAppPayroll.dbo.Attendance.Out_Time,3,4)) AS Outtime, MAppPayroll.dbo.Attendance.Present_Absent as Status from MAppPayroll.dbo.Attendance, MAppPayroll.dbo.Employee_Info";
           sql = sql + " Where MAppPayroll.dbo.Attendance.Emp_Id=MAppPayroll.dbo.Employee_Info.EmpID";
           sql = sql + " AND MAppPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT MAppPayroll.dbo.Terminate_Resign.EmpID FROM MAppPayroll.dbo.Terminate_Resign)";
           sql = sql + " AND MAppPayroll.dbo.Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103) AND MAppPayroll.dbo.Attendance.Emp_Id= '" + objAtten.EMPID + "'";
           sql = sql + " ORDER BY CONVERT(DATE,MAppPayroll.dbo.Attendance.Atten_Date,103) ASC";
           DbCommand dbCommand = db.GetSqlStringCommand(sql);
           DataSet ds = db.ExecuteDataSet(dbCommand);
           return ds.Tables[0];
       }
       public DataTable GetAllAppearlRecord(object param)
       {
           Database db = DatabaseFactory.CreateDatabase();
           ApprealInfoEntity objAtten = (ApprealInfoEntity)param;
           string sql = "SELECT MAppPayroll.dbo.Employee_Info.EmpID AS EMPID, MAppPayroll.dbo.Employee_Info.Ename AS EName from MAppPayroll.dbo.Employee_Info WHERE MAppPayroll.dbo.Employee_Info.EmpID NOT IN (SELECT MAppPayroll.dbo.Terminate_Resign.EmpID FROM MAppPayroll.dbo.Terminate_Resign)";
           sql = sql + " ORDER BY Employee_Info.EmpID ASC"; 
           DbCommand dbCommand = db.GetSqlStringCommand(sql);
           DataSet ds = db.ExecuteDataSet(dbCommand);
           return ds.Tables[0];
       }
   

    }
}
