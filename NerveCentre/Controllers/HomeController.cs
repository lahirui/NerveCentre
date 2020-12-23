using NerveCentre.App_DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NerveCentre.Controllers
{
    public class HomeController : Controller
    {
        private NerveCentreEntities db = new NerveCentreEntities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AdminLogin()
        {
            ViewBag.FactoryId = new SelectList(db.Factories.Where(f => f.IsDeleted == false).OrderBy(f => f.FactoryCode), "ID", "FactoryCode");

            return View();
        }

        public ActionResult GetAdminFactory(int? SelectedFactoryId)
        {
            if (SelectedFactoryId !=null)
            {
                Session["AdminFactoryId"] = SelectedFactoryId;
              
            }

            return Json(new { SelectedFactoryId }, JsonRequestBehavior.AllowGet);

        }
    }
}