using AIStoryteller_Repository.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Services
{
    public interface IPdfService
    {
        Task SavePdfToDatabase(NewBookDto book);
    }
}
