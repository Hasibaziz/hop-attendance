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
   public partial class NorthtowerdaycalDAL
    {
       public DataTable GetNorthtowerdaycalRecord(object param)
       {
           Database db = DatabaseFactory.CreateDatabase();
           NorthTowerdaycal objAtten = (NorthTowerdaycal)param;
           string sql = "select EmpID as EMPID, Ename as ENAME, Designation, Section as SECTION, J_Date as JDate, Status as Status ";
           sql = sql + " ,(Select  count(Present_Absent) FROM MNTPayroll.dbo.Attendance where Emp_Id = MNTPayroll.dbo.Employee_Info.EmpID  and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as TTDay";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MNTPayroll].[dbo].[Attendance] where Emp_Id = [MNTPayroll].dbo.Employee_Info.EmpID  and Present_Absent='H' and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as Holiday";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MNTPayroll].[dbo].[Attendance] where Emp_Id = [MNTPayroll].dbo.Employee_Info.EmpID  and Present_Absent in('P','LA') and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as Present";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MNTPayroll].[dbo].[Attendance] where Emp_Id = [MNTPayroll].dbo.Employee_Info.EmpID  and Present_Absent='A' and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as Absent";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MNTPayroll].[dbo].[Attendance] where Emp_Id = [MNTPayroll].dbo.Employee_Info.EmpID  and Present_Absent='CL' and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as CL";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MNTPayroll].[dbo].[Attendance] where Emp_Id = [MNTPayroll].dbo.Employee_Info.EmpID  and Present_Absent='SL' and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as SL";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MNTPayroll].[dbo].[Attendance] where Emp_Id = [MNTPayroll].dbo.Employee_Info.EmpID  and Present_Absent='ML' and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as ML";
           sql = sql + " ,(Select  count(Present_Absent) FROM [MNTPayroll].[dbo].[Attendance] where Emp_Id = [MNTPayroll].dbo.Employee_Info.EmpID  and Present_Absent='EL' and Atten_Date between  convert(date,'" + objAtten.StartDate + "',103) AND convert(date,'" + objAtten.EndDate + "',103)) as EL";
           sql = sql + " FROM [MNTPayroll].[dbo].[Employee_Info] where EmpID not in";
           sql = sql + " (select EmpID from [MNTPayroll].dbo.Terminate_Resign ) and Grade not like 'w%'";    //Not like 'w%' Means North Tower
           sql = sql + " order by [MNTPayroll].[dbo].[Employee_Info].Section ASC, [MNTPayroll].[dbo].[Employee_Info].EmpID ASC";           
           DbCommand dbCommand = db.GetSqlStringCommand(sql);
           DataSet ds = db.ExecuteDataSet(dbCommand);
           return ds.Tables[0];
       }


    }
}
