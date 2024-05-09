using AIStoryteller_Repository.Entities;
using AIStoryteller_Repository.Migrations;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Repositories.Implementation
{
    public class PageRepositoryNoPTWBulkInsert : IPageRepository
    {
        private IConfiguration _configuration;
        private string _connectionString;
        private DataTable _bulkInsertDataTable;
        private readonly AIStorytellerDbContext _dbContext;
        public PageRepositoryNoPTWBulkInsert(IConfiguration configuration, AIStorytellerDbContext dbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext; 
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }
        public Task BulkInsert(List<Page> pages)
        {
            return Task.Run(() =>
            {
                CreatePageDataTable();
                InsertPagesIntoPageDataTable(pages);
                InsertPageDataTableToDatabase();
            });
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

        private void CreatePageDataTable()
        {
            _bulkInsertDataTable = new DataTable();
            _bulkInsertDataTable.Columns.Add("Id", typeof(int));
            _bulkInsertDataTable.Columns.Add("Content", typeof(string));
            _bulkInsertDataTable.Columns.Add("PageNumber", typeof(int));
            _bulkInsertDataTable.Columns.Add("BookId", typeof(int));
            _bulkInsertDataTable.Columns.Add("AudioPath", typeof(string));

        }
        private void InsertPagesIntoPageDataTable(List<Page> pages)
        {
            foreach (var page in pages)
            {
                _bulkInsertDataTable.Rows.Add(page.Id, page.Content, page.PageNumber, page.BookId, page.AudioPath);
            }

        }
        private void InsertPageDataTableToDatabase()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "Pages";
                    bulkCopy.WriteToServer(_bulkInsertDataTable);
                }
            }
        }
    }
}
