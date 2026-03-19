using System;
using System.Collections.Generic;
using Services.DomainModel;
using Services.DomainModel.Composite;
using Services.DTOs;

namespace TicketPro.MVP.Views
{
    public interface IFormGestionAccesosView
    {
        void LlenarComboUsuarios(IEnumerable<Usuario> usuarios);
        Usuario UsuarioSeleccionado { get; }
        void LlenarComboFamilias(IEnumerable<Familia> familias, Guid? idSeleccionado = null);
        Familia FamiliaSeleccionada { get; }
        void MostrarListasUsuario(IEnumerable<DisplayItemDTO> famAsig, IEnumerable<DisplayItemDTO> famDisp, IEnumerable<DisplayItemDTO> patAsig, IEnumerable<DisplayItemDTO> patDisp);
        void LimpiarListasUsuario();
        List<Guid> ObtenerFamiliasAsignadasUsuarioIds();
        List<Guid> ObtenerPatentesAsignadasUsuarioIds();
        void LlenarListaTodasFamilias(IEnumerable<Familia> familias);
        Familia FamiliaGlobalSeleccionada { get; }
        string NuevoNombreFamiliaIngresado { get; }
        void MostrarPatentesDeFamilia(IEnumerable<DisplayItemDTO> patAsig, IEnumerable<DisplayItemDTO> patDisp);
        List<Guid> ObtenerPatentesDisponiblesFamiliaSeleccionadasIds();
        List<Guid> ObtenerPatentesAsignadasFamiliaSeleccionadasIds();
        void LlenarCheckPatentesParaFamilia(IEnumerable<DisplayItemDTO> todasPatentes, IEnumerable<Guid> idsAsignadas);
        List<Guid> ObtenerPatentesCheckeadasFamiliaIds();
        void LlenarListaTodasPatentes(IEnumerable<Patente> patentes);
        Patente PatenteGlobalSeleccionada { get; }
        void MostrarMensajeExito(string mensaje, string titulo);
        void MostrarMensajeAdvertencia(string mensaje, string titulo);
        void MostrarError(string mensaje, Exception ex = null);
        bool ConfirmarAccion(string mensaje, string titulo);
        string PedirValor(string mensaje, string titulo);
        void SetearTituloFormulario(string titulo);
        void RefrescarPermisosUI();
    }
}
