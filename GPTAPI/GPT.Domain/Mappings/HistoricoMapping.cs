using GPT.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT.Domain.Mappings
{
    public class HistoricoMapping : IEntityTypeConfiguration<Historico>
    {
        public void Configure(EntityTypeBuilder<Historico> builder)
        {
            builder.HasOne(x=> x.Usuario).WithMany(x=> x.Historico).HasForeignKey(x=>x.UsuarioId);
        }
    }
}
