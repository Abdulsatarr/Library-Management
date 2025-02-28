using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DAL.Abstractions;
using WebApi.DAL.DBContext;

namespace WebApi.DAL.Repostories
{
    public class PublicationRepository : IPublicationRepository
    { 
        private readonly ApplicationDbContext _context;

        public PublicationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Publication GetPublicationID(int PublicationID)
        {
            return _context.Publication.FirstOrDefault(x => x.PublicationID == PublicationID);
        }

        public List<Publication> GetAll()
        {
            return _context.Publication.ToList();

        }

        public PublicationDto AddPublication(PublicationDto pubdto)
        {
            var pubData = Entities(pubdto);
            _context.Publication.Add(pubData);
            _context.SaveChanges();
            return pubdto;
        }
        public bool DeletePublication(int publicationId)
        {
            var publication = _context.Publication.Find(publicationId);
            if (publication == null) return false;
            _context.Publication.Remove(publication);
            _context.SaveChanges();
            return true;
        }
        public Publication UpdatePublication(Publication updatedPublication)
        {
            var publication = _context.Publication.Find(updatedPublication.PublicationID);
            if (publication != null)
            {
                publication.PublicationYear = updatedPublication.PublicationYear;

                _context.SaveChanges();
            }
            return publication;
        }
        private Publication Entities(PublicationDto pubdto)
        {
            Publication pub = new Publication();
            pub.PublicationID = pubdto.PublicationID;
            pub.PublicationYear = pubdto.PublicationYear;


            //List<Book> bookList = new List<Book>();
            //foreach (var publication in pubdto.Book)
            //{
            //    var bookdata = _context.Books.FirstOrDefault(book => book.BookId == publication);
            //    bookList.Add(bookdata);
            //}
            //pub.Book = bookList;
            return pub;
        }
    }
}
