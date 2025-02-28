using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebApi.DAL.Abstractions;
using WebApi.DAL.DBContext;

namespace WebApi.DAL.Repostories
{
    public class BorrowRepository : IBorrowRepository
    {
        private readonly ApplicationDbContext _context;

        public BorrowRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Borrow> GetAllBorrows()
        {
            return _context.Borrows.ToList();
        }

        public Borrow GetBorrowById(int borrowId)
        {
            return _context.Borrows.FirstOrDefault(x => x.BorrowId == borrowId);
        }

        public BorrowDto AddBorrow(BorrowDto borrowDto)
        {
            var borrow = new Borrow
            {
                BookId = borrowDto.BookId,
                StudentId = borrowDto.StudentId,
                BorrowDate = borrowDto.BorrowDate,
                ReturnDate = borrowDto.ReturnDate
            };
            _context.Borrows.Add(borrow);
            _context.SaveChanges();
            borrowDto.BorrowId = borrow.BorrowId;
            return borrowDto;
        }

        public bool DeleteBorrow(int borrowId)
        {
            var borrow = _context.Borrows.Find(borrowId);
            if (borrow == null) return false;
            _context.Borrows.Remove(borrow);
            _context.SaveChanges();
            return true;
        }

        public Borrow UpdateBorrow(Borrow updatedBorrow)
        {
            var borrow = _context.Borrows.Find(updatedBorrow.BorrowId);
            if (borrow != null)
            {
                borrow.ReturnDate = updatedBorrow.ReturnDate;
                _context.Borrows.Update(borrow);
                _context.SaveChanges();
            }
            return borrow;
        }
        private Borrow Entities(BorrowDto borrowDto)
        {
            return new Borrow
            {
                BorrowId = borrowDto.BorrowId, 
                BookId = borrowDto.BookId,
                StudentId = borrowDto.StudentId,
                BorrowDate = borrowDto.BorrowDate,
                ReturnDate = borrowDto.ReturnDate
            };
        }
    }
}
