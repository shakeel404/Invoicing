 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UEWeb.Context;
using UEWeb.Models;

namespace UEWeb.Controllers
{
    public class SaleController : Controller
    {

        UEDbContext Db = new UEDbContext();

        // GET: Sale
        public ActionResult Index()
        {
            return View();
        }

        // GET: Sale/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sale/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sale/Create
        [HttpPost]
        public ActionResult Create(int customerId, List<Sale> sales)
        {
            try
            {
                var invoice = new Invoice()
                {
                    CustomerId = customerId,
                    Date=DateTime.Now,
                   
                };
                
                Db.Invoices.Add(invoice);
                Db.SaveChanges();

                int InvoiceId = invoice.Id;

                foreach (var sale in sales)
                {
                    var invoiceDetails = new InvoiceDetails()
                    {
                        InvoiceId = InvoiceId,
                        ProductId = sale.Id,
                        Quantity = sale.Quantity,
                        TradPrice = (int)sale.Price,
                        Bonus = sale.Bonus,
                        Dicount = sale.DiscountPerCent 
                    };

                    Db.InvoiceDetails.Add(invoiceDetails);
                }
                Db.SaveChanges();
                return Json( new { redirect= $"Details/{InvoiceId}" });
            }
            catch
            {
                return View();
            }
        }

        // GET: Sale/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sale/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sale/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sale/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
    }
}
