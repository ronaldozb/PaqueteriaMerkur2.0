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
    public partial class FormUsuario : Form
    {
        SeguridadBL _seguridadBL;

        public FormUsuario()
        {
            InitializeComponent();
            _seguridadBL = new SeguridadBL();

            listaUsuariosBindingSource.DataSource =
                _seguridadBL.ObtenerUsuarios();
        }

        private void FormUsuario_Load(object sender, EventArgs e)
        {

        }

        private void bindingNavigatorAddNewItem_Click(object sender, EventArgs e)
        {
            _seguridadBL.AgregarUsuario();
             listaUsuariosBindingSource.MoveLast();

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
            toolStripLabel1.Visible = !valor;
        }

        private void listaUsuariosBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            listaUsuariosBindingSource.EndEdit();
            var usuario = (Usuarios)listaUsuariosBindingSource.Current;

            var resultado = _seguridadBL.GuardarUsuario(usuario);

            if (resultado.Exitoso == true)
            {
                listaUsuariosBindingSource.ResetBindings(false);
                DeshabilitarHabilitarBotones(true);
                MessageBox.Show("Usuario guardado");
            }
            else
            {
                MessageBox.Show(resultado.Mensaje);
            }
        }
        private void Eliminar(int id)
        {
            var resultado = _seguridadBL.EliminarUsuario(id);

            if (resultado == true)
            {
                listaUsuariosBindingSource.ResetBindings(false);
            }
            else
            {
                MessageBox.Show("Ocurrio un error al eliminar el usuario");
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            _seguridadBL.CancelarCambios();
            DeshabilitarHabilitarBotones(true);
        }

        
    }
}
