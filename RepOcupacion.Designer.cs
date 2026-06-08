namespace Pantallas_alto_volumen_de_datos
{
    partial class RepOcupacion
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.dataGridViewResumen = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbPais = new System.Windows.Forms.ComboBox();
            this.cbAnio = new System.Windows.Forms.ComboBox();
            this.cbCiudad = new System.Windows.Forms.ComboBox();
            this.btnFiltrar = new System.Windows.Forms.Button();
            this.cbHotel = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dgvOcupacion = new System.Windows.Forms.DataGridView();
            this.chartRepOcupacion = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btnMostrarGrafico = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResumen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOcupacion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRepOcupacion)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewResumen
            // 
            this.dataGridViewResumen.AllowUserToAddRows = false;
            this.dataGridViewResumen.AllowUserToDeleteRows = false;
            this.dataGridViewResumen.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dataGridViewResumen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewResumen.Location = new System.Drawing.Point(96, 466);
            this.dataGridViewResumen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataGridViewResumen.Name = "dataGridViewResumen";
            this.dataGridViewResumen.ReadOnly = true;
            this.dataGridViewResumen.RowHeadersWidth = 51;
            this.dataGridViewResumen.RowTemplate.Height = 24;
            this.dataGridViewResumen.Size = new System.Drawing.Size(783, 239);
            this.dataGridViewResumen.TabIndex = 0;
            this.dataGridViewResumen.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewResumen_CellClick_1);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Lucida Fax", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(91, 25);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(346, 34);
            this.label11.TabIndex = 29;
            this.label11.Text = "Ocupación de Hoteles";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(93, 82);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 16);
            this.label1.TabIndex = 30;
            this.label1.Text = "País";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(325, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 16);
            this.label2.TabIndex = 31;
            this.label2.Text = "Año ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(551, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 16);
            this.label3.TabIndex = 32;
            this.label3.Text = "Ciudad";
            // 
            // cbPais
            // 
            this.cbPais.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cbPais.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbPais.FormattingEnabled = true;
            this.cbPais.Location = new System.Drawing.Point(96, 102);
            this.cbPais.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbPais.Name = "cbPais";
            this.cbPais.Size = new System.Drawing.Size(175, 24);
            this.cbPais.TabIndex = 33;
            this.cbPais.SelectedIndexChanged += new System.EventHandler(this.cbPais_SelectedIndexChanged);
            // 
            // cbAnio
            // 
            this.cbAnio.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cbAnio.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbAnio.FormattingEnabled = true;
            this.cbAnio.Location = new System.Drawing.Point(329, 102);
            this.cbAnio.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbAnio.Name = "cbAnio";
            this.cbAnio.Size = new System.Drawing.Size(177, 24);
            this.cbAnio.TabIndex = 34;
            // 
            // cbCiudad
            // 
            this.cbCiudad.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cbCiudad.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbCiudad.FormattingEnabled = true;
            this.cbCiudad.Location = new System.Drawing.Point(555, 102);
            this.cbCiudad.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbCiudad.Name = "cbCiudad";
            this.cbCiudad.Size = new System.Drawing.Size(183, 24);
            this.cbCiudad.TabIndex = 35;
            this.cbCiudad.SelectedIndexChanged += new System.EventHandler(this.cbCiudad_SelectedIndexChanged);
            // 
            // btnFiltrar
            // 
            this.btnFiltrar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnFiltrar.FlatAppearance.BorderSize = 0;
            this.btnFiltrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrar.Font = new System.Drawing.Font("Lucida Fax", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFiltrar.Location = new System.Drawing.Point(992, 94);
            this.btnFiltrar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFiltrar.Name = "btnFiltrar";
            this.btnFiltrar.Size = new System.Drawing.Size(117, 41);
            this.btnFiltrar.TabIndex = 36;
            this.btnFiltrar.Text = "Filtrar";
            this.btnFiltrar.UseVisualStyleBackColor = false;
            this.btnFiltrar.Click += new System.EventHandler(this.btnFiltrar_Click);
            // 
            // cbHotel
            // 
            this.cbHotel.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.cbHotel.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbHotel.FormattingEnabled = true;
            this.cbHotel.Location = new System.Drawing.Point(773, 102);
            this.cbHotel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbHotel.Name = "cbHotel";
            this.cbHotel.Size = new System.Drawing.Size(183, 24);
            this.cbHotel.TabIndex = 37;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Lucida Fax", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(771, 78);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 38;
            this.label4.Text = "Hotel";
            // 
            // dgvOcupacion
            // 
            this.dgvOcupacion.AllowUserToAddRows = false;
            this.dgvOcupacion.AllowUserToDeleteRows = false;
            this.dgvOcupacion.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgvOcupacion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOcupacion.Location = new System.Drawing.Point(96, 185);
            this.dgvOcupacion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dgvOcupacion.Name = "dgvOcupacion";
            this.dgvOcupacion.ReadOnly = true;
            this.dgvOcupacion.RowHeadersWidth = 51;
            this.dgvOcupacion.RowTemplate.Height = 24;
            this.dgvOcupacion.Size = new System.Drawing.Size(1357, 244);
            this.dgvOcupacion.TabIndex = 39;
            // 
            // chartRepOcupacion
            // 
            chartArea1.Name = "ChartArea1";
            this.chartRepOcupacion.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartRepOcupacion.Legends.Add(legend1);
            this.chartRepOcupacion.Location = new System.Drawing.Point(963, 448);
            this.chartRepOcupacion.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.chartRepOcupacion.Name = "chartRepOcupacion";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartRepOcupacion.Series.Add(series1);
            this.chartRepOcupacion.Size = new System.Drawing.Size(501, 270);
            this.chartRepOcupacion.TabIndex = 40;
            this.chartRepOcupacion.Text = "chart1";
            this.chartRepOcupacion.Visible = false;
            // 
            // btnMostrarGrafico
            // 
            this.btnMostrarGrafico.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnMostrarGrafico.FlatAppearance.BorderSize = 0;
            this.btnMostrarGrafico.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMostrarGrafico.Font = new System.Drawing.Font("Rockwell", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMostrarGrafico.Location = new System.Drawing.Point(837, 725);
            this.btnMostrarGrafico.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnMostrarGrafico.Name = "btnMostrarGrafico";
            this.btnMostrarGrafico.Size = new System.Drawing.Size(117, 41);
            this.btnMostrarGrafico.TabIndex = 41;
            this.btnMostrarGrafico.Text = "Mostrar grafico";
            this.btnMostrarGrafico.UseVisualStyleBackColor = false;
            this.btnMostrarGrafico.Visible = false;
            this.btnMostrarGrafico.Click += new System.EventHandler(this.btnMostrarGrafico_Click);
            // 
            // RepOcupacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(1467, 778);
            this.Controls.Add(this.dgvOcupacion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbHotel);
            this.Controls.Add(this.btnFiltrar);
            this.Controls.Add(this.cbCiudad);
            this.Controls.Add(this.cbAnio);
            this.Controls.Add(this.cbPais);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.dataGridViewResumen);
            this.Controls.Add(this.btnMostrarGrafico);
            this.Controls.Add(this.chartRepOcupacion);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "RepOcupacion";
            this.Text = "RepOcupacion";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewResumen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOcupacion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartRepOcupacion)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewResumen;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbPais;
        private System.Windows.Forms.ComboBox cbAnio;
        private System.Windows.Forms.ComboBox cbCiudad;
        private System.Windows.Forms.Button btnFiltrar;
        private System.Windows.Forms.ComboBox cbHotel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView dgvOcupacion;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartRepOcupacion;
        private System.Windows.Forms.Button btnMostrarGrafico;
    }
}