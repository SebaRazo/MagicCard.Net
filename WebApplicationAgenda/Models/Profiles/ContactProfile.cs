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
            CreateMap<Call, CallInfoDto>()
           .ForMember(dest => dest.TimeCall, opt => opt.MapFrom(src => src.TimeCall.ToString("dd/MM/yyyy - HH:mm")));

            CreateMap<Contact, BlockedContactWithCallInfoDto>()
            .ForMember(dest => dest.CallInfo, opt => {
                opt.MapFrom(src => new CallInfoDto
                {
                    CountCall = src.Calls != null ? src.Calls.Sum(call => call.CountCall) : 0,
                    TimeCall = src.Calls != null && src.Calls.Any() ?
                       src.Calls.Max(call => call.TimeCall).ToString("dd/MM/yyyy - HH:mm") :
                       string.Empty
                });
            });
            //revisar bien esto



            /*
            CreateMap<Contact, BlockedContactWithCallInfoDto>()
           .ForMember(dest => dest.CallInfo, opt => { 
               opt.MapFrom(src => src.Calls);
               opt.NullSubstitute(new CallInfoDto()); //Si src.Call es nulo, utiliza un objeto Call vacío
           });
            */
        }
    }
}
