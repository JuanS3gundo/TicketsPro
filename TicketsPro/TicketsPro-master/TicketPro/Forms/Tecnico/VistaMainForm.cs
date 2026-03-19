using BLL;
using BLL.Implementations;
using Entity.Domain;
using Microsoft.Extensions.DependencyInjection;
using Services.BLL;
using Services.DomainModel;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TicketPro.Tecnico;
using TicketPro.VistaCliente;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro
{
    public partial class VistaMainForm : BaseForm, IVistaMainFormView
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly Guid _idUsuario;
        private Button _botonActivo;
        private readonly Color _colorActivo = Color.FromArgb(70, 70, 90);
        private readonly Color _colorInactivo = Color.FromArgb(45, 45, 60);
        private bool _isUpdatingFilters = false;
        private VistaMainFormPresenter _presenter;
        public VistaMainForm(Guid idUsuario, IServiceProvider serviceProvider)
        {
            _idUsuario = idUsuario;
            _serviceProvider = serviceProvider;
            InitializeComponent();
            typeof(FlowLayoutPanel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic,
                null, flowTickets, new object[] { true });
            ConfigurarTraducciones();
            this.FormClosing += VistaTecnicoForm_FormClosing;
            btnCasos.Click += MenuButton_Click;
            btnCrearTicket.Click += MenuButton_Click;
            this.Resize += (s, e) => AjustarTarjetas();
            _presenter = new VistaMainFormPresenter(
                this,
                serviceProvider.GetRequiredService<BLL.ITicketBLL>(),
                serviceProvider.GetRequiredService<BLL.Interfaces.IEstadoTicketBLL>(),
                serviceProvider.GetRequiredService<BLL.Interfaces.ICategoriaTicketBLL>(),
                serviceProvider.GetRequiredService<BLL.Interfaces.IUbicacionBLL>(),
                serviceProvider.GetRequiredService<BLL.Interfaces.IPrioridadBLL>()
            );
            this.Load += (s, e) =>
            {
                _presenter.Iniciar();
                CargarVistaInicial();
                _presenter.LimpiarFiltros();
            };
            txtBuscar.TextChanged += (s, e) => { if (!_isUpdatingFilters) _presenter.CargarTickets(); };
            txtBuscarId.TextChanged += (s, e) => { if (!_isUpdatingFilters) _presenter.CargarTickets(); };
            cboEstado.SelectedIndexChanged += (s, e) => { if (!_isUpdatingFilters) _presenter.CargarTickets(); };
            cboCategoria.SelectedIndexChanged += (s, e) => { if (!_isUpdatingFilters) _presenter.CargarTickets(); };
            cboUbicacion.SelectedIndexChanged += (s, e) => { if (!_isUpdatingFilters) _presenter.CargarTickets(); };
            cboTecnico.SelectedIndexChanged += (s, e) => { if (!_isUpdatingFilters) _presenter.CargarTickets(); };
            cboPrioridad.SelectedIndexChanged += (s, e) => { if (!_isUpdatingFilters) _presenter.CargarTickets(); };
            cboFiltroSLA.SelectedIndexChanged += (s, e) => { if (!_isUpdatingFilters) _presenter.CargarTickets(); };
            btnLimpiarFiltros.Click += (s, e) => _presenter.LimpiarFiltros();
        }
        public void MostrarAdvertenciaAccesoDenegado()
        {
            MessageBox.Show(
                LanguageBLL.Translate("Msg_Acceso_Denegado_Detalle"), 
                LanguageBLL.Translate("Acceso_Denegado"), 
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning
            );
        }
        public void HabilitarMenu(string menuName, bool habilitado)
        {
            Button btn;
            switch(menuName)
            {
                case "GESTION_ACCESOS": btn = btnGestionAccesos; break;
                case "GESTION_INVENTARIO": btn = btnGestionInventario; break;
                case "GESTION_MAESTROS": btn = btnGestionMaestros; break;
                case "VISUALIZAR_BITACORA": btn = btnBitacora; break;
                case "GESTION_ANALITICAS": btn = btnAnaliticas; break;
                case "GESTION_BACKUP": btn = btnBackup; break;
                default: return;
            }
            if (!habilitado)
            {
                btn.Cursor = Cursors.No;
                btn.Click -= MenuButton_Click;
                btn.Click -= btnGestionInventario_Click;
                btn.Click += (s, e) => _presenter.AccionAccesoDenegado();
            }
            else
            {
                btn.Cursor = Cursors.Hand;
                if (btn == btnGestionInventario)
                    btn.Click += btnGestionInventario_Click;
                else
                    btn.Click += MenuButton_Click;
            }
        }
        private void CargarVistaInicial()
        {
            ActivarBoton(btnCasos);
            MostrarCasos();
        }
        private void MenuButton_Click(object sender, EventArgs e)
        {
            var botonPresionado = sender as Button;
            if (botonPresionado == null || botonPresionado == _botonActivo) return;
            ActivarBoton(botonPresionado);
            if (botonPresionado == btnCasos)
                MostrarCasos();
            else if (botonPresionado == btnCrearTicket)
                MostrarCrearTicket();
            else if (botonPresionado == btnGestionAccesos)
            {
                MostrarGestionAccesos();
            }
            else if (botonPresionado == btnGestionMaestros)
            {
                MostrarGestionMaestros();
            }
            else if (botonPresionado == btnBitacora) 
            {
                MostrarBitacora();
            }
            else if (botonPresionado == btnAnaliticas)
            {
                MostrarAnaliticas();
            }
            else if (botonPresionado == btnGestionInventario)
            {
                btnGestionInventario_Click(sender, e);
            }
            else if (botonPresionado == btnBackup) 
            {
                MostrarBackup();
            }
        }
        public new void MostrarError(string mensaje, Exception ex = null)
        {
            base.MostrarError(mensaje, ex);
        }
        public void AplicarPermisos()
        {
            if (_presenter != null) 
            {
                _presenter.Iniciar();
            }
        }
        public void LimpiarFiltrosUI()
        {
            _isUpdatingFilters = true;
            try
            {
                txtBuscar.Clear();
                txtBuscarId.Clear();
                cboEstado.SelectedIndex = -1;
                cboCategoria.SelectedIndex = -1;
                cboUbicacion.SelectedIndex = -1;
                cboTecnico.SelectedIndex = -1;
                cboTecnico.Text = "";
                if (cboPrioridad.Items.Count > 0) cboPrioridad.SelectedIndex = 0; 
                if (cboFiltroSLA.Items.Count > 0) cboFiltroSLA.SelectedIndex = 0; 
            }
            finally
            {
                _isUpdatingFilters = false;
            }
        }
        private void MostrarCasos()
        {
            lblTitulo.Text = LanguageBLL.Translate("Casos");
            panelContenido.Controls.Clear();
            panelContenido.Controls.Add(flowTickets);
            panelContenido.Controls.Add(panelFiltros);
            panelFiltros.Dock = DockStyle.Top;
            flowTickets.Dock = DockStyle.Fill;
            if (_presenter != null) 
            {
                _presenter.LimpiarFiltros();
            }
        }
        private void MostrarCrearTicket()
        {
            lblTitulo.Text = LanguageBLL.Translate("Crear_Ticket");
            var formCrear = new CrearTicket(_idUsuario);
            AbrirFormEnPanel(formCrear);
            formCrear.FormClosed += (s, args) =>
            {
                ActivarBoton(btnCasos);
                MostrarCasos();
            };
        }
        private void MostrarGestionAccesos()
        {
            lblTitulo.Text = LanguageBLL.Translate("Gestion_de_Accesos");
            var formAccesos = _serviceProvider.GetRequiredService<FormGestionAccesos>();
            AbrirFormEnPanel(formAccesos);
        }
        private void MostrarGestionMaestros()
        {
            lblTitulo.Text = LanguageBLL.Translate("Gestion_Maestros");
            var formMaestros = _serviceProvider.GetRequiredService<FormGestionMaestros>();
            AbrirFormEnPanel(formMaestros);
        }
        private void MostrarBitacora()
        {
            lblTitulo.Text = LanguageBLL.Translate("Bitacora");
            var formBitacora = new VistaBitacora();
            AbrirFormEnPanel(formBitacora);
        }
        private void MostrarAnaliticas()
        {
            lblTitulo.Text = LanguageBLL.Translate("Analiticas");
            var formAnaliticas = _serviceProvider.GetRequiredService<VistaAnaliticas>();
            AbrirFormEnPanel(formAnaliticas);
        }
        private void MostrarBackup() 
        {
            lblTitulo.Text = LanguageBLL.Translate("Backup");
            var formBackup = _serviceProvider.GetRequiredService<FormGestionBackUp>();
            AbrirFormEnPanel(formBackup);
        }
        private void AbrirFormEnPanel(Form formHijo)
        {
            formHijo.TopLevel = false;
            formHijo.FormBorderStyle = FormBorderStyle.None;
            formHijo.Dock = DockStyle.Fill;
            panelContenido.Controls.Clear();
            panelContenido.Controls.Add(formHijo);
            formHijo.Show();
        }
        private void ActivarBoton(Button botonActivo)
        {
            if (_botonActivo != null)
            {
                _botonActivo.BackColor = _colorInactivo;
                _botonActivo.ForeColor = Color.Gainsboro;
                _botonActivo.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular);
            }
            _botonActivo = botonActivo;
            _botonActivo.BackColor = _colorActivo;
            _botonActivo.ForeColor = Color.White;
            _botonActivo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }
        public string FiltroTexto => string.IsNullOrWhiteSpace(txtBuscar.Text) ? null : txtBuscar.Text.Trim();
        public string FiltroId => string.IsNullOrWhiteSpace(txtBuscarId.Text) ? null : txtBuscarId.Text.Trim();
        public Guid? FiltroEstadoId => cboEstado.SelectedValue is Guid gEst && gEst != Guid.Empty ? gEst : (Guid?)null;
        public Guid? FiltroCategoriaId => cboCategoria.SelectedValue is Guid gCat && gCat != Guid.Empty ? gCat : (Guid?)null;
        public Guid? FiltroUbicacionId => cboUbicacion.SelectedValue is Guid gUbi && gUbi != Guid.Empty ? gUbi : (Guid?)null;
        public Guid? FiltroTecnicoId 
        {
            get 
            {
                if (cboTecnico.SelectedIndex >= 0 && !string.IsNullOrWhiteSpace(cboTecnico.Text) &&
                    cboTecnico.SelectedItem is Services.DomainModel.Usuario tecnicoSel)
                {
                    return tecnicoSel.IdUsuario;
                }
                return null;
            }
        }
        public Guid? FiltroPrioridadId => cboPrioridad.SelectedValue is Guid gPri && gPri != Guid.Empty ? gPri : (Guid?)null;
        public string FiltroSLA => cboFiltroSLA.SelectedValue?.ToString();
        public void PoblarFiltros(object estados, object categorias, object ubicaciones, object tecnicos, object prioridades, object filtrosSLA)
        {
            _isUpdatingFilters = true;
            cboEstado.DataSource = estados;
            cboEstado.DisplayMember = "Nombre";
            cboEstado.ValueMember = "Id";
            cboCategoria.DataSource = categorias;
            cboCategoria.DisplayMember = "Nombre";
            cboCategoria.ValueMember = "Id";
            cboUbicacion.DataSource = ubicaciones;
            cboUbicacion.DisplayMember = "Nombre";
            cboUbicacion.ValueMember = "Id";
            cboTecnico.DropDownStyle = ComboBoxStyle.DropDown;
            cboTecnico.DataSource = tecnicos;
            cboTecnico.DisplayMember = "UserName";
            cboTecnico.ValueMember = "IdUsuario";
            cboPrioridad.DataSource = prioridades;
            cboPrioridad.DisplayMember = "Nombre";
            cboPrioridad.ValueMember = "Id";
            cboFiltroSLA.DataSource = filtrosSLA;
            cboFiltroSLA.DisplayMember = "Nombre";
            cboFiltroSLA.ValueMember = "Id";
            _isUpdatingFilters = false;
        }
        public void RenderizarTickets(IEnumerable<Ticket> tickets, IEnumerable<Services.DomainModel.Usuario> tecnicosTotales)
        {
            List<Control> tarjetas = new List<Control>();
            var cachedTecnicos = tecnicosTotales.ToList();
            foreach (var t in tickets)
            {
                var tecnicoNombre = cachedTecnicos
                    .FirstOrDefault(x => x.IdUsuario == (t.TecnicoAsignado?.IdUsuario ?? Guid.Empty))?.UserName ?? "No asignado";
                var card = CrearTarjetaTicket(t, tecnicoNombre);
                tarjetas.Add(card);
            }
            flowTickets.SuspendLayout();
            flowTickets.Controls.Clear();
            if (tarjetas.Count > 0)
            {
                flowTickets.Controls.AddRange(tarjetas.ToArray());
            }
            flowTickets.ResumeLayout();
            AjustarTarjetas();
        }
        private void VistaTecnicoForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                var sessionBLL = new SessionBLL();
                sessionBLL.CerrarSesion();
                var login = new LogIn(_serviceProvider);
                login.Show();
            }
            catch (Exception ex)
            {
                MostrarError("Error al cerrar Sesion", ex);
            }
        }
        private void btnCrearTicket_Click(object sender, EventArgs e) { }
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            try
            {
                new LoginBLL().CerrarSesion();
                this.Close();
                var loginForm = new LogIn(_serviceProvider);
                loginForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MostrarError("Error al cerrar Sesion", ex);
            }
        }
        private void btnGestionInventario_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = LanguageBLL.Translate("Gestion_de_Inventario");
            var formInventario = _serviceProvider.GetRequiredService<Tecnico.FormGestionInventario>();
            AbrirFormEnPanel(formInventario);
            ActivarBoton(btnGestionInventario);
        }
        private void ConfigurarTraducciones()
        {
            lblTitulo.Tag = "Casos_Titulo";
            lblSubtitulo.Tag = "Casos_Subtitulo";
            btnLimpiarFiltros.Tag = "btnLimpiarFiltros";
            this.RefreshLanguage();
        }
        private void VistaTecnicoForm_Load(object sender, EventArgs e) { }
        private void VistaTecnicoForm_FormClosing_1(object sender, EventArgs e)
        {
            this.Close();
        }
        private void cboFiltroSLA_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void btnLimpiarFiltros_Click_1(object sender, EventArgs e)
        {
        }
        private void btnCerrarSesion_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
