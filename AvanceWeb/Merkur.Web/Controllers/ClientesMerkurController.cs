using Merkur.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Merkur.Web.Controllers
{
    [Authorize]
    public class ClientesMerkurController : Controller
    {

        ClientesBL clienteBL;

        public ClientesMerkurController()
        {
            clienteBL = new ClientesBL();
        }
            
            // GET: ClientesMerkur
        public ActionResult Index()
        {            
            var ListadeClientes = clienteBL.ObtenerClientes();
            return View(ListadeClientes);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var nuevoCliente = new Cliente();
            return View(nuevoCliente);
        }
        [HttpPost]
        public ActionResult Create(Cliente cliente, HttpPostedFileBase imagen)
        {
            if (ModelState.IsValid)
            {
               if (cliente.Nombres != cliente.Nombres.Trim() || cliente.Cedula!= cliente.Cedula.Trim() || cliente.Email!=cliente.Email.Trim())

                {
                    ModelState.AddModelError("Error", "Los Campos no deben contener espacios en blanco");
                    return View(cliente);
                }
               if (imagen != null)
                {
                    cliente.UrlImagen = GuardarImagen(imagen);
                }
                clienteBL.GuardarCliente(cliente);

                return RedirectToAction("Index");
            }
            return View(cliente);
        } 

        public ActionResult Edit(int id)
        {
            var cliente = clienteBL.ObtenerCliente(id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Edit(Cliente cliente, HttpPostedFileBase imagen)
        {
            if (imagen != null)
            {
                cliente.UrlImagen = GuardarImagen(imagen);
            }
            clienteBL.GuardarCliente(cliente);

            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            var clientesBL = new ClientesBL();
            var cliente = clientesBL.ObtenerCliente(id);
            return View(cliente);
        }

        [HttpPost]
        public ActionResult Delete(Cliente cliente)
        {
            var clientesBL = new ClientesBL();
            clientesBL.Eliminar(cliente);

            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            var cliente = clienteBL.ObtenerCliente(id);
            return View(cliente);
        }
        private string GuardarImagen(HttpPostedFileBase imagen)
        {
            string path = Server.MapPath("~/Imagenes/" + imagen.FileName);
            imagen.SaveAs(path);

            return "/Imagenes/" + imagen.FileName;
        }
    }
}