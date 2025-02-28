using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DAL.DBContext;
using Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL.Abstractions;
using DTOs;
using System.Security.AccessControl;

namespace WebApi.DAL.Repostories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly DBContext.ApplicationDbContext _context;

        public AuthorRepository(DBContext.ApplicationDbContext context)
        {
            _context = context;
        }
        public Author GetAuthorID(int AuthorID )
        {
            return _context.Authors.FirstOrDefault(x => x.AuthorId == AuthorID);
        }

        public List<Author> GetAll()
        {
            return _context.Authors.ToList();

        }

        public AuthorDto AddAuthor(AuthorDto authorDto)
        {
            var authorData = Entities(authorDto);
            _context.Authors.Add(authorData);
            _context.SaveChanges();
            return authorDto;
        }
        public bool DeleteAuthor(int authorId)
        {
            var author = _context.Authors.FirstOrDefault(x=>x.AuthorId==authorId);
            if (author == null) return false;
            _context.Authors.Remove(author);
            _context.SaveChanges();
            return true;
        }
        public Author UpdateAuthor(Author updatedAuthor)
        {
            var author = _context.Authors.Find(updatedAuthor.AuthorId);
            if (author != null)
            {
                author.Age = updatedAuthor.Age;
                
                _context.SaveChanges();
            }
            return author;
        }
        private Author Entities(AuthorDto authorDto)
        {
            Author author = new Author();
            author.AuthorId = authorDto.AuthorId;
            author.AuthorFirstName = authorDto.AuthorFirstName;
            author.AuthorLastName = authorDto.AuthorLastName;
            author.Age = authorDto.Age;
            author.AuthorLevel = authorDto.AuthorLevel;


            //List<Book> bookList = new List<Book>();
            //foreach (var authors in authorDto.Books)
            //{
            //    var bookdata = _context.Books.FirstOrDefault(book => book.BookId == authors);
            //    bookList.Add(bookdata);
            //}
            //author.Books = bookList;
           return author;
        }
    }
}
