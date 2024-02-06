using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationAgenda.Data.Repository.Interfaces;
using WebApplicationAgenda.Models.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System;

namespace WebApplicationAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public UserController(IConfiguration config, IMapper mapper, IUserRepository userRepository)
        {
            _config = config;
            _mapper = mapper;
            _userRepository = userRepository;
            
        }

        

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Autenticar(AuthenticationRequestBody authenticationRequestBody)
        {
            // Paso 1: Validamos las credenciales
            var user = await _userRepository.Validate(authenticationRequestBody); 
            if (user is null)
                return Unauthorized();

            // Paso 2: Crear el token
            var securityPassword = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Authentication:SecretForKey"]));

            var credentials = new SigningCredentials(securityPassword, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>
            {
                new Claim("sub", user.Id.ToString()),
                new Claim("given_name", user.Name),
                new Claim("family_name", user.LastName)
            };

            var jwtSecurityToken = new JwtSecurityToken(
                _config["Authentication:Issuer"],
                _config["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                credentials);

            var tokenToReturn = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Ok(tokenToReturn);
        }

        [HttpGet]
        [Authorize]
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
             
        }

        [HttpGet]
        [Authorize]
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
        [Authorize]
        public async Task<ActionResult> UpdateUser(int id_user, CreateAndUpdateUser dto)
        {
            try
            {
                var userItem= await _userRepository.GetById(id_user);
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
        [Authorize]
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
