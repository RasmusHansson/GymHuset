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
            //Välj ut alla produkter i databasen och sortera på antal sålda produkter, flest -> minst
            List<tbProduct> productList = (from p in db.tbProducts
                                           select p).OrderByDescending(p => p.iItemsSold).ToList();


            List<GymHuset.Models.tbProduct> popularList = new List<GymHuset.Models.tbProduct>();

            //Plocka ut de fem högst säljande produkterna och lägg i en ny lista 
            for (int i = 0; i < 5; i++)
            {
                popularList.Add((productList).First());

                productList.Remove((productList).First());
            }

            //Skicka med listan på top 5 populära produkter till vyn Shared/_PopProductsPartial.cshtml
            return PartialView(popularList);
        }

    }
}
