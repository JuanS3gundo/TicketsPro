namespace TicketPro.Tecnico
{
    partial class FormGestionBackUp
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
            this.dgvBackups = new System.Windows.Forms.DataGridView();
            this.colBase = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNombre = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colFecha = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colRuta = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnBackupTickets = new System.Windows.Forms.Button();
            this.btnBackupServices = new System.Windows.Forms.Button();
            this.btnRestaurar = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackups)).BeginInit();
            this.SuspendLayout();
            this.dgvBackups.AllowUserToAddRows = false;
            this.dgvBackups.AllowUserToDeleteRows = false;
            this.dgvBackups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBackups.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(55)))), ((int)(((byte)(70)))));
            this.dgvBackups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBackups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colBase,
            this.colNombre,
            this.colFecha,
            this.colRuta});
            this.dgvBackups.Location = new System.Drawing.Point(20, 20);
            this.dgvBackups.MultiSelect = false;
            this.dgvBackups.Name = "dgvBackups";
            this.dgvBackups.ReadOnly = true;
            this.dgvBackups.RowHeadersVisible = false;
            this.dgvBackups.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBackups.Size = new System.Drawing.Size(820, 310);
            this.dgvBackups.TabIndex = 0;
            this.colBase.HeaderText = "Base";
            this.colBase.Name = "colBase";
            this.colBase.ReadOnly = true;
            this.colNombre.HeaderText = "Archivo";
            this.colNombre.Name = "colNombre";
            this.colNombre.ReadOnly = true;
            this.colNombre.Width = 250;
            this.colFecha.HeaderText = "Fecha";
            this.colFecha.Name = "colFecha";
            this.colFecha.ReadOnly = true;
            this.colFecha.Width = 170;
            this.colRuta.HeaderText = "Ruta";
            this.colRuta.Name = "colRuta";
            this.colRuta.ReadOnly = true;
            this.colRuta.Width = 300;
            this.btnBackupTickets.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBackupTickets.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackupTickets.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackupTickets.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBackupTickets.Location = new System.Drawing.Point(20, 340);
            this.btnBackupTickets.Name = "btnBackupTickets";
            this.btnBackupTickets.Size = new System.Drawing.Size(150, 35);
            this.btnBackupTickets.TabIndex = 1;
            this.btnBackupTickets.Tag = "Backup_Tickets";
            this.btnBackupTickets.Text = "Backup TicketsPro";
            this.btnBackupTickets.UseVisualStyleBackColor = true;
            this.btnBackupTickets.Click += new System.EventHandler(this.btnBackupTickets_Click);
            this.btnBackupServices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnBackupServices.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBackupServices.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBackupServices.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnBackupServices.Location = new System.Drawing.Point(190, 340);
            this.btnBackupServices.Name = "btnBackupServices";
            this.btnBackupServices.Size = new System.Drawing.Size(150, 35);
            this.btnBackupServices.TabIndex = 2;
            this.btnBackupServices.Tag = "Backup_Services";
            this.btnBackupServices.Text = "Backup ServicesPP";
            this.btnBackupServices.UseVisualStyleBackColor = true;
            this.btnBackupServices.Click += new System.EventHandler(this.btnBackupServices_Click);
            this.btnRestaurar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRestaurar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRestaurar.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRestaurar.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnRestaurar.Location = new System.Drawing.Point(360, 340);
            this.btnRestaurar.Name = "btnRestaurar";
            this.btnRestaurar.Size = new System.Drawing.Size(150, 35);
            this.btnRestaurar.TabIndex = 3;
            this.btnRestaurar.Tag = "Restore_Backup";
            this.btnRestaurar.Text = "Restaurar Backup";
            this.btnRestaurar.UseVisualStyleBackColor = true;
            this.btnRestaurar.Click += new System.EventHandler(this.btnRestaurar_Click);
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefresh.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnRefresh.Location = new System.Drawing.Point(530, 340);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(120, 35);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Tag = "Refresh";
            this.btnRefresh.Text = "Actualizar";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            this.btnOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnOpenFolder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenFolder.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnOpenFolder.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnOpenFolder.Location = new System.Drawing.Point(660, 340);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(180, 35);
            this.btnOpenFolder.TabIndex = 5;
            this.btnOpenFolder.Tag = "Open_Backup_Folder";
            this.btnOpenFolder.Text = "Abrir carpeta";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(45)))), ((int)(((byte)(60)))));
            this.ClientSize = new System.Drawing.Size(860, 400);
            this.Controls.Add(this.btnOpenFolder);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnRestaurar);
            this.Controls.Add(this.btnBackupServices);
            this.Controls.Add(this.btnBackupTickets);
            this.Controls.Add(this.dgvBackups);
            this.Name = "FormGestionBackUp";
            this.Tag = "Gestion_Backups";
            this.Text = "Gestion de Backups";
            this.Load += new System.EventHandler(this.FormGestionBackUp_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBackups)).EndInit();
            this.ResumeLayout(false);
        }
        #endregion
        private System.Windows.Forms.DataGridView dgvBackups;
        private System.Windows.Forms.Button btnBackupTickets;
        private System.Windows.Forms.Button btnBackupServices;
        private System.Windows.Forms.Button btnRestaurar;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBase;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNombre;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colRuta;
        private System.Windows.Forms.Button btnOpenFolder;
    }
}
