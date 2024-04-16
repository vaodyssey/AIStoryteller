using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Payload.Request
{
    public class GetBooksPagingRequest
    {
        public int PageCount { get; set; }
        public int PageSize { get; set; }
    }
}
