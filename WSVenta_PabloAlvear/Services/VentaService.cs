using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta_PabloAlvear.Models;
using WSVenta_PabloAlvear.Models.Request;

namespace WSVenta_PabloAlvear.Services
{
    public class VentaService : IVentaService
    {
        public void Add( VentaRequest model)
        {
            
                using (VentaRealContext db = new VentaRealContext())
                {
                    using (var transaction = db.Database.BeginTransaction())
                    {
                        try
                        {
                            var venta = new Ventum();
                            venta.Total = model.Conceptos.Sum(d => d.Cantidad * d.PrecioUnitario);
                            venta.Fecha = DateTime.Now;
                            venta.IdCliente = model.IdCliente;
                            db.Venta.Add(venta);
                            db.SaveChanges();

                            foreach (var modelconcepto in model.Conceptos)
                            {
                                var concepto = new Models.Concepto();
                                concepto.Cantidad = modelconcepto.Cantidad;
                                concepto.IdProducto = modelconcepto.IdProducto;
                                concepto.PrecioUnitario = modelconcepto.PrecioUnitario;
                                concepto.Importe = modelconcepto.Importe;
                                concepto.IdVenta = venta.Id;
                                db.Conceptos.Add(concepto);
                                db.SaveChanges();
                            }
                            transaction.Commit();
                            

                        }
                        catch (Exception)
                        {

                            transaction.Rollback();
                            throw new Exception("Ocurrio un error en la insercion");
                        }

                    }

                }
            
            
        }


    }
}
