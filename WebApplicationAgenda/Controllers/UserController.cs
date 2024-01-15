using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAgenda.Data.Repository.Interfaces;
using WebApplicationAgenda.Models.Dtos;

namespace WebApplicationAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public UserController(IMapper mapper, IUserRepository userRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userRepository.GetAll();
                return Ok(users);
            }
            catch (Exception ex)
            {
                
                return BadRequest($"Error: {ex.Message}");
            }
             //return Ok(_userRepository.GetAll());
        }

        [HttpGet]
        [Route("{Id}")]
        public async Task<IActionResult> GetOneById(int Id)
        {
            try
            {
                var User_Id = await _userRepository.GetById(Id);

                if (User_Id == null)
                {
                    return NotFound();
                }
                var dto = _mapper.Map<GetUserByIdReponse>(User_Id);
                return Ok(dto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateAndUpdateUser dto)
        {
            try
            {
                await _userRepository.Create(dto);
                return Created("Created", dto);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
            
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id_user, CreateAndUpdateUser dto)
        {
            try
            {
                var userItem= await _userRepository.GetById(id_user);//podria utilizar el UserExists
                if(userItem == null)
                {
                    return NotFound();
                }
                await _userRepository.Update(id_user, dto);
                return NoContent();
            }
            catch(Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
            
        
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user=await _userRepository.GetById(id);
                if(user==null)
                {
                    return NotFound();
                }
                else
                {
                    await _userRepository.Delete(id);
                    return NoContent();
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}
