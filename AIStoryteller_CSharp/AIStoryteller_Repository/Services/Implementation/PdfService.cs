using AIStoryteller_Repository.Constants;
using AIStoryteller_Repository.DTO;
using AIStoryteller_Repository.Entities;
using AIStoryteller_Repository.Migrations;
using AutoMapper;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AIStoryteller_Repository.Services.Implementation
{
    public class PdfService : IPdfService
    {

        private NewBookDto _newBookDto;
        private IMapper _mapper;
        private int _bookId;
        private List<string> _textPerPageList;
        private AIStorytellerDbContext _dbContext;

        public PdfService(IMapper mapper, AIStorytellerDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            InitializeObjects();
        }
        public async Task SavePdfToDatabase(NewBookDto book)
        {
            _newBookDto = book;
            await SaveBookToDatabase();
            await GetBookIdByName();
            await ReadAllPagesFromPdf();
            await SaveAllPagesToDatabase();
        }
        private async Task SaveBookToDatabase()
        {

            Book book = _mapper.Map<Book>(_newBookDto);
            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();
        }

        private async Task GetBookIdByName()
        {
            Book book = await _dbContext.Books
                .Where(book => book.Name == _newBookDto.Name)
                .FirstOrDefaultAsync();
            _bookId = book.Id;
        }
        private Task ReadAllPagesFromPdf()
        {
           
            return Task.Run(() =>
            {                
                var reader = new PdfReader(_newBookDto.TextData);
                
                for (int page = 1; page < reader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy Strategy = new SimpleTextExtractionStrategy();
                    string content = PdfTextExtractor.GetTextFromPage(reader, page, Strategy);                                                                            
                    _textPerPageList.Add(content);                    
                }

            });
        }

        private async Task SaveAllPagesToDatabase()
        {
            int currentPage = 1;
            List<Page> buffer = new List<Page>();
            foreach (var text in _textPerPageList)
            {
                Page pg = new Page()
                {
                    BookId = _bookId,
                    Content = text,
                    PageNumber = currentPage
                };
                buffer.Add(pg);
                if (buffer.Count == BulkInsertConfig.MAX_PAGES_PER_BULK)
                {
                    await _dbContext.BulkInsertAsync(buffer);
                    await _dbContext.SaveChangesAsync();
                    buffer.Clear();
                }
                currentPage++;
            }
        }
        private void InitializeObjects()
        {
            _textPerPageList = new List<string>();
        }

    }
}
