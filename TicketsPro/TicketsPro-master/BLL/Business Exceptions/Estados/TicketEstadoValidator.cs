using System;
using System.Collections.Generic;
using System.Linq;
using Services.BLL;

namespace BLL.Exceptions
{
    internal static class TicketEstadoValidator
    {
        private static readonly Dictionary<string, HashSet<string>> TransicionesPermitidas =
            new Dictionary<string, HashSet<string>>(StringComparer.OrdinalIgnoreCase)
        {
            ["Nuevo"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Abierto", "Cancelado" },
            ["Abierto"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "En Progreso", "Resuelto", "Cancelado" },
            ["En Progreso"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Resuelto", "Cancelado" },
            ["Resuelto"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase) { "Cerrado", "En Progreso" },
            ["Cerrado"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase),
            ["Cancelado"] = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        };

        // Diccionario de transiciones prohibidas y sus razones específicas
        private static readonly Dictionary<(string Origen, string Destino), string> RazonesProhibicion =
            new Dictionary<(string, string), string>(new TransicionComparer())
        {
            // Estados terminales
            [("Cerrado", "")] = "Ticket_Transicion_EstadoTerminalCerrado",
            [("Cancelado", "")] = "Ticket_Transicion_EstadoTerminalCancelado",

            // Desde Nuevo
            [("Nuevo", "En Progreso")] = "Ticket_Transicion_NuevoAEnProgreso",
            [("Nuevo", "Resuelto")] = "Ticket_Transicion_NuevoAResuelto",
            [("Nuevo", "Cerrado")] = "Ticket_Transicion_NuevoACerrado",

            // Desde Abierto
            [("Abierto", "Nuevo")] = "Ticket_Transicion_AbiertoANuevo",
            [("Abierto", "Cerrado")] = "Ticket_Transicion_AbiertoACerrado",

            // Desde En Progreso
            [("En Progreso", "Nuevo")] = "Ticket_Transicion_EnProgresoANuevo",
            [("En Progreso", "Abierto")] = "Ticket_Transicion_EnProgresoAAbierto",
            [("En Progreso", "Cerrado")] = "Ticket_Transicion_EnProgresoACerrado",

            // Desde Resuelto
            [("Resuelto", "Nuevo")] = "Ticket_Transicion_ResueltoANuevo",
            [("Resuelto", "Abierto")] = "Ticket_Transicion_ResueltoAAbierto",
            [("Resuelto", "Cancelado")] = "Ticket_Transicion_ResueltoACancelado"
        };

        public static void ValidarYLanzarSiInvalida(string estadoActual, string estadoDestino)
        {
            estadoActual = string.IsNullOrWhiteSpace(estadoActual)
                ? LanguageBLL.Translate("Ticket_Update_EstadoDefaultNuevo")
                : estadoActual;

            if (string.IsNullOrWhiteSpace(estadoDestino))
                throw new ArgumentException(LanguageBLL.Translate("Ticket_Error_EstadoDestinoVacio"));

            if (estadoActual.Equals(estadoDestino, StringComparison.OrdinalIgnoreCase))
                return;

            if (!TransicionesPermitidas.ContainsKey(estadoActual))
                return;

            if (!TransicionesPermitidas[estadoActual].Contains(estadoDestino))
            {
                string razon = ObtenerRazonProhibicion(estadoActual, estadoDestino);
                throw new TransicionEstadoInvalidaException(estadoActual, estadoDestino, razon);
            }
        }

        public static bool EsTransicionPermitida(string estadoActual, string estadoDestino)
        {
            try
            {
                ValidarYLanzarSiInvalida(estadoActual, estadoDestino);
                return true;
            }
            catch (TransicionEstadoInvalidaException)
            {
                return false;
            }
        }

        public static IEnumerable<string> ObtenerEstadosPermitidos(string estadoActual)
        {
            if (string.IsNullOrWhiteSpace(estadoActual))
                estadoActual = LanguageBLL.Translate("Ticket_Update_EstadoDefaultNuevo");

            return TransicionesPermitidas.ContainsKey(estadoActual)
                ? TransicionesPermitidas[estadoActual]
                : Enumerable.Empty<string>();
        }

        private static string ObtenerRazonProhibicion(string estadoActual, string estadoDestino)
        {
            // Buscar razón específica para estado terminal
            if (RazonesProhibicion.TryGetValue((estadoActual, ""), out string razonTerminal))
                return LanguageBLL.Translate(razonTerminal);

            // Buscar razón específica para la transición
            if (RazonesProhibicion.TryGetValue((estadoActual, estadoDestino), out string razonEspecifica))
                return LanguageBLL.Translate(razonEspecifica);

            // Razón genérica
            return string.Format(
                LanguageBLL.Translate("Ticket_Transicion_NoPermitida"),
                estadoActual,
                estadoDestino);
        }

        // Comparer que ignora mayúsculas/minúsculas para tuplas
        private class TransicionComparer : IEqualityComparer<(string, string)>
        {
            public bool Equals((string, string) x, (string, string) y)
            {
                return string.Equals(x.Item1, y.Item1, StringComparison.OrdinalIgnoreCase) &&
                       string.Equals(x.Item2, y.Item2, StringComparison.OrdinalIgnoreCase);
            }

            public int GetHashCode((string, string) obj)
            {
                unchecked
                {
                    int hash = 17;
                    hash = hash * 31 + (obj.Item1?.ToUpperInvariant()?.GetHashCode() ?? 0);
                    hash = hash * 31 + (obj.Item2?.ToUpperInvariant()?.GetHashCode() ?? 0);
                    return hash;
                }
            }
        }
    }
}
