using System;
using System.Collections.Generic;
using Entity.Domain;
namespace TicketPro.MVP.Views
{
    public interface IDetalleTicketView : Base.IView
    {
        Guid TicketId { get; }
        string NuevoComentarioTexto { get; set; }
        bool NuevoComentarioEsInterno { get; set; }
        void MostrarTicketNoEncontrado();
        void MostrarAdvertenciaIntegridad(Ticket ticket);
        void OcultarAdvertenciaIntegridad();
        void LimpiarChat();
        void AgregarBurbujaChat(string texto, string autor, DateTime fecha, bool esTecnico);
        void ScrollChatAlFinal();
        void RenderizarCabecera(string tituloTicket);
        void ConfigurarOpcionesUsuario(bool esTecnico, bool ticketResuelto);
        void RenderizarPanelDetalles(
            Ticket ticket, 
            SolucionTicket solucion, 
            string nombreCliente, 
            string nombreTecnico,
            PrioridadTicket prioridad,
            PoliticaSLA politicaSLA,
            List<AdjuntoTicket> adjuntos);
        void MostrarMensajeExito(string mensaje);
        void LimpiarInputComentario();
    }
}
