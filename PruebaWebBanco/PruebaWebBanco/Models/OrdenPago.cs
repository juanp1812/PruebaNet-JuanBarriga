using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PruebaWebBanco.Models
{
    public class OrdenPago
    {
        public string cn = System.Configuration.ConfigurationManager.ConnectionStrings["CnBanco"].ToString();

        public int iOrdenPago { get; set; }
        public int iSucursal { get; set; }
        public decimal fMonto { get; set; }
        public int iTipoMoneda { get; set; }
        public string xMoneda { get; set; }
        public int iEstado { get; set; }
        public string xDescripcionEstado { get; set; }
        public string dFechaPago { get; set; }

        public OrdenPago obtenerOrdenPAgo(int iOrdenPago)
        {
            var ordenPagoBE = new OrdenPago();
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_obtenerOrdenPagoPorId", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pr1 = cmd.Parameters.Add("@iOrdenPago", SqlDbType.Int);
                    pr1.Value = iOrdenPago;

                    con.Open();
                    SqlDataReader drd = cmd.ExecuteReader();
                    if (drd != null)
                    {
                        while (drd.Read())
                        {
                            if (drd.HasRows && !drd.IsDBNull(drd.GetOrdinal("iSucursal")))
                            {
                                ordenPagoBE = new OrdenPago();
                                ordenPagoBE.iOrdenPago = drd.GetInt32(drd.GetOrdinal("iOrden"));
                                ordenPagoBE.iSucursal = drd.GetInt32(drd.GetOrdinal("iSucursal"));
                                ordenPagoBE.fMonto = (decimal)drd.GetDouble(drd.GetOrdinal("fMonto"));
                                ordenPagoBE.iTipoMoneda = drd.GetInt32(drd.GetOrdinal("iTipoMoneda"));
                                ordenPagoBE.xMoneda = drd.GetString(drd.GetOrdinal("xMoneda"));
                                ordenPagoBE.iEstado = drd.GetInt32(drd.GetOrdinal("iEstado"));
                                ordenPagoBE.xDescripcionEstado = drd.GetString(drd.GetOrdinal("xDescripcionEstado"));
                                ordenPagoBE.dFechaPago = drd.GetString(drd.GetOrdinal("dFechaPago"));
                            }
                        }
                    }
                }
            }
            return ordenPagoBE;
        }

        public int insertarOrdenPago(OrdenPago ordenPagoBE)
        {
            var result = 0;
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_insertOrdenPago", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pr1 = cmd.Parameters.Add("@iSucursal", SqlDbType.Int);
                    pr1.Value = ordenPagoBE.iSucursal;

                    SqlParameter pr2 = cmd.Parameters.Add("@fMonto", SqlDbType.Float);
                    pr2.Value = ordenPagoBE.fMonto;

                    SqlParameter pr3 = cmd.Parameters.Add("@iTipoMoneda", SqlDbType.Int);
                    pr3.Value = ordenPagoBE.iTipoMoneda;

                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        public int Edit(OrdenPago ordenPagoBE)
        {
            var result = 0;
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_modificarOrdenPago", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pr1 = cmd.Parameters.Add("@iOrdenPago", SqlDbType.Int);
                    pr1.Value = ordenPagoBE.iOrdenPago;

                    SqlParameter pr2 = cmd.Parameters.Add("@fMonto", SqlDbType.Float);
                    pr2.Value = ordenPagoBE.fMonto;

                    SqlParameter pr3 = cmd.Parameters.Add("@iTipoMoneda", SqlDbType.Int);
                    pr3.Value = ordenPagoBE.iTipoMoneda;

                    SqlParameter pr4 = cmd.Parameters.Add("@iEstado", SqlDbType.Int);
                    pr4.Value = ordenPagoBE.iEstado;

                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }

            return 1;
        }


    }

    public class OrdenesPago
    {
        public string cn = System.Configuration.ConfigurationManager.ConnectionStrings["CnBanco"].ToString();

        public int iBanco { get; set; }
        public int iSucursal { get; set; }
        public string xSucursal { get; set; }
        public IEnumerable<OrdenPago> lstOrderVenta { get; set; }


        public OrdenesPago getAllOrdenes(int iSucursal)
        {
            var sucursalBE = new Sucursal().obtenerSucursal(iSucursal);
            var lstOrdenesVentas = new List<OrdenPago>();
            var ordenVentaBE = new OrdenPago();

            var ordenesVentaBE = new OrdenesPago();
            ordenesVentaBE.iSucursal = sucursalBE.iSucursal;
            ordenesVentaBE.xSucursal = sucursalBE.xNombre;
            ordenesVentaBE.iBanco = sucursalBE.iBanco;

            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_obtenerOrdenesPago", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pr1 = cmd.Parameters.Add("@iSucursal", SqlDbType.Int);
                    pr1.Value = iSucursal;

                    con.Open();
                    SqlDataReader drd = cmd.ExecuteReader();
                    if (drd != null)
                    {
                        while (drd.Read())
                        {
                            if (drd.HasRows && !drd.IsDBNull(drd.GetOrdinal("iOrden")))
                            {
                                ordenVentaBE = new OrdenPago();
                                ordenVentaBE.iOrdenPago = drd.GetInt32(drd.GetOrdinal("iOrden"));
                                ordenVentaBE.iSucursal = drd.GetInt32(drd.GetOrdinal("iSucursal"));
                                ordenVentaBE.fMonto = (decimal)drd.GetDouble(drd.GetOrdinal("fMonto"));
                                ordenVentaBE.iTipoMoneda = drd.GetInt32(drd.GetOrdinal("iTipoMoneda"));
                                ordenVentaBE.xMoneda = drd.GetString(drd.GetOrdinal("xMoneda"));
                                ordenVentaBE.iEstado = drd.GetInt32(drd.GetOrdinal("iEstado"));
                                ordenVentaBE.xDescripcionEstado = drd.GetString(drd.GetOrdinal("xDescripcionEstado"));
                                ordenVentaBE.dFechaPago = drd.GetString(drd.GetOrdinal("dFechaPago"));
                                lstOrdenesVentas.Add(ordenVentaBE);
                            }
                        }
                    }
                }
            }
            ordenesVentaBE.lstOrderVenta = lstOrdenesVentas.AsEnumerable();
            return ordenesVentaBE;
        }

    }
}