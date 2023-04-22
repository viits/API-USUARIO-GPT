using FluentResults;
using GPT.Domain.DTO.Request;

namespace GPT.Application.Interface
{
    public interface ILogin
    {
        Result LoginUsuario(LoginRequestDTO loginDto);
    }
}
