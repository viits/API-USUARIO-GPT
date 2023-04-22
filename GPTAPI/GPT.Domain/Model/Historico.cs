using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT.Domain.Model
{
    public class Historico
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
