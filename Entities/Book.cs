using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Book
    {

        public int BookId { get; set; }
        public string Name { get; set; }
        public string Genre { get; set; }
     

       
        public int? PublicationId { get; set; }
        public Publication? Publication { get; set; }
        public List<Author>? Authors { get; set; }
        public List<Borrow>? Borrows { get; set; }
    }
}
