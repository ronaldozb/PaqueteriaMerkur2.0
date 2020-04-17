using Merkur.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Merkur.Web.Controllers
{
    [Authorize]
    public class OrdenDetalleController : Controller
    {
        OrdenesEnvioBL _ordenBL;
        ProductosBL _productosBL;
        public OrdenDetalleController()
        {
            _ordenBL = new OrdenesEnvioBL();
            _productosBL = new ProductosBL();
        }
        // GET: OrdenDetalle
        public ActionResult Index(int id)
        {
            var orden = _ordenBL.ObtenerOrden(id);
            orden.ListadeOrdenEnviosDetalle = _ordenBL.ObtenerOrdenDetalle(id);
            return View(orden);
        }

        public ActionResult Crear(int id)
        {
            var nuevaOrdeDetalle = new OrdenEnviosDetalle();
            nuevaOrdeDetalle.OrdenId = id;
            var productos = _productosBL.ObtenerproductosActivos();

            ViewBag.ProductoId = new SelectList(productos, "Id", "Descripcion");
            return View(nuevaOrdeDetalle);
        }
        [HttpPost]
        public ActionResult Crear(OrdenEnviosDetalle ordenDetalle)
        {
            if(ModelState.IsValid)
            {
                if(ordenDetalle.ProductoId==0)
                {
                    ModelState.AddModelError("ProductoId", "Selecciones un Paquete");
                    return View(ordenDetalle);
                }
                _ordenBL.GuardarOrdenDetalle(ordenDetalle);
                return RedirectToAction("index", new { id= ordenDetalle.OrdenId});
            }

            var productos = _productosBL.ObtenerproductosActivos();
            ViewBag.ProductoId = new SelectList(productos, "Id", "Descripcion");

            return View(ordenDetalle);
        }

        public ActionResult Eliminar(int id)
        {
            var ordenDetalle = _ordenBL.ObtenerOrdenDetallePorId(id);
            return View(ordenDetalle);
        }

        [HttpPost]
        public ActionResult Eliminar(OrdenEnviosDetalle ordeDetalle)
        {
            _ordenBL.EliminarOrdenDetalle(ordeDetalle.Id);

            return RedirectToAction("Index", new { id = ordeDetalle.OrdenId });
        }
    }
}