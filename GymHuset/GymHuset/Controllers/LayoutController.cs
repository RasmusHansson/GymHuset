using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GymHuset.Models;

namespace GymHuset.Controllers
{
    public class LayoutController : Controller
    {
        //
        // GET: /Layout/
        DataClasses1DataContext db = new DataClasses1DataContext();

        public ActionResult _PopProductsPartial()
        {
            List<tbProduct> productList = (from p in db.tbProducts
                                           select p).OrderByDescending(p => p.iItemsSold).ToList();


            List<GymHuset.Models.tbProduct> popularList = new List<GymHuset.Models.tbProduct>();

            for (int i = 0; i < 5; i++)
            {
                popularList.Add((productList).First());

                productList.Remove((productList).First());
            }

            return PartialView(popularList);
        }

    }
}
