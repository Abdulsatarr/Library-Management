using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DAL.Abstractions
{
    public interface IBookRepository
    {
        Book GetBookID(int BookId);
        List<Book> GetAllBooks();
        BookDto AddBook(BookDto bookDto);
       bool  DeleteBook(int Id);
        BookDto UpdateBook(BookDto updatedBook);

        List<BookDto> GetMostBorrowedBooks();
        List<Book> GetBooksBorrowedInMay();
    }
}
