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
    public partial class Facturas : Form
    {
        FacturaBL _facturaBL;
        ClientesBL _clientesBL;
        ProductosBL _productoBL;

               
        public Facturas()
        {
            InitializeComponent();
            _facturaBL = new FacturaBL();
            listadeFacturasBindingSource.DataSource = _facturaBL.ObtenerFacturas();

            _clientesBL = new ClientesBL();
            listadeClientesBindingSource.DataSource = _clientesBL.ObtenerClientes();

            _productoBL = new ProductosBL();
            listadeProductosBindingSource.DataSource = _productoBL.Obtenerproductos();

           

            

            
        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _facturaBL.AgregarFactura();
            listadeFacturasBindingSource.MoveLast();

            DeshabilitarHabilitarBotones(false);
        }

        private void DeshabilitarHabilitarBotones(bool valor)
        {
            bindingNavigatorMoveFirstItem.Enabled = valor;
            bindingNavigatorMoveLastItem.Enabled = valor;
            bindingNavigatorMovePreviousItem.Enabled = valor;
            bindingNavigatorMoveNextItem.Enabled = valor;
            bindingNavigatorPositionItem.Enabled = valor;
            bindingNavigatorAddNewItem.Enabled = valor;
            bindingNavigatorDeleteItem.Enabled = valor;
            toolStripButtoncancelar.Visible = !valor;
        }

        private void listadeFacturasBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listadeFacturasBindingSource.EndEdit();

            var factura = (facturas1)listadeFacturasBindingSource.Current;
            var resultado3 = _facturaBL.GuardarFactura(factura);

            if (resultado3.Exitoso == true)
            {
                listadeClientesBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Factura Generada");

            }
            else
            {
                MessageBox.Show(resultado3.Mensaje);
            }
        }

        private void toolStripButtoncancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarHabilitarBotones(true);
            _facturaBL.CancelarCambios();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var factura = (facturas1)listadeFacturasBindingSource.Current;
            _facturaBL.AgregarFacturaDetalle(factura);

            DeshabilitarHabilitarBotones(false);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var factura = (facturas1)listadeFacturasBindingSource.Current;
            var facturaDetalle = (FacturasDetalle)facturaDetalleBindingSource.Current;

            _facturaBL.RemoverFacturaDetalle(factura, facturaDetalle);

            DeshabilitarHabilitarBotones(false);
        }

        private void facturaDetalleDataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            e.ThrowException = false;
        }

        private void facturaDetalleDataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            var factura = (facturas1)listadeFacturasBindingSource.Current;
            _facturaBL.CalcularFactura(factura);

            listadeFacturasBindingSource.ResetBindings(false);
        }



        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            if (idTextBox.Text != " ")
            {
                var resultado3 = MessageBox.Show("Desea anular esta factura?", "Anular", MessageBoxButtons.YesNo);
                if (resultado3 == DialogResult.Yes)
                {
                    var id = Convert.ToInt32(idTextBox.Text);
                    anular(id);
                }
            }
        }
        private void anular(int id)
        {
            var resultado3 = _facturaBL.AnularFactura(id);

            if (resultado3 == true)
            {
                listadeFacturasBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error a anular la factura");
            }
        }

        private void listadeFacturasBindingSource_CurrentChanged(object sender, EventArgs e)
        {

            var factura = (facturas1)listadeFacturasBindingSource.Current;

            if (factura != null && factura.Id != 0 && factura.Activo == false)
            {
                anulado.Visible = true;
            }
            else
            {
                anulado.Visible = false;
            }
     
            
         }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            var Buscar = txtBuscar.Text;

            listadeFacturasBindingSource.DataSource = _facturaBL.ObtenerFacturas(Buscar);
            listadeFacturasBindingSource.ResetBindings(false);
        }

        private void productocomboBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }    
}
