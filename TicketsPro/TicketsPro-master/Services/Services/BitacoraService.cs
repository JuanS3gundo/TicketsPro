using Services.DomainModel;
using Services.Implementations;
using System;
using System.Collections.Generic;
namespace Services.Services
{
    public static class BitacoraService
    {
        public static void RegistrarEvento(Bitacora bitacora)
        {
            try
            {
                if (bitacora.Accion == null)
                {
                    bitacora.Accion = "Accion no especificada";
                }
                bitacora.Fecha = DateTime.Now;
                bitacora.Id = Guid.NewGuid();
                BitacoraRepository.Instance.Agregar(bitacora);
            }
            catch (Exception ex)
            {
                try
                {
                    LoggerService.Instance.LogError(
                        $"ERROR al registrar en Bitacora: {bitacora.Accion ?? "Sin accion"}",
                        ex
                    );
                }
                catch
                {
                }
            }
        }
        public static List<Bitacora> ObtenerBitacora(int pagina = 1, int registrosPorPagina = 100)
        {
            try
            {
                return BitacoraRepository.Instance.ObtenerTodos(pagina, registrosPorPagina);
            }
            catch (Exception ex)
            {
                LoggerService.Instance.LogError("Error al obtener la bitacora", ex);
                return new List<Bitacora>();
            }
        }
        public static List<Bitacora> ObtenerBitacoraPorFiltros(DateTime? fechaDesde, DateTime? fechaHasta, string usuario, string nivel, string accion, int topN = 1000)
        {
            try
            {
                return BitacoraRepository.Instance.ObtenerPorFiltros(fechaDesde, fechaHasta, usuario, nivel, accion, topN);
            }
            catch (Exception ex)
            {
                LoggerService.Instance.LogError("Error al obtener la bitacora por filtros", ex);
                return new List<Bitacora>();
            }
        }
        public static List<Bitacora> ObtenerBitacoraPorEquipoId(int equipoId)
        {
            try
            {
                return BitacoraRepository.Instance.ObtenerPorEquipoId(equipoId);
            }
            catch (Exception ex)
            {
                LoggerService.Instance.LogError($"Error al obtener la bitacora por EquipoId: {equipoId}", ex);
                return new List<Bitacora>();
            }
        }
        public static List<Bitacora> ObtenerBitacoraPorUsuario(string usuario)
        {
            try
            {
                if (usuario == null)
                {
                    LoggerService.Instance.LogWarning("ObtenerBitacoraPorUsuario llamado con usuario nulo");
                    return new List<Bitacora>();
                }
                return BitacoraRepository.Instance.ObtenerPorUsuario(usuario);
            }
            catch (Exception ex)
            {
                LoggerService.Instance.LogError($"Error al obtener la bitacora por usuario: {usuario}", ex);
                return new List<Bitacora>();
            }
        }
    }
}
