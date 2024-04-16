using AIStoryteller_Repository.Payload.Request;
using AIStoryteller_Repository.Payload.Response;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Services
{
    public interface IStorytellerService
    {        
        Task<BookResponse> SaveBookToDatabase(NewBookRequest book);
        Task ConvertBookToAudioBook(int bookId);
    }
}
