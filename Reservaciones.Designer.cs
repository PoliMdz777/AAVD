namespace Pantallas_alto_volumen_de_datos
{
    partial class Reservaciones
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
            this.label11 = new System.Windows.Forms.Label();
            this.HotelesenCiudad = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.txtRFC = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCiudad = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnFiltrarCiudad = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.TiposdHabitacion = new System.Windows.Forms.DataGridView();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtAnticipo = new System.Windows.Forms.TextBox();
            this.dgvDisponibles = new System.Windows.Forms.DataGridView();
            this.label10 = new System.Windows.Forms.Label();
            this.btnConfirmarReservacion = new System.Windows.Forms.Button();
            this.label12 = new System.Windows.Forms.Label();
            this.dtpEntrada = new System.Windows.Forms.DateTimePicker();
            this.dtpSalida = new System.Windows.Forms.DateTimePicker();
            this.label13 = new System.Windows.Forms.Label();
            this.btnBuscarHab = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.txtNombreCliente = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.tbnGuardarHab = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.txtFormaPago = new System.Windows.Forms.ComboBox();
            this.dgvDetalleReservacion = new System.Windows.Forms.DataGridView();
            this.ciudadFiltro = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.HotelesenCiudad)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TiposdHabitacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisponibles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleReservacion)).BeginInit();
            this.SuspendLayout();
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Rockwell", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(13, 9);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(216, 35);
            this.label11.TabIndex = 11;
            this.label11.Text = "Reservaciones";
            // 
            // HotelesenCiudad
            // 
            this.HotelesenCiudad.AllowUserToAddRows = false;
            this.HotelesenCiudad.AllowUserToDeleteRows = false;
            this.HotelesenCiudad.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.HotelesenCiudad.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.HotelesenCiudad.Location = new System.Drawing.Point(20, 194);
            this.HotelesenCiudad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.HotelesenCiudad.Name = "HotelesenCiudad";
            this.HotelesenCiudad.ReadOnly = true;
            this.HotelesenCiudad.RowHeadersWidth = 51;
            this.HotelesenCiudad.RowTemplate.Height = 24;
            this.HotelesenCiudad.Size = new System.Drawing.Size(1268, 180);
            this.HotelesenCiudad.TabIndex = 12;
            this.HotelesenCiudad.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.HotelesenCiudad_CellClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(1309, 249);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "RFC del cliente:";
            // 
            // txtRFC
            // 
            this.txtRFC.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtRFC.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRFC.Location = new System.Drawing.Point(1451, 245);
            this.txtRFC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtRFC.Multiline = true;
            this.txtRFC.Name = "txtRFC";
            this.txtRFC.Size = new System.Drawing.Size(264, 32);
            this.txtRFC.TabIndex = 26;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(102, 16);
            this.label2.TabIndex = 28;
            this.label2.Text = "Ciudad a visitar:";
            // 
            // txtCiudad
            // 
            this.txtCiudad.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtCiudad.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCiudad.Location = new System.Drawing.Point(380, 21);
            this.txtCiudad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCiudad.Multiline = true;
            this.txtCiudad.Name = "txtCiudad";
            this.txtCiudad.Size = new System.Drawing.Size(197, 34);
            this.txtCiudad.TabIndex = 29;
            this.txtCiudad.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(17, 162);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 16);
            this.label3.TabIndex = 30;
            this.label3.Text = "Hoteles en esa ciudad:";
            // 
            // btnFiltrarCiudad
            // 
            this.btnFiltrarCiudad.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFiltrarCiudad.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrarCiudad.Location = new System.Drawing.Point(225, 98);
            this.btnFiltrarCiudad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFiltrarCiudad.Name = "btnFiltrarCiudad";
            this.btnFiltrarCiudad.Size = new System.Drawing.Size(126, 39);
            this.btnFiltrarCiudad.TabIndex = 31;
            this.btnFiltrarCiudad.Text = "Filtrar";
            this.btnFiltrarCiudad.UseVisualStyleBackColor = false;
            this.btnFiltrarCiudad.Click += new System.EventHandler(this.btnFiltrarCiudad_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(17, 394);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(147, 16);
            this.label5.TabIndex = 34;
            this.label5.Text = "Habitaciones en el hotel:";
            // 
            // TiposdHabitacion
            // 
            this.TiposdHabitacion.AllowUserToAddRows = false;
            this.TiposdHabitacion.AllowUserToDeleteRows = false;
            this.TiposdHabitacion.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.TiposdHabitacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TiposdHabitacion.Location = new System.Drawing.Point(20, 423);
            this.TiposdHabitacion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.TiposdHabitacion.Name = "TiposdHabitacion";
            this.TiposdHabitacion.ReadOnly = true;
            this.TiposdHabitacion.RowHeadersWidth = 51;
            this.TiposdHabitacion.RowTemplate.Height = 24;
            this.TiposdHabitacion.Size = new System.Drawing.Size(1268, 160);
            this.TiposdHabitacion.TabIndex = 35;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(1309, 348);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 16);
            this.label8.TabIndex = 42;
            this.label8.Text = "Total:";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtTotal.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(1375, 338);
            this.txtTotal.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTotal.Multiline = true;
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(120, 34);
            this.txtTotal.TabIndex = 43;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(1542, 348);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 16);
            this.label9.TabIndex = 44;
            this.label9.Text = "Anticipo:";
            // 
            // txtAnticipo
            // 
            this.txtAnticipo.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtAnticipo.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAnticipo.Location = new System.Drawing.Point(1621, 338);
            this.txtAnticipo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtAnticipo.Multiline = true;
            this.txtAnticipo.Name = "txtAnticipo";
            this.txtAnticipo.ReadOnly = true;
            this.txtAnticipo.Size = new System.Drawing.Size(135, 34);
            this.txtAnticipo.TabIndex = 45;
            // 
            // dgvDisponibles
            // 
            this.dgvDisponibles.AllowUserToAddRows = false;
            this.dgvDisponibles.AllowUserToDeleteRows = false;
            this.dgvDisponibles.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvDisponibles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDisponibles.Location = new System.Drawing.Point(20, 635);
            this.dgvDisponibles.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDisponibles.Name = "dgvDisponibles";
            this.dgvDisponibles.ReadOnly = true;
            this.dgvDisponibles.RowHeadersWidth = 51;
            this.dgvDisponibles.RowTemplate.Height = 24;
            this.dgvDisponibles.Size = new System.Drawing.Size(1268, 160);
            this.dgvDisponibles.TabIndex = 47;
            this.dgvDisponibles.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDisponibles_CellClick);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(17, 607);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(188, 16);
            this.label10.TabIndex = 46;
            this.label10.Text = "Disponibilidad de habitaciones:";
            // 
            // btnConfirmarReservacion
            // 
            this.btnConfirmarReservacion.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnConfirmarReservacion.FlatAppearance.BorderSize = 0;
            this.btnConfirmarReservacion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarReservacion.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConfirmarReservacion.Location = new System.Drawing.Point(1420, 879);
            this.btnConfirmarReservacion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnConfirmarReservacion.Name = "btnConfirmarReservacion";
            this.btnConfirmarReservacion.Size = new System.Drawing.Size(227, 65);
            this.btnConfirmarReservacion.TabIndex = 48;
            this.btnConfirmarReservacion.Text = "Confirmar Reservacion";
            this.btnConfirmarReservacion.UseVisualStyleBackColor = false;
            this.btnConfirmarReservacion.Click += new System.EventHandler(this.btnConfirmarReservacion_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(659, 606);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(0, 16);
            this.label12.TabIndex = 49;
            // 
            // dtpEntrada
            // 
            this.dtpEntrada.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpEntrada.Location = new System.Drawing.Point(705, 85);
            this.dtpEntrada.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpEntrada.Name = "dtpEntrada";
            this.dtpEntrada.Size = new System.Drawing.Size(200, 23);
            this.dtpEntrada.TabIndex = 50;
            // 
            // dtpSalida
            // 
            this.dtpSalida.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpSalida.Location = new System.Drawing.Point(1020, 85);
            this.dtpSalida.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtpSalida.Name = "dtpSalida";
            this.dtpSalida.Size = new System.Drawing.Size(200, 23);
            this.dtpSalida.TabIndex = 51;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(897, 39);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(107, 16);
            this.label13.TabIndex = 52;
            this.label13.Text = "Rango de fechas:";
            // 
            // btnBuscarHab
            // 
            this.btnBuscarHab.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnBuscarHab.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuscarHab.Location = new System.Drawing.Point(929, 124);
            this.btnBuscarHab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnBuscarHab.Name = "btnBuscarHab";
            this.btnBuscarHab.Size = new System.Drawing.Size(126, 39);
            this.btnBuscarHab.TabIndex = 53;
            this.btnBuscarHab.Text = "Buscar";
            this.btnBuscarHab.UseVisualStyleBackColor = false;
            this.btnBuscarHab.Click += new System.EventHandler(this.btnBuscarHab_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(1309, 206);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(178, 16);
            this.label14.TabIndex = 54;
            this.label14.Text = "Nombre de la reservacion:";
            // 
            // txtNombreCliente
            // 
            this.txtNombreCliente.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.txtNombreCliente.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreCliente.Location = new System.Drawing.Point(1504, 202);
            this.txtNombreCliente.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtNombreCliente.Multiline = true;
            this.txtNombreCliente.Name = "txtNombreCliente";
            this.txtNombreCliente.Size = new System.Drawing.Size(276, 34);
            this.txtNombreCliente.TabIndex = 55;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(1309, 297);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(108, 16);
            this.label15.TabIndex = 57;
            this.label15.Text = "Forma de pago:";
            // 
            // tbnGuardarHab
            // 
            this.tbnGuardarHab.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.tbnGuardarHab.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbnGuardarHab.Location = new System.Drawing.Point(1457, 679);
            this.tbnGuardarHab.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tbnGuardarHab.Name = "tbnGuardarHab";
            this.tbnGuardarHab.Size = new System.Drawing.Size(155, 66);
            this.tbnGuardarHab.TabIndex = 56;
            this.tbnGuardarHab.Text = "Guardar habitacion";
            this.tbnGuardarHab.UseVisualStyleBackColor = false;
            this.tbnGuardarHab.Click += new System.EventHandler(this.tbnGuardarHab_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(19, 812);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(172, 16);
            this.label16.TabIndex = 61;
            this.label16.Text = "Habitaciones Seleccionadas:";
            this.label16.Visible = false;
            // 
            // btnCalcular
            // 
            this.btnCalcular.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnCalcular.Font = new System.Drawing.Font("Lucida Fax", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCalcular.Location = new System.Drawing.Point(1467, 433);
            this.btnCalcular.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(155, 66);
            this.btnCalcular.TabIndex = 62;
            this.btnCalcular.Text = "Total";
            this.btnCalcular.UseVisualStyleBackColor = false;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click_1);
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
            this.txtFormaPago.Location = new System.Drawing.Point(1451, 297);
            this.txtFormaPago.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtFormaPago.Name = "txtFormaPago";
            this.txtFormaPago.Size = new System.Drawing.Size(161, 24);
            this.txtFormaPago.TabIndex = 118;
            // 
            // dgvDetalleReservacion
            // 
            this.dgvDetalleReservacion.AllowUserToAddRows = false;
            this.dgvDetalleReservacion.AllowUserToDeleteRows = false;
            this.dgvDetalleReservacion.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvDetalleReservacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDetalleReservacion.Location = new System.Drawing.Point(20, 831);
            this.dgvDetalleReservacion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvDetalleReservacion.Name = "dgvDetalleReservacion";
            this.dgvDetalleReservacion.ReadOnly = true;
            this.dgvDetalleReservacion.RowHeadersWidth = 51;
            this.dgvDetalleReservacion.RowTemplate.Height = 24;
            this.dgvDetalleReservacion.Size = new System.Drawing.Size(773, 95);
            this.dgvDetalleReservacion.TabIndex = 60;
            this.dgvDetalleReservacion.Visible = false;
            this.dgvDetalleReservacion.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDetalleReservacion_CellContentClick);
            // 
            // ciudadFiltro
            // 
            this.ciudadFiltro.FormattingEnabled = true;
            this.ciudadFiltro.Location = new System.Drawing.Point(20, 101);
            this.ciudadFiltro.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ciudadFiltro.Name = "ciudadFiltro";
            this.ciudadFiltro.Size = new System.Drawing.Size(197, 24);
            this.ciudadFiltro.TabIndex = 119;
            // 
            // Reservaciones
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1917, 982);
            this.Controls.Add(this.ciudadFiltro);
            this.Controls.Add(this.txtFormaPago);
            this.Controls.Add(this.btnCalcular);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.dgvDetalleReservacion);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.tbnGuardarHab);
            this.Controls.Add(this.txtNombreCliente);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.btnBuscarHab);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.dtpSalida);
            this.Controls.Add(this.dtpEntrada);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btnConfirmarReservacion);
            this.Controls.Add(this.dgvDisponibles);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtAnticipo);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtTotal);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.TiposdHabitacion);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnFiltrarCiudad);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCiudad);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtRFC);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.HotelesenCiudad);
            this.Controls.Add(this.label11);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Reservaciones";
            this.Text = "Reservaciones";
            this.Load += new System.EventHandler(this.Reservaciones_Load);
            ((System.ComponentModel.ISupportInitialize)(this.HotelesenCiudad)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TiposdHabitacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDisponibles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalleReservacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridView HotelesenCiudad;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtRFC;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCiudad;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFiltrarCiudad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView TiposdHabitacion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtAnticipo;
        private System.Windows.Forms.DataGridView dgvDisponibles;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnConfirmarReservacion;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.DateTimePicker dtpEntrada;
        private System.Windows.Forms.DateTimePicker dtpSalida;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnBuscarHab;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtNombreCliente;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button tbnGuardarHab;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.ComboBox txtFormaPago;
        private System.Windows.Forms.DataGridView dgvDetalleReservacion;
        private System.Windows.Forms.ComboBox ciudadFiltro;
    }
}