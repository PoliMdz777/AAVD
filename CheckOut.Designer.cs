namespace Pantallas_alto_volumen_de_datos
{
    partial class CheckOut
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
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnbuscar = new System.Windows.Forms.Button();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.CLBServicios = new System.Windows.Forms.CheckedListBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnCalcularSA = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTotalServicios = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.cbHotel = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtNameHotel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dgvDetalles2 = new System.Windows.Forms.DataGridView();
            this.txtFormaPago = new System.Windows.Forms.ComboBox();
            this.btnGenerarFacturaPDF = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmar.Location = new System.Drawing.Point(809, 702);
            this.btnConfirmar.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(213, 78);
            this.btnConfirmar.TabIndex = 123;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click);
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNombreCliente.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreCliente.Location = new System.Drawing.Point(62, 227);
            this.txtNombreCliente.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtNombreCliente.Multiline = true;
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.ReadOnly = true;
            this.txtNombreCliente.Size = new System.Drawing.Size(473, 40);
            this.txtNombreCliente.TabIndex = 118;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Lucida Fax", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(59, 190);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 19);
            this.label5.TabIndex = 117;
            this.label5.Text = "RFC del cliente";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Lucida Fax", 19.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(70, 21);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(194, 40);
            this.label11.TabIndex = 116;
            this.label11.Text = "Check Out";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // btnbuscar
            // 
            this.btnbuscar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnbuscar.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnbuscar.Location = new System.Drawing.Point(1150, 103);
            this.btnbuscar.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnbuscar.Name = "btnbuscar";
            this.btnbuscar.Size = new System.Drawing.Size(161, 40);
            this.btnbuscar.TabIndex = 115;
            this.btnbuscar.Text = "Buscar";
            this.btnbuscar.UseVisualStyleBackColor = false;
            this.btnbuscar.Click += new System.EventHandler(this.btnbuscar_Click);
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCodigo.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(429, 112);
            this.txtCodigo.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtCodigo.Multiline = true;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(494, 40);
            this.txtCodigo.TabIndex = 114;
            this.txtCodigo.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Fax", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(425, 76);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 19);
            this.label2.TabIndex = 113;
            this.label2.Text = "Reserva:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // CLBServicios
            // 
            this.CLBServicios.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.CLBServicios.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CLBServicios.FormattingEnabled = true;
            this.CLBServicios.Location = new System.Drawing.Point(62, 540);
            this.CLBServicios.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.CLBServicios.Name = "CLBServicios";
            this.CLBServicios.Size = new System.Drawing.Size(712, 234);
            this.CLBServicios.TabIndex = 129;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Lucida Fax", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(59, 517);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(297, 19);
            this.label7.TabIndex = 128;
            this.label7.Text = "Servicios Adicionales utilizados:";
            // 
            // btnCalcularSA
            // 
            this.btnCalcularSA.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCalcularSA.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalcularSA.Location = new System.Drawing.Point(1065, 597);
            this.btnCalcularSA.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnCalcularSA.Name = "btnCalcularSA";
            this.btnCalcularSA.Size = new System.Drawing.Size(161, 40);
            this.btnCalcularSA.TabIndex = 130;
            this.btnCalcularSA.Text = "Calcular SA";
            this.btnCalcularSA.UseVisualStyleBackColor = false;
            this.btnCalcularSA.Click += new System.EventHandler(this.btnCalcularSA_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Fax", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(805, 519);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(280, 19);
            this.label4.TabIndex = 131;
            this.label4.Text = "Total de servicios adicionales:";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // txtTotalServicios
            // 
            this.txtTotalServicios.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotalServicios.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalServicios.Location = new System.Drawing.Point(1075, 514);
            this.txtTotalServicios.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtTotalServicios.Multiline = true;
            this.txtTotalServicios.Name = "txtTotalServicios";
            this.txtTotalServicios.ReadOnly = true;
            this.txtTotalServicios.Size = new System.Drawing.Size(151, 40);
            this.txtTotalServicios.TabIndex = 132;
            this.txtTotalServicios.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Lucida Fax", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(805, 584);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(148, 19);
            this.label15.TabIndex = 135;
            this.label15.Text = "Forma de pago:";
            this.label15.Click += new System.EventHandler(this.label15_Click);
            // 
            // cbHotel
            // 
            this.cbHotel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cbHotel.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHotel.FormattingEnabled = true;
            this.cbHotel.Items.AddRange(new object[] {
            "Codigo de reservación",
            "RFC",
            "Nombre"});
            this.cbHotel.Location = new System.Drawing.Point(76, 112);
            this.cbHotel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.cbHotel.Name = "cbHotel";
            this.cbHotel.Size = new System.Drawing.Size(220, 24);
            this.cbHotel.TabIndex = 138;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 81);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 19);
            this.label1.TabIndex = 137;
            this.label1.Text = "Forma de busqueda:";
            // 
            // txtNameHotel
            // 
            this.txtNameHotel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNameHotel.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNameHotel.Location = new System.Drawing.Point(595, 227);
            this.txtNameHotel.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtNameHotel.Multiline = true;
            this.txtNameHotel.Name = "txtNameHotel";
            this.txtNameHotel.ReadOnly = true;
            this.txtNameHotel.Size = new System.Drawing.Size(473, 40);
            this.txtNameHotel.TabIndex = 140;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Fax", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(592, 190);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 19);
            this.label3.TabIndex = 139;
            this.label3.Text = "Nombre del Hotel:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Lucida Fax", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(60, 307);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(237, 19);
            this.label6.TabIndex = 142;
            this.label6.Text = "Habitaciones reservadas:";
            // 
            // dgvDetalles2
            // 
            this.dgvDetalles2.AllowUserToAddRows = false;
            this.dgvDetalles2.AllowUserToDeleteRows = false;
            this.dgvDetalles2.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvDetalles2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalles2.Location = new System.Drawing.Point(63, 328);
            this.dgvDetalles2.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.dgvDetalles2.Name = "dgvDetalles2";
            this.dgvDetalles2.ReadOnly = true;
            this.dgvDetalles2.RowHeadersWidth = 51;
            this.dgvDetalles2.RowTemplate.Height = 24;
            this.dgvDetalles2.Size = new System.Drawing.Size(1453, 146);
            this.dgvDetalles2.TabIndex = 141;
            // 
            // txtFormaPago
            // 
            this.txtFormaPago.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtFormaPago.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFormaPago.FormattingEnabled = true;
            this.txtFormaPago.Items.AddRange(new object[] {
            "Efectivo",
            "Credito",
            "Debito"});
            this.txtFormaPago.Location = new System.Drawing.Point(809, 613);
            this.txtFormaPago.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.txtFormaPago.Name = "txtFormaPago";
            this.txtFormaPago.Size = new System.Drawing.Size(220, 24);
            this.txtFormaPago.TabIndex = 143;
            // 
            // btnGenerarFacturaPDF
            // 
            this.btnGenerarFacturaPDF.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnGenerarFacturaPDF.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGenerarFacturaPDF.Location = new System.Drawing.Point(1065, 720);
            this.btnGenerarFacturaPDF.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.btnGenerarFacturaPDF.Name = "btnGenerarFacturaPDF";
            this.btnGenerarFacturaPDF.Size = new System.Drawing.Size(161, 40);
            this.btnGenerarFacturaPDF.TabIndex = 144;
            this.btnGenerarFacturaPDF.Text = "Generar PDF";
            this.btnGenerarFacturaPDF.UseVisualStyleBackColor = false;
            this.btnGenerarFacturaPDF.Visible = false;
            // 
            // CheckOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(1581, 1015);
            this.Controls.Add(this.btnGenerarFacturaPDF);
            this.Controls.Add(this.txtFormaPago);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgvDetalles2);
            this.Controls.Add(this.txtNameHotel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbHotel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.txtTotalServicios);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCalcularSA);
            this.Controls.Add(this.CLBServicios);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnConfirmar);
            this.Controls.Add(this.txtNombreCliente);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.btnbuscar);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Lucida Fax", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 2, 4, 2);
            this.Name = "CheckOut";
            this.Text = "CheckOut";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnbuscar;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox CLBServicios;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnCalcularSA;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtTotalServicios;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cbHotel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtNameHotel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DataGridView dgvDetalles2;
        private System.Windows.Forms.ComboBox txtFormaPago;
        private System.Windows.Forms.Button btnGenerarFacturaPDF;
    }
}