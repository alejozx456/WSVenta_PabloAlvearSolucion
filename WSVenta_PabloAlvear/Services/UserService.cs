using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta_PabloAlvear.Models;
using WSVenta_PabloAlvear.Models.Request;
using WSVenta_PabloAlvear.Models.Response;
using WSVenta_PabloAlvear.Tools;

namespace WSVenta_PabloAlvear.Services
{
    public class UserService : IUserService
    {
        public UserResponse Auth(AuthRequest model)

        {
            UserResponse userResponse = null;
            using (var db=new VentaRealContext())
            {
                string spassword = Encrypt.GetSHA256(model.Password);
                var usuario = db.Usuarios.Where(d => d.Email == model.Email &&
                                                  d.Password == spassword).FirstOrDefault();
                if (usuario == null) return null;
                userResponse.Email = usuario.Email;
            }
            return userResponse;
            
        }
    }
}
