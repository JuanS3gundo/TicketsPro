using BLL;
using Services.BLL;
using Services.DomainModel;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro.Tecnico
{
    public partial class VistaBitacora : BaseForm, IVistaBitacoraView
    {
        private string _colFechaKey, _colUsuarioKey, _colAccionKey, _colDetalleKey, _colNivelKey;
        private const int LIMITE_REGISTROS_DEFAULT = 1000;
        private readonly VistaBitacoraPresenter _presenter;
        public VistaBitacora()
        {
            InitializeComponent();
            CargarClavesTraduccion();
            ConfigurarTraducciones();
            ConfigurarControles();
            _presenter = new VistaBitacoraPresenter(this);
            _presenter.Iniciar();
        }
        private void CargarClavesTraduccion()
        {
            _colFechaKey = LanguageBLL.Translate("Bitacora_Col_Fecha");
            _colUsuarioKey = LanguageBLL.Translate("Bitacora_Col_Usuario");
            _colAccionKey = LanguageBLL.Translate("Bitacora_Col_Accion");
            _colDetalleKey = LanguageBLL.Translate("Bitacora_Col_Detalle");
            _colNivelKey = LanguageBLL.Translate("Bitacora_Col_Nivel");
        }
        private void ConfigurarTraducciones()
        {
            lblUsuario.Tag = "Bitacora_Filtro_Usuario";
            lblNivel.Tag = "Bitacora_Filtro_Nivel";
            lblAccion.Tag = "Bitacora_Filtro_Accion";
            lblDesde.Tag = "Bitacora_Filtro_Desde";
            lblHasta.Tag = "Bitacora_Filtro_Hasta";
            btnLimpiar.Tag = "Bitacora_Btn_Limpiar";
            this.RefreshLanguage();
        }
        private void ConfigurarControles()
        {
            dgvBitacora.AutoGenerateColumns = true;
            dgvBitacora.AllowUserToAddRows = false;
            dgvBitacora.AllowUserToDeleteRows = false;
            dgvBitacora.ReadOnly = true;
            dgvBitacora.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBitacora.RowHeadersVisible = false;
            dgvBitacora.EnableHeadersVisualStyles = false;
            dgvBitacora.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvBitacora.MultiSelect = false;
            dgvBitacora.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dgvBitacora.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(35, 35, 50);
            dgvBitacora.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvBitacora.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvBitacora.ColumnHeadersDefaultCellStyle.Padding = new Padding(8);
            dgvBitacora.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvBitacora.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvBitacora.ColumnHeadersHeight = 50;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvBitacora.DefaultCellStyle.BackColor = Color.FromArgb(55, 55, 70);
            dgvBitacora.DefaultCellStyle.ForeColor = Color.White;
            dgvBitacora.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgvBitacora.DefaultCellStyle.SelectionBackColor = Color.FromArgb(88, 101, 242);
            dgvBitacora.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvBitacora.DefaultCellStyle.Padding = new Padding(5);
            dgvBitacora.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(60, 60, 75);
            dgvBitacora.RowTemplate.Height = 35;
            dgvBitacora.BorderStyle = BorderStyle.None;
            dgvBitacora.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvBitacora.GridColor = Color.FromArgb(45, 45, 60);
            txtUsuario.KeyDown += Filtro_KeyDown;
            txtAccion.KeyDown += Filtro_KeyDown;
            cboNivel.SelectedIndexChanged += Filtro_ValueChanged;
            dtpDesde.ValueChanged += Filtro_ValueChanged;
            dtpHasta.ValueChanged += Filtro_ValueChanged;
            dgvBitacora.CellMouseEnter += DgvBitacora_CellMouseEnter;
        }
        public DateTime FechaDesde { get => dtpDesde.Value; set => dtpDesde.Value = value; }
        public DateTime FechaHasta { get => dtpHasta.Value; set => dtpHasta.Value = value; }
        public string UsuarioFiltro { get => string.IsNullOrWhiteSpace(txtUsuario.Text) ? null : txtUsuario.Text.Trim(); set => txtUsuario.Text = value; }
        public string NivelFiltro { get => string.IsNullOrWhiteSpace(cboNivel.Text) ? null : cboNivel.Text; set => cboNivel.Text = value; }
        public string AccionFiltro { get => string.IsNullOrWhiteSpace(txtAccion.Text) ? null : txtAccion.Text.Trim(); set => txtAccion.Text = value; }
        public int LimiteRegistros => LIMITE_REGISTROS_DEFAULT;
        public object DatosBitacora { get => dgvBitacora.DataSource; set => dgvBitacora.DataSource = value; }
        public void CargarNiveles(string[] niveles)
        {
            cboNivel.Items.Clear();
            cboNivel.Items.AddRange(niveles);
        }
        public void ConfigurarEstilosYColumnasGrid()
        {
            if (dgvBitacora.Columns["Id"] != null) dgvBitacora.Columns["Id"].Visible = false;
            if (dgvBitacora.Columns["EquipoId"] != null) dgvBitacora.Columns["EquipoId"].Visible = false;
            if (dgvBitacora.Columns["TicketId"] != null) dgvBitacora.Columns["TicketId"].Visible = false;
            if (dgvBitacora.Columns["Fecha"] != null)
            {
                dgvBitacora.Columns["Fecha"].HeaderText = _colFechaKey;
                dgvBitacora.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm:ss";
                dgvBitacora.Columns["Fecha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvBitacora.Columns["Fecha"].Width = 170;
                dgvBitacora.Columns["Fecha"].FillWeight = 50;
            }
            if (dgvBitacora.Columns["Usuario"] != null)
            {
                dgvBitacora.Columns["Usuario"].HeaderText = _colUsuarioKey;
                dgvBitacora.Columns["Usuario"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvBitacora.Columns["Usuario"].Width = 140;
                dgvBitacora.Columns["Usuario"].FillWeight = 50;
            }
            if (dgvBitacora.Columns["Accion"] != null)
            {
                dgvBitacora.Columns["Accion"].HeaderText = _colAccionKey;
                dgvBitacora.Columns["Accion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvBitacora.Columns["Accion"].Width = 220;
                dgvBitacora.Columns["Accion"].FillWeight = 80;
            }
            if (dgvBitacora.Columns["Nivel"] != null)
            {
                dgvBitacora.Columns["Nivel"].HeaderText = _colNivelKey;
                dgvBitacora.Columns["Nivel"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgvBitacora.Columns["Nivel"].Width = 100;
                dgvBitacora.Columns["Nivel"].FillWeight = 30;
                dgvBitacora.Columns["Nivel"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            if (dgvBitacora.Columns["Detalle"] != null)
            {
                dgvBitacora.Columns["Detalle"].HeaderText = _colDetalleKey;
                dgvBitacora.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvBitacora.Columns["Detalle"].MinimumWidth = 300;
                dgvBitacora.Columns["Detalle"].FillWeight = 400;
                dgvBitacora.Columns["Detalle"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvBitacora.Columns["Detalle"].DefaultCellStyle.Padding = new Padding(5, 8, 5, 8);
            }
        }
        public void ColorizarFilasGrid()
        {
            foreach (DataGridViewRow row in dgvBitacora.Rows)
            {
                if (row.Cells["Nivel"] != null && row.Cells["Nivel"].Value != null)
                {
                    string nivel_row = row.Cells["Nivel"].Value.ToString();
                    if (nivel_row == "ERROR")
                    {
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 100, 100);
                        row.DefaultCellStyle.SelectionForeColor = Color.White;
                    }
                    else if (nivel_row == "WARNING")
                    {
                        row.DefaultCellStyle.ForeColor = Color.FromArgb(255, 193, 7);
                        row.DefaultCellStyle.SelectionForeColor = Color.White;
                    }
                }
            }
        }
        public void ActualizarContadorInfo(int totalRegistros, int limite)
        {
            string textoContador = LanguageBLL.Translate("Bitacora_Total_Registros");
            if (totalRegistros >= limite)
            {
                string msjLimite = string.Format(LanguageBLL.Translate("Bitacora_Mostrando_Ultimos"), limite);
                lblContador.Text = $"{textoContador}: {totalRegistros} {msjLimite}";
                lblContador.ForeColor = Color.FromArgb(255, 193, 7);
            }
            else
            {
                lblContador.Text = $"{textoContador}: {totalRegistros}";
                lblContador.ForeColor = Color.White;
            }
        }
        public void MostrarMensajeValidacion(string msj, string titulo)
        {
            MessageBox.Show(msj, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public new void MostrarError(string mensaje, Exception ex = null)
        {
            base.MostrarError(mensaje, ex);
        }
        private void pnlFiltros_Paint(object sender, PaintEventArgs e)
        {
        }
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            _presenter.LimpiarFiltros();
        }
        private void Filtro_ValueChanged(object sender, EventArgs e)
        {
            if (_presenter != null) 
                _presenter.CargarDatos();
        }
        private void Filtro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; 
                if (_presenter != null)
                    _presenter.CargarDatos();
            }
        }
        private void DgvBitacora_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                var columnName = dgvBitacora.Columns[e.ColumnIndex].Name;
                if (columnName == "Detalle")
                {
                    var cellValue = dgvBitacora.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if (cellValue != null && !string.IsNullOrWhiteSpace(cellValue.ToString()))
                    {
                        dgvBitacora.Rows[e.RowIndex].Cells[e.ColumnIndex].ToolTipText = cellValue.ToString();
                    }
                }
            }
        }
    }
}
