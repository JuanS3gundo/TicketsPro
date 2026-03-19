using Services.BLL;
using Services.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    /// <summary>
    /// Servicio de exportación a Excel/CSV.
    /// Opera con DTOs propios de Services (EquipoInformaticoDto, InventarioItemDto)
    /// para no depender de Entity.Domain.
    /// El llamador (BLL/Controller) es responsable de mapear las entidades a estos DTOs.
    /// </summary>
    public class ExportarExcelService
    {
        private static ExportarExcelService _instance;
        public static ExportarExcelService Instance => _instance ?? (_instance = new ExportarExcelService());
        private ExportarExcelService() { }

        public string ExportarEquipos(List<EquipoInformaticoDto> equipos, List<InventarioItemDto> items)
        {
            if (equipos == null || !equipos.Any())
            {
                return null;
            }

            var csv = new StringBuilder();

            string headerID       = LanguageBLL.Translate("Inventario_Export_Header_ID");
            string headerModelo   = LanguageBLL.Translate("Inventario_Export_Header_Modelo");
            string headerNroInv   = LanguageBLL.Translate("Inventario_Export_Header_NroInventario");
            string headerTipo     = LanguageBLL.Translate("Inventario_Export_Header_Tipo");
            string headerUbicacion= LanguageBLL.Translate("Inventario_Export_Header_Ubicacion");
            string headerProc     = LanguageBLL.Translate("Inventario_Export_Header_Procesador");
            string headerRAM      = LanguageBLL.Translate("Inventario_Export_Header_RAM");
            string headerROM      = LanguageBLL.Translate("Inventario_Export_Header_ROM");
            string headerUser     = LanguageBLL.Translate("Inventario_Export_Header_Usuario");
            string sinProc        = LanguageBLL.Translate("Sin_Procesador");
            string sinUser        = LanguageBLL.Translate("Inventario_Gestion_Equipo_Usuario_Sin");

            csv.AppendLine($"{headerID};{headerModelo};{headerNroInv};{headerTipo};{headerUbicacion};{headerProc};{headerRAM} (GB);{headerROM} (GB);{headerUser}");

            foreach (var eq in equipos)
            {
                var itemsEq = items.Where(i => i.EquipoAsignadoId == eq.Id).ToList();

                var proc = itemsEq
                    .FirstOrDefault(i => i.CategoriaNombre?.Equals("Procesador", System.StringComparison.OrdinalIgnoreCase) ?? false)
                    ?.Nombre ?? sinProc;

                var ram = itemsEq
                    .Where(i => i.CategoriaNombre?.Equals("RAM", System.StringComparison.OrdinalIgnoreCase) ?? false)
                    .Sum(i => i.Valor);

                var rom = itemsEq
                    .Where(i => i.CategoriaNombre?.Equals("ROM", System.StringComparison.OrdinalIgnoreCase) ?? false)
                    .Sum(i => i.Valor);

                string usuarioAsignado = string.IsNullOrWhiteSpace(eq.UsuarioAsignadoUserName)
                    ? sinUser
                    : eq.UsuarioAsignadoUserName;

                string tipoEquipo     = eq.TipoEquipoNombre     ?? "Sin Tipo";
                string ubicacionEquipo= eq.UbicacionEquipoNombre ?? "Sin Ubicación";

                csv.AppendLine($"{eq.Id};" +
                               $"{eq.ModeloEquipo};" +
                               $"{eq.NroInventario};" +
                               $"{tipoEquipo};" +
                               $"{ubicacionEquipo};" +
                               $"{proc};" +
                               $"{ram:F2};" +
                               $"{rom:F2};" +
                               $"{usuarioAsignado}");
            }

            return csv.ToString();
        }
    }
}
