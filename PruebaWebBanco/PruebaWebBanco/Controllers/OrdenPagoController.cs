using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaWebBanco.Models;

namespace PruebaWebBanco.Controllers
{
    public class OrdenPagoController : Controller
    {
        //
        // GET: /OrdenVenta/

        public ActionResult Index(int id)
        {
            Session["SucursalRecibido"] = id;
            var ordenVentas = new OrdenesPago();
            var resultSet = ordenVentas.getAllOrdenes(id);
            return View(resultSet);
        }
        
        [HttpGet]
        public ActionResult Insert()
        {
            var lstMonedas = new Moneda().getMonedas(1);
            ViewBag.TipoMonedaList = lstMonedas;
            return View();
        }

        [HttpPost]
        public ActionResult Insert(OrdenPago ordenPagoModel)
        {
            ordenPagoModel.iSucursal = Int32.Parse(Session["SucursalRecibido"].ToString());
            if (ModelState.IsValid)
            {
                string iTipoMoneda = Request.Form["TipoMonedaList"].ToString();
                ordenPagoModel.iTipoMoneda = int.Parse(iTipoMoneda);
                int retval = ordenPagoModel.insertarOrdenPago(ordenPagoModel);
                if (retval > 0)
                {
                    return RedirectToAction("Index", "OrdenPago", new { id = ordenPagoModel.iSucursal });
                }
                else
                {
                    ModelState.AddModelError("", "Error al insertar el orden de pago.");
                }
            }
            return View();

        }

        [HttpGet]
        public ActionResult Edit(OrdenPago ordenPagoModel, int id)
        {

            var ordenPagoBE = new OrdenPago();
            ordenPagoBE = ordenPagoModel.obtenerOrdenPAgo(id);

            var lstMonedas = new Moneda().getMonedas(ordenPagoBE.iTipoMoneda);
            ViewBag.TipoMonedaList = lstMonedas;

            var lstEstados = new Estado().getEstados(ordenPagoBE.iEstado);
            ViewBag.EstadoList = lstEstados;

            return View(ordenPagoBE);
        }

        [HttpPost]
        public ActionResult Edit(OrdenPago ordenPagoModel, FormCollection form)
        {
            var iSucursal = Int32.Parse(Session["SucursalRecibido"].ToString());

            string iTipoMoneda = Request.Form["TipoMonedaList"].ToString();
            string iEstado = Request.Form["EstadoList"].ToString();
            if (ModelState.IsValid)
            {
                ordenPagoModel.iTipoMoneda = int.Parse(iTipoMoneda);
                ordenPagoModel.iEstado = int.Parse(iEstado);
                int result = ordenPagoModel.Edit(ordenPagoModel);
                if (result > 0)
                {
                    return RedirectToAction("Index", "OrdenPago", new { id = iSucursal });
                }
                {
                    ModelState.AddModelError("", "Hubo un problema al actualizar la orden de pago");
                }
            }
            return View(ordenPagoModel);
        }
    
    }
}
