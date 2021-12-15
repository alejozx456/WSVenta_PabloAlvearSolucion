using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta_PabloAlvear.Models;
using WSVenta_PabloAlvear.Models.Response;
using WSVenta_PabloAlvear.Models.Request;

namespace WSVenta_PabloAlvear.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {

                using (VentaRealContext db = new VentaRealContext())
                {
                    var lst = db.Clientes.OrderByDescending(d=>d.Id).ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;

                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;

            }
            return Ok(oRespuesta);


        }
        [HttpPost]
        public IActionResult Add(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    oRespuesta.Exito = 1;
                    Cliente oCliente = new Cliente();
                    oCliente.Nombre = oModel.Nombre;
                    db.Clientes.Add(oCliente);
                    db.SaveChanges();
                }


            }
            catch (Exception ex)
            {

                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        [HttpPut]
        public IActionResult Edit(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                    oRespuesta.Exito = 1;
                    Cliente oCliente = db.Clientes.Find(oModel.id);
                    oCliente.Nombre = oModel.Nombre;
                    db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                }


            }
            catch (Exception ex)
            {

                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (VentaRealContext db = new VentaRealContext())
                {
                   
                    Cliente oCliente = db.Clientes.Find(id);
                    db.Remove(oCliente);
                    //db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }


            }
            catch (Exception ex)
            {

                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }
    }
}
