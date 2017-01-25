using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaWebBanco.Models
{
    public class Moneda
    {
        public string cn = System.Configuration.ConfigurationManager.ConnectionStrings["CnBanco"].ToString();
        public int iMoneda { get; set; }
        public string xMoneda { get; set; }


        public SelectList getMonedas(int iDefault)
        {

            var TipoMonedaList = new SelectList(
                                new[]
                        {

                            new {Value = 1,Text="Soles"},
                            new {Value = 2,Text="Dolares"}

                        }, "Value", "Text", iDefault);

            return TipoMonedaList;
        }
    }
}