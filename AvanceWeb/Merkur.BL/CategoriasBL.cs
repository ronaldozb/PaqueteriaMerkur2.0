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
    public class CategoriasBL
    {
        Contexto _contexto;
        public BindingList<Categoria> ListaCategoria { get; set; }
        public CategoriasBL()
        {
            _contexto = new Contexto();
            ListaCategoria = new BindingList<Categoria>();
        }

        public BindingList<Categoria> ObtenerCategoria()
        {
            _contexto.Categorias.Load();
            ListaCategoria = _contexto.Categorias.Local.ToBindingList();

            return ListaCategoria;

        }
    }
    public class Categoria
    {
        public int Id { get; set; }

        
        public string  Descripcion { get; set; }

    }
}
