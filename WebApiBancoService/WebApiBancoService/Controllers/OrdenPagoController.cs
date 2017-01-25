using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiBancoService.Models;
using System.Web.Http.Description;

namespace WebApiBancoService.Controllers
{
    public class OrdenPagoController : ApiController
    {
        //http://localhost:3814/api/OrdenPago?iSucursal=1&iTipoMoneda=1
        [HttpGet]
        public IHttpActionResult ObtenerOrdenes(int iSucursal, int iTipoMoneda)
        {
            var lstOrdenPagoBE = new OrdenPago().getAll(iSucursal, iTipoMoneda);
            if (lstOrdenPagoBE == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(lstOrdenPagoBE);
            }
        }
    }
}
