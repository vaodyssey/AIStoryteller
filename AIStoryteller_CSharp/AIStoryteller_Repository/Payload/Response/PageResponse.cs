using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Payload.Response
{
    public class PageResponse
    {
        public int Id { get; set; } 
        public string Content { get; set; }
        public int PageNumber { get; set; }        
        public string AudioPath { get; set; }   
    }
}
