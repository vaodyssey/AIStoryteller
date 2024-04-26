using AIStoryteller_Repository.Entities;
using AIStoryteller_Repository.Migrations;
using AIStoryteller_Repository.Payload.Request;
using AIStoryteller_Repository.Payload.Response;
using AIStoryteller_Repository.Repositories;
using AutoMapper;
using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Services.Implementation
{
    public class BookService : IBookService
    {
        private List<Book> _books;
        private Book _book;
        private BookResponse _bookResponse;
        private List<BookResponse> _bookResponseList;
        private GetBooksPagingRequest _pagingRequest;
        private IBookRepository _bookRepository;
        private IMapper _mapper;
        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
            InitializeObjects();
        }
        public async Task<List<BookResponse>> GetBooks(GetBooksPagingRequest pagingRequest)
        {
            _pagingRequest = pagingRequest;
            _books = await _bookRepository.GetByPagination(_pagingRequest.PageCount, _pagingRequest.PageSize);
            await MapBooksToBookResponseList();
            return _bookResponseList;
        }
        public async Task<BookResponse> GetBookById(int bookId)
        {
            _book = await _bookRepository.GetBy(book => book.Id == bookId);
            await MapBookToBookResponse();
            return _bookResponse;
        }
        private Task MapBooksToBookResponseList()
        {
            return Task.Run(() =>
            {
                foreach (var book in _books)
                {
                    _bookResponseList.Add(new BookResponse()
                    {
                        Id = book.Id,
                        Name = book.Name,
                        Description = book.Description,
                        AuthorName = book.AuthorName,
                        Size = book.Size,
                    });
                }
            });
        }
        private Task MapBookToBookResponse()
        {
            return Task.Run(() =>
            {
                _bookResponse = new BookResponse()
                {
                    Id = _book.Id,
                    Name = _book.Name,
                    Size = _book.Size,
                    AuthorName= _book.AuthorName,   
                };
            });
        }
        private void InitializeObjects()
        {
            _books = new List<Book>();
            _bookResponseList = new List<BookResponse>();
        }
    }
}
