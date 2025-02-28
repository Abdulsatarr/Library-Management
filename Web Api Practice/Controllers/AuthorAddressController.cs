using DTOs;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DAL.Abstractions;
using WebApi.DAL.Repostories;

namespace Web_Api_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorAddressController : ControllerBase
    {
        private readonly IAuthorAddressRepository _authoraddressRepository;

        public AuthorAddressController(IAuthorAddressRepository authoraddressRepository)
        {
            _authoraddressRepository = authoraddressRepository;
        }

        // GET: api/AuthorAddress
        [HttpGet]
        public ActionResult<List<AuthorAddress>> GetAllAddress()
        {
            return _authoraddressRepository.GetAllAddress();
        }

        // GET: api/AuthorAddress/5
        [HttpGet("{id}")]
        public ActionResult<AuthorAddress> AuthorAddressGetID(int id)
        {
            var address = _authoraddressRepository.AuthorAddressGetID(id);

            if (address == null)
            {
                return NotFound();
            }

            return address;
        }

        // POST: api/AuthorAddress
        [HttpPost]
        public ActionResult<AuthorAddress> AddAddress(AuthorAddressDto addressdto)
        {
            var addedAuthorAddress = _authoraddressRepository.AddAddress(addressdto);
            return CreatedAtAction(nameof(AuthorAddressGetID), new { id = addedAuthorAddress.AddressID }, addedAuthorAddress);
        }

        // PUT: api/AuthorAddress/5
        [HttpPut("{id}")]
        public IActionResult UpdateAuthorAddress(int id, AuthorAddress address)
        {
            if (id != address.AddressID)
            {
                return BadRequest();
            }

            var updatedAddress = _authoraddressRepository.UpdateAddress(address);

            if (updatedAddress == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/AuthorAddress/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthorAddress(int id)
        {
            bool result = _authoraddressRepository.DeleteAddress(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
