using AutoMapper;
using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Models.Profiles
{
    public class ContactProfile: Profile
    {
        public ContactProfile()
        {
            CreateMap<Contact, CreateAndUpdateContact>();
            CreateMap<CreateAndUpdateContact, Contact>();
            
        }
    }
}
