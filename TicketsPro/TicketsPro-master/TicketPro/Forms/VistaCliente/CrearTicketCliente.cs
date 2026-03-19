using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BLL;
using BLL.Interfaces;
using Services.BLL;
using TicketPro.MVP.DTOs;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro.VistaCliente
{
    public partial class CrearTicketCliente : BaseForm, ICrearTicketClienteView
    {
        private readonly CrearTicketClientePresenter _presenter;
        private readonly Guid _idUsuario;
        private readonly List<ArchivoTemporalDTO> _archivosAdjuntos = new List<ArchivoTemporalDTO>();
        public CrearTicketCliente(Guid idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            
            this.Tag = "Ticket_Cliente_Crear_Titulo";
            lblTituloSeccion.Tag = "Ticket_Cliente_Crear_Titulo";
            lblTituloTicket.Tag = "Ticket_Cliente_Crear_LabelTitulo";
            lblDescripcion.Tag = "Ticket_Cliente_Crear_LabelDescripcion";
            lblCategoria.Tag = "Ticket_Cliente_Crear_LabelCategoria";
            lblUbicacion.Tag = "Ticket_Cliente_Crear_LabelUbicacion";
            lblEquipo.Tag = "Ticket_Cliente_Crear_LabelEquipo";
            lblAdjuntosSeccion.Tag = "Ticket_Cliente_Crear_SeccionAdjuntos";
            colNombre.Tag = "Ticket_Cliente_Crear_ColNombre";
            colTamanio.Tag = "Ticket_Cliente_Crear_ColTamanio";
            btnAgregarAdjunto.Tag = "Ticket_Cliente_Crear_BotonAgregarAdjunto";
            btnEliminarAdjunto.Tag = "Ticket_Cliente_Crear_BotonEliminarAdjunto";
            lblAdjuntosInfo.Tag = "Ticket_Cliente_Crear_InfoAdjuntos";
            btnCrearTicket.Tag = "Ticket_Cliente_Crear_BotonCrear";
            btnCancelar.Tag = "Ticket_Cliente_Crear_BotonCancelar";
            var estadoService = Program.ServiceProvider.GetRequiredService<IEstadoTicketBLL>();
            var ticketService = Program.ServiceProvider.GetRequiredService<ITicketBLL>();
            var adjuntoService = Program.ServiceProvider.GetRequiredService<IAdjuntoBLL>();
            var categoriaService = Program.ServiceProvider.GetRequiredService<ICategoriaTicketBLL>();
            var ubicacionService = Program.ServiceProvider.GetRequiredService<IUbicacionBLL>();
            var equipoService = Program.ServiceProvider.GetRequiredService<IEquipoBLL>();
            _presenter = new CrearTicketClientePresenter(this, estadoService, ticketService, adjuntoService, categoriaService, ubicacionService, equipoService);
            _presenter.Inicializar();
            btnAgregarAdjunto.Click += btnAgregarAdjunto_Click;
            btnEliminarAdjunto.Click += btnEliminarAdjunto_Click;
            btnCancelar.Click += btnCancelar_Click;
        }
        public Guid IdUsuario => _idUsuario;
        public string Titulo => txtTitulo.Text;
        public string Descripcion => txtDescripcion.Text;
        public Guid? CategoriaId => cmbCategoria.SelectedValue as Guid?;
        public Guid? UbicacionId => cmbUbicacion.SelectedValue as Guid?;
        public int? EquipoId => cmbEquipo.SelectedValue as int?;
        public IEnumerable<ArchivoTemporalDTO> ArchivosAdjuntos => _archivosAdjuntos;
        public void LlenarCategorias(object data)
        {
            cmbCategoria.DataSource = data;
            cmbCategoria.DisplayMember = "Nombre";
            cmbCategoria.ValueMember = "Id";
            cmbCategoria.SelectedIndex = -1;
        }
        public void LlenarUbicaciones(object data)
        {
            cmbUbicacion.DataSource = data;
            cmbUbicacion.DisplayMember = "Nombre";
            cmbUbicacion.ValueMember = "Id";
            cmbUbicacion.SelectedIndex = -1;
        }
        public void LlenarEquipos(object data)
        {
            cmbEquipo.DataSource = data;
            cmbEquipo.DisplayMember = "NroInventario";
            cmbEquipo.ValueMember = "Id";
            cmbEquipo.SelectedItem = null;
            cmbEquipo.Text = LanguageBLL.Translate("Combo_Seleccione_Equipo");
        }
        public void MostrarMensajeExito(string mensaje, string titulo = "Exito")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void MostrarMensajeAdvertencia(string mensaje, string titulo = "Advertencia")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void MostrarMensajeError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public new void MostrarError(string mensaje, Exception ex = null)
        {
            base.MostrarError(mensaje, ex);
        }
        public void CerrarFormularioConExito()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        public void CancelarYCerrar()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        public void AgregarArchivoALista(ArchivoTemporalDTO archivo)
        {
            _archivosAdjuntos.Add(archivo);
            var item = new ListViewItem(archivo.Nombre);
            item.SubItems.Add(FormatFileSize(archivo.Tamanio));
            item.Tag = archivo;
            lstAdjuntos.Items.Add(item);
        }
        public void EliminarArchivoDeLista(ArchivoTemporalDTO archivo)
        {
            _archivosAdjuntos.Remove(archivo);
            var itemToRemove = lstAdjuntos.Items.Cast<ListViewItem>().FirstOrDefault(i => i.Tag == archivo);
            if (itemToRemove != null)
            {
                lstAdjuntos.Items.Remove(itemToRemove);
            }
        }
        public bool ConfirmarAccion(string mensaje, string titulo)
        {
            return MessageBox.Show(mensaje, titulo, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        public int ObtenerCantidadArchivosSeleccionados() => lstAdjuntos.SelectedItems.Count;
        public IEnumerable<ArchivoTemporalDTO> ObtenerArchivosSeleccionadosParaEliminar()
        {
            var list = new List<ArchivoTemporalDTO>();
            foreach (ListViewItem item in lstAdjuntos.SelectedItems)
            {
                if (item.Tag is ArchivoTemporalDTO dto) list.Add(dto);
            }
            return list;
        }
        private string FormatFileSize(long bytes)
        {
            if (bytes < 1024) return $"{bytes} B";
            else if (bytes < 1024 * 1024) return $"{bytes / 1024.0:F2} KB";
            else return $"{bytes / (1024.0 * 1024.0):F2} MB";
        }
        private void btnCrearTicket_Click(object sender, EventArgs e) => _presenter.CrearTicket();
        private void btnAgregarAdjunto_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Seleccionar archivo para adjuntar";
                openFileDialog.Filter = "Archivos permitidos|*.pdf;*.jpg;*.jpeg;*.png;*.doc;*.docx;*.xls;*.xlsx;*.txt;*.zip;*.rar|Todos los archivos (*.*)|*.*";
                openFileDialog.Multiselect = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    _presenter.AgregarArchivos(openFileDialog.FileNames);
                }
            }
        }
        private void btnEliminarAdjunto_Click(object sender, EventArgs e) => _presenter.EliminarArchivosAdjuntos();
        private void btnCancelar_Click(object sender, EventArgs e) => _presenter.Cancelar();
    }
}
