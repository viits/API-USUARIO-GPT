using AutoMapper;
using FluentResults;
using GPT.Application.Interface;
using GPT.Domain.Data;
using GPT.Domain.DTO.Request;
using GPT.Domain.DTO.Response;
using GPT.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT.Application.Service
{
    public class HistoricoService : IHistorico
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public HistoricoService(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Result> CadastrarHistorico(CadastrarHistoricoDTO historicoDto)
        {
            try
            {
                var historico = _mapper.Map<Historico>(historicoDto);
                _context.Historicos.Add(historico);
                _context.SaveChanges();
                return Result.Ok().WithSuccess("Histórico Cadastrado com Sucesso");
            }catch(Exception ex)
            {
                return Result.Fail(ex.Message);
            }
            
        }

        public async Task<Result> DeleteHistorico(int id)
        {
            var historico = _context.Historicos.Where(i => i.Id == id).FirstOrDefault();
            if (historico != null) return Result.Fail("Usuario não encontrado!");
            _context.Historicos.Remove(historico);
            _context.SaveChanges();
            return Result.Ok().WithSuccess("Deletado com sucesso!");
        }

        public HistoricoResponseDTO GetById(int id)
        {
            var historico = _context.Historicos.Where(i=> i.Id == id).FirstOrDefault();
            if (historico == null) return null;
            return _mapper.Map<HistoricoResponseDTO>(historico);
        }

        public IEnumerable<HistoricoResponseDTO> ListHistorico(int usuarioId)
        {
            var historicos = _context.Historicos.Where(i=> i.UsuarioId == usuarioId).ToList();
            if (historicos == null) return null;
            var historicoDTO = _mapper.Map<List<HistoricoResponseDTO>>(historicos);
            return historicoDTO;
        }
    }
}
