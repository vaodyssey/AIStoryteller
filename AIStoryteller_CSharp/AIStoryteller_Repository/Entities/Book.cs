using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Entities
{
    public class Book:BaseEntity
    {
        public string Name { get; set; }    
        public long Size { get; set; }    
        public virtual ICollection<Page> Pages { get; set; }
    }
}
