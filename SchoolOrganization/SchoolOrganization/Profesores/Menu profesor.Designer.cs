namespace SchoolOrganization
{
    partial class Menu_profesor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu_profesor));
            this.btnAlumno = new Telerik.WinControls.UI.RadButton();
            this.btnGrupo = new Telerik.WinControls.UI.RadButton();
            this.rbContraseña = new Telerik.WinControls.UI.RadButton();
            this.visualStudio2012DarkTheme1 = new Telerik.WinControls.Themes.VisualStudio2012DarkTheme();
            ((System.ComponentModel.ISupportInitialize)(this.btnAlumno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGrupo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbContraseña)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnAlumno
            // 
            this.btnAlumno.Location = new System.Drawing.Point(29, 44);
            this.btnAlumno.Name = "btnAlumno";
            this.btnAlumno.Size = new System.Drawing.Size(110, 24);
            this.btnAlumno.TabIndex = 0;
            this.btnAlumno.Text = "Alumnos";
            this.btnAlumno.ThemeName = "VisualStudio2012Dark";
            this.btnAlumno.Click += new System.EventHandler(this.btnAlumno_Click);
            // 
            // btnGrupo
            // 
            this.btnGrupo.Location = new System.Drawing.Point(183, 44);
            this.btnGrupo.Name = "btnGrupo";
            this.btnGrupo.Size = new System.Drawing.Size(110, 24);
            this.btnGrupo.TabIndex = 1;
            this.btnGrupo.Text = "Grupo";
            this.btnGrupo.ThemeName = "VisualStudio2012Dark";
            this.btnGrupo.Click += new System.EventHandler(this.btnGrupo_Click);
            // 
            // rbContraseña
            // 
            this.rbContraseña.Location = new System.Drawing.Point(325, 44);
            this.rbContraseña.Name = "rbContraseña";
            this.rbContraseña.Size = new System.Drawing.Size(110, 24);
            this.rbContraseña.TabIndex = 2;
            this.rbContraseña.Text = "Cambiar contraseña";
            this.rbContraseña.ThemeName = "VisualStudio2012Dark";
            this.rbContraseña.Click += new System.EventHandler(this.rbContraseña_Click);
            // 
            // Menu_profesor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(477, 110);
            this.Controls.Add(this.rbContraseña);
            this.Controls.Add(this.btnGrupo);
            this.Controls.Add(this.btnAlumno);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Menu_profesor";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu_profesor";
            this.ThemeName = "VisualStudio2012Dark";
            this.Load += new System.EventHandler(this.Menu_profesor_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnAlumno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGrupo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbContraseña)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnAlumno;
        private Telerik.WinControls.UI.RadButton btnGrupo;
        private Telerik.WinControls.UI.RadButton rbContraseña;
        private Telerik.WinControls.Themes.VisualStudio2012DarkTheme visualStudio2012DarkTheme1;
    }
}
