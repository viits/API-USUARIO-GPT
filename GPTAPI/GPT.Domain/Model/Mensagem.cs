using GPT.Domain.DTO.Response;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT.Domain.Model
{
    public class Mensagem
    {
        public MailboxAddress Destinatario { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(UsuarioResponseDTO usuario, string assunto, string token) {
            Destinatario = new MailboxAddress(usuario.Nome, usuario.Email);
            Assunto = assunto;
            Conteudo = $"http://localhost:6000/ativa?usuarioId={usuario.Id}&CodigoDeAtivacao={token}";
        }
    }
}
