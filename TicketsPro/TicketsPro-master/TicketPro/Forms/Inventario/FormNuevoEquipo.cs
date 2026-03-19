using Microsoft.Extensions.DependencyInjection;
using BLL;
using BLL.Implementations;
using BLL.Interfaces;
using DAL.Contracts.UnitOfWork;
using Entity.Domain;
using Services.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TicketPro.Temas;
using TicketPro.MVP.DTOs;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro.Tecnico
{
    public partial class FormNuevoEquipo : BaseForm, IFormNuevoEquipoView
    {
        private FormNuevoEquipoPresenter _presenter;
        public FormNuevoEquipo()
        {
            InitializeComponent();
            _presenter = new FormNuevoEquipoPresenter(this, Program.ServiceProvider.GetRequiredService<IEquipoBLL>());
        }
        public string NroInventario => txtInventario.Text;
        public string Modelo => txtModelo.Text;
        public string Ubicacion => txtUbicacion.Text;
        public Guid TipoEquipoId => cmbTipo.SelectedValue is Guid id ? id : Guid.Empty;
        public string Procesador => txtProcesador.Text;
        public IEnumerable<ModuloInventarioDTO> RamModules
        {
            get
            {
                var list = new List<ModuloInventarioDTO>();
                foreach (Panel p in flowRamModules.Controls)
                {
                    if (p.Controls.Count < 3) continue;
                    var txtNombre = p.Controls[0] as TextBox;
                    var cmbValor = p.Controls[1] as ComboBox;
                    var cmbUnidad = p.Controls[2] as ComboBox;
                    if (txtNombre == null || string.IsNullOrWhiteSpace(txtNombre.Text)) continue;
                    list.Add(new ModuloInventarioDTO
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Valor = Convert.ToInt32(cmbValor.SelectedItem),
                        Unidad = cmbUnidad.SelectedItem.ToString()
                    });
                }
                return list;
            }
        }
        public IEnumerable<ModuloInventarioDTO> RomModules
        {
            get
            {
                var list = new List<ModuloInventarioDTO>();
                foreach (Panel p in flowRomModules.Controls)
                {
                    if (p.Controls.Count < 3) continue;
                    var txtNombre = p.Controls[0] as TextBox;
                    var cmbValor = p.Controls[1] as ComboBox;
                    var cmbUnidad = p.Controls[2] as ComboBox;
                    if (txtNombre == null || string.IsNullOrWhiteSpace(txtNombre.Text)) continue;
                    list.Add(new ModuloInventarioDTO
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Valor = Convert.ToInt32(cmbValor.SelectedItem),
                        Unidad = cmbUnidad.SelectedItem.ToString()
                    });
                }
                return list;
            }
        }
        public void LlenarTiposEquipo(IEnumerable<TipoEquipo> tipos)
        {
            cmbTipo.DataSource = tipos.ToList();
            cmbTipo.DisplayMember = "Nombre";
            cmbTipo.ValueMember = "Id";
        }
        public void MostrarError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MostrarAdvertencia(string mensaje, string titulo = "Advertencia")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void NotificarExito(string mensaje, string titulo = "Exito")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void CerrarPantalla()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        private void FormNuevoEquipo_Load(object sender, EventArgs e)
        {
            Theme.Apply(this);
            Theme.ButtonPrimary(btnGuardar);
            Theme.ButtonDanger(btnCancelar);
            Theme.ButtonPrimary(btnAddRam);
            Theme.ButtonPrimary(btnAddRom);
            _presenter.Iniciar();
            btnAddRam.Click += (s, ev) =>
            {
                flowRamModules.Controls.Add(CreateRamModule());
            };
            btnAddRom.Click += (s, ev) =>
            {
                flowRomModules.Controls.Add(CreateRomModule());
            };
            flowRamModules.Controls.Add(CreateRamModule());
            flowRamModules.Controls.Add(CreateRamModule());
            flowRomModules.Controls.Add(CreateRomModule());
            flowRomModules.Controls.Add(CreateRomModule());
        }
        private Panel CreateRamModule()
        {
            return CreateModulePanel("RAM");
        }
        private Panel CreateRomModule()
        {
            return CreateModulePanel("ROM");
        }
        private Panel CreateModulePanel(string tipo)
        {
            Panel panel = new Panel();
            panel.Width = 470;
            panel.Height = 40;
            panel.Margin = new Padding(5);
            TextBox txtNombre = new TextBox();
            txtNombre.Width = 140;
            txtNombre.Location = new Point(0, 8);
            ComboBox cmbValor = new ComboBox();
            cmbValor.Width = 80;
            cmbValor.Location = new Point(150, 8);
            cmbValor.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBox cmbUnidad = new ComboBox();
            cmbUnidad.Width = 60;
            cmbUnidad.Location = new Point(240, 8);
            cmbUnidad.DropDownStyle = ComboBoxStyle.DropDownList;
            if (tipo == "RAM")
            {
                cmbValor.Items.AddRange(new object[] { 4, 8, 16, 32, 64 });
                cmbUnidad.Items.Add("GB");
            }
            else 
            {
                cmbValor.Items.AddRange(new object[] { 128, 256, 512, 1024, 2048 });
                cmbUnidad.Items.AddRange(new object[] { "GB", "TB" });
            }
            cmbValor.SelectedIndex = 0;
            cmbUnidad.SelectedIndex = 0;
            Button btnEliminar = new Button();
            btnEliminar.Text = LanguageBLL.Translate("Boton_Eliminar_Dinamico");
            btnEliminar.Width = 40;
            btnEliminar.Location = new Point(320, 6);
            btnEliminar.Tag = "ELIMINAR";
            btnEliminar.Click += (s, e) =>
            {
                flowRamModules.Controls.Remove(panel);
                flowRomModules.Controls.Remove(panel);
                panel.Dispose();
            };
            panel.Controls.Add(txtNombre);
            panel.Controls.Add(cmbValor);
            panel.Controls.Add(cmbUnidad);
            panel.Controls.Add(btnEliminar);
            return panel;
        }
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            _presenter.GuardarEquipo();
        }
    }
}
