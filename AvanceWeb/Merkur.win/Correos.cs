using System;
using System.Windows.Forms;


namespace Merkur.win
{
    public partial class Correos : Form
    {
       
        public Correos()
        {
            InitializeComponent();
        }  

        private void BtnEnviar_Click(object sender, EventArgs e)
        {
        
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();
            mmsg.To.Add(txtReceptor.Text);
            mmsg.Subject = txtAsunto.Text;
            mmsg.SubjectEncoding = System.Text.Encoding.UTF8;
            mmsg.Bcc.Add(txtBBC.Text);
            mmsg.Body = txtMensaje.Text;

            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = true;
            mmsg.From = new System.Net.Mail.MailAddress("merkurlapaquetria@gmail.com");
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();
            cliente.Credentials = new System.Net.NetworkCredential("merkurlapaquetria@gmail.com", "123merkur");
            cliente.Port = 587;
            cliente.EnableSsl = true;
            cliente.Host = "smtp.gmail.com";

            try
            {
                cliente.Send(mmsg);
                MessageBox.Show("Correo enviado con exito");
            }
            catch (Exception)
            {

                MessageBox.Show("correo no enviado");
            }
        }
    }
    
}
