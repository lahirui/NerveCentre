using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NerveCentre.App_DB;

namespace NerveCentre.Controllers
{
    public class TelevisionsController : Controller
    {
        private NerveCentreEntities db = new NerveCentreEntities();

        // GET: Televisions
        public ActionResult Index()
        {
            var AdminFactoryId = Convert.ToInt32(Session["AdminFactoryId"].ToString());
            var televisions = db.Televisions.Include(t => t.Factory).Where(t=>t.IsDeleted==false && t.FactoryId==AdminFactoryId).OrderBy(t=>t.TVName);
            return View(televisions.ToList());
        }

        // GET: Televisions/Create
        public ActionResult Create()
        {
            ViewBag.FactoryId = new SelectList(db.Factories, "ID", "FactoryCode");
            return View();
        }

        // POST: Televisions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FactoryId,TVCode,TVName,IsDeleted,DeletedDate,NewTVCode")] Television television)
        {
            if (ModelState.IsValid)
            {
                var AdminFactoryId = Convert.ToInt32(Session["AdminFactoryId"].ToString());
                television.FactoryId = AdminFactoryId;
                television.NewTVCode = television.NewTVCode.ToUpper();
                television.TVName = television.TVName.ToUpper();
                television.IsDeleted = false;
                db.Televisions.Add(television);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FactoryId = new SelectList(db.Factories, "ID", "FactoryCode", television.FactoryId);
            return View(television);
        }

        // GET: Televisions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Television television = db.Televisions.Find(id);
            if (television == null)
            {
                return HttpNotFound();
            }
            ViewBag.FactoryId = new SelectList(db.Factories, "ID", "FactoryCode", television.FactoryId);
            return View(television);
        }

        // POST: Televisions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FactoryId,TVCode,TVName,IsDeleted,DeletedDate,NewTVCode")] Television television)
        {
            if (ModelState.IsValid)
            {
                var AdminFactoryId = Convert.ToInt32(Session["AdminFactoryId"].ToString());
                television.FactoryId = AdminFactoryId;
                television.NewTVCode = television.NewTVCode.ToUpper();
                television.TVName = television.TVName.ToUpper();
                television.IsDeleted = false;

                db.Entry(television).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FactoryId = new SelectList(db.Factories, "ID", "FactoryCode", television.FactoryId);
            return View(television);
        }

        // GET: Televisions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Television television = db.Televisions.Find(id);
            if (television == null)
            {
                return HttpNotFound();
            }
            return View(television);
        }

        // POST: Televisions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var DelTv = db.Televisions.Where(t => t.ID == id).FirstOrDefault();
            DelTv.IsDeleted = true;
            DelTv.DeletedDate = DateTime.Now;
            db.Entry(DelTv).State = EntityState.Modified;
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
    }
}
