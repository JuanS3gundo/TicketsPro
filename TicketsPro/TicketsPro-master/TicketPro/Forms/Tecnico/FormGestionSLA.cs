using Microsoft.Extensions.DependencyInjection;
using BLL;
using BLL.Implementations;
using BLL.Interfaces;
using Entity.Domain;
using Services.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TicketPro.Temas;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro.Tecnico
{
    public partial class FormGestionSLA : BaseForm, IFormGestionSLAView
    {
        private readonly IServiceProvider _serviceProvider;
        private FormGestionSLAPresenter _presenter;
        private bool _cargandoDatos;
        public FormGestionSLA(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _presenter = new FormGestionSLAPresenter(
                this,
                serviceProvider.GetRequiredService<ISLABLL>(),
                serviceProvider.GetRequiredService<IPrioridadBLL>());
            this.Load += (s, e) => 
            {
                this.Text = LanguageBLL.Translate("Gestion_SLA");
                Theme.Apply(this);
                _presenter.Iniciar();
            };
            lstPoliticas.SelectedIndexChanged += LstPoliticas_SelectedIndexChanged;
            btnNuevo.Click += (s, e) => _presenter.PrepararNuevo();
            btnGuardar.Click += (s, e) => _presenter.Guardar();
            btnCancelar.Click += (s, e) => _presenter.Cancelar();
            btnEliminar.Click += (s, e) => _presenter.Eliminar();
        }
        private void LstPoliticas_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_cargandoDatos || lstPoliticas.SelectedItem == null)
                return;
            _presenter.SeleccionarPolitica(lstPoliticas.SelectedItem as PoliticaSLA);
        }
        public string Nombre { get => txtNombre.Text; set => txtNombre.Text = value; }
        public Guid? PrioridadId 
        { 
            get => cmbPrioridad.SelectedValue as Guid?; 
            set => cmbPrioridad.SelectedValue = value ?? Guid.Empty; 
        }
        public int HorasAtencion { get => (int)numHorasAtencion.Value; set => numHorasAtencion.Value = value; }
        public int HorasResolucion { get => (int)numHorasResolucion.Value; set => numHorasResolucion.Value = value; }
        public bool SoloHorasLaborales { get => chkSoloHorasLaborales.Checked; set => chkSoloHorasLaborales.Checked = value; }
        public PoliticaSLA PoliticaSeleccionada => lstPoliticas.SelectedItem as PoliticaSLA;
        public void MostrarPrioridades(object prioridades)
        {
            cmbPrioridad.DataSource = null;
            cmbPrioridad.DataSource = prioridades;
            cmbPrioridad.DisplayMember = "Nombre";
            cmbPrioridad.ValueMember = "Id";
            cmbPrioridad.SelectedIndex = -1;
        }
        public void MostrarPoliticas(object politicas, int cantidad)
        {
            _cargandoDatos = true;
            lstPoliticas.DataSource = null;
            lstPoliticas.DataSource = politicas;
            lstPoliticas.DisplayMember = "Nombre";
            lstPoliticas.ValueMember = "Id";
            lblCantidad.Text = $"{cantidad} {LanguageBLL.Translate("Politicas_SLA")}";
            _cargandoDatos = false;
        }
        public void CargarDatosFormulario(PoliticaSLA politica)
        {
            Nombre = politica.Nombre;
            PrioridadId = politica.Prioridad?.Id ?? Guid.Empty;
            HorasAtencion = politica.HorasAtencion;
            HorasResolucion = politica.HorasResolucion;
            SoloHorasLaborales = politica.SoloHorasLaborales;
        }
        public void LimpiarFormulario()
        {
            Nombre = string.Empty;
            cmbPrioridad.SelectedIndex = -1;
            HorasAtencion = 1;
            HorasResolucion = 8;
            SoloHorasLaborales = true;
        }
        public void HabilitarFormulario(bool habilitar)
        {
            txtNombre.Enabled = habilitar;
            cmbPrioridad.Enabled = habilitar;
            numHorasAtencion.Enabled = habilitar;
            numHorasResolucion.Enabled = habilitar;
            chkSoloHorasLaborales.Enabled = habilitar;
            btnGuardar.Enabled = habilitar;
            btnCancelar.Enabled = habilitar;
        }
        public void ConfigurarParaNuevaPolitica()
        {
            btnGuardar.Text = LanguageBLL.Translate("Crear");
            btnEliminar.Enabled = false;
            lstPoliticas.SelectedIndex = -1;
        }
        public void ConfigurarParaEdicion()
        {
            btnGuardar.Text = LanguageBLL.Translate("Guardar");
            btnEliminar.Enabled = true;
        }
        public void MostrarMensajeExito(string mensaje)
        {
            MessageBox.Show(mensaje, LanguageBLL.Translate("Exito"), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void MostrarMensajeValidacion(string mensaje)
        {
            MessageBox.Show(mensaje, LanguageBLL.Translate("Validacion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public new void MostrarError(string mensaje, Exception ex = null)
        {
            base.MostrarError(mensaje, ex);
        }
        public bool PedirConfirmacionEliminacion(string nombrePolitica)
        {
            var result = MessageBox.Show(
                string.Format(LanguageBLL.Translate("SLA_Confirmar_Eliminar"), nombrePolitica),
                LanguageBLL.Translate("Confirmar"),
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );
            return result == DialogResult.Yes;
        }
        public void EstablecerFocoNuevo() => txtNombre.Focus();
        public void EstablecerFocoPrioridad() => cmbPrioridad.Focus();
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
