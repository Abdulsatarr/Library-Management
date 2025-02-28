using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class AuthorAddressDto
    {
        [Key]
        public int AddressID { get; set; }
        public string Country { get; set; }
        public string City { get; set; }

        [ForeignKey("Author")]
        public int? AuthorId { get; set; }
       
    }
}
