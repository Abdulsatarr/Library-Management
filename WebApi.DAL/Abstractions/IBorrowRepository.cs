using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DAL.Abstractions
{
    public interface IBorrowRepository
    {
        List<Borrow> GetAllBorrows();
        Borrow GetBorrowById(int borrowId);
        BorrowDto AddBorrow(BorrowDto borrowDto);
        bool DeleteBorrow(int borrowId);
        Borrow UpdateBorrow(Borrow updatedBorrow);
    }
}
