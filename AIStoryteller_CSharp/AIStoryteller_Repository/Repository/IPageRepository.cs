using AIStoryteller_Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Repository
{
    public interface IPageRepository
    {
        public Task BulkInsert(List<Page> pages);
        public Task<List<Page>> GetMultipleBy(Expression<Func<Page,bool>> predicate);
    }
}
