using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;

namespace GymHuset.Controllers
{
    public class ProdukterController : Controller
    {
        //
        // GET: /Produkter/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Klader(string id)
        {
            return View();
        }

        public ActionResult Kosttillskott(string id)
        {
            return View();
        }

        public ActionResult Search(string searchString)
        {
            return View();
        }

     
     

    }
}
