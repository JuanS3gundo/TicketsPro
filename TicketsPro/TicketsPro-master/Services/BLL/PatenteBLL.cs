using Services.DomainModel;
using Services.Facade;
using Services.Implementations;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
namespace Services.BLL
{
    public class PatenteBLL
    {
        private readonly PatenteRepository _repo = PatenteRepository.Current;
        public List<Patente> GetAll() => _repo.GetAll().ToList();
        public void Add(string nombre, string dataKey = "")
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    throw new ArgumentException("El nombre no puede estar vacio.");
                if (_repo.GetAll().Any(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException("Ya existe una patente con ese nombre.");
                var patente = new Patente
                {
                    idPatente = Guid.NewGuid(),
                    Nombre = nombre,
                    DataKey = dataKey ?? "",
                    tipoAcceso = TipoAcceso.UI
                };
                _repo.Add(patente);
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "CREACION_PATENTE",
                    Detalle = $"Se creo la patente '{patente.Nombre}' (ID: {patente.idPatente}, DataKey: '{dataKey}')",
                    Nivel = "Info"
                });
            }
            catch (Exception ex)
            {
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "ERROR_CREACION_PATENTE",
                    Detalle = $"Error al crear patente '{nombre}': {ex.Message}",
                    Nivel = "Error"
                });
                throw;
            }
        }
        public void Delete(Guid idPatente)
        {
            try
            {
                var patente = _repo.GetById(idPatente);
                var asociada = FamiliaPatenteRepository.Current.GetAll().Any(fp => fp.idPatente == idPatente)
                            || UsuarioPatenteRepository.Current.GetAll().Any(up => up.IdPatente == idPatente);
                if (asociada)
                    throw new InvalidOperationException("No se puede eliminar una patente asociada a una familia o usuario.");
                string nombrePatente = patente?.Nombre ?? idPatente.ToString();
                _repo.Remove(idPatente);
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "ELIMINACION_PATENTE",
                    Detalle = $"Se elimino la patente '{nombrePatente}' (ID: {idPatente})",
                    Nivel = "Info"
                });
            }
            catch (Exception ex)
            {
                BitacoraService.RegistrarEvento(new Bitacora
                {
                    Usuario = SessionService.GetUsuario()?.UserName ?? "Desconocido",
                    Accion = "ERROR_ELIMINACION_PATENTE",
                    Detalle = $"Error al eliminar patente (ID: {idPatente}): {ex.Message}",
                    Nivel = "Error"
                });
                throw;
            }
        }
        public List<Patente> CargarPatentes()
        {
            return _repo.GetAll()
                .OrderBy(p => p.Nombre)
                .ToList();
        }
        public (List<Patente> patentesAsignadas, List<Patente> patentesDisponibles)
            CargarPatentesDeFamilia(Guid idFamilia)
        {
            var todasPat = _repo.GetAll();
            var patFamilia = FamiliaPatenteRepository.Current.GetByFamilia(idFamilia);
            var idsPatAsig = new HashSet<Guid>(patFamilia.Select(p => p.idPatente));
            var asignadas = todasPat.Where(p => idsPatAsig.Contains(p.idPatente)).ToList();
            var disponibles = todasPat.Where(p => !idsPatAsig.Contains(p.idPatente)).ToList();
            return (asignadas, disponibles);
        }
    }
}
