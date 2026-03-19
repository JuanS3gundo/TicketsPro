using Services.Dao.Contracts;
using Services.DomainModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Services.Implementations
{
    public sealed class BitacoraRepository : IBitacora
    {
        private static readonly Lazy<BitacoraRepository> _instance =
            new Lazy<BitacoraRepository>(() => new BitacoraRepository());
        public static BitacoraRepository Instance => _instance.Value;
        private readonly string _connectionString;
        private BitacoraRepository()
        {
            _connectionString = ConfigurationManager.ConnectionStrings["ServicesPP"].ConnectionString;
        }
        public void Agregar(Bitacora bitacora)
        {
            Agregar(bitacora, null);
        }
        public void Agregar(Bitacora bitacora, object uowAdapter)
        {
            var builder = new SqlConnectionStringBuilder(_connectionString);
            string dbName = builder.InitialCatalog;
            string tableName = string.IsNullOrEmpty(dbName) ? "Bitacora" : $"[{dbName}].[dbo].[Bitacora]";
            string query = $@"
                INSERT INTO {tableName}
                    (Id, Fecha, Usuario, Accion, Detalle, Nivel, EquipoId, TicketId)
                VALUES
                    (@Id, @Fecha, @Usuario, @Accion, @Detalle, @Nivel, @EquipoId, @TicketId)";
            var parametros = new Dictionary<string, object>
            {
                { "Id", bitacora.Id },
                { "Fecha", bitacora.Fecha },
                { "Usuario", bitacora.Usuario ?? "Sistema" },
                { "Accion", bitacora.Accion ?? "Accion no especificada" },
                { "Detalle", bitacora.Detalle ?? string.Empty },
                { "Nivel", bitacora.Nivel ?? "Info" },
                { "EquipoId", bitacora.EquipoId.HasValue ? (object)bitacora.EquipoId.Value : DBNull.Value },
                { "TicketId", bitacora.TicketId.HasValue ? (object)bitacora.TicketId.Value : DBNull.Value }
            };
            if (uowAdapter != null)
            {
                // Usar reflection para llamar a ExecuteCommand sin depender del tipo IUnitOfWorkAdapter de DAL
                var metod = uowAdapter.GetType().GetMethod("ExecuteCommand");
                if (metod != null)
                {
                    metod.Invoke(uowAdapter, new object[] { query, parametros });
                }
            }
            else
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    using (var command = new SqlCommand(query, connection))
                    {
                        foreach (var p in parametros)
                        {
                            command.Parameters.AddWithValue("@" + p.Key, p.Value);
                        }
                        connection.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        private void MapBitacora(SqlDataReader reader, Bitacora bitacora)
        {
            int col = 0;
            bitacora.Id = reader.GetGuid(col++);           
            bitacora.Fecha = reader.GetDateTime(col++);     
            bitacora.Usuario = reader.GetString(col++);     
            bitacora.Accion = reader.GetString(col++);      
            bitacora.Detalle = reader.GetString(col++);     
            bitacora.Nivel = reader.GetString(col++);       
            if (!reader.IsDBNull(col))
                bitacora.EquipoId = reader.GetInt32(col);
            col++; 
            if (!reader.IsDBNull(col))
                bitacora.TicketId = reader.GetGuid(col);
            col++; 
        }
        public List<Bitacora> ObtenerPorUsuario(string usuario)
        {
            var lista = new List<Bitacora>();
            const string query = @"
                SELECT Id, Fecha, Usuario, Accion, Detalle, Nivel, EquipoId, TicketId
                FROM Bitacora
                WHERE Usuario = @Usuario
                ORDER BY Fecha DESC";
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Usuario", usuario);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var bitacora = new Bitacora();
                            MapBitacora(reader, bitacora);
                            lista.Add(bitacora);
                        }
                    }
                }
                return lista;
            }
        }
        public List<Bitacora> ObtenerTodos(int pagina = 1, int registrosPorPagina = 100)
        {
            if (pagina < 1) pagina = 1;
            if (registrosPorPagina < 1) registrosPorPagina = 100;
            if (registrosPorPagina > 1000) registrosPorPagina = 1000; 
            int offset = (pagina - 1) * registrosPorPagina;
            const string query = @"
                SELECT Id, Fecha, Usuario, Accion, Detalle, Nivel, EquipoId, TicketId
                FROM Bitacora
                ORDER BY Fecha DESC
                OFFSET @Offset ROWS
                FETCH NEXT @PageSize ROWS ONLY";
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Offset", offset);
                    command.Parameters.AddWithValue("@PageSize", registrosPorPagina);
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        var bitacoras = new List<Bitacora>();
                        while (reader.Read())
                        {
                            var bitacora = new Bitacora();
                            MapBitacora(reader, bitacora);
                            bitacoras.Add(bitacora);
                        }
                        return bitacoras;
                    }
                }
            }
        }
        public List<Bitacora> ObtenerPorFiltros(DateTime? fechaDesde, DateTime? fechaHasta, string usuario, string nivel, string accion, int topN = 1000)
        {
            var lista = new List<Bitacora>();
            if (topN < 1) topN = 1000;
            if (topN > 10000) topN = 10000; 
            string query = $@"
                SELECT TOP (@TopN) Id, Fecha, Usuario, Accion, Detalle, Nivel, EquipoId, TicketId
                FROM Bitacora
                WHERE 1=1 ";
            if (fechaDesde.HasValue)
                query += " AND Fecha >= @FechaDesde";
            if (fechaHasta.HasValue)
                query += " AND Fecha <= @FechaHasta";
            if (!string.IsNullOrWhiteSpace(usuario))
                query += " AND Usuario LIKE @Usuario";
            if (!string.IsNullOrWhiteSpace(nivel))
                query += " AND Nivel = @Nivel";
            if (!string.IsNullOrWhiteSpace(accion))
                query += " AND Accion LIKE @Accion";
            query += " ORDER BY Fecha DESC";
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TopN", topN);
                    if (fechaDesde.HasValue)
                        command.Parameters.AddWithValue("@FechaDesde", fechaDesde.Value);
                    if (fechaHasta.HasValue)
                        command.Parameters.AddWithValue("@FechaHasta", fechaHasta.Value);
                    if (!string.IsNullOrWhiteSpace(usuario))
                        command.Parameters.AddWithValue("@Usuario", $"%{usuario}%");
                    if (!string.IsNullOrWhiteSpace(nivel))
                        command.Parameters.AddWithValue("@Nivel", nivel);
                    if (!string.IsNullOrWhiteSpace(accion))
                        command.Parameters.AddWithValue("@Accion", $"%{accion}%");
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var bitacora = new Bitacora();
                            MapBitacora(reader, bitacora);
                            lista.Add(bitacora);
                        }
                    }
                }
                return lista;
            }
        }
        public List<Bitacora> ObtenerPorEquipoId(int equipoId)
        {
            var lista = new List<Bitacora>();
            const string query = @"
                SELECT Id, Fecha, Usuario, Accion, Detalle, Nivel, EquipoId, TicketId
                FROM Bitacora
                WHERE (EquipoId = @EquipoId OR Detalle LIKE @DetalleBusqueda)
                ORDER BY Fecha DESC";
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@EquipoId", equipoId);
                    command.Parameters.AddWithValue("@DetalleBusqueda", $"%al equipo con codigo '{equipoId}'%");
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var bitacora = new Bitacora();
                            MapBitacora(reader, bitacora);
                            lista.Add(bitacora);
                        }
                    }
                }
                return lista;
            }
        }
    }
}
