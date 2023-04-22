using AutoMapper;
using FluentResults;
using GPT.Application.Interface;
using GPT.Domain.Data;
using GPT.Domain.DTO.Request;
using GPT.Domain.DTO.Response;
using GPT.Domain.Model;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT.Application.Service
{
    public class UsuarioService : IUsuario
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly EmailService _emailService;
        private readonly TokenService _tokenService;
        private readonly IHttpContextAccessor _httpContext;
        public UsuarioService(DataContext context, IMapper mapper, EmailService emailService, TokenService tokenService, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _emailService = emailService;
            _tokenService= tokenService;
            _httpContext = httpContextAccessor;
        }

        public Task<Result> AlterarUsuario(UsuarioUpdateDTO usuarioDto)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> CadastrarUsuario(UsuarioRequestDTO usuarioDto)
        {
            var resultado = validacaoUsuario(usuarioDto);
            if (resultado.IsFailed) return resultado;
            var usuario = _mapper.Map<Usuario>(usuarioDto);
            usuario.Ativo = 0;
            try
            {
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                var dto = _mapper.Map<UsuarioResponseDTO>(usuario);
                var token = _tokenService.GenerateToken(usuario.Id);
                _emailService.enviarEmail(
                    dto,
                    "Link de ativação",
                    token.Value);
                return Result.Ok();
            }catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }
        public async Task<Result> AtivacaoUsuario(RequestAtivacaoDTO usuarioToken)
        {
            try
            {
                var verificaToken = _tokenService.verifyToken(usuarioToken);
                if (!verificaToken) return Result.Fail("token inválido");
                Usuario usuario = _context.Usuarios.Where(u => u.Id == usuarioToken.Id).FirstOrDefault();
                if (usuario == null) return Result.Fail("Usuario Não encontrado");
                usuario.Ativo = 1;
                _context.Usuarios.Update(usuario);
                _context.SaveChanges();
                return Result.Ok().WithSuccess("Usuário Ativado com Sucesso!");

            }catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
        }

        private Result validacaoUsuario(UsuarioRequestDTO usuarioDto)
        {
            var emailExistente = _context.Usuarios.Where(i => i.Email == usuarioDto.Email).FirstOrDefault();
            if (emailExistente != null) return Result.Fail("Email já existente!");
            if (usuarioDto.Senha.Length < 8) return Result.Fail("A Senha tem que ter no minimo 8 caracteres!");
            return Result.Ok();
        }
        public UsuarioResponseDTO GetById(int id)
        {
            var usuario = _context.Usuarios.Where(i => i.Id.Equals(id)).FirstOrDefault();
            if (usuario == null) return null;
            return _mapper.Map<UsuarioResponseDTO>(usuario);
            
        }

    }
}
