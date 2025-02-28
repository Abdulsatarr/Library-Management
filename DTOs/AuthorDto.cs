using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AuthorDto
    {
        public int AuthorId { get; set; }
        public string? AuthorFirstName { get; set; }
        public string? AuthorLastName { get; set; }

        public int Age { get; set; }

        public string AuthorLevel { get; set; }

   
       
    }
}
