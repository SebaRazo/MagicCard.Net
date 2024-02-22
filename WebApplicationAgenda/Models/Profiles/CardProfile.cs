using AutoMapper;
using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Models.Profiles
{
    public class CardProfile: Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CreateAndUpdateCard>();
            CreateMap<CreateAndUpdateCard, Card>();
            CreateMap<Card, CardInfoDto>();
            CreateMap<CardInfoDto, Card>();
        }

    }
}
