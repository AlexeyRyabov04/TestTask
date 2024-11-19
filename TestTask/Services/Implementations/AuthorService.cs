using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {
        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Author> GetAuthor()
        {
            return await _context.Authors
                .OrderByDescending(author => author.Books.Max(book => book.Title.Length))
                .ThenBy(author => author.Id)
                .FirstOrDefaultAsync();
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await _context.Authors
                .Where(author => author.Books.Count(book => book.PublishDate.Year > 2015) > 0
                 && author.Books.Count(book => book.PublishDate.Year > 2015) % 2 == 0)
                .ToListAsync();
        }
    }
}
