using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta_PabloAlvear.Models.Request;

namespace WSVenta_PabloAlvear.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("login")]
        public IActionResult Autentificar([FromBody] AuthRequest model)
        {
            return Ok(model);

        }
    }
}
