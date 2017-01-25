using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiBancoService.Models;

namespace WebApiBancoService.Controllers
{
    public class SucursalController : ApiController
    {
        //http://localhost:3814/api/Sucursal?iBanco=1
        [HttpGet]
        public IHttpActionResult obtenerSucursalPorBanco(int iBanco)
        {
            var lstSucursales = new Sucursal().obtenerSucursalPorBanco(iBanco);
            if (lstSucursales == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(lstSucursales);
            }
        }
    }
}
