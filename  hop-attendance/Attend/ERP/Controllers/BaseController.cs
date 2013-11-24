using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using ERP.Models;
using ERP.Structure;
using ERP.Domain.Model;
using System.Data;



namespace ERP.Controllers
{
    public class BaseController : Controller
    {
        public object ExecuteDB(string methodName, object param)
        {
            object retObject = ServerManager.GetTaskManager.Execute(1, methodName, param);
            return retObject;
        }

        public string CurrentUserId
        {
            get
            {
                if (Session["UserId"] != null)
                {
                    return (Session["UserId"].ToString());
                }

                return string.Empty;
            }
            set
            {
                Session["UserId"] = value;
            }
        }

        public string CurrentUserName
        {
            get
            {
                if (Session["UserName"] != null)
                {
                    return Session["UserName"].ToString();
                }

                return string.Empty;
            }

            set { Session["UserName"] = value; }
        }

        public string LoginDatetime
        {
            get
            {
                if (Session["LoginDatetime"] != null)
                {
                    return Session["LoginDatetime"].ToString();
                }

                return string.Empty;
            }

            set { Session["LoginDatetime"] = value; }
        }

        public string CurrentUserEmail
        {
            get
            {
                if (Session["UserEmail"] != null)
                {
                    return Session["UserEmail"].ToString();
                }

                return string.Empty;
            }

            set { Session["UserEmail"] = value; }
        }

        public string CurrentUserPassword
        {
            get
            {
                if (Session["UserPassword"] != null)
                {
                    return (Session["UserPassword"].ToString());
                }

                return string.Empty;
            }
            set
            {
                Session["UserPassword"] = value;
            }
        }
        protected void SetLoginSessionData(LoginModel LoginM, bool createPersistentCookie)
        {
            SetUserSessionData(LoginM);
            FormsAuthentication.SetAuthCookie("1", createPersistentCookie);
            Session.Timeout = 1;

        }

        protected void SetUserSessionData(LoginModel LoginM)
        {
            //CurrentUserName = LoginM.Id + " " + LoginM.Password;
            CurrentUserPassword = LoginM.Password;
            CurrentUserName = LoginM.UserName;
            LoginDatetime = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            CurrentUserId = LoginM.USERID;
        }

        public bool isValidField(string _val)
        {
            if (_val == null)
                return false;
            else if (_val.Trim().Length == 0)
                return false;
            else if (_val.Trim() == "Select")
                return false;
            else if (_val.Trim() == "0")
                return false;
            return true;
        }
        public bool isValidNumericField(string _val)
        {
            if (_val == null)
                return false;
            else if (_val.Trim().Length == 0)
                return false;
            else if (_val.Trim() == "0")
                return false;
            else
            {
                try
                {
                    double dl = Convert.ToDouble(_val.Trim());
                    if (dl < 0)
                        return false;
                }
                catch { return false; }
            }
            return true;
        }
        public string ConvertNulltoString(string _val)
        {
            if (_val == null)
                return "";
            else
                return _val;
        }

        public string GetConvertedDate(string _dt)
        {
            if (_dt == null) return null;

            string[] _Convertdate = _dt.Split('/');
            string _dtNewdate = _Convertdate[1] + "/" + _Convertdate[0] + "/" + _Convertdate[2];
            return _dtNewdate;
        }


        public List<SelectListItem> GetAllServiceNameListItem()
        {
            try
            {
                DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAllTrServicemasterRecord, null);
                List<SelectListItem> ItemList = null;
                ItemList = new List<SelectListItem>();
                foreach (DataRow dr in dt.Rows)
                {
                    ItemList.Add(new SelectListItem()
                    {
                        Value = dr["ID"].ToString(),
                        Text = dr["Servicename"].ToString()
                    });

                }
                return ItemList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SelectListItem> GetAllEmployeewiseListItem()
        {
            try
            {
                DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAllEmployeewiseRecord, null);
                List<SelectListItem> ItemList = null;
                ItemList = new List<SelectListItem>();
                foreach (DataRow dr in dt.Rows)
                {
                    ItemList.Add(new SelectListItem()
                    {
                        Text = dr["EMPID"].ToString()
                    });

                }
                return ItemList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SelectListItem> GetAllManagerListItem()
        {
            try
            {
                DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAllManagerRecord, null);
                List<SelectListItem> ItemList = null;
                ItemList = new List<SelectListItem>();
                foreach (DataRow dr in dt.Rows)
                {
                    ItemList.Add(new SelectListItem()
                    {
                        Text = dr["EMPID"].ToString()
                    });

                }
                return ItemList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SelectListItem> GetAllBDEmployeewiseListItem()
        {
            try
            {
                DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAllBDEmployeewiseRecord, null);
                List<SelectListItem> ItemList = null;
                ItemList = new List<SelectListItem>();
                foreach (DataRow dr in dt.Rows)
                {
                    ItemList.Add(new SelectListItem()
                    {
                        Text = dr["EMPID"].ToString()
                    });

                }
                return ItemList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SelectListItem> GetAllWelformWiseListItem()
        {
            try
            {
                DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAllWelformWiseRecord, null);
                List<SelectListItem> ItemList = null;
                ItemList = new List<SelectListItem>();
                foreach (DataRow dr in dt.Rows)
                {
                    ItemList.Add(new SelectListItem()
                    {
                        Text = dr["EMPID"].ToString()
                    });

                }
                return ItemList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<SelectListItem> GetAllAppearlListItem()
        {
            try
            {
                DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAllAppearlRecord, null);
                List<SelectListItem> ItemList = null;
                ItemList = new List<SelectListItem>();
                foreach (DataRow dr in dt.Rows)
                {
                    ItemList.Add(new SelectListItem()
                    {
                        Text = dr["EMPID"].ToString()
                    });

                }
                return ItemList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<SelectListItem> GetAllDepatementListItem()
        {
            try
            {
                DataTable dt = (DataTable)ExecuteDB(ERPTask.AG_GetAllDeptRecord, null);
                List<SelectListItem> ItemList = null;
                ItemList = new List<SelectListItem>();
                foreach (DataRow dr in dt.Rows)
                {
                    ItemList.Add(new SelectListItem()
                    {
                        Text = dr["DeptName"].ToString()
                    });

                }
                return ItemList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public JsonResult UserConnected()
        {
            string ip = Request.UserHostAddress;

            if (MvcApplication.ConnectedtUsers.ContainsKey(ip))
            {
                MvcApplication.ConnectedtUsers[ip] = DateTime.Now;
            }
            else
            {
                MvcApplication.ConnectedtUsers.Add(ip, DateTime.Now);
            }

            int connected = MvcApplication.ConnectedtUsers.Where(c => c.Value.AddSeconds(30d) > DateTime.Now).Count();

            foreach (string key in MvcApplication.ConnectedtUsers.Where(c => c.Value.AddSeconds(30d) < DateTime.Now).Select(c => c.Key))
            {
                MvcApplication.ConnectedtUsers.Remove(key);
            }

            return Json(new { count = connected }, JsonRequestBehavior.AllowGet);
        }

        public class MyExpirePageActionFilterAttribute : System.Web.Mvc.ActionFilterAttribute
        {
            public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
            {
                base.OnActionExecuted(filterContext);

                filterContext.HttpContext.Response.Expires = -1;
                filterContext.HttpContext.Response.Cache.SetNoServerCaching();
                filterContext.HttpContext.Response.Cache.SetAllowResponseInBrowserHistory(false);
                filterContext.HttpContext.Response.CacheControl = "no-cache";
                filterContext.HttpContext.Response.Cache.SetNoStore();

            }
        }


    }
}
