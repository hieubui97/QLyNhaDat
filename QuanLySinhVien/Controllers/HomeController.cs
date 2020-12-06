using QuanLyNhaDat.DAO;
using QuanLyNhaDat.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuanLyNhaDat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var dao = new nhaDAO();
            //dao.InsertData();
            var nha = dao.FindAll();
            return View(nha);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Nha nha)
        {
            var dao = new nhaDAO();
            if (ModelState.IsValid)
            {
                dao.Create(nha);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Error in saving data");
                return View(nha);
            }
        }

        public ActionResult Edit(string id)
        {
            var dao = new nhaDAO();
            var obj = dao.GetById(id);

            return View(obj);
        }

        [HttpPost]
        public ActionResult Edit(string Id, Nha nha)
        {
            try
            {
                var dao = new nhaDAO();
                dao.Update(Id, nha);

                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Error in saving data");
                return View(nha);
            }
        }

        //[HttpPost]
        public ActionResult Delete(string id)
        {
            var dao = new nhaDAO();
            dao.Delete(id);

            return RedirectToAction("Index");
        }

        public ActionResult Search(int soPhong = 1)
        {
            var dao = new nhaDAO();
            List<Nha> data = new List<Nha>();

            data = dao.Search(soPhong);

            ViewBag.SoPhong = soPhong;

            return View(data);
        }
    }
}