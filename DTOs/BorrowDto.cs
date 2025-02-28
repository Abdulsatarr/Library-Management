using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class BorrowDto
    {
        public int BorrowId { get; set; }
        public int BookId { get; set; }
        public int StudentId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime? ReturnDate { get; set; }

  
    }
}
