using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DS.Kids.Site.Controllers
{
    public class TrocaDeSenhaController : Controller
    {
        [HttpGet]
        public ActionResult Index(string t)
        {
            return View();
        }
    }
}