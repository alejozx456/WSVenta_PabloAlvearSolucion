using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSVenta_PabloAlvear.Models.Request;

namespace WSVenta_PabloAlvear.Services
{
    public interface IVentaService
    {
        public void Add(VentaRequest model);
    }
}
