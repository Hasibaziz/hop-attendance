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

namespace ERP.Controllers
{
    public class DeptwiseController : BaseController
    {
        //
        // GET: /Deptwise/

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ManagerDept()
        {
            ManagerinfoEntity Model = new ManagerinfoEntity();
            ViewData["DeptName"] = GetAllDepatementListItem();
            Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(Model);
        }
        public ActionResult NorthtowerDept()
        {
            EmployeewiseEntity iDEPTWise = new EmployeewiseEntity();
            ViewData["DeptName"] = GetAllDepatementListItem();
            iDEPTWise.StartDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            iDEPTWise.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View("NorthtowerDept", iDEPTWise);
        }
        public ActionResult WelformDept()
        {
            WelformEntity iWF = new WelformEntity();
            ViewData["DeptName"]=GetAllDepatementListItem();
            iWF.StartDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            iWF.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View("WelformDept",iWF);
        }
        public ActionResult AppearlDept()
        {
            ApprealInfoEntity iAPP = new ApprealInfoEntity();
            ViewData["DeptName"] = GetAllDepatementListItem();
            iAPP.StartDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            iAPP.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View("AppearlDept", iAPP);
        }
        public ActionResult BDDept()
        {
            BDEmployeeEntity _Model = new BDEmployeeEntity();
            ViewData["DeptName"] = GetAllDepatementListItem();
            _Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            _Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");

            return View(_Model);
        }

        [HttpPost]
        public JsonResult ManagerDeptList(string startDate = "", string endDate = "", string DeptName = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    ManagerinfoEntity _Model = new ManagerinfoEntity();
                    _Model.DeptName = DeptName;
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetMGRDeptRecord, _Model);
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
                    Session["MGRDept"] = ItemList;
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
        public JsonResult NorthtowerDeptList(string startDate = "", string endDate = "", string DeptName = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    EmployeewiseEntity _Model = new EmployeewiseEntity();
                    _Model.DeptName = DeptName;
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetNTDeptRecord, _Model);
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
                    Session["NTDept"] = ItemList;
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
        public JsonResult WelformDeptList(string startDate = "", string endDate = "", string DeptName = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    WelformEntity _Model = new WelformEntity();
                    _Model.DeptName = DeptName;
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetWFDeptRecord, _Model);
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
                    Session["WFDept"] = ItemList;
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
        public JsonResult AppearlDeptList(string startDate = "", string endDate = "", string DeptName = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    ApprealInfoEntity _Model = new ApprealInfoEntity();
                    _Model.DeptName = DeptName;
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAPPDeptRecord, _Model);
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
                    Session["APPDept"] = ItemList;
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
        public JsonResult BDDeptList(string startDate = "", string endDate = "", string DeptName = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    BDEmployeeEntity _Model = new BDEmployeeEntity();
                    _Model.DeptName = DeptName;
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetBDDeptRecord, _Model);
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
                    Session["BDDept"] = ItemList;
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


         public ActionResult ManagerDeptrpt()
        {
            ManagerinforptEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/MGRDeptrpt.rpt");
            rptH.Load();

            List<ManagerinfoEntity> ItemList = (List<ManagerinfoEntity>)Session["MGRDept"];

            foreach (ManagerinfoEntity dr in ItemList)
            {
                obj = new ManagerinforptEntity();

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
         public ActionResult NorthtowerDeptrpt()
         {
             EmployeeWise obj;


             ReportClass rptH = new ReportClass();
             ArrayList al = new ArrayList();
             rptH.FileName = Server.MapPath("/Reports/NTDeptrpt.rpt");
             rptH.Load();

             List<EmployeewiseEntity> ItemList = (List<EmployeewiseEntity>)Session["NTDept"];

             foreach (EmployeewiseEntity dr in ItemList)
             {
                 obj = new EmployeeWise();

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
         public ActionResult NorthtowerDeptrptExcel()
         {
             EmployeeWise obj;


             ReportClass rptH = new ReportClass();
             ArrayList al = new ArrayList();
             rptH.FileName = Server.MapPath("/Reports/NTDeptrpt.rpt");
             rptH.Load();

             List<EmployeewiseEntity> ItemList = (List<EmployeewiseEntity>)Session["NTDept"];

             foreach (EmployeewiseEntity dr in ItemList)
             {
                 obj = new EmployeeWise();

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
             return File(stream, "application/octet-stream", "NorthTower.xls");    //For Excel with File Name
         }
         public ActionResult WelformDeptrpt()
         {
             WelformWiserptEntity obj;


             ReportClass rptH = new ReportClass();
             ArrayList al = new ArrayList();
             rptH.FileName = Server.MapPath("/Reports/WelformDeptrpt.rpt");
             rptH.Load();

             List<WelformEntity> ItemList = (List<WelformEntity>)Session["WFDept"];

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
         public ActionResult AppearlDeptrpt()
         {
             ApparelWiserptEntity obj;


             ReportClass rptH = new ReportClass();
             ArrayList al = new ArrayList();
             rptH.FileName = Server.MapPath("/Reports/AppearlDeptrpt.rpt");
             rptH.Load();

             List<ApprealInfoEntity> ItemList = (List<ApprealInfoEntity>)Session["APPDept"];

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
         public ActionResult BDDeptrpt()
         {
             ApparelWiserptEntity obj;


             ReportClass rptH = new ReportClass();
             ArrayList al = new ArrayList();
             rptH.FileName = Server.MapPath("/Reports/BDDeptrpt.rpt");
             rptH.Load();

             List<BDEmployeeEntity> ItemList = (List<BDEmployeeEntity>)Session["BDDept"];

             foreach (BDEmployeeEntity dr in ItemList)
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

    }
}
