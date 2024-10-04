using BaiTap2_64131060.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace BaiTap2_64131060.Controllers
{
    public class SinhVien_64131060Controller : Controller
    {
        // GET: SinhVien_64131060
        //Use FormCollection
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register1(FormCollection field)
        {
            ViewBag.Id = field["Id"];
            ViewBag.Name = field["Name"];
            ViewBag.Marks = field["Marks"];
            //return View(); 
            return View();
        }

        //Use Request
        public ActionResult Index2()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register2(string id, string name, string marks)
        {
            ViewBag.Id = Request["Id"];
            ViewBag.Name = Request["Name"];
            ViewBag.Marks = Request["Marks"];
            return View();
        }
       

        //Sử dụng đối số Action
        public ActionResult Index3()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register3(string id, string name, string marks)
        {
            ViewBag.Id = id;
            ViewBag.Name = name;
            ViewBag.Marks = marks;
            return View();
        }

        //Sử dụng Model
        public ActionResult Index4()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register4(Student hs)
        {
            ViewBag.Id = hs.Id;
            ViewBag.Name = hs.Name;
            ViewBag.Marks = hs.Marks;
            return View();
        }
    }
}