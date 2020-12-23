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
    public class SetupDisplaysController : Controller
    {
        private NerveCentreEntities db = new NerveCentreEntities();

        // GET: SetupDisplays
        public ActionResult Index()
        {
            var AdminFactoryId = Convert.ToInt32(Session["AdminFactoryId"].ToString());
            var setupDisplays = db.SetupDisplays.Include(s => s.Factory).Include(s => s.Resource).Include(s => s.Television).Where(s=>s.IsDeleted==false && s.FactoryId==AdminFactoryId).OrderBy(s=>s.Television.TVName).ThenBy(s=>s.SortOrder);
            return View(setupDisplays.ToList());
        }


        // GET: SetupDisplays/Create
        public ActionResult Create()
        {

            var AdminFactoryId = Convert.ToInt32(Session["AdminFactoryId"].ToString());
            ViewBag.ResourceId = new SelectList(db.Resources.Where(r=>r.IsDeleted==false).OrderBy(r=>r.ResourceCode), "ID", "ResourceCode");
            ViewBag.TelevisionId = new SelectList(db.Televisions.Where(t=>t.IsDeleted==false && t.FactoryId==AdminFactoryId).OrderBy(t=>t.TVName), "ID", "TVName");
            return View();
        }

        // POST: SetupDisplays/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,FactoryId,TelevisionId,ResourceId,ResourcePath,Duration,IsActive,IsDeleted,DeletedDate,SortOrder")] SetupDisplay setupDisplay)
        {
            var AdminFactoryId = Convert.ToInt32(Session["AdminFactoryId"].ToString());
            if (ModelState.IsValid)
            {

                var path = "";
               
                if (AdminFactoryId == 1)
                {
                    path = $@"E:\QualityPics\NerveCentre\CME\";
                }
                else if (AdminFactoryId == 2)
                {
                    path = $@"E:\QualityPics\NerveCentre\CMGM\";
                }
                else if (AdminFactoryId == 3)
                {
                    path = $@"E:\QualityPics\NerveCentre\CMW\";
                }
                else if (AdminFactoryId == 4)
                {
                    path = $@"E:\QualityPics\NerveCentre\CMCD\";
                }
                else if (AdminFactoryId == 5)
                {
                    path = $@"E:\QualityPics\NerveCentre\CMCG\";
                }
                else if (AdminFactoryId == 6)
                {
                    path = $@"E:\QualityPics\NerveCentre\CMPK\";
                }
                setupDisplay.ResourcePath = path + setupDisplay.ResourcePath;
                setupDisplay.FactoryId = AdminFactoryId;
                setupDisplay.IsActive = true;
                setupDisplay.IsDeleted = false;
                db.SetupDisplays.Add(setupDisplay);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

           
            ViewBag.ResourceId = new SelectList(db.Resources.Where(r => r.IsDeleted == false).OrderBy(r => r.ResourceCode), "ID", "ResourceCode", setupDisplay.ResourceId);
            ViewBag.TelevisionId = new SelectList(db.Televisions.Where(t => t.IsDeleted == false && t.FactoryId == AdminFactoryId).OrderBy(t => t.TVName), "ID", "TVName", setupDisplay.TelevisionId);
            return View(setupDisplay);
        }

        // GET: SetupDisplays/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetupDisplay setupDisplay = db.SetupDisplays.Find(id);
            if (setupDisplay == null)
            {
                return HttpNotFound();
            }
            var AdminFactoryId = Convert.ToInt32(Session["AdminFactoryId"].ToString());
            ViewBag.ResourceId = new SelectList(db.Resources.Where(r => r.IsDeleted == false).OrderBy(r => r.ResourceCode), "ID", "ResourceCode", setupDisplay.ResourceId);
            ViewBag.TelevisionId = new SelectList(db.Televisions.Where(t => t.IsDeleted == false && t.FactoryId == AdminFactoryId).OrderBy(t => t.TVName), "ID", "TVName", setupDisplay.TelevisionId);
            return View(setupDisplay);
        }

        // POST: SetupDisplays/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,FactoryId,TelevisionId,ResourceId,ResourcePath,Duration,IsActive,IsDeleted,DeletedDate,SortOrder")] SetupDisplay setupDisplay)
        {
            var AdminFactoryId = Convert.ToInt32(Session["AdminFactoryId"].ToString());
            if (ModelState.IsValid)
            {
              
                setupDisplay.FactoryId = AdminFactoryId;
                setupDisplay.IsDeleted = false;
                db.Entry(setupDisplay).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ResourceId = new SelectList(db.Resources.Where(r => r.IsDeleted == false).OrderBy(r => r.ResourceCode), "ID", "ResourceCode", setupDisplay.ResourceId);
            ViewBag.TelevisionId = new SelectList(db.Televisions.Where(t => t.IsDeleted == false && t.FactoryId == AdminFactoryId).OrderBy(t => t.TVName), "ID", "TVName", setupDisplay.TelevisionId);
            return View(setupDisplay);
        }

        // GET: SetupDisplays/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SetupDisplay setupDisplay = db.SetupDisplays.Find(id);
            if (setupDisplay == null)
            {
                return HttpNotFound();
            }
            return View(setupDisplay);
        }

        // POST: SetupDisplays/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var delSetup = db.SetupDisplays.Where(s => s.ID == id).FirstOrDefault();
            delSetup.IsActive = false;
            delSetup.IsDeleted = true;
            delSetup.DeletedDate = DateTime.Now;
            db.Entry(delSetup).State = EntityState.Modified;
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
