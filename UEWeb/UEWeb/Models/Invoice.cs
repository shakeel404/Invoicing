using System;
using System.Collections.Generic;

namespace UEWeb.Models
{
    public class Invoice
    {
        public Invoice()
        {
            InvoiceDetails = new List<InvoiceDetails>();
        }

        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public ICollection<InvoiceDetails> InvoiceDetails { get; set; }

    }
}
