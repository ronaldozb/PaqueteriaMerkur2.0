using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merkur.BL
{
    public class OrdenEnvios
    { 
     public int Id { get; set; }
    public int ClienteId { get; set; }
    public Cliente Cliente { get; set; }
    public DateTime Fecha { get; set; }
    public double Total { get; set; }
    public bool Activo { get; set; }

    public List<OrdenEnviosDetalle> ListadeOrdenEnviosDetalle { get; set; }

    public OrdenEnvios()
    {
        Activo = true;
        Fecha = DateTime.Now;

        ListadeOrdenEnviosDetalle = new List<OrdenEnviosDetalle>();
    }
}

public class OrdenEnviosDetalle
{
    public int Id { get; set; }
    public int OrdenId { get; set; }
    public OrdenEnvios OrdenEnvios { get; set; }

    public int ProductoId { get; set; }
    public Producto Producto { get; set; }

    public int Cantidad { get; set; }
    public double Precio { get; set; }
    public double Total { get; set; }
}
    
}
