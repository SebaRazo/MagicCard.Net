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
            public async Task<IActionResult> GetAll()
            {
                int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                var cards = await _cardRepository.GetAll(userId);
                return Ok(cards);
            }

            [HttpGet("{id}")]
            public async Task<IActionResult> GetOne(int id)
            {
                try
                {
                    int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                    List<Card> cards = await _cardRepository.GetAllByUser(userId);
                    Card card = cards.FirstOrDefault(x => x.Id == id);
                    if (card != null)
                    {
                        return Ok(card);
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
            public async Task<IActionResult> CreateCard(CreateAndUpdateCard createCardDto)
            {
                try
                {

                    int userId = Int32.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type.Contains("nameidentifier")).Value);
                    await _cardRepository.Create(createCardDto, userId);



                    return Created("Created", createCardDto);
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }


            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateCard(int id, CreateAndUpdateCard dto)
            {
                try
                {
                    await _cardRepository.Update(id, dto);
                    return NoContent();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }

            }



            [HttpDelete("{id}")]
            public async Task<IActionResult> DeleteCardById(int id)
            {
                try
                {
                    await _cardRepository.Delete(id);
                }
                catch (Exception ex)
                {
                    return NotFound(ex.Message);
                }

                return NoContent();
            }


            
            /*

            [HttpGet("getcall/{contactId}")]
            public async Task<IActionResult> GetCallByCardId(int id)
            {
                var call = await _cardRepository.GetCallByCardId(id);

                if (call == null)
                {
                    return NotFound("No se encontró ninguna llamada con el ID de contacto proporcionado.");
                }

                var callInfoDto = _mapper.Map(call, new CardInfoDto());
                if (callInfoDto == null)
                {

                    return BadRequest("Error al mapear la llamada a CardInfoDto.");
                }


                return Ok(callInfoDto);
            }


            [HttpDelete("deletecalls/{contactId}")]
            public async Task<IActionResult> DeleteCallsByContactId(int cardId)
            {
                try
                {
                    _cardRepository.DeleteCardsByUserId(cardId);
                    return Ok("Se eliminaron las llamadas asociadas al ID de contacto proporcionado.");
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }*/

        }
    }

