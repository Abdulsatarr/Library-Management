using Azure;
using DTOs;
using Entities;
using JWTAuthentication.NET8._0.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL.Abstractions;
using WebApi.DAL.DBContext;
using WebApi.DAL.Repositories;
using WebApi.DAL.Repostories;
using static System.Reflection.Metadata.BlobBuilder;

namespace Web_Api_Practice.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        // GET: api/Books
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<List<BookDto>> GetAllBooks()
        {
            var books = _bookRepository.GetAllBooks().Select(b => new BookDto
            {
                BookId = b.BookId,
                Name = b.Name,
                Genre = b.Genre
                // Add other properties if BookDto contains more fields that should be returned
            }).ToList();
            return books;
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public ActionResult<BookDto> GetBook(int id)
        {
            var book = _bookRepository.GetBookID(id);
            if (book == null)
            {
                return NotFound();
            }

            var bookDto = new BookDto
            {
                BookId = book.BookId,
                Name = book.Name,
                Genre = book.Genre
                // Map other properties if needed
            };
            return bookDto;
        }

        // POST: api/Books
        [HttpPost]
        public ActionResult<BookDto> AddBook(BookDto bookDto)
        {
            var addedBook = _bookRepository.AddBook(bookDto);
            return CreatedAtAction(nameof(GetBook), new { id = addedBook.BookId }, addedBook);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, BookDto bookDto)
        {
            if (id != bookDto.BookId)
            {
                return BadRequest();
            }

            var updatedBook = _bookRepository.UpdateBook(bookDto); // Assuming UpdateBook accepts and returns a BookDto
            if (updatedBook == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            bool result = _bookRepository.DeleteBook(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("MostBorrowedBooks")]
        public ActionResult<List<BookDto>> GetMostBorrowedBooks()
        {
            var mostBorrowedBooks = _bookRepository.GetMostBorrowedBooks();
            if (mostBorrowedBooks == null || !mostBorrowedBooks.Any())
                return NotFound("No borrowing records found.");

            return Ok(mostBorrowedBooks);
        }

        [HttpGet("GetBooksBorrowedInMay")]
        public ActionResult<List<BookDto>> GetBooksBorrowedInMay()
        {
            var books = _bookRepository.GetBooksBorrowedInMay();
            if (books == null || !books.Any())
                return NotFound("No borrowing records found.");

            var results = books.Select(book => new BookDto
            {
                BookId = book.BookId,
                Name = book.Name,
                Genre = book.Genre
                // Map other fields if necessary
            }).ToList();

            return Ok(results);
        }
    }
}
