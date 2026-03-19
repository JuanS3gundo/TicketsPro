using Microsoft.Extensions.DependencyInjection;
using BLL.Implementations;
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
using BLL.Interfaces;
namespace TicketPro.Tecnico
{
    public class FormEditarCategoria : BaseForm, IFormEditarCategoriaView
    {
        private TextBox txtNombre;
        private TextBox txtDescripcion;
        private ComboBox cmbSLA;
        private Button btnAceptar;
        private Button btnCancelar;
        private Label lblNombre;
        private Label lblDescripcion;
        private Label lblSLA;
        private FormEditarCategoriaPresenter _presenter;
        public CategoriaTicket Categoria => CategoriaResultante;
        public FormEditarCategoria() : this(null) { }
        public FormEditarCategoria(CategoriaTicket categoriaExistente)
        {
            InitUI();
            _presenter = new FormEditarCategoriaPresenter(this, Program.ServiceProvider.GetRequiredService<ISLABLL>());
            _presenter.Iniciar(categoriaExistente);
        }
        private void BtnAceptar_Click(object sender, EventArgs e) => _presenter.Aceptar();
        private void BtnCancelar_Click(object sender, EventArgs e) => _presenter.Cancelar();
        public string NombreCategoria 
        { 
            get => txtNombre.Text; 
            set => txtNombre.Text = value; 
        }
        public string DescripcionCategoria 
        { 
            get => txtDescripcion.Text; 
            set => txtDescripcion.Text = value; 
        }
        public Guid? SLASeleccionado 
        { 
            get 
            {
                if (cmbSLA.SelectedIndex > 0 && cmbSLA.SelectedValue is Guid slaId)
                    return slaId;
                return null;
            }
            set 
            {
                if (value.HasValue)
                    cmbSLA.SelectedValue = value.Value;
                else
                    cmbSLA.SelectedIndex = 0;
            }
        }
        public CategoriaTicket CategoriaResultante { get; set; }
        public string TituloFormulario { set => this.Text = value; }
        public void CargarOpcionesSLA(object opcionesSLA)
        {
            cmbSLA.DataSource = opcionesSLA;
            cmbSLA.DisplayMember = "Nombre";
            cmbSLA.ValueMember = "Id";
        }
        public void MostrarMensajeValidacion(string mensaje)
        {
            MessageBox.Show(mensaje, LanguageBLL.Translate("Validacion"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtNombre.Focus();
        }
        public void CerrarConExito()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public void CerrarCancelado()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void InitUI()
        {
            this.ClientSize = new Size(450, 260);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            lblNombre = new Label { Text = LanguageBLL.Translate("Nombre") + ":", Location = new Point(20, 20), AutoSize = true };
            txtNombre = new TextBox { Location = new Point(20, 42), Width = 400 };
            lblDescripcion = new Label { Text = LanguageBLL.Translate("Descripcion") + ":", Location = new Point(20, 72), AutoSize = true };
            txtDescripcion = new TextBox { Location = new Point(20, 94), Width = 400 };
            lblSLA = new Label { Text = "SLA:", Location = new Point(20, 124), AutoSize = true };
            cmbSLA = new ComboBox
            {
                Location = new Point(20, 146),
                Width = 400,
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            btnAceptar = new Button { Text = LanguageBLL.Translate("Aceptar"), Location = new Point(230, 200), Width = 90 };
            btnCancelar = new Button { Text = LanguageBLL.Translate("Cancelar"), Location = new Point(330, 200), Width = 90 };
            btnAceptar.Click += BtnAceptar_Click;
            btnCancelar.Click += BtnCancelar_Click;
            this.Controls.AddRange(new Control[] {
                lblNombre, txtNombre,
                lblDescripcion, txtDescripcion,
                lblSLA, cmbSLA,
                btnAceptar, btnCancelar
            });
            this.AcceptButton = btnAceptar;
            this.CancelButton = btnCancelar;
            Theme.Apply(this);
        }
    }
}
