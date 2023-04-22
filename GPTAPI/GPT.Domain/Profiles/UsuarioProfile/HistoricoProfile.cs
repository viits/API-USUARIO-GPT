using AutoMapper;
using GPT.Domain.DTO.Request;
using GPT.Domain.DTO.Response;
using GPT.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT.Domain.Profiles.UsuarioProfile
{
    internal class HistoricoProfile : Profile
    {
        public HistoricoProfile()
        {
            CreateMap< CadastrarHistoricoDTO, Historico>();
            CreateMap<Historico, HistoricoResponseDTO>();
        }
    }
}
