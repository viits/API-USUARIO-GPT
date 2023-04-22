using GPTAPI.DTO;
using GPTAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GPTAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private ChatGptService _service;
        public WeatherForecastController(ChatGptService service)
        {
            _service = service;
        }
        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Chat([FromBody] ChatDTO dto)
        {
           var chat = await _service.chatResponse(dto);
           return Ok(chat);
        }
    }
}