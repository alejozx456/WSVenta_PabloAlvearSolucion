using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta_PabloAlvear.Models;
using WSVenta_PabloAlvear.Models.Request;
using WSVenta_PabloAlvear.Models.Response;
using WSVenta_PabloAlvear.Services;

namespace WSVenta_PabloAlvear.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VentaController : ControllerBase
    {
        private IVentaService _venta;

        public VentaController(IVentaService venta)
        {
            this._venta = venta;
        }
        [HttpPost]
        public IActionResult Add(VentaRequest model)
        {
            Respuesta respuesta = new Respuesta();
            try
            {
                using(VentaRealContext db= new VentaRealContext())
                {
                   
                    _venta.Add(model);
                    respuesta.Exito = 1;
                }
            }
            catch (Exception ex)
            {

                respuesta.Mensaje = ex.Message;

            }

            return Ok(respuesta);
        }
    }
}
