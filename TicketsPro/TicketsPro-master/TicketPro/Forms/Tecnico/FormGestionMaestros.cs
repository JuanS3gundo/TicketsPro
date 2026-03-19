using BLL;
using BLL.Implementations;
using BLL.Interfaces;
using Entity.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
using Services.BLL;
using Services.Services;
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
    public partial class FormGestionMaestros : BaseForm, IFormGestionMaestrosView
    {
        private readonly IServiceProvider _serviceProvider;
        private FormGestionMaestrosPresenter _presenter;
        public FormGestionMaestros(IServiceProvider serviceProvider)
        {
            InitializeComponent();
            _serviceProvider = serviceProvider;
            _presenter = new FormGestionMaestrosPresenter(
                this,
                serviceProvider.GetRequiredService<IPrioridadBLL>(),
                serviceProvider.GetRequiredService<IEstadoTicketBLL>(),
                serviceProvider.GetRequiredService<ICategoriaTicketBLL>(),
                serviceProvider.GetRequiredService<IUbicacionBLL>(),
                serviceProvider.GetRequiredService<ITipoEquipoBLL>(),
                serviceProvider.GetRequiredService<IUbicacionEquipoBLL>(),
                serviceProvider.GetRequiredService<ICategoriaItemBLL>()
            );
            this.Load += (s, e) =>
            {
                this.Text = LanguageBLL.Translate("Gestion_Maestros");
                Theme.Apply(this);
                lblTitulo.Text           = LanguageBLL.Translate("Gestion_Maestros");
                lblSeleccioneTipo.Text   = LanguageBLL.Translate("Maestros_Label_Seleccione_Tipo");
                btnAgregar.Text          = LanguageBLL.Translate("Agregar");
                btnEditar.Text           = LanguageBLL.Translate("Editar");
                btnEliminar.Text         = LanguageBLL.Translate("Eliminar");
                btnGestionSLA.Text       = LanguageBLL.Translate("SLA_Form_Titulo");
                _presenter.Iniciar();
            };
            cmbTipoMaestro.SelectedIndexChanged += (s, e) => _presenter.CambiarTipoMaestro();
            btnAgregar.Click += (s, e) => _presenter.Agregar();
            btnEditar.Click += (s, e) => _presenter.Editar();
            btnEliminar.Click += (s, e) => _presenter.Eliminar();
            btnGestionSLA.Click += (s, e) => _presenter.GestionarSLA();
            lstItems.DisplayMember = "Nombre";
            lstItems.ValueMember = "Id";
            lstItems.SelectionMode = SelectionMode.One;
        }
        public string TipoMaestroActual 
        {
            get 
            {
                var seleccionado = cmbTipoMaestro.SelectedItem as dynamic;
                return seleccionado?.Clave;
            }
        }
        public object ItemSeleccionado => lstItems.SelectedItem;
        public void CargarTiposMaestro(object tiposMaestro)
        {
            cmbTipoMaestro.DataSource = tiposMaestro;
            cmbTipoMaestro.DisplayMember = "Nombre";
            cmbTipoMaestro.ValueMember = "Clave";
            cmbTipoMaestro.SelectedIndex = 0;
        }
        public void MostrarItems(object items, int cantidad)
        {
            lstItems.DataSource = null;
            lstItems.DataSource = items;
            lblCantidad.Text = $"{cantidad} {LanguageBLL.Translate("Registros")}";
        }
        public void ConfigurarOpcionesSLA(bool visible)
        {
            btnGestionSLA.Visible = visible;
        }
        public string PedirNombreNuevoItem(string titulo, string prompt)
        {
            return Interaction.InputBox(prompt, titulo);
        }
        public string PedirNuevoNombreEdicion(string titulo, string prompt, string valorActual)
        {
            return Interaction.InputBox(prompt, titulo, valorActual);
        }
        public bool PedirConfirmacionEliminacion(string nombreItem)
        {
            string confirmacion = string.Format(LanguageBLL.Translate("Confirmar_Eliminar_Item"), nombreItem);
            var resultado = MessageBox.Show(confirmacion, LanguageBLL.Translate("Confirmar"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            return resultado == DialogResult.Yes;
        }
        public CategoriaTicket MostrarFormularioNuevaCategoria()
        {
            using (var form = new FormEditarCategoria())
            {
                if (form.ShowDialog() == DialogResult.OK)
                    return form.Categoria;
            }
            return null;
        }
        public CategoriaTicket MostrarFormularioEdicionCategoria(CategoriaTicket categoriaActual)
        {
            using (var form = new FormEditarCategoria(categoriaActual))
            {
                if (form.ShowDialog() == DialogResult.OK)
                    return form.Categoria;
            }
            return null;
        }
        public void AbrirFormularioSLA()
        {
            var formSLA = _serviceProvider.GetRequiredService<FormGestionSLA>();
            formSLA.ShowDialog();
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
    }
}
