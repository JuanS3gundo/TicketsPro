using Microsoft.Extensions.DependencyInjection;
using BLL;
using BLL.Implementations;
using BLL.Interfaces;
using Entity;
using Entity.Domain;
using Services.BLL;
using Services.DomainModel;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
using TicketPro.Temas;
namespace TicketPro.Inventario
{
    public partial class FormDetalleEquipo : BaseForm, IFormDetalleEquipoView
    {
        private FormDetalleEquipoPresenter _presenter;
        public FormDetalleEquipo(int equipoId)
        {
            InitializeComponent();
            _presenter = new FormDetalleEquipoPresenter(
                this, 
                Program.ServiceProvider.GetRequiredService<IEquipoBLL>(), 
                Program.ServiceProvider.GetRequiredService<IInventarioItemBLL>(), 
                UsuarioBLL.Instance);
            _equipoId = equipoId;
            if (!System.ComponentModel.LicenseManager.UsageMode
                    .Equals(System.ComponentModel.LicenseUsageMode.Designtime))
            {
                Theme.ApplyForm(this);
                Theme.MakeCard(panelInfo);
                Theme.MakeCard(panelRelaciones);
                Theme.ApplyButtonPrimary(btnGuardar);
                Theme.ApplyButtonSecondary(btnCerrar);
                Theme.ApplyButtonPrimary(btnAgregar);
                Theme.ApplyButtonSecondary(btnQuitar);
                Theme.ApplyButtonPrimary(btnAsignarUsuario);
                Theme.ApplyButtonDanger(btnQuitarUsuario);
            }
        }
        private int _equipoId;
        public int EquipoId => _equipoId;
        public string Modelo 
        { 
            get => txtModelo.Text; 
            set => txtModelo.Text = value; 
        }
        public string NroInventario 
        { 
            get => txtInventario.Text; 
            set => txtInventario.Text = value; 
        }
        public string UsuarioAsignado { set => txtUsuarioAsignado.Text = value; }
        public string Procesador { set => txtProcesador.Text = value; }
        public string RAM { set => txtRAM.Text = value; }
        public string ROM { set => txtROM.Text = value; }
        public InventarioItem ItemAsignadoSeleccionado => lstAsignados.SelectedItem as InventarioItem;
        public InventarioItem ItemDisponibleSeleccionado => lstDisponibles.SelectedItem as InventarioItem;
        public Services.DomainModel.Usuario UsuarioSeleccionado => cmbUsuarios.SelectedItem as Services.DomainModel.Usuario;
        public void LlenarUsuarios(IEnumerable<Services.DomainModel.Usuario> usuarios)
        {
            cmbUsuarios.DisplayMember = "UserName";
            cmbUsuarios.ValueMember = "IdUsuario";
            cmbUsuarios.DataSource = usuarios.ToList();
        }
        public void LlenarItemsAsignados(IEnumerable<InventarioItem> items, string labelText)
        {
            lstAsignados.DisplayMember = "Nombre";
            lstAsignados.ValueMember = "Id";
            lstAsignados.DataSource = items.ToList();
            lblAsignadosCount.Text = labelText;
        }
        public void LlenarItemsDisponibles(IEnumerable<InventarioItem> items, string labelText)
        {
            lstDisponibles.DisplayMember = "Nombre";
            lstDisponibles.ValueMember = "Id";
            lstDisponibles.DataSource = items.ToList();
            lblDisponiblesCount.Text = labelText;
        }
        public void MostrarMensaje(string mensaje, string titulo = "Informacion")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void MostrarError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void CerrarConExito()
        {
            DialogResult = DialogResult.OK;
            Close();
        }
        public void CerrarPantalla()
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void FormDetalleEquipo_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            _presenter.Iniciar();
        }
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            _presenter.AsignarItem();
        }
        private void btnQuitar_Click(object sender, EventArgs e)
        {
            _presenter.QuitarItem();
        }
        private void btnAsignarUsuario_Click(object sender, EventArgs e)
        {
            _presenter.AsignarUsuario();
        }
        private void btnQuitarUsuario_Click(object sender, EventArgs e)
        {
            _presenter.QuitarUsuario();
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            _presenter.GuardarCambios();
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            CerrarPantalla();
        }
    }
}
