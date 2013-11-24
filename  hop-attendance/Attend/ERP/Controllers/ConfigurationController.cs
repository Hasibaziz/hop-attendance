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
using Microsoft.Reporting.WebForms;

namespace ERP.Controllers
{
    public class ConfigurationController : BaseController
    {
        //
        // GET: /Configuration/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AttenInfo()
        {
            AttendanceEntity Model = new AttendanceEntity();
            Model.StartDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View("AttenInfo", Model);
        }
        public ActionResult ApprealInfo()
        {
            ApprealInfoEntity Model = new ApprealInfoEntity();
            Model.StartDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(Model);
        }
        public ActionResult ApprealWise()
        {
            ApprealInfoEntity _Model = new ApprealInfoEntity();
            ViewData["EMPID"] = GetAllAppearlListItem();
            _Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            _Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(_Model);
        }
        public ActionResult ManagerInfo()
        {
            ManagerinfoEntity Model = new ManagerinfoEntity();
            Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(Model);
        }
        public ActionResult ManagerWise()
        {
            ManagerinfoEntity Model = new ManagerinfoEntity();
            ViewData["EMPID"] = GetAllManagerListItem();
            Model.StartDate = DateTime.Today.ToString("dd/MM/yyyy");
            Model.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(Model);
        }
        public ActionResult ServiceName()
        {
            return View();
        }
        public ActionResult ServiceDetails()
        {
            return View();
        }
        public ActionResult Welform()
        {
            WelformEntity iWelform = new WelformEntity();
            iWelform.StartDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            iWelform.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View("Welform", iWelform);
        }
        public ActionResult WelformWise()
        {
            WelformEntity iWelform = new WelformEntity();
            ViewData["EMPID"] = GetAllWelformWiseListItem();
            iWelform.StartDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            iWelform.EndDate = DateTime.Today.Date.ToString("dd/MM/yyyy");
            return View(iWelform);
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
        public ActionResult BDEmployee()
        {
            BDEmployeeEntity _Model = new BDEmployeeEntity();
            ViewData["EMPID"] = GetAllBDEmployeewiseListItem();
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

        [HttpPost]
        public JsonResult ServiceNameList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAllTrServicemasterRecord, null);
                    List<TrServicemasterEntity> ItemList = null;
                    ItemList = new List<TrServicemasterEntity>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new TrServicemasterEntity()
                            {
                                Id = dr["ID"].ToString(),
                                Servicename = dr["Servicename"].ToString(),
                                Description = dr["Description"].ToString()
                            });
                        }
                        iCount += 1;
                    }
                    var RecordCount = dt.Rows.Count;
                    var Record = ItemList;
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
        public JsonResult AddUpdateServiceName(TrServicemasterEntity _Model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }


                bool isUpdate = false;
                if (_Model.Id == null)
                    isUpdate = (bool)ExecuteDB(ERPTask.AG_SaveTrServicemasterInfo, _Model);
                else
                    isUpdate = (bool)ExecuteDB(ERPTask.AG_UpdateTrServicemasterInfo, _Model);
                if (isUpdate)
                {
                    var addedModel = _Model;
                    return Json(new { Result = "OK", Record = addedModel });
                }
                else
                    return Json(new { Result = "ERROR", Message = "Information failed to save" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteServiceName(string ID)
        {
            try
            {
                Thread.Sleep(50);
                bool isUpdate = false;
                isUpdate = (bool)ExecuteDB(ERPTask.AG_DeleteTrServicemasterInfoById, ID);
                if (isUpdate)
                    return Json(new { Result = "OK" });
                else
                    return Json(new { Result = "ERROR", Message = "Failed to Delete" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult AllServiceNameListItem()
        {
            try
            {
                var jList = GetAllServiceNameListItem().Select(c => new { DisplayText = c.Text, Value = c.Value });
                return Json(new { Result = "OK", Options = jList });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult ServiceNameDetilsList(int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAllTrServicedetailsRecord, null);
                    List<TrServicedetailsEntity> ItemList = null;
                    ItemList = new List<TrServicedetailsEntity>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new TrServicedetailsEntity()
                            {
                                Id = dr["ID"].ToString(),
                                Srvicenameid = dr["Srvicenameid"].ToString(),
                                Detailsname = dr["Detailsname"].ToString(),
                                Govfee = dr["Govfee"].ToString(),
                                Servicefee = dr["Servicefee"].ToString(),
                                Othersfee = dr["Othersfee"].ToString(),
                                Fixedfigure =Convert.ToBoolean( dr["Fixedfigure"].ToString()),
                                Cc = dr["Cc"].ToString(),
                                Sit = dr["Sit"].ToString()
                            });
                        }
                        iCount += 1;
                    }
                    var RecordCount = dt.Rows.Count;
                    var Record = ItemList;
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
        public JsonResult AddUpdateServiceNameDetils(TrServicedetailsEntity _Model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return Json(new { Result = "ERROR", Message = "Form is not valid! Please correct it and try again." });
                }


                bool isUpdate = false;
                if (_Model.Id == null)
                    isUpdate = (bool)ExecuteDB(ERPTask.AG_SaveTrServicedetailsInfo, _Model);
                else
                    isUpdate = (bool)ExecuteDB(ERPTask.AG_UpdateTrServicedetailsInfo, _Model);
                if (isUpdate)
                {
                    var addedModel = _Model;
                    return Json(new { Result = "OK", Record = addedModel });
                }
                else
                    return Json(new { Result = "ERROR", Message = "Information failed to save" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }
        [HttpPost]
        public JsonResult DeleteServiceNameDetils(string ID)
        {
            try
            {
                Thread.Sleep(50);
                bool isUpdate = false;
                isUpdate = (bool)ExecuteDB(ERPTask.AG_DeleteTrServicedetailsInfoById, ID);
                if (isUpdate)
                    return Json(new { Result = "OK" });
                else
                    return Json(new { Result = "ERROR", Message = "Failed to Delete" });
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult AttenInfoList(string startDate="", string endDate="", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    AttendanceEntity _Model = new AttendanceEntity();
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAttenInfoRecord, _Model);
                    List<AttendanceEntity> ItemList = null;
                    ItemList = new List<AttendanceEntity>();
                    int iCount = 0;
                    int offset = 0;
                    offset = jtStartIndex / jtPageSize;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (iCount >= jtStartIndex && iCount < (jtPageSize * (offset + 1)))
                        {
                            ItemList.Add(new AttendanceEntity()
                            {
                                SL = (iCount + 1).ToString(),
                                PDate =dr["PDate"].ToString(),
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
                    Session["opl"] = ItemList;
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
        public JsonResult WelformInfoList(string startDate = "", string endDate = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    WelformEntity _Model = new WelformEntity();
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetWelformRecord, _Model);
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
                    Session["wel"] = ItemList;
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
        public JsonResult ApprealInfoList(string startDate = "", string endDate = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    ApprealInfoEntity _Model = new ApprealInfoEntity();
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetApprealInfoRecord, _Model);
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
                    Session["wel"] = ItemList;
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
                    Session["wel"] = ItemList;
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
        public JsonResult EmployeeWiseList(string startDate = "", string endDate = "", string Empid="", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
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
        public JsonResult ManagerInfoList(string startDate = "", string endDate = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    ManagerinfoEntity _Model = new ManagerinfoEntity();
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetManagerRecord, _Model);
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
                    Session["ALLMGR"] = ItemList;
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
        public JsonResult BDEmployeeList(string startDate = "", string endDate = "", int jtStartIndex = 0, int jtPageSize = 0, string jtSorting = null)
        {
            try
            {
                try
                {
                    BDEmployeeEntity _Model = new BDEmployeeEntity();
                    _Model.StartDate = startDate;
                    _Model.EndDate = endDate;
                    DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetBDEmployeeRecord, _Model);
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
                    Session["opl"] = ItemList;
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
        public ActionResult ALLManagerRPT()
        {
            ALLManagerrptEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/ALLManagerrpt.rpt");
            rptH.Load();

            List<ManagerinfoEntity> ItemList = (List<ManagerinfoEntity>)Session["ALLMGR"];

            foreach (ManagerinfoEntity dr in ItemList)
            {
                obj = new ALLManagerrptEntity();

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
        public ActionResult ALLManagerRPTEXCEL()
        {
            ALLManagerrptEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/ALLManagerrpt.rpt");
            rptH.Load();

            List<ManagerinfoEntity> ItemList = (List<ManagerinfoEntity>)Session["ALLMGR"];

            foreach (ManagerinfoEntity dr in ItemList)
            {
                obj = new ALLManagerrptEntity();

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
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.Excel);
            return File(stream, "application/octet-stream", "ManagerAttend.xls");
        } 
        public ActionResult ReportView01()
        {
            AttendanceEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/CrystalReport1.rpt");
            rptH.Load();

            List<AttendanceEntity> ItemList = (List<AttendanceEntity>)Session["opl"];

            foreach (AttendanceEntity dr in ItemList)
            {
                obj = new AttendanceEntity();

                obj.PDate = dr.PDate;
                obj.EMPID = dr.EMPID;
                obj.EName = dr.EName;
                obj.Designation = dr.Designation;
                obj.DeptName = dr.DeptName;
                obj.Intime = dr.Intime;
                obj.Outtime=dr.Outtime;
                obj.Status = dr.Status;
                al.Add(obj);
            }

            rptH.SetDataSource(al);
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");
           
        }
        public ActionResult ReportViewExcel()
        {
            AttendanceEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/CrystalReport1.rpt");
            rptH.Load();

            List<AttendanceEntity> ItemList = (List<AttendanceEntity>)Session["opl"];

            foreach (AttendanceEntity dr in ItemList)
            {
                obj = new AttendanceEntity();

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
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.Excel);
            return File(stream, "application/octet-stream", "NorthTower.xls");
        }
        public ActionResult EmployeeWiserpt()
        {
            EmployeewiseEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/EmployeeWiserpt.rpt");
            rptH.Load();

            List<EmployeewiseEntity> ItemList = (List<EmployeewiseEntity>)Session["EMPWISE"];

            foreach (EmployeewiseEntity dr in ItemList)
            {
                obj = new EmployeewiseEntity();

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
        public ActionResult EmployeeWiserptExcel()
        {
            EmployeewiseEntity obj;


            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/EmployeeWiserpt.rpt");
            rptH.Load();

            List<EmployeewiseEntity> ItemList = (List<EmployeewiseEntity>)Session["EMPWISE"];

            foreach (EmployeewiseEntity dr in ItemList)
            {
                obj = new EmployeewiseEntity();

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
        public ActionResult AllManagerInforpt()
        {
            ManagerinforptEntity obj;
            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/AllManagerInforpt.rpt");
            rptH.Load();

            List<ManagerinfoEntity> ItemList = (List<ManagerinfoEntity>)Session["INDMGR"];

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
        public ActionResult AllManagerInforptExcel()
        {
            ManagerinforptEntity obj;
            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/AllManagerInforpt.rpt");
            rptH.Load();

            List<ManagerinfoEntity> ItemList = (List<ManagerinfoEntity>)Session["INDMGR"];

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
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.Excel);   //For Excel File
            return File(stream, "application/octet-stream", "ManagerInfo.xls");    //For Excel with File Name

        }
        public ActionResult BDEmployeedaycalrpt()
        {
            ManagerdaycalEntity obj;
            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/BDEmployeedaycalrpt.rpt");
            rptH.Load();

            List<ManagerdaycalEntity> ItemList = (List<ManagerdaycalEntity>)Session["BDSummary"];

            foreach (ManagerdaycalEntity dr in ItemList)
            {
                obj = new ManagerdaycalEntity();

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
        public ActionResult NorthTowerrptall(string p1 = "", string p2 = "")
        {
            AttendanceEntity _Model = new AttendanceEntity();
            _Model.StartDate = p1;
            _Model.EndDate = p2;
            AttendanceEntity obj;
          
            //ReportClass rptH = new ReportClass();
            ReportClass rpt = new ReportClass();
            ArrayList al = new ArrayList();
            rpt.FileName = Server.MapPath("/Reports/CrystalReport1.rpt");
            rpt.Load();

            DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAttenInfoRecord, _Model);
            List<AttendanceEntity> ItemList = null;
            ItemList = new List<AttendanceEntity>();
           // List<NorthTowerdaycal> ItemList = (List<NorthTowerdaycal>)Session["NTR"];
           foreach (DataRow dr in dt.Rows)
           {
               ItemList.Add(new AttendanceEntity()
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
           foreach (AttendanceEntity dr in ItemList)
           {
               obj = new AttendanceEntity();

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
            rpt.SetDataSource(al);
            MemoryStream stream = (MemoryStream)rpt.ExportToStream(ExportFormatType.Excel);
            return File(stream, "application/octet-stream", "ManagerAttend.xls");     

        }

        public ActionResult ManagerRPTEXCELall(string startDate = "", string endDate="")
        {
            ManagerinfoEntity _Model = new ManagerinfoEntity();
            _Model.StartDate = startDate;
            _Model.EndDate = endDate;
            ALLManagerrptEntity obj;

            ReportClass rptH = new ReportClass();
            ArrayList al = new ArrayList();
            rptH.FileName = Server.MapPath("/Reports/ALLManagerrpt.rpt");
            rptH.Load();
           
            DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetManagerRecord, _Model);
            List<ManagerinfoEntity> ItemList = null;
            ItemList = new List<ManagerinfoEntity>();
           
            foreach (DataRow dr in dt.Rows)
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
            foreach (ManagerinfoEntity dr in ItemList)
            {
                obj = new ALLManagerrptEntity();

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
            MemoryStream stream = (MemoryStream)rptH.ExportToStream(ExportFormatType.Excel);
            return File(stream, "application/octet-stream", "ManagerAttend.xls");
        }


        public ActionResult ExcelReport(string EX1="", string EX2="")
        {

            AttendanceEntity _Model = new AttendanceEntity();
            _Model.StartDate = EX1;
            _Model.EndDate = EX2;
            DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAttenInfoRecord, _Model);
            StringBuilder sb = new StringBuilder();
            sb.Append("<table border='" + "2px" + "'b>");

            ////For Header
            sb.Append("<td><td><td><b><font face=Arial size=2>" + "Attendance Report" + "</font></b></td></td></td>");
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
            ////For Footer
            sb.Append("<tr>");
            sb.Append("<tr>");
            sb.Append("<td>");
            sb.Append("<td>");
            sb.Append("<td>");
            sb.Append("<td>");
            sb.Append("<td><b><font face=Arial size=2>" + "Powered By: Hasib, IT Department" + "</font></b></td>");
            sb.Append("</td>");
            sb.Append("</td>");
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</tr>");
            sb.Append("</table>");

            this.Response.ContentType = "application/vnd.ms-excel";
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(sb.ToString());
            return File(buffer, "application/vnd.ms-excel", "NorthTower.xls");                   
        }

       public ActionResult MGRExcelReport(string startDate = "", string endDate = "")
        {

             ManagerinfoEntity _Model = new ManagerinfoEntity();
            _Model.StartDate = startDate;
            _Model.EndDate = endDate;
            DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetManagerRecord, _Model);
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
            return File(buffer, "application/vnd.ms-excel", "MGRAttendance.xls");
        }

        public ActionResult ReportView()
        { 
            ReportClass rptH = new ReportClass();
            DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetManagerReportView, null);
            rptH.FileName = Server.MapPath("/Reports/ALLManagerrpt.rpt");
            rptH.Load();
            rptH.SetDataSource(dt);
            Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(stream, "application/pdf");   
        }
       
    }
}
