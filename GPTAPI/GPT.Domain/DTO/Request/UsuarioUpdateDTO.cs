using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT.Domain.DTO.Request
{
    public class UsuarioUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "The field name is required!")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "The field email is required!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "The field password is required!")]
        public string Senha { get; set; }
    }
}
