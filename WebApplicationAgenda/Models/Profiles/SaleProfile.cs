using AutoMapper;
using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Models.Profiles
{
    public class SaleProfile:Profile
    {

        public SaleProfile()
        {
            CreateMap<Sale, CreateAndUpdateSale>();
            CreateMap<CreateAndUpdateSale, Sale>();
            CreateMap<Card, CardInfoDto>();
        }
          
        
    }
}
