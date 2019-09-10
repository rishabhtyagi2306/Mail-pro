using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MailPro.Models;
using System.Web.Security;
using System.Net.Mail;
using System.Net;

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
                FacultyTable Fac = new FacultyTable();
                //Fac.Password = Crypto.Hash(Fac.Password);
                Fac.ActivationCode = Guid.NewGuid();
               
                model.Password = Crypto.Hash(model.Password);
                context.FacultyTable.Add(model);
                context.SaveChanges();
                EmailVerification(model.FacultyEmail, Fac.ActivationCode.ToString());
            }
            //FacultyTable.Password = Crypto.Hash(FacultyTable.Password);
            return RedirectToAction("Login");
        }

        [NonAction]
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "<Pending>")]
        public void EmailVerification(string FacultyEmail, string ActivationCode )
        {
            var verifyUrl = "/Account/VerifyAccount" + ActivationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var FromEmail = new MailAddress("4as1827000224@gmail.com", "Rishabh Tyagi");
            var ToEmail = new MailAddress(FacultyEmail);
            var FromEmailPassword = "**********";
            string Body = "<br/>Please click on the link below to verify your account" +
                "<br/><br/><a href = '" + link + "'>" + link + "<a/>";

            SmtpClient smtp = new SmtpClient()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(FromEmail.Address, FromEmailPassword)
            };

            using (var message = new MailMessage(FromEmail, ToEmail)
            {
                Body = Body,
                IsBodyHtml = true
            })

                smtp.Send(message);
        }
    }
}