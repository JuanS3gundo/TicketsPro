using Microsoft.Extensions.DependencyInjection;
using BLL;
using BLL.Implementations;
using BLL.Interfaces;
using Entity.Domain;
using Services.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TicketPro.Temas;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro.Tecnico
{
    public partial class FormNuevoItem : BaseForm, IFormNuevoItemView
    {
        public InventarioItem ItemCreado { get; private set; }
        private FormNuevoItemPresenter _presenter;
        private readonly List<int> potenciasDos = new List<int>
        {
            1, 2, 4, 8, 16, 32, 64, 128, 256, 512,
            1024, 2048, 4096, 8192, 16384
        };
        public FormNuevoItem()
        {
            InitializeComponent();
            _presenter = new FormNuevoItemPresenter(
                this, 
                Program.ServiceProvider.GetRequiredService<ICategoriaItemBLL>(), 
                Program.ServiceProvider.GetRequiredService<IInventarioItemBLL>()
            );
            this.Text = LanguageBLL.Translate("Inventario_NuevoItem_Form_Titulo");
            this.Load += FormNuevoItem_Load;
            Load += (s, e) => Theme.Apply(this);
            Theme.ButtonPrimary(btnGuardar);
            Theme.ButtonDanger(btnCancelar);
            btnGuardar.Click += BtnGuardar_Click;
            btnCancelar.Click += (s, e) => Close();
            cmbCategoria.SelectedIndexChanged += CmbCategoria_SelectedIndexChanged;
        }
        public string Codigo => txtCodigo.Text;
        public string Nombre => txtNombre.Text;
        public string CategoriaSeleccionada => cmbCategoria.SelectedItem?.ToString();
        public string Ubicacion => txtUbicacion.Text;
        public int Cantidad => (int)numCantidad.Value;
        public string Descripcion => txtDescripcion.Text;
        public bool Disponible => chkDisponible.Checked;
        public object ValorSeleccionado => cmbValor.SelectedItem;
        public object UnidadSeleccionada => cmbUnidad.SelectedItem;
        public void LlenarCategorias(string[] categorias)
        {
            cmbCategoria.Items.Clear();
            cmbCategoria.Items.AddRange(categorias);
            cmbCategoria.SelectedIndex = 0;
            cmbValor.Enabled = false;
            cmbUnidad.Enabled = false;
        }
        public void ConfigurarControlesMemoria(bool habilitar, List<int> potenciasDos, string[] unidades)
        {
            cmbValor.Enabled = habilitar;
            cmbUnidad.Enabled = habilitar;
            cmbValor.Items.Clear();
            cmbUnidad.Items.Clear();
            if (habilitar)
            {
                foreach (var p in potenciasDos)
                    cmbValor.Items.Add(p);
                cmbUnidad.Items.AddRange(unidades);
                cmbValor.SelectedIndex = 0;
                cmbUnidad.SelectedIndex = 1;
            }
        }
        public void MostrarError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MostrarAdvertencia(string mensaje, string titulo = "Advertencia")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void NotificarExitoYAsignarItem(string mensaje, string titulo, InventarioItem item)
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            ItemCreado = item;
            DialogResult = DialogResult.OK;
            Close();
        }
        private void FormNuevoItem_Load(object sender, EventArgs e)
        {
            _presenter.Iniciar();
        }
        private void CmbCategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            _presenter.AlCambiarCategoria();
        }
        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            _presenter.GuardarItem();
        }
    }
}
