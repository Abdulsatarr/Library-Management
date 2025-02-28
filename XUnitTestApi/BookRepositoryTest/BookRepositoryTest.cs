using Entities;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DAL.DBContext;
using WebApi.DAL.Repositories;

namespace XUnitTestApi.BookRepositoryTest
{
    public class InMemoryDatabaseFixture : IDisposable
    {

        public ApplicationDbContext _context { get; private set; }

        public InMemoryDatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
           .UseInMemoryDatabase(databaseName: "TestDatabase")
           .Options;
            _context = new ApplicationDbContext(options);

            SeedData();

        }

        public void Dispose()
        {
            _context.Dispose();

        }

        public void SeedData()
        {
            var context = _context.Books.Add(new Book() { BookId = 1, Name = "Python", Genre = "Programming" });
            _context.SaveChanges();
        }


    }
}

