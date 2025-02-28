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
    public class PublicationController : ControllerBase
    {
        private readonly IPublicationRepository _publicationRepository;

        public PublicationController(IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }

        // GET: api/Publication
        [HttpGet]
        public ActionResult<List<Publication>> GetAll()
        {
            return _publicationRepository.GetAll();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public ActionResult<Publication> GetPublicationID(int id)
        {
            var publication = _publicationRepository.GetPublicationID(id);

            if (publication == null)
            {
                return NotFound();
            }

            return publication;
        }

        // POST: api/Publication
        [HttpPost]
        public IActionResult AddPublication(PublicationDto pubdto)
        {
            var addedAuthor = _publicationRepository.AddPublication(pubdto);
            return CreatedAtAction(nameof(GetPublicationID), new { id = addedAuthor }, addedAuthor);
        }


        // PUT: api/Publication/5
        [HttpPut("{id}")]
        public IActionResult UpdatePublication(int id, Publication publication)
        {
            if (id != publication.PublicationID)
            {
                return BadRequest();
            }

            var updatedPub = _publicationRepository.UpdatePublication(publication);

            if (updatedPub == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Publication/5
        [HttpDelete("{id}")]
        public IActionResult DeletePublication(int id)
        {
            bool result = _publicationRepository.DeletePublication(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
