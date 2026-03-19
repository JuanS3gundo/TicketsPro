namespace TicketPro.Tecnico
{
    partial class FormGestionMaestros
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.cmbTipoMaestro = new System.Windows.Forms.ComboBox();
            this.lblSeleccioneTipo = new System.Windows.Forms.Label();
            this.panelCenter = new System.Windows.Forms.Panel();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnGestionSLA = new System.Windows.Forms.Button();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnAgregar = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelCenter.SuspendLayout();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            
            
            
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(34)))));
            this.panelTop.Controls.Add(this.lblTitulo);
            this.panelTop.Controls.Add(this.cmbTipoMaestro);
            this.panelTop.Controls.Add(this.lblSeleccioneTipo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(800, 120);
            this.panelTop.TabIndex = 0;
            
            
            
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 15);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(226, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Gestion de Maestros";
            
            
            
            this.cmbTipoMaestro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.cmbTipoMaestro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoMaestro.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbTipoMaestro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoMaestro.ForeColor = System.Drawing.Color.White;
            this.cmbTipoMaestro.FormattingEnabled = true;
            this.cmbTipoMaestro.Location = new System.Drawing.Point(20, 80);
            this.cmbTipoMaestro.Name = "cmbTipoMaestro";
            this.cmbTipoMaestro.Size = new System.Drawing.Size(300, 25);
            this.cmbTipoMaestro.TabIndex = 2;
            
            
            
            this.lblSeleccioneTipo.AutoSize = true;
            this.lblSeleccioneTipo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSeleccioneTipo.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblSeleccioneTipo.Location = new System.Drawing.Point(20, 55);
            this.lblSeleccioneTipo.Name = "lblSeleccioneTipo";
            this.lblSeleccioneTipo.Size = new System.Drawing.Size(175, 19);
            this.lblSeleccioneTipo.TabIndex = 1;
            this.lblSeleccioneTipo.Text = "Seleccione tipo de maestro:";
            
            
            
            this.panelCenter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.panelCenter.Controls.Add(this.lblCantidad);
            this.panelCenter.Controls.Add(this.lstItems);
            this.panelCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelCenter.Location = new System.Drawing.Point(0, 120);
            this.panelCenter.Name = "panelCenter";
            this.panelCenter.Padding = new System.Windows.Forms.Padding(20);
            this.panelCenter.Size = new System.Drawing.Size(800, 380);
            this.panelCenter.TabIndex = 1;
            
            
            
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCantidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblCantidad.ForeColor = System.Drawing.Color.Gray;
            this.lblCantidad.Location = new System.Drawing.Point(20, 345);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(63, 15);
            this.lblCantidad.TabIndex = 1;
            this.lblCantidad.Text = "0 Registros";
            
            
            
            this.lstItems.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.lstItems.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstItems.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstItems.ForeColor = System.Drawing.Color.White;
            this.lstItems.FormattingEnabled = true;
            this.lstItems.ItemHeight = 17;
            this.lstItems.Location = new System.Drawing.Point(20, 20);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(760, 340);
            this.lstItems.TabIndex = 0;
            
            
            
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(34)))));
            this.panelButtons.Controls.Add(this.btnGestionSLA);
            this.panelButtons.Controls.Add(this.btnEliminar);
            this.panelButtons.Controls.Add(this.btnEditar);
            this.panelButtons.Controls.Add(this.btnAgregar);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 500);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(20);
            this.panelButtons.Size = new System.Drawing.Size(800, 80);
            this.panelButtons.TabIndex = 2;
            
            
            
            this.btnGestionSLA.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(101)))), ((int)(((byte)(242)))));
            this.btnGestionSLA.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGestionSLA.FlatAppearance.BorderSize = 0;
            this.btnGestionSLA.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGestionSLA.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGestionSLA.ForeColor = System.Drawing.Color.White;
            this.btnGestionSLA.Location = new System.Drawing.Point(573, 20);
            this.btnGestionSLA.Name = "btnGestionSLA";
            this.btnGestionSLA.Size = new System.Drawing.Size(215, 48);
            this.btnGestionSLA.TabIndex = 3;
            this.btnGestionSLA.Text = "Gestion Avanzada SLA";
            this.btnGestionSLA.UseVisualStyleBackColor = false;
            this.btnGestionSLA.Visible = false;
            
            
            
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(280, 20);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(120, 40);
            this.btnEliminar.TabIndex = 2;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            
            
            
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(150, 20);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(120, 40);
            this.btnEditar.TabIndex = 1;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = false;
            
            
            
            this.btnAgregar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.btnAgregar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAgregar.FlatAppearance.BorderSize = 0;
            this.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAgregar.ForeColor = System.Drawing.Color.White;
            this.btnAgregar.Location = new System.Drawing.Point(20, 20);
            this.btnAgregar.Name = "btnAgregar";
            this.btnAgregar.Size = new System.Drawing.Size(120, 40);
            this.btnAgregar.TabIndex = 0;
            this.btnAgregar.Text = "Agregar";
            this.btnAgregar.UseVisualStyleBackColor = false;
            
            
            
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(800, 580);
            this.Controls.Add(this.panelCenter);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelTop);
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "FormGestionMaestros";
            this.Text = "Gestion de Maestros";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelCenter.ResumeLayout(false);
            this.panelCenter.PerformLayout();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblSeleccioneTipo;
        private System.Windows.Forms.ComboBox cmbTipoMaestro;
        private System.Windows.Forms.Panel panelCenter;
        private System.Windows.Forms.ListBox lstItems;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnAgregar;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnEliminar;
        private System.Windows.Forms.Button btnGestionSLA;
    }
}
