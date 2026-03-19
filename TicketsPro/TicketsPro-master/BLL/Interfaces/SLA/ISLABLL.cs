using Entity.Domain;
using System;
using System.Collections.Generic;
namespace BLL.Interfaces
{
    public interface ISLABLL
    {
        List<PoliticaSLA> ObtenerTodas();
        PoliticaSLA ObtenerPorId(Guid id);
        PoliticaSLA ObtenerPorPrioridad(Guid prioridadId);
        bool CrearPoliticaSLA(PoliticaSLA politica, out string mensaje);
        bool ActualizarPoliticaSLA(PoliticaSLA politica, out string mensaje);
        bool EliminarPoliticaSLA(Guid id, out string mensaje);
        DateTime CalcularFechaVencimiento(DateTime fechaInicio, PoliticaSLA sla);
    }
}
