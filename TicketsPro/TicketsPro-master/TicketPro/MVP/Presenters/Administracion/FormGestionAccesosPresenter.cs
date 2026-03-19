using System;
using System.Collections.Generic;
using System.Linq;
using Services.BLL;
using Services.DomainModel;
using Services.DomainModel.Composite;
using Services.DTOs;
using Services.Implementations;
using TicketPro.MVP.Views;
namespace TicketPro.MVP.Presenters
{
    public class FormGestionAccesosPresenter
    {
        private readonly IFormGestionAccesosView _view;
        private readonly UsuarioAccesoBLL _usuarioAccesoBLL;
        private readonly FamiliaBLL _familiaBLL;
        private readonly PatenteBLL _patenteBLL;
        private readonly UsuarioBLL _usuarioBLL; 
        public FormGestionAccesosPresenter(IFormGestionAccesosView view)
        {
            _view = view ?? throw new ArgumentNullException(nameof(view));
            _usuarioAccesoBLL = UsuarioAccesoBLL.Instance;
            _familiaBLL = new FamiliaBLL();
            _patenteBLL = new PatenteBLL();
            _usuarioBLL = new UsuarioBLL();
        }
        public void Iniciar()
        {
            _view.SetearTituloFormulario(LanguageBLL.Translate("Gestion_de_Accesos"));
            CargarUsuarios();
            RecargarTodo();
        }
        private void CargarUsuarios()
        {
            try
            {
                var usuarios = _usuarioAccesoBLL.GetAllUsuarios();
                _view.LlenarComboUsuarios(usuarios);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al cargar usuarios", ex);
            }
        }
        public void RecargarTodo()
        {
            try
            {
                CargarFamilias();
                CargarPatentes();
                if (_view.UsuarioSeleccionado != null)
                {
                    CargarListasUsuario(_view.UsuarioSeleccionado.IdUsuario);
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al recargar datos", ex);
            }
        }
        public void CargarListasUsuario(Guid idUsuario)
        {
            try
            {
                var datos = _usuarioAccesoBLL.CargarAccesosUsuario(idUsuario);
                var famAsig = datos.familiasAsignadas.Select(f => new DisplayItemDTO(f.IdFamilia, f.NombreFamilia)).OrderBy(i => i.Display).ToList();
                var famDisp = datos.familiasDisponibles.Select(f => new DisplayItemDTO(f.IdFamilia, f.NombreFamilia)).OrderBy(i => i.Display).ToList();
                var patAsig = datos.patentesAsignadas.Select(p => new DisplayItemDTO(p.idPatente, p.Nombre)).OrderBy(i => i.Display).ToList();
                var patDisp = datos.patentesDisponibles.Select(p => new DisplayItemDTO(p.idPatente, p.Nombre)).OrderBy(i => i.Display).ToList();
                _view.MostrarListasUsuario(famAsig, famDisp, patAsig, patDisp);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al cargar accesos de usuario", ex);
            }
        }
        public void GuardarAccesosUsuario()
        {
            try
            {
                var usuarioSel = _view.UsuarioSeleccionado;
                if (usuarioSel == null)
                {
                    _view.MostrarMensajeAdvertencia(LanguageBLL.Translate("Seleccione_un_usuario_antes_de_guardar"), LanguageBLL.Translate("Advertencia"));
                    return;
                }
                var famSel = _view.ObtenerFamiliasAsignadasUsuarioIds();
                var patSel = _view.ObtenerPatentesAsignadasUsuarioIds();
                _usuarioAccesoBLL.GuardarCambios(usuarioSel.IdUsuario, famSel, patSel);
                _view.RefrescarPermisosUI();
                _view.MostrarMensajeExito(LanguageBLL.Translate("Cambios_guardados_correctamente"), LanguageBLL.Translate("Exito"));
                CargarListasUsuario(usuarioSel.IdUsuario);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al guardar cambios", ex);
            }
        }
        public void EliminarUsuario()
        {
            try
            {
                var usuarioSeleccionado = _view.UsuarioSeleccionado;
                if (usuarioSeleccionado == null)
                {
                    _view.MostrarMensajeAdvertencia(LanguageBLL.Translate("Seleccione_un_usuario_para_eliminar"), LanguageBLL.Translate("Advertencia"));
                    return;
                }
                string msgConfirmar = string.Format(LanguageBLL.Translate("Confirmar_Eliminar_Usuario"), usuarioSeleccionado.UserName); 
                if (!_view.ConfirmarAccion(msgConfirmar, LanguageBLL.Translate("Confirmar_eliminacion")))
                    return;
                _usuarioBLL.Delete(usuarioSeleccionado.IdUsuario);
                _view.MostrarMensajeExito(LanguageBLL.Translate("Usuario_eliminado_correctamente"), LanguageBLL.Translate("Exito"));
                CargarUsuarios();
                _view.LimpiarListasUsuario();
            }
            catch (InvalidOperationException ex)
            {
                _view.MostrarError("Advertencia al eliminar usuario", ex);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al eliminar usuario", ex);
            }
        }
        private void CargarFamilias()
        {
            try
            {
                var familias = _familiaBLL.CargarFamilias();
                Guid? selectedId = _view.FamiliaSeleccionada?.IdFamilia;
                _view.LlenarComboFamilias(familias, selectedId);
                _view.LlenarListaTodasFamilias(familias);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al cargar familias", ex);
            }
        }
        public void NuevaFamilia()
        {
            try
            {
                string nombre = _view.PedirValor(LanguageBLL.Translate("Ingrese_nombre_nueva_familia"), LanguageBLL.Translate("Nueva_Familia"));
                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    _familiaBLL.Add(nombre);
                    _view.MostrarMensajeExito(LanguageBLL.Translate("Familia_creada_correctamente"), LanguageBLL.Translate("Exito")); 
                    RecargarTodo();
                    _view.RefrescarPermisosUI();
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al crear familia", ex);
            }
        }
        public void EliminarFamilia()
        {
            try
            {
                var f = _view.FamiliaGlobalSeleccionada;
                if (f != null)
                {
                    string msgConfirmar = string.Format(LanguageBLL.Translate("Confirmar_Eliminar_Familia"), f.NombreFamilia);
                    if (_view.ConfirmarAccion(msgConfirmar, LanguageBLL.Translate("Confirmar")))
                    {
                        _familiaBLL.Delete(f.IdFamilia);
                        RecargarTodo();
                        _view.RefrescarPermisosUI();
                    }
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al eliminar familia", ex);
            }
        }
        public void CargarPatentesDeFamilia(Guid idFamilia)
        {
            try
            {
                var resultado = _patenteBLL.CargarPatentesDeFamilia(idFamilia);
                var asig = resultado.patentesAsignadas.Select(p => new DisplayItemDTO(p.idPatente, p.Nombre)).ToList();
                var disp = resultado.patentesDisponibles.Select(p => new DisplayItemDTO(p.idPatente, p.Nombre)).ToList();
                var todas = new List<DisplayItemDTO>();
                todas.AddRange(asig);
                todas.AddRange(disp);
                todas = todas.OrderBy(i => i.Display).ToList();
                var idsAsignadas = asig.Select(a => a.Id).ToList();
                _view.LlenarCheckPatentesParaFamilia(todas, idsAsignadas);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al cargar patentes de la familia", ex);
            }
        }
        public void GuardarPatentesParaFamilia()
        {
            try
            {
                var f = _view.FamiliaSeleccionada;
                if (f == null) return;
                var patentesIds = _view.ObtenerPatentesCheckeadasFamiliaIds();
                _familiaBLL.SetPatentes(f.IdFamilia, patentesIds);
                CargarPatentesDeFamilia(f.IdFamilia);
                _view.RefrescarPermisosUI();
                _view.MostrarMensajeExito(LanguageBLL.Translate("Cambios_guardados_correctamente"), LanguageBLL.Translate("Exito"));
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al guardar patentes de la familia", ex);
            }
        }
        public void RenombrarFamilia()
        {
            try
            {
                var f = _view.FamiliaGlobalSeleccionada;
                string nuevoNombre = _view.NuevoNombreFamiliaIngresado;
                if (f != null && !string.IsNullOrWhiteSpace(nuevoNombre))
                {
                    _familiaBLL.Rename(f.IdFamilia, nuevoNombre);
                    _view.MostrarMensajeExito("Familia renombrada correctamente.", LanguageBLL.Translate("Exito"));
                    RecargarTodo();
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al renombrar familia", ex);
            }
        }
        private void CargarPatentes()
        {
            try
            {
                var patentes = _patenteBLL.CargarPatentes();
                _view.LlenarListaTodasPatentes(patentes);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al cargar patentes", ex);
            }
        }
        public void NuevaPatente()
        {
            try
            {
                string nombre = _view.PedirValor(LanguageBLL.Translate("Ingrese_nombre_nueva_patente"), LanguageBLL.Translate("Nueva_Patente")); 
                if (!string.IsNullOrWhiteSpace(nombre))
                {
                    _patenteBLL.Add(nombre, "");
                    _view.MostrarMensajeExito(LanguageBLL.Translate("Patente_creada_correctamente"), LanguageBLL.Translate("Exito")); 
                    RecargarTodo();
                }
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al crear patente", ex);
            }
        }
        public void EliminarPatente()
        {
            try
            {
                var p = _view.PatenteGlobalSeleccionada;
                if (p != null)
                {
                    string msgConfirmar = string.Format(LanguageBLL.Translate("Confirmar_Eliminar_Patente"), p.Nombre); 
                    if (_view.ConfirmarAccion(msgConfirmar, LanguageBLL.Translate("Confirmar")))
                    {
                        _patenteBLL.Delete(p.idPatente);
                        _view.MostrarMensajeExito(LanguageBLL.Translate("Patente_eliminada_correctamente"), LanguageBLL.Translate("Exito")); 
                        RecargarTodo();
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                _view.MostrarError("Advertencia al eliminar patente", ex);
            }
            catch (BLL.Exceptions.BLLException ex)
            {
                _view.MostrarError(ex.Message);
            }
            catch (Exception ex)
            {
                _view.MostrarError("Error al eliminar patente", ex);
            }
        }
    }
}
