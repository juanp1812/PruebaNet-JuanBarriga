using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace WebApiBancoService.Models
{
    public class Sucursal
    {
        public int iCodigoSucursal { get; set; }
        public int iCodigoBanco { get; set; }
        public string xNombreBanco { get; set; }
        public string xNombreSucursal { get; set; }
        public string xDireccion { get; set; }
        public string dFechaRegistro { get; set; }

        
        public List<Sucursal> obtenerSucursalPorBanco(int iBanco)
        {
            string cn = ConfigurationManager.ConnectionStrings["CnBanco"].ConnectionString;
            var lstSucursal = new List<Sucursal>();
            var SucursalBE = new Sucursal();
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerSucursalesPorBanco", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pr1 = cmd.Parameters.Add("@iBanco", SqlDbType.Int);
                    pr1.Value = iBanco;

                    con.Open();
                    SqlDataReader drd = cmd.ExecuteReader();
                    if (drd != null)
                    {
                        while (drd.Read())
                        {
                            if (drd.HasRows && !drd.IsDBNull(drd.GetOrdinal("iSucursal")))
                            {
                                SucursalBE = new Sucursal();
                                SucursalBE.iCodigoSucursal = drd.GetInt32(drd.GetOrdinal("iSucursal"));
                                SucursalBE.iCodigoBanco = drd.GetInt32(drd.GetOrdinal("iBanco"));
                                SucursalBE.xNombreBanco = drd.GetString(drd.GetOrdinal("xNombreBanco"));
                                SucursalBE.xNombreSucursal = drd.GetString(drd.GetOrdinal("xNombre"));
                                SucursalBE.xDireccion = drd.GetString(drd.GetOrdinal("xDireccion"));
                                SucursalBE.dFechaRegistro = drd.GetString(drd.GetOrdinal("dFechaRegistro"));
                                lstSucursal.Add(SucursalBE);
                            }
                        }
                    }
                }
            }
            return lstSucursal;
        }
    }

}