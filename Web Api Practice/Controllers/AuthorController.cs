using DTOs;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.DAL.Abstractions;

namespace Web_Api_Practice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        // GET: api/Authors
        [Authorize(Policy ="get_users")]
        [HttpGet]
        public ActionResult<List<Author>> GetAll()
        {
            return _authorRepository.GetAll();
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public ActionResult<Author> GetAuthorID(int id)
        {
            var author = _authorRepository.GetAuthorID(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // POST: api/Authors
        [HttpPost]
        public ActionResult<Author> AddAuthor(AuthorDto authorDto)
        {
            var addedAuthor = _authorRepository.AddAuthor(authorDto);
            return CreatedAtAction(nameof(GetAuthorID), new { id = addedAuthor }, addedAuthor);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, Author author)
        {
            if (id != author.AuthorId)
            {
                return BadRequest();
            }

            var updatedAuthor = _authorRepository.UpdateAuthor(author);

            if (updatedAuthor == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            bool result = _authorRepository.DeleteAuthor(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
    

