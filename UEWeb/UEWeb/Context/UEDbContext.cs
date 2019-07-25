using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using UEWeb.Models;
namespace UEWeb.Context
{
    public class UEDbContext : DbContext
    {

        public UEDbContext():base("UEConnectionString")
        {

        }
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceDetails> InvoiceDetails { get; set; }
    }
}