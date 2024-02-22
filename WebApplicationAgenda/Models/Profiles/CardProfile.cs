using AutoMapper;
using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Models.Profiles
{
    public class CardProfile: Profile
    {
        public CardProfile()
        {
            CreateMap<Sale, CreateAndUpdateCard>();
            CreateMap<CreateAndUpdateCard, Sale>();
            CreateMap<Sale, SaleInfoDto>();
        }

    }
}
