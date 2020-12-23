using NerveCentre.App_DB;
using NerveCentre.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

namespace NerveCentre.Controllers
{
    public class DisplaysController : Controller
    {
        private NerveCentreEntities db = new NerveCentreEntities();
        private List<DisplayValues> displayValues = new List<DisplayValues>();
        private List<FactoryTVs> FactoryTVs = new List<FactoryTVs>();

        public ActionResult DisplayLogin()
        {
            ViewBag.FactoryId = new SelectList(db.Factories.Where(f => f.IsDeleted == false).OrderBy(f => f.FactoryCode), "ID", "FactoryCode");
            //ViewBag.TelevisionID = new SelectList(db.Televisions.Where(t => t.IsDeleted == false).OrderBy(t => t.TVName), "ID", "TVName");
            return View();
        }

        public JsonResult GetTvList(int? FactoryId)
        {
            var TelevisionID = new SelectList(db.Televisions.Where(t => t.FactoryId == FactoryId && t.IsDeleted == false).OrderBy(t => t.TVName), "ID", "TVName");
            ViewBag.TelevisionID = TelevisionID;
            return Json(db.Televisions.Where(t => t.FactoryId == FactoryId && t.IsDeleted == false).OrderBy(t => t.TVName).Select(f => new
            {
                Id = f.ID,
                Name = f.TVName
            }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayTrans(int? SelectedFactoryId, int? SelectedTVId)
        {
            if (SelectedFactoryId != null && SelectedTVId >-1)
            {
                Session["FactoryId"] = SelectedFactoryId;
                Session["TVID"] = SelectedTVId;
                //return RedirectToAction("Display");
            }
            return Json(new { SelectedFactoryId, SelectedTVId }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Display()
        {
            return View();
        }

        public JsonResult DisplayBoard()
        {
            var FactoryId = Convert.ToInt32(Session["FactoryId"].ToString());
            var TVId = Convert.ToInt32(Session["TVID"].ToString());

            var ResourceForTv = db.SetupDisplays.Where(s => s.FactoryId == FactoryId && s.TelevisionId == TVId && s.IsActive == true && s.IsDeleted == false).OrderBy(s => s.SortOrder).ToList();
            foreach (var items in ResourceForTv)
            {
                displayValues.Add(new DisplayValues { ModelFactoryId = items.FactoryId, ModelTelevisionId = items.TelevisionId, ModelResourceId = items.ResourceId, ModelResourcePath = items.ResourcePath, ModelDuration = items.Duration, ModelIsActive = items.IsActive, ModelIsDeleted = items.IsDeleted, ModelSortOrder = items.SortOrder });
            }

            return Json(displayValues.Where(d => d.ModelIsActive == true && d.ModelIsDeleted == false && d.ModelFactoryId == FactoryId && d.ModelTelevisionId == TVId).OrderBy(d => d.ModelSortOrder).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayDefault()
        {
            return View();
        }

        public ActionResult _WIPKanbanChart()
        {
            return PartialView("_WIPKanbanChart");
        }

        public ActionResult AuditDetails()
        {
            return View();
        }

        public ActionResult AuditImage()
        {
            var path = "";

            if (Session["FactoryId"].Equals(1))
            {
                path = $@"E:\QualityPics\NerveCentre\CME\AuditImage.JPG";
            }
            else if (Session["FactoryId"].Equals(2))
            {
                path = $@"E:\QualityPics\NerveCentre\CMGM\AuditImage.JPG";
            }
            else if (Session["FactoryId"].Equals(3))
            {
                path = $@"E:\QualityPics\NerveCentre\CMW\AuditImage.JPG";
            }
            else if (Session["FactoryId"].Equals(4))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCD\AuditImage.JPG";
            }
            else if (Session["FactoryId"].Equals(5))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCG\AuditImage.JPG";
            }
            else if (Session["FactoryId"].Equals(6))
            {
                path = $@"E:\QualityPics\NerveCentre\CMPK\AuditImage.JPG";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpg");
        }

        public ActionResult VisionMision()
        {
            return View();
        }

        public ActionResult VisionMisionImage()
        {
            var path = "";

            if (Session["FactoryId"].Equals(1))
            {
                path = $@"E:\QualityPics\NerveCentre\CME\VisionMision.JPG";
            }
            else if (Session["FactoryId"].Equals(2))
            {
                path = $@"E:\QualityPics\NerveCentre\CMGM\VisionMision.JPG";
            }
            else if (Session["FactoryId"].Equals(3))
            {
                path = $@"E:\QualityPics\NerveCentre\CMW\VisionMision.JPG";
            }
            else if (Session["FactoryId"].Equals(4))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCD\VisionMision.JPG";
            }
            else if (Session["FactoryId"].Equals(5))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCG\VisionMision.JPG";
            }
            else if (Session["FactoryId"].Equals(6))
            {
                path = $@"E:\QualityPics\NerveCentre\CMPK\VisionMision.JPG";
            }
            
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpg");
        }

        public ActionResult Birthday()
        {
            return View();
        }

        public ActionResult BirthdayImage()
        {
            var path = "";

            if (Session["FactoryId"].Equals(1))
            {
                path = $@"E:\QualityPics\NerveCentre\CME\Birthday.JPG";
            }
            else if (Session["FactoryId"].Equals(2))
            {
                path = $@"E:\QualityPics\NerveCentre\CMGM\Birthday.JPG";
            }
            else if (Session["FactoryId"].Equals(3))
            {
                path = $@"E:\QualityPics\NerveCentre\CMW\Birthday.JPG";
            }
            else if (Session["FactoryId"].Equals(4))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCD\Birthday.JPG";
            }
            else if (Session["FactoryId"].Equals(5))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCG\Birthday.JPG";
            }
            else if (Session["FactoryId"].Equals(6))
            {
                path = $@"E:\QualityPics\NerveCentre\CMPK\Birthday.JPG";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpg");
        }

        public ActionResult Cutting_Production()
        {
            return View();
        }

        public ActionResult Cutting_ProductionImage()
        {
            var path = "";

            if (Session["FactoryId"].Equals(1))
            {
                path = $@"E:\QualityPics\NerveCentre\CME\Cutting_Production.JPG";
            }
            else if (Session["FactoryId"].Equals(2))
            {
                path = $@"E:\QualityPics\NerveCentre\CMGM\Cutting_Production.JPG";
            }
            else if (Session["FactoryId"].Equals(3))
            {
                path = $@"E:\QualityPics\NerveCentre\CMW\Cutting_Production.JPG";
            }
            else if (Session["FactoryId"].Equals(4))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCD\Cutting_Production.JPG";
            }
            else if (Session["FactoryId"].Equals(5))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCG\Cutting_Production.JPG";
            }
            else if (Session["FactoryId"].Equals(6))
            {
                path = $@"E:\QualityPics\NerveCentre\CMPK\Cutting_Production.JPG";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpg");
        }
    }
}