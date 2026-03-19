using Services.DomainModel;
using Services.DomainModel.Composite;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Services.BLL
{
    public class FamiliaBLL
    {
        private readonly FamiliaRepository _familiaRepo = FamiliaRepository.Current;
        private readonly LoggerService _logger = LoggerService.Instance;
        public List<Familia> GetAll() => _familiaRepo.GetAll().ToList();
        public void Add(string nombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    throw new ArgumentException("El nombre de la familia es obligatorio", nameof(nombre));
                if (_familiaRepo.GetAll().Any(f => f.NombreFamilia.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException($"Ya existe una familia con nombre '{nombre}'");
                var familia = new Familia
                {
                    IdFamilia = Guid.NewGuid(),
                    NombreFamilia = nombre
                };
                _familiaRepo.Add(familia);
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "CREACION_FAMILIA",
                    Detalle = $"Se creo la familia '{familia.NombreFamilia}' (ID: {familia.IdFamilia})",
                    Nivel = "Info"
                });
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "ERROR_CREACION_FAMILIA",
                    Detalle = $"Error al crear familia '{nombre}': {ex.Message}",
                    Nivel = "Error"
                });
                _logger.LogError($"Error al crear la familia '{nombre}'", ex);
                throw new InvalidOperationException("Ocurrió un error al crear la familia. Verifique los datos e intente nuevamente.", ex);
            }
        }
        public void GetById(Guid idFamilia)
        {
            _familiaRepo.GetById(idFamilia);
        }
        public void Delete(Guid idFamilia)
        {
            try
            {
                var fam = _familiaRepo.GetById(idFamilia);
                if (fam == null)
                    throw new InvalidOperationException($"Familia con ID '{idFamilia}' no fue encontrada");
                var tienePatentes = FamiliaPatenteRepository.Current.GetAll().Any(fp => fp.idFamilia == idFamilia);
                var tieneUsuarios = UsuarioFamiliaRepository.Current.GetAll().Any(uf => uf.IdFamilia == idFamilia);
                if (tienePatentes || tieneUsuarios)
                    throw new InvalidOperationException("No se puede eliminar la familia porque tiene usuarios o patentes asociados");
                string nombreFamilia = fam.NombreFamilia;
                _familiaRepo.Remove(idFamilia);
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "ELIMINACION_FAMILIA",
                    Detalle = $"Se elimino la familia '{nombreFamilia}' (ID: {idFamilia})",
                    Nivel = "Info"
                });
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "ERROR_ELIMINACION_FAMILIA",
                    Detalle = $"Error al eliminar familia (ID: {idFamilia}): {ex.Message}",
                    Nivel = "Error"
                });
                _logger.LogError($"Error inesperado al eliminar la familia {idFamilia}", ex);
                throw new InvalidOperationException("Ocurrió un error inesperado al eliminar la familia.", ex);
            }
        }
        public List<Familia> CargarFamilias()
        {
            try
            {
                return _familiaRepo.GetAll()
                    .OrderBy(f => f.NombreFamilia)
                    .ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError("Error al cargar familias desde la base de datos", ex);
                throw new InvalidOperationException("Ocurrió un error al cargar las familias desde la base de datos.", ex);
            }
        }
        public void AsignarPatente(Guid idFamilia, Guid idPatente)
        {
            try
            {
                var familia = _familiaRepo.GetById(idFamilia);
                var patente = PatenteRepository.Current.GetById(idPatente);
                if (familia == null)
                    throw new InvalidOperationException($"Familia con ID '{idFamilia}' no fue encontrada");
                if (patente == null)
                    throw new InvalidOperationException($"Patente con ID '{idPatente}' no fue encontrada");
                var yaAsignada = FamiliaPatenteRepository.Current
                    .GetByFamilia(idFamilia)
                    .Any(fp => fp.idPatente == idPatente);
                if (yaAsignada)
                {
                    throw new InvalidOperationException("La patente ya está asignada a esta familia");
                }
                FamiliaPatenteRepository.Current.Add(new FamiliaPatente
                {
                    idFamilia = idFamilia,
                    idPatente = idPatente
                });
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "ASIGNACION_PATENTE_A_FAMILIA",
                    Detalle = $"Se asigno la patente '{patente.Nombre}' a la familia '{familia.NombreFamilia}'",
                    Nivel = "Info"
                });
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "ERROR_ASIGNACION_PATENTE_A_FAMILIA",
                    Detalle = $"Error al asignar patente (ID: {idPatente}) a familia (ID: {idFamilia}): {ex.Message}",
                    Nivel = "Error"
                });
                _logger.LogError($"Error al asignar patente {idPatente} a familia {idFamilia}", ex);
                throw new InvalidOperationException("Ocurrió un error al asignar la patente a la familia.", ex);
            }
        }
        public void QuitarPatente(Guid idFamilia, Guid idPatente)
        {
            try
            {
                var familia = _familiaRepo.GetById(idFamilia);
                var patente = PatenteRepository.Current.GetById(idPatente);
                FamiliaPatenteRepository.Current.Remove(idFamilia, idPatente);
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "REMOCION_PATENTE_DE_FAMILIA",
                    Detalle = $"Se quito la patente '{patente?.Nombre ?? idPatente.ToString()}' de la familia '{familia?.NombreFamilia ?? idFamilia.ToString()}'",
                    Nivel = "Info"
                });
            }
            catch (Exception ex)
            {
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "ERROR_REMOCION_PATENTE_DE_FAMILIA",
                    Detalle = $"Error al quitar patente (ID: {idPatente}) de familia (ID: {idFamilia}): {ex.Message}",
                    Nivel = "Error"
                });
                _logger.LogError($"Error al quitar patente {idPatente} de familia {idFamilia}", ex);
                throw new InvalidOperationException("Ocurrió un error al quitar la patente de la familia.", ex);
            }
        }
        public void Rename(Guid idFamilia, string nuevoNombre)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nuevoNombre))
                    throw new ArgumentException("El nombre de la familia es obligatorio", nameof(nuevoNombre));
                var todas = _familiaRepo.GetAll();
                if (todas.Any(f => f.IdFamilia != idFamilia && f.NombreFamilia.Equals(nuevoNombre, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException($"Ya existe una familia con el nombre '{nuevoNombre}'");
                var familia = _familiaRepo.GetById(idFamilia);
                if (familia == null)
                    throw new InvalidOperationException($"Familia con ID '{idFamilia}' no fue encontrada");
                familia.NombreFamilia = nuevoNombre;
                _familiaRepo.Update(familia);
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "RENOMBRE_FAMILIA",
                    Detalle = $"Se renombro la familia a '{nuevoNombre}' (ID: {idFamilia})",
                    Nivel = "Info"
                });
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "ERROR_RENOMBRE_FAMILIA",
                    Detalle = $"Error al renombrar familia (ID: {idFamilia}): {ex.Message}",
                    Nivel = "Error"
                });
                _logger.LogError($"Error al renombrar la familia '{idFamilia}' a '{nuevoNombre}'", ex);
                throw new InvalidOperationException("Ocurrió un error al renombrar la familia. Verifique los datos e intente nuevamente.", ex);
            }
        }
        public void SetPatentes(Guid idFamilia, List<Guid> idPatentes)
        {
            try
            {
                var familia = _familiaRepo.GetById(idFamilia);
                if (familia == null)
                    throw new InvalidOperationException($"Familia con ID '{idFamilia}' no fue encontrada");
                var asignadasHoy = FamiliaPatenteRepository.Current.GetByFamilia(idFamilia).Select(fp => fp.idPatente).ToList();
                foreach (var idPatente in asignadasHoy)
                {
                    if (!idPatentes.Contains(idPatente))
                    {
                        QuitarPatente(idFamilia, idPatente);
                    }
                }
                foreach (var idPatente in idPatentes)
                {
                    if (!asignadasHoy.Contains(idPatente))
                    {
                        AsignarPatente(idFamilia, idPatente);
                    }
                }
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "ACTUALIZACION_PATENTES_FAMILIA",
                    Detalle = $"Se actualizaron las patentes de la familia '{familia.NombreFamilia}' (ID: {idFamilia}). Total patentes: {idPatentes.Count}",
                    Nivel = "Info"
                });
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "ERROR_ACTUALIZACION_PATENTES_FAMILIA",
                    Detalle = $"Error al actualizar patentes para la familia (ID: {idFamilia}): {ex.Message}",
                    Nivel = "Error"
                });
                _logger.LogError($"Error al actualizar patentes de la familia {idFamilia}", ex);
                throw new InvalidOperationException("Ocurrió un error al actualizar las patentes de la familia.", ex);
            }
        }
    }
}
