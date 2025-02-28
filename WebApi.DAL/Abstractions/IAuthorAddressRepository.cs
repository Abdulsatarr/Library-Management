using DTOs;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.DAL.Abstractions
{
    public interface IAuthorAddressRepository
    {
        AuthorAddress AuthorAddressGetID(int AuthorAddressId);
        List<AuthorAddress> GetAllAddress();
        AuthorAddressDto AddAddress(AuthorAddressDto addressdto);
        bool DeleteAddress(int Id);
        AuthorAddress UpdateAddress(AuthorAddress updatedAuthorAddress);
    }
}
