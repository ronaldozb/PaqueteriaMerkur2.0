using Merkur.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Merkur.BL
{
    public class ProductosBL
    {
        Contexto _contexto;
        public BindingList<Producto> ListadeProductos { get; set; }
        
        public ProductosBL()
        {
            _contexto = new Contexto();
            ListadeProductos = new BindingList<Producto>();
                       
        }
        public BindingList<Producto> Obtenerproductos()
        {
            _contexto.Productos.Load();
            var query = _contexto.Productos
                .Include("Categorias")
                .OrderBy(r=>r.Categorias.Descripcion)
                .ThenBy(r=>r.Descripcion)
                .ToList();
            var resultado = new BindingList<Producto>(query);
            return resultado; 
        }

        public BindingList<Producto> ObtenerproductosActivos()
        {
            _contexto.Productos.Load();
            var query = _contexto.Productos
            .Include("Categorias").Where(r=> r.Activo==true)
            .OrderBy(r=>r.Descripcion)
            .ToList();
            var resultado = new BindingList<Producto>(query);

            return resultado;

           
        }

        public BindingList<Producto> Obtenerproductos(int buscar)
        {
            var query = _contexto.Productos
                .Where(p => p.Id == buscar).ToList();

            var resultado = new BindingList<Producto>(query);

            return resultado;
        }
        public Producto Obtenerproducto (int id)
        {
            var resultado = _contexto.Productos.FirstOrDefault(p => p.Id == id);
            return resultado;
        }
        public Resultado GuardarProductos(Producto producto)
        {
            var resultado = Validar(producto); 
            if (resultado.Exitoso == false )
            {

                return resultado;
            }
            if(producto.Id==0)
            {
                _contexto.Productos.Add(producto);
            }
            else
            {
                var productoExitente = _contexto.Productos.Find(producto.Id);
                productoExitente.Descripcion = producto.Descripcion;
                productoExitente.Destino = producto.Destino;
                productoExitente.Activo = producto.Activo; 
            }

            _contexto.Productos.Add(producto);
            _contexto.SaveChanges();

            resultado.Exitoso = true;            
            return resultado;
        }
        public void AgregarProducto()
        {
            var nuevoProducto = new Producto();

            ListadeProductos.Add(nuevoProducto);
            
        }
     
        public bool EliminarProducto(int id)
        {
            foreach (var producto in ListadeProductos)
            {
                if (producto.Id == id)
                {
                    ListadeProductos.Remove(producto);
                    _contexto.SaveChanges();
                    return true;
                }
               
            }
            return false;
        }
        public void Eliminar (int id)
        {
            var producto = _contexto.Productos.Find(id);
            _contexto.Productos.Remove(producto);
            _contexto.SaveChanges();
        }

        private Resultado Validar (Producto producto)
        {
            var resultado = new Resultado();
            resultado.Exitoso = true;

            if (producto == null)
            {
                resultado.Mensaje = "Agregue un producto valido";
                resultado.Exitoso = false;

                return resultado; 
            }

            if(string.IsNullOrEmpty(producto.Descripcion) == true)
            {
                resultado.Mensaje = "Ingrese una Descripcion";
                resultado.Exitoso = false;
            }

            if (producto.Id<0)
            {
                resultado.Mensaje = "el Id debe ser mayor que 0";
                resultado.Exitoso = false;
            }
            if (producto.Precio < 0)
            {
                resultado.Mensaje = "Agregue un precio";
                resultado.Exitoso = false;
            }


            return resultado;
        }

        public void Actualizar(int id, string descripcion)
        {
            var productoExistente = _contexto.Productos.Find(id);

            productoExistente.Descripcion = descripcion;

            _contexto.SaveChanges();
        }
        public List<Producto> Obtener()
        {
            return _contexto.Productos.ToList();
        }

    }
    public class Producto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Destino { get; set; }
        public int CategoriaId { get; set; }
        public Categoria Categorias { get; set; }
        public int TipoId { get; set; }
        public Tipo Tipo { get; set; }
        public DateTime FechadeEntrega { get; set; }
        public double Precio { get; set; }
        [Display(Name = "Imagen")]
        public string UrlImagen { get; set; }
        public bool Activo { get; set; }
        public string Longitud1 { get; set; }
        public string Longitud2 { get; set; }
        public string Latitud1 { get; set; }
        public string Latitud2 { get; set; }

        public Producto()
        {
            Activo = true;

        }
    }
    public class Resultado
    {
        public bool Exitoso { get; set; }
        public string Mensaje { get; set; }
    }

}
