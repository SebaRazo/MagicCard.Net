using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAgenda.Data;

namespace WebApplicationAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly AgendaContext _context;
        private readonly IMapper _mapper; 

        public ContactController(AgendaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
    }
}
