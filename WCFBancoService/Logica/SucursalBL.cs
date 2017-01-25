using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;
using Datos;

namespace Logica
{
    public class SucursalBL
    {
        public List<Sucursal> obtenerSucursalPorBanco(int iBanco) {
            return new SucursalDA().obtenerSucursalPorBanco(iBanco);
        }
    }
}
