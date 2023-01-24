using BookStore1.Data;
using BookStore1.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore1.Controllers
{
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        public BookController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Book> books = _context.Books.ToList();
            return View(books);
        }
    }
}
