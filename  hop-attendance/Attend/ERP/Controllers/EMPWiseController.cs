using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP.Structure;
using ERP.Domain.Model;
using System.Threading;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using System.Collections;
using System.IO;
using CrystalDecisions.Shared;
using ERP.Reports;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Text;

namespace ERP.Controllers
{
    public class EMPWiseController : BaseController
    {
        //
        // GET: /EMPWise/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManagerWise()
        {
            ManagerinfoEntity Model = new ManagerinfoEntity();
            ViewData["EMPID"] = GetAllManagerListItem();
            Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(Model);
        }
        public ActionResult EmployeeWise()
        {
            EmployeewiseEntity iEMPWise = new EmployeewiseEntity();
            ViewData["EMPID"] = GetAllEmployeewiseListItem();
            //iEMPWise.EMPID = frm["DropDown"];                   *Pass value as Parameter: EmployeeWise(string Empid, FormCollection frm)
            //iEMPWise.EMPID = Request["DropDown"];
            //iEMPWise.EMPID = ID;
            iEMPWise.StartDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            iEMPWise.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");


            return View("EmployeeWise", iEMPWise);
        }
        public ActionResult WelformWise()
        {
            WelformEntity iWelform = new WelformEntity();
            ViewData["EMPID"] = GetAllWelformWiseListItem();
            iWelform.StartDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            iWelform.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(iWelform);
        }
        public ActionResult ApprealWise()
        {
            ApprealInfoEntity _Model = new ApprealInfoEntity();
            ViewData["EMPID"] = GetAllAppearlListItem();
            _Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            _Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(_Model);
        }
        public ActionResult BDEmployeewise()
        {
            BDEmployeeEntity _Model = new BDEmployeeEntity();
            ViewData["EMPID"] = GetAllBDEmployeewiseListItem();
            _Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            _Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");

            return View(_Model);
        }

