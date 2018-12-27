namespace SchoolOrganization
{
    partial class Menu_alumnos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Menu_alumnos));
            this.btnMateria = new Telerik.WinControls.UI.RadButton();
            this.btnCalificaciones = new Telerik.WinControls.UI.RadButton();
            this.btnContraseña = new Telerik.WinControls.UI.RadButton();
            this.visualStudio2012DarkTheme1 = new Telerik.WinControls.Themes.VisualStudio2012DarkTheme();
            ((System.ComponentModel.ISupportInitialize)(this.btnMateria)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCalificaciones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnContraseña)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // btnMateria
            // 
            this.btnMateria.Location = new System.Drawing.Point(26, 38);
            this.btnMateria.Name = "btnMateria";
            this.btnMateria.Size = new System.Drawing.Size(110, 24);
            this.btnMateria.TabIndex = 1;
            this.btnMateria.Text = "Materia";
            this.btnMateria.ThemeName = "VisualStudio2012Dark";
            this.btnMateria.Click += new System.EventHandler(this.btnMateria_Click);
            // 
            // btnCalificaciones
            // 
            this.btnCalificaciones.Location = new System.Drawing.Point(171, 38);
            this.btnCalificaciones.Name = "btnCalificaciones";
            this.btnCalificaciones.Size = new System.Drawing.Size(110, 24);
            this.btnCalificaciones.TabIndex = 2;
            this.btnCalificaciones.Text = "Calificaciones";
            this.btnCalificaciones.ThemeName = "VisualStudio2012Dark";
            this.btnCalificaciones.Click += new System.EventHandler(this.btnCalificaciones_Click);
            // 
            // btnContraseña
            // 
            this.btnContraseña.Location = new System.Drawing.Point(316, 38);
            this.btnContraseña.Name = "btnContraseña";
            this.btnContraseña.Size = new System.Drawing.Size(110, 24);
            this.btnContraseña.TabIndex = 3;
            this.btnContraseña.Text = "Cambiar contraseña";
            this.btnContraseña.ThemeName = "VisualStudio2012Dark";
            this.btnContraseña.Click += new System.EventHandler(this.btnContraseña_Click);
            // 
            // Menu_alumnos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 98);
            this.Controls.Add(this.btnContraseña);
            this.Controls.Add(this.btnCalificaciones);
            this.Controls.Add(this.btnMateria);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Menu_alumnos";
            // 
            // 
            // 
            this.RootElement.ApplyShapeToControl = true;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu alumnos";
            this.ThemeName = "VisualStudio2012Dark";
            this.Load += new System.EventHandler(this.Menu_alumnos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnMateria)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCalificaciones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnContraseña)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnMateria;
        private Telerik.WinControls.UI.RadButton btnCalificaciones;
        private Telerik.WinControls.UI.RadButton btnContraseña;
        private Telerik.WinControls.Themes.VisualStudio2012DarkTheme visualStudio2012DarkTheme1;
    }
}
