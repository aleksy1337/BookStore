
using BookStore1.Data;
using BookStore1.Interfaces;
using BookStore1.Models;
using BookStore1.Repository;
using BookStore1.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BookStore1.Controllers
{

    public class BookController : Controller
    {

        private readonly IBookRepository _bookRepository;
        private readonly IPhotoService _photoService;
        private readonly AppDbContext _context;
        public BookController(AppDbContext context, IBookRepository BookRepository, IPhotoService photoService)
        {
            _bookRepository = BookRepository;
            _photoService = photoService;
            _context = context;


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
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateBookViewModel bookVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(bookVM.Image);
                var book = new Book
                {
                    Title = bookVM.Title,
                    Author = bookVM.Author,
                    Price = bookVM.Price,
                    Image = result.Url.ToString()

                };
                _bookRepository.Add(book);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload failed");
            }
            return View(bookVM);


        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return View("Error");
            var bookVM = new EditBookViewModel
            {
                Title = book.Title,
                Author = book.Author,
                Price = book.Price,
                URL = book.Image,
            };
            return View(bookVM);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditBookViewModel bookVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit book");
                return View("Edit", bookVM);
            }
            var Book = await _bookRepository.GetByIdAsyncNoTracking(id);
            if (Book != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(Book.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(bookVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(bookVM.Image);
                var book = new Book
                {
                    ID = id,
                    Title = bookVM.Title,
                    Author = bookVM.Author,
                    Price = bookVM.Price,
                    Image = photoResult.Url.ToString()
                    
                };
                _bookRepository.Update(book);
                return RedirectToAction("Index");
            }
            else
            {
                return View(bookVM);
            }
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var bookDetails = await _bookRepository.GetByIdAsync(id);
            if (bookDetails == null) return View("Error");
            return View(bookDetails);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteBook")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var bookDetails = await _bookRepository.GetByIdAsync(id);
            if (bookDetails == null) return View("Error");
            _bookRepository.Delete(bookDetails);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Search(string title)
        {
            var book = from m in _context.Books
                       select m;

            if (!string.IsNullOrEmpty(title))
            {
                  book = book.Where(x => x.Title!.Contains(title));
            }

            return View(book.ToList());
        }
    }
}
