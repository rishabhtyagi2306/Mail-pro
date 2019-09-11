using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult SendEmail()
        {
            return View();
        }
    }
    [HttpPost]
    public ActionResult SendEmail(string receiver, string subject, string message)
    {
        try
        {
            if (ModelState.IsValid)
            {
                var senderEmail = new MailAddress("jamilmoughal786@gmail.com", "Jamil");
                var receiverEmail = new MailAddress(receiver, "Receiver");
                var password = "Your Email Password here";
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                return View();
            }
        }
        catch (Exception)
        {
            ViewBag.Error = "Some Error";
        }
        return View();
    }


}