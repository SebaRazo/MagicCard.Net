using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebApplicationAgenda.Data.Repository.Interfaces;
using WebApplicationAgenda.Models.Dtos;
using System; // Asegúrate de incluir System para DateTime
using Microsoft.Extensions.Configuration; // Para IConfiguration

namespace WebApplicationAgenda.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public AuthenticationController(IConfiguration config, IUserRepository userRepository)
        {
            _config = config; // Inyección para poder usar el appsettings.json
            this._userRepository = userRepository;
        }

        [HttpPost("authenticate")]
        public async Task<ActionResult<string>> Autenticar(AuthenticationRequestBody authenticationRequestBody)
        {
            // Paso 1: Validamos las credenciales
            var user = await _userRepository.Validate(authenticationRequestBody); // Asegúrate de que Validate sea un método asíncrono

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
    }
}
