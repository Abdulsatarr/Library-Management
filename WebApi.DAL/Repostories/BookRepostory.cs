using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL.DBContext;
using Entities;
using WebApi.DAL.Abstractions;
using DTOs;

namespace WebApi.DAL.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DBContext.ApplicationDbContext _context;

        public BookRepository(DBContext.ApplicationDbContext context)
        {
            _context = context;
        }

        public Book GetBookID(int bookId )
        {

            return _context.Books.FirstOrDefault(x => x.BookId == bookId);
        }


        public List<Book> GetAllBooks()
        {
            return _context.Books.ToList();
        }

        public BookDto AddBook(BookDto bookDto)
        {
          var bookData =  Entities(bookDto);
            _context.Books.Add(bookData);
            _context.SaveChanges();
            return bookDto;
        }

        public bool DeleteBook(int bookId)
        {
            var book = _context.Books.Find(bookId);
            if (book == null) return false;
            _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }

        public BookDto UpdateBook(BookDto updatedBook)
        {
            var book = _context.Books.Find(updatedBook.BookId);
            if (book != null)
            {
                book.Name = updatedBook.Name;
                book.Genre = updatedBook.Genre;
                _context.SaveChanges();
            }
            return updatedBook;
        }



        public List<BookDto> GetMostBorrowedBooks()
        {
            return _context.Books
                .Include(b => b.Borrows)
                .OrderByDescending(b => b.Borrows.Count())
                .Take(5)
                .Select(book => new BookDto
                {
                    BookId = book.BookId,
                    Name = book.Name,
                    Genre = book.Genre,
                    PublicationId = book.PublicationId,
                    BorrowCount = book.Borrows.Count 

                })
                .ToList();
        }



        public List<Book> GetBooksBorrowedInMay()
        {
            
            DateTime startDate = new DateTime(DateTime.Now.Year, 5, 1);

            
            DateTime endDate = new DateTime(DateTime.Now.Year, 5, DateTime.DaysInMonth(DateTime.Now.Year, 5));

            var books = _context.Books
                .Include(b => b.Borrows) 
                .Where(b => b.Borrows.Any(br => br.BorrowDate >= startDate && br.BorrowDate <= endDate))
                .OrderByDescending(b => b.Borrows.Count(br => br.BorrowDate >= startDate && br.BorrowDate <= endDate)) 
                .Take(1) 
                .ToList(); 

            return books; 
        }
        private Book Entities(BookDto bookDto)
        {
            Book book = new Book();
            book.BookId = bookDto.BookId;
            book.Name = bookDto.Name;
            book.Genre = bookDto.Genre;
            book.PublicationId = bookDto.PublicationId;
   
            //     List<Author> authorList = new List<Author>();
            //    foreach(var books in bookDto.AuthorId)
            //{
            //    var authordata =_context.Authors.FirstOrDefault(author => author.AuthorId == books);
            //    authorList.Add(authordata);
            //}
            //book.Authors = authorList;
            return book;
        }
    }
}

