using AIStoryteller_Repository.Payload.Request;
using AIStoryteller_Repository.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Services
{
    public interface IBookService
    {
        public Task<List<BookResponse>> GetBooks(GetBooksPagingRequest pagingRequest);
    }
}
