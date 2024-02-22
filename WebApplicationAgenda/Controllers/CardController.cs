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
    public class CardController : ControllerBase
            {
            
            private readonly ICardRepository _cardRepository;
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;

            public CardController(ICardRepository cardRepository, IUserRepository userRepository, IMapper mapper)
            {
                _cardRepository = cardRepository;
                _userRepository = userRepository;
                _mapper = mapper;

            }

            [HttpGet("all")]
            [Authorize(Roles = "ADMIN,USER,SELLER")]
            public async Task<IActionResult> GetAll()
            {
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                var cards = await _cardRepository.GetAll();
                return Ok(cards);
            }

            [HttpGet("{id}")]
            [Authorize(Roles = "ADMIN,USER,SELLER")]    
            public async Task<IActionResult> GetOneById(int cardId)
            {
            try
            {
                var Card_Id = await _cardRepository.GetById(cardId);

                if (Card_Id == null)
                {
                    return NotFound();
                }
                var dto = _mapper.Map<CardInfoDto>(Card_Id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
            }
            [HttpPost]
            [Authorize(Roles = "ADMIN,SELLER")]
            public async Task<IActionResult> CreateCard(CreateAndUpdateCard createCardDto)
            {
            try
            {
                await _cardRepository.Create(createCardDto);
                return Created("Created", createCardDto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }


            }

            [HttpPut("{id}")]
            [Authorize(Roles = "ADMIN,SELLER")]
            public async Task<IActionResult> UpdateCard(int cardId, CreateAndUpdateCard dto)
            {
                try
                {
                    await _cardRepository.Update(cardId, dto);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }



            [HttpDelete("{id}")]
            [Authorize(Roles = "ADMIN,SELLER")]
            public async Task<IActionResult> DeleteCardById(int cardId)
            {
                try
                {
                    await _cardRepository.Delete(cardId);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }

                return NoContent();
            }


       
            

        }
    }

