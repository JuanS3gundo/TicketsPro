using Services.BLL;
using Services.Services;
using System;
using System.Drawing;
using System.Windows.Forms;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
using TicketPro.Temas;
namespace TicketPro.Inventario
{
    public partial class FormHistorialEquipo : BaseForm, IFormHistorialEquipoView
    {
        private FormHistorialEquipoPresenter _presenter;
        public FormHistorialEquipo(int equipoId)
        {
            InitializeComponent();
            _equipoId = equipoId;
            _presenter = new FormHistorialEquipoPresenter(this);
        }
        private readonly int _equipoId;
        public int EquipoId => _equipoId;
        public void EstablecerSubtitulo(string subtitulo)
        {
            lblSubtitulo.Text = subtitulo;
        }
        public void MostrarHistorial(object historial)
        {
            dgvHistorial.DataSource = historial;
            ConfigurarDataGridView();
            TraducirColumnas();
        }
        public void MostrarError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void FormHistorialEquipo_Load(object sender, EventArgs e)
        {
            Theme.Apply(this);
            Theme.ButtonNeutral(btnCerrar);
            _presenter.Iniciar();
        }
        private void ConfigurarDataGridView()
        {
            dgvHistorial.EnableHeadersVisualStyles = false;
            dgvHistorial.BackgroundColor = Color.FromArgb(20, 25, 35); 
            dgvHistorial.GridColor = Color.FromArgb(40, 50, 70);       
            dgvHistorial.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 30, 45); 
            dgvHistorial.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistorial.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvHistorial.DefaultCellStyle.BackColor = Color.FromArgb(20, 25, 35); 
            dgvHistorial.DefaultCellStyle.ForeColor = Color.White;
            dgvHistorial.DefaultCellStyle.Font = new Font("Segoe UI", 9.5F, FontStyle.Regular); 
            dgvHistorial.DefaultCellStyle.SelectionBackColor = Color.FromArgb(40, 150, 255);
            dgvHistorial.DefaultCellStyle.SelectionForeColor = Color.White;
            dgvHistorial.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(25, 30, 45);
            dgvHistorial.CellBorderStyle = DataGridViewCellBorderStyle.None;
            dgvHistorial.RowHeadersVisible = false; 
            dgvHistorial.RowTemplate.Height = 35; 
            dgvHistorial.ColumnHeadersHeight = 40;
            dgvHistorial.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvHistorial.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgvHistorial.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            if (dgvHistorial.Columns["Id"] != null)
                dgvHistorial.Columns["Id"].Visible = false;
            if (dgvHistorial.Columns["EquipoId"] != null)
                dgvHistorial.Columns["EquipoId"].Visible = false;
            if (dgvHistorial.Columns.Count > 0)
            {
                foreach (DataGridViewColumn col in dgvHistorial.Columns)
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    col.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
                }
            }
            if (dgvHistorial.Columns["Detalle"] != null)
            {
                dgvHistorial.Columns["Detalle"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvHistorial.Columns["Detalle"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvHistorial.Columns["Detalle"].MinimumWidth = 350;
                dgvHistorial.Columns["Detalle"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft; 
                dgvHistorial.Columns["Detalle"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }
            if (dgvHistorial.Columns["Fecha"] != null)
                dgvHistorial.Columns["Fecha"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
        }
        private void TraducirColumnas()
        {
            if (dgvHistorial.Columns["Id"] != null)
                dgvHistorial.Columns["Id"].HeaderText = LanguageBLL.Translate("Historial_Col_Id");
            if (dgvHistorial.Columns["Fecha"] != null)
                dgvHistorial.Columns["Fecha"].HeaderText = LanguageBLL.Translate("Historial_Col_Fecha");
            if (dgvHistorial.Columns["Usuario"] != null)
                dgvHistorial.Columns["Usuario"].HeaderText = LanguageBLL.Translate("Historial_Col_Usuario");
            if (dgvHistorial.Columns["Accion"] != null)
                dgvHistorial.Columns["Accion"].HeaderText = LanguageBLL.Translate("Historial_Col_Accion");
            if (dgvHistorial.Columns["Detalle"] != null)
                dgvHistorial.Columns["Detalle"].HeaderText = LanguageBLL.Translate("Historial_Col_Detalle");
            if (dgvHistorial.Columns["Nivel"] != null)
                dgvHistorial.Columns["Nivel"].HeaderText = LanguageBLL.Translate("Historial_Col_Nivel");
            if (dgvHistorial.Columns["EquipoId"] != null)
                dgvHistorial.Columns["EquipoId"].HeaderText = LanguageBLL.Translate("Historial_Col_EquipoId");
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
