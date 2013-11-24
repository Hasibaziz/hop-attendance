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
   public partial class BDEmployeedaycalDAL
    {
       public DataTable GetBDEmployeedaycalRecord(object param)
       {
           Malconn conn = new Malconn();
           //Database db = DatabaseFactory.CreateDatabase();
           BDEmployeedaycal objAtten = (BDEmployeedaycal)param;
           string sql = "select EmpID as EMPID, Ename as ENAME, Section as SECTION, CONVERT(DATE,J_Date,101) as JDate, Status as Status ";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MBDPayroll].[dbo].[Attendance] where Emp_Id = [MBDPayroll].dbo.Employee_Info.EmpID  and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as TTDay";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MBDPayroll].[dbo].[Attendance] where Emp_Id = [MBDPayroll].dbo.Employee_Info.EmpID  and Present_Absent='H' and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as Holiday";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MBDPayroll].[dbo].[Attendance] where Emp_Id = [MBDPayroll].dbo.Employee_Info.EmpID  and Present_Absent in('P','LA') and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as Present";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MBDPayroll].[dbo].[Attendance] where Emp_Id = [MBDPayroll].dbo.Employee_Info.EmpID  and Present_Absent='A' and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as Absent";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MBDPayroll].[dbo].[Attendance] where Emp_Id = [MBDPayroll].dbo.Employee_Info.EmpID  and Present_Absent='CL' and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as CL";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MBDPayroll].[dbo].[Attendance] where Emp_Id = [MBDPayroll].dbo.Employee_Info.EmpID  and Present_Absent='SL' and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as SL";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MBDPayroll].[dbo].[Attendance] where Emp_Id = [MBDPayroll].dbo.Employee_Info.EmpID  and Present_Absent='ML' and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as ML";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MBDPayroll].[dbo].[Attendance] where Emp_Id = [MBDPayroll].dbo.Employee_Info.EmpID  and Present_Absent='EL' and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as EL";
           sql = sql + " FROM [MBDPayroll].[dbo].[Employee_Info] where EmpID not in";
           sql = sql + " (select EmpID from [MBDPayroll].dbo.Terminate_Resign ) ";    //Not like 'w%' Means North Tower, Like means Welform
           sql = sql + " order by [MBDPayroll].[dbo].[Employee_Info].Section";
          
           DataSet ds = new DataSet();
           ds = conn.ExecuteSelectSQL(sql, null, false);
           return ds.Tables[0];
       }
    }
}
