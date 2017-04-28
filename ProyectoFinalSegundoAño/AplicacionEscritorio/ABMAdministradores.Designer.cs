namespace AplicacionEscritorio
{
    partial class ABMAdministradores
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblMensaje = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtConfContrasenia = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbSorteo = new System.Windows.Forms.CheckBox();
            this.txtContrasenia = new System.Windows.Forms.TextBox();
            this.txtNomLogueo = new System.Windows.Forms.TextBox();
            this.txtNomCompleto = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDocumento = new System.Windows.Forms.TextBox();
            this.epError = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnAlta = new System.Windows.Forms.ToolStripButton();
            this.btnBaja = new System.Windows.Forms.ToolStripButton();
            this.btnModificar = new System.Windows.Forms.ToolStripButton();
            this.btnLimpiar = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epError)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnAlta,
            this.btnBaja,
            this.btnModificar,
            this.btnLimpiar});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(504, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblMensaje});
            this.statusStrip1.Location = new System.Drawing.Point(0, 240);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(504, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // lblMensaje
            // 
            this.lblMensaje.Name = "lblMensaje";
            this.lblMensaje.Size = new System.Drawing.Size(0, 17);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtConfContrasenia);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cbSorteo);
            this.groupBox1.Controls.Add(this.txtContrasenia);
            this.groupBox1.Controls.Add(this.txtNomLogueo);
            this.groupBox1.Controls.Add(this.txtNomCompleto);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDocumento);
            this.groupBox1.Location = new System.Drawing.Point(12, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(480, 198);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "ABM Administradores";
            // 
            // txtConfContrasenia
            // 
            this.txtConfContrasenia.CharacterCasing = System.Windows.Forms.CharacterCasing.Lower;
            this.txtConfContrasenia.Location = new System.Drawing.Point(247, 131);
            this.txtConfContrasenia.Name = "txtConfContrasenia";
            this.txtConfContrasenia.PasswordChar = '*';
            this.txtConfContrasenia.Size = new System.Drawing.Size(200, 20);
            this.txtConfContrasenia.TabIndex = 11;
            this.txtConfContrasenia.Validating += new System.ComponentModel.CancelEventHandler(this.txtConfContrasenia_Validating);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(244, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Confirmar Contraseña";
            // 
            // cbSorteo
            // 
            this.cbSorteo.AutoSize = true;
            this.cbSorteo.Location = new System.Drawing.Point(9, 174);
            this.cbSorteo.Name = "cbSorteo";
            this.cbSorteo.Size = new System.Drawing.Size(15, 14);
            this.cbSorteo.TabIndex = 9;
            this.cbSorteo.UseVisualStyleBackColor = true;
            // 
            // txtContrasenia
            // 
            this.txtContrasenia.Location = new System.Drawing.Point(7, 131);
            this.txtContrasenia.Name = "txtContrasenia";
            this.txtContrasenia.PasswordChar = '*';
            this.txtContrasenia.Size = new System.Drawing.Size(200, 20);
            this.txtContrasenia.TabIndex = 8;
            this.txtContrasenia.Validating += new System.ComponentModel.CancelEventHandler(this.txtContrasenia_Validating);
            // 
            // txtNomLogueo
            // 
            this.txtNomLogueo.Location = new System.Drawing.Point(247, 88);
            this.txtNomLogueo.Name = "txtNomLogueo";
            this.txtNomLogueo.Size = new System.Drawing.Size(200, 20);
            this.txtNomLogueo.TabIndex = 7;
            // 
            // txtNomCompleto
            // 
            this.txtNomCompleto.Location = new System.Drawing.Point(7, 88);
            this.txtNomCompleto.Name = "txtNomCompleto";
            this.txtNomCompleto.Size = new System.Drawing.Size(200, 20);
            this.txtNomCompleto.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Ejecuta sorteo?";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Contraseña";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(244, 72);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Nombre de logueo";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Nombre Completo";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Documento";
            // 
            // txtDocumento
            // 
            this.txtDocumento.Location = new System.Drawing.Point(7, 45);
            this.txtDocumento.Name = "txtDocumento";
            this.txtDocumento.Size = new System.Drawing.Size(200, 20);
            this.txtDocumento.TabIndex = 0;
            this.txtDocumento.Validating += new System.ComponentModel.CancelEventHandler(this.txtDocumento_Validating);
            // 
            // epError
            // 
            this.epError.ContainerControl = this;
            // 
            // btnAlta
            // 
            this.btnAlta.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnAlta.Image = global::AplicacionEscritorio.Properties.Resources.agregar;
            this.btnAlta.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAlta.Name = "btnAlta";
            this.btnAlta.Size = new System.Drawing.Size(23, 22);
            this.btnAlta.Text = "Alta";
            this.btnAlta.Click += new System.EventHandler(this.btnAlta_Click);
            // 
            // btnBaja
            // 
            this.btnBaja.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnBaja.Image = global::AplicacionEscritorio.Properties.Resources.borrar;
            this.btnBaja.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnBaja.Name = "btnBaja";
            this.btnBaja.Size = new System.Drawing.Size(23, 22);
            this.btnBaja.Text = "Baja";
            this.btnBaja.Click += new System.EventHandler(this.btnBaja_Click);
            // 
            // btnModificar
            // 
            this.btnModificar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnModificar.Image = global::AplicacionEscritorio.Properties.Resources.editar;
            this.btnModificar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModificar.Name = "btnModificar";
            this.btnModificar.Size = new System.Drawing.Size(23, 22);
            this.btnModificar.Text = "Modificar";
            this.btnModificar.Click += new System.EventHandler(this.btnModificar_Click);
            // 
            // btnLimpiar
            // 
            this.btnLimpiar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnLimpiar.Image = global::AplicacionEscritorio.Properties.Resources.reiniciar;
            this.btnLimpiar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLimpiar.Name = "btnLimpiar";
            this.btnLimpiar.Size = new System.Drawing.Size(23, 22);
            this.btnLimpiar.Text = "Limpiar";
            this.btnLimpiar.Click += new System.EventHandler(this.btnLimpiar_Click);
            // 
            // ABMAdministradores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(504, 262);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "ABMAdministradores";
            this.Text = "ABMAdministradores";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.epError)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripButton btnAlta;
        private System.Windows.Forms.ToolStripButton btnBaja;
        private System.Windows.Forms.ToolStripButton btnModificar;
        private System.Windows.Forms.ToolStripButton btnLimpiar;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtDocumento;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbSorteo;
        private System.Windows.Forms.TextBox txtContrasenia;
        private System.Windows.Forms.TextBox txtNomCompleto;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ErrorProvider epError;
        private System.Windows.Forms.ToolStripStatusLabel lblMensaje;
        public System.Windows.Forms.TextBox txtNomLogueo;
        private System.Windows.Forms.TextBox txtConfContrasenia;
        private System.Windows.Forms.Label label6;
    }
}