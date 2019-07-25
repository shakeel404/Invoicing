using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UEWeb.Context;
using UEWeb.Models;

namespace UEWeb.Controllers
{
    public class HomeController : Controller
    {
        private UEDbContext db = new UEDbContext();
        public async Task<ActionResult> Index()
        {

            var topCustomers =  await db.Customers.OrderByDescending(c => c.Invoices.Count).Take(5).ToListAsync();
            var topProducts = await db.Products.OrderByDescending(p => p.InvoiceDetails.Count).Take(5).ToListAsync();
            var topInvoices = await db.Invoices.Include(i=>i.Customer).OrderByDescending(i => i.InvoiceDetails.Sum(s => (s.Quantity * s.TradPrice) - ((s.Quantity * s.TradPrice) * (s.Dicount / 100)))).Take(5).ToListAsync();
            
            var recentCustomers = await db.Customers.OrderByDescending(c => c.Id).Take(5).ToListAsync();
            var recentInvoices = await db.Invoices.Include(i => i.Customer).OrderByDescending(c => c.Id).Take(5).ToListAsync();


            var stats = new Statistics(topCustomers, topInvoices, topProducts,recentInvoices,recentCustomers);

            return View(stats);
        }

        public ActionResult About()
        {
            

            return View();
        }

        public ActionResult Contact()
        {  
            return View();
        }
    }
}