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
    public interface IHistorico
    {
        Task<Result> CadastrarHistorico(CadastrarHistoricoDTO historicoDto);
        Task<Result> DeleteHistorico(int id);
        HistoricoResponseDTO GetById(int id);
        IEnumerable<HistoricoResponseDTO> ListHistorico(int usuarioId);
    }
}
