using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIStoryteller_Repository.Payload.Response
{
    public class BookResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public long Size { get; set; }
    }
}
