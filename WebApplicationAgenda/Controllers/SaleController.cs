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
            [Authorize(Roles = "ADMIN")]
            public async Task<IActionResult> GetAll()
            {
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                var contacts = await _saleRepository.GetAll(userId);
                return Ok(contacts);
            }

            [HttpGet("{id}")]
            [Authorize(Roles = "ADMIN,SELLER,USER")]
            public async Task<IActionResult> GetOne(int id)
            {
                
                try
                {
                    int uuserId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                    List<Sale> sales = await _saleRepository.GetAllByUser(uuserId);
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
            [Authorize(Roles = "ADMIN,USER")]
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
            [Authorize(Roles = "ADMIN")]
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
            [Authorize(Roles = "ADMIN")]
            public async Task<IActionResult> DeleteSaleById(int userId)
            {
                try
                {
                    await _saleRepository.Delete(userId);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }

                return NoContent();
            }


        }
    }
