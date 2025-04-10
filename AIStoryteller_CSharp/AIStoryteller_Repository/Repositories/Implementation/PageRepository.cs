﻿using AIStoryteller_Repository.Entities;
using AIStoryteller_Repository.Migrations;
using AIStoryteller_Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Repositories.Implementation
{
    public class PageRepository : IPageRepository
    {
        private readonly AIStorytellerDbContext _dbContext;
        public PageRepository(AIStorytellerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task BulkInsert(List<Page> pages)
        {
            await _dbContext.BulkInsertAsync(pages);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Page> GetById(int id)
        {
            var result = await _dbContext.Pages.FirstOrDefaultAsync(page => page.Id == id);
            return result!;
        }

        public async Task<List<Page>> GetMultipleBy(Expression<Func<Page, bool>> predicate)
        {
            var result = await _dbContext.Pages.Where(predicate).ToListAsync()!;
            return result;
        }

        public async Task Update(Page page)
        {
            _dbContext.Pages.Update(page);            
            await _dbContext.SaveChangesAsync();
        }
    }
}
