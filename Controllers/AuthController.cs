using Microsoft.AspNetCore.Mvc;
using Api.Services;
using Api.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/v1/auth")]
    public class AuthController : Controller
    {
        [HttpPost]
        public IActionResult Auth(string username, string password)
        {
            if (username == "luiz" && password == "123456")
            {
                var token = TokenServices.GerarToken(new FuncionarioModel());
                return Ok(token);
            }

            return BadRequest("Usuário ou Senha incorreto.");
        }
    }
}