using System;
using System.Collections.Generic;
using TicketPro.MVP.DTOs;

namespace TicketPro.MVP.Views
{
    public interface ICrearTicketClienteView
    {
        Guid IdUsuario { get; }
        string Titulo { get; }
        string Descripcion { get; }
        Guid? CategoriaId { get; }
        Guid? UbicacionId { get; }
        int? EquipoId { get; }
        void LlenarCategorias(object data);
        void LlenarUbicaciones(object data);
        void LlenarEquipos(object data);
        void MostrarMensajeExito(string mensaje, string titulo = "Exito");
        void MostrarMensajeAdvertencia(string mensaje, string titulo = "Advertencia");
        void MostrarMensajeError(string mensaje, string titulo = "Error");
        void MostrarError(string mensaje, Exception ex = null);
        void CerrarFormularioConExito();
        void CancelarYCerrar();
        IEnumerable<ArchivoTemporalDTO> ArchivosAdjuntos { get; }
        void AgregarArchivoALista(ArchivoTemporalDTO archivo);
        void EliminarArchivoDeLista(ArchivoTemporalDTO archivo);
        bool ConfirmarAccion(string mensaje, string titulo);
        int ObtenerCantidadArchivosSeleccionados();
        IEnumerable<ArchivoTemporalDTO> ObtenerArchivosSeleccionadosParaEliminar();
    }
}
