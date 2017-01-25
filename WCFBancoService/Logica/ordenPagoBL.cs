using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Datos;
using Entidades;

namespace Logica
{
    public class ordenPagoBL
    {
         public List<OrdenPago> getAll(int iSucursal, int iTipoMoneda){

             return new OrdenPagoDA().getAll(iSucursal, iTipoMoneda);

         }
    }
}
