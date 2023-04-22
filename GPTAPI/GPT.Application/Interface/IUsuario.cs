using FluentResults;
using GPT.Domain.DTO.Request;
using GPT.Domain.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT.Application.Interface
{
    public interface IUsuario
    {
        Task<Result> CadastrarUsuario(UsuarioRequestDTO usuarioDto);
        Task<Result> AlterarUsuario(UsuarioUpdateDTO usuarioDto);
        UsuarioResponseDTO GetById(int id);
        Task<Result> AtivacaoUsuario(RequestAtivacaoDTO usuarioToken);
    }
}
