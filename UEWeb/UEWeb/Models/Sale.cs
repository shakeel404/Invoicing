using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UEWeb.Models
{
    public class Sale
    {
       
        public int Id { get; set; }
        public string FullName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public int Bonus { get;  set; }
        public double Amount { get; set; }
        public int DiscountPerCent { get; set; }
        public double NetAmount { get; set; }
    }
}