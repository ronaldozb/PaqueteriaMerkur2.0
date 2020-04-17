 using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Merkur.BL;

namespace Merkur.win
{
    public partial class Frmlogin : Form
    {
        SeguridadBL _seguridadBL;
        public Frmlogin()
        {
            InitializeComponent();
            _seguridadBL = new SeguridadBL();
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg,
        int wparam, int lparam);

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Frmlogin_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string usuario;
            string contrasena;

            usuario = textBox1.Text;
            contrasena = textBox2.Text;

            button1.Enabled = false;
            button1.Text = "Verificando...";
            Application.DoEvents();

            var UsuarioDB = _seguridadBL.Autenticar(usuario, contrasena);

            if (UsuarioDB !=null )
            {
                Program.UsuarioLogueado = UsuarioDB;
                this.Close();
            }
            else
            {
                label2.Visible = true;
                textBox1.Clear();
                textBox2.Clear();
                if (textBox2.Text == "")
                {
                    textBox2.Text = "CONTRASEÑA";
                    textBox2.ForeColor = Color.LightGray;
                    textBox2.UseSystemPasswordChar = false;
                }

                textBox1.Focus();
                button1.Enabled = true;
                button1.Text = "ACCEDER";
                Application.DoEvents();
            }

        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "USUARIO")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.LightGray;
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "USUARIO";
                textBox1.ForeColor = Color.LightGray;
            }
        }

        private void textBox2_Enter(object sender, EventArgs e)
        {
            if (textBox2.Text == "CONTRASEÑA")
            {
                textBox2.Text = "";
                textBox2.ForeColor = Color.LightGray;
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void textBox2_Leave(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            {
                textBox2.Text = "CONTRASEÑA";
                textBox2.ForeColor = Color.LightGray;
                textBox2.UseSystemPasswordChar = false;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar== Convert.ToChar(Keys.Enter)
                && !string.IsNullOrEmpty(textBox1.Text))
            {
                textBox2.Focus();
            }

        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter)
    && !string.IsNullOrEmpty(textBox2.Text))
            {
                button1.PerformClick();
              //this.Close();
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}