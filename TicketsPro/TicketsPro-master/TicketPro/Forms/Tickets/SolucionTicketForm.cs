using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows.Forms;
using Services.Services;
using BLL.Interfaces;
using TicketPro.MVP.Views;
using TicketPro.MVP.Presenters;
namespace TicketPro
{
    public partial class SolucionTicketForm : BaseForm, ISolucionTicketView
    {
        private readonly SolucionTicketPresenter _presenter;
        private readonly Guid _ticketId;
        public SolucionTicketForm(Guid ticketId)
        {
            InitializeComponent();
            _ticketId = ticketId;
            var solucionService = Program.ServiceProvider.GetRequiredService<ISolucionTicketBLL>();
            _presenter = new SolucionTicketPresenter(this, solucionService);
            this.Tag = "TicketSolucion_Titulo";
            lblTitulo.Tag = "TicketSolucion_Titulo";
            lblSubtitulo.Tag = "TicketSolucion_Subtitulo";
            lblSolucion.Tag = "TicketSolucion_Detalle";
            btnGuardar.Tag = "TicketSolucion_Guardar";
            btnCancelar.Tag = "TicketSolucion_Cerrar";
            this.RefreshLanguage();
            string idCorto = _ticketId.ToString().Substring(0, 8).ToUpper();
            lblTicketId.Text = $"Ticket ID: #{idCorto}";
        }
        public Guid TicketId => _ticketId;
        public string SolucionData => txtSolucion.Text;
        public void MostrarMensajeExito(string mensaje, string titulo = "Exito")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public void MostrarMensajeAdvertencia(string mensaje, string titulo = "Advertencia")
        {
            MessageBox.Show(mensaje, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
        private void btnGuardar_Click(object sender, EventArgs e) => _presenter.GuardarSolucion();
        private void btnCancelar_Click(object sender, EventArgs e) => _presenter.Cancelar();
    }
}
