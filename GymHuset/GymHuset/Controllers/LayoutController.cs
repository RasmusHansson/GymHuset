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
      
        DataClasses1DataContext db = new DataClasses1DataContext();

        //Visar hur många varor det är i kundkorgen+total kostnad
        public ActionResult _CartSmall()
        {
           
            if (Session["cartList"] != null)
            {
                ViewBag.cartCount = ((List<tbProduct>)Session["cartList"]).Count;
                ViewBag.cartSum = ((List<tbProduct>) Session["cartList"]).Sum(prod => prod.iPrice);
               
                return View();

            }
            return View();
        }
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
