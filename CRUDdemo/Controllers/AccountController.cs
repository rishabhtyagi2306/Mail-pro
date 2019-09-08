using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demoModel.Model;
using demoDB.Db.DbOperations;
using demoDB.Db;
//using System.Web.Security;

namespace CRUDdemo.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]

        public ActionResult Login(Membership model)
        {
            using (var context = new demoCRUDEntities())
            {
                bool isvalid = context.user.Any(x => x.User_name == model.User_name && x.Password == model.Password);
                if(isvalid)
                {
                    //FormsAuthentication.SetAuthCookie(model.User_name, false);
                    return RedirectToAction("GetAllStudents" , "Home");
                }
                ModelState.AddModelError("", "Invalid user name and password");
                return View();
            }
            
        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(user model)
        {
            using (var context = new demoDB.Db.demoCRUDEntities())
            {
                context.user.Add(model);
                context.SaveChanges();
            }
            return RedirectToAction("Login");
            
        }
    }
}