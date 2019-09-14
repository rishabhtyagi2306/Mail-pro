using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;
using System.Net;

namespace MailPro.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email

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

                context.Mails.Add(model);
                _ = context.SaveChanges();
                EmailVerification(model);
            }
            return View();
        }
        [HttpGet]
        public void EmailVerification(Mails model)
        {


            var FromEmail = new MailAddress("swasti.tiwari@gmail.com", "Mail Pro");
            var ToEmail = new MailAddress(model.Sent);
            var FromEmailPassword = "swastipratyush";
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
    }
}


