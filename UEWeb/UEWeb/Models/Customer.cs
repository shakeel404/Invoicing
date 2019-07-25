using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UEWeb.Models
{
    public class Customer
    {
        public Customer()
        {
            Invoices = new List<Invoice>();
        }
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter valid customer name")]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter valid address")]
        [MaxLength(100)]
        public string Address { get; set; }

        [MaxLength(16)]
        public string Contact { get; set; } 

        public ICollection<Invoice> Invoices { get; set; }
    }
}
