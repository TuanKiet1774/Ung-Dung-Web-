using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ktr_Mau_64131060.Models;

namespace Ktr_Mau_64131060.Controllers
{
    public class SinhVien_64131060Controller : Controller
    {
        private QLSV_64131060Entities db = new QLSV_64131060Entities();

        //string LayMaNV()
        //{
        //    var maMax = db.SINHVIENs.ToList().Select(n => n.MaSV).Max();
        //    int maSV = int.Parse(maMax.Substring(2)) + 1;
        //    string SV = String.Concat("000", maSV.ToString());
        //   return "SV" + SV.Substring(maSV.ToString().Length - 1);
        //}

        // GET: TimKiem
        //public ActionResult TimKiem()
        //{
            // Lấy toàn bộ danh sách lớp
            //ViewBag.TenLop = new SelectList(db.LOPs, "MaLop", "TenLop"); // "MaLop" là value, "TenLop" là text

           // var SinhVien = db.SINHVIENs.Include(n => n.LOP).ToList();
            //return View(SinhVien.ToList());
        //}


        // POST: TimKiem
        //[HttpPost]
        //public ActionResult TimKiem(string msv)
        //{
            // Tìm sinh viên theo mã sinh viên
            //var SinhVien = db.SINHVIENs.Where(sv => sv.MaSV == msv).Include(sv => sv.LOP).ToList();
            //return View(SinhVien.ToList());
        //}

        // GET: TimKiemSV
        [HttpGet]
        public ActionResult TimKiemSV(string maSV = "", string hoTen = "")
        {
            // Cung cấp dữ liệu cho dropdown
            ViewBag.TenLop = new SelectList(db.LOPs, "MaLop", "TenLop");

            ViewBag.maSV = maSV;
            ViewBag.hoTen = hoTen;

            var sinhvien = db.SINHVIENs.SqlQuery(
                "EXEC SinhVien_TimKiem @maSV, @hoTen",
                new SqlParameter("maSV", maSV),
                new SqlParameter("hoTen", hoTen)
            ).ToList();

            if (sinhvien.Count == 0)
                ViewBag.TB = "Không có thông tin tìm kiếm.";

            return View(sinhvien.ToList());
        }


        public ActionResult GioiThieu()
        {
            return View();
        }

        // GET: SinhVien_63131060
        public ActionResult Index()
        {
            var sINHVIENs = db.SINHVIENs.Include(s => s.LOP);
            return View(sINHVIENs.ToList());
        }

        // GET: SinhVien_63131060/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }

        // GET: SinhVien_63131060/Create
        public ActionResult Create()
        {
            //ViewBag.MaSV = LayMaNV(); 
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop");
            return View();
        }

        // POST: SinhVien_63131060/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,HoSV,TenSV,GioiTinh,NgaySinh,AnhSV,DiaChi,MaLop")] SINHVIEN sINHVIEN)
        {
            Debug.WriteLine("MaSV received: " + sINHVIEN.MaSV);
            //System.Web.HttpPostedFileBase Avatar;
            var imgSV = Request.Files["Avatar"];
            //Lấy thông tin từ input type=file có tên Avatar
            string postedFileName = System.IO.Path.GetFileName(imgSV.FileName);
            //Lưu hình đại diện về Server
            var path = Server.MapPath("/Images/" + postedFileName);
            imgSV.SaveAs(path);
            if (ModelState.IsValid)
            {
                //sINHVIEN.MaSV = LayMaNV();
                sINHVIEN.AnhSV = postedFileName;
                db.SINHVIENs.Add(sINHVIEN);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", sINHVIEN.MaLop);
            return View(sINHVIEN);
        }

        // GET: SinhVien_63131060/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", sINHVIEN.MaLop);
            return View(sINHVIEN);
        }

        // POST: SinhVien_63131060/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSV,HoSV,TenSV,GioiTinh,NgaySinh,AnhSV,DiaChi,MaLop")] SINHVIEN sINHVIEN)
        {
            var imgSV = Request.Files["Avatar"];
            try
            {
                //Lấy thông tin từ input type=file có tên Avatar
                string postedFileName = System.IO.Path.GetFileName(imgSV.FileName);
                //Lưu hình đại diện về Server
                var path = Server.MapPath("/Images/" + postedFileName);
                imgSV.SaveAs(path);
            }
            catch 
            { }
            if (ModelState.IsValid)
            {
                db.Entry(sINHVIEN).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLop = new SelectList(db.LOPs, "MaLop", "TenLop", sINHVIEN.MaLop);
            return View(sINHVIEN);
        }

        // GET: SinhVien_63131060/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            if (sINHVIEN == null)
            {
                return HttpNotFound();
            }
            return View(sINHVIEN);
        }

        // POST: SinhVien_63131060/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            SINHVIEN sINHVIEN = db.SINHVIENs.Find(id);
            db.SINHVIENs.Remove(sINHVIEN);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult PrintList()
        {
            var SinhVien = db.SINHVIENs.Include(n => n.LOP).OrderBy(n => n.TenSV);
            return PartialView(SinhVien.ToList());
        }
    }
}
