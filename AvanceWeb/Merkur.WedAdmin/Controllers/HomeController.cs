using Merkur.BL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Merkur.WedAdmin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            var productosBL = new ProductosBL();
            var listadeProductos = productosBL.Obtener();
            ViewBag.adminWebsiteUrl= ConfigurationManager.AppSettings["adminWebsiteUrl"];
            return View(listadeProductos);
        }
    }
}