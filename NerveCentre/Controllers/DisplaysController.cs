using NerveCentre.App_DB;
using NerveCentre.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Cors;
using System.Web.UI.DataVisualization.Charting;
using System.Data;

namespace NerveCentre.Controllers
{
    public class DisplaysController : Controller
    {
        NerveCentreEntities db = new NerveCentreEntities();
        List<DisplayValues> displayValues = new List<DisplayValues>();

        //[EnableCors(origins: "*", headers: "*", methods: "*")]
        public ActionResult Display()
        {
            
            return View();
        }

        public JsonResult DisplayBoard()
        {
            Session["FactoryId"] = 1;
            Session["TVID"] = 1;

            var FactoryId = Convert.ToInt32(Session["FactoryId"].ToString());
            var TVId = Convert.ToInt32(Session["TVID"].ToString());

            var ResourceForTv = db.SetupDisplays.Where(s => s.FactoryId == FactoryId && s.TelevisionId == TVId && s.IsActive == true && s.IsDeleted == false).OrderBy(s => s.SortOrder).ToList();
            foreach (var items in ResourceForTv)
            {
                displayValues.Add(new DisplayValues { ModelFactoryId = items.FactoryId, ModelTelevisionId = items.TelevisionId, ModelResourceId = items.ResourceId, ModelResourcePath = items.ResourcePath, ModelDuration = items.Duration, ModelIsActive = items.IsActive, ModelIsDeleted = items.IsDeleted, ModelSortOrder=items.SortOrder });
            }

            return Json(displayValues.Where(d=>d.ModelIsActive==true && d.ModelIsDeleted==false && d.ModelFactoryId==FactoryId && d.ModelTelevisionId==TVId ).OrderBy(d=>d.ModelSortOrder).ToList(), JsonRequestBehavior.AllowGet);
        }

        public ActionResult DisplayDefault()
        {
            return View();
        }

        public ActionResult _WIPKanbanChart()
        {

            

            return PartialView("_WIPKanbanChart");
        }
    }
}