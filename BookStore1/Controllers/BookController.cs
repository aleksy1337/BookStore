using AspNetCore;
using BookStore1.Data;
using BookStore1.Interfaces;
using BookStore1.Models;
using BookStore1.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore1.Controllers
{

    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        public BookController(AppDbContext context, IBookRepository BookRepository)
        {
            _bookRepository = BookRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Book> books = await _bookRepository.GetAll();
            return View(books);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Book book = await _bookRepository.GetByIdAsync(id);
            return View(book);

        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if(!ModelState.IsValid)
            {
                return View(book);
            }
            _bookRepository.Add(book);
            return RedirectToAction("Index");
        }
    }
}
