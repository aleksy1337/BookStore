using System.ComponentModel.DataAnnotations;

namespace BookStore1.Models
{
    public class Book
    {
        [Key]
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }

        public decimal? Price { get; set; }

    }
}
