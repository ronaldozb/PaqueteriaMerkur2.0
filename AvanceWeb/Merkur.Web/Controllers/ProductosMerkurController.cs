using Merkur.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Merkur.Web.Controllers
{
    [Authorize]
    public class ProductosMerkurController : Controller
    {
        ProductosBL _productosBL;

        public ProductosMerkurController()
        {
            _productosBL = new ProductosBL();
        }

        // GET: ProductosMerkur
        public ActionResult Index()
        {
            var listadeProductos = _productosBL.Obtener();
            return View(listadeProductos);
        }
    }
}