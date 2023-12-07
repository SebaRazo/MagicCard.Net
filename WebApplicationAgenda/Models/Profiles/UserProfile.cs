using AutoMapper;
using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Models.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateAndUpdateUser>();
            CreateMap<User, GetUserByIdReponse>();

            CreateMap<CreateAndUpdateUser, User>();
        }
    }
}
