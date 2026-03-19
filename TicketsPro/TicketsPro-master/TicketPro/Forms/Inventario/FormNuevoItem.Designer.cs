namespace TicketPro.Tecnico
{
    partial class FormNuevoItem
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblCodigo;
        private System.Windows.Forms.Label lblNombre;
        private System.Windows.Forms.Label lblCategoria;
        private System.Windows.Forms.Label lblCantidad;
        private System.Windows.Forms.Label lblUbicacion;
        private System.Windows.Forms.Label lblDescripcion;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtNombre;
        private System.Windows.Forms.ComboBox cmbCategoria;
        private System.Windows.Forms.NumericUpDown numCantidad;
        private System.Windows.Forms.TextBox txtUbicacion;
        private System.Windows.Forms.CheckBox chkDisponible;
        private System.Windows.Forms.TextBox txtDescripcion;
        private System.Windows.Forms.Button btnGuardar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Label lblValor;
        private System.Windows.Forms.Label lblUnidad;
        private System.Windows.Forms.ComboBox cmbValor;
        private System.Windows.Forms.ComboBox cmbUnidad;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormNuevoItem));
            this.lblCodigo = new System.Windows.Forms.Label();
            this.lblNombre = new System.Windows.Forms.Label();
            this.lblCategoria = new System.Windows.Forms.Label();
            this.lblCantidad = new System.Windows.Forms.Label();
            this.lblUbicacion = new System.Windows.Forms.Label();
            this.lblDescripcion = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtNombre = new System.Windows.Forms.TextBox();
            this.cmbCategoria = new System.Windows.Forms.ComboBox();
            this.numCantidad = new System.Windows.Forms.NumericUpDown();
            this.txtUbicacion = new System.Windows.Forms.TextBox();
            this.chkDisponible = new System.Windows.Forms.CheckBox();
            this.txtDescripcion = new System.Windows.Forms.TextBox();
            this.btnGuardar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.lblValor = new System.Windows.Forms.Label();
            this.lblUnidad = new System.Windows.Forms.Label();
            this.cmbValor = new System.Windows.Forms.ComboBox();
            this.cmbUnidad = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).BeginInit();
            this.SuspendLayout();
            this.lblCodigo.AutoSize = true;
            this.lblCodigo.Location = new System.Drawing.Point(20, 20);
            this.lblCodigo.Name = "lblCodigo";
            this.lblCodigo.Size = new System.Drawing.Size(93, 13);
            this.lblCodigo.TabIndex = 0;
            this.lblCodigo.Text = "Inventario_NuevoItem_LabelCodigo"; 
            this.lblNombre.AutoSize = true;
            this.lblNombre.Location = new System.Drawing.Point(20, 70);
            this.lblNombre.Name = "lblNombre";
            this.lblNombre.Size = new System.Drawing.Size(47, 13);
            this.lblNombre.TabIndex = 2;
            this.lblNombre.Text = "Inventario_NuevoItem_LabelNombre"; 
            this.lblCategoria.AutoSize = true;
            this.lblCategoria.Location = new System.Drawing.Point(20, 120);
            this.lblCategoria.Name = "lblCategoria";
            this.lblCategoria.Size = new System.Drawing.Size(57, 13);
            this.lblCategoria.TabIndex = 4;
            this.lblCategoria.Text = "Inventario_NuevoItem_LabelCategoria"; 
            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Location = new System.Drawing.Point(20, 170);
            this.lblCantidad.Name = "lblCantidad";
            this.lblCantidad.Size = new System.Drawing.Size(52, 13);
            this.lblCantidad.TabIndex = 6;
            this.lblCantidad.Text = "Inventario_NuevoItem_LabelCantidad"; 
            this.lblUbicacion.AutoSize = true;
            this.lblUbicacion.Location = new System.Drawing.Point(20, 220);
            this.lblUbicacion.Name = "lblUbicacion";
            this.lblUbicacion.Size = new System.Drawing.Size(58, 13);
            this.lblUbicacion.TabIndex = 12;
            this.lblUbicacion.Text = "Inventario_NuevoItem_LabelUbicacion"; 
            this.lblDescripcion.AutoSize = true;
            this.lblDescripcion.Location = new System.Drawing.Point(20, 300);
            this.lblDescripcion.Name = "lblDescripcion";
            this.lblDescripcion.Size = new System.Drawing.Size(66, 13);
            this.lblDescripcion.TabIndex = 15;
            this.lblDescripcion.Text = "Inventario_NuevoItem_LabelDescripcion"; 
            this.txtCodigo.Location = new System.Drawing.Point(20, 40);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(320, 20);
            this.txtCodigo.TabIndex = 1;
            this.txtNombre.Location = new System.Drawing.Point(20, 90);
            this.txtNombre.Name = "txtNombre";
            this.txtNombre.Size = new System.Drawing.Size(320, 20);
            this.txtNombre.TabIndex = 3;
            this.cmbCategoria.Location = new System.Drawing.Point(20, 140);
            this.cmbCategoria.Name = "cmbCategoria";
            this.cmbCategoria.Size = new System.Drawing.Size(320, 21);
            this.cmbCategoria.TabIndex = 5;
            this.numCantidad.Location = new System.Drawing.Point(20, 190);
            this.numCantidad.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numCantidad.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numCantidad.Name = "numCantidad";
            this.numCantidad.Size = new System.Drawing.Size(80, 20);
            this.numCantidad.TabIndex = 7;
            this.numCantidad.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtUbicacion.Location = new System.Drawing.Point(20, 240);
            this.txtUbicacion.Name = "txtUbicacion";
            this.txtUbicacion.Size = new System.Drawing.Size(320, 20);
            this.txtUbicacion.TabIndex = 13;
            this.chkDisponible.Checked = true;
            this.chkDisponible.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkDisponible.Location = new System.Drawing.Point(20, 270);
            this.chkDisponible.Name = "chkDisponible";
            this.chkDisponible.Size = new System.Drawing.Size(104, 24);
            this.chkDisponible.TabIndex = 14;
            this.chkDisponible.Text = "Inventario_NuevoItem_LabelDisponible"; 
            this.txtDescripcion.Location = new System.Drawing.Point(20, 320);
            this.txtDescripcion.Multiline = true;
            this.txtDescripcion.Name = "txtDescripcion";
            this.txtDescripcion.Size = new System.Drawing.Size(360, 60);
            this.txtDescripcion.TabIndex = 16;
            this.btnGuardar.Location = new System.Drawing.Point(80, 390);
            this.btnGuardar.Name = "btnGuardar";
            this.btnGuardar.Size = new System.Drawing.Size(100, 30);
            this.btnGuardar.TabIndex = 17;
            this.btnGuardar.Text = "Inventario_NuevoItem_BotonGuardar"; 
            this.btnCancelar.Location = new System.Drawing.Point(200, 390);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.TabIndex = 18;
            this.btnCancelar.Text = "Inventario_NuevoItem_BotonCancelar"; 
            this.lblValor.AutoSize = true;
            this.lblValor.Location = new System.Drawing.Point(110, 170);
            this.lblValor.Name = "lblValor";
            this.lblValor.Size = new System.Drawing.Size(34, 13);
            this.lblValor.TabIndex = 8;
            this.lblValor.Text = "Inventario_NuevoItem_LabelValor"; 
            this.lblUnidad.AutoSize = true;
            this.lblUnidad.Location = new System.Drawing.Point(210, 170);
            this.lblUnidad.Name = "lblUnidad";
            this.lblUnidad.Size = new System.Drawing.Size(44, 13);
            this.lblUnidad.TabIndex = 10;
            this.lblUnidad.Text = "Inventario_NuevoItem_LabelUnidad"; 
            this.cmbValor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbValor.Location = new System.Drawing.Point(110, 190);
            this.cmbValor.Name = "cmbValor";
            this.cmbValor.Size = new System.Drawing.Size(80, 21);
            this.cmbValor.TabIndex = 9;
            this.cmbUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnidad.Location = new System.Drawing.Point(210, 190);
            this.cmbUnidad.Name = "cmbUnidad";
            this.cmbUnidad.Size = new System.Drawing.Size(100, 21);
            this.cmbUnidad.TabIndex = 11;
            this.ClientSize = new System.Drawing.Size(420, 440);
            this.Controls.Add(this.lblCodigo);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.lblNombre);
            this.Controls.Add(this.txtNombre);
            this.Controls.Add(this.lblCategoria);
            this.Controls.Add(this.cmbCategoria);
            this.Controls.Add(this.lblCantidad);
            this.Controls.Add(this.numCantidad);
            this.Controls.Add(this.lblValor);
            this.Controls.Add(this.cmbValor);
            this.Controls.Add(this.lblUnidad);
            this.Controls.Add(this.cmbUnidad);
            this.Controls.Add(this.lblUbicacion);
            this.Controls.Add(this.txtUbicacion);
            this.Controls.Add(this.chkDisponible);
            this.Controls.Add(this.lblDescripcion);
            this.Controls.Add(this.txtDescripcion);
            this.Controls.Add(this.btnGuardar);
            this.Controls.Add(this.btnCancelar);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormNuevoItem";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inventario_NuevoItem_Form_Titulo"; 
            ((System.ComponentModel.ISupportInitialize)(this.numCantidad)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
