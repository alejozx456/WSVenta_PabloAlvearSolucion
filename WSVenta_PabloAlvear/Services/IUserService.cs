using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta_PabloAlvear.Models.Request;
using WSVenta_PabloAlvear.Models.Response;

namespace WSVenta_PabloAlvear.Services
{
    public interface IUserService
    {
        UserResponse Auth(AuthRequest model);
    }
}
