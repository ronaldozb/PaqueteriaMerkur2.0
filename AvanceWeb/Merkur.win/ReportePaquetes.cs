using Merkur.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Merkur.win
{
    public partial class ReportePaquetes : Form
    {
        public ReportePaquetes()
        {
            InitializeComponent();

            var _productoBL = new ProductosBL();
            var bindingsource = new BindingSource();
            bindingsource.DataSource = _productoBL.Obtenerproductos();

            var reporte = new ReporteProductos();
            reporte.SetDataSource(bindingsource);

            crystalReportViewer1.ReportSource = reporte;
            crystalReportViewer1.Refresh();
        }
    }
}
