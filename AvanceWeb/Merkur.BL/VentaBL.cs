using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merkur.BL
{
    public class VentaBL
    {
        Contexto _contexto;
        public BindingList<venta> Listadeventa { get; set; }

        public VentaBL()
        {
            _contexto = new Contexto();
            Listadeventa = new BindingList<venta>();
        }

        public BindingList<venta> Obtenerventa()
        {
            _contexto.Ventas.Load();
            Listadeventa = _contexto.Ventas.Local.ToBindingList();

            return Listadeventa;
        }

        public venta Obtenerventas(int id)
        {
            var venta = _contexto.Ventas.Find(id);

            return venta;
        }

        public void GuardarVenta(venta ventas)
        {
            if (ventas.Id == 0)
            {
                _contexto.Ventas.Add(ventas);
            }
            else
            {
                var ventaExistente = _contexto.Cliente.Find(ventas.Id);
                ventaExistente.Nombres = ventas.Descripcion;
              
                //ventaExistente.UrlImagen = ventas.UrlImagen;
                //if (ventas.UrlImagen != null)
                //{
                //    ventaExistente.UrlImagen = ventas.UrlImagen;
                //}
            }

            _contexto.SaveChanges();
        }

        public void EliminarVenta (venta venta)
        {
            var ventaExistente = _contexto.Ventas.Find(venta.Id);
            _contexto.Ventas.Remove(ventaExistente);
            _contexto.SaveChanges();
        }



    }

    public class venta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }

        //[Display(Name = "Imagen")]
        //public string UrlImagen { get; set; }
    }
}
