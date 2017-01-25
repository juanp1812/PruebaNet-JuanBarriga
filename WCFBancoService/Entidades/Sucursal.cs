using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    [DataContract]
    public class Sucursal
    {
        [DataMember]
        public int iCodigoSucursal { get; set; }
        [DataMember]
        public int iCodigoBanco { get; set; }
        [DataMember]
        public string xNombreBanco { get; set; }
        [DataMember]
        public string xNombreSucursal { get; set; }
        [DataMember]
        public string xDireccion { get; set; }
        [DataMember]
        public string dFechaRegistro { get; set; }
    }
}
