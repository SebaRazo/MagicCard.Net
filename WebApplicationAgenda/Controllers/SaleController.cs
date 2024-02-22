using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAgenda.Data.Repository.Interfaces;
using WebApplicationAgenda.Entities;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SaleController : ControllerBase
    {
        
     
       
            private readonly ISaleRepository _saleRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public SaleController(ISaleRepository saleRepository, IUserRepository userRepository, IMapper mapper)
            {
                _saleRepository = saleRepository;
                _userRepository = userRepository;
                _mapper = mapper;

            }

            [HttpGet("all")]
            public async Task<IActionResult> GetAll()
            {
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                var contacts = await _saleRepository.GetAll(userId);
                return Ok(contacts);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetOne(int id)
            {
                try
                {
                    int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                    List<Sale> sales = await _saleRepository.GetAllByUser(userId);
                    Sale sale = sales.FirstOrDefault(x => x.Id == id);
                    if (sale != null)
                    {
                        return Ok(sale);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            [HttpPost]
            public async Task<IActionResult> CreateSale(CreateAndUpdateSale createSaleDto)
            {
                try
                {

                    int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                    await _saleRepository.Create(createSaleDto, userId);



                    return Created("Created", createSaleDto);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }


            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateSale(int id, CreateAndUpdateSale dto)
            {
                try
                {
                    await _saleRepository.Update(id, dto);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }



            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteSaleById(int id)
            {
                try
                {
                    await _saleRepository.Delete(id);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }

                return NoContent();
            }


            

            //para card
            /*
            [HttpGet("getcall/{contactId}")]
            public async Task<IActionResult> GetCallByContactId(int contactId)
            {
                var call = await _contactRepository.GetCallByContactId(contactId);

                if (call == null)
                {
                    return NotFound("No se encontró ninguna llamada con el ID de contacto proporcionado.");
                }

                var callInfoDto = _mapper.Map(call, new CallInfoDto());
                if (callInfoDto == null)
                {

                    return BadRequest("Error al mapear la llamada a CallInfoDto.");
                }


                return Ok(callInfoDto);
            }


            [HttpDelete("deletecalls/{contactId}")]
            public async Task<IActionResult> DeleteCallsByContactId(int contactId)
            {
                try
                {
                    _contactRepository.DeleteCallsByContactId(contactId);
                    return Ok("Se eliminaron las llamadas asociadas al ID de contacto proporcionado.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }*/

        }
    }
