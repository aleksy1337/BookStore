namespace BookStore1.ViewModels
{
    public class CreateBookViewModel
    {
        public int ID { get; set; }
        public string? Title { get; set; }
        public string? Author { get; set; }

        public decimal? Price { get; set; }
        public IFormFile Image { get; set; }

    }
}
