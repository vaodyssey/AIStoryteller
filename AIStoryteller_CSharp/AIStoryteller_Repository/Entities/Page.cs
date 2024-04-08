using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Entities
{
    public class Page:BaseEntity
    {
        public string Content { get; set; } 
        public int PageNumber { get; set; } 
        public int BookId { get; set; } 
        public virtual Book Book { get; set; }  
    }
}
