using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT.Domain.Model
{
    public class Usuario
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo nome é obrigatorio!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo email é obrigatorio!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatorio!")]
        public string Senha { get; set; }
        public int Ativo { get; set; }
        public virtual ICollection<Historico>? Historico { get; set; }
    }
}
