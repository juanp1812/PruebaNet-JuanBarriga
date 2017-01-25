using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PruebaWebBanco.Models
{
    public class Sucursal
    {
        public string cn = System.Configuration.ConfigurationManager.ConnectionStrings["CnBanco"].ToString();

        public int iSucursal { get; set; }
        public int iBanco { get; set; }
        public string xBanco { get; set; }
        public string xNombre { get; set; }
        public string xDireccion { get; set; }
        public string dFechaRegistro { get; set; }


        public IEnumerable getAll(int iBanco)
        {
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
                                SucursalBE.iSucursal = drd.GetInt32(drd.GetOrdinal("iSucursal"));
                                SucursalBE.iBanco = drd.GetInt32(drd.GetOrdinal("iBanco"));
                                SucursalBE.xBanco = drd.GetString(drd.GetOrdinal("xNombreBanco"));
                                SucursalBE.xNombre = drd.GetString(drd.GetOrdinal("xNombre"));
                                SucursalBE.xDireccion = drd.GetString(drd.GetOrdinal("xDireccion"));
                                SucursalBE.dFechaRegistro = drd.GetString(drd.GetOrdinal("dFechaRegistro"));
                                lstSucursal.Add(SucursalBE);
                            }
                        }
                    }
                }
            }
            return lstSucursal.AsEnumerable();
        }

        public int insertarSucursal(Sucursal SucursalBE)
        {
            var result = 0;
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_insertSucursal", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pr1 = cmd.Parameters.Add("@iBanco", SqlDbType.Int);
                    pr1.Value = SucursalBE.iBanco;

                    SqlParameter pr2 = cmd.Parameters.Add("@xNombreSucursal", SqlDbType.VarChar);
                    pr2.Value = SucursalBE.xNombre;

                    SqlParameter pr3 = cmd.Parameters.Add("@xDireccion", SqlDbType.VarChar);
                    pr3.Value = SucursalBE.xDireccion;

                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }

        public int Edit(Sucursal SucursalBE)
        {
            var result = 0;
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_updateSucursal", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pr1 = cmd.Parameters.Add("@iSucursal", SqlDbType.Int);
                    pr1.Value = SucursalBE.iSucursal;

                    SqlParameter pr2 = cmd.Parameters.Add("@xNombreSucursal", SqlDbType.VarChar);
                    pr2.Value = SucursalBE.xNombre;

                    SqlParameter pr3 = cmd.Parameters.Add("@xDireccion", SqlDbType.VarChar);
                    pr3.Value = SucursalBE.xDireccion;

                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }

            return 1;
        }

        public Sucursal obtenerSucursal(int iSucursal)
        {
            var sucursalBE = new Sucursal();
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerSucursalesPorId", con))
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
                            if (drd.HasRows && !drd.IsDBNull(drd.GetOrdinal("iSucursal")))
                            {
                                sucursalBE = new Sucursal();
                                sucursalBE.iSucursal = drd.GetInt32(drd.GetOrdinal("iSucursal"));
                                sucursalBE.iBanco = drd.GetInt32(drd.GetOrdinal("iBanco"));
                                sucursalBE.xNombre = drd.GetString(drd.GetOrdinal("xNombre"));
                                sucursalBE.xDireccion = drd.GetString(drd.GetOrdinal("xDireccion"));
                            }
                        }
                    }
                }
            }
            return sucursalBE;
        }

        public int eliminarSucursal(int iSucursal)
        {
            var result = 0;
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_deleteSucursal", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pr1 = cmd.Parameters.Add("@isucursal", SqlDbType.Int);
                    pr1.Value = iSucursal;

                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }
            return 1;
        }  
    
    }

    public class Sucursales {

        public string cn = System.Configuration.ConfigurationManager.ConnectionStrings["CnBanco"].ToString();
        public int iBanco  { get; set; }
        public string xNombreBanco { get; set; }
        public IEnumerable<Sucursal> SucursalList { get; set; }


        public Sucursales getAllInfo(int iBanco)
        {
            var bancoBE = new Banco().obtenerBanco(iBanco);
            var lstSucursal = new List<Sucursal>();
            var SucursalBE = new Sucursal();

            var SucursalesBE = new Sucursales();
            SucursalesBE.iBanco = bancoBE.iBanco;
            SucursalesBE.xNombreBanco = bancoBE.xNombreBanco;

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
                                SucursalBE.iSucursal = drd.GetInt32(drd.GetOrdinal("iSucursal"));
                                SucursalBE.iBanco = drd.GetInt32(drd.GetOrdinal("iBanco"));
                                SucursalBE.xBanco = drd.GetString(drd.GetOrdinal("xNombreBanco"));
                                SucursalBE.xNombre = drd.GetString(drd.GetOrdinal("xNombre"));
                                SucursalBE.xDireccion = drd.GetString(drd.GetOrdinal("xDireccion"));
                                SucursalBE.dFechaRegistro = drd.GetString(drd.GetOrdinal("dFechaRegistro"));
                                lstSucursal.Add(SucursalBE);
                            }
                        }
                    }
                }
            }
            SucursalesBE.SucursalList = lstSucursal.AsEnumerable();
            return SucursalesBE;
        }


    }
}