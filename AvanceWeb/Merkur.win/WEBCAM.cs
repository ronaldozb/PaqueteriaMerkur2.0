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
    public partial class WEBCAM : Form
    {
        public WEBCAM()
        {
            InitializeComponent();
            webCam1.Start();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            webCam1.Start();
            webCam1.Start();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webCam1.Stop();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = webCam1.Imagen;
            saveFileDialog1.ShowDialog();
            pictureBox1.Image.Save(saveFileDialog1.FileName);
        }

    
    }
}
