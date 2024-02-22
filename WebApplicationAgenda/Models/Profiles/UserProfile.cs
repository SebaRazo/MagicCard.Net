using AutoMapper;
using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Models.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateAndUpdateSale>();
            CreateMap<User, GetUserByIdReponse>();
            CreateMap<User, UserWithCardInfoDto>();//ver
            CreateMap<CreateAndUpdateUser, User>();
        }
    }
}
