using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Entidades;
using Logica;

namespace WCFBancoService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "BancoService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select BancoService.svc or BancoService.svc.cs at the Solution Explorer and start debugging.
    public class BancoService : IBancoService
    {
        public List<OrdenPago> getAll(int iSucursal, int iTipoMoneda) {
            return new ordenPagoBL().getAll(iSucursal, iTipoMoneda);
        }
        public List<Sucursal> obtenerSucursalPorBanco(int iBanco){
            return new SucursalBL().obtenerSucursalPorBanco(iBanco);
        }
    }
}
