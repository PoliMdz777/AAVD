namespace Pantallas_alto_volumen_de_datos
{
    partial class CheckIn
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
            this.btnBuscarReserva = new System.Windows.Forms.Button();
            this.txtBuscar = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnCheckIn = new System.Windows.Forms.Button();
            this.txtAnticipo = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTotalRestante = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.dgvDetalles2 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtnombreHotel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbHotel = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBuscarReserva
            // 
            this.btnBuscarReserva.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBuscarReserva.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarReserva.Location = new System.Drawing.Point(713, 51);
            this.btnBuscarReserva.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnBuscarReserva.Name = "btnBuscarReserva";
            this.btnBuscarReserva.Size = new System.Drawing.Size(176, 42);
            this.btnBuscarReserva.TabIndex = 68;
            this.btnBuscarReserva.Text = "Buscar";
            this.btnBuscarReserva.UseVisualStyleBackColor = false;
            this.btnBuscarReserva.Click += new System.EventHandler(this.btnBuscarReserva_Click);
            // 
            // txtBuscar
            // 
            this.txtBuscar.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtBuscar.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBuscar.Location = new System.Drawing.Point(377, 102);
            this.txtBuscar.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtBuscar.Multiline = true;
            this.txtBuscar.Name = "txtBuscar";
            this.txtBuscar.Size = new System.Drawing.Size(512, 42);
            this.txtBuscar.TabIndex = 67;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(64, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(148, 15);
            this.label2.TabIndex = 66;
            this.label2.Text = "Forma de busqueda:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Lucida Fax", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(60, 22);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(155, 36);
            this.label11.TabIndex = 69;
            this.label11.Text = "Check In";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNombreCliente.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreCliente.Location = new System.Drawing.Point(68, 321);
            this.txtNombreCliente.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtNombreCliente.Multiline = true;
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.ReadOnly = true;
            this.txtNombreCliente.Size = new System.Drawing.Size(472, 42);
            this.txtNombreCliente.TabIndex = 103;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(64, 285);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 15);
            this.label5.TabIndex = 102;
            this.label5.Text = "RFC del cliente:";
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCheckIn.FlatAppearance.BorderSize = 0;
            this.btnCheckIn.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckIn.Location = new System.Drawing.Point(747, 321);
            this.btnCheckIn.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(232, 82);
            this.btnCheckIn.TabIndex = 108;
            this.btnCheckIn.Text = "Confirmar";
            this.btnCheckIn.UseVisualStyleBackColor = false;
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            // 
            // txtAnticipo
            // 
            this.txtAnticipo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtAnticipo.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnticipo.Location = new System.Drawing.Point(591, 241);
            this.txtAnticipo.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtAnticipo.Multiline = true;
            this.txtAnticipo.Name = "txtAnticipo";
            this.txtAnticipo.ReadOnly = true;
            this.txtAnticipo.Size = new System.Drawing.Size(140, 42);
            this.txtAnticipo.TabIndex = 112;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(593, 206);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(110, 15);
            this.label9.TabIndex = 111;
            this.label9.Text = "Anticipo dado:";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // txtTotalRestante
            // 
            this.txtTotalRestante.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalRestante.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRestante.Location = new System.Drawing.Point(839, 241);
            this.txtTotalRestante.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtTotalRestante.Multiline = true;
            this.txtTotalRestante.Name = "txtTotalRestante";
            this.txtTotalRestante.ReadOnly = true;
            this.txtTotalRestante.Size = new System.Drawing.Size(140, 42);
            this.txtTotalRestante.TabIndex = 110;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(835, 203);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(114, 15);
            this.label8.TabIndex = 109;
            this.label8.Text = "Total Restante:";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // dgvDetalles2
            // 
            this.dgvDetalles2.AllowUserToAddRows = false;
            this.dgvDetalles2.AllowUserToDeleteRows = false;
            this.dgvDetalles2.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvDetalles2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalles2.Location = new System.Drawing.Point(66, 476);
            this.dgvDetalles2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.dgvDetalles2.Name = "dgvDetalles2";
            this.dgvDetalles2.ReadOnly = true;
            this.dgvDetalles2.RowHeadersWidth = 51;
            this.dgvDetalles2.RowTemplate.Height = 24;
            this.dgvDetalles2.Size = new System.Drawing.Size(1156, 252);
            this.dgvDetalles2.TabIndex = 113;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(68, 431);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 15);
            this.label1.TabIndex = 114;
            this.label1.Text = "Habitaciones reservadas:";
            // 
            // txtnombreHotel
            // 
            this.txtnombreHotel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtnombreHotel.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtnombreHotel.Location = new System.Drawing.Point(68, 205);
            this.txtnombreHotel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtnombreHotel.Multiline = true;
            this.txtnombreHotel.Name = "txtnombreHotel";
            this.txtnombreHotel.ReadOnly = true;
            this.txtnombreHotel.Size = new System.Drawing.Size(472, 42);
            this.txtnombreHotel.TabIndex = 115;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(68, 181);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(109, 15);
            this.label3.TabIndex = 116;
            this.label3.Text = "Nombre Hotel:";
            // 
            // cbHotel
            // 
            this.cbHotel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cbHotel.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHotel.FormattingEnabled = true;
            this.cbHotel.Items.AddRange(new object[] {
            "Codigo de reservación",
            "Nombre",
            "RFC"});
            this.cbHotel.Location = new System.Drawing.Point(68, 120);
            this.cbHotel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.cbHotel.Name = "cbHotel";
            this.cbHotel.Size = new System.Drawing.Size(240, 24);
            this.cbHotel.TabIndex = 117;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(373, 78);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 15);
            this.label4.TabIndex = 118;
            this.label4.Text = "Reserva:";
            // 
            // CheckIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1279, 747);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbHotel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtnombreHotel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvDetalles2);
            this.Controls.Add(this.txtAnticipo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtTotalRestante);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnCheckIn);
            this.Controls.Add(this.txtNombreCliente);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnBuscarReserva);
            this.Controls.Add(this.txtBuscar);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Lucida Fax", 10.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "CheckIn";
            this.Text = "CheckIn";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnBuscarReserva;
        private System.Windows.Forms.TextBox txtBuscar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnCheckIn;
        private System.Windows.Forms.TextBox txtAnticipo;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtTotalRestante;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView dgvDetalles2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtnombreHotel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbHotel;
        private System.Windows.Forms.Label label4;
    }
}