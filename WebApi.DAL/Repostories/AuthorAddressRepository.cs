using DTOs;
using Entities;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApi.DAL.Abstractions;
using WebApi.DAL.DBContext;

namespace WebApi.DAL.Repostories
{
    public class AuthorAddressRepository : IAuthorAddressRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthorAddressRepository(ApplicationDbContext context)
        {
            _context = context;
        }
      

        public AuthorAddress AuthorAddressGetID(int AuthorAddressId)
        {
            return _context.AuthorAddress.FirstOrDefault(x => x.AddressID == AuthorAddressId);
        }

        [Authorize(Roles = "Admin")]
        public List<AuthorAddress> GetAllAddress()
        {
            return _context.AuthorAddress.ToList();
        }

        public AuthorAddressDto AddAddress(AuthorAddressDto addressdto)
        {
            var addressData = Entities(addressdto);
            _context.AuthorAddress.Add(addressData);
            _context.SaveChanges();
            return addressdto;
        }

        public bool DeleteAddress(int Id)
        {
            var address = _context.AuthorAddress.Find(Id);
            if (address == null) return false;
            _context.AuthorAddress.Remove(address);
            _context.SaveChanges();
            return true;
        }

        public AuthorAddress UpdateAddress(AuthorAddress updatedAuthorAddress)
        {
            var address = _context.AuthorAddress.Find(updatedAuthorAddress.AddressID);
            if (address != null)
            {
                _context.Entry(address).CurrentValues.SetValues(updatedAuthorAddress);
                _context.SaveChanges();
            }
            return address;
        }
        private AuthorAddress Entities(AuthorAddressDto addressdto)
        {
            AuthorAddress address = new AuthorAddress();
            address.AddressID = addressdto.AddressID;
            address.City = addressdto.City;
            address.Country = addressdto.Country;
            address.AuthorId = addressdto.AuthorId;
            return address;

        }
    }
}