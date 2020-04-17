using Merkur.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merkur.BL
{
    public class FacturaBL
    {
        Contexto _contexto;

        public BindingList<facturas1> ListadeFacturas { get; set; }

        public FacturaBL()
        {
            _contexto = new Contexto();
        }

        public BindingList<facturas1> ObtenerFacturas()
        {
            _contexto.Facturas.Include("FacturaDetalle").Load();
            ListadeFacturas = _contexto.Facturas.Local.ToBindingList();

            return ListadeFacturas;
        }
        public BindingList<facturas1> ObtenerFacturas(string Buscar)
        {
            var query = _contexto.Facturas.Where(p => p.Destino.ToLower()
                       .Contains(Buscar.ToLower()) == true).ToList();

            var resultado = new BindingList<facturas1>(query);

            return resultado;
        }

        public void AgregarFactura()
        {
            var nuevaFactura = new facturas1();
            _contexto.Facturas.Add(nuevaFactura);
        }

        public void AgregarFacturaDetalle(facturas1 factura)
        {
            if (factura != null)
            {
                var nuevaDetalle = new FacturasDetalle();
                factura.FacturaDetalle.Add(nuevaDetalle);
            }
        }
        public void RemoverFacturaDetalle(facturas1 factura, FacturasDetalle facturaDetalle)
        {
            if (factura != null && facturaDetalle != null)
            {
                factura.FacturaDetalle.Remove(facturaDetalle);
            }
        }

        public void CancelarCambios()
        {
            foreach (var item in _contexto.ChangeTracker.Entries())
            {
                item.State = EntityState.Unchanged;
                item.Reload();

            }
        }

        public Resultado3 GuardarFactura(facturas1 factura)
        {
            var resultado3 = Validar(factura);
            if (resultado3.Exitoso == false)
            {
                return resultado3;
            }
            _contexto.SaveChanges();
            resultado3.Exitoso = true;
            return resultado3;
        }

        private Resultado3 Validar(facturas1 factura)
        {
            var resultado3 = new Resultado3();
            resultado3.Exitoso = true;
            if (factura == null)
            {
                resultado3.Mensaje = "Agregue un Factura Nueva!";
                resultado3.Exitoso = false;

                return resultado3;
            }
            if (factura.Id != 0 && factura.Activo == true)
            {
                resultado3.Mensaje = "La factura ya fue emitida y no se pueden realizar cambios en ella!";
                resultado3.Exitoso = false;
            }

            if (factura.Activo == false)
            {
                resultado3.Mensaje = "La factura esta Anula y No se Puede Modificar!";
                resultado3.Exitoso = false;
            }
            if (factura.ClienteId == 0)
            {
                resultado3.Mensaje = "Seleccione un Cliente";
                resultado3.Exitoso = false;
            }
           
           
            if (factura.FacturaDetalle.Count == 0)
            {
                resultado3.Mensaje = "Agregue producto a la factura";
                resultado3.Exitoso = false;
            }
            foreach (var detalle in factura.FacturaDetalle)
            {
                if (detalle.ProductoId == 0)
                {
                    resultado3.Mensaje = "Seleccione producto validados";
                    resultado3.Exitoso = false;
                }
            }

            return resultado3;
        }

        public void CalcularFactura(facturas1 factura)
        {
            if (factura != null)
            {
                double Subtotal = 0;

                foreach (var detalle in factura.FacturaDetalle)
                {
                    var producto = _contexto.Productos.Find(detalle.ProductoId);

                    if (producto != null)
                    {
                        detalle.Precio = producto.Precio;
                        detalle.Total = detalle.Paquetes * producto.Precio;

                        Subtotal += detalle.Total;
                    }
                }
                factura.SubTotal = Subtotal;
                factura.IVS = Subtotal * 0.15;
                factura.Total = Subtotal + factura.IVS;
            }

        }

        public bool AnularFactura(int id)
        {
            foreach (var factura in ListadeFacturas)
            {
                if (factura.Id == id)
                {
                    factura.Activo = false;
                    _contexto.SaveChanges();
                    return true;
                }
            }

            return false;
        }

       

    }

    public class facturas1
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public DateTime EntradaPaquete { get; set; }
        public string Direccion { get; set; }
        public string Destino { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        
        public string Producto { get; set; }
        public Producto ProductoId { get; set; }

        public BindingList<FacturasDetalle> FacturaDetalle { get; set; }
        public double SubTotal { get; set; }
        public double IVS { get; set; }
        public double Total { get; set; }
        public bool Activo { get; set; }

        public facturas1()
        {
            Fecha = DateTime.Now;
            EntradaPaquete = DateTime.Now;
            FacturaDetalle = new BindingList<FacturasDetalle>();
            Activo = true;
        }
      }

        public class FacturasDetalle
        {
          public int Id { get; set; }
          public int ProductoId { get; set; }
        public Producto Producto { get; set; }
        
        public int Paquetes { get; set; }
        public double Precio { get; set; }
        public double Total { get; set; }

        public FacturasDetalle()
        {
            Paquetes = 1;
        }


    }



    
}
