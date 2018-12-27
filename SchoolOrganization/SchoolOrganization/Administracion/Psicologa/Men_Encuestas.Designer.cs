namespace SchoolOrganization
{
    partial class Men_Encuestas
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
            this.lbFicha_clinica = new System.Windows.Forms.Label();
            this.lbProxEgresar = new System.Windows.Forms.Label();
            this.lbEgresados = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSalir = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbFicha_clinica
            // 
            this.lbFicha_clinica.AutoSize = true;
            this.lbFicha_clinica.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbFicha_clinica.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbFicha_clinica.Location = new System.Drawing.Point(36, 42);
            this.lbFicha_clinica.Name = "lbFicha_clinica";
            this.lbFicha_clinica.Size = new System.Drawing.Size(82, 16);
            this.lbFicha_clinica.TabIndex = 0;
            this.lbFicha_clinica.Text = "Ficha clínica";
            this.lbFicha_clinica.Click += new System.EventHandler(this.lbFicha_clinica_Click);
            // 
            // lbProxEgresar
            // 
            this.lbProxEgresar.AutoSize = true;
            this.lbProxEgresar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbProxEgresar.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbProxEgresar.Location = new System.Drawing.Point(36, 70);
            this.lbProxEgresar.Name = "lbProxEgresar";
            this.lbProxEgresar.Size = new System.Drawing.Size(118, 16);
            this.lbProxEgresar.TabIndex = 1;
            this.lbProxEgresar.Text = "Próximo a egresar";
            this.lbProxEgresar.Click += new System.EventHandler(this.lbProxEgresar_Click);
            // 
            // lbEgresados
            // 
            this.lbEgresados.AutoSize = true;
            this.lbEgresados.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lbEgresados.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbEgresados.Location = new System.Drawing.Point(36, 97);
            this.lbEgresados.Name = "lbEgresados";
            this.lbEgresados.Size = new System.Drawing.Size(75, 16);
            this.lbEgresados.TabIndex = 2;
            this.lbEgresados.Text = "Egresados";
            this.lbEgresados.Click += new System.EventHandler(this.lbEgresados_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(36, 123);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "label4";
            // 
            // btnSalir
            // 
            this.btnSalir.Location = new System.Drawing.Point(12, 160);
            this.btnSalir.Name = "btnSalir";
            this.btnSalir.Size = new System.Drawing.Size(75, 23);
            this.btnSalir.TabIndex = 6;
            this.btnSalir.Text = "Cerrar";
            this.btnSalir.UseVisualStyleBackColor = true;
            this.btnSalir.Click += new System.EventHandler(this.btnSalir_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(20, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "Encuestas";
            // 
            // Men_Encuestas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(177, 195);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSalir);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbEgresados);
            this.Controls.Add(this.lbProxEgresar);
            this.Controls.Add(this.lbFicha_clinica);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Men_Encuestas";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Men_Encuestas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbFicha_clinica;
        private System.Windows.Forms.Label lbProxEgresar;
        private System.Windows.Forms.Label lbEgresados;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSalir;
        private System.Windows.Forms.Label label1;
    }
}