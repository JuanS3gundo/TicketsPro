namespace TicketPro
{
    partial class CrearTicket
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.TituloTxt = new System.Windows.Forms.TextBox();
            this.DescrTxt = new System.Windows.Forms.TextBox();
            this.UbicacionLab = new System.Windows.Forms.TextBox();
            this.EstadoLab = new System.Windows.Forms.TextBox();
            this.FechaApeLab = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 56);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "label1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cmbCategoria);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.FechaApeLab);
            this.panel1.Controls.Add(this.EstadoLab);
            this.panel1.Controls.Add(this.UbicacionLab);
            this.panel1.Controls.Add(this.DescrTxt);
            this.panel1.Controls.Add(this.TituloTxt);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(69, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(549, 325);
            this.panel1.TabIndex = 1;
            // 
            // TituloTxt
            // 
            this.TituloTxt.Location = new System.Drawing.Point(57, 85);
            this.TituloTxt.Name = "TituloTxt";
            this.TituloTxt.Size = new System.Drawing.Size(205, 20);
            this.TituloTxt.TabIndex = 1;
            // 
            // DescrTxt
            // 
            this.DescrTxt.Location = new System.Drawing.Point(57, 123);
            this.DescrTxt.Name = "DescrTxt";
            this.DescrTxt.Size = new System.Drawing.Size(205, 20);
            this.DescrTxt.TabIndex = 2;
            // 
            // UbicacionLab
            // 
            this.UbicacionLab.Location = new System.Drawing.Point(330, 168);
            this.UbicacionLab.Name = "UbicacionLab";
            this.UbicacionLab.Size = new System.Drawing.Size(205, 20);
            this.UbicacionLab.TabIndex = 5;
            // 
            // EstadoLab
            // 
            this.EstadoLab.Location = new System.Drawing.Point(330, 123);
            this.EstadoLab.Name = "EstadoLab";
            this.EstadoLab.Size = new System.Drawing.Size(205, 20);
            this.EstadoLab.TabIndex = 6;
            // 
            // FechaApeLab
            // 
            this.FechaApeLab.AutoSize = true;
            this.FechaApeLab.Location = new System.Drawing.Point(54, 175);
            this.FechaApeLab.Name = "FechaApeLab";
            this.FechaApeLab.Size = new System.Drawing.Size(12, 13);
            this.FechaApeLab.TabIndex = 7;
            this.FechaApeLab.Text = "/";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(160, 235);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 10;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // cmbCategoria
            // 
            this.cmbCategoria.FormattingEnabled = true;
            this.cmbCategoria.Items.AddRange(new object[] {
            "Software",
            "Hardware"});
            this.cmbCategoria.Location = new System.Drawing.Point(330, 84);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(205, 21);
            this.cmbCategoria.TabIndex = 11;
            // 
            // CrearTicket
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "CrearTicket";
            this.Text = "CrearTicket";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label FechaApeLab;
        private System.Windows.Forms.TextBox EstadoLab;
        private System.Windows.Forms.TextBox UbicacionLab;
        private System.Windows.Forms.TextBox DescrTxt;
        private System.Windows.Forms.TextBox TituloTxt;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox cmbCategoria;
    }
}