using DAL.Contracts.Repositories;
using Entity.Domain;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
namespace DAL.Implementations.SqlServer.Repositories
{
    public class InventarioItemRepository : BaseRepository<InventarioItem>, IInventarioItemRepository
    {
        private const string SelectBase = @"
SELECT I.*,
       C.Nombre AS CategoriaNombre, C.Descripcion AS CategoriaDescripcion,
       UE.Nombre AS UbicacionEquipoNombre, UE.Descripcion AS UbicacionEquipoDescripcion,
       E.NroInventario AS EquipoNroInventario, E.ModeloEquipo AS EquipoModelo
FROM InventarioItem I
    LEFT JOIN CategoriaItem C ON I.CategoriaItemId = C.Id
    LEFT JOIN UbicacionEquipo UE ON I.UbicacionEquipoId = UE.Id
    LEFT JOIN EquipoInformatico E ON I.EquipoId = E.Id";
        public InventarioItemRepository(SqlConnection connection, SqlTransaction transaction)
            : base(connection, transaction) { }
        public void Add(InventarioItem item)
        {
            if (item.CategoriaItem == null)
                throw new InvalidOperationException("InventarioItem.CategoriaItem no puede ser null al persistir");
            const string sql = @"
INSERT INTO InventarioItem
(CodigoInventario, Nombre, CategoriaItemId, Descripcion, Cantidad, Valor, Unidad, UbicacionEquipoId, FechaIngreso, Disponible, EquipoId)
VALUES (@Codigo, @Nombre, @CategoriaItemId, @Descripcion, @Cantidad, @Valor, @Unidad, @UbicacionEquipoId, @Fecha, @Disponible, @EquipoId)";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@Codigo", item.CodigoInventario);
                AddParam(cmd, "@Nombre", item.Nombre);
                AddParam(cmd, "@CategoriaItemId", item.CategoriaItem.Id);
                AddParam(cmd, "@Descripcion", item.Descripcion);
                AddParam(cmd, "@Cantidad", item.Cantidad);
                AddParam(cmd, "@Valor", item.Valor);
                AddParam(cmd, "@Unidad", item.Unidad ?? "");
                AddParam(cmd, "@UbicacionEquipoId", item.UbicacionEquipo?.Id);
                AddParam(cmd, "@Fecha", item.FechaIngreso);
                AddParam(cmd, "@Disponible", item.Disponible);
                AddParam(cmd, "@EquipoId", item.EquipoAsignado?.Id);
            });
        }
        public void Update(InventarioItem item)
        {
            if (item.CategoriaItem == null)
                throw new InvalidOperationException("InventarioItem.CategoriaItem no puede ser null al persistir");
            const string sql = @"
UPDATE InventarioItem
SET Nombre=@Nombre, CategoriaItemId=@CategoriaItemId, Descripcion=@Descripcion,
    Cantidad=@Cantidad, Valor=@Valor, Unidad=@Unidad, UbicacionEquipoId=@UbicacionEquipoId,
    FechaIngreso=@FechaIngreso, Disponible=@Disponible, EquipoId=@EquipoId
WHERE Id=@Id";
            ExecuteNonQuery(sql, cmd =>
            {
                AddParam(cmd, "@Id", item.Id);
                AddParam(cmd, "@Nombre", item.Nombre);
                AddParam(cmd, "@CategoriaItemId", item.CategoriaItem.Id);
                AddParam(cmd, "@Descripcion", item.Descripcion);
                AddParam(cmd, "@Cantidad", item.Cantidad);
                AddParam(cmd, "@Valor", item.Valor);
                AddParam(cmd, "@Unidad", item.Unidad ?? "");
                AddParam(cmd, "@UbicacionEquipoId", item.UbicacionEquipo?.Id);
                AddParam(cmd, "@FechaIngreso", item.FechaIngreso);
                AddParam(cmd, "@Disponible", item.Disponible);
                AddParam(cmd, "@EquipoId", item.EquipoAsignado?.Id);
            });
        }
        public void Remove(InventarioItem item)
        {
            ExecuteNonQuery("DELETE FROM InventarioItem WHERE Id=@Id",
                cmd => AddParam(cmd, "@Id", item.Id));
        }
        public IEnumerable<InventarioItem> GetAll()
        {
            var lista = new List<InventarioItem>();
            using (var cmd = CreateCommand(SelectBase))
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var item = MapItem(reader);
                    MapRelaciones(reader, item);
                    lista.Add(item);
                }
            }
            return lista;
        }
        public IEnumerable<InventarioItem> GetDisponibles()
        {
            return QueryList("SELECT * FROM InventarioItem WHERE Disponible = 1",
                MapItem);
        }
        public IEnumerable<InventarioItem> GetByCategoriaYDisponibilidad(string categoria, bool disponible)
        {
            const string sql = @"
SELECT I.*
FROM InventarioItem I
INNER JOIN CategoriaItem C ON I.CategoriaItemId = C.Id
WHERE C.Nombre LIKE '%' + @Categoria + '%'
AND I.Disponible = @Disponible";
            return QueryList(sql,
                cmd =>
                {
                    AddParam(cmd, "@Categoria", categoria);
                    AddParam(cmd, "@Disponible", disponible);
                },
                MapItem);
        }
        public void AsignarAEquipo(int itemId, int equipoId)
        {
            ExecuteNonQuery("UPDATE InventarioItem SET EquipoId = @EquipoId, Disponible = 0 WHERE Id = @ItemId",
                cmd =>
                {
                    AddParam(cmd, "@EquipoId", equipoId);
                    AddParam(cmd, "@ItemId", itemId);
                });
        }
        public void QuitarDeEquipo(int itemId)
        {
            ExecuteNonQuery("UPDATE InventarioItem SET EquipoId = NULL, Disponible = 1 WHERE Id = @ItemId",
                cmd => AddParam(cmd, "@ItemId", itemId));
        }
        public InventarioItem GetById(int id)
        {
            const string sql = @"
SELECT I.*,
       C.Nombre AS CategoriaNombre, C.Descripcion AS CategoriaDescripcion,
       UE.Nombre AS UbicacionEquipoNombre, UE.Descripcion AS UbicacionEquipoDescripcion,
       E.NroInventario AS EquipoNroInventario, E.ModeloEquipo AS EquipoModelo
FROM InventarioItem I
    LEFT JOIN CategoriaItem C ON I.CategoriaItemId = C.Id
    LEFT JOIN UbicacionEquipo UE ON I.UbicacionEquipoId = UE.Id
    LEFT JOIN EquipoInformatico E ON I.EquipoId = E.Id
WHERE I.Id = @Id";
            using (var cmd = CreateCommand(sql))
            {
                AddParam(cmd, "@Id", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var item = MapItem(reader);
                        
                        if (item.EquipoAsignado != null && !reader.IsDBNull(reader.GetOrdinal("EquipoNroInventario")))
                        {
                            item.EquipoAsignado.NroInventario = ReadString(reader, "EquipoNroInventario");
                            item.EquipoAsignado.ModeloEquipo = ReadString(reader, "EquipoModelo");
                        }
                        return item;
                    }
                    return null;
                }
            }
        }
        public bool DeleteById(int id)
        {
            int rowsAffected = ExecuteNonQuery("DELETE FROM InventarioItem WHERE Id=@Id",
                cmd => AddParam(cmd, "@Id", id));
            return rowsAffected > 0;
        }
        
        private InventarioItem MapItem(SqlDataReader reader)
        {
            var categoriaItemId = reader.GetGuid(reader.GetOrdinal("CategoriaItemId"));
            var ubicacionEquipoId = ReadNullableGuid(reader, "UbicacionEquipoId");
            var equipoId = ReadNullableInt(reader, "EquipoId");
            return new InventarioItem
            {
                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                CodigoInventario = reader.GetString(reader.GetOrdinal("CodigoInventario")),
                Nombre = reader.GetString(reader.GetOrdinal("Nombre")),
                Descripcion = ReadString(reader, "Descripcion", null),
                Cantidad = reader.GetInt32(reader.GetOrdinal("Cantidad")),
                Valor = ReadDecimal(reader, "Valor"),
                Unidad = ReadString(reader, "Unidad"),
                FechaIngreso = reader.GetDateTime(reader.GetOrdinal("FechaIngreso")),
                Disponible = reader.GetBoolean(reader.GetOrdinal("Disponible")),
                CategoriaItem = new CategoriaItem
                {
                    Id = categoriaItemId,
                    Nombre = ReadString(reader, "CategoriaNombre"),
                    Descripcion = ReadString(reader, "CategoriaDescripcion")
                },
                UbicacionEquipo = ubicacionEquipoId.HasValue
                    ? new UbicacionEquipo
                    {
                        Id = ubicacionEquipoId.Value,
                        Nombre = ReadString(reader, "UbicacionEquipoNombre"),
                        Descripcion = ReadString(reader, "UbicacionEquipoDescripcion")
                    }
                    : null,
                EquipoAsignado = equipoId.HasValue
                    ? new EquipoInformatico
                    {
                        Id = equipoId.Value,
                        NroInventario = ReadString(reader, "EquipoNroInventario"),
                        ModeloEquipo = ReadString(reader, "EquipoModelo")
                    }
                    : null
            };
        }
        private void MapRelaciones(SqlDataReader reader, InventarioItem item)
        {
            if (item.EquipoAsignado != null && !reader.IsDBNull(reader.GetOrdinal("EquipoNroInventario")))
            {
                item.EquipoAsignado.NroInventario = ReadString(reader, "EquipoNroInventario");
                item.EquipoAsignado.ModeloEquipo = ReadString(reader, "EquipoModelo");
            }
        }
    }
}
