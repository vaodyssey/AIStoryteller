using AIStoryteller_Repository.Constants;
using Microsoft.AspNetCore.SignalR;
using AIStoryteller_Repository.Entities;
using iTextSharp.text.pdf.parser;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AIStoryteller_Repository.Migrations;
using AutoMapper;
using AIStoryteller_Repository.SignalR;
using System.Numerics;
using AIStoryteller_Repository.Payload.Response;
using AIStoryteller_Repository.Payload.Request;
using AIStoryteller_Repository.Repository;
using AIStoryteller_Repository.Repository.Implementation;


namespace AIStoryteller_Repository.Services.Implementation
{
    public class PdfService
    {
        private NewBookRequest _newBookDto;
        private IMapper _mapper;
        private int _bookId;
        private List<string> _textPerPageList;
        private List<Page> _buffer;
        private IBookRepository _bookRepository;
        private IPageRepository _pageRepository;
        private readonly IHubContext<ProgressHub> _hubContext;
        public PdfService(IMapper mapper, IBookRepository bookRepository,
            IPageRepository pageRepository,
        IHubContext<ProgressHub> hubContext)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _pageRepository = pageRepository;
            _hubContext = hubContext;
            InitializeObjects();
        }
        public async Task<BookResponse> SaveBookToDatabase(NewBookRequest book)
        {
            _newBookDto = book;
            await SaveBookToDatabase();
            await GetBookIdByName();
            await ReadAllPagesFromPdf();
            await SaveAllPagesToDatabase();
            return GetBookDto();
        }
        private async Task SaveBookToDatabase()
        {

            Book book = _mapper.Map<Book>(_newBookDto);
            await _bookRepository.Insert(book);
        }

        private async Task GetBookIdByName()
        {
            Book book = await _bookRepository.GetBy(book => book.Name == _newBookDto.Name);
            _bookId = book.Id;
        }
        private Task ReadAllPagesFromPdf()
        {

            return Task.Run(() =>
            {
                var reader = new PdfReader(_newBookDto.TextData);
                _textPerPageList.Clear();
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

            foreach (var text in _textPerPageList)
            {
                Page pg = new Page()
                {
                    BookId = _bookId,
                    Content = text,
                    PageNumber = currentPage
                };
                _buffer.Add(pg);
                await UpdateProgress(currentPage, _textPerPageList.Count());
                await CommitBufferToDatabase();
                currentPage++;
            }
        }
        private async Task CommitBufferToDatabase()
        {
            if (_buffer.Count == BulkInsertConfig.MAX_PAGES_PER_BULK)
            {
                await _pageRepository.BulkInsert(_buffer);
                _buffer.Clear();
            }
        }
        private BookResponse GetBookDto()
        {
            return new BookResponse()
            {
                Id = _bookId,
                Name = _newBookDto.Name,
                Size = _newBookDto.Size,
            };
        }
        private async Task UpdateProgress(int current, int total)
        {
            int progress = (int)Math.Round((double)(100 * current) / total);
            await _hubContext.Clients.All.SendAsync("UploadProgressChanged", progress);
        }
        private void InitializeObjects()
        {
            _textPerPageList = new List<string>();
            _buffer = new List<Page>();
        }

    }
}
