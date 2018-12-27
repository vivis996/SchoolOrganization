namespace SchoolOrganization
{
    partial class Instalación
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Instalación));
            this.txbRepetir = new Telerik.WinControls.UI.RadTextBox();
            this.txbContraseña = new Telerik.WinControls.UI.RadTextBox();
            this.btnAgregar = new Telerik.WinControls.UI.RadButton();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.visualStudio2012DarkTheme1 = new Telerik.WinControls.Themes.VisualStudio2012DarkTheme();
            this.txbUsuario = new System.Windows.Forms.MaskedTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.txbRepetir)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbContraseña)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAgregar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // txbRepetir
            // 
            this.txbRepetir.Location = new System.Drawing.Point(174, 157);
            this.txbRepetir.Name = "txbRepetir";
            this.txbRepetir.PasswordChar = '*';
            this.txbRepetir.Size = new System.Drawing.Size(100, 21);
            this.txbRepetir.TabIndex = 2;
            this.txbRepetir.ThemeName = "VisualStudio2012Dark";
            // 
            // txbContraseña
            // 
            this.txbContraseña.Location = new System.Drawing.Point(174, 105);
            this.txbContraseña.Name = "txbContraseña";
            this.txbContraseña.PasswordChar = '*';
            this.txbContraseña.Size = new System.Drawing.Size(100, 21);
            this.txbContraseña.TabIndex = 1;
            this.txbContraseña.ThemeName = "VisualStudio2012Dark";
            // 
            // btnAgregar
            // 
            this.btnAgregar.Location = new System.Drawing.Point(81, 207);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(110, 24);
            this.btnAgregar.TabIndex = 3;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.ThemeName = "VisualStudio2012Dark";
            this.btnAgregar.Click += new System.EventHandler(this.btnAgregar_Click);
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(25, 158);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(100, 18);
            this.radLabel3.TabIndex = 19;
            this.radLabel3.Text = "Repetir contraseña";
            this.radLabel3.ThemeName = "VisualStudio2012Dark";
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(25, 106);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(63, 18);
            this.radLabel2.TabIndex = 18;
            this.radLabel2.Text = "Contraseña";
            this.radLabel2.ThemeName = "VisualStudio2012Dark";
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(25, 49);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(44, 18);
            this.radLabel1.TabIndex = 17;
            this.radLabel1.Text = "Usuario";
            this.radLabel1.ThemeName = "VisualStudio2012Dark";
            // 
            // txbUsuario
            // 
            this.txbUsuario.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(63)))), ((int)(((byte)(70)))));
            this.txbUsuario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txbUsuario.ForeColor = System.Drawing.Color.White;
            this.txbUsuario.Location = new System.Drawing.Point(174, 47);
            this.txbUsuario.Mask = "99999";
            this.txbUsuario.Name = "txbUsuario";
            this.txbUsuario.Size = new System.Drawing.Size(100, 20);
            this.txbUsuario.TabIndex = 0;
            this.txbUsuario.ValidatingType = typeof(int);
            // 
            // Instalación
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 279);
            this.Controls.Add(this.txbUsuario);
            this.Controls.Add(this.txbRepetir);
            this.Controls.Add(this.txbContraseña);
            this.Controls.Add(this.btnAgregar);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.radLabel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Instalación";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Instalación";
            this.ThemeName = "VisualStudio2012Dark";
            ((System.ComponentModel.ISupportInitialize)(this.txbRepetir)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txbContraseña)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAgregar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox txbRepetir;
        private Telerik.WinControls.UI.RadTextBox txbContraseña;
        private Telerik.WinControls.UI.RadButton btnAgregar;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.Themes.VisualStudio2012DarkTheme visualStudio2012DarkTheme1;
        private System.Windows.Forms.MaskedTextBox txbUsuario;
    }
}
