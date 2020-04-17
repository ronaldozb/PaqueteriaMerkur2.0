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
    public partial class RpteFactura : Form
    {
        public RpteFactura()
        {
            InitializeComponent();
            var _facturasBL = new FacturaBL();
            var bindingsource = new BindingSource();
            bindingsource.DataSource = _facturasBL.ObtenerFacturas();

            var reporte = new ReporteFactura();
            reporte.SetDataSource(bindingsource);

            crystalReportViewer1.ReportSource = reporte;
            crystalReportViewer1.Refresh();
        }
    }
}
