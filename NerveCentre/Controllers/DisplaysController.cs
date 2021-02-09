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
        private List<SetMediaPath> setMediaPaths = new List<SetMediaPath>();
        private List<CombineResources> combineResources = new List<CombineResources>();

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
            if (SelectedFactoryId != null && SelectedTVId > -1)
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

            //var ResourceForTv = db.SetupDisplays.Where(s => s.FactoryId == FactoryId && s.TelevisionId == TVId && s.IsActive == true && s.IsDeleted == false).OrderBy(s => s.SortOrder).ToList();
            //foreach (var items in ResourceForTv)
            //{
            //    if (items.ResourceId == 1)
            //    {
            //        displayValues.Add(new DisplayValues { ModelFactoryId = Convert.ToInt32(items.FactoryId), ModelTelevisionId = Convert.ToInt32(items.TelevisionId), ModelResourceId = Convert.ToInt32(items.ResourceId), ModelResourcePath = items.ResourcePath, ModelDuration = Convert.ToDecimal(items.Duration), ModelIsActive = Convert.ToBoolean(items.IsActive), ModelIsDeleted = Convert.ToBoolean(items.IsDeleted), ModelSortOrder = Convert.ToInt32(items.SortOrder) });
            //    }
            //}

            //var MediaResources = db.SetupDisplays.Where(s => s.FactoryId == FactoryId && s.TelevisionId == TVId && s.IsDeleted == false && s.IsActive == true).ToList();
            //foreach (var items in MediaResources)
            //{
            //    if (items.ResourceId == 2 || items.ResourceId == 3)
            //    {
            //        var path = "";

            //        if (items.FactoryId == 1)
            //        {
            //            path = $@"D:\NCCME\{items.ResourcePath}";
            //            //path = $@"E:\QualityPics\NerveCentre\CME\{items.ResourcePath}";
            //        }
            //        else if (items.FactoryId == 2)
            //        {
            //            path = $@"D:\NCCME\{items.ResourcePath}";
            //            //path = $@"E:\QualityPics\NerveCentre\CMGM\{items.ResourcePath}";
            //        }
            //        else if (items.FactoryId == 3)
            //        {
            //            path = $@"D:\NCCME\{items.ResourcePath}";
            //            //path = $@"E:\QualityPics\NerveCentre\CMW\{items.ResourcePath}";
            //        }
            //        else if (items.FactoryId == 4)
            //        {
            //            path = $@"D:\NCCME\{items.ResourcePath}";
            //            //path = $@"E:\QualityPics\NerveCentre\CMCD\{items.ResourcePath}";
            //        }
            //        else if (items.FactoryId == 5)
            //        {
            //            path = $@"D:\NCCME\{items.ResourcePath}";
            //            //path = $@"E:\QualityPics\NerveCentre\CMCG\{items.ResourcePath}";
            //        }
            //        else if (items.FactoryId == 6)
            //        {
            //            path = $@"D:\NCCME\{items.ResourcePath}";
            //            //path = $@"E:\QualityPics\NerveCentre\CMPK\{items.ResourcePath}";
            //        }

            //        setMediaPaths.Add(new SetMediaPath { SetMediaPathFactoryId = Convert.ToInt32(items.FactoryId), SetMediaPathTVId = Convert.ToInt32(items.TelevisionId), SetMediaPathResourceId = Convert.ToInt32(items.ResourceId), SetMediaPathResourcePath = path, SetMediaPathDuration = Convert.ToDecimal(items.Duration), SetMediaPathIsActive = Convert.ToBoolean(items.IsActive), SetMediaPathIsDeleted = Convert.ToBoolean(items.IsDeleted), SetMediaPathSortOrder = Convert.ToInt32(items.SortOrder) });
            //    }
            //}

            //foreach (var item1 in setMediaPaths)
            //{
            //    combineResources.Add(new CombineResources { CombineResourcesFactoryId = item1.SetMediaPathFactoryId, CombineResourcesTVId = item1.SetMediaPathTVId, CombineResourcesResourceId = item1.SetMediaPathResourceId, CombineResourcesResourcePath = item1.SetMediaPathResourcePath, CombineResourcesDuration = item1.SetMediaPathDuration, CombineResourcesIsActive = item1.SetMediaPathIsActive, CombineResourcesIsDeleted = item1.SetMediaPathIsDeleted, CombineResourcesSortOrder = item1.SetMediaPathSortOrder });
            //}
            //foreach (var item2 in displayValues)
            //{
            //    combineResources.Add(new CombineResources { CombineResourcesFactoryId = item2.ModelFactoryId, CombineResourcesTVId = item2.ModelTelevisionId, CombineResourcesResourceId = item2.ModelResourceId, CombineResourcesResourcePath = item2.ModelResourcePath, CombineResourcesDuration = item2.ModelDuration, CombineResourcesIsActive = item2.ModelIsActive, CombineResourcesIsDeleted = item2.ModelIsDeleted, CombineResourcesSortOrder = item2.ModelSortOrder });
            //}

            //return Json(combineResources.Where(d => d.CombineResourcesIsActive == true && d.CombineResourcesIsDeleted == false && d.CombineResourcesFactoryId == FactoryId && d.CombineResourcesTVId == TVId).OrderBy(d => d.CombineResourcesSortOrder).ToList(), JsonRequestBehavior.AllowGet);

            var ResourceForTv = db.SetupDisplays.Where(s => s.FactoryId == FactoryId && s.TelevisionId == TVId && s.IsActive == true && s.IsDeleted == false).OrderBy(s => s.SortOrder).ToList();
            foreach (var items in ResourceForTv)
            {
                displayValues.Add(new DisplayValues { ModelFactoryId = Convert.ToInt32(items.FactoryId), ModelTelevisionId = Convert.ToInt32(items.TelevisionId), ModelResourceId = Convert.ToInt32(items.ResourceId), ModelResourcePath = items.ResourcePath, ModelDuration = Convert.ToDecimal(items.Duration), ModelIsActive = Convert.ToBoolean(items.IsActive), ModelIsDeleted = Convert.ToBoolean(items.IsDeleted), ModelSortOrder = Convert.ToInt32(items.SortOrder) });
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

        public ActionResult CriticalPath()
        {
            return View();
        }

        public ActionResult CriticalPathImage()
        {
            var path = "";

            if (Session["FactoryId"].Equals(1))
            {
                path = $@"E:\QualityPics\NerveCentre\CME\CriticalPath.JPG";
            }
            else if (Session["FactoryId"].Equals(2))
            {
                path = $@"E:\QualityPics\NerveCentre\CMGM\CriticalPath.JPG";
            }
            else if (Session["FactoryId"].Equals(3))
            {
                path = $@"E:\QualityPics\NerveCentre\CMW\CriticalPath.JPG";
            }
            else if (Session["FactoryId"].Equals(4))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCD\CriticalPath.JPG";
            }
            else if (Session["FactoryId"].Equals(5))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCG\CriticalPath.JPG";
            }
            else if (Session["FactoryId"].Equals(6))
            {
                path = $@"E:\QualityPics\NerveCentre\CMPK\CriticalPath.JPG";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpg");
        }

        public ActionResult MonthlySafetySummary()
        {
            return View();
        }

        public ActionResult MonthlySafetySummaryImage()
        {
            var path = "";

            if (Session["FactoryId"].Equals(1))
            {
                path = $@"E:\QualityPics\NerveCentre\CME\MonthlySafetySummary.JPG";
            }
            else if (Session["FactoryId"].Equals(2))
            {
                path = $@"E:\QualityPics\NerveCentre\CMGM\MonthlySafetySummary.JPG";
            }
            else if (Session["FactoryId"].Equals(3))
            {
                path = $@"E:\QualityPics\NerveCentre\CMW\MonthlySafetySummary.JPG";
            }
            else if (Session["FactoryId"].Equals(4))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCD\MonthlySafetySummary.JPG";
            }
            else if (Session["FactoryId"].Equals(5))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCG\MonthlySafetySummary.JPG";
            }
            else if (Session["FactoryId"].Equals(6))
            {
                path = $@"E:\QualityPics\NerveCentre\CMPK\MonthlySafetySummary.JPG";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpg");
        }

        public ActionResult YearlySafetySummary()
        {
            return View();
        }

        public ActionResult YearlySafetySummaryImage()
        {
            var path = "";

            if (Session["FactoryId"].Equals(1))
            {
                path = $@"E:\QualityPics\NerveCentre\CME\YearlySafetySummary.JPG";
            }
            else if (Session["FactoryId"].Equals(2))
            {
                path = $@"E:\QualityPics\NerveCentre\CMGM\YearlySafetySummary.JPG";
            }
            else if (Session["FactoryId"].Equals(3))
            {
                path = $@"E:\QualityPics\NerveCentre\CMW\YearlySafetySummary.JPG";
            }
            else if (Session["FactoryId"].Equals(4))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCD\YearlySafetySummary.JPG";
            }
            else if (Session["FactoryId"].Equals(5))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCG\YearlySafetySummary.JPG";
            }
            else if (Session["FactoryId"].Equals(6))
            {
                path = $@"E:\QualityPics\NerveCentre\CMPK\YearlySafetySummary.JPG";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpg");
        }

        public ActionResult ProductsPic1()
        {
            return View();
        }
        public ActionResult ProductsPic1Image()
        {
            var path = "";

            if (Session["FactoryId"].Equals(1))
            {
                path = $@"E:\QualityPics\NerveCentre\CME\ProductsPic1.JPG";
            }
            else if (Session["FactoryId"].Equals(2))
            {
                path = $@"E:\QualityPics\NerveCentre\CMGM\ProductsPic1.JPG";
            }
            else if (Session["FactoryId"].Equals(3))
            {
                path = $@"E:\QualityPics\NerveCentre\CMW\ProductsPic1.JPG";
            }
            else if (Session["FactoryId"].Equals(4))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCD\ProductsPic1.JPG";
            }
            else if (Session["FactoryId"].Equals(5))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCG\ProductsPic1.JPG";
            }
            else if (Session["FactoryId"].Equals(6))
            {
                path = $@"E:\QualityPics\NerveCentre\CMPK\ProductsPic1.JPG";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpg");
        }

        public ActionResult ProductsPic2()
        {
            return View();
        }

        public ActionResult ProductsPic2Image()
        {
            var path = "";

            if (Session["FactoryId"].Equals(1))
            {
                path = $@"E:\QualityPics\NerveCentre\CME\ProductsPic2.JPG";
            }
            else if (Session["FactoryId"].Equals(2))
            {
                path = $@"E:\QualityPics\NerveCentre\CMGM\ProductsPic2.JPG";
            }
            else if (Session["FactoryId"].Equals(3))
            {
                path = $@"E:\QualityPics\NerveCentre\CMW\ProductsPic2.JPG";
            }
            else if (Session["FactoryId"].Equals(4))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCD\ProductsPic2.JPG";
            }
            else if (Session["FactoryId"].Equals(5))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCG\ProductsPic2.JPG";
            }
            else if (Session["FactoryId"].Equals(6))
            {
                path = $@"E:\QualityPics\NerveCentre\CMPK\ProductsPic2.JPG";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpg");
        }

        public ActionResult ProductsPic3()
        {
            return View();

        }
        public ActionResult ProductsPic3Image()
        {
            var path = "";

            if (Session["FactoryId"].Equals(1))
            {
                path = $@"E:\QualityPics\NerveCentre\CME\ProductsPic3.JPG";
            }
            else if (Session["FactoryId"].Equals(2))
            {
                path = $@"E:\QualityPics\NerveCentre\CMGM\ProductsPic3.JPG";
            }
            else if (Session["FactoryId"].Equals(3))
            {
                path = $@"E:\QualityPics\NerveCentre\CMW\ProductsPic3.JPG";
            }
            else if (Session["FactoryId"].Equals(4))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCD\ProductsPic3.JPG";
            }
            else if (Session["FactoryId"].Equals(5))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCG\ProductsPic3.JPG";
            }
            else if (Session["FactoryId"].Equals(6))
            {
                path = $@"E:\QualityPics\NerveCentre\CMPK\ProductsPic3.JPG";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpg");
        }

        public ActionResult ProductsPic4()
        {
            return View();
        }
        public ActionResult ProductsPic4Image()
        {
            var path = "";

            if (Session["FactoryId"].Equals(1))
            {
                path = $@"E:\QualityPics\NerveCentre\CME\ProductsPic4.JPG";
            }
            else if (Session["FactoryId"].Equals(2))
            {
                path = $@"E:\QualityPics\NerveCentre\CMGM\ProductsPic4.JPG";
            }
            else if (Session["FactoryId"].Equals(3))
            {
                path = $@"E:\QualityPics\NerveCentre\CMW\ProductsPic4.JPG";
            }
            else if (Session["FactoryId"].Equals(4))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCD\ProductsPic4.JPG";
            }
            else if (Session["FactoryId"].Equals(5))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCG\ProductsPic4.JPG";
            }
            else if (Session["FactoryId"].Equals(6))
            {
                path = $@"E:\QualityPics\NerveCentre\CMPK\ProductsPic4.JPG";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpg");
        }

        public ActionResult ProductsPic5()
        {
            return View();
        }
        public ActionResult ProductsPic5Image()
        {
            var path = "";

            if (Session["FactoryId"].Equals(1))
            {
                path = $@"E:\QualityPics\NerveCentre\CME\ProductsPic5.JPG";
            }
            else if (Session["FactoryId"].Equals(2))
            {
                path = $@"E:\QualityPics\NerveCentre\CMGM\ProductsPic5.JPG";
            }
            else if (Session["FactoryId"].Equals(3))
            {
                path = $@"E:\QualityPics\NerveCentre\CMW\ProductsPic5.JPG";
            }
            else if (Session["FactoryId"].Equals(4))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCD\ProductsPic5.JPG";
            }
            else if (Session["FactoryId"].Equals(5))
            {
                path = $@"E:\QualityPics\NerveCentre\CMCG\ProductsPic5.JPG";
            }
            else if (Session["FactoryId"].Equals(6))
            {
                path = $@"E:\QualityPics\NerveCentre\CMPK\ProductsPic5.JPG";
            }
            var bytes = System.IO.File.ReadAllBytes(path);
            return File(bytes, "image/jpg");
        }
    }
}