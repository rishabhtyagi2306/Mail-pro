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
        public ActionResult Login(Models.Membership model, FacultyTable faculty)
        {
            using (var context = new MailProEntities())
            {
                FacultyTable Fac = new FacultyTable();
                FacultyTable faculty1 = new FacultyTable();
                faculty1 = context.FacultyTable.SingleOrDefault(m => m.FacultyEmail == model.FacultyEmail);
                var abc = faculty1.IsEmailVerified;
                Fac = context.FacultyTable.SingleOrDefault(n => n.FacultyEmail == model.FacultyEmail);
                int fid = Fac.FacultyID;                
                var y = Crypto.Hash(model.Password);
                bool IsValid = context.FacultyTable.Any(x => x.FacultyEmail == model.FacultyEmail && x.Password == y && abc!=null);
                if (IsValid)
                {
                    
                        FormsAuthentication.SetAuthCookie(model.FacultyEmail, false);
                        
                        Session["FacultyID"] = fid;
                        
                        return RedirectToAction("ShowSentEmail", "Email");
                    
                }
                else
                {
                    ViewBag.Msg = "Invalid Email or Password";
                }
                context.SaveChanges();
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
                var IsExist = IsEmailExist(model.FacultyEmail);
                if(IsExist)
                {
                    ModelState.AddModelError("Email Exist", "Email already exist");
                    return View();
                }
                Fac.ActivationCode = Guid.NewGuid();
                model.ActivationCode = Fac.ActivationCode;
                model.Password = Crypto.Hash(model.Password);
                context.FacultyTable.Add(model);
                context.SaveChanges();
                Session["FacultyEmail"] = model.FacultyEmail;
                EmailVerification(model.FacultyID, model.FacultyEmail, Fac.ActivationCode.ToString());
                //ViewBag.Message = "A veification link has been sent to your email , Please verify to continue access";
            }
            //FacultyTable.Password = Crypto.Hash(FacultyTable.Password);
            return RedirectToAction("VMSent");
            //return View(model);
        }

        public ActionResult VMSent()
        {
            String Email = Session["FacultyEmail"].ToString();
            ViewBag.Email = Email;
            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ForgotPassword(string EmailID)
        {
            string message = "";
            bool status = false;

            using(MailProEntities Mp = new MailProEntities())
            {
                var acc = Mp.FacultyTable.Where(x => x.FacultyEmail == EmailID).FirstOrDefault();
                if(acc != null)
                {
                    string ResetCode = Guid.NewGuid().ToString();
                    EmailVerification(acc.FacultyID, acc.FacultyEmail, ResetCode, "ResetPassword");
                    acc.ResetPasswordCode = ResetCode;
                    Mp.Configuration.ValidateOnSaveEnabled = false;
                    Mp.SaveChanges();
                    message = "Reset password link has been sent your your email";
                }
                else
                {
                    message = "Account not found";
                }
            }
            ViewBag.Message = message;
            return View();
        }

        public ActionResult ResetPassword(string ID)
        {
            using (MailProEntities Mp = new MailProEntities())
            {
                var user = Mp.FacultyTable.Where(x => x.ResetPasswordCode == ID).FirstOrDefault();
                if(user !=null)
                {
                    ResetPasswordModel model = new ResetPasswordModel();
                    model.ResetCode = ID;
                    return View(model);
                }
                else
                {
                    return HttpNotFound();
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            var message = "";
            if(ModelState.IsValid)
            {
                using (MailProEntities Mp = new MailProEntities())
                {
                    var user = Mp.FacultyTable.Where(x => x.ResetPasswordCode == model.ResetCode).FirstOrDefault();
                    if(user != null)
                    {
                        user.Password = Crypto.Hash(model.NewPassword);
                        user.ResetPasswordCode = "";
                        Mp.Configuration.ValidateOnSaveEnabled = false;
                        Mp.SaveChanges();
                        message = "New Psssword updated successfully";
                    }
                }
            }
            else
            {
                message = "Something Invalid";
            }
            ViewBag.Message = message;
            return View(model);
        }

        [HttpGet]
        public ActionResult VerifyAccount(string id, Models.Membership model)
        {
            bool status = false;
            using (var context = new MailProEntities())
            {
                FacultyTable Fac = new FacultyTable();
                //Mp.Configuration.ValidateOnSaveEnabled = false;
                var v = context.FacultyTable.Where(a => a.ActivationCode == new Guid(id)).FirstOrDefault();
                if(v != null)
                {
                    Fac.IsEmailVerified = true;
                    v.IsEmailVerified = Convert.ToBoolean(Fac.IsEmailVerified);
                    context.SaveChanges();
                    status = true;
                }
                else
                {
                    ViewBag.Message = "invalid request";
                }
                Session["IsEmailVerified"] = model.IsEmailVerified;
            }
            ViewBag.Status = status;
            return View();
        }

        [NonAction]
        public bool IsEmailExist(string FacultyEmail)
        {
            using(MailProEntities Mp = new MailProEntities())
            {
                var v = Mp.FacultyTable.Where(a => a.FacultyEmail == FacultyEmail).FirstOrDefault();
                return v != null;
            }
        }

        [NonAction]
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Code Quality", "IDE0067:Dispose objects before losing scope", Justification = "<Pending>")]
        public void EmailVerification(int FacultyID, string FacultyEmail, string ActivationCode, string EmailFor = "VerifyAccount")
        {
            var verifyUrl = "/Account/"+EmailFor+"/" + ActivationCode;
            var link = Request.Url.AbsoluteUri.Replace(Request.Url.PathAndQuery, verifyUrl);
            var FromEmail = new MailAddress("4as1827000224@gmail.com", "Mail Pro");
            var ToEmail = new MailAddress(FacultyEmail);
            var FromEmailPassword = "Rishabh@2306";
<<<<<<< HEAD
            string Subject = "Email Verification for Mail Pro Account";
            string Body = "Your Faculty ID" + " = '" + FacultyID + "'" + "<br/>Please click on the link below to verify your account" + 
                "<br/><br/><a href = '" + link + "'>" + link + "<a/>";
=======
            string Subject = "";
            string Body = "";
            if(EmailFor == "VerifyAccount")
            {
                Subject = "Email Verification for Mail Pro Account";
                Body = "Your Faculty ID" + " = '" + FacultyID + "'" + "<br/>Please click on the link below to verify your account" +
                    "<br/><br/><a href = '" + link + "'>" + link + "<a/>";
            }
            else if(EmailFor == "ResetPassword")
            {
                Subject = "Reset Password";
                Body = "Hi,<br/><br/>Forgot your password , Don't worry click on the link below to reset your password<br/><br/><a href= '" + link + "'>"+link+"<a/>";
            }
>>>>>>> Rishabh

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
                Subject = Subject,
                Body = Body,
                IsBodyHtml = true
            })

                smtp.Send(message);
        }
    }
}