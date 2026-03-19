using Services.DomainModel;
using System;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;

namespace Services.Services
{
    public static class IntegrityService
    {
        private static byte[] GetKey()
        {
            var keyString = ConfigurationManager.AppSettings["IntegrityKey"];
            if (string.IsNullOrWhiteSpace(keyString))
                throw new InvalidOperationException("No se encontro la clave de integridad en App.config");
            return Encoding.UTF8.GetBytes(keyString);
        }

        public static string CrearHash(string data)
        {
            var key = GetKey();
            using (var hmac = new HMACSHA256(key))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return Convert.ToBase64String(hash);
            }
        }

        public static string ConstruirCadena(ITicketIntegridad t)
        {
            return
                $"Id={t.Id}|" +
                $"Titulo={t.Titulo}|" +
                $"Descripcion={t.Descripcion}|" +
                $"FechaApertura={t.FechaApertura:O}|" +
                $"CategoriaId={t.CategoriaId}|" +
                $"EstadoId={t.EstadoId}|" +
                $"UbicacionId={t.UbicacionId}|" +
                $"PrioridadId={t.PrioridadId}|" +
                $"PoliticaSLAId={t.PoliticaSLAId}|" +
                $"FechaVencimiento={t.FechaVencimiento:O}|" +
                $"CreadorUsuarioId={t.CreadorUsuarioId}|" +
                $"TecnicoAsignadoId={t.TecnicoAsignadoId}|" +
                $"EquipoAsignadoId={t.EquipoAsignadoId}";
        }

        public static void RecalcularHash(ITicketIntegridad t)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));
            string cadena = ConstruirCadena(t);
            t.IntegridadHash = CrearHash(cadena);
        }

        public static bool ValidarIntegridad(ITicketIntegridad t)
        {
            var cadena = ConstruirCadena(t);
            var hashRecalculado = CrearHash(cadena);
            return hashRecalculado == t.IntegridadHash;
        }
    }
}
