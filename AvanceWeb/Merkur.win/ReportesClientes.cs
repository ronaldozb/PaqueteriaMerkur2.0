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
    public partial class ReportesClientes : Form
    {
        public ReportesClientes()
        {
            InitializeComponent();
            var _clienteBL = new ClientesBL();
            var bindingsource = new BindingSource();
            bindingsource.DataSource = _clienteBL.ObtenerClientes();

            var reporte = new ReporteClientes();
            reporte.SetDataSource(bindingsource);

            crystalReportViewer1.ReportSource = reporte;
            crystalReportViewer1.Refresh();
        }
    }
}
