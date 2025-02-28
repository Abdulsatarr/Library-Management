using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class BookDto
    {
        public int BookId { get; set; }
        public string Name { get; set; }

        public string Genre { get; set; }


        [ForeignKey("Publication")]
        public int? PublicationId { get; set; }
        //public List<int> AuthorId { get; set; }
        public int BorrowCount { get; set; }
    }
}
