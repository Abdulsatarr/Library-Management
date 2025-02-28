using DTOs;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DAL.DBContext;
using WebApi.DAL.Repositories;

namespace XUnitTestApi.BookRepositoryTest
{    
    public class TestMethods
    {
        private InMemoryDatabaseFixture _InMemory;
        private BookRepository _bookRepository;

        public TestMethods()
        {
            _InMemory = new InMemoryDatabaseFixture(); 
            _bookRepository = new BookRepository(_InMemory._context);
        }

        [Fact]
        public void GetBookID()
        {
            // Act
            var result = _bookRepository.GetBookID(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.BookId);
        }
        [Fact]
        public void GetAllBooks()
        {
            // Act
            var result = _bookRepository.GetAllBooks();

            // Assert
            Assert.Single(result);
        }
        [Fact]
        public void AddBook()
        {
            // Arrange
            var bookDto = new BookDto { BookId = 2, Name = "Steve Jobs", Genre = "Non-Fiction" };

            // Act
            var result = _bookRepository.AddBook(bookDto);

            // Assert
            Assert.Equal(bookDto, result);
            Assert.Equal(2, _InMemory._context.Books.Count());
        }
    }
}
