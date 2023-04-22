using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPT.Domain.Model
{
    public class Token
    {
        public Token(string value) {
            this.Value = value;
        }
        public string Value { get; set; }
    }
}
