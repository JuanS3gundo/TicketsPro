using Entity.Domain;
using System;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface IAdjuntoBLL
    {
        bool AgregarAdjunto(AdjuntoTicket adjunto, out string mensaje);
        List<AdjuntoTicket> ObtenerAdjuntosPorTicket(Guid ticketId);
        AdjuntoTicket ObtenerPorId(Guid id);
        bool EliminarAdjunto(Guid adjuntoId, bool eliminarArchivo, out string mensaje);
    }
}
