using Merkur.BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Merkur.win
{
    public partial class Clientes : Form
    {
        ClientesBL _clientesBL;
        public Clientes()
        {

            _clientesBL = new ClientesBL();
            
            InitializeComponent();
            listadeClientesBindingSource.DataSource = _clientesBL.ObtenerClientes();            
        }

      
        private void listadeClientesBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            var nuevoCliente = new Cliente();


            listadeClientesBindingSource.EndEdit();
            var clientes = (Cliente)listadeClientesBindingSource.Current;
            var resultado = _clientesBL.GuardarClientes(clientes);

            if (fotoPictureBox.Image != null)
            {
                clientes.Foto = Program.imageToByArray(fotoPictureBox.Image);
            }
            else
            {
                clientes.Foto = null;
            }
            var resultado3 = _clientesBL.GuardarClientes(clientes);

            if (resultado3.Exitoso == true)
            {
                listadeClientesBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Cliente Guardado!");
            }
            else
            {
                MessageBox.Show(resultado3.Mensaje);
            }
        }      

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _clientesBL.AgregarClientes();
            listadeClientesBindingSource.MoveLast();
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
        private void toolStripButtoncancelar_Click(object sender, EventArgs e)
        {
            DeshabilitarHabilitarBotones(true);
            Eliminar(0);
        }
        private void Eliminar(int id)
        {
            var resultado3 = _clientesBL.EliminarClientes(id);
            if (resultado3 == true)
            {
                listadeClientesBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar al Cliente");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            var id = int.Parse(idTextBox.Text);
            var Cedula = cedulaTextBox.Text;
            var Nombres = nombresTextBox.Text;
            var Apellidos = apellidosTextBox.Text;
            var Email = emailTextBox.Text;
            var Telefono = telefonoTextBox.Text;                      
            _clientesBL.Actualizar(id, Cedula, Nombres, Apellidos, Email, Telefono);
            MessageBox.Show("Datos Actualizados");
        }

        private void button2_Click(object sender, EventArgs e)
        {

            var clientes = (Cliente)listadeClientesBindingSource.Current;
            if (clientes != null)
            {
                openFileDialog1.ShowDialog();
                var archivo = openFileDialog1.FileName;

                if (archivo != "")
                {
                    var fileInfo = new FileInfo(archivo);
                    var fileStream = fileInfo.OpenRead();

                    fotoPictureBox.Image = Image.FromStream(fileStream);
                }
            }
            else
            {
                MessageBox.Show("Cree un cliente primero!");
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            
            listadeClientesBindingSource.DataSource = _clientesBL.ObtenerClientes();
            
        }

        

        private void button4_Click(object sender, EventArgs e)
        {
            fotoPictureBox.Image = null;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form webcam = new WEBCAM();
            webcam.Show();
        }

        private void listadeClientesDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            listadeClientesBindingSource.DataSource = Visible;
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            var Buscar = txtBuscar.Text;

            listadeClientesBindingSource.DataSource = _clientesBL.ObtenerClientes(Buscar);
            listadeClientesBindingSource.ResetBindings(false);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {

        }

        private void fotoPictureBox_Click(object sender, EventArgs e)
        {

        }
    }



    
}
