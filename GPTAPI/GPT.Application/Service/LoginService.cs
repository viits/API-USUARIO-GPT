using FluentResults;
using GPT.Application.Interface;
using GPT.Domain.Data;
using GPT.Domain.DTO.Request;
using GPT.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT.Application.Service
{
    public class LoginService : ILogin
    {
        private readonly DataContext _context;
        private readonly TokenService _tokenService;
        public LoginService(DataContext dataContext, TokenService tokenService)
        {
            _context = dataContext;
            _tokenService = tokenService;
        }

        public Result LoginUsuario(LoginRequestDTO loginDto)
        {
            var usuario = _context.Usuarios.Where(i => i.Email == loginDto.Email && i.Senha == loginDto.Senha).FirstOrDefault();
            if (usuario == null) return Result.Fail("Email ou Senha inválidas");
            if (usuario.Ativo == 0) return Result.Fail("Usuario não esta ativo ainda!");
            Token token = _tokenService.CreateToken(usuario);
            return Result.Ok().WithSuccess(token.Value);
        }
        
    }
}
