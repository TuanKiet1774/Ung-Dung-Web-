using BaiTap5_64131060.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BaiTap5_64131060.Controllers
{
    public class Page_64131060Controller : Controller
    {
        // GET: Page_64131060
        //Trang chủ
        public ActionResult Index()
        {
            return View();
        }
        
        //Thay đổi Banner
        public ActionResult ChangeBanner()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangeBanner(HttpPostedFileBase banner)
        {
            string postedFileName =
            System.IO.Path.GetFileName(banner.FileName);
            var path = Server.MapPath("/Images/" + postedFileName);
            banner.SaveAs(path);
            string fSave = Server.MapPath("/banner.txt");
            System.IO.File.WriteAllText(fSave, postedFileName);
            return View();
        }
        
        //Form đăng ký
        public ActionResult RegisterEmp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterEmp(HttpPostedFileBase Avatar, EmpModel emp)
        {
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(Avatar.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/" + postedFileName);
            Avatar.SaveAs(path);
            string fSave = Server.MapPath("/emp.txt");
            string[] emInfo =
            {emp.EmpID, emp.Name, emp.BirthOfDate.ToShortDateString(),emp.Email,emp.Password,emp.Department, postedFileName};
            //Lưu các thông ti vào tập tin emp.txt
            System.IO.File.WriteAllLines(fSave, emInfo);
            //Ghi nhận các thông tin đăng ký để hiện thị trên View Confirm
            ViewBag.EmpID = emInfo[0];
            ViewBag.Name = emInfo[1];
            ViewBag.BirthOfDate = emInfo[2].ToString();
            ViewBag.Email = emInfo[3];
            ViewBag.Password = emInfo[4];
            ViewBag.Department = emInfo[5];
            ViewBag.Avatar = "/Images/" + emInfo[6];
            return View("Confirm");
        }
        public ActionResult Confirm (EmpModel emp)
        {
            return View();
        }

        //Gửi email
        public ActionResult SendMail()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMail(MailInfo model)
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
            return View("MailAlert");
        }
        public ActionResult MailAlert(MailInfo model)
        {
            return View();
        }
    }
}