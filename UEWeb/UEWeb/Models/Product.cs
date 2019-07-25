using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UEWeb.Models
{
    public class Product
    {
        public Product()
        {
            InvoiceDetails = new List<InvoiceDetails>();
        }

        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter a valid product name")]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(20)]
        public string Code { get; set; }

        [Required(ErrorMessage ="Please enter valid price")]
        [Range(0,9999999,ErrorMessage ="Please enter valid price")]
        public double Price { get; set; }


        public ICollection<InvoiceDetails> InvoiceDetails { get; set; }


        [NotMapped]
        public string FullName
        {
            get { return $"{Name} {Code}"; }
        }
    }
}
