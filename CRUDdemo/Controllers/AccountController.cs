using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demoModel.Model;
using demoDB.Db.DbOperations;
using demoDB.Db;

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
            return View();
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