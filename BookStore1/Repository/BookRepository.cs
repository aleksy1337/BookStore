using BookStore1.Data;
using BookStore1.Interfaces;
using BookStore1.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore1.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        public BookRepository(AppDbContext context ) 
        {
            _context= context;
        }
        public bool Add(Book book)
        {
            _context.Add( book );
            return Save();
        }

        public bool Delete(Book book)
        {
            _context.Remove( book );
            return Save();  
        }

        public async Task<IEnumerable<Book>> GetAll()
        {
            return await _context.Books.ToListAsync();
            
        }

        

        public async Task<Book> GetByIdAsync(int id)
        {
            return await _context.Books.FirstOrDefaultAsync(i => i.ID == id);
        }
        public async Task<Book> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Books.AsNoTracking().FirstOrDefaultAsync(i => i.ID == id);
        }
        public async Task<IEnumerable<Book>> GetBook(string Title)
        {
            return await _context.Books.Where(b => b.Title.Contains(Title)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return  saved > 0 ? true: false;
        }

        public bool Update(Book book)
        {
            _context.Update(book);
            return Save();  
        }
    }
}
