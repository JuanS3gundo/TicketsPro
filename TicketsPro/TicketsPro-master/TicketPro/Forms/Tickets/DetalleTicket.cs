using Microsoft.Extensions.DependencyInjection;
using BLL;
using BLL.Implementations;
using Entity.Domain;
using Services.BLL;
using Services.Services;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
using BLL.Interfaces;
namespace TicketPro.VistaCliente
{
    public partial class DetalleTicket : BaseForm, IDetalleTicketView
    {
        private readonly DetalleTicketPresenter _presenter;
        private readonly Guid _idTicket;
        public Guid TicketId => _idTicket;
        public string NuevoComentarioTexto 
        { 
            get => txtNuevoComentario?.Text ?? ""; 
            set { if(txtNuevoComentario != null) txtNuevoComentario.Text = value; } 
        }
        public bool NuevoComentarioEsInterno 
        { 
            get => chkComentarioInterno?.Checked ?? false; 
            set { if(chkComentarioInterno != null) chkComentarioInterno.Checked = value; } 
        }
        private bool _dragging = false;
        private Point _dragCursorPoint;
        private Point _dragFormPoint;
        public DetalleTicket(Guid idTicket)
        {
            InitializeComponent();
            _idTicket = idTicket;
            
            this.Tag = "DetalleTicket";
            lblIntegridadTitulo.Tag = "Ticket_Detalle_IntegridadTitulo";
            btnIntegridadVerIgual.Tag = "Ticket_Detalle_IntegridadVerIgual";
            btnIntegridadCerrar.Tag = "Ticket_Detalle_IntegridadCerrar";
            chkComentarioInterno.Tag = "Ticket_Detalle_ComentarioInterno";
            btnEnviarComentario.Tag = "Ticket_Detalle_ButtonEnviar";
            btnEditar.Tag = "Ticket_Detalle_ButtonEditar";
            btnResolver.Tag = "Ticket_Detalle_ButtonResolver";
            btnCerrar.Tag = "Ticket_Detalle_ButtonCerrar";
            _presenter = new DetalleTicketPresenter(
                this,
                Program.ServiceProvider.GetRequiredService<ITicketBLL>(),
                Program.ServiceProvider.GetRequiredService<ISolucionTicketBLL>(),
                Program.ServiceProvider.GetRequiredService<IComentarioBLL>(),
                Program.ServiceProvider.GetRequiredService<IAdjuntoBLL>(),
                Program.ServiceProvider.GetRequiredService<IPrioridadBLL>(),
                Program.ServiceProvider.GetRequiredService<ISLABLL>()
            );
        }
        private void DetalleTicketForm_Load(object sender, EventArgs e)
        {
            _presenter.OnViewLoad();
        }
        public void MostrarError(string mensaje, string titulo = "Error")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MostrarAdvertencia(string mensaje, string titulo = "Advertencia")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        public void MostrarMensaje(string mensaje, string titulo = "Aviso")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void MostrarMensajeExito(string mensaje)
        {
            MessageBox.Show(mensaje, "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void ConfigurarOpcionesUsuario(bool esTecnico, bool ticketResuelto)
        {
            btnResolver.Visible = esTecnico && !ticketResuelto;
            btnEditar.Visible = esTecnico;
            pnlComentario.Visible = !ticketResuelto;
            chkComentarioInterno.Visible = esTecnico;
        }
        private void BtnResolver_Click(object sender, EventArgs e)
        {
            try
            {
                var formSolucion = new SolucionTicketForm(_idTicket)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                if (formSolucion.ShowDialog() == DialogResult.OK)
                {
                    _presenter.CargarDetalle();
                }
            }
            catch (Exception ex)
            {
                MostrarError(LanguageBLL.Translate("Ticket_Detalle_Error_AbrirFormularioSolucion"), ex);
            }
        }
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            try
            {
                var formEditar = new EditarTicketForm(_idTicket)
                {
                    StartPosition = FormStartPosition.CenterParent
                };
                if (formEditar.ShowDialog() == DialogResult.OK)
                {
                    _presenter.CargarDetalle();
                }
            }
            catch (Exception ex)
            {
                MostrarError(LanguageBLL.Translate("Ticket_Detalle_Error_AbrirFormularioEdicion"), ex);
            }
        }
        public void MostrarTicketNoEncontrado()
        {
            MessageBox.Show(LanguageBLL.Translate("Msg_Ticket_NoEncontrado"), LanguageBLL.Translate("Error"), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public void MostrarAdvertenciaIntegridad(Ticket ticket)
        {
            string mensajeCompleto = LanguageBLL.Translate("Msg_Advertencia_Integridad_Detalle");
            lblIntegridadDetalle.Text = $"{mensajeCompleto}\r\n{LanguageBLL.Translate("ID")}: {ticket.Id}";
            pnlOverlay.Visible = true;
            pnlIntegridad.Visible = true;
            pnlOverlay.BringToFront();
        }
        public void OcultarAdvertenciaIntegridad()
        {
            pnlOverlay.Visible = false;
            pnlIntegridad.Visible = false;
        }
        public void RenderizarCabecera(string tituloTicket)
        {
            lblTituloTicket.Text = $"{LanguageBLL.Translate("Ticket_Label")} {tituloTicket}";
        }
        public void LimpiarInputComentario()
        {
            if (txtNuevoComentario != null) txtNuevoComentario.Clear();
            if (chkComentarioInterno != null) chkComentarioInterno.Checked = false;
        }
        private void RefrescarVista()
        {
            _presenter.CargarDetalle();
        }
        private void BtnCerrar_Click(object sender, EventArgs e) => this.Close();
        private void btnIntegridadCerrar_Click(object sender, EventArgs e) => this.Close();
        private void btnIntegridadVerIgual_Click(object sender, EventArgs e)
        {
            pnlOverlay.Visible = false;
            pnlIntegridad.Visible = false;
        }
        private void BtnEnviarComentario_Click(object sender, EventArgs e)
        {
            _presenter.EnviarComentario();
        }
        private void PnlAdjunto_Click(object sender, EventArgs e)
        {
            var control = sender as Control;
            var adjunto = control?.Tag as AdjuntoTicket ?? (control?.Parent?.Tag as AdjuntoTicket);
            if (adjunto != null)
            {
                DescargarAdjunto(adjunto);
            }
        }
        private void DescargarAdjunto(AdjuntoTicket adjunto)
        {
            try
            {
                if (adjunto == null)
                    return;
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.FileName = adjunto.NombreArchivo;
                    saveFileDialog.Filter = $"Archivos (*{adjunto.Extension})|*{adjunto.Extension}";
                    saveFileDialog.Title = LanguageBLL.Translate("Guardar_Archivo");
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (!System.IO.File.Exists(adjunto.Ruta))
                        {
                            MessageBox.Show(
                                LanguageBLL.Translate("Error_Archivo_No_Encontrado"),
                                LanguageBLL.Translate("Error"),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error
                            );
                            return;
                        }
                        System.IO.File.Copy(adjunto.Ruta, saveFileDialog.FileName, overwrite: true);
                        MessageBox.Show(
                            LanguageBLL.Translate("Archivo_Descargado"),
                            LanguageBLL.Translate("Exito"),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error", ex); 
            }
        }
        private void BtnAdjuntar_Click(object sender, EventArgs e)
        {
            try
            {
                using (var openFileDialog = new OpenFileDialog())
                {
                    openFileDialog.Filter = "Archivos permitidos|*.pdf;*.jpg;*.jpeg;*.png;*.doc;*.docx;*.xls;*.xlsx;*.txt;*.zip;*.rar";
                    openFileDialog.Title = LanguageBLL.Translate("Seleccionar_Archivo");
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        var fileInfo = new System.IO.FileInfo(openFileDialog.FileName);
                        _presenter.AdjuntarArchivo(fileInfo.FullName, fileInfo.Name, fileInfo.Extension, fileInfo.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                MostrarError("Error", ex);
            }
        }
        #region Movimiento de Ventana sin Bordes
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
        #endregion

        private void lblIntegridadTitulo_Click(object sender, EventArgs e)
        {

        }
    }
}
