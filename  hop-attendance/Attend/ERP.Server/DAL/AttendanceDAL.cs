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
   public partial class AttendanceDAL
    {
       public DataTable GetAttendanceInfoRecord(object param)
       {
           Database db = DatabaseFactory.CreateDatabase();
           AttendanceEntity objAtten = (AttendanceEntity)param;
           //string sql = "SELECT ID, SrviceNameID, DetailsName, GovFee, ServiceFee, OthersFee, FixedFigure, CC, Sit, UserID, EnterDate, UpdateDate FROM TR_ServiceDetails";
           //103-dd/mm/yyyy
           //string sql = "select CONVERT(DATE, Attendance.Atten_Date,103) AS PDate";
           string sql = "select ( CAST(DATEPART(YY, Attendance.Atten_Date) AS VARCHAR(10))+ LEFT('-' + CAST(DATEPART(mm, Attendance.Atten_Date) AS VARCHAR(10)), 4) + LEFT('-' + CAST(DATEPART(dd, Attendance.Atten_Date) AS VARCHAR(10)), 4)) AS PDate";
           sql=sql+"    , Attendance.Emp_Id as EMPID, Employee_Info.Ename AS EName, Employee_Info.Designation AS Designation, Employee_Info.SECTION AS DeptName, (substring(Attendance.IN_time,1,2)+'.'+substring(Attendance.IN_time,3,4)) AS Intime, (substring(Attendance.Out_Time,1,2)+'.'+substring(Attendance.Out_Time,3,4)) AS Outtime, Attendance.Present_Absent as Status from Attendance, Employee_Info";
           sql = sql + " Where Attendance.Emp_Id=Employee_Info.EmpID";
           sql = sql + " and EmpID NOT IN (SELECT EmpID FROM Terminate_Resign)";
           sql = sql + " and Employee_Info.Grade LIKE 'H%' AND Attendance.Atten_Date BETWEEN convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)";
           //sql = sql + " ORDER BY CONVERT(DATE,Attendance.Atten_Date,103) ASC, Employee_Info.Designation DESC, Employee_Info.Section ASC";
           sql = sql + " ORDER BY Employee_Info.Section ASC, Attendance.Emp_Id ASC, CONVERT(DATE,Attendance.Atten_Date,103) ASC, Employee_Info.Designation DESC";
           DbCommand dbCommand = db.GetSqlStringCommand(sql);
           DataSet ds = db.ExecuteDataSet(dbCommand);
           return ds.Tables[0];
       }
      
    }
}
