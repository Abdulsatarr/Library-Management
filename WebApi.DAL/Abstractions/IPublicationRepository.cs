using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DAL.Abstractions
{
    public interface IPublicationRepository
    {
        Publication GetPublicationID(int PublicationId);
        List<Publication> GetAll();
        PublicationDto AddPublication(PublicationDto pubdto);
        bool DeletePublication(int Id);
        Publication UpdatePublication(Publication updatedPublication);
    }
}
