using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string? AuthorFirstName { get; set; }
        public string? AuthorLastName { get; set; }

        public int Age { get; set; }

        public string AuthorLevel { get; set; }

        public List<Book>? Books { get; set; }
        public List<AuthorAddress>? Addresses { get; set; }
    }
   
}
