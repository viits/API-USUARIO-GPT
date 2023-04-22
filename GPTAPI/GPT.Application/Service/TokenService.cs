using GPT.Domain.Model;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using GPT.Application.Interface;
using GPT.Domain.DTO.Request;

namespace GPT.Application.Service
{
    public class TokenService
    {
        public Token CreateToken(Usuario usuario)
        {
            Claim[] usuarioClaim = new Claim[]
            {
                new Claim("username", usuario.Nome),
                new Claim("id",usuario.Id.ToString())
            };
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn"));
            var credenciais = new SigningCredentials(chave,SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims:usuarioClaim,signingCredentials: credenciais, expires: DateTime.UtcNow.AddHours(1));
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }

        public Token GenerateToken(int id)
        {
            var securityToken = new JwtSecurityTokenHandler();
            var token = securityToken.CreateToken(new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn" + id)), SecurityAlgorithms.HmacSha256Signature)
            });

            return new Token(securityToken.WriteToken(token));
        }

        public bool verifyToken(RequestAtivacaoDTO token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(token.Token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0asdjas09djsa09djasdjsadajsd09asjd09sajcnzxn" + token.Id)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                },out _);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

    }
}
