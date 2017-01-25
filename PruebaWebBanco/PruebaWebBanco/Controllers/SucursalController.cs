using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaWebBanco.Models;

namespace PruebaWebBanco.Controllers
{
    public class SucursalController : Controller
    {
        //
        // GET: /Sucursal/

        public ActionResult Index(int id)
        {
            Session["BancoRecibido"] = id;
            var sucursalModel = new Sucursales();
            var resultSet = sucursalModel.getAllInfo(id);
            return View(resultSet);
        }

        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Sucursal sucursalModel)
        {
            sucursalModel.iBanco = Int32.Parse(Session["BancoRecibido"].ToString());
            if (ModelState.IsValid)
            {
                int retval = sucursalModel.insertarSucursal(sucursalModel);
                if (retval > 0)
                {
                    return RedirectToAction("Index", "Sucursal", new { id = sucursalModel.iBanco });
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }
            return View();

        }


        [HttpGet]
        public ActionResult Edit(Sucursal SucursalModel, int id)
        {
            var SucursalBE = new Sucursal();
            SucursalBE = SucursalModel.obtenerSucursal(id);
            return View(SucursalBE);
        }

        [HttpPost]
        public ActionResult Edit(Sucursal sucursalBE, FormCollection form)
        {
            var iBanco = Int32.Parse(Session["BancoRecibido"].ToString());

            if (ModelState.IsValid)
            {
                int result = sucursalBE.Edit(sucursalBE);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Sucursal", new { id = iBanco });
                }
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }
            return View(sucursalBE);
        }

        public ActionResult Eliminar(Sucursal sucursalBE, int id)
        {
            var iBanco = Int32.Parse(Session["BancoRecibido"].ToString());

            var result = sucursalBE.eliminarSucursal(id);
            if (result > 0)
            {
                return RedirectToAction("Index", "Sucursal", new { id = iBanco });
            }
            {
                ModelState.AddModelError("", "Can Not Update");
                return View("Index");
            }
        }
    }
}
