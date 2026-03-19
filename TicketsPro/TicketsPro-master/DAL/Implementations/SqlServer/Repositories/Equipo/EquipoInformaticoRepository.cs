using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class EquipoInformaticoRepository : BaseRepository<EquipoInformatico>, IEquipoInformaticoRepository
    {
        private const string SelectAll = @"
SELECT
    e.Id, e.NroInventario, e.ModeloEquipo, e.TipoEquipoId,
    e.UbicacionEquipoId, e.FechaCreacion, e.Estado, e.IdUsuarioAsignado,
    te.Nombre AS TipoEquipoNombre, te.Descripcion AS TipoEquipoDescripcion,
    ue.Nombre AS UbicacionEquipoNombre, ue.Descripcion AS UbicacionEquipoDescripcion,
    u.UserName AS UsuarioNombre
FROM EquipoInformatico e
    LEFT JOIN TipoEquipo te ON e.TipoEquipoId = te.Id
    LEFT JOIN UbicacionEquipo ue ON e.UbicacionEquipoId = ue.Id
    LEFT JOIN Usuario u ON e.IdUsuarioAsignado = u.IdUsuario";
        public EquipoInformaticoRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public int InsertAndReturnId(EquipoInformatico entity)
        {
            if (entity.TipoEquipo == null)
                throw new InvalidOperationException("EquipoInformatico.TipoEquipo no puede ser null al persistir");
            if (entity.UbicacionEquipo == null)
                throw new InvalidOperationException("EquipoInformatico.UbicacionEquipo no puede ser null al persistir");
            const string sql = @"
INSERT INTO EquipoInformatico
(NroInventario, ModeloEquipo, TipoEquipoId, UbicacionEquipoId, FechaCreacion, Estado, IdUsuarioAsignado)
VALUES
(@NroInventario, @ModeloEquipo, @TipoEquipoId, @UbicacionEquipoId, @FechaCreacion, @Estado, @IdUsuarioAsignado);
SELECT CAST(SCOPE_IDENTITY() AS INT);";
            var result = ExecuteScalar(sql, cmd =>
            {
                AddParam(cmd, "@NroInventario", entity.NroInventario);
                AddParam(cmd, "@ModeloEquipo", entity.ModeloEquipo);
                AddParam(cmd, "@TipoEquipoId", entity.TipoEquipo.Id);
                AddParam(cmd, "@UbicacionEquipoId", entity.UbicacionEquipo.Id);
                AddParam(cmd, "@FechaCreacion", entity.FechaCreacion);
                AddParam(cmd, "@Estado", entity.Estado ?? "Activo");
                AddParam(cmd, "@IdUsuarioAsignado", entity.UsuarioAsignado?.IdUsuario);
            });
            entity.Id = Convert.ToInt32(result);
            return entity.Id;
        }
        public void Add(EquipoInformatico entity) => InsertAndReturnId(entity);
        public IEnumerable<EquipoInformatico> GetAll()
        {
            return QueryList(SelectAll, MapFromReader);
        }
        public EquipoInformatico GetById(int id)
        {
            return QuerySingle(
                SelectAll + " WHERE e.Id=@Id",
                cmd => AddParam(cmd, "@Id", id),
                MapFromReader);
        }
        public EquipoInformatico GetByInventario(string nroInventario)
        {
            return QuerySingle(
                SelectAll + " WHERE e.NroInventario = @NroInventario",
                cmd => AddParam(cmd, "@NroInventario", nroInventario),
                MapFromReader);
        }
        public void Update(EquipoInformatico entity)
        {
            if (entity.TipoEquipo == null)
                throw new InvalidOperationException("EquipoInformatico.TipoEquipo no puede ser null al persistir");
            if (entity.UbicacionEquipo == null)
                throw new InvalidOperationException("EquipoInformatico.UbicacionEquipo no puede ser null al persistir");
            const string sql = @"
UPDATE EquipoInformatico
SET ModeloEquipo = @ModeloEquipo,
    NroInventario = @NroInventario,
    TipoEquipoId = @TipoEquipoId,
    UbicacionEquipoId = @UbicacionEquipoId,
    Estado = @Estado,
    IdUsuarioAsignado = @IdUsuarioAsignado
WHERE Id = @Id";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@ModeloEquipo", entity.ModeloEquipo);
                AddParam(cmd, "@NroInventario", entity.NroInventario);
                AddParam(cmd, "@TipoEquipoId", entity.TipoEquipo.Id);
                AddParam(cmd, "@UbicacionEquipoId", entity.UbicacionEquipo.Id);
                AddParam(cmd, "@Id", entity.Id);
                AddParam(cmd, "@Estado", entity.Estado ?? "Activo");
                AddParam(cmd, "@IdUsuarioAsignado", entity.UsuarioAsignado?.IdUsuario);
            });
        }
        public void Remove(EquipoInformatico entity)
        {
            ExecuteNonQuery("DELETE FROM EquipoInformatico WHERE Id=@Id",
                cmd => AddParam(cmd, "@Id", entity.Id));
        }
        
        private EquipoInformatico MapFromReader(SqlDataReader reader)
        {
            try
            {
                var tipoEquipoId = reader.GetGuid(reader.GetOrdinal("TipoEquipoId"));
                var ubicacionEquipoId = reader.GetGuid(reader.GetOrdinal("UbicacionEquipoId"));
                var idUsuarioAsignado = ReadNullableGuid(reader, "IdUsuarioAsignado");
                return new EquipoInformatico
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                NroInventario = ReadString(reader, "NroInventario"),
                ModeloEquipo = ReadString(reader, "ModeloEquipo"),
                FechaCreacion = reader.GetDateTime(reader.GetOrdinal("FechaCreacion")),
                Estado = ReadString(reader, "Estado", "Activo"),
                TipoEquipo = new TipoEquipo
                {
                    Id = tipoEquipoId,
                    Nombre = ReadString(reader, "TipoEquipoNombre"),
                    Descripcion = ReadString(reader, "TipoEquipoDescripcion")
                },
                UbicacionEquipo = new UbicacionEquipo
                {
                    Id = ubicacionEquipoId,
                    Nombre = ReadString(reader, "UbicacionEquipoNombre"),
                    Descripcion = ReadString(reader, "UbicacionEquipoDescripcion")
                },
                UsuarioAsignado = idUsuarioAsignado.HasValue
                    ? new Usuario
                    {
                        IdUsuario = idUsuarioAsignado.Value,
                        UserName = ReadString(reader, "UsuarioNombre")
                    }
                    : null
                };
            }
            catch (IndexOutOfRangeException ex)
            {
                // Listar todas las columnas disponibles para debugging
                var columnas = new System.Text.StringBuilder("Columnas disponibles: ");
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    columnas.Append(reader.GetName(i)).Append(", ");
                }
                throw new InvalidOperationException(
                    $"Error al mapear EquipoInformatico. {columnas}. Error original: {ex.Message}",
                    ex);
            }
        }
    }
}
