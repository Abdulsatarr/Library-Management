using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL.Abstractions;

namespace Web_Api_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BorrowController : ControllerBase
    {
        private readonly IBorrowRepository _borrowRepository;

        public BorrowController(IBorrowRepository borrowRepository)
        {
            _borrowRepository = borrowRepository;
        }

        [HttpGet]
        public ActionResult<List<Borrow>> GetAllBorrows()
        {
            return _borrowRepository.GetAllBorrows();
        }

        [HttpGet("{id}")]
        public ActionResult<Borrow> GetBorrow(int id)
        {
            var borrow = _borrowRepository.GetBorrowById(id);
            if (borrow == null)
            {
                return NotFound();
            }
            return borrow;
        }

        [HttpPost]
        public ActionResult<BorrowDto> AddBorrow(BorrowDto borrowDto)
        {
            var addedBorrow = _borrowRepository.AddBorrow(borrowDto);
            return CreatedAtAction(nameof(GetBorrow), new { id = addedBorrow.BorrowId }, addedBorrow);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBorrow(int id, Borrow borrow)
        {
            if (id != borrow.BorrowId)
            {
                return BadRequest();
            }

            var updatedBorrow = _borrowRepository.UpdateBorrow(borrow);
            if (updatedBorrow == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBorrow(int id)
        {
            bool result = _borrowRepository.DeleteBorrow(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        
    }
}