        [HttpPost]
        public JsonResult AllManagerInfoList(string startDate = "", string endDate = "", string Empid = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    ManagerinfoEntity _Model = new ManagerinfoEntity();
                    _Model.EMPID = Empid;
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAllManagerInfoRecord, _Model);
                    List<ManagerinfoEntity> ItemList = null;
                    ItemList = new List<ManagerinfoEntity>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new ManagerinfoEntity()
                            {
                                PDate = dr["PDate"].ToString(),
                                EMPID = dr["EMPID"].ToString(),
                                EName = dr["EName"].ToString(),
                                Designation = dr["Designation"].ToString(),
                                DeptName = dr["DeptName"].ToString(),
                                Intime = dr["Intime"].ToString(),
                                Outtime = dr["Outtime"].ToString(),
                                Status = dr["Status"].ToString(),
                            });
                        }
                        iCount += 1;
                    }
                    var RecordCount = dt.Rows.Count;
                    var Record = ItemList;
                    Session["INDMGR"] = ItemList;
                    return Json(new { Result = "OK", Records = Record, TotalRecordCount = RecordCount });
                }
                catch (Exception ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult EmployeeWiseList(string startDate = "", string endDate = "", string Empid = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    EmployeewiseEntity _Model = new EmployeewiseEntity();
                    _Model.EMPID = Empid;
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetEmployeewiseRecord, _Model);
                    List<EmployeewiseEntity> ItemList = null;
                    ItemList = new List<EmployeewiseEntity>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new EmployeewiseEntity()
                            {
                                PDate = dr["PDate"].ToString(),
                                EMPID = dr["EMPID"].ToString(),
                                EName = dr["EName"].ToString(),
                                Designation = dr["Designation"].ToString(),
                                DeptName = dr["DeptName"].ToString(),
                                Intime = dr["Intime"].ToString(),
                                Outtime = dr["Outtime"].ToString(),
                                Status = dr["Status"].ToString(),
                            });
                        }
                        iCount += 1;
                    }
                    var RecordCount = dt.Rows.Count;
                    var Record = ItemList;
                    Session["EMPWISE"] = ItemList;
                    return Json(new { Result = "OK", Records = Record, TotalRecordCount = RecordCount });
                }
                catch (Exception ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult ApprealEMPWiseList(string startDate = "", string endDate = "", string Empid = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    ApprealInfoEntity _Model = new ApprealInfoEntity();
                    _Model.EMPID = Empid;
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetApprealEMPWiseRecord, _Model);
                    List<ApprealInfoEntity> ItemList = null;
                    ItemList = new List<ApprealInfoEntity>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new ApprealInfoEntity()
                            {
                                PDate = dr["PDate"].ToString(),
                                EMPID = dr["EMPID"].ToString(),
                                EName = dr["EName"].ToString(),
                                Designation = dr["Designation"].ToString(),
                                DeptName = dr["DeptName"].ToString(),
                                Intime = dr["Intime"].ToString(),
                                Outtime = dr["Outtime"].ToString(),
                                Status = dr["Status"].ToString(),
                            });
                        }
                        iCount += 1;
                    }
                    var RecordCount = dt.Rows.Count;
                    var Record = ItemList;
                    Session["Apparel"] = ItemList;
                    return Json(new { Result = "OK", Records = Record, TotalRecordCount = RecordCount });
                }
                catch (Exception ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult WelformWiseList(string startDate = "", string endDate = "", string Empid = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    WelformEntity _Model = new WelformEntity();
                    _Model.EMPID = Empid;
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetWelformWiseRecord, _Model);
                    List<WelformEntity> ItemList = null;
                    ItemList = new List<WelformEntity>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new WelformEntity()
                            {
                                PDate = dr["PDate"].ToString(),
                                EMPID = dr["EMPID"].ToString(),
                                EName = dr["EName"].ToString(),
                                Designation = dr["Designation"].ToString(),
                                DeptName = dr["DeptName"].ToString(),
                                Intime = dr["Intime"].ToString(),
                                Outtime = dr["Outtime"].ToString(),
                                Status = dr["Status"].ToString(),
                            });
                        }
                        iCount += 1;
                    }
                    var RecordCount = dt.Rows.Count;
                    var Record = ItemList;
                    Session["Welform"] = ItemList;
                    return Json(new { Result = "OK", Records = Record, TotalRecordCount = RecordCount });
                }
                catch (Exception ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult BDEmployeeWiseList(string startDate = "", string endDate = "", string Empid = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    BDEmployeeEntity _Model = new BDEmployeeEntity();
                    _Model.EMPID = Empid;
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetBDEmployeewiseRecord, _Model);
                    List<BDEmployeeEntity> ItemList = null;
                    ItemList = new List<BDEmployeeEntity>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new BDEmployeeEntity()
                            {
                                PDate = dr["PDate"].ToString(),
                                EMPID = dr["EMPID"].ToString(),
                                EName = dr["EName"].ToString(),
                                Designation = dr["Designation"].ToString(),
                                DeptName = dr["DeptName"].ToString(),
                                Intime = dr["Intime"].ToString(),
                                Outtime = dr["Outtime"].ToString(),
                                Status = dr["Status"].ToString(),
                            });
                        }
                        iCount += 1;
                    }
                    var RecordCount = dt.Rows.Count;
                    var Record = ItemList;
                    Session["BD"] = ItemList;
                    return Json(new { Result = "OK", Records = Record, TotalRecordCount = RecordCount });
                }
                catch (Exception ex)
                {
                    return Json(new { Result = "ERROR", Message = ex.Message });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }


        public ActionResult ApparelWiserpt()
        {
            ApparelWiserptEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/ApparelWiserpt.rpt");
            rptH.Load();

            List<ApprealInfoEntity> ItemList = (List<ApprealInfoEntity>)Session["Apparel"];

            foreach (ApprealInfoEntity dr in ItemList)
            {
                obj = new ApparelWiserptEntity();

                obj.PDate = dr.PDate;
                obj.EMPID = dr.EMPID;
                obj.EName = dr.EName;
                obj.Designation = dr.Designation;
                obj.DeptName = dr.DeptName;
                obj.Intime = dr.Intime;
                obj.Outtime = dr.Outtime;
                obj.Status = dr.Status;
                al.Add(obj);
            }

            rptH.SetDataSource(al);
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");           
        }
        public ActionResult ApparelWiserptExcel()
        {
            ApparelWiserptEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/ApparelWiserpt.rpt");
            rptH.Load();

            List<ApprealInfoEntity> ItemList = (List<ApprealInfoEntity>)Session["Apparel"];

            foreach (ApprealInfoEntity dr in ItemList)
            {
                obj = new ApparelWiserptEntity();

                obj.PDate = dr.PDate;
                obj.EMPID = dr.EMPID;
                obj.EName = dr.EName;
                obj.Designation = dr.Designation;
                obj.DeptName = dr.DeptName;
                obj.Intime = dr.Intime;
                obj.Outtime = dr.Outtime;
                obj.Status = dr.Status;
                al.Add(obj);
            }

            rptH.SetDataSource(al);
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.Excel);   //For Excel File
            return File(stream, "application/octet-stream", "Apparel.xls");    //For Excel with File Name
        }
        public ActionResult WelformWiserpt()
        {
            WelformWiserptEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/WelformWiserpt.rpt");
            rptH.Load();

            List<WelformEntity> ItemList = (List<WelformEntity>)Session["Welform"];

            foreach (WelformEntity dr in ItemList)
            {
                obj = new WelformWiserptEntity();

                obj.PDate = dr.PDate;
                obj.EMPID = dr.EMPID;
                obj.EName = dr.EName;
                obj.Designation = dr.Designation;
                obj.DeptName = dr.DeptName;
                obj.Intime = dr.Intime;
                obj.Outtime = dr.Outtime;
                obj.Status = dr.Status;
                al.Add(obj);
            }

            rptH.SetDataSource(al);
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
          
        }
        public ActionResult WelformWiserptExcel()
        {
            WelformWiserptEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/WelformWiserpt.rpt");
            rptH.Load();

            List<WelformEntity> ItemList = (List<WelformEntity>)Session["Welform"];

            foreach (WelformEntity dr in ItemList)
            {
                obj = new WelformWiserptEntity();

                obj.PDate = dr.PDate;
                obj.EMPID = dr.EMPID;
                obj.EName = dr.EName;
                obj.Designation = dr.Designation;
                obj.DeptName = dr.DeptName;
                obj.Intime = dr.Intime;
                obj.Outtime = dr.Outtime;
                obj.Status = dr.Status;
                al.Add(obj);
            }

            rptH.SetDataSource(al);
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.Excel);   //For Excel File
            return File(stream, "application/octet-stream", "Welform.xls");    //For Excel with File Name
        }
        public ActionResult BDEmployeeWiserpt()
        {
            BDEmployeeWiserptEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/BDEmployeeWiserpt.rpt");
            rptH.Load();

            List<BDEmployeeEntity> ItemList = (List<BDEmployeeEntity>)Session["BD"];

            foreach (BDEmployeeEntity dr in ItemList)
            {
                obj = new BDEmployeeWiserptEntity();

                obj.PDate = dr.PDate;
                obj.EMPID = dr.EMPID;
                obj.EName = dr.EName;
                obj.Designation = dr.Designation;
                obj.DeptName = dr.DeptName;
                obj.Intime = dr.Intime;
                obj.Outtime = dr.Outtime;
                obj.Status = dr.Status;
                al.Add(obj);
            }

            rptH.SetDataSource(al);
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");

        }
        public ActionResult BDEmployeeWiserptExcel()
        {
            BDEmployeeWiserptEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/BDEmployeeWiserpt.rpt");
            rptH.Load();

            List<BDEmployeeEntity> ItemList = (List<BDEmployeeEntity>)Session["BD"];

            foreach (BDEmployeeEntity dr in ItemList)
            {
                obj = new BDEmployeeWiserptEntity();

                obj.PDate = dr.PDate;
                obj.EMPID = dr.EMPID;
                obj.EName = dr.EName;
                obj.Designation = dr.Designation;
                obj.DeptName = dr.DeptName;
                obj.Intime = dr.Intime;
                obj.Outtime = dr.Outtime;
                obj.Status = dr.Status;
                al.Add(obj);
            }

            rptH.SetDataSource(al);
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.Excel);   //For Excel File
            return File(stream, "application/octet-stream", "BDEmployee.xls");    //For Excel with File Name

        }

        public ActionResult NTEMPExcelReport(string EX1 = "", string EX2 = "", string EX3 = "")
        {

            EmployeewiseEntity _Model = new EmployeewiseEntity();
            _Model.StartDate = EX1;
            _Model.EndDate = EX2;
            _Model.EMPID = EX3;
            DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetEmployeewiseRecord, _Model);
            StringBuilder sb = new StringBuilder();
            sb.Append("<table border='" + "2px" + "'b>");

            //write column headings
            sb.Append("<tr>");

            foreach (System.Data.DataColumn dc in dt.Columns)
            {
                sb.Append("<td><b><font face=Arial size=2>" + dc.ColumnName + "</font></b></td>");
            }
            sb.Append("</tr>");

            foreach (System.Data.DataRow dr in dt.Rows)
            {
                sb.Append("<tr>");
                foreach (System.Data.DataColumn dc in dt.Columns)
                {
                    sb.Append("<td><font face=Arial size=" + "14px" + ">" + dr[dc].ToString() + "</font></td>");
                }
                sb.Append("</tr>");
            }
            sb.Append("</table>");

            //this.Response.AddHeader("Content-Disposition", "Employees.xls");
            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel", "NorthTower.xls");
        }


    }
}
