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
    public class SummaryController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Northtowerdaycal()
        {
            NorthTowerdaycal Model = new NorthTowerdaycal();
            Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(Model);
        }
        public ActionResult Welformdaycal()
        {
            WelformdaycalEntity Model = new WelformdaycalEntity();
            Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(Model);
        }
        public ActionResult Managerdaycal()
        {
            ManagerdaycalEntity Model = new ManagerdaycalEntity();
            Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(Model);
        }
        public ActionResult Appearldaycal()
        {
            AppearldaycalEntity Model = new AppearldaycalEntity();
            Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(Model);
        }
        public ActionResult BDEmployeedaycal()
        {
            BDEmployeedaycal Model = new BDEmployeedaycal();
            Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(Model);
        }

        [HttpPost]
        public JsonResult NorthtowerdaycalList(string startDate = "", string endDate = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    NorthTowerdaycal _Model = new NorthTowerdaycal();
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetNorthtowerdaycalRecord, _Model);
                    List<NorthTowerdaycal> ItemList = null;
                    ItemList = new List<NorthTowerdaycal>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new NorthTowerdaycal()
                            {
                                EMPID = dr["EMPID"].ToString(),
                                ENAME = dr["ENAME"].ToString(),
                                Designation = dr["Designation"].ToString(),
                                SECTION = dr["SECTION"].ToString(),
                                JDate = dr["JDate"].ToString(),
                                Status = dr["Status"].ToString(),
                                TTDay = dr["TTDay"].ToString(),
                                Holiday = dr["Holiday"].ToString(),
                                Present = dr["Present"].ToString(),
                                Absent = dr["Absent"].ToString(),
                                CL = dr["CL"].ToString(),
                                SL = dr["SL"].ToString(),
                                ML = dr["ML"].ToString(),
                                EL = dr["EL"].ToString(),
                            });
                        }
                        iCount += 1;
                    }
                    var RecordCount = dt.Rows.Count;
                    var Record = ItemList;
                    Session["NTR"] = ItemList;
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
        public JsonResult WelformdaycalList(string startDate = "", string endDate = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    WelformdaycalEntity _Model = new WelformdaycalEntity();
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetWelformdaycalRecord, _Model);
                    List<WelformdaycalEntity> ItemList = null;
                    ItemList = new List<WelformdaycalEntity>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new WelformdaycalEntity()
                            {
                                EMPID = dr["EMPID"].ToString(),
                                ENAME = dr["ENAME"].ToString(),
                                SECTION = dr["SECTION"].ToString(),
                                JDate = dr["JDate"].ToString(),
                                Status = dr["Status"].ToString(),
                                TTDay = dr["TTDay"].ToString(),
                                Holiday = dr["Holiday"].ToString(),
                                Present = dr["Present"].ToString(),
                                Absent = dr["Absent"].ToString(),
                                CL = dr["CL"].ToString(),
                                SL = dr["SL"].ToString(),
                                ML = dr["ML"].ToString(),
                                EL = dr["EL"].ToString(),
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
        public JsonResult ManagerdaycalList(string startDate = "", string endDate = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    ManagerdaycalEntity _Model = new ManagerdaycalEntity();
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetManagerdaycalRecord, _Model);
                    List<ManagerdaycalEntity> ItemList = null;
                    ItemList = new List<ManagerdaycalEntity>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new ManagerdaycalEntity()
                            {
                                EMPID = dr["EMPID"].ToString(),
                                ENAME = dr["ENAME"].ToString(),
                                Designation = dr["Designation"].ToString(),
                                SECTION = dr["SECTION"].ToString(),
                                JDate = dr["JDate"].ToString(),
                                Status = dr["Status"].ToString(),
                                TTDay = dr["TTDay"].ToString(),
                                Holiday = dr["Holiday"].ToString(),
                                Present = dr["Present"].ToString(),
                                Absent = dr["Absent"].ToString(),
                                CL = dr["CL"].ToString(),
                                SL = dr["SL"].ToString(),
                                ML = dr["ML"].ToString(),
                                EL = dr["EL"].ToString(),
                            });
                        }
                        iCount += 1;
                    }
                    var RecordCount = dt.Rows.Count;
                    var Record = ItemList;
                    Session["MGR"] = ItemList;
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
        public JsonResult AppearldaycalList(string startDate = "", string endDate = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    AppearldaycalEntity _Model = new AppearldaycalEntity();
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAppearldaycalRecord, _Model);
                    List<AppearldaycalEntity> ItemList = null;
                    ItemList = new List<AppearldaycalEntity>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new AppearldaycalEntity()
                            {
                                EMPID = dr["EMPID"].ToString(),
                                ENAME = dr["ENAME"].ToString(),
                                SECTION = dr["SECTION"].ToString(),
                                JDate = dr["JDate"].ToString(),
                                Status = dr["Status"].ToString(),
                                TTDay = dr["TTDay"].ToString(),
                                Holiday = dr["Holiday"].ToString(),
                                Present = dr["Present"].ToString(),
                                Absent = dr["Absent"].ToString(),
                                CL = dr["CL"].ToString(),
                                SL = dr["SL"].ToString(),
                                ML = dr["ML"].ToString(),
                                EL = dr["EL"].ToString(),
                            });
                        }
                        iCount += 1;
                    }
                    var RecordCount = dt.Rows.Count;
                    var Record = ItemList;
                    Session["Appreal"] = ItemList;
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
        public JsonResult BDEmployeedaycalList(string startDate = "", string endDate = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    BDEmployeedaycal _Model = new BDEmployeedaycal();
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetBDEmployeedaycalRecord, _Model);
                    List<BDEmployeedaycal> ItemList = null;
                    ItemList = new List<BDEmployeedaycal>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new BDEmployeedaycal()
                            {
                                EMPID = dr["EMPID"].ToString(),
                                ENAME = dr["ENAME"].ToString(),
                                SECTION = dr["SECTION"].ToString(),
                                JDate = dr["JDate"].ToString(),
                                Status = dr["Status"].ToString(),
                                TTDay = dr["TTDay"].ToString(),
                                Holiday = dr["Holiday"].ToString(),
                                Present = dr["Present"].ToString(),
                                Absent = dr["Absent"].ToString(),
                                CL = dr["CL"].ToString(),
                                SL = dr["SL"].ToString(),
                                ML = dr["ML"].ToString(),
                                EL = dr["EL"].ToString(),
                            });
                        }
                        iCount += 1;
                    }
                    var RecordCount = dt.Rows.Count;
                    var Record = ItemList;
                    Session["BDSummary"] = ItemList;
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


        public ActionResult NorthTowerrpt()
        {
            NorthTowerrptEntity obj;
            //ReportClass rptH = new ReportClass();
            ReportDocument rpt = new ReportDocument();
            ArrayList al = new ArrayList();
            //rptH.FileName = Server.MapPath("/Reports/Northtowercal.rpt");
            //rptH.Load();
            rpt.Load(Server.MapPath("~/Reports/Northtowercal.rpt"));

            List<NorthTowerdaycal> ItemList = (List<NorthTowerdaycal>)Session["NTR"];

            foreach (NorthTowerdaycal dr in ItemList)
            {
                obj = new NorthTowerrptEntity();

                obj.EMPID = dr.EMPID;
                obj.ENAME = dr.ENAME;
                obj.Designation = dr.Designation;
                obj.SECTION = dr.SECTION;
                obj.JDate = dr.JDate;
                obj.Status = dr.Status;
                obj.TTDay = dr.TTDay;
                obj.Holiday = dr.Holiday;
                obj.Present = dr.Present;
                obj.Absent = dr.Absent;
                obj.CL = dr.CL;
                obj.SL = dr.SL;
                obj.ML = dr.ML;
                obj.EL = dr.EL;

                al.Add(obj);
            }

            rpt.SetDataSource(al);
            //MemoryStream stream=rptH.ExportToHttpResponse(ExportFormatType.PortableDocFormat, System.Web.HttpContext.Current.Response, false, "crReport");
            MemoryStream stream = (MemoryStream)rpt.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
            //return File(stream, "applicaton/vnd.ms-excel");
            // return File(stream, "application/octet-stream", "mytestfile.xls");

        }

        public ActionResult NorthTowerrptExcel()
        {
            NorthTowerrptEntity obj;
            //ReportClass rptH = new ReportClass();
            ReportDocument rpt = new ReportDocument();
            ArrayList al = new ArrayList();
            //rptH.FileName = Server.MapPath("/Reports/Northtowercal.rpt");
            //rptH.Load();
            rpt.Load(Server.MapPath("~/Reports/Northtowercal.rpt"));

            List<NorthTowerdaycal> ItemList = (List<NorthTowerdaycal>)Session["NTR"];

            foreach (NorthTowerdaycal dr in ItemList)
            {
                obj = new NorthTowerrptEntity();

                obj.EMPID = dr.EMPID;
                obj.ENAME = dr.ENAME;
                obj.SECTION = dr.SECTION;
                obj.JDate = dr.JDate;
                obj.Status = dr.Status;
                obj.TTDay = dr.TTDay;
                obj.Holiday = dr.Holiday;
                obj.Present = dr.Present;
                obj.Absent = dr.Absent;
                obj.CL = dr.CL;
                obj.SL = dr.SL;
                obj.ML = dr.ML;
                obj.EL = dr.EL;

                al.Add(obj);
            }

            rpt.SetDataSource(al);
            MemoryStream stream = (MemoryStream)rpt.ExportToStream(ExportFormatType.Excel);
            return File(stream, "application/octet-stream", "NorthTowerSummary.xls");

        }

        public ActionResult Welformdaycalrpt()
        {
            WelformdaycalrptEntity obj;
            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/Welformdaycalrpt.rpt");
            rptH.Load();

            List<WelformdaycalEntity> ItemList = (List<WelformdaycalEntity>)Session["Welform"];

            foreach (WelformdaycalEntity dr in ItemList)
            {
                obj = new WelformdaycalrptEntity();

                obj.EMPID = dr.EMPID;
                obj.ENAME = dr.ENAME;
                obj.SECTION = dr.SECTION;
                obj.JDate = dr.JDate;
                obj.Status = dr.Status;
                obj.TTDay = dr.TTDay;
                obj.Holiday = dr.Holiday;
                obj.Present = dr.Present;
                obj.Absent = dr.Absent;
                obj.CL = dr.CL;
                obj.SL = dr.SL;
                obj.ML = dr.ML;
                obj.EL = dr.EL;

                al.Add(obj);
            }

            rptH.SetDataSource(al);
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }

        public ActionResult Appearldaycalrpt()
        {
            AppearldaycalrptEntity obj;
            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/Appearldaycalrpt.rpt");
            rptH.Load();

            List<AppearldaycalEntity> ItemList = (List<AppearldaycalEntity>)Session["Appreal"];

            foreach (AppearldaycalEntity dr in ItemList)
            {
                obj = new AppearldaycalrptEntity();

                obj.EMPID = dr.EMPID;
                obj.ENAME = dr.ENAME;
                obj.SECTION = dr.SECTION;
                obj.JDate = dr.JDate;
                obj.Status = dr.Status;
                obj.TTDay = dr.TTDay;
                obj.Holiday = dr.Holiday;
                obj.Present = dr.Present;
                obj.Absent = dr.Absent;
                obj.CL = dr.CL;
                obj.SL = dr.SL;
                obj.ML = dr.ML;
                obj.EL = dr.EL;

                al.Add(obj);
            }

            rptH.SetDataSource(al);
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }

        public ActionResult BDEmployeedaycalrpt()
        {
            BDEmployeedaycalrpt obj;
            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/BDEmployeedayrpt.rpt");
            rptH.Load();

            List<BDEmployeedaycal> ItemList = (List<BDEmployeedaycal>)Session["BDSummary"];

            foreach (BDEmployeedaycal dr in ItemList)
            {
                obj = new BDEmployeedaycalrpt();

                obj.EMPID = dr.EMPID;
                obj.ENAME = dr.ENAME;
                obj.SECTION = dr.SECTION;
                obj.JDate = dr.JDate;
                obj.Status = dr.Status;
                obj.TTDay = dr.TTDay;
                obj.Holiday = dr.Holiday;
                obj.Present = dr.Present;
                obj.Absent = dr.Absent;
                obj.CL = dr.CL;
                obj.SL = dr.SL;
                obj.ML = dr.ML;
                obj.EL = dr.EL;

                al.Add(obj);
            }

            rptH.SetDataSource(al);
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
        }

        /// <summary>
        /// Excel-Export
        /// </summary>
        public ActionResult MgrdaycalExcel(string startDate = "", string endDate = "")
        {
            // DataTable dt = -- > get your data
            ManagerdaycalEntity _Model = new ManagerdaycalEntity();
            _Model.StartDate = startDate;
            _Model.EndDate = endDate;
            DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetManagerdaycalRecord, _Model);
            ERP.Utility.Excelimport.ExcelFileResult actionResult = new ERP.Utility.Excelimport.ExcelFileResult(dt) { FileDownloadName = "MgrdaycalExcel.xls" };
            return actionResult;
        }
        public ActionResult NTDaycalExcel(string startDate = "", string endDate = "")
        {
            // DataTable dt = -- > get your data
            NorthTowerdaycal _Model = new NorthTowerdaycal();
            _Model.StartDate = startDate;
            _Model.EndDate = endDate;
            DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetNorthtowerdaycalRecord, _Model);
            ERP.Utility.Excelimport.ExcelFileResult actionResult = new ERP.Utility.Excelimport.ExcelFileResult(dt) { FileDownloadName = "NTDaycalExcel.xls" };
            return actionResult;
        }
    }
}