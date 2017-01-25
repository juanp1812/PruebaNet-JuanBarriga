using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Entidades
{
    [DataContract]
    public class OrdenPago
    {
        [DataMember]
        public int iCodigoOrdenPago { get; set; }
        [DataMember]
        public int iCodigoSucursal { get; set; }
        [DataMember]
        public string xNombreSucursal { get; set; }
        [DataMember]
        public decimal fMonto { get; set; }
        [DataMember]
        public int iCodigoTipoMoneda { get; set; }
        [DataMember]
        public string xMonedaDescripcion { get; set; }
        [DataMember]
        public int iCodigoEstado { get; set; }
        [DataMember]
        public string xDescripcionEstado { get; set; }
        [DataMember]
        public string dFechaPago { get; set; }

    }
}
