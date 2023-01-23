using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookStore1.Models
{
    public class Order
    {
        [Key]
        public int ID { get; set; }
        public decimal? PriceTotal { get; set; }
        public bool? IsPaid { get; set; }
        [ForeignKey("Book")]
        public int BookID { get; set; }
        public Book Book { get; set; }
    }
}
