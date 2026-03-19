using Microsoft.Extensions.DependencyInjection;
using BLL;
using Services.BLL;
using Services.DomainModel;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro.VistaCliente
{
    public partial class FormCliente : BaseForm, IFormClienteView
    {
        private readonly FormClientePresenter _presenter;
        private readonly Guid _idUsuario;
        public FormCliente(Guid idUsuario)
        {
            InitializeComponent();
            _idUsuario = idUsuario;
            var ticketService = Program.ServiceProvider.GetRequiredService<ITicketBLL>();
            _presenter = new FormClientePresenter(this, ticketService);
            
            this.Tag = "Cliente_WindowTitulo";
            btnCerrarSesion.Tag = "Cliente_ButtonCerrarSesion";
            btnNuevoTicket.Tag = "Cliente_ButtonNuevoTicket";
            lblTitulo.Tag = "Cliente_LabelTitulo";
            btnLimpiarFiltros.Tag = "Cliente_ButtonLimpiar";
            lblBuscar.Tag = "Cliente_LabelBuscar";
            lblFiltroEstado.Tag = "Cliente_LabelFiltroEstado";
            txtBuscar.TextChanged += (s, e) => _presenter.AplicarFiltros();
            cboEstado.SelectedIndexChanged += (s, e) => _presenter.AplicarFiltros();
            btnLimpiarFiltros.Click += BtnLimpiarFiltros_Click;
            btnCerrarSesion.Click += BtnCerrarSesion_Click;
            btnNuevoTicket.Click -= btnNuevoTicket_Click;
            btnNuevoTicket.Click += btnNuevoTicket_Click;
            this.Load += FormCliente_Load;
        }
        public Guid IdUsuario => _idUsuario;
        public string EstadoSeleccionado => cboEstado.SelectedItem?.ToString();
        public string TextoBusqueda => txtBuscar.Text;
        private void FormCliente_Load(object sender, EventArgs e)
        {
            _presenter.CargarInformacionUsuario();
            _presenter.CargarComboEstados();
            _presenter.CargarTickets();
        }
        public void MostrarUsuario(string nombreUsuario)
        {
            lblUsuario.Text = nombreUsuario != null ? $"Usuario: {nombreUsuario}" : "Usuario";
        }
        public void CargarCombosEstados(IEnumerable<string> estados)
        {
            cboEstado.Items.Clear();
            foreach (var est in estados)
            {
                cboEstado.Items.Add(est);
            }
            if (cboEstado.Items.Count > 0)
                cboEstado.SelectedIndex = 0;
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
        public void ReiniciarAplicacion()
        {
            this.Close();
            Application.Restart();
        }
        private void BtnLimpiarFiltros_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            if (cboEstado.Items.Count > 0)
                cboEstado.SelectedIndex = 0;
            _presenter.AplicarFiltros();
        }
        private void BtnCerrarSesion_Click(object sender, EventArgs e)
        {
            _presenter.CerrarSesion();
        }
        private void btnNuevoTicket_Click(object sender, EventArgs e)
        {
            if (_presenter.PuedeCrearTicket())
            {
                var form = new CrearTicketCliente(_idUsuario);
                form.ShowDialog();
                _presenter.CargarTickets();
            }
        }
        public void MostrarTickets(List<Entity.Domain.Ticket> tickets)
        {
            flowTickets.SuspendLayout();
            flowTickets.Controls.Clear();
            string estadoEnProceso = LanguageBLL.Translate("Estado_EnProceso");
            string estadoNuevo = LanguageBLL.Translate("Estado_Nuevo");
            string estadoResuelto = LanguageBLL.Translate("Estado_Resuelto");
            string ticketLabel = LanguageBLL.Translate("Ticket_Label");
            string botonDetalle = LanguageBLL.Translate("Cliente_Boton_VerDetalle");
            string ticketVacio = LanguageBLL.Translate("Cliente_Vacio_Msg");
            string prioridadLabel = LanguageBLL.Translate("Label_Prioridad");
            foreach (var t in tickets)
            {
                string estadoTexto = t.Estado?.Nombre ?? string.Empty;
                string estadoLegible = estadoTexto;
                Color estadoColor = Color.IndianRed;
                if (estadoTexto == "Resuelto")
                {
                    estadoLegible = estadoResuelto;
                    estadoColor = Color.FromArgb(50, 205, 50);
                }
                else if (estadoTexto == "EnProgreso")
                {
                    estadoLegible = estadoEnProceso;
                    estadoColor = Color.FromArgb(255, 193, 7);
                }
                else if (estadoTexto == "Nuevo")
                {
                    estadoLegible = estadoNuevo;
                    estadoColor = Color.FromArgb(244, 67, 54);
                }
                string codigo = t.Id.ToString().Substring(0, 8).ToUpper();
                var card = new Panel
                {
                    Width = 450,
                    Height = 220,
                    BackColor = Color.FromArgb(60, 60, 75),
                    Margin = new Padding(10),
                    BorderStyle = BorderStyle.None,
                    Cursor = Cursors.Hand
                };
                card.Paint += (s, e) =>
                {
                    var g = e.Graphics;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                    using (var pen = new Pen(Color.FromArgb(70, 70, 85), 1))
                    {
                        g.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                    }
                };
                card.MouseEnter += (s, ev) => card.BackColor = Color.FromArgb(65, 65, 80);
                card.MouseLeave += (s, ev) => card.BackColor = Color.FromArgb(60, 60, 75);
                EventHandler abrirDetalle = (s, e) =>
                {
                    var detalle = new DetalleTicket(t.Id);
                    detalle.StartPosition = FormStartPosition.CenterScreen;
                    detalle.ShowDialog();
                    _presenter.CargarTickets();
                };
                card.DoubleClick += abrirDetalle;
                var tblCard = new TableLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    ColumnCount = 1,
                    RowCount = 5,
                    Padding = new Padding(15),
                    BackColor = Color.Transparent
                };
                tblCard.DoubleClick += abrirDetalle;
                tblCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                tblCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
                tblCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                tblCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 30F));
                tblCard.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
                var lblTitulo = new Label
                {
                    Text = $" {ticketLabel} #{codigo}",
                    ForeColor = Color.FromArgb(88, 101, 242),
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleLeft,
                    AutoEllipsis = true
                };
                lblTitulo.DoubleClick += abrirDetalle;
                var lblDescripcion = new Label
                {
                    Text = t.Titulo,
                    ForeColor = Color.White,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.TopLeft,
                    AutoEllipsis = true
                };
                lblDescripcion.DoubleClick += abrirDetalle;
                var pnlInfo = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
                pnlInfo.DoubleClick += abrirDetalle;
                var lblFecha = new Label
                {
                    Text = $" {t.FechaApertura:dd/MM/yyyy HH:mm}",
                    ForeColor = Color.LightGray,
                    Font = new Font("Segoe UI", 8),
                    AutoSize = true,
                    Location = new Point(0, 5)
                };
                var lblEstado = new Label
                {
                    Text = estadoLegible,
                    AutoSize = true,
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    Padding = new Padding(8, 3, 8, 3),
                    BackColor = estadoColor,
                    ForeColor = Color.White,
                    Location = new Point(200, 2)
                };
                pnlInfo.Controls.Add(lblFecha);
                pnlInfo.Controls.Add(lblEstado);
                var pnlPrioridad = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
                pnlPrioridad.DoubleClick += abrirDetalle;
                string prioridadNombre = t.Prioridad?.Nombre ?? "Normal";
                Color prioridadColor = Color.Gray;
                if (prioridadNombre == "Critica" || prioridadNombre == "Critica") prioridadColor = Color.FromArgb(244, 67, 54);
                else if (prioridadNombre == "Alta") prioridadColor = Color.FromArgb(255, 152, 0);
                else if (prioridadNombre == "Media") prioridadColor = Color.FromArgb(255, 193, 7);
                else if (prioridadNombre == "Baja") prioridadColor = Color.FromArgb(76, 175, 80);
                var lblPrioridad = new Label
                {
                    Text = $"{prioridadLabel}: {prioridadNombre}",
                    ForeColor = prioridadColor,
                    Font = new Font("Segoe UI", 8, FontStyle.Bold),
                    AutoSize = true,
                    Location = new Point(0, 5)
                };
                pnlPrioridad.Controls.Add(lblPrioridad);
                var pnlBoton = new Panel { Dock = DockStyle.Fill, BackColor = Color.Transparent };
                var btnDetalle = new Button
                {
                    Text = botonDetalle,
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(88, 101, 242),
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 9, FontStyle.Bold),
                    Size = new Size(140, 32),
                    Location = new Point(280, 0),
                    Cursor = Cursors.Hand
                };
                btnDetalle.FlatAppearance.BorderSize = 0;
                btnDetalle.Tag = "Cliente_Boton_VerDetalle";
                btnDetalle.Click += (s, ev) =>
                {
                    var detalle = new DetalleTicket(t.Id);
                    detalle.StartPosition = FormStartPosition.CenterScreen;
                    detalle.ShowDialog();
                    _presenter.CargarTickets();
                };
                btnDetalle.MouseEnter += (s, ev) => btnDetalle.BackColor = Color.FromArgb(98, 111, 252);
                btnDetalle.MouseLeave += (s, ev) => btnDetalle.BackColor = Color.FromArgb(88, 101, 242);
                pnlBoton.Controls.Add(btnDetalle);
                tblCard.Controls.Add(lblTitulo, 0, 0);
                tblCard.Controls.Add(lblDescripcion, 0, 1);
                tblCard.Controls.Add(pnlInfo, 0, 2);
                tblCard.Controls.Add(pnlPrioridad, 0, 3);
                tblCard.Controls.Add(pnlBoton, 0, 4);
                card.Controls.Add(tblCard);
                flowTickets.Controls.Add(card);
            }
            if (tickets.Count == 0)
            {
                var lblVacio = new Label
                {
                    Text = ticketVacio,
                    ForeColor = Color.Gray,
                    Font = new Font("Segoe UI", 12, FontStyle.Italic),
                    AutoSize = true,
                    Padding = new Padding(20),
                    Margin = new Padding(20)
                };
                flowTickets.Controls.Add(lblVacio);
            }
            flowTickets.ResumeLayout();
        }
    }
}
