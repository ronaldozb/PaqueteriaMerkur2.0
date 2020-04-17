using Merkur.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Merkur.Web.Controllers
{
    [Authorize]
    public class VentasMerkurController : Controller
    {
        VentaBL _ventaBL;
        public VentasMerkurController()
        {
            _ventaBL = new VentaBL();
        }

        // GET: VentasMerkur
        public ActionResult Index()
        {
            var VentasBL = new VentaBL();
            var Listadeventa = _ventaBL.Obtenerventa();
            return View(Listadeventa);
        }

        [HttpGet]
        public ActionResult Crear()
        {
            var nuevoVenta = new venta();
            return View(nuevoVenta);
        }
        [HttpPost]
        public ActionResult Create(venta venta, HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid)
            {
                if (venta.Descripcion != venta.Descripcion.Trim() )

                {
                    ModelState.AddModelError("Error", "Los Campos no deben contener espacios en blanco");
                    return View(venta);
                }
                //if (imagen != null)
                //{
                //    venta.UrlImagen = GuardarImagen(imagen);
                //}
                _ventaBL.GuardarVenta(venta);

                return RedirectToAction("Index");
            }
            return View(venta);
        }

        //private string GuardarImagen(HttpPostedFileBase imagen)
        //{
        //    string path = Server.MapPath("~/Imagenes/" + imagen.FileName);
        //    imagen.SaveAs(path);

        //    return "/Imagenes/" + imagen.FileName;
        //}

        public ActionResult Edit(int id)
        {
            var venta = _ventaBL.Obtenerventas(id);
            return View(venta);
        }

        [HttpPost]
        public ActionResult Edit(venta venta, HttpPostedFileBase imagen)
        {
            //if (imagen != null)
            //{
            //    venta.UrlImagen = GuardarImagen(imagen);
            //}
            _ventaBL.GuardarVenta(venta);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var _ventaBL = new VentaBL();
            var venta = _ventaBL.Obtenerventas(id);
            return View(venta);
        }

        [HttpPost]
        public ActionResult Delete(venta venta)
        {
            var _ventaBL = new VentaBL();
            _ventaBL.EliminarVenta(venta);

            return RedirectToAction("Index");
        }

        public ActionResult Details(int id)
        {
            var venta = _ventaBL.Obtenerventas(id);
            return View(venta);
        }
    }
}