using AIStoryteller_Repository.Payload.Request;
using AIStoryteller_Repository.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Services
{
    public interface IPageService
    {
        Task<List<PageResponse>> GetPagesByBookId(int bookId);
    }
}
