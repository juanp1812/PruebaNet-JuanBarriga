using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Entidades;
using Logica;
using System.ServiceModel.Web;

namespace WCFBancoService
{

    [ServiceContract]
    public interface IBancoService
    {
        //http://localhost:2314/BancoService.svc/ObtenerOrdenes?iSucursal=1&iTipoMoneda=2
        [OperationContract]
        [WebGet(UriTemplate = "ObtenerOrdenes?iSucursal={iSucursal}&iTipoMoneda={iTipoMoneda}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        List<OrdenPago> getAll(int iSucursal, int iTipoMoneda);

        //http://localhost:2314/BancoService.svc/obtenerSucursalPorBanco?iBanco=1
        [OperationContract]
        [WebGet(UriTemplate = "obtenerSucursalPorBanco?iBanco={iBanco}", ResponseFormat = WebMessageFormat.Xml)]
        List<Sucursal> obtenerSucursalPorBanco(int iBanco);
    }
}
