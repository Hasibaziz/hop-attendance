using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using ERP.Models;
using System.Data;
using ERP.DAL;

namespace ERP.Controllers
{
    public class AccountController : BaseController
    {
        [HttpGet]
        public ActionResult Login(LoginModel model)
        {
            ModelState.Clear();
            return View("Login", model);
        }
        [HttpPost]
        public ActionResult Login(string submit, LoginModel model)
        {
            USERS iUser = new USERS();
            DataTable dt = iUser.GetUserInfo(model);

            if (dt.Rows.Count > 0)
            {
                if ((model.UserName == dt.Rows[0]["UserName"].ToString())
                    && (model.Password == dt.Rows[0]["Password"].ToString()))
                {
                    //model.Id = dt.Rows[0]["UserName"].ToString();
                    model.UserName = dt.Rows[0]["UserName"].ToString();
                    model.Password = dt.Rows[0]["Password"].ToString();
                    SetLoginSessionData(model, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {

                    ModelState.AddModelError("UserName", "invalid username or password.");
                }
            }
            else
            {

                ModelState.AddModelError("UserName", "invalid username or password.");
            }

            return View("Login", model);
        }

        public ActionResult Logout(SystemUserModel model)
        {
            System.Web.Security.FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}
