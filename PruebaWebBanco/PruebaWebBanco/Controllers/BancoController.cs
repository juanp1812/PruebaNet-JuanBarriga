using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PruebaWebBanco.Models;

namespace PruebaWebBanco.Controllers
{
    public class BancoController : Controller
    {
        //
        // GET: /Banco/

        public ActionResult Index(Banco BancoModel)
        {
            Banco BancoModelDA = new Banco();
            var resultSet = BancoModelDA.getAll();

            return View(resultSet);
        }
        
        [HttpGet]
        public ActionResult Insert()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Banco BancoModel)
        {
            if (ModelState.IsValid)
            {
                int retval = BancoModel.insertarBanco(BancoModel);
                if (retval > 0)
                {
                    return RedirectToAction("Index", "Banco");
                }
                else
                {
                    ModelState.AddModelError("", "Can Not Insert");
                }
            }
            return View();

        }

        [HttpGet]
        public ActionResult Edit(Banco BancoModel, int id)
        {
            var BancoBE = new Banco();
            BancoBE = BancoModel.obtenerBanco(id);
            return View(BancoBE);
        }
        
        [HttpPost]
        public ActionResult Edit(Banco bancoBE, FormCollection form)
        {
            if (ModelState.IsValid)
            {
                int result = bancoBE.Edit(bancoBE);
                if (result > 0)
                {
                    return RedirectToAction("Index", "Banco");
                }
                {
                    ModelState.AddModelError("", "Can Not Update");
                }
            }
            return View(bancoBE);
        }
        
        public ActionResult Eliminar(Banco BancoModel, int id)
        {
            var result = BancoModel.eliminarBanco(id);
            if (result > 0)
            {
                return RedirectToAction("Index", "Banco");
            }
            {
                ModelState.AddModelError("", "Can Not Update");
                return View("Index");
            }
        }
    }
}
