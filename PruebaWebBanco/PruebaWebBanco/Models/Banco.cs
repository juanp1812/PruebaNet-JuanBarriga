using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace PruebaWebBanco.Models
{
    public class Banco
    {
        public string cn = System.Configuration.ConfigurationManager.ConnectionStrings["CnBanco"].ToString();

        public int iBanco { get; set; }
        public string xNombreBanco { get; set; }
        public string xDireccion { get; set; }
        public string dFechaRegistro { get; set; }



        public IEnumerable getAll()
        {
            var lstbanco = new List<Banco>();
            var bancoBE = new Banco();
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_listarBancos", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    con.Open();
                    SqlDataReader drd = cmd.ExecuteReader();
                    if (drd != null)
                    {
                        while (drd.Read())
                        {
                            if (drd.HasRows && !drd.IsDBNull(drd.GetOrdinal("iBanco"))){
                                bancoBE = new Banco();
                                bancoBE.iBanco = drd.GetInt32(drd.GetOrdinal("iBanco"));
                                bancoBE.xNombreBanco = drd.GetString(drd.GetOrdinal("xNombreBanco"));
                                bancoBE.xDireccion = drd.GetString(drd.GetOrdinal("xDireccion"));
                                bancoBE.dFechaRegistro = drd.GetString(drd.GetOrdinal("dFechaRegistro"));
                                lstbanco.Add(bancoBE);
                            }
                        }
                    }
                }
            }
            return lstbanco.AsEnumerable();
        }

        public Banco obtenerBanco(int iBanco)
        {
            var bancoBE = new Banco();
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_ObtenerBanco", con))
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
                            if (drd.HasRows && !drd.IsDBNull(drd.GetOrdinal("iBanco")))
                            {
                                bancoBE = new Banco();
                                bancoBE.iBanco = drd.GetInt32(drd.GetOrdinal("iBanco"));
                                bancoBE.xNombreBanco = drd.GetString(drd.GetOrdinal("xNombreBanco"));
                                bancoBE.xDireccion = drd.GetString(drd.GetOrdinal("xDireccion"));
                                bancoBE.dFechaRegistro = drd.GetString(drd.GetOrdinal("dFechaRegistro"));
                            }
                        }
                    }
                }
            }
            return bancoBE;
        }

        public int insertarBanco(Banco BancoBE)
        {
            var result = 0;
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_insertBanco", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter pr2 = cmd.Parameters.Add("@xNombreBanco", SqlDbType.VarChar);
                    pr2.Value = BancoBE.xNombreBanco;

                    SqlParameter pr3 = cmd.Parameters.Add("@xDireccion", SqlDbType.VarChar);
                    pr3.Value = BancoBE.xDireccion;

                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }
            return result;
        }  

        public int Edit(Banco BancoBE)
        {
            var result = 0;
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_updateBanco", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pr1 = cmd.Parameters.Add("@iBanco", SqlDbType.Int);
                    pr1.Value = BancoBE.iBanco;

                    SqlParameter pr2 = cmd.Parameters.Add("@xNombreBanco", SqlDbType.VarChar);
                    pr2.Value = BancoBE.xNombreBanco;

                    SqlParameter pr3 = cmd.Parameters.Add("@xDireccion", SqlDbType.VarChar);
                    pr3.Value = BancoBE.xDireccion;

                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }
            return 1;
        }

        public int eliminarBanco(int iBanco)
        {
            var result = 0;
            using (SqlConnection con = new SqlConnection(cn))
            {
                using (SqlCommand cmd = new SqlCommand("sp_deleteBanco", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter pr1 = cmd.Parameters.Add("@iBanco", SqlDbType.Int);
                    pr1.Value = iBanco;

                    con.Open();
                    result = cmd.ExecuteNonQuery();
                }
            }
            return 1;
        }  
    }
}