using GPT.Application.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPTAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class HistoricoController : ControllerBase
    {
        private readonly IHistorico _context;
        private readonly IHttpContextAccessor _httpContext;
        public HistoricoController(IHistorico context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id) {
            var historicoDto = _context.GetById(id);
            if (historicoDto == null) return NotFound();
            return Ok(historicoDto);
        }
        [HttpGet]
        public IActionResult GetHistorico() {
            var historicos = _context.ListHistorico(Convert.ToInt32(_httpContext.HttpContext.User.FindFirst(x => x.Type == "id")?.Value));
            return Ok(historicos);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteHistorico(int id) {
            var resultado = _context.DeleteHistorico(id);
            if (resultado == null) return NotFound();
            return Ok("Histórico deletado com sucesso!");
        }

    }
}
