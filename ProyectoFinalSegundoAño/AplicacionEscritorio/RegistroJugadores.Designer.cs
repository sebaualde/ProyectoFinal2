namespace AplicacionEscritorio
{
    partial class RegistroJugadores
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
            this.gvJugadores = new System.Windows.Forms.DataGridView();
            this.Documento = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreCompleto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreLogueo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NombreBanco = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMensaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblDocumento = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.rbRechazar = new System.Windows.Forms.RadioButton();
            this.rbAgregar = new System.Windows.Forms.RadioButton();
            this.lblBanco = new System.Windows.Forms.Label();
            this.lblNombreLogueo = new System.Windows.Forms.Label();
            this.lblNombreCompleto = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gvJugadores)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gvJugadores
            // 
            this.gvJugadores.AllowUserToAddRows = false;
            this.gvJugadores.AllowUserToDeleteRows = false;
            this.gvJugadores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gvJugadores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gvJugadores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Documento,
            this.NombreCompleto,
            this.NombreLogueo,
            this.NombreBanco});
            this.gvJugadores.Location = new System.Drawing.Point(233, 44);
            this.gvJugadores.MultiSelect = false;
            this.gvJugadores.Name = "gvJugadores";
            this.gvJugadores.ReadOnly = true;
            this.gvJugadores.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gvJugadores.Size = new System.Drawing.Size(515, 221);
            this.gvJugadores.TabIndex = 0;
            this.gvJugadores.SelectionChanged += new System.EventHandler(this.gvJugadores_SelectionChanged);
            // 
            // Documento
            // 
            this.Documento.DataPropertyName = "Documento";
            this.Documento.HeaderText = "Documento";
            this.Documento.Name = "Documento";
            this.Documento.ReadOnly = true;
            // 
            // NombreCompleto
            // 
            this.NombreCompleto.DataPropertyName = "NombreCompleto";
            this.NombreCompleto.HeaderText = "Nombre Completo";
            this.NombreCompleto.Name = "NombreCompleto";
            this.NombreCompleto.ReadOnly = true;
            this.NombreCompleto.Width = 120;
            // 
            // NombreLogueo
            // 
            this.NombreLogueo.DataPropertyName = "NombreLogueo";
            this.NombreLogueo.HeaderText = "Nombre de Logueo";
            this.NombreLogueo.Name = "NombreLogueo";
            this.NombreLogueo.ReadOnly = true;
            this.NombreLogueo.Width = 150;
            // 
            // NombreBanco
            // 
            this.NombreBanco.DataPropertyName = "NombreBanco";
            this.NombreBanco.HeaderText = "Banco";
            this.NombreBanco.Name = "NombreBanco";
            this.NombreBanco.ReadOnly = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(759, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensaje});
            this.statusStrip1.Location = new System.Drawing.Point(0, 272);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(759, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMensaje
            // 
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.lblDocumento);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnConfirmar);
            this.groupBox1.Controls.Add(this.rbRechazar);
            this.groupBox1.Controls.Add(this.rbAgregar);
            this.groupBox1.Controls.Add(this.lblBanco);
            this.groupBox1.Controls.Add(this.lblNombreLogueo);
            this.groupBox1.Controls.Add(this.lblNombreCompleto);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(13, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(214, 227);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Solicitud de Usuario";
            // 
            // lblDocumento
            // 
            this.lblDocumento.AutoSize = true;
            this.lblDocumento.Location = new System.Drawing.Point(68, 27);
            this.lblDocumento.Name = "lblDocumento";
            this.lblDocumento.Size = new System.Drawing.Size(0, 13);
            this.lblDocumento.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Documento:";
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.Location = new System.Drawing.Point(57, 171);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(75, 23);
            this.btnConfirmar.TabIndex = 8;
            this.btnConfirmar.Text = "Confirmar";
            this.btnConfirmar.UseVisualStyleBackColor = true;
            this.btnConfirmar.Click += new System.EventHandler(this.btnConfirmar_Click_1);
            // 
            // rbRechazar
            // 
            this.rbRechazar.AutoSize = true;
            this.rbRechazar.Location = new System.Drawing.Point(113, 133);
            this.rbRechazar.Name = "rbRechazar";
            this.rbRechazar.Size = new System.Drawing.Size(71, 17);
            this.rbRechazar.TabIndex = 7;
            this.rbRechazar.TabStop = true;
            this.rbRechazar.Text = "Rechazar";
            this.rbRechazar.UseVisualStyleBackColor = true;
            this.rbRechazar.CheckedChanged += new System.EventHandler(this.rbRechazar_CheckedChanged);
            // 
            // rbAgregar
            // 
            this.rbAgregar.AutoSize = true;
            this.rbAgregar.Location = new System.Drawing.Point(19, 133);
            this.rbAgregar.Name = "rbAgregar";
            this.rbAgregar.Size = new System.Drawing.Size(62, 17);
            this.rbAgregar.TabIndex = 6;
            this.rbAgregar.TabStop = true;
            this.rbAgregar.Text = "Agregar";
            this.rbAgregar.UseVisualStyleBackColor = true;
            this.rbAgregar.CheckedChanged += new System.EventHandler(this.rbAgregar_CheckedChanged);
            // 
            // lblBanco
            // 
            this.lblBanco.AutoSize = true;
            this.lblBanco.Location = new System.Drawing.Point(44, 96);
            this.lblBanco.Name = "lblBanco";
            this.lblBanco.Size = new System.Drawing.Size(0, 13);
            this.lblBanco.TabIndex = 5;
            // 
            // lblNombreLogueo
            // 
            this.lblNombreLogueo.AutoSize = true;
            this.lblNombreLogueo.Location = new System.Drawing.Point(104, 73);
            this.lblNombreLogueo.Name = "lblNombreLogueo";
            this.lblNombreLogueo.Size = new System.Drawing.Size(0, 13);
            this.lblNombreLogueo.TabIndex = 4;
            // 
            // lblNombreCompleto
            // 
            this.lblNombreCompleto.AutoSize = true;
            this.lblNombreCompleto.Location = new System.Drawing.Point(97, 50);
            this.lblNombreCompleto.Name = "lblNombreCompleto";
            this.lblNombreCompleto.Size = new System.Drawing.Size(0, 13);
            this.lblNombreCompleto.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Banco:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Nombre de Logueo:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 50);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Nombre Completo:";
            // 
            // RegistroJugadores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(759, 294);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.gvJugadores);
            this.Name = "RegistroJugadores";
            this.Text = "RegistroJugadores";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RegistroJugadores_FormClosing);
            this.Load += new System.EventHandler(this.RegistroJugadores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvJugadores)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gvJugadores;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConfirmar;
        private System.Windows.Forms.RadioButton rbRechazar;
        private System.Windows.Forms.RadioButton rbAgregar;
        private System.Windows.Forms.Label lblBanco;
        private System.Windows.Forms.Label lblNombreLogueo;
        private System.Windows.Forms.Label lblNombreCompleto;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDocumento;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ToolStripStatusLabel lblMensaje;
        private System.Windows.Forms.DataGridViewTextBoxColumn Documento;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreCompleto;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreLogueo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NombreBanco;
    }
}