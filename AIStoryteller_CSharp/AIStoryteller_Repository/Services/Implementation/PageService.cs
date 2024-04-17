using AIStoryteller_Repository.Entities;
using AIStoryteller_Repository.Payload.Request;
using AIStoryteller_Repository.Payload.Response;
using AIStoryteller_Repository.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Services.Implementation
{
    public class PageService : IPageService
    {
        private List<Page> _pages;
        private Page _page;
        private List<PageResponse> _pageResponseList;
        private int _bookId;
        private IPageRepository _pageRepository;
        private IMapper _mapper;
        public PageService(IPageRepository pageRepository, IMapper mapper)
        {
            _mapper = mapper;
            _pageRepository = pageRepository;
            InitializeObjects();
        }
        public async Task<List<PageResponse>> GetPagesByBookId(int bookId)
        {
            _bookId = bookId;
            await LoadPagesFromDatabase();
            await MapPageToPageResponseList();
            return _pageResponseList;
        }
        private async Task LoadPagesFromDatabase()
        {
            _pages = await _pageRepository.GetMultipleBy(page => page.BookId == _bookId);
            await MapPageToPageResponseList();
        }
        private Task MapPageToPageResponseList()
        {
            return Task.Run(() =>
            {
                foreach (var page in _pages)
                {
                    _pageResponseList.Add(new PageResponse()
                    {
                        Content = page.Content,
                        PageNumber = page.PageNumber,
                    });
                }
            });
        }
        private void InitializeObjects()
        {
            _pageResponseList = new List<PageResponse>();
            _pages = new List<Page>();
        }
    }
}
