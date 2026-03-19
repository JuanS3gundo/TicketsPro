using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Entity.Domain;
namespace TicketPro.MVP.Views
{
    public interface IVistaMainFormView
    {
        string FiltroTexto { get; }
        string FiltroId { get; }
        Guid? FiltroEstadoId { get; }
        Guid? FiltroCategoriaId { get; }
        Guid? FiltroUbicacionId { get; }
        Guid? FiltroTecnicoId { get; }
        Guid? FiltroPrioridadId { get; }
        string FiltroSLA { get; }
        void PoblarFiltros(
            object estados,
            object categorias,
            object ubicaciones,
            object tecnicos,
            object prioridades,
            object filtrosSLA
        );
        void LimpiarFiltrosUI();
        void RenderizarTickets(IEnumerable<Ticket> tickets, IEnumerable<Services.DomainModel.Usuario> tecnicosTotales);
        void MostrarError(string mensaje, Exception ex = null);
        void MostrarAdvertenciaAccesoDenegado();
        void HabilitarMenu(string menuName, bool habilitado);
    }
}
