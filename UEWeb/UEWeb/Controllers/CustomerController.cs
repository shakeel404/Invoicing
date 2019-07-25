using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UEWeb.Context;
using UEWeb.Models;
using PagedList;
using System.Threading.Tasks;

namespace UEWeb.Controllers
{
    public class CustomerController : Controller
    {
        private UEDbContext db = new UEDbContext();

      
        public async Task<ActionResult> Index(string search,int? page)
        {
            IQueryable<Customer> customers = null;
            ViewBag.CurrentSearch = search;
           
            if (string.IsNullOrWhiteSpace(search))
            {
                customers = db.Customers.OrderByDescending(o => o.Id).AsQueryable();
            }
            else
            {
                customers = db.Customers
                    .Where(c=>c.Name.Contains(search) || c.Address.Contains(search))
                    .OrderByDescending(o => o.Id)
                    .AsQueryable();
            }

            int pagesize = 10;
            int pageno = (page ?? 1);
            ViewBag.CurrentPage = pageno;
            return await Task.FromResult(View(customers.ToPagedList(pageno,pagesize)));
        }

         
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return await Task.FromResult( new  HttpStatusCodeResult(HttpStatusCode.BadRequest));
            }
            Customer customer = await db.Customers.Include(c =>   c.Invoices.Select(i=>i.InvoiceDetails)).FirstOrDefaultAsync(c=>c.Id==id);
            if (customer == null)
            {
                return await Task.FromResult(HttpNotFound());
            }
            customer.Invoices = customer.Invoices.Take(20).ToList();
            return  await Task.FromResult(View(customer));
        }

        
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Address,Contact")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                await db.SaveChangesAsync();
                return await Task.FromResult( RedirectToAction("Index"));
            }

            return await Task.FromResult(View(customer));
        }

       
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return await Task.FromResult( HttpNotFound());
            }
            return await Task.FromResult(View(customer));
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Address,Contact")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
               await db.SaveChangesAsync();
                return  RedirectToAction("Index");
            }
            return View(customer);
        }

        
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = await db.Customers.FindAsync(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Customer customer = await db.Customers.FindAsync(id);
            db.Customers.Remove(customer);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
