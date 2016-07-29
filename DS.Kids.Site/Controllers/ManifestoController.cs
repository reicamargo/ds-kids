using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DS.Kids.Site.Controllers
{
    public class ManifestoController : Controller
    {
        // GET: Manifesto
        public ActionResult Index()
        {
            ViewBag.SubMenu = true;
            return View();
        }
    }
}