using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> GetBook()
        {
            return await _context.Books
                .OrderByDescending(book => book.QuantityPublished * book.Price)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Book>> GetBooks()
        {
            return await _context.Books
                .Where(book => book.Title.Contains("Red") && book.PublishDate > new DateTime(2012, 5, 25))
                .ToListAsync();
        }
    }
}
