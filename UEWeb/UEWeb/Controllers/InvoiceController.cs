using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using UEWeb.Context;
using UEWeb.Models;

namespace UEWeb.Controllers
{
    public class InvoiceController : Controller
    {
        private UEDbContext db = new UEDbContext();

         
        public async Task<ActionResult> Index(string search, int? page)
        {

            IQueryable<Invoice> invoices = null;
            ViewBag.CurrentSearch = search;

            if (string.IsNullOrWhiteSpace(search))
            {
                invoices = db.Invoices.Include(i => i.Customer).OrderByDescending(o => o.Id).AsQueryable();
            }
            else
            {
                invoices = db.Invoices.Include(i=>i.Customer)
                    .Where(c => c.Customer.Name.Contains(search) || (c.Id.ToString()+c.Date.Year.ToString()+c.InvoiceDetails.Count.ToString()).Contains(search))
                    .OrderByDescending(o => o.Id)
                    .AsQueryable();
            }

            int pagesize = 10;
            int pageno = (page ?? 1);
            ViewBag.CurrentPage = pageno;
            return await Task.FromResult(View(invoices.ToPagedList(pageno, pagesize)));
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            Invoice invoice = db.Invoices
                                .Include(i => i.Customer)
                                .Include(i => i.InvoiceDetails.Select(p=>p.Product))                                
                                .Where(i => i.Id == id).FirstOrDefault(); 

            if (invoice == null)
            { 
                return HttpNotFound();
            }
            return View(invoice);
        }

        // GET: Invoice/Create
        public ActionResult Create(int  customerId )
        {
            var customer = db.Customers.Find(customerId);
            return View(customer);
        }

         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,CustomerId")] Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                db.Invoices.Add(invoice);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", invoice.CustomerId);
            return View(invoice);
        }

        
        public ActionResult Edit(int? id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //Invoice invoice = db.Invoices.Find(id);
            //if (invoice == null)
            //{
            //    return HttpNotFound();
            //}
            //ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", invoice.CustomerId);
            //return View(invoice);
            return Content("Invoice can not be edited.");
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,CustomerId")] Invoice invoice)
        {
            //if (ModelState.IsValid)
            //{
            //    db.Entry(invoice).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.CustomerId = new SelectList(db.Customers, "Id", "Name", invoice.CustomerId);
            //return View(invoice);

            return Content("Invoice can not be edited.");
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices
                                .Include(i => i.Customer)
                                .Include(i => i.InvoiceDetails.Select(p => p.Product))
                                .Where(i => i.Id == id).FirstOrDefault();
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);
            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        public async Task<ActionResult> Print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice invoice = db.Invoices
                                .Include(i => i.Customer)
                                .Include(i => i.InvoiceDetails.Select(p => p.Product))
                                .Where(i => i.Id == id).FirstOrDefault();

            if (invoice == null)
            {
                return HttpNotFound();
            }

            /// Pdf Section
            /// 

            Utilities.ViewRenderer viewRenderer = new Utilities.ViewRenderer(this.ControllerContext);

           string reportContent =  viewRenderer.RenderView($@"~/Views/Invoice/Print.cshtml", invoice);


            IronPdf.HtmlToPdf htmlToPdf = new IronPdf.HtmlToPdf();

            htmlToPdf.PrintOptions = new IronPdf.PdfPrintOptions()
            {
                PaperSize = IronPdf.PdfPrintOptions.PdfPaperSize.A4,
                PaperOrientation = IronPdf.PdfPrintOptions.PdfPaperOrientation.Portrait,
                MarginBottom = 0,
                MarginLeft = 0,
                MarginRight = 0,
                MarginTop = 0 
                
            };




            
            var waterMark = "<h1 style='font-size: 46px !important;' >Umair Enterprise</h1> <small>Developed By Muhammad Shakeel, Contact: 0346-9476404</small>";
            // var document = htmlToPdf.RenderUrlAsPdf($@"http://localhost:49767/Invoice/Details/{id}");
            var document = htmlToPdf.RenderHtmlAsPdf(reportContent);


           
            document.WatermarkAllPages( waterMark, IronPdf.PdfDocument.WaterMarkLocation.MiddleCenter, 20, -45);
          
            //return a  pdf document from a view
            var contentLength = document.BinaryData.Length;
            Response.AppendHeader("Content-Length", contentLength.ToString());
            Response.AppendHeader("Content-Disposition", "inline; filename=Invoice_" + id + ".pdf");
            return await Task.FromResult(File(document.BinaryData, "application/pdf;"));
        }

        public async Task<ActionResult> Test(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice invoice = db.Invoices
                                .Include(i => i.Customer)
                                .Include(i => i.InvoiceDetails.Select(p => p.Product))
                                .Where(i => i.Id == id).FirstOrDefault();

            if (invoice == null)
            {
                return HttpNotFound();
            }

            return View("Print", invoice);
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
