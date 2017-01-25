using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApiBancoService.Models
{
    public class OrdenPago
    {
        public int iCodigoOrdenPago { get; set; }
        public int iCodigoSucursal { get; set; }
        public string xNombreSucursal { get; set; }
        public decimal fMonto { get; set; }
        public int iCodigoTipoMoneda { get; set; }
        public string xMonedaDescripcion { get; set; }
        public int iCodigoEstado { get; set; }
        public string xDescripcionEstado { get; set; }
        public string dFechaPago { get; set; }


        public List<OrdenPago> getAll(int iSucursal, int iTipoMoneda)
        {
            string cn = System.Configuration.ConfigurationManager.ConnectionStrings["CnBanco"].ToString();
            var lstOrdenPago = new List<OrdenPago>();
            var OrdenPagoBE = new OrdenPago();

            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_obtenerOrdenesPagoPorSucursalTipoMoneda", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pr1 = cmd.Parameters.Add("@iSucursal", SqlDbType.Int);
                    pr1.Value = iSucursal;
                    SqlParameter pr2 = cmd.Parameters.Add("@iTipoMoneda", SqlDbType.Int);
                    pr2.Value = iTipoMoneda;

                    con.Open();
                    SqlDataReader drd = cmd.ExecuteReader();
                    if (drd != null)
                    {
                        while (drd.Read())
                        {
                            if (drd.HasRows && !drd.IsDBNull(drd.GetOrdinal("iOrden")))
                            {
                                OrdenPagoBE = new OrdenPago();
                                OrdenPagoBE.iCodigoOrdenPago = drd.GetInt32(drd.GetOrdinal("iOrden"));
                                OrdenPagoBE.iCodigoSucursal = drd.GetInt32(drd.GetOrdinal("iSucursal"));
                                OrdenPagoBE.xNombreSucursal = drd.GetString(drd.GetOrdinal("xNombre"));
                                OrdenPagoBE.fMonto = (decimal)drd.GetDouble(drd.GetOrdinal("fMonto"));
                                OrdenPagoBE.iCodigoTipoMoneda = drd.GetInt32(drd.GetOrdinal("iTipoMoneda"));
                                OrdenPagoBE.xMonedaDescripcion = drd.GetString(drd.GetOrdinal("xMoneda"));
                                OrdenPagoBE.iCodigoEstado = drd.GetInt32(drd.GetOrdinal("iEstado"));
                                OrdenPagoBE.xDescripcionEstado = drd.GetString(drd.GetOrdinal("xDescripcionEstado"));
                                OrdenPagoBE.dFechaPago = drd.GetString(drd.GetOrdinal("dFechaPago"));
                                lstOrdenPago.Add(OrdenPagoBE);
                            }
                        }
                    }
                }
            }
            return lstOrdenPago;
        }
    }

}