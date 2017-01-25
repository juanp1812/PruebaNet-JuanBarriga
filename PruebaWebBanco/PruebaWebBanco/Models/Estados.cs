using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PruebaWebBanco.Models
{
    public class Estado
    {
        public string cn = System.Configuration.ConfigurationManager.ConnectionStrings["CnBanco"].ToString();
        public int iEstado { get; set; }
        public string xEstado { get; set; }


        public SelectList getEstados(int iDefault)
        {

            var lstEstados = new SelectList(
                                new[]
                        {

                            new {Value = 1,Text="Pagada"},
                            new {Value = 2,Text="Declinada"},
                            new {Value = 3,Text="Fallida"},
                            new {Value = 4,Text="Anulada"}

                        }, "Value", "Text", iDefault);

            return lstEstados;
        }
    }
}