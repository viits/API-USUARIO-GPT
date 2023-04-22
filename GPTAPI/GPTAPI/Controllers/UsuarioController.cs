using GPT.Application.Interface;
using GPT.Domain.DTO.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPTAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _context;
        public UsuarioController(IUsuario context)
        {
            _context = context;
        }
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetUsuario(int id) {
            var usuarioDto = _context.GetById(id);
            if(usuarioDto == null) return NotFound();
            return Ok(usuarioDto);
        }
       
        [HttpPost]
        public IActionResult CadastrarUsuario([FromBody] UsuarioRequestDTO dto)
        {
            var resultado = _context.CadastrarUsuario(dto);
            if (resultado.Result.IsFailed) return BadRequest(resultado.Result.Reasons);
            return Ok("Cadastrado com Sucesso");
        }

        [HttpPost("AtivacaoUsuario")]
        public IActionResult AtivacaoUsuario([FromBody] RequestAtivacaoDTO dto)
        {
            var resultado = _context.AtivacaoUsuario(dto);
            if (resultado.Result.IsFailed) return BadRequest(resultado.Result.Reasons);
            return Ok("Ativado");
        }
    }
}
