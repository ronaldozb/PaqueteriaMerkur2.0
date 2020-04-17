using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Merkur.BL
{
    public class ReporteEnvioPorPaqueteBL
    {

        Contexto _contexto;
        public List<ReporteEnvioPorPaquetes> ListaDeEnviosPorPaquetes { get; set; }

        public ReporteEnvioPorPaqueteBL()
        {
            _contexto = new Contexto();
            ListaDeEnviosPorPaquetes = new List<ReporteEnvioPorPaquetes>();
        }

        public List<ReporteEnvioPorPaquetes> ObtenerEnvioPorPaquetes()
        {
            ListaDeEnviosPorPaquetes = _contexto.OrdenDetalle
                .Include("Productos")
                .Where(r => r.OrdenEnvios.Activo)
                .GroupBy(r => r.Producto.Descripcion)
                .Select(r => new ReporteEnvioPorPaquetes()
                {
                    Producto = r.Key,
                    Cantidad = r.Sum(s => s.Cantidad),
                    Total = r.Sum(s => s.Total)
                }
                ).ToList();
            return ListaDeEnviosPorPaquetes;
        }

    }
}
