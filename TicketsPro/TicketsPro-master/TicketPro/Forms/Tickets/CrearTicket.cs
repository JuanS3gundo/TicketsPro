using Microsoft.Extensions.DependencyInjection;
using BLL;
using BLL.Implementations;
using BLL.Interfaces;
using Entity.Domain;
using Services.BLL;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using Services.DomainModel;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro
{
    public partial class CrearTicket : BaseForm, ICrearTicketView
    {
        private readonly Guid _idUsuario;
        private readonly string _placeholderTitulo;
        private readonly string _placeholderDescripcion;
        private CrearTicketPresenter _presenter;
        public string TituloTicket => TituloTxt.Text == _placeholderTitulo ? "" : TituloTxt.Text.Trim();
        public string DescripcionTicket => DescrTxt.Text == _placeholderDescripcion ? "" : DescrTxt.Text.Trim();
        public Guid? CategoriaSeleccionada => EsItemRealSeleccionado(cmbCategoria) ? (Guid?)cmbCategoria.SelectedValue : null;
        public Guid? UbicacionSeleccionada => EsItemRealSeleccionado(cmbUbicacion) ? (Guid?)cmbUbicacion.SelectedValue : null;
        public Guid? PrioridadSeleccionada => EsItemRealSeleccionado(cmbPrioridad) ? (Guid?)cmbPrioridad.SelectedValue : null;
        public int? EquipoSeleccionado => EsItemRealSeleccionado(cmbEquipo) ? (int?)cmbEquipo.SelectedValue : null;
        public Guid? TecnicoSeleccionado => EsItemRealSeleccionado(cmbTecnico) ? (Guid?)cmbTecnico.SelectedValue : null;
        public CrearTicket(Guid idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            this.Tag = "Ticket_Cliente_Crear_Titulo";
            lblTituloForm.Tag = "Ticket_Cliente_Crear_Titulo";
            lblTitulo.Tag = "Ticket_Cliente_LabelTitulo";
            lblDescripcion.Tag = "Ticket_Cliente_LabelDescripcion";
            lblCategoria.Tag = "Ticket_Cliente_LabelCategoria";
            lblUbicacion.Tag = "Ticket_Cliente_LabelUbicacion";
            label1.Tag = "Label_Tecnico";
            label2.Tag = "Ticket_Cliente_LabelEquipo";
            lblPrioridad.Tag = "Prioridad";
            button1.Tag = "Ticket_Cliente_BotonCrear";
            _placeholderTitulo = LanguageBLL.Translate("Placeholder_Titulo");
            _placeholderDescripcion = LanguageBLL.Translate("Placeholder_Descripcion");
            InicializarPlaceholdersTexto();
            this.RefreshLanguage();
            _presenter = new CrearTicketPresenter(
                this, 
                _idUsuario,
                Program.ServiceProvider.GetRequiredService<BLL.Interfaces.ICategoriaTicketBLL>(),
                Program.ServiceProvider.GetRequiredService<BLL.Interfaces.IUbicacionBLL>(),
                Program.ServiceProvider.GetRequiredService<BLL.Interfaces.IPrioridadBLL>(),
                Program.ServiceProvider.GetRequiredService<BLL.Interfaces.IEquipoBLL>(),
                Program.ServiceProvider.GetRequiredService<BLL.Interfaces.IEstadoTicketBLL>(),
                Program.ServiceProvider.GetRequiredService<BLL.ITicketBLL>()
            );
            _presenter.OnViewLoad();
        }
        public void MostrarMensaje(string mensaje, string titulo = "Aviso")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void MostrarError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MostrarAdvertencia(string mensaje, string titulo = "Advertencia")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void CerrarPantallaExito(string mensaje)
        {
            MostrarMensaje(mensaje, LanguageBLL.Translate("Exito"));
            this.Close();
        }
        public void CargarCategorias(List<CategoriaTicket> categorias)
        {
            string placeholderCategoria = LanguageBLL.Translate("Combo_Seleccione_Categoria");
            var lista = new List<CategoriaTicket> { new CategoriaTicket { Id = Guid.Empty, Nombre = placeholderCategoria } };
            lista.AddRange(categorias);
            cmbCategoria.DataSource = lista;
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "Id";
            cmbCategoria.SelectedIndex = 0;
        }
        public void CargarUbicaciones(List<Ubicacion> ubicaciones)
        {
            string placeholderUbicacion = LanguageBLL.Translate("Combo_Seleccione_Ubicacion");
            var lista = new List<Ubicacion> { new Ubicacion { Id = Guid.Empty, Nombre = placeholderUbicacion } };
            lista.AddRange(ubicaciones);
            cmbUbicacion.DataSource = lista;
            cmbUbicacion.DisplayMember = "Nombre";
            cmbUbicacion.ValueMember = "Id";
            cmbUbicacion.SelectedIndex = 0;
        }
        public void CargarPrioridades(List<PrioridadTicket> prioridades)
        {
            string placeholderPrioridad = LanguageBLL.Translate("Combo_Seleccione_Prioridad");
            var lista = new List<PrioridadTicket> { new PrioridadTicket { Id = Guid.Empty, Nombre = placeholderPrioridad } };
            lista.AddRange(prioridades);
            cmbPrioridad.DataSource = lista;
            cmbPrioridad.DisplayMember = "Nombre";
            cmbPrioridad.ValueMember = "Id";
            cmbPrioridad.SelectedIndex = 0;
        }
        public void CargarTecnicos(List<Services.DomainModel.Usuario> tecnicos)
        {
            string placeholderTecnico = LanguageBLL.Translate("Combo_Seleccione_Tecnico");
            var lista = new List<Services.DomainModel.Usuario> { new Services.DomainModel.Usuario { IdUsuario = Guid.Empty, UserName = placeholderTecnico } };
            lista.AddRange(tecnicos);
            cmbTecnico.DataSource = lista;
            cmbTecnico.DisplayMember = "UserName";
            cmbTecnico.ValueMember = "IdUsuario";
            cmbTecnico.SelectedIndex = 0;
        }
        public void CargarEquipos(List<EquipoInformatico> equipos)
        {
            string placeholderEquipo = LanguageBLL.Translate("Combo_Seleccione_Equipo");
            var lista = new List<EquipoInformatico> { new EquipoInformatico { Id = 0, NroInventario = placeholderEquipo } };
            lista.AddRange(equipos);
            cmbEquipo.DataSource = lista;
            cmbEquipo.DisplayMember = "NroInventario";
            cmbEquipo.ValueMember = "Id";
            cmbEquipo.SelectedIndex = 0;
        }
        private bool EsItemRealSeleccionado(ComboBox cmb)
        {
            if (!string.IsNullOrEmpty(cmb.ValueMember))
            {
                object selectedValue = cmb.SelectedValue;
                if (selectedValue == null) return false;
                if (selectedValue is Guid)
                {
                    return !selectedValue.Equals(Guid.Empty);
                }
                if (selectedValue is int)
                {
                    return (int)selectedValue != 0;
                }
                return true;
            }
            return cmb.SelectedIndex > 0;
        }
        private void InicializarPlaceholdersTexto()
        {
            TituloTxt.Text = _placeholderTitulo;
            TituloTxt.ForeColor = Color.DarkGray;
            TituloTxt.GotFocus += TituloTxt_GotFocus;
            TituloTxt.LostFocus += TituloTxt_LostFocus;
            DescrTxt.Text = _placeholderDescripcion;
            DescrTxt.ForeColor = Color.DarkGray;
            DescrTxt.GotFocus += DescrTxt_GotFocus;
            DescrTxt.LostFocus += DescrTxt_LostFocus;
        }
        private void TituloTxt_GotFocus(object sender, EventArgs e)
        {
            if (TituloTxt.Text == _placeholderTitulo)
            {
                TituloTxt.Text = "";
                TituloTxt.ForeColor = Color.White;
            }
        }
        private void TituloTxt_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TituloTxt.Text))
            {
                TituloTxt.Text = _placeholderTitulo;
                TituloTxt.ForeColor = Color.DarkGray;
            }
        }
        private void DescrTxt_GotFocus(object sender, EventArgs e)
        {
            if (DescrTxt.Text == _placeholderDescripcion)
            {
                DescrTxt.Text = "";
                DescrTxt.ForeColor = Color.White;
            }
        }
        private void DescrTxt_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescrTxt.Text))
            {
                DescrTxt.Text = _placeholderDescripcion;
                DescrTxt.ForeColor = Color.DarkGray;
            }
        }
        private void cmbPrioridad_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblPoliticaSLA.Text = "";
            lblFechaVencimiento.Text = "";
        }
        private void button1_Click(object sender, EventArgs e)
        {
            _presenter.GuardarTicket();
        }
        private void CrearTicket_Load(object sender, EventArgs e) { }
        private void cmbTecnico_SelectedIndexChanged(object sender, EventArgs e) { }
        private void cmbEquipo_SelectedIndexChanged(object sender, EventArgs e) { }
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        private void pnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            _dragging = true;
            _dragCursorPoint = Cursor.Position;
            _dragFormPoint = this.Location;
        }
        private void pnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point diff = Point.Subtract(Cursor.Position, new Size(_dragCursorPoint));
                this.Location = Point.Add(_dragFormPoint, new Size(diff));
            }
        }
        private void pnlHeader_MouseUp(object sender, MouseEventArgs e)
        {
            _dragging = false;
        }
    }
}
