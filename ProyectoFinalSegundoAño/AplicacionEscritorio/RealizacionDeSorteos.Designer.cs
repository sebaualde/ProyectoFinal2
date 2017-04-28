namespace AplicacionEscritorio
{
    partial class RealizacionDeSorteos
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
            this.gvSorteosDisponibles = new System.Windows.Forms.DataGridView();
            this.FechaHora = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMensaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.gvPremiadas = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.controlRealizarSorteo1 = new ControlesWindows.ControlRealizarSorteo();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Jugador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gvSorteosDisponibles)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPremiadas)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvSorteosDisponibles
            // 
            this.gvSorteosDisponibles.AllowUserToAddRows = false;
            this.gvSorteosDisponibles.AllowUserToDeleteRows = false;
            this.gvSorteosDisponibles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.gvSorteosDisponibles.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvSorteosDisponibles.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvSorteosDisponibles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FechaHora});
            this.gvSorteosDisponibles.Location = new System.Drawing.Point(27, 42);
            this.gvSorteosDisponibles.MultiSelect = false;
            this.gvSorteosDisponibles.Name = "gvSorteosDisponibles";
            this.gvSorteosDisponibles.ReadOnly = true;
            this.gvSorteosDisponibles.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvSorteosDisponibles.Size = new System.Drawing.Size(282, 351);
            this.gvSorteosDisponibles.TabIndex = 0;
            // 
            // FechaHora
            // 
            this.FechaHora.DataPropertyName = "FechaHora";
            this.FechaHora.HeaderText = "Fecha y Hora";
            this.FechaHora.Name = "FechaHora";
            this.FechaHora.ReadOnly = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensaje});
            this.statusStrip1.Location = new System.Drawing.Point(0, 405);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(693, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMensaje
            // 
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(0, 17);
            // 
            // gvPremiadas
            // 
            this.gvPremiadas.AllowUserToAddRows = false;
            this.gvPremiadas.AllowUserToDeleteRows = false;
            this.gvPremiadas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvPremiadas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.gvPremiadas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvPremiadas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Jugador,
            this.FH});
            this.gvPremiadas.Location = new System.Drawing.Point(6, 135);
            this.gvPremiadas.Name = "gvPremiadas";
            this.gvPremiadas.ReadOnly = true;
            this.gvPremiadas.Size = new System.Drawing.Size(354, 232);
            this.gvPremiadas.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sorteos disponibles";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Jugadas Premiadas";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.controlRealizarSorteo1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.gvPremiadas);
            this.groupBox1.Location = new System.Drawing.Point(315, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(366, 373);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // controlRealizarSorteo1
            // 
            this.controlRealizarSorteo1.Location = new System.Drawing.Point(11, 14);
            this.controlRealizarSorteo1.Name = "controlRealizarSorteo1";
            this.controlRealizarSorteo1.Size = new System.Drawing.Size(344, 92);
            this.controlRealizarSorteo1.TabIndex = 2;
            this.controlRealizarSorteo1.Text = "controlRealizarSorteo1";
            this.controlRealizarSorteo1.SorteoRealizado += new System.EventHandler(this.controlRealizarSorteo1_SorteoRealizado);
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Id";
            this.Column1.HeaderText = "ID";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Jugador
            // 
            this.Jugador.DataPropertyName = "ElJugador";
            this.Jugador.HeaderText = "Jugador";
            this.Jugador.Name = "Jugador";
            this.Jugador.ReadOnly = true;
            // 
            // FH
            // 
            this.FH.DataPropertyName = "FechaHora";
            this.FH.HeaderText = "Fecha y hora";
            this.FH.Name = "FH";
            this.FH.ReadOnly = true;
            // 
            // RealizacionDeSorteos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 427);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gvSorteosDisponibles);
            this.Name = "RealizacionDeSorteos";
            this.Text = "RealizacionDeSorteos";
            ((System.ComponentModel.ISupportInitialize)(this.gvSorteosDisponibles)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvPremiadas)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvSorteosDisponibles;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblMensaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn FechaHora;
        private ControlesWindows.ControlRealizarSorteo controlRealizarSorteo1;
        private System.Windows.Forms.DataGridView gvPremiadas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Jugador;
        private System.Windows.Forms.DataGridViewTextBoxColumn FH;
    }
}