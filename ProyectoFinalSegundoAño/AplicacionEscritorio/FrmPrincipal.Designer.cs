namespace AplicacionEscritorio
{
    partial class FrmPrincipal
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ABMAdministradoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RegistroDeJugadoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ABMBancosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GeneracionDeSorteosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RealizaciónDeSorteosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UsuarioDeBDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ABMAdministradoresToolStripMenuItem,
            this.RegistroDeJugadoresToolStripMenuItem,
            this.ABMBancosToolStripMenuItem,
            this.GeneracionDeSorteosToolStripMenuItem,
            this.RealizaciónDeSorteosToolStripMenuItem,
            this.UsuarioDeBDToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(794, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ABMAdministradoresToolStripMenuItem
            // 
            this.ABMAdministradoresToolStripMenuItem.Name = "ABMAdministradoresToolStripMenuItem";
            this.ABMAdministradoresToolStripMenuItem.Size = new System.Drawing.Size(137, 20);
            this.ABMAdministradoresToolStripMenuItem.Text = "ABM-Administradores";
            this.ABMAdministradoresToolStripMenuItem.Click += new System.EventHandler(this.ABMAdministradoresToolStripMenuItem_Click);
            // 
            // RegistroDeJugadoresToolStripMenuItem
            // 
            this.RegistroDeJugadoresToolStripMenuItem.Name = "RegistroDeJugadoresToolStripMenuItem";
            this.RegistroDeJugadoresToolStripMenuItem.Size = new System.Drawing.Size(134, 20);
            this.RegistroDeJugadoresToolStripMenuItem.Text = "Registro de Jugadores";
            this.RegistroDeJugadoresToolStripMenuItem.Click += new System.EventHandler(this.RegistroDeJugadoresToolStripMenuItem_Click);
            // 
            // ABMBancosToolStripMenuItem
            // 
            this.ABMBancosToolStripMenuItem.Name = "ABMBancosToolStripMenuItem";
            this.ABMBancosToolStripMenuItem.Size = new System.Drawing.Size(94, 20);
            this.ABMBancosToolStripMenuItem.Text = "ABM - Bancos";
            this.ABMBancosToolStripMenuItem.Click += new System.EventHandler(this.ABMBancosToolStripMenuItem_Click);
            // 
            // GeneracionDeSorteosToolStripMenuItem
            // 
            this.GeneracionDeSorteosToolStripMenuItem.Name = "GeneracionDeSorteosToolStripMenuItem";
            this.GeneracionDeSorteosToolStripMenuItem.Size = new System.Drawing.Size(137, 20);
            this.GeneracionDeSorteosToolStripMenuItem.Text = "Generacion de Sorteos";
            this.GeneracionDeSorteosToolStripMenuItem.Click += new System.EventHandler(this.GeneracionDeSorteosToolStripMenuItem_Click);
            // 
            // RealizaciónDeSorteosToolStripMenuItem
            // 
            this.RealizaciónDeSorteosToolStripMenuItem.Name = "RealizaciónDeSorteosToolStripMenuItem";
            this.RealizaciónDeSorteosToolStripMenuItem.Size = new System.Drawing.Size(136, 20);
            this.RealizaciónDeSorteosToolStripMenuItem.Text = "Realización de Sorteos";
            this.RealizaciónDeSorteosToolStripMenuItem.Click += new System.EventHandler(this.RealizaciónDeSorteosToolStripMenuItem_Click);
            // 
            // UsuarioDeBDToolStripMenuItem
            // 
            this.UsuarioDeBDToolStripMenuItem.Name = "UsuarioDeBDToolStripMenuItem";
            this.UsuarioDeBDToolStripMenuItem.Size = new System.Drawing.Size(93, 20);
            this.UsuarioDeBDToolStripMenuItem.Text = "Usuario de BD";
            this.UsuarioDeBDToolStripMenuItem.Click += new System.EventHandler(this.UsuarioDeBDToolStripMenuItem_Click);
            // 
            // FrmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 261);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmPrincipal";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ABMAdministradoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RegistroDeJugadoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ABMBancosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GeneracionDeSorteosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RealizaciónDeSorteosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UsuarioDeBDToolStripMenuItem;
    }
}