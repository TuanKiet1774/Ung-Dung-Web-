using BaiTap4_64131060.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTap4_64131060.Controllers
{
    public class GuiEmail_64131060Controller : Controller
    {
        // GET: GuiEmail
        public ActionResult Index()
        {
            return View();
        }
        [
        HttpPost]
        public ActionResult Index(MailInfo model)
        {
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.From = new System.Net.Mail.MailAddress(model.From);
            mail.To.Add(model.To);
            mail.Subject = model.Subject;
            mail.Body = model.Body;
            mail.IsBodyHtml = true;
            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new System.Net.NetworkCredential(model.From, model.Password);
            smtp.EnableSsl = true;
            smtp.Send(mail);
            return Content("Đã gửi mail");
        }
    }
}