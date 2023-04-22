using GPT.Application.Interface;
using GPT.Domain.DTO.Request;
using GPTAPI.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using OpenAI_API;

namespace GPTAPI.Service
{
    public class ChatGptService
    {
        private string model = "text-davinci-002";    
        private IConfiguration _configuration;
        private string apiKey = "";
        private IHistorico _historico;
        private readonly IHttpContextAccessor _httpContext;
        public ChatGptService(IConfiguration configuration, IHistorico historico, IHttpContextAccessor httpContext)
        {
            _configuration = configuration;
            apiKey = _configuration["GptKey"];
            _historico = historico;
            _httpContext = httpContext;
        }

        public async Task<ChatResponseDTO> chatResponse(ChatDTO dto){
            var openaiApi = new OpenAIAPI(apiKey);
            var temperature = 0.5;
            var maxTokens = 500;

            var response = await openaiApi.Completions.CreateCompletionAsync(
                prompt: dto.Text,
                temperature: temperature,
                max_tokens: maxTokens
            );
           
            var answer = response.Completions[0].Text;
            var dtos = new ChatResponseDTO();
            dtos.Text = answer;
            var dtoHistorico = historicoDTO(dto.Text, dtos.Text);
            var historico = _historico.CadastrarHistorico(dtoHistorico);
            return dtos;
        }
        private CadastrarHistoricoDTO historicoDTO(string pergunta, string resposta)
        {
            return new CadastrarHistoricoDTO() { 
                Pergunta = pergunta,
                Resposta = resposta,
                UsuarioId = Convert.ToInt32(_httpContext.HttpContext.User.FindFirst(x=> x.Type == "id")?.Value)
            };
        }
    }
}