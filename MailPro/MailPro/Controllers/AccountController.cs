﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailPro.Models;
using System.Web.Security;

namespace MailPro.Controllers
{
    //[Authorize]
    public class AccountController : Controller
    {
        // GET: Account

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var context = new MailProEntities())
            {
                var y = Crypto.Hash(model.Password);
                bool IsValid = context.FacultyTable.Any(x => x.FacultyEmail == model.FacultyEmail && x.Password == y);
                if (IsValid)
                {
                    FormsAuthentication.SetAuthCookie(model.FacultyEmail, false);
                    return RedirectToAction("Index", "GetAllSudents");
                }
                ModelState.AddModelError("", "Invalid Email or Password");
                return View();
            }

        }

        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(FacultyTable model)
        {
            //FacultyTable.Password = Crypto.Hash(FacultyTable.Password);
            using (var context = new MailProEntities())
            {
                //FacultyTable.Password = Crypto.Hash(FacultyTable.Password);
                //FacultyTable Fac = new FacultyTable();
                //Fac.Password = Crypto.Hash(Fac.Password);
                model.Password = Crypto.Hash(model.Password);
                context.FacultyTable.Add(model);
                context.SaveChanges();
            }
            //FacultyTable.Password = Crypto.Hash(FacultyTable.Password);
            return RedirectToAction("Login");
        }
    }
}