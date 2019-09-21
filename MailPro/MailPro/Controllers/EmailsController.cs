using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;
using MailPro.Models;

namespace MailPro.Controllers
{
    [Authorize]
    public class EmailController : Controller
    {
        DataContext Db = new DataContext();
        // GET: Email
        [Authorize]
        public ActionResult Mail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Mail(Mails model)
        {
           
            using (var context = new MailProEntities())
            {
                //FacultyTable.Password = Crypto.Hash(FacultyTable.Password);
                //FacultyTable Fac = new FacultyTable();

                //Fac.Password = Crypto.Hash(Fac.Password);
                int Fac = (int)Session["FacultyID"];
                model.FacultyID = Fac;
                context.Mails.Add(model);
                context.SaveChanges();
                EmailSent(model);
            }
            return View();
        }
        [HttpGet]
        public void EmailSent(Mails model)
        {

            int Fac = (int)Session["FacultyID"];
            FacultyTable ft = new FacultyTable();
            var context = new MailProEntities();
            ft = context.FacultyTable.Find(Fac);


            var FromEmail = new MailAddress(ft.FacultyEmail, ft.FacultyName);
            var ToEmail = new MailAddress(model.Sent);
            var FromEmailPassword = model.GmailPassword;
                  
            string Subject = model.Subject;
            string Body = model.Contents;

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

        public ActionResult ShowSentEmail()
        {
            int fac = (int)Session["FacultyID"];
            var Data = Db.GetMails.SqlQuery("Select *From Mails where FacultyID ="+ fac).ToList();
            return View(Data);
        }

        public ActionResult DeleteEmail()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DeleteEmail(int MailID)
        {
            var productlist = Db.Database.ExecuteSqlCommand("delete from Mails where MailID = @p0", MailID);

            if (productlist != 0)
            {
                return RedirectToAction("ShowSentEmails");
            }
            return View(productlist);
        }

        public ActionResult EmailDetail(int MailID)
        {
            var data = Db.GetMails.SqlQuery("Select *From Mails where MailID = " + MailID).SingleOrDefault();
            return View(data);
        }
    }
}