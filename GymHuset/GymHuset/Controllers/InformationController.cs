using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GymHuset.Controllers
{
    public class InformationController : Controller
    {
        //
        // GET: /Information/

        public ActionResult OmOss()
        {
            return View();
        }

        public ActionResult HittaHit()
        {
            return View();        
        }

        public ActionResult Kontakt()
        {
            return View();        
        }

        public ActionResult FAQ()
        {
            return View();        
        }

        public ActionResult Evenemang()
        {
            return View();        
        }
    }
}
