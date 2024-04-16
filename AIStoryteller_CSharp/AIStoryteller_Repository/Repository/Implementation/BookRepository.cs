using AIStoryteller_Repository.Entities;
using AIStoryteller_Repository.Migrations;
using Azure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Repository.Implementation
{
    public class BookRepository : IBookRepository
    {
        private AIStorytellerDbContext _dbContext;
        public BookRepository(AIStorytellerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Book>> GetByPagination(int pageCount, int pageSize)
        {
            var result = _dbContext.Books.OrderByDescending(x => x.Id)
                .Skip((pageCount - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            return result;
        }

        public async Task<Book> GetById(int bookId)
        {
            throw new NotImplementedException();
        }
        public async Task<Book> GetBy(Expression<Func<Book,bool>> predicate)
        {
            var result = await _dbContext.Books.FirstOrDefaultAsync(predicate);
            return result;
        }
        public async Task Insert(Book book)
        {
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
        }
    }
}
