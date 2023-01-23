
using System.ComponentModel.DataAnnotations;

namespace BookStore1.Models
{
    public class Customer
    {
        [Key]
        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime? DateOfBirth { get; set; }

    }
}
