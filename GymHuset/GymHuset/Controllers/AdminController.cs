﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Runtime.Remoting.Messaging;
using System.Web.Providers.Entities;
using GymHuset.Models;

namespace GymHuset.Controllers
{

    public class AdminController : Controller
    {

        DataClasses1DataContext db = new DataClasses1DataContext();

        public ActionResult AdminSidan()
        {
            return View();
        }

        public ActionResult AdderaProdukt(string productName, string productPrice, string productQuantity, string productDesc, string productType)
        {
            bool status = false; // Meddelande indikator
            List<tbProduct> products = getProducts();

            try
            {
                if (productName != null)
                {
                    if (productPrice == null)
                    {
                        ViewBag.status = "Du måste ange ett pris.";
                        status = true;
                    }
                    if (productQuantity == null)
                    {
                        ViewBag.status = "Ange lagersaldo på produkt.";
                        status = true;
                    }
                    if (string.IsNullOrEmpty(productDesc))
                    {
                        productDesc = "";
                    }
                    if (!status)
                        foreach (var f in db.tbProducts)
                            if (f.sName.ToLower() == productName.ToLower())
                            {
                                status = true;
                                @ViewBag.status = "Produkten finns redan, Välj ett annat namn.";
                            }

                    if (!status)
                    {
                        var product = new tbProduct
                        {
                            sName = productName,
                            sDescription = productDesc,
                            iPrice = int.Parse(productPrice),
                            iStockBalance = int.Parse(productQuantity),
                            iProductType = int.Parse(productType)
                        };
                        db.tbProducts.InsertOnSubmit(product);
                        db.SubmitChanges();
                        ViewBag.status = "Du har nu lagt till " + productName + " till produktlistan";
                        status = true;
                    }
                }
            }
            catch 
            {
                ViewBag.status = "Du måste fylla i alla fält korrekt.";
                status = true;
                return View();
            }
            return View();
        }

        public ActionResult TaBortProdukt(string id)        // int borde användas som id
        {
            List<tbProduct> productlista = new List<tbProduct>();
            productlista = (from f in db.tbProducts
                            select f).ToList();

            var product2Delete = (from f in db.tbProducts               
                                  where f.sName == id
                                  select f).FirstOrDefault();

            if (id != null)
            {
                db.tbProducts.DeleteOnSubmit(product2Delete);
                db.SubmitChanges();

                List<tbProduct> productlista2 = new List<tbProduct>();
                productlista2 = (from f in db.tbProducts
                                 select f).ToList();

                return View(productlista2);
            }
            return View(productlista);
        }

        private List<tbProduct> getProducts()
        {
            List<tbProduct> products = new List<tbProduct>();
            products = (from f in db.tbProducts
                       select f).ToList();
            return products;
        }
        //Orders.cshtml, listar ut alla orders
        public ActionResult Orders(string emailFilter)
        {
            if (emailFilter == null)
            {
            var listOrder = from o in db.tbOrders.OrderBy(c => c.dtOrderDate)
                select o;
            return View(listOrder);
            }
            var listOrderFilter = from o in db.tbOrders.Where(c => c.tbUser.sEmail.Contains(emailFilter)).OrderBy(c => c.dtOrderDate)
                            select o;
            return View(listOrderFilter);
        }

        public ActionResult HandleOrder(int id)
        {
            var singleOrder = from o in db.tbOrders.Where(c => c.iID == id) select o;
         
            return View(singleOrder);
        }

     


    }
}
