using AIStoryteller_Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Repository
{
    public interface IBookRepository
    {
        public Task<List<Book>> GetByPagination(int pageNumber, int pageSize);
        public Task<Book> GetById(int bookId);
        public Task<Book> GetBy(Expression<Func<Book, bool>> predicate);
        public Task Insert(Book book);        
    }
}
