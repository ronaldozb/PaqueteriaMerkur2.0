namespace Merkur.win
{
    partial class Reporte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.listaDeEnviosPorPaquetesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.listaDeEnviosPorPaquetesDataGridView = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.listaDeEnviosPorPaquetesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaDeEnviosPorPaquetesDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // listaDeEnviosPorPaquetesBindingSource
            // 
            this.listaDeEnviosPorPaquetesBindingSource.DataSource = typeof(Merkur.BL.ReporteEnvioPorPaquetes);
            // 
            // listaDeEnviosPorPaquetesDataGridView
            // 
            this.listaDeEnviosPorPaquetesDataGridView.AutoGenerateColumns = false;
            this.listaDeEnviosPorPaquetesDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.listaDeEnviosPorPaquetesDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3});
            this.listaDeEnviosPorPaquetesDataGridView.DataSource = this.listaDeEnviosPorPaquetesBindingSource;
            this.listaDeEnviosPorPaquetesDataGridView.Location = new System.Drawing.Point(12, 80);
            this.listaDeEnviosPorPaquetesDataGridView.Name = "listaDeEnviosPorPaquetesDataGridView";
            this.listaDeEnviosPorPaquetesDataGridView.Size = new System.Drawing.Size(300, 220);
            this.listaDeEnviosPorPaquetesDataGridView.TabIndex = 1;
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "Paquete";
            this.dataGridViewTextBoxColumn1.HeaderText = "Paquete";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "Cantidad";
            this.dataGridViewTextBoxColumn2.HeaderText = "Cantidad";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Total";
            this.dataGridViewTextBoxColumn3.HeaderText = "Total";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(224, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Refresh";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Reporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(331, 315);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.listaDeEnviosPorPaquetesDataGridView);
            this.Name = "Reporte";
            this.Text = "Reporte";
            ((System.ComponentModel.ISupportInitialize)(this.listaDeEnviosPorPaquetesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.listaDeEnviosPorPaquetesDataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource listaDeEnviosPorPaquetesBindingSource;
        private System.Windows.Forms.DataGridView listaDeEnviosPorPaquetesDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.Button button1;
    }
}