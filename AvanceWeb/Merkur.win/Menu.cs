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

namespace Merkur.win
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
            OcultadorMenu();
        }

        private void OcultadorMenu()
        {
            SubMenuReportes.Visible = false;
        }
        private void hidesubmenureporte()
        {
            if (SubMenuReportes.Visible == true)
                SubMenuReportes.Visible = false;    
        }
        private void showsubmenureporte(Panel submenureporte)
        {
            if (submenureporte.Visible == false)
            {
                hidesubmenureporte();
                submenureporte.Visible = true;
            }
            else
            {
                submenureporte.Visible = false;
            }
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg,
        int wparam, int lparam);

        private void Salir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }


        private void Maximizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            Maximizar.Visible = false;
            Restuarar.Visible = true;
        }

        private void Minimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void Restuarar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            Restuarar.Visible = false;
            Maximizar.Visible = true;
        }

        private void Menu_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void ContenedorMenu_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Menutop_Paint(object sender, PaintEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void Menutop_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void AbrirFormInPanel(object Formhijo)
        {
            if (this.contenedropanel.Controls.Count > 0)
                this.contenedropanel.Controls.RemoveAt(0);
            Form fh = Formhijo as Form;
            fh.TopLevel = false;            
            fh.Dock = DockStyle.Fill;
            this.contenedropanel.Controls.Add(fh);
            this.contenedropanel.Tag = fh;
            fh.Show();
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Clientes());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Facturas());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Rutas());
        }

        private void button4_Click(object sender, EventArgs e)
        {
             showsubmenureporte(SubMenuReportes);
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            var login = new Menu();
            this.Hide();
            login.Show();
            //this.Hide();
            //login.Close();
            //this.Show();
            if (Program.UsuarioLogueado == null)
            {               
                Application.Exit();
            }
            

        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (ContenedorMenu.Width == 210)
            {
                ContenedorMenu.Width = 71;
            }
            else
            {
                ContenedorMenu.Width = 210;
            }
        }


        private void Menu_Load(object sender, EventArgs e)
        {
            var login = new Frmlogin();
            ContenedorMenu.Width = 210;
            login.ShowDialog();
            if (Program.UsuarioLogueado == null)
            {
                Application.Exit();
            }
            if (Program.UsuarioLogueado != null)
            {
                toolStripStatusLabel1.Text =  Program.UsuarioLogueado.Nombre;
                if (Program.UsuarioLogueado.Privilegio == "Usuario")
                {
                    btnReportes.Visible = false;
                    button9.Visible = false;

                    
                }
                else
                {
                    if (Program.UsuarioLogueado.Privilegio == "Usuario1")
                    {
                        btnReportes.Visible = false;
                        button9.Visible = false;
                    }
                }
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Productos());
        }         

        private void button6_Click_1(object sender, EventArgs e)
        {
            AbrirFormInPanel(new ReportePaquetes());
        }

        private void button8_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new RpteFactura());
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new ReportesClientes());
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            AbrirFormInPanel(new Correos());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new MerkurLogo());
        }

        private void button9_Click(object sender, EventArgs e)
        {
            AbrirFormInPanel(new FormUsuario());
        }
    }
}
