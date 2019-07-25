namespace UEWeb.Models
{
    public  class InvoiceDetails
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }

        public Invoice Invoice { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public int Bonus { get; set; }

        public int TradPrice { get; set; }

        public double Dicount { get; set; }

    }
}
