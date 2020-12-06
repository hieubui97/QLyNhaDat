using QuanLySinhVien.DAO;
using QuanLySinhVien.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLySinhVien.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dao = new DiemSV_DAO();
            var diemsv = dao.FindAll();
            return View(diemsv);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(DiemSV diemSV)
        {
            var dao = new DiemSV_DAO();
            if (ModelState.IsValid)
            {
                dao.Create(diemSV);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View(diemSV);
            }
        }

        public ActionResult Edit(string id)
        {
            var dao = new DiemSV_DAO();
            var obj = dao.GetById(id);

            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(string Id, DiemSV diemSV)
        {
            try
            {
                var dao = new DiemSV_DAO();
                dao.Update(Id, diemSV);

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Error in saving data");
                return View(diemSV);
            }
        }

        //[HttpPost]
        public ActionResult Delete(string id)
        {
            var dao = new DiemSV_DAO();
            dao.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Search(bool ketQua = false)
        {
            var dao = new DiemSV_DAO();
            List<DiemSV> data = new List<DiemSV>();

            data = dao.Search(ketQua);

            ViewBag.Condition = ketQua;

            return View(data);
        }
    }
}