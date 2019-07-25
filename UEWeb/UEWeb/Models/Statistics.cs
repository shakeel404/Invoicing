using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UEWeb.Models
{
    public class Statistics
    {
        public Statistics(IList<Customer> customers, IList<Invoice> invoices, IList<Product> products,IList<Invoice> recentInvoice, IList<Customer> recentCustomer)
        {
            Customers = customers;
            Invoices = invoices;
            Products = products;
            RecentCustomers = recentCustomer;
            RecentInvoices = recentInvoice;
        }

        public ICollection<Customer> Customers { get; private set; }
        public ICollection<Invoice> Invoices { get; private set; }
        public ICollection<Product> Products { get; private set; }

        public ICollection<Invoice> RecentInvoices { get; private set; }
        public ICollection<Customer> RecentCustomers { get; private set; }
    }
}