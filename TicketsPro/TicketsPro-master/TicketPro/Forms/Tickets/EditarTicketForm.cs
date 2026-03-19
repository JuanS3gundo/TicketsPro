using Microsoft.Extensions.DependencyInjection;
using Entity.Domain;
using Services.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro.VistaCliente
{
    public partial class EditarTicketForm : BaseForm, IEditarTicketView
    {
        private readonly Guid _ticketId;
        private readonly EditarTicketPresenter _presenter;
        public Guid TicketId => _ticketId;
        public string Titulo
        {
            get => txtTitulo.Text;
            set => txtTitulo.Text = value;
        }
        public string Descripcion
        {
            get => txtDescripcion.Text;
            set => txtDescripcion.Text = value;
        }
        public string FechaApertura
        {
            get => txtFechaApertura.Text;
            set => txtFechaApertura.Text = value;
        }
        public Guid? EstadoId
        {
            get => cboEstado.SelectedValue as Guid?;
            set => cboEstado.SelectedValue = value;
        }
        public Guid? CategoriaId
        {
            get => cboCategoria.SelectedValue as Guid?;
            set => cboCategoria.SelectedValue = value;
        }
        public Guid? UbicacionId
        {
            get => cboUbicacion.SelectedValue as Guid?;
            set => cboUbicacion.SelectedValue = value;
        }
        public Guid? TecnicoAsignadoId
        {
            get => cboTecnico.SelectedValue as Guid?;
            set => cboTecnico.SelectedValue = value ?? (object)Guid.Empty;
        }
        public Guid? PrioridadId
        {
            get => cboPrioridad.SelectedValue as Guid?;
            set => cboPrioridad.SelectedValue = value;
        }
        public void CargarEstados(IEnumerable<EstadoTicket> estados)
        {
            cboEstado.DataSource = estados.ToList();
            cboEstado.DisplayMember = "Nombre";
            cboEstado.ValueMember = "Id";
            cboEstado.SelectedIndex = -1;
        }
        public void CargarCategorias(IEnumerable<CategoriaTicket> categorias)
        {
            cboCategoria.DataSource = categorias.ToList();
            cboCategoria.DisplayMember = "Nombre";
            cboCategoria.ValueMember = "Id";
            cboCategoria.SelectedIndex = -1;
        }
        public void CargarUbicaciones(IEnumerable<Ubicacion> ubicaciones)
        {
            cboUbicacion.DataSource = ubicaciones.ToList();
            cboUbicacion.DisplayMember = "Nombre";
            cboUbicacion.ValueMember = "Id";
            cboUbicacion.SelectedIndex = -1;
        }
        public void CargarTecnicos(IEnumerable<Services.DomainModel.Usuario> tecnicos)
        {
            cboTecnico.DataSource = tecnicos.ToList();
            cboTecnico.DisplayMember = "UserName";
            cboTecnico.ValueMember = "IdUsuario";
            cboTecnico.SelectedIndex = -1;
            cboTecnico.Text = "";
        }
        public void CargarPrioridades(IEnumerable<PrioridadTicket> prioridades)
        {
            cboPrioridad.SelectedIndexChanged -= cboPrioridad_SelectedIndexChanged;
            cboPrioridad.DataSource = prioridades.ToList();
            cboPrioridad.DisplayMember = "Nombre";
            cboPrioridad.ValueMember = "Id";
            cboPrioridad.SelectedIndex = -1;
            cboPrioridad.SelectedIndexChanged += cboPrioridad_SelectedIndexChanged;
        }
        public void MostrarSLA(string mensajePolitica, string mensajeVencimiento, Color colorVencimiento)
        {
            lblPoliticaSLA.Text = mensajePolitica;
            lblFechaVencimiento.Text = mensajeVencimiento;
            lblFechaVencimiento.ForeColor = colorVencimiento;
        }
        public void MostrarError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MostrarAdvertencia(string mensaje, string titulo = "Advertencia")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void MostrarMensaje(string mensaje, string titulo = "Informacion")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void CerrarConExito()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public EditarTicketForm(Guid ticketId)
        {
            InitializeComponent();
            _ticketId = ticketId;
            this.Text = LanguageBLL.Translate("EditarTicket_TituloForm"); 
            _presenter = new EditarTicketPresenter(
                this,
                Program.ServiceProvider.GetRequiredService<BLL.ITicketBLL>(),
                Program.ServiceProvider.GetRequiredService<BLL.Interfaces.IEstadoTicketBLL>(),
                Program.ServiceProvider.GetRequiredService<BLL.Interfaces.ICategoriaTicketBLL>(),
                Program.ServiceProvider.GetRequiredService<BLL.Interfaces.IUbicacionBLL>(),
                Program.ServiceProvider.GetRequiredService<BLL.Interfaces.IPrioridadBLL>(),
                Program.ServiceProvider.GetRequiredService<BLL.Interfaces.ISLABLL>()
            );
            _presenter.OnViewLoad();
        }
        private void cboPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPrioridad.SelectedValue is Guid prioridadId)
            {
                _presenter?.AlCambiarPrioridad(prioridadId);
            }
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            _presenter.GuardarTicket();
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
