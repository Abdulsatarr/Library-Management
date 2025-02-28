using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DAL.Abstractions
{
    public interface IAuthorRepository
    {
        Author GetAuthorID(int AuthorId);
        List<Author> GetAll();
        AuthorDto AddAuthor(AuthorDto authordto);
        bool DeleteAuthor(int Id);
        Author UpdateAuthor(Author updatedAuthor);
   
    }
}
