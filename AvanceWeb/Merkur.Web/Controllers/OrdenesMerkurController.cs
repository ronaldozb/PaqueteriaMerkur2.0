using Merkur.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Merkur.Web.Controllers
{
    [Authorize]
    public class OrdenesMerkurController : Controller
    {
        OrdenesEnvioBL _ordenesBL;
        ClientesBL _clientesBL;
        public OrdenesMerkurController()
        {
            _ordenesBL = new OrdenesEnvioBL();
            _clientesBL = new ClientesBL();
        }
        // GET: OrdenesMerkur
        public ActionResult Index()
        {
            var listadeOrdenes = _ordenesBL.ObtenerOrdenes();

            return View(listadeOrdenes);
        }

        public ActionResult Crear()
        {
            var nuevaOrden = new OrdenEnvios();
            var clientes = _clientesBL.ObtenerClientesActivos();

            ViewBag.ClienteId = new SelectList(clientes, "Id", "Nombres");

            return View(nuevaOrden);
        }
        [HttpPost]
        public ActionResult Crear(OrdenEnvios orden)
        {
            if(ModelState.IsValid)
            {
                if(orden.ClienteId==0)
                {
                    ModelState.AddModelError("ClienteId", "Selecciones un Cliente");
                    return View(orden);
                }
                _ordenesBL.GuardarOrden(orden);
                return RedirectToAction("Index");
            }
            var clientes = _clientesBL.ObtenerClientesActivos();
            ViewBag.ClienteId = new SelectList(clientes, "Id", "Nombres");
            return View(orden);
        }
        public ActionResult Editar(int id)
        {
            var editarorden =_ordenesBL.ObtenerOrden(id);
            var clientes = _clientesBL.ObtenerClientesActivos();

            ViewBag.ClienteId = new SelectList(clientes, "Id", "Nombres", editarorden.ClienteId);

            return View(editarorden);
        }

       
        [HttpPost]
        public ActionResult Editar(OrdenEnvios orden)
        {
            if(ModelState.IsValid)
            {
                if(orden.ClienteId==0)
                {
                    ModelState.AddModelError("ClienteId", "Selecciones un cliente");
                }
                _ordenesBL.GuardarOrden(orden);
                return RedirectToAction("Index");
            }
            var clientes = _clientesBL.ObtenerClientesActivos();
            ViewBag.ClienteId = new SelectList(clientes, "Id", "Nombres", orden.ClienteId);
            return View(orden);
        }
        public ActionResult Detalle(int id)
        {
            var orden = _ordenesBL.ObtenerOrden(id);
            return View(orden);
        }

    }
}
