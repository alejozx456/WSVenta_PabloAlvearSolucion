using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WSVenta_PabloAlvear.Models;
using WSVenta_PabloAlvear.Models.Common;
using WSVenta_PabloAlvear.Models.Request;
using WSVenta_PabloAlvear.Models.Response;
using WSVenta_PabloAlvear.Tools;

namespace WSVenta_PabloAlvear.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }
        public UserResponse Auth(AuthRequest model)

        {
            UserResponse userResponse = new UserResponse();
            using (var db=new VentaRealContext())
            {
                string spassword = Encrypt.GetSHA256(model.Password);
                var usuario = db.Usuarios.Where(d => d.Email == model.Email &&
                                                  d.Password == spassword).FirstOrDefault();
                if (usuario == null) return null;
                userResponse.Email = usuario.Email;
                userResponse.Token = GetToken(usuario);
            }
            return userResponse;
            
        }
        private string GetToken(Usuario usuario)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var llave= Encoding.ASCII.GetBytes(_appSettings.Secreto);
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                   new Claim[] {

                    new Claim(ClaimTypes.NameIdentifier,usuario.Id.ToString()),
                    new Claim(ClaimTypes.Email,usuario.Email)
                   }
                    ),
                Expires = DateTime.UtcNow.AddDays(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(llave), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(TokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
