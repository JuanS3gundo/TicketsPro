using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TicketPro.MVP.DTOs;
using TicketPro.MVP.Presenters;
using TicketPro.MVP.Views;
namespace TicketPro.Tecnico
{
    public partial class FormGestionBackUp : BaseForm, IFormGestionBackUpView
    {
        private FormGestionBackUpPresenter _presenter;
        public FormGestionBackUp()
        {
            InitializeComponent();
            _presenter = new FormGestionBackUpPresenter(this);
            this.Load += FormGestionBackUp_Load;
            btnBackupTickets.Click += btnBackupTickets_Click;
            btnBackupServices.Click += btnBackupServices_Click;
            btnRestaurar.Click += btnRestaurar_Click;
            btnOpenFolder.Click += btnOpenFolder_Click;
            btnRefresh.Click += btnRefresh_Click;
        }
        private void FormGestionBackUp_Load(object sender, EventArgs e)
        {
            _presenter.CargarBackups();
        }
        public void MostrarBackups(IEnumerable<BackupDTO> backups)
        {
            dgvBackups.Rows.Clear();
            foreach (var dto in backups)
            {
                dgvBackups.Rows.Add(
                    dto.BaseName,
                    dto.FileName,
                    dto.CreationTime,
                    dto.FullPath
                );
            }
        }
        public BackupDTO BackupSeleccionado
        {
            get
            {
                if (dgvBackups.SelectedRows.Count == 0) return null;
                return new BackupDTO
                {
                    BaseName = dgvBackups.SelectedRows[0].Cells["colBase"].Value?.ToString(),
                    FullPath = dgvBackups.SelectedRows[0].Cells["colRuta"].Value?.ToString()
                };
            }
        }
        private void btnBackupTickets_Click(object sender, EventArgs e) => _presenter.HacerBackupTickets();
        private void btnBackupServices_Click(object sender, EventArgs e) => _presenter.HacerBackupServices();
        private void btnRestaurar_Click(object sender, EventArgs e) => _presenter.RestaurarBackup();
        private void btnOpenFolder_Click(object sender, EventArgs e) => _presenter.AbrirCarpetaBackups();
        private void btnRefresh_Click(object sender, EventArgs e) => _presenter.CargarBackups();
        public void MostrarMensajeExito(string mensaje, string titulo = "exito")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void MostrarMensajeAdvertencia(string mensaje, string titulo = "Advertencia")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public new void MostrarError(string mensaje, Exception ex = null)
        {
            base.MostrarError(mensaje, ex);
        }
        public bool ConfirmarAccion(string mensaje, string titulo)
        {
            return MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;
        }
        public void AbrirCarpeta(string ruta)
        {
            System.Diagnostics.Process.Start("explorer.exe", ruta);
        }
    }
}
