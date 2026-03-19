namespace TicketPro.Tecnico
{
    partial class FormGestionSLA
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
            this.btnCerrar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelLeft = new System.Windows.Forms.Panel();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lstPoliticas = new System.Windows.Forms.ListBox();
            this.panelRight = new System.Windows.Forms.Panel();
            this.chkSoloHorasLaborales = new System.Windows.Forms.CheckBox();
            this.numHorasResolucion = new System.Windows.Forms.NumericUpDown();
            this.lblHorasResolucion = new System.Windows.Forms.Label();
            this.numHorasAtencion = new System.Windows.Forms.NumericUpDown();
            this.lblHorasAtencion = new System.Windows.Forms.Label();
            this.cmbPrioridad = new System.Windows.Forms.ComboBox();
            this.lblPrioridad = new System.Windows.Forms.Label();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.lblNombre = new System.Windows.Forms.Label();
            this.panelButtons = new System.Windows.Forms.Panel();
            this.btnEliminar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnNuevo = new System.Windows.Forms.Button();
            this.panelTop.SuspendLayout();
            this.panelLeft.SuspendLayout();
            this.panelRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHorasResolucion)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHorasAtencion)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.SuspendLayout();
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(34)))));
            this.panelTop.Controls.Add(this.btnCerrar);
            this.panelTop.Controls.Add(this.btnMinimizar);
            this.panelTop.Controls.Add(this.lblTitulo);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(900, 70);
            this.panelTop.TabIndex = 0;
            this.btnCerrar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCerrar.BackColor = System.Drawing.Color.Transparent;
            this.btnCerrar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCerrar.FlatAppearance.BorderSize = 0;
            this.btnCerrar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCerrar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnCerrar.ForeColor = System.Drawing.Color.White;
            this.btnCerrar.Location = new System.Drawing.Point(860, 10);
            this.btnCerrar.Name = "btnCerrar";
            this.btnCerrar.Size = new System.Drawing.Size(30, 30);
            this.btnCerrar.TabIndex = 2;
            this.btnCerrar.Text = "X";
            this.btnCerrar.UseVisualStyleBackColor = false;
            this.btnCerrar.Click += new System.EventHandler(this.BtnCerrar_Click);
            this.btnMinimizar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMinimizar.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimizar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(70)))), ((int)(((byte)(70)))), ((int)(((byte)(80)))));
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(825, 10);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(30, 30);
            this.btnMinimizar.TabIndex = 1;
            this.btnMinimizar.Text = "-";
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.BtnMinimizar_Click);
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(20, 20);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(182, 30);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "SLA_Form_Titulo";
            this.panelLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.panelLeft.Controls.Add(this.lblCantidad);
            this.panelLeft.Controls.Add(this.lstPoliticas);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 70);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Padding = new System.Windows.Forms.Padding(20);
            this.panelLeft.Size = new System.Drawing.Size(350, 530);
            this.panelLeft.TabIndex = 1;
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCantidad.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblCantidad.ForeColor = System.Drawing.Color.Gray;
            this.lblCantidad.Location = new System.Drawing.Point(20, 495);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(82, 15);
            this.lblCantidad.TabIndex = 1;
            this.lblCantidad.Text = "0 politicas SLA";
            this.lstPoliticas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.lstPoliticas.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lstPoliticas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstPoliticas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lstPoliticas.ForeColor = System.Drawing.Color.White;
            this.lstPoliticas.FormattingEnabled = true;
            this.lstPoliticas.ItemHeight = 17;
            this.lstPoliticas.Location = new System.Drawing.Point(20, 20);
            this.lstPoliticas.Name = "lstPoliticas";
            this.lstPoliticas.Size = new System.Drawing.Size(310, 490);
            this.lstPoliticas.TabIndex = 0;
            this.panelRight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(50)))), ((int)(((byte)(70)))));
            this.panelRight.Controls.Add(this.chkSoloHorasLaborales);
            this.panelRight.Controls.Add(this.numHorasResolucion);
            this.panelRight.Controls.Add(this.lblHorasResolucion);
            this.panelRight.Controls.Add(this.numHorasAtencion);
            this.panelRight.Controls.Add(this.lblHorasAtencion);
            this.panelRight.Controls.Add(this.cmbPrioridad);
            this.panelRight.Controls.Add(this.lblPrioridad);
            this.panelRight.Controls.Add(this.txtNombre);
            this.panelRight.Controls.Add(this.lblNombre);
            this.panelRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelRight.Location = new System.Drawing.Point(350, 70);
            this.panelRight.Name = "panelRight";
            this.panelRight.Padding = new System.Windows.Forms.Padding(30);
            this.panelRight.Size = new System.Drawing.Size(550, 450);
            this.panelRight.TabIndex = 2;
            this.chkSoloHorasLaborales.AutoSize = true;
            this.chkSoloHorasLaborales.Checked = true;
            this.chkSoloHorasLaborales.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSoloHorasLaborales.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkSoloHorasLaborales.ForeColor = System.Drawing.Color.Gainsboro;
            this.chkSoloHorasLaborales.Location = new System.Drawing.Point(30, 310);
            this.chkSoloHorasLaborales.Name = "chkSoloHorasLaborales";
            this.chkSoloHorasLaborales.Size = new System.Drawing.Size(216, 23);
            this.chkSoloHorasLaborales.TabIndex = 8;
            this.chkSoloHorasLaborales.Text = "SLA_Checkbox_HorasLaborales";
            this.chkSoloHorasLaborales.UseVisualStyleBackColor = true;
            this.numHorasResolucion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.numHorasResolucion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numHorasResolucion.ForeColor = System.Drawing.Color.White;
            this.numHorasResolucion.Location = new System.Drawing.Point(30, 265);
            this.numHorasResolucion.Maximum = new decimal(new int[] {
            720,
            0,
            0,
            0});
            this.numHorasResolucion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHorasResolucion.Name = "numHorasResolucion";
            this.numHorasResolucion.Size = new System.Drawing.Size(200, 25);
            this.numHorasResolucion.TabIndex = 7;
            this.numHorasResolucion.Value = new decimal(new int[] {
            8,
            0,
            0,
            0});
            this.lblHorasResolucion.AutoSize = true;
            this.lblHorasResolucion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHorasResolucion.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblHorasResolucion.Location = new System.Drawing.Point(30, 240);
            this.lblHorasResolucion.Name = "lblHorasResolucion";
            this.lblHorasResolucion.Size = new System.Drawing.Size(177, 19);
            this.lblHorasResolucion.TabIndex = 6;
            this.lblHorasResolucion.Text = "SLA_Label_HorasResolucion";
            this.numHorasAtencion.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.numHorasAtencion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numHorasAtencion.ForeColor = System.Drawing.Color.White;
            this.numHorasAtencion.Location = new System.Drawing.Point(30, 195);
            this.numHorasAtencion.Maximum = new decimal(new int[] {
            168,
            0,
            0,
            0});
            this.numHorasAtencion.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numHorasAtencion.Name = "numHorasAtencion";
            this.numHorasAtencion.Size = new System.Drawing.Size(200, 25);
            this.numHorasAtencion.TabIndex = 5;
            this.numHorasAtencion.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lblHorasAtencion.AutoSize = true;
            this.lblHorasAtencion.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHorasAtencion.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblHorasAtencion.Location = new System.Drawing.Point(30, 170);
            this.lblHorasAtencion.Name = "lblHorasAtencion";
            this.lblHorasAtencion.Size = new System.Drawing.Size(166, 19);
            this.lblHorasAtencion.TabIndex = 4;
            this.lblHorasAtencion.Text = "SLA_Label_HorasAtencion";
            this.cmbPrioridad.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.cmbPrioridad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPrioridad.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbPrioridad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbPrioridad.ForeColor = System.Drawing.Color.White;
            this.cmbPrioridad.FormattingEnabled = true;
            this.cmbPrioridad.Location = new System.Drawing.Point(30, 125);
            this.cmbPrioridad.Name = "cmbPrioridad";
            this.cmbPrioridad.Size = new System.Drawing.Size(490, 25);
            this.cmbPrioridad.TabIndex = 3;
            this.lblPrioridad.AutoSize = true;
            this.lblPrioridad.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPrioridad.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblPrioridad.Location = new System.Drawing.Point(30, 100);
            this.lblPrioridad.Name = "lblPrioridad";
            this.lblPrioridad.Size = new System.Drawing.Size(131, 19);
            this.lblPrioridad.TabIndex = 2;
            this.lblPrioridad.Text = "SLA_Label_Prioridad";
            this.txtNombre.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(80)))));
            this.txtNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.ForeColor = System.Drawing.Color.White;
            this.txtNombre.Location = new System.Drawing.Point(30, 55);
            this.txtNombre.MaxLength = 100;
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(490, 25);
            this.txtNombre.TabIndex = 1;
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNombre.ForeColor = System.Drawing.Color.Gainsboro;
            this.lblNombre.Location = new System.Drawing.Point(30, 30);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(126, 19);
            this.lblNombre.TabIndex = 0;
            this.lblNombre.Text = "SLA_Label_Nombre";
            this.panelButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(31)))), ((int)(((byte)(34)))));
            this.panelButtons.Controls.Add(this.btnEliminar);
            this.panelButtons.Controls.Add(this.btnCancelar);
            this.panelButtons.Controls.Add(this.btnGuardar);
            this.panelButtons.Controls.Add(this.btnNuevo);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(350, 520);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Padding = new System.Windows.Forms.Padding(20);
            this.panelButtons.Size = new System.Drawing.Size(550, 80);
            this.panelButtons.TabIndex = 3;
            this.btnEliminar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnEliminar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnEliminar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEliminar.FlatAppearance.BorderSize = 0;
            this.btnEliminar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEliminar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEliminar.ForeColor = System.Drawing.Color.White;
            this.btnEliminar.Location = new System.Drawing.Point(410, 20);
            this.btnEliminar.Name = "btnEliminar";
            this.btnEliminar.Size = new System.Drawing.Size(120, 40);
            this.btnEliminar.TabIndex = 3;
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.UseVisualStyleBackColor = false;
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnCancelar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(280, 20);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(120, 40);
            this.btnCancelar.TabIndex = 2;
            this.btnCancelar.Text = "Cancelar";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnGuardar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnGuardar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnGuardar.FlatAppearance.BorderSize = 0;
            this.btnGuardar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardar.ForeColor = System.Drawing.Color.White;
            this.btnGuardar.Location = new System.Drawing.Point(150, 20);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(120, 40);
            this.btnGuardar.TabIndex = 1;
            this.btnGuardar.Text = "Guardar";
            this.btnGuardar.UseVisualStyleBackColor = false;
            this.btnNuevo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.btnNuevo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNuevo.FlatAppearance.BorderSize = 0;
            this.btnNuevo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnNuevo.ForeColor = System.Drawing.Color.White;
            this.btnNuevo.Location = new System.Drawing.Point(20, 20);
            this.btnNuevo.Name = "btnNuevo";
            this.btnNuevo.Size = new System.Drawing.Size(120, 40);
            this.btnNuevo.TabIndex = 0;
            this.btnNuevo.Text = "Nuevo";
            this.btnNuevo.UseVisualStyleBackColor = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(900, 600);
            this.Controls.Add(this.panelRight);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelLeft);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MinimumSize = new System.Drawing.Size(300, 300);
            this.Name = "FormGestionSLA";
            this.Text = "SLA_Form_Titulo";
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelLeft.ResumeLayout(false);
            this.panelLeft.PerformLayout();
            this.panelRight.ResumeLayout(false);
            this.panelRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHorasResolucion)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numHorasAtencion)).EndInit();
            this.panelButtons.ResumeLayout(false);
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnCerrar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.ListBox lstPoliticas;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Panel panelRight;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.Label lblPrioridad;
        private System.Windows.Forms.ComboBox cmbPrioridad;
        private System.Windows.Forms.Label lblHorasAtencion;
        private System.Windows.Forms.NumericUpDown numHorasAtencion;
        private System.Windows.Forms.Label lblHorasResolucion;
        private System.Windows.Forms.NumericUpDown numHorasResolucion;
        private System.Windows.Forms.CheckBox chkSoloHorasLaborales;
        private System.Windows.Forms.Panel panelButtons;
        private System.Windows.Forms.Button btnNuevo;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnEliminar;
    }
}
