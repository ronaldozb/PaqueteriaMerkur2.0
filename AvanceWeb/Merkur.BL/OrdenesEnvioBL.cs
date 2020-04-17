using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merkur.BL
{
    public class OrdenesEnvioBL
    {
        Contexto _contexto;
        public List<OrdenEnvios> ListadeOrdenes { get; set; }

        public OrdenesEnvioBL()
        {
            _contexto = new Contexto();
            ListadeOrdenes = new List<OrdenEnvios>();
        }

        public List<OrdenEnvios> ObtenerOrdenes()
        {
            ListadeOrdenes = _contexto.Ordenes
                .Include("Cliente")
                .ToList();

            return ListadeOrdenes;
        }
       
        public List<OrdenEnviosDetalle> ObtenerOrdenDetalle(int ordenId)
        {
            var listadeOrdenesDetalle = _contexto.OrdenDetalle
                .Include("Producto")
                .Where(o => o.OrdenId == ordenId).ToList();

            return listadeOrdenesDetalle;
        }

        public OrdenEnviosDetalle ObtenerOrdenDetallePorId(int id)
        {
            var ordenDetalle = _contexto.OrdenDetalle
                .Include("Producto").FirstOrDefault(p => p.Id == id);

            return ordenDetalle;
        }

        public OrdenEnvios ObtenerOrden(int id)
        {
            var orden = _contexto.Ordenes
                .Include("Cliente").FirstOrDefault(p => p.Id == id);

            return orden;
        }

        public void GuardarOrden(OrdenEnvios orden)
        {
            if (orden.Id == 0)
            {
                _contexto.Ordenes.Add(orden);
            }
            else
            {
                var ordenExistente = _contexto.Ordenes.Find(orden.Id);
                ordenExistente.ClienteId = orden.ClienteId;
                ordenExistente.Activo = orden.Activo;
            }

            _contexto.SaveChanges();
        }

        public void GuardarOrdenDetalle(OrdenEnviosDetalle ordenDetalle)
        {
            var producto = _contexto.Productos.Find(ordenDetalle.ProductoId);

            ordenDetalle.Precio = producto.Precio;
            ordenDetalle.Total = ordenDetalle.Cantidad * ordenDetalle.Precio;

            _contexto.OrdenDetalle.Add(ordenDetalle);

            var orden = _contexto.Ordenes.Find(ordenDetalle.OrdenId);
            orden.Total = orden.Total + ordenDetalle.Total;

            _contexto.SaveChanges();
        }

        public void EliminarOrdenDetalle(int id)
        {
            var ordenDetalle = _contexto.OrdenDetalle.Find(id);
            _contexto.OrdenDetalle.Remove(ordenDetalle);

            var orden = _contexto.Ordenes.Find(ordenDetalle.OrdenId);
            orden.Total = orden.Total - ordenDetalle.Total;

            _contexto.SaveChanges();
        }
    }
}
