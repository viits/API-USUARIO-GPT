using GPT.Application.Interface;
using GPT.Application.Service;
using GPT.Domain.DTO.Request;
using Microsoft.AspNetCore.Mvc;

namespace GPTAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogin _loginService;
        public LoginController(ILogin loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public ActionResult Login([FromBody] LoginRequestDTO loginDto)
        {
            var resultado =  _loginService.LoginUsuario(loginDto);
            if (resultado.IsFailed) return Unauthorized(resultado.Reasons);
            return Ok(resultado.Reasons);
        }
    }
}
