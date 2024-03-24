using IdentityJwt.Entities;
using IdentityJwt.Models;
using IdentityJwt.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityJwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        //libs do proprio Identity (responsavel por gerenciamento de usuario) (aqui estamos configurando o asp.net core identity)
        //Iremos utilizar para validar o usuario, para verificar se ele realmente existe
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public TokenController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] InputLoginRequest input)
        {
            if (string.IsNullOrWhiteSpace(input.Email) || string.IsNullOrWhiteSpace(input.Password))
            {
                return Unauthorized("Email or Password is empty");
            }

            var result = await _signInManager.PasswordSignInAsync(userName: input.Email, password: input.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                var jwtBearerTokenSettings = _configuration.GetSection("JwtBearerTokenSettings");
                var secretKey = jwtBearerTokenSettings["SecretKey"];
                var issuer = jwtBearerTokenSettings["Issuer"];
                var audience = jwtBearerTokenSettings["Audience"];

                if (string.IsNullOrWhiteSpace(secretKey) || string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(audience))
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao processar as configurações do token JWT");
                }

                var token = new TokenJwtBuilder()
                    .AddSecurityKey(JwtSecurityKey.Create(secretKey))
                    .AddSubject("Vitu aprendendo JWT")
                    .AddIssuer(issuer)
                    .AddAudience(audience)
                    .AddExpiry(5)
                    .Builder();

                return Ok(token.value);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
