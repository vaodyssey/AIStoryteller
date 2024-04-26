  using AIStoryteller_Repository.Constants;
using AIStoryteller_Repository.Entities;
using AIStoryteller_Repository.Migrations;
using AIStoryteller_Repository.Payload.Request;
using AIStoryteller_Repository.Payload.Response;
using AIStoryteller_Repository.Repositories;
using AIStoryteller_Repository.SignalR;
using AutoMapper;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace AIStoryteller_Repository.Services.Implementation
{
    public class StorytellerService : IStorytellerService
    {
        private IMapper _mapper;
        private PdfService _pdfService;
        private TextToSpeechService _ttsService;        
        private IBookRepository _bookRepository;
        private IPageRepository _pageRepository;
        private IHubContext<ProgressHub> _hubContext;
        public StorytellerService(IMapper mapper, IBookRepository bookRepository,
        IHubContext<ProgressHub> hubContext, IPageRepository pageRepository)
        {
            _mapper = mapper;            
            _hubContext = hubContext;
            _bookRepository = bookRepository;
            _pageRepository = pageRepository;
            InitializeServices();
        }
        public async Task<BookResponse> SaveBookToDatabase(NewBookRequest book)
        {
            var result = await _pdfService.SaveBookToDatabase(book);
            return result;
        }
        public async Task ConvertBookToAudioBook(int bookId)
        {
            await _ttsService.Convert(bookId);            
        }
        private void InitializeServices()
        {
            _pdfService = new PdfService(_mapper,_bookRepository,_pageRepository, _hubContext);
            _ttsService = new TextToSpeechService(_pageRepository, _hubContext);
        }

    }
}
