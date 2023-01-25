using BookStore1.Models;

namespace BookStore1.Interfaces
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAll();
        Task<Book> GetByIdAsync(int id);
        Task<IEnumerable<Book>> GetBook(string Title);

        bool Add(Book book);
        bool Delete(Book book);
        bool Update(Book book);
        bool Save();
            

    }
}
